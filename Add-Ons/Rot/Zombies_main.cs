//Zombie Player datablock
$zombierespawn = 1; //inactive
$zombiekilllimit = 500; //inactive
$zombie::timelimit = 5; //in minutes
$zombie::timerenabled = 1; //duh
$zombie::gamemode = bot; //don't change this, only bot is supported
$zombie::Enablehumannames = 1; //0 hides human names for added fun
$zombie::brickdestruction = 1; //zombie brick destruction
$zombie::friendlyfire = 0; //another duh
$zombie::playerszombify = 1; //if players zombify on death
$zombie::killallbricks = 0; //enable if you want zombies to destroy bricks in one hit
AddDamageType("zombiebite",   '<bitmap:add-ons/ci/zombiekill> %1',    '%2 <bitmap:add-ons/ci/zombiekill> %1',1,1);
//$zombie::MaxZombies = 20; not in
//$zombie::gobyspawncount = 1; always enabled currently
exec("add-ons/rot/apfinder.cs");
exec("add-ons/rot/zombies_support_gamemodes.cs");
exec("add-ons/rot/zombies_support_zombiebrick.cs");
//no jets at all
datablock PlayerData(Zplayer : PlayerStandardArmor)
//datablock PlayerData(PlayerStandardArmor)

{
	minJetEnergy = 0;
	jetEnergyDrain = 0;
	canJet = 0;
	maxitems = 10;
	maxtools = 5;
	maxweapons = 5;
	cameramaxdist = 2;
	cameramindist = 0;
	//firstpersononly = 1;

	uiName = "Zombie Game Mode";
	showEnergyBar = false;
};
function Zplayer::onAdd(%this,%obj)
{
	
	%obj.client.haspropaneactive = 0;
parent::onadd(%this,%obj);
%obj.iszombie = 0;
%obj.client.iszombie = 0;
if($zombie::Enablehumannames == 0)
	{
		%obj.schedule(30,setshapename,"");
	}
if($humanzombiegame == 1){
	if(%obj.client.minigame.member0 $= %obj.client){
		$zombiebegin = schedule(10000,0,randomplayerzombie,%obj.client);

	}
}
}
function Zplayer::ondisabled(%this,%obj)
{
	parent::ondisabled(%this,%obj);
	if ($zombie::playerszombify == 1)
	{
	
	%obj.client.iszombie = 1;
	%obj.iszombie = 1;
	%obj.client.minigame.playerdatablock = "PlayerFastZombieArmor";
	//%obj.client.minigame.startequip0 = 0;
	//%obj.client.minigame.startequip1 = 0;
	//%obj.client.minigame.startequip2 = 0;
	//%obj.client.minigame.startequip3 = 0;
	//%obj.client.minigame.startequip4 = 0;
	//%obj.setinventory(
	%obj.client.iszombie = 1;
	checkforhumans(%obj.client);
	//%obj.client.spawnplayer();
	%pos = %obj.gettransform();
	//if($humanzombiegame == 1){
		%obj.client.isfirstzom = 1;
	if($zombie::active == 1)
	{
	 schedule(500,0,delayedzombiefie,%obj.client,%pos,%obj);
	}
	}
	//}
}
function delayedzombiefie(%client,%pos,%obj){
	%client.spawnplayer();
	%client.player.settransform(%pos);
	%client.player.playaudio(0,zombiebiteHitSound);
	%obj.delete();

}
function resetzombies(%client,%minigame)
{
	////echo(%client SPC %minigame SPC "FIN");
	schedule(5000,0,delayvarzet,%minigame);
	if($zombie::gamemode $= bot){
	schedule(5100,0,deletezombies,%client);
	%client.zombdc = schedule(20000,0,startzombiegame,%client);
	%minigame.zombiegameisdone = 1;
	}
}
function getrandomplayerfromminigame(%minigame)
{
	%minicount = %minigame.nummembers;
	%random = getrandom(0,%minicount-1);
	%minimember = %minigame.member[%random];
	return %minimember;
}
function randomplayerzombie(%client)
{
	%minigame = %client.minigame;
	////echo(%minigame);
	%minicount = %minigame.nummembers;
	%randomplayer =getrandom(0,%minicount-1);
	%ranpf = %minigame.member[%randomplayer];
	%ranpf.player.kill();
	%minigame.timer = 1;
	timelimit(1,0,%minigame);
	cancel($ztimelimit);
	messageall("zombiemes",%ranpf.name SPC "is the zombie!");
}
function servercmdcast(%client,%sand){
	if(%sand $= "sandwich"){
Messageclient(%client,'0',"You cast a sandwich, but badspot takes it before you eat it.");
	}

}
function timelimit(%limit,%current,%minigame)
{
	if(%minigame.zombiegameisdone == 1 || $zombie::active == 0){
		return;
	}
	if($zombie::timerenabled == 1){
		if(iseventpending($ztimelimit)){
			cancel($ztimelimit);
		}
		
	//echo(%minigame);
	//echo(gotzero);
	if(%limit != 0 && %limit > 1){
		////echo(gotone);
		%flimit = %limit*60000;
		$ztimelimit = schedule(60000,0,timelimit,0,%flimit-60000,%minigame);
		Centerprintall("<just:center>\c3" @ %limit SPC "minutes left.",5);
		return;
	}
	else if(%limit == 1){
		%flimit = %limit*60000;
		$ztimelimit = schedule(30000,0,timelimit,0,%flimit-30000,%minigame);
		Centerprintall("<just:center>\c3" @ %limit SPC "minutes left.",5);
		return;
	}
	//if(%current >= 60000){
		//displayminutes
	//	//echo(gottwo);
		//return;

	//}
	if(%current > 60000){
		////echo(gotone);
		//%flimit = %limit*60000;
		$ztimelimit = schedule(60000,0,timelimit,0,%current-60000,%minigame);
		Centerprintall("<just:center>\c3" @ %current/60000 SPC "minutes left.",5);
		return;
	}
	if(%current <= 60000 && %current > 10000){
		//displayseconds
		////echo(gotthree);
		Centerprintall("<just:center>\c3" @ %current/1000 SPC "seconds left!",5);
		$ztimelimit = schedule(10000,0,timelimit,0,%current-10000,%minigame);
		return;

	}
	if(%current <= 10000 && %current >0){
		//it's the final countdown!!!1 woo 80s
		////echo(gotthree);
		Centerprintall("<just:center>\c3" @ %current/1000 SPC "seconds left!",5);
		$ztimelimit = schedule(1000,0,timelimit,0,%current-1000,%minigame);
		return;
	}
	if(%current <= 0){
		//Centerprintall("<just:center>\c3Humans win. Resetting",5);
		messageall("","\c3Humans win! Resetting");
		resetzombies(%minigame.member0,%minigame);
		return;
	}
	}
}
function checkforhumans(%client)
{
	%minigame = %client.minigame;
	////echo(%minigame);
	%minicount = %minigame.nummembers;
	////echo(%minicount);
	if(%minigame.zombiegameisdone == 1 || $zombie::active == 0){
		return;
	}
	for(%a = 0;%a<%minicount; %a++){
		////echo(%a);
		////echo(%minigame);
		%play = %minigame.member[%a];
		////echo(%minigame.member[%a]);
		//echo(%play);
		//echo(%play.name);
		if(%play.iszombie != 1){
		//messageall('',"There is a human left");
		//echo(humanleft);
		return;
		}
		//else if(%play.iszombie = 1){
		////messageall('',"Zombies win");
		////echo(oneother);
		//return;
		//}

	}
	schedule(30,0,messageALL,"","Zombies win! Game will reset in 5 seconds.");
	
	//%minigame.schedule(4800,playerdatablock = "Zplayer";
	cancel($zombiebegin);
	%minigame.timer = 0;
	//%minigame.resett = schedule(4000,0,servercmdreloadbricks,%minigame.member0);
	resetzombies(%client,%minigame);
}
function delayvarzet(%minigame){
	%minigame.playerdatablock = "Zplayer";
	%minigame.reset();
	%minigame.zombiegameisdone = 0;
}
datablock PlayerData(PlayerFastZombieArmor: PlayerstandardArmor)
{
	maxitems = 0;
	maxtools = 0;
	maxweapons = 0;
   runForce = 50 * 90;
   runEnergyDrain = 0;
   minRunEnergy = 0;
   maxForwardSpeed = 12;
   maxBackwardSpeed = 12;
   maxSideSpeed = 12;

   maxForwardCrouchSpeed = 3;
   maxBackwardCrouchSpeed = 3;
   maxSideCrouchSpeed = 3;

   jumpForce = 15 * 90; //8.3 * 90;
   jumpEnergyDrain = 0;
   minJumpEnergy = 0;
   jumpDelay = 0;
   maxDamage = 60;

	minJetEnergy = 0;
	jetEnergyDrain = 0;
	canJet = 0;

	uiName = "";
	showEnergyBar = false;

   runSurfaceAngle  = 100;
   jumpSurfaceAngle = 100;

   // Damage location details
  // boxNormalHeadPercentage       = 0.83;
  /// boxNormalTorsoPercentage      = 0.49;
   //boxHeadLeftPercentage         = 0.4;
   //boxHeadRightPercentage        = 1;
   //boxHeadBackPercentage         = 0;
   //boxHeadFrontPercentage        = 1;
};
function delaybah(%obj)
{
	for(%i=0;%i<5;%i++)
		{
			%toolDB = %obj.tool[%i];
			if(%toolDB != 0)
			{
				%obj.tool[%i] = 0;
				%obj.weaponCount--;
				messageClient(%obj.client,'MsgItemPickup','',%i,0);
				serverCmdUnUseTool(%obj.client);
			}
		}
		servercmdzombie(%obj.client);
}
function PlayerFastZombieArmor::onAdd(%this,%obj)
{
	schedule(20,0,delaybah,%obj);

   // Vehicle timeout
   %obj.mountVehicle = false;
   // Default dynamic armor stats
   %obj.setRechargeRate(%this.rechargeRate);
   %obj.setRepairRate(0);
   
   %obj.playthread(1,armreadyboth);
   //%obj.spawnbrick = $zombiespawn;
   %obj.player = %obj;

   if(%obj.getclassname() $= "Player"){
		//zombieme(%obj.client);
		%obj.iszombie = 0;
	    %obj.client.applybodycolors();
		%obj.client.applybodyparts();
		schedule(20,%obj.client,zombieme,%obj.client);
		%obj.mountimage(zombiecallImage,0);
		%obj.schedule(20,setshapename,"");
		%obj.iszombie = 0;
		%obj.client.iszombie = 1;
		if(%obj.client.isfirstzom == 1){
			%obj.client.isfirstzom = 0;
			return;
		}
		else{
			//echo(works);
			%tran = zombiepickspawn();
			//echo(%tran);
		%obj.schedule(20,settransform,%tran);
		}
		
   }
   //%obj.client = findclientbyname(rotondo);
}
function PlayerFastZombieArmor::ondisabled(%this,%obj)
{
	parent::ondisabled(%this,%obj);
	if(%obj.getclassname() $= "AIPlayer"){
		////echo(FOOOOOOOOOOOOOOOOOOOOOOOOOOOOP);
		////echo(%obj.lastattacker);
		//%obj.lastattacker.dump();
		%obj.setimagetrigger(2,0);
		%obj.setimagetrigger(1,0);
		%obj.setimagetrigger(0,0);
		%obj.lastattacker.client.incscore(1);
		//commandtoclient(%obj.lastattacker.client,'bottomprint',"<just:center>You killed Zombie" SPC %obj.name,5);
		if($zombierespawn == 1)
		{
		//%rot = findclientbyname(Rotondo);
		//createzombie(findclientbyname(Rotondo),rotondo);
		//%obj.playthread(0,death1);
		}
	}
	if(%obj.getclassname() $= "Player"){
		//commandtoclient(%obj.lastattacker.client,'bottomprint',"<just:center>You killed Zombie" SPC %obj.name,5);
	}
	//%obj.schedu
}
////////////////////////////
////////BRACKET/////////////
////////////////////////////
datablock PlayerData(PlayerZombieArmor: PlayerStandardArmor)
{
	maxitems = 0;
	maxtools = 0;
	maxweapons = 0;
   runForce = 50 * 90;
   runEnergyDrain = 0;
   minRunEnergy = 0;
   maxForwardSpeed = 4;
   maxBackwardSpeed = 4;
   maxSideSpeed = 4;

   maxForwardCrouchSpeed = 3;
   maxBackwardCrouchSpeed = 3;
   maxSideCrouchSpeed = 3;
   uiName = "";

   jumpForce = 13 * 90; //8.3 * 90;
   jumpEnergyDrain = 0;
   minJumpEnergy = 0;
   jumpDelay = 0;
   maxDamage = 200;

	minJetEnergy = 0;
	jetEnergyDrain = 0;
	canJet = 0;

	showEnergyBar = false;

   runSurfaceAngle  = 100;
   jumpSurfaceAngle = 100;

   // Damage location details
  // boxNormalHeadPercentage       = 0.83;
  /// boxNormalTorsoPercentage      = 0.49;
   //boxHeadLeftPercentage         = 0.4;
   //boxHeadRightPercentage        = 1;
   //boxHeadBackPercentage         = 0;
   //boxHeadFrontPercentage        = 1;
};
function PlayerFastZombieArmor::onCollision(%this, %obj, %col, %fade, %pos, %norm)
{
	if(%col.getdatablock().getname() $= "skivehicle")
	{
		tumble(%col.getmountedobject(0),1);
		return;
	}
	if(%col.getclassname() $= "player" && %col.iszombie != 1)
	{
		////echo(shataaaaaaaaaaaaaaaaaaaaaaa);
		if(%obj.getstate() !$= "Dead"){
			if(%col.getstate() !$= "Dead"){
		//%obj.setmoveobject(%col);
		//%obj.addtofollowgroup();
		
		
		////echo(%obj.getstate());
		//if(%col !$= findclientbyname(Rot).player){
			////echo(minigamecandamage(%obj,%col));
		if(minigamecandamage(%obj,%col) == 1)
			{
			%zomname = 35;
			%zomname.client = 36;
			%zomname.player = 38;
			%zomname.client.netname = "Zombies";
			%zomname.player.netname = "Zombies";
			%zomname.netname = "Zombies";
			
			%zomname.client.name = "Zombies";
			%zomname.player.name = "Zombies";
			%zomname.name = "Zombies";
			////echo(%zomname);
			////echo(%zomname.client.netname);
		%col.damage(%zomname,0,20,$DamageType::zombiebite);
		//%obj.follow(%col);
		%obj.playthread(2,shiftup);

		%objpos = %obj.gettransform();
		%colpos = %col.gettransform();
		
		%col.applyimpulse("0 0 0","0 0 700");

		%randomsound = getrandom(0,10);
		if(%randomsound == 5){
		%obj.playaudio(0,zombiebiteDrawSound);
		}
		}
			}
		
		//}
		if(%col.getstate() $= "Dead")
			{
				//%obj.setaimobject(%col);
				//%obj.schedule(4000,randommove);

			}
		}
	}

	if(%col.getclassname() $= "wheeledvehicle")
	{
		
		if(%obj.getstate() !$= "Dead"){
		//%obj.setmoveobject(%col);
		//%obj.addtofollowgroup();
		if(minigamecandamage(%obj,%col) == 1){
		%obj.playthread(2,shiftup);
		////echo(%obj.getstate());
		//if(%col !$= findclientbyname(Rot).player){

		%col.damage(0,0,10);
		//%obj.follow(%col);
		//}
		//if(%col.getstate() $= "Dead")
			//{
				//%obj.setaimobject(%col);

			//}
		}
	}
	}


}
function PlayerZombieArmor::onCollision(%this, %obj, %col, %fade, %pos, %norm)
{
////echo(%obj);
////echo(%col);
//if(%col.getClassName() $= "FxDtsBrick")
if(%col.getdatablock().getname() $= "skivehicle")
	{
		tumble(%col.getmountedobject(0),1);
		return;
	}
     // {
		//%col.killbrick();
		////echo(worksssssssssssssssssssssssssssssssssss);
	  //}
if(%col.getclassname() $= "player" && %col.iszombie != 1)
	{
		////echo(shataaaaaaaaaaaaaaaaaaaaaaa);
		if(%obj.getstate() !$= "Dead"){
			if(%col.getstate() !$= "Dead"){
		//%obj.setmoveobject(%col);
		//%obj.addtofollowgroup();
		
		
		////echo(%obj.getstate());
		//if(%col !$= findclientbyname(Rot).player){
			////echo(minigamecandamage(%obj,%col));
		if(minigamecandamage(%obj,%col) == 1)
			{
			%zomname = 35;
			%zomname.client = 36;
			%zomname.player = 38;
			%zomname.client.netname = "Zombies";
			%zomname.player.netname = "Zombies";
			%zomname.netname = "Zombies";
			
			%zomname.client.name = "Zombies";
			%zomname.player.name = "Zombies";
			%zomname.name = "Zombies";
			////echo(%zomname);
			////echo(%zomname.client.netname);
		%col.damage(%zomname,0,10,$DamageType::zombiebite);
		%obj.follow(%col);
		%obj.playthread(2,shiftup);

		%objpos = %obj.gettransform();
		%colpos = %col.gettransform();
		
		%col.applyimpulse("0 0 0","0 0 700");

		%randomsound = getrandom(0,10);
		if(%randomsound == 5){
		%obj.playaudio(0,zombiebiteDrawSound);
		}
		}
			}
		
		//}
		if(%col.getstate() $= "Dead")
			{
				//%ranplay = getrandomplayerfromminigame(%client.minigame);
				//%obj.follow(%ranplay);
				//%obj.setaimobject(%col);
				//%obj.schedule(4000,randommove);

			}
		}
	}

	if(%col.getclassname() $= "wheeledvehicle")
	{
		if(%col.getdatablock().getname() $= "PropaneTankCol"){
		//%col.damage(%obj,%pos,500);
		return;
		}
		if(%obj.getstate() !$= "Dead"){
		//%obj.setmoveobject(%col);
		//%obj.addtofollowgroup();
		if(minigamecandamage(%obj,%col) == 1){
		%obj.playthread(2,shiftup);
		////echo(%obj.getstate());
		//if(%col !$= findclientbyname(Rot).player){

		%col.damage(0,0,10);
		//%obj.follow(%col);
		//}
		//if(%col.getstate() $= "Dead")
			//{
				//%obj.setaimobject(%col);

			//}
		}
	}
	}

}
function globalplayertick()
{
	%ccount = clientgroup.getcount();
	for(%a = 0; %a < %ccount; %a++){
		if(clientgroup.getobject(%a).player.iszombie != 1){
	ZomSearch(clientgroup.getobject(%a));
	}
	}
	if($zombie::gamemode $= bot){
	$GlobalPlayerTick = schedule(4000,0,globalplayertick);
	}
}
function globalbottick(%client)
{
	%ccount = zombiesgroup.getcount();
	for(%a = 0; %a < %ccount; %a++){
		%obj = zombiesgroup.getobject(%a);
	%ranplay = getrandomplayerfromminigame(%client.minigame);
	%obj.follow(%ranplay.player);
	}
	if($zombie::gamemode $= bot){
	$GlobalBotTick = schedule(20000,0,globalbottick,%client);
	}
}
function GetRandomPlayer()
{
	%clientcount = clientgroup.getcount();
	%rand = getrandom(0,%clientcount-1);
	//echo("BLAHLALHLAHLALHALHLAHA" @ %rand);
	return clientgroup.getobject(%rand);

}
function PlayerZombieArmor::onAdd(%this,%obj)
{
   // Vehicle timeout
   %obj.mountVehicle = false;
	//echo(%obj);
	//echo(%obj.client);
	//echo(%obj.getclassname());
	//echo(FIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIi);
   // Default dynamic armor stats
   %obj.setRechargeRate(%this.rechargeRate);
   %obj.setRepairRate(0);
   %obj.playthread(1,armreadyboth);
   //%obj.spawnbrick = $zombiespawn;
   %obj.player = %obj;
   if(%obj.getclassname() $= "AIPlayer"){
	   if($zombiedestruct != 1){
		   %obj.mountimage(zombiebrickimage,0);
		   %obj.setimagetrigger(0,1);
	   }
	   %obj.mountimage(zombiejumpcheckimage,1);
	   %obj.setimagetrigger(1,1);
	   %obj.mountimage(donothingimage,2);
   }
   else if(%obj.getclassname() $= "Player"){
		//zombieme(%obj.client);
		schedule(100,%obj.client,zombieme,%obj.client);
		%obj.schedule(100,setshapename,"");
		%obj.iszombie = 1;
   }
   //%obj.client = findclientbyname(rotondo);
}
function PlayerZombieArmor::ondisabled(%this,%obj)
{
	parent::ondisabled(%this,%obj);
	if(%obj.getclassname() $= "AIPlayer"){
		////echo(FOOOOOOOOOOOOOOOOOOOOOOOOOOOOP);
		////echo(%obj.lastattacker);
		//%obj.lastattacker.dump();
		%obj.setimagetrigger(2,0);
		%obj.setimagetrigger(1,0);
		%obj.setimagetrigger(0,0);
		%obj.lastattacker.client.incscore(1);
		commandtoclient(%obj.lastattacker.client,'bottomprint',"<just:center>You killed Zombie" SPC %obj.name,5);
		if($zombierespawn == 1)
		{
		//%rot = findclientbyname(Rotondo);
		 %ranplay = getrandomplayerfromminigame(%obj.lastattacker.client.minigame);
		 
		 ////echo(%ranplay);
		// %ranplay.dump();
		%random = getrandom(0,10000);
		schedule(%random,0,createzombie,%obj.lastattacker.client.minigame.member0,%ranplay.name);	
		//createzombie(%obj.lastattacker.client.minigame.member0,%ranplay.name);
		%obj.playthread(0,death1);
		}
	}
	if(%obj.getclassname() $= "Player"){
		commandtoclient(%obj.lastattacker.client,'bottomprint',"<just:center>You killed Zombie" SPC %obj.name,5);
	}
	//%obj.schedule(1000,delete);
}
function ServerCMDstartzombiegame(%client)
{
	
	if(zombiespawngroup.getcount() == 0){
		messageclient(%client,'',"There are no zombie spawns");
		return;
	}
	if(%client.startedzomalready != 1){
	if(%client.minigame != 0){
		if(%client.issuperadmin){
			if(%client.minigame.member0 = %client){
				messageclient(%client,'',"\c4Zombie game started");
				globalplayertick();
				globalbottick(%client);
				%client.startedzomalready = 1;
				$zombie::active = 1;
				$zombie::gamemode = bot;
				//$ZombieClient = new GameConnection();
				//%randplay = GetRandomPlayer();
				%minigameplayer = %client.minigame;
				////echo(%minigameplayer);
					%count = zombiespawngroup.getcount();
					timelimit($zombie::timelimit,0,%client.minigame);
				 %ranplay = getrandomplayerfromminigame(%client.minigame);
					for(%a = 0; %a < %count; %a++){
						%random = getrandom(0,10000);
					schedule(%random,0,createzombie,%client,%ranplay.name,%ranplay);	
				}	
			}
		}
	}
	}
	else
		{
		messageclient(%client,'',"Make sure you're super admin and you're the one who created the minigame.");
		}
}
function startzombiegame(%client)
{
	if(iseventpending(%client.zombdc))
	{
		//messageclient(%client,'',"Doi");
		cancel(%client.zombdc);
	}
	if(zombiespawngroup.getcount() == 0){
		$zombie::gamemode = "";
		cancel($globalplayertick);
		cancel($globalbotTick);
		%client.minigame.membernum0.startedzomalready = 0;
		//messageclient(%client,'',"There are no zombie spawns");
		return;
	}
	if($zombie::gamemode $= bot){
	if(%client.minigame != 0){
				globalplayertick();
				globalbottick(%client);
				%client.startedzomalready = 1;
				//$ZombieClient = new GameConnection();
				//%randplay = GetRandomPlayer();
				%minigameplayer = %client.minigame;
				////echo(%minigameplayer);
					%count = zombiespawngroup.getcount();
					timelimit($zombie::timelimit,%current,%client.minigame);
				 %ranplay = getrandomplayerfromminigame(%client.minigame);
					for(%a = 0; %a < %count; %a++){
						%random = getrandom(0,10000);
					schedule(%random,0,createzombie,%client,%ranplay.name,%ranplay);	
			}
		}
	}
	else
		{
		messageclient(%client,'',"Error");
		}
}

