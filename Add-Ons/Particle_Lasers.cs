//basic particle emitters

datablock ParticleData(LaserParticle1)
{
	textureName          = "base/data/particles/dot";
	dragCoefficient      = 0.0;
	gravityCoefficient   = 0.0;
	inheritedVelFactor   = 0.0;
	windCoefficient      = 0;
	constantAcceleration = 0;
	lifetimeMS           = 1100;
	lifetimeVarianceMS   = 300;
	spinSpeed     = 0;
	spinRandomMin = -90.0;
	spinRandomMax =  90.0;
	useInvAlpha   = false;

	colors[0]	= "0.5 0.5 0.0 1.0";
	colors[1]	= "1.0 0.0 0.0 1.0";
	colors[2]	= "1.0 0.0 0.0 0.0";

	sizes[0]	= 0.1;
	sizes[1]	= 0.1;
	sizes[2]	= 0.1;

	times[0]	= 0.0;
	times[1]	= 0.9;
	times[2]	= 1.0;
};

datablock ParticleEmitterData(LaserEmitter1)
{
   ejectionPeriodMS = 8;
   periodVarianceMS = 0;
   ejectionVelocity = 6.0;
   ejectionOffset   = 0.0;
   velocityVariance = 0.0;
   thetaMin         = 0;
   thetaMax         = 0;
   phiReferenceVel  = 0;
   phiVariance      = 0;
   overrideAdvance = false;

   //lifetimeMS = 5000;
   particles = LaserParticle1;   

	uiName = "Laser Fire";
};




datablock ParticleData(LaserParticleB)
{
	textureName          = "base/data/particles/dot";
	dragCoefficient      = 0.0;
	gravityCoefficient   = 0.0;
	inheritedVelFactor   = 0.0;
	windCoefficient      = 0;
	constantAcceleration = 0;
	lifetimeMS           = 1100;
	lifetimeVarianceMS   = 300;
	spinSpeed     = 0;
	spinRandomMin = -90.0;
	spinRandomMax =  90.0;
	useInvAlpha   = false;

	colors[0]	= "0 0 1 0.1";
	colors[1]	= "0 0 1 0.1";
	colors[2]	= "0 0 1 0.1";

	sizes[0]	= 0.1;
	sizes[1]	= 0.1;
	sizes[2]	= 0.1;

	times[0]	= 0.0;
	times[1]	= 0.9;
	times[2]	= 1.0;
};

datablock ParticleEmitterData(LaserEmitterB)
{
    ejectionPeriodMS = 8;
   periodVarianceMS = 0;
   ejectionVelocity = 6.0;
   ejectionOffset   = 0.0;
   velocityVariance = 0.0;
   thetaMin         = 0;
   thetaMax         = 0;
   phiReferenceVel  = 0;
   phiVariance      = 0;
   overrideAdvance = false;

   //lifetimeMS = 5000;
   particles = LaserParticleB;   
 

	uiName = "Laser Trans Blue";
};


datablock ParticleData(LaserParticleC)
{
	textureName          = "base/data/particles/dot";
	dragCoefficient      = 0.0;
	gravityCoefficient   = 0.0;
	inheritedVelFactor   = 0.0;
	windCoefficient      = 0;
	constantAcceleration = 0;
	lifetimeMS           = 1100;
	lifetimeVarianceMS   = 300;
	spinSpeed     = 0;
	spinRandomMin = -90.0;
	spinRandomMax =  90.0;
	useInvAlpha   = false;

	colors[0]     = "1 1 1 1";
	colors[1]     = "1 1 1 1";
	colors[2]     = "1 1 1 1";
	colors[3]     = "1 1 1 1";

	sizes[0]	= 0.01;
	sizes[1]	= 0.01;
	sizes[2]	= 0.01;
	sizes[3]	= 0.01;

	times[0]	= 0.0;
	times[1]	= 0.9;
	times[2]	= 1.0;
};

datablock ParticleEmitterData(LaserEmitterC)
{
   ejectionPeriodMS = 8;
   periodVarianceMS = 0;
   ejectionVelocity = 6.0;
   ejectionOffset   = 0.0;
   velocityVariance = 0.0;
   thetaMin         = 0;
   thetaMax         = 0;
   phiReferenceVel  = 0;
   phiVariance      = 0;
   overrideAdvance = false;

   //lifetimeMS = 5000;
   particles = LaserParticleC;   

	uiName = "Laser White Tiny";
};


