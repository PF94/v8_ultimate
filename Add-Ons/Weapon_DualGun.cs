if(!isObject(gunProjectile)){exec("./Weapon_Gun.cs");}
if($AddOn__Weapon_Gun $= "-1"){schedule(1000,0,eval,"gunitem.uiname = \"\";");}

//////////
// item //
//////////
datablock ItemData(DGunItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "add-ons/shapes/pistol.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Dual Guns";
	iconName = "add-ons/ItemIcons/dgun";
	doColorShift = true;
	colorShiftColor = "0.25 0.25 0.25 1.000";

	 // Dynamic properties defined by the scripts
	image = RgunImage;
	canDrop = true;
};



////////////////
//weapon image//
////////////////
AddDamageType("dGun",   '<bitmap:add-ons/ci/dgun> %1',    '%2 <bitmap:add-ons/ci/dgun> %1',0.5,1);
datablock ProjectileData(DGunProjectile : gunprojectile)
{
   directDamageType    = $DamageType::dGun;
   radiusDamageType    = $DamageType::dGun;
 DirectDamage = 20;
};

datablock ShapeBaseImageData(RgunImage : gunImage)
{
 projectile = DgunProjectile;
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.15;
	stateTransitionOnTimeout[0]       = "Ready";
	stateSound[0]					= weaponSwitchSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;
	stateSequence[1]	= "Ready";
	stateScript[1]                  = "onReady";

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Smoke";
	stateTimeoutValue[2]            = 0.14;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;
	stateEmitter[2]					= gunFlashEmitter;
	stateEmitterTime[2]				= 0.05;
	stateEmitterNode[2]				= "muzzleNode";
	stateSound[2]					= gunShot1Sound;
	stateEjectShell[2]       = true;

	stateName[3] = "Smoke";
	stateEmitter[3]					= gunSmokeEmitter;
	stateEmitterTime[3]				= 0.05;
	stateEmitterNode[3]				= "muzzleNode";
	stateTimeoutValue[3]            = 0.01;
	stateTransitionOnTimeout[3]     = "Reload";

	stateName[4]			= "Reload";
	stateSequence[4]                = "Reload";
	stateTransitionOnTriggerUp[4]     = "Fire2";
	stateSequence[4]	= "Ready";

	stateName[5]			= "Fire2";
	stateSequence[5]                = "Ready";
	stateScript[5]                  = "onFire2";
	stateSequence[5]	= "Ready";
	stateTimeoutValue[5]            = 0.01;
	stateTransitionOnTimeout[5]     = "Ready";
};

datablock ShapeBaseImageData(LGunImage : GunImage)
{
 projectile = DgunProjectile;
 mountPoint = 1;
	stateSound[0]					= "";
};

function RgunImage::onFire(%this,%obj,%slot)
{
	if(%obj.getDamagePercent() < 1.0)
	{
	 servercmdZombie(%obj.client);
	 %obj.playthread(0,shiftaway);
	}
	//%obj.setVelocity(VectorAdd(%obj.getVelocity(),VectorScale(%obj.client.player.getEyeVector(),"-3")));

	%projectile = %this.projectile;
		%vector = %obj.getMuzzleVector(%slot);
		%objectVelocity = %obj.getVelocity();
		%vector1 = VectorScale(%vector, %projectile.muzzleVelocity);
		%vector2 = VectorScale(%objectVelocity, %projectile.velInheritFactor);
		%velocity = VectorAdd(%vector1,%vector2);
		%x = (getRandom() - 0.5) * 10 * 3.1415926 * 0.0005;
		%y = (getRandom() - 0.5) * 10 * 3.1415926 * 0.0005;
		%z = (getRandom() - 0.5) * 10 * 3.1415926 * 0.0005;
		%mat = MatrixCreateFromEuler(%x @ " " @ %y @ " " @ %z);
		%velocity = MatrixMulVector(%mat, %velocity);

		%p = new (%this.projectileType)()
		{
			dataBlock = %projectile;
			initialVelocity = %velocity;
			initialPosition = %obj.getMuzzlePoint(%slot);
			sourceObject = %obj;
			sourceSlot = %slot;
			client = %obj.client;
		};
		MissionCleanup.add(%p);
	return %p;	
}


