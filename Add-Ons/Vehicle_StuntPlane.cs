datablock AudioProfile(fastImpactSound)
{
   filename    = "./sound/fastimpact.WAV";
   description = AudioDefault3d;
   preload = true;
};
datablock AudioProfile(slowImpactSound)
{
   filename    = "./sound/slowimpact.wav";
   description = AudioDefault3d;
   preload = true;
};
// datablock AudioProfile(engineidleSound)
// {
   // filename    = "./sound/prop.wav";
   // description = AudioDefault3d;
   // preload = true;
// };

datablock WheeledVehicleData(stuntplaneVehicle)
{
	category = "Vehicles";
	displayName = " ";
	shapeFile = "./shapes/stuntplane.dts"; //"~/data/shapes/skivehicle.dts"; //
	emap = true;
	minMountDist = 3;
   
   numMountPoints = 3;
   mountThread[0] = "sit";
   mountThread[1] = "Root";
   mountThread[2] = "Root";

	maxDamage = 200.00;
	destroyedLevel = 200.00;
	energyPerDamagePoint = 160;
	speedDamageScale = 1.04;
	collDamageThresholdVel = 20.0;
	collDamageMultiplier   = 0.02;

	massCenter = "0 0 0";
   //massBox = "2 5 1";

	maxSteeringAngle = 0.9785;  // Maximum steering angle, should match animation
	integration = 4;           // Force integration time: TickSec/Rate
	tireEmitter = stuntplaneTireEmitter; // All the tires use the same dust emitter

	// 3rd person camera settings
	cameraRoll = false;         // Roll the camera with the vehicle
	cameraMaxDist = 13;         // Far distance from vehicle
	cameraOffset = 7.5;        // Vertical offset from camera mount point
	cameraLag = 0.0;           // Velocity lag of camera
	cameraDecay = 0.75;        // Decay per sec. rate of velocity lag
	cameraTilt = 0.4;
   collisionTol = 0.1;        // Collision distance tolerance
   contactTol = 0.1;

	useEyePoint = false;	

	defaultTire	= stuntplaneTire;
	defaultSpring	= stuntplaneSpring;
	flatTire	= stuntplaneFlatTire;
	flatSpring	= stuntplaneFlatSpring;

   numWheels = 3;

	// Rigid Body
	mass = 200;
	density = 5.0;
	drag = 0.6;
	bodyFriction = 0.6;
	bodyRestitution = 0.6;
	minImpactSpeed = 10;        // Impacts over this invoke the script callback
	softImpactSpeed = 10;       // Play SoftImpact Sound
	hardImpactSpeed = 15;      // Play HardImpact Sound
	groundImpactMinSpeed    = 10.0;

	// Engine
	engineTorque = 12000; //4000;       // Engine power
	engineBrake = 2000;         // Braking when throttle is 0
	brakeTorque = 4000;        // When brakes are applied
	maxWheelSpeed = 20;        // Engine scale by current speed / max speed

	rollForce		= 900;
	yawForce		= 600;
	pitchForce		= 1000;
	rotationalDrag		= 0.2;

	// Energy
	maxEnergy = 100;
	jetForce = 3000;
	minJetEnergy = 30;
	jetEnergyDrain = 2;

   isSled = false;

   forwardThrust		= 3000;
	reverseThrust		= 2000;
	lift			= 100;
	maxForwardVel		= 40;
	maxReverseVel		= 40;
	horizontalSurfaceForce	= 130;   // Horizontal center "wing" (provides "bite" into the wind for climbing/diving and turning)
	verticalSurfaceForce	= 130; 
	rollForce		= 8000;
	yawForce		= 6000;
	pitchForce		= 6000;
	rotationalDrag		= 0.5;
	stallSpeed		= 10;


//   forwardThrust		= 2000;
//	reverseThrust		= 2000;
//	lift			= 100;
//	maxForwardVel		= 70;
//	maxReverseVel		= 70;
//	horizontalSurfaceForce	= 100;   // Horizontal center "wing" (provides "bite" into the wind for climbing/diving and turning)
//	verticalSurfaceForce	= 100; 
//	rollForce		= 6000;
//	yawForce		= 6000;
//	pitchForce		= 4000;
//	rotationalDrag		= 0.5;
//	stallSpeed		= 10;
//
//   forwardThrust		= 2500;
//	reverseThrust		= 500;
//	lift			= 15000;
//	maxForwardVel		= 60;
//	maxReverseVel		= 10;
//	horizontalSurfaceForce	= 500;   // Horizontal center "wing" (provides "bite" into the wind for climbing/diving and turning)
//	verticalSurfaceForce	= 500; 
//	rollForce		= 5000;
//	yawForce		= 5000;
//	pitchForce		= 5000;
//	rotationalDrag		= 0.1;
//	stallSpeed		= 10;

	splash = stuntplaneSplash;
	splashVelocity = 4.0;
	splashAngle = 67.0;
	splashFreqMod = 300.0;
	splashVelEpsilon = 0.60;
	bubbleEmitTime = 1.4;
	splashEmitter[0] = stuntplaneFoamDropletsEmitter;
	splashEmitter[1] = stuntplaneFoamEmitter;
	splashEmitter[2] = stuntplaneBubbleEmitter;
	mediumSplashSoundVelocity = 10.0;   
	hardSplashSoundVelocity = 20.0;   
	exitSplashSoundVelocity = 5.0;
		
	//mediumSplashSound = "";
	//hardSplashSound = "";
	//exitSplashSound = "";
	
	// Sounds
	//   jetSound = ScoutThrustSound;
	//engineSound = engineidleSound;
	//squealSound = skidSound;
	softImpactSound = slowImpactSound;
	hardImpactSound = fastImpactSound;
	//wheelImpactSound = slowImpactSound;

	//   explosion = VehicleExplosion;
	justcollided = 0;

   uiName = "Stunt Plane ";
	rideable = true;
		lookUpLimit = 0.65;
		lookDownLimit = 0.45;

	paintable = true;
   
   damageEmitter[0] = stuntplaneBurnEmitter;
	damageEmitterOffset[0] = "0.0 0.0 0.0 ";
	damageLevelTolerance[0] = 0.99;

   damageEmitter[1] = stuntplaneBurnEmitter;
	damageEmitterOffset[1] = "0.0 0.0 0.0 ";
	damageLevelTolerance[1] = 1.0;

   numDmgEmitterAreas = 1;

   initialExplosionProjectile = stuntplaneExplosionProjectile;
   initialExplosionOffset = 0;         //offset only uses a z value for now

   burnTime = 4000;

   finalExplosionProjectile = stuntplaneFinalExplosionProjectile;
   finalExplosionOffset = 0.5;          //offset only uses a z value for now


   minRunOverSpeed    = 2;   //how fast you need to be going to run someone over (do damage)
   runOverDamageScale = 5;   //when you run over someone, speed * runoverdamagescale = damage amt
   runOverPushScale   = 1.2; //how hard a person you're running over gets pushed

   minContrailSpeed = 30;
};

