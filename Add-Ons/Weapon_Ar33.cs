//ar33.cs

//audio
datablock AudioProfile(ar33Shot1Sound)
{
   filename    = "./sound/m16fire.wav";
   description = AudioClose3d;
   preload = true;
};
datablock AudioProfile(bulletHitSound)
{
   filename    = "./sound/arrowhit.wav";
   description = AudioClose3d;
   preload = true;
};
datablock AudioProfile(activate2Sound)
{
   filename    = "./sound/activate2.wav";
   description = AudioClose3d;
   preload = true;
};


//shell
datablock DebrisData(ar33ShellDebris)
{
	shapeFile = "./shapes/rifleShell.dts";
	lifetime = 2.0;
	minSpinSpeed = -400.0;
	maxSpinSpeed = 200.0;
	elasticity = 0.5;
	friction = 0.2;
	numBounces = 3;
	staticOnMaxBounce = true;
	snapOnMaxBounce = false;
	fade = true;

	gravModifier = 2;
};


//muzzle flash effects
datablock ParticleData(ar33FlashParticle)
{
	dragCoefficient      = 3;
	gravityCoefficient   = -0.5;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.0;
	lifetimeMS           = 25;
	lifetimeVarianceMS   = 15;
	textureName          = "base/data/particles/star6";
	spinSpeed		= 1.0;
	spinRandomMin		= -500.0;
	spinRandomMax		= 500.0;
	colors[0]     = "0.9 0.5 0.0 0.9";

	sizes[0]      = 1.5;


	useInvAlpha = false;
};
datablock ParticleEmitterData(ar33FlashEmitter)
{
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;
   ejectionVelocity = 1.0;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "ar33FlashParticle";
};

datablock ParticleData(ar33SmokeParticle)
{
	dragCoefficient      = 3;
	gravityCoefficient   = -0.5;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.0;
	lifetimeMS           = 525;
	lifetimeVarianceMS   = 55;
	textureName          = "base/data/particles/cloud";
	spinSpeed		= 10.0;
	spinRandomMin		= -500.0;
	spinRandomMax		= 500.0;
	colors[0]     = "0.5 0.5 0.5 0.9";
	colors[1]     = "0.5 0.5 0.5 0.0";
	sizes[0]      = 0.15;
	sizes[1]      = 0.15;

	useInvAlpha = false;
};
datablock ParticleEmitterData(ar33SmokeEmitter)
{
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;
   ejectionVelocity = 1.0;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "ar33SmokeParticle";
};


//bullet trail effects
datablock ParticleData(bulletTrailParticle)
{
	dragCoefficient      = 3;
	gravityCoefficient   = -0.0;
	inheritedVelFactor   = 1.0;
	constantAcceleration = 0.0;
	lifetimeMS           = 525;
	lifetimeVarianceMS   = 55;
	textureName          = "base/data/particles/thinRing";
	spinSpeed		= 10.0;
	spinRandomMin		= -500.0;
	spinRandomMax		= 500.0;
	colors[0]     = "0.3 0.3 0.9 0.4";
	colors[1]     = "0.5 0.5 0.5 0.0";
	sizes[0]      = 0.15;
	sizes[1]      = 0.25;

	useInvAlpha = false;
};
datablock ParticleEmitterData(bulletTrailEmitter)
{
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;
   ejectionVelocity = 0.0;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "bulletTrailParticle";
};


datablock ParticleData(ar33ExplosionParticle)
{
	dragCoefficient      = 8;
	gravityCoefficient   = -0.5;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.0;
	lifetimeMS           = 700;
	lifetimeVarianceMS   = 400;
	textureName          = "base/data/particles/cloud";
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	colors[0]     = "0.9 0.9 0.6 0.9";
	colors[1]     = "0.9 0.5 0.6 0.0";
	sizes[0]      = 0.25;
	sizes[1]      = 1.0;

	useInvAlpha = true;
};
datablock ParticleEmitterData(ar33ExplosionEmitter)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   ejectionVelocity = 5;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 89;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "ar33ExplosionParticle";
};


datablock ParticleData(ar33ExplosionRingParticle)
{
	dragCoefficient      = 8;
	gravityCoefficient   = -0.5;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.0;
	lifetimeMS           = 300;
	lifetimeVarianceMS   = 100;
	textureName          = "base/data/particles/star1";
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	colors[0]     = "1 1 0.0 0.9";
	colors[1]     = "0.9 0.0 0.0 0.0";
	sizes[0]      = 0.2;
	sizes[1]      = 0.2;

	useInvAlpha = false;
};
datablock ParticleEmitterData(ar33ExplosionRingEmitter)
{
	lifeTimeMS = 50;

   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   ejectionVelocity = 5;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 89;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "ar33ExplosionRingParticle";
};

