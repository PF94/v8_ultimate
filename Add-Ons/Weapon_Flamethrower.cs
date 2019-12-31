datablock ParticleData(playerNapalmFireParticle)
{
	textureName          = "base/data/particles/cloud";
	dragCoefficient      = 0.0;
	gravityCoefficient   = -1.0;
	inheritedVelFactor   = 0.0;
	windCoefficient      = 0;
	constantAcceleration = 3.0;
	lifetimeMS           = 1200;
	lifetimeVarianceMS   = 100;
	spinSpeed     = 0;
	spinRandomMin = -90.0;
	spinRandomMax =  90.0;
	useInvAlpha   = false;

	colors[0]	= "1   1   0.3 0.0";
	colors[1]	= "1   1   0.3 1.0";
	colors[2]	= "0.6 0.0 0.0 0.0";

	sizes[0]	= 0.0;
	sizes[1]	= 2.0;
	sizes[2]	= 1.0;

	times[0]	= 0.0;
	times[1]	= 0.2;
	times[2]	= 1.0;
};

datablock ParticleEmitterData(playerNapalmFireEmitter)
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 4;
   ejectionVelocity = 0;
   ejectionOffset   = 1.00;
   velocityVariance = 0.0;
   thetaMin         = 30;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;

   //lifetimeMS = 5000;
   particles = playerNapalmFireParticle;   
};

datablock ShapeBaseImageData(playerNapalmBurnImage)
{
   	shapeFile = "base/data/shapes/empty.dts";
	emap = false;

	mountPoint = $HeadSlot;
	offset = "0 0 -2";

	stateName[0]					= "Ready";
	stateTransitionOnTimeout[0]			= "FireA";
	stateTimeoutValue[0]				= 0.01;

	stateName[1]					= "FireA";
	stateTransitionOnTimeout[1]			= "Done";
	stateWaitForTimeout[1]				= True;
	stateTimeoutValue[1]				= 0.9;
	stateEmitter[1]					= playerNapalmFireEmitter;
	stateEmitterTime[1]				= 0.9;

	stateName[2]					= "Done";
	stateTransitionOnTimeout[2]			= "FireA";
	stateTimeoutValue[2]				= 0.01;
};

datablock ParticleData(flamethrowerParticle)
{
	dragCoefficient      = 2.9;
	gravityCoefficient   = 0.1;
	inheritedVelFactor   = 1.0;
	constantAcceleration = 0.0;
	lifetimeMS           = 500;
	lifetimeVarianceMS   = 75;
	textureName          = "base/data/particles/cloud";
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	colors[0]     = "0.000000 0.000000 1.000000 1.000000";
	colors[1]     = "1.000000 0.496063 0.000000 0.000000";
   	colors[2]     = "1.000000 1.000000 1.000000 1.000000";
   	colors[3]     = "1.000000 1.000000 1.000000 1.000000";

	sizes[0]      = 0.0946103;
	sizes[1]      = 0.897272;
 	sizes[2]      = 1;
 	sizes[3]      = 1;

   	times[0] = 0;
   	times[1] = 1;
   	times[2] = 2;
  	times[3] = 2;

	useInvAlpha = false;
};

datablock ParticleEmitterData(flamethrowerEmitter)
{
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;
   ejectionVelocity = 20;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 5;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   orientOnVelocity = true;
   emitterOffset = "0.0 0.0 0.8 ";
   particles = "flamethrowerParticle";
};

datablock ParticleData(flamethrowerGasParticle)
{
	dragCoefficient      = 0.0;
	gravityCoefficient   = -1;
	inheritedVelFactor   = 0.0;
	constantAcceleration = 0.0;
	lifetimeMS           = 100;
	lifetimeVarianceMS   = 75;
	textureName          = "base/data/particles/cloud";
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	colors[0]     = "0.000000 0.000000 1.000000 0.500000";
	colors[1]     = "0.000000 0.496063 1.000000 0.000000";

	sizes[0]      = 0.04;
	sizes[1]      = 0.01;

   	times[0] = 0;
   	times[1] = 1;

	useInvAlpha = false;
};

datablock ParticleEmitterData(flamethrowerGasEmitter)
{
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;
   ejectionVelocity = 0.0;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 5;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   orientOnVelocity = true;
   particles = "flamethrowerGasParticle";
};


