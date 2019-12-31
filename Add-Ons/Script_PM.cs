$PM_ShowClanTags = 1;
$PM_MaxChatLength = 120;
$PM_Prefix = "@";

package PersonalMessages {
	function SubString(%string, %substring){
		return strStr(strLwr(%string), strLwr(%substring)) >= 0 ? 1 : 0;
	}
	function serverCmdPMHelp(%client){
		messageClient(%client, "", 'To send a personal message to somebody, simply type:');
		messageClient(%client, "", '\c0%1\c3(Part of recipient\'s name) \c6(Message)', $PM_Prefix);
		messageClient(%client, "", 'Example:');
		messageClient(%client, "", '\c0%1\c3Bob \c6How are you?', $PM_Prefix);
	}
	function serverCmdMessageSent(%client, %text){
		if(getSubStr(%text, 0, strlen($PM_Prefix)) $= $PM_Prefix){
			%victimnamepart = getsubstr(getWord(%text, 0), strlen($PM_Prefix), strlen(getWord(%text, 0))-strlen($PM_Prefix));
			%messagestart = strlen(%victimnamepart) + 1 + strlen($PM_Prefix);
			%text = getSubStr(trim(getSubStr(%text, %messagestart, strlen(%text) - %messagestart)), 0, $PM_MaxChatLength);
			%sendername = $PM_ShowClanTags ? "\c7" @ %client.clanPrefix @ "\c3" @ %client.name @ "\c7" @ %client.clanSuffix @ "\c3" : "\c3" @ %client.name;
			if(%victimnamepart $= "")
				return;
			for(%i=0;%i<ClientGroup.getCount();%i++){
				%cl = ClientGroup.getObject(%i);
				if(SubString(%cl.name, %victimnamepart)){
					if(%victim){
						messageClient(%client, "", '\c0More than one person was found with \'\c6%1\c0\' in their name. Please be more specfic.', %victimnamepart);
						return;
					} else {
						%victim = %cl;
					}
				}
			}
			if(%victim){
				if(%client == %victim)
					return;
				messageClient(%client, "", '%1(\c0%2\c3)\c6: %3', %sendername, %victim.name, %text);
				messageClient(%victim, "", '%1(\c0PM\c3)\c6: %2', %sendername, %text);
			} else {
				messageClient(%client, "", '\c0Nobody was found with \'\c6%1\c0\' in their name.', %victimnamepart);
				return;
			}
		} else {
			Parent::serverCmdMessageSent(%client, %text);
		}
	}
};
ActivatePackage(PersonalMessages);