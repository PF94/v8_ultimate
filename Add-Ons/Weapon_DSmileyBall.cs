//DSmileyBall.cs
//The Bouncy Ball from Garry's Mod

//DSmileyBall trail
datablock ParticleData(DSmileyBallTrailParticle)
{
	dragCoefficient      = 0;
	gravityCoefficient   = -0.0;
	inheritedVelFactor   = 0.0;
	constantAcceleration = 0.0;
	lifetimeMS           = 45;
	lifetimeVarianceMS   = 0;
	textureName          = "base/data/particles/DSmileyBall";
	spinSpeed		= 0.0;
	spinRandomMin		= -0.0;
	spinRandomMax		= 0.0;
	colors[0]     = "1.0 1.0 1.0 0.4";
	colors[1]     = "1.0 1.0 1.0 0.0";
	sizes[0]      = 0.85;
	sizes[1]      = 0.85;

	useInvAlpha = true;
};

datablock ParticleEmitterData(DSmileyBallTrailEmitter)
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
   particles = DSmileyBallTrailParticle;
};

datablock ExplosionData(DSmileyBallExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 150;

   //particleEmitter = none;
   //particleDensity = 30;
   //particleRadius = 1.0;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = false;
   camShakeFreq = "7.0 8.0 7.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.5;
   camShakeRadius = 15.0;

   // Dynamic light
   lightStartRadius = 0;
   lightEndRadius = 0;
   lightStartColor = "0 0 0";
   lightEndColor = "0 0 0";
};

//projectile
AddDamageType("DSmileyBallDirect",   '<bitmap:add-ons/ci/DSmileyBall> %1',    '%2 <bitmap:add-ons/ci/DSmileyBall> %1',1,1);
datablock ProjectileData(DSmileyBallProjectile)
{
   //projectileShapeName = "Add-Ons/Shapes/DSmileyBall.dts";
   directDamage        = 5;
   directDamageType  = $DamageType::DSmileyBallDirect;
   explosion           = DSmileyBallExplosion;
   particleEmitter     = DSmileyBallTrailEmitter;

   brickExplosionRadius = 2;
   brickExplosionImpact = true; //destroy a brick if we hit it directly?
   brickExplosionForce  = 20;
   brickExplosionMaxVolume = 200;

   muzzleVelocity      = 35;
   velInheritFactor    = 0;

   armingDelay         = 50000;
   lifetime            = 90000;
   fadeDelay           = 19500;
   bounceElasticity    = 0.999;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0.70;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";
};


//////////
// item //
//////////
datablock ItemData(DSmileyBallItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "Add-Ons/Shapes/DSmileyBall.dts";
	mass = 0;
	density = 0.2;
	elasticity = 1.0;
	friction = 0;
	emap = true;

	//gui stuff
	uiName = "D-Smiley Ball";
	iconName = "./ItemIcons/DSmileyBall";
	doColorShift = true;
	colorShiftColor = "1.000 1.000 1.000 1.000";

	 // Dynamic properties defined by the scripts
	image = DSmileyBallImage;
	canDrop = true;
};

//function DSmileyBall::onUse(%this,%user)
//{
//	//mount the image in the right hand slot
//	%user.mountimage(%this.image, $RightHandSlot);
//}

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(DSmileyBallImage)
{
   // Basic Item properties
   shapeFile = "Add-Ons/Shapes/DSmileyBall.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   //eyeOffset = "0 0 0";

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = DSmileyBallItem;
   ammo = " ";
   projectile = DSmileyBallProjectile;
   projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;

   //casing = " ";
   doColorShift = false;
   colorShiftColor = "1.000 1.000 1.000 1.000";

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
	stateEmitter[0]			   = DSmileyBallTrailEmitter;
	stateEmitterTime[0]		   = 1000;
	stateSound[0]					= weaponSwitchSound;

	stateName[1]			= "Ready";
	stateTransitionOnTriggerDown[1]	= "Charge";
	stateAllowImageChange[1]	= true;
	
	stateName[2]                    = "Charge";
	stateTransitionOnTimeout[2]	= "Armed";
	stateTimeoutValue[2]            = 0.7;
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
};

function DSmileyBallImage::onCharge(%this, %obj, %slot)
{
	%obj.playthread(2, spearReady);
}

function DSmileyBallImage::onAbortCharge(%this, %obj, %slot)
{
	%obj.playthread(2, root);
}

function DSmileyBallImage::onFire(%this, %obj, %slot)
{
	%obj.playthread(2, spearThrow);
	Parent::OnFire(%this, %obj, %slot);

	for(%i=0;%i<5;%i++)
	{
		%toolDB = %obj.tool[%i];
		if(%toolDB $= %this.item.getID() && %i == %obj.currTool)
		{
			%obj.tool[%i] = 0;
			%obj.weaponCount--;
			messageClient(%obj.client,'MsgItemPickup','',%i,0);
			serverCmdUnUseTool(%obj.client);
			break;
		}
	}
}

function DSmileyBallImage::onDone(%this,%obj,%slot)
{
	%obj.unMountImage(%slot);
}