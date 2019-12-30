function serverCmdSelectObject(%client)
{
   // mouseVec = vector from camera point to 3d mouse coords (normalized)
   %mouseVec = %client.player.getEyeVector();
   // cameraPoint = the world position of the camera
   %cameraPoint = %client.player.getEyePoint();
   //Determine how far should the picking ray extend into the world?
   %selectRange = 4;
   // scale mouseVec to the range the player is able to select with mouse
   %mouseScaled = VectorScale(%mouseVec, %selectRange);
   // cameraPoint = the world position of the camera
   // rangeEnd = camera point + length of selectable range
   %rangeEnd = VectorAdd(%cameraPoint, %mouseScaled);

   // Search for anything that is selectable – below are some examples
   %searchMasks = $TypeMasks::PlayerObjectType | $TypeMasks::CorpseObjectType | $TypeMasks::ItemObjectType | $TypeMasks::TriggerObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::ShapeBaseObjectType | $TypeMasks::FXBrickObjectType;

   // Search for objects within the range that fit the masks above
   // If we are in first person mode, we make sure player is not selectable by setting fourth parameter (exempt
   // from collisions) when calling ContainerRayCast
   %player = %client.player;
   if ($firstPerson)
   {
	  %scanTarg = ContainerRayCast (%cameraPoint, %rangeEnd, %searchMasks, %player);
   }
   else //3rd person - player is selectable in this case
   {
	  %scanTarg = ContainerRayCast (%cameraPoint, %rangeEnd, %searchMasks);
   }

   // a target in range was found so select it
   if (%scanTarg)
   {
      %targetObject = firstWord(%scanTarg);
      if(%targetObject.getClassName() $= "Player")
      {
       %str = "<color:ffff00>" @ %targetObject.client.name @ "<color:ffffff>:" SPC strReplace(%targetObject.client.line,"%1",%client.name);
       %str2 = strReplace(%targetObject.line,"%1",%client.name);
       messageClient(%client,"",%str);
       messageClient(%targetObject.client,"","<color:ffff00>" @ %client.name @ " is trying to get your attention...");
       }
      if(%targetObject.getClassName() $= "AIPlayer")
      {
       %str = "<color:ffff00>" @ %targetObject.getShapeName() @ "<color:ffffff>:" SPC strReplace(%targetObject.line,"%1",%client.name);
       %str2 = strReplace(%targetObject.line,"%1",%client.name);
       if (%str2!$="" && %str2!$=" " && %str2!$="  "){
       messageClient(%client,"",%str);}
       eval(%targetObject.code);
       if(%targetObject.answer_1 !$= "")
       {
	%client.botwait=%targetObject;
       }
      }
      if(%targetObject.getClassName() $= "fxDTSBrick")
      {
	if(%targetObject.effect $= "Door"){%targetObject.setColliding(0);%targetObject.setColor(17);schedule(2000,0,"close",%targetObject);}
	if(%targetObject.effect $= "Teleport"){%targetObject.emitter.setEmitterDataBlock(PlayerBubbleEmitter);schedule(1000,0,"teleport1",%targetObject,%client.player);}
      }
     }
    }

function close(%targetObject)
{
 %targetObject.setColliding(1);%targetObject.setColor(0);
}

function teleport1(%targetObject,%player)
{
 %targetObject.emitter.setEmitterDataBlock(PlayerFoamEmitter);
 %targetObject.telebrick.emitter.setEmitterDataBlock(PlayerBubbleEmitter);schedule(1000,0,"teleport2",%targetObject);
 %pos=%targetObject.telebrick.position;
 %pos2=getword(%pos,0) SPC getword(%pos,1) SPC getword(%pos,2)+1;
 %player.position=%pos2;
}

function teleport2(%targetObject)
{
 %targetObject.telebrick.emitter.setEmitterDataBlock(PlayerFoamEmitter);
}

