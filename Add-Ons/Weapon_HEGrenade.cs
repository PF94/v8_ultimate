datablock AudioProfile(hegrenadeExplosionSound)
{
   filename    = "./sound/jeepExplosion.wav";
   description = AudioDefault3d;
   preload = false;
};

datablock AudioProfile(hegrenadeBounceSound)
{
   filename    = "./sound/hegrenadeBounce.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(hegrenadeFireSound)
{
   filename    = "./sound/hegrenadeFire.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock DebrisData(hegrenadePinDebris)
{
	shapeFile = "./shapes/hegrenadePin.dts";
	lifetime = 5.0;
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

datablock ParticleData(hegrenadeExplosionParticle)
{
	dragCoefficient		= 1.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= -0.2;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 4000;
	lifetimeVarianceMS	= 3990;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "base/data/particles/cloud";
	//animTexName		= "~/data/particles/cloud";

	// Interpolation variables
	colors[0]	= "0.2 0.2 0.2 1.0";
	colors[1]	= "0.25 0.25 0.25 0.2";
   colors[2]	= "0.4 0.4 0.4 0.0";

	sizes[0]	= 5.0;
	sizes[1]	= 10.0;
   sizes[2]	= 5.0;

	times[0]	= 0.0;
	times[1]	= 0.1;
   times[2]	= 1.0;
};

datablock ParticleEmitterData(hegrenadeExplosionEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   lifeTimeMS	   = 21;
   ejectionVelocity = 10;
   velocityVariance = 1.0;
   ejectionOffset   = 1.0;
   thetaMin         = 0;
   thetaMax         = 0;
   phiReferenceVel  = 0;
   phiVariance      = 90;
   overrideAdvance = false;
   particles = "hegrenadeExplosionParticle";
};

datablock ParticleData(hegrenadeExplosionParticle2)
{
	dragCoefficient		= 1.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= -0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 4000;
	lifetimeVarianceMS	= 3990;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "base/data/particles/cloud";
	//animTexName		= "~/data/particles/cloud";

	// Interpolation variables
	colors[0]	= "0.2 0.2 0.2 0.0";
	colors[1]	= "0.25 0.25 0.25 0.2";
   colors[2]	= "0.4 0.4 0.4 0.0";

	sizes[0]	= 5.0;
	sizes[1]	= 10.0;
   sizes[2]	= 5.0;

	times[0]	= 0.0;
	times[1]	= 0.1;
   times[2]	= 1.0;
};

datablock ParticleEmitterData(hegrenadeExplosionEmitter2)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   lifetimeMS       = 120;
   ejectionVelocity = 15;
   velocityVariance = 5.0;
   ejectionOffset   = 0.0;
   thetaMin         = 85;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "hegrenadeExplosionParticle2";
};



datablock ParticleData(hegrenadeExplosionParticle3)
{
	dragCoefficient		= 1.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= -0.2;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 4000;
	lifetimeVarianceMS	= 3990;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "base/data/particles/cloud";
	//animTexName		= "~/data/particles/cloud";

	// Interpolation variables
	colors[0]	= "0.2 0.2 0.2 0.0";
	colors[1]	= "0.25 0.25 0.25 0.2";
   colors[2]	= "0.4 0.4 0.4 0.0";

	sizes[0]	= 5.0;
	sizes[1]	= 10.0;
   sizes[2]	= 5.0;

	times[0]	= 0.0;
	times[1]	= 0.1;
   times[2]	= 1.0;
};

datablock ParticleEmitterData(hegrenadeExplosionEmitter3)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   lifetimeMS       = 150;
   ejectionVelocity = 10;
   velocityVariance = 5.0;
   ejectionOffset   = 0.0;
   thetaMin         = 85;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "hegrenadeExplosionParticle3";
};

datablock ExplosionData(hegrenadeExplosion)
{
   explosionShape = "./shapes/explosionSphere1.dts";
   lifeTimeMS = 150;

   soundProfile = hegrenadeExplosionSound;
   
   emitter[0] = hegrenadeExplosionEmitter3;
   emitter[1] = hegrenadeExplosionEmitter2;

   particleEmitter = hegrenadeExplosionEmitter;
   particleDensity = 20;
   particleRadius = 1.0;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = true;
   camShakeFreq = "7.0 8.0 7.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.5;
   camShakeRadius = 15.0;

   // Dynamic light
   lightStartRadius = 0;
   lightEndRadius = 0;
   lightStartColor = "0.45 0.3 0.1";
   lightEndColor = "0 0 0";

   //impulse
   impulseRadius = 20;
   impulseForce = 4000;

   //radius damage
   damageRadius = 17;
   radiusDamage = 250;

};

//projectile
AddDamageType("hegrenadeDirect",   '<bitmap:add-ons/ci/hegrenade> %1',    '%2 <bitmap:add-ons/ci/hegrenade> %1',1,1);
AddDamageType("hegrenadeRadius",   '<bitmap:add-ons/ci/hegrenade> %1',    '%2 <bitmap:add-ons/ci/hegrenade> %1',1,0);
datablock ProjectileData(hegrenadeProjectile)
{
   projectileShapeName = "./shapes/hegrenadeProjectile.dts";
   directDamage        = 0;
   directDamageType  = $DamageType::hegrenadeDirect;
   radiusDamageType  = $DamageType::hegrenadeRadius;
   impactImpulse	   = 200;
   verticalImpulse	   = 200;
   explosion           = hegrenadeExplosion;
   //particleEmitter     = hegrenadeTrailEmitter;

   brickExplosionRadius = 10;
   brickExplosionImpact = false; //destroy a brick if we hit it directly?
   brickExplosionForce  = 25;             
   brickExplosionMaxVolume = 100;          //max volume of bricks that we can destroy
   brickExplosionMaxVolumeFloating = 60;  //max volume of bricks that we can destroy if they aren't connected to the ground (should always be >= brickExplosionMaxVolume)

   muzzleVelocity      = 30;
   velInheritFactor    = 0;
   explodeOnDeath = true;

   armingDelay         = 2500; 
   lifetime            = 2500;
   fadeDelay           = 3000;
   bounceElasticity    = 0.4;
   bounceFriction      = 0.3;
   isBallistic         = true;
   gravityMod = 1.0;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";
};


//////////
// item //
//////////
datablock ItemData(hegrenadeItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./shapes/hegrenade.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "HE-Grenade";
	iconName = "./ItemIcons/HEGrenade";
	doColorShift = false;
	colorShiftColor = "0.400 0.196 0 1.000";

	 // Dynamic properties defined by the scripts
	image = hegrenadeImage;
	canDrop = true;
};

function hegrenadeItem::onPickup(%this,%obj,%user,%amount)
{
	return;
}

function hegrenadeItem::onCollision(%data,%obj,%col)
{
	%id = getBrickGroupFromObject(%obj).client.bl_id;
	%trust = getTrustLevel(getBrickGroupFromObject(%obj).client,%col.client);
		%cl = getBrickGroupFromObject(%obj).client;
	if(%obj.canpickup == 0){
				return;
			}
			if(%col.client.minigame == 0){
				%col.client.minigame = -1;

			}
			if(%cl.minigame == 0){
				%cl.minigame = -1;

			}
	if(%col.client.minigame != %cl.minigame)
	{
		if(%col.client.minigame == -1){
		centerprint(%col.client,"This item is part of a mini-game.",1,2);
		return;
		}
		else
		{
		centerprint(%col.client,"This item is not part of the mini-game.",1,2);
		return;
		}
	}
	if(%col.client.minigame == %cl.minigame && %cl.minigame != -1 && %cl.minigame.useallplayersbricks == 0 && %cl.minigame.owner.bl_id != %id)
	{
		centerprint(%col.client,"This item is not part of the mini-game.",1,2);
		return;
	}
	if(%trust >= $TrustLevel::Build || %cl== %col.client || %col.client.minigame == %cl.minigame && %cl.minigame != -1){
		//
	if(isobject(%obj.spawnbrick)){
		for(%i=0;%i<5;%i++)
		{
			%toolDB = %col.tool[%i];
			if(%obj.canpickup == 0){
				return;
			}
			if(%toolDB == 0)
			{
				%col.tool[%i] = %obj.getdatablock();
				%col.weaponCount++;
				messageClient(%col.client,'MsgItemPickup','',%i,%obj.getdatablock());
				%obj.canpickup = 0;
				%obj.fadeout();
				schedule(%obj.spawnbrick.itemrespawntime,0,heoverwrite,%obj);
				break;
			}
		}
	}
	else{
	for(%i=0;%i<5;%i++)
	{
		%toolDB = %col.tool[%i];
		if(%obj.canpickup == 0){
			return;
		}
		if(%toolDB == 0)
		{
			%col.tool[%i] = %obj.getdatablock();
			%col.weaponCount++;
			messageClient(%col.client,'MsgItemPickup','',%i,%obj.getdatablock());
			%obj.delete();
			break;
		}
	}
	}
	}
	else{
		
	centerprint(%col.client, ""@ %cl.name @" does not trust you enough to use this item.",1,2);

	}
	
}

function getclientfromBLID(%id)
{
 for(%i=0;%i<ClientGroup.getCount();%i++){if(%id = ClientGroup.getObject(%i).bl_id){return ClientGroup.getObject(%i);}}
 return -1;
}

function heoverwrite(%obj)
{
	if(isobject(%obj)){
	%obj.fadein();
	%obj.canpickup = 1;
	}

}


////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(hegrenadeImage)
{
   // Basic Item properties
   shapeFile = "./shapes/hegrenade.dts";
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
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = hegrenadeItem;
   ammo = " ";
   projectile = hegrenadeProjectile;
   projectileType = Projectile;

   	casing = hegrenadePinDebris;
	shellExitDir        = "-2.0 1.0 1.0";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 15.0;	
	shellVelocity       = 7.0;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;

   //casing = " ";
   doColorShift = false;
   colorShiftColor = "0.400 0.196 0 1.000";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]			= "Activate";
	stateTimeoutValue[0]		= 0.1;
	stateTransitionOnTimeout[0]	= "Ready";
	stateSequence[0]		= "ready";
	stateSound[0]					= weaponSwitchSound;

	stateName[1]			= "Ready";
	stateTransitionOnTriggerDown[1]	= "Pindrop";
	stateAllowImageChange[1]	= true;

	stateName[2]			= "Pindrop";
	stateTransitionOnTimeout[2]	= "Pinfallen";
	stateAllowImageChange[2]	= false;
	stateTimeoutValue[2]		= 0.2;
	stateSound[2]				= hegrenadeFireSound;
	stateSequence[2]                = "Pinpull";
	stateEjectShell[2]       = true;

	stateName[3]			= "Pinfallen";
	stateTransitionOnTriggerDown[3]	= "Charge";
	stateAllowImageChange[3]	= false;
	
	stateName[4]                    = "Charge";
	stateTransitionOnTimeout[4]	= "Armed";
	stateTimeoutValue[4]            = 0.7;
	stateWaitForTimeout[4]		= false;
	stateTransitionOnTriggerUp[4]	= "AbortCharge";
	stateScript[4]                  = "onCharge";
	stateAllowImageChange[4]        = false;
	
	stateName[5]			= "AbortCharge";
	stateTransitionOnTimeout[5]	= "Pinfallen";
	stateTimeoutValue[5]		= 0.3;
	stateWaitForTimeout[5]		= true;
	stateScript[5]			= "onAbortCharge";
	stateAllowImageChange[5]	= false;

	stateName[6]			= "Armed";
	stateTransitionOnTriggerUp[6]	= "Fire";
	stateAllowImageChange[6]	= false;

	stateName[7]			= "Fire";
	stateTransitionOnTimeout[7]	= "Done";
	stateTimeoutValue[7]		= 0.5;
	stateFire[7]			= true;
	stateSequence[7]		= "fire";
	stateScript[7]			= "onFire";
	stateWaitForTimeout[7]		= true;
	stateAllowImageChange[7]	= false;

	stateName[8]					= "Done";
	stateScript[8]					= "onDone";

};

function hegrenadeImage::onCharge(%this, %obj, %slot)
{
	%obj.playthread(2, spearReady);
}

function hegrenadeImage::onCharge(%this, %obj, %slot)
{
	%obj.playthread(2, spearReady);
}

function hegrenadeImage::onAbortCharge(%this, %obj, %slot)
{
	%obj.playthread(2, root);
}

function hegrenadeProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
		serverPlay3D(hegrenadeBounceSound,%obj.getTransform());
}

function hegrenadeImage::onFire(%this, %obj, %slot)
{
	%obj.playthread(2, spearThrow);
	Parent::OnFire(%this, %obj, %slot);

	for(%i=0;%i<5;%i++)
	{
		%toolDB = %obj.tool[%i];
		if(%toolDB $= %this.item.getID() && %i == %obj.currTool)
		{
			%obj.tool[%i] = 0;
			%obj.weaponCount--;
			messageClient(%obj.client,'MsgItemPickup','',%i,0);
			serverCmdUnUseTool(%obj.client);
			break;
		}
	}
}

function hegrenadeImage::onDone(%this,%obj,%slot)
{
	%obj.unMountImage(%slot);
}