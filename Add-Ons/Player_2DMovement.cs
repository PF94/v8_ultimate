//Movement is locked to the angle at which you spawned. Camera is placed to the side. Always 'third person' according to the player. (An invisible AIPlayer is used for the camera)
//Player has four 'hit points', becoming invincible for 1.5 seconds when hit. Every weapon does 1 damage to the player, except for those doing above 900 damage. ('Instant Kill' weapons or death bricks)

$Player2d::CameraScroll = 0; //Set to a number if you want the camera to 'scroll' as you move.

if(!isEventPending($spaceTick))
{
 if(isFile("Add-Ons/Support_SpaceTick.cs.noexec"))
 {
  exec("Add-Ons/Support_SpaceTick.cs.noexec");
 }
 else
 {
  clientcmdMessageBoxOK("2D Player","Add-Ons/Support_SpaceTick.cs.noexec not found. Reinstall the mod. Correctly.");
  return;
 }
}

function TwoDCtrlSO::addValue(%this,%obj)
{
 %this.value[%this.count] = %obj;
 %this.count++;
}

function TwoDCtrlSO::delValue(%this,%index)
{
 if(%index < 0 || %index > %this.count){error("[BotCtrlSO::delValue] ERROR: Array index out of bounds. " @ %index @ "/" @ %this.count);return;}
 %this.value[%index] = "";
 for(%i = %index+1;%i<%this.count;%i++)
 {
  %this.value[%i-1] = %this.value[%i];
 }
 %this.count--;
}

function TwoDCtrlSO::delValueID(%this,%obj)
{
 for(%i=0;%i<%this.count;%i++)
 {
  if(%this.value[%i] $= %obj)
  {
   %this.value[%i] = "";
   for(%j=%i+1;%j<%this.count;%j++)
   {
    %this.value[%j-1] = %this.value[%j];
   }
   %this.count--;
   return;
  }
 }
 warn("[TwoDCtrlSO::delValueID()] " @ %obj @ " does not exist in the stack.");
}

function TwoDCtrlSO::getValue(%this,%index)
{
 if(%index < 0 || %index > %this.count){error("[TwoDCtrlSO::getValue] ERROR: Array index out of bounds. " @ %index @ "/" @ %this.count);return -1;}
 return %this.value[%index];
}

function TwoDCtrlSO::dumpStack(%this)
{
 echo("[TwoDCtrlSO::dumpStack()]");
 echo("Total Values: " @ %this.count);
 for(%i=0;%i<%this.count;%i++)
 {
  echo(">Value " @ %i @ ": " @ %this.value[%i]);
 }
}

function getLeftRight(%a,%b)
{
     %t = mATan(getWord(%a,0),getWord(%a,1))*180/$pi;
     %n = mATan(getWord(%b,0),getWord(%b,1))*180/$pi;
     if(%n-%t < -180){%n+=360;}
     if(%n-%t > 180){%n-=360;}
     if(%n-%t<0){return 1;}else if(%n-%t>0){return -1;}else{return 0;}
}

function TwoDCtrlSO::tick(%this)
{
 %this.updatetimer++;
 if(%this.updatetimer == 20){%this.updatetimer = 0;}
 for(%i=0;%i<%this.count;%i++)
 {
  %obj = %this.getValue(%i);
  if(!isObject(%obj) || %obj.getState() $= "Dead"){%this.delValue(%i);continue;}

  if(isEventPending(%obj.damageFlicker) && %this.updatetimer == 0)
  {
   %obj.flickertype = !%obj.flickertype;
   if(%obj.flickertype){%obj.hidenode("ALL");}else{%obj.client.applybodyparts();%obj.unhidenode("headskin");%obj.client.applybodycolors();}
  }

  initContainerBoxSearch(%obj.getPosition(),"0.25 0.25 0.25",$typemasks::fxbrickobjecttype);
  while((%searchObj = containersearchnext()))
  {
   if((fileName(%searchObj.getDatablock().brickfile) $= "spawnpoint.blb" || %searchObj.getDatablock().isTurn || %searchObj.evParam[%searchObj.curstate,0] $= "Checkpoint") && %searchObj.isPlanted && getField(vecFromBrick(%searchObj),0) !$= %obj.vector)
   {
    %vec = vecFromBrick(%searchObj);
    %obj.vector = getField(%vec,0);
    %obj.invvector = getField(%vec,1);
    %obj.startrot = getField(%vec,2);
    %obj.startpos = posFromTransform(%searchObj.getTransform());
    %this.value[%i] = "";
    for(%j=%i+1;%j<%this.count;%j++)
    {
     %this.value[%j-1] = %this.value[%j];
    }
    %this.count--;
    %obj.client.setControlObject(%obj);
    %obj.cam.setTransform(vectorAdd(%obj.getPosition(),vectorScale(%obj.invvector,15)) SPC rotFromTransform(%obj.cam.getTransform()));
    %obj.cam.setAimLocation(vectorAdd(%obj.getPosition(),"0 0 2"));
    schedule(80,0,setrot,%obj.cam,%obj);
    %exit = 1;break;
   }
  }
  if(%exit){continue;}
  if(%obj.client.getControlObject() $= %obj && isObject(%obj.cam)){%obj.client.setControlObject(%obj.cam);%obj.cam.setControlObject(%obj);}
  %toMe = vectorSub(%obj.startpos,%obj.getPosition());
  %pos = vectorAdd(%obj.startpos,vectorScale(%obj.vector,-vectorDot(%toMe,%obj.vector)));
  %pos = getWords(%pos,0,1) SPC getWord(%obj.getPosition(),2);
   %obj.setTransform(%pos SPC %obj.startrot);
  %b = %obj.geteyevector();
  %aXY0 = "0 1 0";%bXY0 = vectorNormalize(getWords(%b,0,1));
  %r = rotFromTransform(%obj.cam.getTransform());
  %gopos = vectorAdd(vectorAdd(%pos,vectorScale(%obj.invvector,15)),vectorScale(getWords(%obj.getVelocity(),0,1),$Player2d::CameraScroll));
  %ray = containerRayCast(vectorAdd(%obj.getPosition(),"0 0 1"),vectorAdd(%gopos,"0 0 1"),$typemasks::fxbrickobjecttype | $TypeMasks::InteriorObjectType);
  if(isObject(firstWord(%ray)))
  {
   %gopos = vectorAdd(vectorAdd(%pos,vectorScale(%obj.invvector,vectorDist(%pos,posFromRaycast(%ray))*0.8)),vectorScale(getWords(%obj.getVelocity(),0,1),$Player2d::CameraScroll));
  }
  %obj.cam.setTransform(%gopos SPC %r);
 }
 if(!isObject(%this.value0)){%this.delete();}
}

