//guitar.cs
datablock AudioProfile(chainsawDrawSound)
{
   filename    = "./sound/draw_chainsaw.ogg";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(chainsawIdleSound)
{
   filename    = "./sound/idle_chainsaw.ogg";
   description = AudioCloseLooping3d;
   preload = true;
};

datablock AudioProfile(chainsawHitSound)
{
   filename    = "./sound/hit_chainsaw.ogg";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(chainsawActiveSound)
{
   filename    = "./sound/active_chainsaw.ogg";
   description = AudioCloseLooping3d;
   preload = true;
};

//effects
datablock ParticleData(sparkParticle)
{
   textureName          = "~/Particles/spark";
   dragCoefficient      = 1;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 100;
   lifetimeVarianceMS   = 50;

   colors[0]     = "0.60 0.40 0.30 1.0";
   colors[1]     = "0.60 0.40 0.30 1.0";
   colors[2]     = "1.0 0.40 0.30 0.0";

   sizes[0]      = 0.25;
   sizes[1]      = 0.15;
   sizes[2]      = 0.15;

   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(spark)
{
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;
   ejectionVelocity = 5;
   velocityVariance = 1;
   ejectionOffset   = 0.2;
   thetaMin         = 0;
   thetaMax         = 180;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   orientParticles  = true;
   lifetimeMS       = 50;
   particles = "sparkParticle";
};

datablock ExplosionData(chainsawExplosion)
{
   lifeTimeMS = 500;

   soundProfile = chainsawHitSound;

   particleEmitter = spark;
   particleDensity = 10;
   particleRadius = 0.2;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = true;
   camShakeFreq = "20.0 22.0 20.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.5;
   camShakeRadius = 10.0;
};


//projectile
AddDamageType("chainsaw",   '<bitmap:add-ons/ci/chainsaw> %1',    '%2 <bitmap:add-ons/ci/chainsaw> %1',1,1);
datablock ProjectileData(chainsawProjectile)
{
   directDamage        = 20;
   impactImpulse       = 20;
   verticalImpulse     = 20;
   directDamageType  = $DamageType::chainsaw;
   radiusDamageType  = $DamageType::chainsaw;
   explosion           = chainsawExplosion;

   muzzleVelocity      = 35;
   velInheritFactor    = 1;

   armingDelay         = 0;
   lifetime            = 64;
   fadeDelay           = 0;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.0;
};

datablock ParticleData(chainsawpuffParticle)
{
	dragCoefficient      = 1.0;
	gravityCoefficient   = -1;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.5;
	lifetimeMS           = 300;
	lifetimeVarianceMS   = 200;
	textureName          = "base/data/particles/cloud";
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	colors[0]     = "1.000000 1.000000 1.000000 0.010000";
	colors[1]     = "1.000000 1.000000 1.000000 0.020000";
	colors[2]     = "1.000000 1.000000 1.000000 0.020000";
	colors[3]     = "1.000000 1.000000 1.000000 0.030000";
	colors[4]     = "1.000000 1.000000 1.000000 0.040000";
	colors[5]     = "1.000000 1.000000 1.000000 0.006000";
	colors[6]     = "1.000000 1.000000 1.000000 0.007000";

	sizes[0]      = 0.50;
	sizes[1]      = 1.1;
	sizes[2]      = 1.2;
	sizes[3]      = 1.3;
	sizes[4]      = 0.50;
	sizes[5]      = 0.2;
	sizes[6]      = 0.1;

   	times[0] = 0;
   	times[1] = 1;
	times[2] = 4;

	useInvAlpha = false;
};

datablock ParticleEmitterData(chainsawpuffEmitter)
{
   ejectionPeriodMS = 13;
   periodVarianceMS = 4;
   ejectionVelocity = 0.3;
   velocityVariance = 0.1;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 0;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   orientOnVelocity = true;
   particles = "chainsawpuffParticle";
};

datablock ParticleData(chainsawpuff2Particle)
{
	dragCoefficient      = 1.0;
	gravityCoefficient   = -1;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.5;
	lifetimeMS           = 300;
	lifetimeVarianceMS   = 200;
	textureName          = "base/data/particles/cloud";
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	colors[0]     = "1.000000 1.000000 1.000000 0.030000";
	colors[1]     = "1.000000 1.000000 1.000000 0.030000";
	colors[2]     = "1.000000 1.000000 1.000000 0.040000";
	colors[3]     = "1.000000 1.000000 1.000000 0.040000";
	colors[4]     = "1.000000 1.000000 1.000000 0.050000";
	colors[5]     = "1.000000 1.000000 1.000000 0.007000";
	colors[6]     = "1.000000 1.000000 1.000000 0.008000";

	sizes[0]      = 0.50;
	sizes[1]      = 1.1;
	sizes[2]      = 1.2;
	sizes[3]      = 1.3;
	sizes[4]      = 0.50;
	sizes[5]      = 0.2;
	sizes[6]      = 0.1;

   	times[0] = 0;
   	times[1] = 1;
	times[2] = 4;

	useInvAlpha = false;
};

datablock ParticleEmitterData(chainsawpuff2Emitter)
{
   ejectionPeriodMS = 13;
   periodVarianceMS = 4;
   ejectionVelocity = 0.3;
   velocityVariance = 0.1;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 0;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   orientOnVelocity = true;
   particles = "chainsawpuff2Particle";
};


//////////
// item //
//////////
datablock ItemData(chainsawItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./shapes/chainsawitem.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Chainsaw";
	iconName = "./ItemIcons/chainsaw";
	doColorShift = false;
	colorShiftColor = "0.471 0.471 0.471 1.000";

	 // Dynamic properties defined by the scripts
	image = chainsawImage;
	canDrop = true;
};


////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(chainsawImage)
{
   // Basic Item properties
   shapeFile = "./shapes/chainsawweapon.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = 0; //"0.7 1.2 -0.5";
   //rotation = eulerToMatrix( "0 0 0" );

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = false;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = chainsawItem;
   ammo = " ";
   projectile = chainsawProjectile;
   projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = false;
   doRetraction = true;
   //raise your arm up or not
   armReady = true;

   //casing = " ";
   doColorShift = false;
   colorShiftColor = "0.471 0.471 0.471 1.000";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.5;
	stateTransitionOnTimeout[0]      = "Ready";
	stateSound[0]                    = chainsawDrawSound;

    stateName[1]                     = "Ready";
    stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]        = true;
	stateTimeoutValue[1]             = 0.1;
	stateTransitionOnTimeout[1]      = "Ready";
	stateSequence[1]	= "Active";
	stateSound[1]					= chainsawIdleSound;
	stateEmitter[1]                 = chainsawpuffEmitter;
	stateEmitterNode[1]				= everything;
	stateEmitterTime[1]		   = 0.2;

	stateName[2]					= "Fire";
	stateScript[2]                  = "onFire";
	stateFire[2]					= true;
	stateAllowImageChange[2]        = true;
	stateTimeoutValue[2]            = 0.10;
	stateTransitionOnTimeout[2]     = "Fire";
	stateTransitionOnTriggerUp[2]	= "Ready";
	stateSound[2]					= chainsawActiveSound;
	stateSequence[2]                = "TrigDown";
			stateEmitter[2]                 = chainsawpuff2Emitter;
			stateEmitterNode[2]				= everything;
	stateEmitterTime[2]		   = 0.2;
	//stateTransitionOnTriggerUp[3]	= "StopFire";


};

function chainsawProjectile::Damage(%this, %obj, %col, %fade, %pos, %normal)
{

 if(%col.getclassname() $= "Player")
 {
%damLoc = %col.getDamageLocation(%pos);

if(getword(%pos, 2) > getword(%col.getWorldBoxCenter(), 2) - 3.3)
{
%col.hideNode(headSkin);
%col.hideNode(helmet);
%col.hideNode(pointyHelmet);
%col.hideNode(flareHelmet);
%col.hideNode(scoutHat);
%col.hideNode(bicorn);
%col.hideNode(copHat);
%col.hideNode(knitHat);
%col.hideNode(triplume);
%col.hideNode(septplume);
%col.hideNode(visor);
%col.hideNode(plume);
}else if(getword(%pos, 2) > getword(%col.getWorldBoxCenter(), 2) - 4.5){ //adjust the 4.5
	//Torso shot
	if(strstr(%damLoc, "left") != -1){
		%col.hideNode(lhand);
        %col.hideNode(lhook);
	    %col.hideNode(larm);
		%col.hideNode(larmslim);
	}else if(strstr(%damLoc, "right") != -1){
		%col.hideNode(rhand);
        %col.hideNode(rhook);
		%col.hideNode(rarm);
		%col.hideNode(rarmslim);
	}
}else{
	//Leg shot
	if(strstr(%damLoc, "left") != -1){
		%col.hideNode(LShoe);
		%col.hideNode(LPeg);
	}else if(strstr(%damLoc, "right") != -1){
		%col.hideNode(RShoe);
		%col.hideNode(RPeg);
	}
}
}

return Parent::Damage(%this, %obj, %col, %fade, %pos, %normal);
}