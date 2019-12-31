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

datablock WheeledVehicleTire(drTire)
{
   shapeFile = "./shapes/-=REDco=-/dragracer/dragwheel.dts";
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

datablock WheeledVehicleSpring(drSpring)
{
   // Wheel suspension properties
   length = 0.4;                
   force = 4000;              
   damping = 800;             
   antiSwayForce = 6;        
};

datablock WheeledVehicleData(drVehicle)
{
	category = "Vehicles";
	displayName = "Drag Racer";
	shapeFile = "./shapes/-=REDco=-/dragracer/dragracer.dts";
	emap = true;
	minMountDist = 3;
   
   numMountPoints = 1;
   mountThread[0] = "sit";

	maxDamage = 200.00;
	destroyedLevel = 200000000.00;
	energyPerDamagePoint = 160;
	speedDamageScale = 1.04;
	collDamageThresholdVel = 20.0;
	collDamageMultiplier   = 0.02;

	massCenter = "0 0 0";
   //massBox = "2 5 1";

	maxSteeringAngle = 0.45;  
	integration = 4;           
	//tireEmitter = TireEmitter; 

	// 3rd person camera settings
	cameraRoll = false;         
	cameraMaxDist = 13;         
	cameraOffset = 7.5;        
	cameraLag = 001;           
	cameraDecay = 0.75;        
	cameraTilt = 0.4;
   collisionTol = 0.1;        
   contactTol = 0.1;

	useEyePoint = false;	

	defaultTire	= drTire;
	defaultSpring	= drSpring;
	flatTire	= drTire;
	flatSpring	= drFlatSpring;

   numWheels = 4;

	// Rigid Body
	mass = 400;
	density = 7.0;
	drag = 1.6;
	bodyFriction = 0.6;
	bodyRestitution = 0.6;
	minImpactSpeed = 5;        
	softImpactSpeed = 5;       
	hardImpactSpeed = 15;      
	groundImpactMinSpeed    = 10.0;

	// Engine
	engineTorque = 12000; 
	engineBrake = 2000;         
	brakeTorque = 8000;        
	maxWheelSpeed = 90;        

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

   uiName = "Drag Racer ";
	rideable = true;
		lookUpLimit = 1.4;
		lookDownLimit = 1;

	paintable = true;
   
   //damageEmitter[0] = ;
	damageEmitterOffset[0] = "0.0 0.0 0.0 ";
	damageLevelTolerance[0] = 0.99;

   //damageEmitter[1] = ;
	damageEmitterOffset[1] = "0.0 0.0 0.0 ";
	damageLevelTolerance[1] = 1.0;

   numDmgEmitterAreas = 1;


   minRunOverSpeed    = 20;   
   runOverDamageScale = 30;   
   runOverPushScale   = 1000; 
};

function dr::onCollision(%this,%obj,%col,%vec,%speed)
{
   // Collision with other objects, including items
   	Parent::onDamage(%this, %obj);
}