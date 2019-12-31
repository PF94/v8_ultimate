//GUI References:
//-1: No option. ("-------")
//0: Tickbox [Text]
//1: Number [Text, Max Digits]
//2: Text [Text, Default]
//3: Select Box [Text, [Value 0...n %[Data Ref] - Seperator _ Space]
//Example: 3 Particles %ParticleEmitterData - AdminWand_A AdminWand_B
//4: Select Box 2 [Text, PREVIOUS VAL = X : [Value 0...n %[Data Ref] - Seperator _ Space] / PREVIOUS VAL = Y : [Value 0...n %[Data Ref] - Seperator _ Space]]
//Example: 4 Weapons Special : Fire Spread Aiming Artillery_Rocket / Weapons : %ItemData
//5: Direction Chooser [Text, Style [0 = NESW, 1 = UDNESW, 2 = Altitude]]
//6: Color Chooser [Text]

if(!isObject(arrowProjectile)){exec("./Weapon_Bow.cs");}
if($AddOn__Weapon_Bow $= "-1"){schedule(1000,0,eval,"bowitem.uiname = \"\";");}
if(!isObject(gunProjectile)){exec("./Weapon_Gun.cs");}
if($AddOn__Weapon_Gun $= "-1"){schedule(1000,0,eval,"gunitem.uiname = \"\";");}
if(!isObject(rocketLauncherProjectile)){exec("./Weapon_Rocket Launcher.cs");}
if($AddOn__Weapon_Rocket_Launcher $= "-1"){schedule(1000,0,eval,"rocketlauncheritem.uiname = \"\";");}
if(!isObject(alarmSound)){exec("./Emote_Alarm.cs");}
if($AddOn__Emote_Alarm $= "-1"){schedule(1000,0,eval,"function servercmdAlarm(){}");}
if(!isObject(hateImage)){exec("./Emote_Hate.cs");}
if($AddOn__Emote_Hate $= "-1"){schedule(1000,0,eval,"function servercmdHate(){}");}
if(!isObject(loveImage)){exec("./Emote_Love.cs");}
if($AddOn__Emote_Love $= "-1"){schedule(1000,0,eval,"function servercmdLove(){}");}
if(!isObject(wtfImage)){exec("./Emote_Confusion.cs");}
if($AddOn__Emote_Confusion $= "-1"){schedule(1000,0,eval,"function servercmdwtf(){}");}
if($Pref::EventBricks::MaxEventsPlayer $= "")
{
 $Pref::EventBricks::MaxEventsPlayer = 5;
 $Pref::EventBricks::MaxEventsAdmin = 20;
 $Pref::EventBricks::MaxEventsSAdmin = -1;
}


function evSet()
{
%evcount = -1;
//Category: Shows in list
//Cancel: Calls WrenchEvCancel[NAME] when event ends
//GUI: up to 5 GUI options for setting up events
$EventBricks::Server::Category[%evcount++] = "Color";
$EventBricks::Server::Cancel[$EventBricks::Server::Category[%evcount]] = 1;
 $EventBricks::Server::GUI[%evcount,0] = "6 Color:";
 $EventBricks::Server::GUI[%evcount,1] = "3 FX: NONE Pearl Chrome Glow Blink Swirl Rainbow";
 $EventBricks::Server::GUI[%evcount,2] = "0 Undulo: ";
 $EventBricks::Server::GUI[%evcount,3] = "0 Persistent:";
 $EventBricks::Server::GUI[%evcount,4] = "-1";
$EventBricks::Server::Category[%evcount++] = "Particles";
$EventBricks::Server::Cancel[$EventBricks::Server::Category[%evcount]] = 1;
 $EventBricks::Server::GUI[%evcount,0] = "3 Particles: NONE %ParticleEmitterData";
 $EventBricks::Server::GUI[%evcount,1] = "5 Direction: 1";
 $EventBricks::Server::GUI[%evcount,2] = "0 Persistent:";
 $EventBricks::Server::GUI[%evcount,3] = "-1";
 $EventBricks::Server::GUI[%evcount,4] = "-1";
$EventBricks::Server::Category[%evcount++] = "Items";
$EventBricks::Server::Cancel[$EventBricks::Server::Category[%evcount]] = 1;
 $EventBricks::Server::GUI[%evcount,0] = "3 Item: NONE %ItemData";
 $EventBricks::Server::GUI[%evcount,1] = "5 Position: 1";
 $EventBricks::Server::GUI[%evcount,2] = "5 Facing: 0";
 $EventBricks::Server::GUI[%evcount,3] = "0 Persistent:";
 $EventBricks::Server::GUI[%evcount,4] = "-1";
$EventBricks::Server::Category[%evcount++] = "Lights";
$EventBricks::Server::Cancel[$EventBricks::Server::Category[%evcount]] = 1;
 $EventBricks::Server::GUI[%evcount,0] = "3 Light: NONE %FxLightData";
 $EventBricks::Server::GUI[%evcount,1] = "0 Persistent:";
 $EventBricks::Server::GUI[%evcount,2] = "-1";
 $EventBricks::Server::GUI[%evcount,3] = "-1";
 $EventBricks::Server::GUI[%evcount,4] = "-1";
$EventBricks::Server::Category[%evcount++] = "Sound";
 $EventBricks::Server::GUI[%evcount,0] = "3 Type: NONE Music Notes Other";
 $EventBricks::Server::GUI[%evcount,1] = "4 Sound: !Music %AudioProfile !Notes 0 1 2 3 4 5 6 7 8 9 10 11 !Other Alarm Reward LightOn LightOff Click";
 $EventBricks::Server::GUI[%evcount,2] = "-1";
 $EventBricks::Server::GUI[%evcount,3] = "-1";
 $EventBricks::Server::GUI[%evcount,4] = "-1";
$EventBricks::Server::Category[%evcount++] = "Emotes";
 $EventBricks::Server::GUI[%evcount,0] = "3 Type: Alarm Confuse Love Hate Burn Win Bricks";
 $EventBricks::Server::GUI[%evcount,1] = "-1";
 $EventBricks::Server::GUI[%evcount,2] = "-1";
 $EventBricks::Server::GUI[%evcount,3] = "-1";
 $EventBricks::Server::GUI[%evcount,4] = "-1";
$EventBricks::Server::Category[%evcount++] = "Hurt";
 $EventBricks::Server::GUI[%evcount,0] = "1 DPS: 3";
 $EventBricks::Server::GUI[%evcount,1] = "0 Kill:";
 $EventBricks::Server::GUI[%evcount,2] = "0 You_get_Points:";
 $EventBricks::Server::GUI[%evcount,3] = "3 Type: None Fall Smack Suicide Star Trophy Ribbon Car Generic %ItemData";
 $EventBricks::Server::GUI[%evcount,4] = "-1";
$EventBricks::Server::Category[%evcount++] = "Door";
$EventBricks::Server::Cancel[$EventBricks::Server::Category[%evcount]] = 1;
 $EventBricks::Server::GUI[%evcount,0] = "0 Explode_FX:";
 $EventBricks::Server::GUI[%evcount,1] = "0 Invisible";
 $EventBricks::Server::GUI[%evcount,2] = "0 Decollide";
 $EventBricks::Server::GUI[%evcount,3] = "-1";
 $EventBricks::Server::GUI[%evcount,4] = "-1";
$EventBricks::Server::Category[%evcount++] = "Impulse";
 $EventBricks::Server::GUI[%evcount,0] = "1 Speed: 2";
 $EventBricks::Server::GUI[%evcount,1] = "5 Direction: 0";
 $EventBricks::Server::GUI[%evcount,2] = "5 Attitude: 2";
 $EventBricks::Server::GUI[%evcount,3] = "0 Add_to_Velocity:";
 $EventBricks::Server::GUI[%evcount,4] = "-1";
$EventBricks::Server::Category[%evcount++] = "Teleport";
 $EventBricks::Server::GUI[%evcount,0] = "2 Group: ";
 $EventBricks::Server::GUI[%evcount,1] = "-1";
 $EventBricks::Server::GUI[%evcount,2] = "-1";
 $EventBricks::Server::GUI[%evcount,3] = "-1";
 $EventBricks::Server::GUI[%evcount,4] = "-1";
$EventBricks::Server::Category[%evcount++] = "Shoot";
 $EventBricks::Server::GUI[%evcount,0] = "3 Type: Weapons Specials";
 $EventBricks::Server::GUI[%evcount,1] = "4 Projectile: !Weapons %ItemData !Specials Artillery_Rocket Homing_Turret Spread_Turret Spike Fire Fizzler";
 $EventBricks::Server::GUI[%evcount,2] = "5 Direction: 0";
 $EventBricks::Server::GUI[%evcount,3] = "5 Attitude: 2";
 $EventBricks::Server::GUI[%evcount,4] = "1 Shots/sec: 2";
$EventBricks::Server::Category[%evcount++] = "Triggers";
 $EventBricks::Server::GUI[%evcount,0] = "3 Type: Touch Click Laser IR Shot Proximity Auto";
 $EventBricks::Server::GUI[%evcount,1] = "2 Group: ";
 $EventBricks::Server::GUI[%evcount,2] = "5 Direction: 1";
 $EventBricks::Server::GUI[%evcount,3] = "-1";
 $EventBricks::Server::GUI[%evcount,4] = "-1";
$EventBricks::Server::Category[%evcount++] = "Wait For";
 $EventBricks::Server::GUI[%evcount,0] = "3 Type: Triggered Time Touch Click Laser IR Shot Proximity Impact";
 $EventBricks::Server::GUI[%evcount,1] = "5 Direction: 1";
 $EventBricks::Server::GUI[%evcount,2] = "0 Loop_Last:";
 $EventBricks::Server::GUI[%evcount,3] = "-1";
 $EventBricks::Server::GUI[%evcount,4] = "-1";
$EventBricks::Server::Category[%evcount++] = "Admin Only Misc";
$EventBricks::Server::Admin[$EventBricks::Server::Category[%evcount]] = 1;
 $EventBricks::Server::GUI[%evcount,0] = "3 Type: Lose_Inventory Checkpoint";
 $EventBricks::Server::GUI[%evcount,1] = "-1";
 $EventBricks::Server::GUI[%evcount,2] = "-1";
 $EventBricks::Server::GUI[%evcount,3] = "-1";
 $EventBricks::Server::GUI[%evcount,4] = "-1";
}
evset();

