

function servercmdSetTeam(%client,%team)  //Allows the client to set his/her team using /setteam 1 or /setteam 2 in chat.
{
 if (%team != 1 & %team != 2 & %team != 3){servercmdleaveteam(%client);return;}
 if (%client.minigame)
 {
  %client.team=%team;
  if (%team == 1){messageAll("","\c3" @ %client.name @ " joined \c0red\c3 team.");%client.player.kill($DamageType::Misuse);}
  if (%team == 2){messageAll("","\c3" @ %client.name @ " joined \c4blue\c3 team.",%client.name);%client.player.kill($DamageType::Misuse);}
  if (%team == 3){messageAll("","\c3" @ %client.name @ " joined \c2green\c3 team.",%client.name);%client.player.kill($DamageType::Misuse);}
 }else{messageClient(%client,"","\c0You can't join teams if you aren't in a minigame!");%client.team=0;}
}

function servercmdLeaveTeam(%client)  //Allows the client to leave his/her team using /leaveteam in chat
{
 if (%client.minigame)
 {
  messageAll("","\c0" @ %client.name @" is no longer in a team.");
  %client.team=0;
  %client.player.kill($DamageType::Misuse);
 }else{messageClient(%client,"","\c0You are not in a minigame!");%client.team=0;}
}

function servercmdgetteamgun(%client)  //Gives the client a Team Spawn Gun
{
 if (%client.isSuperAdmin)
 {
	%client.player.mountImage(TGunImage, 0);
	servercmdzombie(%client);
 }
}

datablock TriggerData(TeamSpawnSet)  //TeamSpawnSet moves you to the correct brick set as your spawn when you touch it. It also changes your Torso colour to the correct one.
{
   // The period is value is used to control how often the console
   // onTriggerTick callback is called while there are any objects
   // in the trigger.  The default value is 100 MS.
   tickPeriodMS = 100;
};


function TeamSpawnSet::onLeaveTrigger(%this,%trigger,%obj)
{
}

function TeamSpawnSet::onEnterTrigger(%this,%trigger,%obj)
{

//This Trigger gets your "team" variable and sends you to a spawn brick of the same variable.
 if (%obj.client.minigame == 0 || %obj.client.team == 0){%client.team=0;}
 if (%obj.client.team == 1)
 {
	if($Pref::Server::TeamDMUniform)
	{
		uniform(%obj,"0.900 0.000 0.000 1.000");
	}
	else
	{
		%obj.setNodeColor(chest,"0.900 0.000 0.000 1.000");
		%obj.setNodeColor(femchest,"0.900 0.000 0.000 1.000");
	}
 	%obj.setTransform($Pref::Server::team1brick.getTransform());
	%numbricks=0;
	for (%i = 0; %i < ServerConnection.getCount(); %i++)
	{
		%b = ServerConnection.getObject(%i)-1;
		//echo(%b);
		 if (%b.isSpawn $= "1")
		 {
			%brick[%numbricks]=%b;
			//error(%b);
			%numbricks++;
		 }
	}
	%index=getRandom(%numbricks);
	//if (%numbricks == 0){%obj.setTransform($Pref::Server::Team1Brick.getTransform());}
	//echo("Rnd" SPC %index SPC "/" SPC %numbricks-1);
	%obj.setTransform(%brick[%index].getTransform());
	%obj.setShapeNameColor("0 0 1 1");
 }
 if (%obj.client.team == 2)
 {
	if($Pref::Server::TeamDMUniform)
	{
		uniform(%obj,"0.000 0.000 0.900 1.000");
	}
	else
	{
		%obj.setNodeColor(chest,"0.000 0.000 0.900 1.000");
		%obj.setNodeColor(femchest,"0.000 0.000 0.900 1.000");
	}
 	%obj.setTransform($Pref::Server::team2brick.getTransform());
	%numbricks=0;
	for (%i = 0; %i < ServerConnection.getCount(); %i++)
	{
		%b = ServerConnection.getObject(%i)-1;
		//echo(%b);
		 if (%b.isSpawn $= "2")
		 {
			%brick[%numbricks]=%b;
			//error(%b);
			%numbricks++;
		 }
	}
	%index=getRandom(%numbricks);
	//if (%numbricks == 0){%obj.setTransform($Pref::Server::Team2Brick.getTransform());}
	//echo("Rnd" SPC %index SPC "/" SPC %numbricks-1);
	%obj.setTransform(%brick[%index].getTransform());
	%obj.setShapeNameColor("0 0 1 1");
 }
 if (%obj.client.team == 3)
 {
	if($Pref::Server::TeamDMUniform)
	{
		uniform(%obj,"0.000 0.900 0.000 1.000");
	}
	else
	{
		%obj.setNodeColor(chest,"0.000 0.900 0.000 1.000");
		%obj.setNodeColor(femchest,"0.000 0.900 0.000 1.000");
	}
 	%obj.setTransform($Pref::Server::team3brick.getTransform());
	%numbricks=0;
	for (%i = 0; %i < ServerConnection.getCount(); %i++)
	{
		%b = ServerConnection.getObject(%i)-1;
		//if(%b.isSpawn!$=""){echo(%b.isSpawn);}
		 if (%b.isSpawn $= "3")
		 {
			%brick[%numbricks]=%b;
			//error(%b);
			%numbricks++;
		 }
	}
	%index=getRandom(%numbricks-1);
	//if (%numbricks == 0){%obj.setTransform($Pref::Server::Team3Brick.getTransform());}
	//echo("Rnd" SPC %index SPC "/" SPC %numbricks-1);
	%obj.setTransform(%brick[%index].getTransform());
	%obj.setShapeNameColor("0 0 1 1");
 }
}

