//By Ephialtes
datablock ParticleData(sparklerParticleA)
{
   dragCoefficient      = 0;
   gravityCoefficient   = 0;
   inheritedVelFactor   = 1;
   constantAcceleration = 0.0;
   spinRandomMin = -90;
   spinRandomMax = 90;
   lifetimeMS           = 100;
   lifetimeVarianceMS   = 20;
   textureName          = "base/data/particles/star1";
   colors[0]     = "1 1 1 1";
   colors[1]     = "1 1 1 1";
   sizes[0]      = 0.1;
   sizes[1]      = 0.2;
};

datablock ParticleEmitterData(sparklerEmitterA)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   ejectionVelocity = 6;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 180;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "sparklerParticleA";
};

datablock ParticleData(sparklerParticleB)
{
   dragCoefficient      = 0;
   windCoefficient     = 0;
   gravityCoefficient   = 0;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   spinRandomMin = -90;
   spinRandomMax = 90;
   lifetimeMS           = 800;
   lifetimeVarianceMS   = 200;
   textureName          = "base/data/particles/star1";
   colors[0]     = "1 1 1 1";
   colors[1]     = "0.2 0.2 1 0";
   sizes[0]      = 0.1;
   sizes[1]      = 0.05;
};

datablock ParticleEmitterData(sparklerEmitterB)
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
   particles = "sparklerParticleB";
};

datablock ItemData(sparklerItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "base/data/shapes/wand.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Sparkler";
	iconName = "base/client/ui/itemIcons/wand";
	doColorShift = true;
	colorShiftColor = "0 0 0 1";

	 // Dynamic properties defined by the scripts
	image = sparklerImage;
	canDrop = true;
};

datablock ShapeBaseImageData(sparklerImage)
{
   // Basic Item properties
   shapeFile = "base/data/shapes/wand.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   //eyeOffset = "0.1 0.2 -0.55";

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = false;

   eyeOffset = "0.7 1.2 -0.25";

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = sparklerItem;
   ammo = " ";

   //melee particles shoot from eye node for consistancy
   melee = true;
   doRetraction = false;
   //raise your arm up or not
   armReady = true;

   //casing = " ";
   doColorShift = true;
   colorShiftColor = "0 0 0 1";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.01;
	stateTransitionOnTimeout[0]      = "Ready";
	stateEmitter[0]			   = SparklerEmitterB;
	stateEmitterTime[0]		   = 1000;
	stateSound[0]                    = weaponSwitchSound;

	stateName[1]                     = "Ready";
	stateEmitter[1]			   = SparklerEmitterA;
	stateEmitterTime[1]		   = 1000;
	stateTransitionOnTriggerDown[1]  = "PreFire";
	stateAllowImageChange[1]         = true;

	stateName[2]			  = "PreFire";
	stateScript[2]                  = "onPreFire";
	stateAllowImageChange[2]        = false;
	stateTimeoutValue[2]            = 0.1;
	stateTransitionOnTimeout[2]     = "Fire";

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "CheckFire";
	stateTimeoutValue[3]            = 0.2;
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateWaitForTimeout[3]		  = true;

	stateName[4]			= "CheckFire";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4] = "Fire";

	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.2;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		  = true;
	stateSequence[5]                = "StopFire";
	stateScript[5]                  = "onStopFire";
};

function sparklerImage::onPreFire(%this, %obj, %slot)
{
	%obj.playthread(2, armattack);
}

function sparklerImage::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}