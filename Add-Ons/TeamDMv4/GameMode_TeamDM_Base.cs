////--------------------------------------------------------------------------------\\\\
////Team Deathmatch Mod: Base Functions		\\\\
////Release 1.00                                         		\\\\
////--------------------------------------------------------------------------------\\\\

function reexec()
{
 exec("./GameMode_TeamDM_Base.cs");
}

////----Variable Set Up----\\\\
//First Run variables
if($Pref::TeamDM::SecureLevel $= "")
{
 $Pref::TeamDM::SecureLevel = "3";
 $Pref::TeamDM::Uniform = "1";
 $Pref::TeamDM::TeamChat = "1";
 $Pref::TeamDM::FFire = "0";
 //The default description if no package is on and /tdmhelp is called.
 $Pref::TeamDM::DefaultDesc = "Kill people on enemy teams to gain points!\n\c5Scores are totalled and shown every minute.\n\c5/jointeam [red] to join a team, /leaveteam to leave all teams.";
}

//Color list set up
$TeamDM::ColorList = "";
$TeamDM::Color["Red",0] = "1 0 0";$TeamDM::Color["Red",1] = "FF0000";$TeamDM::ColorList = $TeamDM::ColorList SPC "Red";
$TeamDM::Color["Blue",0] = "0 0 1";$TeamDM::Color["Blue",1] = "0000FF";$TeamDM::ColorList = $TeamDM::ColorList SPC "Blue";
$TeamDM::Color["Green",0] = "0 1 0";$TeamDM::Color["Green",1] = "00FF00";$TeamDM::ColorList = $TeamDM::ColorList SPC "Green";
$TeamDM::Color["Yellow",0] = "1 1 0";$TeamDM::Color["Yellow",1] = "FFFF00";$TeamDM::ColorList = $TeamDM::ColorList SPC "Yellow";
$TeamDM::Color["Cyan",0] = "0 1 1";$TeamDM::Color["Cyan",1] = "00FFFF";$TeamDM::ColorList = $TeamDM::ColorList SPC "Cyan";
$TeamDM::Color["Pink",0] = "1 0 1";$TeamDM::Color["Pink",1] = "FF00FF";$TeamDM::ColorList = $TeamDM::ColorList SPC "Pink";
$TeamDM::Color["White",0] = "1 1 1";$TeamDM::Color["White",1] = "FFFFFF";$TeamDM::ColorList = $TeamDM::ColorList SPC "White";
$TeamDM::Color["Grey",0] = "0.4 0.4 0.4";$TeamDM::Color["Grey",1] = "606060";$TeamDM::ColorList = $TeamDM::ColorList SPC "Grey";
$TeamDM::Color["Black",0] = "0 0 0";$TeamDM::Color["Black",1] = "000000";$TeamDM::ColorList = $TeamDM::ColorList SPC "Black";

function TeamStackSO::addteam(%this,%obj)
{
 %newObj = new ScriptObject(){class = TeamStack2SO;serverID = %this.teamCount;memberCount = 0;};
 %this.team[%this.teamCount] = %newObj;
 %this.teamCount++;
}
function TeamStackSO::delteam(%this,%index)
{
 if(%index < 0 || %index > %this.teamCount){error("[TeamStackSO::delteam] ERROR: Array index out of bounds. " @ %index @ "/" @ %this.teamCount);return;}
 %this.team[%index] = "";
 for(%i = %index+1;%i<%this.teamCount;%i++)
 {
  %this.team[%i-1] = %this.team[%i];
 }
 %this.teamCount--;
}
function TeamStackSO::removeteam(%this,%obj)
{
 %obj.delete();
 for(%i=0;%i<%this.teamCount;%i++)
 {
  if(%this.team[%i] $= %obj)
  {
   %this.team[%i] = "";
   for(%j=%i+1;%j<%this.teamCount;%j++)
   {
    %this.team[%j-1] = %this.team[%j];
   }
   %this.teamCount--;
   return;
  }
 }
 warn("[TeamStackSO::delteamID()] " @ %obj @ " does not exist in the stack.");
}
function TeamStackSO::getteam(%this,%index)
{
 if(%index < 0 || %index > %this.teamCount){error("[TeamStackSO::getteam] ERROR: Array index out of bounds. " @ %index @ "/" @ %this.teamCount);return -1;}
 return %this.team[%index];
}
function TeamStackSO::dumpStack(%this)
{
 echo("[TeamStackSO::dumpStack()]");
 echo("Total Values: " @ %this.teamCount);
 for(%i=0;%i<%this.teamCount;%i++)
 {
  echo(">Value " @ %i @ ": " @ %this.team[%i]);
 }
}
function TeamStackSO::setTeamName(%this,%obj,%name)
{
 %this.getTeam(%obj).name = %name;
}

function TeamStack2SO::addmember(%this,%obj)
{
 %this.memberID[%this.memberCount] = %obj;
 %this.memberCount++;
}
function TeamStack2SO::removeMember(%this,%obj)
{
 for(%i=0;%i<%this.memberCount;%i++)
 {
  if(%this.memberID[%i] $= %obj)
  {
   %this.memberID[%i] = "";
   for(%j=%i+1;%j<%this.memberCount;%j++)
   {
    %this.memberID[%j-1] = %this.memberID[%j];
   }
   %this.memberCount--;
   return;
  }
 }
 warn("[TeamStack2SO::delteamID()] " @ %obj @ " does not exist in the stack.");
}
function TeamStack2SO::getmember(%this,%index)
{
 if(%index < 0 || %index > %this.memberCount){error("[TeamStack2SO::getmember] ERROR: Array index out of bounds. " @ %index @ "/" @ %this.memberCount);return -1;}
 return %this.memberID[%index];
}
function TeamStack2SO::dumpStack(%this)
{
 echo("[TeamStack2SO::dumpStack()]");
 echo("Total Values: " @ %this.memberCount);
 for(%i=0;%i<%this.memberCount;%i++)
 {
  echo(">Value " @ %i @ ": " @ %this.memberID[%i]);
 }
}

if(!isObject($TeamDM::TeamManager))
{
$TeamDM::TeamManager = new ScriptObject(TeamDM_TeamManager) {
  class = TeamStackSO;
  teamcount=0;
 };}

////----Server Commands----\\\\
////servercmdTeamsOn()
//Type: Server Command
//Turns Team Deathmatch Mode on if it isn't already.
//Takes: None
//Returns: None
function servercmdTeamsOn(%client)
{
 if(AdminCheck(%client,$Pref::TeamDM::SecureLevel) !$= "1")
 {
  messageclient(%client,'',"\c5You need to be " @ AdminCheck(%client,$Pref::TeamDM::SecureLevel) @ " to start \c3" @ $TeamDM::PackageOn @ "\c5 mode.");
  return;
 }
 if($TeamDM::TeamsOn)
 {
  messageclient(%client,'',"\c3" @ $TeamDM::PackageOn @ "\c5 mode is already on!");
  return;
 }
 $TeamDM::TeamsOn = 1;
 messageallexcept(%client,-1,'',"\c3" @ %client.name @ "\c5 has started \c3" @ $TeamDM::PackageOn @ "\c5 mode!");
 messageclient(%client,'',"\c5You have started \c3" @ $TeamDM::PackageOn @ "\c5 mode.");
 %str = "";
 for(%i=0;%i<$TeamDM::TeamManager.teamCount;%i++)
 {
  %str = %str NL "<color:" @ TDMColorGet($TeamDM::TeamManager.team[%i].color,2) @ ">" @ $TeamDM::TeamManager.team[%i].name;
 }
 for(%i=0;%i<ClientGroup.getCount();%i++)
 {
  %cl = ClientGroup.getObject(%i);
  if(isObject(%cl.tdmTeam) && isObject(%cl.minigame))
  {
   schedule(100,0,respawn,%cl);
  }
 }
 if(isObject($TeamDM::TeamManager.team[0])){messageall('',"\c5Current Teams:" @ %str @ "<color:FF0000>");}else{messageall('',"\c5No teams exist.");}
 $TeamDM::Score = schedule(60000,0,tdmShowScores);
 if(isObject($TeamDM::PackageManager)){$TeamDM::PackageManager.tdmStart();}
}
////servercmdTeamsOff()
//Type: Server Command
//Turns Team Deathmatch Mode off if it isn't already.
//Takes: None
//Returns: None
function servercmdTeamsOff(%client)
{
 if(AdminCheck(%client,$Pref::TeamDM::SecureLevel) !$= "1")
 {
  messageclient(%client,'',"\c5You need to be " @ AdminCheck(%client,$Pref::TeamDM::SecureLevel) @ " to end \c3" @ $TeamDM::PackageOn @ "\c5 mode.");
  return;
 }
 if(!$TeamDM::TeamsOn)
 {
  messageclient(%client,'',"\c3" @ $TeamDM::PackageOn @ "\c5 mode is not enabled.");
  return;
 }
 if($TeamDM::TeamsOn && $TeamDM::AutoSort)
 {
  messageclient(%client,'',"\c5Auto Sort must be turned off to disable \c3" @ $TeamDM::PackageOn @ "\c5 mode.");
  return;
 }
 $TeamDM::TeamsOn = 0;
 for(%i=0;%i<ClientGroup.getCount();%i++)
 {
  %cl = ClientGroup.getObject(%i);
  if(isObject(%cl.tdmTeam) && isObject(%cl.minigame))
  {
   schedule(100,0,respawn,%cl);
  }
 }
 messageallexcept(%client,-1,'',"\c3" @ %client.name @ "\c5 has ended \c3" @ $TeamDM::PackageOn @ "\c5 mode!");
 messageclient(%client,'',"\c5You have ended \c3" @ $TeamDM::PackageOn @ "\c5 mode.");
 cancel($teamDM::score);
 if(isObject($TeamDM::PackageManager)){$TeamDM::PackageManager.tdmEnd();}
}
////servercmdAddTeam()
//Type: Server Command
//Adds a team to the list in Team Deathmatch Mode
//Takes: Name of Team, Color reference (list at top)
//Returns: None (Adds a team)
function servercmdAddTeam(%client,%name,%color)
{
 
 if(AdminCheck(%client,$Pref::TeamDM::SecureLevel) !$= "1")
 {
  messageclient(%client,'',"\c5You need to be " @ AdminCheck(%client,$Pref::TeamDM::SecureLevel) @ " to add teams.");
  return;
 }
 if(%name $= "" || %color $= "")
 {
  messageclient(%client,'',"\c5Usage: \c3/addteam Attackers Blue");
  return;
 }
 if($TeamDM::PackageTeamLock)
 {
  messageclient(%client,'',"\c5The current Package, \c3" @ $TeamDM::PackageOn @ "\c5 has disabled the adding/removing of teams.");
  return;
 }
 if(getWordCount(%name) > 4)
 {
  messageclient(%client,'',"\c5No more than \c3four\c5 words in a team name is allowed."); //By GUI you can type any number
  return;
 }
 if(!TDMColorGet(%color,0))
 {
  %str = "";
  for(%i=1;getWord($TeamDM::ColorList,%i) !$= "";%i++)
  {
   %col = getWord($TeamDM::ColorList,%i);
   %str = %str NL "<color:" @ TDMColorGet(%col,2) @ ">" @ %col;
  }
  messageclient(%client,'',"\c5Color doesn't exist. List:" @ %str);
  return;
 }
 for(%i=0;%i<$TeamDM::TeamManager.teamCount;%i++)
 {
  if($TeamDM::TeamManager.team[%i].name $= %name)
  {
   messageclient(%client,'',"\c5\"<color:" @ $TeamDM::Color[$TeamDM::TeamManager.team[%i].color,1] @ ">" @ $TeamDM::TeamManager.team[%i].name @ "\c5\" already exists!");
   return;
  }
 }
 for(%id=0;$TeamDM::TeamManager.team[%id].serverID !$= "";%id++)
 {
 }
 $TeamDM::TeamManager.addTeam(%id);
 $TeamDM::TeamManager.setTeamName(%id,%name);
 $TeamDM::TeamManager.team[%id].color = %color;
 servercmdTeamUniform(%client,%id,"DEFAULT");
 servercmdLeadUniform(%client,%id,"DEFAULT");
 for(%cl=0;%cl < ClientGroup.getCount();%cl++)
 {
  %cl2 = ClientGroup.getObject(%cl);
  if(%cl2.hasTeamGUI){commandtoclient(%cl2,'TeamInfo',%name,"NONE" SPC "NONE" SPC "NONE" SPC "NONE" SPC "NONE" SPC "Standard_Player");}
 }
 if($TeamDM::TeamsOn)
 {
  tdmmessageallexcept(%client,-1,'',"\c3" @ %client.name @ "\c5 has added a new team: " @ "<color:" @ TDMColorGet(%color,2) @ ">" @ %name);
 }
 messageclient(%client,'',"\c5You have added a new team: " @ "<color:" @ TDMColorGet(%color,2) @ ">" @ %name);
}

