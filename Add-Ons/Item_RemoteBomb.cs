datablock ItemData(BombRItem)
{
	category = "Item";  // Mission editor category

	equipment = true;

	//its already a member of item namespace so dont break it
	//className = "Item"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./shapes/RCcontroller.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Remote Bomb";
	iconName = "./ItemIcons/BombR";
	doColorShift = true;
	colorShiftColor = "0.200 0.200 0.200 1.000";
	
	 // Dynamic properties defined by the scripts
	image = BombRImage;
	canDrop = true;
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(BombRImage)
{
   // Basic Item properties
   shapeFile = "./shapes/RCcontroller.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   rotation = eulerToMatrix("0 0 0");
   eyeOffset = "0 0 0";
   eyeRotation = eulerToMatrix("0 0 0");

   doColorShift = true;
	colorShiftColor = BombRItem.colorShiftColor;
   

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = false;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "ToolImage";

   // Projectile && Ammo.
   item = BombRItem;

   //melee particles shoot from eye node for consistancy
   melee = true;
   //raise your arm up or not
   armReady = true;

   doColorShift = true;
   colorShiftColor = BombRItem.colorShiftColor; //"0.200 0.200 0.200 1.000";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.5;
	stateTransitionOnTimeout[0]       = "Ready";


	stateName[1]                     = "Ready";
	stateScript[1]                  = "onUse";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;


	stateName[2]                    = "Fire";
	stateTransitionOnTriggerUp[2]	= "Ready";
	stateScript[2]                  = "onFire";
};

function BombRImage::onFire(%this, %obj, %slot)
{
	if(%obj.client.minigame == 0) 
	{
	return;
	}
	if(%obj.client.bombstage > 3)
	{
	%obj.client.bombstage = 0;
	}
	%obj.client.bombstage++;
	if(%obj.client.bombstage == 1)
	{
	%location = %obj.getposition();
	%player = %obj.client.player;
	%pmount = %player.getObjectMount();
		if(%pmount.getClassName() $= "WheeledVehicle"){
		%obj.client.bomb = new StaticShape()
		{
		position = %location;
		rotation = "1 0 0 0";
		scale = "1 1 1";
		dataBlock = "NoColBomb";
		};
		sendtoobject(%obj.client.bomb, %pmount);
		%pmount.bomb = %obj.client.bomb;
		%obj.client.bomb.car = %pmount;
		}else{
		%obj.client.bomb = new StaticShape()
		{
		position = %location;
		rotation = "1 0 0 0";
		scale = "1 1 1";
		dataBlock = "Bomb";
		};
		}
	%obj.client.bomb.owner = %obj.client;
	schedule(2500, 0, "Prepme", %obj.client.bomb);
	}



	if(%obj.client.bombstage == 2)
	{
		if(!isobject(%obj.client.bomb))
		{
		%obj.client.bombstage = 0;
		return;
		}
	if(%obj.client.bomb.preped != 1)
	{
	messageclient(%obj.client,"","Your bomb has not been prepped!");
	%obj.client.bombstage = 1;
	return;
	}
      	%obj.client.bombemitter = new ParticleEmitterNode(bomb2explosioner) {
      	position = %obj.client.bomb.getTransform();
      	rotation = "1 0 0 0";
      	scale = "1 1 1";
      	dataBlock = "bombParticleEmitterNode";
      	emitter = "bomb2ExplosionEmitter";
      	velocity = "1.0";
	};
	Schedule(1300,0,"Deletethis",%obj.client.bombemitter);
	bombradiusdamage(%obj.client.bomb, %obj.client.bomb.getposition(), 12, 100, Explosion, 10000, %obj.client);
	%obj.client.bombstage = 0;
	}
}

//TAKEN FROM RTB, modified (kind of a lot-ish)!! Don't expect it to be neat and tidy.
function bombradiusDamage(%sourceObject, %position, %radius, %damage, %damageType, %impulse, %rigger)
{
	%pos = VectorAdd(%position, "0 0" SPC 0.2);
	new Projectile()
	{
	dataBlock        = "BrickdestroyerProjectile";
	initialPosition  = %pos;
	initialVelocity  = "0 0 -500";
	sourceObject     = %rigger.player;
	client = %rigger;
	};
	%light = new ParticleEmitterNode(WhiteLight) {
	position = %sourceobject.getposition();
	rotation = "1 0 0 0";
	scale = "1 1 1";
	dataBlock = "bombParticleEmitterNode";
	emitter = "WhiteLightEmitter";
	velocity = "1.0";
	};
	schedule(100, 0, "deletethis", %sourceObject);
	schedule(100, 0, "deletethis", %light);
		InitContainerRadiusSearch(%position, %radius, $TypeMasks::ShapeBaseObjectType | $TypeMasks::FxBrickObjectType);
		%halfRadius = %radius / 2;
		while ((%targetObject = containerSearchNext()) != 0) {
		%coverage = calcExplosionCoverage(%position, %targetObject,
     	    	$TypeMasks::InteriorObjectType |  $TypeMasks::TerrainObjectType |
     	    	$TypeMasks::ForceFieldObjectType | $TypeMasks::VehicleObjectType);
     	 		if (%coverage == 0)
        			continue;
			%dist = containerSearchCurrRadiusDist();
		  	%distScale = (%dist < %halfRadius)? 1.0:
		      	   1.0 - ((%dist - %halfRadius) / %halfRadius);
			if(%targetObject.getClassname() $= "Player")
			{
				if(!miniGameCanDamage(%rigger.player, %targetobject)) return;
				if(%rigger.minigame.selfdamage == 0 && %targetobject == %rigger.player)
				{}else{
					if(%rigger.minigame.weapondamage == 1)
					{
						if(%targetobject.client.name !$= "" && %targetObject.getclassname() !$= "fxDTSBrick"){
						%targetObject.kill();
						messageAll('','%2 <Bitmap:Add-Ons/CI/bomb.png> %1',%targetobject.client.name,%rigger.name);
						}
					}
				}
			}
			if(%targetObject.getclassname() $= "WheeledVehicle" && %rigger.minigame.vehicleDamage == 1)
			{
			%targetObject.damage(%Targetobject, %targetobject.getposition(), 1000, Bomb);
			}
     			if (%impulse)
			{
			if(%targetObject.client.minigame || %targetObject.getclassname() $= "WheeledVehicle"){
        			%impulseVec = VectorSub(%targetObject.getWorldBoxCenter(), %position);
         			%impulseVec = VectorNormalize(%impulseVec);
         			%impulseVec = VectorScale(%impulseVec, %impulse * %distScale);
         			%targetObject.applyImpulse(%position, %impulseVec);
			}
      			}
    		}
}

datablock StaticShapeData(Bomb)
{
	category = "Static Shapes";   //Mission editor category
	item = Bomb;
	shapeFile = "./shapes/bomb.dts";
};

datablock StaticShapeData(NoColBomb)
{
	category = "Static Shapes";   //Mission editor category
	item = NocolBomb;
	shapeFile = "./shapes/Nocolbomb.dts";
};

datablock ParticleData(bomb2ExplosionParticle)
{
	dragCoefficient      = 0;
	gravityCoefficient   = 2.0;
	inheritedVelFactor   = 0.0;
	constantAcceleration = 0.0;
	lifetimeMS           = 1000;
	lifetimeVarianceMS   = 400;
	textureName          = "base/data/particles/star1";
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	colors[0]     = "0.5 0.5 0.0 1.0";
	colors[1]     = "0.65 0.0 0.0 1.0";
	sizes[0]      = 0.0;
	sizes[1]      = 8.0;
	useInvAlpha = true;
};

datablock ParticleData(WhiteLightParticle)
{
	dragCoefficient		= 3.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= 0.5;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 400;
	lifetimeVarianceMS	= 0;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= false;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "./shapes/cloud";

	// Interpolation variables
	colors[0]	= "1 2 1 1";
	colors[1]	= "1 2 1 1";
	sizes[0]	= 0;
	sizes[1]	= 20;
	times[0]	= 0.0;
	times[1]	= 0.1;
};

datablock ParticleEmitterData(WhiteLightEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   lifeTimeMS	   = 21;
   ejectionVelocity = 8;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "WhiteLightParticle";
};

datablock ParticleEmitterData(bomb2ExplosionEmitter)
{
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;
   ejectionVelocity = 10;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "bomb2ExplosionParticle";
};

datablock ParticleEmitterNodeData(bombParticleEmitterNode)
{
	timeMultiple = 1.0;   //time multiple must be between 0.01 and 100.0
};

function deletethis(%delete)
{
%delete.delete();
}

function sendtoobject(%send, %obj)
{
if(!isobject(%obj))
{
if(!isObject(%send)) return;
%send.delete();
return;
}
%send.settransform(%obj.gettransform());
%send.sendtoobj = schedule(100, 0,"Sendtoobject",%send,%obj);
}

datablock ProjectileData(BrickdestroyerProjectile)
{
 //projectileShapeName = "./shapes/RocketProjectile.dts";
   directDamage        = 0;
   directDamageType    = $DamageType::ArrowDirect;
   radiusDamage        = 0;
   damageRadius        = 0;
   radiusDamageType    = $DamageType::ArrowDirect;
   muzzleVelocity      = 0.1;
   velInheritFactor    = 1;
   armingDelay         = 0;
   lifetime            = 200;
   fadeDelay           = 170;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 1.0;
   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";
   brickExplosionRadius = 6;
   brickExplosionImpact = true;          //destroy a brick if we hit it directly?
   brickExplosionForce  = 30;             
   brickExplosionMaxVolume = 30;          //max volume of bricks that we can destroy
   brickExplosionMaxVolumeFloating = 60;
};

function Prepme(%bomb)
{
%bomb.preped = 1;
}
