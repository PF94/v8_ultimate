// Behaviours:
// Stand
// Walk
// Crouch
// Jump
// Follow Player
// Attack - Done
// Patrol PathID
// Pursue (= Attack + Follow) Player - Done
// Guard (= Attack + Follow + Patrol) PathID
// AI
// Fighter
// Talk Line

/////////////////////
//Pref Setting//
/////////////////////
if($Pref::SpaceBots::MaxBotsPlayer $= "")
{
 $Pref::SpaceBots::MaxBotsPlayer = 2;
 $Pref::SpaceBots::MaxBotsAdmin = 5;
 $Pref::SpaceBots::MaxBotsSAdmin = -1;
 $Pref::SpaceBots::MaxAIPlayer = 1;
 $Pref::SpaceBots::MaxAIAdmin = 3;
 $Pref::SpaceBots::MaxAISAdmin = 99;
}

//Change to 0 for less lag but bots may walk into walls etc.
$SpaceBots::Server::PathFinding = 1;


//////////////////////////////////////////
//SpaceTick and BotCtrlSO//
/////////////////////////////////////////

if(!isEventPending($spaceTick))
{
 if(isFile("Add-Ons/Support_SpaceTick.cs.noexec"))
 {
  exec("Add-Ons/Support_SpaceTick.cs.noexec");
 }
 else
 {
  if($Server::Dedicated)
  {
   echo("Bots Mod:");
   echo("Add-Ons/Support_SpaceTick.cs.noexec not found. Reinstall the mod. Correctly.");
  }
  else
  {
   schedule(2000,0,clientcmdMessageBoxOK,"Bots Mod","Add-Ons/Support_SpaceTick.cs.noexec not found. Reinstall the mod. Correctly.");
  }  
  return;
 }
}

function BotCtrlSO::addValue(%this,%obj)
{
 %this.value[%this.count] = %obj;
 %this.count++;
}

function BotCtrlSO::delValue(%this,%index)
{
 if(%index < 0 || %index > %this.count){error("[BotCtrlSO::delValue] ERROR: Array index out of bounds. " @ %index @ "/" @ %this.count);return;}
 %this.value[%index] = "";
 for(%i = %index+1;%i<%this.count;%i++)
 {
  %this.value[%i-1] = %this.value[%i];
 }
 %this.count--;
}

function BotCtrlSO::delValueID(%this,%obj)
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
 warn("[BotCtrlSO::delValueID()] " @ %obj @ " does not exist in the stack.");
}

function BotCtrlSO::getValue(%this,%index)
{
 if(%index < 0 || %index > %this.count){error("[BotCtrlSO::getValue] ERROR: Array index out of bounds. " @ %index @ "/" @ %this.count);return -1;}
 return %this.value[%index];
}

function BotCtrlSO::dumpStack(%this)
{
 echo("[BotCtrlSO::dumpStack()]");
 echo("Total Values: " @ %this.count);
 for(%i=0;%i<%this.count;%i++)
 {
  echo(">Value " @ %i @ ": " @ %this.value[%i]);
 }
}

function BotCtrlSO::tick(%this)
{
 if(isObject($BotSpawnCtrl)){$BotSpawnCtrl.tick();}
 %this.updatetimer++;
 if(%this.updatetimer == 10000)
 {
  %this.updatetimer = 0;
 }
 if($SpaceBots::Server::LoadMode)
 {
  %line = $SpaceBots::Server::LoadObject.line[$SpaceBots::Server::LoadCount];
  if(!isObject($SpaceBots::Server::Loading))
  {
   messageall('MsgProcessComplete',"\c0The loading client has left the game. Cancelled.");
   $SpaceBots::Server::Loading = "";
   $SpaceBots::Server::LoadMode = 0;
   $SpaceBots::Server::LoadCount = 0;
   %line = "ABORT";
  }
  if($SpaceBots::Server::LoadCount >= $SpaceBots::Server::LoadObject.linecount)
  {
   messageall('MsgProcessComplete',"\c0Finished.");
   $SpaceBots::Server::Loading = "";
   $SpaceBots::Server::LoadMode = 0;
   $SpaceBots::Server::LoadCount = 0;
   %line = "ABORT";
  }
  switch$(getField(%line,0))
  {
   case "BOT":
    if((%msg = createBotAllowed($SpaceBots::Server::Loading,getField(%line,3))) $= "1")
    {
     %bot = spawnBot(getField(%line,1));
     %bot.name = getField(%line,2);
     if(%bot.name $= ""){%bot.name = "Blockhead";}
     %bot.client = %bot;
     %bot.player = %bot;
     %bot.ownerClient = $SpaceBots::Server::Loading;
     %bot.ownerID = $SpaceBots::Server::Loading.bl_id;
     %bot.isPermenant = getField(%line,5);
     %bot.behaviour = getField(%line,3);
     %bot.bArg = getField(%line,4);
     %bot.isFirstSpawned = 1;
     %bot.setShapeName(%bot.name);
     %line2 = $SpaceBots::Server::LoadObject.line[$SpaceBots::Server::LoadCount++];
     %a = getFields(%line2,1);
     if(%a $= "")
     {
      %a = 	"0" TAB "1 1 1 1" TAB
			"0" TAB "1 0 0 1" TAB
			"0" TAB "1 0 0 1" TAB
			"0" TAB "1 0.878431 0.611765 1" TAB
			"0" TAB "1 0.878431 0.611765 1" TAB
			"0" TAB "0 0 1 1" TAB
			"0" TAB "0 0 1 1" TAB
			"0" TAB "0 0 1 1" TAB
			"0" TAB "0 0 0 0" TAB
			"0" TAB "0 0 0 0" TAB
			"0" TAB "0 0 0 0" TAB
			"0" TAB "0 0 0 0" TAB
			"1 0.878431 0.611765 1" TAB
			"AAA-None" TAB "smileyCreepy";
     }
     %bot.appearance = %a;
     botAppearance(%bot);
     %team = getField(%line,6);
     if(isObject($TeamDM::TeamManager) && %team !$= "")
     {
      for(%i = 0;%i<$teamDM::TeamManager.teamcount;%i++)
      {
       if($teamDM::TeamManager.team[%i].name $= %team){%goteam = $teamDM::TeamManager.team[%i];}
      }
      if(isObject(%goteam))
      {
       %bot.tdmteam = %goteam;
      }
     }
     for(%i=0;%i<clientgroup.getCount();%i++)
     {
      %c = clientgroup.getobject(%i);
      if(%c.hasSBotGUI)
      {
       commandtoclient(%c,'SBotInformation',%bot.getPosition(),%bot.name TAB %bot.behaviour TAB %bot.bArg TAB %bot.isPermenant TAB %team,%bot.appearance);
      }
     }
     $BotCtrl.addValue(%bot);
    }
    else
    {
     $SpaceBots::Server::LoadCount++; //Skip next APP for errors
     commandtoclient($SpaceBots::Server::Loading,'centerprint',%msg,2,3);
    }
   case "SPAWN":
    $SpaceBots::Server::LoadNextBrick = 0;
    initContainerBoxSearch(posFromTransform(getField(%line,1)),"0.1 0.1 0.1",$TypeMasks::FxBrickObjectType);
    %brick = containerSearchNext();
    if(!isObject(%brick))
    {
     %brick = new fxDTSBrick(){datablock = brickSBotSpawnData;position = posFromTransform(getField(%line,1));rotation = "0 0 1 " @ getField(%line,2)*90;angleID = getField(%line,2);};
     if(%brick.plant())
     {
      %brick.delete();
     }
     else
     {
      $SpaceBots::Server::Loading.brickgroup.add(%brick);
      %brick.setColor(getField(%line,3));
      %brick.setColorFX(getField(%line,4));
      %brick.setShapeFX(getField(%line,5));
     }
    }
    $SpaceBots::Server::LoadNextBrick = %brick;
   case "DATA":
    if(isObject($SpaceBots::Server::LoadNextBrick))
    {
     %a = getFields($SpaceBots::Server::LoadObject.line[$SpaceBots::Server::LoadCount++],1); //Appearance
     %client = $SpaceBots::Server::Loading;
     %ms = getField(%line,1);
     %max = getField(%line,2);
     %name = getField(%line,3);
     %ai = getField(%line,4) TAB getField(%line,5);
     %team = getField(%line,6);
     if(%ms > 100000)
     {
      commandtoclient(%client,'centerprint',"Bots must spawn at least once every 100 seconds.",2,3);
      %ms = 100000;
     }
     if(%max > 15)
     {
      commandtoclient(%client,'centerprint',"You can only have 15 bots per spawn.",2,3);
      %max = 15;
     }
     if(%max < 0)
     {
      %max = 0;
     }
     if(%ms < 100)
     {
      commandtoclient(%client,'centerprint',"You can only spawn bots every 100ms.",2,3);
      %ms = 100;
     }
     if(%a $= "")
     {
      %a = 	"0" TAB "1 1 1 1" TAB
			"0" TAB "1 0 0 1" TAB
			"0" TAB "1 0 0 1" TAB
			"0" TAB "1 0.878431 0.611765 1" TAB
			"0" TAB "1 0.878431 0.611765 1" TAB
			"0" TAB "0 0 1 1" TAB
			"0" TAB "0 0 1 1" TAB
			"0" TAB "0 0 1 1" TAB
			"0" TAB "0 0 0 0" TAB
			"0" TAB "0 0 0 0" TAB
			"0" TAB "0 0 0 0" TAB
			"0" TAB "0 0 0 0" TAB
			"1 0.878431 0.611765 1" TAB
			"AAA-None" TAB "smileyCreepy";
     }
     if(%name $= "")
     {
      %name = "Blockhead";
     }
     %brick = $SpaceBots::Server::LoadNextBrick;
     %brick.botName = %name;
     %brick.botAI = %ai;
     %brick.appearance = %a;
     %brick.botTeam = "";
     if(isObject($teamDM::teamManager))
     {
      for(%i=0;%i<$teamdm::teammanager.teamcount;%i++)
      {
       if($teamdm::teammanager.team[%i].name $= %team){%brick.botTeam = $teamdm::teammanager.team[%i];break;}
      }
     }	
     %brick.botMS = %ms;
     %brick.botMax = %max;
     for(%i=0;%i<$BotCtrl.count;%i++)
     {
      %b = $botctrl.getValue(%i);
      if(%b.isBotSpawned $= %brick){%b.removebody();}
     }
     %brick.ticknum = %brick.botMS;
     if(!%brick.listAdded)
     {
      	 if(!isObject($botSpawnCtrl)){$botSpawnCtrl = new ScriptObject(){class = BotSpawnCtrlSO;count = 0;};}
 	 %brick.listAdded = 1;
 	 $botSpawnCtrl.addValue(%brick);
     }
     for(%i=0;%i<clientgroup.getcount();%i++)
     {
      %c=clientgroup.getobject(%i);
      if(%c.hasSBotGUI)
      {
       commandtoclient(%c,'botSpawnInformation',%brick.getPosition(),0,%brick.botMS TAB %brick.botMax TAB %brick.botName TAB %brick.botAI TAB %brick.botTeam.name);
       commandtoclient(%c,'botSpawnInformation',%brick.getPosition(),1,%brick.appearance);
      }
     }
    }
    else
    {
     error("[BotCtrlSO::tick] ERROR: $SpaceBots::Server::LoadNextBrick does not exist!\n> " @  %line);
    }
   case "ABORT":
    //Quit loading
   default:
    messageall('',"\c0Error while loading. Cancelled.");
    error("[BotCtrlSO::tick] ERROR: Unrecognised line in file.\n> " @  %line);
    $SpaceBots::Server::Loading = "";
    $SpaceBots::Server::LoadMode = 0;
    $SpaceBots::Server::LoadCount = 0;
  }
  $SpaceBots::Server::LoadCount++;
 }
 %timer = %this.updateTimer % 5;
 for(%i=0;%i<%this.count;%i++)
 {
  %obj = %this.getValue(%i);
  if(!isObject(%obj) || %obj.getState() $= "Dead"){%this.delValue(%i);continue;}
  %iMod5 = (%i+3) % 5;
  if(%timer == %iMod5){updateMove(%obj);updateAim(%obj);}
  //updateMove(%obj);updateAim(%obj);
  if(%obj.isBotSpawned !$= "" && (!isObject(%obj.isBotSpawned) || %obj.isBotSpawned.isDead())){%obj.removebody();}
  if(%obj.minigame !$= %obj.ownerclient.minigame || !isObject(%obj.minigame))
  {
   if(!isObject(%obj.ownerclient.minigame) && %obj.minigame !$= "")
   {
    if(%obj.isFirstCreated){%pos = %obj.getTransform();}else{%pos = "PICK";}
    if(isObject(%obj.isBotSpawned)){%obj.isBotSpawned.ticknum = %obj.isBotSpawned.botMS;%obj.delete();continue;}
    respawnBot("",%obj.ownerclient,%obj.ownerclient.bl_id,%obj.name,%obj.appearance,%obj.tdmteam TAB %obj.isLeader,%obj.behaviour TAB %obj.barg,%pos,%obj.isPermenant);
    %obj.delete();
   }
   if(!isObject(%obj.ownerclient)){%obj.ownerclient = findClientByBL_ID(%obj.ownerid);}
   if(isObject(%obj.ownerclient.minigame) && (%obj.ownerclient.minigame.includeallplayersbricks || %obj.ownerclient.minigame.owner $= %obj.ownerclient))
   {
    if(%obj.isFirstCreated){%pos = %obj.getTransform();}else{%pos = "PICK";}
    if(isObject(%obj.isBotSpawned)){%obj.isBotSpawned.ticknum = %obj.isBotSpawned.botMS;%obj.delete();continue;}
    respawnBot(%obj.ownerclient.minigame,%obj.ownerclient,%obj.ownerclient.bl_id,%obj.name,%obj.appearance,%obj.tdmteam TAB %obj.isLeader,%obj.behaviour TAB %obj.barg,%pos,%obj.isPermenant);
    %obj.delete();
   }
  }
  if(%obj.behaviour $= "" || strStr(strLwr($SpaceBots::Server::BehaviourList),strLwr(%obj.behaviour)) $= "-1"){%obj.behaviour = "Stand";}
  %string = strReplace(%obj.behaviour," ","_");
  if(%string $= ""){%string = "Stand";}
  call("SBotBehaviour" @ %string,%this,%obj,%i);
 }
}

function BotSpawnCtrlSO::addValue(%this,%obj)
{
 %this.value[%this.count] = %obj;
 %this.count++;
}

function BotSpawnCtrlSO::delValue(%this,%index)
{
 if(%index < 0 || %index > %this.count){error("[BotCtrlSO::delValue] ERROR: Array index out of bounds. " @ %index @ "/" @ %this.count);return;}
 %this.value[%index] = "";
 for(%i = %index+1;%i<%this.count;%i++)
 {
  %this.value[%i-1] = %this.value[%i];
 }
 %this.count--;
}

function BotSpawnCtrlSO::delValueID(%this,%obj)
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
 warn("[BotCtrlSO::delValueID()] " @ %obj @ " does not exist in the stack.");
}

function BotSpawnCtrlSO::getValue(%this,%index)
{
 if(%index < 0 || %index > %this.count){error("[BotCtrlSO::getValue] ERROR: Array index out of bounds. " @ %index @ "/" @ %this.count);return -1;}
 return %this.value[%index];
}

function BotSpawnCtrlSO::dumpStack(%this)
{
 echo("[BotCtrlSO::dumpStack()]");
 echo("Total Values: " @ %this.count);
 for(%i=0;%i<%this.count;%i++)
 {
  echo(">Value " @ %i @ ": " @ %this.value[%i]);
 }
}

function BotSpawnCtrlSO::tick(%this)
{
 for(%i=0;%i<$botctrl.count;%i++)
 {
  %obj = $botctrl.value[%i];
  %botcount[%obj.ownerID]++;
  if(%obj.behaviour $= "AI"){%aicount[%obj.ownerID]++;}
  if(%obj.isBotSpawned){%spawncount[%obj.isBotSpawned]++;}
 }
 for(%i=0;%i<%this.count;%i++)
 {
  %obj = %this.getValue(%i);
  if(!isObject(%obj) || %obj.isDead())
  {
   %this.delValue(%i);
   continue;
  }
  %obj.ticknum+=10;
  if(%obj.ticknum > %obj.botMS)
  {
   %obj.ticknum = 0;
   %count = 0;
   %client = %obj.getGroup().client;
   %max = $Pref::SpaceBots::MaxBotsPlayer;
   if(%client.isAdmin){%max = $Pref::SpaceBots::MaxBotsAdmin;}
   if(%client.isSuperAdmin){%max = $Pref::SpaceBots::MaxBotsSAdmin;}
   %AImax = $Pref::SpaceBots::MaxAIPlayer;
   if(%client.isAdmin){%AImax = $Pref::SpaceBots::MaxAIAdmin;}
   if(%client.isSuperAdmin){%AImax = $Pref::SpaceBots::MaxAISAdmin;}
   if(((%botcount[%client.bl_id] >= %max && %max !$= "-1")) || (%spawncount[%obj] >= %obj.botMax) || (getField(%obj.botAI,0) $= "AI" && %aicount[%client.bl_id] >= %aiMAX)){%obj.ticknum = %obj.botMS - 500;continue;}
   %b = spawnBot(%obj.getTransform());
   %minigame = "";
   if(isObject(%client.minigame) && (%client.minigame.includeallplayersbricks || %client.minigame.owner $= %client))
   {
    %minigame = %client.minigame;
   }
   %b.emote(spawnProjectile,1);
   if(isObject(%minigame.playerdatablock))
   {
    %b.setDatablock(%minigame.playerdatablock);
   }
   if(isObject(%minigame))
   {
    %b.shapename = %b.schedule(100,setShapeNameColor,$MinigameColorF[%minigame.colorIdx] SPC "1");
    %b.minigame = %minigame;
    for(%i=0;%i<5;%i++)
    {
     %b.tool[%i] = (isObject(%b.minigame.startequip[%i]) ? %b.minigame.startequip[%i] : 0);
    }
   }
   %b.name = %obj.botName;
   %b.client = %b;
   %b.player = %b;
   %b.ownerClient = %client;
   %b.ownerID = %client.bl_id;
   %b.isPermenant = 0;
   %b.isBotSpawned = %obj;
   %b.behaviour = getField(%obj.botAI,0);
   %b.bArg = getField(%obj.botAI,1);
   %b.isFirstSpawned = 1;
   %b.setShapeName(%b.name);
   %botcount[%client.bl_id]++;
   %spawncount[%obj]++;
   %b.appearance = %obj.appearance;
   botAppearance(%b);
   %team = %obj.botTeam;
   if(isObject(%team))
   {
    %b.tdmteam = %team;
    if(isObject(%minigame) && $teamdm::teamson)
    {
     cancel(%b.shapename);
     schedule(100,0,botTeamSetup,%b,%team,1);
    }
   }
   else
   {
    %b.tdmteam = "";
   }
   $BotCtrl.addValue(%b);
  }
 }
}

