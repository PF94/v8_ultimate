if(!isObject(HeadExplosionSound))
{
   exec("./Support_FoolyFunCmds.cs");
}

//-------Help--------------------------------------------------------------------------------------------------------------------------------------------------------------
function serverCmdHelp(%Client,%Arg)
{
	switch(%Arg)
	{
		case 0:
			commandToClient(%Client,'MessageBoxOK',"Fun Cmds Page 1","/NormalMe (Turns you back to normal)\n/ResizeMe X Y Z (XYZ are your diminsions)\n/CloakMe (Turns you invisible)\n/FloatingHeadMe (Turns you into a floating head)\n/SmurfMe (Turns you into a smurf)\n/MonkeyMe (Turns you into a monkey)\n/Help 2 (for Page 2)");
		case 1:
			commandToClient(%Client,'MessageBoxOK',"Fun Cmds Page 1","/NormalMe (Turns you back to normal)\n/ResizeMe X Y Z (XYZ are your diminsions)\n/CloakMe (Turns you invisible)\n/FloatingHeadMe (Turns you into a floating head)\n/SmurfMe (Turns you into a smurf)\n/MonkeyMe (Turns you into a monkey)\n/Help 2 (for Page 2)");
		case 2:
			commandToClient(%Client,'MessageBoxOK',"Fun Cmds Page 2","/HorseMe (Turns you into a horse)\n/SkeleMe (Turns you into a skeleton)\n/Mount partname bodypart (Mounts to player)\n/Respawn (Warps to spawn or gives new body)\n/ExplodeHead (Explodes your head)\n/Status status (Sets status)");
	}
}

function serverCmdAdminHelp(%Client,%Arg)
{
	switch(%Client.isSuperAdmin)
	{
		case 1:
			switch(%Arg)
			{
				case 0:
					commandToClient(%Client,'MessageBoxOK',"Admin Fun Cmds Page 1","/Fetchall (Fetchs everyone in server)\n/Kidnap (Blinds and fetchs a player)\n/Kidnapall (Blinds and fetchs all players)");
				case 1:
					commandToClient(%Client,'MessageBoxOK',"Admin Fun Cmds Page 1","/Fetchall (Fetchs everyone in server)\n/Kidnap (Blinds and fetchs a player)\n/Kidnapall (Blinds and fetchs all players)");
			}
	}
}

//--Respawn------------------------------------------------------------
function serverCmdReSpawn(%Client)
{
	if(!isObject(%Client.player))
	{
		%Client.spawnplayer();
	}
	switch(%Client.isImprisoned)
	{
		case 0:
			%Client.player.setTransform(pickSpawnPoint());
		case 1:
			cantDoMessage(%Client,"Jail");
	}
}

//--Fetch All----------------------------------------------------------
function serverCmdFetchAll(%Client)
{
	switch(%Client.isSuperAdmin)
	{
		case 1:
			for(%i=0;%i<ClientGroup.getCount();%i++)
			{
				%Cl = ClientGroup.getObject(%i);
				serverCmdFetch(%Client,%Cl.name);
			}
	}
}

//--Mount Player-------------------------------------------------------
function serverCmdMount(%Client,%Arg,%Arg2)
{
	%Victim = findClientByName(%Arg);
	%Trust = getTrustLevel(%Victim,%Client.player);
	if( %Client.minigame > 0 && %Client.minigame !$= %Victim.minigame || %Victim.minigame > 0 && %Client.minigame !$= %Victim.minigame)
	{
		commandToClient(%Client,'CenterPrint',"You are not in the same minigame.",2);
		return;
	}
	switch(%Client.isImprisoned)
	{
		case 0:
			if(%Trust < 1)
			{
				commandToClient(%Client,'CenterPrint',%Victim.name SPC "does not trust you enough to do that.",2);
				return;
			}
			switch$(%Arg2)
			{
				case   "Hat": %Pos = 5;
				case  "Head": %Pos = 2;
				case  "Body": %Pos = 7;
				case "LHand": %Pos = 1;
				case "RHand": %Pos = 0;
				case "Pants": %Pos = 8;
				case "LFoot": %Pos = 4;
				case "RFoot": %Pos = 3;
			}
			if(!%Pos)
			{
				commandToClient(%Client,'CenterPrint',"Mount point was not found.",2);
				return;
			}
			%Victim.player.mountObject(%Client.player,%Pos);
		case 1:
			cantDoMessage(%Client,"Jail");
	}
}

