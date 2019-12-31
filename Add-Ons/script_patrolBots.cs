//By MrPickle
//Thanks to Randy for fixing a few things.

$Pref::Server::MaxPbots = 10;
$Pref::Server::BotGuns = 0;

function serverCmdPatrolBot(%client, %botName, %botLastName){
	if(%client.pbotCount >= $Pref::Server::MaxPbots)
		return;
	if(!isObject(%client.pbot[1]))
		%client.pbotCount = 0;
	%client.pBotCount++;
	%bot = new AIPlayer(){
		datablock = PlayerNoJet;
		patrolNum = 0;
	};
	%client.currentPBot = %bot;
	%bot.setTransform(%client.player.getPosition());
	if(%botLastName !$= "")
		%bot.name = %botName SPC %botLastName;
	else
		%bot.name = %botName;
	%bot.setShapeName(%bot.name);
	%bot.number = %client.pbotCount;
	%lolkthx = %client.player;
	%client.player = %bot;
	%client.applyBodyParts();
	%client.applyBodyColors();
	%client.player = %lolkthx;
	messageclient(%client, "", "\c6Patrol bot spawned at \c0" @ %bot.getPosition() @ "\c6.");
	%bot.isPBot = 1;
	if(isObject(%client.minigame))
		%bot.minigame = %client.mimigame;
	%bot.owner = %client;
	%client.pBot[%client.pBotCount] = %bot;
}

function serverCmdPatrol(%client){
	%bot = %client.currentPBot;
	if(isObject(%bot)){
		%pos = %client.player.getPosition();
		%bot.patrol[%bot.patrolNum] = %pos;
		%bot.patrolNum++;
		%bot.setMoveDestination(%pos, 0);
		messageclient(%client, "", "\c6Patrol point " @ %bot.patrolNum @  " set at:\c0 " @ %pos @ "\c6.");
	} else {
		messageclient(%client, "", "\c6You don't have any patrol bots."); 
			return;
	}
}

function serverCmdDeletePBot(%client, %num){
	if(%num !$= "All" && %num !$= "" && %client.pbotCount >= %num){
		%bot = %client.pbot[%num];
		if(isObject(%bot)){
			%name = %bot.name;
			%num = %bot.number;
			%bot.delete();
			if(!isObject(%bot)){
				messageclient(%client, "", "\c6Patrol bot \"\c0" @ %name @ "\c6\" has been deleted.");
				reorderPatrolBots(%client, %num);
				%client.pbotCount--;
			} else
				messageclient(%client, "", "\c6Patrol bot \"\c0" @ %name @ "\c6\" could not be deleted.");
		} else {
			messageclient(%client, "", "\c6Patrol bot \c0" @ %num @ "\c6 doesn't exist!");
		}
	} else if(%num $= "All"){
		if(%client.pbotCount < 1)
			return;
		for(%i=1;%i<%client.pbotCount+1;%i++){
			%bot = %client.pbot[%i];
			if(isObject(%bot))
				%bot.delete();
		}
		%client.pbotCount = "0";
		messageall("", %client.name @ "\c6 has cleared all their patrol bots.");
	} else if(%num $= ""){
		%name = %client.currentPBot.name;
		%num = %client.currentPBot.number;
		%client.currentPBot.delete();
		if(!isObject(%bot)){
			messageclient(%client, "", "\c6Patrol bot \"\c0" @ %name @ "\c6\" has been deleted.");
			reorderPatrolBots(%client, %num);
			%client.pbotCount--;
		} else
			messageclient(%client, "", "\c6Patrol bot \"\c0" @ %name @ "\c6\" could not be deleted.");
	} else
		messageclient(%client, "", "\c6Patrol bot \c0" @ %num @ "\c6 doesn't exist!");
}