////servercmdJoinTeam()
//Type: Server Command
//Allows a client to join a team.
//Takes: ID of client, Name of team
//Returns: None (Edits variables)
function servercmdJoinTeam(%cl,%n1,%n2,%n3,%n4)
{
 if($TeamDM::TeamsLocked)
 {
   messageclient(%cl,'',"\c5Teams have been locked, joining and leaving is disabled.");return;
 }
 %name = "";
 %name = %name @ (%n1 !$= "" ? (%n1) : "");
 %name = %name @ (%n2 !$= "" ? (" " @ %n2) : "");
 %name = %name @ (%n3 !$= "" ? (" " @ %n3) : "");
 %name = %name @ (%n4 !$= "" ? (" " @ %n4) : "");
 %team = -1;
 for(%i=0;%i<$TeamDM::TeamManager.teamCount;%i++)
 { 
  if(%name $= $TeamDM::TeamManager.team[%i].name){%team = %i+1;}
 }
 if(%team !$= "-1")
 {
  if(isObject(%cl.tdmTeam)){%str = "left <color:" @ $TeamDM::Color[%cl.tdmTeam.color,1] @ ">" @ %cl.tdmTeam.name @ "\c5 and joined";}else{%str = "joined";}
  tdmmessageall('',"\c3" @ %cl.name @ "\c5 " @ %str @ " <color:" @ $TeamDM::Color[$TeamDM::TeamManager.team[%team-1].color,1] @ ">" @ $TeamDM::TeamManager.team[%team-1].name @ "\c5.");
  if(isObject(%cl.isLeader))
  {
   %cl.isLeader.leader = "";
   %cl.isLeader = "";
  }  
  if(isObject(%cl.tdmTeam)){%cl.tdmTeam.removeMember(%cl);}
  %cl.tdmTeam = $TeamDM::TeamManager.team[%team-1];
  if($TeamDM::TeamsOn && isObject(%cl.minigame))
  schedule(100,0,respawn,%cl);
  $TeamDM::TeamManager.team[%team-1].addMember(%cl);
 }
 else
 {
  %str = "";
  for(%i=0;%i<$TeamDM::TeamManager.teamCount;%i++)
  {
   %str = %str NL "<color:" @ TDMColorGet($TeamDM::TeamManager.team[%i].color,2) @ ">" @ $TeamDM::TeamManager.team[%i].name;
  }
  messageclient(%cl,'',"\c5That team doesn't exist. Avaliable:" @ %str);
 } 
}

////servercmdLeaveTeam()
//Type: Server Command
//Allows a client to leave all teams
//Takes: ID of client
//Returns: None (Edits variables)
function servercmdLeaveTeam(%client)
{
 if(!isObject(%client.tdmTeam))
 {
  messageclient(%client,'',"\c5You aren't in a team.");return;
 }
 if($TeamDM::TeamsLocked)
 {
   messageclient(%client,'',"\c5Teams have been locked, joining and leaving is disabled.");return;
 }
 tdmmessageall('',"\c3" @ %client.name @ "\c5 left <color:" @ $TeamDM::Color[%client.tdmTeam.color,1] @ ">" @ %client.tdmTeam.name @ "\c5.");
 if(isObject(%client.tdmTeam)){%client.tdmTeam.removeMember(%client);}
 if(isObject(%client.isLeader))
 {
  %client.isLeader.leader = "";
  %client.isLeader = "";
 }
 %client.tdmTeam = "";
 if($TeamDM::TeamsOn && isObject(%client.minigame))
 schedule(100,0,respawn,%client);
}

////servercmdSortTeams()
//Type: Server Command
//Sorts teams between all existing ones, randomly.
//Takes: ID of client
//Returns: None (Edits variables)
function servercmdSortTeams(%client)
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
 for(%j=0;%j<ClientGroup.getCount();%j++)
 {
  %count = (%count + 1) % %i;
  servercmdSetTeamGUI(%client,%count+1,%j);
 }
}
////servercmdtogAutoSort()
//Type: Server Command
//Toggles the "Auto Sort" function
//Takes: ID of client
//Returns: None (Edits variables)
function servercmdtogAutoSort(%client)
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
 if(isObject(%client.minigame) && !isObject($TeamDM::Minigame) && !$TeamDM::AutoSort)
 {
  messageclient(%client,'',"\c5End your Minigame before toggling Auto Sort.");
  return;
 }
 $TeamDM::AutoSort = !$TeamDM::AutoSort;
 messageall('',"\c3Auto Sort\c5 is now " @ ($TeamDM::AutoSort ? "\c2on" : "\c0off") @ "\c5.");
 if(!$TeamDM::AutoSort && isObject($TeamDM::Minigame)){toggleForce(%client,0);}
 if(!$TeamDM::AutoSort && $TeamDM::TeamsOn){servercmdTeamsOff(%client);}
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
    schedule(100,0,respawn,%cl);
    messageallexcept(%cl,-1,'',"\c3" @ %cl.name @ "\c5 was put into <color:" @ $TeamDM::Color[%team.color,1] @ ">" @ %team.name @ "\c5. (Auto Sort)");
    messageclient(%cl,'',"\c5You were put into <color:" @ $TeamDM::Color[%team.color,1] @ ">" @ %team.name @ "\c5. (Auto Sort)");
    }
   }
  }
 }

////servercmdtogUniform()
//Type: Server Command
//Toggles team uniforms
//Takes: ID of client
//Returns: None (Edits variables)
function servercmdtogUniform(%client)
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
 tdmmessageall('',"\c3Team Uniforms\c5 are now " @ ($Pref::TeamDM::Uniform ? "\c2on" : "\c0off") @ "\c5.");
 if(!isObject(%client.minigame)){messageclient(%client,'',"\c3Team Uniforms\c5 are now " @ ($Pref::TeamDM::Uniform ? "\c2on" : "\c0off") @ "\c5.");}
}

////servercmdtogChat()
//Type: Server Command
//Toggles team chat
//Takes: ID of client
//Returns: None (Edits variables)
function servercmdtogChat(%client)
{
 if(AdminCheck(%client,$Pref::TeamDM::SecureLevel) !$= "1")
 {
  messageclient(%client,'',"\c5You need to be " @ AdminCheck(%client,$Pref::TeamDM::SecureLevel) @ " to toggle \c3Team Chat\c5.");
  return;
 }
 $Pref::TeamDM::TeamChat = !$Pref::TeamDM::teamChat;
 tdmmessageall('',"\c3Team Chat\c5 is now " @ ($Pref::TeamDM::TeamChat ? "\c2on" : "\c0off") @ "\c5.");
 if(!isObject(%client.minigame)){messageclient(%client,'',"\c3Team Chat\c5 is now " @ ($Pref::TeamDM::TeamChat ? "\c2on" : "\c0off") @ "\c5.");}
}

////servercmdtogFFire()
//Type: Server Command
//Toggles Friendly Fire in a Team DM
//Takes: ID of client
//Returns: None (Edits variables)
function servercmdtogFFire(%client)
{
 if(AdminCheck(%client,$Pref::TeamDM::SecureLevel) !$= "1")
 {
  messageclient(%client,'',"\c5You need to be " @ AdminCheck(%client,$Pref::TeamDM::SecureLevel) @ " to toggle \c3Friendly Fire\c5.");
  return;
 }
 $Pref::TeamDM::FFire = !$Pref::TeamDM::FFire;
 tdmmessageall('',"\c3Friendly Fire\c5 is now " @ ($Pref::TeamDM::FFire ? "\c2on" : "\c0off") @ "\c5.");
 if(!isObject(%client.minigame)){messageclient(%client,'',"\c3Friendly Fire\c5 is now " @ ($Pref::TeamDM::FFire ? "\c2on" : "\c0off") @ "\c5.");}
}
////servercmdtogTeamLock()
//Type: Server Command
//Locks or Unlocks teams
//Takes: ID of client
//Returns: None (Edits variables)
function servercmdtogTeamLock(%client)
{
 if(AdminCheck(%client,$Pref::TeamDM::SecureLevel) !$= "1")
 {
  messageclient(%client,'',"\c5You need to be " @ AdminCheck(%client,$Pref::TeamDM::SecureLevel) @ " to \c3lock and unlock teams\c5.");
  return;
 }
 if($TeamDM::PackageAutoSortLock)
 {
  messageclient(%client,'',"\c5The current Package, \c3" @ $TeamDM::PackageOn @ "\c5 has disabled the editing of Auto Sort and Lock Teams.");
  return;
 }
 if($TeamDM::AutoSort && $TeamDM::TeamsLocked)
 {
  messageclient(%client,'',"\c3Auto Sort\c5 must be turned off to unlock teams.");
  return;
 }
 $TeamDM::TeamsLocked = !$TeamDM::TeamsLocked;
 tdmmessageall('',"\c3Teams\c5 are now " @ ($TeamDM::TeamsLocked ? "\c0locked" : "\c2unlocked") @ "\c5.");
 if(!isObject(%client.minigame)){messageclient(%client,'',"\c3Teams\c5 are now " @ ($TeamDM::TeamsLocked ? "\c0locked" : "\c2unlocked") @ "\c5.");}
}
////----Server/Client Communication----\\\\
function servercmdconfirmTeamGUIcheck(%client)
{
 %client.hasTeamGUI = 1;
}
//servercmdTDMgetPackages is in the "Package Manager" section
function servercmdTDMgetTeams(%client)
{
 commandtoclient(%client,'GUIset',"listRow","lstTDMteamlist","NONE",0);
 for(%i=0;%i<$TeamDM::TeamManager.teamCount;%i++)
 {
  commandtoclient(%client,'GUIset',"listRow","lstTDMteamlist",$TeamDM::TeamManager.team[%i].name TAB $TeamDM::TeamManager.team[%i].color,%i+1);
 }
}
function servercmdTDMgetPlayers(%client)
{
 for(%i=0;%i<ClientGroup.getCount();%i++)
 {
  %cl = ClientGroup.getObject(%i);
  if(%cl.minigame $= %client.minigame && isObject(%client.minigame)){%mini = "*";}else{%mini = "-";}
  if(isObject(%cl.tdmTeam)){%team = %cl.tdmTeam.name;}else{%team = "-";}
  if(%cl.tdmTeam.leader $= %cl){%lead = "*";}else{%lead = "-";}
  commandtoclient(%client,'GUIset',"listRow","lstTDMplayerlist",%cl.name TAB %mini TAB %team TAB %lead,%i);
 }
}
function servercmdTDMgetColorList(%client)
{
  for(%i=1;getWord($TeamDM::ColorList,%i) !$= "";%i++)
  {
   %col = getWord($TeamDM::ColorList,%i);
   commandtoclient(%client,'GUIset',"popRow","popTDMcolormenu",%i-1,%col);
  }
}

