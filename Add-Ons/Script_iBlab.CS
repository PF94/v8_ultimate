// Iban's iBlab
// $Iban::Server::iBlab == 1 is on.

package iBlab
{
	function ServerCmdMessageSent(%client, %message)
	{
		if($Iban::Server::iBlab::On == 0){$Iban::Server::iBlab::On = 1;}
		if($Iban::Server::iBlab::Timer == 0){$Iban::Server::iBlab::Timer = 2500;}
		if($Iban::Server::iBlab::tooLong == 0){$Iban::Server:iBlab::tooLong = 1;}
		if($Iban::Server::iBlab::On == 1 && $Iban::Server:iBlab::tooLong != 2)
		{
			%MTS = %message;
			if($Iban::Server::iBlab::tooLong == 3 && strlen(%message) > 50){%MTS = "Blah Blah Blah";}
			if($Iban::Server::iBlab::tooLong == 2 && strlen(%message) > 50){Parent::ServerCmdMessageSent(%client, %message);return;}
			
			%client.player.setShapeName(%client.name @ ":" SPC %MTS);
			cancel($Iban::Server::iBlabMsg[%client.BL_ID]);
			$Iban::Server::iBlabMsg[%client.BL_ID] = schedule($Iban::Server::iBlab::Timer,0,RemoveMessageFromHead,%client);
		}
			
		
		Parent::ServerCmdMessageSent(%client, %message);
	}
};
ActivatePackage(iBlab);

function RemoveMessageFromHead(%client)
{
	%client.player.setShapeName(%client.name);
	cancel($Iban::Server::BountyToggle[%client.BL_ID]);
}

function ServerCmdToggleiBlab(%client)
{
	if(%client.isSuperAdmin || %client.isAdmin)
	{
		switch($Iban::Server::iBlab::On)
		{
		case 0 : $Iban::Server::iBlab::On = 2; messageAll("","iBlab is now DISABLED");
		case 1 : $Iban::Server::iBlab::On = 2; messageAll("","iBlab is now DISABLED");
		case 2 : $Iban::Server::iBlab::On = 1; messageAll("","\c2iBlab is now ENABLED");
		}
	}
}

function ServerCmdiBlabTimer(%client,%time)
{
	if(%client.isSuperAdmin != 1 && %client.isAdmin != 1){return;}
	messageClient(%client,"","\c3The iBlab Over-Head timer is now" SPC %time SPC "seconds.");
	%time *= 1000;
	$Iban::Server::iBlab::Timer = %time;
}

function ServerCmdiBlabTooLong(%client,%opt)
{
	if(%client.isSuperAdmin != 1 && %client.isAdmin != 1){return;}
	switch(%opt)
	{
	case 1 : $Iban::Server::iBlab::tooLong = 1;%msg = "\c2show\c6";
	case 2 : $Iban::Server::iBlab::tooLong = 2;%msg = "\c0hide\c6";
	case 3 : $Iban::Server::iBlab::tooLong = 3;%msg = "\c3iBlah thc6";
	}
	messageClient(%client,"","\c6You have choosen to" SPC %msg SPC "messages that are very long.");
}