//Support_racingrideon.cs

// explosions, particle emitters, etc for racingrideon 
// putting them in a seperate file so other add-ons can use them



datablock AudioProfile(fastImpactSound)
{
   filename    = "./sound/fastimpact.WAV";
   description = AudioDefault3d;
   preload = true;
};
datablock AudioProfile(slowImpactSound)
{
   filename    = "./sound/slowimpact.wav";
   description = AudioDefault3d;
   preload = true;
};
//datablock AudioProfile(landingSound)
//{
//   filename    = "base/data/sound/landing.wav";
//   description = AudioDefault3d;
//   preload = true;
//};
//datablock AudioProfile(skidSound)
//{
//   filename    = "base/data/sound/steam.wav";
//   description = AudioDefaultLooping3d;
//   preload = true;
//};
datablock AudioProfile(idleSound)
{
   filename    = "base/data/sound/idle.wav";
   description = AudioDefaultLooping3d;
   preload = true;
};
//datablock AudioProfile(vehiclecrash1Sound)
//{
//   filename    = "./sound/vehiclecrash1.wav";
//   description = AudioDefault3d;
//   preload = true;
//};

datablock AudioProfile(racingrideonExplosionSound)
{
   filename    = "./sound/racingrideonExplosion.wav";
   description = AudioDefault3d;
   preload = true;
};
//----------------------------------------------------------------------------
// Splash
//----------------------------------------------------------------------------

datablock ParticleData(racingrideonSplashMist)
{
   dragCoefficient      = 2.0;
   gravityCoefficient   = -0.05;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 400;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = false;
   spinRandomMin        = -90.0;
   spinRandomMax        = 500.0;
   textureName          = "base/data/particles/cloud";
   colors[0]     = "0.7 0.8 1.0 1.0";
   colors[1]     = "0.7 0.8 1.0 0.5";
   colors[2]     = "0.7 0.8 1.0 0.0";
   sizes[0]      = 2.5;
   sizes[1]      = 2.5;
   sizes[2]      = 5.0;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(racingrideonSplashMistEmitter)
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 0;
   ejectionVelocity = 3.0;
   velocityVariance = 2.0;
   ejectionOffset   = 1.0;
   thetaMin         = 85;
   thetaMax         = 85;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   lifetimeMS       = 250;
   particles = "racingrideonSplashMist";

   uiName = "racingrideon Splash Mist";
   emitterNode = FifthEmitterNode;
};


datablock ParticleData(racingrideonBubbleParticle)
{
   dragCoefficient      = 0.0;
   gravityCoefficient   = -0.50;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 400;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = false;
   textureName          = "base/data/particles/cloud";
   colors[0]     = "0.7 0.8 1.0 0.4";
   colors[1]     = "0.7 0.8 1.0 0.4";
   colors[2]     = "0.7 0.8 1.0 0.0";
   sizes[0]      = 0.1;
   sizes[1]      = 0.3;
   sizes[2]      = 0.3;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(racingrideonBubbleEmitter)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   ejectionVelocity = 2.0;
   ejectionOffset   = 1.5;
   velocityVariance = 0.5;
   thetaMin         = 0;
   thetaMax         = 80;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   particles = "racingrideonBubbleParticle";

   uiName = "racingrideon Bubbles";
   emitterNode = FifthEmitterNode;
};