function createbot(%client,%victem,%minigame)
{
	if(zombiespawngroup.getcount() == 0){
		$zombie::gamemode = "";
		cancel($globalplayertick);
		cancel($globalbottick);
		%client.minigame.membernum0.startedzomalready = 0;
		//messageclient(%client,'',"There are no zombie spawns");
		return;
	}
	if($zombie::gamemode $= bot){
	if(%client.issuperadmin){
	%newc = findclientbyname(%victem);
	%player = %client.player;
	if (isObject(%player))
	{
		%newBot = new AIPlayer(bot) {
			datablock = "PlayerZombieArmor";
		};
		simgroupfunc();
		//%newbot.client = %newbot;
		//%newbot.client.player = %newbot;
		%newbot.player = %newbot;
		//%newbot.client = 0;
		//%newbot.client = $ZombieClient;
		//$zombieclient.player = %newbot;
		%newbot.client.minigame = %minigame;
		%newbot.minigame = %minigame;
		randomidprefz(%newbot);
		GameConnection::applyBodyParts(%newbot);
		GameConnection::applyBodyColors(%newbot);
		//clearAllPlayerNodes(%newBot);
		//assignPPwithC(%newBot, %client);
		%newBot.setTransform(zombiepickspawn());
	}

	ZombieHim(%newbot);
	zombiesgroup.add(%newbot);
	%newbot.spawnbrick = zombiepickspawnid();
	%newcp = %newc.player;
	if(%victem $= random)
		{
			%randplay = GetRandomPlayer();
			%newbot.follow(%randplay);
		}
	else
		{
			%newbot.follow(%newcp);
		}
	}
  }
}
function CreateZombie(%client,%victem,%minigame)
{
	if(zombiespawngroup.getcount() == 0){
		$zombie::gamemode = "";
		cancel($globalplayertick);
		cancel($globalbottick);
		cancel($ztimelimit);
		//endzombiegame(%client);
		%client.minigame.membernum0.startedzomalready = 0;
		//messageclient(%client,'',"There are no zombie spawns");
		return;
	}
	if($zombie::gamemode $= bot){
	%newc = findclientbyname(%victem);
	%player = %client.player;
		%newBot = new AIPlayer(bot) {
			datablock = "PlayerZombieArmor";
		};
		simgroupfunc();
		//%newbot.client = %newbot;
		//%newbot.client.player = %newbot;
		%newbot.player = %newbot;
		//%newbot.client = 0;
		//%newbot.client = $ZombieClient;
		//$zombieclient.player = %newbot;
		%newbot.client.minigame = %minigame;
		%newbot.minigame = %minigame;
		////echo(%minigame);
		randomidprefz(%newbot);
		GameConnection::applyBodyParts(%newbot);
		GameConnection::applyBodyColors(%newbot);
		//clearAllPlayerNodes(%newBot);
		//assignPPwithC(%newBot, %client);
		%newBot.setTransform(zombiepickspawn());

	ZombieHim(%newbot);
	zombiesgroup.add(%newbot);
	%newbot.spawnbrick = zombiepickspawnid();
	%newcp = %newc.player;
	if(%victem $= random)
		{
			%randplay = GetRandomPlayer();
			%newbot.follow(%randplay);
		}
	else
		{
			%newbot.follow(%newcp);
		}
	}
}
function randomidprefz(%client)
{
	%rand = getRandom($pref::apfinder::idcount);
	//echo(%rand);
	idpreftoclient(%client,$pref::apfinder::ids[getRandom(%rand- 1)]);
	
}