datablock ExplosionData(ar33Explosion)
{
   //explosionShape = "";
	soundProfile = bulletHitSound;

   lifeTimeMS = 150;

   particleEmitter = ar33ExplosionEmitter;
   particleDensity = 10;
   particleRadius = 0.2;

   emitter[0] = ar33ExplosionRingEmitter;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = false;
   camShakeFreq = "10.0 11.0 10.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.5;
   camShakeRadius = 10.0;

   // Dynamic light
   lightStartRadius = 0;
   lightEndRadius = 2;
   lightStartColor = "0.3 0.6 0.7";
   lightEndColor = "0 0 0";
};


AddDamageType("ar33",   '<bitmap:add-ons/ci/ar33> %1',    '%2 <bitmap:add-ons/ci/ar33> %1',0.5,1);
datablock ProjectileData(ar33Projectile)
{
   projectileShapeName = "./shapes/bullet.dts";
   directDamage        = 60;
   directDamageType    = $DamageType::ar33;
   radiusDamageType    = $DamageType::ar33;

   brickExplosionRadius = 0;
   brickExplosionImpact = true;          //destroy a brick if we hit it directly?
   brickExplosionForce  = 10;
   brickExplosionMaxVolume = 100;          //max volume of bricks that we can destroy
   brickExplosionMaxVolumeFloating = 200;  //max volume of bricks that we can destroy if they aren't connected to the ground

   impactImpulse	     = 0;
   verticalImpulse	  = 0;
   explosion           = ar33Explosion;
   //particleEmitter     = bulletTrailEmitter;

   muzzleVelocity      = 200;
   velInheritFactor    = 1;

   armingDelay         = 00;
   lifetime            = 40000;
   fadeDelay           = 35000;
   bounceElasticity    = 0.5;
   bounceFriction      = 0.20;
   isBallistic         = false;
   gravityMod = 0.0;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";
};

//////////
// item //
//////////
datablock ItemData(ar33Item)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./shapes/m16a1.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Ar33";
	iconName = "./ItemIcons/ar33";
	doColorShift = true;
	colorShiftColor = "0.5 0.5 0.5 1.000";

	 // Dynamic properties defined by the scripts
	image = ar33Image;
	canDrop = true;
};



datablock ShapeBaseImageData(ar33Image)
{
   // Basic Item properties
   shapeFile = "./shapes/m16a1.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = 0; //"0.7 1.2 -0.5";
   rotation = eulerToMatrix( "0 0 0" );

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = BowItem;
   ammo = " ";
   projectile = ar33Projectile;
   projectileType = Projectile;

	casing = ar33ShellDebris;
	shellExitDir        = "1.0 -1.3 1.0";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 15.0;	
	shellVelocity       = 7.0;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;

   doColorShift = true;
   colorShiftColor = ar33Item.colorShiftColor;//"0.400 0.196 0 1.000";

   //casing = " ";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

// Initial start up state
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.1;
	stateTransitionOnTimeout[0]       = "Ready";

	stateSound[0]					= activate2Sound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;
   stateTransitionOnNoAmmo[1]       = "NoAmmo";
	stateSequence[1]	= "Ready";

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "smoke";
	stateTimeoutValue[2]            = 0.04;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;
	stateEmitter[2]					= ar33FlashEmitter;
	stateEmitterTime[2]				= 0.06;
	stateEjectShell[2]       	  = true;
	stateEmitterNode[2]				= "muzzleNode";
	stateSound[2]					= ar33Shot1Sound;
   stateSequence[2]                = "Fire";
	//stateEjectShell[2]       = true;





	stateName[3] = "Smoke";
	stateEmitter[3]					= ar33SmokeEmitter;
	stateEmitterTime[3]				= 0;
	stateEmitterNode[3]				= "muzzleNode";
	stateTimeoutValue[3]            = 0.0;
        stateTransitionOnTimeout[3]     = "Reload";



	stateName[4]			= "Reload";
	stateAllowImageChange[4]        = false;
	stateTimeoutValue[4]            = 0.06;
	stateWaitForTimeout[4]		= true;
	stateTransitionOnTimeout[4]     = "Check";
	//stateTransitionOnTriggerUp[4]     = "Ready";

	
	stateName[5]			= "Check";
	stateTransitionOnTriggerUp[5]	= "StopFire";
	stateTransitionOnTriggerDown[5]	= "Ready";
	
	stateName[6]                    = "StopFire";
	stateTransitionOnTimeout[6]     = "Ready";
	stateTimeoutValue[6]            = 0.03;
	stateAllowImageChange[6]        = false;
	stateWaitForTimeout[6]		    = true;

	stateScript[6]                  = "onStopFire";

};