datablock ParticleData(contrailParticle)
{
   dragCoefficient      = 0;
   windCoefficient     = 0;
   gravityCoefficient   = 0;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   spinRandomMin = -90;
   spinRandomMax = 90;
   lifetimeMS           = 500;
   lifetimeVarianceMS   = 0;
   textureName          = "base/data/particles/cloud";
   colors[0]     = "1 1 1 1";
   colors[1]     = "0.2 0.2 1 0";
   sizes[0]      = 0.1;
   sizes[1]      = 0.05;
};

datablock ParticleEmitterData(contrailEmitter)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   ejectionVelocity = 0;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 180;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "contrailParticle";
};

function stuntplanevehicle::onadd(%this,%obj)
{ 
	
	parent::onadd(%this,%obj);
	   %obj.playThread(0,"propfast");

	contrailCheck(%obj);
	    
}

datablock ShapeBaseImageData(ContrailImage1)
{
   shapeFile = "base/data/shapes/empty.dts";
	emap = false;

	mountPoint = 3;
   rotation = "1 0 0 -90";

	stateName[0]					= "Ready";
	stateTransitionOnTimeout[0]		= "FireA";
	stateTimeoutValue[0]			= 0.01;

	stateName[1]					= "FireA";
	stateTransitionOnTimeout[1]		= "Done";
	stateWaitForTimeout[1]			= True;
	stateTimeoutValue[1]			= 10000;
	stateEmitter[1]					= ContrailEmitter;
	stateEmitterTime[1]				= 10000;

	stateName[2]					= "Done";
	stateScript[2]					= "onDone";
};
function ContrailImage1::onDone(%this,%obj,%slot)
{
	%obj.unMountImage(%slot);
}
datablock ShapeBaseImageData(ContrailImage2)
{
   	shapeFile = "base/data/shapes/empty.dts";
	emap = false;

	mountPoint = 4;
   	rotation = "1 0 0 -90";

	stateName[0]					= "Ready";
	stateTransitionOnTimeout[0]		= "FireA";
	stateTimeoutValue[0]			= 0.01;

	stateName[1]					= "FireA";
	stateTransitionOnTimeout[1]		= "Done";
	stateWaitForTimeout[1]			= True;
	stateTimeoutValue[1]			= 10000;
	stateEmitter[1]					= ContrailEmitter;
	stateEmitterTime[1]				= 10000;

	stateName[2]					= "Done";
	stateScript[2]					= "onDone";
};
function ContrailImage2::onDone(%this,%obj,%slot)
{
	%obj.unMountImage(%slot);
}

