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

//-----------------------------------------------------------------------------
if(!isObject(jeepExplosionSound))
{
   exec("./Support_Jeep.cs");
}
//----------------------------------------------------------------------------

datablock WheeledVehicleData(DDKartVehicle)
{
	category = "Vehicles";
	displayName = " ";
	shapeFile = "./shapes/mariokartDD.dts"; //"~/data/shapes/skivehicle.dts"; //
	emap = true;
	minMountDist = 3;
   
   numMountPoints = 2;
   mountThread[0] = "sit";
   mountThread[1] = "stand";

	maxDamage = 200000000000.00;
	destroyedLevel = 200000000000.00;
	energyPerDamagePoint = 160;
	speedDamageScale = 1.04;
	collDamageThresholdVel = 20.0;
	collDamageMultiplier   = 0.02;

	massCenter = "0 0 -0.5";
   //massBox = "2 5 1";

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

	defaultTire	= JeepTire;
	defaultSpring	= JeepSpring;
	flatTire	= JeepFlatTire;
	flatSpring	= JeepFlatSpring;

   numWheels = 4;

	// Rigid Body
	mass = 600;
	density = 15.0;
	drag = 1.6;
	bodyFriction = 0.6;
	bodyRestitution = 0.6;
	minImpactSpeed = 10;        // Impacts over this invoke the script callback
	softImpactSpeed = 10;       // Play SoftImpact Sound
	hardImpactSpeed = 15;      // Play HardImpact Sound
	groundImpactMinSpeed    = 10.0;

	// Engine
	engineTorque = 16000; //4000;       // Engine power
	engineBrake = 2000;         // Braking when throttle is 0
	brakeTorque = 50000;        // When brakes are applied
	maxWheelSpeed = 60;        // Engine scale by current speed / max speed

	rollForce		= 900;
	yawForce		= 600;
	pitchForce		= 1000;
	rotationalDrag		= 0.2;

	// Energy
	maxEnergy = 100;
	jetForce = 3000;
	minJetEnergy = 30;
	jetEnergyDrain = 2;

	splash = JeepSplash;
	splashVelocity = 4.0;
	splashAngle = 67.0;
	splashFreqMod = 300.0;
	splashVelEpsilon = 0.60;
	bubbleEmitTime = 1.4;
	splashEmitter[0] = JeepFoamDropletsEmitter;
	splashEmitter[1] = JeepFoamEmitter;
	splashEmitter[2] = JeepBubbleEmitter;
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

   uiName = "DoubleDash!! Kart ";
	rideable = true;
		lookUpLimit = 0.65;
		lookDownLimit = 0.45;

	paintable = true;
   
   damageEmitter[0] = JeepBurnEmitter;
	damageEmitterOffset[0] = "0.0 0.0 0.0 ";
	damageLevelTolerance[0] = 0.99;

   damageEmitter[1] = JeepBurnEmitter;
	damageEmitterOffset[1] = "0.0 0.0 0.0 ";
	damageLevelTolerance[1] = 1.0;

   numDmgEmitterAreas = 1;

   initialExplosionProjectile = JeepExplosionProjectile;
   initialExplosionOffset = 0;         //offset only uses a z value for now

   burnTime = 4000;

   finalExplosionProjectile = JeepFinalExplosionProjectile;
   finalExplosionOffset = 0.5;          //offset only uses a z value for now


   minRunOverSpeed    = 2;   //how fast you need to be going to run someone over (do damage)
   runOverDamageScale = 0;   //when you run over someone, speed * runoverdamagescale = damage amt
   runOverPushScale   = 30; //how hard a person you're running over gets pushed
};

function DDKartVehicle::OnTrigger(%this, %obj, %triggerNum, %val)
{
   if(%val){
      if(%triggerNum == 2){
		DoKartNitro(%obj);
	  }
   }
}

function DoKartNitro(%obj)
{
if(%obj.justnitro){
echo(%obj.getmountedobject(0).client);
commandtoclient(%obj.getmountedobject(0).client,'centerPrint',"You can't Nitro that often.",1.75);
return;
}
%forvec = %obj.getforwardvector();
%forvec = vectorscale(%forvec,5000);
%forvec = VectorAdd(%forvec,"0 0 1.5");
%obj.setvelocity(%forvec);
schedule(200,0,"ContinueNitro",%obj,0);
%obj.justnitro = 1;
schedule(30000,0,"ReNitro",%obj);
}

function continuenitro(%obj, %count)
{
if(%count == 6){
return;
}
%forvec = %obj.getforwardvector();
%forvec = vectorscale(%forvec,5000);
%forvec = VectorAdd(%forvec,"0 0 0.5");
%obj.setvelocity(%forvec);
%count++;
schedule(200,0,"ContinueNitro",%obj,%count);
}

function ReNitro(%obj)
{
%obj.justnitro = 0;
commandtoclient(%obj.getmountedobject(0).client,'bottomPrint',"Nitro recharged!",2);
}