function servercmdTDMgetEnabledList(%client)
{
 commandtoclient(%client,'ButtonSet',butTDMAutoSort,($TeamDM::AutoSort $= 1 ? "2" : "1"));
 commandtoclient(%client,'ButtonSet',butTDMUniform,($Pref::TeamDM::Uniform $= 1 ? "2" : "1"));
 commandtoclient(%client,'ButtonSet',butTDMTeamChat,($Pref::TeamDM::TeamChat $= 1 ? "2" : "1"));
 commandtoclient(%client,'ButtonSet',butTDMFFire,($Pref::TeamDM::FFire $= 1 ? "2" : "1"));
 commandtoclient(%client,'ButtonSet',butTDMLockTeams,($TeamDM::TeamsLocked $= 1 ? "2" : "1"));
 commandtoclient(%client,'ButtonSet',butTDMDefMini,4,(isObject($TeamDM::Minigame) ? "Off" : "On"));
 if($TeamDM::TeamsOn)
 {
  commandtoclient(%client,'ButtonSet',butTDMEnd,3);
  commandtoclient(%client,'ButtonSet',butTDMStart,0);
 }
 else
 {
  commandtoclient(%client,'ButtonSet',butTDMEnd,0);
  commandtoclient(%client,'ButtonSet',butTDMStart,3);
 }
}

////----Remote Upload Save File----\\\\

function servercmdinitTDMupload(%client,%filename)
{
 echo("----Upload Request from: " @ %client.name);
 if(AdminCheck(%client,$Pref::TeamDM::SecureLevel) !$= "1")
 {
  messageclient(%client,'',"\c5You need to be " @ AdminCheck(%client,$Pref::TeamDM::SecureLevel) @ " to \c3upload saves\c5.");
  echo("--Rejected: Low Rank");
  return;
 }
 if($TeamDM::Uploading)
 {
  messageclient(%client,'',"Someone else is already uploading a file.");
  echo("--Rejected: In progress load");
  return;
 }
 if(!%client.tdmspawned)
 {
  messageclient(%client,'',"You need to have spawned in the server to upload files.");
  echo("--Rejected: Not spawned");
  return;
 }
 echo("--Accepted");
 messageall('',"\c3" @ %client.name @ "\c5 is uploading a TDM save...");
 $TeamDM::Uploading = 1;
 %file = new FileObject();
 %file.openforWrite("Add-Ons/TeamDMv4/Saves/temp.Tsave");%file.close();%file.delete();
 commandtoclient(%client,'TDM_Load_Confirm',%filename);
}

function servercmdTDMuploadline(%client,%filename,%line,%curline,%numlines)
{
 if(AdminCheck(%client,$Pref::TeamDM::SecureLevel) !$= "1")
 {
  messageclient(%client,'',"\c5You need to be " @ AdminCheck(%client,$Pref::TeamDM::SecureLevel) @ " to \c3upload saves\c5.");
  return;
 }
 if(!%client.tdmspawned)
 {
  messageclient(%client,'',"\c5You need to have spawned in the server to upload files.");
  return;
 }
 %open = new FileObject();
 %open.openforAppend("Add-Ons/TeamDMv4/Saves/temp.Tsave");
 %open.writeline(%line);
 %open.close();
 %open.delete();
 if(%curline < %numlines)
 {
  commandtoclient(%client,'TDMuploadlineconf',%filename,%curline++,%numlines);
 }
 else
 {
  echo("--Upload Finished");
  loadTDMSave(%client,"Add-Ons/TeamDMv4/Saves/temp.Tsave");
 }
}

function loadTDMSave(%client,%file)
{
 for(%i=$TeamDM::TeamManager.teamCount-1;%i>-1;%i--)
 {
  %team = $TeamDM::TeamManager.team[%i];
  if(isObject(%team)){
  %team.leader.isLeader = "";
  $TeamDM::TeamManager.removeTeam(%i);}
 }
 %origbrick = %cl.player.tempbrick;
 %open = new FileObject();
 %open.openforRead(%file);
 while(!%open.isEOF())
 {
  %line = %open.readline();
  switch$(firstWord(%line))
  {
   case "//": //Do nothing
   case "TEAM": servercmdaddTeam(%client,getWords(%line,1,getWordCount(%line)-2),getWord(%line,getWordCount(%line)-1));
   case "INFO": %team = $TeamDM::TeamManager.team[$TeamDM::TeamManager.teamcount-1];
    %ui[0] = strReplace(getWord(%line,1),"_"," ");
    %ui[1] = strReplace(getWord(%line,2),"_"," ");
    %ui[2] = strReplace(getWord(%line,3),"_"," ");
    %ui[3] = strReplace(getWord(%line,4),"_"," ");
    %ui[4] = strReplace(getWord(%line,5),"_"," ");
    %pl = strReplace(getWord(%line,6),"_"," ");
    uiTable();
    for(%i=0;%i<5;%i++){if(%ui[%i] !$= "NONE"){%team.tool[%i] = $WrenchUITable[%ui[%i]];}else{%team.tool[%i] = -1;}}
    if(%pl !$= "Standard Player"){%team.data = $PlayerDataUITable[%pl];}else{%team.data = PlayerStandardArmor;}
    if(!$TeamDM::NoSendUpdates)
    {
     for(%cl=0;%cl < ClientGroup.getCount();%cl++)
     {
      %cl2 = ClientGroup.getObject(%cl);
      if(%cl2.hasTeamGUI){commandtoclient(%cl2,'TeamInfo',%team.name,
						strReplace(%ui[0]," ","_") SPC 
						strReplace(%ui[1]," ","_") SPC 
						strReplace(%ui[2]," ","_") SPC 
						strReplace(%ui[3]," ","_") SPC 
						strReplace(%ui[4]," ","_") SPC 
						strReplace(%pl," ","_"));
	commandtoclient(%cl2,'clearlists');
 	servercmdTDMgetPackages(%cl2);
 	servercmdTDMgetTeams(%cl2);
 	servercmdTDMgetPlayers(%cl2);
 	servercmdTDMgetColorList(%cl2);
 	servercmdTDMgetEnabledList(%cl2);
     }
    }
   }
   case "BRICK":  schedule(1000,0,loadbrick,%client,%line);%bricknum++;
   case "UNI": $TeamDM::TeamManager.team[$TeamDM::TeamManager.teamcount-1].set[getWord(%line,1)] = getWords(%line,2,8);
    for(%cl=0;%cl < ClientGroup.getCount();%cl++)
    {
     %cl2 = ClientGroup.getObject(%cl);
     if(%cl2.hasTeamGUI)
     {
       commandtoclient(%cl2,'TeamUniformSet',$TeamDM::TeamManager.team[$TeamDM::TeamManager.teamcount-1].name,getWord(%line,1),$TeamDM::TeamManager.team[$TeamDM::TeamManager.teamcount-1].set[getWord(%line,1)]);
     }
    }
  }
 }
 if($TeamDM::AutoSort)
 {
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
    tdmmessageallexcept(%cl,-1,'',"\c3" @ %cl.name @ "\c5 was put into <color:" @ $TeamDM::Color[%team.color,1] @ ">" @ %team.name @ "\c5. (Auto Sort)");
    messageclient(%cl,'',"\c5You were put into <color:" @ $TeamDM::Color[%team.color,1] @ ">" @ %team.name @ "\c5. (Auto Sort)");
    }
   }
  }
 schedule(%bricknum * 310,0,eval," $TeamDM::Uploading = 0;  messageall(\'\',\"\c5Upload complete.\");echo(\"--Brick Load Finished\");");
}

function loadbrick(%client,%line)
{
for(%i=0;%i<MainBrickGroup.getCount();%i++)
{
 for(%j=0;%j<MainBrickGroup.getObject(%i).getCount();%j++)
 {
  if(vectorDist(MainBrickGroup.getObject(%i).getObject(%j).position,getWords(%line,1,3)) < 0.5)
  {
   schedule(300,0,setbrickteam,MainBrickGroup.getObject(%i).getObject(%j),getWords(%line,8));
   return;
  }
 }
}
%a = new FXDtsBrick(){datablock = brickTDMSpawnData;position = getWords(%line,1,3);tdmteam = RGBtoTeam(getWords(%line,9));};
%a.setTransform(getWords(%line,1,7));
%a.angleID = getWord(%line,8);
    schedule(200,0,fakeplant,%a,%client.player,%origbrick,0,0,%line);
}

 function fakeplant(%obj,%player,%origbrick,%col,%fx,%line)
 {
  //%trans = %player.getTransform();
  //%player.setTransform(vectorAdd(%obj.getTransform(),"1.5 1.5 1.5"));
  %player.tempbrick = %obj;
  %obj.setTrusted(1);
  if(%obj.plant()){%obj.delete();return;}
  //servercmdPlantBrick(%client);
  //%player.tempbrick.delete();
  %obj.setColorFX(%fx);
  %player.tempbrick = %origbrick;
  //%player.setTransform(%trans);
     %player.client.brickGroup.add(%obj);
     schedule(100,0,setbrickteam,%obj,getWords(%line,9));
 }

 function RGBtoTeam(%rgb)
 {
  %dist = 10000;
  %closest = -1;
  for(%i=0;%i<$TeamDM::TeamManager.teamCount;%i++)
  {
   if(vectorDist(getWords(%rgb,0,2),$TeamDM::Color[$TeamDM::TeamManager.team[%i].color,0]) < %dist)
   {
    %closest = $TeamDM::TeamManager.team[%i];
    %dist = vectorDist(%rgb,$TeamDM::Color[$TeamDM::TeamManager.team[%i].color,0]);
   }
  }
  return %closest;
 }

 function setBrickTeam(%brick,%col)
 {
  %team = RGBtoTeam(%col);
  for(%i=0;%i<$TeamDM::TeamManager.teamCount;%i++)
  {
   if($TeamDM::TeamManager.team[%i] $= %team){%brick.tdmteam = %i;}
  }
  %team = $TeamDM::TeamManager.team[%brick.tdmteam];
  if(isObject(%team)){%team.spawnlist = %team.spawnlist @ %brick @ " ";}
  %brick.setColor(rgbtopaint($TeamDM::Color[%team.color,0] SPC "1"));
 }