function contrailCheck(%obj)
{
//	return;
	if(!isObject(%obj))
		return;

	%speed = vectorLen(%obj.getVelocity());
	if(%speed < %obj.dataBlock.minContrailSpeed)
	{
		if(%obj.getMountedImage(3) !$= "")
		{
			%obj.unMountImage(2);
			%obj.unMountImage(3);
		}
	}
	else
	{
		if(%obj.getMountedImage(3) $= 0)
		{
			%obj.mountImage(contrailImage1,2);
			%obj.mountImage(contrailImage2,3);
		}
	}

	if(%speed < 5)
	{
		if(%obj.prop !$= "slow")
		{
			%obj.playThread(0,propslow);
			%obj.prop = "slow";
		}
	}
	else
	{
		if(%obj.prop !$= "fast")
		{
			%obj.playThread(0,propfast);
			%obj.prop = "fast";
		}
	}

	schedule(2000,0,"contrailCheck",%obj);
}

//function gunImage::onFire(%this,%obj,%slot)
//{
//	if(%obj.getDamagePercent() < 1.0)
//		%obj.playThread(2, shiftAway);
//	Parent::onFire(%this,%obj,%slot);	
//}

//Support_Jeep.cs

// explosions, particle emitters, etc for jeep 
// putting them in a seperate file so other add-ons can use them




//datablock AudioProfile(landingSound)
//{
//   filename    = "base/data/sound/landing.wav";
//   description = AudioDefault3d;
//   preload = true;
//};
//datablock AudioProfile(skidSound)
//{
//   filename    = "base/data/sound/steam.wav";
//   description = AudioDefaultLooping3d;
//   preload = true;
//};
//datablock AudioProfile(idleSound)
//{
//   filename    = "base/data/sound/idle.wav";
//   description = AudioDefaultLooping3d;
//   preload = true;
//};
//datablock AudioProfile(vehiclecrash1Sound)
//{
//   filename    = "./sound/vehiclecrash1.wav";
//   description = AudioDefault3d;
//   preload = true;
//};

datablock AudioProfile(stuntplaneExplosionSound)
{
   filename    = "./sound/jeepExplosion.wav";
   description = AudioDefault3d;
   preload = true;
};
//----------------------------------------------------------------------------
// Splash
//----------------------------------------------------------------------------
datablock ParticleData(stuntplaneSplashMist)
{
   dragCoefficient      = 2.0;
   gravityCoefficient   = -0.05;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 400;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = false;
   spinRandomMin        = -90.0;
   spinRandomMax        = 500.0;
   textureName          = "base/data/particles/cloud";
   colors[0]     = "0.7 0.8 1.0 1.0";
   colors[1]     = "0.7 0.8 1.0 0.5";
   colors[2]     = "0.7 0.8 1.0 0.0";
   sizes[0]      = 2.5;
   sizes[1]      = 2.5;
   sizes[2]      = 5.0;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(stuntplaneSplashMistEmitter)
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 0;
   ejectionVelocity = 3.0;
   velocityVariance = 2.0;
   ejectionOffset   = 1.0;
   thetaMin         = 85;
   thetaMax         = 85;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   lifetimeMS       = 250;
   particles = "stuntplaneSplashMist";

   uiName = "stuntplane Splash Mist";
   emitterNode = FifthEmitterNode;
};