if(!isEventPending($spaceTick))
{
 if(isFile("Add-Ons/Support_SpaceTick.cs.noexec"))
 {
  exec("Add-Ons/Support_SpaceTick.cs.noexec");
 }
 else
 {
  clientcmdMessageBoxOK("Wrench Events","Add-Ons/Support_SpaceTick.cs.noexec not found. Reinstall the mod. Correctly.");
  return;
 }
}

function WrenchEventCtrlSO::addValue(%this,%obj)
{
 %this.value[%this.count] = %obj;
 %this.count++;
}

function WrenchEventCtrlSO::delValue(%this,%index)
{
 if(%index < 0 || %index > %this.count){error("[WrenchEventCtrlSO::delValue] ERROR: Array index out of bounds. " @ %index @ "/" @ %this.count);return;}
 %this.value[%index] = "";
 for(%i = %index+1;%i<%this.count;%i++)
 {
  %this.value[%i-1] = %this.value[%i];
 }
 %this.count--;
}

function WrenchEventCtrlSO::delValueID(%this,%obj)
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
 warn("[WrenchEventCtrlSO::delValueID()] " @ %obj @ " does not exist in the stack.");
}

function WrenchEventCtrlSO::getValue(%this,%index)
{
 if(%index < 0 || %index > %this.count){error("[WrenchEventCtrlSO::getValue] ERROR: Array index out of bounds. " @ %index @ "/" @ %this.count);return -1;}
 return %this.value[%index];
}

function WrenchEventCtrlSO::dumpStack(%this)
{
 echo("[WrenchEventCtrlSO::dumpStack()]");
 echo("Total Values: " @ %this.count);
 for(%i=0;%i<%this.count;%i++)
 {
  echo(">Value " @ %i @ ": " @ %this.value[%i]);
 }
}

function WrenchEventCtrlSO::tick(%this)
{
 for(%i=0;%i<%this.count;%i++)
 {
  %obj = %this.getValue(%i);
  if(!isObject(%obj) || %obj.isDead()){%this.delValue(%i);continue;}
  if(%obj.evState[0] $= ""){continue;}
  if(%obj.evState[%obj.curState] $= ""){%obj.curState = 0;%obj.ticknum = 0;}
  if(%obj.evState[%obj.curState] !$= "" && %obj.ticknum >= 0 && !%obj.isFakeDead()){call("WrenchEv" @ strReplace(%obj.evName[%obj.curState]," ","_"),%obj,%obj.ticknum,%obj.curState);}
  %obj.ticknum++;
  %obj.ticknum = %obj.ticknum % 2000000;
  if(%obj.ticknum >= %obj.evState[%obj.curState]*10)
  {
   %obj.ticknum = 0;
   if(%obj.evName[%obj.curState] !$= "Wait For"){if($EventBricks::Server::Cancel[%obj.evName[%obj.curState]]){call("WrenchEvCancel" @ strReplace(%obj.evName[%obj.curState]," ","_"),%obj,%obj.curState);}%obj.curstate++;}
  }
 }
 if(!isObject(%this.value[0])){%this.delete();}
}

package WrenchEvents
{
 function paintProjectile::onCollision(%this,%obj,%col,%pos,%fade,%normal)
 {
  Parent::onCollision(%this,%obj,%col,%pos,%fade,%normal);
 }
 function GameConnection::onConnect(%this,%a,%b,%c,%d,%e,%f,%g)
 {
  Parent::onConnect(%this,%a,%b,%c,%d,%e,%f,%g);
  schedule(5000,0,clientjoinWrench,%this);
 }
 function clientjoinWrench(%this)
 {
  if(!isObject(%this)){return;}
  for(%i=0;$EventBricks::Server::Category[%i] !$= "";%i++)
  {
   commandtoclient(%this,'WrenchEventCat',
    %i,
    $EventBricks::Server::Category[%i],
    $EventBricks::Server::GUI[%i,0],
    $EventBricks::Server::GUI[%i,1],
    $EventBricks::Server::GUI[%i,2],
    $EventBricks::Server::GUI[%i,3],
    $EventBricks::Server::GUI[%i,4]);
  }
 }
 function Player::activateStuff(%this)
 {
  Parent::activateStuff(%this);
 }
 function Armor::onTrigger(%data,%obj,%slot,%on)
 {
  Parent::onTrigger(%data,%obj,%slot,%on);
  if(!%on || %slot !$= "0" || isObject(%obj.getMountedImage(0))){return;}
  %this = %obj;
  %mouseVec = %this.getEyeVector();
  %cameraPoint = %this.getEyePoint();
  %selectRange = 8;
  %mouseScaled = VectorScale(%mouseVec, %selectRange);
  %rangeEnd = VectorAdd(%cameraPoint, %mouseScaled);
  %searchMasks = $TypeMasks::FXBrickObjectType;
  %scanTarg = ContainerRayCast (%cameraPoint, %rangeEnd, %searchMasks);
  if(!isObject(firstWord(%scanTarg))){return;}
  if(strStr(firstWord(%scanTarg).getClassName(),"fxDTSBrick") !$= -1)
  {
   %brick = firstWord(%scanTarg);
   for(%i=0;%i<10;%i++)
   {
    if((%brick.evName[%i] $= "Triggers" || %brick.evName[%i] $= "Wait For") && %brick.evParam[%i,0] $= "Click" && %brick.clickActivated < 6)
    {
     if((%str = wrenchTrust(%brick,%obj.client)) !$= "1")
     {
      commandtoclient(%obj.client,'centerprint',%str,2,3);
      return;
     }
     %brick.clickActivated++;return;
    }
   }
  }
 }
 function ProjectileData::onCollision(%this,%obj,%brick,%fade,%pos,%normal)
 {
  Parent::onCollision(%this,%obj,%brick,%fade,%pos,%normal);
  if(!isObject(%brick) || %brick.getClassName() !$= "fxDTSBrick" || %brick.isFakeDead()){return;}
  %i = %brick.curState;
    if((%brick.evName[%i] $= "Triggers" || %brick.evName[%i] $= "Wait For") && %brick.evParam[%i,0] $= "Shot")
    {
     %brick.shotActivated = 1;
    }
 }
};activatePackage(wrenchEvents);

function servercmdWrenchEventAdd(%client,%type,%length,%group,%e0,%e1,%e2,%e3,%e4)
{
 if(!isObject($wrenchCtrl)){$wrenchCtrl = new ScriptObject(){class = WrenchEventCtrlSO;count = 0;};}
 if(!isObject(%client.wrenchbrick)){return;}
 for(%i=0;%i<$wrenchCtrl.count;%i++){if($wrenchCtrl.getValue(%i).getGroup().client $= %client && $wrenchCtrl.getValue(%i) !$= %client.wrenchbrick){%found++;}}
 %max = $Pref::EventBricks::MaxEventsPlayer;
 if(%client.isAdmin){%max = $Pref::EventBricks::MaxEventsAdmin;}
 if(%client.isSuperAdmin){%max = $Pref::EventBricks::MaxEventsSAdmin;}
 if(%found > %max && %max !$= "-1")
 {
  commandtoclient(%client,'messageboxok',"Error","You have too many event bricks! (" @ %max @ ")");
  return;
 }
 if(%length < 1){commandtoclient(%client,'messageboxok',"Error","Events must go on at least 100ms.");return;}
 if(%length > 50 && !%client.isAdmin && !%client.isSuperAdmin){commandtoclient(%client,'messageboxok',"Error","Non-Admins can only have events which run for 5 seconds at a time.");%length = 50;}
 if(%length > 100){commandtoclient(%client,'messageboxok',"Error","You can only have events which run for 10 seconds at a time.");%length = 100;}
 for(%i=0;$EventBricks::Server::Category[%i] !$= "";%i++){if($EventBricks::Server::Category[%i] $= %type){%found = 1;}}
 if(!%found){return;}

 if($EventBricks::Server::Admin[%type] && !%client.isAdmin && !%client.isSuperAdmin){commandtoclient(%client,'messageboxok',"Error","Only Admins can use this event for bricks.");return;}

 if(%client.wrenchbrick.maxStates >= 10){return;}
 %brick = %client.wrenchbrick;
 if(%brick.maxstates $= ""){%brick.maxStates = 0;}
 %state = %client.wrenchbrick.maxstates;
 %brick.evName[%state] = %type;
 for(%i=0;%i<5;%i++){%brick.evParam[%state,%i] = %e[%i];}
 %brick.evState[%state] = %length;
 %brick.maxStates++;
 %brick.evGroup = %group;
 %brick.ticknum = -1;%brick.curstate = 0;
 %brick.setColor = %brick.getColorID();
 %brick.setFX = %brick.getColorFXID();
 %brick.setShape = %brick.shapefxid;
 %brick.setLight = (isObject(%brick.light) ? %brick.light.getDatablock() : -1);
 %brick.setParts = (isObject(%brick.emitter) ? %brick.emitter.emitter : -1);
 %brick.dirParts = %brick.emitterDirection;
 %brick.setItem = (isObject(%brick.item) ? %brick.item.getDatablock() : -1);
 %brick.dirItem = %brick.itemDirection;
 %brick.posItem = %brick.itemPosition;

 if(%brick.evGroup !$= "")
 {
  for(%i=0;%i<$WrenchCtrl.count;%i++)
  {
   %a = $wrenchctrl.getValue(%i);
   if(%a.evGroup $= %brick.evGroup)
   {
    %a.ticknum = 1;
    if($EventBricks::Server::Cancel[%a.evName[%a.curstate]])
    {
     call("WrenchEvCancel" @ strReplace(%a.evName[%a.curState]," ","_"),%a,%a.curState);
    }
    %a.curstate = 0;
   }
  }
 }
 for(%i=0;%i<$wrenchctrl.count;%i++){if($wrenchctrl.getvalue(%i) $= %brick){return;}}$wrenchCtrl.addValue(%brick);
}

function servercmdWrenchEvGroup(%client,%group,%trust)
{
 if(!isObject($wrenchCtrl)){$wrenchCtrl = new ScriptObject(){class = WrenchEventCtrlSO;count = 0;};}
 if(!isObject(%client.wrenchbrick)){return;}
 %brick = %client.wrenchbrick;
 %brick.evGroup = %group;
 %brick.evTrust = %trust;
 for(%i=0;%i<$wrenchctrl.count;%i++){if($wrenchctrl.getvalue(%i) $= %brick){return;}}$wrenchCtrl.addValue(%brick);
}

