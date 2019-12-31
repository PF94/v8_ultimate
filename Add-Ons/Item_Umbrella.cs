datablock ItemData(umbrellaItem)
{
	category = "Item";  // Mission editor category

	equipment = true;

	//its already a member of item namespace so dont break it
	//className = "Item"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./shapes/umbrella.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Umbrella";
	iconName = "./ItemIcons/umbrella";
	doColorShift = true;
	colorShiftColor = "0.200 0.200 0.200 1.000";
	
	 // Dynamic properties defined by the scripts
	image = umbrellaImage;
	canDrop = true;
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(umbrellaImage)
{
   // Basic Item properties
   shapeFile = "./shapes/umbrella.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   rotation = eulerToMatrix("0 0 0");
   eyeOffset = "0 0 0";
   eyeRotation = eulerToMatrix("0 0 0");

   doColorShift = true;
	colorShiftColor = umbrellaItem.colorShiftColor;
   

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = false;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "ToolImage";

   // Projectile && Ammo.
   item = umbrellaItem;

   //melee particles shoot from eye node for consistancy
   melee = true;
   //raise your arm up or not
   armReady = true;

   doColorShift = true;
   colorShiftColor = umbrellaItem.colorShiftColor; //"0.200 0.200 0.200 1.000";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.5;
	stateTransitionOnTimeout[0]       = "Ready";


	stateName[1]                     = "Ready";
	stateScript[1]                  = "onUse";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;


	stateName[2]                    = "Fire";
	stateTransitionOnTriggerUp[2]	= "Ready";
	stateScript[2]                  = "onFire";
};



datablock ShapeBaseImageData(umbrella2Image)
{
   // Basic Item properties
   shapeFile = "./shapes/umbrellaitem.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   rotation = eulerToMatrix("0 0 0");
   eyeOffset = "0 0 0";
   eyeRotation = eulerToMatrix("0 0 0");

   doColorShift = true;
	colorShiftColor = umbrella2Item.colorShiftColor;
   

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = false;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "ToolImage";

   // Projectile && Ammo.
   item = umbrellaItem;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = false;

   doColorShift = true;
   colorShiftColor = umbrellaItem.colorShiftColor; //"0.200 0.200 0.200 1.000";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.5;
	stateTransitionOnTimeout[0]       = "Ready";
};



function umbrellaImage::onFire(%this, %obj, %slot)
{
%client = %obj.client;
if(%client.umbrellaing == 0){
	%client.player.setDataBlock("Playerumbrella"); 
	%client.schedule(0 , stopthread, 0);
schedule(200, 0, checkvel, %client);
clearAllPlayerNodes(%client.player);
assignPPwithC(%client.player, %client);
%client.player.mountimage(umbrella2image, 0);
%client.umbrellaing = 1;
}else{
clearAllPlayerNodes(%client.player);
assignPPwithC(%client.player, %client);
%client.player.setDataBlock("PlayerStandardArmor");
%client.player.setTempColor("1 1 1 1",1);
%client.umbrellaing = 0;
}
}

//Some required functions....

datablock PlayerData(Playerumbrella : PlayerStandardArmor)
{
	canJet = 0;
	drag = 4;
	airControl = 1;
};

function CheckVel(%client)
{
if(!Isobject(%client)) return;
%Zspeed = getWord(%client.player.getvelocity(), 2);
if(%Zspeed == 0)
{
clearAllPlayerNodes(%client.player);
assignPPwithC(%client.player, %client);
%client.player.setDataBlock("PlayerStandardArmor");
%client.player.setTempColor("1 1 1 1",1);
%client.umbrellaing = 0;
return;
}
schedule(150, 0, checkvel, %client);
}




//THE FOLLOWING SCRIPTS ARE BY NITRAMJ, AND DEAL WITH RESETTING APPEARANCE!!
function clearPlayerNodes(%player)
{
	if (isObject(%player))
	{
		for (%i = 0; $accent[%i] !$= ""; %i++) %player.hideNode($accent[%i]);
		for (%i = 0; $chest[%i] !$= ""; %i++) %player.hideNode($chest[%i]);
		for (%i = 0; $hat[%i] !$= ""; %i++) %player.hideNode($hat[%i]);
		for (%i = 0; $hip[%i] !$= ""; %i++) %player.hideNode($hip[%i]);
		for (%i = 0; $LArm[%i] !$= ""; %i++) %player.hideNode($LArm[%i]);
		for (%i = 0; $LHand[%i] !$= ""; %i++) %player.hideNode($LHand[%i]);
		for (%i = 0; $LLeg[%i] !$= ""; %i++) %player.hideNode($LLeg[%i]);
		for (%i = 0; $pack[%i] !$= ""; %i++) %player.hideNode($pack[%i]);
		for (%i = 0; $RArm[%i] !$= ""; %i++) %player.hideNode($RArm[%i]);
		for (%i = 0; $RHand[%i] !$= ""; %i++) %player.hideNode($RHand[%i]);
		for (%i = 0; $RLeg[%i] !$= ""; %i++) %player.hideNode($RLeg[%i]);
		for (%i = 0; $secondPack[%i] !$= ""; %i++) %player.hideNode($secondPack[%i]);
	}
}

function clearAllPlayerNodes(%player)
{
	if (isObject(%player))
	{
		clearPlayerNodes(%player);
		%player.hideNode("headSkin");
		%player.hideNode("LSki");
		%player.hideNode("RSki");
		%player.hideNode("skirtTrimLeft");
		%player.hideNode("skirtTrimRight");
	}
}

function assignPPwithC(%player, %client)
{
	if (isObject(%player) && isObject(%client))
	{
	echo("dd");
		%player.unHideNode($accent[%client.accent]);
		%player.setNodeColor($accent[%client.accent], %client.accentColor);
		%player.unHideNode($chest[%client.chest]);
		%player.setNodeColor($chest[%client.chest], %client.chestColor);
		%player.unHideNode($hat[%client.hat]);
		%player.setNodeColor($hat[%client.hat], %client.hatColor);
		%player.unHideNode($hip[%client.hip]);
		%player.setNodeColor($hip[%client.hip], %client.hipColor);
		%player.unHideNode($larm[%client.larm]);
		%player.setNodeColor($larm[%client.larm], %client.larmColor);
		%player.unHideNode($lhand[%client.lhand]);
		%player.setNodeColor($lhand[%client.lhand], %client.lhandColor);
		%player.unHideNode($lleg[%client.lleg]);
		%player.setNodeColor($lleg[%client.lleg], %client.llegColor);
		%player.unHideNode($pack[%client.pack]);
		%player.setNodeColor($pack[%client.pack], %client.packColor);
		%player.unHideNode($rarm[%client.rarm]);
		%player.setNodeColor($rarm[%client.rarm], %client.rarmColor);
		%player.unHideNode($rhand[%client.rhand]);
		%player.setNodeColor($rhand[%client.rhand], %client.rhandColor);
		%player.unHideNode($rleg[%client.rleg]);
		%player.setNodeColor($rleg[%client.rleg], %client.rlegColor);
		%player.unHideNode($secondpack[%client.secondpack]);
		%player.setNodeColor($secondpack[%client.secondpack], %client.secondpackColor);
		%player.unHideNode("headSkin");
		%player.setNodeColor("headSkin", %client.headColor);
		%player.setFaceName(%client.faceName);
		return %player;
	}
}