package BotTalk { //Credit to Randy for the SubString function
	function SubString(%string, %substring){
		for(%i=0;%i<=strlen(%string) - strlen(%substring);%i++){
			if(getSubStr(%string, %i, strlen(%substring)) $= %substring)
				return 1;
		}
		return 0;
	}
	function serverCmdMessageSent(%client, %text)
	{
		if(!isObject(%client.botwait))
		{
			Parent::serverCmdMessageSent(%client, %text);
			return;
		}
		%mes1 = %client.player.position;
		%mes2 = %client.botwait.position;
		%vec = VectorSub(%mes1, %mes2);
		%len = VectorLen(%vec);
		if(%len > 6){messageClient(%client,"","\c5You are too far away.");%client.botwait=0;return;}
       		%str = "<color:ffff00>" @ %client.name @ "<color:ffffff>:" SPC %text;
       		messageClient(%client,"",%str);
		for(%i=1;%i<=%client.botwait.lines;%i++)
		{
				echo(%i);echo(%client.botwait.reply_[%i]);echo(%client.botwait.replyline_[%i]);echo(%client.botwait.answer_[%i]);
			if(%client.botwait.answer_[%i] !$= "" && subString(%text,%client.botwait.answer_[%i]))
			{
				eval(%client.botwait.reply_[%i]);
				if(%client.botwait.replyline_[%i] !$= "")
					messageclient(%client,"",%str = "<color:ffff00>" @ %client.botwait.getShapeName() @ "<color:ffffff>:" SPC strReplace(%client.botwait.replyline_[%i],"%1",%client.name));
			}
		}
		%client.botwait = 0;
	}
};
ActivatePackage(BotTalk);

//Sets your Automessage line.
function servercmdLine(%client,%line)
{
 %client.line = strReplace(%line,"_"," ");
 messageclient(%client,"","\c0Your line set to \"" @ %client.line @ "\".");
}

//Makes a You-Clone.
function servercmdCloneMe(%client)
{
 if(isObject(%client.mybot) && !%client.isSuperAdmin){%client.mybot.delete();messageclient(%client,"","\c5You can only have one bot at a time. Bot " @ %client.mybot.name @ " deleted.");}
 %client.player.name=%client.name;
 if(%client.botnum $= ""){%client.botnum = 0;}
 if(%client.isSuperAdmin){%client.mybot[%client.botnum]=%client.player;%client.botnum++;}else{%client.mybot=%client.player;}
 messageclient(%client,"","ID:" SPC %client.player);
 %client.createPlayer();
 %client.player.kill();
}

function Blockhead(%player) //Makes %player's appearance into the generic Blockhead appearance
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

%player.hideNode("headSkin");
%player.hideNode("LSki");
%player.hideNode("RSki");
%player.hideNode("skirtTrimLeft");
%player.hideNode("skirtTrimRight");

%player.unhidenode("chest");
%player.setNodeColor("chest","1 1 1 1");
%player.unhidenode("rarm");
%player.setNodeColor("rarm", "1 0 0 1");
%player.unhidenode("larm");
%player.setNodeColor("larm", "1 0 0 1");
%player.unhidenode("rhand");
%player.setNodeColor("rhand", "1 0.878431 0.611765 1");
%player.unhidenode("lhand");
%player.setNodeColor("lhand", "1 0.878431 0.611765 1");
%player.unhidenode("pants");
%player.setNodeColor("pants", "0 0 1 1");
%player.unhidenode("rshoe");
%player.setNodeColor("rshoe", "0 0 1 1");
%player.unhidenode("lshoe");
%player.setNodeColor("lshoe", "0 0 1 1");
%player.setDecalName("AAA-None");
%player.unhidenode("headskin");
%player.setNodeColor("headskin", "1 0.878431 0.611765 1");
%player.setFaceName("smileyCreepy");
}
}

