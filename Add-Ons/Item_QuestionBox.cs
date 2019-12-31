//By MrPickle
$QuestionBox::Mode = 1;
$QuestionBox::Weapons = "Sword PushBroom RocketLauncher Gun Bow Spear";
$QuestionBox::Exlude ="Wrench Hammer";

datablock ItemData(QuestionBoxItem){
	category = "Item";
	equipment = true;
	shapeFile = "./shapes/questionbox.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	image = QuestionBoxImage;
	canDrop = true;
	uiName = "Question Box";
	doColorShift = true;
	colorShiftColor = "1.000 0.500 0.000 1.000";
};

datablock ShapeBaseImageData(QuestionBoxImage){
   shapeFile = "./shapes/questionbox.dts";
   emap = true;
   mountPoint = 0;
   offset = "-0.17 0.17 -0.07";
   rotation = eulerToMatrix("-90 90 0");
   eyeOffset = "0.7 1.2 -0.15";
   eyeRotation = eulerToMatrix("90 -90 0");
   doColorShift = true;
   colorShiftColor = SkiItem.colorShiftColor;
   correctMuzzleVector = false;
   className = "WeaponImage";
   item = QuestionBoxItem;
   doColorShift = true;
   colorShiftColor = QuestionBoxItem.colorShiftColor;
};

function QuestionBoxItem::onAdd(%this, %obj){
	%obj.rotate = 1;
	Parent::onAdd(%this, %obj);
}

function QuestionBoxItem::onCollision(%this, %obj, %col){
	if(strStr(strLwr(%col.getClassname()),"vehicle")!=-1){
		for(%i=0; %i<%col.getDatablock().numMountPoints; %i++){
			%player = %col.getMountedObject(%i);
			if(isObject(%player))
				%this.onPickup(%obj, %player);
		}
	} else
	Parent::onCollision(%this, %obj, %col);
}

function QuestionBoxItem::onPickup(%this, %obj, %player){
Parent::onPickup(%this, %obj, %player);	
%this.Random(%player);
}

function QuestionBoxItem::Random(%this, %player){
	switch($QuestionBox::Mode){
		case 1:
			%ranWeapon = nameToID(getWord($QuestionBox::Weapons,getRandom(0,getWordCount($QuestionBox::Weapons))) @ "Item");
		case 2:
			%weaponCount = "";
			for(%i=0; %i<DatablockGroup.getCount(); %i++){ 
			%obj = DatablockGroup.getObject(%i); 
				if(%obj.getClassname() $= "ItemData" && %obj.getName() !$= "QuestionBoxItem")
					%weapon[%weaponCount++] = %obj;
			}
			%ranWeapon = %weapon[getRandom(0,%weaponCount)];
		case 3:
			%weaponCount = "";
			for(%i=0; %i<DatablockGroup.getCount(); %i++){ 
			%obj = DatablockGroup.getObject(%i); 
				if(%obj.getClassname() $= "ItemData" && %obj.getName() !$= "QuestionBoxItem" && !isExcluded(%obj.getName()))
					%weapon[%weaponCount++] = %obj;
			}
			%ranWeapon = %weapon[getRandom(0,%weaponCount)];
	}
	
if(%ranWeapon != -1){
	for(%i=0; %i<=4; %i++){
		if(%player.tool[%i] == %this){
			%player.tool[%i] = %ranWeapon;
			messageClient(%player.client,'MsgItemPickup','',%i,%ranWeapon);
			break;
		}
	}
} else {
	for(%i=0; %i<=4; %i++){
		if(%player.tool[%i] == %this){
			%player.tool[%i] = 0;
			messageClient(%player.client,'MsgItemPickup','',%i,"");
			break;
			}
		}
	}
}

function isExcluded(%weapon){
	for(%i=0; %i<getWordCount($QuestionBox::Exlude); %i++){
		%excluded = getWord($QuestionBox::Exlude,%i) @ "Item";
		if(%excluded $= %weapon){
			return 1;
			break;
		}
	}
	return 0;
}

package minigameRandom{
	function GameConnection::SpawnPlayer(%this){
		Parent::SpawnPlayer(%this);
		if(isObject(%this.minigame)){
			for(%i=0; %i<=4; %i++)
				nameToID(QuestionBoxItem).random(%this.player);
		}
	}
	
	function serverCmdsetMinigameData(%client, %a){
		Parent::serverCmdsetMinigameData(%client, %a);
		if(isObject(%client.minigame)){
			for(%i=0; %i<%client.minigame.numMembers; %i++){
				for(%j=0; %j<=4; %j++)
					nameToID(QuestionBoxItem).random(%client.minigame.member[%i].player);
			}
		}
	}
};
activatePackage(minigameRandom);