//--Explode Head----------------------------------------------------------------------
function serverCmdExplodeHead(%Client,%Arg)
{
	%Victim = findclientbyname(%Arg);
	if(%Client.isSuperAdmin || %Client.name $= %Victim.name)
	{
		%Victim.player.emote(HeadExplosionImage);
		for(%i=0;$accent[%i]!$="";%i++)%Victim.player.hideNode($accent[%i]);
		for(%i=0;$hat[%i]!$="";%i++)%Victim.player.hideNode($hat[%i]);
		%Victim.player.hideNode("headSkin");
		%Victim.player.unMountImage(2);
		%Victim.player.setShapeName('');
		if(%Client.name $= %Victim.name)
		{
			messageAll('','\c3%1 \c0has blown up their own head.',%Client.name);
		}
		else
		{
			messageAll('','\c3%1 \c0blew up \c3%2s \c0head.',%Client.name,%Victim.name);
		}
	}
	
}

//--Kidnap (Same as fetch but blinds the victim)--------------------------------------------------------------------------------
function serverCmdKidnap(%Client,%Arg)
{
	switch(%Client.isSuperAdmin)
	{
		case 1:
			%Victim = findclientbyname(%Arg);
			%Victim.player.setWhiteout(20);
			serverCmdFetch(%Client,%Victim.name);
	}
	
}

//--Kidnap All (Same as fetchall but blinds everyone)-----------------------------------------------------------------------------
function serverCmdKidnapAll(%Client)
{
	switch(%Client.isSuperAdmin)
	{
		case 1:
			for(%i = 0;%i < ClientGroup.getCount(); %i++)
			{
				%Cl = ClientGroup.getObject(%i);
				%Cl.player.setWhiteout(20);
				serverCmdFetch(%Client,%Cl.name);
			}
	}
}

//-------Resize------------------------------------------------------------------------------------------------------------------------------------
function serverCmdResizeMe(%Client,%Arg,%Arg2,%Arg3)
{
	if(%Client.minigame <= 0)
	{
		switch(%Client.isImprisoned)
		{
			case 0:
				switch(isObject(%Client.player))
				{
					case 0:
						cantDoMessage(%Client,"Dead");
					case 1:
						%Size = %Arg SPC %Arg2 SPC %Arg3;
						if(%Arg <=5 && %Arg2 <=5 && %Arg3<=5 && %Arg >= 0.1 && %Arg2 >= 0.1 && %Arg3 >= 0.1)
						{
							%Client.player.setScale(%Size);
						}
						else
						{
							commandToClient(%Client,'CenterPrint',"\c3Max: \c05\n\c3Min: \c00.1",3);
						}
				}
			case 1:
				cantDoMessage(%Client,"Jail");
		}
	}
	else
	{
		cantDoMessage(%Client,"MiniGame");
	}
}

//---------SetStatus-------------------------------------------------------------------------------------------------------------------------------
function serverCmdStatus(%Client,%Arg)
{
	if(%Arg !$= "" && strLen(%Arg) < 10)
	{
		%Client.Status = %Arg;
		%Status = "["@%Arg@"]" SPC %Client.name;
		%Client.player.setShapeName(%Status);
		messageClient(%Client,'','\c3Updated your status to \c0%1',%Arg);
	}
	else
	{
		%Client.Status = "";
		%Client.player.setShapeName(%Client.name);
		messageClient(%Client,'','\c3Updated your status to \c0Back');
	}
}

