if(!isObject(jeepExplosionSound))
{
   exec("./Support_Jeep.cs");
   exec("./weapon_gun.cs");
}

datablock ExplosionData(HeliExplosion: jeepExplosion)
{
   debrisNum = 0;
};

datablock ProjectileData(HeliExplosionProjectile)
{
   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 0;
   explosion           = HeliExplosion;

   directDamageType  = $DamageType::JeepExplosion;
   radiusDamageType  = $DamageType::JeepExplosion;

   explodeOnDeath		= 1;

   armingDelay         = 0;
   lifetime            = 40;
};


datablock ExplosionData(HeliFinalExplosion)
{
     //explosionShape = "";
   lifeTimeMS = 150;

   soundProfile = jeepExplosionSound;
   
   emitter[0] = jeepFinalExplosionEmitter3;
   emitter[1] = jeepFinalExplosionEmitter2;

   particleEmitter = jeepFinalExplosionEmitter;
   particleDensity = 20;
   particleRadius = 1.0;

   debris = HeliDebris;
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

datablock ProjectileData(HeliFinalExplosionProjectile)
{
   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 0;
   explosion           = HeliFinalExplosion;

   directDamageType  = $DamageType::JeepExplosion;
   radiusDamageType  = $DamageType::JeepExplosion;

   explodeOnDeath		= 1;

   armingDelay         = 0;
   lifetime            = 10;
};
datablock DebrisData(HeliDebris)
{
   emitters = "JeepDebrisTrailEmitter";

	shapeFile = "./shapes/U1.dts";
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

datablock FlyingVehicleData(HeliVehicle)
{
   //Tagged fields for mission editor
      category = "Vehicles";
      displayName = " ";

   //Shapebase Fields
      shapeFile   = "./shapes/U1.dts";  //.dts File
      emap        = true;
      mass        = 200;
      drag        = 1.7;
      density     = 4;
      
      maxDamage = 100.00;
      destroyedLevel = 100.00;
      energyPerDamagePoint = 160;
      speedDamageScale = 1.04;
      collDamageThresholdVel = 20.0;
      collDamageMultiplier   = 0.02;

   //Tagged fields for mounting
      minMountDist = 3;   
      numMountPoints = 4;
      mountThread[0] = "sit";
      mountThread[1] = "sit";
      mountThread[2] = "sit";
      mountThread[3] = "sit";


      lookUpLimit = 0.75;
		lookDownLimit = 0.35;

   //Vehicle Fields:
      jetForce          = 500;
      jetEnergyDrain    = 8;
      minJetEnergy      = 1;

      massCenter        = "0 0 0";
      //massBox           = "1 1 1";
      bodyRestitution   = 0.5;
      bodyFriction      = 0.5;
      //softImpactSound   = ; //AudioProfile
      //hardImpactSound   = ; //AudioProfile

      minImpactSpeed    = 25;
      softImpactSpeed   = 25;
      hardImpactSpeed   = 50;
      minRollSpeed      = 0;
      maxSteeringAngle  = 0.785;

      maxDrag        = 40;
      minDrag        = 50;
      integration    = 4;
      collisionTol   = 0.1;
      contactTol     = 0.1;

      cameraRoll     = false;
      cameraMaxDist  = 16;        
      cameraLag      = 0.0;
      cameraDecay    = 0.0;
      cameraOffset   = 5.5;
      cameraTilt     = 0.0;

      //dustEmitter       = ; //ParticleEmitterData
      triggerDustHeight = 3.0;
      dustHeight        = 1.0;

      numDmgEmitterAreas   = 0;
      
      damageEmitter[0] = JeepBurnEmitter;
      damageEmitterOffset[0] = "0.0 0.0 0.0 ";
      damageLevelTolerance[0] = 0.99;

      damageEmitter[1] = JeepBurnEmitter;
      damageEmitterOffset[1] = "0.0 0.0 0.0 ";
      damageLevelTolerance[1] = 1.0;

      //splashEmitter[0]        = ; //ParticleEmitterData

      splashFreqMod     = 300.0;
      splashVelEpsilon  = 0.50;

      exitSplashSoundVelocity    = 2.0;
      softSplashSoundVelocity    = 1.0;
      mediumSplashSoundVelocity  = 2.0;
      hardSplashSoundVelocity    = 3.0;
      //exitingWater               = ;   //AudioProfile
      //impactWaterEasy            = ;   //AudioProfile
      //impactWaterMedium          = ;   //AudioProfile
      //impactWaterHard            = ;   //AudioProfile
      //waterWakeSound             = ;   //AudioProfile

      collDamageThresholdVel  = 20;
      collDamageMultiplier    = 0.05;

   //For Wrench Gui
      uiName   = "UH-1 Iroquois";
      rideAble = true;
      paintable = true;
		
   //Flying vehicle fields
      //jetSound = ;      //AudioProfile
      //engineSound = ;   //AudioProfile

maneuveringForce = 3000; // Horizontal jets (W,S,D,A key thrust) 
horizontalSurfaceForce = 10; // Horizontal center "wing" (provides "bite" into the wind for climbing/diving and turning) 
verticalSurfaceForce = 100; // Vertical center "wing" (controls side slip. lower numbers make MORE slide.) 
autoInputDamping = 0.95; // Dampen control input so you don't` whack out at very slow speeds 
steeringForce = 1000; // Steering jets (force applied when you move the mouse) 
steeringRollForce = -5; // Steering jets (how much you heel over when you turn) 
rollForce = 1; // Auto-roll (self-correction to right you after you roll/invert) 
autoAngularForce = 100; // Angular stabilizer force (this force levels you out when autostabilizer kicks in) 
rotationalDrag = 8; // Anguler Drag (dampens the drift after you stop moving the mouse...also tumble drag)
autoLinearForce = 100; // Linear stabilzer force (this slows you down when autostabilizer kicks in) 
maxAutoSpeed = 15; // Autostabilizer kicks in when less than this speed. (meters/second) 
hoverHeight = 0.7; // Height off the ground at rest 
createHoverHeight = 0.7; // Height off the ground when created //exec("add-ons/vehicle_U1.cs");

      //forwardJetEmitter    = ; //ParticleEmitterData
      //backwardJetEmitter   = ; //ParticleEmitterData
      //downJetEmitter       = ; //ParticleEmitterData
      //trailEmitter         = ; //ParticleEmitterData

      minTrailSpeed        = 1;
      vertThrustMultiple   = 1.0;
   
   //Tagged fields for damage
      initialExplosionProjectile = HeliExplosionProjectile;
      initialExplosionOffset = 0;         //offset only uses a z value for now

      burnTime = 500;

      finalExplosionProjectile = HeliFinalExplosionProjectile;
      finalExplosionOffset = 0.5;          //offset only uses a z value for now

      minRunOverSpeed    = 5;   //how fast you need to be going to run someone over (do damage)
      runOverDamageScale = 5;   //when you run over someone, speed * runoverdamagescale = damage amt
      runOverPushScale   = 1.2; //how hard a person you're running over gets pushed
};


function Helivehicle::onadd(%this,%obj)
{ 
	parent::onadd(%this,%obj);
	   %obj.playThread(0,"Ambient");
}