////----GUI Server Commands----\\\\
////servercmdSetTeamGUI()
//Type: Server Command
//Puts a certain player in a specific team. (Uses GUI)
//Takes: ClientGroup ID of player, TeamManager ID of team
//Returns: None (Edits variables)
function servercmdSetTeamGUI(%client,%team,%player)
{
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
 %cl = ClientGroup.getObject(%player);
 if(%team !$= "0")
 {
  if(isObject(%cl.tdmTeam)){%str = "moved to";}else{%str = "put into";}
  tdmmessageallexcept(%cl,-1,'',"\c3" @ %cl.name @ "\c5 was " @ %str @ " <color:" @ $TeamDM::Color[$TeamDM::TeamManager.team[%team-1].color,1] @ ">" @ $TeamDM::TeamManager.team[%team-1].name @ "\c5.");
  messageclient(%cl,'',"\c5You were " @ %str @ " <color:" @ $TeamDM::Color[$TeamDM::TeamManager.team[%team-1].color,1] @ ">" @ $TeamDM::TeamManager.team[%team-1].name @ "\c5.");
  if(!isObject(%client.minigame)){messageclient(%client,'',"\c3" @ %cl.name @ "\c5 was " @ %str @ " <color:" @ $TeamDM::Color[$TeamDM::TeamManager.team[%team-1].color,1] @ ">" @ $TeamDM::TeamManager.team[%team-1].name @ "\c5.");}
  if(isObject(%cl.isLeader))
  {
   %cl.isLeader.leader = "";
   %cl.isLeader = "";
  }
  if(isObject(%cl.tdmTeam)){%cl.tdmTeam.removeMember(%cl);}
  if($TeamDM::TeamsOn && isObject(%cl.minigame)){schedule(100,0,respawn,%cl);}
  %cl.tdmTeam = $TeamDM::TeamManager.team[%team-1];
  $TeamDM::TeamManager.team[%team-1].addMember(%cl);
 }
 else
 {
  if(!isObject(%cl.tdmTeam)){messageclient(%client,'',"\c3" @ %cl.name @ "\c5 isn't in a team.");return;}
  messageallexcept(%cl,-1,'',"\c3" @ %cl.name @ "\c5 was removed from <color:" @ $TeamDM::Color[%cl.tdmTeam.color,1] @ ">" @ %cl.tdmTeam.name @ "\c5.");
  messageclient(%cl,'',"\c5You were removed from <color:" @ $TeamDM::Color[%cl.tdmTeam.color,1] @ ">" @ %cl.tdmTeam.name @ "\c5.");
  if(isObject(%cl.isLeader))
  {
   %cl.isLeader.leader = "";
   %cl.isLeader = "";
  }
  %cl.tdmTeam.removeMember(%cl);
  %cl.tdmTeam = "";
  if($TeamDM::TeamsOn && isObject(%cl.minigame)){schedule(100,0,respawn,%cl);}
 } 
}

////servercmdSetLeadGUI()
//Type: Server Command
//Puts a certain player in a specific team. (Uses GUI)
//Takes: ClientGroup ID of player, TeamManager ID of team
//Returns: None (Edits variables)
function servercmdSetLeadGUI(%client,%team,%player)
{
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
 %cl = ClientGroup.getObject(%player);
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

////servercmdEditTeam()
//Type: Server Command
//Checks whether the client is allowed to edit teams and sends a command back opening GUI and sending info
//Takes: Client ID, Team Manager Team ID
//Returns: None / Client commands
function servercmdEditTeam(%client,%team)
{
 if(AdminCheck(%client,$Pref::TeamDM::SecureLevel) !$= "1")
 {
  messageclient(%client,'',"\c5You need to be " @ AdminCheck(%client,$Pref::TeamDM::SecureLevel) @ " \c5to edit teams.");
  return;
 }
 if(!isObject($TeamDM::TeamManager.team[%team-1]))
 {
  messageclient(%client,'',"That team doesn't exist.");
  return;
 }
 uiTable();
 for(%i=0;%i<5;%i++){if(isObject($TeamDM::TeamManager.team[%team-1].tool[%i])){%ui[%i] = $TeamDM::TeamManager.team[%team-1].tool[%i].uiname;}else{%ui[%i] = "NONE";}}
 if(isObject($TeamDM::TeamManager.team[%team-1].data)){%pl = $TeamDM::TeamManager.team[%team-1].data.uiname;}else{%pl = "Standard Player";}
 commandtoclient(%client,'openteamEdit',$TeamDM::TeamManager.team[%team-1].name,%team-1,%ui0,%ui1,%ui2,%ui3,%ui4,%pl);
}

////servercmdEditTeam2()
//Type: Server Command
//Checks whether the client is allowed to edit teams and changes team settings
//Takes: Client ID, Team Manager Team ID, Name text, UI name texts (Items + PlayerData)
//Returns: None / Variable Setting
function servercmdeditTeam2(%client,%team,%name,%ui0,%ui1,%ui2,%ui3,%ui4,%pl)
{
 if(AdminCheck(%client,$Pref::TeamDM::SecureLevel) !$= "1")
 {
  messageclient(%client,'',"\c5You need to be " @ AdminCheck(%client,$Pref::TeamDM::SecureLevel) @ " \c5to edit teams.");
  return;
 }
 if(!isObject($TeamDM::TeamManager.team[%team]))
 {
  messageclient(%client,'',"\c5The team you are editing doesn't exist!");
  return;
 }
 %team = $TeamDM::TeamManager.team[%team];
 if(%name !$= %team.name)
 {
   tdmmessageall('',"<color:" @ $TeamDM::Color[%team.color,1] @ ">" @ %team.name @ "\c5 is now known as <color:" @ $TeamDM::Color[%team.color,1] @ ">" @ %name @ "\c5.");
   %team.name = %name;
 }
 uiTable();
 for(%i=0;%i<5;%i++){if(%ui[%i] !$= "NONE"){%team.tool[%i] = $WrenchUITable[%ui[%i]];}else{%team.tool[%i] = -1;}}
 if(%pl !$= "Standard Player"){%team.data = $PlayerDataUITable[%pl];}else{%team.data = PlayerStandardArmor;}
 for(%i=0;%i<%team.memberCount;%i++)
 {
  itemSet(%team.memberID[%i]);
 }
 commandtoall('TeamInfo',%team.name,strReplace(%ui0," ","_") SPC strReplace(%ui1," ","_") SPC strReplace(%ui2," ","_")SPC strReplace(%ui3," ","_")SPC strReplace(%ui4," ","_") SPC strReplace(%pl," ","_"));
}

////servercmdtdmHelp()
//Type: Server Command
//Sends the client a "default" description if a package is not on or the Description of it set in the file
//Takes: Client ID
//Returns: None
function servercmdtdmHelp(%client)
{
 messageclient(%client,'',"\c3" @ $teamdm::packageon);
 if($teamdm::packageon $= "Team Deathmatch")
 {
  messageclient(%client,'',"\c5" @ $Pref::TeamDM::DefaultDesc);
 }
 else
 {
  messageclient(%client,'',"\c5" @ $TeamDM::PackageDesc[$teamDM::PackageNumber[$TeamDM::PackageOn]]);
 }
}

function newTeam(%name,%color)
{
 if(!TDMColorGet(%color,0))
 {
  %str = "";
  error("ERROR: newTeam(" @ %name @ "," @ %color @ "); - Color doesn't exist!");
  return;
 }
 for(%i=0;%i<$TeamDM::TeamManager.teamCount;%i++)
 {
  if($TeamDM::TeamManager.team[%i].name $= %name)
  {
   error("ERROR: newTeam(" @ %name @ "," @ %color @ "); - Name already exists!");
   return;
  }
 }
 for(%id=0;$TeamDM::TeamManager.team[%id].serverID !$= "";%id++)
 {
 }
 $TeamDM::TeamManager.addTeam(%id);
 $TeamDM::TeamManager.setTeamName(%id,%name);
 $TeamDM::TeamManager.team[%id].color = %color;
 servercmdTeamUniform(-1,%id,"DEFAULT");
 servercmdLeadUniform(-1,%id,"DEFAULT");
 for(%cl=0;%cl < ClientGroup.getCount();%cl++)
 {
  %cl2 = ClientGroup.getObject(%cl);
  if(%cl2.hasTeamGUI){commandtoclient(%cl2,'TeamInfo',%name,"NONE" SPC "NONE" SPC "NONE" SPC "NONE" SPC "NONE" SPC "Standard_Player");}
 }
 tdmmessageall('',"\c5A new team has been added: " @ "<color:" @ TDMColorGet(%color,2) @ ">" @ %name);
}

////servercmdteamUniform()
//Type: Server Command
//Either sets the team (general player) uniform to the default or client's current appearance
//Takes: Client ID, String: Default or Current
//Returns: None / Variable Setting
function servercmdTeamUniform(%client,%team,%arg)
{
 if(isObject(%client) && AdminCheck(%client,$Pref::TeamDM::SecureLevel) !$= "1")
 {
  messageclient(%client,'',"\c5You need to be " @ AdminCheck(%client,$Pref::TeamDM::SecureLevel) @ " \c5to edit teams.");
  return;
 }
 if(!isObject($TeamDM::TeamManager.team[%team]))
 {
  messageclient(%client,'',"\c5The team you are editing doesn't exist!");
  return;
 }
 %team = $TeamDM::TeamManager.team[%team];
 switch$(%arg)
 {
  case "CURRENT":
   %team.setaccent = %client.accent;
   %team.setaccentColor = %client.accentColor;
   %team.setchest = %client.chest;
   %team.setchestColor = %client.chestColor;
   %team.setdecalName = %client.decalName;
   %team.setfaceName = %client.faceName;
   %team.sethat = %client.hat;
   %team.sethatColor = %client.hatColor;
   %team.setlhand = %client.lhand;
   %team.setlhandcolor = %client.lhandcolor;
   %team.setrhand = %client.rhand;
   %team.setrhandcolor = %client.rhandcolor;
   %team.setlarm = %client.larm;
   %team.setlarmcolor = %client.larmcolor;
   %team.setrarm = %client.rarm;
   %team.setrarmcolor = %client.rarmcolor;
   %team.setlleg = %client.lleg;
   %team.setllegcolor = %client.llegcolor;
   %team.setrleg = %client.rleg;
   %team.setrlegcolor = %client.rlegcolor;
   %team.setpack = %client.pack;
   %team.setpackColor = %client.packColor;
   %team.setsecondpack = %client.secondpack;
   %team.setsecondpackColor = %client.secondpackcolor;
   %team.setheadColor = %client.headColor;
   %team.sethip = %client.hip;
   %team.sethipcolor = %client.hipcolor;
  case "DEFAULT":
   %team.setaccent = 1;
   %team.setaccentColor = "0 0.2 0.64 0.7";
   %team.setchest = 0;
   %team.setchestColor = $TeamDM::Color[%team.color,0] SPC 1;
   %team.setdecalName = "AAA-None";
   %team.setfaceName = "smileyCreepy";
   %team.sethat = 1;
   %team.sethatColor = $TeamDM::Color[%team.color,0] SPC 1;
   %team.setlhand = 0;
   %team.setlhandcolor = "0.2 0.2 0.2 1";
   %team.setrhand = 0;
   %team.setrhandcolor = "0.2 0.2 0.2 1";
   %team.setlarm = 0;
   %team.setlarmcolor = $TeamDM::Color[%team.color,0] SPC 1;
   %team.setrarm = 0;
   %team.setrarmcolor = $TeamDM::Color[%team.color,0] SPC 1;
   %team.setlleg = 0;
   %team.setllegcolor = $TeamDM::Color[%team.color,0] SPC 1;
   %team.setrleg = 0;
   %team.setrlegcolor = $TeamDM::Color[%team.color,0] SPC 1;
   %team.setpack = 0;
   %team.setpackColor = "0.2 0.2 0.2 1";
   %team.setsecondpack = 0;
   %team.setsecondpackColor = "0.9 0.9 0.0 1";
   %team.setheadColor = "1 0.878 0.611 1";
   %team.sethip = 0;
   %team.sethipcolor = "0.2 0.2 0.2 1";
 }
 for(%cl=0;%cl < ClientGroup.getCount();%cl++)
 {
  %cl2 = ClientGroup.getObject(%cl);
  if(%cl2.hasTeamGUI)
  {
   //The reason I didn't use just __ and __color auto is because of the strange ones like decalName, faceName and headColor.
   %list = "accent accentColor chest chestColor decalName faceName hat hatColor lhand lhandColor rhand rhandColor larm larmcolor rarm rarmcolor lleg llegcolor rleg rlegColor pack packColor secondpack";
   %list = %list SPC "secondPackColor headColor hip hipColor";
   for(%j=0;%j<getWordCount(%list);%j++)
   {
    commandtoclient(%cl2,'TeamUniformSet',%team.name,getWord(%list,%j),%team.set[getWord(%list,%j)]);
   }
  }
 }
}

////servercmdleadUniform()
//Type: Server Command
//Either sets the team leader uniform to the default or client's current appearance
//Takes: Client ID, String: Default or Current
//Returns: None / Variable Setting
function servercmdLeadUniform(%client,%team,%arg)
{
 if(isObject(%client) && AdminCheck(%client,$Pref::TeamDM::SecureLevel) !$= "1")
 {
  messageclient(%client,'',"\c5You need to be " @ AdminCheck(%client,$Pref::TeamDM::SecureLevel) @ " \c5to edit teams.");
  return;
 }
 if(!isObject($TeamDM::TeamManager.team[%team]))
 {
  messageclient(%client,'',"\c5The team you are editing doesn't exist!");
  return;
 }
 %team = $TeamDM::TeamManager.team[%team];

 switch$(%arg)
 {
  case "CURRENT":
   %team.setLEADaccent = %client.accent;
   %team.setLEADaccentColor = %client.accentColor;
   %team.setLEADchest = %client.chest;
   %team.setLEADchestColor = %client.chestColor;
   %team.setLEADdecalName = %client.decalName;
   %team.setLEADfaceName = %client.faceName;
   %team.setLEADhat = %client.hat;
   %team.setLEADhatColor = %client.hatColor;
   %team.setLEADlhand = %client.lhand;
   %team.setLEADlhandcolor = %client.lhandcolor;
   %team.setLEADrhand = %client.rhand;
   %team.setLEADrhandcolor = %client.rhandcolor;
   %team.setLEADlarm = %client.larm;
   %team.setLEADlarmcolor = %client.larmcolor;
   %team.setLEADrarm = %client.rarm;
   %team.setLEADrarmcolor = %client.rarmcolor;
   %team.setLEADlleg = %client.lleg;
   %team.setLEADllegcolor = %client.llegcolor;
   %team.setLEADrleg = %client.rleg;
   %team.setLEADrlegcolor = %client.rlegcolor;
   %team.setLEADpack = %client.pack;
   %team.setLEADpackColor = %client.packColor;
   %team.setLEADsecondpack = %client.secondpack;
   %team.setLEADsecondpackColor = %client.secondpackcolor;
   %team.setLEADheadColor = %client.headColor;
   %team.setLEADhip = %client.hip;
   %team.setLEADhipcolor = %client.hipcolor;
  case "DEFAULT":
   %team.setLEADaccent = 1;
   %team.setLEADaccentColor = "0 0.2 0.64 0.7";
   %team.setLEADchest = 0;
   %team.setLEADchestColor = $TeamDM::Color[%team.color,0] SPC 1;
   %team.setLEADdecalName = "Mod-Daredevil";
   %team.setLEADfaceName = "smileyCreepy";
   %team.setLEADhat = 1;
   %team.setLEADhatColor = $TeamDM::Color[%team.color,0] SPC 1;
   %team.setLEADlhand = 0;
   %team.setLEADlhandcolor = "0.2 0.2 0.2 1";
   %team.setLEADrhand = 0;
   %team.setLEADrhandcolor = "0.2 0.2 0.2 1";
   %team.setLEADlarm = 0;
   %team.setLEADlarmcolor = $TeamDM::Color[%team.color,0] SPC 1;
   %team.setLEADrarm = 0;
   %team.setLEADrarmcolor = $TeamDM::Color[%team.color,0] SPC 1;
   %team.setLEADlleg = 0;
   %team.setLEADllegcolor = $TeamDM::Color[%team.color,0] SPC 1;
   %team.setLEADrleg = 0;
   %team.setLEADrlegcolor = $TeamDM::Color[%team.color,0] SPC 1;
   %team.setLEADpack = 0;
   %team.setLEADpackColor = "0.9 0.9 0.0 1";
   %team.setLEADsecondpack = 3;
   %team.setLEADsecondpackColor = "0.9 0.9 0.0 1";
   %team.setLEADheadColor = "1 0.878 0.611 1";
   %team.setLEADhip = 0;
   %team.setLEADhipcolor = "0.2 0.2 0.2 1";
 }
 for(%cl=0;%cl < ClientGroup.getCount();%cl++)
 {
  %cl2 = ClientGroup.getObject(%cl);
  if(%cl2.hasTeamGUI)
  {
   //The reason I didn't use just __ and __color auto is because of the strange ones like decalName, faceName and headColor.
   %list = "accent accentColor chest chestColor decalName faceName hat hatColor lhand lhandColor rhand rhandColor larm larmcolor rarm rarmcolor lleg llegcolor rleg rlegColor pack packColor secondpack";
   %list = %list SPC "secondPackColor headColor hip hipColor";
   for(%j=0;%j<getWordCount(%list);%j++)
   {
    commandtoclient(%cl2,'TeamUniformSet',%team.name,"LEAD" @ getWord(%list,%j),%team.setLEAD[getWord(%list,%j)]);
   }
  }
 }
}

////servercmdteamdisband()
//Type: Server Command
//Deletes a team
//Takes: Client ID, ID of team
//Returns: None / Variable Setting
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
    schedule(100,0,respawn,%cl);
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

////servercmdclearteams()
//Type: Server Command
//Deletes all teams
//Takes: Client ID
//Returns: None / Variable Setting
function servercmdClearTeams(%client)
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
 tdmmessageall('',"\c3" @ %client.name @ " \c5cleared all teams.");
 clearTeams();
}

