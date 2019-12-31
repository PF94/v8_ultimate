//grenade.cs


datablock AudioProfile(grenExplosionSound)
{
   filename    = "./sound/jeepExplosion.wav";
   description = AudioClose3d;
   preload = false;
};

datablock AudioProfile(grenFireSound)
{
   filename    = "./sound/spearFire.wav";
   description = AudioClose3d;
   preload = true;
};

datablock DebrisData(grenPinDebris)
{
	shapeFile = "./shapes/grenadePin.dts";
	lifetime = 2.0;
	minSpinSpeed = -400.0;
	maxSpinSpeed = 200.0;
	elasticity = 0.6;
	friction = 0.2;
	numBounces = 3;
	staticOnMaxBounce = true;
	snapOnMaxBounce = false;
	fade = true;
	gravModifier = 2.5;
};

datablock ParticleData(grenTrailParticle)
{
	dragCoefficient		= 3.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= -0.2;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 1000;
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
	colors[0]	= "1 0.0 0.0 0.1";
	colors[1]	= "1 0.0 0.0 0.05";
	colors[2]	= "1 0 0 0.0";
	sizes[0]	= 0.25;
	sizes[1]	= 0.10;
	sizes[2]	= 0.01;
	times[0]	= 0.0;
	times[1]	= 0.5;
	times[2]	= 1.0;
};

datablock ParticleEmitterData(grenTrailEmitter)
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 0;

   ejectionVelocity = 0; //0.25;
   velocityVariance = 0; //0.10;

   ejectionOffset = 0;

   thetaMin         = 0.0;
   thetaMax         = 90.0;  

   particles = grenTrailParticle;
};


//effects
datablock ParticleData(grenExplosionParticle)
{
	dragCoefficient		= 3.0;
	windCoefficient		= 0.2;
	gravityCoefficient	= 0.5;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 1200;
	lifetimeVarianceMS	= 300;
	spinSpeed		= 10.0;
	spinRandomMin		= -10.0;
	spinRandomMax		= 10.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "base/data/particles/cloud";

	// Interpolation variables
	colors[0]	= "0.5 0.5 0.5 0.9";
	colors[1]	= "0.7 0.7 0.7 0.0";
	sizes[0]	= 7.0;
	sizes[1]	= 15.0;
	times[0]	= 0.0;
	times[1]	= 1.0;
};

datablock ParticleEmitterData(grenExplosionEmitter)
{
   ejectionPeriodMS = 4;
   periodVarianceMS = 0;
   lifeTimeMS	   = 21;
   ejectionVelocity = 20;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 40;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "grenExplosionParticle";
};

datablock ParticleData(grenExplosionParticle2)
{
	dragCoefficient		= 0.1;
	windCoefficient		= 0.0;
	gravityCoefficient	= 2.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 400;
	lifetimeVarianceMS	= 500;
	spinSpeed		= 10.0;
	spinRandomMin		= -200.0;
	spinRandomMax		= 200.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "base/data/particles/cloud";

	// Interpolation variables
	colors[0]	= "0.7 0.5 0.0 1.0";
	colors[1]	= "0.0 0.0 0.0 0.0";
	sizes[0]	= 0.5;
	sizes[1]	= 0.1;
	times[0]	= 0.0;
	times[1]	= 1.0;
};

datablock ParticleEmitterData(grenExplosionEmitter2)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   lifetimeMS       = 7;
   ejectionVelocity = 7;
   velocityVariance = 5.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "grenExplosionParticle2";
};

datablock ParticleData(grenExplosionParticle3)
{
	dragCoefficient		= 3.0;
	windCoefficient		= 0.2;
	gravityCoefficient	= 2.5;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 1000;
	lifetimeVarianceMS	= 300;
	spinSpeed		= 10.0;
	spinRandomMin		= -10.0;
	spinRandomMax		= 10.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "base/data/particles/chunk";

	// Interpolation variables
	colors[0]	= "0.6 0.4 0.0 0.9";
	colors[1]	= "0.0 0.0 0.0 0.0";
	sizes[0]	= 0.4;
	sizes[1]	= 0.4;
	times[0]	= 0.0;
	times[1]	= 1.0;
};