function servercmdClearWrenchEvents(%client)
{
 if(!isObject($wrenchCtrl)){$wrenchCtrl = new ScriptObject(){class = WrenchEventCtrlSO;count = 0;};}
 if(!isObject(%client.wrenchbrick)){return;}
 %brick = %client.wrenchbrick;
 if(%brick.maxStates > 0){$wrenchCtrl.delValueID(%brick);}
 %brick.maxStates = 0;
  %brick.evGroup[%i] = "";
  %brick.setColliding(1);
  //if(%brick.setColor !$= ""){%brick.setColor(%brick.setColor);%brick.setColorFX(%brick.setFX);%brick.setShapeFX(%brick.setShape);}
  //if(%brick.setItem !$= ""){%brick.setItem(%brick.setItem);%brick.setItemDirection(%brick.dirItem);%brick.setItemPosition(%brick.posItem);}
  //if(%brick.setParts !$= ""){%brick.setEmitter(%brick.setParts);%brick.setEmitterDirection(%brick.dirParts);}
  %brick.setRayCasting(1);
  %a = %brick.getDatablock().specialbricktype;
  %brick.getDatablock().specialbricktype = "Sound";
  %brick.setSound(wrenchImage);
  %brick.getDatablock().specialbricktype = %a;
 %brick.getGroup().lastsettime = $Sim::Time+1;
 for(%i=0;%i<10;%i++)
 {
  %brick.evName[%i] = "";
  %brick.evState[%i] = "";
  for(%j=0;%j<5;%j++){%brick.evParam[%i,%j] = "";}
 }
}

function servercmdgetWrenchEventList(%client)
{
 if(!isObject(%client.wrenchbrick)){return;}
 %brick = %client.wrenchbrick;
 commandtoclient(%client,'wrencheventmiscdata',%brick.evGroup,%brick.evTrust);
 for(%i=0;%i<%brick.maxstates;%i++)
 {
  if(%brick.evName[%i] !$= "")
  {
   commandtoclient(%client,'wrencheventguirow',%brick.evState[%i] * 100,%brick.evName[%i],%brick.evParam[%i,0],%brick.evParam[%i,1],%brick.evParam[%i,2],%brick.evParam[%i,3],%brick.evParam[%i,4]);
  }
 }
}

function WrenchEvColor(%brick,%ticknum,%state)
{
 if(%ticknum != 0){return;}
 %brick.setColor(WrenchcolorMatch(%brick.evParam[%state,0],1));
 %brick.setColorFX(nametoFX(%brick.evParam[%state,1]));
 %brick.setShapeFX(%brick.evParam[%state,2]);
}

function WrenchEvCancelColor(%brick,%state)
{
  if(!%brick.evParam[%state,3]){%brick.setColor(%brick.setColor);%brick.setColorFX(%brick.setFX);%brick.setShapeFX(%brick.setShape);}
}

function nametoFX(%p)
{
 switch$(%p)
 {
  case "NONE": return 0;
  case "Pearl": return 1;
  case "Chrome": return 2;
  case "Glow": return 3;
  case "Blink": return 4;
  case "Swirl": return 5;
  case "Rainbow": return 6;
 }
}

function WrenchcolorMatch(%rgb,%secure)
{
 %prevdist = 100000;
 %alphadist = 100000;
 %usecolor = 0; //Default
 for(%i = 0;%i < 64;%i++)
 {
     %color = getColorIDTable(%i);
     if(getWord(%color,3) > getWord(%rgb,3)){%a = getWord(%color,3);%b = getWord(%rgb,3);}else{%a = getWord(%color,3);%b = getWord(%rgb,3);}
     if(vectorDist(%rgb,getWords(%color,0,2)) < %prevdist && ((%a - %b) <= %alphadist) && (getWord(%color,3) > 0 && %secure || !%secure))
     {
      %alphadist = (%a - %b);
      %prevdist = vectorDist(%rgb,%color);%usecolor = %i;
     }
 }
 return %usecolor;
}

function WrenchEvParticles(%brick,%ticknum,%state)
{
 if(%ticknum != 0){return;}
 for(%i=0;%i<datablockgroup.getcount();%i++)
 {
  %a = datablockgroup.getobject(%i);
  if(%a.getClassName() $= "ParticleEmitterData" && %a.uiname $= %brick.evParam[%state,0]){%emitter = %a;}
 }
 %brick.setEmitter(%emitter,%brick.getGroup().client);
 %brick.setEmitterDirection(%brick.evParam[%state,1],%brick.getGroup().client);
}

function WrenchEvCancelParticles(%brick,%state)
{
  if(!%brick.evParam[%state,2]){%brick.setEmitter(%brick.setParts);%brick.setEmitterDirection(%brick.dirParts);return;}
}

function WrenchEvLights(%brick,%ticknum,%state)
{
 if(%ticknum != 0){return;}
 for(%i=0;%i<datablockgroup.getcount();%i++)
 {
  %a = datablockgroup.getobject(%i);
  if(%a.getClassName() $= "fxLightData" && %a.uiname $= %brick.evParam[%state,0]){%light = %a;}
 }
 %brick.setLight(%light,%brick.getGroup().client);
}

function wrenchEvCancelLights(%brick,%state)
{
 if(!%brick.evParam[%state,1]){%brick.setLight(%brick.setLight);}return;
}

function WrenchEvItems(%brick,%ticknum,%state)
{
 if(%ticknum != 0){return;}
 for(%i=0;%i<datablockgroup.getcount();%i++)
 {
  %a = datablockgroup.getobject(%i);
  if(%a.getClassName() $= "ItemData" && %a.uiname $= %brick.evParam[%state,0]){%item = %a;}
 }
 %brick.setItem(%item);
 %brick.setItemDirection(%brick.evParam[%state,2]+2);
 %brick.setItemPosition(%brick.evParam[%state,1]);
}

function WrenchEvCancelItems(%brick,%state)
{
 if(!%brick.evParam[%state,3]){%brick.setItem(%brick.setItem);%brick.setItemDirection(%brick.dirItem);%brick.setItemPosition(%brick.posItem);return;}
}

datablock AudioProfile(Note0_3dSound : hammerHitSound) {
   fileName = "base/data/sound/notes/Synth 4/00.wav";
   description = AudioClose3d;
};

datablock AudioProfile(Note1_3dSound : Note0_3dSound){fileName = "base/data/sound/notes/Synth 4/01.wav";};
datablock AudioProfile(Note2_3dSound : Note0_3dSound){fileName = "base/data/sound/notes/Synth 4/02.wav";};
datablock AudioProfile(Note3_3dSound : Note0_3dSound){fileName = "base/data/sound/notes/Synth 4/03.wav";};
datablock AudioProfile(Note4_3dSound : Note0_3dSound){fileName = "base/data/sound/notes/Synth 4/04.wav";};
datablock AudioProfile(Note5_3dSound : Note0_3dSound){fileName = "base/data/sound/notes/Synth 4/05.wav";};
datablock AudioProfile(Note6_3dSound : Note0_3dSound){fileName = "base/data/sound/notes/Synth 4/06.wav";};
datablock AudioProfile(Note7_3dSound : Note0_3dSound){fileName = "base/data/sound/notes/Synth 4/07.wav";};
datablock AudioProfile(Note8_3dSound : Note0_3dSound){fileName = "base/data/sound/notes/Synth 4/08.wav";};
datablock AudioProfile(Note9_3dSound : Note0_3dSound){fileName = "base/data/sound/notes/Synth 4/09.wav";};
datablock AudioProfile(Note10_3dSound : Note0_3dSound){fileName = "base/data/sound/notes/Synth 4/10.wav";};
datablock AudioProfile(Note11_3dSound : Note0_3dSound){fileName = "base/data/sound/notes/Synth 4/11.wav";};

function WrenchEvSound(%brick,%ticknum,%state)
{
 if(%ticknum != 0){return;}
 if(%brick.evParam[%state,0] $= "Notes")
 {
  if(isObject("Note" @ %brick.evParam[%state,1] @ "_3dSound")){serverPlay3d(("Note" @ %brick.evParam[%state,1] @ "_3dSound"),%brick.getTransform());}
 }
 if(%brick.evParam[%state,0] $= "Other")
 {
  switch$(%brick.evParam[%state,1])
  {
   case "Alarm": %sound = alarmSound;
   case "Reward": %sound = rewardSound;
   case "LightOn": %sound = lightOnSound;
   case "LightOff": %sound = lightOffSound;
   case "Click": %sound = playerMountSound;
  }
  serverPlay3d(%sound,%brick.getTransform());
 }
 if(%brick.evParam[%state,0] $= "Music")
 {
  for(%i=0;%i<datablockgroup.getcount();%i++)
  {
   %a = datablockgroup.getobject(%i);
   if(%a.getClassName() $= "AudioProfile" && %a.uiname $= %brick.evParam[%state,1]){%sound = %a;}
  }
  %a = %brick.getDatablock().specialbricktype;
  %brick.getDatablock().specialbricktype = "Sound";
  %brick.setSound(%sound.getName());
  %brick.getDatablock().specialbricktype = %a;
 }
 if(%brick.evParam[%state,0] $= "NONE")
 {
  %a = %brick.getDatablock().specialbricktype;
  %brick.getDatablock().specialbricktype = "Sound";
  %brick.setSound(wrenchImage);
  %brick.getDatablock().specialbricktype = %a;
 }
}

datablock ExplosionData(Alarm2Explosion)
{
   lifeTimeMS = 2000;
   emitter[0] = AlarmEmitter;
   soundProfile = "";
};

//we cant spawn explosions, so this is a workaround for now
datablock ProjectileData(Alarm2Projectile)
{
   explosion           = Alarm2Explosion;

   armingDelay         = 0;
   lifetime            = 10;
   explodeOnDeath		= true;
};

