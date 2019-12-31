if (!$addedJet)
{
	$remapDivision[$remapCount] = "Auto-Jet";
	$remapName[$remapCount] = "Activate/Deacticate";
	$remapCmd[$remapCount] = "activateJet";
	$remapCount++;
	$remapName[$remapCount] = "Elevate";
	$remapCmd[$remapCount] = "autoJetHeightUp";
	$remapCount++;
	$remapName[$remapCount] = "Decend";
	$remapCmd[$remapCount] = "autoJetHeightDown";
	$remapCount++;
	$addedJet = true;
}

function autoJetHeightUp()
{
	$autoJetHeight++;
}

function autoJetHeightDown()
{
	$autoJetHeight--;
}

function autoJet(%player)
{
	if (!isObject(%player) || $autojetheight $= "")
	{
		$mvtriggercount[4] = 0;
		return;
	}
	%curHeight = getWord(%player.getPosition(), 2);
	%heightDif = %curHeight - $autojetheight;
	%curVel = getWord(%player.getVelocity(), 2);
//	if ($mvtriggercount[3] > 0) //someone can try fixing this if they want, it was going to stop the jet while crouching
//	{
//		$mvtriggerCount[4] = false;
//	}
	if (%curVel < -1)
	{
		$mvtriggercount[4] = true;
	}
	else if (%heightDif <= 0.2)
	{
		$mvtriggercount[4] = true;
	}
	else if (%heightDif > 0)
	{
		$mvtriggercount[4] = 0;
	}
	$jettick = schedule(50, 0, "autoJet", %player);
}

function findJetPlayer(%playername)
{
	for (%i = 0; %i < ServerConnection.getCount(); %i++)
	{
		%testObj = ServerConnection.getObject(%i);
		if (%testObj.getClassName() $= "Player")
		{
			if (%testObj.getShapeName() $= $pref::Player::NetName)
			{
				%player = %testObj;
				return %player;
			}
		}
	}
	return false;
}

function activateJet(%val)
{
	if (!%val) return;
	if (!isEventPending($jettick))
	{
		%player = findJetPlayer();
		if (isObject(%player))
		{
			$autojetheight = getWord(%player.getPosition(), 2);
			$jettick = schedule(0, 0, "autoJet", %player);		
		}
	}
	else
	{
		deactivateJet();
	}
}

function deactivateJet()
{
	if (isEventPending($jettick))
	{
		cancel($jettick);
		$mvTriggerCount[4] = false;
	}
}