function idpreftoclient(%client, %id)
{
	eval("%prefexist = $pref::apfinder::" @ %id @ ";");
	if (isObject(%client) && %prefexist)
	{
		eval("%prefroot = \"$pref::apfinder::" @ %id @ "::\";");
		eval("%client.LanName = " @ %prefroot @ "LanName;");
		eval("%client.llegColor = " @ %prefroot @ "llegColor;");
		eval("%client.secondPackColor = " @ %prefroot @ "secondPackColor;");
		eval("%client.lhand = " @ %prefroot @ "lhand;");
		eval("%client.hip = " @ %prefroot @ "hip;");
		eval("%client.faceName = " @ %prefroot @ "faceName;");
		eval("%client.gender = " @ %prefroot @ "gender;");
		eval("%client.rarmColor = " @ %prefroot @ "rarmColor;");
		eval("%client.hatColor = " @ %prefroot @ "hatColor;");
		eval("%client.hipColor = " @ %prefroot @ "hipColor;");
		eval("%client.chest = " @ %prefroot @ "chest;");
		eval("%client.rarm = " @ %prefroot @ "rarm;");
		eval("%client.packColor = " @ %prefroot @ "packColor;");
		eval("%client.pack = " @ %prefroot @ "pack;");
		eval("%client.clanPrefix = " @ %prefroot @ "clanPrefix;");
		eval("%client.clanSuffix = " @ %prefroot @ "clanSuffix;");
		eval("%client.decalName = " @ %prefroot @ "decalName;");
		eval("%client.larmColor = " @ %prefroot @ "larmColor;");
		eval("%client.secondPack = " @ %prefroot @ "secondPack;");
		eval("%client.larm = " @ %prefroot @ "larm;");
		eval("%client.chestColor = " @ %prefroot @ "chestColor;");
		eval("%client.name = " @ %prefroot @ "name;");
		eval("%client.accentColor = " @ %prefroot @ "accentColor;");
		eval("%client.rhandColor = " @ %prefroot @ "rhandColor;");
		eval("%client.rleg = " @ %prefroot @ "rleg;");
		eval("%client.rlegColor = " @ %prefroot @ "rlegColor;");
		eval("%client.accent = " @ %prefroot @ "accent;");
		eval("%client.headColor = " @ %prefroot @ "headColor;");
		eval("%client.rhand = " @ %prefroot @ "rhand;");
		eval("%client.lleg = " @ %prefroot @ "lleg;");
		eval("%client.lhandColor = " @ %prefroot @ "lhandColor;");
		eval("%client.hat = " @ %prefroot @ "hat;");
	}
}


