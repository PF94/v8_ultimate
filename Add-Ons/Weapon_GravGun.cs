if($AddOn__Gravity_Gun !$= "-1" && $AddOn__Gravity_Gun !$= ""){schedule(2000,0,clientcmdservermessage,'',"\c6The Gravity Gun may not work with the alternate Gravity Gun posted by \c0SolarFlare\c6, disable that to ensure that this Add-On runs correctly.");}

datablock ParticleData(grGunParticle : printGunParticle)
{
 	colors[0]     = "0.9 0.2 0.0 0.9";
	colors[1]     = "0.9 0.2 0.0 0.0";
};

datablock ParticleData(grGunParticle2 : grGunParticle)
{
 	colors[0]     = "0.9 0.2 0.0 0.9";
	colors[1]     = "0.9 0.2 0.0 0.0";
	sizes[0]      = 0.1;
	sizes[1]      = 0.1;
              dragCoefficient = 0;
	lifetimeMS           = 500;
};

datablock ParticleEmitterData(grGunEmitter : printGunEmitter)
{
 particles = grGunParticle;
};

datablock ParticleEmitterData(grGunEmitter2 : printGunEmitter)
{
 particles = grGunParticle2;
};
datablock ParticleData(grGunExplodeParticle : printGunExplosionParticle)
{
 	colors[0]     = "0.9 0.2 0.0 0.9";
	colors[1]     = "0.9 0.2 0.0 0.0";
};

datablock ParticleEmitterData(grGunExplodeEmitter : printGunExplosionEmitter)
{
 particles = grGunParticle2;
 ejectionPeriodMS = 10;
};

datablock ExplosionData(grGunExplosion : printGunExplosion)
{
 emitter[0] = grGunExplodeEmitter;
};

datablock ProjectileData(grGunProjectile : printGunProjectile)
{
muzzleVelocity      = 175;
fadeDelay           = 3500;
lifetime            = 100;
explosion = grGunExplosion;
particleEmitter = "";
explodeondeath = 0;
};

datablock ExplosionData(grGunExplosion2 : printGunExplosion)
{
 emitter[0] = grGunExplodeEmitter;
   impulseRadius = 2;
   impulseForce = 8000;
};

datablock ProjectileData(grGunBlastProjectile : printGunProjectile)
{
muzzleVelocity      = 80;
fadeDelay           = 3500;
lifetime            = 1000;
explosion = grGunExplosion2;
particleEmitter = grGunExplodeEmitter;
explodeondeath = 1;
   impactImpulse	   = 4000;
   verticalImpulse	   = 4000;
};

function grGunBlastProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal) 
{
 if(strStr(strLwr(%col.getClassName()),"vehicle") !$= "-1"){%col.setVelocity(vectorAdd(%col.getVelocity(),vectorScale(%obj.initialVelocity,0.5)));}
}

//////////
// item //
//////////
datablock ItemData(grGunItem : printGun)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	//gui stuff
	uiName = "Gravity Gun";
	doColorShift = true;
	
	colorShiftColor = "0.2 0.2 0.2 1.000";

	 // Dynamic properties defined by the scripts
	image = grGunImage;
	canDrop = true;
};

datablock ShapeBaseImageData(grGunImage : printGunImage)
{
   colorShiftColor = "0.2 0.2 0.2 1.000";
   item = grGunItem;
   projectile = grGunProjectile;
   projectileType = Projectile;


	// States
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.15;
	stateTransitionOnTimeout[0]       = "Ready";
	stateSound[0]					= sprayActivateSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;
	stateSequence[1]	= "Ready";

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Reload";
	stateTimeoutValue[2]            = 0.2;
              stateTransitionOnAmmo[2] = "GravLoop";
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;
	stateEmitter[2]					= "grGunemitter";
	stateEmitterTime[2]				= 0.15;
	stateEmitterNode[2]				= "muzzleNode";
	stateSound[2]					= printFireSound;
	stateEjectShell[2]       = false;

	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateTransitionOnTrigger[3]     = "Ready";
	stateSequence[3]	= "Ready";

	stateName[4]			= "GravLoop";
	stateEmitter[4]					= grGunEmitter2;
	stateEmitterTime[4]				= 0.01;
	stateEmitterNode[4]				= "muzzleNode";
              stateTransitionOnTimeout[4]     = "GravLoop";
              stateTransitionOnTriggerUp[4] = "Cancel";
              stateTransitionOnNoAmmo[4] = "Cancel";
              stateTimeoutValue[4]            = 0.01;
              stateScript[4]                  = "onGrav";
	stateSequence[4]	= "Ready";

              stateName[5] = "Cancel";
              stateScript[5] = "onCancel";
              stateTimeoutValue[5] = "0.1";
              stateTransitionOnTimeout[5] = "Ready";
};

function grGunProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
 if((strStr(strLwr(%col.getClassName()),"vehicle") !$= "-1" || %col.getclassname() $= "AIPlayer") && (!%col.isHeld) && %col.spawnbrick.getGroup().client.minigame $= %obj.client.minigame)
 {
  %obj.client.player.gravobj = %col;
  %obj.client.player.gravobj.holder = %obj.client.player;
  %obj.client.player.gravobj.lastpos = %col.position;
  %obj.client.player.gravobj.lastpos2 = %col.position;
  %obj.client.player.gravobj.lastrot = "0 0 0";
  %obj.client.player.setImageAmmo(0,1);
  %obj.client.player.gravobj.disablemove = 1;
 }
}

