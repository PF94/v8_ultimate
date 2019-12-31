if(!$AddOn__Script_InServCmds)
{
	exec("./Script_InServCmds.cs");
}

function isClean(%Client,%msg,%Filter)
{
	switch$(%Filter)
	{
		case "eTard":
			%FilterEnabled = $Pref::server::eTardFilter;
			%FilterList = $Pref::Server::ETardList;
			%FilterMessage = '\c5This is a civilized game.  Please use full words.';
		case "Curse":
			%FilterEnabled = $Pref::server::CurseFilter;
			%FilterList = $Pref::Server::CurseList;
			%FilterMessage = '\c5Do not use foul language.';
	}
	switch(%FilterEnabled)
	{
		case 0:return 1;
		case 1:return chatFilter(%client,%msg,%FilterList,%FilterMessage);
	
	}
}

function MessageLen(%msg)
{
	if(strlen(%msg)>$Pref::Server::MaxChatLen)
	{
		%msg = getSubStr(%msg,0,$Pref::Server::MaxChatLen);
	}
	return %msg;
}

function messageAllAdmin(%msgType,%msgString,%a1,%a2,%a3,%a4,%a5,%a6,%a7,%a8,%a9,%a10,%a11,%a12,%a13)
{
	for(%i = 0;%i < ClientGroup.getCount();%i++)
	{
		%Cl = ClientGroup.getObject(%i);
		if(%Cl.isAdmin || %Cl.isSuperAdmin)
		{
			messageClient(%Cl,%msgType,%msgString,%a1,%a2,%a3,%a4,%a5,%a6,%a7,%a8,%a9,%a10,%a11,%a12,%a13);
		}
	}
}

function messageAdmin(%msgType,%msgString,%a1,%a2,%a3,%a4,%a5,%a6,%a7,%a8,%a9,%a10,%a11,%a12,%a13)
{
	for(%i = 0;%i < ClientGroup.getCount();%i++)
	{
		%Cl = ClientGroup.getObject(%i);
		if(!%Cl.isSuperAdmin && %Cl.isAdmin)
		{
			messageClient(%Cl,%msgType,%msgString,%a1,%a2,%a3,%a4,%a5,%a6,%a7,%a8,%a9,%a10,%a11,%a12, %a13);
		}
	}
}

function messageSuperAdmin(%msgType,%msgString,%a1,%a2,%a3,%a4,%a5,%a6,%a7,%a8,%a9,%a10,%a11,%a12,%a13)
{
	for(%i = 0; %i < ClientGroup.getCount(); %i++)
	{
		%Cl = ClientGroup.getObject(%i);
		if(%Cl.isSuperAdmin)
		{
			messageClient(%Cl,%msgType,%msgString,%a1,%a2,%a3,%a4,%a5,%a6,%a7,%a8,%a9,%a10,%a11,%a12,%a13);
		}
	}
}

function serverCmdAdminMessageSent(%Client,%msg)
{
	if(%Client.isAdmin || %Client.isSuperAdmin)
	{
		if(!strlen(%msg)){return;}
		%msg = MessageLen(%msg);
		switch(isClean(%Client,%msg,"Curse"))
		{
			case 0 :return;
			case 1 :switch(isClean(%Client,%msg,"eTard"))
				{
					case 0 :return;
					case 1 :if(%Client.bl_id $= $Pref::Server::Host)
						{
							%Admin = "Host";
						}
						else if(%Client.isSuperAdmin)
						{
							%Admin = "Super Admin";
						}
						else if(%Client.isAdmin)
						{
							%Admin = "Admin";
						}
						messageAllAdmin(%client,'[\c3%1\c0] \c7%2\c3%3\c7%4\c6: \c0%5',%Admin,%client.ClanPrefix,%client.name,%client.ClanSuffix,%msg);
				}
		}
	}
}

function serverCmdPmMessageSent(%Client,%pmClient,%Pm)
{
	if(!strlen(%Pm)){return;}
	%Pm = MessageLen(%Pm);
	switch(isClean(%Client,%Pm,"Curse"))
	{
		case 0 :return;
		case 1 :switch(isClean(%Client,%Pm,"eTard"))
			{
				case 0 :return;
				case 1 :centerPrint(%Client,"\c6PM Sent.",5,1);
					centerPrint(%pmClient,"\c6You have a new PM.",5,1);
					commandToClient(%pmClient,'AddMessage',%Client.name,%Pm);
			}
	}
}

function serverCmdAnnounce(%Client,%a1,%a2,%a3,%a4,%a5,%a6,%a7,%a8,%a9,%a10,%a11,%a12,%a13,%a14,%a15)
{
	if(%Client.isAdmin || %Client.isSuperAdmin)
	{
		%Announce = %a1 SPC %a2 SPC %a3 SPC %a4 SPC %a5 SPC %a6 SPC %a7 SPC %a8 SPC %a9 SPC %a10 SPC %a11 SPC %a12 SPC %a13 SPC %a14 SPC %a15;
		if(!strlen(%Announce)){return;}
		%Announce = MessageLen(%Announce);
		switch(isClean(%Client,%Announce,"Curse"))
		{
			case 0 :return;
			case 1 :switch(isClean(%Client,%Announce,"eTard"))
				{
					case 0 :return;
					case 1 : bottomPrintAll("\c3Announcement: "@"\c0"@%Announce,25,3);
				}
		}
	}
}

function serverCmdWhis(%Client,%whisClient,%a1,%a2,%a3,%a4,%a5,%a6,%a7,%a8,%a9,%a10,%a11,%a12,%a13,%a14,%a15)
{
	%whisClient = findClientByName(%whisClient);
	%Msg = %a1 SPC %a2 SPC %a3 SPC %a4 SPC %a5 SPC %a6 SPC %a7 SPC %a8 SPC %a9 SPC %a10 SPC %a11 SPC %a12 SPC %a13 SPC %a14 SPC %a15;
	if(strlen(%Msg) == 0){return;}
	%Msg = MessageLen(%Msg);
	if(%whisClient != 0)
	{
		switch(isClean(%Client,%pm,"Curse"))
		{
			case 0 :return;
			case 1 :switch(isClean(%Client,%pm,"eTard"))
				{
					case 0 :return;
					case 1 :messageClient(%whisClient, '', '[\c3Whis\c0] \c3%1\c6: %2',%Client.name,%Msg);
						messageClient(%Client, '', '[\c3Whis to %1\c0]\c6: %2', %whisClient.name,%Msg);
				}
		}
	}
	else
	{
		messageClient(%Client,'',"Player not found!");
	}
}