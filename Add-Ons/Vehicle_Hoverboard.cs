datablock ParticleData(TireParticle)
{
   textureName          = "";

   dragCoefficient      = 0.0;
   gravityCoefficient   = 2.0;
   windCoefficient      = 0.0;

   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;

   lifetimeMS           = 0;
   lifetimeVarianceMS   = 0;

   colors[0]     = "0 0 0 0"; //"0.46 0.36 0.26 1.0";
   colors[1]     = "0 0 0 0"; //"0.46 0.46 0.36 0.0";
   sizes[0]      = 0;
   sizes[1]      = 0.0;

   useInvAlpha = true;
};

datablock ParticleEmitterData(TireEmitter)
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
   particles = "";
};

datablock WheeledVehicleTire(hovTire)
{
   shapeFile = "./shapes/-=REDco=-/hov/hov.dts";
   staticFriction = 4;
   kineticFriction = 7;

   // Spring that generates lateral tire forces
   lateralForce = 18000;
   lateralDamping = 4000;
   lateralRelaxation = 1;

   // Spring that generates longitudinal tire forces
   longitudinalForce = 18000;
   longitudinalDamping = 4000;
   longitudinalRelaxation = 1;
};

datablock WheeledVehicleSpring(hovSpring)
{
   // Wheel suspension properties
   length = 0.4;                // Suspension travel
   force = 2000;              // Spring force
   damping = 600;             // Spring damping
   antiSwayForce = 6;        // Lateral anti-sway force
};

datablock WheeledVehicleData(hovVehicle)
{
	category = "Vehicles";
	displayName = "Hover Board";
	shapeFile = "./shapes/-=REDco=-/hov/hoverboard.dts";
	emap = true;
	minMountDist = 3;
   
   numMountPoints = 1;
   mountThread[0] = "root";

	maxDamage = 200.00;
	destroyedLevel = 200000000.00;
	energyPerDamagePoint = 160;
	speedDamageScale = 1.04;
	collDamageThresholdVel = 20.0;
	collDamageMultiplier   = 0.02;

	massCenter = "0 0 0";
   //massBox = "2 5 1";

	maxSteeringAngle = 0.85;  // Maximum steering angle, should match animation
	integration = 4;           // Force integration time: TickSec/Rate
	//tireEmitter = TireEmitter; // All the tires use the same dust emitter

	// 3rd person camera settings
	cameraRoll = false;         // Roll the camera with the vehicle
	cameraMaxDist = 13;         // Far distance from vehicle
	cameraOffset = 7.5;        // Vertical offset from camera mount point
	cameraLag = 001;           // Velocity lag of camera
	cameraDecay = 0.75;        // Decay per sec. rate of velocity lag
	cameraTilt = 0.4;
   collisionTol = 0.1;        // Collision distance tolerance
   contactTol = 0.1;

	useEyePoint = false;	

	defaultTire	= hovTire;
	defaultSpring	= hovSpring;
	flatTire	= hovTire;
	flatSpring	= hovFlatSpring;

   numWheels = 4;

	// Rigid Body
	mass = 300;
	density = 5.0;
	drag = 1.6;
	bodyFriction = 0.6;
	bodyRestitution = 0.6;
	minImpactSpeed = 5;        // Impacts over this invoke the script callback
	softImpactSpeed = 5;       // Play SoftImpact Sound
	hardImpactSpeed = 15;      // Play HardImpact Sound
	groundImpactMinSpeed    = 10.0;

	// Engine
	engineTorque = 4000; //4000;       // Engine power
	engineBrake = 600;         // Braking when throttle is 0
	brakeTorque = 8000;        // When brakes are applied
	maxWheelSpeed = 25;        // Engine scale by current speed / max speed

	rollForce		= 900;
	yawForce		= 600;
	pitchForce		= 1000;
	rotationalDrag		= 0.2;

	// Energy
	maxEnergy = 100;
	jetForce = 3000;
	minJetEnergy = 30;
	jetEnergyDrain = 2;

	//   explosion = VehicleExplosion;
	justcollided = 0;

   uiName = "Hover Board ";
	rideable = true;
		lookUpLimit = 1.8;
		lookDownLimit = 1;

	paintable = true;
   
   //damageEmitter[0] = ;
	damageEmitterOffset[0] = "0.0 0.0 0.0 ";
	damageLevelTolerance[0] = 0.99;

   //damageEmitter[1] = ;
	damageEmitterOffset[1] = "0.0 0.0 0.0 ";
	damageLevelTolerance[1] = 1.0;

   numDmgEmitterAreas = 1;


   minRunOverSpeed    = 1;   //how fast you need to be going to run someone over (do damage)
   runOverDamageScale = 10;   //when you run over someone, speed * runoverdamagescale = damage amt
   runOverPushScale   = 1; //how hard a person you're running over gets pushed
};

function hov::onCollision(%this,%obj,%col,%vec,%speed)
{
   // Collision with other objects, including items
   	Parent::onDamage(%this, %obj);
}