//SniperRifle.cs
//exec("add-ons/weapon_sniperrifle.cs");

//audio
datablock AudioProfile(SniperRifleShot1Sound)
{
   filename    = "./sound/m82.ogg";
   description = AudioClose3d;
   preload = true;
};
datablock AudioProfile(sniperriflebulletHitSound)
{
   filename    = "./sound/arrowHit.wav";
   description = AudioClose3d;
   preload = true;
};

//shell
datablock DebrisData(SniperRifleShellDebris)
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
datablock ParticleData(SniperRifleFlashParticle)
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
datablock ParticleEmitterData(SniperRifleFlashEmitter)
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
   particles = "SniperRifleFlashParticle";
};

datablock ParticleData(SniperRifleSmokeParticle)
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
datablock ParticleEmitterData(SniperRifleSmokeEmitter)
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
   particles = "SniperRifleSmokeParticle";
};


//bullet trail effects
datablock ParticleData(SniperRiflebulletTrailParticle)
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
datablock ParticleEmitterData(SniperRiflebulletTrailEmitter)
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
   particles = "SniperRiflebulletTrailParticle";
};


datablock ParticleData(SniperRifleExplosionParticle)
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
datablock ParticleEmitterData(SniperRifleExplosionEmitter)
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
   particles = "SniperRifleExplosionParticle";
};


datablock ParticleData(SniperRifleExplosionRingParticle)
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
datablock ParticleEmitterData(SniperRifleExplosionRingEmitter)
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
   particles = "SniperRifleExplosionRingParticle";
};

datablock ExplosionData(SniperRifleExplosion)
{
   //explosionShape = "";
	soundProfile = sniperriflebulletHitSound;

   lifeTimeMS = 150;

   particleEmitter = SniperRifleExplosionEmitter;
   particleDensity = 10;
   particleRadius = 0.2;

   emitter[0] = SniperRifleExplosionRingEmitter;

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


AddDamageType("Sniperrifle",   '<bitmap:add-ons/ci/barrett> %1',    '%2 <bitmap:add-ons/ci/barrett> %1',0.5,1);
datablock ProjectileData(SniperRifleProjectile)
{
   projectileShapeName = "./shapes/bullet.dts";
   directDamage        = 150;
   directDamageType    = $DamageType::Sniperrifle;
   radiusDamageType    = $DamageType::Sniperrifle;

   brickExplosionRadius = 0;
   brickExplosionImpact = true;          //destroy a brick if we hit it directly?
   brickExplosionForce  = 25;
   brickExplosionMaxVolume = 200;          //max volume of bricks that we can destroy
   brickExplosionMaxVolumeFloating = 200;  //max volume of bricks that we can destroy if they aren't connected to the ground

   impactImpulse	     = 1200;
   verticalImpulse	  = 1400;
   explosion           = SniperRifleExplosion;
   particleEmitter     = SniperRiflebulletTrailEmitter;

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
datablock ItemData(SniperRifleItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./shapes/barrettm82.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Barrett M82";
	iconName = "./ItemIcons/barrett";
	doColorShift = false;
	colorShiftColor = "0.25 0.25 0.25 1.000";

	 // Dynamic properties defined by the scripts
	image = SniperRifleImage;
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
datablock ShapeBaseImageData(SniperRifleImage)
{
   // Basic Item properties
   shapeFile = "./shapes/barrettm82.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = "0.25 1 -0.2";
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
   projectile = SniperRifleProjectile;
   projectileType = Projectile;

	casing = SniperRifleShellDebris;
	shellExitDir        = "1.0 -1.3 1.0";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 15.0;	
	shellVelocity       = 7.0;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;
   minShotTime = 2000;   //minimum time allowed between shots (needed to prevent equip/dequip exploit)

   doColorShift = false;
   colorShiftColor = SniperRifleItem.colorShiftColor;//"0.400 0.196 0 1.000";

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
	stateSound[0]					= weaponSwitchSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;
	stateSequence[1]	= "Ready";

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Smoke";
	stateTimeoutValue[2]            = 0.14;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;
	stateEmitter[2]					= SniperRifleFlashEmitter;
	stateEmitterTime[2]				= 0.05;
	stateEmitterNode[2]				= "muzzleNode";
	stateSound[2]					= SniperRifleShot1Sound;
	stateEjectShell[2]       = true;

	stateName[3] = "Smoke";
	stateEmitter[3]					= SniperRifleSmokeEmitter;
	stateEmitterTime[3]				= 2.00;
	stateEmitterNode[3]				= "muzzlePoint";
	stateTimeoutValue[3]            = 2.00;
	stateTransitionOnTimeout[3]     = "Reload";
	stateWaitForTimeout[3]			= true;

	stateName[4]			= "Reload";
	stateSequence[4]                = "Reload";
	stateTransitionOnTriggerUp[4]     = "Ready";
	//stateSequence[4]	= "Ready";

};

function SniperRifleImage::onFire(%this,%obj,%slot)
{
	if(%obj.getDamagePercent() < 1.0)
		%obj.playThread(2, shiftAway);
	Parent::onFire(%this,%obj,%slot);	
}