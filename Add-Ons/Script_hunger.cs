//Default Settings
$foodcost = 10;
$decaytime = 120000;
$Hungeron = 1;
$foodcount = 4;

//Default foods

$food[1] = "Bread";
$food[2] = "Soup";
$food[3] = "Sandwich";
$food[4] = "Pizza";

$foodcost[1] = "1";
$foodcost[2] = "5";
$foodcost[3] = "10";
$foodcost[4] = "15";

$foodheal[1] = "5";
$foodheal[2] = "10";
$foodheal[3] = "15";
$foodheal[4] = "20";

//Package for joining and leaving game
package Hunger
{
   	function GameConnection::onConnect(%client)
   	{   
	$HungerLevel[%client] = 100;
	$decay[%client] = schedule($decaytime,0,"hungerdecay",%client);
	$Food[%client] = 1;
	$canbuy[%client] = 0;
	$Inv[%client,2] = 1;
        Parent::onConnect(%client);
   	}

	function GameConnection::OnClientLeaveGame(%client)
	{
	cancel($decay[%client]);
	Parent::onClientLeaveGame(%client);
	}

	function brickFoodShopData::onPlant(%this,%brick)
	{
 				
		Parent::onPlant(%this,%brick);
 		schedule(100,0,"foodshop",%brick);

	}

	function brickFoodShopData::onRemove(%this,%brick)
	{
 		Parent::onRemove(%this,%brick);
 		if(isObject(%brick.trigger)){%brick.trigger.delete();}
	}	

};
activatePackage(Hunger);

function serverCmdCheckHunger(%client)
{
	if($HungerLevel[%client] >= 50)
	{
	messageclient(%client,"","\c6Your fullness level is \c2" @ $HungerLevel[%client] @ "/100");
	}
	else
	{
	messageclient(%client,"","\c6Your fullness level is \c0" @ $HungerLevel[%client] @ "/100");
	}
}


function HUngerdecay(%client){
if($hungeron == 0){
$decay[%client] = schedule($decaytime,0,"hungerdecay",%client);
}
else
{
	%hungerl = $HungerLevel[%client];
	$HungerLevel[%client] = %hungerl - 10;
	if($HungerLevel[%client] == 0)
		{
		servercmdsuicide(%client);
		messageAll("", %client.name @ " has died of hunger.");
		$HungerLevel[%client] = 50;
		$decay[%client] = schedule($decaytime,0,"hungerdecay",%client);
		}
		else 
		{
		schedule(100,0,"showhunger",%client);
		$decay[%client] = schedule($decaytime,0,"hungerdecay",%client);
		}
	}
}

function showhunger(%client){
	if($HungerLevel[%client] >= 50)
	{
	messageclient(%client,"","\c6Your fullness level is \c2" @ $HungerLevel[%client] @ "/100");
	}
	else
	{
	messageclient(%client,"","\c6Your fullness level is \c0" @ $HungerLevel[%client] @ "/100");
	}
}



function serverCmdDecaytime(%client, %temp)
{
	if(%client.isSuperAdmin || %client.isAdmin){
	%time = %temp * 60000;
	$decaytime = %time;
	messageclient(%client,"","\c6You have changed the decay time to \c7" @ %temp @ "\c6 minutes.");
	}		
}

function serverCmdHunger(%client, %temp)
{
	if(%client.isSuperAdmin || %client.isAdmin){
		if($hungeron == 1){
		$hungeron = 0;
		messageall("","\c6Hunger has been turned off.");
		}
		else
		{
		$hungeron = 1;
		messageall("","\c6Hunger has been turned on.");
		}
	}		
}


//Shops

datablock TriggerData(Shop){
   tickPeriodMS = 100;
};

function shop::onEnterTrigger(%this, %trigger, %obj){
	if(!%obj.client.inshop){// if the enteree isnt already inside
	%obj.client.inshop = 1;
      	messageClient(%obj.client, "", "\c6Welcome to the food shop, type \c7/items\c6 for a list of items." @ %obj.client.inshop);
}
}

function shop::OnLeaveTrigger(%thid, %trigger, %obj){
	if(%obj.client.inshop){//make sure they're actually inside
	%obj.client.inshop = 0;//hes no longer inside
      messageClient(%obj.client, "", "\c6You have left the shop.");
  }
}

function serverCmditems(%client)
{
	if(%client.inshop)
	{
	%i = 1;
	%foodname = $food[%i];
	%tfoodcost = $foodcost[%i];
	%tfoodheal = $foodheal[%i];
	while(%i <= $foodcount)
	{
	messageClient(%client, "", "\c7" @ %i @ ".\c6" @ %foodname @ ",\c2 +" @ %tfoodheal @ ", " @ $Currency @ %tfoodcost);
	%foodname = $food[%i++];
	%tfoodcost = $foodcost[%i];
	%tfoodheal = $foodheal[%i];
	}
	messageClient(%client, "", "\c6To buy an item type /buyitem \c7[Number] [Amount]");
	}
	else
	{
	messageClient(%client, "", "\c0You are not in a shop.");		
	}
}

