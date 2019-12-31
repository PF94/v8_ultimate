$Pref::Server::Impulse_AllowVehicles = 1; //Allows vehicles to be used 

datablock AudioProfile(PulserHitSound)
{
   filename    = "./sound/pushBroomHit.wav";
   description = AudioClosest3d;
   preload = true;
};
datablock AudioProfile(PulserSwingSound)
{
   filename    = "./sound/pushBroomSwing.wav";
   description = AudioClosestLooping3d;
   preload = true;
};



//effects
datablock ParticleData(PulserSparkParticle)
{
   dragCoefficient      = 4;
   gravityCoefficient   = 1;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 400;
   lifetimeVarianceMS   = 300;
   textureName          = "base/data/particles/chunk";
	
	useInvAlpha = false;
	spinSpeed		= 150.0;
	spinRandomMin		= -150.0;
	spinRandomMax		= 150.0;

   colors[0]     = "0.30 0.10 0.0 0.0";
   colors[1]     = "0.30 0.10 0.0 0.5";
   colors[2]     = "0.30 0.10 0.0 0.0";
   sizes[0]      = 0.15;
   sizes[1]      = 0.15;
   sizes[2]      = 0.15;

   times[0]	= 0.1;
   times[1] = 0.5;
   times[2] = 1.0;

   useInvAlpha = true;
};

datablock ParticleEmitterData(PulserSparkEmitter)
{
	lifeTimeMS = 10;

   ejectionPeriodMS = 3;
   periodVarianceMS = 0;
   ejectionVelocity = 5;
   velocityVariance = 3.0;
   ejectionOffset   = 1.50;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = PulserSparkParticle;
};




datablock ParticleData(PulserExplosionParticle)
{
   dragCoefficient      = 10;
   gravityCoefficient   = -0.15;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 800;
   lifetimeVarianceMS   = 500;
   textureName          = "base/data/particles/cloud";

	spinSpeed		= 50.0;
	spinRandomMin		= -500.0;
	spinRandomMax		= 500.0;

   colors[0]     = "1.0 1.0 1.0 0.25";
   colors[1]     = "0.0 0.0 0.0 0.0";
   sizes[0]      = 0.5;
   sizes[1]      = 1.0;

   useInvAlpha = true;
};

datablock ParticleEmitterData(PulserExplosionEmitter)
{
	lifeTimeMS = 50;

   ejectionPeriodMS = 3;
   periodVarianceMS = 0;
   ejectionVelocity = 10;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 95;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = PulserExplosionParticle;
};

datablock ExplosionData(PulserExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 400;
   
   //emitter[0] = PulserExplosionEmitter;
   emitter[0] = PulserSparkEmitter;
   particleEmitter = PulserExplosionEmitter;
   particleDensity = 30;
	particleRadius = 1.0;
   
   faceViewer     = true;
   explosionScale = "1 1 1";

   soundProfile = PulserHitSound;

   
   shakeCamera = true;
   cameraShakeFalloff = false;
   camShakeFreq = "2.0 3.0 1.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 2.5;
   camShakeRadius = 0.0001;

   // Dynamic light
   lightStartRadius = 0;
   lightEndRadius = 0;
   lightStartColor = "0.0 0.0 0.0";
   lightEndColor = "0 0 0";
};


//projectile
datablock ProjectileData(PulserProjectile)
{
   //projectileShapeName = "~/data/shapes/arrow.dts";
   directDamage        = 0;
   impactImpulse       = 1300;
   verticalImpulse     = 1300;
   explosion           = PulserExplosion;
   //particleEmitter     = as;

   muzzleVelocity      = 50;
   velInheritFactor    = 1;

   armingDelay         = 0;
   lifetime            = 100;
   fadeDelay           = 70;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.0;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";
};


//////////
// item //
//////////
datablock ItemData(PulserItem)
{
	category = "Tools";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./shapes/pushBroom.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui properties
	uiName = "Pulser";
	iconName = "./ItemIcons/pushBroom";
	doColorShift = true;
	colorShiftColor = (213/255) SPC (17/255) SPC (17/255) SPC (255/255);
	

	 // Dynamic properties defined by the scripts
	image = PulserImage;
	canDrop = true;
};

