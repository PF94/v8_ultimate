//spear.cs

exec("add-ons/support_jeep.cs");
datablock AudioProfile(PropaneTankExplosionSound)
{
   filename    = "./sound/PropaneTankHit.wav";
   description = AudioClose3d;
   preload = false;
};

datablock AudioProfile(PropaneTankFireSound)
{
   filename    = "./sound/PropaneTankFire.wav";
   description = AudioClose3d;
   preload = true;
};


//PropaneTank trail
datablock ParticleData(PropaneTankTrailParticle)
{
	dragCoefficient		= 3.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 600;
	lifetimeVarianceMS	= 0;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "base/data/particles/ring";
	//animTexName		= " ";

	// Interpolation variables
	colors[0]	= "0.75 0.75 0.75 0.3";
	colors[1]	= "0.75 0.75 0.75 0.2";
	colors[2]	= "1 1 1 0.0";
	sizes[0]	= 0.15;
	sizes[1]	= 0.35;
	sizes[2]	= 0.05;
	times[0]	= 0.0;
	times[1]	= 0.1;
	times[2]	= 1.0;
};

datablock ParticleEmitterData(PropaneTankTrailEmitter)
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 0;

   ejectionVelocity = 0; //0.25;
   velocityVariance = 0; //0.10;

   ejectionOffset = 0;

   thetaMin         = 0.0;
   thetaMax         = 90.0;  

   particles = PropaneTankTrailParticle;
};


//effects
datablock ParticleData(PropaneTankExplosionParticle)
{
	dragCoefficient		= 3.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= 0.5;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 900;
	lifetimeVarianceMS	= 300;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "base/data/particles/cloud";
	//animTexName		= "~/data/particles/cloud";

	// Interpolation variables
	colors[0]	= "0.3 0.3 0.2 0.9";
	colors[1]	= "0.2 0.2 0.2 0.0";
	sizes[0]	= 4.0;
	sizes[1]	= 7.0;
	times[0]	= 0.0;
	times[1]	= 1.0;
};

//projectile
AddDamageType("PropaneTankDirect",   '<bitmap:add-ons/ci/PropaneTank> %1',    '%2 <bitmap:add-ons/ci/PropaneTank> %1',1,1);
AddDamageType("PropaneTankRadius",   '<bitmap:add-ons/ci/PropaneTankRadius> %1',    '%2 <bitmap:add-ons/ci/PropaneTankRadius> %1',1,0);
datablock ProjectileData(PropaneTankProjectile)
{
   projectileShapeName = "./propanetank.dts";
   directDamage        = 50;
   directDamageType  = $DamageType::PropaneTankDirect;
   radiusDamageType  = $DamageType::PropaneTankRadius;
   impactImpulse	   = 1000;
   verticalImpulse	   = 1000;
   explosion           = "";
   particleEmitter     = "";

   brickExplosionRadius = 0;
   brickExplosionImpact = false; //destroy a brick if we hit it directly?
   brickExplosionForce  = 0;
   brickExplosionMaxVolume = 200;

   muzzleVelocity      = 20;
   velInheritFactor    = 1;

   armingDelay         = 0;
   lifetime            = 20000;
   fadeDelay           = 19500;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0.50;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";
};


