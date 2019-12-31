//wrenchRemote.cs

//audio


//muzzle flash effects
datablock ParticleData(wrenchRemoteFlashParticle)
{
	dragCoefficient      = 3;
	gravityCoefficient   = 0;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.0;
	lifetimeMS           = 500;
	lifetimeVarianceMS   = 0;
	textureName          = "base/data/particles/ring";
	spinSpeed		= 0.0;
	spinRandomMin		= -000.0;
	spinRandomMax		= 000.0;
	colors[0]     = "0.0 0.9 0.9 0.9";
	colors[1]     = "0.0 0.4 0.4 0.0";
	sizes[0]      = 0.1;
	sizes[1]      = 3.0;

	useInvAlpha = false;
};
datablock ParticleEmitterData(wrenchRemoteFlashEmitter)
{
   ejectionPeriodMS = 100;
   periodVarianceMS = 0;
   ejectionVelocity = 0;
   velocityVariance = 0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 00;
   phiReferenceVel  = 0;
   phiVariance      = 0;
   overrideAdvance = false;
   particles = "wrenchRemoteFlashParticle";
};

datablock ParticleEmitterData(wrenchRemoteExplosionEmitter)
{
   ejectionPeriodMS = 100;
   periodVarianceMS = 0;
   ejectionVelocity = 0;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 0;
   phiReferenceVel  = 0;
   phiVariance      = 0;
   overrideAdvance = false;
   particles = "wrenchRemoteFlashParticle";
};

datablock ExplosionData(wrenchRemoteExplosion)
{
   //explosionShape = "";
	//soundProfile = wrenchRemotebulletHitSound;

   lifeTimeMS = 150;

   particleEmitter = wrenchRemoteExplosionEmitter;
   particleDensity = 0.1;
   particleRadius = 0.1;

   emitter[0] = wrenchRemoteExplosionEmitter;

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


datablock ProjectileData(wrenchRemoteProjectile)
{
   projectileShapeName = "";
   directDamage        = 0;
   directDamageType    = $DamageType::Generic;
   radiusDamageType    = $DamageType::Generic;

   brickExplosionRadius = 0;
   brickExplosionImpact = true;          //destroy a brick if we hit it directly?
   brickExplosionForce  = 0;
   brickExplosionMaxVolume = 1;          //max volume of bricks that we can destroy
   brickExplosionMaxVolumeFloating = 2;  //max volume of bricks that we can destroy if they aren't connected to the ground

   impactImpulse	     = 100;
   verticalImpulse	  = 4000;
   explosion           = wrenchRemoteExplosion;
   particleEmitter     = "";
   explodeonDeath = 1;
   muzzleVelocity      = 360;
   velInheritFactor    = 1;

   armingDelay         = 00;
   lifetime            = 500;
   fadeDelay           = 3500;
   bounceElasticity    = 0.5;
   bounceFriction      = 0.20;
   isBallistic         = false;
   gravityMod = 0.0;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";
};

////////////////
//Weapon image//
////////////////
datablock ShapeBaseImageData(wrenchRemoteImage)
{
   // Basic Item properties
   shapeFile = "base/data/shapes/brickweapon.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = 0; //"0.7 1.2 -0.5";
   rotation = eulerToMatrix( "0 0 0" );

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this Weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = wrenchRemoteItem;
   ammo = " ";
   projectile = wrenchRemoteProjectile;
   projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;

   doColorShift = true;
   colorShiftColor = wrenchRemoteItemItem.colorShiftColor;//"0.400 0.196 0 1.000";

   //casing = " ";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.15;
	stateTransitionOnTimeout[0]       = "Ready";
	stateSound[0]					= WeaponSwitchSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;
	stateSequence[1]	= "Ready";

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Reload";
	stateTimeoutValue[2]            = 0.14;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateEmitter[2] = "WrenchRemoteFlashEmitter";
	stateEmitterTime[2] = 0.1;
	stateEmitterNode[2]				= "muzzleNode";

	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;
	//stateEjectShell[2]       = true;


	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateTransitionOnTriggerUp[3]     = "Ready";
	stateSequence[3]	= "Ready";

};

function wrenchRemoteImage::onFire(%this,%obj,%slot)
{
	Parent::onFire(%this,%obj,%slot);	
}

function servercmdRemote(%c)
{
 if(%c.isSuperAdmin && isObject(%c.player))
 {
  %c.player.mountImage(wrenchRemoteImage,0);
  fixArmReady(%c.player);
 }
}

function wrenchRemoteProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal) 
{
	if(isObject(%col) && %col.getClassName() $= "fxDTSBrick" && %col.evName[%col.curState] $= "Wait For")
	{
	 %col.ticknum = -1;%col.curState++;
	 %brick = %col;
	 %state = %col.curstate;
               if(%brick.evName[%state - 1] !$= "" && %brick.evParam[%state,2] && $EventBricks::Server::Cancel[%obj.evName[%state - 1]])
	 {
		call("WrenchEvCancel" @ strReplace(%brick.evName[%state - 1]," ","_"),%brick,%state-1);
	 }
	}
}