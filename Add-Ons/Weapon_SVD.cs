//SniperRifle.cs

//audio
datablock AudioProfile(sniperrifleLVShot1Sound)
{
   filename    = "./sound/silence.wav";
   description = AudioClose3d;
   preload = true;
};
datablock AudioProfile(sniperrifleLVbulletHitSound)
{
   filename    = "./sound/arrowHit.wav";
   description = AudioClose3d;
   preload = true;
};


//shell
datablock DebrisData(sniperrifleLVShellDebris)

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
datablock ParticleData(sniperrifleLVFlashParticle)
{
	dragCoefficient      = 3;
	gravityCoefficient   = -0.5;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.0;
	lifetimeMS           = 25;
	lifetimeVarianceMS   = 15;
	textureName          = "base/data/particles/star1";
	spinSpeed		= 10.0;
	spinRandomMin		= -500.0;
	spinRandomMax		= 500.0;
	colors[0]     = "0.9 0.9 0.0 0.9";
	colors[1]     = "0.9 0.5 0.0 0.0";
	sizes[0]      = 0.5;
	sizes[1]      = 1.0;

	useInvAlpha = false;
};
datablock ParticleEmitterData(sniperrifleLVFlashEmitter)
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
   particles = "sniperrifleLVFlashParticle";
};

datablock ParticleData(sniperrifleLVSmokeParticle)
{
	dragCoefficient      = 3;
	gravityCoefficient   = -0.5;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.0;
	lifetimeMS           = 1525;
	lifetimeVarianceMS   = 55;
	textureName          = "base/data/particles/cloud";
	spinSpeed		= 10.0;
	spinRandomMin		= -500.0;
	spinRandomMax		= 500.0;
	colors[0]     = "0.5 0.5 0.5 0.9";
	colors[1]     = "0.5 0.5 0.5 0.0";
	sizes[0]      = 0.15;
	sizes[1]      = 0.25;

	useInvAlpha = false;
};
datablock ParticleEmitterData(sniperrifleLVSmokeEmitter)
{
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;
   ejectionVelocity = 1.0;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "sniperrifleLVSmokeParticle";
};


//bullet trail effects
datablock ParticleData(sniperrifleLVbulletTrailParticle)
{
	dragCoefficient      = 3;
	gravityCoefficient   = -0.0;
	inheritedVelFactor   = 0.0;
	constantAcceleration = 0.0;
	lifetimeMS           = 625;
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
datablock ParticleEmitterData(sniperrifleLVbulletTrailEmitter)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   ejectionVelocity = 0.0;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "sniperrifleLVbulletTrailParticle";
};


datablock ParticleData(sniperrifleLVExplosionParticle)
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
datablock ParticleEmitterData(sniperrifleLVExplosionEmitter)
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
   particles = "sniperrifleLVExplosionParticle";
};


datablock ParticleData(sniperrifleLVExplosionRingParticle)
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
datablock ParticleEmitterData(sniperrifleLVExplosionRingEmitter)
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
   particles = "sniperrifleLVExplosionRingParticle";
};

