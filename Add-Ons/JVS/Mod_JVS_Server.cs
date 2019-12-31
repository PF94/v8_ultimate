exec("add-ons/ADD_ON_LIST.cs");

//Server Overrides

package commonServerOverrides
{
	function Armor::onDisabled(%datablock,%player,%enabled)
	{
		Parent::onDisabled(%datablock,%player,%enabled);

		if($JVS::ServerMods::Spectator == 1 && isFile("add-ons/jvs/spectator/server.code"))
		{
			%client = %player.client;

			if((%client.minigame == 0 || %client.minigame $= "") && (returnSpectator() || %client.isAdmin || %client.isSuperAdmin))
			{
				schedule(100,0,"serverCmdNoClip",%client);
			}
			else if(%client.minigame != 0 && %client.minigame !$= "" && returnMGSpectator())
			{
				schedule(100,0,"serverCmdNoClip",%client);
			}
		}
		else
		{
			$JVS::ServerMods::Spectator = 0;
		}

		if($JVS::ServerMods::Doors == 1 && isFile("add-ons/jvs/doors/server.code"))
		{
			if(%player.inDoorway)
			{
				%door = %player.door;

				for(%i = 0;%i <= 90;%i++)	
				{
					if(%door.closesched[%i])
					{
						%doorhasclosesched = 1;
						break;
					}		
				}

				%playersindoorway = 0;
				%player.inDoorway = 0;
				%player.door = "";

				for(%i = 0;%i < ClientGroup.getCount();%i++)
				{
					%cl = ClientGroup.getObject(%i);

					if(%cl.Player.inDoorway == 1 & %cl.Player.door == %door)
					{
						%playersindoorway++;
					}
				}

				if(%playersindoorway == 0 && !%doorhasclosesched)
				{
					%cycles2 = 0;

					if(%door.spawnBrick.rotdir == 0)
					{
						for(%i = %door.endRot;%i <= %door.startRot;%i++)
						{
							%cycles2++;

							if(%i < %door.startRot)
							{
								%door.closesched[%cycles2 - 1] = schedule(%cycles * 5 + 3000 + %cycles2 * 5,0,"serverCmdCloseDoor",%obj.client,%door,%i,1,1,%cycles2 - 1);
							}
							else if(%i == %door.startRot)
							{
								%door.closesched[%cycles2 - 1] = schedule(%cycles * 5 + 3000 + %cycles2 * 5,0,"serverCmdCloseDoor",%obj.client,%door,%i,0,0,%cycles2 - 1);
							}
						}
					}
					else if(%door.spawnBrick.rotdir == 1)
					{
						for(%i = %door.endRot;%i >= %door.startRot;%i--)
						{
							%cycles2++;

							if(%i > %door.startRot)
							{
								%door.closesched[%cycles2 - 1] = schedule(%cycles * 5 + 3000 + %cycles2 * 5,0,"serverCmdCloseDoor",%obj.client,%door,%i,1,1,%cycles2 - 1);
							}
							else if(%i == %door.startRot)
							{
								%door.closesched[%cycles2 - 1] = schedule(%cycles * 5 + 3000 + %cycles2 * 5,0,"serverCmdCloseDoor",%obj.client,%door,%i,0,0,%cycles2 - 1);
							}
						}
					}
				}
			}
		}
		else
		{
			$JVS::ServerMods::Doors = 0;
		}
	}

	function eulerToMatrix(%euler)
	{
		%euler = VectorScale(%euler,$pi / 180);
		%matrix = MatrixCreateFromEuler(%euler);
		%xvec = getWord(%matrix,3);
		%yvec = getWord(%matrix,4);
		%zvec = getWord(%matrix,5);
		%ang  = getWord(%matrix,6);
		%rotationMatrix = %xvec @ " " @ %yvec @ " " @ %zvec @ " " @ %ang;  
		return %rotationMatrix;
	}

	function GameConnection::OnClientEnterGame(%client)
	{
		if($JVS::ServerMods::Doors && isFile("add-ons/jvs/doors/server.code"))
		{
			%client.serverhasdoorsystem = 1;
			messageClient(%client,'MsgClearDoorList','');
			messageClient(%client,'MsgClearDoorList2','');
			messageClient(%client,'MsgUpdateDoorList',''," NONE ",0);
			messageClient(%client,'MsgUpdateDoorList2',''," NONE ",0);

			for(%i = 0;%i < DoorSO.getCount();%i++)
			{
				messageClient(%client,'MsgUpdateDoorList','',DoorSO.getDisplayName(%i),%i + 1);
				messageClient(%client,'MsgUpdateDoorList2','',DoorSO.getDisplayName(%i),%i + 1);
			}

			messageClient(%client,'MsgSortDoorList','');
			messageClient(%client,'MsgSortDoorList2','');
		}

		if($JVS::ServerMods::Portals && isFile("add-ons/jvs/portals/server.code"))
		{
			%client.serverhasportalsystem = 1;
		}

		%client.jvswrench = 0;
		Parent::OnClientEnterGame(%client);
	}

	function sendDoorWrenchTrustMessage(%client,%owner)
	{
		commandtoclient(%client,'centerprint',"\c0" @ %owner.name @ " does not trust you enough to do that.",2,2,5000);
	}

	function serverCmdGetBrickProperties(%client)
	{
		if(%client.wrenchBrick.isPortal)
		{
			%portalrestrict = %client.wrenchBrick.restrictportal;
			%portaldesc = %client.wrenchBrick.PortalDesc;
		}
		else
		{
			%portalrestrict = 0;
			%portaldesc = "";
		}

		if(%client.wrenchBrick.isDoor)
		{
			%doorrestrict = %client.wrenchBrick.restrictdoor;
			%doordesc = %client.wrenchBrick.DoorDesc;
			%door = %client.wrenchBrick.door;
			%itemdir = %door.spawnBrick.itemdir;
			%rotdir = %door.spawnBrick.rotdir;
			%doortype = DoorSO.getDisplayFromData(%door.dataBlock);
		}
		else
		{
			%doorrestrict = 0;
			%doordesc = "";
			%itemdir = 2;
			%rotdir = 0;
			%doortype = "NONE";
		}

		messageClient(%client,'MsgGetBrickProperties','',%portalrestrict,%portaldesc,%doorrestrict,%doordesc,%itemdir,%rotdir,%doortype);
	}

	function serverCmdTogJVSWrench(%client)
	{
		if(%client.hasSpawnedOnce)
		{
			if(!%client.jvswrench)
			{
				%client.jvswrench = 1;
				commandtoclient(%client,'centerprint',"\c0Your wrench is now in JVS mode.",2,2,5000);
			}
			else
			{
				%client.jvswrench = 0;
				commandtoclient(%client,'centerprint',"\c0Your wrench is now in normal mode.",2,2,5000);
			}
		}
	}

	function wrenchProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
	{
		if(%col.getClassName() $= "FxDTSBrick" && %obj.client.jvswrench == 1)
		{
			if(getTrustLevel(%obj, %col) >= 1)
			{
				%obj.client.wrenchBrick = %col;
				messageClient(%obj.client,'MsgOpenJVSGUI','',%col,%col.getGroup().name);
			}
			else
			{
				%client = %obj.client;
				%owner = %col.getGroup();
				sendDoorWrenchTrustMessage(%client,%owner);
			}
		}
		else
		{
			Parent::onCollision(%this,%obj,%col,%fade,%pos,%normal);
		}
	}
};