datablock ParticleEmitterData(grenExplosionEmitter3)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   lifeTimeMS	   = 21;
   ejectionVelocity = 60;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "grenExplosionParticle3";
};

datablock ExplosionData(grenExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 150;

   soundProfile = grenExplosionSound;

   emitter[0] = grenExplosionEmitter;
   emitter[1] = grenExplosionEmitter2;
   emitter[2] = grenExplosionEmitter3;
   //particleDensity = 30;
   //particleRadius = 1.0;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = true;
   camShakeFreq = "7.0 8.0 7.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.5;
   camShakeRadius = 15.0;

   // Dynamic light
   lightStartRadius = 6;
   lightEndRadius = 3;
   lightStartColor = "0.45 0.3 0.1";
   lightEndColor = "0 0 0";

   //impulse
   impulseRadius = 3.5;
   impulseForce = 2000;

   //radius damage
   radiusDamage        = 100;
   damageRadius        = 7.0;
};

//projectile
AddDamageType("grenDirect",   '<bitmap:add-ons/ci/frag> %1',    '%2 <bitmap:add-ons/ci/frag> %1',1,1);
AddDamageType("grenRadius",   '<bitmap:add-ons/ci/fragRadius> %1',    '%2 <bitmap:add-ons/ci/fragRadius> %1',1,0);
datablock ProjectileData(grenProjectile)
{
   projectileShapeName = "./shapes/grenade.dts";
   directDamage        = 100;
   directDamageType  = $DamageType::grenDirect;
   radiusDamageType  = $DamageType::grenRadius;
   impactImpulse	   = 1000;
   verticalImpulse	   = 1000;
   explosion           = grenExplosion;
   particleEmitter     = grenTrailEmitter;

   brickExplosionRadius = 0;
   brickExplosionImpact = false; //destroy a brick if we hit it directly?
   brickExplosionForce  = 20;
   brickExplosionMaxVolume = 200;

   muzzleVelocity      = 20;
   velInheritFactor    = 1;

   armingDelay         = 1600;
   lifetime            = 4500;
   fadeDelay           = 4000;
   bounceElasticity    = 0.6;
   bounceFriction      = 0.1;
   isBallistic         = true;
   gravityMod = 1.0;

   hasLight    = false;
   lightRadius = 5.0;
   lightColor  = "0 0 0.5";
};


//////////
// item //
//////////
datablock ItemData(grenItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./shapes/grenadebox.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Grenade";
	iconName = "./ItemIcons/grenade";
	doColorShift = false;
	colorShiftColor = "0.400 0.196 0 1.000";

	 // Dynamic properties defined by the scripts
	image = grenImage;
	canDrop = true;
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(grenImage)
{
   // Basic Item properties
   shapeFile = "./shapes/grenadeitem.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "-0.025 0.05 0";
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
   item = grenItem;
   ammo = " ";
   projectile = grenProjectile;
   projectileType = Projectile;
   
   	casing = grenPinDebris;
	shellExitDir        = "1.0 -0.5 1.0";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 15.0;	
	shellVelocity       = 6.0;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;

	
   doColorShift = false;
   colorShiftColor = "0.000 0.50 0 0.000";

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
	stateTimeoutValue[2]            = 0.8;
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
	stateEjectShell[5]   	        = true;
	stateSound[5]				= grenFireSound;
};

function grenImage::onCharge(%this, %obj, %slot)
{
	%obj.playthread(2, spearReady);
}

function grenImage::onAbortCharge(%this, %obj, %slot)
{
	%obj.playthread(2, root);
}

function grenImage::onFire(%this, %obj, %slot)
{
	%obj.playthread(2, spearThrow);
	Parent::onFire(%this, %obj, %slot);
}