datablock TriggerData(KillTickTrigger)
{
	category = "KillTickTrigger";
	tickPeriodMS = 1000;
};

function KillTickTrigger::onTickTrigger(%this,%trigger)
{
	if (%trigger.damageType $= "")
	{
		error(%trigger SPC "has no damage type! Set it with .damageType = TriggerName");
		return;
	}
	if (%trigger.damagePercent $= "")
	{
		error(%trigger SPC "has no set damage percent! Set it with .damagePercent = 1-100");
		return;
	}
	eval("%damageType = $DamageType::" @ %trigger.damageType @ ";");
	for (%i = 0; %i < %trigger.getNumObjects(); %i++)
	{
		%obj = %trigger.getObject(%i);
		%className = %obj.getClassName();
		if (%className $= "Player" || %className $= "AIPlayer" || %className $= "WheeledVehicle" || %className $= "FlyingVehicle" || %className $= "HoverVehicle")
		{
			%damageAmount = %obj.getDataBlock().maxDamage * (%trigger.damagePercent / 100);
			echo(%damageAmount);
			%obj.damage(%trigger, getWords(%obj.getTransform(), 0, 2), %damageAmount, %damageType);
		}
	}
	Parent::onTickTrigger(%this,%trigger);
}
AddDamageType("SunDamage",   "%client has died from solar exposure");
AddDamageType("SpaceDamage",   "%client has drifted off into the far reaches of space");
AddDamageType("ColdDamage",   "%client has died from over-exposure to frigid temperatures");