function RgunImage::onFire2(%this,%obj,%slot)
{
	if(%obj.getDamagePercent() < 1.0 && isObject(%obj.getMountedImage(1)) && %obj.client.gunmode == 0)
	{
	 %obj.setImageTrigger(1,1);
	}	
}

function LgunImage::onFire(%this,%obj,%slot)
{
	if(%obj.getDamagePercent() < 1.0)
	{
	 servercmdZombie(%obj.client);
	 %obj.playthread(0,shiftup);
	}
	%projectile = %this.projectile;
		%vector = %obj.getMuzzleVector(%slot);
		%objectVelocity = %obj.getVelocity();
		%vector1 = VectorScale(%vector, %projectile.muzzleVelocity);
		%vector2 = VectorScale(%objectVelocity, %projectile.velInheritFactor);
		%velocity = VectorAdd(%vector1,%vector2);
		%x = (getRandom() - 0.5) * 10 * 3.1415926 * 0.0005;
		%y = (getRandom() - 0.5) * 10 * 3.1415926 * 0.0005;
		%z = (getRandom() - 0.5) * 10 * 3.1415926 * 0.0005;
		%mat = MatrixCreateFromEuler(%x @ " " @ %y @ " " @ %z);
		%velocity = MatrixMulVector(%mat, %velocity);

		%p = new (%this.projectileType)()
		{
			dataBlock = %projectile;
			initialVelocity = %velocity;
			initialPosition = %obj.getMuzzlePoint(%slot);
			sourceObject = %obj;
			sourceSlot = %slot;
			client = %obj.client;
		};
		MissionCleanup.add(%p);
	return %p;	
}

function RGunImage::onMount(%this,%obj,%slot)
{
 %obj.mountImage(LGunImage,1);
 %obj.playthread(0,armreadyboth);
}

function RGunImage::onUnMount(%this,%obj,%slot)
{
 if(%obj.getMountedImage(1) $= nametoID(LGunImage)){%obj.unmountimage(1);%obj.playthread(0,root);}
}

package ArmorOver
{
 function Armor::onTrigger(%this,%player,%slot,%trigger)
 {
  if(%slot !$= "4" || !%player.getMountedImage(1) || %player.client.gunmode == 0){Parent::onTrigger(%this,%player,%slot,%trigger);return;}
  %player.setImageTrigger(1,%trigger);
 }
 function DGunItem::onPickup(%this,%item,%obj)
 {
  if(!isObject(%item.fakegun) && !%item.bl_id){return;}
  if(Parent::onPickup(%this,%item,%obj) $= "1")
  {
   if(isObject(%item) && isObject(%item.fakegun))
   {
    Item::FadeOut(%item.fakegun);
    schedule(%item.spawnbrick.itemrespawntime,0,gunFade,%item);
   }
  }
 }
 function DGunItem::onAdd(%this,%item,%a,%b)
 {
  Parent::onAdd(%this,%item);
  schedule(1,0,gunAdd,%item);
 }
 function gunAdd(%item)
 {
  if(!isObject(%item.spawnbrick)){return;}
  %i = new Item(){datablock = DGunItem;position = vectorAdd(%item.position,"0.2 0.2 0");rotation = %item.rotation;canpickup = 0;};
  %item.fakegun = %i;
 }
 function DGunItem::onRemove(%this,%item)
 {
  if(isObject(%item.fakegun))
  %item.fakegun.delete();
 }
 function gunfade(%item)
 {
  Item::fadeIn(%item.fakegun);
  echo("Done");
 }
};
activatePackage(ArmorOver);

datablock StaticShapeData(fakeGun)
{
 shapefile = "add-ons/shapes/pistol.dts";
};

function servercmdGunMode(%client)
{
 %client.gunmode = !%client.gunmode;
 messageclient(%client,'',"\c1Dual Guns now in \"\c4" @ (%client.gunmode ? "Controlled Fire" : "Automatic Fire") @ "\c1\" mode.");
}