function uniform(%player,%color)
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
%player.unhideNode(RShoe);
%player.unhideNode(LShoe);
%player.unhideNode(rhand);
%player.unhideNode(lhand);
%player.unhidenode(rarm);
%player.unhidenode(larm);
%player.unhideNode(pants);
%player.unhideNode(chest);
%player.unhidenode(headskin);
%player.unhideNode(helmet);
%player.unhideNode(visor);
%player.setNodeColor(RShoe,%color);
%player.setNodeColor(LShoe,%color);
%player.setNodeColor(rhand,%color);
%player.setNodeColor(lhand,%color);
%player.setNodeColor(rarm,"0 0 0 1");
%player.setNodeColor(larm,"0 0 0 1");
%player.setNodeColor(pants,"0 0 0 1");
%player.setNodeColor(chest,%color);
%player.setNodeColor(helmet,%color);
%player.setNodeColor(visor,"0 0 1 0.25");
%player.setDecalName(1);
}
}

function ProjectileData::damage(%this,%obj,%col,%fade,%pos,%normal)  //Edited Damage function stopping you from damaging teammates.
{
   if(%this.directDamage <= 0)
      return;
   
   //if (%obj.client.team == %col.client.team) //& %obj.client.minigame & %col.client.minigame)
   //   return;

   //direct damage doubles for crouching players
   %damageType = $DamageType::Direct;
   if(%this.DirectDamageType)
      %damageType = %this.DirectDamageType;

   if(%col.getType() & $TypeMasks::PlayerObjectType)
   {
      %col.damage(%obj, %pos, %this.directDamage, %damageType);
   }
   else
   {
      %col.damage(%obj, %pos, %this.directDamage, %damageType);
   }
}

function ProjectileData::radiusDamage(%this, %obj, %col, %distanceFactor, %pos, %damageAmt) //Edited Radius Damage function stopping you from damaging teammates.
{
   //validate distance factor
   if(%distanceFactor <= 0)
      return;
   else if(%distanceFactor > 1)
      %distanceFactor = 1;
   
   %damageAmt *= %distanceFactor;
   
   //if (%obj.client.team == %col.client.team) //& %obj.client.minigame & %col.client.minigame)
   //   return;

   if(%damageAmt)
   {
      //use default damage type if no damage type is given
      %damageType = $DamageType::Radius;
      if(%this.RadiusDamageType)
      %damageType = %this.RadiusDamageType;

      %col.damage(%obj, %pos, %damageAmt, %damageType);

      //burn the player?
      if(%this.explosion.playerBurnTime > 0)
      {
         if(%col.getType() & $TypeMasks::PlayerObjectType)
         {
            %col.burn(%this.explosion.playerBurnTime * %distanceFactor);
         } 
      }
   }
}

