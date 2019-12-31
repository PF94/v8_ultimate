if(!isObject(jeepExplosionSound))
{
   exec("./Support_Jeep.cs");
}

datablock ExplosionData(PatrolBoat2Explosion: jeepExplosion)
{
   debrisNum = 0;
};

datablock ProjectileData(PatrolBoat2ExplosionProjectile)
{
   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 0;
   explosion           = PatrolBoat2Explosion;

   directDamageType  = $DamageType::JeepExplosion;
   radiusDamageType  = $DamageType::JeepExplosion;

   explodeOnDeath		= 1;

   armingDelay         = 0;
   lifetime            = 10;
};


datablock ExplosionData(PatrolBoat2FinalExplosion)
{
     //explosionShape = "";
   lifeTimeMS = 150;

   soundProfile = jeepExplosionSound;
   
   emitter[0] = jeepFinalExplosionEmitter3;
   emitter[1] = jeepFinalExplosionEmitter2;

   particleEmitter = jeepFinalExplosionEmitter;
   particleDensity = 20;
   particleRadius = 1.0;

   debris = jeepDebris;
   debrisNum = 0;
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

datablock ProjectileData(PatrolBoat2FinalExplosionProjectile)
{
   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 0;
   explosion           = PatrolBoat2FinalExplosion;

   directDamageType  = $DamageType::JeepExplosion;
   radiusDamageType  = $DamageType::JeepExplosion;

   explodeOnDeath		= 1;

   armingDelay         = 0;
   lifetime            = 10;
};
//Lego Boat
//---------------------------------------------------------------------------------------
// Drone Definition
datablock FlyingVehicleData(PatrolBoat)
{
	spawnOffset						= "0 0 2";
	emap							= true;
	category						= "Vehicles";
    shapeFile                       = "~/shapes/PTboat1.dts";
	multipassenger					= True;
	computeCRC						= true;

	drag							= 100;
	density							= 0.7;

   maxMountSpeed = 0.3;
   mountDelay = 8;
   dismountDelay = 1;
   maxDismountSpeed = 0.0;

   stationaryThreshold = 0;

      minMountDist = 3;   
      numMountPoints = 6;
      mountThread[0] = "root";
      mountThread[1] = "root";
      mountThread[2] = "root";
      mountThread[3] = "root";
      mountThread[4] = "root";
      mountThread[5] = "root";
      mountThread[6] = "root";
      mountThread[7] = "root";

    cameraOffset = 4;        // Vertical offset from camera mount point
	cameraMaxDist					= 16;
	cameraLag						= 0.1;
    cameraRoll = true;         // Roll the camera with the vehicle

	explosionDamage					= 10.5;
	explosionRadius					= 15.0;

	maxDamage						= 50.40;
	destroyedLevel					= 50.40;
									
	energyPerDamagePoint			= 160;
	maxEnergy						= 280;      // Afterburner and any energy weapon pool
	rechargeRate					= 0.8;

	minDrag							= 40;           // Linear Drag (eventually slows you down when not thrusting...constant drag)
	rotationalDrag					= 20;        // Anguler Drag (dampens the drift after you stop moving the mouse...also tumble drag)

	maxAutoSpeed					= 2000;       // Autostabilizer kicks in when less than this speed. (meters/second)
	autoAngularForce				= 60000;       // Angular stabilizer force (this force levels you out when autostabilizer kicks in)
	autoLinearForce					= 300;        // Linear stabilzer force (this slows you down when autostabilizer kicks in)
	autoInputDamping				= 1;      // Dampen control input so you don't` whack out at very slow speeds

	// Maneuvering
	maxSteeringAngle				= 1;    // Max radiens you can rotate the wheel. Smaller number is more maneuverable.
	horizontalSurfaceForce			= 0;   // Horizontal center "wing" (provides "bite" into the wind for climbing/diving and turning)
	verticalSurfaceForce			= -1;     // Vertical center "wing" (controls side slip. lower numbers make MORE slide.)
	maneuveringForce				= 10000;      // Horizontal jets (W,S,D,A key thrust)
	steeringForce					= 500;         // Steering jets (force applied when you move the mouse)
	steeringRollForce				= -30;      // Steering jets (how much you heel over when you turn)
	rollForce						= 10;  // Auto-roll (self-correction to right you after you roll/invert)
	hoverHeight						= 0.5;       // Height off the ground at rest
	createHoverHeight				= 0.5;  // Height off the ground when created
	maxForwardSpeed					= 10000;  // speed in which forward thrust force is no longer applied (meters/second)

	// Turbo Jet
	jetForce						= 10000;      // Afterburner thrust (this is in addition to normal thrust)
	minJetEnergy					= 28;     // Afterburner can't be used if below this threshhold.
	jetEnergyDrain					= 2.8;       // Energy use of the afterburners (low number is less drain...can be fractional)                                                                                                                                                                                                                                                                                          // Auto stabilize speed
	vertThrustMultiple				= 0;

	// Rigid body
	mass							= 100;        // Mass of the vehicle
    integration                     = 3;           // Physics integration: TickSec/Rate
    collisionTol = 0.6; // Collision distance tolerance
    contactTol = 0; // Contact velocity tolerance

    bodyFriction					= 0;     // Don't mess with this.
	bodyRestitution					= 0.9;   // When you hit the ground, how much you rebound. (between 0 and 1)
	minRollSpeed					= 2000;     // Don't mess with this.
	softImpactSpeed					= 3;       // Sound hooks. This is the soft hit.
	hardImpactSpeed					= 15;    // Sound hooks. This is the hard hit.

	// Ground Impact Damage (uses DamageType::Ground)
	minImpactSpeed					= 0;      // If hit ground at speed above this then it's an impact. Meters/second
	speedDamageScale				= 0.06;

	// Object Impact Damage (uses DamageType::Impact)
	collDamageThresholdVel			= 23.0;
	collDamageMultiplier			= 0.02;

	//
	minTrailSpeed					= 15;      // The speed your contrail shows up at.
	//trailEmitter					= DroneContrailEmitter;
	//forwardJetEmitter				= DroneJetEmitter;
	//downJetEmitter					= DroneJetEmitter;

	//
	//jetSound						= DroneThrustSound;
	//engineSound						= DroneEngineSound;
	//softImpactSound					= DroneSoftImpactSound;
	//hardImpactSound					= DroneHardImpactSound;
	//
	//softSplashSoundVelocity			= 10.0;
	//mediumSplashSoundVelocity		= 15.0;
	//hardSplashSoundVelocity			= 20.0;
	//exitSplashSoundVelocity			= 10.0;

	//exitingWater					= DroneExitWaterMediumSound;
	//impactWaterEasy					= DroneImpactWaterSoftSound;
	//impactWaterMedium				= DroneImpactWaterMediumSound;
	//impactWaterHard					= DroneImpactWaterMediumSound;
	//waterWakeSound					= DroneWakeMediumSplashSound;

//	dustEmitter						= VehicleLiftoffDustEmitter;
	
	triggerDustHeight				= 4.0;
	dustHeight						= 1.0;

//	damageEmitter[0]				= LightDamageSmoke;

//	damageEmitter[1]				= HeavyDamageSmoke;

//	damageEmitter[2]				= DamageBubbles;

	damageEmitterOffset[0]			= "0.0 -3.0 0.0 ";
	damageLevelTolerance[0]			= 0.3;
	damageLevelTolerance[1]			= 0.7;
	numDmgEmitterAreas				= 3;
						
	//
	//max[RocketAmmo]					= 1000;

	minMountDist					= 2;

	//shieldImpact					= VehicleShieldImpact;

	//cmdCategory						= "Tactical";
	//cmdIcon							= CMDFlyingScoutIcon;
	//cmdMiniIconName					= "commander/MiniIcons/com_scout_grey";
	//targetNameTag					= 'Drone';
	//targetTypeTag					= 'FlyingVehicle';
	//sensorData						= AWACPulseSensor;
	//sensorRadius					= AWACPulseSensor.detectRadius;
	//sensorColor						= "255 194 9";
									
	checkRadius						= 15.5;
	observeParameters				= "0 0 1";
									
	shieldEffectScale				= "0.937 1.125 0.60";


	paintable = true;
   
   damageEmitter[0] = JeepBurnEmitter;
	damageEmitterOffset[0] = "0.0 0.0 0.0 ";
	damageLevelTolerance[0] = 2.0;

   damageEmitter[1] = JeepBurnEmitter;
	damageEmitterOffset[1] = "0.0 0.0 0.0 ";
	damageLevelTolerance[1] = 2.8;

   numDmgEmitterAreas = 1;

   initialExplosionProjectile = JeepExplosionProjectile;
   initialExplosionOffset = 0;         //offset only uses a z value for now

   burnTime = 4000;

   finalExplosionProjectile = JeepFinalExplosionProjectile;
   finalExplosionOffset = 0.5;          //offset only uses a z value for now


   minRunOverSpeed    = 2;   //how fast you need to be going to run someone over (do damage)
   runOverDamageScale = 10;   //when you run over someone, speed * runoverdamagescale = damage amt
   runOverPushScale   = 1.2; //how hard a person you're running over gets pushed




   //For Wrench Gui
      uiName   = "Patrol Boat";
      rideAble = true;
      paintable = true;
		lookUpLimit = 0.65;
		lookDownLimit = 0.45;
};










// End Drone Definition
//----------------------------------------------------------------------------------------
// Vehicle Drone Functions

function PatrolBoat::onEnterLiquid(%this, %obj, %coverage, %type)
{
	cancel(%obj.floatSchedule);
}
function PatrolBoat::onLeaveLiquid(%this, %obj, %type)
{
	%obj.floatSchedule = schedule(10,0,lowerboat,%obj);
}
function PatrolBoat::onImpact(%this, %obj, %collidedObject, %vec, %vecLen)
{
	%obj.setvelocity("0 0 0");
}
function PatrolBoat::onDamage(%this, %obj, %delta)
{
	Parent::onDamage(%this, %obj);
	%currentDamage = %obj.getDamageLevel();
	if(%currentDamage > %obj.destroyedLevel)
	{
		if(%obj.getDamageState() !$= "Destroyed")
		{
			if(%obj.respawnTime !$= "")
				%obj.marker.schedule = %obj.marker.data.schedule(%obj.respawnTime, "respawn", %obj.marker); 
			%obj.setDamageState(Destroyed);
		}
	}
	else
	{
		if(%obj.getDamageState() !$= "Enabled")
			%obj.setDamageState(Enabled);
	}
}

function PatrolBoat::onAdd(%this,%obj)
{		 
	parent::onAdd(%this,%obj);
	   %rnd = getRandom($TotalColors);
	   if(%rnd == 1)%rnd = 2;
	   %obj.setSkinName($legoColor[%rnd]);   
	   %obj.mountable = true;
}


function PatrolBoat2::onEnterLiquid(%this, %obj, %coverage, %type)
{
	cancel(%obj.floatSchedule);
	ServerPlay3D(WakeSound, %obj.getTransform());
}
function PatrolBoat2::onLeaveLiquid(%this, %obj, %type)
{
	%obj.floatSchedule = schedule(10,0,lowerboat,%obj);
	ServerPlay3D(WakeSound, %obj.getTransform());
}
function lowerboat(%obj){
%pos = %obj.getposition();
%pos = addtoz(%pos,-0.01);
%obj.settransform(%pos);
ServerPlay3D(WakeSound, %obj.getTransform());
if(isObject(%obj))
%obj.floatSchedule = schedule(8,0,lowerboat,%obj);
}

function PatrolBoat2::onImpact(%this, %obj, %collidedObject, %vec, %vecLen)
{
	%obj.setvelocity("0 0 0");
}
function PatrolBoat2::onDamage(%this, %obj, %delta)
{
	Parent::onDamage(%this, %obj);
	%currentDamage = %obj.getDamageLevel();
	if(%currentDamage > %obj.destroyedLevel)
	{
		if(%obj.getDamageState() !$= "Destroyed")
		{
			if(%obj.respawnTime !$= "")
				%obj.marker.schedule = %obj.marker.data.schedule(%obj.respawnTime, "respawn", %obj.marker); 
			%obj.setDamageState(Destroyed);
		}
	}
	else
	{
		if(%obj.getDamageState() !$= "Enabled")
			%obj.setDamageState(Enabled);
	}
}

function PatrolBoat2::onAdd(%this,%obj)
{		 
	parent::onAdd(%this,%obj);
	   %rnd = getRandom($TotalColors);
	   if(%rnd == 1)%rnd = 2;
	   %obj.setSkinName($legoColor[%rnd]);   
	   %obj.mountable = true;
}

function addtoz(%pos,%add){
%posx = getword(%pos,0);
%posy = getword(%pos,1);
%posz = getword(%pos,2);
%newposz = %posz + %add;
%pos = %posx SPC %posy SPC %newposz;
return %pos;
}
