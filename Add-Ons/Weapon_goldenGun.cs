//goldengun.cs

//audio
datablock AudioProfile(goldengunShot1Sound)
{
   filename    = "./sound/gunShot1.wav";
   description = AudioClose3d;
   preload = true;
};
datablock AudioProfile(bulletHitSound)
{
   filename    = "./sound/arrowHit.wav";
   description = AudioClose3d;
   preload = true;
};
datablock AudioProfile(reloadSound)
{
   filename    = "./sound/shotgunreload.wav";
   description = AudioClose3d;
   preload = true;
};


//shell
datablock DebrisData(goldengunShellDebris)
{
	shapeFile = "./shapes/gunShell.dts";
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
datablock ParticleData(goldengunFlashParticle)
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
datablock ParticleEmitterData(goldengunFlashEmitter)
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
   particles = "goldengunFlashParticle";
};

datablock ParticleData(goldengunSmokeParticle)
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
datablock ParticleEmitterData(goldengunSmokeEmitter)
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
   particles = "goldengunSmokeParticle";
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


datablock ParticleData(goldengunExplosionParticle)
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
datablock ParticleEmitterData(goldengunExplosionEmitter)
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
   particles = "goldengunExplosionParticle";
};


datablock ParticleData(goldengunExplosionRingParticle)
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
datablock ParticleEmitterData(goldengunExplosionRingEmitter)
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
   particles = "goldengunExplosionRingParticle";
};

datablock ExplosionData(goldengunExplosion)
{
   //explosionShape = "";
	soundProfile = bulletHitSound;

   lifeTimeMS = 150;

   particleEmitter = goldengunExplosionEmitter;
   particleDensity = 10;
   particleRadius = 0.2;

   emitter[0] = goldengunExplosionRingEmitter;

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


AddDamageType("Gun",   '<bitmap:add-ons/ci/gun> %1',    '%2 <bitmap:add-ons/ci/gun> %1',0.5,1);
datablock ProjectileData(goldengunProjectile)
{
   projectileShapeName = "./shapes/bullet.dts";
   directDamage        = 1000;
   directDamageType    = $DamageType::Gun;
   radiusDamageType    = $DamageType::Gun;

   brickExplosionRadius = 0;
   brickExplosionImpact = true;          //destroy a brick if we hit it directly?
   brickExplosionForce  = 10;
   brickExplosionMaxVolume = 100;          //max volume of bricks that we can destroy
   brickExplosionMaxVolumeFloating = 200;  //max volume of bricks that we can destroy if they aren't connected to the ground

   impactImpulse	     = 0;
   verticalImpulse	  = 0;
   explosion           = goldengunExplosion;
   particleEmitter     = bulletTrailEmitter;

   muzzleVelocity      = 200;
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
datablock ItemData(goldengunItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./shapes/goldengun.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Golden Gun";
	iconName = "./ItemIcons/gun";
	doColorShift = true;
	colorShiftColor = "0.800 0.700 0.100 1.000";

	 // Dynamic properties defined by the scripts
	image = goldengunImage;
	canDrop = true;
};

//function BowItem::onUse(%this, %player, %InvPosition)
//{
//	//check for quiver 
//	//if you dont have it, regular bow
//	//if you do, super bow
//
//	%client = %player.client;
//
//	%mountPoint = %this.image.mountPoint;
//	%mountedImage = %player.getMountedImage(%mountPoint); 
//
//
//	if(%mountedImage)
//	{
//		if(%mountedImage == bowImage.getId() || %mountedImage == superbowImage.getId())
//		{
//			//some kind of bow mounted so, unmount it
//			%player.unMountImage(%mountPoint);
//			messageClient(%client, 'MsgHilightInv', '', -1);
//			%player.currWeaponSlot = -1;
//		}
//		else
//		{
//			//something other than bow mounted, so do bow selection and mount
//			if(%player.getMountedImage($BackSlot))
//			{
//				if(%player.getMountedImage($BackSlot) == quiverImage.getId())
//				{
//					%player.mountimage(superBowImage, $RightHandSlot, 1, %skin);
//					messageClient(%client, 'MsgHilightInv', '', %InvPosition);
//					%player.currWeaponSlot = %invPosition;
//				}
//				else
//				{
//					%player.mountimage(bowImage, $RightHandSlot, 1, %skin);
//					messageClient(%client, 'MsgHilightInv', '', %InvPosition);
//					%player.currWeaponSlot = %invPosition;
//				}
//			}
//			else
//			{
//				%player.mountimage(bowImage, $RightHandSlot, 1, %skin);
//				messageClient(%client, 'MsgHilightInv', '', %InvPosition);
//				%player.currWeaponSlot = %invPosition;
//			}
//		}
//		
//	}
//	else
//	{
//		//nothing mounted so do bow selection and mount
//		//something other than bow mounted, so do bow selection and mount
//		if(%player.getMountedImage($BackSlot))
//		{
//			if(%player.getMountedImage($BackSlot) == quiverImage.getId())
//			{
//				%player.mountimage(superBowImage, $RightHandSlot, 1, %skin);
//				messageClient(%client, 'MsgHilightInv', '', %InvPosition);
//				%player.currWeaponSlot = %invPosition;
//			}
//			else
//			{
//				%player.mountimage(bowImage, $RightHandSlot, 1, %skin);
//				messageClient(%client, 'MsgHilightInv', '', %InvPosition);
//				%player.currWeaponSlot = %invPosition;
//			}
//		}
//		else
//		{
//			%player.mountimage(bowImage, $RightHandSlot, 1, %skin);
//			messageClient(%client, 'MsgHilightInv', '', %InvPosition);
//			%player.currWeaponSlot = %invPosition;
//		}
//	}
//}

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(goldengunImage)
{
   // Basic Item properties
   shapeFile = "./shapes/goldengun.dts";
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
   projectile = goldengunProjectile;
   projectileType = Projectile;

	casing = goldengunShellDebris;
	shellExitDir        = "1.0 -1.3 1.0";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 15.0;	
	shellVelocity       = 7.0;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;

   doColorShift = true;
   colorShiftColor = goldengunItem.colorShiftColor;//"0.400 0.196 0 1.000";

   //casing = " ";

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
	stateEmitter[2]			  = goldengunFlashEmitter;
	stateEmitterTime[2]		  = 0.05;
	stateEmitterNode[2]		  = "muzzleNode";
	stateSound[2]			  = goldengunShot1Sound;

	stateName[3] 			  = "Smoke";
	stateEmitter[3]			  = goldengunSmokeEmitter;
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
	stateSound[4]			  = ReloadSound;
	
	stateName[5]			  = "Wait";
	stateTimeoutValue[5]		  = 0.6;
	stateTransitionOnTimeout[5]	  = "Ready";
};

function goldengunImage::oncharge(%this, %obj, %slot)
{
%obj.playThread(1, armready);
}







