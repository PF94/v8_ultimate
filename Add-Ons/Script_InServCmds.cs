if(!$AddOn__Support_MessageCmds)
{
	exec("./Support_MessageCmds.cs");
}

function UpdatePlayList()
{
	for(%i = 0;%i < ClientGroup.getCount(); %i++)
	{
		%Cl = ClientGroup.getObject(%i);
		messageClient(%Cl,'MsgClientJoin','',%Cl.name,%Cl,%Cl.bl_id,%Cl.score,%Cl.minigame,%Cl.isAdmin,%Cl.isSuperAdmin);
	}
}

function serverCmdUpdateColors(%Client)
{
	CommandToClient(%Client,'UpdateButtonColor',"FallDamage",$Pref::Server::FallingDamage);
	CommandToClient(%Client,'UpdateButtonColor',"Etard",$Pref::Server::ETardFilter);
	CommandToClient(%Client,'UpdateButtonColor',"Curse",$Pref::Server::CurseFilter);
	CommandToClient(%Client,'UpdateButtonColor',"FloodProtect",$Pref::Server::FloodProtectionEnabled);
}

function serverCmdHostBLID(%Client,%Arg)
{
	%Local = findLocalClient();
	if(%Client $= %Local)
	{
		$Pref::Server::Host = %Arg;
		%Msg = "The host BLID has been set to" SPC %Arg;
		commandToClient(%Client,'CenterPrint',%Msg,2);
	}
	else
	{
		commandToClient(%Client,'CenterPrint',"You're not the host.",2);
	}
}

function serverCmdToggleFallDamage(%client)
{
	if(%Client.isAdmin || %Client.isSuperAdmin)
	{
		if(!$Pref::Server::FallingDamage)
		{
			$Pref::Server::FallingDamage = 1;
			messageAll("","\c3Fall Damage is now \c0ON.");
		}
		else
		{
			$Pref::Server::FallingDamage = 0;
			messageAll("","\c3Fall Damage is now \c0OFF.");
		}
		CommandToClient(%Client,'UpdateButtonColor',"FallDamage",$Pref::Server::FallingDamage);
	}
}

function serverCmdToggleEtard(%Client)
{
	if(%Client.isAdmin || %Client.isSuperAdmin)
	{
		if(!$Pref::Server::ETardFilter)
		{
			$Pref::Server::ETardFilter = 1;
			messageAll("","\c3E-Tard Filter is now \c0ON.");
		}
		else
		{
			$Pref::Server::ETardFilter = 0;
			messageAll("","\c3E-Tard Filter is now \c0OFF.");
		}
		CommandToClient(%Client,'UpdateButtonColor',"Etard",$Pref::Server::ETardFilter);
	}
}

function serverCmdToggleCurse(%client)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		if(!$Pref::Server::CurseFilter)
		{
			$Pref::Server::CurseFilter = 1;
			messageAll("","\c3Curse Filter is now \c0ON.");
		}
		else
		{
			$Pref::Server::CurseFilter = 0;
			messageAll("","\c3Curse Filter is now \c0OFF.");
		}
		CommandToClient(%Client,'UpdateButtonColor',"Curse",$Pref::Server::CurseFilter);
	}
}

function serverCmdToggleFloodProtection(%Client)
{
	if(%Client.isAdmin || %Client.isSuperAdmin)
	{
		if(!$Pref::Server::FloodProtectionEnabled)
		{
			$Pref::Server::FloodProtectionEnabled = 1;
			messageAll("","\c3Flood Protection is now \c0ON.");
		}
		else
		{
			$Pref::Server::FloodProtectionEnabled = 0;
			messageAll("","\c3Flood Protection is now \c0OFF.");
		}
		CommandToClient(%Client,'UpdateButtonColor',"FloodProtect",$Pref::Server::FloodProtectionEnabled);
	}
}

function serverCmdBrickLimit(%Client,%Brick)
{
	if(%Client.isSuperAdmin)
	{
		if(!%Brick)
		{
			$Pref::Server::BrickLimit = 0;
			messageall('','\c3Bricks may no longer be placed.');
		}
		else
		{
			$Pref::Server::BrickLimit = %brick;
			messageall('','\c3The servers brick limit is now:\c0%1',%Brick);
		}
	}
}

function serverCmdPass(%Client,%Pass)
{
	if(%Client.isSuperAdmin)
	{
		if(%Pass $= "")
		{
			$Pref::Server::Password = "";
			messageall('','\c3The server is no longer password protected.');
		}
		else
		{
			$Pref::Server::Password = %Pass;
			messageall('','\c3The server is now password protected.');
			messageAllAdmin('','\c3 The new server pass is: \c0%1',%Pass);
		}
	}
}

