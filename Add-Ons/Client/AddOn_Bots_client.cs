if(!isObject(BotGui))
	exec("./BotGui.gui");

if (!$addedBotMaps)
{
	$remapDivision[$remapCount] = "Space Guy's Bot Mod";
	$remapName[$remapCount] = "Toggle GUI";
	$remapCmd[$remapCount] = "toggleBotWindow";
	$remapCount++;
	$remapName[$remapCount] = "Select Object";
	$remapCmd[$remapCount] = "selectObject";
	$remapCount++;
	$addedBotMaps = true;
}

function selectObject()
{
 commandtoserver('selectobject');
}

function toggleBotWindow(%val)
{
	if(%val)
		if(BotGui.isAwake())
			canvas.popdialog(botgui);
		else
			canvas.pushdialog(botgui);
}

function servercmdupdatebotgui(%client)
{
 if(!%client.isSuperAdmin)
 {
  lblDelBotName.setText(%client.mybot.name);
  lblBotLine2.setText(%client.mybot.replyline_[%client.mybot.lines]);
  lblBotCode2.setText(%client.mybot.reply_[%client.mybot.lines]);
  lblBotTrigger2.setText(%client.mybot.answer_[%client.mybot.lines]);
 }
 else
 {
  lblDelBotName.setText(%client.mybot[%client.botnum-1].name);
  lblBotLine2.setText(%client.mybot[%client.botnum-1].replyline_[%client.mybot[%client.botnum-1].lines]);
  lblBotCode2.setText(%client.mybot[%client.botnum-1].answer_[%client.mybot[%client.botnum-1].lines]);
  lblBotTrigger2.setText(%client.mybot[%client.botnum-1].reply_[%client.mybot[%client.botnum-1].lines]);
 }
}

function BotGUI_MakeBot()
{
 commandtoserver('makebot',txtBotName.getValue(),txtBotLine.getValue());
 commandtoserver('updatebotgui');
}

function BotGUI_MakeFollowBot()
{
 commandtoserver('makefollowbot',txtFBotName.getValue(),txtFBotName2.getValue());
 commandtoserver('updatebotgui');
}

function BotGUI_CloneMe()
{
 commandtoserver('cloneme');
 commandtoserver('updatebotgui');
}

function BotGUI_DeleteBot()
{
 commandtoserver('deletebot');
 commandtoserver('updatebotgui');
}

function BotGUI_BotLine()
{
 commandtoserver('line',txtBotLine2.getValue()); 
 commandtoserver('updatebotgui');
}

function servercmdBotCode(%client)
{
 if(!%client.isSuperAdmin)
 {
  %client.mybot.code = txtBotCode.getValue(); 
  messageclient(%client,"","\c0" @ %client.mybot.name @ "\'s line set to:" SPC txtBotCode.getValue());
 }
 else
 {
  %client.mybot[%client.botnum-1].code = txtBotCode.getValue(); 
  messageclient(%client,"","\c0" @ %client.mybot[%client.botnum-1].name @ "\'s line set to:" SPC txtBotCode.getValue());
 }
 commandtoserver('updatebotgui');
}

function servercmdBotReply(%client)
{
 if(!%client.isSuperAdmin)
 {
  %client.mybot.lines++;
  %client.mybot.replyline_[%client.mybot.lines] = txtBotReply.getValue();
  %client.mybot.answer_[%client.mybot.lines] = txtBotTrigWord.getValue();  
  %client.mybot.reply_[%client.mybot.lines] = txtBotReplyCode.getValue();
  messageclient(%client,"","\c0Added reply to:" @ %client.mybot.name);
  messageclient(%client,"","\c0Line said:" SPC txtBotReply.getValue());
  messageclient(%client,"","\c0Trigger Word:" SPC txtBotTrigWord.getValue());
  messageclient(%client,"","\c0Code Executed:" SPC txtBotReplyCode.getValue());
 }
 else
 {
  %client.mybot[%client.botnum-1].lines++;
  %client.mybot[%client.botnum-1].replyline_[%client.mybot[%client.botnum-1].lines] = txtBotReply.getValue();
  %client.mybot[%client.botnum-1].answer_[%client.mybot[%client.botnum-1].lines] = txtBotTrigWord.getValue();  
  %client.mybot[%client.botnum-1].reply_[%client.mybot[%client.botnum-1].lines] = txtBotReplyCode.getValue(); 
  messageclient(%client,"","\c0Added reply to:" @ %client.mybot[%client.botnum-1].name);
  messageclient(%client,"","\c0Line said:" SPC txtBotReply.getValue());
  messageclient(%client,"","\c0Trigger Word:" SPC txtBotTrigWord.getValue());
  messageclient(%client,"","\c0Code Executed:" SPC txtBotReplyCode.getValue());
 }
 commandtoserver('updatebotgui');
}

function servercmdBotDelReply(%client)
{
 if(!%client.isSuperAdmin)
 {
  if(%client.mybot.lines $= "" || %client.mybot.lines $= "0") return;
  %client.mybot.replyline_[%client.mybot.lines] = "";
  %client.mybot.answer_[%client.mybot.lines] = "";
  %client.mybot.reply_[%client.mybot.lines] = "";
  %client.mybot.lines--;
 }
 else
 {
  if(%client.mybot[%client.botnum-1].lines $= "" || %client.mybot[%client.botnum-1].lines $= "0") return;
  %client.mybot[%client.botnum-1].replyline_[%client.mybot[%client.botnum-1].lines] = "";
  %client.mybot[%client.botnum-1].answer_[%client.mybot[%client.botnum-1].lines] = "";
  %client.mybot[%client.botnum-1].reply_[%client.mybot[%client.botnum-1].lines] = "";
  %client.mybot[%client.botnum-1].lines--;
 }
 commandtoserver('updatebotgui');
}

function BotGUI_SaveBots()
{
 commandtoserver('savebots',txtSaveName.getValue());
 commandtoserver('updatebotgui');
}

function BotGUI_LoadBots()
{
 commandtoserver('loadbots',txtLoadName.getValue());
 commandtoserver('updatebotgui');
}
