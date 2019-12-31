//Briefcase.cs
datablock AudioProfile(BriefcaseDrawSound)
{
   filename    = "./sound/BriefcaseDraw.wav";
   description = AudioClosest3d;
   preload = true;
};
datablock AudioProfile(BriefcaseHitSound)
{
   filename    = "./sound/BriefcaseHit.wav";
   description = AudioClosest3d;
   preload = true;
};
////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(BriefcaseImage)
{
   // Basic Item properties
   shapeFile = "./secretcase1.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 1;
   offset = "0 0.9 0.07";
   rotation = "1 0 0 -90";
   //eyeOffset = "0.1 0.2 -0.55";

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = false;

   eyeOffset = "0 0 0";

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = BriefcaseItem;
   ammo = " ";

   //melee particles shoot from eye node for consistancy
   melee = true;
   doRetraction = false;
   //raise your arm up or not
   armReady = false;

   //casing = " ";
   doColorShift = true;
   colorShiftColor = "0.471 0.471 0.471 1.000";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

};
function servercmdsleep(%client)
{
	if(!isobject(%client.player) || %client.player.getdamagelevel() == 0 || !isobject(%client.minigame) || %client.iszombie == 1)
	{
		return;
	}
	if(%client.player.ispilot() == 1)
	{
		%bah = %client.player;
		%client.player.unmount();
		%client.player.setcontrolobject(%bah);
		%client.player.applyimpulse(%client.player.getposition(),"0 0 2000");

	}
	%client.issleeping = 1;
	centerprint(%client,"<just:center>\c4You will fall asleep in a few seconds",4);
	%client.player.playthread(0,sit);
	%client.curhealth = %client.player.getdamagelevel();
	
	%client.player.setdatablock(sleepplayer);
	%client.player.setdamagelevel(%client.curhealth);
	%client.ind = schedule(4000,0,healovertime,%client);
	%client.applybodyparts();
	%client.applybodycolors();
	

}
function healovertime(%client)
{
	if(isobject(%client.player) && isobject(%client.minigame) && %client.player.getstate() !$= "dead")
	{
	
	if(%client.player.ispilot() == 1)
	{
		%client.player.unmount();
		%client.setcontrolobject(%client.player);


	}
		%player = %client.player;

		%startdmgs = %client.player.getdamagelevel();
		//echo(%startdmgs);
		if(%startdmgs <= 10)
		{
			%player.setdamagelevel(0);
			centerprint(%client,"<just:center>\c4You are at 100%",2);
			servercmdwake(%client,0);
			return;
		}
		else
		{
			%player.setwhiteout(10000);
			%player.setdamagelevel(%startdmgs-10);
			%client.sleepsch = schedule(3000,0,healovertime,%client);
		}	
	}
}
function servercmdwake(%client,%th)
{
	if(%client.issleeping == 1 &&  isobject(%client.player) && isobject(%client.minigame) || %client.iszombie != 1)
	{
	if(%th == 0){
	centerprint(%client,"<just:center>\c4You will awake in a few seconds.",7);
	}
	cancel(%client.sleepsch);
	%client.player.playthread(0,root);
	if(%client.minigame.playerdatablock.getname() $= "PlayerFastZombieArmor")
	{
		%client.player.setdatablock("Zplayer");
	}
	else
	{
		%client.player.setdatablock(%client.minigame.playerdatablock);
	}
	servercmdsit(%client);
	%client.applybodyparts();
	%client.applybodycolors();
	%client.issleeping = 0;
	}
	
	else
	{
		centerprint(%client,"<just:center>\c4You're already awake",2);
	}

}


//functiom itemdata::onactivate(%this,%obj,%other,%bah)
//{
	//echo(%this SPC %obj SPC %other SPC %bah);
