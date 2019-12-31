//very little credit really go to me for this, i actually jsut changed arund dual laserguns to work with the laser dd44
//credits

if(!isObject(dd44Projectile)){exec("./Weapon_dd44.cs");}
if($AddOn__Weapon_dd44 $= "-1"){schedule(1000,0,eval,"dd44item.uiname = \"\";");}

//////////
// item //
//////////
datablock ItemData(dualdd44Item)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./shapes/dd44.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Dual DD44 Dostovei";
	iconName = "add-ons/ItemIcons/dd44";
	doColorShift = true;
	colorShiftColor = "0.25 0.25 0.25 1.000";

	 // Dynamic properties defined by the scripts
	image = Rightdd44Image;
	canDrop = true;
};



////////////////
//weapon image//
////////////////
AddDamageType("dualdd44",   '<bitmap:add-ons/ci/dualdd44> %1',    '%2 <bitmap:add-ons/ci/dualdd44> %1',0.5,1);
datablock ProjectileData(dualdd44Projectile : dd44projectile)
{
   directDamageType    = $DamageType::dualdd44;
   radiusDamageType    = $DamageType::dualdd44;
 DirectDamage = 40;
};

datablock ShapeBaseImageData(Rightdd44Image : dd44Image)
{
 projectile = dualdd44Projectile;
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.15;
	stateTransitionOnTimeout[0]       = "Ready";
	stateSound[0]					= activate4Sound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;
	stateSequence[1]	= "Ready";
	stateScript[1]                  = "onReady";

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Smoke";
	stateTimeoutValue[2]            = 0.08;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;
	stateEmitter[2]					= dd44FlashEmitter;
	stateEmitterTime[2]				= 0.05;
	stateEmitterNode[2]				= "muzzleNode";
	stateSound[2]					= DD44Shot1Sound;
	stateEjectShell[2]       = true;

	stateName[3] = "Smoke";
	stateEmitter[3]					= dd44SmokeEmitter;
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
	stateTimeoutValue[5]            = 0.02;
	stateTransitionOnTimeout[5]     = "Ready";
};

datablock ShapeBaseImageData(Leftdd44Image : dd44Image)
{
 projectile = dualdd44Projectile;
 mountPoint = 1;
	stateSound[0]					= "";
};

function Rightdd44Image::onFire(%this,%obj,%slot)
{
	if(%obj.getDamagePercent() < 1.0)
	{

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


function Rightdd44Image::onFire2(%this,%obj,%slot)
{
	if(%obj.getDamagePercent() < 1.0 && isObject(%obj.getMountedImage(1)) && %obj.client.dd44mode == 0)
	{
	 %obj.setImageTrigger(1,1);
	}	
}

function Leftdd44Image::onFire(%this,%obj,%slot)
{
	if(%obj.getDamagePercent() < 1.0)
	{

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

function Rightdd44Image::onMount(%this,%obj,%slot)
{
 %obj.mountImage(Leftdd44Image,1);
 %obj.playthread(0,armreadyboth);
}

function Rightdd44Image::onUnMount(%this,%obj,%slot)
{
 if(%obj.getMountedImage(1) $= nametoID(Leftdd44Image)){%obj.unmountimage(1);%obj.playthread(0,root);}
}

package ArmorOver
{
 function Armor::onTrigger(%this,%player,%slot,%trigger)
 {
  if(%slot !$= "4" || !%player.getMountedImage(1) || %player.client.dd44mode == 0){Parent::onTrigger(%this,%player,%slot,%trigger);return;}
  %player.setImageTrigger(1,%trigger);
 }
 function dualdd44Item::onPickup(%this,%item,%obj)
 {
  if(!isObject(%item.fakedd44) && !%item.bl_id){return;}
  if(Parent::onPickup(%this,%item,%obj) $= "1")
  {
   if(isObject(%item) && isObject(%item.fakedd44))
   {
    Item::FadeOut(%item.fakedd44);
    schedule(%item.spawnbrick.itemrespawntime,0,dd44Fade,%item);
   }
  }
 }
 function dualdd44Item::onAdd(%this,%item,%a,%b)
 {
  Parent::onAdd(%this,%item);
  schedule(1,0,dd44Add,%item);
 }
 function dd44Add(%item)
 {
  if(!isObject(%item.spawnbrick)){return;}
  %i = new Item(){datablock = dualdd44Item;position = vectorAdd(%item.position,"0.2 0.2 0");rotation = %item.rotation;canpickup = 0;};
  %item.fakedd44 = %i;
 }
 function dualdd44Item::onRemove(%this,%item)
 {
  if(isObject(%item.fakedd44))
  %item.fakedd44.delete();
 }
 function dd44fade(%item)
 {
  Item::fadeIn(%item.fakedd44);
  echo("Done");
 }
};
activatePackage(ArmorOver);

datablock StaticShapeData(fakedd44)
{
 shapefile = "add-ons/shapes/dd44.dts";
};

function servercmdualdd44Mode(%client)
{
 %client.dd44mode = !%client.dd44mode;
 messageclient(%client,'',"\c1Dual dd44s now in \"\c4" @ (%client.dd44mode ? "Controlled Fire" : "Automatic Fire") @ "\c1\" mode.");
}