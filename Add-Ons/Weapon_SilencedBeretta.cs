//SilencedBeretta.cs

//audio
datablock AudioProfile(SilencedBerettaShot1Sound)
{
   filename    = "./sound/silencer2.wav";
   description = AudioClose3d;
   preload = true;
};
datablock AudioProfile(bulletHitSound)
{
   filename    = "./sound/arrowHit.wav";
   description = AudioClose3d;
   preload = true;
};


//shell
datablock DebrisData(SilencedBerettaShellDebris)
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
datablock ParticleData(SilencedBerettaFlashParticle)
{
	dragCoefficient      = 3;
	gravityCoefficient   = -0.5;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.0;
	lifetimeMS           = 25;
	lifetimeVarianceMS   = 15;
	textureName          = "base/data/particles/star4";
	spinSpeed		= 10.0;
	spinRandomMin		= -500.0;
	spinRandomMax		= 500.0;
	colors[0]     = "0.9 0.9 0.0 0.9";
	colors[1]     = "0.9 0.5 0.0 0.0";
	sizes[0]      = 0.5;
	sizes[1]      = 1.0;

	useInvAlpha = false;
};
datablock ParticleEmitterData(SilencedBerettaFlashEmitter)
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
   particles = "SilencedBerettaFlashParticle";
};

datablock ParticleData(SilencedBerettaSmokeParticle)
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
datablock ParticleEmitterData(SilencedBerettaSmokeEmitter)
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 0;
   ejectionVelocity = 1.0;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "SilencedBerettaSmokeParticle";
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


datablock ParticleData(SilencedBerettaExplosionParticle)
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
datablock ParticleEmitterData(SilencedBerettaExplosionEmitter)
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
   particles = "SilencedBerettaExplosionParticle";
};


datablock ParticleData(SilencedBerettaExplosionRingParticle)
{
	dragCoefficient      = 8;
	gravityCoefficient   = -0.5;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.0;
	lifetimeMS           = 300;
	lifetimeVarianceMS   = 100;
	textureName          = "base/data/particles/star4";
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	colors[0]     = "1 1 0.0 0.9";
	colors[1]     = "0.9 0.0 0.0 0.0";
	sizes[0]      = 0.2;
	sizes[1]      = 0.2;

	useInvAlpha = false;
};
datablock ParticleEmitterData(SilencedBerettaExplosionRingEmitter)
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
   particles = "SilencedBerettaExplosionRingParticle";
};

datablock ExplosionData(SilencedBerettaExplosion)
{
   //explosionShape = "";
	soundProfile = bulletHitSound;

   lifeTimeMS = 150;

   particleEmitter = SilencedBerettaExplosionEmitter;
   particleDensity = 10;
   particleRadius = 0.2;

   emitter[0] = SilencedBerettaExplosionRingEmitter;

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


AddDamageType("SilencedBeretta",   '<bitmap:add-ons/ci/SilencedBeretta> %1',    '%2 <bitmap:add-ons/ci/SilencedBeretta> %1',0.5,1);
datablock ProjectileData(SilencedBerettaProjectile)
{
   projectileShapeName = "./shapes/bullet.dts";
   directDamage        = 40;
   directDamageType    = $DamageType::SilencedBeretta;
   radiusDamageType    = $DamageType::SilencedBeretta;

   brickExplosionRadius = 0;
   brickExplosionImpact = true;          //destroy a brick if we hit it directly?
   brickExplosionForce  = 5;
   brickExplosionMaxVolume = 1;          //max volume of bricks that we can destroy
   brickExplosionMaxVolumeFloating = 2;  //max volume of bricks that we can destroy if they aren't connected to the ground

   impactImpulse	     = 700;
   verticalImpulse	  = 1000;
   explosion           = SilencedBerettaExplosion;
   particleEmitter     = bulletTrailEmitter;

   muzzleVelocity      = 90;
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
datablock ItemData(SilencedBerettaItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./shapes/SilencedBeretta.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	

	//gui stuff
	uiName = "SilencedBeretta";
	iconName = "./ItemIcons/Beretta";
	doColorShift = true;
	colorShiftColor = "0.5 0.5 0.5 1.000";

	 // Dynamic properties defined by the scripts
	image = SilencedBerettaImage;
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
datablock ShapeBaseImageData(SilencedBerettaImage)
{
   // Basic Item properties
   shapeFile = "./shapes/SilencedBeretta.dts";
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
   projectile = SilencedBerettaProjectile;
   projectileType = Projectile;

	casing = SilencedBerettaShellDebris;
	shellExitDir        = "1.0 -1.3 1.0";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 15.0;	
	shellVelocity       = 7.0;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;

   doColorShift = true;
   colorShiftColor = SilencedBerettaItem.colorShiftColor;//"0.400 0.196 0 1.000";

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
	stateEmitter[2]					= SilencedBerettaFlashEmitter;
	stateEmitterTime[2]				= 0.05;
	stateEmitterNode[2]				= "muzzleNode";
	stateSound[2]					= SilencedBerettaShot1Sound;
	stateEjectShell[2]       = true;

	stateName[3] = "Smoke";
	stateEmitter[3]					= SilencedBerettaSmokeEmitter;
	stateEmitterTime[3]				= 0.05;
	stateEmitterNode[3]				= "muzzleNode";
	stateTimeoutValue[3]            = 0.01;
	stateTransitionOnTimeout[3]     = "Reload";

	stateName[4]			= "Reload";
	stateSequence[4]                = "Reload";
	stateTransitionOnTriggerUp[4]     = "Ready";
	stateSequence[4]	= "Ready";

};

function SilencedBerettaImage::onFire(%this,%obj,%slot)
{
	if(%obj.getDamagePercent() < 1.0)
		%obj.playThread(2, shiftAway);
	Parent::onFire(%this,%obj,%slot);	
}