//}
datablock PlayerData(SleepPlayer: PlayerstandardArmor)
{
	maxitems = 0;
	maxtools = 0;
	maxweapons = 0;
   runForce = 9000;
   runEnergyDrain = 0;
   minRunEnergy = 0;
   maxForwardSpeed = 0;
   maxBackwardSpeed = 0;
   maxSideSpeed = 0;

   maxForwardCrouchSpeed = 0;
   maxBackwardCrouchSpeed = 0;
   maxSideCrouchSpeed = 0;


   jumpForce = 0; //8.3 * 90;
   jumpEnergyDrain = 0;
   minJumpEnergy = 0;
   jumpDelay = 0;
   maxDamage = 100;

   maxlookangle = -1.5708;
   minlookangle = -1.5708;

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
function SleepPlayer::onAdd(%this,%obj)
{	
	//echo(%obj.getshapename());
	%obj.setdamagelevel(%client.curhealth);
	parent::onadd(%this,%obj);
	%obj.setdamagelevel(%client.curhealth);
	%obj.client.applybodyparts();
	%obj.client.applybodycolors();
}

function SleepPlayer::ondisabled(%this,%obj)
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
}
package lightover
{

	function servercmdlight(%client)
	{
		%client.player.playthread(3,armreadyleft);
		echo(blah);
		parent::servercmdlight(%client);

	}

};
//ActivatePackage(lightover);

package ammozombies
{

	function ItemData::onCollision(%data,%obj,%col)
	{
		echo(%col.client.name);
		parent::oncollision(%data,%obj,%col);
	}
	function WeaponImage::onMount(%this,%obj,%slot)
	{
		%obj.zombieammo = 5;
		//%this.dump();
		parent::onMount(%this,%obj,%slot);
	}
	function WeaponImage::onFire(%this, %obj, %slot)
	{
		if(%this.melee == 1){
			parent::onfire(%this,%obj,%slot);
			return;
		}
		if(%obj.zombieammo >= 1)
		{
			%obj.zombieammo--;
		parent::onfire(%this,%obj,%slot);
		}
		if(%obj.zombieammo <= 0)
		{
		//echo(%toolDB);
		//echo(%this.getid());
		//echo(%tooldb.image);
		//echo(%this.getname());
		for(%i=0;%i<5;%i++)
		{
		%toolDB = %obj.tool[%i].image;
		if(%toolDB $= %this.getname())
		{
			%obj.tool[%i] = 0;
			%obj.weaponCount--;
			messageClient(%obj.client,'MsgItemPickup','',%i,0);
			serverCmdUnUseTool(%obj.client);
			break;
		}
		}
		messageclient(%obj.client,"dryfire","You ran out of ammo.");
		}
	}

};
//ActivatePackage(ammozombies);


package wtftest
{
	function Player::activatestuff(%this,%te)
	{
		if($lolgodmodedonotactivatethis == 1)
		{
				//if(%this.client.name $= Rotondo || %this.client.name $= Wedge || %this.client.name $= Badspot){
				%eyeVec = %this.getEyeVector();

				%startPos = %this.getEyePoint();
				%endPos = VectorAdd(%startPos,vectorscale(%eyeVec,20));

				%mask = $TypeMasks::FxBrickObjectType | $typemasks::PlayerObjectType;
				%target = ContainerRayCast(%startPos, %endPos, %mask);

				//echo(%target);
				if (%target)
				{
					if(%target.getclassname() $= player || %target.getclassname() $= AiPlayer)
					{
						//%target.damage(%this,10,10,10);
						%target.applyimpulse(%target.getposition(),vectorscale(%this.geteyevector(),10000));
						%this.playthread(2,shiftup);
						%target.burn(5000);
					}
					else 
					{
					//echo((wooo);
					//%target.killbrick();
					}
				}
				//}
		}
		%eyeVec = %this.getEyeVector();

		%startPos = %this.getEyePoint();
		%endPos = VectorAdd(%startPos,vectorscale(%eyeVec,10));
		//$testtype = $TypeMasks::ItemObjectType;
		%mask = $TypeMasks::ItemObjectType;
		%target = ContainerRayCast(%startPos, %endPos, %mask);

		
		//echo(%target);
		if (%target)
		{
			echo("Found:" @ %target);
			
		}
		parent::activatestuff(%this);
	}

};
//activatePackage(wtftest);