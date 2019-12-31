//flyingcap.cs

//This item is mostly based off the rocket skis scrpt, by muffinmix.
//The script was organized and edited by -=REDco=-Captain.
//Model made by -=REDco=-Captain.
//Specific lines of code by other users are credited withen the script.
//Big thanks to Aloshi for his contribution.

//Edit 8/8/08: Re-packaged for v9. Some edits made to get it to work.

///////////////////
//basic item data//
///////////////////

datablock ItemData(flyingcapItem)
{
category = "Item";  // Mission editor category
equipment = true;

// Basic Item Properties
shapeFile = "./shapes/flyingcap2.dts";
mass = 1;
density = 0.2;
elasticity = 0.2;
friction = 0.6;
emap = true;

//gui stuff
uiName = "Flying Cap";
iconName = "./ItemIcons/flyingcap";
doColorShift = false;

// Dynamic properties defined by the scripts
image = flyingcapWeaponImage;
canDrop = true;
};

//////////////
//sound data//
//////////////

//datablock AudioProfile(flyingcapIdleSound)
//{
//   filename    = "./sound/wingcap.wav";
//   description = AudioCloseLooping3d;
//   preload = true;
//};

datablock AudioProfile(herewegoSound)
{
   filename    = "./sound/herewego.wav";
   description = AudioClose3d;
   preload = true;
};

//////////////
//image data//
//////////////

datablock ShapeBaseImageData(flyingcapWeaponImage)
{
// Basic Item properties
shapeFile = "./shapes/flyingcap2.dts";
emap = true;

// Specify mount point & offset for 3rd person, and eye offset
// for first person rendering.
mountPoint = 0;
offset = "0 .35 .1";
rotation = eulerToMatrix("0 90 0");
eyeOffset = ".50 .55 -.45";
eyeRotation = eulerToMatrix("0 90 0");
doColorShift = false;

// When firing from a point offset from the eye, muzzle correction
// will adjust the muzzle vector to point to the eye LOS point.
// Since this weapon doesn't actually fire from the muzzle point,
// we need to turn this off. 
correctMuzzleVector = false;

// Add the WeaponImage namespace as a parent, WeaponImage namespace
// provides some hooks into the inventory system.
className = "WeaponImage";

// Projectile && Ammo.
item = flyingcapItem;
//melee particles shoot from eye node for consistancy
melee = true;
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
stateName[0]                      = "Activate";
stateTimeoutValue[0]              = 0.5;
stateTransitionOnTimeout[0]       = "Ready";

stateName[1]                      = "Ready";
stateTransitionOnTriggerDown[1]   = "Fire";
stateAllowImageChange[1]          = true;

stateName[2]                      = "Fire";
stateTransitionOnTriggerUp[2]     = "Ready";
stateScript[2]                    = "onFire";

//stateSequence[3]		 	    = "Active";
//stateSound[3]			    = flyingcapIdleSound;
};

datablock ShapeBaseImageData(flyingcapHatImage)
{
// Basic Item properties
shapeFile = "./Shapes/flyingcap2.dts";
emap = true;

// Specify mount point & offset for 3rd person, and eye offset
// for first person rendering.
mountPoint = $HeadSlot;
offset = "0 0 0";
eyeOffset = "0 0 5";
rotation = eulerToMatrix( "0 0 0" );
scale = "3 3 3";
offset = "0.0095 0.047 .325";
doColorShift = false;
};

////////////////
//vehicle data//
////////////////

datablock WheeledVehicleSpring(flyingcapSpring)
{
// Wheel suspension properties
length = 0.2;                // Suspension travel
force = 195; //3000;         // Spring force
damping = 60; //600;         // Spring damping
antiSwayForce = 0; //3;      // Lateral anti-sway force
};