function grGunImage::onCancel(%this,%obj,%slot)
{
 if(isObject(%obj.gravObj)){ %obj.gravObj.setVelocity(vectorScale(vectorSub(%obj.gravObj.getTransform(),%obj.gravObj.lastpos2),5));}
 %obj.gravobj.disablemove = 0;
 %obj.gravObj.isHeld = 0;
 schedule(3000,0,gravhold,%obj.gravobj);
 %obj.gravObj = "";
 %obj.setImageAmmo(0,0);
}

function grGunImage::onUnMount(%this,%obj,%slot)
{
 %obj.gravobj.disablemove = 0;
 schedule(3000,0,gravhold,%obj.gravobj);
 %obj.gravObj.holder = "";
 %obj.gravObj.isHeld = "";
 %obj.gravObj = "";
}



function grGunImage::onMount(%this,%obj,%slot)
{
 %obj.setImageAmmo(0,0);
}

function isMountedTo(%a,%b)
{
 for(%i=0;%i<%b.getDatablock().numMountPoints;%i++)
 {
  if(%b.getMountedObject(%i) $= %a){return 1;}
 }
 return 0;
}

function grGunImage::onGrav(%this,%obj,%slot)
{
 %obj.setImageTrigger(0,1);
 if(!isObject(%obj.gravobj)){%obj.setImageTrigger(0,0);%obj.setImageAmmo(0,0);return;}
 if(isMountedTo(%obj,%obj.gravObj)){%obj.setImageTrigger(0,0);%obj.setImageAmmo(0,0);return;}
   %mouseVec = %obj.getMuzzleVector(0);
   %cameraPoint = %obj.getEyePoint();
   %selectRange = %obj.gravObj.getDatablock().mass / 30;
   %mouseScaled = VectorScale(%mouseVec, %selectRange);
   %rangeEnd = VectorAdd(%cameraPoint, %mouseScaled);

   %searchMasks = $TypeMasks::VehicleObjectType | $TypeMasks::FXBrickObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::StaticObjectType | $TypeMasks::TerrainObjectType;

   %player = %obj;
	  %scanTarg = ContainerRayCast (%cameraPoint, %rangeEnd, %searchMasks,%obj.gravobj);
   if(!isObject(firstWord(%scanTarg)))
   {
    %pos = %rangeEnd;
   }
   else
   {
    %sub = vectorSub(posFromRaycast(%scanTarg),%cameraPoint);
    %nom = vectorNormalize(%sub);
    %len = vectorLen(%sub);
    %pos = vectorAdd(%cameraPoint,vectorScale(%nom,%len*0.5));
   }
   %obj.gravObj.holder = %obj;
   %obj.gravObj.isHeld = 1;
   %obj.gravObj.lastPos2 = %obj.gravObj.lastPos;
   %obj.gravObj.lastPos = %obj.gravObj.getTransform();
   %t = %obj.gravObj.getTransform();
   %rot =  eulerToMatrix(%obj.gravobj.lastrot); 
     //Uncomment the line below and comment out the one below this to use alternate control method. It "swings" more realistically but kills people very easily and you if you turn too fast.
   //%obj.gravObj.setTransform(%obj.gravObj.position SPC %rot);
   %obj.gravObj.setTransform(%pos SPC %rot);
  %obj.gravobj.lastrot = vectorAdd(%obj.gravobj.lastrot,"0 0 0.025");
  //Uncomment the line below and comment out the one below this to use alternate control method. It "swings" more realistically but kills people very easily and you if you turn too fast.
   //%obj.gravObj.setVelocity(vectorScale(vectorSub(%pos,%obj.gravObj.position),5));
   %obj.gravObj.setVelocity("0 0 0");
}
AddDamageType("ThrowCar",   '<bitmap:add-ons/ci/generic> <bitmap:add-ons/ci/car> %1',    '%2 <bitmap:add-ons/ci/generic> <bitmap:add-ons/ci/car> %1');
package grGunOver
{
 function Armor::onTrigger(%this,%player,%slot,%trigger)
 {
  if(%trigger != 1 || %slot !$= "4" || %player.getMountedImage(0) != nametoID(grGunImage)){Parent::onTrigger(%this,%player,%slot,%trigger);return;}
  if(isObject(%player.gravobj))
  {
    %player.setImageTrigger(0,0);
    %player.setImageAmmo(0,0);
    %mouseVec = %player.getMuzzleVector(0);
   %vel = vectorScale(%mouseVec,4000 / (%player.gravobj.getDatablock().mass));
   %player.gravobj.setVelocity(%vel);
   %player.gravobj.disablemove = 0;
   %player.gravObj.isHeld = 0;
   schedule(3000,0,gravhold,%player.gravobj);
   %player.gravobj = "";
  }
  else
  {
	%projectile = grGunBlastProjectile;
	%obj = %player;
		%vector = %obj.getEyeVector();
		%objectVelocity = %obj.getVelocity();
		%vector1 = VectorScale(%vector, %projectile.muzzleVelocity);
		%vector2 = VectorScale(%objectVelocity, %projectile.velInheritFactor);
		%velocity = VectorAdd(%vector1,%vector2);

		%p = new Projectile()
		{
			dataBlock = %projectile;
			initialVelocity = %velocity;
			initialPosition = %obj.getMuzzlePoint(0);
			sourceObject = %obj;
			sourceSlot = %slot;
			client = %obj.client;
		};
		MissionCleanup.add(%p);
  }
 }
 function GameConnection::OnDeath(%client,%obj,%a,%b,%c)
 {
  if(%obj.holder !$= ""){%a = %obj.holder.client;%b = $DamageType::ThrowCar;}
  Parent::onDeath(%client,%obj,%a,%b,%c);
 }
};
activatePackage(grGunOver);

function gravclient(%a,%b){%a.client = %b;}
function gravhold(%a){%a.holder = "";}