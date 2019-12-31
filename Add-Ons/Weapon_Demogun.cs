//Demogun.cs
//Demogun and Demobomb weapon stuff

datablock AudioProfile(DemobombExplosionSound)
{
   filename    = "./sound/jeepExplosion.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(DemogunFireSound)
{
   filename    = "./sound/bowFire.wav";
   description = AudioClosest3d;
   preload = true;
};


//Demobomb trail
datablock ParticleData(DemobombTrailParticle)
{
	dragCoefficient		= 3.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 200;
	lifetimeVarianceMS	= 0;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= false;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "base/data/particles/dot";
	//animTexName		= "~/data/particles/dot";

	// Interpolation variables
	colors[0]	= "0 148 189 3";
	colors[1]	= "0 148 189 3";
	sizes[0]	= 0.3;
	sizes[1]	= 0.3;
	times[0]	= 0.0;
	times[1]	= 1.0;
};

datablock ParticleEmitterData(DemobombTrailEmitter)
{
   ejectionPeriodMS = 2;
   periodVarianceMS = 0;

   ejectionVelocity = 0; //0.25;
   velocityVariance = 0; //0.10;

   ejectionOffset = 0;

   thetaMin         = 0.0;
   thetaMax         = 90.0;  

   particles = DemobombTrailParticle;
};

//effects
datablock ParticleData(DemobombExplosionParticle)
{
	dragCoefficient      = 5;
	gravityCoefficient   = 0.1;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.0;
	lifetimeMS           = 600;
	lifetimeVarianceMS   = 600;
	textureName          = "base/data/particles/cloud";
	//animTexName		= "~/data/particles/cloud";
        spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	colors[0]     = "0.9 0.2 0.2 0.8";
	colors[1]     = "0.6 0.4 0.0 0.0";
	sizes[0]      = 5;
	sizes[1]      = 5;
};

datablock ParticleEmitterData(DemobombExplosionEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   ejectionVelocity = 5;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "DemobombExplosionParticle";
};

datablock ExplosionData(DemobombExplosion)
{
   //explosionShape = "";
	soundProfile = DemobombExplosionSound;

   lifeTimeMS = 150;

   particleEmitter = DemobombExplosionEmitter;
   particleDensity = 10;
   particleRadius = 0.2;

   faceViewer     = true;
   explosionScale = "9 9 9";

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

   //impulse
   impulseRadius = 15;
   impulseForce = 2500;

   //radius damage
   damageRadius = 10;
   radiusDamage = 50;


};


//projectile
AddDamageType("DemobombDirect",   '<bitmap:add-ons/ci/Demobomb> %1',    '%2 <bitmap:add-ons/ci/Demobomb> %1',1,1);

datablock ProjectileData(DemobombProjectile)
{
   projectileShapeName = "./shapes/Demobomb.dts";

   directDamage        = 100;
   directDamageType    = $DamageType::DemobombDirect;
   impactImpulse	   = 4000;
   radiusDamage        = 0;
   damageRadius        = 0;
   radiusDamageType    = $DamageType::DemobombDirect;
   explosion           = DemobombExplosion;
   particleEmitter     = DemobombTrailEmitter;

   brickExplosionRadius = 3;
   brickExplosionImpact = false;          //destroy a brick if we hit it directly?
   brickExplosionForce  = 30;             
   brickExplosionMaxVolume = 30;          //max volume of bricks that we can destroy
   brickExplosionMaxVolumeFloating = 60;  //max volume of bricks that we can destroy if they aren't connected to the ground (should always be >= brickExplosionMaxVolume)

   muzzleVelocity      = 35;
   velInheritFactor    = 35;

   armingDelay         = 5000;
   lifetime            = 3000;
   fadeDelay           = 3000;
   bounceElasticity    = 0.6;
   bounceFriction      = 0.5;
   isBallistic         = true;
   gravityMod = 600.50;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";

   explodeOnDeath = true;
};


//////////
// item //
//////////
datablock ItemData(DemogunItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./shapes/Demogun.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Demogun";
	iconName = "./ItemIcons/Demogun";
	doColorShift = true;
	colorShiftColor = "0.400 0.196 0 1.000";

	 // Dynamic properties defined by the scripts
	image = DemogunImage;
	canDrop = true;
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(DemogunImage)
{
   // Basic Item properties
   shapeFile = "./shapes/Demogun.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = 0; //"0.7 1.2 -0.5";
   rotation = eulerToMatrix( "0 0 10" );

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = DemogunItem;
   ammo = " ";
   projectile = DemobombProjectile;
   projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;

   doColorShift = true;
   colorShiftColor = DemogunItem.colorShiftColor;//"0.400 0.196 0 1.000";

   //casing = " ";

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
	stateSound[0]					= weaponSwitchSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Reload";
	stateTimeoutValue[2]            = 0.05;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;
	stateSound[2]					= DemogunFireSound;

	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]            = 0.5;
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTimeout[3]     = "Check";

	stateName[4]			= "Check";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "Fire";

	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.2;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	//stateSequence[5]                = "Reload";
	stateScript[5]                  = "onStopFire";


};