datablock ExplosionData(sniperrifleLVExplosion)
{
   //explosionShape = "";
	soundProfile = sniperrifleLVbulletHitSound;

   lifeTimeMS = 150;

   particleEmitter = sniperrifleLVExplosionEmitter;
   particleDensity = 10;
   particleRadius = 0.2;

   emitter[0] = sniperrifleLVExplosionRingEmitter;

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


AddDamageType("sniperrifle",   '<bitmap:add-ons/ci/sniperrifleLV> %1',    '%2 <bitmap:add-ons/ci/sniperrifle> %1',0.5,1);
AddDamageType("Sniperrifleheadshot",'<bitmap:add-ons/ci/star><bitmap:add-ons/ci/sniperrifle> %1','%2 <bitmap:add-ons/ci/star><bitmap:add-ons/ci/sniperrifle> %1',0.5,1);

datablock ProjectileData(sniperrifleLVProjectile)
{
   projectileShapeName = "./shapes/bullet.dts";
   directDamage        = 70;
   directDamageType    = $DamageType::sniperrifleLV;
   radiusDamageType    = $DamageType::sniperrifleLV;

   brickExplosionRadius = 0;
   brickExplosionImpact = false;          //destroy a brick if we hit it directly?
   brickExplosionForce  = 25;
   brickExplosionMaxVolume = 200;          //max volume of bricks that we can destroy
   brickExplosionMaxVolumeFloating = 2;  //max volume of bricks that we can destroy if they aren't connected to the ground

   impactImpulse	     = 0;
   verticalImpulse	  = 0;
   explosion           = sniperrifleLVExplosion;
   particleEmitter     = sniperrifleLVbulletTrailEmitter;

   muzzleVelocity      = 2000;
   velInheritFactor    = 1;

   armingDelay         = 00;
   lifetime            = 4000;
   fadeDelay           = 3500;
   bounceElasticity    = 0.5;
   bounceFriction      = 0.20;
   isBallistic         = false;
   gravityMod = 0.0;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";

   headshot=sniperrifleLVheadshotProjectile;
};

datablock ProjectileData(sniperrifleLVheadshotProjectile)
{
   projectileShapeName = "./shapes/bullet.dts";
   directDamage        = 100;
   directDamageType    = $DamageType::sniperrifleheadshot;
   radiusDamageType    = $DamageType::sniperrifle;

   brickExplosionRadius = 0;
   brickExplosionImpact = true;          //destroy a brick if we hit it directly?
   brickExplosionForce  = 25;
   brickExplosionMaxVolume = 200;          //max volume of bricks that we can destroy
   brickExplosionMaxVolumeFloating = 2;  //max volume of bricks that we can destroy if they aren't connected to the ground

   impactImpulse	     = 1200;
   verticalImpulse	  = 1400;
   explosion           = sniperrifleLVExplosion;
   particleEmitter     = sniperrifleLVbulletTrailEmitter;

   muzzleVelocity      = 2000;
   velInheritFactor    = 1;

   armingDelay         = 00;
   lifetime            = 4000;
   fadeDelay           = 3500;
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
datablock ItemData(sniperrifleLVItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./shapes/svd.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "SVD";
	iconName = "./ItemIcons/sniperrifle";
	doColorShift = true;
	colorShiftColor = "0.70 0.70 0.70 1.000";

	 // Dynamic properties defined by the scripts
	image = sniperrifleLVImage;
	canDrop = true;
};




////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(sniperrifleLVImage)
{
   // Basic Item properties
   shapeFile = "./shapes/svd.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = "0 0 0";//(left to right Forward? elevation0.022 0.2 -0.48
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
   projectile = sniperrifleLVProjectile;
   projectileType = Projectile;

	casing = sniperrifleLVShellDebris;
	shellExitDir        = "1.0 -1.3 1.0";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 15.0;	
	shellVelocity       = 7.0;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;

   doColorShift = true;
   colorShiftColor = sniperrifleLVItem.colorShiftColor;//"0.400 0.196 0 1.000";

   //casing = " ";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.15;
	stateTransitionOnTimeout[0]       = "Ready";
	stateSound[0]					= weaponSwitchSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;
	stateSequence[1]	= "Ready";
	stateScript[1]                  = "onReady";



	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "reload";
	stateTimeoutValue[2]            = 0.14;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;

	stateEmitterNode[2]				= "muzzleNode";
	stateSound[2]					= sniperrifleLVShot1Sound;
	stateEjectShell[2]       = true;



	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateTransitionOnTriggerUp[3]     = "Fire2";
	stateSequence[3]	= "Ready";

	stateName[4]			= "Fire2";
	stateSequence[4]                = "reload";
	stateScript[4]                  = "onFire2";
	stateSequence[4]	= "Ready";
	stateTimeoutValue[4]            = 0.01;
	stateTransitionOnTimeout[4]     = "Ready";


};




package headshot{
 function ProjectileData::damage(%this, %obj, %col, %fade, %pos, %normal){
  %this2=%this;
  if(%col.getClassName()$="Player"){
   if(getword(%pos,2)>getword(%col.getWorldBoxCenter(),2)-3.3){
    %name = %this.GetName();
    if(%name$="sniperrifleLVProjectile"){
     %this2=sniperrifleLVheadshotprojectile;
     showheadshot(%obj,%col);
     %obj.emote(WinStarProjectile);
     %obj.client.play2d(rewardSound);
    }
   }
  }
   return Parent::damage(%this2, %obj, %col, %fade, %pos, %normal);
 }
};
ActivatePackage(headshot);

function showheadshot(%obj,%col){
 if(!$TG_headshots){
  bottomprint(%obj.client,"<bitmap:add-ons/ci/star> \c3Headshot "@%col.client.name,3,4);
  bottomprint(%col.client,"<bitmap:add-ons/ci/star> \c7"@%obj.client.name@" Headshot",3,4);
 }
}