function AiPlayer::follow(%obj,%col)
{
  %obj.setmoveobject(%col);
  %obj.setaimobject(%col);
  %obj.addtofollowgroup();
  %obj.hum = %col;


}
 function GlobalFollowTick()
 {
for(%a = 1; %a < zombieFollowingGroup.getcount(); %a++){
%zombie = zombiefollowinggroup.getobject(%a);
startdischeck(%zombie,%zombie.hum);

}
$globaltick = schedule(5000,0,GlobalFollowTick);
//echo(thisisingame);
 }

 function ZomSearch(%client)
{
	if(%client.player){
   %player = %client.player;
   %searchMasks = $TypeMasks::PlayerObjectType;
   %radius = 10;
   %pos = %player.getPosition();
   InitContainerRadiusSearch(%pos, %radius, %searchMasks);
  
   while ((%corpse = containerSearchNext()) != 0 ) {
   //%dist = containerSearchCurrRadiusDist();
   //%target = %corpse.getPosition();
   %id = %corpse.getId();
   //%crossbowAmount = %id.getInventory(CrossBow);
   //%crossbowAmmo = %id.getInventory(CrossBowAmmo);
   if(%id.getclassname() $= "AIPlayer")
	   {
	   ////echo(%id.name);
			if(minigamecandamage(%id,%player) == 1){
			%id.follow(%player);
			}
			//%id.setmoveobject(%player);

	   }
  }  
	}
}

 function servercmdcallzombies(%client)
{
	if(%client.player){
   %player = %client.player;
   %searchMasks = $TypeMasks::PlayerObjectType;
   %radius = 10;
   %pos = %player.getPosition();
   InitContainerRadiusSearch(%pos, %radius, %searchMasks);
  
   while ((%corpse = containerSearchNext()) != 0 ) {
   //%dist = containerSearchCurrRadiusDist();
   //%target = %corpse.getPosition();
   %id = %corpse.getId();
   //%crossbowAmount = %id.getInventory(CrossBow);
   //%crossbowAmmo = %id.getInventory(CrossBowAmmo);
      
  // //echo( "\c1 -- Search Corpse -- " );
   ////echo( " Distans             = " SPC %dist);
   ////echo( " Player Pos          = " SPC %pos);
   ////echo( " Corpse Pos          = " SPC %target);
   ////echo( " Target Id           = " SPC %id);
   ////echo( " Amount CrossBow     = " SPC %id.getshapename());
   ////echo( " Amount CrossBowAmmo = " SPC %crossbowAmmo);  
   ////echo( "\c1 - End Search Corpse - " );
   if(%id.getclassname() $= "AIPlayer")
	   {
	  if( %client.player.iszombie == 1){
	   ////echo(%id.name);
			if(minigamecandamage(%id,%player) == 1){
			%id.follow(%player);
			}
			//%id.setmoveobject(%player);
	   }
	   }
  }  
	}
}

