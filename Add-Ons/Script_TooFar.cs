package TooFar{
	function serverCmdPlantBrick(%client){
		%client.player.lastpos = %client.player.getPosition();
		%client.player.position = %client.player.tempbrick.getPosition();
		Parent::serverCmdPlantBrick(%client);
		%client.player.position = %client.player.lastpos;
		%client.player.lastpos = 0;
	}
};
ActivatePackage(TooFar);