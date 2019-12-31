function TeamDMGui::update(%this,%clientId,%name,%isSuperAdmin,%isAdmin,%isAI,%score)
{
   // Build the row to display.  The name can have ML control tags,
   // including color and font.  Since we're not using and
   // ML control here, we need to strip them off.
   %tag = %isSuperAdmin? "[Super]":
          (%isAdmin? "[Admin]":
          (%isAI? "[Bot]":
          ""));
   if(%clientId.team == 1){%teamname="\c0Red";}else
   if(%clientId.team == 2){%teamname="\c4Blue";}else
   if(%clientId.team == 3){%teamname="\c2Green";}else{%teamname="None";}
   %text = setField(%text,1,%teamname);
   %text = StripMLControlChars(%name) SPC %tag TAB %teamname;

   // Update or add the player to the control
   if (TeamDMGuiList.getRowNumById(%clientId) == -1)
      TeamDMGuiList.addRow(%clientId, %text);
   else
      TeamDMGuiList.setRowById(%clientId, %text);

   // Sorts by score
   TeamDMGuiList.sortNumerical(1,false);
}

function TeamDMGui::updateScore(%this,%clientId,%score)
{
   %text = TeamDMGuiList.getRowTextById(%clientId);
   if(%clientId.team == 1){%teamname="\c0Red";}else
   if(%clientId.team == 2){%teamname="\c0Blue";}else
   if(%clientId.team == 3){%teamname="\c0BGreen";}else{%teamname="None";}
   %text = setField(%text,1,%teamname);
   TeamDMGuiList.setRowById(%clientId, %text);
   TeamDMGuiList.sortNumerical(1,false);
   TeamDMGuiList.clearSelection();
}

function TeamDMGui::remove(%this,%clientId)
{
   TeamDMGuiList.removeRowById(%clientId);
}

function TeamDMGui::toggle(%this)
{
   if (%this.isAwake())
      Canvas.popDialog(%this);
   else
      Canvas.pushDialog(%this);
}

function TeamDMGui::clear(%this)
{
   // Override to clear the list.
   TeamDMGuiList.clear();
}

function TeamDMGui::zeroScores(%this)
{
   for (%i = 0; %i < TeamDMGuiList.rowCount(); %i++) {
      %text = TeamDMGuiList.getRowText(%i);
      %text = setField(%text,1,"0");
      TeamDMGuiList.setRowById(TeamDMGuiList.getRowId(%i), %text);
   }
   TeamDMGuiList.clearSelection();
}

function TeamDMGui::OnWake(%this)
{
 TeamDMGuiList.clear();
   %count = ClientGroup.getCount();
   for(%cl= 0; %cl < %count; %cl++)
   {
      %clientId = ClientGroup.getObject(%cl);
	TeamDMGui::update(%this,%clientId,%clientId.name,%clientId.isSuperAdmin,%clientId.isAdmin,%clientId.isAI,%clientId.score);
   }
}

function servercmdClientSetTeam(%client,%team)
{
	//get client id from player list
	%victimId = TeamDMGuiList.getSelectedId();
	//set team if client is admin otherwise join it
	if (%client.isSuperAdmin)
	{
	 servercmdPutTeam(%client,%victimId,%team);
	}
	else
	{
	 servercmdsetteam(%client,%team);
	}
 TeamDMGuiList.clear();
   %count = ClientGroup.getCount();
   for(%cl= 0; %cl < %count; %cl++)
   {
      %clientId = ClientGroup.getObject(%cl);
	TeamDMGui::update(%this,%clientId,%clientId.name,%clientId.isSuperAdmin,%clientId.isAdmin,%clientId.isAI,%clientId.score);
   }
}