datablock WheeledVehicleData(flyingcapVehicle)
{
//tagged fields
doSimpleDismount = true; //just unmount the player, dont look for a free space
category = "Vehicles";
shapeFile = "./shapes/SkiVehicle.dts";
emap = true;
maxDamage = 1.0;
destroyedLevel = 0.5;
maxSteeringAngle = 1.0;

// 3rd person camera settings
cameraRoll = false;    //Roll the camera with the vehicle?
cameraMaxDist = 11;    //Far distance from vehicle
cameraOffset = 6.8;    //Vertical offset from camera mount point
cameraLag = 0.0;       //Velocity lag of camera
cameraDecay = 1.75;    //Decay per sec. rate of velocity lag
cameraTilt = 0.3201;   //tilt adjustment for camera: ~20 degrees down

// Rigid Body
mass = 100;
density = 7;
massCenter = "0.0 0.0 0.5";  // Center of mass for rigid body
massBox = "1.5 1.5 1.5";     // Size of box used for moment of inertia,
drag = 5.0;                  // Drag coefficient
bodyFriction = 0.5;
bodyRestitution = 0.0;	     // The bounce effect.
minImpactSpeed = 3;          // Impacts over this invoke the script callback
integration = 10;            // Physics integration: TickSec/Rate
collisionTol = 0.1;          // Collision distance tolerance
contactTol = 0.01;           // Contact velocity tolerance


isSled = false; 
// Engine
engineTorque = 4000;      // Engine power
engineBrake = 2040;        // Braking when throttle is 0
brakeTorque = 4040;        // When brakes are applied
maxWheelSpeed = 40;       // Engine scale by current speed / max speed
forwardThrust = 5000;
reverseThrust = 5000;
lift = 100;
maxForwardVel = 50;
maxReverseVel = 15;
horizontalSurfaceForce = 130;
verticalSurfaceForce = 130;
rollForce = 400;
yawForce = 600;
pitchForce = 600;
rotationalDrag = 1.5;
stallSpeed = 0;
jumpForce = 100;          //havent added this into code yet.


// Energy
maxEnergy = 100;
jetForce = 3000;
minJetEnergy = 30;
jetEnergyDrain = 2;

steeringUseStrafeSteering = false; //this vehicle has pitch control, so we can't use strafe steering
};

///////////////////
//start functions//
///////////////////

function flyingcapWeaponImage::onFire(%this, %obj, %slot)
{
%player = %obj;
if(%player.isMounted())
{
%mountedVehicleName = %player.getObjectMount().getDataBlock().getName();
if(%mountedVehicleName !$= "flyingcapVehicle")
{
//we're mounted on some other kind of vehicle
commandToClient(%player.client, 'CenterPrint', "\c4Can\'t use the Flying Cap right now.", 2);
//messageClient(%player.client, 'Clientmsg', 'Can\'t use Flying Cap right now.');
return;
}
else
{
//we're mounted on a flyingcapVehicle, so stop flying
%player.stopflyingcap();
%player.unMount();
}}
else
{
//we are not mounted on anything
if(vectorLen(%player.getVelocity()) <= 50)
{
%player.startflyingcap();
//messageClient(%player.client, 'MsgEquipInv', '', %InvPosition);
commandToClient(%player.client,'setScrollMode', -1);
//%player.isEquiped[%invPosition] = true;
%player.unMountimage(%slot);
fixArmReady(%player);
for(%i = 0;$hat[%i] !$= "";%i++)
{

%player.hideNode($hat[%i]);
%player.hideNode($accent[%i]);
//The above two lines are thanks to masterlegodude.

}
%player.MountImage(flyingcapHatImage,2);
%obj.playAudio(1,herewegoSound);

//%obj.playAudio(0,flyingcapIdleSound);
//The above line is thanks to trader's help.

%obj.playthread(3, crouch);

}
else
{
//echo(" velocity = ", vectorLen(%player.getVelocity()));
commandToClient(%player.client, 'CenterPrint', "\c4Can\'t use the Flying Cap while moving.", 2);
//messageClient(%player.client, 'CenterPrint', 'Can\'t use  the Flying Cap while moving.');
}}}