function serverCmdAdminPassEdit(%Client,%Pass)
{
	if(%Client.isAdmin || %Client.isSuperAdmin)
	{
		if(%Pass $= "")
		{
			$Pref::Server::AdminPassword = "";
			messageall('','\c3The server no longer has an admin password');
		}
		else
		{
			$Pref::Server::AdminPassword = %Pass;
			messageall('','\c3The admin password has been changed');
			messageAllAdmin('','\c3The new admin password is: \c0%1',%Pass);
		}
	}
}

function serverCmdSuperAdminPassEdit(%Client,%Pass)
{
	if(%Client.isSuperAdmin)
	{
		if(%Pass $= "")
		{
			$Pref::Server::SuperAdminPassword = "";
			messageall('','\c3The server no longer has a super admin password');
		}
		else
		{
			$Pref::Server::SuperAdminPassword = %Pass;
			messageall('','\c3The super admin password has been changed');
			messageSuperAdmin('','\c3The new super admin password is: \c0%1',%Pass);
		}
	}
}

function serverCmdWelcome(%Client,%Welcome)
{
	if(%Client.isSuperAdmin)
	{
		if(%Welcome $= "")
		{
			$Pref::Server::WelcomeMessage = "";
			messageall('','\c3The server no longer has a welcome message');
		}
		else
		{
			$Pref::Server::WelcomeMessage = "\c2 "@%Welcome@" %1";
			messageall('','\c3The welcome message is now: \c0%1',%Welcome);
		}
	}
}

function serverCmdSvrName(%Client, %Name)
{
	if(%Client.isSuperAdmin)
	{
		if(%Name $= "")
		{
			%Name = "Blockland Retail Server";
		}
		$Pref::Server::Name = %Name;
		messageall('','\c3The server name is now: \c0%1',%Name);
	}
}

function serverCmdPort(%Client,%Port)
{
	if(%Client.isSuperAdmin)
	{
		if(!%Port)
		{
			messageClient(%client,'','\c3The server must have a port');	
		}
		else
		{
			$Pref::Server::Port = %Port;
			messageall('','\c3The server is now using port: \c0%1',%Port);
		}
	}
}

function serverCmdClearChat(%Client)
{
	%Time = $Sim::Time - %Client.LastKillTime;
	if(%Time > 3)
	{	
		if(%Client.isAdmin || %Client.isSuperAdmin)
		{
			for(%i=0;%i<45;%i++)
			{
				messageall('',' ');
			}
		}
		%Client.LastKillTime = $Sim::Time;	
	}
	if(%Time < 3) 
	{
		messageClient(%Client,'','\c3You cannot clear the chat hud that often.');
	}
}

function serverCmdMaxP(%Client)
{
	if(%Client.isAdmin || %Client.isSuperAdmin)
	{
		if($Pref::Server::MaxPlayers < 64)
		{
			$Pref::Server::MaxPlayers++;
			messageall('','\c3Max Players increased by one to: \c0%1',$Pref::Server::MaxPlayers);
		}
		else
		{
			messageClient(%client, '','\c3You cannot raise the max players anymore');
		}
	}
}

function serverCmdMaxSub(%Client)
{
	if(%Client.isAdmin || %Client.isSuperAdmin)
	{
		%playerCount = ClientGroup.getCount();
		if($Pref::Server::MaxPlayers > %playerCount)
		{
			$Pref::Server::MaxPlayers--;
			messageall('','\c3Max Players decreased by one to: \c0%1',$Pref::Server::MaxPlayers);
		}
		else
		{
			messageClient(%Client,'','\c3You cannot lower the max players anymore');
		}
	}
}

function serverCmdgetPMList(%Client)
{
	for(%i = 0; %i < ClientGroup.getCount(); %i++)
	{
		%cl = ClientGroup.getObject(%i);
		if(%cl !$= %Client)
		{
			commandToClient(%Client,'UpdateList',"pmList",%cl,%cl.name);
		}
	}
}

function serverCmdgetAdminList(%Client)
{
	for(%i = 0; %i < ClientGroup.getCount(); %i++)
	{
		%cl = ClientGroup.getObject(%i);
		if(%Cl.bl_id $= $Pref::Server::Host)
		{
			%Name = ("[Host]"@%cl.name);
		}
		else if(%cl.isSuperAdmin && %cl.isAdmin)
		{
			%Name = ("[Super]"@%cl.name);
		}
		else if(%cl.isAdmin)
		{
			%Name = ("[Admin]"@%cl.name);
		}

		if(%Cl.bl_id $= $Pref::Server::Host || %cl.isSuperAdmin || %cl.isAdmin)
		{
			commandToClient(%client,'UpdateList',"AdminList",%cl,%Name);
		}
		else
		{
			commandToClient(%client,'UpdateList',"InServPlayerList",%cl,%cl.name);
		}
	}
}