////////////////////
//Behaviours//
////////////////////
$SpaceBots::Server::BehaviourList = "";
$SpaceBots::Server::BehaviourControls = "";

$SpaceBots::Server::BehaviourList = $SpaceBots::Server::BehaviourList @ "Stand" @ "\t";
$SpaceBots::Server::BehaviourControls = $SpaceBots::Server::BehaviourControls @ "0" @ "\t";
function SBotBehaviourStand(%this,%obj,%count)
{
 //Stand There
}

$SpaceBots::Server::BehaviourList = $SpaceBots::Server::BehaviourList @ "Walk" @ "\t";
$SpaceBots::Server::BehaviourControls = $SpaceBots::Server::BehaviourControls @ "0" @ "\t";
function SBotBehaviourWalk(%this,%obj,%count)
{
    if($SpaceBots::Server::PathFinding == 0)
    {
     if(%obj.randdir $= ""){%obj.randdir = 15;}
     if(%obj.timer > 0){%obj.timer--;}
      %vec = %obj.getForwardVector();
      %pos = %obj.getEyePoint();
      %endpos = vectorAdd(%pos,vectorScale(%vec,7.5));
      %masks = $TypeMasks::PlayerObjectType | $TypeMasks::VehicleObjectType | $TypeMasks::TerrainObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::FXBrickObjectType;
      %block = firstWord(containerRayCast(%pos,%endpos,%masks,%obj));
      if(%block !$= "0")
      {
       %vec = %obj.getForwardVector();
       %ang = %obj.randdir*$pi/180;
       %x = getWord(%vec,0);%y = getWord(%vec,1);
       %newX = mCos(%ang)*%x - mSin(%ang)*%y;
       %newY = mSin(%ang)*%x + mCos(%ang)*%y;
       %aimVec = %newX SPC %newY SPC getWord(%vec,2);
       %endpos = vectorAdd(%obj.getEyePoint(),vectorScale(%aimVec,100));
       %obj.timer = 400;
       %obj.randomturn = 0;
      }else if(%obj.timer == 0)
      {
       %obj.randdir = -%obj.randdir;
       //%obj.timer = 100;
      }
      %obj.setAimLocation(%endpos);
      %obj.setMoveDestination(%endpos,0); //,0 = No slowdown
     }
     else
    {
     if(getRandom(0,400) $= "0" && %obj.timer == 0 && %obj.randomturn == 0){%obj.randomturn = 30;}
     if(%obj.randdir $= ""){%obj.randdir = 15;}
     if(%obj.timer > 0){%obj.timer--;}
      %vec = %obj.getForwardVector();
      %pos = %obj.getEyePoint();
      %endpos = vectorAdd(%pos,vectorScale(%vec,7.5));
      %masks = $TypeMasks::PlayerObjectType | $TypeMasks::VehicleObjectType | $TypeMasks::TerrainObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::FXBrickObjectType;
      %block = firstWord(containerRayCast(%pos,%endpos,%masks,%obj));
      if(%block !$= "0")
      {
       %vec = %obj.getForwardVector();
       %ang = %obj.randdir*$pi/180;
       %x = getWord(%vec,0);%y = getWord(%vec,1);
       %newX = mCos(%ang)*%x - mSin(%ang)*%y;
       %newY = mSin(%ang)*%x + mCos(%ang)*%y;
       %aimVec = %newX SPC %newY SPC getWord(%vec,2);
       %endpos = vectorAdd(%obj.getEyePoint(),vectorScale(%aimVec,100));
       %obj.timer = 400;
       %obj.randomturn = 0;
      }else if(%obj.randomturn)
      {
       %vec = %obj.getForwardVector();
       %ang = sign(%obj.randdir)*$pi/180;
       %x = getWord(%vec,0);%y = getWord(%vec,1);
       %newX = mCos(%ang)*%x - mSin(%ang)*%y;
       %newY = mSin(%ang)*%x + mCos(%ang)*%y;
       %aimVec = %newX SPC %newY SPC getWord(%vec,2);
       %endpos = vectorAdd(%obj.getEyePoint(),vectorScale(%aimVec,100));
       %obj.randomturn--;
      } else if(%obj.timer == 0)
      {
       %obj.randdir = -%obj.randdir;
       //%obj.timer = 100;
      }
      %obj.setAimLocation(%endpos);
      %obj.setMoveDestination(%endpos,0); //,0 = No slowdown
     }
}

$SpaceBots::Server::BehaviourList = $SpaceBots::Server::BehaviourList @ "Crouch" @ "\t";
$SpaceBots::Server::BehaviourControls = $SpaceBots::Server::BehaviourControls @ "0" @ "\t";
function SBotBehaviourCrouch(%this,%obj,%count)
{
 %obj.setCrouch(1);
}

$SpaceBots::Server::BehaviourList = $SpaceBots::Server::BehaviourList @ "Jump" @ "\t";
$SpaceBots::Server::BehaviourControls = $SpaceBots::Server::BehaviourControls @ "0" @ "\t";
function SBotBehaviourJump(%this,%obj,%count)
{
 %obj.setJump(1);
}

$SpaceBots::Server::BehaviourList = $SpaceBots::Server::BehaviourList @ "Follow" @ "\t";
$SpaceBots::Server::BehaviourControls = $SpaceBots::Server::BehaviourControls @ "1" @ "\t";
function SBotBehaviourFollow(%this,%obj,%count)
{
   if((%this.updatetimer % 5) != (%count % 5)){return;}
   %n = findClientByName(%obj.bArg);
   if(%obj.bArg $= "" || %n $= "0")
   {
    %n = findClientByBL_ID(%obj.ownerID);
   }
   if(isObject(%n.player) && %n.player.getState() !$= "Dead")
   {
    %obj.setAimLocation(vectorAdd(%n.player.getPosition(),"0 0 2"));
    %obj.setMoveDestination(%n.player.getPosition(),0);
   }
   else
   {
    %p = %obj.getPosition();%vec = %obj.getForwardVector();
    %p = vectorAdd(%p,vectorScale(%vec,50));
    %obj.setAimLocation(vectorAdd(%p,"0 0 2"));
    %obj.setMoveDestination(%obj.getPosition(),0);
   }
}

$SpaceBots::Server::BehaviourList = $SpaceBots::Server::BehaviourList @ "Attack" @ "\t";
$SpaceBots::Server::BehaviourControls = $SpaceBots::Server::BehaviourControls @ "0" @ "\t";
function SBotBehaviourAttack(%this,%obj,%count)
{
   if((%this.updatetimer % 5) != (%count % 5)){return;}
   if(%obj.normalLook $= ""){%obj.normalLook = %obj.getForwardVector();}
   %range = 150;
   if(isObject(%obj.target) && %obj.target.getState() !$= "Dead")
   {
    %searchObj = %obj.target;
    if(botisAllied(%obj,%searchObj)){%obj.target = "";return;}
    %p = %searchObj.gethackposition();
    if(vectorDist(%obj.getHackPosition(),%p) < %range)
    {
     if(isObject(%obj.getMountedImage(0).projectile))
     {
       %d = vectorDist(getWords(%obj.getHackPosition(),0,1),getWords(%p,0,1));
       %amount = 1.5*(%d/%obj.getMountedImage(0).projectile.muzzlevelocity);
       %p = vectorAdd(%p,vectorScale(getWords(%searchObj.getVelocity(),0,1),%amount));
       if(%obj.getMountedImage(0).projectile.gravitymod > 0){%p = vectorAdd(%p,"0 0 0.5");}
     }
     %obj.fireAt(%p);
     %obj.fired = 1;
     %obj.targetfound = 1;
     return;
    }
   }
   initContainerRadiusSearch(%obj.position,%range,$TypeMasks::PlayerObjectType);
   %obj.fired = 0;
   %obj.targetfound = 0;
   while(isObject(firstWord(%searchObj = containerSearchNext())))
   {
    if(%searchObj.getState() $= "Dead"){continue;}
    if(%searchObj $= %obj){continue;}
    if(%searchObj.client.minigame !$= %obj.client.minigame){continue;}
    if(botisAllied(%obj,%searchObj)){continue;}
     %a = %obj.getEyeVector();
     %b = vectorNormalize(vectorSub(%obj.getPosition(),%searchObj.getPosition()));
     if(mACos(vectorDot(%a,%b)) > 2.35)
     {
      %p = %searchObj.gethackposition();
      if(isObject(%obj.getMountedImage(0).projectile))
      {
       %d = vectorDist(getWords(%obj.getHackPosition(),0,1),getWords(%p,0,1));
       %amount = 1.5*(%d/%obj.getMountedImage(0).projectile.muzzlevelocity);
       %p = vectorAdd(%p,vectorScale(getWords(%searchObj.getVelocity(),0,1),%amount));
       if(%obj.getMountedImage(0).projectile.gravitymod > 0){%p = vectorAdd(%p,"0 0 0.5");}
      }
      %obj.fireAt(%p);
      %obj.fired = 1;
      %obj.targetfound = 1;
      %obj.target = %searchObj;
      break;
     }
   }
   if(!%obj.fired)
   {
    %obj.setAimLocation(vectorAdd(vectorAdd(%obj.getHackPosition(),vectorScale(%obj.normalLook,12)),"0 0 1"));
    if(isObject(%obj.getMountedImage(0)) && %obj.getMountedImage(0).botweapontype $= "bow")
    {
     %obj.connection.setTrigger(0,0);
    }
   }
   if(!%obj.targetfound){%obj.target = "";}
}

$SpaceBots::Server::BehaviourList = $SpaceBots::Server::BehaviourList @ "Patrol" @ "\t";
$SpaceBots::Server::BehaviourControls = $SpaceBots::Server::BehaviourControls @ "2" @ "\t";
function SBotBehaviourPatrol(%this,%obj,%count)
{
   if((%this.updatetimer % 5) != (%count % 5)){return;}
   if(%obj.followpath $= "")
   {
    for(%i=0;$SpaceBots::Path[%obj.ownerID,%i] !$= "";%i++)
    {
     if(getField($SpaceBots::Path[%obj.ownerID,%i],0) $= %obj.bArg)
     {
      %obj.followpath = %i;
      %obj.randdir = 15;
     }
    }
   }
   %path = $SpaceBots::Path[%obj.ownerID,%obj.followpath];
   if(%path $= "") //It couldn't find a path
   {
    %p = %obj.getPosition();%vec = %obj.getForwardVector();
    %p = vectorAdd(%p,vectorScale(%vec,50));
    %obj.setAimLocation(vectorAdd(%p,"0 0 3"));
    %obj.setMoveDestination(%obj.getPosition(),0);
    return;
   }
   if(%obj.followpoint $= "" || getField(%path,%obj.followpoint) $= ""){%obj.followpoint = 1;}
   %destination = getField(%path,%obj.followpoint);
   %radius = (%obj.isMounted() ? 2.5 : 0.8);
   if($SpaceBots::Server::Pathfinding == 0)
   {
    if(vectorDist(getWords(%obj.getposition(),0,1),getWords(%destination,0,1)) < %radius)
    {
     %obj.followpoint++;
    }
    %obj.setAimLocation(vectorAdd(getField(%path,%obj.followpoint),"0 0 2"));
    %obj.setMoveDestination(%endpos,0);
    return;
   }

   if(vectorDist(getWords(%obj.getposition(),0,1),getWords(getField(%path,%obj.followpoint),0,1)) < %radius)
   {
    %obj.followpoint++;
    if(getField(%path,%obj.followpoint) $= ""){%obj.followpoint = 1;}
    if(getFieldCount(%path) > 2)
    {
     %obj.setAimLocation(vectorAdd(getField(%path,%obj.followpoint),"0 0 2"));
    }
    else
    {
    %p = %obj.getPosition();%vec = %obj.getForwardVector();
    %p = vectorAdd(%p,vectorScale(%vec,50));
    %obj.setAimLocation(vectorAdd(%p,"0 0 3"));
    }
   }
   if(%obj.randdir $= ""){%obj.randdir = 15;}
   if(getFieldCount(%path) > 2 || vectorDist(%obj.getposition(),%obj.getmovedestination()) > %radius)
   {
    %allow = 1;
   }
   else
   {
    %p = %obj.getPosition();%vec = %obj.getForwardVector();
    %p = vectorAdd(%p,vectorScale(%vec,50));
    %obj.setAimLocation(vectorAdd(%p,"0 0 3"));
   }
   if(%this.updatetimer % 2 == 0 && (getFieldCount(%path) > 2 || vectorDist(%obj.getposition(),%obj.getmovedestination()) > %radius))
   {
    %masks = $TypeMasks::PlayerObjectType | $TypeMasks::VehicleObjectType | $TypeMasks::TerrainObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::FXBrickObjectType;
    initContainerBoxSearch(vectorAdd(%obj.getPosition(),"0 0 3"),"8 8 1",%masks);
    %blocked = 0;
    %endpos = getField(%path,%obj.followpoint);
    while(isObject(%boxfound = containerSearchNext()))
    {
     if(%boxfound !$= %obj)
     {
      %blocked = 1;break;
     }
    }
    if(%blocked)
    {
     %start = vectorAdd(%obj.getPosition(),"0 0 2");
     %end = vectorAdd(%start,vectorScale(%obj.getForwardVector(),4));
     %ray = containerRayCast(%start,%end,%masks,%obj);
     if(!isObject(firstWord(%ray)))
     {
      if(%obj.timer){%obj.timer--;return;}
      %obj.setAimLocation(vectorAdd(getField(%path,%obj.followpoint),"0 0 2"));
      %obj.setMoveDestination(%endpos,0);
      %obj.randdir = -%obj.randdir;
      return;
     }
     %vec = %obj.getForwardVector();
     %ang = %obj.randdir*$pi/180;
     %x = getWord(%vec,0);%y = getWord(%vec,1);
     %newX = mCos(%ang)*%x - mSin(%ang)*%y;
     %newY = mSin(%ang)*%x + mCos(%ang)*%y;
     %aimVec = %newX SPC %newY SPC getWord(%vec,2);
     %endpos = vectorAdd(%obj.getEyePoint(),vectorScale(%aimVec,100));
    }
    else
    {
      if(%obj.timer){%obj.timer--;return;}
      %obj.setAimLocation(vectorAdd(getField(%path,%obj.followpoint),"0 0 2"));
      %obj.setMoveDestination(%endpos,0);
      %obj.randdir = -%obj.randdir;
    }
    %obj.setAimLocation(vectorAdd(%endpos,"0 0 2"));
    %obj.setMoveDestination(%endpos,0);
   }
}