//----------Clear Nodes--------------------------------------------------------------------------------------------------------------------
function clearAllPlayerNodes(%Player)
{
	if (isObject(%Player))
	{
		%Player.hideNode("headSkin");
		%Player.hideNode("LSki");
		%Player.hideNode("RSki");
		%Player.hideNode("skirtTrimLeft");
		%Player.hideNode("skirtTrimRight");
		for (%i = 0; $accent[%i] !$= ""; %i++) %player.hideNode($accent[%i]);
		for (%i = 0; $chest[%i] !$= ""; %i++) %Player.hideNode($chest[%i]);
		for (%i = 0; $hat[%i] !$= ""; %i++) %Player.hideNode($hat[%i]);
		for (%i = 0; $hip[%i] !$= ""; %i++) %Player.hideNode($hip[%i]);
		for (%i = 0; $LArm[%i] !$= ""; %i++) %Player.hideNode($LArm[%i]);
		for (%i = 0; $LHand[%i] !$= ""; %i++) %Player.hideNode($LHand[%i]);
		for (%i = 0; $LLeg[%i] !$= ""; %i++) %Player.hideNode($LLeg[%i]);
		for (%i = 0; $pack[%i] !$= ""; %i++) %Player.hideNode($pack[%i]);
		for (%i = 0; $RArm[%i] !$= ""; %i++) %Player.hideNode($RArm[%i]);
		for (%i = 0; $RHand[%i] !$= ""; %i++) %Player.hideNode($RHand[%i]);
		for (%i = 0; $RLeg[%i] !$= ""; %i++) %Player.hideNode($RLeg[%i]);
		for (%i = 0; $secondPack[%i] !$= ""; %i++) %Player.hideNode($secondPack[%i]);
	}
}

//-------UnHideAllNodes------------------------------------------------------------------------------------------------------------------------
function UnHideAllNodes(%Client)
{
	%Player = %Client.player;
	if (isObject(%Player))
	{
		%Player.unHideNode("headSkin");
		%Player.setNodeColor("headSkin", %client.headColor);
		%Player.unHideNode($hat[%client.hat]);
		%Player.setNodeColor($hat[%client.hat], %client.hatColor);
		if(%Client.hat == 1 && %Client.accent != 0)
		{
			%Player.unHideNode($accent[4]);
		}
		else if(%Client.hat == 4 || %Client.hat == 6 || %Client.hat == 7 && %Client.accent != 0)
		{
			%Player.unHideNode($accent[%client.accent]);
		}
		%Player.setNodeColor($accent[%client.accent], %client.accentColor);
		%Player.unHideNode($chest[%client.chest]);
		%Player.setNodeColor($chest[%client.chest], %client.chestColor);
		%Player.unHideNode($hip[%client.hip]);
		%Player.setNodeColor($hip[%client.hip], %client.hipColor);
		%Player.unHideNode($larm[%client.larm]);
		%Player.setNodeColor($larm[%client.larm], %client.larmColor);
		%Player.unHideNode($lhand[%client.lhand]);
		%Player.setNodeColor($lhand[%client.lhand], %client.lhandColor);
		%Player.unHideNode($lleg[%client.lleg]);
		%Player.setNodeColor($lleg[%client.lleg], %client.llegColor);
		%Player.unHideNode($pack[%client.pack]);
		%Player.setNodeColor($pack[%client.pack], %client.packColor);
		%Player.unHideNode($rarm[%client.rarm]);
		%Player.setNodeColor($rarm[%client.rarm], %client.rarmColor);
		%Player.unHideNode($rhand[%client.rhand]);
		%Player.setNodeColor($rhand[%client.rhand], %client.rhandColor);
		%Player.unHideNode($rleg[%client.rleg]);
		%Player.setNodeColor($rleg[%client.rleg], %client.rlegColor);
		%Player.unHideNode($secondpack[%client.secondpack]);
		%Player.setNodeColor($secondpack[%client.secondpack], %client.secondpackColor);
		%Player.setFaceName(%client.faceName);
	}
}

