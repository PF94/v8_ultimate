//TommyGun.cs

//audio
datablock AudioProfile(TommyGunFireSound)
{
   filename    = "./sound/TommyGun.wav";
   description = AudioClose3d;
   preload = true;
};
datablock AudioProfile(TommyGunExplosionSound)
{
   filename    = "./sound/smash.wav";
   description = AudioClose3d;
   preload = true;
};

//shell
datablock DebrisData(TommyGunShellDebris)
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
datablock ParticleData(TommyGunFlashParticle)
{
	dragCoefficient      = 3;
	gravityCoefficient   = -0.5;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.0;
	lifetimeMS           = 30;
	lifetimeVarianceMS   = 20;
	textureName          = "base/data/particles/cloud";
	spinSpeed		= 100.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	colors[0]     = "0.7 0.2 0.2 0.9";
	colors[1]     = "1.0 1.0 1.0 0.8";
	sizes[0]      = 0.5;
	sizes[1]      = 0.4;

	useInvAlpha = false;
};
datablock ParticleEmitterData(TommyGunFlashEmitter)
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
   particles = "TommyGunFlashParticle";
};

//TommyGun trail
datablock ParticleData(TommyGunTrailParticle)
{
	dragCoefficient		= 9.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 100;
	lifetimeVarianceMS	= 0;
	spinSpeed		= 0.0;
	spinRandomMin		= 0.0;
	spinRandomMax		= 0.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "base/data/particles/dot";
	//animTexName		= "base/data/particles/dot";

	// Interpolation variables
	colors[0]	= "1 1 0.7 0.5";
	colors[1]	= "1 0.8 0.6 0.0";
	sizes[0]	= 0.4;
	sizes[1]	= 0.32;
	times[0]	= 0.0;
	times[1]	= 1.0;
};

datablock ParticleEmitterData(TommyGunTrailEmitter)
{
   ejectionPeriodMS = 0;
   periodVarianceMS = 0;

   ejectionVelocity = 0; //0.25;
   velocityVariance = 0; //0.10;

   ejectionOffset = 0;

   thetaMin         = 0.0;
   thetaMax         = 90.0; 

   particles = TommyGunTrailParticle;
};

//effects
datablock ParticleData(TommyGunExplosionParticle)
{
	dragCoefficient      = 5;
	gravityCoefficient   = 0.1;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.0;
	lifetimeMS           = 500;
	lifetimeVarianceMS   = 300;
	useInvAlpha	= true;
	textureName          = "base/data/particles/cloud";
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	colors[0]     = "0.46 0.36 0.26 1.0";
	colors[1]     = "0.46 0.46 0.36 0.0";
	sizes[0]      = 0.30;
	sizes[1]      = 0.15;
};

datablock ParticleEmitterData(TommyGunExplosionEmitter)
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
   particles = "TommyGunExplosionParticle";
};

datablock ExplosionData(TommyGunExplosion)
{
   //explosionShape = "";
	soundProfile = TommyGunExplosionSound;

   lifeTimeMS = 150;

   particleEmitter = TommyGunExplosionEmitter;
   particleDensity = 10;
   particleRadius = 0.2;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = false;
   camShakeFreq = "10.0 11.0 10.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.5;
   camShakeRadius = 10.0;

   // Dynamic light
   lightStartRadius = 0;
   lightEndRadius = 0;
   lightStartColor = "0.0 0.0 0.0";
   lightEndColor = "0 0 0";
};


AddDamageType("Gun",   '<bitmap:add-ons/ci/gun> %1',    '%2 <bitmap:add-ons/ci/gun> %1',0.5,1);
datablock ProjectileData(TommyGunProjectile)
{
   projectileShapeName = "./shapes/bullet.dts";
   directDamage        = 60;
   directDamageType    = $DamageType::Gun;
   radiusDamageType    = $DamageType::Gun;

   brickExplosionRadius = 0;
   brickExplosionImpact = true;          //destroy a brick if we hit it directly?
   brickExplosionForce  = 10;
   brickExplosionMaxVolume = 1;          //max volume of bricks that we can destroy
   brickExplosionMaxVolumeFloating = 2;  //max volume of bricks that we can destroy if they aren't connected to the ground

   impactImpulse	     = 700;
   verticalImpulse	  = 1000;
   explosion           = TommyGunExplosion;
   particleEmitter     = TommyGunTrailEmitter;

   muzzleVelocity      = 600;
   velInheritFactor    = 0;

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
datablock ItemData(TommyGun)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./shapes/TommyGun.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "TommyGun";
	iconName = "./ItemIcons/none";
	doColorShift = true;
	colorShiftColor = "0.25 0.25 0.25 1.000";

	 // Dynamic properties defined by the scripts
	image = TommyGunImage;
	canDrop = true;
};

