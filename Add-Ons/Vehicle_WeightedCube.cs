if(!isObject(jeepExplosionSound))
{
   exec("./Support_Jeep.cs");
}
if(!isObject(FogEmitter))
{
 exec("./Particle_Basic.cs");
}
if($AddOn__Particle_Basic $= "-1")
{
 BurnEmitterA.uiname = "";
 BurnEmitterB.uiname = "";
 LaserEmitterA.uiname = "";
 FogEmitterA.uiname = "";
 WaterEmitterA.uiname = "";
 FogEmitter.uiname = "";
 FridgeFog1Emitter.uiname = "";
}

datablock WheeledVehicleData(WCubeVehicle) //Portal's Weighted Cube to throw around with the Gravity Gun
{
	category = "Vehicles";
	displayName = " ";
	shapeFile = "./shapes/weightedcube.dts"; 
	emap = true;
	minMountDist = 3;
   
   numMountPoints = 0;
   mountThread[0] = "sit";
   mountThread[1] = "sit";
   mountThread[2] = "sit";
   mountThread[3] = "sit";
   mountThread[4] = "sit";
   mountThread[5] = "sit";
   mountThread[6] = "sit";
   mountThread[7] = "sit";

	maxDamage = 99999.00;
	destroyedLevel = 99999.00;
	energyPerDamagePoint = 160;
	speedDamageScale = 1.04;
	collDamageThresholdVel = 20.0;
	collDamageMultiplier   = 0.02;

	massCenter = "0 0 0";
   massBox = "3 3 3";

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

	defaultTire	= jeepTire;
	defaultSpring	= jeepSpring;
	flatTire	= jeepFlatTire;
	flatSpring	= jeepFlatSpring;

   numWheels = 0;

	// Rigid Body
	mass = 200;
	density = 5.0;
	drag = 0.6;
	bodyFriction = 0.6;
	bodyRestitution = 0.2;
	minImpactSpeed = 10;        // Impacts over this invoke the script callback
	softImpactSpeed = 10;       // Play SoftImpact Sound
	hardImpactSpeed = 15;      // Play HardImpact Sound
	groundImpactMinSpeed    = 10.0;

	// Engine
	engineTorque = 12000; //4000;       // Engine power
	engineBrake = 2000;         // Braking when throttle is 0
	brakeTorque = 4000;        // When brakes are applied
	maxWheelSpeed = 20;        // Engine scale by current speed / max speed

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
	horizontalSurfaceForce	= 0;   // Horizontal center "wing" (provides "bite" into the wind for climbing/diving and turning)
	verticalSurfaceForce	= 0; 
	rollForce		= 4000;
	yawForce		= 6000;
	pitchForce		= 6000;
	rotationalDrag		= 0.15;
	stallSpeed		= 10;

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

   uiName = "Weighted Cube ";
	rideable = true;
		lookUpLimit = 0.65;
		lookDownLimit = 0.45;

	paintable = true;
   
   damageEmitter[0] = FogEmitter;
	damageEmitterOffset[0] = "0.0 0.0 0.0 ";
	damageLevelTolerance[0] = 0.99;

   damageEmitter[1] = FogEmitterA;
	damageEmitterOffset[1] = "0.0 0.0 0.0 ";
	damageLevelTolerance[1] = 1.0;

   numDmgEmitterAreas = 1;

   initialExplosionProjectile = wcubeFinalExplosionProjectile;
   initialExplosionOffset = 0;         //offset only uses a z value for now

   burnTime = 10;

   finalExplosionProjectile = wcubeFinalExplosionProjectile;
   finalExplosionOffset = 0.5;          //offset only uses a z value for now

   minRunOverSpeed    = 2;   //how fast you need to be going to run someone over (do damage)
   runOverDamageScale = 5;   //when you run over someone, speed * runoverdamagescale = damage amt
   runOverPushScale   = 1.2; //how hard a person you're running over gets pushed
};

datablock ExplosionData(wcubeFinalExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 150;

   soundProfile = jeepExplosionSound;
   
   emitter[0] = FridgeFog1Emitter;
   emitter[1] = FogEmitter;

   particleEmitter = FogEmitterA;
   particleDensity = 20;
   particleRadius = 1.0;

   debris = "";
   debrisNum = "";
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
   impulseRadius = 3;
   impulseForce = 100;
   impulseVertical = 200;

   //radius damage
   radiusDamage        = 10;
   damageRadius        = 2.0;

   //burn the players?
   playerBurnTime = 0;

};

datablock ProjectileData(wcubeFinalExplosionProjectile)
{
   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 0;
   explosion           = wcubeFinalExplosion;

   directDamageType  = $DamageType::JeepExplosion;
   radiusDamageType  = $DamageType::JeepExplosion;

   explodeOnDeath		= 1;

   armingDelay         = 0;
   lifetime            = 10;
};

function wcubeVehicle::onAdd(%this,%a,%b,%c)
{
	//%a.setscale("1.5 1.5 1.5");
}


function wcubeVehicle::onRemove(%this,%a,%b,%c)
{
	//nothing
}