function WrenchEvEmotes(%brick,%ticknum,%state)
{
 %data = %brick.getdatablock();
 if((%brick.angleID % 2) == 1){%bX = %data.bricksizeY;%bY = %data.bricksizeX;}else{%bX = %data.bricksizeX;%bY = %data.bricksizeY;}
 initContainerBoxSearch(%brick.getWorldBoxCenter(), %bX/2 SPC %bY/2 SPC 1, $typemasks::playerobjecttype);
 while (%searchObj= containerSearchNext())
 {
    %x1 = getWord(vectorAdd(%searchObj.getPosition(),"0 0 1.5"),0);
    %y1 = getWord(vectorAdd(%searchObj.getPosition(),"0 0 1.5"),1);
    %x2 = getWord(%brick.getWorldBoxCenter(),0);
    %y2 = getWord(%brick.getWorldBoxCenter(),1);
    %w = %bX/4;
    %h = %bY/4;
    %z1 = getWord(vectorAdd(%searchObj.getPosition(),"0 0 1.5"),2);
    %z2 = getWord(%brick.getWorldBoxCenter(),2);
    if(($Sim::Time - %searchobj.lastemote) < 1){continue;}
    %searchobj.lastemote = $Sim::Time;
    if(%x1 > %x2-%w-1 && %x1 < %x2+%w+1 && %y1 > %y2-%h-1 && %y1 < %y2+%h+1 && %z1 > %z2-2 && %z1 < %z2+2)
    {
     switch$(%brick.evParam[%state,0])
     {
      case "Alarm": %searchobj.emote(alarm2Projectile,1);serverPlay3d(alarmSound,vectorAdd(%searchObj.gettransform(),"0 0 0.1"));
      case "Love": %searchobj.emote(loveImage,1);servercmdHug(%searchobj.client);
      case "Hate": %searchobj.emote(hateImage,1);
      case "Confuse": %searchobj.emote(wtfImage,1);
      case "Bricks": %searchobj.emote(BSDProjectile,1);
      case "Win": %searchobj.emote(winStarProjectile,1);serverplay3d(rewardSound,vectorAdd(%searchObj.gettransform(),"0 0 0.1"));
      case "Burn": %searchobj.burn(2000);
     }
    }
 }
}

function WrenchEvAdmin_Only_Misc(%brick,%ticknum,%state)
{
 %data = %brick.getdatablock();
 if((%brick.angleID % 2) == 1){%bX = %data.bricksizeY;%bY = %data.bricksizeX;}else{%bX = %data.bricksizeX;%bY = %data.bricksizeY;}
 initContainerBoxSearch(%brick.getWorldBoxCenter(), %bX/2 SPC %bY/2 SPC 1, $typemasks::playerobjecttype);
 while (%searchObj= containerSearchNext())
 {
    %x1 = getWord(vectorAdd(%searchObj.getPosition(),"0 0 1.5"),0);
    %y1 = getWord(vectorAdd(%searchObj.getPosition(),"0 0 1.5"),1);
    %x2 = getWord(%brick.getWorldBoxCenter(),0);
    %y2 = getWord(%brick.getWorldBoxCenter(),1);
    %w = %bX/4;
    %h = %bY/4;
    %z1 = getWord(vectorAdd(%searchObj.getPosition(),"0 0 1.5"),2);
    %z2 = getWord(%brick.getWorldBoxCenter(),2);
    if(%x1 > %x2-%w-1 && %x1 < %x2+%w+1 && %y1 > %y2-%h-1 && %y1 < %y2+%h+1 && %z1 > %z2-2 && %z1 < %z2+2)
    {
     switch$(%brick.evParam[%state,0])
     {
      case "Lose Inventory":
	%obj = %searchObj;
       	for(%i = 0;%i<%obj.getDatablock().maxtools;%i++)
	{
		if(%obj.tool[%i] == 0){continue;}
		messageClient(%obj.client,'MsgItemPickup','',%i,0);
		%obj.tool[%i] = 0;
		%obj.weaponCount--;
		serverCmdUnUseTool(%obj.client);
	}
      case "Checkpoint":
	%obj = %searchObj;
	if(isObject(%obj.client.minigame) && %obj.client.checkpoint !$= %brick)
	{
	 commandtoclient(%obj.client,'centerprint',"\c4Checkpoint saved!",2,2,2);
	 %obj.client.checkpoint = %brick;
	}
     }
    }
 }
}
//Port of the Checkpoint brick
package Spawning
{
 function GameConnection::CreatePlayer(%this,%transform) //Uniform
 {
  if(isObject(%this.minigame) && isObject(%this.checkpoint))
  {
   %Transform = %this.checkpoint.getTransform();
  }
  %ret = Parent::CreatePlayer(%this,%transform);
  return %ret;
 }
 function servercmdLeaveMinigame(%client)
 {
   Parent::servercmdLeaveMinigame(%client);
   schedule(100,0,minigamecheck,%client);
  }
 function servercmdEndMinigame(%client)
 {
   Parent::servercmdEndMinigame(%client);
   schedule(100,0,minigamecheck,%client);
  }
 function minigamecheck(%client)
 {
   if(!isObject(%client.minigame)) //If they aren't "forced" to one still i.e. Aloshi's mod/Team DM
   {
    %client.checkpoint = "";
   }
 }
};
activatepackage(Spawning);

function WrenchEvImpulse(%brick,%ticknum,%state)
{
 %data = %brick.getdatablock();
 if((%brick.angleID % 2) == 1){%bX = %data.bricksizeY;%bY = %data.bricksizeX;}else{%bX = %data.bricksizeX;%bY = %data.bricksizeY;}
 initContainerBoxSearch(%brick.getWorldBoxCenter(), %bX/2 SPC %bY/2 SPC 1, $typemasks::playerobjecttype | $typemasks::vehicleobjecttype);
 while (%searchObj= containerSearchNext())
 {
    %x1 = getWord(vectorAdd(%searchObj.getPosition(),"0 0 1.5"),0);
    %y1 = getWord(vectorAdd(%searchObj.getPosition(),"0 0 1.5"),1);
    %x2 = getWord(%brick.getWorldBoxCenter(),0);
    %y2 = getWord(%brick.getWorldBoxCenter(),1);
    %w = %bX/4;
    %h = %bY/4;
    %z1 = getWord(vectorAdd(%searchObj.getPosition(),"0 0 1.5"),2);
    %z2 = getWord(%brick.getWorldBoxCenter(),2);
    %player = %searchObj;
    if((%x1 > %x2-%w-1 && %x1 < %x2+%w+1 && %y1 > %y2-%h-1 && %y1 < %y2+%h+1 && %z1 > %z2-2 && %z1 < %z2+2) || ((%player.getclassname() !$= "Player") && (%player.getclassname() !$= "AIPlayer")))
    {
	%d1 = %brick.evParam[%state,1];
   	%d2 = %brick.evParam[%state,2];
	%vector = dirToNormal2(%d1,%d2);
	if(%brick.evParam[%state,3])
	%searchObj.setVelocity(vectorAdd(%searchObj.getVelocity(),vectorScale(%vector,%brick.evParam[%state,0])));
	else
	%searchObj.setVelocity(vectorScale(%vector,%brick.evParam[%state,0]));
    }
 }
}

function WrenchEvTeleport(%brick,%ticknum,%state)
{
 %data = %brick.getdatablock();
 if((%brick.angleID % 2) == 1){%bX = %data.bricksizeY;%bY = %data.bricksizeX;}else{%bX = %data.bricksizeX;%bY = %data.bricksizeY;}
 initContainerBoxSearch(%brick.getWorldBoxCenter(), %bX/2 SPC %bY/2 SPC 1, $typemasks::playerobjecttype | $typemasks::vehicleobjecttype);
 while (%searchObj= containerSearchNext())
 {
    %x1 = getWord(vectorAdd(%searchObj.getPosition(),"0 0 1.5"),0);
    %y1 = getWord(vectorAdd(%searchObj.getPosition(),"0 0 1.5"),1);
    %x2 = getWord(%brick.getWorldBoxCenter(),0);
    %y2 = getWord(%brick.getWorldBoxCenter(),1);
    %w = %bX/4;
    %h = %bY/4;
    %z1 = getWord(vectorAdd(%searchObj.getPosition(),"0 0 1.5"),2);
    %z2 = getWord(%brick.getWorldBoxCenter(),2);
    %player = %searchObj;
    if((%x1 > %x2-%w-1 && %x1 < %x2+%w+1 && %y1 > %y2-%h-1 && %y1 < %y2+%h+1 && %z1 > %z2-2 && %z1 < %z2+2) || ((%player.getclassname() !$= "Player") && (%player.getclassname() !$= "AIPlayer")))
    {
	%count = 0;
	for(%i=0;%i<$wrenchCtrl.count;%i++)
	{
		if($wrenchCtrl.getValue(%i).evGroup $= %brick.evParam[%state,0]){%tele[%count] = $wrenchCtrl.getValue(%i);%count++;}
	}
	if(!isObject(%tele[0])){return;}
	%searchobj.emote(spawnProjectile,1);
	%searchobj.setTransform(vectorAdd(%tele[getRandom(0,%count-1)].getTransform(),"0 0 1") SPC rotFromTransform(%searchObj.getTransform()));
	%searchobj.setVelocity("0 0 0");
	%searchobj.emote(spawnProjectile,1);
    }
 }
}

function WrenchEvDoor(%brick,%ticknum,%state)
{
 if(%ticknum != 0){return;}
 if(%brick.evParam[%state,0])
 {
  %i = new FXdtsBrick(){datablock = %brick.getDatablock();position = %brick.position;angleID = %brick.angleID;};
  %i.setTransform(%brick.getTransform());
  %i.setColor(%brick.getColorID());
  %i.onPlant();
  %i.killbrick();
 }
 if(%brick.evParam[%state,2])
 {
  %brick.setColliding(0);%brick.setRayCasting(0);
 }
 if(%brick.evParam[%state,1])
 {
  %client = %brick.getgroup().client;
  if(%client.isAdmin || %client.isSuperAdmin){%brick.setColor(wrench0Alpha());}else{%brick.setColor(wrenchLowAlpha());%brick.setRayCasting(1);} //Can be killed by wand etc
 }
}

function WrenchEvCancelDoor(%brick,%state)
{
  if(%brick.evParam[%state,1])
  {
   %brick.setColor(%brick.setColor);
  }
  %brick.setColliding(1);
  %brick.setRayCasting(1);
  return;
}

function Wrench0alpha()
{
 %alpha = 100000;
 %usecolor = 0; //Default
 for(%i = 0;%i < 64;%i++)
 {
     %color = getColorIDTable(%i);
     if(getWord(%color,3) < %alpha){%usecolor = %i;%alpha = getWord(%color,3);}
 }
 return %usecolor;
}

