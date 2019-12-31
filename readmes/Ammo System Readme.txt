Any modifications to this are allowed. Give credit to Space Guy.

Simple install into the Blockland folder (it extracts itself into Add-Ons automatically) and enable it when starting a server. It makes the Gun, Bow and Rocket Launcher have limited ammo, Auto Rocket Launcher use the same rockets if you have it, and add these two commands:

/inv - view current list of ammo.
/use NAME - NAME is the short name given in the /inv list. This only works for woodarrow and firearrow, setting the bow's ammo type to that arrow, and any others added via the code below.

Fire arrows do 50 damage over time, but killing someone with it will not give you points. When you run out of ammo collect an "Ammo Item", which gives you a random type out of the types you have and up to 15 shots of it. You can also get ammo pickups when a player dies [players only, I don't think it works for bots] and if you die and respawn.

/////////////////////////////////To add Ammo Types: [Non-coders may not understand this!]\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
To add a new ammo type, copy this:
$inv[4,0] = nametoID(genericProjectile);			//Projectile
$inv[4,1] = "Ammo Name";				//Inv Name
$inv[4,2] = "Shortcut Name";				//Shortcut Name
$inv[4,3] = 3;					//Amount

and paste it on line 24 of AddOn_WeaponAmmo.cs. (Between $invX = 4 and the line above.) Change the 4's to 5's ($inv[5,0] etc) and change the $invX = 4; to $invX = 5;. Replace the "genericProjectile" to the projectile your weapon fires i.e. minigunProjectile or hookshotProjectile, found somewhere around line 129 of the weapon's .cs file. Replace "Ammo Name" with the name it should say when selected. (Ex. Minigun Ammo or Hookshot Arrows),change Shortcut Name to the one shown next to it. (miniammo or hookarrow) and the Amount (3, here) with how many a player should spawn with.

To add multiple types of ammo for a single weapon, use these lines after the ones above:

$inv[4,4] = nametoID(genericImage);			//Override image
$inv[4,5] = "Weapon Name";

Replace genericImage with the name of your weapon's "image" (around line 191 and something like "bowImage".) and the name with the weapon name.