//////////
// item //
//////////
datablock ItemData(PropaneTankItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./propanetankbox.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "PropaneTank";
	iconName = "./ItemIcons/PropaneTank";
	doColorShift = true;
		colorShiftColor = "0.471 0.471 0.471 1.000";

	 // Dynamic properties defined by the scripts
	image = PropaneTankImage;
	canDrop = true;
};
function PropaneTankItem::onPickup(%this, %obj, %player)
{  
   %player.hasepropane = 1;
   %player.propanenumtank = %obj.spawnbrick;
   ItemData::onPickup(%this, %obj, %player);
}
//function PropaneTank::onUse(%this,%user)
//{
//	//mount the image in the right hand slot
//	%user.mountimage(%this.image, $RightHandSlot);
//}

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(PropaneTankImage)
{
   // Basic Item properties
   shapeFile = "./propanetank.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   //eyeOffset = "0.1 0.2 -0.55";

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = PropaneTankItem;
   ammo = " ";
   projectile = PropaneTankProjectile;
   projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;

   //casing = " ";
   doColorShift = true;
   colorShiftColor = "0.471 0.471 0.471 1.000";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]			= "Activate";
	stateTimeoutValue[0]		= 0.1;
	stateTransitionOnTimeout[0]	= "Ready";
	stateSequence[0]		= "ready";
	stateSound[0]					= weaponSwitchSound;

	stateName[1]			= "Ready";
	stateTransitionOnTriggerDown[1]	= "Charge";
	stateAllowImageChange[1]	= true;
	
	stateName[2]                    = "Charge";
	stateTransitionOnTimeout[2]	= "Armed";
	stateTimeoutValue[2]            = 0.7;
	stateWaitForTimeout[2]		= false;
	stateTransitionOnTriggerUp[2]	= "AbortCharge";
	stateScript[2]                  = "onCharge";
	stateAllowImageChange[2]        = false;
	
	stateName[3]			= "AbortCharge";
	stateTransitionOnTimeout[3]	= "Ready";
	stateTimeoutValue[3]		= 0.3;
	stateWaitForTimeout[3]		= true;
	stateScript[3]			= "onAbortCharge";
	stateAllowImageChange[3]	= false;

	stateName[4]			= "Armed";
	stateTransitionOnTriggerUp[4]	= "Fire";
	stateAllowImageChange[4]	= false;

	stateName[5]			= "Fire";
	stateTransitionOnTimeout[5]	= "Ready";
	stateTimeoutValue[5]		= 0.5;
	stateFire[5]			= true;
	stateSequence[5]		= "fire";
	stateScript[5]			= "onFire";
	stateWaitForTimeout[5]		= true;
	stateAllowImageChange[5]	= false;
	stateSound[5]				= PropaneTankFireSound;
};

function PropaneTankImage::onCharge(%this, %obj, %slot)
{
	%obj.playthread(2, spearReady);
}

function PropaneTankImage::onAbortCharge(%this, %obj, %slot)
{
	%obj.playthread(2, root);
}

function PropaneTankImage::onFire(%this, %obj, %slot)
{
	if(%obj.client.minigame == 0 || %obj.client.minigame == -1){
		return;
	}
	if(%obj.client.haspropaneactive != 1)
	{
	%obj.client.haspropaneactive = 1;
	%obj.playthread(2, spearThrow);
	//Parent::onFire(%this, %obj, %slot);
	for(%i=0;%i<5;%i++)
	{
		%toolDB = %obj.tool[%i];
		if(%toolDB $= %this.item.getID())
		{
			%obj.tool[%i] = 0;
			%obj.weaponCount--;
			messageClient(%obj.client,'MsgItemPickup','',%i,0);
			serverCmdUnUseTool(%obj.client);
			break;
		}
	}
	%obj.sourcerotation = %obj.gettransform();
	//echo(%obj.getforwardvector());


		%item = new WheeledVehicle(PropaneTank) 
  {  
   // position = localclientconnection.camera.getPosition();
    //position = vectoradd(%obj.getposition(),"0 0 10");
	rotation = getwords(%obj.sourcerotation,3,6);
    datablock  = PropaneTankCol;
    
  };
  schedule(60000,%item,delaydelete,%item);
  schedule(20000,0,badfunctionnamea,%obj);
  %item.startfade(5000,55000,1);
  %item.sourceobject = %obj;
  %item.setnodecolor("ALL", "0.471 0.471 0.471 1.000");
 // %item.setvelocity(vectorscale(vectorscale(%obj.geteyevector(),14),%obj.getvelocity()));
    

  %item.spawnbrick = %obj.propanenumtank; 
  %first = vectoradd(getwords(%obj.gettransform(),0,2),"0 0 2");
	%second= getwords(%obj.sourceobject.sourcerotation,3,6);
	%item.settransform(%first SPC %second);
	%item.setvelocity(vectorscale(%obj.geteyevector(),10));
	}
}
function badfunctionnamea(%obj)
{
	%obj.client.haspropaneactive = 0;
}
function PropaneTankProjectile::oncollision(%this,%obj,%col)
{
	
}
function delaydelete(%obj)
{
	if(isobject(%obj))
	{
		%obj.delete();

	}


}
datablock StaticShapeData(PropaneTankstatic)
{
	category = "Rot";
	 // Basic Item Properties
	shapeFile = "./propanetank.dts";
	 doColorShift = true;
	colorShiftColor = "0.471 0.471 0.471 1.000";
};