$SpaceBots::Server::BehaviourList = $SpaceBots::Server::BehaviourList @ "Guard" @ "\t";
$SpaceBots::Server::BehaviourControls = $SpaceBots::Server::BehaviourControls @ "2" @ "\t";
function SBotBehaviourGuard(%this,%obj,%count)
{
  if((%this.updatetimer % 5) != (%count % 5)){return;}
  if(%obj.startpos $= "")
  {
   %obj.startpos = %obj.getposition();
   %obj.startaim = vectorAdd(vectorAdd(%obj.getposition(),"0 0 3"),vectorScale(%obj.getforwardvector(),50));
  }
  if(%obj.guardalert $= "" || !isObject(%obj.guardalert) || %obj.guardalert.getstate() $= "Dead" || botIsAllied(%obj,%obj.guardalert))
  {
   %obj.guardalert = "";
   if(isObject(%obj.getMountedImage(0)) && %obj.getMountedImage(0).botweapontype $= "bow")
   {
     %obj.connection.setTrigger(0,0);
   }
   %range = 30;
   initContainerRadiusSearch(%obj.position,%range,$TypeMasks::PlayerObjectType);
   %obj.fired = 0;
   %obj.targetfound = 0;
   while(isObject(firstWord(%searchObj = containerSearchNext())))
   {
    if(%searchObj.getState() $= "Dead"){continue;}
    if(%searchObj $= %obj){continue;}
    if(%searchObj.client.minigame !$= %obj.client.minigame){continue;}
    if(botisAllied(%obj,%searchObj)){continue;}
    %a = %obj.getEyeVector();
    %b = vectorNormalize(vectorSub(%obj.getPosition(),%searchObj.getPosition()));
    if(mACos(vectorDot(%a,%b)) > 2.35)
    {
     %obj.guardalert = %searchObj;
    }
   }
   if(%obj.followpath $= "")
   {
    for(%i=0;$SpaceBots::Path[%obj.ownerID,%i] !$= "";%i++)
    {
     if(getField($SpaceBots::Path[%obj.ownerID,%i],0) $= %obj.bArg)
     {
      %obj.followpath = %i;
      %obj.randdir = 15;
     }
    }
   }
   %path = $SpaceBots::Path[%obj.ownerID,%obj.followpath];
   if(%path $= "") //It couldn't find a path
   {
    %obj.setMoveDestination(%obj.startpos,0);
    if(vectorDist(%obj.getposition(),%obj.startpos) < 0.8)
    {
     %obj.setAimLocation(%obj.startaim);
    }
    else
    {
     %obj.setAimLocation(vectorAdd(%obj.startpos,"0 0 3"));
    }
    return;
   }
   if(%obj.followpoint $= "" || getField(%path,%obj.followpoint) $= ""){%obj.followpoint = 1;}
   %destination = getField(%path,%obj.followpoint);
   %radius = (%obj.isMounted() ? 2.5 : 0.8);
   if($SpaceBots::Server::Pathfinding == 0)
   {
    if(vectorDist(getWords(%obj.getposition(),0,1),getWords(%destination,0,1)) < %radius)
    {
     %obj.followpoint++;
    }
    %obj.setAimLocation(vectorAdd(getField(%path,%obj.followpoint),"0 0 2"));
    %obj.setMoveDestination(%endpos,0);
    return;
   }

   if(vectorDist(getWords(%obj.getposition(),0,1),getWords(getField(%path,%obj.followpoint),0,1)) < %radius)
   {
    %obj.followpoint++;
    if(getField(%path,%obj.followpoint) $= ""){%obj.followpoint = 1;}
    if(getFieldCount(%path) > 2)
    {
     %obj.setAimLocation(vectorAdd(getField(%path,%obj.followpoint),"0 0 2"));
    }
    else
    {
    %p = %obj.getPosition();%vec = %obj.getForwardVector();
    %p = vectorAdd(%p,vectorScale(%vec,50));
    %obj.setAimLocation(vectorAdd(%p,"0 0 3"));
    }
   }
   if(%obj.randdir $= ""){%obj.randdir = 15;}
   if(getFieldCount(%path) > 2 || vectorDist(%obj.getposition(),%obj.getmovedestination()) > %radius)
   {
    %allow = 1;
   }
   else
   {
    %p = %obj.getPosition();%vec = %obj.getForwardVector();
    %p = vectorAdd(%p,vectorScale(%vec,50));
    %obj.setAimLocation(vectorAdd(%p,"0 0 3"));
   }
   if(%this.updatetimer % 2 == 0 && (getFieldCount(%path) > 2 || vectorDist(%obj.getposition(),%obj.getmovedestination()) > %radius))
   {
    %masks = $TypeMasks::PlayerObjectType | $TypeMasks::VehicleObjectType | $TypeMasks::TerrainObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::FXBrickObjectType;
    initContainerBoxSearch(vectorAdd(%obj.getPosition(),"0 0 3"),"8 8 1",%masks);
    %blocked = 0;
    %endpos = getField(%path,%obj.followpoint);
    while(isObject(%boxfound = containerSearchNext()))
    {
     if(%boxfound !$= %obj)
     {
      %blocked = 1;break;
     }
    }
    if(%blocked)
    {
     %start = vectorAdd(%obj.getPosition(),"0 0 2");
     %end = vectorAdd(%start,vectorScale(%obj.getForwardVector(),4));
     %ray = containerRayCast(%start,%end,%masks,%obj);
     if(!isObject(firstWord(%ray)))
     {
      if(%obj.timer){%obj.timer--;return;}
      %obj.setAimLocation(vectorAdd(getField(%path,%obj.followpoint),"0 0 2"));
      %obj.setMoveDestination(%endpos,0);
      %obj.randdir = -%obj.randdir;
      return;
     }
     %vec = %obj.getForwardVector();
     %ang = %obj.randdir*$pi/180;
     %x = getWord(%vec,0);%y = getWord(%vec,1);
     %newX = mCos(%ang)*%x - mSin(%ang)*%y;
     %newY = mSin(%ang)*%x + mCos(%ang)*%y;
     %aimVec = %newX SPC %newY SPC getWord(%vec,2);
     %endpos = vectorAdd(%obj.getEyePoint(),vectorScale(%aimVec,100));
    }
    else
    {
      if(%obj.timer){%obj.timer--;return;}
      %obj.setAimLocation(vectorAdd(getField(%path,%obj.followpoint),"0 0 2"));
      %obj.setMoveDestination(%endpos,0);
      %obj.randdir = -%obj.randdir;
    }
    %obj.setAimLocation(vectorAdd(%endpos,"0 0 2"));
    %obj.setMoveDestination(%endpos,0);
   }
  }
  else //Fire at target
  {
    %n = %obj.guardalert;
    %p = %n.gethackposition();
    if(isObject(%obj.getMountedImage(0).projectile))
    {
     %d = vectorDist(getWords(%obj.getHackPosition(),0,1),getWords(%p,0,1));
     %amount = 1.5*(%d/%obj.getMountedImage(0).projectile.muzzlevelocity);
     %p = vectorAdd(%p,vectorScale(getWords(%n.getVelocity(),0,1),%amount));
     if(%obj.getMountedImage(0).projectile.gravitymod > 0){%p = vectorAdd(%p,"0 0 0.5");}
    }
    %obj.setMoveDestination(%n.getPosition(),0);
    if(vectorDist(%obj.getHackPosition(),%n.getHackPosition()) < 30)
    {
     %obj.fireAt(%p);
    }
    else
    {
     %obj.setAimLocation(vectorAdd(%n.getPosition(),"0 0 2"));
    }
  }
}

$SpaceBots::Server::BehaviourList = $SpaceBots::Server::BehaviourList @ "Pursue" @ "\t";
$SpaceBots::Server::BehaviourControls = $SpaceBots::Server::BehaviourControls @ "1" @ "\t";
function SBotBehaviourPursue(%this,%obj,%count)
{
   if((%this.updatetimer % 5) != (%count % 5)){return;}
   %n = findClientByName(%obj.bArg);
   if(%obj.bArg $= "" || %n $= "0")
   {
    %n = findClientByBL_ID(%obj.ownerID);
   }
   if(isObject(%n.player) && %n.player.getState() !$= "Dead")
   {
    %obj.setMoveDestination(%n.player.getPosition(),0);
    %p = %n.player.gethackposition();
    if(isObject(%obj.getMountedImage(0).projectile))
    {
     %d = vectorDist(getWords(%obj.getHackPosition(),0,1),getWords(%p,0,1));
     %amount = 1.5*(%d/%obj.getMountedImage(0).projectile.muzzlevelocity);
       %p = vectorAdd(%p,vectorScale(getWords(%n.player.getVelocity(),0,1),%amount));
     if(%obj.getMountedImage(0).projectile.gravitymod > 0){%p = vectorAdd(%p,"0 0 0.5");}
    }
    if(vectorDist(%obj.getHackPosition(),%n.player.getHackPosition()) < 30)
    {
     %obj.fireAt(%p);
    }
    else
    {
     %obj.setAimLocation(vectorAdd(%n.player.getPosition(),"0 0 2"));
    }
   }
   else
   {
    %p = %obj.getPosition();%vec = %obj.getForwardVector();
    %p = vectorAdd(%p,vectorScale(%vec,50));
        %obj.setAimLocation(vectorAdd(%p,"0 0 2"));
    %obj.setMoveDestination(%obj.getPosition(),0);
    if(isObject(%obj.getMountedImage(0)) && %obj.getMountedImage(0).botweapontype $= "bow")
    {
     %obj.connection.setTrigger(0,0);
    }
   }
}

$SpaceBots::Server::BehaviourList = $SpaceBots::Server::BehaviourList @ "Talk" @ "\t";
$SpaceBots::Server::BehaviourControls = $SpaceBots::Server::BehaviourControls @ "3" @ "\t";
function SBotBehaviourTalk(%this,%obj,%count)
{
 //Handled in Armor::onTrigger
}

$SpaceBots::Server::BehaviourList = $SpaceBots::Server::BehaviourList @ "AI" @ "\t";
$SpaceBots::Server::BehaviourControls = $SpaceBots::Server::BehaviourControls @ "0" @ "\t";
function SBotBehaviourAI(%this,%obj,%count)
{
 %hasweapon = 0;
 for(%i=0;%i<%obj.getDatablock().maxtools;%i++)
 {
  if(!isObject(%obj.tool[%i]))
  {
   %getItem = 1;
  }
  else if(%obj.tool[%i].image.botWeaponType !$= "unusable")
  {
   %hasWeapon = 1;
   %hasItem[%obj.tool[%i].getname()] = 1;
  }
 }
 if((%this.updatetimer % 20) == (%count % 20))
 {
  %range = 30;
  %obj.AIObjects = "";
  %masks = $TypeMasks::PlayerObjectType | $TypeMasks::ProjectileObjectType;
  if(%getItem){%masks = %masks | $TypeMasks::ItemObjectType;}
  initContainerRadiusSearch(%obj.position,%range,%masks);
  while(isObject(%searchObj = containerSearchNext()))
  {
   if(%searchObj.getClassName() $= "Projectile" && %searchObj.client !$= %obj && isObject(%searchObj.client.player) && !botIsAllied(%obj,%searchObj.client.player) && %searchObj.client.minigame $= %obj.minigame)
   {
    %obj.AIAlert = 1;
    %obj.AIAlertFirer = %searchObj.client.player;
    %obj.AIAlertPosition = %searchObj.client.player.getHackPosition();
   }
   else if(%searchObj.getClassName() !$= "Projectile")
   {
    %obj.AIObjects = %obj.AIObjects TAB %searchObj;
   }
  }
  %obj.AIObjects = getFields(%obj.AIObjects,1);
 }
 switch$(%obj.AIMode)
 {
  case "Scout":
   if(%obj.AIAlert){defaultAIAlert(%this,%obj,%hasWeapon);return;}
   SBotBehaviourWalk(%this,%obj,%count);
   if((%this.updatetimer % 20) != (%count % 20)){return;}
   %range = 30;
   for(%n=0;(%searchObj = getField(%obj.AIObjects,%n)) !$= "";%n++)
   {
    if(%searchObj.getClassName() $= "Item" && %getItem)
    {
     if(%hasItem[%searchObj.getDatablock().getName()]){continue;}
     if(!minigameCanUse(%obj,%searchObj)){continue;}
     if(strStr(%searchObj.getDatablock().getname(),"CTF") !$= "-1"){continue;}
     if(%searchObj.getDatablock().getName() $= "DGunItem" && !isObject(%searchObj.fakegun)){continue;}
      %masks = $TypeMasks::TerrainObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::FXBrickObjectType;
      %ray = containerRayCast(%obj.getEyePoint(),vectorAdd(%searchObj.getPosition(),"0 0 1"),%masks,%searchobj);
      if(!isObject(%ray))
      {
       %obj.AITarget = %searchObj;
       %obj.AIMode = "Pickup";
       return;
      }
    }
    else
    {
     if(%searchObj.getState() $= "Dead"){continue;}
     if(%searchObj $= %obj){continue;}
     if(%searchObj.client.minigame !$= %obj.client.minigame){continue;}
     if(botisAllied(%obj,%searchObj)){continue;}
     %a = %obj.getEyeVector();
     %b = vectorNormalize(vectorSub(%obj.getPosition(),vectorAdd(%searchObj.getPosition(),"0 0 1")));
     if(mACos(vectorDot(%a,%b)) > 2.35)
     {
      %masks = $TypeMasks::TerrainObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::FXBrickObjectType;
      %ray = containerRayCast(%obj.getEyePoint(),vectorAdd(%searchObj.getPosition(),"0 0 1"),%masks,%searchobj);
      if(!isObject(%ray))
      {
       %obj.AITarget = %searchObj;
       if(%hasWeapon)
        %obj.AIMode = "Attack";
       else
        %obj.AIMode = "Flee";
       return;
      }
     }
    }
   }
  case "Attack":
   if(!isObject(%obj.AITarget) || %obj.AITarget.getState() $= "Dead" || botIsAllied(%obj,%obj.AITarget))
   {
    %obj.AIMode = "Scout";
    %obj.AITarget = "";
    return;
   }
   if((%this.updatetimer % 20) == (%count % 20) && $SpaceBots::Server::PathFinding)
   {
    %masks = $TypeMasks::TerrainObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::FXBrickObjectType;
    %ray = containerRayCast(%obj.getHackPosition(),%obj.AITarget.getHackPosition(),%masks,%obj);
    if(isObject(%ray))
    {
     %obj.AIMode = "Scout";
     %obj.AITarget = "";
     return;
    }
   }
   %n = %obj.AITarget;
   %p = %n.gethackposition();
   %d = vectorDist(getWords(%obj.getHackPosition(),0,1),getWords(%p,0,1));
   if(isObject(%obj.getMountedImage(0).projectile))
   {
     %amount = 1.5*(%d/%obj.getMountedImage(0).projectile.muzzlevelocity);
     %p = vectorAdd(%p,vectorScale(getWords(%n.getVelocity(),0,1),%amount));
    if(%obj.getMountedImage(0).projectile.gravitymod > 0){%p = vectorAdd(%p,"0 0 0.5");}
   }
   if(vectorDist(%obj.getPosition(),%n.getPosition()) > 11 || %n.getMountedImage(0).botWeaponRange $= "" || %n.getMountedImage(0).botWeaponRange == 1)
   {
    %obj.setMoveDestination(%n.getPosition(),0);
   }
   else
   {
    %a = vectornormalize(vectorSub(getWords(%obj.getHackPosition(),0,1),getWords(%p,0,1)));
    %b = "1 0 0";
    %t = mATan(getWord(%a,0),getWord(%a,1));
    %a = mATan(getWord(%b,0),getWord(%b,1));
    if(%a-%t < -$pi){%a+=2*$pi;}
    if(%a-%t > $pi){%a-=2*$pi;}
    %ang = %a-%t;
    %s = -mSin(%ang);
    %c = mCos(%ang);
    %vec = vectorNormalize(%s SPC %c);
    %obj.setMoveDestination(vectorAdd(%obj.getPosition(),%vec));
   }
   %obj.fireAt(%p);
  case "Flee":
   if(%obj.AIAlert){%obj.AIAlert = 0;%obj.AITarget = %obj.AIAlertPosition;}
   if(getWordCount(%obj.AITarget) == 1) //It's an object
   {
    if(!isObject(%obj.AITarget) || %obj.AITarget.getState() $= "Dead" || botIsAllied(%obj,%obj.AITarget))
    {
     %obj.AIMode = "Scout";
     %obj.AITarget = "";
     return;
    }
    %run = %obj.AITarget.getPosition();
   }
   else
   {
    %run = %obj.AITarget;
   }
   %pos = vectorAdd(%obj.getEyePoint(),vectorScale(vectorNormalize(vectorSub(%obj.getPosition(),%run)),1000));
   %obj.setMoveDestination(%pos,0);
   %obj.setAimLocation(%pos);
   if(vectorDist(%obj.getposition(),%run) > 15)
   {
    %obj.AIMode = "Scout";
    %obj.AITarget = "";
    return;
   }
   for(%n=0;(%searchObj = getField(%obj.AIObjects,%n)) !$= "";%n++)
   {
    if(!isObject(%searchObj)){continue;}
    if(%searchObj.getClassName() !$= "Item"){continue;}
    if(%hasItem[%searchObj.getDatablock().getName()]){continue;}
    if(!minigameCanUse(%obj,%searchObj)){continue;}
    if(strStr(%searchObj.getDatablock().getname(),"CTF") !$= "-1"){continue;}
    if(%searchObj.getDatablock().getName() $= "DGunItem" && !isObject(%searchObj.fakegun)){continue;}
    %a = %obj.getEyeVector();
    %b = vectorNormalize(vectorSub(%obj.getPosition(),vectorAdd(%searchObj.getPosition(),"0 0 1")));
    if(mACos(vectorDot(%a,%b)) > 2.35)
    {
     %masks = $TypeMasks::TerrainObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::FXBrickObjectType;
     %ray = containerRayCast(%obj.getEyePoint(),vectorAdd(%searchObj.getPosition(),"0 0 1"),%masks,%searchobj);
     if(!isObject(%ray))
     {
      %obj.AITarget = %searchObj;
      %obj.AIMode = "Pickup";
      return;
     }
    }
   }
  case "Pickup":
   if(%obj.AIAlert){defaultAIAlert(%this,%obj,%hasWeapon);return;}
   %item = %obj.AITarget;
   if(!isObject(%item) || isEventPending(%item.fadeInSchedule) || %hasItem[%item.getName()])
   {
    %obj.AIMode = "Scout";
    %obj.AITarget = "";
    return;
   }
   %obj.setMoveDestination(%item.getPosition(),0);
   %obj.setAimLocation(vectorAdd(vectorAdd(%obj.getPosition(),"0 0 2"),vectorSub(getWords(%item.getPosition(),0,1),getWords(%obj.getPosition(),0,1))),0);
  case "AlertResponse":
   if(%obj.AIAlertTime < 1 && $SpaceBots::Server::PathFinding)
   {
    %masks = $TypeMasks::TerrainObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::FXBrickObjectType;
    %ray = containerRayCast(%obj.getHackPosition(),%obj.AIAlertPosition,%masks,%obj);
    if(isObject(%ray))
    {
     %obj.AIMode = "Scout";
     %obj.AITarget = "";
     return;
    }
   }
   %obj.AIAlertTimer++;
   if(%hasWeapon)
   {
    if(%obj.AIAlertTimer > 50)
    {
     %obj.AIMode = "Scout";
     %obj.AITarget = "";
     return;
    }
    %obj.fireAt(%obj.AIAlertPosition);
   }
  default:
   %obj.AIMode = "Scout";
 }
}

