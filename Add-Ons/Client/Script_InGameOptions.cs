$IGSOColor[0] = "53 154 255 255";
$IGSOColor[1] = "255 40 40 255";
$IGSOColor[2] = "40 255 40 255";
$IGSOColor[3] = "255 255 40 255";
$IGSOColor[4] = "255 128 40 255";
$IGSOColor[5] = "128 40 128 255";

if(!$Pref::IGSO::OnColor || !$Pref::IGSO::OffColor)
{
	$Pref::IGSO::OnColor = $IGSOColor[0];
	$Pref::IGSO::OffColor = $IGSOColor[1];
}

if(!isObject(InGameOptions))
{
	exec("./InGameOptions.gui");
}

if (!$addedInGameMaps)
{
	$remapDivision[$remapCount] = "IGSO";
	$remapName[$remapCount] = "Toggle Game Options Window";
	$remapCmd[$remapCount] = "toggleInGameWindow";
	$remapCount++;
	$remapName[$remapCount] = "Admin Chat";
	$remapCmd[$remapCount] = "AdminChat";
	$remapCount++;
	$remapName[$remapCount] = "Pm";
	$remapCmd[$remapCount] = "PmChat";
	$remapCount++;
	$addedInGameMaps = true;
}

function toggleInGameWindow(%val)
{
	if(%val)
	{
		switch(InGameOptions.isAwake())
		{
			case 0:
				canvas.pushdialog(InGameOptions);
				InGameOptions.setPane("IGSO");
			case 1:
				canvas.popdialog(InGameOptions);
		}
	}
}

function InGameOptions::onSleep(%this)
{
	if(MessagesList.RowCount() > 0)
	{
		SavePm();
	}
}

function setButtonColor()
{
	%SelOnColor = popOnColor.getSelected();
	%SelOffColor = popOffColor.getSelected();
	$Pref::IGSO::OnColor = $IGSOColor[%SelOnColor];
	$Pref::IGSO::OffColor = $IGSOColor[%SelOffColor];
	btnPort.mColor = $Pref::IGSO::OnColor;
	btnAdminPass.mColor = $Pref::IGSO::OnColor;
	btnSuperAdminPass.mColor = $Pref::IGSO::OnColor;
	btnPass.mColor = $Pref::IGSO::OnColor;
	btnSvrName.mColor = $Pref::IGSO::OnColor;
	btnWelcome.mColor = $Pref::IGSO::OnColor;
	btnClearChat.mColor = $Pref::IGSO::OffColor;
	btnButtonColor.mColor = $Pref::IGSO::OnColor;
	btnBrickLimit.mColor = $Pref::IGSO::OnColor;
	btnBrickMessage.mColor = $Pref::IGSO::OnColor;
	btnMaxP.mColor = $Pref::IGSO::OnColor;
	btnMaxSub.mColor = $Pref::IGSO::OffColor;
	btnPmSend.mColor = $Pref::IGSO::OnColor;
	btnClearPm.mColor = $Pref::IGSO::OffColor;
	btnResetFilter.mColor = $Pref::IGSO::OnColor;
	btnClearFilter.mColor = $Pref::IGSO::OnColor;
	btnAddFilter.mColor = $Pref::IGSO::OnColor;
	btnRemoveFilter.mColor = $Pref::IGSO::OffColor;
	btnAddAdmin.mColor = $Pref::IGSO::OnColor;
	btnRemoveAdmin.mColor = $Pref::IGSO::OffColor;
	btnAddSuperAdmin.mColor = $Pref::IGSO::OnColor;
	btnRemoveSuperAdmin.mColor = $Pref::IGSO::OffColor;
	btnAutoAdmin.mColor = $Pref::IGSO::OnColor;
	btnAutoSuper.mColor = $Pref::IGSO::OnColor;
	btnUnAutoAdmin.mColor = $Pref::IGSO::OffColor;
	btnUnAutoSuper.mColor = $Pref::IGSO::OffColor;
	CommandToServer('UpdateColors');
}

function clientCmdUpdateButtonColor(%Name,%Toggle)
{
	%Button = "btn"@%Name;
	switch(%Toggle)
	{
		case 0: %Button.mColor = $Pref::IGSO::OffColor;
		case 1: %Button.mColor = $Pref::IGSO::OnColor;
		
	}
}

function setPref(%Pref)
{
	%TextEd = "txt"@%Pref;
	%Value = %TextEd.getValue();
	%Cmd = addTaggedString(%Pref);
	commandtoserver(%Cmd,%Value);
}

function InGameOptions::setPane(%this, %pane)
{
	IGSOPane.setVisible(false);
	AdminPane.setVisible(false);
	filterPane.setVisible(false);
	PmPane.setVisible(false);
	btnIGSOPane.mColor = $Pref::IGSO::OffColor;
	btnAdminPane.mColor = $Pref::IGSO::OffColor;
	btnETardPane.mColor = $Pref::IGSO::OffColor;
	btnCursePane.mColor = $Pref::IGSO::OffColor;
	btnPmPane.mColor = $Pref::IGSO::OffColor;
	switch$(%pane)
	{
		case "IGSO":
			IGSOPane.setVisible(true);
			btnIGSOPane.mColor = $Pref::IGSO::OnColor;
			%this.getColorList();
		case "Admin":
			AdminPane.setVisible(true);
			btnAdminPane.mColor = $Pref::IGSO::OnColor;
			%this.UpdateAdminList();
		case "eTard":
			filterPane.setVisible(true);
			btnETardPane.mColor = $Pref::IGSO::OnColor;
			%this.pane = %pane;
			%this.getFilterList();
		case "Curse":
			filterPane.setVisible(true);
			btnCursePane.mColor = $Pref::IGSO::OnColor;
			%this.pane = %pane;
			%this.getFilterList();
		case "Pm":
			PmPane.setVisible(true);
			btnPmPane.mColor = $Pref::IGSO::OnColor;
			%this.getList("PMList");
	}
	setButtonColor();
	IGSOPanes.setText(%pane @ " Menu");
}