function WrenchLowalpha() //Non-Admin
{
 %alpha = 100000;
 %usecolor = 0; //Default
 for(%i = 0;%i < 64;%i++)
 {
     %color = getColorIDTable(%i);
     if(getWord(%color,3) < %alpha && getWord(%color,3) > 0){%usecolor = %i;%alpha = getWord(%color,3);}
 }
 return %usecolor;
}

if($DamageType::skull $= "")
{
 AddDamageType("blueRibbon",   '<bitmap:add-ons/ci/blueRibbon> %1',    '%2 <bitmap:add-ons/ci/blueRibbon> %1',0.5,1);
 AddDamageType("groundFall",   '<bitmap:add-ons/ci/crater> %1',    '%2 <bitmap:add-ons/ci/crater> %1',0.5,1);
 AddDamageType("wallFall",   '<bitmap:add-ons/ci/splat> %1',    '%2 <bitmap:add-ons/ci/splat> %1',0.5,1);
 AddDamageType("star",   '<bitmap:add-ons/ci/star> %1',    '%2 <bitmap:add-ons/ci/star> %1',0.5,1);
 AddDamageType("trophy",   '<bitmap:add-ons/ci/trophy> %1',    '%2 <bitmap:add-ons/ci/trophy> %1',0.5,1);
 AddDamageType("fakecar",   '<bitmap:add-ons/ci/car> %1',    '%2 <bitmap:add-ons/ci/car> %1',0.5,1);
 AddDamageType("fakegen",   '<bitmap:add-ons/ci/generic> %1',    '%2 <bitmap:add-ons/ci/generic> %1',0.5,1);
 AddDamageType("skull",   '<bitmap:add-ons/ci/skull> %1',    '%2 <bitmap:add-ons/ci/skull> %1',0.5,1);
}
function WrenchEvHurt(%brick,%ticknum,%state)
{
 %data = %brick.getdatablock();
 if((%brick.angleID % 2) == 1){%bX = %data.bricksizeY;%bY = %data.bricksizeX;}else{%bX = %data.bricksizeX;%bY = %data.bricksizeY;}
 initContainerBoxSearch(%brick.getWorldBoxCenter(), %bX/2 SPC %bY/2 SPC 1, $typemasks::playerobjecttype | $typemasks::vehicleobjecttype);
 while (%searchObj= containerSearchNext())
 {
    %x1 = getWord(vectorAdd(%searchObj.getPosition(),"0 0 1.5"),0);
    %y1 = getWord(vectorAdd(%searchObj.getPosition(),"0 0 1.5"),1);
    %x2 = getWord(%brick.getWorldBoxCenter(),0);
    %y2 = getWord(%brick.getWorldBoxCenter(),1);
    %w = %bX/4;
    %h = %bY/4;
    %z1 = getWord(vectorAdd(%searchObj.getPosition(),"0 0 1.5"),2);
    %z2 = getWord(%brick.getWorldBoxCenter(),2);
    if(%x1 > %x2-%w-1 && %x1 < %x2+%w+1 && %y1 > %y2-%h-1 && %y1 < %y2+%h+1 && %z1 > %z2-2 && %z1 < %z2+2)
    {
     if(isObject(%searchObj.client.minigame) && %searchObj.client.minigame $= %brick.getgroup().client.minigame)
     {
      %dmg = %brick.evParam[%state,0] / 100;
      if(%brick.evParam[%state,1]){%dmg = 20000;}
      switch$(%brick.evParam[%state,3])
      {
	case "Fall": %type = $DamageType::groundFall;
	case "Smack": %type = $DamageType::wallFall;
	case "Suicide": %type = $DamageType::skull; //::suicide has "%2 SKULL %1!" instead of "%2 SKULL %1"
	case "Star": %type = $DamageType::star;
	case "Trophy": %type = $DamageType::trophy;
	case "Car": %type = $DamageType::fakecar;
	case "Ribbon": %type = $DamageType::blueRibbon;
	case "Generic": %type = $DamageType::fakegen;
	case "None": %type = "";
	default:
		%type = $DamageType::fakegen;
		for(%i=0;%i<datablockgroup.getcount();%i++)
		{
			%d = datablockgroup.getobject(%i);
			if(%d.getClassname() $= "ItemData" && %d.uiname $= %brick.evParam[%state,3] && %d.image.projectile.directdamagetype !$= "")
			{
				%type = %d.image.projectile.directdamagetype;
			}
		}
      }
      if(isObject(%brick.getGroup().client.player) && %brick.evParam[%state,2]){%player = %brick.getGroup().client.player;}else{%player = %searchObj;}
      if(%searchObj.getClassName() $= "Player" || %searchObj.getClassName() $= "AIPlayer")
      {
       Armor::damage(%searchobj.getDataBlock(),%searchobj, %player, %searchobj.position, %dmg, %type);
      }
      if(%searchObj.getClassName() $= "WheeledVehicle" || %searchObj.getClassName() $= "FlyingWheeledVehicle" || %searchObj.getClassName() $= "FlyingVehicle" || %searchObj.getClassName() $= "HoverVehicle")
      {
       Vehicle::damage(%searchobj, %player, %searchobj.position, %dmg, %type);
      }
     }
    }
 }
}

datablock ProjectileData(wrenchspikeProjectile)
{
   projectileShapeName = "./shapes/arrow.dts";

   directDamage        = 60;
   directDamageType    = $DamageType::ArrowDirect;

   radiusDamage        = 0;
   damageRadius        = 0;
   radiusDamageType    = $DamageType::ArrowDirect;

   explosion           = "";
   impactImpulse	   = 500;
   verticalImpulse	   = 2000;
   particleEmitter     = "";

   muzzleVelocity      = 2;
   velInheritFactor    = 1;

   armingDelay         = 0;
   lifetime            = 500;
   fadeDelay           = 3500;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = 0;
   gravityMod = 0.1;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";
};

datablock ProjectileData(wrenchfireProjectile : arrowprojectile)
{
	muzzleVelocity = 7;
	lifetime = 300;
	directDamage        = 15.0;
	directDamageType    = $DamageType::fakegen;
	projectileShapeName = "./shapes/blank.dts";
	radiusDamage        = 0;
	damageRadius        = 0;
	radiusDamageType    = $DamageType::fakegen;
	explosion = "";
   	particleEmitter     = adminwandemitterA;

};

package wrenchFire
{
function wrenchFireprojectile::damage(%this,%obj,%col,%fade,%pos,%normal)
{
 Parent::damage(%this,%obj,%col,%fade,%pos,%normal);
 %col.burn(2000);
}
};activatepackage(wrenchFire);

datablock ProjectileData(wrenchbulletprojectile : gunprojectile)
{
 genericVariable = 1; //Errors
};

datablock ParticleData(WrocketTrailParticle : rocketTrailParticle)
{
	colors[0]     = "1 0 1 0.4";
	colors[1]     = "0.2 0 1 0.5";
   colors[2]     = "0.20 0.20 0.20 0.3";
   colors[3]     = "0.0 0.0 0.0 0.0";
};
datablock ParticleEmitterData(WrocketTrailEmitter)
{
   ejectionPeriodMS = 2;
   particles = "WrocketTrailParticle";
};

datablock ProjectileData(wrenchrocketprojectile : rocketLauncherProjectile)
{
 lightcolor = "0.5 0 1 1";
   isBallistic         = true;
   muzzleVelocity = 30;
   gravityMod = 1;
   lifetime = 10000;
   fadedelay = 39000;
   particleEmitter = WrocketTrailEmitter;
};

function WrenchEvShoot(%brick,%ticknum,%state)
{
 %shots = %brick.evParam[%state,4];
 if(%brick.evParam[%state,4] <= 0){return;}
 if(%brick.evParam[%state,4] > 5 && !%brick.getGroup().client.isSuperAdmin){%shots = 5;}
 %n = 100/%shots;
 if(%ticknum % mFloor(%n) != 0) return;
 //%brick.getGroup().shotnum+=1;
 //Artillery_Rocket Homing_Turret Spread_Turret Spike Fire
 switch$(%brick.evParam[%state,1])
 {
  case "Fizzler":
   %projectile = wrenchFizzlerProjectile;
   %flash = "";
   %d1 = %brick.evParam[%state,2];
   %d2 = %brick.evParam[%state,3];
   %vector = dirToNormal2(%d1,%d2);
  case "Fire":
   %projectile = wrenchFireProjectile;
   %flash = "";
   %d1 = %brick.evParam[%state,2];
   %d2 = %brick.evParam[%state,3];
   %vector = dirToNormal2(%d1,%d2);
  case "Artillery Rocket":
   %projectile = wrenchRocketProjectile;
   %flash = rocketLauncherFlashEmitter;
   %time = 70;
   %d1 = %brick.evParam[%state,2];
   %d2 = %brick.evParam[%state,3];
   %vector = dirToNormal2(%d1,%d2);
  case "Homing Turret":
   %projectile = wrenchBulletProjectile;
   %flash = gunFlashEmitter;
   %time = 70;
   %dist = 20;
   %found = -1;
   for(%i=0;%i<clientgroup.getCount();%i++)
   {
    %c = clientgroup.getobject(%i);
    if(%c.minigame $= %brick.getGroup().client.minigame && isObject(%c.player) && vectorDist(%c.player.getTransform(),%brick.getTransform()) < %dist)
    {
     %found = %c.player;
     %dist = vectorDist(%c.player.getTransform(),%brick.getTransform());
    }
   }
   if(%found == -1){return;}
   %add = (%found.isCrouched() ? "0" : "2");
   %vector = vectorNormalize(vectorSub(vectorAdd(%found.position,"0 0" SPC %add),%brick.position));
  case "Spread Turret":
   %projectile = wrenchBulletProjectile;
   %flash = gunFlashEmitter;
   %time = 70;
   %d1 = %brick.evParam[%state,2];
   %d2 = %brick.evParam[%state,3];
   %vector = dirToNormal2(%d1,%d2);
   %x = (1 - 2*getRandom());
   %y = (1 - 2*getRandom());
   %z = (1 - 2*getRandom());
   %aimPoint = vectorAdd(%brick.getTransform(),vectorScale(%vector,80));
   %aimPoint = vectorAdd(%aimPoint,%x SPC %y SPC %z);
   %vector = vectorNormalize(vectorSub(%aimPoint,%brick.getTransform()));
  case "Spike":
   %projectile = wrenchSpikeProjectile;
   %d1 = %brick.evParam[%state,2];
   %d2 = %brick.evParam[%state,3];
   %vector = dirToNormal2(%d1,%d2);
  default:
   for(%i=0;%i<datablockgroup.getcount();%i++)
   {
    %d = datablockgroup.getobject(%i);
    if(%d.getClassname() $= "ItemData" && %d.uiname $= %brick.evParam[%state,1] && %d.image.projectile !$= "")
    {
     %projectile = %d.image.projectile;
     for(%j=0;%d.image.stateName[%j] !$= "";%j++)
     {
      if(%d.image.stateName[%j] $= "Fire" && isObject(%d.image.stateEmitter[%j])){%flash = %d.image.stateEmitter[%j];%time = %d.image.stateEmitterTime[%j]*1000;}
     }
    }
   }
   %d1 = %brick.evParam[%state,2];
   %d2 = %brick.evParam[%state,3];
   %vector = dirToNormal2(%d1,%d2);
 }
 if(!isObject(%projectile)){return;}
 if(isObject(findLocalClient())){%c = findLocalClient();}
 if(isObject(%brick.getGroup().client)){%c = %brick.getGroup().client;}
 if(isObject($TeamDM::Minigame)){%c = $TeamDM::Minigame;}
 if(!isObject(%c)){return;}
 //Having the source of the projectile as an item disables the "invalid source" error and allows the projectile to work at short range.
 %p = new Projectile()
 {
	dataBlock = %projectile;
	initialVelocity = vectorScale(%vector,%projectile.muzzleVelocity);
	initialPosition = %brick.getTransform(); //vectorSub(,vectorScale(%vector,%projectile.muzzleVelocity / 2));
	sourceObject = $wrenchItem;
	sourceSlot = 0;
	client = %c;
	override = 1;
 };//%brick.getgroup().shottime[%t] = $Sim::Time;
 MissionCleanup.add(%p);
 if(!isObject(%flash)){return;}
   %d1 = %brick.evParam[%state,2];
   %d2 = %brick.evParam[%state,3];
 %p = new ParticleEmitterNode()
 {
  datablock = genericEmitterNode;
  emitter = %flash;
  scale = "0.1 0.1 0.1";
  rotation = dirToRot2(%d1,%d2);
  velocity = "1";
  position = vectorAdd(%brick.getTransform(),vectorScale(%vector,0.5));
  brick = %brick;
 };%p.schedule(%time,delete);
 MissionCleanup.add(%p);
}

