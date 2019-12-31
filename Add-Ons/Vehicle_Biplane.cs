if(!isObject(jeepExplosionSound))
{
   exec("./Support_Jeep.cs");
}

datablock DebrisData(BiplaneTireDebris)
{
   emitters = "JeepTireDebrisTrailEmitter";

	shapeFile = "./shapes/biplanefrontwheel.dts";
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

datablock WheeledVehicleTire(BiplaneTire)
{
   // Tires act as springs and generate lateral and longitudinal
   // forces to move the vehicle. These distortion/spring forces
   // are what convert wheel angular velocity into forces that
   // act on the rigid body.
   shapeFile = "./shapes/biplanefrontwheel.dts";
	
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

datablock WheeledVehicleSpring(BiplaneSpring)
{
   // Wheel suspension properties
   length = 0.1;			 // Suspension travel
   force = 3000; //3000;		 // Spring force
   damping = 400; //600;		 // Spring damping
   antiSwayForce = 3; //3;		 // Lateral anti-sway force
};
///////////////////////////////////////////////////////////////

datablock WheeledVehicleTire(BiplaneBackTire)
{
   // Tires act as springs and generate lateral and longitudinal
   // forces to move the vehicle. These distortion/spring forces
   // are what convert wheel angular velocity into forces that
   // act on the rigid body.
   shapeFile = "./shapes/biplanebackwheel.dts";
	
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

datablock WheeledVehicleSpring(BiplaneBackSpring)
{
   // Wheel suspension properties
   length = 0.1;			 // Suspension travel
   force = 3000; //3000;		 // Spring force
   damping = 400; //600;		 // Spring damping
   antiSwayForce = 3; //3;		 // Lateral anti-sway force
};

///////////////////////////////////////////////////////////////

datablock ExplosionData(BiplaneExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 150;

   soundProfile = jeepExplosionSound;

   emitter[0] = jeepExplosionEmitter;
   emitter[1] = jeepExplosionEmitter2;
   //particleDensity = 30;
   //particleRadius = 1.0;

   debris = BiplaneTireDebris;
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

//////////////////////////////////////
// Initial Explosion (lose wheels)  //
//////////////////////////////////////
AddDamageType("BiplaneExplosion",   '<bitmap:add-ons/ci/carExplosion> %1',    '%2 <bitmap:add-ons/ci/carExplosion> %1');
datablock ProjectileData(BiplaneExplosionProjectile)
{
   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 0;
   explosion           = BiplaneExplosion;

   directDamageType  = $DamageType::BiplaneExplosion;
   radiusDamageType  = $DamageType::BiplaneExplosion;

   explodeOnDeath		= 1;

   armingDelay         = 0;
   lifetime            = 10;
};

//////////////////////////////////////
//////////////////////////////////////

//////////////////////////////////////
// Final Explosion                  //
//////////////////////////////////////



datablock DebrisData(BiplaneDebris)
{
   emitters = "JeepDebrisTrailEmitter";

	shapeFile = "./shapes/biplaneWreckage.dts";
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







datablock ExplosionData(BiplaneFinalExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 150;

   soundProfile = jeepExplosionSound;
   
   emitter[0] = jeepFinalExplosionEmitter3;
   emitter[1] = jeepFinalExplosionEmitter2;

   particleEmitter = jeepFinalExplosionEmitter;
   particleDensity = 20;
   particleRadius = 1.0;

   debris = BiplaneDebris;
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

datablock ProjectileData(BiplaneFinalExplosionProjectile)
{
   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 0;
   explosion           = BiplaneFinalExplosion;

   directDamageType  = $DamageType::BiplaneExplosion;
   radiusDamageType  = $DamageType::BiplaneExplosion;

   explodeOnDeath		= 1;

   armingDelay         = 0;
   lifetime            = 10;
};

//////////////////////////////////////
//////////////////////////////////////

datablock WheeledVehicleData(BiplaneVehicle)
{
	category = "Vehicles";
	displayName = " ";
	shapeFile = "./shapes/biplane.dts";
	emap = true;
	minMountDist = 3;
   
   numMountPoints = 4;
   mountThread[0] = "sit";
   mountThread[1] = "sit";
   mountThread[2] = "sit";
   mountThread[3] = "sit";


	maxDamage = 100.00;
	destroyedLevel = 100.00;
	energyPerDamagePoint = 160;
	speedDamageScale = 1.04;
	collDamageThresholdVel = 20.0;
	collDamageMultiplier   = 0.02;

	massCenter = "0 0 0";
   //massBox = "2 5 1";

	maxSteeringAngle = 0.9785;  // Maximum steering angle, should match animation
	integration = 4;           // Force integration time: TickSec/Rate
	tireEmitter = TireEmitter; // All the tires use the same dust emitter

	// 3rd person camera settings
	cameraRoll = false;         // Roll the camera with the vehicle
	cameraMaxDist = 10;         // Far distance from vehicle
	cameraOffset = 7.5;        // Vertical offset from camera mount point
	cameraLag = 0.0;           // Velocity lag of camera
	cameraDecay = 0.75;        // Decay per sec. rate of velocity lag
	cameraTilt = 0.4;
   collisionTol = 0.1;        // Collision distance tolerance
   contactTol = 0.1;

	useEyePoint = false;	

	// defaultTire	= biplaneTire;
	// defaultSpring	= biplaneSpring;
	// flatTire	= biplaneFlatTire;
	// flatSpring	= biplaneFlatSpring;

   //numWheels = 3;

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
	engineTorque = 4000; //4000;       // Engine power
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
	rollForce		= 4000;
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

	splash = jeepSplash;
	splashVelocity = 4.0;
	splashAngle = 67.0;
	splashFreqMod = 300.0;
	splashVelEpsilon = 0.60;
	bubbleEmitTime = 1.4;
	splashEmitter[0] = jeepFoamDropletsEmitter;
	splashEmitter[1] = jeepFoamEmitter;
	splashEmitter[2] = jeepBubbleEmitter;
	mediumSplashSoundVelocity = 10.0;   
	hardSplashSoundVelocity = 20.0;   
	exitSplashSoundVelocity = 5.0;
		
	//mediumSplashSound = "";
	//hardSplashSound = "";
	//exitSplashSound = "";
	
	// Sounds
	//   jetSound = ScoutThrustSound;
	//engineSound = idleSound;
	//squealSound = skidSound;
	softImpactSound = slowImpactSound;
	hardImpactSound = fastImpactSound;
	//wheelImpactSound = slowImpactSound;

	//   explosion = VehicleExplosion;
	justcollided = 0;

   uiName = "Biplane ";
	rideable = true;
		lookUpLimit = 0.50;
		lookDownLimit = 0.50;

	paintable = true;
   
   damageEmitter[0] = JeepBurnEmitter;
	damageEmitterOffset[0] = "0.0 0.0 0.0 ";
	damageLevelTolerance[0] = 0.99;

   damageEmitter[1] = JeepBurnEmitter;
	damageEmitterOffset[1] = "0.0 0.0 0.0 ";
	damageLevelTolerance[1] = 1.0;

   numDmgEmitterAreas = 1;

   initialExplosionProjectile = biplaneExplosionProjectile;
   initialExplosionOffset = 0;         //offset only uses a z value for now

   burnTime = 4000;

   finalExplosionProjectile = biplaneFinalExplosionProjectile;
   finalExplosionOffset = 0.5;          //offset only uses a z value for now


   minRunOverSpeed    = 2;   //how fast you need to be going to run someone over (do damage)
   runOverDamageScale = 5;   //when you run over someone, speed * runoverdamagescale = damage amt
   runOverPushScale   = 1.2; //how hard a person you're running over gets pushed
   
   // minContrailSpeed = 30;
};

// datablock ParticleData(contrailParticle)
// {
   // dragCoefficient      = 0;
   // windCoefficient     = 0;
   // gravityCoefficient   = 0;
   // inheritedVelFactor   = 0.0;
   // constantAcceleration = 0.0;
   // spinRandomMin = -90;
   // spinRandomMax = 90;
   // lifetimeMS           = 500;
   // lifetimeVarianceMS   = 0;
   // textureName          = "base/data/particles/cloud";
   // colors[0]     = "1 1 1 1";
   // colors[1]     = "0.2 0.2 1 0";
   // sizes[0]      = 0.1;
   // sizes[1]      = 0.05;
// };

// datablock ParticleEmitterData(contrailEmitter)
// {
   // ejectionPeriodMS = 1;
   // periodVarianceMS = 0;
   // ejectionVelocity = 0;
   // velocityVariance = 0.0;
   // ejectionOffset   = 0.0;
   // thetaMin         = 0;
   // thetaMax         = 180;
   // phiReferenceVel  = 0;
   // phiVariance      = 360;
   // overrideAdvance = false;
   // particles = "contrailParticle";
// };

// datablock ShapeBaseImageData(ContrailImage1)
// {
   // shapeFile = "base/data/shapes/empty.dts";
	// emap = false;

	// mountPoint = 3;
   // rotation = "1 0 0 -90";

	// stateName[0]					= "Ready";
	// stateTransitionOnTimeout[0]		= "FireA";
	// stateTimeoutValue[0]			= 0.01;

	// stateName[1]					= "FireA";
	// stateTransitionOnTimeout[1]		= "Done";
	// stateWaitForTimeout[1]			= True;
	// stateTimeoutValue[1]			= 10000;
	// stateEmitter[1]					= ContrailEmitter;
	// stateEmitterTime[1]				= 10000;

	// stateName[2]					= "Done";
	// stateScript[2]					= "onDone";
// };
// function ContrailImage1::onDone(%this,%obj,%slot)
// {
	// %obj.unMountImage(%slot);
// }
// datablock ShapeBaseImageData(ContrailImage2)
// {
   	// shapeFile = "base/data/shapes/empty.dts";
	// emap = false;

	// mountPoint = 4;
   	// rotation = "1 0 0 -90";

	// stateName[0]					= "Ready";
	// stateTransitionOnTimeout[0]		= "FireA";
	// stateTimeoutValue[0]			= 0.01;

	// stateName[1]					= "FireA";
	// stateTransitionOnTimeout[1]		= "Done";
	// stateWaitForTimeout[1]			= True;
	// stateTimeoutValue[1]			= 10000;
	// stateEmitter[1]					= ContrailEmitter;
	// stateEmitterTime[1]				= 10000;

	// stateName[2]					= "Done";
	// stateScript[2]					= "onDone";
// };
// function ContrailImage2::onDone(%this,%obj,%slot)
// {
	// %obj.unMountImage(%slot);
// }

function biplanecontrailCheck(%obj)
{
	//return;
	if(!isObject(%obj))
		return;

	%speed = vectorLen(%obj.getVelocity());
	// if(%speed < %obj.dataBlock.minContrailSpeed)
	// {
		// if(%obj.getMountedImage(3) !$= "")
		// {
			// %obj.unMountImage(2);
			// %obj.unMountImage(3);
		// }
	// }
	// else
	// {
		// if(%obj.getMountedImage(3) $= 0)
		// {
			// %obj.mountImage(contrailImage1,2);
			// %obj.mountImage(contrailImage2,3);
		// }
	// }

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

	schedule(2000,0,"biplanecontrailCheck",%obj);
   
}

function Biplanevehicle::onadd(%this,%obj)
{ 
	parent::onadd(%this,%obj);
		
	%obj.setWheelTire(0, biplanetire);
	%obj.setWheelTire(1, biplanetire);
	%obj.setWheelTire(2, biplanebacktire);

	%obj.setWheelSpring(0, biplaneSpring);
	%obj.setWheelSpring(1, biplaneSpring);
	%obj.setWheelSpring(2, biplanebackSpring);
	
	%obj.setWheelSteering(0,0);
	%obj.setWheelSteering(1,0);
	%obj.setWheelSteering(2,-1);

	%obj.setWheelPowered(0,true);
    %obj.setWheelPowered(1,true);
	%obj.setWheelPowered(2,true);
	
	%obj.playthread(0,propslow);
	
	biplanecontrailCheck(%obj);
	    
}

datablock WheeledVehicleTire(BiplaneFauxTire)
{
	shapeFile = "base/data/shapes/empty.dts";
	mass = 0;
	radius = 0;
	staticFriction = 5;
	kineticFriction = 5;
	restitution = 0;	
	lateralForce = 18000;
	lateralDamping = 4000;
	lateralRelaxation = 0.01;
	longitudinalForce = 14000;
	longitudinalDamping = 2000;
	longitudinalRelaxation = 0.01;
};

datablock WheeledVehicleSpring(BiplaneFauxSpring)
{
	length = 0.1;
	force = 1000;
	damping = 100;
	antiSwayForce = 3;
};

package hideBiplaneTires
{
	function BiplaneVehicle::Damage(%this,%obj,%col,%pos,%damage,%type)
	{
		Parent::Damage(%this,%obj,%col,%pos,%damage,%type);

		if(%obj.getDamageState() $= "Disabled")
		{
			%obj.setWheelTire(0,biplaneFauxTire);
			%obj.setWheelSpring(0,biplaneFauxSpring);
			%obj.setWheelTire(1,biplaneFauxTire);
			%obj.setWheelSpring(1,biplaneFauxSpring);
			%obj.setWheelTire(2,biplaneFauxTire);
			%obj.setWheelSpring(2,biplaneFauxSpring);
		}
	}
};

activatePackage(hideBiplaneTires);