//------Normal--------------------------------------------------------------------------------------------------------------------------------------
function serverCmdNormalMe(%Client)
{
	if(%Client.minigame <= 0)
	{
		switch(%Client.isImprisoned)
		{
			case 0:
				switch(isObject(%Client.player))
				{
					case 0:
						cantDoMessage(%Client,"Dead");
					case 1:
						%Player = %Client.player;
						%Player.setDataBlock("PlayerStandardArmor");
						%Player.setScale("1 1 1");
						ClearAllPlayerNodes(%Player);
						UnHideAllNodes(%Client);
						%Player.setShapeName(%Client.name);
						for(%i = 0; %i < 5;%i++)
						{
							%Player.unMountImage(%i);
						}
				}
			case 1:
				cantDoMessage(%Client,"Jail");
		}
	}
	else
	{
		cantDoMessage(%Client,"MiniGame");
	}
}

//-----------SmurfMe-----------------------------------------------------------------------------------------------------------------------------
function serverCmdSmurfMe(%Client)
{
	if(%Client.minigame <= 0)
	{
		switch(%Client.isImprisoned)
		{
			case 0:
				switch(isObject(%Client.player))
				{
					case 0:
						cantDoMessage(%Client,"Dead");
					case 1:
						%Player = %Client.player;
						serverCmdNormalMe(%Client);
						%LightBlue = "0.106 0.459 0.7969 1";
						%White = "1 1 1 1";
						ClearAllPlayerNodes(%Player);
						%Player.setScale("0.2 0.2 0.2");
						%Player.unhideNode(KnitHat);
						%Player.unhideNode(HeadSkin);
						%Player.unhideNode(Chest);
						%Player.unhideNode(Pants);
						%Player.unhideNode(LShoe);
						%Player.unhideNode(RShoe);
						%Player.unhideNode(LArm);
						%Player.unhideNode(RArm);
						%Player.unhideNode(LHand);
						%Player.unhideNode(RHand);
						%Player.setNodeColor(KnitHat,%White);
						%Player.setNodeColor(HeadSkin,%LightBlue);
						%Player.setNodeColor(Chest,%LightBlue);
						%Player.setNodeColor(Pants,%White);
						%Player.setNodeColor(LShoe,%White);
						%Player.setNodeColor(RShoe,%White);
						%Player.setNodeColor(LArm,%LightBlue);
						%Player.setNodeColor(RArm,%LightBlue);
						%Player.setNodeColor(LHand,%LightBlue);
						%Player.setNodeColor(RHand,%LightBlue);
						%Player.setDecalName("AAA-None");
				}
			case 1:
				cantDoMessage(%Client,"Jail");
		}
	}
	else
	{
		cantDoMessage(%Client,"MiniGame");
	}
}

//--------Skeleton----------------------------------------------------------------------------------------------------------------------------------
function serverCmdSkeleMe(%Client)
{
	if(%Client.minigame <= 0)
	{
		switch(%Client.isImprisoned)
		{
			case 0:
				switch(isObject(%Client.player))
				{
					case 0:
						cantDoMessage(%Client,"Dead");
					case 1:
						%Player = %Client.player;
						serverCmdNormalMe(%Client);
						ClearAllPlayerNodes(%Player);
						%Player.unhideNode(RHand);
						%Player.unhideNode(LHand);
						%Player.unhideNode(RArmSlim);
						%Player.unhideNode(LArmSlim);
						%Player.unhideNode(RShoe);
						%Player.unhideNode(LShoe);
						%Player.setNodeColor(RHand,"1 1 1 1");
						%Player.setNodeColor(LHand,"1 1 1 1");
						%Player.setNodeColor(RArmSlim,"1 1 1 1");
						%Player.setNodeColor(LArmSlim,"1 1 1 1");
						%Player.setNodeColor(RShoe,"1 1 1 1");
						%Player.setNodeColor(LShoe,"1 1 1 1");
						%Player.mountImage(skelheadImage,2);
						%Player.mountImage(skelbodyImage,1);
				}
			case 1:
				cantDoMessage(%Client,"Jail");
		}
	}
	else
	{
		cantDoMessage(%Client,"MiniGame");
	}
}