$SpaceBots::Server::BehaviourList = $SpaceBots::Server::BehaviourList @ "Fighter" @ "\t";
$SpaceBots::Server::BehaviourControls = $SpaceBots::Server::BehaviourControls @ "0" @ "\t";
function SBotBehaviourFighter(%this,%obj,%count)
{
 for(%i=0;%i<%obj.getDatablock().maxtools;%i++)
 {
  if(!isObject(%obj.tool[%i])){%getItem = 1;}else{%hasWeapon = 1;%hasItem[%obj.tool[%i].getname()] = 1;}
 }
 switch$(%obj.AIMode)
 {
  case "Scout":
   SBotBehaviourWalk(%this,%obj,%count);
   if((%this.updatetimer % 20) != (%count % 20)){return;}
   %range = 30;
   %masks = $TypeMasks::PlayerObjectType;
   initContainerRadiusSearch(%obj.position,%range,%masks);
   while(isObject(%searchObj = containerSearchNext()))
   {
     if(!%hasWeapon){continue;}
     if(%searchObj.getState() $= "Dead"){continue;}
     if(%searchObj $= %obj){continue;}
     if(%searchObj.client.minigame !$= %obj.client.minigame){continue;}
     if(botisAllied(%obj,%searchObj)){continue;}
     %a = %obj.getEyeVector();
     %b = vectorNormalize(vectorSub(%obj.getPosition(),vectorAdd(%searchObj.getPosition(),"0 0 1")));
     if(mACos(vectorDot(%a,%b)) > 2.35)
     {
      %masks = $TypeMasks::TerrainObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::FXBrickObjectType;
      %ray = containerRayCast(%obj.getEyePoint(),vectorAdd(%searchObj.getPosition(),"0 0 1"),%masks,%searchobj);
      if(!isObject(%ray))
      {
       %obj.AITarget = %searchObj;
       %obj.AIMode = "Attack";
       return;
      }
     }
   }
  case "Attack":
   if(!isObject(%obj.AITarget) || %obj.AITarget.getState() $= "Dead" || botIsAllied(%obj,%obj.AITarget))
   {
    %obj.AIMode = "Scout";
    %obj.AITarget = "";
    return;
   }
   %n = %obj.AITarget;
   %p = %n.gethackposition();
   if(isObject(%obj.getMountedImage(0).projectile))
   {
     %d = vectorDist(getWords(%obj.getHackPosition(),0,1),getWords(%p,0,1));
     %amount = 1.5*(%d/%obj.getMountedImage(0).projectile.muzzlevelocity);
     %p = vectorAdd(%p,vectorScale(getWords(%n.getVelocity(),0,1),%amount));
    if(%obj.getMountedImage(0).projectile.gravitymod > 0){%p = vectorAdd(%p,"0 0 0.5");}
   }
   %obj.setMoveDestination(%n.getPosition(),0);
   %obj.fireAt(%p);
  default:
   %obj.AIMode = "Scout";
 }
}

function defaultAIAlert(%this,%obj,%hasWeapon)
{
 %obj.AIAlert = 0;
 if(%hasWeapon)
 {
  %obj.AIMode = "AlertResponse";
  %obj.AIAlertTimer = 0;
  %obj.fireAt(%obj.AIAlertPosition);
 }
 else
 {
  %obj.AIMode = "Flee";
  %obj.AITarget = %obj.AIAlertPosition;
 }
}


//Test Look was used to make bots sense whether objects are 'left' or 'right' of them.
//$SpaceBots::Server::BehaviourList = $SpaceBots::Server::BehaviourList @ "Test Look" @ "\t";
//$SpaceBots::Server::BehaviourControls = $SpaceBots::Server::BehaviourControls @ "0" @ "\t";
//function SBotBehaviourTest_Look(%this,%obj,%count)
//{
  // if(%obj.normalLook $= ""){%obj.normalLook = %obj.getForwardVector();}
  // %range = 30;
  // initContainerRadiusSearch(%obj.position,%range,$TypeMasks::PlayerObjectType);
  // %obj.fired = 0;
  // %obj.targetfound = 0;
  // while(isObject(firstWord(%searchObj = containerSearchNext())))
  // {
  //  if(%searchObj.getState() $= "Dead"){continue;}
  //  if(%searchObj $= %obj){continue;}
  //  if(%searchObj.client.minigame !$= %obj.client.minigame){continue;}
  //  %a = %obj.getEyeVector();
  //  %b = vectorNormalize(vectorSub(%searchObj.getPosition(),%obj.getPosition()));
  //  if(mACos(vectorDot(%a,vectorScale(%b,-1))) > 2.35)
  //  {
  //   %t = mATan(getWord(%a,0),getWord(%a,1))*180/$pi;
  //   %n = mATan(getWord(%b,0),getWord(%b,1))*180/$pi;
  //   if(%n-%t < -180){%n+=360;}
  //   if(%n-%t > 180){%n-=360;}
  //   if(%n-%t<0){%str = "Left";}else{%str = "Right";}
  //   commandtoclient(clientgroup.getobject(0),'centerprint',%str,0.5,3);
  //   //%obj.fireAt(%p);
  //   %obj.fired = 1;
  //   %obj.targetfound = 1;
  //   //%obj.target = %searchObj;
  //   break;
  //  }
  // }
//}



//////////////////////////////
//Slash Commands//
//////////////////////////////

function ServerCmdcreatebot(%client, %name, %perm, %behaviour, %arg,%team)
{
 if(!isObject(%client.player) || %client.player.getState() $= "Dead")
  return;
 if(!isObject($BotCtrl)){$BotCtrl = new ScriptObject(){class = BotCtrlSO;count = 0;};}
 if((%error = createBotAllowed(%client,%behaviour)) !$= "1")
 {
  commandtoclient(%client,'centerprint',%error,2,3);
  return;
 }

 %name = strReplace(%name,"_"," ");
 %name = trim(%name);
 %name = getSubStr(%name,0,19);
 if(%name $= "")
 {
  commandtoclient(%client,'centerprint',"The bot must have a name.",2,3);
  return;
 }
 if(%behaviour $= ""){%behaviour = "Stand";} 

 %bot = spawnbot(%client.player.getTransform());
 %bot.name = %name;
 %bot.client = %bot;
 %bot.player = %bot;
 %bot.ownerClient = %client;
 %bot.ownerID = %client.bl_id;
 %bot.setShapeName(%name);
 %bot.behaviour = %behaviour;
 %bot.bArg = trim(%arg);
 %bot.isPermenant = %perm;
 %bot.isFirstCreated = 1;

 %vec = %client.player.getForwardVector();
 %point = %client.player.getEyePoint();
 %end = vectorAdd(%point,vectorScale(%vec,50));
 %bot.setAimLocation(%end);

 if(isObject($TeamDM::TeamManager) && %team !$= "")
 {
  for(%i = 0;%i<$teamDM::TeamManager.teamcount;%i++)
  {
   if($teamDM::TeamManager.team[%i].name $= %team){%goteam = $teamDM::TeamManager.team[%i];}
  }

  if(isObject(%goteam))
  {
   %bot.tdmteam = %goteam;
  }
  else
  {
   %msg = "\c5That team doesn't exist.";
   commandtoclient(%client,'centerprint',%msg,2,3);
  }
 }

 $BotCtrl.addValue(%bot);
 botBlockhead(%bot);

 for(%i=0;%i<clientgroup.getCount();%i++)
 {
  %c = clientgroup.getobject(%i);
  if(%c.hasSBotGUI)
  {
   commandtoclient(%c,'SBotInformation',%bot.getPosition(),%bot.name TAB %bot.behaviour TAB %bot.bArg TAB %bot.isPermenant TAB %team,%bot.appearance);
  }
 }
}

function ServerCmdcreateclone(%client, %player, %name, %perm, %behaviour, %arg, %team)
{
 if(!isObject(%client.player) || %client.player.getState() $= "Dead")
  return;
 if(!isObject($BotCtrl)){$BotCtrl = new ScriptObject(){class = BotCtrlSO;count = 0;};}
 if((%error = createBotAllowed(%client,%behaviour)) !$= "1")
 {
  commandtoclient(%client,'centerprint',%error,2,3);
  return;
 }

 %name = strReplace(%name,"_"," ");
 %name = trim(%name);
 %name = getSubStr(%name,0,19);
 if(%name $= "")
 {
  commandtoclient(%client,'centerprint',"The bot must have a name.",2,3);
  return;
 }
 if(%behaviour $= ""){%behaviour = "Stand";} 
 %bot = spawnbot(%client.player.getTransform());
 %bot.name = %name;
 %bot.client = %bot;
 %bot.player = %bot;
 %bot.ownerClient = %client;
 %bot.ownerID = %client.bl_id;
 %bot.setShapeName(%name);
 %bot.behaviour = %behaviour;
 %bot.bArg = trim(%arg);
 %bot.isPermenant = %perm;
 %bot.isFirstCreated = 1;

 %vec = %client.player.getForwardVector();
 %point = %client.player.getEyePoint();
 %end = vectorAdd(%point,vectorScale(%vec,50));
 %bot.setAimLocation(%end);

 if(isObject($TeamDM::TeamManager) && %team !$= "")
 {
  for(%i = 0;%i<$teamDM::TeamManager.teamcount;%i++)
  {
   if($teamDM::TeamManager.team[%i].name $= %team){%goteam = $teamDM::TeamManager.team[%i];}
  }

  if(isObject(%goteam))
  {
   %bot.tdmteam = %goteam;
  }
  else
  {
   %msg = "\c5That team doesn't exist.";
   commandtoclient(%client,'centerprint',%msg,2,3);
  }
 }

 $BotCtrl.addValue(%bot);
 %player = trim(%player);
 %n = findClientByName(%player);
 if(!isObject(%n) || %player $= "") //It detects clientgroup object 0 if you set it to ""
 {
  botClone(%bot,%client);
 }
 else
 {
  botClone(%bot,%n);
 }

 for(%i=0;%i<clientgroup.getCount();%i++)
 {
  %c = clientgroup.getobject(%i);
  if(%c.hasSBotGUI)
  {
   commandtoclient(%c,'SBotInformation',%bot.getPosition(),%bot.name TAB %bot.behaviour TAB %bot.bArg TAB %bot.isPermenant TAB %team,%bot.appearance);
  }
 }
}

function servercmdDeleteBot(%client,%name)
{
 %count = 0;
 if(%name !$= "")
 {
  for(%i=0;%i<$BotCtrl.count;%i++)
  {
   %bot = $BotCtrl.getValue(%i);
   if(%bot.ownerID $= %client.bl_id && %bot.name $= %name && !%bot.isBotSpawned)
   {
    %bot.removeBody();
    %count++;
   }
  }
  if(%count == 1){%msg = "\c31\c5 bot was deleted.";}else
  if(%count == 0){%msg = "\c5You don't have any bots with that name.";}else
  %msg = "\c3" @ %count @ "\c5 bots were deleted.";
  commandtoclient(%client,'centerprint',%msg,2,3);
 }
 else
 {
  for(%i=0;%i<$BotCtrl.count;%i++)
  {
   %bot = $BotCtrl.getValue(%i);
   if(%bot.ownerID $= %client.bl_id)
   {
    %found = %bot;
    %name = %bot.name;
   }
  }
  if(%found)
  {
   %found.removeBody();
   commandtoclient(%client,'centerprint',"\c3" @ %name @ "\c5 was deleted.",2,3);
  }
  else
  {
   commandtoclient(%client,'centerprint',"\c5You don't have any bots.",2,3);
  }
 }
}

function servercmdbotjointeam(%client,%name,%team,%nomsg)
{
 %count = 0;
 if(%team $= "")
 {
  %msg = "\c5Specify a team for the bot.";
  commandtoclient(%client,'centerprint',%msg,2,3);
  return;
 }
 if(!isObject($TeamDM::TeamManager)){return;}
 for(%i = 0;%i<$teamDM::TeamManager.teamcount;%i++)
 {
  if($teamDM::TeamManager.team[%i].name $= %team){%goteam = $teamDM::TeamManager.team[%i];}
 }
 if(%goteam $= "")
 {
  %msg = "\c5That team doesn't exist.";
  commandtoclient(%client,'centerprint',%msg,2,3);
  return;
 }
 if(isObject(%goteam.leader))
 {
  respawn(%goteam.leader);
  %goteam.leader.isLeader = "";
  %goteam.leader = "";
 }
 if($TeamDM::TeamsLocked)
 {
  %msg = "\c5Teams have been locked, joining and leaving is disabled.";
  commandtoclient(%client,'centerprint',%msg,2,3);
  return;
 }
 %name = strReplace(%name,"_"," ");
 %name = trim(%name);
 if(%name !$= "")
 {
  %count = 0;
  for(%i=0;%i<$BotCtrl.count;%i++)
  {
   %bot = $BotCtrl.getValue(%i);
   if(%bot.ownerID $= %client.bl_id && %bot.name $= %name && !%bot.isBotSpawned)
   {
    %correctname = %bot.name;
    %addlist[%count] = %bot;
    %count++;
   }
   for(%i=0;%i<%count;%i++)
   {
    %obj = %addlist[%i];
    respawnBot(%obj.minigame,%obj.ownerclient,%obj.ownerclient.bl_id,%obj.name,%obj.appearance,%goteam TAB 0,%obj.behaviour TAB %obj.barg,"PICK",%obj.isPermenant);
    %obj.delete();
   }
  }
  if(%count == 1){%msg = "\c3" @ %correctname @ "\c5 was moved.";}else
  if(%count == 0){%msg = "\c5You don't have any bots with that name.";}else
  %msg = "\c3" @ %count @ "\c5 bots were moved.";
  if(!%nomsg){commandtoclient(%client,'centerprint',%msg,2,3);}
 }
 else
 {
  %msg = "\c5Specify a name for the bot.";
  commandtoclient(%client,'centerprint',%msg,2,3);
  return;
 }
}

function servercmdbotpathpoint(%client,%path)
{
 %path = trim(%path);
 if(%path $= "" || !isObject(%client.player) || %client.player.getState() $= "Dead")
  return;
 for(%i=0;$SpaceBots::Path[%client.bl_id,%i] !$= "";%i++)
 {
  if(getField($SpaceBots::Path[%client.bl_id,%i],0) $= %path)
  {
   %foundpath = %i;
  }
 }
 if(%foundpath $= ""){%foundpath = %i;}

 commandtoclient(%client,'centerprint',"\c5Point added to path \c3" @ %path @ "\c5.",2,3);
 showMarker(vectorAdd(%client.player.getPosition(),"0 0 1"));
 if($SpaceBots::Path[%client.bl_id,%foundpath] $= "")
 {
  $SpaceBots::Path[%client.bl_id,%foundpath] = %path TAB %client.player.getPosition();
 }
 else
 {
  $SpaceBots::Path[%client.bl_id,%foundpath] = $SpaceBots::Path[%client.bl_id,%foundpath] TAB %client.player.getPosition();
 }
}

function servercmdbotclearpath(%client,%path)
{
 %path = trim(%path);
 for(%i=0;$SpaceBots::Path[%client.bl_id,%i] !$= "";%i++)
 {
  if(getField($SpaceBots::Path[%client.bl_id,%i],0) $= %path)
  {
   %foundpath = %i;
  }
 }
 if(%foundpath $= "")
 {
  commandtoclient(%client,'centerprint',"\c5The path \c3" @ %path @ "\c5 was not found!",2,3);
  return;
 }
 commandtoclient(%client,'centerprint',"\c5The path \c3" @ %path @ "\c5 was deleted.",2,3);
 $SpaceBots::Path[%client.bl_id,%foundpath] = "";
 %exit = 0;
 for(%i = %foundpath+1;!%exit;%i++)
 {
  $SpaceBots::Path[%client.bl_id,%i-1] = $SpaceBots::Path[%client.bl_id,%i];
  if($SpaceBots::Path[%client.bl_id,%i] $= ""){%exit = 1;}
 }
}

function servercmdbotclearallpaths(%client)
{
 %foundpath = 0;
 for(%i=0;$SpaceBots::Path[%client.bl_id,%i] !$= "";%i++)
 {
   $SpaceBots::Path[%client.bl_id,%i] = "";
   %foundpath = 1;
 }
 if(!%foundpath)
 {
  commandtoclient(%client,'centerprint',"\c5You don't have any paths!",2,3);
  return;
 }
 commandtoclient(%client,'centerprint',"\c5All paths deleted.",2,3);
}

function servercmdbotshowpath(%client,%path)
{
 if(iseventpending(%client.botshow)){cancel(%client.botshow);botshowend(%client);return;}
 %path = trim(%path);
 if(isObject($pathmarker[%client])){$pathmarker[%client].delete();}
 $pathmarker[%client] = new Marker();
 for(%i=0;$SpaceBots::Path[%client.bl_id,%i] !$= "";%i++)
 {
  if(getField($SpaceBots::Path[%client.bl_id,%i],0) $= %path)
  {
   %foundpath = %i;
  }
 }
 if(%foundpath $= "")
 {
  commandtoclient(%client,'centerprint',"\c5The path \c3" @ %path @ "\c5 was not found!",2,3);
  return;
 }
 %client.botshow = schedule(100,0,botshowpath,%client,$SpaceBots::Path[%client.bl_id,%foundpath],1);
}

function botshowpath(%client,%pathstr,%pos)
{
 if(!isObject(%client))
 {
  if(isObject($pathmarker[%client])){$pathmarker[%client].delete();}
  return;
 }
 //%client.setControlObject(%client.camera);
 //$pathmarker[%client].settransform(getField(%pathstr,%pos) SPC "1 0 0 0");
 //%client.camera.setOrbitMode(%client.player,getField(%pathstr,%pos) SPC "0 0 0 45",3,3,3,1); //setDollyMode(vectorAdd(getField(%pathstr,%pos),"0 0 1"),vectorAdd(getField(%pathstr,%pos),"0 0 2")); 
 showMarker(vectorAdd(getField(%pathstr,%pos),"0 0 1"));
 if(getField(%pathstr,%pos+1) !$= "")
 {
  %client.botshow = schedule(1000,0,botshowpath,%client,%pathstr,%pos+1);
 }
 else
 {
  %client.botshow = schedule(1500,0,botshowend,%client,%pathstr);
 }
}

function botshowend(%client,%path)
{
 if(isObject($pathmarker[%client])){$pathmarker[%client].delete();}
 if(isObject(%client.player)){%client.setControlObject(%client.player);}
 if(%path !$= "")
 {
  for(%i=1;%i<getFieldCount(%path);%i++){showMarker(vectorAdd(getField(%path,%i),"0 0 1"));}
 }
}