function clearTeams()
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
    schedule(100,0,respawn,%cl);
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

////servercmdtoggleTeamMini()
//Type: Server Command
//Starts or ends a default minigame
//Takes: Client ID
//Returns: None / Variable Setting
function servercmdToggleTeamMini(%client)
{
 if(AdminCheck(%client,$Pref::TeamDM::SecureLevel) !$= "1")
 {
  messageclient(%client,'',"\c5You need to be " @ AdminCheck(%client,$Pref::TeamDM::SecureLevel) @ " \c5to toggle the \c3Default Minigame\c5.");
  return;
 }
 if($TeamDM::AutoSort)
 {
  messageclient(%client,'',"\c3Auto Sort \c5must be turned off to toggle the \c3Default Minigame\c5.");
  return;
 }
 if(isObject(%client.minigame) && !isObject($TeamDM::Minigame) && !$TeamDM::ForceJoin)
 {
  messageclient(%client,'',"\c5End your Minigame before toggling Auto Sort.");
  return;
 }
 %on = !$TeamDM::ForceJoin;
 toggleForce(%client,%on);
}

////----Support Functions----\\\\
////TDMColorGet()
//Type: Support, single file
//Takes a color reference and returns whether it exists, an RGB code or a Hex code
//Takes: Color name, return type
//Returns: "whether it exists, an RGB code or a Hex code"
function TDMColorGet(%color,%return)
{
 switch(%return)
 {
  case 0: //Is Color
   return (strStr(strLwr($TeamDM::ColorList),strLwr(%color)) !$= "-1");
  case 1: //RGB
   return $TeamDM::Color[%color,0];
  case 2: //Hex
   return $TeamDM::Color[%color,1];
 }
 return 0;
}