function serverCmdgetAutoAdminList(%Client)
{
	%AdminList = $Pref::Server::AutoAdminList;
	%SuperList = $Pref::Server::AutoSuperAdminList;
	%AutoAdminCount = getWordCount(%AdminList);
	%AutoSuperCount = getWordCount(%SuperList);
	for(%i = 0; %i < %AutoAdminCount; %i++)
	{
		%AdminId = getword(%AdminList,%i);
		%Admin = findClientByBL_ID(%AdminId);
		if(!%Admin)
		{
			commandToClient(%client,'UpdateList',"AutoAdminList",%AdminId,%AdminId);
		}
		else
		{
			commandToClient(%client,'UpdateList',"AutoAdminList",%AdminId,%Admin.name);
		}
	}

	for(%i = 0; %j < %AutoSuperCount; %j++)
	{
		%SuperId = getword(%SuperList,%j);
		%Super = findClientByBL_ID(%SuperId);
		if(!%Super)
		{
			commandToClient(%client,'UpdateList',"AutoSuperList",%SuperId,%SuperId);
		}
		else
		{
			commandToClient(%client,'UpdateList',"AutoSuperList",%SuperId,%Super.name);
		}
	}

	
}

function serverCmdEditAutoList(%Client,%Selected,%Type)
{
	if(%Client.bl_id $= $Pref::Server::Host)
	{
		%AdminList = $Pref::Server::AutoAdminList;
		%SuperList = $Pref::Server::AutoSuperAdminList;
		switch$(%Type)
		{
			case "Super":
				if(getWordCount(%SuperList) == 0)
				{
					%NewSuperList = %Selected;
				}
				else
				{
					%NewSuperList = %SuperList SPC %Selected;
				}
				$Pref::Server::AutoSuperAdminList = %NewSuperList;
			case "Admin":
				if(getWordCount(%AdminList) == 0)
				{
					%NewAdminList = %Selected;
				}
				else
				{
					%NewAdminList = %AdminList SPC %Selected;
				}
				$Pref::Server::AutoAdminList = %NewAdminList;
			case "unSuper":
				%SuperListLen = getWordCount(%SuperList);
				if(getWord(%SuperList,0) == %Selected)
				{
					%NewSuperList = removeWord(%SuperList,0);
				}
				else if(getWord(%SuperList,%SuperListLen-1) == %Selected)
				{
					%NewSuperList = removeWord(%SuperList,%SuperListLen-1);
				}
				else if(%Selected !$="")
				{
					%NewSuperList = strReplace(%SuperList," "@%Selected@" "," ");
				}
				else
				{
					%NewSuperList = %SuperList;
				}
				$Pref::Server::AutoSuperAdminList = %NewSuperList;
			case "unAdmin":
				%AdminListLen = getWordCount(%AdminList);
				if(getWord(%AdminList,0) == %Selected)
				{
					%NewAdminList = removeWord(%AdminList,0);
				}
				else if(getWord(%AdminList,%AdminListLen-1) == %Selected)
				{
					%NewAdminList = removeWord(%AdminList,%AdminListLen-1);
				}
				else if(%Selected !$="")
				{
					%NewAdminList = strReplace(%AdminList," "@%Selected@" "," ");
				}
				else
				{
					%NewAdminList = %AdminList;
				}
				$Pref::Server::AutoAdminList = %NewAdminList;
		}
	}
	else
	{
		CommandToClient(%Client,'CenterPrint',"You are not the host",2);
	}
}

function serverCmdEditAdminList(%Client,%Selected,%Type)
{
	if(%Client.bl_id $= $Pref::Server::Host && %Selected.bl_id !$= $Pref::Server::Host)
	{
		switch$(%Type)
		{
			case "SuperAdmin":
				serverCmdSAD(%Selected,$Pref::Server::SuperAdminPassword);
			case "unSuperAdmin":
				if(%Selected.isSuperAdmin && %Selected.bl_id != $Pref::Server::Host)
				{
					%Selected.isSuperAdmin = 0;
					%Selected.isAdmin = 0;
					UpdatePlayList();
					messageAll('','\c2%1 has been UnSupered by host',%Selected.name);
				}
			case "Admin":
				serverCmdSAD(%Selected,$Pref::Server::AdminPassword);
			case "unAdmin":
				if(%Selected.isAdmin && !%Selected.isSuperAdmin && %Selected.bl_id != $Pref::Server::Host)
				{
					%Selected.isAdmin = 0;
					UpdatePlayList();
					messageAll('','\c2%1 has been UnAdmined by host',%Selected.name);
				}
		}
	}
	else
	{
		CommandToClient(%Client,'CenterPrint',"You are not the host or you tried to unadmin yourself",2);
	}
}