//function Pulser::onUse(%this,%user)
//{
//	//mount the image in the right hand slot
//	%user.mountimage(%this.image, $RightHandSlot);
//}

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(PulserImage)
{
   // Basic Item properties
   shapeFile = "./shapes/pushBroom.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   //eyeOffset = "0.7 1.2 -0.15";

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = false;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = PulserItem;
   ammo = " ";
   projectile = PulserProjectile;
   projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = true;
   doRetraction = false;
   //raise your arm up or not
   armReady = true;

   //casing = " ";

   doColorShift = true;
   colorShiftColor = PulserItem.colorShiftColor;

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.0;
	stateTransitionOnTimeout[0]       = "Ready";

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "PreFire";
	stateAllowImageChange[1]         = true;

	stateName[2]			= "PreFire";
	stateScript[2]                  = "onPreFire";
	stateAllowImageChange[2]        = true;
	stateTimeoutValue[2]            = 0.01;
	stateTransitionOnTimeout[2]     = "Fire";

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "Fire";
	stateTimeoutValue[3]            = 0.2;
	stateFire[3]                    = true;
	stateAllowImageChange[3]        = true;
	stateSequence[3]                = "Fire";
	stateScript[3]                  = "onFire";
	stateWaitForTimeout[3]			= true;
	stateSequence[3]				= "Fire";
   stateTransitionOnTriggerUp[3]	= "StopFire";
   stateSound[3] = PulserSwingSound;
	//stateTransitionOnTriggerUp[3]	= "StopFire";

	stateName[4]			= "CheckFire";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "Fire";
   stateSound[4] = PulserSwingSound;

	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.2;
	stateAllowImageChange[5]        = true;
	stateWaitForTimeout[5]		= true;
	stateSequence[5]                = "StopFire";
	stateScript[5]                 = "onStopFire";
};

function PulserProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
if(%col.getclassname() $= "fxDTSBrick")
{
%trust = getTrustLevel(%obj.client, %col);
if(%trust == 2 || %obj.client.issuperadmin){
	if(%col.impulse)
	{
	if(%col.impulse !$= %obj.client.impulse)
		{
			%col.impulse = %obj.client.impulse;
			CreateImpulseTrigger(%col);
			%col.impulse = %obj.client.impulse;
			messageClient(%obj.client,'',"\c2Brick Impulse has been updated!");
			KillImpTrig(%col.trigger);
			return;
		}
		%col.impulse = 0;
		KillImpTrig(%col.trigger);
		messageClient(%obj.client,'',"\c2Brick's Impulse has been removed.");
	}
	else
	{
			if(%obj.client.impulse $= "")
			{
			messageClient(%obj.client,'',"\c2You have no Impulse set, type /setImpulse (X) (Y) (Z)!");
			return;
			}
			%col.impulse = %obj.client.impulse;
			CreateImpulseTrigger(%col);
			%col.impulse = %obj.client.impulse;
			messageClient(%obj.client,'',"\c2Brick now has an Impulse !");
	}
	}else{
		commandToClient(%obj.client, 'centerPrint', "" @ %col.client.name @ " does not trust you enough to do that.");
	}
}
}

function PulserImage::onFire(%this, %obj, %slot)
{
	//messageAll( 'MsgClient', 'pushBroom prefired!!!');
	Parent::onFire(%this, %obj, %slot);
	%obj.playthread(2, rotCW);
}

function PulserImage::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
	//messageAll( 'MsgClient', 'stopfire');
}

datablock TriggerData(ImpulseTrigger)
{
	// tickPeriodMS is a value is used to control how often the console
	// onTriggerTick callback is called while there are any objects
	// in the trigger. The default value is 100 MS.
	tickPeriodMS = 100;
};

function ImpulseTrigger::onTickTrigger(%this)
{
//Do nothing as it spams console :(
}