function dirToNormal2(%a,%b)
{
 if(%a == 0){%vec = "0 1 0";}else if(%a == 1){%vec = "1 0 0";}else if(%a == 2){%vec = "0 -1 0";}else %vec = "-1 0 0";
 if(%b == 0){%vec = "0 0 1";}else if(%b == 1){%vec = "0 0 -1";}else if(%b == 2){%vec = vectorAdd(%vec,"0 0 1");}else if(%b == 3){%vec = vectorAdd(%vec,"0 0 -1");}
 return vectorNormalize(%vec) SPC "0";
}

function dirToRot2(%a,%b)
{
 if(%a == 0){%vec = "1 0 0";}else if(%a == 1){%vec = "0 -1 0";}else if(%a == 2){%vec = "-1 0 0";}else %vec = "0 1 0";
 if(%b == 0){%r = 0;}else if(%b == 1){%r = 180;}else if(%b == 2){%r = 45;}else if(%b == 3){%r = 135;}else{%r = 90;}
 return vectorNormalize(%vec) SPC %r;
}

function dirToNormal(%a,%b)
{
 if(%a == 0){%vec = "0 0 1";}else if(%a == 1){%vec = "0 0 -1";}else if(%a == 2){%vec = "0 1 0";}else if(%a == 3){%vec = "1 0 0";}else if(%a == 4){%vec = "0 -1 0";}else %vec = "-1 0 0";
 return %vec;
}

datablock ParticleData(LaserParticleB)
{
	textureName          = "base/data/particles/dot";
	dragCoefficient      = 0.0;
	gravityCoefficient   = 0.0;
	inheritedVelFactor   = 0.0;
	windCoefficient      = 0;
	constantAcceleration = 0;
	lifetimeMS           = 800;
	lifetimeVarianceMS   = 0;
	spinSpeed     = 0;
	spinRandomMin = -90.0;
	spinRandomMax =  90.0;
	useInvAlpha   = false;

	colors[0]	= "1.0 0.0 0.0 1.0";
	colors[1]	= "1.0 0.0 0.0 1.0";
	colors[2]	= "1.0 0.0 0.0 1.0";

	sizes[0]	= 0.1;
	sizes[1]	= 0.1;
	sizes[2]	= 0.1;

	times[0]	= 0.0;
	times[1]	= 0.9;
	times[2]	= 1.0;
};

datablock ParticleEmitterData(LaserEmitterB)
{
   ejectionPeriodMS = 20;
   periodVarianceMS = 0;
   ejectionVelocity = 1.0;
   ejectionOffset   = 0.0;
   velocityVariance = 0.0;
   thetaMin         = 0;
   thetaMax         = 0;
   phiReferenceVel  = 0;
   phiVariance      = 0;
   overrideAdvance = false;

   lifetimeMS = 200;
   particles = LaserParticleB;   

	uiName = "";
};

datablock ProjectileData(wrenchLaserProjectile : arrowprojectile)
{
	muzzleVelocity = 20;
	lifetime = 2500;
	gravitymod = 0;
	directDamage        = 0.0;
	directDamageType    = $DamageType::FireDirect;
	projectileShapeName = "./shapes/blank.dts";
	radiusDamage        = 0;
	damageRadius        = 0;
	radiusDamageType    = $DamageType::FireDirect;
	explosion = "";
   	particleEmitter     = LaserEmitterB;

};

function WrenchEvTriggers(%brick,%ticknum,%state)
{
 if(%brick.evParam[%state,1] $= ""){return;}
 if(%brick.evActivated)
 {
    %brick.evActivated = 0;
    for(%i=0;%i<$wrenchCtrl.count;%i++){if($wrenchCtrl.getValue(%i).evGroup $= %brick.evParam[%state,1] && wrenchTrust($wrenchCtrl.getvalue(%i),%brick) $= "1"){$wrenchCtrl.getValue(%i).evActivated = 1;}}
 }
 switch$(%brick.evParam[%state,0])
 {
  case "Click":
   if(%brick.clickActivated && %ticknum == 0)
   {
    %brick.clickActivated--;
    for(%i=0;%i<$wrenchCtrl.count;%i++){if($wrenchCtrl.getValue(%i).evGroup $= %brick.evParam[%state,1] && wrenchTrust($wrenchCtrl.getvalue(%i),%brick) $= "1"){$wrenchCtrl.getValue(%i).evActivated = 1;}}
   }
  case "Shot":
   if(%brick.shotActivated)
   {
    %brick.shotActivated = 0;
    for(%i=0;%i<$wrenchCtrl.count;%i++){if($wrenchCtrl.getValue(%i).evGroup $= %brick.evParam[%state,1] && wrenchTrust($wrenchCtrl.getvalue(%i),%brick) $= "1"){$wrenchCtrl.getValue(%i).evActivated = 1;}}
   }
  case "Proximity":
   for(%i=0;%i<clientgroup.getCount();%i++)
   {
    %c = clientgroup.getobject(%i);
    if(isObject(%c.player) && vectorDist(%c.player.getHackPosition(),%brick.getTransform()) < 6)
    {
     %found = 1;
    }
   }
   if(%found){for(%i=0;%i<$wrenchCtrl.count;%i++){if($wrenchCtrl.getValue(%i).evGroup $= %brick.evParam[%state,1] && wrenchTrust($wrenchCtrl.getvalue(%i),%brick) $= "1"){$wrenchCtrl.getValue(%i).evActivated = 1;}}}
  case "Laser":
   %p = new Projectile()
   {
	dataBlock = wrenchLaserProjectile;
	initialVelocity = vectorScale(dirToNormal(%brick.evParam[%state,2]),8);
	initialPosition = %brick.getTransform();
	sourceObject = $wrenchItem;
	sourceSlot = 0;
	client = %brick.getGroup().client;
   };
   %searchMasks = $TypeMasks::FXBrickObjectType | $TypeMasks::PlayerObjectType | $TypeMasks::VehicleObjectType; //Bricks and vehicles can block the laser
   %scanTarg = ContainerRayCast (%brick.position, vectorAdd(%brick.position,vectorScale(dirToNormal(%brick.evParam[%state,2]),18.5)), %searchMasks);
   %obj = firstWord(%scanTarg);
   if(!isObject(%obj)){return;}
     if(%obj.getClassName() $= "Player" && (%str = wrenchTrust(%brick,%obj.client)) !$= "1")
     {
      commandtoclient(%obj.client,'centerprint',%str,2,3);
      return;
     }
   if(%obj.getClassname() !$= "Player" && %obj.getClassName() !$= "AIPlayer"){return;}
   for(%i=0;%i<$wrenchCtrl.count;%i++){%o = $wrenchCtrl.getValue(%i);if(%o.evGroup $= %brick.evParam[%state,1] && %o.evName[%o.curState] $= "Wait For" && wrenchTrust($wrenchCtrl.getvalue(%i),%brick) $= "1"){$wrenchCtrl.getValue(%i).evActivated = 1;}}

  case "IR":
   %searchMasks = $TypeMasks::FXBrickObjectType | $TypeMasks::PlayerObjectType | $TypeMasks::VehicleObjectType; //Bricks and vehicles can block the laser
   %scanTarg = ContainerRayCast (%brick.position, vectorAdd(%brick.position,vectorScale(dirToNormal(%brick.evParam[%state,2]),18.5)), %searchMasks);
   %obj = firstWord(%scanTarg);
   if(!isObject(%obj)){return;}
     if(%obj.getClassName() $= "Player" && (%str = wrenchTrust(%brick,%obj.client)) !$= "1")
     {
      commandtoclient(%obj.client,'centerprint',%str,2,3);
      return;
     }
   if(%obj.getClassname() !$= "Player" && %obj.getClassName() !$= "AIPlayer"){return;}
   for(%i=0;%i<$wrenchCtrl.count;%i++){%o = $wrenchCtrl.getValue(%i);if(%o.evGroup $= %brick.evParam[%state,1] && %o.evName[%o.curState] $= "Wait For" && wrenchTrust($wrenchCtrl.getvalue(%i),%brick) $= "1"){$wrenchCtrl.getValue(%i).evActivated = 1;}}

  case "Touch":
   %data = %brick.getdatablock();
   if((%brick.angleID % 2) == 1){%bX = %data.bricksizeY;%bY = %data.bricksizeX;}else{%bX = %data.bricksizeX;%bY = %data.bricksizeY;}
   initContainerBoxSearch(%brick.getWorldBoxCenter(), %bX/2 SPC %bY/2 SPC 1, $typemasks::playerobjecttype | $typemasks::vehicleobjecttype);
   while (%searchObj= containerSearchNext())
   {
    %x1 = getWord(vectorAdd(%searchObj.getPosition(),"0 0 1.5"),0);
    %y1 = getWord(vectorAdd(%searchObj.getPosition(),"0 0 1.5"),1);
    %x2 = getWord(%brick.getWorldBoxCenter(),0);
    %y2 = getWord(%brick.getWorldBoxCenter(),1);
    %w = %bX/4;
    %h = %bY/4;
    %z1 = getWord(vectorAdd(%searchObj.getPosition(),"0 0 1.5"),2);
    %z2 = getWord(%brick.getWorldBoxCenter(),2);
    if((%x1 > %x2-%w-1 && %x1 < %x2+%w+1 && %y1 > %y2-%h-1 && %y1 < %y2+%h+1 && %z1 > %z2-2 && %z1 < %z2+2) || (%searchObj.getClassname() !$= "Player" && %searchObj.getClassName() !$= "AIPlayer"))
    {
     if(%searchObj.getclassname() $= "Player" & (%str = wrenchTrust(%brick,%searchobj.client)) !$= "1")
     {
      commandtoclient(%searchobj.client,'centerprint',%str,2,3);
      return;
     }
     for(%i=0;%i<$wrenchCtrl.count;%i++){%o = $wrenchCtrl.getValue(%i);if(%o.evGroup $= %brick.evParam[%state,1] && %o.evName[%o.curState] $= "Wait For" && wrenchTrust($wrenchCtrl.getvalue(%i),%brick) $= "1"){$wrenchCtrl.getValue(%i).evActivated = 1;}}
    }
   }
  case "Auto":
    if(%ticknum == 0){for(%i=0;%i<$wrenchCtrl.count;%i++){%o = $wrenchCtrl.getValue(%i);if(%o.evGroup $= %brick.evParam[%state,1] && %o.evName[%o.curState] $= "Wait For" && wrenchTrust($wrenchCtrl.getvalue(%i),%brick) $= "1"){$wrenchCtrl.getValue(%i).evActivated = 1;}}}
 }
}