function serverCmdClearAllPBots(%client){
	if(%client.isAdmin || %client.isSuperAdmin){
		for(%i=0;%i<ClientGroup.getCount();%i++){
			%cl = ClientGroup.getObject(%i);
			if(%cl.pBotCount >= 1){
				for(%j=1; %j<%cl.pbotCount; %j++){
					%bot = %cl.pbot[%i];
					if(isObject(%bot))
						%bot.delete();
				}
				%cl.pbotCount = 0;
			}
		}
		messageall("", %client.name SPC "\c6has cleared all patrol bots.");
	} else
		messageclient("", "\c6Only \c0Admins \c6can clear all patrol bots.");
}

function serverCmdClearPBots(%client, %name){
	if(%client.isAdmin || %client.isSuperAdmin){
		%cl = findClientByName(%name);
		if(%cl.pbotcount < 1){
			messageclient(%client, "", %cl.name @ "\c6 has no patrol bots.");
			return;
		}
		for(%i=0;%i<%cl.pbotcount+1;%i++){
			if(isObject(%cl.pbot[%i]))
				%cl.pbot[%i].delete();
		}
		%cl.pbotcount = 0;
		messageall("", %client.name @ "\c6 has cleared all of \c0" @ %cl.name @ "\c6's patrol bots.");
	}
}

function Armor::onReachDestination(%this, %obj){
	if(!%obj.isPBot)
		return;
	if(%obj.getControlObject() != 0) %dist = 5; else %dist = 2;
		
	for(%i=0;%i<%obj.patrolNum;%i++){
		if(vectorDist(%obj.getPosition(), %obj.patrol[%obj.patrolNum - 1]) < %dist){
			%obj.setMoveDestination(%obj.patrol[0], 0);
		} else if(vectorDist(%obj.getMoveDestination(), %obj.patrol[%i]) < %dist){
			%obj.setMoveDestination(%obj.patrol[%i++], 0);
		}
	}
}

function PBotShoot(%bot){
	if(isObject(%bot)){
		%bot.setImageTrigger(0,0);
		%eyeVec = %bot.getEyeVector();
		%startPos = %bot.getEyePoint();
		%endPos = VectorAdd(%startPos,vectorscale(%eyeVec,20));
		%mask = $TypeMasks::PlayerObjectType;
		%target = ContainerRayCast(%startPos, %endPos, %mask);
		if(%target)
			%bot.setImageTrigger(0,1);
	}
	if($Pref::Server::BotGuns)
		schedule(1000, 0, "PBotShoot", %bot);
}

function serverCmdBotWeapons(%client, %onOff){
	if(%client.isAdmin || %client.isSuperAdmin){
		if(%onOff $= "On" && !$Pref::Server::BotGuns){
			$Pref::Server::BotGuns = 1;
			messageall("","\c0" @ %client.name @ "\c6 has \c0enabled \c6patrol bot guns.");
		} else if(%onOff $= "Off" && $Pref::Server::BotGuns){
			$Pref::Server::BotGuns = 0;
			messageall("","\c0" @ %client.name @ "\c6 has \c0disabled \c6patrol bot guns.");
			commandtoserver('UpdateBots');
		}
	} else
		messageclient("","\c6Only \c0Admins \c6can clear all patrol bots.");
}

function serverCmdUpdatePBot(%client, %update, %botName, %botNameLast){
if(isObject(%client.currentPBot)){
%bot = %client.currentPBot;
	switch$(%update){
		case "Weapon":
			if(!isObject(%botName @ "Image")){ messageclient(%client,"","\c6The weapon: \c0" @ %botName @ "\c6 doesn't exist!"); return; }
			%bot.weapon = %botName @ "Image";
			messageclient(%client,"","\c6Bot weapon set to the\c0 " @ %botName @ "\c6.");
				if($Pref::Server::BotGuns){
					%bot.mountImage(%bot.Weapon,0);
					%bot.playThread(0, armReadyRight);
					schedule(1000,0,"PBotShoot",%bot);
				}
		case "Clothes":
			%lolkthx = %client.player;
			%client.player = %bot;
			%client.applyBodyParts();
			%client.applyBodyColors();
			%client.player = %lolkthx;
		case "Name":
			if(%botNameLast !$= "")
				%bot.name = %BotName SPC %BotNameLast;
			else
				%bot.name = %BotName;
			%bot.setShapeName(%bot.name);
		}
	}
}