datablock ParticleData(racingrideonFoamParticle)
{
   dragCoefficient      = 2.0;
   gravityCoefficient   = -0.05;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 400;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = false;
   spinRandomMin        = -90.0;
   spinRandomMax        = 500.0;
   textureName          = "base/data/particles/cloud";
   colors[0]     = "0.7 0.8 1.0 0.20";
   colors[1]     = "0.7 0.8 1.0 0.20";
   colors[2]     = "0.7 0.8 1.0 0.00";
   sizes[0]      = 1.2;
   sizes[1]      = 1.4;
   sizes[2]      = 2.6;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(racingrideonFoamEmitter)
{
   ejectionPeriodMS = 20;
   periodVarianceMS = 0;
   ejectionVelocity = 3.0;
   velocityVariance = 1.0;
   ejectionOffset   = 0.75;
   thetaMin         = 85;
   thetaMax         = 85;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   particles = "racingrideonFoamParticle";

   uiName = "racingrideon Foam";
   emitterNode = GenericEmitterNode;
};


datablock ParticleData( racingrideonFoamDropletsParticle )
{
   dragCoefficient      = 1;
   gravityCoefficient   = 0.2;
   inheritedVelFactor   = 0.2;
   constantAcceleration = -0.0;
   lifetimeMS           = 600;
   lifetimeVarianceMS   = 0;
   textureName          = "base/data/particles/cloud";
   colors[0]     = "0.7 0.8 1.0 1.0";
   colors[1]     = "0.7 0.8 1.0 0.5";
   colors[2]     = "0.7 0.8 1.0 0.0";
   sizes[0]      = 0.8;
   sizes[1]      = 0.3;
   sizes[2]      = 0.0;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData( racingrideonFoamDropletsEmitter )
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   ejectionVelocity = 2;
   velocityVariance = 1.0;
   ejectionOffset   = 1.0;
   thetaMin         = 60;
   thetaMax         = 80;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   orientParticles  = true;
   particles = "racingrideonFoamDropletsParticle";

   uiName = "racingrideon Foam Droplets";
   emitterNode = GenericEmitterNode;
};


datablock ParticleData( racingrideonSplashParticle )
{
   dragCoefficient      = 1;
   gravityCoefficient   = 0.2;
   inheritedVelFactor   = 0.2;
   constantAcceleration = -0.0;
   lifetimeMS           = 600;
   lifetimeVarianceMS   = 0;
   textureName          = "base/data/particles/cloud";
   colors[0]     = "0.7 0.8 1.0 1.0";
   colors[1]     = "0.7 0.8 1.0 0.5";
   colors[2]     = "0.7 0.8 1.0 0.0";
   sizes[0]      = 0.5;
   sizes[1]      = 0.5;
   sizes[2]      = 0.5;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData( racingrideonSplashEmitter )
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   ejectionVelocity = 3;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 60;
   thetaMax         = 80;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   orientParticles  = true;
   lifetimeMS       = 100;
   particles = "racingrideonSplashParticle";

   uiName = "racingrideon Splash";
   emitterNode = TenthEmitterNode;
};


///////////////////////////////////////////////////////////////////


datablock ParticleData(TireParticle)
{
   textureName          = "base/data/particles/chunk";

   dragCoefficient      = 0.0;
   gravityCoefficient   = 2.0;
   windCoefficient      = 0.0;

   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;

   lifetimeMS           = 800;
   lifetimeVarianceMS   = 300;

   colors[0]     = "0 0 0 1"; //"0.46 0.36 0.26 1.0";
   colors[1]     = "0 0 0 0"; //"0.46 0.46 0.36 0.0";
   sizes[0]      = 0.25;
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
   particles = "TireParticle";
};


datablock ParticleData(racingrideonBurnParticle)
{
	textureName          = "base/data/particles/cloud";
	dragCoefficient      = 0.0;
	gravityCoefficient   = -1.0;
	inheritedVelFactor   = 0.0;
	windCoefficient      = 0;
	constantAcceleration = 3.0;
	lifetimeMS           = 1200;
	lifetimeVarianceMS   = 100;
	spinSpeed     = 0;
	spinRandomMin = -90.0;
	spinRandomMax =  90.0;
	useInvAlpha   = false;

	colors[0]	= "1   1   0.3 0.0";
	colors[1]	= "1   1   0.3 1.0";
	colors[2]	= "0.6 0.0 0.0 0.0";

	sizes[0]	= 0.0;
	sizes[1]	= 2.0;
	sizes[2]	= 1.0;

	times[0]	= 0.0;
	times[1]	= 0.2;
	times[2]	= 1.0;
};

datablock ParticleEmitterData(racingrideonBurnEmitter)
{
   ejectionPeriodMS = 14;
   periodVarianceMS = 4;
   ejectionVelocity = 0;
   ejectionOffset   = 1.00;
   velocityVariance = 0.0;
   thetaMin         = 30;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;

   //lifetimeMS = 5000;
   particles = racingrideonBurnParticle;   

   uiName = "racingrideon Fire";
};


datablock ParticleData(racingrideontireDebrisTrailParticle)
{
	dragCoefficient		= 3.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= -0.5;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 500;
	lifetimeVarianceMS	= 150;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "base/data/particles/cloud";
	//animTexName		= "~/data/particles/cloud";

	// Interpolation variables
	colors[0]	= "0.0 0.0 0.0 0.0";
	colors[1]	= "0.0 0.0 0.0 0.250";
   colors[2]	= "0.0 0.0 0.0 0.0";

	sizes[0]	= 1.50;
	sizes[1]	= 2.50;
   sizes[2]	= 3.50;

	times[0]	= 0.0;
	times[1]	= 0.1;
   times[2]	= 1.0;
};

datablock ParticleEmitterData(racingrideontireDebrisTrailEmitter)
{
   ejectionPeriodMS = 90;
   periodVarianceMS = 0;
   ejectionVelocity = 0.0;
   velocityVariance = 0.0;
   ejectionOffset   = 1.0;
   thetaMin         = 0;
   thetaMax         = 0;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "racingrideontireDebrisTrailParticle";
};

datablock DebrisData(racingrideontireDebris)
{
   emitters = "racingrideontireDebrisTrailEmitter";

	shapeFile = "./shapes/jeeptire.dts";
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

datablock ParticleData(racingrideonExplosionParticle)
{
	dragCoefficient		= 3.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= -0.5;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 1900;
	lifetimeVarianceMS	= 300;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "base/data/particles/cloud";
	//animTexName		= "~/data/particles/cloud";

	// Interpolation variables
	colors[0]	= "0.9 0.3 0.2 0.9";
	colors[1]	= "0.0 0.0 0.0 0.0";
	sizes[0]	= 4.0;
	sizes[1]	= 10.0;
	times[0]	= 0.0;
	times[1]	= 1.0;
};

datablock ParticleEmitterData(racingrideonExplosionEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   lifeTimeMS	   = 21;
   ejectionVelocity = 8;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "racingrideonExplosionParticle";

   uiName = "racingrideon Explosion";
   emitterNode = TenthEmitterNode;
};

datablock ParticleData(racingrideonExplosionParticle2)
{
	dragCoefficient		= 0.1;
	windCoefficient		= 0.0;
	gravityCoefficient	= 2.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 1000;
	lifetimeVarianceMS	= 500;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "base/data/particles/chunk";
	//animTexName		= "~/data/particles/cloud";

	// Interpolation variables
	colors[0]	= "1.0 1.0 0.0 1.0";
	colors[1]	= "1.0 0.0 0.0 0.0";
	sizes[0]	= 0.5;
	sizes[1]	= 0.5;
	times[0]	= 0.0;
	times[1]	= 1.0;
};

datablock ParticleEmitterData(racingrideonExplosionEmitter2)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   lifetimeMS       = 7;
   ejectionVelocity = 15;
   velocityVariance = 5.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "racingrideonExplosionParticle2";

   uiName = "racingrideon Explosion 2";
   emitterNode = TenthEmitterNode;
};


datablock ParticleData(racingrideonExplosionParticle3)
{
	dragCoefficient		= 0.1;
	windCoefficient		= 0.0;
	gravityCoefficient	= 2.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 1000;
	lifetimeVarianceMS	= 500;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "base/data/particles/star1";
	//animTexName		= "~/data/particles/cloud";

	// Interpolation variables
	colors[0]	= "1.0 1.0 0.0 1.0";
	colors[1]	= "1.0 0.0 0.0 0.0";
	sizes[0]	= 20.0;
	sizes[1]	= 1.0;
	times[0]	= 0.0;
	times[1]	= 1.0;
};

datablock ParticleEmitterData(racingrideonExplosionEmitter3)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   lifetimeMS       = 50;
   ejectionVelocity = 15;
   velocityVariance = 5.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "racingrideonExplosionParticle3";

   uiName = "racingrideon Explosion 3";
   emitterNode = FourtiethEmitterNode;
};