datablock ParticleData(stuntplaneBubbleParticle)
{
   dragCoefficient      = 0.0;
   gravityCoefficient   = -0.50;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 400;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = false;
   textureName          = "base/data/particles/cloud";
   colors[0]     = "0.7 0.8 1.0 0.4";
   colors[1]     = "0.7 0.8 1.0 0.4";
   colors[2]     = "0.7 0.8 1.0 0.0";
   sizes[0]      = 0.1;
   sizes[1]      = 0.3;
   sizes[2]      = 0.3;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(stuntplaneBubbleEmitter)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   ejectionVelocity = 2.0;
   ejectionOffset   = 1.5;
   velocityVariance = 0.5;
   thetaMin         = 0;
   thetaMax         = 80;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   particles = "stuntplaneBubbleParticle";

   uiName = "stuntplane Bubbles";
   emitterNode = FifthEmitterNode;
};

datablock ParticleData(stuntplaneFoamParticle)
{
   dragCoefficient      = 2.0;
   gravityCoefficient   = -0.05;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 400;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = false;
   spinRandomMin        = -90.0;
   spinRandomMax        = 500.0;
   textureName          = "base/data/particles/cloud";
   colors[0]     = "0.7 0.8 1.0 0.20";
   colors[1]     = "0.7 0.8 1.0 0.20";
   colors[2]     = "0.7 0.8 1.0 0.00";
   sizes[0]      = 1.2;
   sizes[1]      = 1.4;
   sizes[2]      = 2.6;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(stuntplaneFoamEmitter)
{
   ejectionPeriodMS = 20;
   periodVarianceMS = 0;
   ejectionVelocity = 3.0;
   velocityVariance = 1.0;
   ejectionOffset   = 0.75;
   thetaMin         = 85;
   thetaMax         = 85;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   particles = "stuntplaneFoamParticle";

   uiName = "stuntplane Foam";
   emitterNode = GenericEmitterNode;
};


datablock ParticleData( stuntplaneFoamDropletsParticle )
{
   dragCoefficient      = 1;
   gravityCoefficient   = 0.2;
   inheritedVelFactor   = 0.2;
   constantAcceleration = -0.0;
   lifetimeMS           = 600;
   lifetimeVarianceMS   = 0;
   textureName          = "base/data/particles/cloud";
   colors[0]     = "0.7 0.8 1.0 1.0";
   colors[1]     = "0.7 0.8 1.0 0.5";
   colors[2]     = "0.7 0.8 1.0 0.0";
   sizes[0]      = 0.8;
   sizes[1]      = 0.3;
   sizes[2]      = 0.0;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData( stuntplaneFoamDropletsEmitter )
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   ejectionVelocity = 2;
   velocityVariance = 1.0;
   ejectionOffset   = 1.0;
   thetaMin         = 60;
   thetaMax         = 80;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   orientParticles  = true;
   particles = "stuntplaneFoamDropletsParticle";

   uiName = "stuntplane Foam Droplets";
   emitterNode = GenericEmitterNode;
};


datablock ParticleData( stuntplaneSplashParticle )
{
   dragCoefficient      = 1;
   gravityCoefficient   = 0.2;
   inheritedVelFactor   = 0.2;
   constantAcceleration = -0.0;
   lifetimeMS           = 600;
   lifetimeVarianceMS   = 0;
   textureName          = "base/data/particles/cloud";
   colors[0]     = "0.7 0.8 1.0 1.0";
   colors[1]     = "0.7 0.8 1.0 0.5";
   colors[2]     = "0.7 0.8 1.0 0.0";
   sizes[0]      = 0.5;
   sizes[1]      = 0.5;
   sizes[2]      = 0.5;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData( stuntplaneSplashEmitter )
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   ejectionVelocity = 3;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 60;
   thetaMax         = 80;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   orientParticles  = true;
   lifetimeMS       = 100;
   particles = "stuntplaneSplashParticle";

   uiName = "stuntplane Splash";
   emitterNode = TenthEmitterNode;
};


///////////////////////////////////////////////////////////////////


datablock ParticleData(stuntplaneTireParticle)
{
   textureName          = "base/data/particles/chunk";

   dragCoefficient      = 0.0;
   gravityCoefficient   = 2.0;
   windCoefficient      = 0.0;

   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;

   lifetimeMS           = 800;
   lifetimeVarianceMS   = 300;

   colors[0]     = "0 0 0 1"; //"0.46 0.36 0.26 1.0";
   colors[1]     = "0 0 0 0"; //"0.46 0.46 0.36 0.0";
   sizes[0]      = 0.25;
   sizes[1]      = 0.0;

   useInvAlpha = true;
};

datablock ParticleEmitterData(stuntplaneTireEmitter)
{
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;

   ejectionVelocity = 5;
   velocityVariance = 3.0;

   ejectionOffset   = 0.10;
   thetaMin         = 10;
   thetaMax         = 30;
   phiReferenceVel  = 0;
   phiVariance      = 360;

   overrideAdvances = false;
   particles = "stuntplaneTireParticle";
};


datablock ParticleData(stuntplaneBurnParticle)
{
	textureName          = "base/data/particles/cloud";
	dragCoefficient      = 0.0;
	gravityCoefficient   = -1.0;
	inheritedVelFactor   = 0.0;
	windCoefficient      = 0;
	constantAcceleration = 3.0;
	lifetimeMS           = 1200;
	lifetimeVarianceMS   = 100;
	spinSpeed     = 0;
	spinRandomMin = -90.0;
	spinRandomMax =  90.0;
	useInvAlpha   = false;

	colors[0]	= "1   1   0.3 0.0";
	colors[1]	= "1   1   0.3 1.0";
	colors[2]	= "0.6 0.0 0.0 0.0";

	sizes[0]	= 0.0;
	sizes[1]	= 2.0;
	sizes[2]	= 1.0;

	times[0]	= 0.0;
	times[1]	= 0.2;
	times[2]	= 1.0;
};

datablock ParticleEmitterData(stuntplaneBurnEmitter)
{
   ejectionPeriodMS = 14;
   periodVarianceMS = 4;
   ejectionVelocity = 0;
   ejectionOffset   = 1.00;
   velocityVariance = 0.0;
   thetaMin         = 30;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;

   //lifetimeMS = 5000;
   particles = stuntplaneBurnParticle;   

   uiName = "stuntplane Fire";
};


datablock ParticleData(stuntplaneTireDebrisTrailParticle)
{
	dragCoefficient		= 3.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= -0.5;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 500;
	lifetimeVarianceMS	= 150;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "base/data/particles/cloud";
	//animTexName		= "~/data/particles/cloud";

	// Interpolation variables
	colors[0]	= "0.0 0.0 0.0 0.0";
	colors[1]	= "0.0 0.0 0.0 0.250";
   colors[2]	= "0.0 0.0 0.0 0.0";

	sizes[0]	= 1.50;
	sizes[1]	= 2.50;
   sizes[2]	= 3.50;

	times[0]	= 0.0;
	times[1]	= 0.1;
   times[2]	= 1.0;
};

datablock ParticleEmitterData(stuntplaneTireDebrisTrailEmitter)
{
   ejectionPeriodMS = 90;
   periodVarianceMS = 0;
   ejectionVelocity = 0.0;
   velocityVariance = 0.0;
   ejectionOffset   = 1.0;
   thetaMin         = 0;
   thetaMax         = 0;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "stuntplaneTireDebrisTrailParticle";
};

datablock DebrisData(stuntplaneTireDebris)
{
   emitters = "stuntplaneTireDebrisTrailEmitter";

	shapeFile = "./shapes/stuntplaneTire.dts";
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

datablock ParticleData(stuntplaneExplosionParticle)
{
	dragCoefficient		= 3.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= -0.5;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 1900;
	lifetimeVarianceMS	= 300;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "base/data/particles/cloud";
	//animTexName		= "~/data/particles/cloud";

	// Interpolation variables
	colors[0]	= "0.9 0.3 0.2 0.9";
	colors[1]	= "0.0 0.0 0.0 0.0";
	sizes[0]	= 4.0;
	sizes[1]	= 10.0;
	times[0]	= 0.0;
	times[1]	= 1.0;
};

datablock ParticleEmitterData(stuntplaneExplosionEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   lifeTimeMS	   = 21;
   ejectionVelocity = 8;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "stuntplaneExplosionParticle";

   uiName = "stuntplane Explosion";
   emitterNode = TenthEmitterNode;
};

datablock ParticleData(stuntplaneExplosionParticle2)
{
	dragCoefficient		= 0.1;
	windCoefficient		= 0.0;
	gravityCoefficient	= 2.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 1000;
	lifetimeVarianceMS	= 500;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "base/data/particles/chunk";
	//animTexName		= "~/data/particles/cloud";

	// Interpolation variables
	colors[0]	= "1.0 1.0 0.0 1.0";
	colors[1]	= "1.0 0.0 0.0 0.0";
	sizes[0]	= 0.5;
	sizes[1]	= 0.5;
	times[0]	= 0.0;
	times[1]	= 1.0;
};

datablock ParticleEmitterData(stuntplaneExplosionEmitter2)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   lifetimeMS       = 7;
   ejectionVelocity = 15;
   velocityVariance = 5.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "stuntplaneExplosionParticle2";

   uiName = "stuntplane Explosion 2";
   emitterNode = TenthEmitterNode;
};


datablock ParticleData(stuntplaneExplosionParticle3)
{
	dragCoefficient		= 0.1;
	windCoefficient		= 0.0;
	gravityCoefficient	= 2.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 1000;
	lifetimeVarianceMS	= 500;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "base/data/particles/star1";
	//animTexName		= "~/data/particles/cloud";

	// Interpolation variables
	colors[0]	= "1.0 1.0 0.0 1.0";
	colors[1]	= "1.0 0.0 0.0 0.0";
	sizes[0]	= 20.0;
	sizes[1]	= 1.0;
	times[0]	= 0.0;
	times[1]	= 1.0;
};

datablock ParticleEmitterData(stuntplaneExplosionEmitter3)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   lifetimeMS       = 50;
   ejectionVelocity = 15;
   velocityVariance = 5.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "stuntplaneExplosionParticle3";

   uiName = "stuntplane Explosion 3";
   emitterNode = FourtiethEmitterNode;
};