function AiPlayer::addtofollowgroup(%this)
{
 ZombieFollowingGroup.add(%this);


}
function startdischeck(%zom,%hum)
{
%dist = vectordist(%zom.gettransform(),%hum.gettransform());
 if(%dist >= 25)
	{
		%zom.randommove();
		zombiefollowinggroup.remove(%zom);
	}
}

function AIPLAYER::randommove(%ai)
{

   if(%ai.getState() $= "Dead" || !isobject(%ai))
   {
         return;
   }
   %ai.setmoveobject("");
   %truetime = getrandom(0,2);
   if(%truetime == 0)
   {
   %x = getWord(%ai.gettransform(),0);
   %y = getWord(%ai.gettransform(),1);
   %z = getWord(%ai.gettransform(),2);
   
   %xadd = getrandom(-20,20);
   %yadd = getrandom(-20,20);
   
   %x+=%xadd;
   %y+=%yadd;
   
   %ai.setmovedestination(%x SPC %y SPC %z);
   %ai.clearaim();
   if($debug == 1)
   {
   //echo(RandomMoveing);
   }
   }
}
///wicked helpful client hack %zombie.player = %zombie;
//gameconnection::applybodyparts(%zombie);
//gameconnection::applybodycolors(%zombie);


//Zombie Appearence

function zombieme(%client)
{
	    %client.player.iszombie = 0;
		//darkencolor(%client);
		%client.player.setNodecolor($rhand[0], ".7 0.9 0.4 1");
		%client.player.setNodecolor($lhand[0], "0.7 0.9 0.4 1");
		%client.player.setNodecolor("headskin", "0.7 0.9 0.4 1");
		%randomc = getrandom(0,1);
	if(%randomc == 1)
		{
		%client.player.setNodecolor($Rarm[0], "0.7 0.9 0.4 1");
		%client.player.setNodecolor($Rarm[1], "0.7 0.9 0.4 1");
		%client.player.setNodecolor($Lleg[0], "0.7 0.9 0.4 1");

		}
	else
		{
		%client.player.setNodecolor($Larm[0], "0.7 0.9 0.4 1");
		%client.player.setNodecolor($Larm[1], "0.7 0.9 0.4 1");
		%client.player.setNodecolor($Rleg[0], "0.7 0.9 0.4 1");
		}
		%client.player.playthread(1,armreadyboth);
		%client.player.iszombie = 1;
}

function darkencolor(%client)
{
	    %player = %client.player;
		%player.setNodeColor($accent[%client.accent], darkthree(%client.accentColor));
		%player.setNodeColor($chest[%client.chest], darkthree(%client.chestColor));
		%player.setNodeColor($hat[%client.hat], darkthree(%client.hatColor));
		%player.setNodeColor($hip[%client.hip], darkthree(%client.hipColor));
		%player.setNodeColor($larm[%client.larm], darkthree(%client.larmColor));
		%player.setNodeColor($lhand[%client.lhand], darkthree(%client.lhandColor));
		%player.setNodeColor($lleg[%client.lleg], darkthree(%client.llegColor));
		%player.setNodeColor($pack[%client.pack], darkthree(%client.packColor));
		%player.setNodeColor($rarm[%client.rarm], darkthree(%client.rarmColor));
		%player.setNodeColor($rhand[%client.rhand], darkthree(%client.rhandColor));
		%player.setNodeColor($rleg[%client.rleg], darkthree(%client.rlegColor));
		%player.setNodeColor($secondpack[%client.secondpack], darkthree(%client.secondpackColor));

}

function darkthree(%three)
{
	///%imp = getword(%three,2);
	//%first = getword(%three,0);
	//%second = getword(%three,1);
	//%one = getword(%three,3);

	//%first -= 0.2;
	//%blah = %first SPC %second SPC %imp SPC %one;
	////echo(%blah);
	//%blah = vectoradd(%three,"-0.01 -0.01 -0.01 0");
	return %three;
}

function ZombieHim(%client)
{
	    %client.player = %client;
	
		//darkencolor(%client);
		%client.player.setNodecolor($rhand[0], ".7 0.9 0.4 1");
		%client.player.setNodecolor($lhand[0], "0.7 0.9 0.4 1");
		%client.player.setNodecolor("headskin", "0.7 0.9 0.4 1");
		%randomc = getrandom(0,1);
	if(%randomc == 1)
		{
		%client.player.setNodecolor($Rarm[0], "0.7 0.9 0.4 1");
		%client.player.setNodecolor($Rarm[1], "0.7 0.9 0.4 1");
		%client.player.setNodecolor($Lleg[0], "0.7 0.9 0.4 1");

		}
	else
		{
		%client.player.setNodecolor($Larm[0], "0.7 0.9 0.4 1");
		%client.player.setNodecolor($Larm[1], "0.7 0.9 0.4 1");
		%client.player.setNodecolor($Rleg[0], "0.7 0.9 0.4 1");
		}
		%client.player.playthread(1,armreadyboth);
}