function flyingcapItem::onUse(%this, %player, %InvPosition)
{
%playerData = %player.getDataBlock();
%client = %player.client;
if(%player.getObjectMount())
%mountedVehicleName = %player.getObjectMount().getDataBlock().getName();
%player.updateArm(flyingcapWeaponImage);
%player.MountImage(flyingcapWeaponImage, 0);
return;
if(%player.isMounted())
{
%mountedVehicleName = %player.getObjectMount().getDataBlock().getName();
if(%mountedVehicleName !$= "flyingcapVehicle")
{
//we're mounted on some other kind of vehicle
messageClient(%player.client, 'Clientmsg', 'Can\'t use the Flying Cap right now.');
return;
}
else
{
//we're mounted on the Flying CapVehicle, so stop flying
%player.stopflyingcap();
}}
else
{
//we are not mounted on anything
if(vectorLen(%player.getVelocity()) <= 0.1)
{
%player.startflyingcap();
messageClient(%player.client, 'MsgEquipInv', '', %InvPosition);
%player.isEquiped[%invPosition] = true;
}
else
{
messageClient(%player.client, 'Clientmsg', 'Can\'t use the Flying Cap while moving.');
}}}

function Player::startflyingcap(%obj)
{
//make a new Flying Cap vehicle and mount the player on it
%client = %obj.client;
%position = %obj.getTransform();
%posX = getword(%position, 0);
%posY = getword(%position, 1);
%posZ = getword(%position, 2);
%rot = getWords(%position, 3, 8);
%posZ += 0.3;
%vel = %obj.getVelocity();
%newcar = new WheeledVehicle()
{
dataBlock = flyingcapvehicle;
client = %client;
initialPosition = %posX @ " " @ %posY @ " " @ %posZ;
};
%newcar.setVelocity(%vel);
%newcar.setTransform(%posX @ " " @ %posY @ " " @ %posZ @ " " @ %rot);
%newcar.schedule(250, mountObject, %obj, 0);
}

function flyingcapVehicle::onAdd(%this,%obj)
{
//mount the nothing tire and flyingcap spring
%obj.setWheelTire(0, nothingtire);
%obj.setWheelTire(1, nothingtire);
%obj.setWheelTire(2, nothingtire);
%obj.setWheelTire(3, nothingtire);
%obj.setWheelSpring(0, flyingcapSpring);
%obj.setWheelSpring(1, flyingcapSpring);
%obj.setWheelSpring(2, flyingcapSpring);
%obj.setWheelSpring(3, flyingcapSpring);
}

/////////////////
//end functions//
/////////////////

function Player::stopflyingcap(%obj)
{
return;
//de-equip-hilight inv slot
%player = %obj;
%playerData = %player.getDataBlock();
%client = %player.client;
for(%i = 0; %i < %playerData.maxItems; %i++) //search through other inv slots
{
if(%player.isEquiped[%i] == true) //if it is equipped then
{
if(%player.inventory[%i] == flyingcap.getId()) //if it is the flyingcap
{
messageClient(%client, 'MsgDeEquipInv', '', %i); //then de-equip it
%player.isEquiped[%i] = false;
break; }
}}}

function flyingcapVehicle::onUnMount(%this, %obj)
{
%obj.delete();
}

function flyingcapVehicle::onDriverLeave(%this, %obj, %player)
{
//echo("flyingcapVehicle::onDriverLeave ", %this, " ", %obj, " player=", %player);
%player.playthread(3, root);

//%player.stopAudio(0);
//The above line is thanks to trader's help.

%player.unmountImage(2);
//The above line is thanks to packer.

%obj.client.applyBodyParts();
%obj.client.applyBodyColors();
//The above two lines are thanks to space guy.


if(isObject(%player))
%player.stopflyingcap();
%obj.setTransform("0 0 -9999");
%obj.schedule(10, delete);
}