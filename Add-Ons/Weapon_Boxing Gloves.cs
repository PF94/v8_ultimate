//boxinggloves.cs
datablock AudioProfile(boxingglovesFireSound)
{
   filename    = "./sound/boxingglovesFire.ogg";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(boxingglovesBlockFireSound)
{
   filename    = "./sound/boxingglovesBlockFire.ogg";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(boxingglovesHitSound)
{
   filename    = "./sound/boxingglovesHit.ogg";
   description = AudioClosest3d;
   preload = true;
};


//effects
datablock ParticleData(boxingglovesExplosionParticle)
{
   dragCoefficient      = 2;
   gravityCoefficient   = 1.0;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   spinRandomMin = -90;
   spinRandomMax = 90;
   lifetimeMS           = 500;
   lifetimeVarianceMS   = 300;
   textureName          = "base/data/particles/cloud";
   colors[0]     = "0.7 0.7 0.9 0.2";
   colors[1]     = "0.9 0.9 0.9 0.1";
   sizes[0]      = 0.5;
   sizes[1]      = 0.25;
};

datablock ParticleEmitterData(boxingglovesExplosionEmitter)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   ejectionVelocity = 5;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 89;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "boxingglovesExplosionParticle";
};

datablock ExplosionData(boxingglovesExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 500;

   soundProfile = boxingglovesHitSound;

   particleEmitter = boxingglovesExplosionEmitter;
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
AddDamageType("boxinggloves",   '<bitmap:add-ons/ci/boxinggloves> %1',    '%2 <bitmap:add-ons/ci/boxinggloves> %1',1,1);
datablock ProjectileData(boxingglovesProjectile)
{
   //projectileShapeName = "~/data/shapes/arrow.dts";
   directDamage        = 5;
   directDamageType  = $DamageType::boxinggloves;
   radiusDamageType  = $DamageType::boxinggloves;
   explosion           = boxingglovesExplosion;
   //particleEmitter     = as;

   muzzleVelocity      = 50;
   velInheritFactor    = 1;

   impactImpulse	     = 700;

   armingDelay         = 0;
   lifetime            = 100;
   fadeDelay           = 70;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.0;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";
};


//////////
// item //
//////////
datablock ItemData(boxingglovesItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./shapes/boxinggloveItem.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Boxing Gloves";
	iconName = "./ItemIcons/boxinggloves";
	doColorShift = false;
	colorShiftColor = "0.471 0.471 0.471 1.000";

	 // Dynamic properties defined by the scripts
	image = boxingglovesRightImage;
	canDrop = true;
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(boxingglovesRightImage)
{
   // Basic Item properties
   shapeFile = "./shapes/boxinggloveBothRight.dts";
   emap = true;

   skinName = 'green';

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   //eyeOffset = "0.7 1.2 -0.5";

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = false;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = boxingglovesItem;
   ammo = " ";
   projectile = boxingglovesProjectile;
   projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = true;
   doRetraction = false;
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
	stateTimeoutValue[0]             = 0.15;
	stateTransitionOnTimeout[0]       = "Ready";
	stateSequence[0]	= "Ready";
	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;
	stateSequence[1]	= "Ready";
	stateScript[1]                  = "onReady";

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Reload";
	stateTimeoutValue[2]            = 0.10;
	stateSound[2]					= boxingglovesFireSound;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateScript[2]                  = "onFire";
    stateSequence[2]	= "Fire";
	stateWaitForTimeout[2]			= true;

	stateName[3]			= "Reload";
	stateTimeoutValue[3]            = 0.15;
	stateWaitForTimeout[3]			= true;
	stateSequence[3]	= "Return";
	stateTransitionOnTimeout[3]     = "preFinal";

	stateName[4]			= "preFinal";
	stateTransitionOnTriggerUp[4]  = "Final";

	stateName[5]			= "Final";
	stateScript[5]                  = "onFinal";
};

datablock ShapeBaseImageData(boxingglovesBothImage)
{
   // Basic Item properties
   shapeFile = "./shapes/boxinggloveBothLeft.dts";
   emap = true;

   skinName = 'green';

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   //eyeOffset = "0.7 1.2 -0.5";

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = false;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = boxingglovesItem;
   ammo = " ";
   projectile = boxingglovesProjectile;
   projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = true;
   doRetraction = false;
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
	stateTimeoutValue[0]             = 0.15;
	stateTransitionOnTimeout[0]       = "Ready";
	stateSequence[0]	= "Ready";

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;
	stateSequence[1]	= "Ready";
	stateScript[1]                  = "onReady";

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Reload";
	stateTimeoutValue[2]            = 0.10;
	stateSound[2]					= boxingglovesFireSound;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateScript[2]                  = "onFire";
    stateSequence[2]	= "Fire";
	stateWaitForTimeout[2]			= true;

	stateName[3]			= "Reload";
	stateTimeoutValue[3]            = 0.15;
	stateWaitForTimeout[3]			= true;
	stateSequence[3]	= "Return";
	stateTransitionOnTimeout[3]     = "preFinal";

	stateName[4]			= "preFinal";
	stateTransitionOnTriggerUp[4]  = "Final";

	stateName[5]			= "Final";
	stateScript[5]                  = "onFinal";
};

datablock ShapeBaseImageData(boxingglovesBlockImage)
{
   // Basic Item properties
   shapeFile = "./shapes/boxinggloveBlock.dts";
   emap = true;

   skinName = 'green';

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   //eyeOffset = "0.7 1.2 -0.5";

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = false;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = boxingglovesItem;
   ammo = " ";
   projectile = boxingglovesProjectile;
   projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = true;
   doRetraction = false;
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
	stateName[0]			= "Activate";
	stateTimeoutValue[0]             = 0.15;
	stateTransitionOnTimeout[0]       = "Ready";
	stateSequence[0]	= "Return";
	stateSound[0]					= boxingglovesBlockFireSound;

	stateName[1]			= "Ready";
	stateSequence[1]	= "Fire";
};

function boxingglovesRightImage::OnMount(%this, %obj, %slot)
{
	%obj.hideNode(lhand);
	%obj.hideNode(rhand);
	%obj.blocking = 0;
}

function boxingglovesBothImage::OnFire(%this, %obj, %slot)
{
	if(%obj.blocking == 0)
	{
	%obj.punching = 1;
	if(%obj.getDamagePercent() < 1.0)
	Parent::onFire(%this,%obj,%slot);
	}
}

function boxingglovesRightImage::OnFire(%this, %obj, %slot)
{
	if(%obj.blocking == 0)
	{
	%obj.punching = 1;
	if(%obj.getDamagePercent() < 1.0)
	Parent::onFire(%this,%obj,%slot);
	}
}

function boxingglovesRightImage::OnReady(%this, %obj, %slot)
{
	%obj.punching = 0;
}

function boxingglovesBothImage::OnReady(%this, %obj, %slot)
{
	%obj.punching = 0;
}

function boxingglovesRightImage::OnFinal(%this, %obj, %slot)
{
	  %obj.unmountImage(0);
	  %obj.hideNode(lhand);
	  %obj.hideNode(rhand);
	  %obj.mountImage(boxingglovesBothImage,0);
}

function boxingglovesBothImage::OnFinal(%this, %obj, %slot)
{
	  %obj.unmountImage(0);
	  %obj.hideNode(lhand);
	  %obj.hideNode(rhand);
	  %obj.mountImage(boxingglovesRightImage,0);
}

function boxingglovesProjectile::Oncollision(%this,%obj,%col,%fade,%pos,%normal)
{
	if(%col.getType() & $TypeMasks::PlayerObjectType)
	{

	  if(getword(%pos, 2) > getword(%col.getWorldBoxCenter(), 2) - 3.3)
	  {
	    %damLoc = %col.getDamageLocation(%pos);

	    if (strstr(%damLoc, "front") != -1 && %col.blocking == 1)
		{
			//dontspeak
		}
		else
		{
			Parent::Oncollision(%this,%obj,%col,%fade,%pos,%normal);
		}

	  }
	  else
	  {
		  	Parent::Oncollision(%this,%obj,%col,%fade,%pos,%normal);
	  }
   
   }
	
}

function boxingglovesProjectile::damage(%this, %obj, %col, %fade, %pos, %normal)
{
	   if(%this.directDamage <= 0)
	   return;

	   //direct damage doubles for crouching players
	   %damageType = $DamageType::Direct;
	   if(%this.DirectDamageType)
	   %damageType = %this.DirectDamageType;

	   if(%col.getType() & $TypeMasks::PlayerObjectType)
	   {
		   if(getword(%pos, 2) > getword(%col.getWorldBoxCenter(), 2) - 3.3)
	       {
			   %Knockout = %this.directDamage * 3;
		       %col.damage(%obj, %pos, %Knockout, %damageType);
			   %col.emote(StarImage);
		   }
		   else
		   {
               %col.damage(%obj, %pos, %this.directDamage, %damageType);
		   }
	   }
	   else
	   {
		  %col.damage(%obj, %pos, %this.directDamage, %damageType);
	   } 
}

function boxingglovesRightImage::onUnMount(%this, %obj, %slot)
{
	  	 %obj.unhideNode(lhand);
	     %obj.unhideNode(rhand);
}

function boxingglovesBothImage::onUnMount(%this, %obj, %slot)
{
	  	 %obj.unhideNode(lhand);
	     %obj.unhideNode(rhand);
}

function boxingglovesBlockImage::onUnMount(%this, %obj, %slot)
{
	  	 %obj.unhideNode(lhand);
	     %obj.unhideNode(rhand);
}

package BoxinggloveTrigger
{
 function Armor::onTrigger(%this,%player,%slot,%trigger)
 {
   if(%slot !$= "4" || checkforgloves(%player) == 0){Parent::onTrigger(%this,%player,%slot,%trigger);return;}
     if (%player.blocking == 0 && %player.punching == 0  && %player.justpunched != 1)
     {
	  %player.blocking = 1;
	  %player.unmountImage(0);
	  %player.unmountImage(1);
	  %player.hideNode(lhand);
	  %player.hideNode(rhand);
	  %player.mountImage(boxingglovesBlockImage,0);
	  %player.playthread(2,armreadyleft);
     } 
     else if (%player.blocking == 1)
     {
      %player.blocking = 0;
	  %player.unmountImage(0);
      %player.mountImage(boxingglovesRightImage, 0);
	  %player.playthread(2,armreadyleft);
     }

	 if (%slot $= "4" && %player.punching == 1)
     {
		 %player.justpunched = 1;
	 }
	 else if (%slot $= "4" && %player.punching == 0)
	 {
		  %player.justpunched = 0;
	 }
  }

 function checkforgloves(%player)
 {
	 if (%player.getMountedImage(0) $= nametoID(boxingglovesRightImage) || %player.getMountedImage(0) $= nametoID(boxingglovesBlockImage) || %player.getMountedImage(0) $= nametoID(boxingglovesBothImage))
	 {
		 return 1;
	 }
	 else
	 {
		 return 0;
	 }
 }
};
activatePackage(BoxinggloveTrigger);

datablock ParticleData(StarParticle)
{
   dragCoefficient      = 5.0;
   gravityCoefficient   = -0.2;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 1000;
   lifetimeVarianceMS   = 500;
   useInvAlpha          = false;
   textureName          = "base/data/particles/star1";
   colors[0]     = "0.8 0.4 0.0 0.7";
   colors[1]     = "1.0 0.4 0.0 0.5";
   colors[2]     = "0.8 0.4 0.8 0.0";
   sizes[0]      = 0.4;
   sizes[1]      = 0.6;
   sizes[2]      = 0.4;
   times[0]      = 0.0;
   times[1]      = 0.2;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(StarEmitter)
{
   ejectionPeriodMS = 35;
   periodVarianceMS = 0;
   ejectionVelocity = 0.5;
   ejectionOffset   = 1.0;
   velocityVariance = 0.49;
   thetaMin         = 0;
   thetaMax         = 120;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "StarParticle";
};

datablock ShapeBaseImageData(StarImage)
{
	shapeFile = "base/data/shapes/empty.dts";
	emap = false;

	mountPoint = $HeadSlot;

	stateName[0]					= "Ready";
	stateTransitionOnTimeout[0]		= "FireA";
	stateTimeoutValue[0]			= 0.01;

	stateName[1]					= "FireA";
	stateTransitionOnTimeout[1]		= "Done";
	stateWaitForTimeout[1]			= True;
	stateTimeoutValue[1]			= 0.350;
	stateEmitter[1]					= StarEmitter;
	stateEmitterTime[1]				= 0.350;

	stateName[2]					= "Done";
	stateScript[2]					= "onDone";
};
function StarImage::onDone(%this,%obj,%slot)
{
	%obj.unMountImage(%slot);
}