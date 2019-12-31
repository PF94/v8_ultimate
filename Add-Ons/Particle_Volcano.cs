//particle, and partical emitters for trevs map!

datablock ParticleData(VolcanoSmokeParticle1)
{
	dragCoefficient		= 1.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= -0.5;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 13900;
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
	colors[0]	= "0.6 0.6 0.6 0.5";
	colors[1]	= "0.4 0.4 0.4 1.0";
   colors[2]	= "0.6 0.6 0.6 0.0";

	sizes[0]	= 5.0;
	sizes[1]	= 10.0;
   sizes[2]	= 5.0;

	times[0]	= 0.0;
	times[1]	= 0.1;
   times[2]	= 1.0;
};

datablock ParticleEmitterData(VolcanoSmokeEmitter1)
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
   particles = "VolcanoSmokeParticle1";

   uiName = "VolcanoSmokeEmitter1";
   emitterNode = TwentiethEmitterNode;
};