datablock ExplosionData(stuntplaneExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 150;

   soundProfile = stuntplaneExplosionSound;

   emitter[0] = stuntplaneExplosionEmitter;
   emitter[1] = stuntplaneExplosionEmitter2;
   //particleDensity = 30;
   //particleRadius = 1.0;

   debris = stuntplaneTireDebris;
   debrisNum = 3;
   debrisNumVariance = 0;
   debrisPhiMin = 0;
   debrisPhiMax = 360;
   debrisThetaMin = 40;
   debrisThetaMax = 85;
   debrisVelocity = 14;
   debrisVelocityVariance = 3;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = true;
   camShakeFreq = "7.0 8.0 7.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.5;
   camShakeRadius = 15.0;

   // Dynamic light
   lightStartRadius = 14;
   lightEndRadius = 3;
   lightStartColor = "0.9 0.3 0.1";
   lightEndColor = "0 0 0";

   //impulse
   impulseRadius = 10;
   impulseForce = 500;

   //radius damage
   radiusDamage        = 30;
   damageRadius        = 3.5;
};

datablock ParticleData(stuntplaneDebrisTrailParticle)
{
	dragCoefficient		= 3.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= -0.5;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 600;
	lifetimeVarianceMS	= 150;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "base/data/particles/cloud";
	//animTexName		= "~/data/particles/cloud";

	// Interpolation variables
	colors[0]	= "0.0 0.0 0.0 0.5";
	colors[1]	= "0.0 0.0 0.0 1.0";
   colors[2]	= "0.0 0.0 0.0 0.0";

	sizes[0]	= 2.0;
	sizes[1]	= 5.0;
   sizes[2]	= 5.0;

	times[0]	= 0.0;
	times[1]	= 0.1;
   times[2]	= 1.0;
};