//Makes a generic bot with name %name who says line %line.
function servercmdmakebot(%client,%name,%line)
{
 if(isObject(%client.mybot) && !%client.isSuperAdmin){%client.mybot.delete();messageclient(%client,"","\c5You can only have one bot at a time. Bot " @ %client.mybot.name @ " deleted.");}
 %player = new AIPlayer(){datablock=PlayerNoJet;};
 if(%client.botnum $= ""){%client.botnum = 0;}
 if(%client.isSuperAdmin){%client.mybot[%client.botnum]=%player;%client.botnum++;}else{%client.mybot=%player;}
 if($botnum $= ""){$botnum = 0;}
 $bots[$botnum++] = %player;
%pos = %client.player.position;
%player.client=%player;
%player.client.player=%player;
%player.client.name=strReplace(%name,"_"," ");
%player.position= getword(%pos,0) SPC getword(%pos,1) SPC getword(%pos,2);
%player.aimloc= getword(%pos,0) SPC getword(%pos,1)+17.4 SPC getword(%pos,2);
%player.setAimLocation(%player.aimloc);

 %player.line = strReplace(%line,"_"," ");
 messageclient(%client,"","\c0" @ %player.client.name @ "'s line has been set to \"" @ %player.line @ "\".");

messageclient(%client,"","ID:" SPC %player);
%player.setShapeName(%player.client.name);
Blockhead(%player);
}



//ENEMY BOTS - makefollowbot(position,name of bot,who to attack,datablock,weaponimage)
function makefollowbot(%pos,%name,%followobj,%data,%weapon)
{
%player = new AIPlayer(){datablock=%data;};
%player.client=%player;
%player.client.player=%player;
%player.client.name=%name;
%player.client.player.name=%name;
%player.position=%pos;
servercmdZombie(%player);
%player.setShapeName(%name);
if (isObject(%player))
{
Blockhead(%player);
%player.mountImage(%weapon,0);
%player.client = %player;
%player.client.minigame = %followobj.client.minigame;
%player.setImageTrigger(0,1);
%player.setMoveObject(%followobj);
%player.setAimObject(%followobj);
enemyAI(%player,%followobj,%trigger);
return %player;
}
}

//Command for clients to make a bot of name %name following %followname if admin or just the player
function servercmdmakefollowbot(%client,%name,%followname)
{
 if(isObject(%client.mybot) && !%client.isSuperAdmin){%client.mybot.delete();messageclient(%client,"","\c5You can only have one bot at a time. Bot " @ %client.mybot.name @ " deleted.");}
 %player = new AIPlayer(){datablock=PlayerNoJet;};
 %player.owner=%client;
 if(%client.botnum $= ""){%client.botnum = 0;}
 if(%client.isSuperAdmin){%client.mybot[%client.botnum]=%player;%client.botnum++;}else{%client.mybot=%player;}
%player.client=%player;
%player.client.player=%player;
%player.client.name=strReplace(%name,"_"," ");
servercmdZombie(%player);
%pos = %client.player.position;
%player.position= getword(%pos,0) SPC getword(%pos,1) SPC getword(%pos,2)+3;
%player.setShapeName(%player.client.name);
if (isObject(%player))
{
Blockhead(%player);
%player.client = %player;
%player.client.player = %player;
if(%client.isSuperAdmin && PlayerByName(%followname))
enemyAI2(%player,%followname);
else
enemyAI2(%player,%client.name);
return %player;
}
}

//Enemy AI script. Kills self and removes id from trigger if followobj is dead. Also makes guns fire once a second.
function enemyAI(%player,%followobj,%trigger)
{
 if(!isObject(%player)){return;}
 %pos = %followobj.position;
 %player.setaimLocation(getword(%pos,0) SPC getword(%pos,1) SPC getword(%pos,2)+2);
 %player.setmovedestination(getword(%pos,0) SPC getword(%pos,1) SPC getword(%pos,2)+1);
 %player.setImageTrigger(0,0);
 %player.setImageTrigger(0,1);
 if(!isObject(%followobj))
 {
  %trigger.str = strReplace(%trigger.str," " @ %followobj,"");
  %trigger.strwords--; 
  %player.kill();
  return;
 }
 schedule(1000,0,"enemyAI",%player,%followobj,%trigger);
}

//Enemy AI 2 for following %followname but no trigger code/weapon code
function enemyAI2(%player,%followname)
{
 if(!isObject(%player)){return;}
 %pl=PlayerByName(%followname);
 if(%pl)
 {
 %pos = %pl.position;
 %player.setaimLocation(getword(%pos,0) SPC getword(%pos,1) SPC getword(%pos,2)+2);
 %player.setmovedestination(getword(%pos,0) SPC getword(%pos,1) SPC getword(%pos,2)+1);
 schedule(1000,0,"enemyAI2",%player,%followname);
 }
 else
 {
  %player.kill();
  %player.owner.botnum--;
 }
}

