datablock fxDTSBrickData(brickCheckPointData : brickSpawnPointData)
{
 uiName = "Checkpoint Spawn";
 subCategory = "Interactive";
};
function brickCheckPointData::onPlant(%this,%brick)
{
 Parent::onPlant(%this,%brick);
 schedule(100,0,TriggerMakeBrick,%brick,CheckPointTrigger);
}
function brickCheckPointData::onRemove(%this,%brick)
{
 Parent::onRemove(%this,%brick);
 if(isObject(%brick.trigger)){%brick.trigger.delete();}
}
function TriggerMakeBrick(%brick,%trig)
{
	%dist1 = getWords(%brick.getWorldBox(),0,2);
	%dist2 = getWords(%brick.getWorldBox(),3,6);
	%x = mAbs(getWord(%dist1,0) - getWord(%dist2,0));
	%y = mAbs(getWord(%dist1,1) - getWord(%dist2,1));
	%z = mAbs(getWord(%dist1,2) - getWord(%dist2,2));
	%pos = %brick.position;
	%pos = getWord(%pos,0) SPC getWord(%pos,1) SPC getWord(%pos,2);
	%p = new Trigger()
	{
		datablock = %trig;
		position = vectorAdd(%pos,-%x/2-0.1 SPC %y/2+0.1 SPC -%z/2-0.1);
		polyhedron = "0.0000000 0.0000000 0.0000000 1.0000000 0.0000000 0.0000000 0.0000000 -1.0000000 0.0000000 0.0000000 0.0000000 1.0000000";
		scale = %x+0.2 SPC %y+0.2 SPC %z + 0.5;
		rotation = "1 0 0 0";
	};
	%brick.trigger = %p;
	%p.spawnbrick = %brick;
	%p.player = %brick.getGroup().client.player;
	MissionCleanup.add(%p);
}

datablock TriggerData(CheckPointTrigger)
{
 tickPeriodMS = 0;
};

function CheckPointTrigger::onTickTrigger(%this,%trigger)
{
}

function CheckPointTrigger::onEnterTrigger(%this,%trigger,%obj)
{
	if(isObject(%obj.client) && isObject(%obj.client.minigame))
	{
		if(%obj.client.checkpoint !$= %trigger.spawnbrick)
		commandtoclient(%obj.client,'centerprint',"\c4Checkpoint saved!",2,2,2);
		%obj.client.checkpoint = %trigger.spawnbrick;
	}
}

function CheckPointTrigger::onLeaveTrigger(%this,%trigger,%obj)
{
}

//Spawn
//Type: Package
//When a client spawns, if they have on a Checkpoint, they use that to spawn instead of their usual spawn brick.
package Spawning
{
 function GameConnection::CreatePlayer(%this,%transform) //Uniform
 {
  if(isObject(%this.minigame) && isObject(%this.checkpoint))
  {
   %Transform = %this.checkpoint.getTransform();
  }
  %ret = Parent::CreatePlayer(%this,%transform);
  return %ret;
 }
 function servercmdLeaveMinigame(%client)
 {
   Parent::servercmdLeaveMinigame(%client);
   schedule(100,0,minigamecheck,%client);
  }
 function servercmdEndMinigame(%client)
 {
   Parent::servercmdEndMinigame(%client);
   schedule(100,0,minigamecheck,%client);
  }
 function minigamecheck(%client)
 {
   if(!isObject(%client.minigame)) //If they aren't "forced" to one still i.e. Aloshi's mod/Team DM
   {
    %client.checkpoint = "";
   }
 }
};
activatepackage(Spawning);