datablock ParticleEmitterData(stuntplaneDebrisTrailEmitter)
{
   ejectionPeriodMS = 15;
   periodVarianceMS = 0;
   ejectionVelocity = 8;
   velocityVariance = 1.0;
   ejectionOffset   = 1.0;
   thetaMin         = 40;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "stuntplaneDebrisTrailParticle";

   uiName = "stuntplane Debris Trail";
   emitterNode = FifthEmitterNode;
};


datablock ParticleData(stuntplaneFinalExplosionParticle)
{
	dragCoefficient		= 1.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= -0.5;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 1900;
	lifetimeVarianceMS	= 1000;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "base/data/particles/cloud";
	//animTexName		= "~/data/particles/cloud";

	// Interpolation variables
	colors[0]	= "0.0 0.0 0.0 0.5";
	colors[1]	= "0.0 0.0 0.0 1.0";
   colors[2]	= "0.0 0.0 0.0 0.0";

	sizes[0]	= 5.0;
	sizes[1]	= 10.0;
   sizes[2]	= 5.0;

	times[0]	= 0.0;
	times[1]	= 0.1;
   times[2]	= 1.0;
};

datablock ParticleEmitterData(stuntplaneFinalExplosionEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   lifeTimeMS	   = 21;
   ejectionVelocity = 8;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 10;
   thetaMax         = 40;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "stuntplaneFinalExplosionParticle";

   uiName = "stuntplane Final Explosion";
   emitterNode = TwentiethEmitterNode;
};