function WrenchEvWait_For(%brick,%ticknum,%state)
{
 if(%brick.evName[%state - 1] !$= "" && %brick.evParam[%state,2])
 {
  if($Eventbricks::Server::Cancel[%brick.evName[%state-1]]){%statecall = 1;}
  call("WrenchEv" @ strReplace(%brick.evName[%state - 1]," ","_"),%brick,%ticknum,%state-1);
 }
 if(%brick.evActivated)
 {
  if(%statecall){call("WrenchEvCancel" @ strReplace(%brick.evName[%state - 1]," ","_"),%brick,%state-1);}
  %brick.evActivated = 0;%brick.ticknum = -1;%brick.curState++;
  return;
 }
 switch$(%brick.evParam[%state,0])
 {
  case "Triggered":
   //Nothing, handled in above code

  case "Time":
   if(%brick.ticknum >= (%brick.evState[%state]*10)-1)
   {
    if(%statecall){call("WrenchEvCancel" @ strReplace(%brick.evName[%state - 1]," ","_"),%brick,%state-1);}
    %brick.ticknum = -1;%brick.curState++;
   }

  case "Click":
   if(%brick.clickActivated)
   {
    if(%statecall){call("WrenchEvCancel" @ strReplace(%brick.evName[%state - 1]," ","_"),%brick,%state-1);}
    %brick.clickActivated--;%brick.ticknum = -1;%brick.curState++;
   }

  case "Shot":
   if(%brick.shotActivated)
   {
    if(%statecall){call("WrenchEvCancel" @ strReplace(%brick.evName[%state - 1]," ","_"),%brick,%state-1);}
    %brick.shotActivated = 0;%brick.ticknum = -1;%brick.curState++;
   }

  case "Proximity":
   for(%i=0;%i<clientgroup.getCount();%i++)
   {
    %c = clientgroup.getobject(%i);
    if(isObject(%c.player) && vectorDist(%c.player.getHackPosition(),%brick.getTransform()) < 6)
    {
     %found = 1;
    }
   }
   if(%found)
   {
    if(%statecall){call("WrenchEvCancel" @ strReplace(%brick.evName[%state - 1]," ","_"),%brick,%state-1);}
    %brick.ticknum = -1;%brick.curState++;
   }

  case "Laser":
   %p = new Projectile()
   {
	dataBlock = wrenchLaserProjectile;
	initialVelocity = vectorScale(dirToNormal(%brick.evParam[%state,1]),8);
	initialPosition = %brick.getTransform();
	sourceObject = $wrenchItem;
	sourceSlot = 0;
	client = %brick.getGroup().client;
   };
   %searchMasks = $TypeMasks::FXBrickObjectType | $TypeMasks::PlayerObjectType | $TypeMasks::VehicleObjectType; //Bricks and vehicles can block the laser
   %scanTarg = ContainerRayCast (%brick.position, vectorAdd(%brick.position,vectorScale(dirToNormal(%brick.evParam[%state,1]),18.5)), %searchMasks);
   %obj = firstWord(%scanTarg);
   if(!isObject(%obj)){return;}
     if(%obj.getClassName() $= "Player" && (%str = wrenchTrust(%brick,%obj.client)) !$= "1")
     {
      commandtoclient(%obj.client,'centerprint',%str,2,3);
      return;
     }
   if(%obj.getClassname() !$= "Player" && %obj.getClassName() !$= "AIPlayer"){return;}
   if(%statecall){call("WrenchEvCancel" @ strReplace(%brick.evName[%state - 1]," ","_"),%brick,%state-1);}
   %brick.ticknum = -1;%brick.curState++;

  case "IR":
   %searchMasks = $TypeMasks::FXBrickObjectType | $TypeMasks::PlayerObjectType | $TypeMasks::VehicleObjectType; //Bricks and vehicles can block the laser
   %scanTarg = ContainerRayCast (%brick.position, vectorAdd(%brick.position,vectorScale(dirToNormal(%brick.evParam[%state,1]),18.5)), %searchMasks);
   %obj = firstWord(%scanTarg);
   if(!isObject(%obj)){return;}
     if(%obj.getClassName() $= "Player" && (%str = wrenchTrust(%brick,%obj.client)) !$= "1")
     {
      commandtoclient(%obj.client,'centerprint',%str,2,3);
      return;
     }
   if(%obj.getClassname() !$= "Player" && %obj.getClassName() !$= "AIPlayer"){return;}
    if(%statecall){call("WrenchEvCancel" @ strReplace(%brick.evName[%state - 1]," ","_"),%brick,%state-1);}
   %brick.ticknum = -1;%brick.curState++;

  case "Touch":
   %data = %brick.getdatablock();
   if((%brick.angleID % 2) == 1){%bX = %data.bricksizeY;%bY = %data.bricksizeX;}else{%bX = %data.bricksizeX;%bY = %data.bricksizeY;}
   initContainerBoxSearch(%brick.getWorldBoxCenter(), %bX/2 SPC %bY/2 SPC 1, $typemasks::playerobjecttype | $typemasks::vehicleobjecttype);
   while (%searchObj= containerSearchNext())
   {
    %x1 = getWord(vectorAdd(%searchObj.getPosition(),"0 0 1.5"),0);
    %y1 = getWord(vectorAdd(%searchObj.getPosition(),"0 0 1.5"),1);
    %x2 = getWord(%brick.getWorldBoxCenter(),0);
    %y2 = getWord(%brick.getWorldBoxCenter(),1);
    %w = %bX/4;
    %h = %bY/4;
    %z1 = getWord(vectorAdd(%searchObj.getPosition(),"0 0 1.5"),2);
    %z2 = getWord(%brick.getWorldBoxCenter(),2);
    if((%x1 > %x2-%w-1 && %x1 < %x2+%w+1 && %y1 > %y2-%h-1 && %y1 < %y2+%h+1 && %z1 > %z2-2 && %z1 < %z2+2) || (%searchObj.getClassname() !$= "Player" && %searchObj.getClassName() !$= "AIPlayer"))
    {
     if(%searchObj.getclassname() $= "Player" & (%str = wrenchTrust(%brick,%searchobj.client)) !$= "1")
     {
      commandtoclient(%searchobj.client,'centerprint',%str,2,3);
      return;
     }
     if(%statecall){call("WrenchEvCancel" @ strReplace(%brick.evName[%state - 1]," ","_"),%brick,%state-1);}
     %brick.ticknum = -1;%brick.curState++;
    }
   }

  case "Impact":
   %data = %brick.getdatablock();
   if((%brick.angleID % 2) == 1){%bX = %data.bricksizeY;%bY = %data.bricksizeX;}else{%bX = %data.bricksizeX;%bY = %data.bricksizeY;}
   initContainerBoxSearch(%brick.getWorldBoxCenter(), %bX/2 SPC %bY/2 SPC 4, $typemasks::playerobjecttype | $typemasks::vehicleobjecttype);
   while (%searchObj= containerSearchNext())
   {
    %x1 = getWord(vectorAdd(%searchObj.getPosition(),"0 0 1.5"),0);
    %y1 = getWord(vectorAdd(%searchObj.getPosition(),"0 0 1.5"),1);
    %x2 = getWord(%brick.getWorldBoxCenter(),0);
    %y2 = getWord(%brick.getWorldBoxCenter(),1);
    %w = %bX/4;
    %h = %bY/4;
    if(%searchObj.getClassName() !$= "Player"){%add = 3;}else{%add = 1;}
    %z1 = getWord(vectorAdd(%searchObj.getPosition(),"0 0 1.5"),2);
    %z2 = getWord(%brick.getWorldBoxCenter(),2);
    if(%x1 > %x2-%w-%add && %x1 < %x2+%w+%add && %y1 > %y2-%h-%add && %y1 < %y2+%h+%add && %z1 > %z2-3 && %z1 < %z2+5 && vectorLen(%searchObj.getVelocity()) > 16 || mAbs(getWord(%searchObj.getVelocity(),2)) > 9)
    {
     if(%searchObj.getClassName() $= "Player" && (%str = wrenchTrust(%brick,%searchobj.client)) !$= "1")
     {
      commandtoclient(%searchobj.client,'centerprint',%str,2,3);
      return;
     }
    if(%statecall){call("WrenchEvCancel" @ strReplace(%brick.evName[%state - 1]," ","_"),%brick,%state-1);}
     %brick.ticknum = -1;%brick.curState++;
    }
   }
 }
}

