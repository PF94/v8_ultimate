exec("./Weapon_Bow.cs");
exec("./Weapon_FireArrow.txt");

$inv[1,0] = nametoID(gunProjectile);			//Projectile
$inv[1,1] = "Gun Bullets";				//Inv Name
$inv[1,2] = "bullets";					//Shortcut Name
$inv[1,3] = 10;					//Amount
$inv[2,0] = nametoID(rocketLauncherProjectile);		//Projectile
$inv[2,1] = "Rockets";				//Inv Name
$inv[2,2] = "rockets";				//Shortcut Name
$inv[2,3] = 7;					//Amount
$inv[3,0] = nametoID(arrowProjectile);			//Projectile
$inv[3,1] = "Wooden Arrows";				//Inv Name
$inv[3,2] = "woodarrow";				//Shortcut Name
$inv[3,3] = 5;					//Amount
$inv[3,4] = nametoID(bowImage);			//Override image
$inv[3,5] = "Bow";
$inv[4,0] = nametoID(firearrowProjectile);		//Projectile
$inv[4,1] = "Fire Arrows";				//Inv Name
$inv[4,2] = "firearrow";				//Shortcut Name
$inv[4,3] = 3;					//Amount
$inv[4,4] = nametoID(bowImage);			//Override image
$inv[4,5] = "Bow";
$invX = 4;

package WeaponAmmo
{
	function GameConnection::CreatePlayer(%this,%a,%b,%c,%d,%e)
	{
		Parent::CreatePlayer(%this,%a,%b,%c,%d,%e);
		for(%i=1;%i<=$invX;%i++)
		{
			for(%j=0;%j<=5;%j++)
			{
				%this.inv[%i,%j] = $inv[%i,%j];
			}
		}
	}

	function WeaponImage::onFire(%this,%obj,%slot)
	{
	   if(!%this.ammotype){Parent::onFire(%this,%obj,%slot);return;}
	   %client = %obj.client;
	   if(%client.ammotype[%this] && %client.inv[%client.ammotype[%this],3])
	   {
		echo("Secondary Ammo");
		%projectile = %client.inv[%client.ammotype[%this],0];
	    	%client.inv[%client.ammotype[%this],3]--;
	    	commandtoclient(%client,'centerprint',"<just:left>" @ %client.inv[%client.ammotype[%this],1] @ ": " @ %client.inv[%client.ammotype[%this],3],1,1,10);
	   }else if(%client.inv[%this.ammotype,3])
	   {
	    %projectile = %client.inv[%this.ammotype,0];
	    %client.inv[%this.ammotype,3]--;
	    commandtoclient(%client,'centerprint',"<just:left>" @ %client.inv[%this.ammotype,1] @ ": " @ %client.inv[%this.ammotype,3],1,1,10);
	   }
	   else{return;}
	
	   // Determin initial projectile velocity based on the 
	   // gun's muzzle point and the object's current velocity
	   %muzzleVector = %obj.getMuzzleVector(%slot);
	   %objectVelocity = %obj.getVelocity();
	   %muzzleVelocity = VectorAdd(
	      VectorScale(%muzzleVector, %projectile.muzzleVelocity),
	      VectorScale(%objectVelocity, %projectile.velInheritFactor));
	
	   // Create the projectile object
	   %p = new (%this.projectileType)() {
	      dataBlock        = %projectile;
	      initialVelocity  = %muzzleVelocity;
	      initialPosition  = %obj.getMuzzlePoint(%slot);
	      sourceObject     = %obj;
	      sourceSlot       = %slot;
	      client           = %obj.client;
	   };
	   MissionCleanup.add(%p);
	   return %p;
	}
	
	function WeaponImage::onMount(%this,%obj,%slot)
	{
	   Parent::onMount(%this,%obj,%slot);
	   %client = %obj.client;
	   if(%client.ammotype[%this] && %client.inv[%client.ammotype[%this],3])
	   {
	    commandtoclient(%client,'centerprint',"<just:left>" @ %client.inv[%client.ammotype[%this],1] @ ": " @ %client.inv[%client.ammotype[%this],3],1,1,10);
	   }else if(%client.inv[%this.ammotype,3])
	   {
	    commandtoclient(%client,'centerprint',"<just:left>" @ %client.inv[%this.ammotype,1] @ ": " @ %client.inv[%this.ammotype,3],1,1,10);
	   }
	   else{return;}
	}
	
	function GameConnection::onDeath(%this,%a,%b,%c,%d)
	{
		makeitem(AmmoItem,%this.player.position);
		Parent::onDeath(%this,%a,%b,%c,%d);
	}
};
activatePackage(WeaponAmmo);