////Uniform()
//Type: Support, single file
//Applies a set Team DM uniform to a player
//Takes: Player ID, Team ID
//Returns: "whether it exists, an RGB code or a Hex code"
function Uniform(%player,%team)
{
 if(!isObject(%player) || !isObject(%team)){return;}
 %lead = (%team.leader $= %player.client);
 hideAllNodes(%player);
 if(%lead) //Ugh, %a["String"]b doesn't work?
 {
  if(%player.getDatablock().shapefile !$= "base/data/shapes/player/m.dts"){%player.setNodeColor("body",%team.setLEADchestcolor);return;}
  if($accent[%team.setLEADaccent] !$= "none")
  {
   if($hat[%team.setLEADhat] !$= "helmet")
   {
     %player.unhidenode($accent[%team.setLEADaccent]);
     %player.setnodecolor($accent[%team.setLEADaccent],%team.setLEADaccentcolor);
   }
   else
   {
     %player.unhidenode(visor);
     %player.setnodecolor(visor,%team.setLEADaccentcolor);
   }
  }
 %player.unhidenode($chest[%team.setLEADchest]);
  %player.setnodecolor($chest[%team.setLEADchest],%team.setLEADchestcolor);
 if($hat[%team.setLEADhat] !$= "none")
 {
 %player.unhidenode($hat[%team.setLEADhat]);
  %player.setnodecolor($hat[%team.setLEADhat],%team.setLEADhatcolor);
 }
 %player.unhidenode($lleg[%team.setLEADlleg]);
  %player.setnodecolor($lleg[%team.setLEADlleg],%team.setLEADllegcolor);
 %player.unhidenode($rleg[%team.setLEADrleg]);
  %player.setnodecolor($rleg[%team.setLEADrleg],%team.setLEADrlegcolor);
 %player.unhidenode($lhand[%team.setLEADlhand]);
  %player.setnodecolor($lhand[%team.setLEADlhand],%team.setLEADlhandcolor);
 %player.unhidenode($rhand[%team.setLEADrhand]);
  %player.setnodecolor($rhand[%team.setLEADrhand],%team.setLEADrhandcolor);
 %player.unhidenode($larm[%team.setLEADlarm]);
  %player.setnodecolor($larm[%team.setLEADlarm],%team.setLEADlarmcolor);
 %player.unhidenode($rarm[%team.setLEADrarm]);
  %player.setnodecolor($rarm[%team.setLEADrarm],%team.setLEADrarmcolor);
 if($pack[%team.setLEADpack] !$= "none"){
 %player.unhidenode($pack[%team.setLEADpack]);
  %player.setnodecolor($pack[%team.setLEADpack],%team.setLEADpackcolor);}
 if($secondpack[%team.setLEADsecondpack] !$= "none"){
 %player.unhidenode($secondpack[%team.setLEADsecondpack]);
  %player.setnodecolor($secondpack[%team.setLEADsecondpack],%team.setLEADsecondpackcolor);}
 if(%team.setLEADhip == 0)
 {
  %player.unhidenode("pants");%player.setnodecolor("pants",%team.setLEADhipcolor);
 }
 else
 {
  %player.unhidenode("skirthip");%player.setnodecolor("skirthip",%team.setLEADhipcolor);
  %player.unhideNode("skirtTrimLeft");%player.setnodecolor("skirttrimleft",%team.setLEADllegcolor);
  %player.unhideNode("skirtTrimRight");%player.setnodecolor("skirttrimright",%team.setLEADrlegcolor);
 }
 %player.unhidenode(headSkin);
  %player.setnodecolor(headSkin,%team.setLEADheadcolor);
 %player.setFaceName(%team.setLEADfacename);
 %player.setDecalName(%team.setLEADdecalname);
 }
 else
 {
  if(%player.getDatablock().shapefile !$= "base/data/shapes/player/m.dts"){%player.setNodeColor("body",%team.setchestcolor);return;}
  if($accent[%team.setaccent] !$= "none")
  {
   if($hat[%team.sethat] !$= "helmet")
   {
     %player.unhidenode($accent[%team.setaccent]);
     %player.setnodecolor($accent[%team.setaccent],%team.setaccentcolor);
   }
   else
   {
     %player.unhidenode(visor);
     %player.setnodecolor(visor,%team.setaccentcolor);
   }
  }
 %player.unhidenode($chest[%team.setchest]);
  %player.setnodecolor($chest[%team.setchest],%team.setchestcolor);
 if($hat[%team.sethat] !$= "none")
 {
  %player.unhidenode($hat[%team.sethat]);
  %player.setnodecolor($hat[%team.sethat],%team.sethatcolor);
 }
 %player.unhidenode($lleg[%team.setlleg]);
  %player.setnodecolor($lleg[%team.setlleg],%team.setllegcolor);
 %player.unhidenode($rleg[%team.setrleg]);
  %player.setnodecolor($rleg[%team.setrleg],%team.setrlegcolor);
 %player.unhidenode($lhand[%team.setlhand]);
  %player.setnodecolor($lhand[%team.setlhand],%team.setlhandcolor);
 %player.unhidenode($rhand[%team.setrhand]);
  %player.setnodecolor($rhand[%team.setrhand],%team.setrhandcolor);
 %player.unhidenode($larm[%team.setlarm]);
  %player.setnodecolor($larm[%team.setlarm],%team.setlarmcolor);
 %player.unhidenode($rarm[%team.setrarm]);
  %player.setnodecolor($rarm[%team.setrarm],%team.setrarmcolor);
 if($pack[%team.setpack] !$= "none"){
 %player.unhidenode($pack[%team.setpack]);
  %player.setnodecolor($pack[%team.setpack],%team.setpackcolor);}
 if($secondpack[%team.setsecondpack] !$= "none"){
 %player.unhidenode($secondpack[%team.setsecondpack]);
  %player.setnodecolor($secondpack[%team.setsecondpack],%team.setsecondpackcolor);}
 if(%team.sethip == 0)
 {
  %player.unhidenode("pants");%player.setnodecolor("pants",%team.sethipcolor);
 }
 else
 {
  %player.unhidenode("skirthip");%player.setnodecolor("skirthip",%team.sethipcolor);
  %player.unhideNode("skirtTrimLeft");%player.setnodecolor("skirttrimleft",%team.setllegcolor);
  %player.unhideNode("skirtTrimRight");%player.setnodecolor("skirttrimright",%team.setrlegcolor);
 }
 %player.unhidenode(headSkin);
  %player.setnodecolor(headSkin,%team.setheadcolor);
 %player.setFaceName(%team.setfacename);
 %player.setDecalName(%team.setdecalname);
 }
}

////AdminCheck()
//Type: Support, single file
//Returns whether the client's "rank" is above the level specified (Admin, Super, Host) and returns whether it is ("1") or what level you need to be ("a Super Admin")
//Takes: Client ID, Rank level
//Returns: "1" or text error
function AdminCheck(%client,%level)
{
 if(%client $= "-1"){return 1;}
 if(!%client.isAdmin && !%client.isSuperAdmin && %level == 1){return "\c5an \c3Admin\c5";}
 if(!%client.isSuperAdmin && %level == 2){return "\c5a \c3Super Admin\c5";}
 if(%client !$= FindLocalClient() && %level !$= "1" && %level !$= "2"){return "\c5the \c3Server Host\c5";}
 return "1";
}

////uiTable()
//Type: Support, single file
//Creates a Wrench and Player Datablock UI name table on the server.
//Takes: None
//Returns: None (Variable Editing)
function uiTable()
{
  %group = datablockGroup;
  %count = 0;
  %count2 = 0;
  for(%i=0;%i<%group.getCount();%i++)
  {
   %test = %group.getObject(%i);
   if(%test.getClassName() $= "ItemData" && %test.uiname !$= "")
   {
    $WrenchUITable[%test.uiname] = %test;
    %count++;
   }
   if(%test.getClassName() $= "PlayerData" && %test.uiname !$= "")
   {
    $PlayerDataUITable[%test.uiname] = %test;
    %count2++;
   }
  }
  $PlayerDataUITable["Standard Player"] = PlayerStandardArmor;
  for(%i=0;%i<5;%i++){%team.tool[%i] = $WrenchUITable[%ui[%i]];}
  %team.data = $PlayerDataUITable[%pl];
}
////rgbtopaint()
//Type: Support, multiple uses
//Returns the Paint Can color closest to a color in the RGBA format given
//Takes: RGBA color
//Returns: Paint Can color ID
function rgbtopaint(%rgb)
{
 %prevdist = 100000;
 %usecolor = 0; //Default
 for(%i = 0;%i < 64;%i++)
 {
     %color = getColorIDTable(%i);
     if(vectorDist(%rgb,getWords(%color,0,2)) < %prevdist && getWord(%rgb,3) - getWord(%color,3) == 0 && getWord(%rgb,3) - getWord(%color,3) == 0){%prevdist = vectorDist(%rgb,%color);%usecolor = %i;}
 }
 return %usecolor;
}

function tdmShowScores()
{
 $TeamDM::Score = schedule(60000,0,tdmShowScores);
 %i = 0;
 while(isObject($TeamDM::TeamManager.team[%i]))
 {
  %score = 0;
  for(%j=0;%j<$TeamDM::TeamManager.team[%i].memberCount;%j++)
  {
   %score = %score + $TeamDM::TeamManager.team[%i].memberID[%j].score;
  }
  %str = "<color:" @ $TeamDM::Color[$TeamDM::TeamManager.team[%i].color,1] @ ">" @ $TeamDM::TeamManager.team[%i].name @ "\c5 : " @ %score;
  schedule(%i*2000,0,tdmcommandtoall,'bottomprint',%str,2,2,1);
  %i++;
 }
}
////----Package Manager----\\\\
function servercmdTDMgetPackages(%client)
{
 for(%i=0;%i<$TeamDM::packageNum;%i++)
 {
  commandtoclient(%client,'tdmPackage',$TeamDM::PackageName[%i],$TeamDM::PackageDesc[%i],$TeamDM::PackageAllow[%i]);
 }
}

function servercmdStartPackage(%client,%name)
{
 if($TeamDM::PackageOn $= ""){$TeamDM::PackageOn = "Team Deathmatch";}
 if(AdminCheck(%client,$Pref::TeamDM::SecureLevel) !$= "1")
 {
  messageclient(%client,'',"\c5You need to be " @ AdminCheck(%client,$Pref::TeamDM::SecureLevel) @ " \c5to enable/disable game modes.");
  return;
 }
 if(%name $= ""){return;}
 %packagecode = $TeamDM::PackageCode[$TeamDM::PackageNumber[%name]];
 if(%packagecode $= ""){return;}
 if($TeamDM::TeamsOn)
 {
  messageclient(%client,'',"\c5Turn off \c3" @ $TeamDM::PackageOn @ "\c5 before enabling a package.");
  return;
 }
 %oldpackage = "";
 if(isObject($TeamDM::PackageManager))
 {
  %oldpackage = $TeamDM::PackageManager.code;
  $TeamDM::PackageManager.delete();
  if(%client !$= "-1")
  {
   messageall('',"\c3" @ %client.name @ "\c5 has disabled \c3" @ $TeamDM::PackageOn @ "\c5.");
  }
  else
  {
   messageall('',"\c3The Server\c5 has disabled \c3" @ %name @ "\c5.");
  }
  $TeamDM::PackageOn = "Team Deathmatch";
  $TeamDM::PackageTeamLock = 0;
  $TeamDM::PackageAutoSortLock = 0;
 }
 if(%packagecode !$= %oldpackage)
 {
  $TeamDM::PackageManager = new ScriptObject(){class = %packagecode @ "CtrlSO";code = %packagecode;};
  %a = $TeamDM::PackageManager.tdmCheck();
  if(%a !$= "1")
  {
   if(%client $= "-1")
   {
    echo(%a);
   }
   else
   {
    messageclient(%client,'',%a);
   }
   $TeamDM::PackageManager.delete();
   return;
  }
  $TeamDM::PackageManager.tdmSettings(%client);
  if(%client !$= "-1")
  {
   messageall('',"\c3" @ %client.name @ "\c5 has enabled \c3" @ %name @ "\c5.");
  }
  else
  {
   messageall('',"\c3The Server\c5 has enabled \c3" @ %name @ "\c5.");
  }
  $TeamDM::PackageOn = %name;
 }
}

function isTeam(%name)
{
 for(%j = 0;%j<$teamDM::TeamManager.teamcount;%j++)
 {
  if($TeamDM::TeamManager.team[%i].name $= %name){return 1;}
 }
 return 0;
}

function tdmCheck()
{
 error("TeamDM Package Check called when no packages are on.");
 return 1;
}

function tdmmessageall(%type,%msg,%a,%b,%c,%d)
{
 for(%i=0;%i<ClientGroup.getCount();%i++)
 {
  %client = clientgroup.getobject(%i);
  if(isObject(%client.minigame))
  {
   messageclient(%client,%type,%msg,%a,%b,%c,%d);
  }
 }
}

function tdmmessageallexcept(%c,%team,%type,%msg,%a,%b,%c,%d)
{
 for(%i=0;%i<ClientGroup.getCount();%i++)
 {
  %client = clientgroup.getobject(%i);
  if(isObject(%client.minigame) && %client !$= %c && %c.tdmteam !$= %team)
  {
   messageclient(%client,%type,%msg,%a,%b,%c,%d);
  }
 }
}