datablock ParticleData(flamethrowerExplosionParticle)
{
	dragCoefficient      = 2;
	gravityCoefficient   = 0.0;
	windCoefficient	   = 1;
	inheritedVelFactor   = 0.0;
	constantAcceleration = 0.0;
	lifetimeMS           = 600;
	lifetimeVarianceMS   = 200;
	textureName          = "base/data/particles/cloud";
	spinSpeed		= 100.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	colors[0]     = "1.000000 0.496063 0.000000 0.496063";
	colors[1]     = "1.000000 0.496063 0.000000 0.000000";
	colors[2]     = "1.000000 1.000000 1.000000 1.000000";
	colors[3]     = "1.000000 1.000000 1.000000 1.000000";
	sizes[0]      = 0.799609;
	sizes[1]      = 1.19636;
	sizes[2]      = 1;
	sizes[3]      = 1;
	times[0]	  = 0;
	times[1]	  = 1;
	times[2]	  = 2;
	times[3]	  = 2;

	useInvAlpha = false;
};
datablock ParticleEmitterData(flamethrowerExplosionEmitter)
{
   ejectionPeriodMS = 40;
   periodVarianceMS = 0;
   ejectionVelocity = 3.5;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 35;
   thetaMax         = 85;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   orientOnVelocity = true;
   particles = "flamethrowerExplosionParticle";
};

datablock ExplosionData(flamethrowerExplosion)
{
   lifeTimeMS = 128;

   particleEmitter = flamethrowerExplosionEmitter;
   particleDensity = 10;
   particleRadius = 1;
   explosionScale = "1 1 1";
};


AddDamageType("FlamethrowerDirect",   '<bitmap:add-ons/ci/flamethrower> %1',    '%2 <bitmap:add-ons/ci/flamethrower> %1',1,1);
datablock ProjectileData(flamethrowerProjectile)
{
   directDamage        = 5;
   directDamageType = $DamageType::FlamethrowerDirect;
   explosion           = flamethrowerExplosion;

   muzzleVelocity      = 25;
   velInheritFactor    = 1;

   armingDelay         = 0;
   lifetime            = 300;
   fadeDelay           = 0;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.0;
};

//////////
// item //
//////////
datablock ItemData(flamethrowerItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./shapes/flamethrower.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Flamethrower";
	iconName = "./ItemIcons/FlameThrower";
	doColorShift = true;
	colorShiftColor = "0.9 0.600 0.100 1.000";

	 // Dynamic properties defined by the scripts
	image = flamethrowerImage;
	canDrop = true;
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(flamethrowerImage)
{
   // Basic Item properties
   shapeFile = "./shapes/Flamethrower.dts";
   emap = true;

   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = 0;
   rotation = "1 0 0 0";
   correctMuzzleVector = true;
   className = "WeaponImage";

   // Projectile && Ammo.
   item = flamethrowerItem;
   ammo = " ";
   projectile = flamethrowerProjectile;
   projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;

   doRetraction = false;

   doColorShift = true;
   colorShiftColor = flamethrowerItem.colorShiftColor;//"0.400 0.196 0 1.000";

   //casing = " ";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]                     = "Activate";
	stateTransitionOnTriggerDown[0]  = "Fire";
	stateAllowImageChange[0]	   = true;
	stateSound[0]			   = sprayActivateSound;
	stateTransitionOnTimeout[0]	   = "Ready";
	stateTimeoutValue[0]		   = 0.01;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateTimeoutValue[1]		   = 0.04;
	stateTransitionOnTimeout[1]	   = "Ready";
	stateWaitForTimeout[1]		   = true;
	stateAllowImageChange[1]	   = true;
	stateEmitter[1]			   = flamethrowerGasEmitter;
	stateEmitterTime[1]		   = 0.07;

	stateName[2]                     = "Fire";
	stateTransitionOnTriggerUp[2]	   = "StopFire";
	stateTransitionOnTimeout[2]	   = "Fire";
	stateTimeoutValue[2]		   = 0.04;
	stateWaitForTimeout[2]		   = true;
	stateFire[2]			   = true;
	stateAllowImageChange[2]	   = true;
	stateSound[2]			   = sprayFireSound;
	stateScript[2]			   = "onFire";
	stateEmitter[2]			   = flamethrowerEmitter;
	stateEmitterTime[2]		   = 0.07;
	stateSequence[2]			   = "fire";

	stateName[3] 			   = "StopFire";
	stateTransitionOnTimeout[3]	   = "Ready";
	stateWaitForTimeout[3]		   = true;
	stateAllowImageChange[3]	   = true;
	stateSequence[3]			   = "stopfire";
};

function Player::stopBurn(%this)
{
	%this.isBurning=0;
}

function flamethrowerProjectile::onCollision(%this,%obj,%col,%pos,%fade)
{
	if(%col.getClassName() $= "Player" && %col.isBurning !$= 1 && miniGameCanDamage(%obj.client, %col) $= 1)
	{
		%col.isBurning=1;
		%col.schedule(8000,"stopBurn");
		%col.emote(playerNapalmBurnImage);
		%col.schedule(1000,"emote",playerBurnImage);
		%col.schedule(500,"setTempColor","0 0 0 1",5200);
		%col.schedule(5000,"burn");
	}
	Parent::onCollision(%this,%obj,%col,%pos,%fade);
}