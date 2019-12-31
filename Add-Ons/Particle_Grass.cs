//Particle_Grass.cs
//Creates 200 stationary particles

datablock ParticleData(GrassParticle)
{
   textureName          = "base/data/foliage/bearGrass";
   dragCoefficient      = 10.0;
   windCoefficient      = 0.0;
   gravityCoefficient   = 0.0; 
   inheritedVelFactor   = 0.0;
   lifetimeMS           = 30000;
   lifetimeVarianceMS   = 0;
   useInvAlpha = true;
   spinRandomMin = 0.0;
   spinRandomMax = 0.0;

   colors[0]     = "1 1 1 0";
   colors[1]     = "0 1 0 1";
   colors[2]     = "0 1 0 1";
   colors[3]     = "1 1 1 0.0";

   sizes[0]      = 2.0;
   sizes[1]      = 2.0;
   sizes[2]      = 2.0;
   sizes[3]      = 2.0;

   times[0]      = 0.0;
   times[1]      = 0.1;
   times[2]      = 0.9;
   times[3]      = 1.0;
};

datablock ParticleEmitterData(GrassEmitter)
{
   ejectionPeriodMS = 150;
   periodVarianceMS = 0;

   ejectionOffset = 0.9;
   ejectionOffsetVariance = 0.0;
	
   ejectionVelocity = 0.0;
   velocityVariance = 0.0;

   thetaMin         = 0.0;
   thetaMax         = 0.0;  

   phiReferenceVel  = 0;
   phiVariance      = 360;

   overrideAdvances = false;
   orientParticles  = false;
   orientOnVelocity = false;

	usePlacementForVelocity = 0;

   particles = GrassParticle;

   uiName = "Grass ";
};