datablock TriggerData(TeamRedSet)  //Sets your team to "1", red team, then respawns you.
{
   // The period is value is used to control how often the console
   // onTriggerTick callback is called while there are any objects
   // in the trigger.  The default value is 100 MS.
   tickPeriodMS = 100;
};


function TeamRedSet::onLeaveTrigger(%this,%trigger,%obj)
{
}

function TeamRedSet::onEnterTrigger(%this,%trigger,%obj)
{

//This trigger sets your team to "1", red team
 servercmdsetteam(%obj.client,1);
}

datablock TriggerData(TeamBlueSet)  //Sets your team to "2", blue team, then respawns you.
{
   // The period is value is used to control how often the console
   // onTriggerTick callback is called while there are any objects
   // in the trigger.  The default value is 100 MS.
   tickPeriodMS = 100;
};


function TeamBlueSet::onLeaveTrigger(%this,%trigger,%obj)
{
}

function TeamBlueSet::onEnterTrigger(%this,%trigger,%obj)
{
 servercmdsetteam(%obj.client,2);
}

datablock TriggerData(TeamGreenSet)  //Sets your team to "3", green team, then respawns you.
{
   // The period is value is used to control how often the console
   // onTriggerTick callback is called while there are any objects
   // in the trigger.  The default value is 100 MS.
   tickPeriodMS = 100;
};


function TeamGreenSet::onLeaveTrigger(%this,%trigger,%obj)
{
}

function TeamGreenSet::onEnterTrigger(%this,%trigger,%obj)
{
 servercmdsetteam(%obj.client,3);
}


	function serverCmdTeamMessageSent(%client, %text){ //Replacing "all in game", now does "all in team"
	   if(%client.team $= "" || %client.team == 0){messageClient(%client, "", "\c0You aren't in a team!");return;}
	   if(!$Pref::Server::TeamDMChat){messageClient(%client, "", "\c0Team chat has been turned off.");return;}
   %count = ClientGroup.getCount();
   for(%cl= 0; %cl < %count; %cl++)
   {
      %recipient = ClientGroup.getObject(%cl);
	  if(%recipient.team $= %client.team){
	      messageClient(%recipient, "", '%1(\c4Team\c0)\c6: %2', %client.player.getShapeName(), %text);
   }}
	}

package NoSlam {
	function Armor::damage(%this, %obj, %sourceObject, %position, %damage, %damageType)
	{ 
	 //echo(%obj.client.team SPC "%obj.client.team");
	 //echo(%this SPC "%this");
	 //echo(%obj SPC "%obj [dead guy]");
	 //echo(%sourceObject SPC "%sourceObject");
	 //echo(%sourceObject.team SPC "%sourceObject.team");
	 //echo(%damageType SPC "DAMAGE TYPE");
	 //echo(%position SPC "Position?");
	 //echo(%damage SPC "Damage?");
	 //echo(%sourceObject.client.name SPC "Source Object");
	 //echo(%obj.client.name SPC "Obj Name");
	 if ((%obj.client.team != %sourceObject.client.team) || !isObject(%sourceObject) || %sourceObject.client.team == 0 || %obj.team == 0)
	 {
	  Parent::Damage(%this, %obj, %sourceObject, %position, %damage, %damageType);
	 }
	}
};
ActivatePackage(NoSlam);

if (!isObject(TeamDMGui)){exec("./TeamDMGui.gui");}
exec("./TeamDMGui_functions.cs");

moveMap.bindCmd(keyboard, "ctrl m", "", "TeamDMGui.toggle();"); 