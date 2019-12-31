//Regenerating shield armors by Mr. Wallet (688)

//Shielded Player is a normal player with regenerating shields
//Fuel-Shielded Player is a fuel-jet player (double max energy) with shields.




datablock PlayerData(PlayerShielded : PlayerStandardArmor)
{
	minJetEnergy = 0;
	jetEnergyDrain = 0;

	uiName = "Shielded Player";
	showEnergyBar = true;
	
	rechargeRate = "0.4";
};

datablock PlayerData(PlayerFuelShielded : PlayerShielded)
{
	minJetEnergy = 1;
	jetEnergyDrain = 1;

	uiName = "Fuel-Shielded Player";
};

datablock PlayerData(PlayerNoJetShielded : PlayerShielded)
{
	canJet = 0;

	uiName = "No-Jet Shielded Player";
};

datablock PlayerData(PlayerHALOShielded : PlayerNoJetShielded)
{
	uiName = "HALO-Shielded Player";
};

function PlayerShielded::Damage(%data, %plyr, %a, %b, %amt, %dmgtype)
{
	%nrg = %plyr.getEnergyLevel();
	if(%amt <= %nrg) {
		%plyr.setEnergyLevel(%nrg - %amt);
	} else {
		%plyr.setEnergyLevel(0);
		%amt -= %nrg;
		Parent::Damage(%data, %plyr, %a, %b, %amt, %dmgtype);
	}
}

function PlayerFuelShielded::Damage(%data, %plyr, %a, %b, %amt, %dmgtype)
{
	PlayerShielded::Damage(%data, %plyr, %a, %b, %amt, %dmgtype);
}

function PlayerHALOShielded::Damage(%data, %plyr, %a, %b, %amt, %dmgtype)
{
	PlayerShielded::Damage(%data, %plyr, %a, %b, %amt, %dmgtype);
	%plyr.setrechargerate(0);
	cancel(%plyr.unhurt);
	%plyr.unhurt = schedule(5000, 0, "HALOrecharge", %plyr);
}

function HALOrecharge(%this)
{
	if(isObject(%this)) %this.setrechargerate(0.4);
}