//Server Start-Up Code

if($AddOn__Mod_JVS_Server == 1)
{
	if(isFile("add-ons/jvs/servermods.code"))
	{
		echo("JVS Mod :: Server :: Mods Init");
		exec("add-ons/jvs/servermods.code");
	}
	else
	{
		echo("JVS Mod :: Server :: Mods Init");
		$JVS::ServerMods::Doors = 1;
		$JVS::ServerMods::Portals = 1;
		$JVS::ServerMods::Spectator = 1;
	}

	if($JVS::ServerMods::Doors == 1 && isFile("add-ons/jvs/doors/server.code"))
	{
		echo("JVS Mod :: Doors :: Server Init");
		exec("add-ons/jvs/doors/server.code");
	}
	else
	{
		$JVS::ServerMods::Doors = 0;
	}

	if($JVS::ServerMods::Portals == 1 && isFile("add-ons/jvs/portals/server.code"))
	{
		echo("JVS Mod :: Portals :: Server Init");
		exec("add-ons/jvs/portals/server.code");
	}
	else
	{
		$JVS::ServerMods::Portals = 0;
	}

	if($JVS::ServerMods::Spectator == 1 && isFile("add-ons/jvs/spectator/server.code"))
	{
		echo("JVS Mod :: Spectator :: Server Init");
		exec("add-ons/jvs/spectator/server.code");
	}
	else
	{
		$JVS::ServerMods::Spectator = 0;
	}

	if(isFile("add-ons/jvs/serverprefs.code"))
	{
		echo("JVS Mod :: Server :: Preferences Init");
		exec("add-ons/jvs/serverprefs.code");
	}
	else
	{
		echo("JVS Mod :: Server :: Preferences Init");
		$JVS::Server::ClientsMakePortals = 1;
		$JVS::Server::ClientsUploadPortals = 0;
		$JVS::Server::ClientsUsePortals = 1;
		$JVS::Server::MGSpectator = 1;
		$JVS::Server::RandomMode = 0;
		$JVS::Server::Spectator = 1;
	}

	ActivatePackage(commonServerOverrides);
}