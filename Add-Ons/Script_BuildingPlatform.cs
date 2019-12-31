$BuildingPlatform = 1;

datablock StaticShapeData(bPlatform)
{
shapeFile = "~/shapes/redBox.dts";
};

function serverCmdBPlatform(%client){
if($BuildingPlatform){
%client.hasPlatform = 1;
if(!isObject(%client.player)){return;}
if(isObject(%client.bPlatform)){%client.bPlatform.delete();}
%Platform = new StaticShape()
{
	datablock = bPlatform;
	position = vectorAdd(%client.player.position,"-1.36 -1.36 0");
	scale = "5 5 0.3";
	};
MissionCleanup.add(%platform);
%client.bPlatform = %platform;
	}
}

function serverCmdPlatformSize(%client, %xaxis, %yaxis, %zaxis){
%Platform = %client.BPlatform;
if(%xaxis > 10 || %yaxis > 10 || %zaxis > 1){
messageclient(%client,"","\c6You can't make the platform bigger than 10 x 10 x 1");
} else {
%size = %xaxis SPC %yaxis SPC %zaxis;
%Platform.setScale(%size);
	}
}

function serverCmdMoveP(%client, %axis, %distance){
%Platform = %client.bPlatform;
if(%client.hasPlatform){
if(%axis $= "z"){
%Platform.setTransform(vectorAdd(%Platform.getTransform(),"0 0 "@ %Distance @" 0 0 0 0"));
%Client.player.setTransform(vectorAdd(%client.player.getTransform(),"0 0 "@ %Distance @" 0 0 0 0"));
}else if(%axis $= "y"){
%Platform.setTransform(vectorAdd(%Platform.getTransform(),"0 "@ %Distance @ " 0 0 0 0 0"));
%Client.player.setTransform(vectorAdd(%client.player.getTransform(),"0 "@ %Distance @ " 0 0 0 0 0"));
}else if(%axis $= "x"){
%Platform.setTransform(vectorAdd(%Platform.getTransform(),%Distance @ " 0 0 0 0 0 0"));
%Client.player.setTransform(vectorAdd(%client.player.getTransform(),%Distance @ " 0 0 0 0 0 0"));
		}
	}
}

function serverCmdDeletePlatform(%client){
%Platform = %client.BPlatform;
%client.hasPlatform = 0;
%Platform.delete();
}

function serverCmdTogglePlatform(%client){
$BuildingPlatform = !$BuildingPlatform;
if($BuildingPlatform){
Messageall("","\c6Building platforms now \c0enabled");
} else {
Messageall("","\c6Building platforms now \c0disabled");
	}
}

function serverCmdClearPlatforms(%client){
if(%client.isAdmin || %client.isSuperAdmin){
for(%i=0;%i<MissionCleanUp.getCount();%i++){
		%Platform = MissionCleanUp.getObject(%i);
if(%platform.getDatablock().getName() $= "bPlatform"){
%platform.delete();
			}
		}
	}
}


if(!$addedPlatformMaps){
	$remapDivision[$remapCount] = "Building Platform";
		$remapName[$remapCount] = "x axis +";
		$remapCmd[$remapCount] = "xAxisP";
		$remapCount++;
		$remapName[$remapCount] = "x axis -";
		$remapCmd[$remapCount] = "xAxisN";
		$remapCount++;
		$remapName[$remapCount] = "y axis +";
		$remapCmd[$remapCount] = "yAxisP";
		$remapCount++;
		$remapName[$remapCount] = "y axis -";
		$remapCmd[$remapCount] = "yAxisN";
		$remapCount++;
		$remapName[$remapCount] = "z axis +";
		$remapCmd[$remapCount] = "zAxisP";
		$remapCount++;
		$remapName[$remapCount] = "z axis -";
		$remapCmd[$remapCount] = "zAxisN";
		$remapCount++;
}

function xAxisP(){
commandtoserver('Movep',"x","1");
}

function xAxisN(){
commandtoserver('Movep',"x","-1");
}

function yAxisP(){
commandtoserver('Movep',"y","1");
}

function yAxisN(){
commandtoserver('Movep',"y","-1");
}

function zAxisP(){
commandtoserver('Movep',"z","1");
}

function zAxisN(){
commandtoserver('Movep',"z","-1");
}