function clearPlayerNodes(%player)
{
	if (isObject(%player))
	{
		for (%i = 0; $accent[%i] !$= ""; %i++) %player.hideNode($accent[%i]);
		for (%i = 0; $chest[%i] !$= ""; %i++) %player.hideNode($chest[%i]);
		for (%i = 0; $hat[%i] !$= ""; %i++) %player.hideNode($hat[%i]);
		for (%i = 0; $hip[%i] !$= ""; %i++) %player.hideNode($hip[%i]);
		for (%i = 0; $LArm[%i] !$= ""; %i++) %player.hideNode($LArm[%i]);
		for (%i = 0; $LHand[%i] !$= ""; %i++) %player.hideNode($LHand[%i]);
		for (%i = 0; $LLeg[%i] !$= ""; %i++) %player.hideNode($LLeg[%i]);
		for (%i = 0; $pack[%i] !$= ""; %i++) %player.hideNode($pack[%i]);
		for (%i = 0; $RArm[%i] !$= ""; %i++) %player.hideNode($RArm[%i]);
		for (%i = 0; $RHand[%i] !$= ""; %i++) %player.hideNode($RHand[%i]);
		for (%i = 0; $RLeg[%i] !$= ""; %i++) %player.hideNode($RLeg[%i]);
		for (%i = 0; $secondPack[%i] !$= ""; %i++) %player.hideNode($secondPack[%i]);
	}
}

function clearAllPlayerNodes(%player)
{
	if (isObject(%player))
	{
		clearPlayerNodes(%player);
		%player.hideNode("headSkin");
		%player.hideNode("LSki");
		%player.hideNode("RSki");
		%player.hideNode("skirtTrimLeft");
		%player.hideNode("skirtTrimRight");
	}
}

function assignPPwithC(%player, %client)
{
	if (isObject(%player) && isObject(%client))
	{
	////echo("dd");
		%player.unHideNode($accent[%client.accent]);
		%player.setNodeColor($accent[%client.accent], %client.accentColor);
		%player.unHideNode($chest[%client.chest]);
		%player.setNodeColor($chest[%client.chest], %client.chestColor);
		%player.unHideNode($hat[%client.hat]);
		%player.setNodeColor($hat[%client.hat], %client.hatColor);
		%player.unHideNode($hip[%client.hip]);
		////echo($hip[%client.hip]);
		%player.setNodeColor($hip[%client.hip], %client.hipColor);
		%player.unHideNode($larm[%client.larm]);
		%player.setNodeColor($larm[%client.larm], %client.larmColor);
		%player.unHideNode($lhand[%client.lhand]);
		%player.setNodeColor($lhand[%client.lhand], %client.lhandColor);
		%player.unHideNode($lleg[%client.lleg]);
		%player.setNodeColor($lleg[%client.lleg], %client.llegColor);
		%player.unHideNode($pack[%client.pack]);
		%player.setNodeColor($pack[%client.pack], %client.packColor);
		%player.unHideNode($rarm[%client.rarm]);
		%player.setNodeColor($rarm[%client.rarm], %client.rarmColor);
		%player.unHideNode($rhand[%client.rhand]);
		%player.setNodeColor($rhand[%client.rhand], %client.rhandColor);
		%player.unHideNode($rleg[%client.rleg]);
		%player.setNodeColor($rleg[%client.rleg], %client.rlegColor);
		%player.unHideNode($secondpack[%client.secondpack]);
		%player.setNodeColor($secondpack[%client.secondpack], %client.secondpackColor);
		%player.unHideNode("headSkin");
		%player.setNodeColor("headSkin", %client.headColor);
		%player.setFaceName(%client.faceName);
		return %player;
	}
}
//Misc Zombie functions 

function deletezombies(%client)
{
	//if (!isObject(%client.player) || (!%client.isAdmin && !%client.isSuperAdmin)) return;
	$zombierespawn = 0;
	while (zombiesgroup.getCount())
	{
		//%b = zombiesgroup.getcount();
		//for(%a = 0; %a < %b; %a++){
		%bot = zombiesgroup.getObject(0);
		%bot.delete();
		
		////echo(%bot);
		//}
	}
	$zombierespawn = 1;
	%client.startedzomalready = 0;
	cancel($globalplayertick);
	cancel($globalbottick);
	cancel($ztimelimit);
}
function servercmdendzombiegame(%client)
{
	%client.startedzomalready = 0;
	if(%client.isadmin && %client.issuperadmin && $zombie::active == 1)
	{
	
	if($zombie::active == 1){
	$zombie::gamemode = "";
	$zombie::active = 0;
	
		messageclient(%client,'',"Zombie game ended.");
		deletezombies(%client);

	}
	}
}

//Zombie damage shat