function servercmdclearbots(%client)
{
 %count = 0;
 for(%i=0;%i<$BotCtrl.count;%i++)
 {
  %bot = $BotCtrl.getValue(%i);
  if(%bot.ownerID $= %client.bl_id)
  {
   %bot.removeBody();
   %count++;
  }
 }
 for(%i=0;%i<$BotSpawnCtrl.count;%i++)
 {
  if(isObject($BotSpawnCtrl.value[%i]) && $BotSpawnCtrl.value[%i].getGroup() $= %client.brickgroup)
  {
   $BotSpawnCtrl.value[%i].killbrick();
  }
 }
 if(%count > 0)
 {
  messageall('MsgClearBricks',"\c3" @ %client.name @ "\c2 cleared \c3" @ %client.name @ "'s\c2 bots.");
 }
 else
 {
  commandtoclient(%client,'centerprint',"\c5You don't have any bots.",2,3);
 }
}

function servercmdclearplayerbots(%client,%id)
{
 if(%id $= ""){return;}
 if(%id == 0 && %id !$= "LAN")
 {
  %id = findClientByName(strReplace(%id,"_"," ")).bl_id;
 }
 if(!%client.isAdmin && !%client.isSuperAdmin){return;}
 %count = 0;
 for(%i=0;%i<$BotCtrl.count;%i++)
 {
  %bot = $BotCtrl.getValue(%i);
  if(%bot.ownerID $= %id)
  {
   %bot.removeBody();
   %count++;
  }
 }
 for(%i=0;%i<$BotSpawnCtrl.count;%i++)
 {
  if(isObject($BotSpawnCtrl.value[%i]) && $BotSpawnCtrl.value[%i].getGroup().bl_id $= %id)
  {
   $BotSpawnCtrl.value[%i].killbrick();
  }
 }
 if(%count > 0)
 {
  messageall('MsgClearBricks',"\c3" @ %client.name @ "\c2 cleared \c3" @ ("BrickGroup_" @ %id).name @ "'s\c2 bots.");
 }
 else
 {
  commandtoclient(%client,'centerprint',"\c5You don't have any bots.",2,3);
 }
}

function servercmdclearallbots(%client)
{
 if(!%client.isSuperAdmin){return;}
 %count = 0;
 for(%i=0;%i<$BotCtrl.count;%i++)
 {
  %bot = $BotCtrl.getValue(%i);
  %bot.removeBody();
  %count++;
 }
 for(%i=0;%i<$BotSpawnCtrl.count;%i++)
 {
  if(isObject($BotSpawnCtrl.value[%i]))
  {
   $BotSpawnCtrl.value[%i].killbrick();
  }
 }
 if(%count > 0)
 {
  messageall('MsgClearBricks',"\c3" @ %client.name @ "\c0 cleared all bots.");
 }
 else
 {
  commandtoclient(%client,'centerprint',"\c5No bots exist.",2,3);
 }
}

/////////////////////////////////////////////
//GUI-Only Server Commands//
/////////////////////////////////////////////

function servercmdSBotsInit(%client)
{
 %client.hasSBotGUI = 1;
 commandtoclient(%client,'SBotsConfirmInit',$SpaceBots::Server::BehaviourList,$SpaceBots::Server::BehaviourControls);
}

function servercmdSBotsGUIUpdates(%client)
{
 if(!%client.hasSBotGUI) return;
 for(%i=0;%i<ClientGroup.getCount();%i++)
 {
  commandtoclient(%client,'SBotsGUIPlayer',ClientGroup.getObject(%i).name);
 }
 if(isObject($BotCtrl))
 {
  for(%i=0;%i<$BotCtrl.count;%i++)
  {
   %b = $BotCtrl.getValue(%i);
   if((%client.isAdmin || %client.isSuperAdmin || %b.ownerID $= %client.bl_id) && !%b.isBotSpawned)
    commandtoclient(%client,'SBotsGUIBot',%b.name,("BrickGroup_" @ %b.ownerID).name TAB %b.ownerID,%i);
  }
 }
 if(isObject($TeamDM::TeamManager))
 {
  for(%i=0;%i<$TeamDM::TeamManager.teamcount;%i++)
  {
   commandtoclient(%client,'SBotsGUITeam',$TeamDM::TeamManager.team[%i].name);
  }
 }
 for(%i=0;$SpaceBots::Path[%client.bl_id,%i] !$= "";%i++)
 {
     commandtoclient(%client,'SBotsGUIPath',getField($SpaceBots::Path[%client.bl_id,%i],0));
 }
 commandtoclient(%client,'SBotsIsAdmin',%client.isAdmin,%client.isSuperAdmin,findLocalClient() $= %client); //Clientside $IamAdmin only says whether you are Admin or not, not Super or Host
 commandtoclient(%client,'SBotsGUIManageLimits',$Pref::SpaceBots::MaxBotsPlayer TAB 
						$Pref::SpaceBots::MaxBotsAdmin TAB 
						$Pref::SpaceBots::MaxBotsSAdmin TAB 
						$Pref::SpaceBots::MaxAIPlayer TAB
						$Pref::SpaceBots::MaxAIAdmin TAB 
						$Pref::SpaceBots::MaxAISAdmin);
}

function servercmdGUIDeleteBot(%client,%id,%name)
{
 if(!%client.hasSBotGUI) return;
   %b = $botctrl.value[%id];
   //If the list order hasn't changed since you opened the menu (e.g. a bot died), delete that ID if you own it
   if(isObject(%b) && !%b.isBotSpawned && %b.name $= %name && (%client.isAdmin || %client.isSuperAdmin || %b.ownerID $= %client.bl_id))
   {
    %b.removeBody();
   }
   else //Search for the first bot of the same name and ownership and delete it
   {
    for(%i=0;%i<$botctrl.count;%i++)
    {
     %b = $botctrl.value[%i];
     if(isObject(%b) && %b.name $= %name && !%b.isBotSpawned && (%client.isAdmin || %client.isSuperAdmin || %b.ownerID $= %client.bl_id))
     {
      %b.removeBody();
      return;
     }
    }
   }
   servercmdSBotsGUIUpdates(%client);
}

function servercmdGUIBotPrefs(%client,%preflist)
{
 if(findLocalClient() !$= %client){return;}
 $Pref::SpaceBots::MaxBotsPlayer = getField(%preflist,0);
 $Pref::SpaceBots::MaxBotsAdmin = getField(%preflist,1);
 $Pref::SpaceBots::MaxBotsSAdmin = getField(%preflist,2);
 $Pref::SpaceBots::MaxAIPlayer = getField(%preflist,3);
 $Pref::SpaceBots::MaxAIAdmin = getField(%preflist,4);
 $Pref::SpaceBots::MaxAISAdmin = getField(%preflist,5);
 messageall('msgAdminForce',"\c3" @ %client.name @ "\c0 has changed the server limits for Bots and AI Bots.");
 for(%i=0;%i<$BotCtrl.count;%i++)
 {
  %b = $BotCtrl.getValue(%i);
  %cl = findClientByBL_ID(%b.ownerID);
  %limit = $Pref::SpaceBots::MaxBotsPlayer;
  if(%cl.isAdmin){%limit = $Pref::SpaceBots::MaxBotsAdmin;}
  if(%cl.isSuperAdmin){%limit = $Pref::SpaceBots::MaxBotsSAdmin;}
  if(%count[%b.ownerID] >= %limit && %limit !$= "-1"){%b.removeBody();}
  %count[%b.ownerID]++;
 }
}

function servercmdsetBotSpawn(%client,%copy,%name,%behaviour,%arg,%team,%ms,%max)
{
 if(!%client.hasSBotGUI) return;
 if(!isObject(%client.botbrick) || !isObject(%client.player)) return;
 %name = strReplace(%name,"_"," ");
 %name = trim(%name);
 %name = getSubStr(%name,0,19);
 if(%name $= "")
 {
  commandtoclient(%client,'centerprint',"The bot must have a name.",2,3);
  return;
 }
 if(%ms > 100000)
 {
  commandtoclient(%client,'centerprint',"Bots must spawn at least once every 100 seconds.",2,3);
  %ms = 100000;
 }
 if(%max > 15)
 {
  commandtoclient(%client,'centerprint',"You can only have 15 bots per spawn.",2,3);
  %max = 15;
 }
 if(%max < 0)
 {
  %max = 0;
 }
 if(%ms < 100)
 {
  commandtoclient(%client,'centerprint',"You can only spawn bots every 100ms.",2,3);
  %ms = 100;
 }
 if(%copy $= "")
 {
  %a = 	"0" TAB "1 1 1 1" TAB
			"0" TAB "1 0 0 1" TAB
			"0" TAB "1 0 0 1" TAB
			"0" TAB "1 0.878431 0.611765 1" TAB
			"0" TAB "1 0.878431 0.611765 1" TAB
			"0" TAB "0 0 1 1" TAB
			"0" TAB "0 0 1 1" TAB
			"0" TAB "0 0 1 1" TAB
			"0" TAB "0 0 0 0" TAB
			"0" TAB "0 0 0 0" TAB
			"0" TAB "0 0 0 0" TAB
			"0" TAB "0 0 0 0" TAB
			"1 0.878431 0.611765 1" TAB
			"AAA-None" TAB "smileyCreepy";
 }
 else
 {
  %p = findClientByName(%copy);
  if(isObject(%p))
  {
   %a = 	%p.chest TAB %p.chestcolor TAB
			%p.rArm TAB %p.rArmColor TAB
			%p.lArm TAB %p.lArmColor TAB
			%p.rHand TAB %p.rHandColor TAB
			%p.lHand TAB %p.lHandColor TAB
			%p.hip TAB %p.hipcolor TAB
			%p.rLeg TAB %p.rLegColor TAB
			%p.lLeg TAB %p.lLegColor TAB
			%p.hat TAB %p.hatColor TAB
			%p.accent TAB %p.accentColor TAB
			%p.pack TAB %p.packColor TAB
			%p.secondpack TAB %p.secondpackColor TAB
			%p.headColor TAB
			%p.decalName TAB %p.faceName;
  }
  else
  {
   %a = 	"0" TAB "1 1 1 1" TAB
			"0" TAB "1 0 0 1" TAB
			"0" TAB "1 0 0 1" TAB
			"0" TAB "1 0.878431 0.611765 1" TAB
			"0" TAB "1 0.878431 0.611765 1" TAB
			"0" TAB "0 0 1 1" TAB
			"0" TAB "0 0 1 1" TAB
			"0" TAB "0 0 1 1" TAB
			"0" TAB "0 0 0 0" TAB
			"0" TAB "0 0 0 0" TAB
			"0" TAB "0 0 0 0" TAB
			"0" TAB "0 0 0 0" TAB
			"1 0.878431 0.611765 1" TAB
			"AAA-None" TAB "smileyCreepy";
  }
 }
 %client.botbrick.botClone = %p.name;
 %client.botbrick.botName = %name;
 %client.botbrick.botAI = %behaviour TAB %arg;
 %client.botbrick.appearance = %a;
 %client.botbrick.botTeam = "";
 if(isObject($teamDM::teamManager))
 {
  for(%i=0;%i<$teamdm::teammanager.teamcount;%i++)
  {
   if($teamdm::teammanager.team[%i].name $= %team){%client.botbrick.botTeam = $teamdm::teammanager.team[%i];break;}
  }
 }
 %client.botbrick.botMS = %ms;
 %client.botbrick.botMax = %max;
 for(%i=0;%i<$BotCtrl.count;%i++)
 {
  %b = $botctrl.getValue(%i);
  if(%b.isBotSpawned $= %client.botbrick){%b.removebody();}
 }
 %client.botbrick.ticknum = %client.botbrick.botMS;
 if(!isObject($BotCtrl)){$BotCtrl = new ScriptObject(){class = BotCtrlSO;count = 0;};}
 if(!%client.botbrick.listAdded)
 {
  if(!isObject($botSpawnCtrl)){$botSpawnCtrl = new ScriptObject(){class = BotSpawnCtrlSO;count = 0;};}
  %client.botbrick.listAdded = 1;
  $botSpawnCtrl.addValue(%client.botbrick);
 }
 %brick = %client.botBrick;
 for(%i=0;%i<clientgroup.getcount();%i++)
 {
  %c=clientgroup.getobject(%i);
  if(%c.hasSBotGUI)
  {
   commandtoclient(%c,'botSpawnInformation',%brick.getPosition(),0,%brick.botMS TAB %brick.botMax TAB %brick.botName TAB %brick.botAI TAB %brick.botTeam.name);
   commandtoclient(%c,'botSpawnInformation',%brick.getPosition(),1,%brick.appearance);
  }
 }
}

function servercmdLoadBotSave(%client)
{
 if(!%client.hasSBotGUI) return;
 if(!%client.isAdmin && !%client.isSuperAdmin) return;
 if(isObject($SpaceBots::Server::Loading))
 {
  commandtoclient(%client,'centerprint',"\c3" @ $SpaceBots::Server::Loading.name @ "\c5 is currently loading a save file.",2,3);
  return;
 }
 if(!isObject($BotCtrl)){$BotCtrl = new ScriptObject(){class = BotCtrlSO;count = 0;};}
 messageall('MsgUploadStart',"\c3" @ %client.name @ "\c0 is loading a \c3Bots\c0 save.");
 $SpaceBots::Server::Loading = %client;
 $SpaceBots::Server::LoadMode = 0;
 if(isObject($SpaceBots::Server::LoadObject)){$SpaceBots::Server::LoadObject.delete();}
 $SpaceBots::Server::LoadObject = new ScriptObject();$SpaceBots::Server::LoadObject.linecount = 0;
 commandtoclient(%client,'botLoadNext');
}

function servercmdbotLoadLine(%client,%line)
{
 if(%client !$= $SpaceBots::Server::Loading) return;
 $SpaceBots::Server::LoadObject.line[$SpaceBots::Server::LoadObject.linecount] = %line;
 $SpaceBots::Server::LoadObject.linecount++;
 commandtoclient(%client,'botLoadNext');
}

function servercmdbotLoadFin(%client)
{
 if(%client !$= $SpaceBots::Server::Loading) return;
 messageall('MsgUploadEnd',"\c0Upload complete, loading bots.");
 $SpaceBots::Server::LoadMode = 1;
 $SpaceBots::Server::LoadCount = 0;
}

///////////////////////////////
//Support Functions//
///////////////////////////////

function showmarker(%pos)
{
  %r = "0 0 1 0";
  %p = new ParticleEmitterNode()
  {
   datablock = genericEmitterNode;
   emitter = "botMarkerEmitter";
   scale = "0 0 0";
   rotation = %r;
   velocity = "1";
   position = %pos;
  };%p.brick = %p;%p.schedule(200,delete);
}

function spawnbot(%trans)
{
 if(!isObject($BotCtrl)){$BotCtrl = new ScriptObject(){class = BotCtrlSO;count = 0;};}
 %b = new AIPlayer()
 {
  position = posFromTransform(%trans);
  rotation = rotFromTransform(%trans);
  datablock = PlayerBotNoJet;
 };MissionCleanup.add(%b);%b.giveDefaultEquipment();%b.setTransform(%trans);
 %b.connection = new AIConnection();%b.connection.player = %b;%b.connection.setcontrolobject(%b);
 return %b;
}

function respawnBot(%minigame,%client,%id,%name,%appearance,%team,%behaviour,%pos,%perm)
{
 if(%pos $= "PICK")
 {
  if(isObject(%minigame) && %minigame.usespawnbricks)
  {
   %trans = %minigame.pickSpawnPoint();
  }
  else
  {
   %trans = pickSpawnPoint();
  }
 }
 else
 {
  %trans = %pos;
 }
 %b = spawnbot(%trans);
 if(isObject(%minigame.playerdatablock))
 {
  %b.setDatablock(%minigame.playerdatablock);
 }
 if(isObject(%minigame))
 {
  %b.shapename = %b.schedule(100,setShapeNameColor,$MinigameColorF[%minigame.colorIdx] SPC "1");
  %b.minigame = %minigame;
  for(%i=0;%i<5;%i++)
  {
   %b.tool[%i] = (isObject(%b.minigame.startequip[%i]) ? %b.minigame.startequip[%i] : 0);
  }
 }
 %b.emote(spawnProjectile,1);
 %b.name = %name;
 %b.client = %b;
 %b.player = %b;
 %b.ownerClient = %client;
 %b.ownerID = %id;
 %b.setShapeName(%name);
 %b.behaviour = getField(%behaviour,0);
 %b.bArg = getField(%behaviour,1);
 %b.isPermenant = %perm;
 %l = getField(%team,1);
 if(isObject(%l)){%b.isLeader = %l;%l.leader = %b;}
 %team = getField(%team,0);
 %b.appearance = %appearance;
 if(isObject(%team))
 {
  %b.tdmteam = %team;
  if(isObject(%minigame) && $teamdm::teamson)
  {
   cancel(%b.shapename);
   schedule(100,0,botTeamSetup,%b,%team,(%pos !$= "PICK"));
  }
  else
  {
   botAppearance(%b);
  }
 }
 else
 {
  %b.tdmteam = "";
  botAppearance(%b);
 }
 %bot = %b;
 $BotCtrl.addValue(%b);
 for(%i=0;%i<clientgroup.getCount();%i++)
 {
  %c = clientgroup.getobject(%i);
  if(%c.hasSBotGUI)
  {
   commandtoclient(%c,'SBotInformation',%bot.getPosition(),%bot.name TAB %bot.behaviour TAB %bot.bArg TAB %bot.isPermenant TAB %team.name,%bot.appearance);
  }
 }

 return %b;
}

function BotTeamSetup(%b,%team,%notransform)
{
   if(!isObject(%b)){return;}
   if(%b.tdmTeam.spawnlist !$= "" && !%notransform)
   {
    %s = (getWord(%b.tdmTeam.spawnlist,getRandom(0,getWordCount(%b.tdmTeam.spawnlist)-1)));
    %b.setTransform(vectorSub(posFromTransform(%s.getTransform()),"0 0 1.2") SPC rotFromTransform(%s.getTransform()));
   }
   for(%i=0;%i<5;%i++)
   {
    %b.tool[%i] = (isObject(%b.tdmTeam.tool[%i]) ? %b.tdmTeam.tool[%i] : 0);
   }
   %b.setShapeNameColor($teamDM::color[%b.tdmteam.color,0] SPC "1");
   if(isObject(%b.tdmTeam.data))
    %b.setDatablock(%b.tdmTeam.data);
   else
    %b.setDatablock(PlayerStandardArmor);
   uniform(%b,%team);
}