function TommyGun::onUse(%this, %player, %InvPosition)
{
	//check for quiver 
	//if you dont have it, regular TommyGun
	//if you do, super TommyGun

	%client = %player.client;

	%mountPoint = %this.image.mountPoint;
	%mountedImage = %player.getMountedImage(%mountPoint); 

	%skin = %this.skinname;

	if(%mountedImage)
	{
		if(%mountedImage == TommyGunImage.getId() || %mountedImage == superTommyGunImage.getId())
		{
			//some kind of TommyGun mounted so, unmount it
			%player.unMountImage(%mountPoint);
			messageClient(%client, 'MsgHilightInv', '', -1);
			%player.currWeaponSlot = -1;
		}
		else
		{
			//something other than TommyGun mounted, so do TommyGun selection and mount
			if(%player.getMountedImage($BackSlot))
			{
				if(%player.getMountedImage($BackSlot) == quiverImage.getId())
				{
					%player.mountimage(superTommyGunImage, $RightHandSlot, 1, %skin);
					messageClient(%client, 'MsgHilightInv', '', %InvPosition);
					%player.currWeaponSlot = %invPosition;
				}
				else
				{
					%player.mountimage(TommyGunImage, $RightHandSlot, 1, %skin);
					messageClient(%client, 'MsgHilightInv', '', %InvPosition);
					%player.currWeaponSlot = %invPosition;
				}
			}
			else
			{
				%player.mountimage(TommyGunImage, $RightHandSlot, 1, %skin);
				messageClient(%client, 'MsgHilightInv', '', %InvPosition);
				%player.currWeaponSlot = %invPosition;
			}
		}
		
	}
	else
	{
		//nothing mounted so do TommyGun selection and mount
		//something other than TommyGun mounted, so do TommyGun selection and mount
		if(%player.getMountedImage($BackSlot))
		{
			if(%player.getMountedImage($BackSlot) == quiverImage.getId())
			{
				%player.mountimage(superTommyGunImage, $RightHandSlot, 1, %skin);
				messageClient(%client, 'MsgHilightInv', '', %InvPosition);
				%player.currWeaponSlot = %invPosition;
			}
			else
			{
				%player.mountimage(TommyGunImage, $RightHandSlot, 1, %skin);
				messageClient(%client, 'MsgHilightInv', '', %InvPosition);
				%player.currWeaponSlot = %invPosition;
			}
		}
		else
		{
			%player.mountimage(TommyGunImage, $RightHandSlot, 1, %skin);
			messageClient(%client, 'MsgHilightInv', '', %InvPosition);
			%player.currWeaponSlot = %invPosition;
		}
	}
}

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(TommyGunImage)
{
   // Basic Item properties
   shapeFile = "./shapes/TommyGun.dts";
   emap = true;
   PreviewFileName = "rtb/data/shapes/bricks/Previews/TommyGun.png";
	cloakable = false;
   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
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
   item = TommyGun;
   ammo = " ";
   projectile = TommyGunProjectile;
   projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;

	casing = TommyGunShellDebris;
	shellExitDir        = "1.0 -1.3 1.0";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 15.0;	
	shellVelocity       = 7.0;

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
	stateEmitter[2]					= TommyGunFlashEmitter;
	stateEmitterTime[2]				= 0.05;
	stateEmitterNode[2]				= "muzzleNode";
	stateSound[2]					= TommyGunFireSound;

	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]            = 0.02;
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

function TommyGunImage::onFire(%this,%obj,%slot)
{
	if(%obj.getDamagePercent() < 1.0)
	Parent::onFire(%this,%obj,%slot);	
}
