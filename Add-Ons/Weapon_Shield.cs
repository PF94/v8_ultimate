datablock ShapeBaseImageData(ShieldFrontImage)
{
   // Basic Item properties
   shapeFile = "base/data/shapes/brickweapon.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 1;
   offset = "0 0 0";
   eyeOffset = 0; //"0.7 1.2 -0.5";
   rotation = eulerToMatrix( "0 0 90" );
   scale = "3 3 3";
   offset = "0.5 0.1 0";

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this Weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = " ";
   ammo = " ";
   projectile = "gunProjectile";
   projectileType = "Projectile";

	casing = "";
	//shellExitDir        = "1.0 -1.3 1.0";
	//shellExitOffset     = "0 0 0";
	//shellExitVariance   = 15.0;	
	//shellVelocity       = 7.0;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;

   doColorShift = true;
   colorShiftColor = "0.196 0.196 0.196 1.000";

   //casing = " ";

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
	stateSound[0]					= WeaponSwitchSound;

	stateName[1]                     = "Ready";
	stateAllowImageChange[1]         = true;
	stateSequence[1]	= "Ready";

};

function SwordImage::onMount(%this,%obj,%slot)
{
 %obj.mountImage(ShieldFrontImage,1);
 %obj.playthread(0,"armreadyboth");
}

function SwordImage::onUnMount(%this,%obj,%slot)
{
 if(%obj.getMountedImage(1) $= nametoID(ShieldFrontImage)){%obj.unmountimage(1);%obj.playthread(0,root);}
}

package ShieldOverride
{
 function ProjectileData::damage(%this,%obj,%col,%fade,%pos,%normal) 
 {
  if(%col.getMountedImage(1) !$= nametoID(shieldfrontimage) || (%col.getClassname() !$= "Player" && %col.getClassname() !$= "AIPlayer")){
  Parent::damage(%this,%obj,%col,%fade,%pos,%normal);return;
  }
  else
  {
   %damLoc = %col.getDamageLocation(%pos);
   if(getword(%pos, 2) > getword(%col.getWorldBoxCenter(), 2) - 3.3 || getword(%pos, 2) < getword(%col.getWorldBoxCenter(),2) - 4.4 || strstr(%damLoc, "left") $= "-1")
   {
    Parent::damage(%this,%obj,%col,%fade,%pos,%normal);return;
   }
   else
   {
    return;
   }
  }
 }
};
activatepackage(ShieldOverride);