function ProjectileData::damage(%this,%obj,%col,%fade,%pos,%normal)
{
	if(%this.directDamage <= 0)
      return;
   	%dirdamage = %this.directDamage;
	%col.lastattacker = %obj.sourceobject;
	////echo(%col.getdatablock());
	////echo("LINE----------------------------------------------------");
	////echo(%obj.getclassname());
	if(%col.getdatablock().getname() $= "PropaneTankCol" && %obj.getclassname() $= "AIPlayer"){
		//%col.damage(%obj,%pos,500);
		return;
	}
	if(%col.getdatablock().getname() $= "PropaneTankCol"){
		%col.damage(%obj,%pos,500);
		return;
	}
	if(%col.getclassname() $= "AIPlayer" || %col.getclassname() $= "Player"){
		if(%col.getdatablock().getname() $= "Zplayer" && $zombie::friendlyfire == 0)
		{
			return;
		}
		if(%col.getdatablock().getname() $= "sleepplayer" && $zombie::friendlyfire == 0)
		{
			return;
		}
	%damLoc = %col.getDamageLocation(%pos);
	
	////echo(%damloc);
	////echo("this:" @%this SPC %obj SPC %col SPC "Fade:" @ %fade SPC "Pos:" @ %pos SPC "Normal:" @ %normal);
	////echo(%this.gettransform());
	%ppos = getword(%pos,0);
	%blah = getword(%col.getWorldBoxCenter(),0);
	
   if(%this.directDamage <= 0)
      return;
   	%dirdamage = %this.directDamage;
	%ppos = getword(%pos,2);
	%blah = getword(%col.getWorldBoxCenter(),2)-3.5;
	if(%ppos > %blah){
		//messageclient(%obj.client,'headshot',"\c2BOOMHEADSHOT");
		%dirdamage = %dirdamage*2;
		%col.limbfun(head);
	}
	if(%ppos < %blah-1){
		//messageclient(%obj.client,'headshot',"\c3BOOMLEGSHOTWTF");
		%dirdamage = %dirdamage/2;
		if(strstr(%damLoc, "left") != -1){
			//messageclient(%obj.client,'lefleg','\c1leftlegshot');
			
		}
		else if(strstr(%damLoc, "right") != -1){
			//messageclient(%obj.client,'lefleg','\c1rightlegshot');
			
		}
	}
	%blah2 = %blah-1;
	if(%ppos > %blah2 && %ppos < %blah){
		//messageclient(%obj.client,'headshot',"\c4BOOCHESTSHOT");
		%ppos2 = getword(%pos,0);
		%blah2 = getword(%col.getWorldBoxCenter(),0);
		if(strstr(%damLoc, "left") != -1){
			//messageclient(%obj.client,'lefleg','\c1leftarmshot');
			%ranx = getrandom(0,1);
			if(%ranx == 0){
				%col.limbfun(lhand);
			}
			if(%ranx == 1){
				%col.limbfun(larm);
			}
		}
		else if(strstr(%damLoc, "right") != -1){
			//messageclient(%obj.client,'lefleg','\c1rightarmshot');
			%ranx = getrandom(0,1);
			if(%ranx == 0){
				%col.limbfun(rhand);
			}
			if(%ranx == 1){
				%col.limbfun(rarm);
			}
		}
		%randarm = getrandom(1,10);
		//switch(%randarm)
		//{
		//	case 0:
		//			//echo(bah);
		//	case 1: 
		//			%col.limbfun(lhand);
		//	case 2: 
		//			%col.limbfun(rhand);
		//	case 3:
		//			%col.limbfun(larm);
		//	case 4:
		//			%col.limbfun(rarm);
		//	default:
		//			//echo(error);
		//}
	}
	}

//direct damage doubles for crouching players
   %damageType = $DamageType::Direct;
   if(%this.DirectDamageType)
      %damageType = %this.DirectDamageType;

   if(%col.getType() & $TypeMasks::PlayerObjectType)
   {
      %col.damage(%obj, %pos, %dirdamage, %damageType);
   }
   else
   {
      %col.damage(%obj, %pos, %dirdamage, %damageType);
   }
   if(%col.getclassname() $= "AiPlayer")
	{
	   if(%this.explosion $= "Rocketexplosion")
		{
			//rocketlimbs(%col);

		}
		%col.follow(%obj.client.player);
		//loserandomlimbs(%col);
		//%damLoc = firstWord(%col.getDamageLocation(%pos));
	}
}
function AIPLAYER::LimbFun(%bot,%op)
{
	switch$(%op)
	{
		case head:
				%bot.hat = 0;
				%bot.accent = 0;
				Gameconnection::applybodyparts(%bot);
				%bot.hideNode("headSkin");
		case lhand:
				%bot.lhand = -1;
				gameconnection::applybodyparts(%bot);
		case rhand:
				%bot.rhand = -1;
				gameconnection::applybodyparts(%bot);
		case larm:
				%bot.lhand = -1;
				%bot.larm = -1;
				gameconnection::applybodyparts(%bot);
		case rarm:
				%bot.rhand = -1;
				%bot.rarm = -1;
				gameconnection::applybodyparts(%bot);
		case rleg:
				%bot.rleg = -1;
				gameconnection::applybodyparts(%bot);
		case lleg:
				%bot.lleg = -1;
				gameconnection::applybodyparts(%bot);
		default:
				//echo(Error);
	}

}
function PLAYER::LimbFun(%bot,%op)
{
	if($limbfunhuman == 1){
	switch$(%op)
	{
		case head:
				%bot.hat = 0;
				%bot.accent = 0;
				Gameconnection::applybodyparts(%bot);
				%bot.hideNode("headSkin");
		case lhand:
				%bot.lhand = -1;
				gameconnection::applybodyparts(%bot);
		case rhand:
				%bot.rhand = -1;
				gameconnection::applybodyparts(%bot);
		case larm:
				%bot.lhand = -1;
				%bot.larm = -1;
				gameconnection::applybodyparts(%bot);
		case rarm:
				%bot.rhand = -1;
				%bot.rarm = -1;
				gameconnection::applybodyparts(%bot);
		case rleg:
				%bot.rleg = -1;
				gameconnection::applybodyparts(%bot);
		case lleg:
				%bot.lleg = -1;
				gameconnection::applybodyparts(%bot);
		default:
				//echo(Error);
	}
	}

}
function ProjectileData::radiusDamage(%this, %obj, %col, %distanceFactor, %pos, %damageAmt)
{
   //validate distance factor
   if(%distanceFactor <= 0)
      return;
   else if(%distanceFactor > 1)
      %distanceFactor = 1;
	//echo(%obj.dump());
	%col.lastattacker = %obj.client.player;
		if(%col.getdatablock().getname() $= "Zplayer" && $zombie::friendlyfire == 0)
		{
			return;
		}
		if(%col.getdatablock().getname() $= "sleepplayer" && $zombie::friendlyfire == 0)
		{
			return;
		}
   
   %damageAmt *= %distanceFactor;
	if(%col.getclassname() $= "AiPlayer")
	{
	   if(%this.explosion $= "Rocketexplosion2")
		{
		   %ranlimb = getrandom (0,1);
			if(%ranlimb == 0 && %col.alreadylostlimbs == 0){
			rocketlimbs(%col);
			%col.alreadylostlimbs = 1;
			}
			if(%ranlimb == 1 && %col.alreadylostlimbs == 0){
		loselegs(%col);
		%col.alreadylostlimbs = 1;
		   }
		}
		%col.follow(%obj.client.player);
		
		if(%col.alreadylostlimbs == 0){
		loserandomlimbs(%col);
		}
	}
   if(%damageAmt)
   {
      //use default damage type if no damage type is given
      %damageType = $DamageType::Radius;
      if(%this.RadiusDamageType)
      %damageType = %this.RadiusDamageType;

      %col.damage(%obj, %pos, %damageAmt, %damageType);

      //burn the player?
      if(%this.explosion.playerBurnTime > 0)
      {
         if(%col.getType() & $TypeMasks::PlayerObjectType || %col.gettype() & $TypeMasks::CorpseObjectType)
         {
            %col.burn(%this.explosion.playerBurnTime * %distanceFactor);
         } 
      }
   }
}
function loserandomlimbs(%col)
{
%rana = getrandom(0,20);
		if(%rana == 10)
		{
		%col.hideNode("headSkin");
		%col.hideNode($accent[0]);
		%col.hideNode($accent[1]);
		%col.hideNode($accent[2]);
		%col.hideNode($hat[0]);
		%col.hideNode($hat[1]);
		%col.hideNode($hat[2]);
		%col.hideNode($hat[3]);
		%col.hideNode($hat[4]);
		%col.hideNode($hat[5]);
		%col.hideNode($hat[6]);
		}
		if(%rana == 5)
		{
		%col.hidenode($lhand[0]);
		%col.hidenode($lhand[1]);
		if(getrandom(0,1) == 1)
			{
			////echo(hap);
			%col.hidenode($larm[0]);
			%col.hidenode($larm[1]);
			}
		}
		if(%rana == 6)
		{
		%col.hidenode($rhand[0]);
		%col.hidenode($rhand[1]);
		if(getrandom(0,1) == 1)
			{
			////echo(hap);
			%col.hidenode($rarm[0]);
			%col.hidenode($rarm[1]);
			}
		}


}

function rocketlimbs(%col)
{
%col.hideNode("headSkin");
		%col.hideNode($accent[0]);
		%col.hideNode($accent[1]);
		%col.hideNode($accent[2]);
		%col.hideNode($hat[0]);
		%col.hideNode($hat[1]);
		%col.hideNode($hat[2]);
		%col.hideNode($hat[3]);
		%col.hideNode($hat[4]);
		%col.hideNode($hat[5]);
		%col.hideNode($hat[6]);
		%col.hidenode($lhand[0]);
		%col.hidenode($lhand[1]);
		%col.hidenode($larm[0]);
			%col.hidenode($larm[1]);
			%col.hidenode($rhand[0]);
		%col.hidenode($rhand[1]);
		%col.hidenode($rarm[0]);
			%col.hidenode($rarm[1]);
		%col.hidenode($chest[0]);
		%col.hidenode($chest[1]);
		for (%i = 0; $pack[%i] !$= ""; %i++) %col.hideNode($pack[%i]);
		for (%i = 0; $secondPack[%i] !$= ""; %i++) %col.hideNode($secondPack[%i]);

}
function loselegs(%col)
{
	%col.hidenode($lleg[0]);
	%col.hidenode($lleg[1]);
	%col.hidenode($rleg[0]);
	%col.hidenode($rleg[1]);

	%col.playthread(3,crouchrun);
}

//Zombie Bricks

datablock fxDTSBrickData (brick3x3zombie){
	brickFile = "base/data/bricks/special/zombiespawn.blb";
	category = "Special";
	subCategory = "Zombie";
	uiName = "Zombie Spawn";
	iconName = "";
};


function brick3x3zombie::onadd(%this,%brick)
{
	//echo(simfar);
	simgroupfunc();
	//echo(schedfar);
	schedule(50,0,spawnadddelay,%brick);
	
}
function spawnadddelay(%brick)
{
	if(%brick.isplanted == 0){
		echo(returnfar);
		return;
	}
	//echo(%this SPC %brick);
	//echo(gotfarer);
	ZombieSpawnGroup.add(%brick);

}

