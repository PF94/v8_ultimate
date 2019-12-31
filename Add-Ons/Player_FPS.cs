//FPS Player by Fishpen0
//Credit to Regenerating shield armors by Mr. Wallet (688)

datablock PlayerData(PlayerFPS : PlayerStandardArmor)
{
	canjet=1;
	firstPersonOnly = 1;
	uiName = "FPS Player";
};

datablock PlayerData(PlayerFPSnojet : PlayerStandardArmor)
{
	canjet=0;
	firstPersonOnly = 1;
	uiName = "FPS No-Jet Player";
};

datablock PlayerData(PlayerFPSsheilded : PlayerStandardArmor)
{
	canjet=1;
	firstPersonOnly = 1;
	uiName = "FPS Sheilded Player";
	showEnergyBar = true;
	rechargeRate = "0.4";
};

function PlayerFPSsheilded::Damage(%data, %plyr, %a, %b, %amt, %dmgtype)
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