//-----------CloakMe-------------------------------------------------------------------------------------------------------------------------------
function serverCmdCloakMe(%Client)
{
	if(%Client.minigame <= 0)
	{
		switch(%Client.isImprisoned)
		{
			case 0:
				switch(isObject(%Client.player))
				{
					case 0:
						cantDoMessage(%Client,"Dead");
					case 1:
						serverCmdNormalMe(%Client);
						%Client.player.emote(NinjaPoofImage);
						clearAllPlayerNodes(%Client.player);
						%Client.player.setShapeName("");
				}
			case 1:
				cantDoMessage(%Client,"Jail");
		}
	}
	else
	{
		cantDoMessage(%Client,"MiniGame");
	}
}

//---------Floating Head-------------------------------------------------------------------------------------------------------------------------
function serverCmdFloatingHeadMe(%Client)
{
	if(%Client.minigame <= 0)
	{
		switch(%Client.isImprisoned)
		{
			case 0:
				switch(isObject(%Client.player))
				{
					case 0:
						cantDoMessage(%Client,"Dead");
					case 1:
						%Player = %Client.player;
						serverCmdNormalMe(%Client);
						clearAllPlayerNodes(%Player);
						%Player.unhideNode("HeadSkin");
						%Player.unHideNode($Hat[%Client.hat]);
						if(%Client.hat == 1 && %Client.accent != 0)
						{
							%Player.unHideNode($accent[4]);
						}
						else if(%Client.hat == 4 || %Client.hat == 6 || %Client.hat == 7 && %Client.accent != 0)
						{
							%Player.unHideNode($accent[%Client.accent]);
						}
				}
			case 1:
				cantDoMessage(%Client,"Jail");
		}
	}
	else
	{
		cantDoMessage(%Client,"MiniGame");
	}
}

//-------Horse-------------------------------------------------------------------------------------------------------------------------------
function serverCmdHorseMe(%Client)
{
	if(%Client.minigame <= 0)
	{
		switch(%Client.isImprisoned)
		{
			case 0:
				switch(isObject(%Client.player))
				{
					case 0:
						cantDoMessage(%Client,"Dead");
					case 1:
						serverCmdNormalMe(%Client);
						%Client.player.setDataBlock("HorseArmor");
				}
			case 1:
				cantDoMessage(%Client,"Jail");
		}
	}
	else
	{
		cantDoMessage(%Client,"MiniGame");
	}
}

//-------Monkey----------------------------------------------------------------------------
function serverCmdMonkeyMe(%Client)
{
	if(%Client.minigame <= 0)
	{
		switch(%Client.isImprisoned)
		{
			case 0:
				switch(isObject(%Client.player))
				{
					case 0:
						cantDoMessage(%Client,"Dead");
					case 1:
						serverCmdNormalMe(%Client);
						ClearAllPlayerNodes(%Client.player);
						%Client.player.mountimage(monkeyimage,2); 
				}
			case 1:
				cantDoMessage(%Client,"Jail");
		}
	}
	else
	{
		cantDoMessage(%Client,"MiniGame");
	}
}

//--Can't Do---------------------------------------------------------------------------
function cantDoMessage(%Client,%Type)
{
	switch$(%Type)
	{
		case "MiniGame":
			commandToClient(%Client,'CenterPrint',"You can't do that while in a minigame.",3);
		case "Dead":
			commandToClient(%Client,'CenterPrint',"You can't do that while dead.",3);
		case "Jail":
			commandToClient(%Client,'CenterPrint',"You can't do that while in jail.",3);
		case "ToLong":
			commandToClient(%Client,'CenterPrint',"That is to long.",3);
		case "Imposter":
			commandToClient(%Client,'CenterPrint',"You may not impersonate someone.",3);
	}
}