function vecFromBrick(%b)
{
 //forwardvec TAB rotated by 90 degrees TAB angle/transform
 switch(%b.angleID)
 {
  case 0: return "0 1 0" TAB "1 0 0" TAB "0 0 1 0";
  case 1: return "1 0 0" TAB "0 -1 0" TAB "0 0 1" SPC $pi/2;
  case 2: return "0 -1 0" TAB "-1 0 0" TAB "0 0 1" SPC $pi;
  case 3: return "-1 0 0" TAB "0 1 0" TAB "0 0 -1" SPC $pi/2;
 }
}

datablock PlayerData(Player2DNoJet : PlayerStandardArmor)
{
	minJetEnergy = 0;
	jetEnergyDrain = 0;
	canJet = 0;
	maxSideSpeed = 0;
	maxSideCrouchSpeed = 0;
	maxEnergy = 4;
	uiName = "2D-Style Player";
	showEnergyBar = 1;
	maxDamage = 1;
	rechargeRate = 0;
};

datablock PlayerData(Player2DCameraGuy : PlayerStandardArmor)
{
	minJetEnergy = 0;
	jetEnergyDrain = 0;
	canJet = 0;
	firstPersonOnly = 1;
	maxDamage = 90000;
	boundingBox = "0.1 0.1 0.1";
	crouchBoundingBox = "0.1 0.1 0.1";
	speeddamagescale = 0;
	uiName = "";
	maxEnergy = 4;
	showEnergyBar = 1;
	rechargeRate = 0;
	groundimpactminspeed = 0;
	groundimpactshakeamp = "0 0 0";
	upResistFactor = 0;
	drag = 32;
	maxLookAngle = 0;
	minLookAngle = 0;
	density = 50;
};

function Player2DNoJet::onAdd(%this,%obj)
{
 if(!isObject($TwoDCtrl)){$TwoDCtrl = new ScriptObject(){class = TwoDCtrlSO;count = 0;};}
 Parent::onAdd(%this,%obj);
 schedule(80,0,TwoDCheck,%obj);
 %obj.setEnergyLevel(4);
}

function Player2DNoJet::onDisabled(%this,%obj,%a,%b,%c,%d,%e,%f,%g)
{
 if(isObject(%obj.cam)){%obj.cam.delete();}
 Parent::onDisabled(%this,%obj,%a,%b,%c,%d,%e,%f,%g);
}

function Player2dNoJet::onEnterLiquid(%this,%obj)
{
 Parent::onEnterLiquid(%this,%obj);
 if(%obj.getState() !$= "Dead")
 {
  %obj.kill();
 }
}

