//-----------------------------------------------------------------------------
// Weapon_Banana
// Copyright (c) SolarFlare Productions, Inc.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Credits to Rotondo for the pickup code
// Credits to Aloshi for portions of the weapon stick code I used
//-----------------------------------------------------------------------------

new SimGroup(BananaGroup);

//-----------------------------------------------------------------------------
// Sound

datablock AudioProfile(bananaTripSound)
{
   filename    = "./sound/tripOver.wav";
   description = AudioClose3d;
   preload = false;
};

//-----------------------------------------------------------------------------
// Projectile

AddDamageType("bananaDirect",   '<bitmap:add-ons/ci/banana> %1',    '%2 <bitmap:add-ons/ci/banana> %1',1,1);
datablock ProjectileData(bananaProjectile)
{
   projectileShapeName = "./shapes/SolarMod/banana.dts";
   directDamage        = 0;
   impactImpulse       = 0;
   verticalImpulse     = 0;

   brickExplosionRadius = 0;
   brickExplosionImpact = false; //destroy a brick if we hit it directly?
   brickExplosionForce  = 20;
   brickExplosionMaxVolume = 200;

   muzzleVelocity      = 30;
   velInheritFactor    = 0;
   explodeOnDeath = true;

   armingDelay         = 0;
   lifetime            = 2000;
   fadeDelay           = 100;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 1.0;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";
};

//-----------------------------------------------------------------------------
// Item

datablock ItemData(bananaItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./shapes/SolarMod/banana.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Banana";
	iconName = "./ItemIcons/banana";
	doColorShift = false;

	 // Dynamic properties defined by the scripts
	image = bananaImage;
	canDrop = true;
};

//-----------------------------------------------------------------------------
// Weapon Image

datablock ShapeBaseImageData(bananaImage)
{
   // Basic Item properties
   shapeFile = "./shapes/SolarMod/banana.dts";
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
   item = bananaItem;
   ammo = " ";
   projectile = bananaProjectile;
   projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;

   doColorShift = false;

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
	stateSound[0]			= weaponSwitchSound;

	stateName[1]			= "Ready";
	stateTransitionOnTriggerDown[1]	= "Charge";
	stateAllowImageChange[1]	= true;
	
	stateName[2]                    = "Charge";
	stateTransitionOnTimeout[2]	= "Armed";
	stateTimeoutValue[2]            = 0.7;
	stateWaitForTimeout[2]		= false;
	stateTransitionOnTriggerUp[2]	= "AbortCharge";
	stateScript[2]                  = "onCharge";
	stateAllowImageChange[2]        = false;
	
	stateName[3]			= "AbortCharge";
	stateTransitionOnTimeout[3]	= "Ready";
	stateTimeoutValue[3]		= 0.3;
	stateWaitForTimeout[3]		= true;
	stateScript[3]			= "onAbortCharge";
	stateAllowImageChange[3]	= false;

	stateName[4]			= "Armed";
	stateTransitionOnTriggerUp[4]	= "Fire";
	stateAllowImageChange[4]	= false;

	stateName[5]			= "Fire";
	stateTransitionOnTimeout[5]	= "Ready";
	stateTimeoutValue[5]		= 0.5;
	stateFire[5]			= true;
	stateSequence[5]		= "fire";
	stateScript[5]			= "onFire";
	stateWaitForTimeout[5]		= true;
	stateAllowImageChange[5]	= false;
	stateSound[5]			= spearFireSound;
};

//-----------------------------------------------------------------------------
// Banana Item Pickup Overwrite

function bananaItem::onCollision(%data, %obj, %col)
{
   %id = getBrickGroupFromObject(%obj).client.bl_id;
   %trust = getTrustLevel(getBrickGroupFromObject(%obj).client,%col.client);
      %cl = getBrickGroupFromObject(%obj).client;

   if(%obj.canPickup == 0)
   {
      return;
   }

   if(%col.client.minigame == 0)
   {
      %col.client.minigame = -1;
   }
   if(%cl.minigame == 0)
   {
      %cl.minigame = -1;
   }
   if(%col.client.minigame != %cl.minigame)
   {
      if(%col.client.minigame == -1)
      {
         centerPrint(%col.client, "This item is part of a mini-game.", 1, 2);
         return;
      }
      else
      {
         centerPrint(%col.client, "This item is not part of the mini-game.", 1, 2);
         return;
      }
   }
   if(%col.client.minigame == %cl.minigame && %cl.minigame != -1 && %cl.minigame.useAllPlayersBricks == 0 && %cl.minigame.owner.bl_id != %id)
   {
      centerprint(%col.client, "This item is not part of the mini-game.", 1, 2);
      return;
   }
   if(%trust >= $TrustLevel::Build || %cl== %col.client || %col.client.minigame == %cl.minigame && %cl.minigame != -1)
   {
      if(isObject(%obj.spawnBrick))
      {
         for(%i = 0; %i < 5; %i++)
         {
            %toolDB = %col.tool[%i];
            if(%obj.canPickup == 0)
            {
               return;
            }
            if(%toolDB == 0)
            {
               %col.tool[%i] = %obj.getdatablock();
               %col.weaponCount++;
               messageClient(%col.client,'MsgItemPickup','',%i,%obj.getdatablock());
               %obj.canpickup = 0;
               %obj.fadeout();
               schedule(%obj.spawnBrick.itemRespawnTime, 0, bananaOverwrite, %obj);
               break;
            }
         }
      }
      else
      {
         for(%i = 0; %i < 5; %i++)
         {
            %toolDB = %col.tool[%i];
            if(%obj.canPickup == 0)
            {
               return;
            }
            if(%toolDB == 0)
            {
               %col.tool[%i] = %obj.getDatablock();
               %col.weaponCount++;
               messageClient(%col.client,'MsgItemPickup','',%i,%obj.getDatablock());
               %obj.delete();
               break;
            }
         }
      }
   }
   else
   {
      centerprint(%col.client, %cl.name @ " does not trust you enough to use this item.", 1, 2);
   }
	
}