datablock ParticleData(stuntplaneFinalExplosionParticle2)
{
	dragCoefficient		= 3.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 1000;
	lifetimeVarianceMS	= 500;
	spinSpeed		= 10.0;
	spinRandomMin		= -500.0;
	spinRandomMax		= 500.0;
	useInvAlpha		= false;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "base/data/particles/cloud";
	//animTexName		= "~/data/particles/cloud";

	// Interpolation variables
	colors[0]	= "1.0 0.5 0.0 1.0";
	colors[1]	= "1.0 0.0 0.0 0.0";
	sizes[0]	= 1.5;
	sizes[1]	= 2.5;
	times[0]	= 0.0;
	times[1]	= 1.0;
};

datablock ParticleEmitterData(stuntplaneFinalExplosionEmitter2)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   lifetimeMS       = 15;
   ejectionVelocity = 30;
   velocityVariance = 5.0;
   ejectionOffset   = 0.0;
   thetaMin         = 85;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "stuntplaneFinalExplosionParticle2";

   uiName = "stuntplane Final Explosion 2";
   emitterNode = TenthEmitterNode;
};



datablock ParticleData(stuntplaneFinalExplosionParticle3)
{
	dragCoefficient		= 13.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 100;
	lifetimeVarianceMS	= 50;
	spinSpeed		= 10.0;
	spinRandomMin		= -500.0;
	spinRandomMax		= 500.0;
	useInvAlpha		= false;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "base/data/particles/star1";
	//animTexName		= "~/data/particles/cloud";

	// Interpolation variables
	colors[0]	= "1.0 0.5 0.0 1.0";
	colors[1]	= "1.0 0.0 0.0 0.0";
	sizes[0]	= 15;
	sizes[1]	= 0.5;

	times[0]	= 0.0;
	times[1]	= 1.0;
};

datablock ParticleEmitterData(stuntplaneFinalExplosionEmitter3)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   lifetimeMS       = 15;
   ejectionVelocity = 30;
   velocityVariance = 5.0;
   ejectionOffset   = 0.0;
   thetaMin         = 85;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "stuntplaneFinalExplosionParticle3";   

   uiName = "stuntplane Final Explosion 3";
   emitterNode = TenthEmitterNode;
};