//Support to find a client's player via a name. 0 = not found or 2 the same. Otherwise return the player object
function PlayerByName(%name)
{
			if(%name $= "")
				return 0; //No name?
			for(%i=0;%i<ClientGroup.getCount();%i++){
				%cl = ClientGroup.getObject(%i);
				if(SubString(%cl.name, %name)){
					if(%victim){
						return 0; //More than one
					} else {
						%victim = %cl;
					}
				}
			}
			if(%victim){
				return %victim.player; //Found
			} else {
				return 0; //Noone with that name
			}
}

//Trigger to make a monster.
datablock TriggerData(MonsterMakeTrigger)
{
   tickPeriodMS = 100;
};

function MonsterMakeTrigger::onTickTrigger(%this,%trigger)
{
 for(%i=0;%i<%trigger.strwords+1;%i++)
 {
  %obj=getword(%trigger.str,%i);
  if(getRandom(0,1) > 0){%item=nametoID("swordImage");}else{%item=nametoID("gunImage");}
 if(isObject(%obj) && !isObject(%obj.bot) && %obj.getClassName() $= "Player" && %obj.client.player == %obj)
 %obj.bot = makefollowbot(%trigger.pos,"Blockhead",%obj,PlayerNoJet,%item,%trigger);
 }
}


function MonsterMakeTrigger::onEnterTrigger(%this,%trigger,%obj)
{
 if(%obj.getClassName() $= "Player" && %obj.client.player == %obj){
 %trigger.str = %trigger.str SPC %obj;
 %trigger.strwords++;
 %scaleX = getword(%trigger.getScale(),0);
 %scaleY = getword(%trigger.getScale(),1);
 %scaleZ = getword(%trigger.getScale(),2);
 if(getRandom(0,1) > 0){%item=nametoID("swordImage");}else{%item=nametoID("gunImage");}
 %trigger.pos = getword(%trigger.position,0)+%scaleX/2 SPC getword(%trigger.position,1)-%scaleY/2 SPC getword(%trigger.position,2)+%scaleZ/2;
 %obj.bot = makefollowbot(%trigger.pos,"Blockhead",%obj,PlayerNoJet,%item);}
}

function MonsterMakeTrigger::onLeaveTrigger(%this,%trigger,%obj)
{
 if(%obj.getClassName() $= "Player" && %obj.client.player == %obj){
 %trigger.str = strReplace(%trigger.str," " @ %obj,"");
 %trigger.strwords--;
 %obj.bot.kill();}
}

function servercmddeletebot(%client)
{
 if(!%client.isSuperAdmin)
 {
  if(isObject(%client.mybot)){%client.mybot.delete();messageclient(%client,"","\c5Bot " @ %client.mybot @ " deleted.");}else{%client.mybot.delete();messageclient(%client,"","\c5You have no bots.");}
 }
 else
 {
   if(%client.botnum){%client.mybot[%client.botnum-1].delete();%client.botnum--;}else{%client.mybot.delete();messageclient(%client,"","\c5You have no bots.");}
 }
}

function servercmdbotshelp(%client)
{
 messageclient(%client,"","\c0Bot Commands:\c5\n/selectobject: \'Talk\' to one of the bots while facing it and see a person\'s automessage. (See below)");
 messageclient(%client,"","\c5/makebot Name Message: Makes a bot with name NAME and talk message MESSAGE. Replace spaces in the message with underscores.");
 messageclient(%client,"","\c5/makefollowbot Name [Name to Follow]: Makes a follow bot with name NAME who follows you or [Name] if you are a Super Admin.");
 messageclient(%client,"","\c5/cloneme: Creates a clone of you who says your automessage when talked to.");
 messageclient(%client,"","\c5/deletebot: Deletes your bot. Super Admins can create multiple bots deleted in order.");
 messageclient(%client,"","\c5/line MESSAGE: Sets the automessage sent to a person when /selectobject\'d.");
 messageclient(%client,"","\c5Notes: After /selectobject\'ing a bot with set replies, you can say another line to it with certain words in to trigger responses.");
}