function servercmdPutTeam(%client,%victimId,%team)  //Admin Function, puts client %victimId into team %team
{
 if (%client.isSuperAdmin)
 {
  if (%team != 1 & %team != 2 & %team != 3){servercmdleaveteam(%client);return;}
  if (%victimId.minigame)
  {
   %victimId.team=%team;
   if (%team == 1){messageAll("","\c3" @ %victimId.name @ " was put into \c0red\c3 team.");%victimId.player.kill($DamageType::Misuse);%victimId.team=1;}
   if (%team == 2){messageAll("","\c3" @ %victimId.name @ " was put into \c4blue\c3 team.",%client.name);%victimId.player.kill($DamageType::Misuse);%victimId.team=2;}
   if (%team == 3){messageAll("","\c3" @ %victimId.name @ " was put into \c2green\c3 team.",%client.name);%victimId.player.kill($DamageType::Misuse);%victimId.team=3;}
  }else{messageClient(%client,"","\c0This player isn't in a minigame.");%victimId.team=0;}
 }
}

function servercmdGetGun(%client)  //If admin, gives client a Team Setup Gun
{
 if (%client.isSuperAdmin)
 {
  %client.player.mountimage(Tgunimage,0);
  servercmdZombie(%client);
 }
}

function servercmdRandomTeam(%client)
{
 if (%client.isSuperAdmin)
 {
  TeamDMGuiList.clear();
  %team=1;
    %count = ClientGroup.getCount();
    for(%cl= 0; %cl < %count; %cl++)
    {
       %clientId = ClientGroup.getObject(%cl);
       if(isObject(%clientId.minigame)){
       servercmdPutTeam(%client,%clientId,%team);
       %team++;
       if (%team == 3){%team=1;}}
    }
 }
 TeamDMGuiList.clear();
   %count = ClientGroup.getCount();
   for(%cl= 0; %cl < %count; %cl++)
   {
      %clientId = ClientGroup.getObject(%cl);
	TeamDMGui::update(%this,%clientId,%clientId.name,%clientId.isSuperAdmin,%clientId.isAdmin,%clientId.isAI,%clientId.score);
   }
}

function servercmdRandomTeam3(%client)
{
 if (%client.isSuperAdmin)
 {
  TeamDMGuiList.clear();
  %team=1;
    %count = ClientGroup.getCount();
    for(%cl= 0; %cl < %count; %cl++)
    {
       if(isObject(%clientId.minigame)){
       %clientId = ClientGroup.getObject(%cl);
       servercmdPutTeam(%client,%clientId,%team);
       %team++;
       if (%team == 4){%team=1;}}
    }
 }
 TeamDMGuiList.clear();
   %count = ClientGroup.getCount();
   for(%cl= 0; %cl < %count; %cl++)
   {
      %clientId = ClientGroup.getObject(%cl);
	TeamDMGui::update(%this,%clientId,%clientId.name,%clientId.isSuperAdmin,%clientId.isAdmin,%clientId.isAI,%clientId.score);
   }
}

function serverCmdSetUp(%client)
{
	if(%client.isSuperAdmin)
	{
		for (%i = 0; %i < PlayerDropPoints.getCount(); %i++)
		{
			%obj = PlayerDropPoints.getObject(%i);
			if (%obj.GetClassName() $= "SpawnSphere")
			{
				$Pref::Server::TeamDMSphereTr=getWords(%obj.getTransform(),0,2);
				%obj.delete();
				echo($Pref::Server::TeamDMSphereTr);
			}
		}
						$Pref::Server::TeamDMTrig.delete();
						$Pref::Server::TeamDMTrig =    new Trigger() {
				      position = "-1000 -1000 -1000";
				      rotation = "1 0 0 0";
				      scale = "3 3 3";
				      dataBlock = "TeamSpawnSet";
				      polyhedron = "0 0 0 1 0 0 0 -1 0 0 0 1";
				   };
					$Pref::Server::TeamDMSphere.delete();
				      $Pref::Server::TeamDMSphere = new SpawnSphere() {
				         position = "-1000 -1000 -999";
				         rotation = "1 0 0 0";
				         scale = "0.940827 1.97505 1";
				         dataBlock = "SpawnSphereMarker";
				         radius = "1";
				         sphereWeight = "1";
				         indoorWeight = "0";
				         outdoorWeight = "0";
				         homingCount = "0";
				         lockCount = "0";
				         locked = "false";
				      };
				PlayerDropPoints.add($Pref::Server::TeamDMSphere);
		messageClient(%client,"","\c0Warning: Do NOT suicide/die until you are in a team and that team's spawn is set!");
	}	
}