function propanetankstatic::oncolision(%this,%obj,%col)
{
	////echo(%col);

}
datablock WheeledVehicleData(PropaneTankCol)
{
	category = "Vehicles";
	displayName = "";
	shapeFile = "./PropaneTankCol.dts";
	emap = true;
	minMountDist = 0;
   
   numMountPoints = 0;

	maxDamage = 1;
	destroyedLevel = 1;
	energyPerDamagePoint = 1;
	speedDamageScale = 1.04;
	collDamageThresholdVel = 20.0;
	collDamageMultiplier   = 0.02;
    doColorShift = true;
    colorShiftColor = "0.471 0.471 0.471 1.000";
	massCenter = "0 0 0";
   //massBox = "2 5 1";

	maxSteeringAngle = 0.9785;  // Maximum steering angle, should match animation
	integration = 4;           // Force integration time: TickSec/Rate
	//tireEmitter = TireEmitter; // All the tires use the same dust emitter

	// 3rd person camera settings
	cameraRoll = false;         // Roll the camera with the vehicle
	cameraMaxDist = 13;         // Far distance from vehicle
	cameraOffset = 7.5;        // Vertical offset from camera mount point
	cameraLag = 0.0;           // Velocity lag of camera
	cameraDecay = 0.75;        // Decay per sec. rate of velocity lag
	cameraTilt = 0.4;
   collisionTol = 0.1;        // Collision distance tolerance
   contactTol = 0.1;

	useEyePoint = false;	

	//defaultTire	= PropaneTankTire;
	//defaultSpring	= PropaneTankSpring;
	//flatTire	= PropaneTankFlatTire;
	//flatSpring	= PropaneTankFlatSpring;

   numWheels = 0;

	// Rigid Body
	mass = 800;
	density = 5.0;
	drag = 1.6;
	bodyFriction = 0.6;
	bodyRestitution = 0.6;
	minImpactSpeed = 10;        // Impacts over this invoke the script callback
	softImpactSpeed = 10;       // Play SoftImpact Sound
	hardImpactSpeed = 15;      // Play HardImpact Sound
	groundImpactMinSpeed    = 10.0;

	// Engine
	engineTorque = 12000; //4000;       // Engine power
	engineBrake = 2000;         // Braking when throttle is 0
	brakeTorque = 50000;        // When brakes are applied
	maxWheelSpeed = 0;        // Engine scale by current speed / max speed

	rollForce		= 900;
	yawForce		= 600;
	pitchForce		= 1000;
	rotationalDrag		= 0.2;

	// Energy
	maxEnergy = 5;
	jetForce = 3000;
	minJetEnergy = 30;
	jetEnergyDrain = 2;

	splash = PropaneTankSplash;
	splashVelocity = 4.0;
	splashAngle = 67.0;
	splashFreqMod = 300.0;
	splashVelEpsilon = 0.60;
	bubbleEmitTime = 1.4;
	//splashEmitter[0] = PropaneTankFoamDropletsEmitter;
	//splashEmitter[1] = PropaneTankFoamEmitter;
	//splashEmitter[2] = PropaneTankBubbleEmitter;
	mediumSplashSoundVelocity = 10.0;   
	hardSplashSoundVelocity = 20.0;   
	exitSplashSoundVelocity = 5.0;
		
	//mediumSplashSound = "";
	//hardSplashSound = "";
	//exitSplashSound = "";
	
	// Sounds
	//   jetSound = ScoutThrustSound;
	//engineSound = idleSound;
	//squealSound = skidSound;
	softImpactSound = slowImpactSound;
	hardImpactSound = fastImpactSound;
	//wheelImpactSound = slowImpactSound;

	//   explosion = VehicleExplosion;
	justcollided = 0;

   uiName = "";
	rideable = true;
		lookUpLimit = 0.65;
		lookDownLimit = 0.45;

	paintable = true;
   
  // damageEmitter[0] = PropaneTankBurnEmitter;
	damageEmitterOffset[0] = "0.0 0.0 0.0 ";
	damageLevelTolerance[0] = 0.99;

   //damageEmitter[1] = PropaneTankBurnEmitter;
	damageEmitterOffset[1] = "0.0 0.0 0.0 ";
	damageLevelTolerance[1] = 1.0;

   numDmgEmitterAreas = 1;

   ///initialExplosionProjectile = PropaneTankExplosionProjectile;
  // initialExplosionOffset = 0;         //offset only uses a z value for now

   burnTime = 100;

   finalExplosionProjectile = PropaneTankFinalExplosionProjectile;
   finalExplosionOffset = 0.5;          //offset only uses a z value for now


   minRunOverSpeed    = 2;   //how fast you need to be going to run someone over (do damage)
   runOverDamageScale = 5;   //when you run over someone, speed * runoverdamagescale = damage amt
   runOverPushScale   = 1.2; //how hard a person you're running over gets pushed
};
function PropaneTankCol::onAdd(%this,%obj)
{
	//Parent::onadd(%this,%obj);
	//%obj.spawnbrick = 6956;

}
function propaneTankCol::oncollision(%this, %obj, %col, %fade, %pos, %norm)
{
	//%obj.damage(%col.sourceobject,%pos,500);
	////echo(%col);
	if(%col.getdatablock().getname() $= "Jeepvehicle"){
		if(minigamecandamage(%obj,%col))
		{
			%obj.damage(%col,%col.getposition(),500);
		}
	}

}

