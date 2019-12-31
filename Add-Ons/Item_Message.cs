datablock ItemData(MessageItem)
{
	category = "Item";  // Mission editor category

	equipment = true;

	//its already a member of item namespace so dont break it
	//className = "Item"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./shapes/blank.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Brick Message";
	iconName = "./ItemIcons/Parachute";
	doColorShift = true;
	colorShiftColor = "0.200 0.200 0.200 1.000";
	
	 // Dynamic properties defined by the scripts
	image = ParaImage;
	canDrop = true;
};

function MessageItem::onPickup(%this, %item, %obj, %amount)
{
	if(%item.spawnBrick.client.bl_id == %obj.client.bl_id)
	{
		if(%item.hasamessage){messageClient(%obj.client,"","\c2This brick has a message - respawn the item to set a new one.");return;}
		%obj.client.messageSet = 1;
		%obj.client.messageitem = %item;
		%item.hasamessage = 1;
		messageClient(%obj.client,"","\c2Type in chat what you want this brick to say.");
	}
	else
	{
		messageClient(%obj.client,"","\c2This is not your brick! Ask" SPC %item.spawnBrick.client.name SPC "to set messages if you want.");
	}
}

package MessageSetter {
	function serverCmdMessageSent(%client, %text){
		if(%client.messageSet){
			%client.messageitem.setShapeName(%text);
			%client.messageSet = 0;
			messageClient(%client,"","\c2Brick message set to \"" @ %text @ "\".");
		} else {
			Parent::serverCmdMessageSent(%client, %text);
		}
	}
};
ActivatePackage(MessageSetter);