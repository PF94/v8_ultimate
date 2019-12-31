//-------Monkey---------------------------------------------------------------------------------------------------------------------------------
datablock ParticleData(MonkeySkin)
{
	textureName = "Add-Ons/Shapes/Monkey/base.blank.jpg";
};

datablock ParticleData(MonkeyHand)
{
	textureName = "Add-Ons/Shapes/Monkey/yellow.blank.jpg";
};

datablock ShapeBaseImageData(MonkeyImage)
{
	shapeFile = "Add-ons/shapes/Monkey/monkey.dts";
	emap = false;
	mountPoint = $BackSlot;
	offset = "0 -0.2 -1.6";
	rotation = "0 0 1 180";
	className = "ItemImage";
};

//---------------Skeleton---------------------------------------------------------------------------------------------------------------------------
datablock ParticleData(SkeletonSkin)
{
	textureName = "Add-Ons/Shapes/Skeleton/base.blank.jpg";
};

datablock ParticleData(SkeletonFace)
{
	textureName = "Add-Ons/Shapes/Skeleton/skelface.png";
};

datablock ShapeBaseImageData(skelbodyImage)
{
	shapeFile = "Add-ons/shapes/Skeleton/skelbody.dts";
	emap = false;
	mountPoint = 2;
	offset = "0 .02 -1.18";
};

datablock ShapeBaseImageData(skelheadImage)
{
	shapeFile = "Add-ons/shapes/Skeleton/skelhead.dts";
	emap = false;
	mountPoint = 5;
	offset = "0 0 -.4";
};

//--Head Explosion------------------------------------------------------------------------
datablock AudioProfile(HeadExplosionSound)
{
   filename    = "./sound/jeepExplosion.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock ParticleData(HeadBurnParticle)
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

datablock ParticleEmitterData(HeadBurnEmitter)
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
   particles = HeadBurnParticle;   
};

datablock ParticleData(HeadExplosionParticle)
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
	textureName		= "base/data/particles/cloud";
	colors[0]	= "0.9 0.3 0.2 0.9";
	colors[1]	= "0.0 0.0 0.0 0.0";
	sizes[0]	= 4.0;
	sizes[1]	= 10.0;
	times[0]	= 0.0;
	times[1]	= 1.0;
};

datablock ParticleEmitterData(HeadExplosionEmitter)
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
   particles = "HeadExplosionParticle";
   emitterNode = TenthEmitterNode;
};

datablock ParticleData(HeadDebrisTrailParticle)
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
	textureName		= "base/data/particles/cloud";
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

datablock ParticleEmitterData(HeadDebrisTrailEmitter)
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
   particles = "HeadDebrisTrailParticle";
   emitterNode = FifthEmitterNode;
};

datablock ParticleData(HeadFinalExplosionParticle)
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
	textureName		= "base/data/particles/cloud";
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

datablock ParticleEmitterData(HeadFinalExplosionEmitter)
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
   particles = "HeadFinalExplosionParticle";
   emitterNode = TwentiethEmitterNode;
};

datablock ParticleData(HeadSmokeParticle)
{
	dragCoefficient      = 3;
	gravityCoefficient   = -0.5;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.0;
	lifetimeMS           = 525;
	lifetimeVarianceMS   = 55;
	textureName          = "base/data/particles/cloud";
	spinSpeed		= 10.0;
	spinRandomMin		= -500.0;
	spinRandomMax		= 500.0;
	colors[0]     = "0.5 0.5 0.5 0.9";
	colors[1]     = "0.5 0.5 0.5 0.0";
	sizes[0]      = 0.15;
	sizes[1]      = 0.15;
	useInvAlpha = false;
};

datablock ParticleEmitterData(HeadSmokeEmitter)
{
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;
   ejectionVelocity = 1.0;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "HeadSmokeParticle";
};

datablock ShapeBaseImageData(HeadExplosionImage)
{
	shapeFile = "base/data/shapes/empty.dts";
	emap = false;
	mountPoint = $HeadSlot;
  	rotation = "1 0 0 -90";

	stateName[0]			= "Ready";
	stateTransitionOnTimeout[0]	= "FireA";
	stateTimeoutValue[0]		= 0.01;

	stateName[1]			= "FireA";
	stateTransitionOnTimeout[1]	= "FireB";
	stateWaitForTimeout[1]		= false;
	stateTimeoutValue[1]		= 0.1;
	stateEmitter[1]			= HeadBurnEmitter;
	stateEmitterTime[1]		= 4;

	stateName[2]			= "FireB";
	stateTransitionOnTimeout[2]	= "FireC";
	stateWaitForTimeout[2]		= false;
	stateTimeoutValue[2]		= 0.1;
	stateSound[2]			= HeadExplosionSound;
	stateEmitter[2]			= HeadExplosionEmitter;
	stateEmitterTime[2]		= 3;

	stateName[3]			= "FireC";
	stateTransitionOnTimeout[3]	= "FireD";
	stateWaitForTimeout[3]		= false;
	stateTimeoutValue[3]		= 0.1;
	stateEmitter[3]			= HeadDebrisTrailEmitter;
	stateEmitterTime[3]		= 3;

	stateName[4]			= "FireD";
	stateTransitionOnTimeout[4]	= "FireE";
	stateWaitForTimeout[4]		= false;
	stateTimeoutValue[4]		= 0.1;
	stateEmitter[4]			= HeadFinalExplosionEmitter;
	stateEmitterTime[4]		= 1;


	stateName[5]			= "FireE";
	stateTransitionOnTimeout[5]	= "Done";
	stateWaitForTimeout[5]		= false;
	stateTimeoutValue[5]		= 4.1;
	stateEmitter[5]			= HeadSmokeEmitter;
	stateEmitterTime[5]		= 4;

	stateName[6]			="Done";
	stateScript[6]			="onDone";
};

function HeadExplosionImage::onDone(%this,%obj,%slot)
{
	%obj.unMountImage(%slot);
}

//----Ninja Poof----------------------------------------------------------------------------------------------------------------------------------
datablock ParticleData(NinjaPoofParticle)
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
	textureName		= "base/data/particles/cloud";
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

datablock ParticleEmitterData(NinjaPoofEmitter)
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
   particles = "NinjaPoofParticle";
};

datablock ShapeBaseImageData(NinjaPoofImage)
{
	shapeFile = "base/data/shapes/empty.dts";
	emap = false;
	mountPoint = $Hip;
  	rotation = "1 0 0 -90";

	stateName[0]			= "Ready";
	stateTransitionOnTimeout[0]	= "FireA";
	stateTimeoutValue[0]		= 0.01;

	stateName[1]			= "FireA";
	stateTransitionOnTimeout[1]	= "Done";
	stateWaitForTimeout[1]		= True;
	stateTimeoutValue[1]		= 1.1;
	stateEmitter[1]			= NinjaPoofEmitter;
	stateEmitterTime[1]		= 1;

	stateName[2]			= "Done";
	stateScript[2]			= "onDone";
};

function NinjaPoofImage::onDone(%this,%obj,%slot)
{
	%obj.unMountImage(%slot);
}