function brick3x3zombie::onremove(%this,%brick)
{	
	zombiecleanupbrick(%brick);
//	simgroupfunc();
	if(%brick.isplanted){
	ZombieSpawnGroup.remove(%brickj);
	////echo(gotthisfar);
	////echo(%this SPC %brick);

   if(isObject(%obj.light))
      %obj.light.delete();
   if(isObject(%obj.emitter))
      %obj.emitter.delete();
   if(isObject(%obj.item))
      %obj.item.delete();
   if(isObject(%obj.AudioEmitter))
      %obj.AudioEmitter.delete();
   if(isObject(%obj.vehicle))
      %obj.vehicle.delete();
   if(isObject(%obj.vehicleSpawnMarker))
    %obj.vehicleSpawnMarker.delete();

//minigame points
if(isObject($CurrBrickKiller))
{
if(isObject($CurrBrickKiller.miniGame))
{
$CurrBrickKiller.incScore($CurrBrickKiller.miniGame.Points_BreakBrick);
}
}
//$Server::BrickCount--;
}
}

datablock fxDTSBrickData (brick1x16x100huge){
	brickFile = "base/data/bricks/special/zombieproofwall.blb";
	category = "Special";
	subCategory = "Zombie";
	uiName = "Zombie Proof Wall";
	iconName = "";
};

function brick1x16x100huge::onadd(%this,%brick)
{
	%brick.isunbreakable = 1;
}

function brick1x16x100huge::onremove(%this,%brick)
{	
	//zombiecleanupbrick(%brick);
	//simgroupfunc();
	if(%brick.isplanted){
	//ZombieSpawnGroup.remove(%brickj);
	////echo(gotthisfar);
	////echo(%this SPC %brick);

   if(isObject(%obj.light))
      %obj.light.delete();
   if(isObject(%obj.emitter))
      %obj.emitter.delete();
   if(isObject(%obj.item))
      %obj.item.delete();
   if(isObject(%obj.AudioEmitter))
      %obj.AudioEmitter.delete();
   if(isObject(%obj.vehicle))
      %obj.vehicle.delete();
   if(isObject(%obj.vehicleSpawnMarker))
    %obj.vehicleSpawnMarker.delete();

//minigame points
if(isObject($CurrBrickKiller))
{
if(isObject($CurrBrickKiller.miniGame))
{
$CurrBrickKiller.incScore($CurrBrickKiller.miniGame.Points_BreakBrick);
}
}
//$Server::BrickCount--;
}
}

function simgroupfunc()
{
	if(!isobject(zombiesgroup)){
		new SimSet(zombiesgroup);
		//missionGroup.add(zombiesgroup);
		missioncleanup.add(zombiesgroup);
	}
	if(!isobject(ZombieFollowingGroup)){
		new SimSet(ZombieFollowingGroup);
		//missioncleanup.add(zombieFollowingGroup);
	}
	if(!isobject(ZombieSpawnGroup)){
		new SimSet(ZombieSpawnGroup);
		//MissionGroup.add(ZombieSpawnGroup);
		missioncleanup.add(zombiespawngroup);
	}
}

function zombiepickspawn() 
{
   %groupName = "zombieSpawnGroup";
   %group = nameToID(%groupName);

	if (%group != -1) {
		%count = %group.getCount();
		if (%count != 0) {
			%index = getRandom(%count-1);
			%spawn = %group.getObject(%index);
			////echo("spawn object = ", %spawn);
			%trans = %spawn.getTransform();
			
			%transX = getWord(%trans, 0);
			%transY = getWord(%trans, 1);
			%transZ = getWord(%trans, 2);
			
			
			//%r = getRandom(10) / 10;
			%ang = getRandom($pi * 2 * 100) / 100;
			
			//x = r * cos( theta )
			//%transX += %r * mCos(%ang);
			//%transY += %r * mSin(%ang);
			 
			//%transXY = %transX @ " " @ %transY;
			%transz += 0.05;

			//%transZ = getTerrainHeight(%transXY);

			%spawnAngle = getRandom($pi * 2 * 100) / 100;
			%transz += 1;
			//%transf = vectoradd(%trans,"0 0 0.4");
			%returnTrans = %transX  @ " " @ %transY @ " " @ %transZ @ " 0 0 1 " @ %spawnAngle;
			
			return %returnTrans;
			//return %spawn.getTransform();
		}
		else
			error("No spawn points found in " @ %groupName);
	}
	else
	error("Missing spawn points group " @ %groupName);

	error("default spawn!");
   // Could be no spawn points, in which case we'll stick the
   // player at the center of the world.
   %spf = pickspawnpoint();
   return %spf;
}

function zombiepickspawnid() 
{
   %groupName = "zombieSpawnGroup";
   %group = nameToID(%groupName);

	if (%group != -1) {
		%count = %group.getCount();
		if (%count != 0) {
			%index = getRandom(%count-1);
			%spawn = %group.getObject(%index);
			////echo("spawn object = ", %spawn);
			%trans = %spawn.getTransform();
			
			%transX = getWord(%trans, 0);
			%transY = getWord(%trans, 1);
			%transZ = getWord(%trans, 2);
			
			%r = getRandom(10) / 10;
			%ang = getRandom($pi * 2 * 100) / 100;
			
			//x = r * cos( theta )
			//%transX += %r * mCos(%ang);
			//%transY += %r * mSin(%ang);
			 
			//%transXY = %transX @ " " @ %transY;

			//%transZ = getTerrainHeight(%transXY);
			%transz += 0.05;

			%spawnAngle = getRandom($pi * 2 * 100) / 100;

			%returnTrans = %transX  @ " " @ %transY @ " " @ %transZ @ " 0 0 1 " @ %spawnAngle;
			if(%spawn.isdead() == 1){
				zombiepickspawnid();
				return;
			}
			return %spawn;
			//return %spawn.getTransform();
		}
		else
			error("No spawn points found in " @ %groupName);
	}
	else
	error("Missing spawn points group " @ %groupName);

	error("default spawn!");
   // Could be no spawn points, in which case we'll stick the
   // player at the center of the world.
    %spf = pickspawnpoint();
   return %spf;
}

function zombiecleanupbrick(%spawnbrick)
{
	if(isobject(zombiesgroup)){
	%count = zombiesgroup.getcount();
	for(%a = -1; %a < %count; %a++){
		%obj = zombiesgroup.getobject(%a);
		////echo(%obj.spawnrbick);
		//echo(%obj);
		////echo(%spawnrbick.client.minigame);
		if(%obj.spawnbrick == %spawnbrick){
			%obj.kill();
		}
	}
	}
}

//%blah = newscriptobject(LOD){ classname = foo;}

//function foo::something(%bnlah){

	//iseventpending(); cancel();

package ZombiePrefOver
{
	function fxDTSBrickData::onadd(%this,%brick)
	{
		%brick.zombiedamage = mfloor(%brick.getdatablock().getvolume()/45);
		
		//echo(%brick.zombiedamage);
		Parent::onadd(%this,%brick);
	}
	function gameconnection::applyBodyParts(%blah)
	{
		Parent::applybodyparts(%blah);
		if(isobject(%blah.player)){
		if(%blah.player.getdatablock().getname() $= "PlayerFastZombieArmor")
		{
			zombieme(%blah);
			return;
		}
		}
			

	}

	function gameconnection::applyBodyColors(%blah)
	{
		Parent::applybodycolors(%blah);
		if(isobject(%blah.player)){
		if(%blah.player.getdatablock().getname() $= "PlayerFastZombieArmor")
		{
			zombieme(%blah);
			return;
		}
		}
			

	}
};
ActivatePackage(ZombiePrefOver);