datablock ExplosionData(racingrideonExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 150;

   soundProfile = racingrideonExplosionSound;

   emitter[0] = racingrideonExplosionEmitter;
   emitter[1] = racingrideonExplosionEmitter2;
   //particleDensity = 30;
   //particleRadius = 1.0;

   debris = racingrideontireDebris;
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

datablock ParticleData(racingrideonDebrisTrailParticle)
{
	dragCoefficient		= 3.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= -0.5;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 600;
	lifetimeVarianceMS	= 150;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "base/data/particles/cloud";
	//animTexName		= "~/data/particles/cloud";

	// Interpolation variables
	colors[0]	= "0.0 0.0 0.0 0.5";
	colors[1]	= "0.0 0.0 0.0 1.0";
   colors[2]	= "0.0 0.0 0.0 0.0";

	sizes[0]	= 2.0;
	sizes[1]	= 5.0;
   sizes[2]	= 5.0;

	times[0]	= 0.0;
	times[1]	= 0.1;
   times[2]	= 1.0;
};

datablock ParticleEmitterData(racingrideonDebrisTrailEmitter)
{
   ejectionPeriodMS = 15;
   periodVarianceMS = 0;
   ejectionVelocity = 8;
   velocityVariance = 1.0;
   ejectionOffset   = 1.0;
   thetaMin         = 40;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "racingrideonDebrisTrailParticle";

   uiName = "racingrideon Debris Trail";
   emitterNode = FifthEmitterNode;
};


datablock ParticleData(racingrideonFinalExplosionParticle)
{
	dragCoefficient		= 1.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= -0.5;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 1900;
	lifetimeVarianceMS	= 1000;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "base/data/particles/cloud";
	//animTexName		= "~/data/particles/cloud";

	// Interpolation variables
	colors[0]	= "0.0 0.0 0.0 0.5";
	colors[1]	= "0.0 0.0 0.0 1.0";
   colors[2]	= "0.0 0.0 0.0 0.0";

	sizes[0]	= 5.0;
	sizes[1]	= 10.0;
   sizes[2]	= 5.0;

	times[0]	= 0.0;
	times[1]	= 0.1;
   times[2]	= 1.0;
};

datablock ParticleEmitterData(racingrideonFinalExplosionEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   lifeTimeMS	   = 21;
   ejectionVelocity = 8;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 10;
   thetaMax         = 40;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "racingrideonFinalExplosionParticle";

   uiName = "racingrideon Final Explosion";
   emitterNode = TwentiethEmitterNode;
};

datablock ParticleData(racingrideonFinalExplosionParticle2)
{
	dragCoefficient		= 3.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 1000;
	lifetimeVarianceMS	= 500;
	spinSpeed		= 10.0;
	spinRandomMin		= -500.0;
	spinRandomMax		= 500.0;
	useInvAlpha		= false;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "base/data/particles/cloud";
	//animTexName		= "~/data/particles/cloud";

	// Interpolation variables
	colors[0]	= "1.0 0.5 0.0 1.0";
	colors[1]	= "1.0 0.0 0.0 0.0";
	sizes[0]	= 1.5;
	sizes[1]	= 2.5;
	times[0]	= 0.0;
	times[1]	= 1.0;
};

datablock ParticleEmitterData(racingrideonFinalExplosionEmitter2)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   lifetimeMS       = 15;
   ejectionVelocity = 30;
   velocityVariance = 5.0;
   ejectionOffset   = 0.0;
   thetaMin         = 85;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "racingrideonFinalExplosionParticle2";

   uiName = "racingrideon Final Explosion 2";
   emitterNode = TenthEmitterNode;
};