function createBotAllowed(%client,%b)
{
 for(%i=0;%i<$BotCtrl.count;%i++)
 {
  if($BotCtrl.getValue(%i).ownerID $= %client.bl_id)
  {
   %count++;
   if($BotCtrl.getValue(%i).behaviour $= "AI"){%aiCount++;}
  }
 }
 %max = $Pref::SpaceBots::MaxBotsPlayer;%rank = "Non-Admins";
 if(%client.isAdmin){%max = $Pref::SpaceBots::MaxBotsAdmin;%rank = "Admins";}
 if(%client.isSuperAdmin){%max = $Pref::SpaceBots::MaxBotsSAdmin;%rank = "Super Admins";}
 if(%count >= %max && %max !$= "-1"){return "\c3" @ %rank @ "\c5 can only place \c3" @ %max @ "\c5 bots.";}
 %AImax = $Pref::SpaceBots::MaxAIPlayer;
 if(%client.isAdmin){%AImax = $Pref::SpaceBots::MaxAIAdmin;}
 if(%client.isSuperAdmin){%AImax = $Pref::SpaceBots::MaxAISAdmin;}
 if(%aicount >= %aimax && %b $= "AI"){return "\c3" @ %rank @ "\c5 can only place \c3" @ %aimax @ "\c5 AI bots.";}
 return 1;
}

function botBlockhead(%bot)
{
 if(!isObject(%bot)){return;}
 for (%i = 0; $accent[%i] !$= ""; %i++) %bot.hideNode($accent[%i]);
 for (%i = 0; $chest[%i] !$= ""; %i++) %bot.hideNode($chest[%i]);
 for (%i = 0; $hat[%i] !$= ""; %i++) %bot.hideNode($hat[%i]);
 for (%i = 0; $hip[%i] !$= ""; %i++) %bot.hideNode($hip[%i]);
 for (%i = 0; $LArm[%i] !$= ""; %i++) %bot.hideNode($LArm[%i]);
 for (%i = 0; $LHand[%i] !$= ""; %i++) %bot.hideNode($LHand[%i]);
 for (%i = 0; $LLeg[%i] !$= ""; %i++) %bot.hideNode($LLeg[%i]);
 for (%i = 0; $pack[%i] !$= ""; %i++) %bot.hideNode($pack[%i]);
 for (%i = 0; $RArm[%i] !$= ""; %i++) %bot.hideNode($RArm[%i]);
 for (%i = 0; $RHand[%i] !$= ""; %i++) %bot.hideNode($RHand[%i]);
 for (%i = 0; $RLeg[%i] !$= ""; %i++) %bot.hideNode($RLeg[%i]);
 for (%i = 0; $secondPack[%i] !$= ""; %i++) %bot.hideNode($secondPack[%i]);

 %bot.hideNode("headSkin");
 %bot.hideNode("LSki");
 %bot.hideNode("RSki");
 %bot.hideNode("skirtTrimLeft");
 %bot.hideNode("skirtTrimRight");

 %bot.unhidenode("chest");
 %bot.setNodeColor("chest","1 1 1 1");
 %bot.unhidenode("rarm");
 %bot.setNodeColor("rarm", "1 0 0 1");
 %bot.unhidenode("larm");
 %bot.setNodeColor("larm", "1 0 0 1");
 %bot.unhidenode("rhand");
 %bot.setNodeColor("rhand", "1 0.878431 0.611765 1");
 %bot.unhidenode("lhand");
 %bot.setNodeColor("lhand", "1 0.878431 0.611765 1");
 %bot.unhidenode("pants");
 %bot.setNodeColor("pants", "0 0 1 1");
 %bot.unhidenode("rshoe");
 %bot.setNodeColor("rshoe", "0 0 1 1");
 %bot.unhidenode("lshoe");
 %bot.setNodeColor("lshoe", "0 0 1 1");
 %bot.setDecalName("AAA-None");
 %bot.unhidenode("headskin");
 %bot.setNodeColor("headskin", "1 0.878431 0.611765 1");
 %bot.setFaceName("smileyCreepy");
 //chest chestCol rArm rArmCol lArm lArmCol rHand rHandCol lHand lHandCol pants pantsCol rLeg rLegCol lLeg lLegCol hat hatCol accent accentCol pack packCol pack2 pack2Col headCol decalName faceName
 %bot.appearance = 	"0" TAB "1 1 1 1" TAB
			"0" TAB "1 0 0 1" TAB
			"0" TAB "1 0 0 1" TAB
			"0" TAB "1 0.878431 0.611765 1" TAB
			"0" TAB "1 0.878431 0.611765 1" TAB
			"0" TAB "0 0 1 1" TAB
			"0" TAB "0 0 1 1" TAB
			"0" TAB "0 0 1 1" TAB
			"0" TAB "0 0 0 0" TAB
			"0" TAB "0 0 0 0" TAB
			"0" TAB "0 0 0 0" TAB
			"0" TAB "0 0 0 0" TAB
			"1 0.878431 0.611765 1" TAB
			"AAA-None" TAB "smileyCreepy";
}

function botClone(%bot,%n)
{
 if(!isObject(%bot)){return;}
 for (%i = 0; $accent[%i] !$= ""; %i++) %bot.hideNode($accent[%i]);
 for (%i = 0; $chest[%i] !$= ""; %i++) %bot.hideNode($chest[%i]);
 for (%i = 0; $hat[%i] !$= ""; %i++) %bot.hideNode($hat[%i]);
 for (%i = 0; $hip[%i] !$= ""; %i++) %bot.hideNode($hip[%i]);
 for (%i = 0; $LArm[%i] !$= ""; %i++) %bot.hideNode($LArm[%i]);
 for (%i = 0; $LHand[%i] !$= ""; %i++) %bot.hideNode($LHand[%i]);
 for (%i = 0; $LLeg[%i] !$= ""; %i++) %bot.hideNode($LLeg[%i]);
 for (%i = 0; $pack[%i] !$= ""; %i++) %bot.hideNode($pack[%i]);
 for (%i = 0; $RArm[%i] !$= ""; %i++) %bot.hideNode($RArm[%i]);
 for (%i = 0; $RHand[%i] !$= ""; %i++) %bot.hideNode($RHand[%i]);
 for (%i = 0; $RLeg[%i] !$= ""; %i++) %bot.hideNode($RLeg[%i]);
 for (%i = 0; $secondPack[%i] !$= ""; %i++) %bot.hideNode($secondPack[%i]);

 //%bot.hideNode("headSkin");
 %bot.hideNode("LSki");
 %bot.hideNode("RSki");
 %bot.hideNode("skirtTrimLeft");
 %bot.hideNode("skirtTrimRight");
 
 %pl = %n.player;
 %n.player = %bot;
 %n.applyBodyParts();
 %n.applyBodyColors();
 %n.player = %pl;

 %client = %n;
 //chest chestCol rArm rArmCol lArm lArmCol rHand rHandCol lHand lHandCol pants pantsCol rLeg rLegCol lLeg lLegCol hat hatCol accent accentCol pack packCol pack2 pack2Col headCol decalName faceName
  %bot.appearance = 	%client.chest TAB %client.chestcolor TAB
			%client.rArm TAB %client.rArmColor TAB
			%client.lArm TAB %client.lArmColor TAB
			%client.rHand TAB %client.rHandColor TAB
			%client.lHand TAB %client.lHandColor TAB
			%client.hip TAB %client.hipcolor TAB
			%client.rLeg TAB %client.rLegColor TAB
			%client.lLeg TAB %client.lLegColor TAB
			%client.hat TAB %client.hatColor TAB
			%client.accent TAB %client.accentColor TAB
			%client.pack TAB %client.packColor TAB
			%client.secondpack TAB %client.secondpackColor TAB
			%client.headColor TAB
			%client.decalName TAB %client.faceName;
}

function botAppearance(%bot)
{
 if(!isObject(%bot)){return;}
 //if(isObject(%bot.minigame))
 //{
 // %bot.setShapeNameColor($MinigameColorF[%obj.minigame.colorIdx] SPC "1");
 //}
 //else
 //%bot.setShapeNameColor("1 1 1 1");
 if(isObject(%bot.tdmteam)){uniform(%bot,%bot.tdmteam);return;}
 if(filename(%bot.getdatablock().shapefile) !$= "m.dts"){%bot.setnodecolor("ALL","0 0 0 1");%bot.setnodecolor("body",getField(%bot.appearance,1));return;}
 for (%i = 0; $accent[%i] !$= ""; %i++) %bot.hideNode($accent[%i]);
 for (%i = 0; $chest[%i] !$= ""; %i++) %bot.hideNode($chest[%i]);
 for (%i = 0; $hat[%i] !$= ""; %i++) %bot.hideNode($hat[%i]);
 for (%i = 0; $hip[%i] !$= ""; %i++) %bot.hideNode($hip[%i]);
 for (%i = 0; $LArm[%i] !$= ""; %i++) %bot.hideNode($LArm[%i]);
 for (%i = 0; $LHand[%i] !$= ""; %i++) %bot.hideNode($LHand[%i]);
 for (%i = 0; $LLeg[%i] !$= ""; %i++) %bot.hideNode($LLeg[%i]);
 for (%i = 0; $pack[%i] !$= ""; %i++) %bot.hideNode($pack[%i]);
 for (%i = 0; $RArm[%i] !$= ""; %i++) %bot.hideNode($RArm[%i]);
 for (%i = 0; $RHand[%i] !$= ""; %i++) %bot.hideNode($RHand[%i]);
 for (%i = 0; $RLeg[%i] !$= ""; %i++) %bot.hideNode($RLeg[%i]);
 for (%i = 0; $secondPack[%i] !$= ""; %i++) %bot.hideNode($secondPack[%i]);

 %bot.hideNode("LSki");
 %bot.hideNode("RSki");
 %bot.hideNode("skirtTrimLeft");
 %bot.hideNode("skirtTrimRight");

  //chest chestCol rArm rArmCol lArm lArmCol rHand rHandCol lHand lHandCol pants pantsCol rLeg rLegCol lLeg lLegCol hat hatCol accent accentCol pack packCol pack2 pack2Col headCol decalName faceName
  %app = %bot.appearance;
  %bot.unhidenode($chest[getField(%app,0)]);
  %bot.setNodeColor($chest[getField(%app,0)],getField(%app,1));
  %bot.unhidenode($rarm[getField(%app,2)]);
  %bot.setNodeColor($rarm[getField(%app,2)],getField(%app,3));
  %bot.unhidenode($larm[getField(%app,4)]);
  %bot.setNodeColor($larm[getField(%app,4)],getField(%app,5));
  %bot.unhidenode($rhand[getField(%app,6)]);
  %bot.setNodeColor($rhand[getField(%app,6)],getField(%app,7));
  %bot.unhidenode($lhand[getField(%app,8)]);
  %bot.setNodeColor($lhand[getField(%app,8)],getField(%app,9));
  if(getField(%app,10) $= "0")
  {
   %bot.unhidenode($hip[getField(%app,10)]);
   %bot.setNodeColor($hip[getField(%app,10)],getField(%app,11));
   %bot.unhidenode($rLeg[getField(%app,12)]);
   %bot.setNodeColor($rLeg[getField(%app,12)],getField(%app,13));
   %bot.unhidenode($lLeg[getField(%app,14)]);
   %bot.setNodeColor($lLeg[getField(%app,14)],getField(%app,15));
  }
  else
  {
   %bot.unhidenode($hip[getField(%app,10)]);
   %bot.setNodeColor($hip[getField(%app,10)],getField(%app,11));
   %bot.unhidenode("skirtTrimRight");%bot.setNodeColor("skirtTrimRight",getField(%app,13));
   %bot.unhidenode("skirtTrimLeft");%bot.setNodeColor("skirtTrimLeft",getField(%app,15));
  }
  if($hat[getField(%app,16)] !$= "none")
  {
   %bot.unhidenode($hat[getField(%app,16)]);
   %bot.setNodeColor($hat[getField(%app,16)],getField(%app,17));
   if($accent[getField(%app,18)] !$= "none")
   {
    if($hat[getField(%app,16)] $= "helmet")
    {
     %bot.unhidenode("visor");%bot.setNodecolor("visor",getField(%app,19));
    }
    else
    {
     %bot.unhidenode($accent[getField(%app,18)]);
     %bot.setNodeColor($accent[getField(%app,18)],getField(%app,19));
    }
   }
  }
  if($pack[getField(%app,20)] !$= "none")
  {
   %bot.unhidenode($pack[getField(%app,20)]);
   %bot.setNodeColor($pack[getField(%app,20)],getField(%app,21));
  }
  if($secondpack[getField(%app,22)] !$= "none")
  {
   %bot.unhidenode($secondpack[getField(%app,22)]);
   %bot.setNodeColor($secondpack[getField(%app,22)],getField(%app,23));
  }

  %bot.setNodeColor("headskin",getField(%app,24));
  %bot.setDecalName(getField(%app,25));
  %bot.setFaceName(getField(%app,26));
}

function calculateEffectiveRange(%obj)
{
 if(!isObject(%obj) || !isObject(%obj.projectile)){return 0;}
 %type = %obj.projectile;
 if((%type.lifetime * %type.muzzleVelocity) < 300){%obj.botWeaponRange = 1;return;}
 %obj.botWeaponRange = 10;
}

function calculateWeaponType(%obj)
{
 if(!isObject(%obj)){return 0;}
 for(%i=0;%obj.stateName[%i] !$= "";%i++)
 {
  if(%obj.stateName[%i] $= "Check" || %obj.stateName[%i] $= "CheckFire"){%check = 1;}
  if(%obj.stateName[%i] $= "StopFire"){%stopfire = 1;}
  if(%obj.stateName[%i] $= "Smoke"){%smoke = 1;}
  if(%obj.stateName[%i] $= "Reload"){%reload = 1;}
  if(%obj.stateName[%i] $= "Armed"){%charge = 1;}
 }
 if(%check || %stopfire){%obj.botWeaponType = "bow";return;}
 if(%smoke && %reload){%obj.botWeaponType = "gun";return;}
 if(%charge){%obj.botWeaponType = "spear";return;}
 %obj.botWeaponType = "unusable";
}

//Automatic definitions of 'odd' weapons
exec("./Support_WeaponTypes.cs.noexec");