function tdmcommandtoall(%cmd,%a,%b,%c,%d,%e,%f,%g)
{
 for(%i=0;%i<ClientGroup.getCount();%i++)
 {
  %client = clientgroup.getobject(%i);
  if(isObject(%client.minigame))
  {
   commandtoclient(%client,%cmd,%a,%b,%c,%d,%e,%f,%g);
  }
 }
}

////----Function Overrides----\\\\
//Join
//Type: Package
//When a client joins the server, if Auto Sort is on, the client is put into a team.
package Join
{
 function GameConnection::onClientEnterGame(%this,%name,%a,%b,%c,%d,%e)
 {
  Parent::onClientEnterGame(%this,%name,%a,%b,%c,%d,%e);
   servercmdTdmHelp(%this);
   %this.tdmspawned = 1;
  if($TeamDM::ForceJoin && %this.minigame !$= $TeamDM::Minigame && isObject($TeamDM::Minigame)){$TeamDM::Minigame.addMember(%this);schedule(200,0,respawn,%this);}
  if($TeamDM::AutoSort && isObject($TeamDM::TeamManager.team0) && !isObject(%this.tdmTeam))
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
   %team.addMember(%this);
   %this.tdmTeam = %team;
   tdmmessageallexcept(%this,-1,'',"\c3" @ %this.name @ "\c5 was put into <color:" @ $TeamDM::Color[%team.color,1] @ ">" @ %team.name @ "\c5. (Auto Sort)");
   messageclient(%this,'',"\c5You were put into <color:" @ $TeamDM::Color[%team.color,1] @ ">" @ %team.name @ "\c5. (Auto Sort)");
  }
 }
 function GameConnection::onConnect(%this,%name,%a,%b,%c,%d,%e){Parent::onConnect(%this,%name,%a,%b,%c,%d,%e);}
};
activatepackage(Join);

//Leave
//Type: Package
//When a client leaves the server, they are removed from whatever team they were in.
//Also, when you change map or end the game the Team Manager is cleared.
package Leave
{
 function GameConnection::onClientLeaveGame(%this)
 {
  if(isObject(%this.tdmTeam))
  {
   if(isObject(%this.isleader)){%this.isLeader.leader = "";%this.leader = "";}
   %this.tdmTeam.removeMember(%this);
  }
  if(%this.minigame $= $TeamDM::Minigame){%this.minigame = "";$TeamDM::Minigame.removeMember2(%this);}
  %this.isleaving = 1;
  Parent::onClientLeaveGame(%this);
 }
 function onServerDestroyed(%a,%b,%c,%d)
 {
  Parent::onServerDestroyed(%a,%b,%c,%d);
  if($TeamDM::AutoSort){servercmdtogAutoSort($TeamDM::Minigame.owner);}
  $TeamDM::TeamManager.teamCount = 0;
  $TeamDM::TeamManager.team[0] = "";
  $TeamDM::TeamsOn = 0;
  $TeamDM::ForceJoin = 0;
  cancel($teamDM::score);
  $TeamDM::PackageOn = "Team Deathmatch";
  $TeamDM::PackageTeamLock = 0;
  $TeamDM::PackageAutoSortLock = 0;
  if(isObject($TeamDM::PackageManager)){$TeamDM::PackageManager.tdmEnd();$TeamDM::PackageManager.delete();}
 }
 function endGame()
 {
  Parent::endGame();
  if($TeamDM::AutoSort){servercmdtogAutoSort($TeamDM::Minigame.owner);}
  $TeamDM::TeamManager.teamCount = 0;
  $TeamDM::TeamManager.team[0] = "";
  $TeamDM::TeamsOn = 0;
  $TeamDM::ForceJoin = 0;
  cancel($teamDM::score);
  $TeamDM::PackageOn = "Team Deathmatch";
  $TeamDM::PackageTeamLock = 0;
  $TeamDM::PackageAutoSortLock = 0;
  if(isObject($TeamDM::PackageManager)){$TeamDM::PackageManager.tdmEnd();$TeamDM::PackageManager.delete();}
 }
};
activatepackage(Leave);

//Spawn
//Type: Package
//When a client spawns, if they are in a team, in a minigame (with Use Spawn Bricks on) and Team DM is on, they will spawn at that spawn point instead of their usual spawn.
//They are also given items, set to their team's datablock and have uniform applied.
package SpawnOverride
{
 function GameConnection::spawnPlayer(%this)
 {
   commandtoclient(%this,'centerprint'," ",1,1);
   if(isObject(%this.player.tempbrick)){%this.player.tempbrick.delete();}
    if(isObject(%this.tdmTeam) && isObject(%this.minigame) && $TeamDM::TeamsOn && %this.tdmTeam.spawnlist !$= "")
    {
     %transform = (getWord(%this.tdmTeam.spawnlist,getRandom(0,getWordCount(%this.tdmTeam.spawnlist)-1))).getTransform();
    }
    else if(isObject(%this.minigame))
    {
     %transform = %this.minigame.pickSpawnPoint();
    } 
    else
    {
     %transform = pickSpawnPoint();
    }
  schedule(1000,0,commandtoclient,%this,'centerprint'," ",2,2);
  %this.createPlayer(%transform);
 }
 function respawn(%this)
 {
  if(isObject(%this.player.tempbrick)){%this.player.tempbrick.delete();}
  if(!isObject(%this.player)){%this.spawnPlayer();return;}
  //%this.instantrespawn(); //For some reason scheduling this directly didn't work
  if(isObject($TeamDM::PackageManager)){$TeamDM::PackageManager.respawn(%this);}
  %pl = %this.player;
  if(isObject(%pl)){schedule(110,0,itemSet,%this);}else{return;}
  %this.player.setDamageLevel(0);
  %this.applyBodyParts();
  %this.applyBodyColors();
  %pl.unmountimage(0);fixArmReady(%pl);
  itemSet(%this);
    if(isObject(%this.tdmTeam) && isObject(%this.minigame) && $TeamDM::TeamsOn && %this.tdmTeam.spawnlist !$= "")
    {
     %transform = (getWord(%this.tdmTeam.spawnlist,getRandom(0,getWordCount(%this.tdmTeam.spawnlist)-1))).getTransform();
    }
    else if(isObject(%this.minigame))
    {
     %transform = %this.minigame.pickSpawnPoint();
    } 
    else
    {
     %transform = pickSpawnPoint();
    }%this.player.setTransform(%transform);
  return 1;
 }
 function Armor::onAdd(%this,%player,%a,%b)
 {
  Parent::onAdd(%this,%player,%a,%b);
  if(isObject(%player.client)){schedule(110,0,itemSet,%player.client);}
 }
 function itemSet(%this)
 {
   if(!isObject(%this.player)){return;}
  if(isObject(%this.minigame) && isObject(%this.tdmTeam) && $TeamDM::TeamsOn)
  {
   for(%i=0;%i<5;%i++)
   {
    %this.player.tool[%i] = (isObject(%this.tdmTeam.tool[%i]) ? %this.tdmTeam.tool[%i] : 0);
    messageClient(%this, 'MsgItemPickup', '', %i, nametoID(%this.tdmTeam.tool[%i]));
   }
   if(isObject(%this.tdmTeam.data))
    %this.player.setDatablock(%this.tdmTeam.data);
   else
    %this.player.setDatablock(PlayerStandardArmor);
   %this.applyBodyParts();
   %this.applyBodyColors();
    %this.player.setShapeNameColor($TeamDM::Color[%this.tdmTeam.color,0]);
  }
  if(isObject(%this.minigame) && !isObject(%this.tdmteam))
  {
   for(%i=0;%i<5;%i++)
   {
    %this.player.tool[%i] = (isObject(%this.minigame.startEquip[%i]) ? %this.minigame.startEquip[%i] : 0);
    messageClient(%this, 'MsgItemPickup', '', %i, nametoID(%this.minigame.startEquip[%i]));
   }
   if(isObject(%this.minigame.playerDatablock))
    %this.player.setDatablock(%this.minigame.playerDatablock);
   else
    %this.player.setDatablock(PlayerStandardArmor);
   %this.applyBodyParts();
   %this.applyBodyColors();
  }
 }
 function GameConnection::ApplyBodyColors(%this)
 {
  Parent::ApplyBodyColors(%this);
  if(isObject(%this.minigame) && isObject(%this.tdmTeam) && $TeamDM::TeamsOn)
  {
    if(!$Pref::TeamDM::Uniform)
    {
     if(%this.player.getDatablock().shapefile $= "base/data/shapes/player/m.dts")
     {
      %this.player.setNodeColor("chest",$TeamDM::Color[%this.tdmTeam.color,0] SPC "1");
      %this.player.setNodeColor("femchest",$TeamDM::Color[%this.tdmTeam.color,0] SPC "1");
     }else{%this.player.setNodeColor("body",$TeamDM::Color[%this.tdmTeam.color,0] SPC "1");}
    }
    else
    {
     uniform(%this.player,%this.tdmTeam);
    }
  }
 }
 function MiniGameSO::UpdatePlayerDataBlock(%this,%a,%b,%c,%d)
 {
  if($TeamDM::TeamsOn == 1)
  {
   for(%i=0;isObject(%this.member[%i]);%i++)
   {
    %c = %this.member[%i];
    if(isObject(%c.tdmteam)){continue;}
    if(!isObject(%c.player)){continue;}
    %c.player.setDatablock(%this.playerdatablock);
    %c.applybodyparts();
    %c.applybodycolors();
   }
   return;
  }
  Parent::UpdatePlayerDataBlock(%this,%a,%b,%c,%d);
 }
 function MiniGameSO::pickSpawnPoint(%this)
 {
  if(!isObject($TeamDM::Minigame)){parent::pickSpawnPoint(%this);return;}
  if(!isObject(%this.brickgroup)){return pickSpawnPoint();}
  for(%i=0;%i<%this.brickgroup.getCount();%i++)
  {
   %a = %this.brickgroup.getobject(%i);
   if(%a.getDatablock().uiname $= "Spawn Point")
   {
    %spawnlist = %spawnlist SPC %a;
   }
  }
  %spawnlist = trim(%spawnlist);
  if(%spawnlist $= ""){return pickSpawnPoint();}
  return (getWord(%spawnlist,getRandom(0,getWordCount(%spawnlist)-1))).getTransform();
 }
};
activatepackage(SpawnOverride);

//Chat
//Type: Package
//When a client uses Team Chat, if Team DM is on and they are in a team, the message only sends to their team. Otherwise, it uses default team chat.
package Chat
{
 function servercmdTeamMessageSent(%client,%text)
 {
  if(!$TeamDM::TeamsOn || !isObject(%client.minigame) || !isObject(%client.tdmTeam) || !$Pref::TeamDM::TeamChat)
  { 
   Parent::servercmdTeamMessageSent(%client,%text);
   return;
  }
  %msg = "\c3" @ %client.name @ " \c7[<color:" @ $TeamDM::Color[%client.tdmTeam.color,1] @ ">" @ %client.tdmTeam.name @ "\c7]\c6:" SPC %text;
  for(%i=0;%i<ClientGroup.getCount();%i++)
  {
   %cl = ClientGroup.getObject(%i);
   if(%cl.tdmTeam $= %client.tdmTeam && %cl.minigame $= %client.minigame)
   {
    messageclient(%cl,'',%msg);
   }
  }
 }
};activatepackage(Chat);