function InGameOptions::getColorList(%this)
{
	popOnColor.clear();
	popOffColor.clear();
	popOnColor.add("Blue",0);
	popOffColor.add("Blue",0);
	popOnColor.add("Red",1);
	popOffColor.add("Red",1);
	popOnColor.add("Green",2);
	popOffColor.add("Green",2);
	popOnColor.add("Yellow",3);
	popOffColor.add("Yellow",3);
	popOnColor.add("Oranage",4);
	popOffColor.add("Oranage",4);
	popOnColor.add("Purple",5);
	popOffColor.add("Purple",5);
	for(%i = 0; %i<5; %i++)
	{
		if($Pref::IGSO::OnColor $= $IGSOColor[%i])
		{
			popOnColor.setSelected(%i);
		}
		if($Pref::IGSO::OffColor $= $IGSOColor[%i])
		{
			popOffColor.setSelected(%i);
		}
	}
}

function InGameOptions::getList(%this,%List)
{
	%List.Clear();
	%List = get@%List;
	%List = addTaggedString(%List);
	commandToServer(%List);
}

function InGameOptions::UpdateAdminList(%this)
{
	%this.getList("AdminList");
	InServPlayerList.clear();
	%this.getList("AutoAdminList");
	AutoSuperList.clear();
}

function clientCmdUpdateList(%List,%Id,%Name)
{
	%List.addRow(%Id,%Name);
}

function InGameOptions::getFilterList(%this)
{
	switch$(%this.pane)
	{
		case "eTard":
			%string = $Pref::Server::ETardList;
		case "Curse":
			%string = $Pref::Server::CurseList;
	}
	filterList.clear();
	while(strStr(%string, ",") != -1)
	{
		%strStr = strStr(%string, ",");
		%subStr = getSubStr(%string, 0, %strStr);
		%subStr = strreplace(%subStr," ","");
		%subStr = Trim(%subStr);
		filterList.addRow(%i++, %subStr);
		%strLen = strLen(%string);
		%startSubStr = %strStr + 1;
		%string = getSubStr(%string, %startSubStr, %strLen);
	}
}

function InGameOptions::EditAdminList(%this,%Type)
{
	if(%Type $= "Admin" || %Type $= "SuperAdmin")
	{
		%Selected = InServPlayerList.getSelectedId();
	}
	else
	{
		%Selected = AdminList.getSelectedId();
	}
	commandToServer('EditAdminList',%Selected,%Type);
	%this.UpdateAdminList();
}

function InGameOptions::EditAutoList(%this,%Type)
{
	switch$(%Type)
	{
		case "Super":
			%Selected = txtAutoAdminAdd.getValue();
		case "Admin":
			%Selected = txtAutoAdminAdd.getValue();
		case "unSuper":
			%Selected = AutoSuperList.getSelectedId();
		case "unAdmin":
			%Selected = AutoAdminList.getSelectedId();
	}
	commandToServer('EditAutoList',%Selected,%Type);
	%this.UpdateAdminList();
}

function InGameOptions::addFilterList(%this)
{
	%word = addWordCtrl.getValue();
	if(%word $= ""){return;}
	%word = " "@%word@",";
	switch$(%this.pane)
	{
		case "eTard":
			%string = $Pref::Server::ETardList;
			%string2 = $Pref::Server::CurseList;
			if(strStr(%string, %word) == -1 && strStr(%string2, %word) == -1)
			{
				$Pref::Server::ETardList = %string @ %word;
			}
		case "Curse":
			%string = $Pref::Server::CurseList;
			%string2 = $Pref::Server::ETardList;
			if(strStr(%string, %word) == -1 && strStr(%string2, %word) == -1)
			{
				$Pref::Server::CurseList = %string @ %word;
			}
	}
	%this.getFilterList();
}

function InGameOptions::removeFilterList(%this)
{
	%selectedID = filterList.getSelectedID() - 1;
	%rowText = filterList.getRowText(%selectedID);
	%word = %rowText @ ",";
	if(%rowtext $= ""){return;}
	switch$(%this.pane)
	{
		case "eTard":
			%string = $Pref::Server::ETardList;
			$Pref::Server::ETardList = strReplace(%string, %word, "");
		case "Curse":
			%string = $Pref::Server::CurseList;
			$Pref::Server::CurseList = strReplace(%string, %word, "");
	}
	%this.getFilterList();
}

function InGameOptions::clearFilterList(%this)
{
	switch$(%this.pane)
	{
		case "eTard":
			$Pref::Server::ETardList = "";
		case "Curse":
			$Pref::Server::CurseList = "";
	}
	%this.getFilterList();
}

function InGameOptions::resetFilterList(%this)
{
	switch$(%this.pane)
	{
		case "eTard":
			$Pref::Server::ETardList = " u , r , ur , wat , wut , wuts , wit , dat , loel , y ,";
		case "Curse":
			$Pref::Server::CurseList = "shit,fuck,fuk,cunt,nigger,asshole,faggot, fag , suck my , dick ,";
	}
	%this.getFilterList();
}