function AIPlayer::fireAt(%this,%pos)
{
 if(!isObject(%this.connection))
 {
  error("AIPlayer::fireat() only works with Space Guy's bots mod.");return;
 }
 %this.setAimLocation(%pos);
 if(isObject(%this.getMountedImage(0)) && vectorDist(%this.getEyePoint(),%pos) > 8)
 {
  %pos = vectorAdd(%pos,vectorSub(%this.getEyePoint(),%this.getmuzzlepoint(0)));
     %a = %this.getMuzzleVector(0);
     %b = vectorNormalize(vectorSub(%this.getmuzzlepoint(0),%pos));
  %this.setAimLocation(%pos);
  if(mACos(vectorDot(%a,%b)) < 2.98)
   return;
 }
 if(vectorDist(%this.gethackposition(),%pos) < 4){%range = 1;}else{%range = 10;}
 for(%i=0;%i<5;%i++)
 {
  if(!isObject(%this.tool[%i])){continue;}
  if(%this.tool[%i].image.botWeaponType $= "")
  {
   calculateEffectiveRange(%this.tool[%i].image);
   calculateWeaponType(%this.tool[%i].image);
  }
  if(%this.tool[%i].image.botweaponrange == 1 && %this.tool[%i].image.botweapontype !$= "unusable")
  {
   %hasSword = 1;
   %swordSlot = %i;
   %swordWeapon = %this.tool[%i].image.getID();
  }
  if(%this.tool[%i].image.botweaponrange == 10 && %this.tool[%i].image.botweapontype !$= "unusable")
  {
   %hasGun = 1;
   %gunSlot = %i;
   %gunWeapon = %this.tool[%i].image.getID();
  }
  //if(%this.tool[%i].image.botweaponrange $= %range && %this.tool[%i].image.botweapontype !$= "unusable")
  //{
  // %useWeapon = %this.tool[%i].image.getID();
  // %useSlot = %i;
  // break;
  //}
 }
 if(%hasGun && (%range == 10 || !%hasSword))
 {
  %useWeapon = %gunWeapon;
  %useSlot = %gunSlot;
 }
 if(%hasSword && (%range == 1 || !%hasGun))
 {
  %useWeapon = %swordWeapon;
  %useSlot = %swordSlot;
 }
 if(isObject(%useWeapon) && %this.getMountedImage(0) !$= %useWeapon)
 {
  %this.connection.setTrigger(0,0);
  servercmdUseTool(%this,%useSlot);
  return;
 }
 if(!isObject(%this.getMountedImage(0)))
  return;
 if(%this.getImageState(0) $= "Activate")
  return;
 %useweapon = %this.getMountedImage(0);
 switch$(%useWeapon.botWeaponType)
 {
  case "bow":
   %this.connection.setTrigger(0,1);
  case "gun":
   %this.gunOn = !%this.gunOn;
   %this.connection.setTrigger(0,%this.gunOn);
  case "spear":
   %this.connection.setTrigger(0,(%this.getImageState(0) !$= "Armed"));
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

function getUpDown(%a,%b)
{
 if(getWord(%a,2) < getWord(%b,2)){return -1;}else if(getWord(%a,2) > getWord(%b,2)){return 1;}else{return 0;}
}

function sign(%num)
{
 if(%num < 0){return -1;}
 if(%num > 0){return 1;}
 return 0;
}

function botIsAllied(%bot,%other)
{
 if($AddOn__GameMode_TeamDM_Base !$= "1")
  return 0;
 if(!$TeamDM::TeamsOn)
  return 0;
 if(!isObject(%bot.minigame) || !isObject(%other.client.minigame))
  return 0;
 if(!isObject(%bot.tdmteam))
  return 0;
 if(%bot.tdmteam $= %other.client.tdmteam)
  return 1;
 return 0;
}

/////////////////
//Package//
/////////////////

package SpaceBots
{
 function servercmdNoClip(%client)
 {
  //JVS Spectator Mod calls servercmdNoClip for the bots as they die.
  //If the 'client' is actually a bot, don't call the function, otherwise let JVS code handle it
  if(%client.getClassName() !$= "AIPlayer" && $JVS::ServerMods::Spectator)
  {
   Parent::servercmdNoClip(%client);
  }
 }
 function AIPlayer::onDeath(%this, %sourceObject, %sourceClient, %damageType, %damLoc)
 {
  GameConnection::ondeath(%this, %sourceObject, %sourceClient, %damageType, %damLoc);
  if(%this.isPermenant)
  {
   %respawn = (%this.minigame.respawntime !$= "" ? %this.minigame.respawntime : 1000);
   %bot = %this;
   schedule(%respawn,0,respawnBot,%bot.minigame,%bot.ownerclient,%bot.ownerID,%bot.name,%bot.appearance,%bot.tdmteam TAB %bot.isleader,%bot.behaviour TAB %bot.bArg,"PICK",1);
  }
  %this.isBotSpawned.ticknum = 0;
 }
 function AIPlayer::incScore(%this,%amount)
 {
  //No
 }
 function AIPlayer::Play2d(%this)
 {
  //%this.emote(winstarprojectile,1);
 }
 function servercmdSetMinigamedata(%client,%fields)
 {
  Parent::servercmdSetMinigamedata(%client,%fields);
  if(!isObject(%client.minigame)){return;}
  if(!isObject($botctrl)){return;}
  for(%i=0;%i<$botctrl.count;%i++)
  {
   %obj = $botctrl.getvalue(%i);
   if(%obj.minigame $= %client.minigame)
   {
     %obj.unmountimage(0);
     for(%j=0;%j<5;%j++)
     {
      %obj.tool[%j] = (isObject(%obj.minigame.startEquip[%j]) ? %obj.minigame.startEquip[%j] : 0);
     }
     %obj.setdatablock(%obj.minigame.playerdatablock);
     botAppearance(%obj);
   }
  }
 }
 function AIPlayer::setTempColor(%this,%a,%b)
 {
  Parent::setTempColor(%this,%a,%b);
  if(%this.appearance !$= "")
  {
   cancel(%this.colorSet);
   %this.colorset = schedule(%b,0,botappearance,%this);
  }
 }
 function AIPlayer::applyBodyParts(){}
 function AIPlayer::applyBodyColors(){}
 function AIPlayer::onLeaveMissionArea(){}
 function AIPlayer::onEnterMissionArea(){}
 function AIPlayer::setjump(%this,%on)
 {
  if(!isObject(%this.connection))
  {
   error("AIPlayer::setjump() only works with Space Guy's bots mod.");return;
  }
  %this.connection.setTrigger(2,%on);
 }
 function AIPlayer::setcrouch(%this,%on)
 {
  if(!isObject(%this.connection))
  {
   error("AIPlayer::setcrouch() only works with Space Guy's bots mod.");return;
  }
  %this.connection.setTrigger(3,%on);
 }
 function AIPlayer::setjet(%this,%on)
 {
  if(!isObject(%this.connection))
  {
   error("AIPlayer::setjet() only works with Space Guy's bots mod.");return;
  }
  %this.connection.setTrigger(4,%on);
 }
 function AIPlayer::setMoveDestination(%this,%pos,%slow) //When using an AIConnection for crouching, jetting and jumping, the default function does not work
 {
  if(!isObject(%this.connection))
  {
   Parent::setMoveDestination(%this,%pos,%slow);
   return;
  }
  %this.movedest = %pos;
 }
 function AIPlayer::setAimLocation(%this,%pos) //When using an AIConnection for crouching, jetting and jumping, the default function does not work
 {
  if(!isObject(%this.connection))
  {
   Parent::setAimLocation(%this,%pos);
   return;
  }
  %this.aimloc = %pos;
 }
 function ProjectileData::onExplode(%this,%obj,%a,%b,%c,%d)
 {
  Parent::onExplode(%this,%obj,%a,%b,%c,%d);
 }
 function ProjectileData::Damage(%this,%obj,%col,%fade,%pos,%normal)
 {
  Parent::Damage(%this,%obj,%col,%fade,%pos,%normal);
  if(%col.getClassName() $= "AIPlayer" && %col.behaviour $= "AI" && isObject(%obj.client.player))
  {
   %col.AIAlert = 1;
   %col.AIAlertFirer = %obj.client.player;
   %col.AIAlertPosition = %pos;
  }
 }
 function updateMove(%obj)
 {
  if(!isObject(%obj)){return;}
  %eye = %obj.getEyePoint();
  //if(%obj.isPilot()){%obj.setTransform("0 0 0 0 0 1 0");%eye = %obj.getControlObject().getEyePoint();}
  //I don't use aimObject, moveObject or moveDestination slowdown, I am not adding these
  if(%obj.movedest !$= "")
  {
   if(vectorDist(%obj.movedest,%obj.getPosition()) < 0.2){%obj.connection.setMove("x",0);%obj.connection.setMove("y",0);return;}
   %a = vectornormalize(vectorSub(%obj.movedest,%eye));
   %b = %obj.geteyevector();
   %t = mATan(getWord(%a,0),getWord(%a,1));
   %n = mATan(getWord(%b,0),getWord(%b,1));
   if(%n-%t < -$pi){%n+=2*$pi;}
   if(%n-%t > $pi){%n-=2*$pi;}
   %ang = %n-%t;

   //commandtoall('centerprint',%a NL %b,2,3);

   %obj.connection.setMove("x",-mSin(%ang));
   %obj.connection.setMove("y",mCos(%ang));
  }
 }
 function updateAim(%obj)
 {
  if(!isObject(%obj)){return;}
  %eye = %obj.getEyePoint();
  //if(%obj.isPilot()){%obj.setTransform("0 0 0 0 0 1 0");%eye = %obj.getControlObject().getEyePoint();}
  //I don't use aimObject, moveObject or moveDestination slowdown, I am not adding these
  if(%obj.aimloc !$= "")
  {
   if(vectorDist(%obj.aimloc,%obj.getPosition()) < 0.2){%obj.connection.setMove("yaw",0);%obj.connection.setMove("pitch",0);return;}
   %a = vectornormalize(vectorSub(%obj.aimloc,%eye));
   %b = %obj.geteyevector();

   %aXY0 = vectorNormalize(getWords(%a,0,1));%bXY0 = vectorNormalize(getWords(%b,0,1));
   %aim = getLeftRight(%aXY0,%bXY0)*mACos(vectorDot(%aXY0,%bXY0));
   %obj.connection.setMove("yaw",%aim);
   //%obj.connection.setMove("yaw",0);
   
   %a0 = getWord(%a,0);%a1 = getWord(%a,1);
   %b0 = getWord(%b,0);%b1 = getWord(%b,1);
   %aAZ = 0 SPC mSqrt(%a0*%a0 + %a1*%a1) SPC getWord(%a,2);
   %bAZ = 0 SPC mSqrt(%b0*%b0 + %b1*%b1) SPC getWord(%b,2);
   %aim = getUpDown(%a,%b)*mACos(vectorDot(%aAZ,%bAZ));
   //commandtoall('centerprint',%aAZ SPC "\c3" @ %bAZ NL %aim,2,3);
   %obj.connection.setMove("pitch",-%aim);
   //%obj.connection.setMove("pitch",0);
  }
 }
 function Armor::onMount(%this,%obj,%vehicle,%node)
 {
  Parent::onMount(%this,%obj,%vehicle,%node);
 }
 function Armor::onTrigger(%data,%obj,%slot,%on)
 {
  Parent::onTrigger(%data,%obj,%slot,%on);
  if(!%on || %slot !$= "0" || isObject(%obj.getMountedImage(0))){return;}
  %this = %obj;
  %mouseVec = %this.getEyeVector();
  %cameraPoint = %this.getEyePoint();
  %selectRange = 6;
  %mouseScaled = VectorScale(%mouseVec, %selectRange);
  %rangeEnd = VectorAdd(%cameraPoint, %mouseScaled);
  %searchMasks = $TypeMasks::PlayerObjectType;
  %scanTarg = ContainerRayCast (%cameraPoint, %rangeEnd, %searchMasks);
  if(!isObject(firstWord(%scanTarg))){return;}
  %scanTarg = firstWord(%scanTarg);
  if(%scanTarg.behaviour $= "Talk")
  {
   %scanTarg.playthread(0,talk);
   cancel(%scanTarg.talk);
   %scanTarg.setAimLocation(%obj.getEyePoint());
   messageclient(%obj.client,'',"\c3" @ %scanTarg.name @ "\c6: " @ strReplace(%scanTarg.bArg,"_"," "));
   %scanTarg.talk = %scanTarg.schedule(1500,playthread,0,root);
  }
 }
 function WrenchProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
 {
 	if(!(isObject(%col) && %col.getClassName() $= "fxDTSBrick" && %col.getDatablock().getname() $= "brickSBotSpawnData" && %obj.client.hasSBotGUI)){Parent::onCollision(%this,%obj,%col,%fade,%pos,%normal);return;}
	for(%i=0;%i<clientgroup.getCount();%i++)
	{
		%player = %player TAB clientgroup.getObject(%i).name;
	}
	%player = getFields(%player,1);

	if(isObject($teamDM::teamManager))
	{
		for(%i=0;%i<$teamdm::teammanager.teamcount;%i++)
		{
			%teams = %teams TAB $teamdm::teammanager.team[%i].name;
		}
	}
	%teams = getFields(%teams,1);
	for(%i=0;$SpaceBots::Path[%obj.client.bl_id,%i] !$= "";%i++)
	{
		%paths = %paths TAB getField($SpaceBots::Path[%obj.client.bl_id,%i],0);
	}
	%paths = getFields(%paths,1);
	if(%col.botMS $= ""){%col.botMS = 1000;}
	if(%col.botMax $= ""){%col.botMax = 1;}
	if(%col.botAI $= ""){%col.botAI = "Stand\t";}
	%teamname = %col.botTeam.name;
	%params = %col.botName TAB %col.botAI TAB %col.botClone TAB %teamname TAB %col.botMS TAB %col.botMax;
	%obj.client.botbrick = %col;
	commandtoclient(%obj.client,'OpenSBotSpawnMenu',%params,%player,%teams,%paths);
	serverPlay3d(wrenchHitSound,%pos SPC "1 0 0 0");
 }
 function AIPlayer::delete(%this)
 {
  if(isObject(%this.connection)){%this.connection.delete();}
  Parent::delete(%this);
 }
};activatePackage(SpaceBots);


//Clientgroup:
//TeamsOn (in minigame+team, respawn)
//TeamsOff (in minigame+team, respawn)
//Sortteams (do sort for bots too)
//togAutoSort (do sort and respawn for bots)
//togUniform (do uniform for bots)
//tdmGetPlayers (send bots in menu)
//loadTDMSave (auto sort)
//setTeamGUI (work)
//setLeadGUI (work)
//teamdisband (in minigame+team+teamson, respawn)
//clearteams (in minigame+team+teamson, respawn)

//uniform (use team/lead uniform)
//respawn (yep)
//forceJoin (put bots in minigame instantly)
package TDMSpaceBots
{
 function servercmdteamson(%client)
 {
  Parent::servercmdteamson(%client);
  if(!$TeamDM::TeamsOn) //You weren't allowed to start TDM
   return;
  for(%i=0;%i<$botctrl.count;%i++)
  {
   %b = $BotCtrl.getValue(%i);
   if(isObject(%b.minigame) && isObject(%b.tdmteam))
   {
    respawn(%b);
   }
  }
 }
 function servercmdteamsoff(%client)
 {
  Parent::servercmdteamsoff(%client);
  if($TeamDM::TeamsOn) //You weren't allowed to end TDM
   return;
  for(%i=0;%i<$botctrl.count;%i++)
  {
   %b = $BotCtrl.getValue(%i);
   if((isObject(%b.minigame) || $SpaceBots::RespawnTeamBots) && isObject(%b.tdmteam))
   {
    respawn(%b);
   }
  }
 }
 function servercmdsortteams(%client)
 {
  if(AdminCheck(%client,$Pref::TeamDM::SecureLevel) !$= "1")
  {
   messageclient(%client,'',"\c5You need to be " @ AdminCheck(%client,$Pref::TeamDM::SecureLevel) @ " to reset teams.");
   return;
  }
  if(!isObject($TeamDM::TeamManager.team0))
  {
   messageclient(%client,'',"\c5A team must exist to sort them!");
   return;
  }
  %i = $TeamDM::TeamManager.teamCount;
  %count = getRandom(0,%i);
  for(%j=0;%j<ClientGroup.getCount()+$botctrl.count;%j++)
  {
   %count = (%count + 1) % %i;
   servercmdSetTeamGUI(%client,%count+1,%j);
  }
 }
 function servercmdtogautosort(%client)
 {
 if(AdminCheck(%client,$Pref::TeamDM::SecureLevel) !$= "1")
 {
  messageclient(%client,'',"\c5You need to be " @ AdminCheck(%client,$Pref::TeamDM::SecureLevel) @ " to toggle \c3Auto Sort\c5.");
  return;
 }
 if(!isObject($TeamDM::TeamManager.team0) && !$TeamDM::AutoSort)
 {
  messageclient(%client,'',"\c5A team must exist to turn on Auto Sort.");
  return;
 }
 if($TeamDM::PackageAutoSortLock)
 {
  messageclient(%client,'',"\c5The current Package, \c3" @ $TeamDM::PackageOn @ "\c5 has disabled the editing of Auto Sort and Lock Teams.");
  return;
 }
 if(isObject(%client.minigame) && !$TeamDM::AutoSort)
 {
  messageclient(%client,'',"\c5End your Minigame before toggling Auto Sort.");
  return;
 }
 $TeamDM::AutoSort = !$TeamDM::AutoSort;
 messageall('',"\c3Auto Sort\c5 is now " @ ($TeamDM::AutoSort ? "\c2on" : "\c0off") @ "\c5.");
 if(!$TeamDM::AutoSort && isObject($TeamDM::Minigame)){$SpaceBots::RespawnTeamBots = 1;toggleForce(%client,0);}
 if(!$TeamDM::AutoSort && $TeamDM::TeamsOn){servercmdTeamsOff(%client);$SpaceBots::RespawnTeamBots = 0;}
 if($TeamDM::AutoSort)
 {
 if(!$TeamDM::ForceJoin){toggleForce(%client,1);messageall('',"\c5A global minigame has been created.");}
  if(!$TeamDM::TeamsOn){servercmdTeamsOn(%client);}
  if($TeamDM::TeamsLocked == 0)
  {
   $TeamDM::TeamsLocked = 1;
   messageall('',"\c3Teams\c5 are now \c0locked\c5.");
  }
  for(%j=0;%j<ClientGroup.getCount();%j++)
  {
   %cl = ClientGroup.getObject(%j);
   if(!isObject(%cl.tdmTeam) && isObject($TeamDM::TeamManager.team0))
   {
    %curCount = 10000;
    for(%i=0;%i<$TeamDM::TeamManager.teamCount;%i++)
    {
     if($TeamDM::TeamManager.team[%i].memberCount < %curCount)
     {
      %team = $TeamDM::TeamManager.team[%i];
      %curCount = $TeamDM::TeamManager.team[%i].memberCount;
     }
    }
    %team.addMember(%cl);
    %cl.tdmTeam = %team;
    if($TeamDM::TeamsOn && isObject(%cl.minigame))
    respawn(%cl);
    messageallexcept(%cl,-1,'',"\c3" @ %cl.name @ "\c5 was put into <color:" @ $TeamDM::Color[%team.color,1] @ ">" @ %team.name @ "\c5. (Auto Sort)");
    messageclient(%cl,'',"\c5You were put into <color:" @ $TeamDM::Color[%team.color,1] @ ">" @ %team.name @ "\c5. (Auto Sort)");
   }
  }
  for(%j=0;%j<$botctrl.count;%j++)
  {
   %cl = $botctrl.getValue(%j);
   if(!isObject(%cl.tdmTeam) && isObject($TeamDM::TeamManager.team0))
   {
   %curCount = 10000;
   for(%i=0;%i<$TeamDM::TeamManager.teamCount;%i++)
   {
    if($TeamDM::TeamManager.team[%i].memberCount < %curCount)
    {
     %team = $TeamDM::TeamManager.team[%i];
     %curCount = $TeamDM::TeamManager.team[%i].memberCount;
     }
    }
    %team.addMember(%cl);
    %cl.tdmTeam = %team;
    //if($TeamDM::TeamsOn && isObject(%cl.minigame))
    //schedule(100,0,respawn,%cl);
    messageallexcept(%cl,-1,'',"\c3" @ %cl.name @ "\c5 was put into <color:" @ $TeamDM::Color[%team.color,1] @ ">" @ %team.name @ "\c5. (Auto Sort)");
    }
   }
  }
 }
 function servercmdtoguniform(%client)
 {
  if(AdminCheck(%client,$Pref::TeamDM::SecureLevel) !$= "1")
  {
   messageclient(%client,'',"\c5You need to be " @ AdminCheck(%client,$Pref::TeamDM::SecureLevel) @ " to toggle \c3Team Uniforms\c5.");
   return;
  }
  $Pref::TeamDM::Uniform = !$Pref::TeamDM::Uniform;
  for(%i=0;%i<ClientGroup.getCount();%i++)
  {
   %cl = ClientGroup.getObject(%i);
   if(isObject(%cl.tdmTeam) && isObject(%cl.player) && isObject(%cl.minigame) && $TeamDM::TeamsOn)
   {
    uniform(%cl.player,%cl.tdmTeam);
   }
  }
  for(%i=0;%i<$botctrl.count;%i++)
  {
   %cl = $botctrl.getValue(%i);
   if(isObject(%cl.tdmTeam) && isObject(%cl.player) && isObject(%cl.minigame) && $TeamDM::TeamsOn)
   {
    uniform(%cl.player,%cl.tdmTeam);
   }
  }
  tdmmessageall('',"\c3Team Uniforms\c5 are now " @ ($Pref::TeamDM::Uniform ? "\c2on" : "\c0off") @ "\c5.");
  if(!isObject(%client.minigame)){messageclient(%client,'',"\c3Team Uniforms\c5 are now " @ ($Pref::TeamDM::Uniform ? "\c2on" : "\c0off") @ "\c5.");}
 }
 function servercmdtdmgetplayers(%client)
 {
  Parent::servercmdtdmgetplayers(%client);
  for(%i=0;%i<$botctrl.count;%i++)
  {
   %cl = $botctrl.getValue(%i);
   if(%cl.minigame $= %client.minigame && isObject(%client.minigame)){%mini = "*";}else{%mini = "-";}
   if(isObject(%cl.tdmTeam)){%team = %cl.tdmTeam.name;}else{%team = "-";}
   if(%cl.tdmTeam.leader $= %cl){%lead = "*";}else{%lead = "-";}
   commandtoclient(%client,'GUIset',"listRow","lstTDMplayerlist",%cl.name TAB %mini TAB %team TAB %lead,%i+clientgroup.getcount());
  }
 }
 function loadTDMsave(%client,%file)
 {
  Parent::loadTDMsave(%client,%file);
  if($TeamDM::AutoSort)
  {
   for(%j=0;%j<$botctrl.count;%j++)
   {
    %cl = $botctrl.getValue(%j);
    if(!isObject(%cl.tdmTeam) && isObject($TeamDM::TeamManager.team0))
    {
     %curCount = 10000;
     for(%i=0;%i<$TeamDM::TeamManager.teamCount;%i++)
     {
      if($TeamDM::TeamManager.team[%i].memberCount < %curCount)
      {
       %team = $TeamDM::TeamManager.team[%i];
       %curCount = $TeamDM::TeamManager.team[%i].memberCount;
      }
     }
     %team.addMember(%cl);
     %cl.tdmTeam = %team;
     tdmmessageallexcept(%cl,-1,'',"\c3" @ %cl.name @ "\c5 was put into <color:" @ $TeamDM::Color[%team.color,1] @ ">" @ %team.name @ "\c5. (Auto Sort)");
    }
   }
  }
 }
 function servercmdSetTeamGUI(%client,%team,%player)
 {
  if(%player < clientgroup.getcount())
  {
   Parent::servercmdSetTeamGUI(%client,%team,%player);
   return;
  }
  if(AdminCheck(%client,$Pref::TeamDM::SecureLevel) !$= "1")
  {
   if(%team !$= "0")
   servercmdJoinTeam(%client,$TeamDM::TeamManager.team[%team-1].name);
   else
   servercmdLeaveTeam(%client);
   return;
  }
  if(%player $= "-1")
  {
   messageclient(%client,'',"\c5The player you are editing doesn't exist!");
   return;
  }
  if(%team $= "-1")
  {
   %team = 0;
  }
  %cl = $botctrl.getvalue(%player-clientgroup.getcount());
  if(%cl.isBotSpawned)
  {
   %spawn = %cl.isBotSpawned;
   %spawn.botTeam = $TeamDM::TeamManager.team[%team-1];
   for(%i=0;%i<$BotCtrl.count;%i++)
   {
    %b = $botctrl.getValue(%i);
    if(%b.isBotSpawned $= %spawn){%b.delete();}
   }
   %spawn.ticknum = %spawn.botMS;
   return;
  }
  if(%team !$= "0")
  {
   if(isObject(%cl.tdmTeam)){%str = "moved to";}else{%str = "put into";}
   tdmmessageallexcept(%cl,-1,'',"\c3" @ %cl.name @ "\c5 was " @ %str @ " <color:" @ $TeamDM::Color[$TeamDM::TeamManager.team[%team-1].color,1] @ ">" @ $TeamDM::TeamManager.team[%team-1].name @ "\c5.");
   if(!isObject(%client.minigame)){messageclient(%client,'',"\c3" @ %cl.name @ "\c5 was " @ %str @ " <color:" @ $TeamDM::Color[$TeamDM::TeamManager.team[%team-1].color,1] @ ">" @ $TeamDM::TeamManager.team[%team-1].name @ "\c5.");}
   if(isObject(%cl.isLeader))
   {
    %cl.isLeader.leader = "";
    %cl.isLeader = "";
   }
   if(isObject(%cl.tdmTeam)){%cl.tdmTeam.removeMember(%cl);}
   %cl.tdmTeam = $TeamDM::TeamManager.team[%team-1];
   if($TeamDM::TeamsOn && isObject(%cl.minigame)){respawn(%cl);}
   $TeamDM::TeamManager.team[%team-1].addMember(%cl);
  }
  else
  {
   if(!isObject(%cl.tdmTeam)){messageclient(%client,'',"\c3" @ %cl.name @ "\c5 isn't in a team.");return;}
   messageallexcept(%cl,-1,'',"\c3" @ %cl.name @ "\c5 was removed from <color:" @ $TeamDM::Color[%cl.tdmTeam.color,1] @ ">" @ %cl.tdmTeam.name @ "\c5.");
   if(isObject(%cl.isLeader))
   {
    %cl.isLeader.leader = "";
    %cl.isLeader = "";
   }
   %cl.tdmTeam.removeMember(%cl);
   %cl.tdmTeam = "";
   if($TeamDM::TeamsOn && isObject(%cl.minigame)){respawn(%cl);}
  } 
 }
 function servercmdsetleadGUI(%client,%team,%player)
 {
  if(%player < clientgroup.getcount())
  {
   Parent::servercmdSetLeadGUI(%client,%team,%player);
   return;
  }
  if(AdminCheck(%client,$Pref::TeamDM::SecureLevel) !$= "1")
  {
   messageclient(%client,'',"\c5You need to be " @ AdminCheck(%client,$Pref::TeamDM::SecureLevel) @ " \c5to set team leaders.");
   return;
  }
  if(%player $= "-1")
  {
   messageclient(%client,'',"Edit Teams","\c5The player you are editing doesn't exist!");
   return;
  }
  if(%team $= "-1")
  {
   %team = 0;
  }
  %cl = $botctrl.getvalue(%player-clientgroup.getcount());
  if(%cl.isBotSpawned)
  {
   messageclient(%client,'',"Edit Teams","\c5You can't set a bot created by a Bot Spawn to be a team leader.");
   return;
  }
  %teamID = $TeamDM::TeamManager.team[%team-1];
  if(%team $= "0")
  {
   if(isObject(%cl.isLeader))
   {
    tdmmessageall('',"\c3" @ %cl.name @ "\c5 is no longer a team leader.");
    if(!isObject(%client.minigame)){messageclient(%client,'',"\c3" @ %cl.name @ "\c5 is no longer a team leader.");}
    %cl.isLeader.leader = "";
    %cl.isLeader = "";
    if($TeamDM::TeamsOn){respawn(%cl);}
    return;
   }
   else
   {
    messageclient(%client,'',"\c3" @ %cl.name @ "\c5 doesn't lead a team.");return;
   }
  }
  if(%teamID !$= %cl.tdmTeam)
  {
    if(isObject(%cl.isLeader))
    {
     %cl.isLeader.leader = "";
    }
    tdmmessageall('',"\c3" @ %cl.name @ "\c5 was put into and set as the leader of <color:" @ $TeamDM::Color[%teamID.color,1] @ ">" @ %teamID.name @ "\c5.");
    if(!isObject(%client.minigame)){messageclient(%client,'',"\c3" @ %cl.name @ "\c5 was put into and set as the leader of <color:" @ $TeamDM::Color[%teamID.color,1] @ ">" @ %teamID.name @ "\c5.");}
    %cl.tdmTeam = %teamID;
    %cl.isLeader = %teamID;
    %teamID.leader = %cl;
    if($TeamDM::TeamsOn){respawn(%cl);}
    return;
  }
  if(%teamID $= %cl.tdmTeam)
  {
    messageall('',"\c3" @ %cl.name @ "\c5 was set as the leader of <color:" @ $TeamDM::Color[%teamID.color,1] @ ">" @ %teamID.name @ "\c5.");
    if(!isObject(%client.minigame)){messageclient(%client,'',"\c3" @ %cl.name @ "\c5 was set as the leader of <color:" @ $TeamDM::Color[%teamID.color,1] @ ">" @ %teamID.name @ "\c5.");}
    %cl.isLeader = %teamID;
    %teamID.leader = %cl;
    if($TeamDM::TeamsOn){respawn(%cl);}
    return;
  }
 }
 function servercmdTeamDisband(%client,%teamID)
 {
  if(AdminCheck(%client,$Pref::TeamDM::SecureLevel) !$= "1")
  {
   messageclient(%client,'',"\c5You need to be " @ AdminCheck(%client,$Pref::TeamDM::SecureLevel) @ " \c5to delete teams.");
   return;
  }
  if($TeamDM::PackageTeamLock)
  {
   messageclient(%client,'',"\c5The current Package, \c3" @ $TeamDM::PackageOn @ "\c5 has disabled the adding/removing of teams.");
   return;
  }
  if(!isObject($TeamDM::TeamManager.team[%teamID]))
  {
   messageclient(%client,'',"\c5The team you are deleting doesn't exist!");
   return;
  }
  tdmmessageallexcept(%client,-1,'',"\c3" @ %client.name @ "\c5 disbanded <color:" @ $TeamDM::TeamManager.team[%teamID].color @ ">" @ $TeamDM::TeamManager.team[%teamID].name @ "\c5.");
  messageclient(%client,'',"\c5You disbanded <color:" @ $TeamDM::Color[$TeamDM::TeamManager.team[%teamID].color,1] @ ">" @ $TeamDM::TeamManager.team[%teamID].name @ "\c5.");
   %team = $TeamDM::TeamManager.team[%teamID];
   %team.leader.isLeader = "";
   for(%i = %teamID;%i<$teamDM::TeamManager.teamcount;%i++)
   {
    $teamDM::TeamManager.team[%i].serverID--;
    $teamDM::TeamManager.team[%i] = $teamDM::TeamManager.team[%i+1];
   }
   for(%i=0;%i<ClientGroup.getCount();%i++)
   {
    %cl = ClientGroup.getObject(%i);
    if(%cl.tdmTeam $= %team && $TeamDM::TeamsOn && isObject(%cl.minigame))
    {
     respawn(%cl);
    }
   }
   for(%i=0;%i<$botctrl.count;%i++)
   {
    %cl = $botctrl.getvalue(%i);
    if(%cl.tdmTeam $= %team && $TeamDM::TeamsOn && isObject(%cl.minigame))
    {
     respawn(%cl);
    }
   }
   $TeamDM::TeamManager.teamCount--;
   %team.delete();
  if(!isObject($TeamDM::TeamManager.team0) && $TeamDM::AutoSort)
  {
   messageall('',"\c3Auto Sort\c5 has been turned off. (No teams exist)");
   return;
  }
 }
 function clearteams()
 {
  for(%i = $teamDM::TeamManager.teamcount-1;%i>-1;%i--)
  {
   %team = $TeamDM::TeamManager.team[%i];
   %team.leader.isLeader = "";
   for(%j = %i;%j<$teamDM::TeamManager.teamcount;%j++)
   {
    $teamDM::TeamManager.team[%j].serverID--;
    $teamDM::TeamManager.team[%j] = $teamDM::TeamManager.team[%j+1];
   }
   for(%j=0;%j<ClientGroup.getCount();%j++)
   {
    %cl = ClientGroup.getObject(%j);
    if(%cl.tdmTeam $= %team && $TeamDM::TeamsOn && isObject(%cl.minigame))
    {
     respawn(%cl);
    }
   }
   for(%i=0;%i<$botctrl.count;%i++)
   {
    %cl = $botctrl.getvalue(%i);
    if(%cl.tdmTeam $= %team && $TeamDM::TeamsOn && isObject(%cl.minigame))
    {
     respawn(%cl);
    }
   }
   $TeamDM::TeamManager.teamCount--;
   %team.delete();
   if(!isObject($TeamDM::TeamManager.team0) && $TeamDM::AutoSort)
   {
    servercmdtogautosort(%client);
    messageall('',"\c5(No teams exist)");
    return;
   }
  }
 }
 function respawn(%this)
 {
  if(!isObject(%this)){return;}
  if(%this.getclassname() !$= "AIPlayer"){parent::respawn(%this);return;}
  if(isObject($TeamDM::PackageManager)){$TeamDM::PackageManager.respawn(%this);}
  %obj = %this;
  if(%obj.isBotSpawned)
  {
   %spawn = %obj.isBotSpawned;
   %obj.delete();
   %spawn.ticknum = %spawn.botMS;
   return;
  }
    schedule(10,0,respawnBot,%obj.minigame,%obj.ownerclient,%obj.ownerclient.bl_id,%obj.name,%obj.appearance,%obj.tdmteam TAB %obj.isLeader,%obj.behaviour TAB %obj.barg,"PICK",%obj.isPermenant);
  %this.delete();
  return 1;
 }
 function toggleForce(%client,%on)
 {
  $TeamDM::ForceJoin = %on;
  if($TeamDM::ForceJoin)
  {
   for(%i=0;%i<ClientGroup.getCount();%i++){%c=ClientGroup.getObject(%i);if(isObject(%c) && isObject(%c.minigame) && %c.minigame.owner $= %c){servercmdEndMinigame(%c);}}
   servercmdCreateMinigame(%client,"-Default-",3,0);
   schedule(800,0,miniSet,%client);
   schedule(100,0,respawn,%client);
   //Minigame data.
   //servercmdSetMinigamedata(%client,"T Default Mini-Game" TAB "IO 0" TAB "UAPB 0" TAB "PUOB 0" TAB "USB 1" TAB "PBB 0" TAB "PPB 0" TAB "PKP 1" TAB "PKS -1" TAB "PD 0" TAB "RT 1" TAB "VRT 5" TAB "BRT 10" TAB "FD 1" TAB "WD 1" TAB "SD 1" TAB "VD 1" TAB "BD 1" TAB "EW 0" TAB "EB 1" TAB "EP 1" TAB "DB 44" TAB "SE 0 -1" TAB "SE 1 -1" TAB "SE 2 -1" TAB "SE 3 -1" TAB "SE 4 -1");
   $TeamDM::Minigame = %client.minigame;
   for(%i=0;%i<ClientGroup.getCount();%i++){%c=ClientGroup.getObject(%i);if(%c !$= %client){schedule(100,0,addMember,%c);}}
   //Bots auto join TeamDM::Minigame
   schedule(200,0,commandtoall,'setbuildingdisabled',0);
  }
  else
  {
   if(isObject($TeamDM::Minigame))
   {
    %oldmini = %client.minigame;
    %client.minigame = $TeamDM::Minigame;
    $TeamDM::Minigame.owner = %client;
    servercmdEndMinigame(%client);
    %client.minigame = %oldmini;
    $TeamDM::Minigame = "";
    schedule(200,0,commandtoall,'setbuildingdisabled',0);
   }
  }
 }
};
if($AddOn__GameMode_TeamDM_Base $= "1"){activatepackage(TDMSpaceBots);}

//Patch for a bug when you wrench some things
package TDMWrenchOverRide
{
	function WrenchProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
	{
		if(!isObject(%col) || %col.getClassName() !$= "fxDTSBrick" || %col.tdmTeam $= "" || AdminCheck(%obj.client,$Pref::TeamDM::SecureLevel) !$= "1"){Parent::onCollision(%this,%obj,%col,%fade,%pos,%normal);return;}
		%team = $TeamDM::TeamManager.team[%col.tdmTeam];
		if(isObject(%team)){%team.spawnlist = strReplace(%team.spawnlist,%col @ " ","");}
		%col.tdmTeam++;
		if(!isObject($TeamDM::TeamManager.team[0])){messageclient(%obj.client,'',"No teams exist yet!");return;}
		if(%col.tdmTeam == $TeamDM::TeamManager.teamCount){%col.tdmTeam = 0;}
		%team = $TeamDM::TeamManager.team[%col.tdmTeam];
		%col.setColor(rgbtopaint($TeamDM::Color[%team.color,0] SPC "1"));
		if(isObject(%team)){%team.spawnlist = %team.spawnlist @ %col @ " ";}
		messageclient(%obj.client,'',"\c5Brick now a <color:" @ $TeamDM::Color[%team.color,1] @ ">" @ %team.name @ "\c5 spawn.");
	}
	function PaintProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
	{
		if(%col.getClassName() !$= "fxDTSbrick" || %col.getDatablock().getName() !$= "brickTDMSpawnData")
		{
			Parent::onCollision(%this,%obj,%col,%fade,%pos,%normal);
		}
	}
};
if($AddOn__GameMode_TeamDM_Base $= "1"){activatepackage(TDMWrenchOverRide);}

////////////////////
//Datablocks//
////////////////////

datablock PlayerData(PlayerBotNoJet : PlayerStandardArmor)
{
 minJetEnergy = 0;
 jetEnergyDrain = 0;
 canJet = 0;
 uiName = "";
 showEnergyBar = false;
};

datablock ParticleData(botMarkerParticle)
{
	dragCoefficient      = 3;
	gravityCoefficient   = 0;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.0;
	lifetimeMS           = 1000;
	lifetimeVarianceMS   = 0;
	textureName          = "base/data/particles/ring";
	spinSpeed		= 0.0;
	spinRandomMin		= -000.0;
	spinRandomMax		= 000.0;
	colors[0]     = "0.9 0.9 0.9";
	colors[1]     = "0.9 0.9 0.9 0.0";
	sizes[0]      = 0.1;
	sizes[1]      = 3.5;

	useInvAlpha = false;
};
datablock ParticleEmitterData(botMarkerEmitter)
{
   ejectionPeriodMS = 100;
   periodVarianceMS = 0;
   ejectionVelocity = 0;
   velocityVariance = 0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 00;
   phiReferenceVel  = 0;
   faceViewer = false;
   phiVariance      = 0;
   overrideAdvance = false;
   particles = "botMarkerParticle";
};

datablock fxDTSBrickData(brickSBotSpawnData : brickSpawnPointData)
{
 uiName = "Bot Spawn";
 subCategory = "Space Guy's Bots";
};