function serverCmdBuyitem(%client, %num, %amount)
{
	if(%client.inshop)
	{	
	%foodname = $food[%num];
	%tfoodcost = $foodcost[%num];
	%cost = %tfoodcost * %amount;
		if(%client.Cash >= %cost){
		%client.Cash = %client.Cash - %cost;
		$Inv[%client,%num] = %amount;
		messageClient(%client, "", "\c6 You have bought \c2" @ %amount @ " \c6" @ %foodname @ "(s)");		
		}
		else
		{
		messageclient(%client,"","\c6You Do not have enough money to buy that much food!");
		}

	}
	else
	{
	messageClient(%client, "", "\c0You are not in a shop.");
	}
}


function serverCmdViewfoods(%client)
{
%id = 1;
%name = $food[%id];
%amount = $Inv[%client,%id];
while(%name !$= ""){
messageClient(%client, "", "\c7" @ %id @ ".\c6" @ %name @ "--\c2" @ %amount);
%name = $food[%id++];
%amount = $Inv[%client,%id];
}
messageClient(%client, "", "\c6To eat a food type /eat \c7[Number]");
}


function serverCmdEat(%client, %num){
if($Inv[%client,%num] >= 1)
	{	
	%minfull = 100 - $foodheal[%num];
	if($HungerLevel[%client] <= %minfull)
		{
		$HungerLevel[%client] = $HungerLevel[%client] + $foodheal[%num];
		$Inv[%client,%num] = $Inv[%client,%num] - 1;
		messageclient(%client,"","\c6Yum! Your fullness level is now " @ $HungerLevel[%client] @ "/100");
		}
		else
		{
		messageclient(%client,"","\c6You are not hungery enough to eat, please wait until your fullness is " @ %minfull @" or less");
		}	
	}
	else
	{
	messageclient(%client,"","\c6You do not have any of that food to eat!");
	}

}

function foodshop(%brick)
{
	if(%brick.getGroup().client.isSuperAdmin || %brick.getGroup().client.isAdmin){
	%posx = getWord(%brick.position,0);
	%posy = getWord(%brick.position,1);
	%posz = getWord(%brick.position,2);
	%posx = %posx - 0.5;
	%posy = %posy + 0.5;
	
	%temp = new trigger() {
	Datablock = "Shop";
	position = %posx SPC  %posy SPC %posz;
	scale = "1 1 1";
	rotation = "0 0 0 0";
	polyhedron = "0.0000000 0.0000000 0.0000000 1.0000000 0.0000000 0.0000000 0.0000000 -1.0000000 0.0000000 0.0000000 0.0000000 1.0000000";
	};
	%brick.trigger = %temp;
	MissionCleanup.add(%temp);
  	}
	else
	{
	messageClient(%brick.getGroup().client, "", "This brick is admin only!");
	%brick.killbrick();
	}

}

function serverCmdnewfood(%client, %name, %cost, %heal)
{
	if(%client.isSuperAdmin || %client.isAdmin){
	%num = $foodcount + 1;
	$food[%num] = %name;
	$foodcost[%num] = %cost;
	$foodheal[%num] = %heal;
	messageAll("","\c7" @ %client.name @ "\c6 created a new food called " @ $food[%num]);
	$Foodcount = $foodcount + 1;
	
	}		
}

function serverCmddelfood(%client, %num)
{
	if(%client.isSuperAdmin || %client.isAdmin){
	$food[%num] = "";
	$foodcost[%num] = "";
	$foodheal[%num] = "";
	$Foodcount = $foodcount - 1;
	%food = $food[%num];
	%temp = %num + 1;
	$FOOD[%NUM] = $FOOD[%temp];
	$foodcost[%num] = $foodcost[%temp];
	$foodheal[%num] = $foodhelp[%temp];
	%food = $food[%num++];
	%temp = %Temp++;
		while(%food !$= ""){
		$FOOD[%NUM] = $FOOD[%temp];
		$foodcost[%num] = $foodcost[%temp];
		$foodheal[%num] = $foodhelp[%temp];
		%food = $food[%num++];
		%temp = %Temp++;
		}
		messageclient(%client,"","\c6Food Deleted!");
	}		
}

//DATABLOCK

datablock fxDTSBrickData(brickFoodShopData : brick2x2fdata)
{
 uiName = "Food Shop Point";
 Category = "Special";
 subCategory = "Food Script";
};