function serverCmdSelectPBot(%client,%num){
	if(%num $= ""){
		%start = %client.player.getEyePoint();
		%end = vectorAdd(vectorScale(vectorNormalize(%client.player.getEyeVector()), 5), %start);
		%obj = ContainerRayCast(%start, %end, $TypeMasks::PlayerObjectType);
			if(%obj.isPBot && %obj.owner == %client){
				%client.currentPBot = %obj;
				messageclient(%client,"","\c6Patrol Bot \"\c0" @ %client.currentPBot.name @ "\c6\" has been selected.");
			}				
		} else {
			if(isObject(%client.pbot[%num])){
				%client.currentPBot = %client.pbot[%num];
				messageclient(%client,"","\c6Patrol Bot \"\c0" @ %client.currentPBot.name @ "\c6\"has been selected.");
			} else
				messageclient(%client,"","\c6Patrol Bot number: \c0" @ %num @ "\c6 doesn't exist.");
	}
}

function serverCmdResetPatrols(%client,%num){
	if(%num $= "")
		%bot = %client.currentPBot;
	else
		%bot = %client.pbot[%num];
	for(%i=0;%i<%bot.patrolNum;%i++)
		%bot.patrol[%i] = "";
	messageclient(%client,"",%bot.name @ "\c6's patrol points have been reset.");
}

function reorderPatrolBots(%client, %num){
	for(%i=%num;%i<=%client.pbotCount;%i++){
		%client.pbot[%i] = %client.pbot[%i+1];
		%client.pbot[%i].number = %i;
	}
}

package PatrolBots{
	//~ function GameConnection::onClientLeaveGame(%this){
	//~ Parent::onClientLeaveGame(%this);
		//~ if(!isObject(%client.pbot[0])) return;
			//~ for(%i=0; %i<%this.pBotCount; %i++){
				//~ %bot = %this.pbot[%i];
					//~ if(isObject(%bot))
						//~ %bot.delete();					
		//~ }
	//~ }
	
	function serverCmdCreateMinigame(%client, %name){
	Parent::serverCmdCreateMinigame(%client, %name);
		if(!isObject(%client.pbot[0])) return;		
			for(%i=0; %i<%client.pBotCount; %i++){
					%bot = %client.pbot[%i];
						if(isObject(%bot))
							%bot.minigame = %client.minigame;
		}
	}

	function serverCmdJoinMinigame(%client, %name){
	Parent::serverCmdJoinMinigame(%client, %name);
		if(!isObject(%client.pbot[0])) return;		
			for(%i=0; %i<%client.pBotCount; %i++){
					%bot = %client.pbot[%i];
						if(isObject(%bot))
							%bot.minigame = %client.minigame;
		}
	}

	function serverCmdLeaveMinigame(%client, %name){
	Parent::serverCmdLeaveMinigame(%client, %name);
		if(!isObject(%client.pbot[0])) return;		
			for(%i=0; %i<%client.pBotCount; %i++){
					%bot = %client.pbot[%i];
						if(isObject(%bot))
							%bot.minigame = %client.minigame;
		}
	}
	
	function paintProjectile::OnCollision(%this, %obj, %col, %fade, %pos, %normal){
		if(%col.getClassName() $= "AIPlayer") return;
		parent::OnCollision(%this, %obj, %col, %fade, %pos, %normal);
	}
};
activatepackage(PatrolBots);

function servercmdgetPatrolBotList(%client){
commandtoclient(%client,'updateGUI',"PatrolBotList","listClear");
for(%i=0;%i<%client.pbotcount;%i++)
	commandtoclient(%client,'updateGUI',"PatrolBotList","listRow",%client.pbot[%i+1].number,%client.pbot[%i+1].name TAB  %client.pbot[%i+1].number);
}