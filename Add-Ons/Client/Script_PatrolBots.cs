if(!isObject(PatrolBotGui))
	exec("add-ons/client/PatrolBotGui.gui");

if(!$addedPBotMaps){
	$remapDivision[$remapCount] = "Patrol Bots";
		$remapName[$remapCount] = "Open Gui";
		$remapCmd[$remapCount] = "togglePBotWindow";
		$remapCount++;
	$addedPBotMaps = 1;
}

function togglePBotWindow(%val){
	if(%val)
		if(PatrolBotGui.isAwake())
			canvas.popdialog(PatrolBotGui);
		else
			canvas.pushdialog(PatrolBotGui);
}

function PatrolBotGui::OnWake(){
	commandtoserver('getPatrolBotList');
}

function clientcmdUpdateGUI(%obj,%action,%arg1,%arg2){
	switch$(%action)
	{
		case "listClear": %obj.clear();
		case "listRow": %obj.addRow(%arg1, %arg2);
		case "textSet": %obj.setText(%arg1);
	}
}

function buttonPBotDelete(){
	commandtoserver('deletepbot',PatrolBotList.getSelectedID());
	commandtoserver('getPatrolBotList');
}

function buttonPBotCreate(){
	commandtoserver('patrolbot',PatrolBotCreate.getValue());
	commandtoserver('getPatrolBotList');
}