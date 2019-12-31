exec("Add-Ons/Weapon_Gun.cs");

//audio
datablock AudioProfile(TF2ScatterGunReloadSound)
{
   filename    = "./sound/shotgunReload.wav";
   description = AudioClose3d;
   preload = true;
};

//shell
datablock DebrisData(TF2ScatterGunShellDebris)
{
	shapeFile = "./shapes/shotgunShell.dts";
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

datablock ParticleData(TF2ScatterGunSmokeParticle)
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
	colors[0]     = "0.5 0.5 0.5 0.5";
	colors[1]     = "0.5 0.5 0.5 0.0";
	sizes[0]      = 0.15;
	sizes[1]      = 0.1;

	useInvAlpha = false;
};
datablock ParticleEmitterData(TF2ScatterGunSmokeEmitter)
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
   particles = "TF2ScatterGunSmokeParticle";
};

//projectile
AddDamageType("TF2ScatterGun",   '<bitmap:add-ons/ci/TF2ScatterGun> %1',    '%2 <bitmap:add-ons/ci/TF2ScatterGun> %1',1,1);
datablock ProjectileData(TF2ScatterGunProjectile)
{
   projectileShapeName = "./shapes/bullet.dts";
   directDamage        = 45;
   directDamageType    = $DamageType::TF2ScatterGun;
   radiusDamageType    = $DamageType::TF2ScatterGun;

   brickExplosionRadius = 0.2;
   brickExplosionImpact = true;          //destroy a brick if we hit it directly?
   brickExplosionForce  = 15;
   brickExplosionMaxVolume = 20;          //max volume of bricks that we can destroy
   brickExplosionMaxVolumeFloating = 30;  //max volume of bricks that we can destroy if they aren't connected to the ground

   impactImpulse	     = 1200;
   verticalImpulse     = 1000;
   explosion           = gunExplosion;
   particleEmitter     = bulletTrailEmitter;

   muzzleVelocity      = 150;
   velInheritFactor    = 1;

   armingDelay         = 0;
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
datablock ItemData(TF2ScatterGunItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./shapes/TF2ScatterGun.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "TF2 Scatter Gun";
	iconName = "./ItemIcons/TF2ScatterGun";
	doColorShift = false;
	colorShiftColor = "0.5 0.5 0.5 1.000";

	 // Dynamic properties defined by the scripts
	image = TF2ScatterGunImage;
	canDrop = true;
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(TF2ScatterGunImage)
{
   // Basic Item properties
   shapeFile = "./shapes/TF2ScatterGun.dts";
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
   item = TF2ScatterGunItem;
   ammo = " ";
   projectile = TF2ScatterGunProjectile;
   projectileType = Projectile;

   casing = TF2ScatterGunShellDebris;
   shellExitDir        = "1.0 0.1 1.0";
   shellExitOffset     = "0 0 0";
   shellExitVariance   = 10.0;	
   shellVelocity       = 5.0;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;

   doColorShift = false;
   colorShiftColor = TF2ScatterGunItem.colorShiftColor;

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]                    = "Activate";
	stateTimeoutValue[0]            = 0.15;
	stateTransitionOnTimeout[0]     = "Ready";
	stateSound[0]			  = weaponSwitchSound;

	stateName[1]                    = "Ready";
	stateTransitionOnTriggerDown[1] = "Fire";
	stateAllowImageChange[1]        = true;
	stateTimeoutValue[1]		  = 0.01;

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Smoke";
	stateTimeoutValue[2]            = 0.14;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]		  = true;
	stateEmitter[2]			  = gunFlashEmitter;
	stateEmitterTime[2]		  = 0.05;
	stateEmitterNode[2]		  = "muzzleNode";
	stateSound[2]			  = gunShot1Sound;

	stateName[3] 			  = "Smoke";
	stateEmitter[3]			  = TF2ScatterGunSmokeEmitter;
	stateEmitterTime[3]		  = 0.1;
	stateEmitterNode[3]		  = "muzzleNode";
	stateTimeoutValue[3]            = 0.5;
	stateTransitionOnTimeout[3]     = "Reload";

	stateName[4]			  = "Reload";
	stateTimeoutValue[4]		  = 0.3;
	stateSequence[4]			  = "reload";
	stateTransitionOnTimeout[4]	  = "Wait";
	stateWaitForTimeout[4]		  = true;
	stateEjectShell[4]       	  = true;
	stateSound[4]			  = TF2ScatterGunReloadSound;
	
	stateName[5]			  = "Wait";
	stateTimeoutValue[5]		  = 0.6;
	stateTransitionOnTimeout[5]	  = "Ready";
};

function TF2ScatterGunImage::onFire(%this,%obj,%slot)
{
	%obj.setVelocity(VectorAdd(%obj.getVelocity(),VectorScale(%obj.client.player.getEyeVector(),"-3")));
	%obj.playThread(2, shiftAway);

	%projectile = %this.projectile;
	%spread = 0.0015;
	%shellcount = 3;

	for(%shell=0; %shell<%shellcount; %shell++)
	{
		%vector = %obj.getMuzzleVector(%slot);
		%objectVelocity = %obj.getVelocity();
		%vector1 = VectorScale(%vector, %projectile.muzzleVelocity);
		%vector2 = VectorScale(%objectVelocity, %projectile.velInheritFactor);
		%velocity = VectorAdd(%vector1,%vector2);
		%x = (getRandom() - 0.5) * 10 * 3.1415926 * %spread;
		%y = (getRandom() - 0.5) * 10 * 3.1415926 * %spread;
		%z = (getRandom() - 0.5) * 10 * 3.1415926 * %spread;
		%mat = MatrixCreateFromEuler(%x @ " " @ %y @ " " @ %z);
		%velocity = MatrixMulVector(%mat, %velocity);

		%p = new (%this.projectileType)()
		{
			dataBlock = %projectile;
			initialVelocity = %velocity;
			initialPosition = %obj.getMuzzlePoint(%slot);
			sourceObject = %obj;
			sourceSlot = %slot;
			client = %obj.client;
		};
		MissionCleanup.add(%p);
	}
	return %p;
}