function WrenchTrust(%brick,%client)
{
 %brick = %brick.getGroup();
 switch(%brick.evTrust)
 {
  case 0: return 1;
  case 1: return ((getTrustLevel(%brick,%client) >= 1) ? "1" : "\c0" @ %brick.name SPC "does not trust you enough to do that.");
  case 2: return ((getTrustLevel(%brick,%client) >= 2) ? "1" : "\c0" @ %brick.name SPC "does not trust you enough to do that.");
  case 3: if(isObject($TeamDM::Minigame)){return 1;}
	if(%brick.client.minigame !$= %client.minigame && isObject(%client.minigame)){return "\c0" @ %brick.name SPC "isn't in your minigame.";}
	if(!isObject(%client.minigame) && isObject(%brick.client.minigame)){return "\c0" @ %brick.name SPC "is in a minigame.";}
	return 1;
 }
}

function servercmdSaveEvents(%client,%file)
{
 %file = strReplace(%file,".Wsave","");
 if(%file $= "")
 {
  messageclient(%client,"\c5You need to choose a file to save to.");
  return;
 }
 %file = %file @ ".Wsave";
  messageclient(%client,'',"\c5Saving... (base/config/client/wrenchsaves/" @ %file @ ")");
 commandtoclient(%client,'clearwrenchsavefile',%file);
 for(%i=0;%i<$wrenchCtrl.count;%i++)
 {
  %val = $wrenchCtrl.getValue(%i);
  %str = "";
  commandtoclient(%client,'appendwrenchsavefile',%file,strReplace(%val.getposition()," ","_"),"CLEAR");
  commandtoclient(%client,'appendwrenchsavefile',%file,strReplace(%val.getposition()," ","_"),"MISC" SPC strReplace(%val.evGroup," ","_") SPC %val.evTrust);
  for(%j=0;%j<10;%j++)
  {
   commandtoclient(%client,'appendwrenchsavefile',%file,strReplace(%val.getposition()," ","_"),
   "EVENT" TAB
   strReplace(%val.evName[%j]," ","_") TAB
   %val.evState[%j] TAB
   %val.evGroup TAB
   strReplace(%val.evParam[%j,0]," ","_") TAB
   strReplace(%val.evParam[%j,1]," ","_") TAB
   strReplace(%val.evParam[%j,2]," ","_") TAB
   strReplace(%val.evParam[%j,3]," ","_") TAB
   strReplace(%val.evParam[%j,4]," ","_"));
  }
 }
  messageclient(%client,'',"\c5Finished.");
}

function servercmdLoadEvents(%client,%file)
{
 if(!%client.isSuperAdmin)
 {
  messageclient(%client,"\c5You must be a \c3Super Admin\c5 to upload event saves.");
  return;
 }
 if(isObject($EventBricks::Server::Loading))
 {
  messageclient(%client,"\c5Someone else is already loading.");
  return;
 }
 %file = strReplace(%file,".Wsave","");
 if(%file $= "")
 {
  messageclient(%client,"\c5You need to choose a file to load.");
  return;
 }
 messageall('MsgUploadStart',"\c3" @ %client.name @ "\c0 is loading a \c3Wrench Events\c0 save.");
 $EventBricks::Server::Loading = %client;
 %f = new fileobject();
 %f.openForWrite("base/config/client/WrenchSaves/tempsave.Wsave");
 %f.close();
 %f.delete();
 commandtoclient(%client,'wrenchLoadAccepted',%file);
}

function servercmdLoadEvents_line(%client,%l0,%l1,%l2,%l3,%l4,%l5,%l6,%l7)
{
 if(!%client.isSuperAdmin)
 {
  messageclient(%client,"\c5You must be a \c3Super Admin\c5 to upload event saves.");
  return;
 }
 if(!$EventBricks::Server::Loading)
 {
  messageclient(%client,"\c5You aren't loading a file!");
  return;
 }
 %f = new fileobject();
 %f.openForAppend("base/config/client/WrenchSaves/tempsave.Wsave");
 for(%i=0;%i<8;%i++){if(%l[%i] !$= ""){%f.writeLine(%l[%i]);}}
 %f.close();
 %f.delete();
 commandtoclient(%client,'wrenchLoadNext');
}

function servercmdLoadEvents_fin(%client)
{
 if(!%client.isSuperAdmin)
 {
  messageclient(%client,"\c5You must be a \c3Super Admin\c5 to upload event saves.");
  return;
 }
 if(!$EventBricks::Server::Loading)
 {
  messageclient(%client,"\c5You aren't loading a file!");
  return;
 }
 messageall('MsgUploadEnd',"\c0Upload complete. Loading.");
 $EventBricks::Server::Loading = 0;
 //$EventBricks::Server::LoadTime = $Sim::Time;
 loadEvents(%client,"base/config/client/WrenchSaves/tempsave.Wsave");
}

function loadEvents(%client,%file)
{
 %oldbrick = %client.wrenchbrick;
 %f = new fileobject();
 %f.openForRead(%file);
 %count = 0;
 %typecount = 0;
 for(%i=0;(%line=%f.readLine()) !$= "";%i++)
 {
  if(%line $= ""){continue;}
  %p = getWord(%line,0); //firstWord doesn't take into account TAB
  initContainerBoxSearch(strReplace(%p,"_"," "),"0.1 0.1 0.1",$TypeMasks::FXBrickObjectType);
  %obj = containerSearchNext();
  if(!%type[%p]){%type[%p] = 1;%typecount++;}
  if(!isObject(%obj) || %obj.isDead() || !%obj.isPlanted){continue;}
  if(!%found[%p]){%found[%p] = 1;%count++;}
  %client.wrenchbrick = %obj;
  if(getWord(%line,1) $= "CLEAR")
  {
   servercmdClearWrenchEvents(%client);
  }
  else if(getWord(%line,1) $= "MISC")
  {
   servercmdWrenchEvGroup(%client,strReplace(getWord(%line,2),"_"," "),getWord(%line,3));
  }
  else
  {
   //%client,%type,%length,%group,%e0,%e1,%e2,%e3,%e4
   if(getWord(%line,2) !$= "")
   {
    servercmdWrenchEventAdd(%client,
	strReplace(getWord(%line,2),"_"," "),
	strReplace(getWord(%line,3),"_"," "),
	strReplace(getWord(%line,4),"_"," "),
	strReplace(getWord(%line,5),"_"," "),
	strReplace(getWord(%line,6),"_"," "),
	strReplace(getWord(%line,7),"_"," "),
	strReplace(getWord(%line,8),"_"," "),
	strReplace(getWord(%line,9),"_"," "),
	strReplace(getWord(%line,10),"_"," "));
   }
  }
 }
 %f.close();
 %f.delete();
 messageall('MsgProcessComplete',"\c0" @ %count @ " / " @ %typecount @ " bricks loaded successfully.");
 %client.wrenchbrick = %oldbrick;
}


//clearevents commands
function servercmdclearallevents(%c)
{
 if(!%c.isSuperAdmin){return;}
 for(%i=0;%i<mainbrickgroup.getcount();%i++)
 {
  %a = mainbrickgroup.getobject(%i);
  for(%j = 0;%j<%a.getCount();%j++)
  {
  %b = mainbrickgroup.getobject(%i).getobject(%j);
   if(%b.evName[0] !$= "")
   {
    %b.killbrick();
   }
  }
 }
 messageall('MsgClearBricks',"\c3" @ %c.name @ "\c0 has cleared all event bricks.");
}

function servercmdclearhisevents(%c,%n,%a,%m,%e)
{
 if(!%c.isSuperAdmin){return;}
 %a = trim(%n SPC %a SPC %m SPC %e);
 for(%i=0;%i<mainbrickgroup.getcount();%i++)
 {
  if(mainbrickgroup.getobject(%i).name $= %a){%group = mainbrickgroup.getobject(%i);}
 }
 if(!isObject(%group)){return;}
 for(%i=0;%i<%group.getcount();%i++)
 {
  %b = %group.getobject(%i);
   if(%b.evName[0] !$= "")
   {
    %b.killbrick();
   }
 }
 messageall('MsgClearBricks',"\c3" @ %c.name @ "\c2 has cleared \c3" @ %group.name @ "'s\c2 event bricks.");
}

function servercmdclearevents(%c)
{
 %group = %c.brickgroup;
 if(!isObject(%group)){return;}
 for(%i=0;%i<%group.getcount();%i++)
 {
  %b = %group.getobject(%i);
   if(%b.evName[0] !$= "")
   {
    %b.killbrick();
   }
 }
 messageall('MsgClearBricks',"\c3" @ %c.name @ "\c2 has cleared \c3" @ %group.name @ "'s\c2 event bricks.");
}

datablock ProjectileData(wrenchFizzlerProjectile : arrowprojectile)
{
	muzzleVelocity = 100;
	lifetime = 300;
	directDamage        = 0;
	directDamageType    = $DamageType::fakegen;
	projectileShapeName = "./shapes/blank.dts";
	radiusDamage        = 0;
	damageRadius        = 0;
	radiusDamageType    = $DamageType::fakegen;
	explosion = "";
   	particleEmitter     = FizzlerEmitter;

};

datablock ParticleData(FizzlerParticle : LaserParticleA)
{
	colors[0]	= "0.0 0.0 1.0 1.0";
	colors[1]	= "0.0 0.0 1.0 1.0";
	colors[2]	= "0.0 0.0 1.0 0.0";
};

datablock ParticleEmitterData(FizzlerEmitter)
{
   particles = FizzlerParticle;   
};

function wrenchFizzlerProjectile::onCollision(%this,%obj,%col,%pos,%fade,%normal)
 {
	if(%col.getClassName() $= "Player")
	{
		serverCmdCancelBrick(%col.client);
	}
	if(%col.getClassName() $= "WheeledVehicle")
	{
		%col.spawnBrick.spawnVehicle();
	}
 }