datablock DebrisData(PropaneTankDebris)
{
   emitters = "JeepDebrisTrailEmitter";

	shapeFile = "./PropaneTank.dts";
	lifetime = 3.0;
	minSpinSpeed = -500.0;
	maxSpinSpeed = 500.0;
	elasticity = 0.5;
	friction = 0.2;
	numBounces = 1;
	staticOnMaxBounce = true;
	snapOnMaxBounce = false;
	fade = true;

	gravModifier = 2;
};



datablock ExplosionData(PropaneTankFinalExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 150;

   soundProfile = JeepExplosionSound;
   
   emitter[0] = JeepFinalExplosionEmitter3;
   emitter[1] = JeepFinalExplosionEmitter2;

   particleEmitter =JeepFinalExplosionEmitter;
   particleDensity = 20;
   particleRadius = 1.0;

   debris = PropaneTankDebris;
   debrisNum = 1;
   debrisNumVariance = 0;
   debrisPhiMin = 0;
   debrisPhiMax = 360;
   debrisThetaMin = 0;
   debrisThetaMax = 20;
   debrisVelocity = 18;
   debrisVelocityVariance = 3;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = true;
   camShakeFreq = "7.0 8.0 7.0";
   camShakeAmp = "10.0 10.0 10.0";
   camShakeDuration = 0.75;
   camShakeRadius = 15.0;

   // Dynamic light
   lightStartRadius = 0;
   lightEndRadius = 20;
   lightStartColor = "0.45 0.3 0.1";
   lightEndColor = "0 0 0";

   //impulse
   impulseRadius = 15;
   impulseForce = 1000;
   impulseVertical = 2000;

   //radius damage
   radiusDamage        = 1000;
   damageRadius        = 15.0;

   //burn the players?
   playerBurnTime = 4000;

};

datablock ProjectileData(PropaneTankFinalExplosionProjectile)
{
   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 0;
   explosion           = PropaneTankFinalExplosion;

   directDamageType  = $DamageType::PropaneTankExplosion;
   radiusDamageType  = $DamageType::PropaneTankExplosion;

    brickExplosionRadius = 8;
   brickExplosionImpact = false;          //destroy a brick if we hit it directly?
   brickExplosionForce  = 30;             
   brickExplosionMaxVolume = 30;          //max volume of bricks that we can destroy
   brickExplosionMaxVolumeFloating = 60;  //max volume of bricks that we can destroy if they aren't connected to the ground (should always be >= brickExplosionMaxVolume)

   explodeOnDeath		= 1;

   armingDelay         = 0;
   lifetime            = 10;
};