function del(%obj)
{
 %obj.delete();
}

function serverCmdDelAll(%client)
{
	if(%client.isSuperAdmin)
	{
		for (%i = 0; %i < PlayerDropPoints.getCount(); %i++)
		{
			%obj = PlayerDropPoints.getObject(%i);
			if (%obj.GetClassName() $= "SpawnSphere")
			{
				%obj.delete();
			}
		}
		for (%i = 0; %i < MissionGroup.getCount(); %i++)
		{
			%obj = MissionGroup.getObject(%i);
			if (%obj.isTeamDM)
			{
				schedule(10,0,del,%obj);
			}
		}
						$Pref::Server::TeamDMTrig.delete();
					$Pref::Server::TeamDMSphere.delete();
				      $Pref::Server::TeamDMSphere = new SpawnSphere() {
				         position = $Pref::Server::TeamDMSphereTr;
				         scale = "0.940827 1.97505 1";
				         dataBlock = "SpawnSphereMarker";
				         radius = "1";
				         sphereWeight = "1";
				         indoorWeight = "1";
				         outdoorWeight = "1";
				         homingCount = "0";
				         lockCount = "0";
				         locked = "false";
				      };
				//echo($Pref::Server::TeamDMSphere.position);
				PlayerDropPoints.add($Pref::Server::TeamDMSphere);
		messageClient(%client,"","\c0All triggers and spawns set by the Team DM mod have been removed.");
	}	
}

function getscores()
{
 %redScore=0;
 %blueScore=0;
 %greenScore=0;
   %count = ClientGroup.getCount();
   for(%cl= 0; %cl < %count; %cl++)
   {
      %clientId = ClientGroup.getObject(%cl);
	if (%clientId.team == 1){%redScore = %redScore + %clientId.score;}
	if (%clientId.team == 2){%blueScore = %blueScore + %clientId.score;}
	if (%clientId.team == 3){%greenScore = %greenScore + %clientId.score;}
   }
  messageAll("","\c3The Total Scores:");
  messageAll("","\c0Red Team:" SPC %redScore SPC "points!");
  messageAll("","\c4Blue Team:" SPC %blueScore SPC "points!");
  messageAll("","\c2Green Team:" SPC %greenScore SPC "points!");
  if (%redScore > %blueScore & %redScore > %greenScore){ messageAll("","\c0Red Team is winning!");}else
  if (%blueScore > %redScore & %blueScore > %greenScore){ messageAll("","\c4Blue Team is winning!");}else
  if (%greenScore > %blueScore & %greenScore > %redScore){ messageAll("","\c2Green Team is winning!");}else
  { messageAll("","\c3It's a tie!");}
 $scoreget=schedule(60000,0,getscores);
}

function servercmdstartscore(%client)
{
 if (%client.isSuperAdmin){if(!$scoreget){$scoreget=schedule(10,0,getscores);}}
}

function servercmdstopscore(%client)
{
 if (%client.isSuperAdmin){cancel($scoreget);$scoreget=0;messageClient(%client,"","\c0Score update turned off.");}
}

function servercmdUniToggle(%client)
{
 if (%client.isSuperAdmin)
 {
	if($Pref::Server::TeamDMUniform){$Pref::Server::TeamDMUniform=0;messageAll("","\c3Team DM full uniforms turned \c0off.");}else{$Pref::Server::TeamDMUniform=1;messageAll("","\c3Team DM full uniforms turned \c2on.");}
 }
}

function servercmdChatToggle(%client)
{
 if (%client.isSuperAdmin)
 {
	if($Pref::Server::TeamDMChat){$Pref::Server::TeamDMChat=0;messageAll("","\c3Team chat turned \c0off.");}else{$Pref::Server::TeamDMChat=1;messageAll("","\c3Team chat turned \c2on.");}
 }
}