function bananaOverwrite(%obj)
{
   if(isObject(%obj))
   {
      %obj.fadeIn();
      %obj.canPickup = 1;
   }
}

//-----------------------------------------------------------------------------
// State Functions

function bananaImage::onMount(%this, %obj, %slot)
{
   if(!isObject(%obj.client.minigame))
   {
      serverCmdUnUseTool(%obj.client);
      centerPrint(%obj.client, "You may not use the banana unless you are in a minigame.", 2, 2);
      return;
   }

   %obj.bananaSlot = %obj.currTool;
   parent::onMount(%this, %obj, %slot);
}

function bananaImage::onCharge(%this, %obj, %slot)
{
   %obj.playthread(2, spearReady);
}

function bananaImage::onAbortCharge(%this, %obj, %slot)
{
   %obj.playthread(2, root);
}

function bananaImage::onFire(%this, %obj, %slot)
{

   parent::onFire(%this, %obj, %slot);
   
   %allRot = %obj.getTransform();
   %rot = getWord(%allRot, 3);
   %rot = %rot SPC getWord(%allRot, 4);
   %rot = %rot SPC getWord(%allRot, 5);
   %rot = %rot SPC getWord(%allRot, 6);
   %obj.sRot = %rot;

   %obj.playthread(2, spearThrow);
   %obj.tool[%obj.bananaSlot] = 0;
   %obj.weaponCount--;
   messageClient(%obj.client, 'MsgItemPickup', '', %obj.bananaSlot, 0);
   serverCmdUnUseTool(%obj.client);
}

function bananaProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
   if(%col.getClassName $= "Player")
   {
      tripOver(%col);
      return;
   }
   
   %ssPos = firstWord(%pos);
   %ssPos = %ssPos SPC getWord(%pos, 1);
   %ssPos = %ssPos SPC getWord(%pos, 2) + 0.25;

   %ss = new StaticShape()
   {
      position = %ssPos;
      datablock = BananaStick;
      scale = "1 1 1";
      rotation = %obj.client.player.sRot;
   };
   %ss.setTransform(%ssPos SPC %obj.client.player.sRot);

   %trigPos = getWord(%pos, 0) - 0.5;
   %trigPos = %trigPos SPC getWord(%pos, 1) - 0.5 + 1;
   %trigPos = %trigPos SPC getWord(%pos, 2);

   %t = new Trigger()
   {
      datablock = TripTrigger;
      position = %trigPos;
      polyhedron = "0 0 0 1 0 0 0 -1 0 0 0 1";
      scale = "1 1 1";
      rotation = "1 0 0 0";
   };
   %t.bananaObject = %ss;
   
   MissionCleanup.add(%t);
   MissionCleanup.add(%ss);

   BananaGroup.add(%t);

   serverPlay3D(slowImpactSound, %obj.getTransform());
}

//-----------------------------------------------------------------------------
// Other Functions

function tripOver(%player)
{
   if(isObject(%player) && %player.getClassName() $= "Player")
   {
      %player.playAudio(0, bananaTripSound);
      tumble(%player, 5000);
      %player.setWhiteOut(0.25);
      %player.schedule(5*1000, playThread, 2, root);
      return;
   }
   error("Object not found, or object is not a player!");
}

function serverCmdClearBananas(%client)
{
   if(%client.isAdmin || %client.isSuperAdmin)
   {
      for(%i = 0; %i < BananaGroup.getCount(); %i++)
      {
         %obj = BananaGroup.getObject(%i);
         %obj.bananaObject.schedule(100, delete);
         %obj.schedule(100, delete);
      }
      messageAll('MsgClearBricks', '\c3%1\c0 cleared all bananas.', %client.name);
      return;
   }
   messageClient(%client, '', "You are not an admin!");
}

//-----------------------------------------------------------------------------
// Packaging

package bananaPackage
{
   function destroyServer()
   {
      if(isObject(BananaGroup))
         BananaGroup.delete();
      error("BananaGroup.delete();");
      parent::destroyServer();
   }
};
activatePackage(bananaPackage);

//-----------------------------------------------------------------------------
// Other Datablocks

datablock TriggerData(TripTrigger)
{
   tickPeriodMS = 100;
};

function TripTrigger::onEnterTrigger(%this, %trigger, %obj)
{
   if(%obj.getClassName() $= "Player")
   {
      tripOver(%obj);

      if(isObject(%trigger.bananaObject))
         %trigger.bananaObject.delete();

      %trigger.schedule(10, delete);
   }
}

datablock StaticShapeData(BananaStick)
{
   shapeFile = "./shapes/SolarMod/banana.dts";
};
