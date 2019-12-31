//-----------------------------------------------------------------------------

// Information extacted from the shape.
//
// Wheel Sequences
//    spring#        Wheel spring motion: time 0 = wheel fully extended,
//                   the hub must be displaced, but not directly animated
//                   as it will be rotated in code.
// Other Sequences
//    steering       Wheel steering: time 0 = full right, 0.5 = center
//    breakLight     Break light, time 0 = off, 1 = breaking
//
// Wheel Nodes
//    hub#           Wheel hub, the hub must be in it's upper position
//                   from which the springs are mounted.
//
// The steering and animation sequences are optional.
// The center of the shape acts as the center of mass for the car.

datablock WheeledVehicleTire(boxtruckTire)
{
   // Tires act as springs and generate lateral and longitudinal
   // forces to move the vehicle. These distortion/spring forces
   // are what convert wheel angular velocity into forces that
   // act on the rigid body.
   shapeFile = "./shapes/boxtruckwheel.dts";
	
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

datablock WheeledVehicleSpring(boxtruckSpring)
{
   // Wheel suspension properties
   length = 0.2;			 // Suspension travel
   force = 3000; //3000;		 // Spring force
   damping = 600; //600;		 // Spring damping
   antiSwayForce = 6; //3;		 // Lateral anti-sway force
};

//////////////////////////////////////
// Initial Explosion (lose wheels)  //
//////////////////////////////////////
AddDamageType("boxtruckExplosion",   '<bitmap:add-ons/ci/carExplosion> %1',    '%2 <bitmap:add-ons/ci/carExplosion> %1');
datablock ProjectileData(boxtruckExplosionProjectile)
{
   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 0;
   explosion           = boxtruckExplosion;

   directDamageType  = $DamageType::boxtruckExplosion;
   radiusDamageType  = $DamageType::boxtruckExplosion;

   explodeOnDeath		= 1;

   armingDelay         = 0;
   lifetime            = 10;
};

//////////////////////////////////////
//////////////////////////////////////

//////////////////////////////////////
// Final Explosion                  //
//////////////////////////////////////

datablock DebrisData(boxtruckDebris)
{
   emitters = "jeepDebrisTrailEmitter";

	shapeFile = "./shapes/boxtruckWreckage.dts";
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

datablock ExplosionData(boxtruckFinalExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 150;

   soundProfile = jeepExplosionSound;
   
   emitter[0] = jeepFinalExplosionEmitter3;
   emitter[1] = jeepFinalExplosionEmitter2;

   particleEmitter = jeepFinalExplosionEmitter;
   particleDensity = 20;
   particleRadius = 1.0;

   debris = boxtruckDebris;
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

datablock ProjectileData(boxtruckFinalExplosionProjectile)
{
   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 0;
   explosion           = boxtruckFinalExplosion;

   directDamageType  = $DamageType::boxtruckExplosion;
   radiusDamageType  = $DamageType::boxtruckExplosion;

   explodeOnDeath		= 1;

   armingDelay         = 0;
   lifetime            = 10;
};

//////////////////////////////////////
//////////////////////////////////////

datablock ExplosionData(boxtruckExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 150;

   soundProfile = jeepExplosionSound;

   emitter[0] = jeepExplosionEmitter;
   emitter[1] = jeepExplosionEmitter2;
   //particleDensity = 30;
   //particleRadius = 1.0;

   debris = boxtruckTireDebris;
   debrisNum = 4;
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

datablock DebrisData(boxtruckTireDebris)
{
   emitters = "jeepTireDebrisTrailEmitter";

	shapeFile = "./shapes/boxtruckwheel.dts";
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

//-----------------------------------------------------------------------------
if(!isObject(jeepExplosionSound))
{
   exec("./Support_jeep.cs");
}
//----------------------------------------------------------------------------

datablock WheeledVehicleData(boxtruckVehicle)
{
	category = "Vehicles";
	displayName = " ";
	shapeFile = "./shapes/boxtruck.dts"; //"~/data/shapes/skivehicle.dts"; //
	emap = true;
	minMountDist = 3;
   
	numMountPoints = 12;
	mountThread[0] = "sit";
	mountThread[1] = "sit";
	mountThread[2] = "sit";
	mountThread[3] = "root";
	mountThread[4] = "root";
	mountThread[5] = "sit";
	mountThread[6] = "sit";
	mountThread[7] = "sit";
	mountThread[8] = "root";
	mountThread[9] = "sit";
	mountThread[10] = "root";
	mountThread[11] = "root";

	maxDamage = 200.00;
	destroyedLevel = 200.00;
	energyPerDamagePoint = 160;
	speedDamageScale = 1.04;
	collDamageThresholdVel = 20.0;
	collDamageMultiplier   = 0.02;

	massCenter = "0 0 -2.1";
	//massBox = "4 6 1";

	maxSteeringAngle = 0.9785;  // Maximum steering angle, should match animation
	integration = 4;           // Force integration time: TickSec/Rate
	tireEmitter = TireEmitter; // All the tires use the same dust emitter

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

	 defaultTire	= boxtruckTire;
	 defaultSpring	= boxtruckSpring;
	 flatTire	= boxtruckFlatTire;
	 flatSpring	= boxtruckFlatSpring;

      numWheels = 6;

	// Rigid Body
	mass = 300;
	density = 5.0;
	drag = 1.6;
	bodyFriction = 0.6;
	bodyRestitution = 0.6;
	minImpactSpeed = 10;        // Impacts over this invoke the script callback
	softImpactSpeed = 10;       // Play SoftImpact Sound
	hardImpactSpeed = 15;      // Play HardImpact Sound
	groundImpactMinSpeed    = 10.0;

	// Engine
	engineTorque = 6000; //4000;       // Engine power
	engineBrake = 3000;         // Braking when throttle is 0
	brakeTorque = 5000;        // When brakes are applied
	maxWheelSpeed = 35;        // Engine scale by current speed / max speed

	rollForce		= 900;
	yawForce		= 600;
	pitchForce		= 1000;
	rotationalDrag		= 0.2;

	// Energy
	maxEnergy = 100;
	jetForce = 3000;
	minJetEnergy = 30;
	jetEnergyDrain = 2;

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

   uiName = "Box Truck ";
	rideable = true;
		lookUpLimit = 0.65;
		lookDownLimit = 0.45;

	paintable = true;
   
   damageEmitter[0] = jeepBurnEmitter;
	damageEmitterOffset[0] = "0.0 0.0 0.0 ";
	damageLevelTolerance[0] = 0.99;

   damageEmitter[1] = jeepBurnEmitter;
	damageEmitterOffset[1] = "0.0 0.0 0.0 ";
	damageLevelTolerance[1] = 1.0;

   numDmgEmitterAreas = 1;

   initialExplosionProjectile = boxtruckExplosionProjectile;
   initialExplosionOffset = 0;         //offset only uses a z value for now

   burnTime = 4000;

   finalExplosionProjectile = boxtruckFinalExplosionProjectile;
   finalExplosionOffset = 0.5;          //offset only uses a z value for now


   minRunOverSpeed    = 2;   //how fast you need to be going to run someone over (do damage)
   runOverDamageScale = 5;   //when you run over someone, speed * runoverdamagescale = damage amt
   runOverPushScale   = 1.2; //how hard a person you're running over gets pushed
};

function boxtruckVehicle::onTrigger(%this, %obj, %num, %cli, %wha)
{
	if(%cli $= 1 && $Sim::Time > %obj.nextDoorTime)
	{
		if(%obj.doorOpen $= 1)
		{
			%obj.playThread(0,close);
			%obj.doorOpen = 0;
		}
		else
		{
			%obj.playThread(0,open);
			%obj.doorOpen = 1;
		}
		%obj.nextDoorTime = $Sim::Time+3;
	}
}



// function boxtruckVehicle::onAdd(%this,%obj)
// {
	// %obj.setWheelTire(0, boxtrucktire);
	// %obj.setWheelTire(1, boxtrucktire);
	// %obj.setWheelTire(2, boxtrucktire);
	// %obj.setWheelTire(3, boxtrucktire);
	// %obj.setWheelTire(4, boxtrucktire);
	// %obj.setWheelTire(5, boxtrucktire);

	// %obj.setWheelSpring(0, boxtruckSpring);
	// %obj.setWheelSpring(1, boxtruckSpring);
	// %obj.setWheelSpring(2, boxtruckSpring);
	// %obj.setWheelSpring(3, boxtruckSpring);
	// %obj.setWheelSpring(4, boxtruckSpring);
	// %obj.setWheelSpring(5, boxtruckSpring);
	
	// %obj.setWheelSteering(0,1);
	// %obj.setWheelSteering(1,1);
	
	// %obj.setWheelPowered(0,true);
    // %obj.setWheelPowered(1,true);
	// %obj.setWheelPowered(2,true);
    // %obj.setWheelPowered(3,true);
	// %obj.setWheelPowered(4,true);
    // %obj.setWheelPowered(5,true);
// }	