datablock WheeledVehicleTire(stuntplaneTire)
{
   // Tires act as springs and generate lateral and longitudinal
   // forces to move the vehicle. These distortion/spring forces
   // are what convert wheel angular velocity into forces that
   // act on the rigid body.
   shapeFile = "./shapes/stuntplanetire.dts";
	
	mass = 10;
    radius = 1;
    staticFriction = 5;
   kineticFriction = 5;
   restitution = 0.5;	

   // Spring that generates lateral tire forces
   lateralForce = 18000;
   lateralDamping = 4000;
   lateralRelaxation = 0.01;

   // Spring that generates longitudinal tire forces
   longitudinalForce = 14000;
   longitudinalDamping = 2000;
   longitudinalRelaxation = 0.01;
};

datablock WheeledVehicleSpring(stuntplaneSpring)
{
   // Wheel suspension properties
   length = 0.2;			 // Suspension travel
   force = 3000; //3000;		 // Spring force
   damping = 800; //600;		 // Spring damping
   antiSwayForce = 6; //3;		 // Lateral anti-sway force
};


//////////////////////////////////////
// Initial Explosion (lose wheels)  //
//////////////////////////////////////
AddDamageType("stuntplaneExplosion",   '<bitmap:add-ons/ci/carExplosion> %1',    '%2 <bitmap:add-ons/ci/carExplosion> %1');
datablock ProjectileData(stuntplaneExplosionProjectile)
{
   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 0;
   explosion           = stuntplaneExplosion;

   directDamageType  = $DamageType::stuntplaneExplosion;
   radiusDamageType  = $DamageType::stuntplaneExplosion;

   explodeOnDeath		= 1;

   armingDelay         = 0;
   lifetime            = 10;
};

//////////////////////////////////////
//////////////////////////////////////

//////////////////////////////////////
// Final Explosion                  //
//////////////////////////////////////



datablock DebrisData(stuntplaneDebris)
{
   emitters = "stuntplaneDebrisTrailEmitter";

	shapeFile = "./shapes/stuntplaneWreckage.dts";
	lifetime = 3.0;
	minSpinSpeed = -500.0;
	maxSpinSpeed = 500.0;
	elasticity = 0.5;
	friction = 0.2;
	numBounces = 1;
	staticOnMaxBounce = true;
	snapOnMaxBounce = false;
	fade = true;

	gravModifier = 2;
};







datablock ExplosionData(stuntplaneFinalExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 150;

   soundProfile = stuntplaneExplosionSound;
   
   emitter[0] = stuntplaneFinalExplosionEmitter3;
   emitter[1] = stuntplaneFinalExplosionEmitter2;

   particleEmitter = stuntplaneFinalExplosionEmitter;
   particleDensity = 20;
   particleRadius = 1.0;

   debris = stuntplaneDebris;
   debrisNum = 1;
   debrisNumVariance = 0;
   debrisPhiMin = 0;
   debrisPhiMax = 360;
   debrisThetaMin = 0;
   debrisThetaMax = 20;
   debrisVelocity = 18;
   debrisVelocityVariance = 3;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = true;
   camShakeFreq = "7.0 8.0 7.0";
   camShakeAmp = "10.0 10.0 10.0";
   camShakeDuration = 0.75;
   camShakeRadius = 15.0;

   // Dynamic light
   lightStartRadius = 0;
   lightEndRadius = 20;
   lightStartColor = "0.45 0.3 0.1";
   lightEndColor = "0 0 0";

   //impulse
   impulseRadius = 15;
   impulseForce = 1000;
   impulseVertical = 2000;

   //radius damage
   radiusDamage        = 30;
   damageRadius        = 8.0;

   //burn the players?
   playerBurnTime = 5000;

};

datablock ProjectileData(stuntplaneFinalExplosionProjectile)
{
   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 0;
   explosion           = stuntplaneFinalExplosion;

   directDamageType  = $DamageType::stuntplaneExplosion;
   radiusDamageType  = $DamageType::stuntplaneExplosion;

   explodeOnDeath		= 1;

   armingDelay         = 0;
   lifetime            = 10;
};

//////////////////////////////////////
//////////////////////////////////////

datablock ParticleData(stuntplaneSmokeParticle)
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
datablock ParticleEmitterData(stuntplaneSmokeEmitter)
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
   particles = "stuntplaneSmokeParticle";
};