function Player2dNoJet::damage(%this, %obj, %sourceObject, %position, %damage, %damageType)
{
 if(isEventPending(%obj.damageFlicker)){return;}
 if(%damageType $= $DamageType::Fall || %damagetype $= $Damagetype::groundFall){return;}

 %d = 1;//mCeil(%damage / 100 * 4);
 if(%damage > 900){%d = 4000;} //Instant kill bricks etc
 %damage = 0;
 %curEnergy = %obj.getEnergyLevel();
 if(%d >= %curEnergy)
 {
  %obj.setEnergyLevel(0);
  %damage = 1;
 }
 else
 {
  %obj.setEnergyLevel(%curEnergy - %d);
  cancel(%obj.damageflicker);
  %obj.damageflicker = schedule(2000,0,resetdamageflicker,%obj);
  %obj.flickertype = 0;
  //%obj.settempcolor("0.5 0.5 0.5 1",1500);
  if(isObject(%obj.cam)){%obj.cam.setEnergyLevel(%obj.getEnergyLevel());}
 }
 Parent::damage(%this, %obj, %sourceObject, %position, %damage, %damageType);
 if(%obj.getEnergyLevel() == 2)
 {
  %obj.emote(painMidImage,1);
 }
 if(%obj.getEnergyLevel() < 2)
 {
  %obj.emote(painHighImage,1);
 }
}
function Player2dCameraGuy::damage(%this, %obj, %sourceObject, %position, %damage, %damageType)
{
}
function TwoDCheck(%obj)
{
 if(isObject(%obj.client.camera))
 {
  %obj.startrot = rotFromTransform(%obj.getTransform());
  %obj.startpos = posFromTransform(%obj.getTransform());
  %obj.vector = %obj.getforwardvector();
  %obj.invvector = getWord(%obj.vector,1) SPC -getWord(%obj.vector,0) SPC getWord(%obj.vector,2);
  %obj.cam = new AIPlayer(){datablock = Player2dCameraGuy;position = vectorAdd(%obj.getposition(),vectorScale(%obj.invvector,15));};
  %obj.cam.setAimLocation(vectorAdd(%obj.getposition(),"0 0 2"));
  schedule(80,0,setrot,%obj.cam,%obj);
  %obj.cam.hidenode("ALL");
 }
}

function resetdamageflicker(%obj)
{
 if(isobject(%obj))
 {
  %obj.client.applybodyparts();%obj.unhidenode("headskin");%obj.client.applybodycolors();
  %obj.flickertype = 0;
 }
}

function setrot(%cam,%obj)
{
 if(!isObject(%obj) || %obj.getState() $= "Dead")
  return;
 %obj.client.setControlObject(%cam);
 %cam.setcontrolobject(%obj);
 if(!isObject($TwoDCtrl)){$TwoDCtrl = new ScriptObject(){class = TwoDCtrlSO;count = 0;};}
 $TwoDCtrl.addValue(%obj);
 %obj.cam.setEnergyLevel(%obj.getenergylevel());
}

function getLeftRight(%a,%b)
{
     %t = mATan(getWord(%a,0),getWord(%a,1))*180/$pi;
     %n = mATan(getWord(%b,0),getWord(%b,1))*180/$pi;
     if(%n-%t < -180){%n+=360;}
     if(%n-%t > 180){%n-=360;}
     if(%n-%t<0){return 1;}else if(%n-%t>0){return -1;}else{return 0;}
}

package player2dsuicide
{
 function servercmdsuicide(%client)
 {
  if(isObject(%client.player) && %client.player.getDatablock().getName() $= "Player2dNoJet")
  {
   %client.player.setenergylevel(0);
   %client.player.damageflicker = 0;
  }
  parent::servercmdsuicide(%client);
 }
 function Player::setDatablock(%obj,%a)
 {
  %data = %obj.getDatablock().getName();
  Parent::setDatablock(%obj,%a);
  if(isObject(%obj) && isObject(%a))
  {
   if(%data $= "Player2dNojet" && %a.getName() !$= "Player2dNojet")
   {
    if(isObject($TwoDCtrl)){$TwoDCtrl.delValueID(%obj);}
    if(isObject(%obj.cam)){%obj.cam.delete();%obj.client.setControlObject(%obj);}
   }
   else if(%data !$= "Player2dNojet" && %a.getName() $= "Player2dNojet")
   {
    if(!isObject($TwoDCtrl)){$TwoDCtrl = new ScriptObject(){class = TwoDCtrlSO;count = 0;};}
    schedule(100,0,TwoDCheck,%obj);
    %obj.setEnergyLevel(4);
   }
  }
 }
};activatepackage(player2dsuicide);

//A Turn Brick or brick marked as a "Checkpoint" in Wrench Events turns a 2D player touching it by 90 degrees counterclockwise of whichever way you were facing when you placed it, along the brick grid.
//I don't know exactly why it does it 90 degrees that way, but it works. Just experiment with them.
//Alternately, Spawn Points (and all bricks which look like spawn points such as TDM spawns) rotate you to whichever way the arrow is facing.

datablock fxDTSBrickData(brick2dTurn2x2Data : brick2x2fdata)
{
 uiName = "2D Turn Brick 2x2";
 Category = "Special";
 subCategory = "2D Player";
 isTurn = 1;
};

datablock fxDTSBrickData(brick2dTurn1x1Data : brick1x1fdata)
{
 uiName = "2D Turn Brick 1x1";
 Category = "Special";
 subCategory = "2D Player";
 isTurn = 1;
};