datablock ParticleData(racingrideonFinalExplosionParticle3)
{
	dragCoefficient		= 13.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 100;
	lifetimeVarianceMS	= 50;
	spinSpeed		= 10.0;
	spinRandomMin		= -500.0;
	spinRandomMax		= 500.0;
	useInvAlpha		= false;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "base/data/particles/star1";
	//animTexName		= "~/data/particles/cloud";

	// Interpolation variables
	colors[0]	= "1.0 0.5 0.0 1.0";
	colors[1]	= "1.0 0.0 0.0 0.0";
	sizes[0]	= 15;
	sizes[1]	= 0.5;

	times[0]	= 0.0;
	times[1]	= 1.0;
};

datablock ParticleEmitterData(racingrideonFinalExplosionEmitter3)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   lifetimeMS       = 15;
   ejectionVelocity = 30;
   velocityVariance = 5.0;
   ejectionOffset   = 0.0;
   thetaMin         = 85;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "racingrideonFinalExplosionParticle3";   

   uiName = "racingrideon Final Explosion 3";
   emitterNode = TenthEmitterNode;
};


datablock WheeledVehicleTire(racingrideontire)
{
   // Tires act as springs and generate lateral and longitudinal
   // forces to move the vehicle. These distortion/spring forces
   // are what convert wheel angular velocity into forces that
   // act on the rigid body.
   shapeFile = "./shapes/jeeptire.dts";
	
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

datablock WheeledVehicleSpring(racingrideonSpring)
{
   // Wheel suspension properties
   length = 0.4;			 // Suspension travel
   force = 6000; //3000;		 // Spring force
   damping = 800; //600;		 // Spring damping
   antiSwayForce = 6; //3;		 // Lateral anti-sway force
};


//////////////////////////////////////
// Initial Explosion (lose wheels)  //
//////////////////////////////////////
AddDamageType("racingrideonExplosion",   '<bitmap:add-ons/ci/carExplosion> %1',    '%2 <bitmap:add-ons/ci/carExplosion> %1');
datablock ProjectileData(racingrideonExplosionProjectile)
{
   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 0;
   explosion           = racingrideonExplosion;

   directDamageType  = $DamageType::racingrideonExplosion;
   radiusDamageType  = $DamageType::racingrideonExplosion;

   explodeOnDeath		= 1;

   armingDelay         = 0;
   lifetime            = 10;
};

//////////////////////////////////////
//////////////////////////////////////

//////////////////////////////////////
// Final Explosion                  //
//////////////////////////////////////



datablock DebrisData(racingrideonDebris)
{
   emitters = "racingrideonDebrisTrailEmitter";

	shapeFile = "./shapes/racingrideon.dts";
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







datablock ExplosionData(racingrideonFinalExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 150;

   soundProfile = racingrideonExplosionSound;
   
   emitter[0] = racingrideonFinalExplosionEmitter3;
   emitter[1] = racingrideonFinalExplosionEmitter2;

   particleEmitter = racingrideonFinalExplosionEmitter;
   particleDensity = 20;
   particleRadius = 1.0;

   debris = racingrideonDebris;
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

datablock ProjectileData(racingrideonFinalExplosionProjectile)
{
   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 0;
   explosion           = racingrideonFinalExplosion;

   directDamageType  = $DamageType::racingrideonExplosion;
   radiusDamageType  = $DamageType::racingrideonExplosion;

   explodeOnDeath		= 1;

   armingDelay         = 0;
   lifetime            = 10;
};

//////////////////////////////////////
//////////////////////////////////////