//Damage
//Type: Package
//When a player is hit by a projectile, or damaged by it in any way, the damage is negated if the damager is a team mate and Friendly Fire is off.
AddDamageType("TKill",   '<bitmap:add-ons/ci/generic> %1',    '%2 <bitmap:add-ons/ci/generic> %1 (Teamkill: -1pt)',0.5,1);
package Damage
{
 function ProjectileData::damage(%this,%obj,%col,%fade,%pos,%normal)
 {
  %team1 = %obj.client.tdmteam;%team2 = %col.client.tdmteam;
  if($TeamDM::TeamsOn && %team1 $= %team2 && isObject(%team1) && !$Pref::TeamDM::FFire){return;}
  Parent::damage(%this,%obj,%col,%fade,%pos,%normal);
 }
 function ProjectileData::radiusDamage(%this, %obj, %col, %distanceFactor, %pos, %damageAmt)
 {
  %team1 = %obj.client.tdmteam;%team2 = %col.client.tdmteam;
  if($TeamDM::TeamsOn && %team1 $= %team2 && isObject(%team1) && !$Pref::TeamDM::FFire){return;}
  Parent::radiusdamage(%this, %obj, %col, %distanceFactor, %pos, %damageAmt);
 }
 function Armor::damage(%this, %obj, %sourceObject, %position, %damage, %damageType)
 {
  %team1 = %obj.client.tdmteam;%team2 = %obj.lastpusher.tdmteam;
  if($TeamDM::TeamsOn && %team1 $= %team2 && (%damageType $= 6 || %damageType $= 5) && isObject(%team1) && !$Pref::TeamDM::FFire){return;}
  Parent::damage(%this, %obj, %sourceObject, %position, %damage, %damageType);
 }
 function gameconnection::ondeath(%this, %sourceObject, %sourceClient, %damageType, %damLoc)
 {
  %team1 = %this.tdmteam;%team2 = %sourceclient.tdmteam;
  if($TeamDM::TeamsOn && %team1 $= %team2 && isObject(%team1) && $Pref::TeamDM::FFire && %this !$= %sourceClient)
  {
   %sourceclient.incScore(-%sourceClient.minigame.points_killplayer);
   %sourceclient.incScore(-1);
   %damageType = $DamageType::TKill;
  }
  Parent::ondeath(%this, %sourceObject, %sourceClient, %damageType, %damLoc);
 }
};activatepackage(Damage);

//Minigame
//Type: Package
//Functions for forcing players to join a minigame.
package Minigame
{
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
 function servercmdsetminigamedata(%c,%type)
 {
  if(%c.isSuperAdmin && isObject($TeamDM::Minigame)){%c.minigame = $TeamDM::Minigame;$TeamDM::Minigame.owner = %c;Parent::servercmdsetminigamedata(%c,%type);$teamDM::minigame.useallplayersbricks = 1;return;}
  Parent::servercmdsetminigamedata(%c,%type);
 }
 function getMinigameFromObject(%a)
 {
  if(isObject($TeamDM::Minigame)){return $TeamDM::Minigame;}
  %r = Parent::getMinigameFromObject(%a);return %r;
 }
 function servercmdCreateMinigame(%client,%name,%color,%arg)
 {
  if(!$TeamDM::ForceJoin || ($TeamDM::ForceJoin && !isObject($TeamDM::Minigame)))
  {
   Parent::servercmdCreateMinigame(%client,%name,%color,%arg);
  }
  else
  {
   messageclient(%client,'',"\c5You cannot create minigames right now.");
  }
 }
 function servercmdLeaveMinigame(%client)
 {
  if(%client.minigame !$= $TeamDM::Minigame || %client.isLeaving)
  {
   Parent::servercmdLeaveMinigame(%client);
  }
  else
  {
   messageclient(%client,'',"\c5You cannot leave minigames right now.");
  }
 }
 function miniSet(%client)
 {
   %client.minigame.weaponDamage = 1;
   %client.minigame.selfDamage = 1;
   %client.minigame.vehicleDamage = 1;
   %client.minigame.brickDamage = 1;
   %client.minigame.fallingDamage = 1;
   %client.minigame.points_killplayer = 1;
   %client.minigame.points_killself = -1;
   %client.minigame.enablebuilding = 1;
   %client.minigame.enablewand = 1;
   %client.minigame.useallplayersbricks = 1;
   $TeamDM::Minigame.owner = $TeamDM::Minigame;
   $TeamDM::Minigame.client = $TeamDM::Minigame;
   $TeamDM::Minigame.minigame = $TeamDM::Minigame;
   $TeamDM::Minigame.brickgroup = %client.brickgroup;
 }
 function servercmdEndMinigame(%client)
 {
  if(%cl.isleaving){return;}
  if(!$TeamDM::ForceJoin)
  {
   Parent::servercmdEndMinigame(%client);
  }
  else
  {
   messageclient(%client,'',"\c5You cannot end minigames right now.");
  }
 }
 function addmember(%c){if(%c.isleaving){return;}if(isObject(%c.player)){%c.player.delete();}$TeamDM::Minigame.addMember(%c);%c.minigame = $TeamDM::Minigame;}
 function GameConnection::onClientLeaveGame(%this)
 {
  Parent::onClientLeaveGame(%this);
 }
 function GameConnection::onConnect(%this,%name,%a,%b,%c)
 {
  commandtoclient(%this,'TeamGUIcheck');
  Parent::onConnect(%this,%name,%a,%b,%c);
 }
 function GameConnection::SpawnPlayer(%this,%name,%a,%b,%c)
 {
  Parent::SpawnPlayer(%this,%name,%a,%b,%c);
 }
 function GameConnection::onClientEnterGame(%this,%name,%a,%b,%c)
 {
  Parent::onClientEnterGame(%this,%name,%a,%b,%c);
 }
 function MiniGameSO::Addmember(%this,%a)
 {
  if(%a.isLeaving){return;}
  Parent::Addmember(%this,%a);
 }
function MinigameSO::removeMember2(%this,%obj) //Deletes a member from the minigame with no respawning and no "has left the mini-game" message.
{
 for(%i=0;%i<%this.numMembers;%i++)
 {
  if(%this.member[%i] $= %obj)
  {
   %this.member[%i] = "";
   for(%j=%i+1;%j<%this.numMembers;%j++)
   {
    %this.member[%j-1] = %this.member[%j];
   }
   %this.numMembers--;
   return;
  }
 }
}
};activatepackage(Minigame);

package PackageSystem
{
 function respawn(%this)
 {
  Parent::respawn(%this);
 }
 function gameconnection::ondeath(%this,%obj,%a,%b,%c,%d,%e,%f)
 {
  if(isObject($TeamDM::PackageManager)){$TeamDM::PackageManager.ondeath(%this,%obj,%a,%b,%c,%d,%e,%f);}
  Parent::ondeath(%this,%obj,%a,%b,%c);
 }
 function gameconnection::onclientleavegame(%this)
 {
  if(isObject($TeamDM::PackageManager)){$TeamDM::PackageManager.onclientleavegame(%this);}
  Parent::onclientleavegame(%this);
 }
};activatepackage(packagesystem);
////----Datablocks and Code----\\\\
datablock fxDTSBrickData(brickTDMSpawnData : brickSpawnPointData)
{
 uiName = "Team DM Spawn";
 subCategory = "Team DM";
};
function brickTDMSpawnData::onPlant(%this,%brick)
{
 Parent::onPlant(%this,%brick);
 if($TeamDM::Uploading){return;}
 %brick.tdmteam = -1;
 %brick.setColor(rgbtopaint("0 0 0 1"));
}
function brickTDMSpawnData::onLoadPlant(%this,%brick)
{
 Parent::onLoadPlant(%this,%brick);
}
function brickTDMSpawnData::setColor(%brick,%col)
{
 if(%brick.tdmteam = -1){%col = rgbtopaint("0 0 0 1");}else{%col = rgbtopaint($TeamDM::Color[%brick.team.color,0] SPC "1");}
 Parent::setColor(%brick,%col);
}
function brickTDMSpawnData::killbrick(%this,%brick)
{
 Parent::killbrick(%this,%brick);
 %team = $TeamDM::TeamManager.team[%brick.tdmteam];
 if(isObject(%team)){%team.spawnlist = strReplace(%team.spawnlist,%brick @ " ","");}
}
function brickTDMSpawnData::onRemove(%this,%brick)
{
 Parent::onRemove(%this,%brick);
 %team = $TeamDM::TeamManager.team[%brick.tdmteam];
 if(isObject(%team)){%team.spawnlist = strReplace(%team.spawnlist,%brick @ " ","");}
}
package TDMWrenchOverRide
{
	function WrenchProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
	{
		if(%col.tdmTeam $= "" || AdminCheck(%obj.client,$Pref::TeamDM::SecureLevel) !$= "1"){Parent::onCollision(%this,%obj,%col,%fade,%pos,%normal);return;}
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
activatePackage("tdmwrenchoverride");

function getPackages()
{
echo("***TDM Packages Init");
%open = new FileObject();
 %name = findFirstFile("Add-Ons/TeamDMv4/Packages/*.tdm");
 $TeamDM::PackageNum = 0;
 while(%name !$= "")
 {
  %open.openForRead(%name);
   $TeamDM::PackageName[$TeamDM::PackageNum] = strReplace(%open.readLine(),"//","");
   echo("**Loading Package: " @ $TeamDM::PackageName[$TeamDM::PackageNum]);
   echo("**Number: " @ $TeamDM::PackageNum);
   %descLines = strReplace(%open.readLine(),"//","");
   %desc = "";
   for(%i=0;%i<%descLines;%i++){%desc = %desc @ strReplace(%open.readLine(),"//","") NL "\c5";}
   $TeamDM::PackageDesc[$TeamDM::PackageNum] = %desc;
   $TeamDM::PackageNumber[$TeamDM::PackageName[$TeamDM::PackageNum]] = $TeamDM::PackageNum;
   $TeamDM::PackageCode[$TeamDM::PackageNum] = strReplace(%open.readLine(),"//","");
   echo("**Code: " @ $TeamDM::PackageCode[$TeamDM::PackageNum]);
   exec(%name);
   $TeamDM::PackageAllow[$TeamDM::PackageNum] = strReplace(%open.readLine(),"//","");
   %on = $TeamDM::PackageOn $= $TeamDM::PackageName[$TeamDM::PackageNum];
   %open.close();
   echo("**Loaded: " @ $TeamDM::PackageName[$TeamDM::PackageNum]); 
  %name = findNextFile("Add-Ons/TeamDMv4/Packages/*.tdm");
  $TeamDM::PackageNum++;
 }
%open.delete();
echo("***TDM Packages Done");
}
 if($TeamDM::PackageOn $= ""){$TeamDM::PackageOn = "Team Deathmatch";}
schedule(3000,0,getPackages);