gunImage.ammotype = 1;
rocketLauncherImage.ammotype = 2;
autorocketLauncherImage.ammotype = 2;
bowImage.ammotype = 3;

function servercmdInv(%client)
{
	messageclient(%client,"","\c5Inventory List:");
	for(%i=1;%i<=$invX;%i++)
	{
		if(%client.inv[%i,3] > 0)
		{
			messageclient(%client,"","\c5" @ %client.inv[%i,1] @ "\c0 (" @ %client.inv[%i,2] @ ") \c5x" @ %client.inv[%i,3]);
		}
	}
}

function servercmdUse(%client,%name)
{
	for(%i=1;%i<=$invX;%i++)
	{
		if(%client.inv[%i,2] $= %name && %client.inv[%i,3] > 0)
		{
			echo("Found1");
			if(%client.inv[%i,4] !$= "")
			{
				echo("Found2");
				%client.ammotype[%client.inv[%i,4]] = %i;
				messageclient(%client,"","\c0" @ %client.inv[%i,5] @ " \c5ammo set to \c0" @ %client.inv[%i,1]);
				return;
			}
			else
			{
				echo("Abort1");
				messageclient(%client,"","\c5Can't use that right now.");
				return;
			}
		}
	}
	echo("Abort2");
	messageclient(%client,"","\c5Inventory item \c0" @ %name @ " \c5not found.");
}

datablock ItemData(AmmoItem)
{
	category = "Item";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "base/data/shapes/brickweapon.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Ammo Item";
	iconName = "base/client/ui/brickIcons/1x2F";
	doColorShift = true;
	colorShiftColor = "0.0 0.0 0.5 1.000";

	 // Dynamic properties defined by the scripts
	image = hammerImage;
	canDrop = true;
};

function AmmoItem::onAdd(%this,%item,%a,%b,%c)
{
 Parent::onAdd(%item,%item,%a,%b,%c);
 %item.ammotype = getRandom(1,$invX);
 echo(%this);
 echo(%item);
 %item.ammoname = $inv[%item.ammotype,1];
 %item.ammoamount = getRandom(5,15);
 %item.setShapeName(%item.ammoname @ " x" @ %item.ammoamount);
}

function AmmoItem::onPickup(%this, %item, %obj, %amount)
{
	%client = %obj.client;
		messageclient(%client,"","\c5" @ %client.inv[%item.ammotype,1] @ " ammo restored!");
		%client.inv[%item.ammotype,3] = %client.inv[%item.ammotype,3] + %item.ammoamount;
	schedule(10000,0,respawnitem,%item.spawnbrick,AmmoItem);
	%item.spawnbrick.setItem(0);
	%item.delete();
}

function respawnitem(%spawn,%item)
{
 %spawn.setItem(%item);
 %spawn.setItemPosition(0);
}

function makeitem(%data,%tran)
{
    %item = new Item()
    {
	position = getWord(%tran,0) SPC getWord(%tran, 1)+1 SPC getWord(%tran,2);
	rotation = "0 0 0 0";
	scale = "1 1 1";
	datablock = %data;
	static = 0;
	rotate = 0;
    };
}