function servercmdSaveBots(%client,%filename)
{
 if(!%client.isSuperAdmin){return;}
 messageclient(%client,"","\c5Saving all static bots to file \c3Add-Ons/Client/" @ %filename @ ".bots\c5...");
 %file=new fileObject();
 %file.openForWrite("Add-Ons/Client/" @ %filename @ ".bots");
 for(%i=0;%i<=$botnum;%i++)
 {
  if(isObject($bots[%i]))
  {
  	 %file.writeLine("BOT");
  	 %file.writeLine("NAME" SPC $bots[%i].name);
  	 %file.writeLine("POS" SPC $bots[%i].getTransform());
  	 %file.writeLine("LINE" SPC $bots[%i].line);
  	 %file.writeLine("CODE" SPC $bots[%i].code);
  	 for(%j=1;%j<=$bots[%i].lines;%j++)
  	 {
		if($bots[%i].answer_[%j] !$= "")
		{
			 %file.writeLine("ANSWER " @ %j);
			 %file.writeLine("TRIGGER" SPC $bots[%i].answer_[%j]);
			 %file.writeLine("REPLINE" SPC $bots[%i].replyline_[%j]);
			 %file.writeLine("REPCODE" SPC $bots[%i].reply_[%j]);
		}
	   }
  }
 }
 %file.close();
 %file.delete();
 messageclient(%client,"","\c5Finished saving.");
}

function servercmdLoadBots(%client,%filename)
{
 if(!%client.isSuperAdmin){return;}
 echo("START");
 messageclient(%client,"","\c5Loading static bots from file \c3Add-Ons/Client/" @ %filename @ ".bots\c5...");
 %file=new fileObject();
 %file.openForRead("Add-Ons/Client/" @ %filename @ ".bots");
 while(!%file.isEOF())
 {
 	%line=%file.readLine();
	echo(%line);
	 switch$(firstWord(%line))
	 {
	  	case "BOT":
			%client.mybot[(%client.botnum++)-1] = new AIPlayer(){datablock=PlayerNoJet;};
			%client.mybot[%client.botnum-1].client=%player;
			%client.mybot[%client.botnum-1].client.player=%player;
			Blockhead(%client.mybot[%client.botnum-1]);
	  	case "NAME": 
			%client.mybot[%client.botnum-1].name = restWords(%line);
			%client.mybot[%client.botnum-1].setShapeName(%client.mybot[%client.botnum-1].name);
	  	case "POS": 
			%client.mybot[%client.botnum-1].setTransform(restWords(%line));
	  	case "LINE": 
			%client.mybot[%client.botnum-1].line = restWords(%line);
	  	case "CODE": 
			%client.mybot[%client.botnum-1].code = restWords(%line);
	  	case "ANSWER": 
			%client.mybot[%client.botnum-1].lines = restWords(%line);
	  	case "TRIGGER": 
			%client.mybot[%client.botnum-1].answer_[%client.mybot[%client.botnum-1].lines] = restWords(%line);
	  	case "REPLINE": 
			%client.mybot[%client.botnum-1].replyline_[%client.mybot[%client.botnum-1].lines] = restWords(%line);
	  	case "REPCODE": 
			%client.mybot[%client.botnum-1].reply_[%client.mybot[%client.botnum-1].lines] = restWords(%line);
	  	default: messageclient(%client,"","\c0ERROR: \c3Empty line.");
	 }
	commandtoserver('updatebotgui');
 }
 %file.close();
 %file.delete();
 messageclient(%client,"","\c5Finished loading.");
 echo("END");
}

function servercmdDeleteAllBots(%client)
{
 if(!%client.isSuperAdmin){return;}
 messageall("","\c3" @ %client.name @ "\c0 has deleted all bots.");
	for(%i=0;%i<ClientGroup.getCount();%i++){
		%cl = ClientGroup.getObject(%i);
		for(%j=0;%j<=%cl.botnum+1;%j++){servercmddeletebot(%cl);}
		}
}