function ImpulseTrigger::onEnterTrigger(%this, %trigger, %obj)
{
if(%obj.getClassName() $= "WheeledVehicle")
{
if($Pref::Server::Impulse_AllowVehicles == 1)
{
return;
}
}
%impulse = %trigger.brick.impulse;
if(%impulse $= "")
{
		schedule(100,0,"KillImpTrig",%trigger);
		return;
}
	if(!isObject(%trigger.brick)){
		schedule(100,0,"KillImpTrig",%trigger);
		return;
	}
%impulse = %trigger.brick.impulse;
		%obj.applyImpulse(0,%impulse);
}

function CreateImpulseTrigger(%brick)
{
	if(!isobject(%brick)) return;
	%triggerX = %brick.dataBlock.brickSizeX/2 + 0.1;
	%triggerY = %brick.dataBlock.brickSizeY/2 + 0.1;
	%triggerZ = %brick.dataBlock.brickSizeZ*0.2 + 0.1;

	if(%brick.angleid == 2){
		%newpos = getWord(%brick.getTransform(),0) + %triggerX/2 SPC getWord(%brick.getTransform(),1) - %triggerY/2 SPC getWord(%brick.getTransform(),2) - (%triggerZ / 2);
		%newrot = %brick.rotation;
	}
	if(%brick.angleid == 0){
		%triggerXnew = %triggerX/2;
		%triggerYnew = %triggerY/2;
		%newpos = getWord(%brick.getTransform(),0) - %triggerXnew SPC getWord(%brick.getTransform(),1) + %triggerYnew SPC getWord(%brick.getTransform(),2) - (%triggerZ / 2);
		%newrot = %brick.rotation;
	}
	if(%brick.angleid == 1){
		%triggerYsq = %triggerX/2;
		%triggerYsq = %triggerYsq - %triggerYsq*2;
		%triggerXsq = %triggerY/2;
		%triggerXsq = %triggerXsq - %triggerXsq*2;
		%newrot = %brick.rotation;
		%newpos = getWord(%brick.getTransform(),0) - %TriggerXsq SPC getWord(%brick.getTransform(),1) - %triggerYsq SPC getWord(%brick.getTransform(),2) - (%triggerZ / 2);
	}
	if(%brick.angleid == 3){
		%triggerXsq = %triggerY/2;
		%triggerYsq = %triggerX/2;
		%newrot = %brick.rotation;
		%newpos = getWord(%brick.getTransform(),0) - %TriggerXsq SPC getWord(%brick.getTransform(),1) - %triggerYsq SPC getWord(%brick.getTransform(),2) - (%triggerZ / 2);
	}
	%trigger = new Trigger() {
		position = %newpos;
		rotation = %newrot;
		scale = "1 1 1.2";
		dataBlock = ImpulseTrigger;
		polyhedron = "0.0000000 0.0000000 0.0000000 1.0000000 0.0000000 0.0000000 0.0000000 -1.0000000 0.0000000 0.0000000 0.0000000 1.0000000";
	};
		%brick.trigger = %trigger;
		%trigger.brick = %brick;
		%trigger.setScale(%triggerX SPC %triggerY SPC %triggerZ);
}

function KillImpTrig(%obj)
{
%obj.delete();
}

function serverCmdSetImpulse(%client,%x,%y,%z)
{
if(%x $= "" || %y $= "" || %z $= "")
{
Messageclient(%client,'',"\c2You did not fill in all the specified values!");
%client.impulse = "";
return;
}
if(%x > 100 || %y > 100 || %z > 100)
{
Messageclient(%client,'',"\c2One or more of your values were too big! (< 100)");
%client.impulse = "";
return;
}
if(%x < -100 || %y < -100 || %z < -100)
{
Messageclient(%client,'',"\c2One or more of your values were too small! (> -100)");
%client.impulse = "";
return;
}
%client.impulse = (%x*100) SPC (%y*100) SPC (%z*100);
Messageclient(%client,'',"\c2You have set your impulse, now use the Pulser to create a impulse!");
}