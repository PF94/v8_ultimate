exec("add-ons/ADD_ON_LIST.cs");

//Client Overrides

package commonClientOverrides
{
	function centerWrenchGUI()
	{
		%screenwidth = getWord(PlayGui.extent,0);
		%screenheight = getWord(PlayGui.extent,1);
		%menuwidth = getWord(jvsGUI_Window.extent,0);
		%menuheight = getWord(jvsGUI_Window.extent,1);

		%xpos = %screenwidth / 2 - %menuwidth / 2;
		%ypos = %screenheight / 2 - %menuheight / 2;

		if(isOdd(%menuwidth))
		{
			%xpos += 0.5;
		}

		if(isOdd(%menuheight))
		{
			%ypos += 0.5;
		}

		jvsGUI_Window.position = %xpos SPC %ypos;
	}

	function getBrickProperties()
	{
		commandtoserver('getBrickProperties');
	}

	function handleClearDoorList(%msgType,%msgString)
	{
		doorselect.clear();
	}

	function handleClearDoorsAndPortals(%msgType,%msgString)
	{
		AlxPlay("DoorsAndPortalsClearProfile");
	}

	function handleCloseWrenchDlg(%msgType,%msgString)
	{
		canvas.popDialog(wrenchdlg);
	}

	function handleDoorOrPortalLoadComplete(%msgType,%msgString)
	{
		AlxPlay("DoorOrPortalLoadCompleteProfile");
	}

	function handleDoorOrPortalLoadStart(%msgType,%msgString)
	{
		AlxPlay("DoorOrPortalLoadStartProfile");
	}

	function handleGetBrickProperties(%msgType,%msgString,%prestrict,%portaldesc,%drestrict,%doordesc,%itemdir,%rotdir,%doortype)
	{
		if(%prestrict == 1)
		{
			restrict1.performClick();
		}
		else if(%prestrict == 2)
		{
			restrict2.performClick();
		}
		else if(%prestrict == 3)
		{
			restrict3.performClick();
		}
		else if(%prestrict == 4)
		{
			restrict4.performClick();
		}
		else
		{
			restrict0.performClick();
		}

		if(%drestrict == 1)
		{
			drestrict1.performClick();
		}
		else if(%drestrict == 2)
		{
			drestrict2.performClick();
		}
		else if(%drestrict == 3)
		{
			drestrict3.performClick();
		}
		else if(%drestrict == 4)
		{
			drestrict4.performClick();
		}
		else
		{
			drestrict0.performClick();
		}

		description.setValue(%portaldesc);
		ddescription.setValue(%doordesc);

		if(%itemdir == 3)
		{
			doordirE.performClick();
		}
		else if(%itemdir == 4)
		{
			doordirS.performClick();
		}
		else if(%itemdir == 5)
		{
			doordirW.performClick();
		}
		else
		{
			doordirN.performClick();
		}

		if(%rotdir == 1)
		{
			doorrot90plus.performClick();
		}
		else
		{
			doorrot90minus.performClick();
		}

		doorselect.setValue(%doortype);
	}

	function handleOpenJVSGUI(%msgType,%msgString)
	{
		canvas.pushDialog(jvsGUI);
	}

	function handleOpenWrenchDlg(%msgType,%msgString)
	{
		canvas.pushDialog(wrenchdlg);
	}

	function handleSortDoorList(%msgType,%msgString)
	{
		doorselect.sort();
	}

	function handleUpdateDoorList(%msgType,%msgString,%displayname,%id)
	{
		doorselect.add(%displayname,%id);
	}

	function isOdd(%int)
	{
		if(%int % 2 == 1)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	function jvsGUI::onSleep()
	{
		resetjvsgui();
		moveMap.bindCmd(keyboard, "escape", "", $escapecommand);
	}

	function jvsGUI::onWake()
	{
		resetjvsgui();
		$escapecommand = moveMap.getCommand(keyboard,"escape");
		moveMap.bindCmd(keyboard, "escape", "", "canvas.popDialog(jvsGUI);");

		if($JVS::ClientMods::Doors)
		{
			doorsystemSWATCH.setVisible(false);
		}
		else
		{
			doorsystemSWATCH.setVisible(true);
		}

		if($JVS::ClientMods::Elevators)
		{
			elevatorsystemSWATCH.setVisible(false);
		}
		else
		{
			elevatorsystemSWATCH.setVisible(true);
		}

		if($JVS::ClientMods::Portals)
		{
			portalsystemSWATCH.setVisible(false);
		}
		else
		{
			portalsystemSWATCH.setVisible(true);
		}
	}

	function lstDoorList::onSelect(%this,%id,%text)
	{
		%brick = lstDoorList.getSelectedId();
		%brick = getWord(%brick,0);
		commandtoserver('GetDoorProperties',%brick);
		doorbuttonsswatch.visible = 0;
		doorsettingsswatch.visible = 0;
		doorlistsetbutton.mColor = "50 255 50 255";
		doorlistclearbutton.mColor = "255 75 0 255";
		description2.makeFirstResponder(true);
	}

	function lstDoorLoaderList::onSelect(%this,%id,%text)
	{
		canvas.popDialog(jvsgui);
		%file = lstDoorLoaderList.getRowTextById(%id);
		%file = "add-ons/jvs/doors/saves/" @ %file @ ".save";
		commandtoserver('loaddoors2',%file);
	}

	function lstPortalList::onSelect(%this,%id,%text)
	{
		%brick = lstPortalList.getSelectedId();
		%brick = getWord(%brick,0);
		commandtoserver('GetPortalProperties',%brick);
		portalsettingsswatch.visible = 0;
		portallistsetbutton.mColor = "50 255 50 255";
		portallistclearbutton.mColor = "255 75 0 255";
		description2.makeFirstResponder(true);
	}

	function lstPortalLoaderList::onSelect(%this,%id,%text)
	{
		canvas.popDialog(jvsgui);
		%file = lstPortalLoaderList.getRowTextById(%id);
		%file = "add-ons/jvs/portals/saves/" @ %file @ ".save";
		commandtoserver('loadportals2',%file);
	}

	function onExit()
	{
		echo("Exporting JVS Server Prefs...");
   		export("$JVS::Server::*", "add-ons/jvs/serverprefs.code", False);
   		export("$JVS::ClientMods::*", "add-ons/jvs/clientmods.code", False);
   		export("$JVS::ServerMods::*", "add-ons/jvs/servermods.code", False);
		parent::onExit();
	}

	function playGui::onWake()
	{
		moveMap.bindCmd(keyboard, "escape", "", "escapeMenu.toggle();");
		parent::onWake();
	}

	function resetJVSGUI()
	{
		doorsystemBTN.mColor = "255 255 255 255";
		doorsystemGUI.setVisible(false);
		doorsystemcreateBTN.mColor = "255 255 255 255";
		doorsystemcreateGUI.setVisible(false);
		doorsystemmanageyoursBTN.mColor = "255 255 255 255";
		doorsystemmanageyoursGUI.setVisible(false);
		doorsystemmanageallBTN.mColor = "255 255 255 255";
		doorsystemmanageallGUI.setVisible(false);
		doorsystemsaveBTN.mColor = "255 255 255 255";
		doorsystemsaveGUI.setVisible(false);
		doorsystemloadBTN.mColor = "255 255 255 255";
		doorsystemloadGUI.setVisible(false);

		elevatorsystemBTN.mColor = "255 255 255 255";
		elevatorsystemGUI.setVisible(false);

		portalsystemBTN.mColor = "255 255 255 255";
		portalsystemGUI.setVisible(false);
		portalsystemcreateBTN.mColor = "255 255 255 255";
		portalsystemcreateGUI.setVisible(false);
		portalsystemmanageyoursBTN.mColor = "255 255 255 255";
		portalsystemmanageyoursGUI.setVisible(false);
		portalsystemmanageallBTN.mColor = "255 255 255 255";
		portalsystemmanageallGUI.setVisible(false);
		portalsystemsaveBTN.mColor = "255 255 255 255";
		portalsystemsaveGUI.setVisible(false);
		portalsystemloadBTN.mColor = "255 255 255 255";
		portalsystemloadGUI.setVisible(false);

		windowsystemBTN.mColor = "255 255 255 255";
		windowsystemGUI.setVisible(false);

		jvsGUI_Window.setText("JVS Mod");
		border1a.setVisible(false);
		border2a.setVisible(false);
		border3a.setVisible(false);
		border4a.setVisible(false);
		opentoggle.mColor = "255 255 255 255";
		closedtoggle.mColor = "255 255 255 255";
		doorbuttonsswatch.visible = 1;
		doorsettingsswatch.visible = 1;
		doorlistsetbutton.mColor = "255 255 255 255";
		doorlistclearbutton.mColor = "255 255 255 255";
		jvsGUI_Window.extent = "554 83";
		centerWrenchGUI();
	}

	function showdoorsystemGUI()
	{
		if(doorsystemGUI.visible == 1)
		{
			resetJVSGUI();
			jvsGUI_Window.extent = "555 83";
		}
		else
		{
			resetJVSGUI();
			doorsystemBTN.mColor = "50 130 255 255";
			doorsystemGUI.setVisible(true);
			jvsGUI_Window.extent = "555 140";
			jvsGUI_Window.setText("JVS Mod > Doors");
			border1a.position = "13 72";
			border1a.extent = getWord(jvsGUI_Window.extent,0) - 26 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13;
			border1b.position = "2 2";
			border1b.extent = getWord(jvsGUI_Window.extent,0) - 26 - 4 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13 - 4;
			border1a.setVisible(true);
			border2a.position = "78 64";
			border2a.extent = "100 10";
			border2b.position = "2 0";
			border2b.extent = "96 10";
			border2a.setVisible(true);
		}

		centerWrenchGUI();
	}

	function showdoorsystemcreateGUI()
	{
		if(doorsystemcreateGUI.isVisible())
		{
			resetJVSGUI();
			doorsystemBTN.mColor = "50 130 255 255";
			doorsystemGUI.setVisible(true);
			doorsystemcreateGUI.setVisible(false);
			jvsGUI_Window.extent = "555 140";
			jvsGUI_Window.setText("JVS Mod > Doors");
			border1a.position = "13 72";
			border1a.extent = getWord(jvsGUI_Window.extent,0) - 26 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13;
			border1b.position = "2 2";
			border1b.extent = getWord(jvsGUI_Window.extent,0) - 26 - 4 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13 - 4;
			border1a.setVisible(true);
			border2a.position = "78 64";
			border2a.extent = "100 10";
			border2b.position = "2 0";
			border2b.extent = "96 10";
			border2a.setVisible(true);
		}
		else
		{
			resetJVSGUI();
			doorsystemBTN.mColor = "50 130 255 255";
			doorsystemGUI.setVisible(true);
			jvsGUI_Window.extent = "555 363";
			getBrickProperties();
			doorsystemcreateBTN.mColor = "50 130 255 255";
			doorsystemcreateGUI.setVisible(true);
			doorselect.sort();
			doorselect.setSelected(-1);
			ddescription.makeFirstResponder(true);
			jvsGUI_Window.setText("JVS Mod > Doors > Create");
			border1a.position = "13 72";
			border1a.extent = getWord(jvsGUI_Window.extent,0) - 26 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13;
			border1b.position = "2 2";
			border1b.extent = getWord(jvsGUI_Window.extent,0) - 26 - 4 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13 - 4;
			border1a.setVisible(true);
			border2a.position = "78 64";
			border2a.extent = "100 10";
			border2b.position = "2 0";
			border2b.extent = "96 10";
			border2a.setVisible(true);
			border3a.position = getWord(doorsystemGUI.position,0) + 28 SPC getWord(doorsystemGUI.position,1) + 34;
			border3a.extent = 500 SPC getWord(jvsGUI_Window.extent,1) - 72 - 35 - 13 - 13 - 12;
			border3b.position = "2 2";
			border3b.extent = 496 SPC getWord(jvsGUI_Window.extent,1) - 72 - 35 - 13 - 13 - 12 - 4;
			border3a.setVisible(true);
			border4a.position = getWord(doorsystemGUI.position,0) + 28 SPC getWord(doorsystemGUI.position,1) + 26;
			border4a.extent = "100 10";
			border4b.position = "2 0";
			border4b.extent = "96 10";
			border4a.setVisible(true);
		}

		centerWrenchGUI();
	}

	function showdoorsystemloadGUI()
	{
		if(doorsystemloadGUI.isVisible())
		{
			resetJVSGUI();
			doorsystemBTN.mColor = "50 130 255 255";
			doorsystemGUI.setVisible(true);
			doorsystemloadGUI.setVisible(false);
			jvsGUI_Window.extent = "555 142";
			jvsGUI_Window.setText("JVS Mod > Doors");
			border1a.position = "13 72";
			border1a.extent = getWord(jvsGUI_Window.extent,0) - 26 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13;
			border1b.position = "2 2";
			border1b.extent = getWord(jvsGUI_Window.extent,0) - 26 - 4 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13 - 4;
			border1a.setVisible(true);
			border2a.position = "78 64";
			border2a.extent = "100 10";
			border2b.position = "2 0";
			border2b.extent = "96 10";
			border2a.setVisible(true);
		}
		else
		{
			resetJVSGUI();
			refreshlist2();
			jvsGUI_Window.extent = "555 363";
			doorsystemBTN.mColor = "50 130 255 255";
			doorsystemGUI.setVisible(true);
			doorsystemloadBTN.mColor = "50 130 255 255";
			doorsystemloadGUI.setVisible(true);
			jvsGUI_Window.setText("JVS Mod > Doors > Load");
			border1a.position = "13 72";
			border1a.extent = getWord(jvsGUI_Window.extent,0) - 26 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13;
			border1b.position = "2 2";
			border1b.extent = getWord(jvsGUI_Window.extent,0) - 26 - 4 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13 - 4;
			border1a.setVisible(true);
			border2a.position = "78 64";
			border2a.extent = "100 10";
			border2b.position = "2 0";
			border2b.extent = "96 10";
			border2a.setVisible(true);
			border3a.position = getWord(doorsystemGUI.position,0) + 28 SPC getWord(doorsystemGUI.position,1) + 34;
			border3a.extent = 500 SPC getWord(jvsGUI_Window.extent,1) - 72 - 35 - 13 - 13 - 12;
			border3b.position = "2 2";
			border3b.extent = 496 SPC getWord(jvsGUI_Window.extent,1) - 72 - 35 - 13 - 13 - 12 - 4;
			border3a.setVisible(true);
			border4a.position = getWord(doorsystemGUI.position,0) + 428 SPC getWord(doorsystemGUI.position,1) + 26;
			border4a.extent = "100 10";
			border4b.position = "2 0";
			border4b.extent = "96 10";
			border4a.setVisible(true);
		}

		centerWrenchGUI();
	}

	function showdoorsystemmanageallGUI()
	{
		if(doorsystemmanageallGUI.isVisible())
		{
			resetJVSGUI();
			doorsystemBTN.mColor = "50 130 255 255";
			doorsystemGUI.setVisible(true);
			doorsystemmanageallGUI.setVisible(false);
			jvsGUI_Window.extent = "555 142";
			jvsGUI_Window.setText("JVS Mod > Doors");
			border1a.position = "13 72";
			border1a.extent = getWord(jvsGUI_Window.extent,0) - 26 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13;
			border1b.position = "2 2";
			border1b.extent = getWord(jvsGUI_Window.extent,0) - 26 - 4 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13 - 4;
			border1a.setVisible(true);
			border2a.position = "78 64";
			border2a.extent = "100 10";
			border2b.position = "2 0";
			border2b.extent = "96 10";
			border2a.setVisible(true);
		}
		else
		{
			resetJVSGUI();
			jvsGUI_Window.extent = "555 567";
			doorsystemBTN.mColor = "50 130 255 255";
			doorsystemGUI.setVisible(true);
			doorsystemmanageallBTN.mColor = "50 130 255 255";
			doorsystemmanageallGUI.setVisible(true);
			commandtoserver('managedoorlist');
			jvsGUI_Window.setText("JVS Mod > Doors > Manage All");
			border1a.position = "13 72";
			border1a.extent = getWord(jvsGUI_Window.extent,0) - 26 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13;
			border1b.position = "2 2";
			border1b.extent = getWord(jvsGUI_Window.extent,0) - 26 - 4 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13 - 4;
			border1a.setVisible(true);
			border2a.position = "78 64";
			border2a.extent = "100 10";
			border2b.position = "2 0";
			border2b.extent = "96 10";
			border2a.setVisible(true);
			border3a.position = getWord(doorsystemGUI.position,0) + 28 SPC getWord(doorsystemGUI.position,1) + 34;
			border3a.extent = 500 SPC getWord(jvsGUI_Window.extent,1) - 72 - 35 - 13 - 13 - 12;
			border3b.position = "2 2";
			border3b.extent = 496 SPC getWord(jvsGUI_Window.extent,1) - 72 - 35 - 13 - 13 - 12 - 4;
			border3a.setVisible(true);
			border4a.position = getWord(doorsystemGUI.position,0) + 228 SPC getWord(doorsystemGUI.position,1) + 26;
			border4a.extent = "100 10";
			border4b.position = "2 0";
			border4b.extent = "96 10";
			border4a.setVisible(true);
		}

		centerWrenchGUI();
	}

	function showdoorsystemmanageyoursGUI()
	{
		if(doorsystemmanageyoursGUI.isVisible())
		{
			resetJVSGUI();
			doorsystemBTN.mColor = "50 130 255 255";
			doorsystemGUI.setVisible(true);
			doorsystemmanageallGUI.setVisible(false);
			jvsGUI_Window.extent = "555 142";
			jvsGUI_Window.setText("JVS Mod > Doors");
			border1a.position = "13 72";
			border1a.extent = getWord(jvsGUI_Window.extent,0) - 26 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13;
			border1b.position = "2 2";
			border1b.extent = getWord(jvsGUI_Window.extent,0) - 26 - 4 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13 - 4;
			border1a.setVisible(true);
			border2a.position = "78 64";
			border2a.extent = "100 10";
			border2b.position = "2 0";
			border2b.extent = "96 10";
			border2a.setVisible(true);
		}
		else
		{
			resetJVSGUI();
			commandtoserver('managemydoors2');
			doorsystemBTN.mColor = "50 130 255 255";
			doorsystemGUI.setVisible(true);
			jvsGUI_Window.extent = "555 499";
			doorsystemmanageyoursBTN.mColor = "50 130 255 255";
			doorsystemmanageyoursGUI.setVisible(true);
			doorselect2.sort();
			doorselect2.setSelected(-1);
			jvsGUI_Window.setText("JVS Mod > Doors > Manage Yours");
			border1a.position = "13 72";
			border1a.extent = getWord(jvsGUI_Window.extent,0) - 26 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13;
			border1b.position = "2 2";
			border1b.extent = getWord(jvsGUI_Window.extent,0) - 26 - 4 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13 - 4;
			border1a.setVisible(true);
			border2a.position = "78 64";
			border2a.extent = "100 10";
			border2b.position = "2 0";
			border2b.extent = "96 10";
			border2a.setVisible(true);
			border3a.position = getWord(doorsystemGUI.position,0) + 28 SPC getWord(doorsystemGUI.position,1) + 34;
			border3a.extent = 500 SPC getWord(jvsGUI_Window.extent,1) - 72 - 35 - 13 - 13 - 12;
			border3b.position = "2 2";
			border3b.extent = 496 SPC getWord(jvsGUI_Window.extent,1) - 72 - 35 - 13 - 13 - 12 - 4;
			border3a.setVisible(true);
			border4a.position = getWord(doorsystemGUI.position,0) + 128 SPC getWord(doorsystemGUI.position,1) + 26;
			border4a.extent = "100 10";
			border4b.position = "2 0";
			border4b.extent = "96 10";
			border4a.setVisible(true);
		}

		centerWrenchGUI();
	}

	function showdoorsystemsaveGUI()
	{
		if(doorsystemsaveGUI.isVisible())
		{
			resetJVSGUI();
			doorsystemBTN.mColor = "50 130 255 255";
			doorsystemGUI.setVisible(true);
			doorsystemsaveGUI.setVisible(false);
			jvsGUI_Window.extent = "555 142";
			jvsGUI_Window.setText("JVS Mod > Doors");
			border1a.position = "13 72";
			border1a.extent = getWord(jvsGUI_Window.extent,0) - 26 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13;
			border1b.position = "2 2";
			border1b.extent = getWord(jvsGUI_Window.extent,0) - 26 - 4 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13 - 4;
			border1a.setVisible(true);
			border2a.position = "78 64";
			border2a.extent = "100 10";
			border2b.position = "2 0";
			border2b.extent = "96 10";
			border2a.setVisible(true);
		}
		else
		{
			resetJVSGUI();
			txteditName2.setValue("");
			jvsGUI_Window.extent = "555 270";
			doorsystemBTN.mColor = "50 130 255 255";
			doorsystemGUI.setVisible(true);
			doorsystemsaveBTN.mColor = "50 130 255 255";
			doorsystemsaveGUI.setVisible(true);
			txteditName2.makeFirstResponder(true);
			jvsGUI_Window.setText("JVS Mod > Doors > Save");
			border1a.position = "13 72";
			border1a.extent = getWord(jvsGUI_Window.extent,0) - 26 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13;
			border1b.position = "2 2";
			border1b.extent = getWord(jvsGUI_Window.extent,0) - 26 - 4 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13 - 4;
			border1a.setVisible(true);
			border2a.position = "78 64";
			border2a.extent = "100 10";
			border2b.position = "2 0";
			border2b.extent = "96 10";
			border2a.setVisible(true);
			border3a.position = getWord(doorsystemGUI.position,0) + 28 SPC getWord(doorsystemGUI.position,1) + 34;
			border3a.extent = 500 SPC getWord(jvsGUI_Window.extent,1) - 72 - 35 - 13 - 13 - 12;
			border3b.position = "2 2";
			border3b.extent = 496 SPC getWord(jvsGUI_Window.extent,1) - 72 - 35 - 13 - 13 - 12 - 4;
			border3a.setVisible(true);
			border4a.position = getWord(doorsystemGUI.position,0) + 328 SPC getWord(doorsystemGUI.position,1) + 26;
			border4a.extent = "100 10";
			border4b.position = "2 0";
			border4b.extent = "96 10";
			border4a.setVisible(true);
		}

		centerWrenchGUI();
	}

	function showelevatorsystemGUI()
	{
		resetJVSGUI();
		elevatorsystemBTN.mColor = "50 130 255 255";
		elevatorsystemGUI.setVisible(true);
	}

	function showportalsystemGUI()
	{
		if(portalsystemGUI.visible == 1)
		{
			resetJVSGUI();
			jvsGUI_Window.extent = "555 83";
		}
		else
		{
			resetJVSGUI();
			portalsystemBTN.mColor = "50 130 255 255";
			portalsystemGUI.setVisible(true);
			jvsGUI_Window.extent = "555 140";
			jvsGUI_Window.setText("JVS Mod > Portals");
			border1a.position = "13 72";
			border1a.extent = getWord(jvsGUI_Window.extent,0) - 26 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13;
			border1b.position = "2 2";
			border1b.extent = getWord(jvsGUI_Window.extent,0) - 26 - 4 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13 - 4;
			border1a.setVisible(true);
			border2a.position = "278 64";
			border2a.extent = "100 10";
			border2b.position = "2 0";
			border2b.extent = "96 10";
			border2a.setVisible(true);
		}

		centerWrenchGUI();
	}

	function showportalsystemcreateGUI()
	{
		if(portalsystemcreateGUI.isVisible())
		{
			resetJVSGUI();
			portalsystemBTN.mColor = "50 130 255 255";
			portalsystemGUI.setVisible(true);
			portalsystemcreateGUI.setVisible(false);
			jvsGUI_Window.extent = "555 140";
			jvsGUI_Window.setText("JVS Mod > Portals");
			border1a.position = "13 72";
			border1a.extent = getWord(jvsGUI_Window.extent,0) - 26 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13;
			border1b.position = "2 2";
			border1b.extent = getWord(jvsGUI_Window.extent,0) - 26 - 4 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13 - 4;
			border1a.setVisible(true);
			border2a.position = "278 64";
			border2a.extent = "100 10";
			border2b.position = "2 0";
			border2b.extent = "96 10";
			border2a.setVisible(true);
		}
		else
		{
			resetJVSGUI();
			portalsystemBTN.mColor = "50 130 255 255";
			portalsystemGUI.setVisible(true);
			jvsGUI_Window.extent = "555 363";
			getBrickProperties();
			portalsystemcreateBTN.mColor = "50 130 255 255";
			portalsystemcreateGUI.setVisible(true);
			description.makeFirstResponder(true);
			jvsGUI_Window.setText("JVS Mod > Portals > Create");
			border1a.position = "13 72";
			border1a.extent = getWord(jvsGUI_Window.extent,0) - 26 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13;
			border1b.position = "2 2";
			border1b.extent = getWord(jvsGUI_Window.extent,0) - 26 - 4 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13 - 4;
			border1a.setVisible(true);
			border2a.position = "278 64";
			border2a.extent = "100 10";
			border2b.position = "2 0";
			border2b.extent = "96 10";
			border2a.setVisible(true);
			border3a.position = getWord(doorsystemGUI.position,0) + 28 SPC getWord(doorsystemGUI.position,1) + 34;
			border3a.extent = 500 SPC getWord(jvsGUI_Window.extent,1) - 72 - 35 - 13 - 13 - 12;
			border3b.position = "2 2";
			border3b.extent = 496 SPC getWord(jvsGUI_Window.extent,1) - 72 - 35 - 13 - 13 - 12 - 4;
			border3a.setVisible(true);
			border4a.position = getWord(doorsystemGUI.position,0) + 28 SPC getWord(doorsystemGUI.position,1) + 26;
			border4a.extent = "100 10";
			border4b.position = "2 0";
			border4b.extent = "96 10";
			border4a.setVisible(true);
		}

		centerWrenchGUI();
	}

	function showportalsystemloadGUI()
	{
		if(portalsystemloadGUI.isVisible())
		{
			resetJVSGUI();
			portalsystemBTN.mColor = "50 130 255 255";
			portalsystemGUI.setVisible(true);
			portalsystemloadGUI.setVisible(false);
			jvsGUI_Window.extent = "555 142";
			jvsGUI_Window.setText("JVS Mod > Portals");
			border1a.position = "13 72";
			border1a.extent = getWord(jvsGUI_Window.extent,0) - 26 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13;
			border1b.position = "2 2";
			border1b.extent = getWord(jvsGUI_Window.extent,0) - 26 - 4 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13 - 4;
			border1a.setVisible(true);
			border2a.position = "278 64";
			border2a.extent = "100 10";
			border2b.position = "2 0";
			border2b.extent = "96 10";
			border2a.setVisible(true);
		}
		else
		{
			resetJVSGUI();
			refreshlist();
			jvsGUI_Window.extent = "555 363";
			portalsystemBTN.mColor = "50 130 255 255";
			portalsystemGUI.setVisible(true);
			portalsystemloadBTN.mColor = "50 130 255 255";
			portalsystemloadGUI.setVisible(true);
			jvsGUI_Window.setText("JVS Mod > Portals > Load");
			border1a.position = "13 72";
			border1a.extent = getWord(jvsGUI_Window.extent,0) - 26 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13;
			border1b.position = "2 2";
			border1b.extent = getWord(jvsGUI_Window.extent,0) - 26 - 4 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13 - 4;
			border1a.setVisible(true);
			border2a.position = "278 64";
			border2a.extent = "100 10";
			border2b.position = "2 0";
			border2b.extent = "96 10";
			border2a.setVisible(true);
			border3a.position = getWord(portalsystemGUI.position,0) + 28 SPC getWord(portalsystemGUI.position,1) + 34;
			border3a.extent = 500 SPC getWord(jvsGUI_Window.extent,1) - 72 - 35 - 13 - 13 - 12;
			border3b.position = "2 2";
			border3b.extent = 496 SPC getWord(jvsGUI_Window.extent,1) - 72 - 35 - 13 - 13 - 12 - 4;
			border3a.setVisible(true);
			border4a.position = getWord(portalsystemGUI.position,0) + 428 SPC getWord(portalsystemGUI.position,1) + 26;
			border4a.extent = "100 10";
			border4b.position = "2 0";
			border4b.extent = "96 10";
			border4a.setVisible(true);
		}

		centerWrenchGUI();
	}

	function showportalsystemmanageallGUI()
	{
		if(portalsystemmanageallGUI.isVisible())
		{
			resetJVSGUI();
			portalsystemBTN.mColor = "50 130 255 255";
			portalsystemGUI.setVisible(true);
			portalsystemmanageallGUI.setVisible(false);
			jvsGUI_Window.extent = "555 142";
			jvsGUI_Window.setText("JVS Mod > Portals");
			border1a.position = "13 72";
			border1a.extent = getWord(jvsGUI_Window.extent,0) - 26 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13;
			border1b.position = "2 2";
			border1b.extent = getWord(jvsGUI_Window.extent,0) - 26 - 4 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13 - 4;
			border1a.setVisible(true);
			border2a.position = "278 64";
			border2a.extent = "100 10";
			border2b.position = "2 0";
			border2b.extent = "96 10";
			border2a.setVisible(true);
		}
		else
		{
			resetJVSGUI();
			jvsGUI_Window.extent = "555 567";
			portalsystemBTN.mColor = "50 130 255 255";
			portalsystemGUI.setVisible(true);
			portalsystemmanageallBTN.mColor = "50 130 255 255";
			portalsystemmanageallGUI.setVisible(true);
			commandtoserver('manageportallist');
			jvsGUI_Window.setText("JVS Mod > Portals > Manage All");
			border1a.position = "13 72";
			border1a.extent = getWord(jvsGUI_Window.extent,0) - 26 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13;
			border1b.position = "2 2";
			border1b.extent = getWord(jvsGUI_Window.extent,0) - 26 - 4 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13 - 4;
			border1a.setVisible(true);
			border2a.position = "278 64";
			border2a.extent = "100 10";
			border2b.position = "2 0";
			border2b.extent = "96 10";
			border2a.setVisible(true);
			border3a.position = getWord(portalsystemGUI.position,0) + 28 SPC getWord(portalsystemGUI.position,1) + 34;
			border3a.extent = 500 SPC getWord(jvsGUI_Window.extent,1) - 72 - 35 - 13 - 13 - 12;
			border3b.position = "2 2";
			border3b.extent = 496 SPC getWord(jvsGUI_Window.extent,1) - 72 - 35 - 13 - 13 - 12 - 4;
			border3a.setVisible(true);
			border4a.position = getWord(portalsystemGUI.position,0) + 228 SPC getWord(portalsystemGUI.position,1) + 26;
			border4a.extent = "100 10";
			border4b.position = "2 0";
			border4b.extent = "96 10";
			border4a.setVisible(true);
		}

		centerWrenchGUI();
	}

	function showportalsystemmanageyoursGUI()
	{
		if(portalsystemmanageyoursGUI.isVisible())
		{
			resetJVSGUI();
			portalsystemBTN.mColor = "50 130 255 255";
			portalsystemGUI.setVisible(true);
			portalsystemmanageallGUI.setVisible(false);
			jvsGUI_Window.extent = "555 142";
			jvsGUI_Window.setText("JVS Mod > Portals");
			border1a.position = "13 72";
			border1a.extent = getWord(jvsGUI_Window.extent,0) - 26 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13;
			border1b.position = "2 2";
			border1b.extent = getWord(jvsGUI_Window.extent,0) - 26 - 4 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13 - 4;
			border1a.setVisible(true);
			border2a.position = "278 64";
			border2a.extent = "100 10";
			border2b.position = "2 0";
			border2b.extent = "96 10";
			border2a.setVisible(true);
		}
		else
		{
			resetJVSGUI();
			commandtoserver('managemyportals2');
			portalsystemBTN.mColor = "50 130 255 255";
			portalsystemGUI.setVisible(true);
			jvsGUI_Window.extent = "555 499";
			portalsystemmanageyoursBTN.mColor = "50 130 255 255";
			portalsystemmanageyoursGUI.setVisible(true);
			jvsGUI_Window.setText("JVS Mod > Portals > Manage Yours");
			border1a.position = "13 72";
			border1a.extent = getWord(jvsGUI_Window.extent,0) - 26 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13;
			border1b.position = "2 2";
			border1b.extent = getWord(jvsGUI_Window.extent,0) - 26 - 4 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13 - 4;
			border1a.setVisible(true);
			border2a.position = "278 64";
			border2a.extent = "100 10";
			border2b.position = "2 0";
			border2b.extent = "96 10";
			border2a.setVisible(true);
			border3a.position = getWord(portalsystemGUI.position,0) + 28 SPC getWord(portalsystemGUI.position,1) + 34;
			border3a.extent = 500 SPC getWord(jvsGUI_Window.extent,1) - 72 - 35 - 13 - 13 - 12;
			border3b.position = "2 2";
			border3b.extent = 496 SPC getWord(jvsGUI_Window.extent,1) - 72 - 35 - 13 - 13 - 12 - 4;
			border3a.setVisible(true);
			border4a.position = getWord(portalsystemGUI.position,0) + 128 SPC getWord(portalsystemGUI.position,1) + 26;
			border4a.extent = "100 10";
			border4b.position = "2 0";
			border4b.extent = "96 10";
			border4a.setVisible(true);
		}

		centerWrenchGUI();
	}

	function showportalsystemsaveGUI()
	{
		if(portalsystemsaveGUI.isVisible())
		{
			resetJVSGUI();
			portalsystemBTN.mColor = "50 130 255 255";
			portalsystemGUI.setVisible(true);
			portalsystemsaveGUI.setVisible(false);
			jvsGUI_Window.extent = "555 142";
			jvsGUI_Window.setText("JVS Mod > Portals");
			border1a.position = "13 72";
			border1a.extent = getWord(jvsGUI_Window.extent,0) - 26 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13;
			border1b.position = "2 2";
			border1b.extent = getWord(jvsGUI_Window.extent,0) - 26 - 4 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13 - 4;
			border1a.setVisible(true);
			border2a.position = "278 64";
			border2a.extent = "100 10";
			border2b.position = "2 0";
			border2b.extent = "96 10";
			border2a.setVisible(true);
		}
		else
		{
			resetJVSGUI();
			txteditName.setValue("");
			jvsGUI_Window.extent = "555 270";
			portalsystemBTN.mColor = "50 130 255 255";
			portalsystemGUI.setVisible(true);
			portalsystemsaveBTN.mColor = "50 130 255 255";
			portalsystemsaveGUI.setVisible(true);
			txteditName.makeFirstResponder(true);
			jvsGUI_Window.setText("JVS Mod > Portals > Save");
			border1a.position = "13 72";
			border1a.extent = getWord(jvsGUI_Window.extent,0) - 26 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13;
			border1b.position = "2 2";
			border1b.extent = getWord(jvsGUI_Window.extent,0) - 26 - 4 SPC getWord(jvsGUI_Window.extent,1) - 72 - 13 - 4;
			border1a.setVisible(true);
			border2a.position = "278 64";
			border2a.extent = "100 10";
			border2b.position = "2 0";
			border2b.extent = "96 10";
			border2a.setVisible(true);
			border3a.position = getWord(portalsystemGUI.position,0) + 28 SPC getWord(portalsystemGUI.position,1) + 34;
			border3a.extent = 500 SPC getWord(jvsGUI_Window.extent,1) - 72 - 35 - 13 - 13 - 12;
			border3b.position = "2 2";
			border3b.extent = 496 SPC getWord(jvsGUI_Window.extent,1) - 72 - 35 - 13 - 13 - 12 - 4;
			border3a.setVisible(true);
			border4a.position = getWord(portalsystemGUI.position,0) + 328 SPC getWord(portalsystemGUI.position,1) + 26;
			border4a.extent = "100 10";
			border4b.position = "2 0";
			border4b.extent = "96 10";
			border4a.setVisible(true);
		}

		centerWrenchGUI();
	}

	function showwindowsystemGUI()
	{
		resetJVSGUI();
		windowsystemBTN.mColor = "50 130 255 255";
		windowsystemGUI.setVisible(true);
	}

	function toggleJVSGUI(%val)
	{
		if(%val)
		{
			if(jvsGUI.isAwake())
			{
				canvas.popDialog(jvsGUI);
			}
			else
			{
				canvas.pushDialog(jvsGUI);
			}
		}
	}

	function toggleJVSWrench(%val)
	{
		if(%val)
		{
			commandtoserver('togjvswrench');
		}
	}

	function handleClearDoorList2(%msgType,%msgString)
	{
		doorselect2.clear();
	}

	function handleClearDoorListGui(%msgType,%msgString)
	{
		lstDoorList.clear();
	}

	function handleClearManageList(%msgType,%msgString)
	{
		lstPortalManagementList.clear();
	}

	function handleClearManageList2(%msgType,%msgString)
	{
		lstDoorManagementList.clear();
	}

	function handleClearPortalList(%msgType,%msgString)
	{
		lstPortalSystemList.clear();
	}

	function handleClearPortalListGui(%msgType,%msgString)
	{
		lstPortalList.clear();
	}

	function handleCloseJVSGUI(%msgType,%msgString)
	{
		canvas.popDialog(jvsgui);
	}

	function handleCreateDoor(%msgType,%msgString)
	{
		canvas.pushDialog(jvsgui);
		showdoorsystemcreateGUI();
	}

	function handleCreatePortal(%msgType,%msgString)
	{
		canvas.pushDialog(jvsgui);
		showportalsystemcreateGUI();
	}

	function handleDoorLoad(%msgType,%msgString)
	{
		canvas.pushDialog(jvsgui);
		showdoorsystemloadGUI();
	}

	function handleDoorSave(%msgType,%msgString)
	{
		canvas.pushDialog(jvsgui);
		showdoorsystemsaveGUI();
	}

	function handleManageDoors(%msgType,%msgString)
	{
		canvas.pushDialog(jvsGUI);
		showdoorsystemmanageallGUI();
	}

	function handleManagePortals(%msgType,%msgString)
	{
		canvas.pushDialog(jvsgui);
		showportalsystemmanageallGUI();
	}

	function handleOpenDoorListGui(%msgType,%msgString)
	{
		canvas.pushdialog(jvsGUI);
		showdoorsystemmanageyoursGUI();
	}

	function handleOpenPortalListGui(%msgType,%msgString)
	{
		canvas.pushdialog(jvsGUI);
		showportalsystemmanageyoursGUI();
	}

	function handlePortalLoad(%msgType,%msgString)
	{
		canvas.pushDialog(jvsgui);
		showportalsystemloadGUI();
	}

	function handlePortalSave(%msgType,%msgString)
	{
		canvas.pushDialog(jvsgui);
		showportalsystemsaveGUI();
	}

	function handleReturnDoorProperties(%msgType,%msgString,%open,%close,%drestrict,%doordesc,%itemdir,%rotdir,%doortype)
	{
		if(%open == 1)
		{
			opentoggle.mColor = "50 255 50 255";
		}
		else
		{
			opentoggle.mColor = "255 75 0 255";
		}

		if(%close == 1)
		{
			closedtoggle.mColor = "50 255 50 255";
		}
		else
		{
			closedtoggle.mColor = "255 75 0 255";
		}

		if(%drestrict == 1)
		{
			drestrict12.performClick();
		}
		else if(%drestrict == 2)
		{
			drestrict22.performClick();
		}
		else if(%drestrict == 3)
		{
			drestrict32.performClick();
		}
		else if(%drestrict == 4)
		{
			drestrict42.performClick();
		}
		else
		{
			drestrict02.performClick();
		}

		description2.setValue(%doordesc);

		if(%itemdir == 3)
		{
			doordirE2.performClick();
		}
		else if(%itemdir == 4)
		{
			doordirS2.performClick();
		}
		else if(%itemdir == 5)
		{
			doordirW2.performClick();
		}
		else
		{
			doordirN2.performClick();
		}

		if(%rotdir == 1)
		{
			doorrot90plus2.performClick();
		}
		else
		{
			doorrot90minus2.performClick();
		}

		doorselect2.setValue(%doortype);
	}

	function handleReturnPortalProperties(%msgType,%msgString,%portalrestrict,%portaldesc)
	{
		if(%portalrestrict == 1)
		{
			restrict12.performClick();
		}
		else if(%portalrestrict == 2)
		{
			restrict22.performClick();
		}
		else if(%portalrestrict == 3)
		{
			restrict32.performClick();
		}
		else if(%portalrestrict == 4)
		{
			restrict42.performClick();
		}
		else
		{
			restrict02.performClick();
		}

		description2.setValue(%portaldesc);
	}

	function handleSortDoorList2(%msgType,%msgString)
	{
		doorselect2.sort();
	}

	function handleSortDoorListGui(%msgType,%msgString)
	{
		lstDoorList.sort(0);
	}

	function handleSortManageList(%msgType,%msgString)
	{
		lstPortalManagementList.sort(0);
	}

	function handleSortManageList2(%msgType,%msgString)
	{
		lstDoorManagementList.sort(0);
	}

	function handleSortPortalList(%msgType,%msgString)
	{
		lstPortalSystemList.sort(0);
	}

	function handleSortPortalListGui(%msgType,%msgString)
	{
		lstPortalList.sort(0);
	}

	function handleUpdateDoorList2(%msgType,%msgString,%displayname,%id)
	{
		doorselect2.add(%displayname,%id);
	}

	function handleUpdateDoorListGui(%msgType,%msgString,%obj,%desc)
	{
		lstDoorList.addRow(%obj,%desc);
		%text = lstDoorList.getRowTextById(%obj);
		setField(%text,1,%desc);
	}

	function handleUpdateManageList2(%msgType,%msgString,%client,%name,%doors)
	{
		lstDoorManagementList.addRow(%client,%name TAB %doors);
		%text = lstDoorManagementList.getRowTextById(%client);
		setField(%text,1,%name TAB %doors);
	}

	function handleUpdatePortalListGui(%msgType,%msgString,%obj,%desc)
	{
		lstPortalList.addRow(%obj,%desc);
		%text = lstPortalList.getRowTextById(%obj);
		setField(%text,1,%desc);
	}

	function handleUpdateManageList(%msgType,%msgString,%client,%name,%portals)
	{
		lstPortalManagementList.addRow(%client,%name TAB %portals);
		%text = lstPortalManagementList.getRowTextById(%client);
		setField(%text,1,%name TAB %portals);
	}

	function handleUpdatePortalList(%msgType,%msgString,%brick,%portals,%desc)
	{
		lstPortalSystemList.addRow(%brick,%desc);
		%text = lstPortalSystemList.getRowTextById(%brick);
		setField(%text,1,%portals TAB %desc);
	}
};

//Client Start-Up Code

if($AddOn__Mod_JVS_Client == 1)
{
	if(isFile("add-ons/jvs/clientmods.code"))
	{
		echo("JVS Mod :: Client :: Mods Init");
		exec("add-ons/jvs/clientmods.code");
	}
	else
	{
		echo("JVS Mod :: Client :: Mods Init");
		$JVS::ClientMods::Doors = 1;
		$JVS::ClientMods::Portals = 1;
		$JVS::ClientMods::Spectator = 1;
	}

	if(($JVS::ClientMods::Doors == 1 || $JVS::ClientMods::Portals == 1) && (isFile("add-ons/jvs/doors/client.code") || isFile("add-ons/jvs/doors/client.code")))
	{
		if (!$jvskeybindsadded)
		{
			$remapDivision[$remapCount] = "JVS Mod";
			$remapName[$remapCount] = "Toggle JVS GUI";
			$remapCmd[$remapCount] = "toggleJVSGUI";
			$remapCount++;
			$remapName[$remapCount] = "Toggle JVS Wrench";
			$remapCmd[$remapCount] = "toggleJVSWrench";
			$remapCount++;
			$jvskeybindsadded = 1;
		}

		if(!isObject(JVS_BlockButtonProfile))
		{
			new GuiControlProfile(JVS_BlockButtonProfile) 
			{
				autoSizeHeight = "0";
				autoSizeWidth = "0";
				bitmap = "base/ui/blockScroll.png";
				border = "1";
				borderColor = "0 0 0 255";
				borderColorHL = "128 128 128 255";
				borderColorNA = "64 64 64 255";
				borderThickness = "1";
				canKeyFocus = "0";
				cursorColor = "0 0 0 255";
				doFontOutline = "0";
				fillColor = "149 152 166 255";
				fillColorHL = "171 171 171 255";
				fillColorNA = "221 202 173 255";
				fontColor = "0 0 0 255";
				fontColorHL = "255 255 255 255";
				fontColorLink = "0 0 204 255";
				fontColorLinkHL = "85 26 139 255";
				fontColorNA = "0 0 0 255";
				fontColors[0] = "0 0 0 255";
				fontColors[1] = "255 255 255 255";
				fontColors[2] = "255 255 255";
				fontColors[3] = "200 200 200 255";
				fontColors[4] = "0 0 204 255";
				fontColors[5] = "85 26 139 255";
				fontColors[6] = "0 2 0 0";
				fontColors[7] = "0 186 3 0";
				fontColors[8] = "0 132 6 0";
				fontColors[9] = "0 151 2 0";
				fontColorSEL = "200 200 200 255";
				fontOutlineColor = "255 255 255 255";
				fontSize = "18";
				fontType = "Impact";
				justify = "center";
				modal = "1";
				mouseOverSelected = "0";
				numbersOnly = "0";
				opaque = "1";
				returnTab = "0";
				tab = "0";
				textOffset = "6 6";
			};
		}

		if(!isObject(JVS_GuiWindowProfile))
		{
			new GuiControlProfile(JVS_GuiWindowProfile) 
			{
				autoSizeHeight = "0";
				autoSizeWidth = "0";
				bitmap = "base/ui/BlockWindow";
				border = "2";
				borderColor = "0 0 0 255";
				borderColorHL = "128 128 128 255";
				borderColorNA = "64 64 64 255";
				borderThickness = "1";
				canKeyFocus = "0";
				cursorColor = "0 0 0 255";
				doFontOutline = "0";
				fillColor = "200 200 200 255";
				fillColorHL = "200 200 200 255";
				fillColorNA = "200 200 200 255";
				fontColor = "255 255 255 255";
				fontColorHL = "255 255 255 255";
				fontColorLink = "0 0 204 255";
				fontColorLinkHL = "85 26 139 255";
				fontColorNA = "0 0 0 255";
				fontColors[0] = "0 0 0 255";
				fontColors[1] = "255 255 255 255";
				fontColors[2] = "0 0 0 255";
				fontColors[3] = "200 200 200 255";
				fontColors[4] = "0 0 204 255";
				fontColors[5] = "85 26 139 255";
				fontColors[6] = "0 2 0 0";
				fontColors[7] = "0 186 3 0";
				fontColors[8] = "0 132 6 0";
				fontColors[9] = "0 151 2 0";
				fontColorSEL = "200 200 200 255";
				fontOutlineColor = "255 255 255 255";
				fontSize = "18";
				fontType = "Impact";
				justify = "left";
				modal = "1";
				mouseOverSelected = "0";
				numbersOnly = "0";
				opaque = "1";
				returnTab = "0";
				tab = "0";
				textOffset = "5 2";
			};
		}

		if(!isObject(JVS_GuiWindowProfile2))
		{
			new GuiControlProfile(JVS_GuiWindowProfile2) 
			{
				autoSizeHeight = "0";
				autoSizeWidth = "0";
				bitmap = "base/ui/BlockWindow";
				border = "2";
				borderColor = "0 0 0 255";
				borderColorHL = "128 128 128 255";
				borderColorNA = "64 64 64 255";
				borderThickness = "1";
				canKeyFocus = "0";
				cursorColor = "0 0 0 255";
				doFontOutline = "0";
				fillColor = "200 200 200 255";
				fillColorHL = "200 200 200 255";
				fillColorNA = "200 200 200 255";
				fontColor = "255 255 255 255";
				fontColorHL = "255 255 255 255";
				fontColorLink = "0 0 204 255";
				fontColorLinkHL = "85 26 139 255";
				fontColorNA = "171 171 171 255";
				fontColors[0] = "171 171 171 255";
				fontColors[1] = "255 255 255 255";
				fontColors[2] = "171 171 171 255";
				fontColors[3] = "200 200 200 255";
				fontColors[4] = "0 0 204 255";
				fontColors[5] = "85 26 139 255";
				fontColors[6] = "0 2 0 0";
				fontColors[7] = "0 186 3 0";
				fontColors[8] = "0 132 6 0";
				fontColors[9] = "0 151 2 0";
				fontColorSEL = "200 200 200 255";
				fontOutlineColor = "255 255 255 255";
				fontSize = "18";
				fontType = "Impact";
				justify = "left";
				modal = "1";
				mouseOverSelected = "0";
				numbersOnly = "0";
				opaque = "1";
				returnTab = "0";
				tab = "0";
				textOffset = "5 2";
			};
		}

		if(!isObject(jvsGUI))
		{

new GuiControl(jvsGUI) {
   profile = "GuiDefaultProfile";
   horizSizing = "right";
   vertSizing = "bottom";
   position = "0 0";
   extent = "1024 768";
   minExtent = "8 2";
   visible = "1";

   new GuiWindowCtrl(jvsGUI_Window) {
      profile = "BlockWindowProfile";
      horizSizing = "center";
      vertSizing = "center";
      position = "0 0";
      extent = "554 83";
      minExtent = "8 2";
      visible = "1";
      text = "JVS Mod";
      maxLength = "255";
      resizeWidth = "0";
      resizeHeight = "0";
      canMove = "1";
      canClose = "1";
      canMinimize = "0";
      canMaximize = "0";
      minSize = "50 50";
      closeCommand = "canvas.popDialog(jvsGUI);";

      new GuiBitmapButtonCtrl(doorsystemBTN) {
         profile = "JVS_BlockButtonProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "78 42";
         extent = "100 22";
         minExtent = "8 2";
         visible = "1";
         command = "showdoorsystemGUI();";
         text = "Doors";
         groupNum = "-1";
         buttonType = "PushButton";
         bitmap = "add-ons/jvs/images/jvsGUI";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         mKeepCached = "0";
         mColor = "255 255 255 255";
      };
      new GuiSwatchCtrl(doorsystemSWATCH) {
         profile = "GuiDefaultProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "78 42";
         extent = "100 22";
         minExtent = "8 2";
         visible = "1";
         color = "200 200 200 187";
      };
      new GuiBitmapButtonCtrl(elevatorsystemBTN) {
         profile = "JVS_BlockButtonProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "178 42";
         extent = "100 22";
         minExtent = "8 2";
         visible = "1";
         command = "showelevatorsystemGUI();";
         text = "Elevators";
         groupNum = "-1";
         buttonType = "PushButton";
         bitmap = "add-ons/jvs/images/jvsGUI";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         mKeepCached = "0";
         mColor = "255 255 255 255";
      };
      new GuiSwatchCtrl(elevatorsystemSWATCH) {
         profile = "GuiDefaultProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "178 42";
         extent = "100 22";
         minExtent = "8 2";
         visible = "1";
         color = "200 200 200 187";
      };
      new GuiBitmapButtonCtrl(portalsystemBTN) {
         profile = "JVS_BlockButtonProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "278 42";
         extent = "100 22";
         minExtent = "8 2";
         visible = "1";
         command = "showportalsystemGUI();";
         text = "Portals";
         groupNum = "-1";
         buttonType = "PushButton";
         bitmap = "add-ons/jvs/images/jvsGUI";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         mKeepCached = "0";
         mColor = "255 255 255 255";
      };
      new GuiSwatchCtrl(portalsystemSWATCH) {
         profile = "GuiDefaultProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "278 42";
         extent = "100 22";
         minExtent = "8 2";
         visible = "1";
         color = "200 200 200 187";
      };
      new GuiBitmapButtonCtrl(windowsystemBTN) {
         profile = "JVS_BlockButtonProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "378 42";
         extent = "100 22";
         minExtent = "8 2";
         visible = "1";
         command = "";
         text = "Windows";
         groupNum = "-1";
         buttonType = "PushButton";
         bitmap = "add-ons/jvs/images/jvsGUI";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         mKeepCached = "0";
         mColor = "255 255 255 255";
      };
      new GuiSwatchCtrl(windowsystemSWATCH) {
         profile = "GuiDefaultProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "378 42";
         extent = "100 22";
         minExtent = "8 2";
         visible = "1";
         color = "200 200 200 187";
      };
      new GuiSwatchCtrl(border1a) {
         profile = "GuiDefaultProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "13 74";
         extent = "330 22";
         minExtent = "8 2";
         visible = "0";
         color = "149 152 166 255";

         new GuiSwatchCtrl(border1b) {
            profile = "GuiDefaultProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "2 2";
            extent = "326 18";
            minExtent = "8 2";
            visible = "1";
            color = "220 220 224 255";
         };
      };
      new GuiSwatchCtrl(border2a) {
         profile = "GuiDefaultProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "13 64";
         extent = "100 10";
         minExtent = "8 2";
         visible = "0";
         color = "149 152 166 255";

         new GuiSwatchCtrl(border2b) {
            profile = "GuiDefaultProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "2 0";
            extent = "96 10";
            minExtent = "8 2";
            visible = "1";
            color = "220 220 224 255";
         };
      };
      new GuiSwatchCtrl(border3a) {
         profile = "GuiDefaultProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "27 34";
         extent = "300 22";
         minExtent = "8 2";
         visible = "0";
         color = "149 152 166 255";

         new GuiSwatchCtrl(border3b) {
            profile = "GuiDefaultProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "2 2";
            extent = "296 18";
            minExtent = "8 2";
            visible = "1";
            color = "240 239 248 255";
         };
      };
      new GuiSwatchCtrl(border4a) {
         profile = "GuiDefaultProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "27 26";
         extent = "100 10";
         minExtent = "8 2";
         visible = "0";
         color = "149 152 166 255";

         new GuiSwatchCtrl(border4b) {
            profile = "GuiDefaultProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "2 0";
            extent = "96 10";
            minExtent = "8 2";
            visible = "1";
            color = "240 239 248 255";
         };
      };
      new GuiControl(doorsystemGUI) {
         profile = "GuiDefaultProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "0 83";
         extent = "554 470";
         minExtent = "8 2";
         visible = "0";

         new GuiBitmapButtonCtrl(doorsystemcreateBTN) {
            profile = "JVS_BlockButtonProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "28 6";
            extent = "100 20";
            minExtent = "8 2";
            visible = "1";
            command = "showdoorsystemcreateGUI();";
            text = "Create";
            groupNum = "-1";
            buttonType = "PushButton";
            bitmap = "add-ons/jvs/images/jvsGUI";
            lockAspectRatio = "0";
            alignLeft = "0";
            overflowImage = "0";
            mKeepCached = "0";
            mColor = "255 255 255 255";
         };
         new GuiBitmapButtonCtrl(doorsystemmanageyoursBTN) {
            profile = "JVS_BlockButtonProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "128 6";
            extent = "100 20";
            minExtent = "8 2";
            visible = "1";
            command = "showdoorsystemmanageyoursGUI();";
            text = "Manage Yours";
            groupNum = "-1";
            buttonType = "PushButton";
            bitmap = "add-ons/jvs/images/jvsGUI";
            lockAspectRatio = "0";
            alignLeft = "0";
            overflowImage = "0";
            mKeepCached = "0";
            mColor = "255 255 255 255";
         };
         new GuiBitmapButtonCtrl(doorsystemmanageallBTN) {
            profile = "JVS_BlockButtonProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "228 6";
            extent = "100 20";
            minExtent = "8 2";
            visible = "1";
            command = "showdoorsystemmanageallGUI();";
            text = "Manage All";
            groupNum = "-1";
            buttonType = "PushButton";
            bitmap = "add-ons/jvs/images/jvsGUI";
            lockAspectRatio = "0";
            alignLeft = "0";
            overflowImage = "0";
            mKeepCached = "0";
            mColor = "255 255 255 255";
         };
         new GuiBitmapButtonCtrl(doorsystemsaveBTN) {
            profile = "JVS_BlockButtonProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "328 6";
            extent = "100 20";
            minExtent = "8 2";
            visible = "1";
            command = "showdoorsystemsaveGUI();";
            text = "Save";
            groupNum = "-1";
            buttonType = "PushButton";
            bitmap = "add-ons/jvs/images/jvsGUI";
            lockAspectRatio = "0";
            alignLeft = "0";
            overflowImage = "0";
            mKeepCached = "0";
            mColor = "255 255 255 255";
         };
         new GuiBitmapButtonCtrl(doorsystemloadBTN) {
            profile = "JVS_BlockButtonProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "428 6";
            extent = "100 20";
            minExtent = "8 2";
            visible = "1";
            command = "showdoorsystemloadGUI();";
            text = "Load";
            groupNum = "-1";
            buttonType = "PushButton";
            bitmap = "add-ons/jvs/images/jvsGUI";
            lockAspectRatio = "0";
            alignLeft = "0";
            overflowImage = "0";
            mKeepCached = "0";
            mColor = "255 255 255 255";
         };
         new GuiControl(doorsystemcreateGUI) {
            profile = "GuiDefaultProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "28 25";
            extent = "500 227";
            minExtent = "8 2";
            visible = "0";

            new GuiBitmapButtonCtrl() {
               profile = "JVS_BlockButtonProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "302 181";
               extent = "91 38";
               minExtent = "8 2";
               visible = "1";
               command = "schedule(250,0,\"makedoor\");";
               text = "Set / Modify";
               groupNum = "-1";
               buttonType = "PushButton";
               bitmap = "base/client/ui/button2";
               lockAspectRatio = "0";
               alignLeft = "0";
               overflowImage = "0";
               mKeepCached = "0";
               mColor = "50 255 50 255";
            };
            new GuiBitmapButtonCtrl() {
               profile = "JVS_BlockButtonProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "107 181";
               extent = "91 38";
               minExtent = "8 2";
               visible = "1";
               command = "canvas.popdialog(jvsGUI);cleardoorcolor3();";
               text = "Clear";
               groupNum = "-1";
               buttonType = "PushButton";
               bitmap = "base/client/ui/button2";
               lockAspectRatio = "0";
               alignLeft = "0";
               overflowImage = "0";
               mKeepCached = "0";
               mColor = "255 75 0 255";
            };
            new GuiTextEditCtrl(ddescription) {
               profile = "GuiTextEditProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "205 35";
               extent = "90 18";
               minExtent = "8 2";
               visible = "1";
               maxLength = "255";
               historySize = "0";
               password = "0";
               tabComplete = "0";
               sinkAllKeyEvents = "0";
            };
            new GuiRadioCtrl(doordirN) {
               profile = "GuiRadioProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "194 58";
               extent = "21 22";
               minExtent = "8 2";
               visible = "1";
               text = "N";
               groupNum = "5";
               buttonType = "RadioButton";
            };
            new GuiRadioCtrl(doordirE) {
               profile = "GuiRadioProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "225 58";
               extent = "21 22";
               minExtent = "8 2";
               visible = "1";
               text = "E";
               groupNum = "5";
               buttonType = "RadioButton";
            };
            new GuiRadioCtrl(doordirS) {
               profile = "GuiRadioProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "253 58";
               extent = "26 22";
               minExtent = "8 2";
               visible = "1";
               text = "S";
               groupNum = "5";
               buttonType = "RadioButton";
            };
            new GuiRadioCtrl(doordirW) {
               profile = "GuiRadioProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "283 58";
               extent = "26 22";
               minExtent = "8 2";
               visible = "1";
               text = "W";
               groupNum = "5";
               buttonType = "RadioButton";
            };
            new GuiRadioCtrl(doorrot90plus) {
               profile = "GuiRadioProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "256 80";
               extent = "39 22";
               minExtent = "8 2";
               visible = "1";
               text = "+90°";
               groupNum = "6";
               buttonType = "RadioButton";
            };
            new GuiRadioCtrl(doorrot90minus) {
               profile = "GuiRadioProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "208 80";
               extent = "36 22";
               minExtent = "8 2";
               visible = "1";
               text = "-90°";
               groupNum = "6";
               buttonType = "RadioButton";
            };
            new GuiRadioCtrl(drestrict4) {
               profile = "GuiRadioProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "261 123";
               extent = "58 22";
               minExtent = "8 2";
               visible = "1";
               text = "Personal";
               groupNum = "7";
               buttonType = "RadioButton";
            };
            new GuiRadioCtrl(drestrict3) {
               profile = "GuiRadioProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "182 123";
               extent = "65 22";
               minExtent = "8 2";
               visible = "1";
               text = "Mini-Game";
               groupNum = "7";
               buttonType = "RadioButton";
            };
            new GuiRadioCtrl(drestrict2) {
               profile = "GuiRadioProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "286 104";
               extent = "51 22";
               minExtent = "8 2";
               visible = "1";
               text = "Supers";
               groupNum = "7";
               buttonType = "RadioButton";
            };
            new GuiRadioCtrl(drestrict1) {
               profile = "GuiRadioProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "220 104";
               extent = "50 22";
               minExtent = "8 2";
               visible = "1";
               text = "Admins";
               groupNum = "7";
               buttonType = "RadioButton";
            };
            new GuiRadioCtrl(drestrict0) {
               profile = "GuiRadioProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "166 104";
               extent = "43 22";
               minExtent = "8 2";
               visible = "1";
               text = "None";
               groupNum = "7";
               buttonType = "RadioButton";
            };
            new GuiPopUpMenuCtrl(doorselect) {
               profile = "BlockButtonProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "167 149";
               extent = "162 22";
               minExtent = "8 2";
               visible = "1";
               maxLength = "255";
               maxPopupHeight = "200";
            };
            new GuiTextCtrl() {
               profile = "JVS_GuiWindowProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "134 13";
               extent = "234 18";
               minExtent = "8 2";
               visible = "1";
               text = "Enter your desired Door creation settings:";
               maxLength = "255";
            };
         };
         new GuiControl(doorsystemmanageyoursGUI) {
            profile = "GuiDefaultProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "0 46";
            extent = "500 347";
            minExtent = "8 2";
            visible = "0";

            new GuiSwatchCtrl() {
               profile = "GuiDefaultProfile";
               horizSizing = "right";
               vertSizing = "height";
               position = "140 22";
               extent = "188 172";
               minExtent = "8 2";
               visible = "1";
               color = "255 255 255 128";
            };
            new GuiScrollCtrl() {
               profile = "ColorScrollProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "140 22";
               extent = "188 173";
               minExtent = "8 2";
               visible = "1";
               willFirstRespond = "1";
               hScrollBar = "alwaysOff";
               vScrollBar = "alwaysOn";
               constantThumbHeight = "0";
               childMargin = "0 0";
               rowHeight = "10";
               columnWidth = "30";

               new GuiTextListCtrl(lstDoorList) {
                  profile = "GuiTextListProfile";
                  horizSizing = "center";
                  vertSizing = "center";
                  position = "0 0";
                  extent = "176 2";
                  minExtent = "8 2";
                  visible = "1";
                  enumerate = "0";
                  resizeCell = "0";
                  columns = "0 120";
                  fitParentWidth = "1";
                  clipColumnText = "0";
                     helpTag = "0";
               };
            };
            new GuiTextCtrl() {
               profile = "JVS_GuiWindowProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "140 -1";
               extent = "200 18";
               minExtent = "8 2";
               visible = "1";
               text = "Select the Door you want to manage:";
               maxLength = "255";
            };
            new GuiTextCtrl() {
               profile = "JVS_GuiWindowProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "360 0";
               extent = "48 18";
               minExtent = "8 2";
               visible = "1";
               text = "Toggles:";
               maxLength = "255";
            };
            new GuiBitmapButtonCtrl(opentoggle) {
               profile = "JVS_BlockButtonProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "349 21";
               extent = "70 35";
               minExtent = "8 2";
               visible = "1";
               command = "modifydoor(\"togopen\");";
               text = "Open";
               groupNum = "-1";
               buttonType = "PushButton";
               bitmap = "base/client/ui/button2";
               lockAspectRatio = "0";
               alignLeft = "0";
               overflowImage = "0";
               mKeepCached = "0";
               mColor = "255 255 255 255";
            };
            new GuiBitmapButtonCtrl(closedtoggle) {
               profile = "JVS_BlockButtonProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "349 61";
               extent = "70 35";
               minExtent = "8 2";
               visible = "1";
               command = "modifydoor(\"togclose\");";
               text = "Closed";
               groupNum = "-1";
               buttonType = "PushButton";
               bitmap = "base/client/ui/button2";
               lockAspectRatio = "0";
               alignLeft = "0";
               overflowImage = "0";
               mKeepCached = "0";
               mColor = "255 255 255 255";
            };
            new GuiTextCtrl() {
               profile = "JVS_GuiWindowProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "351 98";
               extent = "67 18";
               minExtent = "8 2";
               visible = "1";
               text = "Commands:";
               maxLength = "255";
            };
            new GuiBitmapButtonCtrl(spydoor) {
               profile = "JVS_BlockButtonProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "349 119";
               extent = "70 35";
               minExtent = "8 2";
               visible = "1";
               command = "modifydoor(\"spy\");";
               text = "Door Cam";
               groupNum = "-1";
               buttonType = "PushButton";
               bitmap = "base/client/ui/button2";
               lockAspectRatio = "0";
               alignLeft = "0";
               overflowImage = "0";
               mKeepCached = "0";
               mColor = "255 255 255 255";
            };
            new GuiSwatchCtrl(doorbuttonsswatch) {
               profile = "GuiDefaultProfile";
               horizSizing = "right";
               vertSizing = "height";
               position = "344 2";
               extent = "82 155";
               minExtent = "8 2";
               visible = "1";
               color = "240 239 248 187";
            };
            new GuiBitmapButtonCtrl(doorlistsetbutton) {
               profile = "JVS_BlockButtonProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "319 286";
               extent = "91 38";
               minExtent = "8 2";
               visible = "1";
               command = "schedule(250,0,\"makedoor2\");";
               text = "Set / Modify";
               groupNum = "-1";
               buttonType = "PushButton";
               bitmap = "base/client/ui/button2";
               lockAspectRatio = "0";
               alignLeft = "0";
               overflowImage = "0";
               mKeepCached = "0";
               mColor = "255 255 255 255";
            };
            new GuiBitmapButtonCtrl(doorlistclearbutton) {
               profile = "JVS_BlockButtonProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "144 286";
               extent = "91 38";
               minExtent = "8 2";
               visible = "1";
               command = "cleardoorcolor2();";
               text = "Clear";
               groupNum = "-1";
               buttonType = "PushButton";
               bitmap = "base/client/ui/button2";
               lockAspectRatio = "0";
               alignLeft = "0";
               overflowImage = "0";
               mKeepCached = "0";
               mColor = "255 255 255 255";
            };
            new GuiTextEditCtrl(description2) {
               profile = "GuiTextEditProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "140 205";
               extent = "90 18";
               minExtent = "8 2";
               visible = "1";
               maxLength = "255";
               historySize = "0";
               password = "0";
               tabComplete = "0";
               sinkAllKeyEvents = "0";
            };
            new GuiRadioCtrl(doordirN2) {
               profile = "GuiRadioProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "235 203";
               extent = "21 22";
               minExtent = "8 2";
               visible = "1";
               text = "N";
               groupNum = "5";
               buttonType = "RadioButton";
            };
            new GuiRadioCtrl(doordirE2) {
               profile = "GuiRadioProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "258 203";
               extent = "21 22";
               minExtent = "8 2";
               visible = "1";
               text = "E";
               groupNum = "5";
               buttonType = "RadioButton";
            };
            new GuiRadioCtrl(doordirS2) {
               profile = "GuiRadioProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "280 203";
               extent = "26 22";
               minExtent = "8 2";
               visible = "1";
               text = "S";
               groupNum = "5";
               buttonType = "RadioButton";
            };
            new GuiRadioCtrl(doordirW2) {
               profile = "GuiRadioProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "303 203";
               extent = "26 22";
               minExtent = "8 2";
               visible = "1";
               text = "W";
               groupNum = "5";
               buttonType = "RadioButton";
            };
            new GuiRadioCtrl(doorrot90plus2) {
               profile = "GuiRadioProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "381 203";
               extent = "39 22";
               minExtent = "8 2";
               visible = "1";
               text = "+90°";
               groupNum = "6";
               buttonType = "RadioButton";
            };
            new GuiRadioCtrl(doorrot90minus2) {
               profile = "GuiRadioProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "343 203";
               extent = "36 22";
               minExtent = "8 2";
               visible = "1";
               text = "-90°";
               groupNum = "6";
               buttonType = "RadioButton";
            };
            new GuiRadioCtrl(drestrict42) {
               profile = "GuiRadioProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "252 229";
               extent = "58 22";
               minExtent = "8 2";
               visible = "1";
               text = "Personal";
               groupNum = "7";
               buttonType = "RadioButton";
            };
            new GuiRadioCtrl(drestrict32) {
               profile = "GuiRadioProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "185 229";
               extent = "65 22";
               minExtent = "8 2";
               visible = "1";
               text = "Mini-Game";
               groupNum = "7";
               buttonType = "RadioButton";
            };
            new GuiRadioCtrl(drestrict22) {
               profile = "GuiRadioProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "365 229";
               extent = "51 22";
               minExtent = "8 2";
               visible = "1";
               text = "Supers";
               groupNum = "7";
               buttonType = "RadioButton";
            };
            new GuiRadioCtrl(drestrict12) {
               profile = "GuiRadioProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "312 229";
               extent = "50 22";
               minExtent = "8 2";
               visible = "1";
               text = "Admins";
               groupNum = "7";
               buttonType = "RadioButton";
            };
            new GuiRadioCtrl(drestrict02) {
               profile = "GuiRadioProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "142 229";
               extent = "43 22";
               minExtent = "8 2";
               visible = "1";
               text = "None";
               groupNum = "7";
               buttonType = "RadioButton";
            };
            new GuiPopUpMenuCtrl(doorselect2) {
               profile = "BlockButtonProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "200 254";
               extent = "162 22";
               minExtent = "8 2";
               visible = "1";
               maxLength = "255";
               maxPopupHeight = "200";
            };
            new GuiSwatchCtrl(doorsettingsswatch) {
               profile = "GuiDefaultProfile";
               horizSizing = "right";
               vertSizing = "height";
               position = "130 195";
               extent = "296 145";
               minExtent = "8 2";
               visible = "1";
               color = "240 239 248 187";
            };
         };
         new GuiControl(doorsystemmanageallGUI) {
            profile = "GuiDefaultProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "0 46";
            extent = "500 425";
            minExtent = "8 2";
            visible = "0";

            new GuiSwatchCtrl() {
               profile = "GuiDefaultProfile";
               horizSizing = "left";
               vertSizing = "top";
               position = "173 44";
               extent = "192 306";
               minExtent = "8 2";
               visible = "1";
               color = "255 255 255 128";
            };
            new GuiScrollCtrl() {
               profile = "ColorScrollProfile";
               horizSizing = "left";
               vertSizing = "top";
               position = "173 44";
               extent = "192 306";
               minExtent = "8 2";
               visible = "1";
               willFirstRespond = "1";
               hScrollBar = "alwaysOff";
               vScrollBar = "alwaysOn";
               constantThumbHeight = "0";
               childMargin = "0 0";
               rowHeight = "10";
               columnWidth = "30";

               new GuiTextListCtrl(lstDoorManagementList) {
                  profile = "GuiTextListProfile";
                  horizSizing = "left";
                  vertSizing = "height";
                  position = "0 0";
                  extent = "192 16";
                  minExtent = "8 2";
                  visible = "1";
                  enumerate = "0";
                  resizeCell = "0";
                  columns = "0 50 135";
                  fitParentWidth = "1";
                  clipColumnText = "1";
                     sortedNumerical = "1";
                     helpTag = "0";
                     sortedAsc = "0";
                     sortedBy = "0";
               };
            };
            new GuiBitmapButtonCtrl() {
               profile = "BlockButtonProfile";
               horizSizing = "left";
               vertSizing = "top";
               position = "319 360";
               extent = "91 38";
               minExtent = "8 2";
               visible = "1";
               command = "messageboxokcancel(\"Clear ALL Doors?\",\"Are you sure you want to destroy ALL of the Doors?\",\"clearalldoors();\",\"\");";
               text = "CLEAR ALL";
               groupNum = "-1";
               buttonType = "PushButton";
               bitmap = "base/client/ui/button2";
               lockAspectRatio = "0";
               alignLeft = "0";
               overflowImage = "0";
               mKeepCached = "0";
               mColor = "255 0 0 255";
            };
            new GuiBitmapButtonCtrl() {
               profile = "BlockButtonProfile";
               horizSizing = "left";
               vertSizing = "top";
               position = "129 360";
               extent = "91 38";
               minExtent = "8 2";
               visible = "1";
               command = "clearclientdoors();";
               text = "Clear";
               groupNum = "-1";
               buttonType = "PushButton";
               bitmap = "base/client/ui/button2";
               lockAspectRatio = "0";
               alignLeft = "0";
               overflowImage = "0";
               mKeepCached = "0";
               mColor = "255 150 0 255";
            };
            new GuiTextCtrl() {
               profile = "JVS_GuiWindowProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "185 24";
               extent = "46 18";
               minExtent = "8 2";
               visible = "1";
               text = "Name:";
               maxLength = "255";
            };
            new GuiTextCtrl() {
               profile = "JVS_GuiWindowProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "304 24";
               extent = "58 18";
               minExtent = "8 2";
               visible = "1";
               text = "# Doors:";
               maxLength = "255";
            };
            new GuiTextCtrl() {
               profile = "JVS_GuiWindowProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "140 0";
               extent = "269 18";
               minExtent = "8 2";
               visible = "1";
               text = "Select the player whose Doors you wish to clear:";
               maxLength = "255";
            };
         };
         new GuiControl(doorsystemsaveGUI) {
            profile = "GuiDefaultProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "28 25";
            extent = "500 227";
            minExtent = "8 2";
            visible = "0";

            new GuiTextCtrl() {
               profile = "JVS_GuiWindowProfile";
               horizSizing = "center";
               vertSizing = "bottom";
               position = "11 25";
               extent = "195 18";
               minExtent = "8 2";
               visible = "1";
               text = "Type a filename for your Door Save:";
               maxLength = "255";
            };
            new GuiTextEditCtrl(txteditName2) {
               profile = "GuiTextEditProfile";
               horizSizing = "center";
               vertSizing = "bottom";
               position = "10 53";
               extent = "189 18";
               minExtent = "8 2";
               visible = "1";
               maxLength = "255";
               historySize = "0";
               password = "0";
               tabComplete = "0";
               sinkAllKeyEvents = "0";
            };
            new GuiBitmapButtonCtrl() {
               profile = "BlockButtonProfile";
               horizSizing = "center";
               vertSizing = "top";
               position = "59 82";
               extent = "91 38";
               minExtent = "8 2";
               visible = "1";
               command = "savedoors();";
               text = "Save";
               groupNum = "-1";
               buttonType = "PushButton";
               bitmap = "base/client/ui/button2";
               mKeepCached = "0";
               mColor = "255 255 255 255";
            };
         };
         new GuiControl(doorsystemloadGUI) {
            profile = "GuiDefaultProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "28 25";
            extent = "500 227";
            minExtent = "8 2";
            visible = "0";

            new GuiSwatchCtrl() {
               profile = "GuiDefaultProfile";
               horizSizing = "center";
               vertSizing = "height";
               position = "10 52";
               extent = "188 161";
               minExtent = "8 2";
               visible = "1";
               color = "255 255 255 128";
            };
            new GuiScrollCtrl() {
               profile = "ColorScrollProfile";
               horizSizing = "center";
               vertSizing = "bottom";
               position = "10 52";
               extent = "188 161";
               minExtent = "8 2";
               visible = "1";
               willFirstRespond = "1";
               hScrollBar = "alwaysOff";
               vScrollBar = "alwaysOn";
               constantThumbHeight = "0";
               childMargin = "0 0";
               rowHeight = "10";
               columnWidth = "30";

               new GuiTextListCtrl(lstDoorLoaderList) {
                  profile = "GuiTextListProfile";
                  horizSizing = "center";
                  vertSizing = "center";
                  position = "0 0";
                  extent = "176 2";
                  minExtent = "8 2";
                  visible = "1";
                  enumerate = "0";
                  resizeCell = "0";
                  columns = "0 120";
                  fitParentWidth = "1";
                  clipColumnText = "0";
                  helpTag = "0";
               };
            };
            new GuiTextCtrl() {
               profile = "JVS_GuiWindowProfile";
               horizSizing = "center";
               vertSizing = "bottom";
               position = "11 25";
               extent = "175 18";
               minExtent = "8 2";
               visible = "1";
               text = "Choose the Door save to load:";
               maxLength = "255";
            };
         };
      };
      new GuiControl(elevatorsystemGUI) {
         profile = "GuiDefaultProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "0 81";
         extent = "354 208";
         minExtent = "8 2";
         visible = "0";
      };
      new GuiControl(portalsystemGUI) {
         profile = "GuiDefaultProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "0 83";
         extent = "554 470";
         minExtent = "8 2";
         visible = "0";

         new GuiBitmapButtonCtrl(portalsystemcreateBTN) {
            profile = "JVS_BlockButtonProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "28 6";
            extent = "100 20";
            minExtent = "8 2";
            visible = "1";
            command = "showportalsystemcreateGUI();";
            text = "Create";
            groupNum = "-1";
            buttonType = "PushButton";
            bitmap = "add-ons/jvs/images/jvsGUI";
            lockAspectRatio = "0";
            alignLeft = "0";
            overflowImage = "0";
            mKeepCached = "0";
            mColor = "255 255 255 255";
         };
         new GuiBitmapButtonCtrl(portalsystemmanageyoursBTN) {
            profile = "JVS_BlockButtonProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "128 6";
            extent = "100 20";
            minExtent = "8 2";
            visible = "1";
            command = "showportalsystemmanageyoursGUI();";
            text = "Manage Yours";
            groupNum = "-1";
            buttonType = "PushButton";
            bitmap = "add-ons/jvs/images/jvsGUI";
            lockAspectRatio = "0";
            alignLeft = "0";
            overflowImage = "0";
            mKeepCached = "0";
            mColor = "255 255 255 255";
         };
         new GuiBitmapButtonCtrl(portalsystemmanageallBTN) {
            profile = "JVS_BlockButtonProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "228 6";
            extent = "100 20";
            minExtent = "8 2";
            visible = "1";
            command = "showportalsystemmanageallGUI();";
            text = "Manage All";
            groupNum = "-1";
            buttonType = "PushButton";
            bitmap = "add-ons/jvs/images/jvsGUI";
            lockAspectRatio = "0";
            alignLeft = "0";
            overflowImage = "0";
            mKeepCached = "0";
            mColor = "255 255 255 255";
         };
         new GuiBitmapButtonCtrl(portalsystemsaveBTN) {
            profile = "JVS_BlockButtonProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "328 6";
            extent = "100 20";
            minExtent = "8 2";
            visible = "1";
            command = "showportalsystemsaveGUI();";
            text = "Save";
            groupNum = "-1";
            buttonType = "PushButton";
            bitmap = "add-ons/jvs/images/jvsGUI";
            lockAspectRatio = "0";
            alignLeft = "0";
            overflowImage = "0";
            mKeepCached = "0";
            mColor = "255 255 255 255";
         };
         new GuiBitmapButtonCtrl(portalsystemloadBTN) {
            profile = "JVS_BlockButtonProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "428 6";
            extent = "100 20";
            minExtent = "8 2";
            visible = "1";
            command = "showportalsystemloadGUI();";
            text = "Load";
            groupNum = "-1";
            buttonType = "PushButton";
            bitmap = "add-ons/jvs/images/jvsGUI";
            lockAspectRatio = "0";
            alignLeft = "0";
            overflowImage = "0";
            mKeepCached = "0";
            mColor = "255 255 255 255";
         };
         new GuiControl(portalsystemcreateGUI) {
            profile = "GuiDefaultProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "28 25";
            extent = "500 227";
            minExtent = "8 2";
            visible = "0";

            new GuiBitmapButtonCtrl() {
               profile = "JVS_BlockButtonProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "304 172";
               extent = "91 38";
               minExtent = "8 2";
               visible = "1";
               command = "schedule(250,0,\"makeportal\");";
               text = "Set / Modify";
               groupNum = "-1";
               buttonType = "PushButton";
               bitmap = "base/client/ui/button2";
               lockAspectRatio = "0";
               alignLeft = "0";
               overflowImage = "0";
               mKeepCached = "0";
               mColor = "50 255 50 255";
            };
            new GuiBitmapButtonCtrl() {
               profile = "JVS_BlockButtonProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "105 172";
               extent = "91 38";
               minExtent = "8 2";
               visible = "1";
               command = "clearthisportal1();";
               text = "Clear";
               groupNum = "-1";
               buttonType = "PushButton";
               bitmap = "base/client/ui/button2";
               lockAspectRatio = "0";
               alignLeft = "0";
               overflowImage = "0";
               mKeepCached = "0";
               mColor = "255 75 0 255";
            };
            new GuiTextEditCtrl(description) {
               profile = "GuiTextEditProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "205 51";
               extent = "90 18";
               minExtent = "8 2";
               visible = "1";
               maxLength = "255";
               historySize = "0";
               password = "0";
               tabComplete = "0";
               sinkAllKeyEvents = "0";
            };
            new GuiRadioCtrl(restrict4) {
               profile = "GuiRadioProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "263 119";
               extent = "58 22";
               minExtent = "8 2";
               visible = "1";
               text = "Personal";
               groupNum = "4";
               buttonType = "RadioButton";
            };
            new GuiRadioCtrl(restrict3) {
               profile = "GuiRadioProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "175 119";
               extent = "65 22";
               minExtent = "8 2";
               visible = "1";
               text = "Mini-Game";
               groupNum = "4";
               buttonType = "RadioButton";
            };
            new GuiRadioCtrl(restrict2) {
               profile = "GuiRadioProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "287 89";
               extent = "53 22";
               minExtent = "8 2";
               visible = "1";
               text = "Supers";
               groupNum = "4";
               buttonType = "RadioButton";
            };
            new GuiRadioCtrl(restrict1) {
               profile = "GuiRadioProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "217 89";
               extent = "50 22";
               minExtent = "8 2";
               visible = "1";
               text = "Admins";
               groupNum = "4";
               buttonType = "RadioButton";
            };
            new GuiRadioCtrl(restrict0) {
               profile = "GuiRadioProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "156 89";
               extent = "43 22";
               minExtent = "8 2";
               visible = "1";
               text = "None";
               groupNum = "4";
               buttonType = "RadioButton";
            };
            new GuiTextCtrl() {
               profile = "JVS_GuiWindowProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "131 13";
               extent = "240 18";
               minExtent = "8 2";
               visible = "1";
               text = "Enter your desired Portal creation settings:";
               maxLength = "255";
            };
         };
         new GuiControl(portalsystemmanageyoursGUI) {
            profile = "GuiDefaultProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "0 46";
            extent = "500 347";
            minExtent = "8 2";
            visible = "0";

            new GuiSwatchCtrl() {
               profile = "GuiDefaultProfile";
               horizSizing = "right";
               vertSizing = "height";
               position = "184 22";
               extent = "188 172";
               minExtent = "8 2";
               visible = "1";
               color = "255 255 255 128";
            };
            new GuiScrollCtrl() {
               profile = "ColorScrollProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "184 22";
               extent = "188 173";
               minExtent = "8 2";
               visible = "1";
               willFirstRespond = "1";
               hScrollBar = "alwaysOff";
               vScrollBar = "alwaysOn";
               constantThumbHeight = "0";
               childMargin = "0 0";
               rowHeight = "10";
               columnWidth = "30";

               new GuiTextListCtrl(lstPortalList) {
                  profile = "GuiTextListProfile";
                  horizSizing = "center";
                  vertSizing = "center";
                  position = "0 0";
                  extent = "176 2";
                  minExtent = "8 2";
                  visible = "1";
                  enumerate = "0";
                  resizeCell = "0";
                  columns = "0 120";
                  fitParentWidth = "1";
                  clipColumnText = "0";
                     helpTag = "0";
               };
            };
            new GuiTextCtrl() {
               profile = "JVS_GuiWindowProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "175 -1";
               extent = "207 18";
               minExtent = "8 2";
               visible = "1";
               text = "Select the Portal you want to manage:";
               maxLength = "255";
            };
            new GuiBitmapButtonCtrl(portallistsetbutton) {
               profile = "JVS_BlockButtonProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "327 293";
               extent = "91 38";
               minExtent = "8 2";
               visible = "1";
               command = "schedule(250,0,\"makeportal\");";
               text = "Set / Modify";
               groupNum = "-1";
               buttonType = "PushButton";
               bitmap = "base/client/ui/button2";
               lockAspectRatio = "0";
               alignLeft = "0";
               overflowImage = "0";
               mKeepCached = "0";
               mColor = "50 255 50 255";
            };
            new GuiBitmapButtonCtrl(portallistclearbutton) {
               profile = "JVS_BlockButtonProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "139 293";
               extent = "91 38";
               minExtent = "8 2";
               visible = "1";
               command = "clearthisportal();";
               text = "Clear";
               groupNum = "-1";
               buttonType = "PushButton";
               bitmap = "base/client/ui/button2";
               lockAspectRatio = "0";
               alignLeft = "0";
               overflowImage = "0";
               mKeepCached = "0";
               mColor = "255 75 0 255";
            };
            new GuiTextEditCtrl(description2) {
               profile = "GuiTextEditProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "233 205";
               extent = "90 18";
               minExtent = "8 2";
               visible = "1";
               maxLength = "255";
               historySize = "0";
               password = "0";
               tabComplete = "0";
               sinkAllKeyEvents = "0";
            };
            new GuiRadioCtrl(restrict42) {
               profile = "GuiRadioProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "293 259";
               extent = "58 22";
               minExtent = "8 2";
               visible = "1";
               text = "Personal";
               groupNum = "4";
               buttonType = "RadioButton";
            };
            new GuiRadioCtrl(restrict32) {
               profile = "GuiRadioProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "205 259";
               extent = "65 22";
               minExtent = "8 2";
               visible = "1";
               text = "Mini-Game";
               groupNum = "4";
               buttonType = "RadioButton";
            };
            new GuiRadioCtrl(restrict22) {
               profile = "GuiRadioProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "317 231";
               extent = "53 22";
               minExtent = "8 2";
               visible = "1";
               text = "Supers";
               groupNum = "4";
               buttonType = "RadioButton";
            };
            new GuiRadioCtrl(restrict12) {
               profile = "GuiRadioProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "247 231";
               extent = "50 22";
               minExtent = "8 2";
               visible = "1";
               text = "Admins";
               groupNum = "4";
               buttonType = "RadioButton";
            };
            new GuiRadioCtrl(restrict02) {
               profile = "GuiRadioProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "186 230";
               extent = "43 22";
               minExtent = "8 2";
               visible = "1";
               text = "None";
               groupNum = "4";
               buttonType = "RadioButton";
            };
            new GuiSwatchCtrl(portalsettingsswatch) {
               profile = "GuiDefaultProfile";
               horizSizing = "right";
               vertSizing = "height";
               position = "130 195";
               extent = "296 145";
               minExtent = "8 2";
               visible = "1";
               color = "240 239 248 187";
            };
         };
         new GuiControl(portalsystemmanageallGUI) {
            profile = "GuiDefaultProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "0 46";
            extent = "500 425";
            minExtent = "8 2";
            visible = "0";

            new GuiSwatchCtrl() {
               profile = "GuiDefaultProfile";
               horizSizing = "left";
               vertSizing = "top";
               position = "173 44";
               extent = "192 306";
               minExtent = "8 2";
               visible = "1";
               color = "255 255 255 128";
            };
            new GuiScrollCtrl() {
               profile = "ColorScrollProfile";
               horizSizing = "left";
               vertSizing = "top";
               position = "173 44";
               extent = "192 306";
               minExtent = "8 2";
               visible = "1";
               willFirstRespond = "1";
               hScrollBar = "alwaysOff";
               vScrollBar = "alwaysOn";
               constantThumbHeight = "0";
               childMargin = "0 0";
               rowHeight = "10";
               columnWidth = "30";

               new GuiTextListCtrl(lstPortalManagementList) {
                  profile = "GuiTextListProfile";
                  horizSizing = "left";
                  vertSizing = "height";
                  position = "0 0";
                  extent = "192 16";
                  minExtent = "8 2";
                  visible = "1";
                  enumerate = "0";
                  resizeCell = "0";
                  columns = "0 50 135";
                  fitParentWidth = "1";
                  clipColumnText = "1";
                     sortedNumerical = "1";
                     helpTag = "0";
                     sortedAsc = "0";
                     sortedBy = "0";
               };
            };
            new GuiBitmapButtonCtrl() {
               profile = "BlockButtonProfile";
               horizSizing = "left";
               vertSizing = "top";
               position = "319 360";
               extent = "91 38";
               minExtent = "8 2";
               visible = "1";
               command = "messageboxokcancel(\"Clear ALL Portals?\",\"Are you sure you want to destroy ALL of the Portals?\",\"clearallportals();\",\"\");";
               text = "CLEAR ALL";
               groupNum = "-1";
               buttonType = "PushButton";
               bitmap = "base/client/ui/button2";
               lockAspectRatio = "0";
               alignLeft = "0";
               overflowImage = "0";
               mKeepCached = "0";
               mColor = "255 0 0 255";
            };
            new GuiBitmapButtonCtrl() {
               profile = "BlockButtonProfile";
               horizSizing = "left";
               vertSizing = "top";
               position = "129 360";
               extent = "91 38";
               minExtent = "8 2";
               visible = "1";
               command = "clearclientportals();";
               text = "Clear";
               groupNum = "-1";
               buttonType = "PushButton";
               bitmap = "base/client/ui/button2";
               lockAspectRatio = "0";
               alignLeft = "0";
               overflowImage = "0";
               mKeepCached = "0";
               mColor = "255 150 0 255";
            };
            new GuiTextCtrl() {
               profile = "JVS_GuiWindowProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "185 24";
               extent = "46 18";
               minExtent = "8 2";
               visible = "1";
               text = "Name:";
               maxLength = "255";
            };
            new GuiTextCtrl() {
               profile = "JVS_GuiWindowProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "304 24";
               extent = "58 18";
               minExtent = "8 2";
               visible = "1";
               text = "# Doors:";
               maxLength = "255";
            };
            new GuiTextCtrl() {
               profile = "JVS_GuiWindowProfile";
               horizSizing = "right";
               vertSizing = "bottom";
               position = "137 0";
               extent = "275 18";
               minExtent = "8 2";
               visible = "1";
               text = "Select the player whose Portals you wish to clear:";
               maxLength = "255";
            };
         };
         new GuiControl(portalsystemsaveGUI) {
            profile = "GuiDefaultProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "28 25";
            extent = "500 227";
            minExtent = "8 2";
            visible = "0";

            new GuiTextCtrl() {
               profile = "JVS_GuiWindowProfile";
               horizSizing = "center";
               vertSizing = "bottom";
               position = "8 25";
               extent = "201 18";
               minExtent = "8 2";
               visible = "1";
               text = "Type a filename for your Portal Save:";
               maxLength = "255";
            };
            new GuiTextEditCtrl(txteditName) {
               profile = "GuiTextEditProfile";
               horizSizing = "center";
               vertSizing = "bottom";
               position = "10 53";
               extent = "189 18";
               minExtent = "8 2";
               visible = "1";
               maxLength = "255";
               historySize = "0";
               password = "0";
               tabComplete = "0";
               sinkAllKeyEvents = "0";
            };
            new GuiBitmapButtonCtrl() {
               profile = "BlockButtonProfile";
               horizSizing = "center";
               vertSizing = "top";
               position = "59 82";
               extent = "91 38";
               minExtent = "8 2";
               visible = "1";
               command = "saveportals();";
               text = "Save";
               groupNum = "-1";
               buttonType = "PushButton";
               bitmap = "base/client/ui/button2";
               mKeepCached = "0";
               mColor = "255 255 255 255";
            };
         };
         new GuiControl(portalsystemloadGUI) {
            profile = "GuiDefaultProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "28 25";
            extent = "500 227";
            minExtent = "8 2";
            visible = "0";

            new GuiSwatchCtrl() {
               profile = "GuiDefaultProfile";
               horizSizing = "center";
               vertSizing = "height";
               position = "10 52";
               extent = "188 161";
               minExtent = "8 2";
               visible = "1";
               color = "255 255 255 128";
            };
            new GuiScrollCtrl() {
               profile = "ColorScrollProfile";
               horizSizing = "center";
               vertSizing = "bottom";
               position = "10 52";
               extent = "188 161";
               minExtent = "8 2";
               visible = "1";
               willFirstRespond = "1";
               hScrollBar = "alwaysOff";
               vScrollBar = "alwaysOn";
               constantThumbHeight = "0";
               childMargin = "0 0";
               rowHeight = "10";
               columnWidth = "30";

               new GuiTextListCtrl(lstPortalLoaderList) {
                  profile = "GuiTextListProfile";
                  horizSizing = "center";
                  vertSizing = "center";
                  position = "0 0";
                  extent = "176 2";
                  minExtent = "8 2";
                  visible = "1";
                  enumerate = "0";
                  resizeCell = "0";
                  columns = "0 120";
                  fitParentWidth = "1";
                  clipColumnText = "0";
                  helpTag = "0";
               };
            };
            new GuiTextCtrl() {
               profile = "JVS_GuiWindowProfile";
               horizSizing = "center";
               vertSizing = "bottom";
               position = "11 25";
               extent = "175 18";
               minExtent = "8 2";
               visible = "1";
               text = "Choose the Portal save to load:";
               maxLength = "255";
            };
         };
      };
      new GuiControl(windowsystemGUI) {
         profile = "GuiDefaultProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "0 81";
         extent = "354 208";
         minExtent = "8 2";
         visible = "0";
      };
   };
};

		}

		if(!isObject(JVSSoundDescription))
		{
		new AudioDescription(JVSSoundDescription)
		{
			volume = 1.0;
			isLooping = false;
			is3D = false;
			type = $DefaultAudioType;
		};
		}

		if(!isObject(DoorsAndPortalsClearProfile))
		{
		new AudioProfile(DoorsAndPortalsClearProfile) 
		{ 
			filename = "base/data/sound/brickClear.wav"; 
			description = "JVSSoundDescription"; 
			preload = false; 
		};
		}

		if(!isObject(DoorOrPortalLoadCompleteProfile))
		{
		new AudioProfile(DoorOrPortalLoadCompleteProfile) 
		{ 
			filename = "base/data/sound/processComplete.wav"; 
			description = "JVSSoundDescription"; 
			preload = false; 
		};
		}

		if(!isObject(DoorOrPortalLoadStartProfile))
		{
		new AudioProfile(DoorOrPortalLoadStartProfile) 
		{ 
			filename = "base/data/sound/uploadStart.wav"; 
			description = "JVSSoundDescription"; 
			preload = false; 
		};
		}

		addMessageCallback('MsgClearDoorList',handleClearDoorList);
		addMessageCallback('MsgClearDoorList2',handleClearDoorList2);
		addMessageCallback('MsgClearDoorListGui',handleClearDoorListGui);
		addMessageCallback('MsgClearDoorsAndPortals',handleClearDoorsAndPortals);
		addMessageCallback('MsgClearManageList',handleClearManageList);
		addMessageCallback('MsgClearManageList2',handleClearManageList2);
		addMessageCallback('MsgClearPortalList',handleClearPortalList);
		addMessageCallback('MsgClearPortalListGui',handleClearPortalListGui);
		addMessageCallback('MsgCloseJVSGUI',handleCloseJVSGUI);
		addMessageCallback('MsgCreateDoor',handleCreateDoor);
		addMessageCallback('MsgCreatePortal',handleCreatePortal);
		addMessageCallback('MsgDoorLoad',handleDoorLoad);
		addMessageCallback('MsgDoorOrPortalLoadComplete',handleDoorOrPortalLoadComplete);
		addMessageCallback('MsgDoorOrPortalLoadStart',handleDoorOrPortalLoadStart);
		addMessageCallback('MsgDoorSave',handleDoorSave);
		addMessageCallback('MsgGetBrickProperties',handleGetBrickProperties);
		addMessageCallback('MsgManageDoors',handleManageDoors);
		addMessageCallback('MsgManagePortals',handleManagePortals);
		addMessageCallback('MsgOpenDoorListGui',handleOpenDoorListGui);
		addMessageCallback('MsgOpenPortalListGui',handleOpenPortalListGui);
		addMessageCallback('MsgOpenJVSGUI',handleOpenJVSGUI);
		addMessageCallback('MsgPortalLoad',handlePortalLoad);
		addMessageCallback('MsgPortalSave',handlePortalSave);
		addMessageCallback('MsgReturnDoorProperties',handleReturnDoorProperties);
		addMessageCallback('MsgReturnPortalProperties',handleReturnPortalProperties);
		addMessageCallback('MsgSortDoorList',handleSortDoorList);
		addMessageCallback('MsgSortDoorList2',handleSortDoorList2);
		addMessageCallback('MsgSortDoorListGui',handleSortDoorListGui);
		addMessageCallback('MsgSortManageList',handleSortManageList);
		addMessageCallback('MsgSortManageList2',handleSortManageList2);
		addMessageCallback('MsgSortPortalList',handleSortPortalList);
		addMessageCallback('MsgSortPortalListGui',handleSortPortalListGui);
		addMessageCallback('MsgUpdateDoorList',handleUpdateDoorList);
		addMessageCallback('MsgUpdateDoorList2',handleUpdateDoorList2);
		addMessageCallback('MsgUpdateDoorListGui',handleUpdateDoorListGui);
		addMessageCallback('MsgUpdateManageList',handleUpdateManageList);
		addMessageCallback('MsgUpdateManageList2',handleUpdateManageList2);
		addMessageCallback('MsgUpdatePortalList',handleUpdatePortalList);
		addMessageCallback('MsgUpdatePortalListGui',handleUpdatePortalListGui);
		activatePackage(commonClientOverrides);
	}

	if($JVS::ClientMods::Doors == 1 && isFile("add-ons/jvs/doors/client.code"))
	{
		echo("JVS Mod :: Doors :: Client Init");
		exec("add-ons/jvs/doors/client.code");
	}
	else
	{
		$JVS::ClientMods::Doors = 0;
	}

	if($JVS::ClientMods::Portals == 1 && isFile("add-ons/jvs/portals/client.code"))
	{
		echo("JVS Mod :: Portals :: Client Init");
		exec("add-ons/jvs/portals/client.code");
	}
	else
	{
		$JVS::ClientMods::Portals = 0;
	}

	if($JVS::ClientMods::Spectator == 1 && isFile("add-ons/jvs/spectator/client.code"))
	{
		echo("JVS Mod :: Spectator :: Client Init");
		exec("add-ons/jvs/spectator/client.code");
	}
	else
	{
		$JVS::ClientMods::Spectator = 0;
	}
}