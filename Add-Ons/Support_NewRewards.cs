///////////////////////////////////////////////////////////////////
//                New Rewards Add-On - by TheGeek                //
///////////////////////////////////////////////////////////////////
//                                                               //
// Shows new reward messages like the mid-aired message:         //
//   * Blockead is on a RAMPAGE - 3 kills in a row!              //
//   * Blockead is on a KILLING SPREE - 6 kills in a row!        //
//   * Blockead is UNSTOPPABLE - 9 kills in a row!               //
//   * Blockead is DOMINATING - 12 kills in a row!               //
//   * Blockead is GOD-LIKE - 15 kills in a row!                 //
//   * HEADSHOT'd by Blockhead!                                  //
//                                                               //
// Includes following commands:                                  //
//   * /headshot [on|off]                                        //
//     Admin-only, turns on headshots. Headshots give instant    //
//     kills, and show a nice message :) Only works for ranged   //
//     weapons.                                                  //
//                                                               //
//   * /ratio [name]                                             //
//     Shows the kill/killed ratio for a certain player. When no //
//     nickname is given, it shows your own kill ratio.          //
//                                                               //
///////////////////////////////////////////////////////////////////

$TG_Headshots = 1;

package TG_NewRewards{
	function GameConnection::onDeath(%this, %sourceObject, %sourceClient, %damageType, %damLoc){
		Parent::onDeath(%this, %sourceObject, %sourceClient, %damageType, %damLoc);		

		//Reset victim's kill count
		%this.RewardKills = 0;

		//Kill ratio stuff
		%this.RatioKilled++;
		
		//Check for suicide
		if(%this == %sourceClient){
			return;
		}

		%sourceClient.RatioKills++;
		
		if(isObject(%sourceClient)){
			%sourceClient.RewardKills++;
			
			//Check for kills in a row
			switch(%sourceClient.RewardKills){
				case 3:
					ShowRewardAll("* is on a RAMPAGE - 3 kills in a row!", "You are on a RAMPAGE - 3 kills in a row!", %sourceClient);
				case 6:
					ShowRewardAll("* is on a KILLING SPREE - 6 kills in a row!", "You are on a KILLING SPREE - 6 kills in a row!", %sourceClient);
				case 9:
					ShowRewardAll("* is UNSTOPPABLE - 9 kills in a row!", "You are UNSTOPPABLE - 9 kills in a row!", %sourceClient);
				case 12:
					ShowRewardAll("* is DOMINATING - 12 kills in a row!", "You are DOMINATING - 12 kills in a row!", %sourceClient);
				case 15:
					ShowRewardAll("* is GOD-LIKE - 15 kills in a row!", "You are GOD-LIKE - 15 kills in a row!", %sourceClient);

			}
		}
	}
	function ProjectileData::damage(%this, %obj, %col, %fade, %pos, %normal){
		//Boom, headshot!
		if($TG_Headshots){
			if(%col.getClassName() $= "Player"){
				if(getword(%pos, 2) > getword(%col.getWorldBoxCenter(), 2) - 3.3){
					//Check if it's a ranged projectile
					%name = %this.GetName();
					if(%name $= "SniperrifleProjectile" || %name $= "rocketLauncherProjectile" || %name $= "gunProjectile" || %name $= "arrowProjectile" || %name $= "spearProjectile"){
						ShowReward("HEADSHOT'd by *!", "You HEADSHOT'd" SPC %col.client.name, %col.client, %obj.client);

						//hack: kill the victim, make sure the right kill message comes up
						Parent::damage(%this, %obj, %col, %fade, %pos, %normal);
						Parent::damage(%this, %obj, %col, %fade, %pos, %normal);
						Parent::damage(%this, %obj, %col, %fade, %pos, %normal);
						Parent::damage(%this, %obj, %col, %fade, %pos, %normal);
						Parent::damage(%this, %obj, %col, %fade, %pos, %normal);
						Parent::damage(%this, %obj, %col, %fade, %pos, %normal);
					}
				}
			}
		}

		return Parent::damage(%this, %obj, %col, %fade, %pos, %normal);
	}
	function ShowRewardAll(%rewardAll, %rewardKiller, %killer){
		%rewardAll = strreplace(%rewardAll, "*", %killer.name);
		%rewardKiller = strreplace(%rewardKiller, "*", %killer.name);

		%count = ClientGroup.getCount();

		for(%i = 0; %i < %count; %i++){
			%cl = ClientGroup.getObject(%i);
			if(!%cl.isAIControlled()){
				if(%cl == %killer){
					bottomPrint(%killer, "<bitmap:add-ons/ci/star> \c3" @ %rewardKiller, 3, 2);
				}else{
					bottomPrint(%cl, "\c5" @ %rewardAll, 3, 2);
				}
			}
		}
		%killer.player.emote(WinStarProjectile);
		%killer.play2d(rewardSound);
	}
	function ShowReward(%rewardVictim, %rewardKiller, %victim, %killer){
		%rewardVictim = strreplace(%rewardVictim, "*", %killer.name);
		%rewardKiller = strreplace(%rewardKiller, "*", %killer.name);

		bottomPrint(%victim, "\c5" @ %rewardVictim, 3, 2);
		bottomPrint(%killer, "<bitmap:add-ons/ci/star> \c3" @ %rewardKiller, 3, 2);

		%killer.player.emote(WinStarProjectile);
		%killer.play2d(rewardSound);
	}
	function serverCmdHeadshot(%client, %onoff){
		if(%client.isAdmin || %client.isSuperAdmin){
			if(%onoff $= "on"){
				$TG_Headshots = 1;
				MessageClient(%client, "", "\c5Headshots have been \c2enabled.");
			}else if(%onoff $= "off"){
				$TG_Headshots = 0;
				MessageClient(%client, "", "\c5Headshots have been \c0disabled.");
			}else{
				MessageClient(%client, "", "\c7Usage: \c5/headshot \c7[\c2on\c7|\c0off\c7]");
			}
		}
	}
	function serverCmdRatio(%client, %nick){
		if(%nick $= ""){
			if(%client.minigame != 0){
				MessageClient(%client, "", "\c3" @ %client.name @ "\c2's kill/killed ratio: \c3" @ %client.RatioKills @ "\c2:\c3" @ %client.RatioKilled);
			}else{
				MessageClient(%client, "", "You're not in a minigame");
			}
		}else{
			%cl = getClientByPartOfNick(%nick);
			if(%cl != -1){
				if(%cl.minigame != 0){
					MessageClient(%client, "", "\c3" @ %cl.name @ "\c2's kill/killed ratio: \c3" @ %cl.RatioKills @ "\c2:\c3" @ %cl.RatioKilled);
				}else{
					MessageClient(%client, "", "Client not in a minigame");
				}
			}else{
				MessageClient(%client, "", "Client not found");
			}
		}
	}
	function getClientByPartOfNick(%nick){
		%count = ClientGroup.getCount();
		
		for(%i = 0; %i < %count; %i++){
			%cl = ClientGroup.getObject(%i);
			if(!%cl.isAIControlled()){
				if(strstr(strlwr(%cl.name), strlwr(%nick)) != -1){
					return %cl;
				}
			}
		}

		return -1;
	}
	//WARNING: hacky code up ahead!
	//I've modified the behavior of BottomPrint/BottomPrintAll so that they will queue messages.
	function DecreaseBPDelay(%client){
		%client.bpdelay--;
	}
	function BottomPrint(%client, %message, %time, %lines){
		if( %lines $= "" || ((%lines > 3) || (%lines < 1)) ){
			%lines = 2;
		}

		if(%client.bpdelay == 0){
			commandtoclient(%client, '_bottomprint', %message, %time, %lines);
		}else{
			schedule(%client.bpdelay*1000, 0, "commandtoclient", %client, '_bottomprint', %message, %time, %lines);
		}
		for(%i = 0; %i != %time; %i++){
			schedule(%i*1000 + 1000, 0, "DecreaseBPDelay", %client);
		}
		%client.bpdelay += %time;
	}
	function BottomPrintAll(%message, %time, %lines){
		%count = ClientGroup.getCount();

		for(%i = 0; %i < %count; %i++){
			%cl = ClientGroup.getObject(%i);
			if(!%cl.isAIControlled()){
				BottomPrint(%cl, %message, %time, %lines);
			}
		}
	}
	function commandtoclient(%client, %command, %a, %b, %c, %d, %e, %f, %g, %h, %i, %j, %k, %l){
		if(getTaggedString(%command) $= "_bottomprint"){
			Parent::commandtoclient(%client, 'bottomprint', %a, %b, %c, %d, %e, %f, %g, %h, %i, %j, %k, %l);
		}else if(getTaggedString(%command) $= "bottomprint"){
			BottomPrint(%client, %a, %b, %c);
		}else if(getTaggedString(%command) $= "SetPlayingMinigame"){
			%client.RewardKills = 0;
			%client.RatioKilled = 0;
			%client.RatioKills = 0;
			Parent::commandtoclient(%client, %command, %a, %b, %c, %d, %e, %f, %g, %h, %i, %j, %k, %l);
		}else{
			Parent::commandtoclient(%client, %command, %a, %b, %c, %d, %e, %f, %g, %h, %i, %j, %k, %l);
		}
	}
};
ActivatePackage(TG_NewRewards);