datablock ParticleData(LaserParticleD)
{
   textureName          = "base/data/particles/dot";
	dragCoefficient      = 0.0;
	gravityCoefficient   = 0.0;
	inheritedVelFactor   = 0.0;
	windCoefficient      = 0;
	constantAcceleration = 0;
	lifetimeMS           = 1100;
	lifetimeVarianceMS   = 300;
	spinSpeed     = 0;
	spinRandomMin = -90.0;
	spinRandomMax =  90.0;
	useInvAlpha   = false;

   colors[0]     = "1 0 0 1";
   colors[1]     = "0 1 0 1";
   colors[2]     = "0 0 1 1";

   sizes[0]      = 1;
   sizes[1]      = 1;
   sizes[2]      = 1;

   times[0]      = 0.0;
   times[1]      = 0.9;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(LaserEmitterD)
{
   ejectionPeriodMS = 50;
   periodVarianceMS = 4;
   ejectionVelocity = 0.9;
   ejectionOffset   = 0.4;
   velocityVariance = 0.0;
   thetaMin         = 0;
   thetaMax         = 5;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;

   //lifetimeMS = 5000;
   particles = LaserParticleD;   

	uiName = "Laser RBG Explosion";
};


datablock ParticleData(LaserParticleE)
{
   textureName          = "base/data/particles/dot";
	dragCoefficient      = 0.0;
	gravityCoefficient   = 0.0;
	inheritedVelFactor   = 0.0;
	windCoefficient      = 0;
	constantAcceleration = 0;
	lifetimeMS           = 1100;
	lifetimeVarianceMS   = 300;
	spinSpeed     = 0;
	spinRandomMin = -90.0;
	spinRandomMax =  90.0;
	useInvAlpha   = false;

   colors[0]     = "1 0 0 1";
   colors[1]     = "0 1 0 1";
   colors[2]     = "0 0 1 1";

   sizes[0]      = 0.1;
   sizes[1]      = 0.1;
   sizes[2]      = 0.1;

   times[0]      = 0.0;
   times[1]      = 0.9;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(LaserEmitterE)
{
   ejectionPeriodMS = 8;
   periodVarianceMS = 0;
   ejectionVelocity = 6.0;
   ejectionOffset   = 0.0;
   velocityVariance = 0.0;
   thetaMin         = 0;
   thetaMax         = 0;
   phiReferenceVel  = 0;
   phiVariance      = 0;
   overrideAdvance = false;
   //lifetimeMS = 5000;
   particles = LaserParticleE;   

	uiName = "Laser RBG";
};


datablock ParticleData(LaserParticleF)
{
   textureName          = "base/data/particles/dot";
	dragCoefficient      = 0.0;
	gravityCoefficient   = 0.0;
	inheritedVelFactor   = 0.0;
	windCoefficient      = 0;
	constantAcceleration = 0;
	lifetimeMS           = 1100;
	lifetimeVarianceMS   = 300;
	spinSpeed     = 0;
	spinRandomMin = -90.0;
	spinRandomMax =  90.0;
	useInvAlpha   = false;

   colors[0]     = "1 1 0 1";
   colors[1]     = "0 1 1 1";
   colors[2]     = "1 0 1 1";

   sizes[0]      = 1;
   sizes[1]      = 1;
   sizes[2]      = 1;

   times[0]      = 0.0;
   times[1]      = 0.9;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(LaserEmitterF)
{
   ejectionPeriodMS = 50;
   periodVarianceMS = 4;
   ejectionVelocity = 0.9;
   ejectionOffset   = 0.4;
   velocityVariance = 0.0;
   thetaMin         = 0;
   thetaMax         = 5;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;

   //lifetimeMS = 5000;
   particles = LaserParticleF;   

	uiName = "Laser YCP Explosion";
};


datablock ParticleData(LaserParticleG)
{
   textureName          = "base/data/particles/dot";
	dragCoefficient      = 0.0;
	gravityCoefficient   = 0.0;
	inheritedVelFactor   = 0.0;
	windCoefficient      = 0;
	constantAcceleration = 0;
	lifetimeMS           = 1100;
	lifetimeVarianceMS   = 300;
	spinSpeed     = 0;
	spinRandomMin = -90.0;
	spinRandomMax =  90.0;
	useInvAlpha   = false;

   colors[0]     = "1 1 0 1";
   colors[1]     = "0 1 1 1";
   colors[2]     = "1 0 1 1";

   sizes[0]      = 0.1;
   sizes[1]      = 0.1;
   sizes[2]      = 0.1;

   times[0]      = 0.0;
   times[1]      = 0.9;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(LaserEmitterG)
{
   ejectionPeriodMS = 8;
   periodVarianceMS = 0;
   ejectionVelocity = 6.0;
   ejectionOffset   = 0.0;
   velocityVariance = 0.0;
   thetaMin         = 0;
   thetaMax         = 0;
   phiReferenceVel  = 0;
   phiVariance      = 0;
   overrideAdvance = false;

   //lifetimeMS = 5000;
   particles = LaserParticleG;   

	uiName = "Laser YCP";
};


datablock ParticleData(LaserParticleH)
{
   textureName          = "base/data/particles/dot";
	dragCoefficient      = 0.0;
	gravityCoefficient   = 0.0;
	inheritedVelFactor   = 0.0;
	windCoefficient      = 0;
	constantAcceleration = 0;
	lifetimeMS           = 1100;
	lifetimeVarianceMS   = 300;
	spinSpeed     = 0;
	spinRandomMin = -90.0;
	spinRandomMax =  90.0;
	useInvAlpha   = false;

   colors[0]     = "0.5 0.5 0.5 1";
   colors[1]     = "0.5 0.5 0.5 1";
   colors[2]     = "0.5 0.5 0.5 1";

   sizes[0]      = 0.1;
   sizes[1]      = 0.1;
   sizes[2]      = 100;

   times[0]      = 0.0;
   times[1]      = 0.9;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(LaserEmitterH)
{
   ejectionPeriodMS = 8;
   periodVarianceMS = 0;
   ejectionVelocity = 6.0;
   ejectionOffset   = 0.0;
   velocityVariance = 0.0;
   thetaMin         = 0;
   thetaMax         = 0;
   phiReferenceVel  = 0;
   phiVariance      = 0;
   overrideAdvance = false;

   //lifetimeMS = 5000;
   particles = LaserParticleH;   

	uiName = "Laser Explosion";
};


datablock ParticleData(LaserParticleI)
{
   textureName          = "base/data/particles/dot";
	dragCoefficient      = 0.0;
	gravityCoefficient   = 0.0;
	inheritedVelFactor   = 0.0;
	windCoefficient      = 0;
	constantAcceleration = 0;
	lifetimeMS           = 1100;
	lifetimeVarianceMS   = 300;
	spinSpeed     = 0;
	spinRandomMin = -90.0;
	spinRandomMax =  90.0;
	useInvAlpha   = false;

   colors[0]     = "0 0 0 1";
   colors[1]     = "0.5 0.5 0.5 1";
   colors[2]     = "1 1 1 1";

   sizes[0]      = 0.1;
   sizes[1]      = 0.1;
   sizes[2]      = 0.1;

   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(LaserEmitterI)
{
   ejectionPeriodMS = 8;
   periodVarianceMS = 0;
   ejectionVelocity = 6.0;
   ejectionOffset   = 0.0;
   velocityVariance = 0.0;
   thetaMin         = 0;
   thetaMax         = 0;
   phiReferenceVel  = 0;
   phiVariance      = 0;
   overrideAdvance = false;

   //lifetimeMS = 5000;
   particles = LaserParticleI;   

	uiName = "Laser Greyscale";
};


datablock ParticleData(LaserParticleJ)
{
   textureName          = "base/data/particles/dot";
	dragCoefficient      = 0.0;
	gravityCoefficient   = 0.0;
	inheritedVelFactor   = 0.0;
	windCoefficient      = 0;
	constantAcceleration = 0;
	lifetimeMS           = 1100;
	lifetimeVarianceMS   = 300;
	spinSpeed     = 0;
	spinRandomMin = -90.0;
	spinRandomMax =  90.0;
	useInvAlpha   = false;

   colors[0]     = "1 0 0 0.8";
   colors[1]     = "1 0.5 0 0.8";
   colors[2]     = "1 1 0 0.8";
   colors[3]     = "0 1 0 0.8";
   colors[4]     = "0 1 1 0.8";
   colors[5]     = "0 0 1 0.8";
   colors[6]     = "1 0 1 0.8";

   sizes[0]      = 0.1;
   sizes[1]      = 0.1;
   sizes[2]      = 0.1;

   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(LaserEmitterJ)
{
   ejectionPeriodMS = 8;
   periodVarianceMS = 0;
   ejectionVelocity = 6.0;
   ejectionOffset   = 0.0;
   velocityVariance = 0.0;
   thetaMin         = 0;
   thetaMax         = 0;
   phiReferenceVel  = 0;
   phiVariance      = 0;
   overrideAdvance = false;

   //lifetimeMS = 5000;
   particles = LaserParticleJ;   

	uiName = "Laser Red~Green";
};