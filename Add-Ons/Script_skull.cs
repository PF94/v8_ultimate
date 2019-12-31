datablock ShapeBaseImageData(faceskullImage)
{
	shapeFile = "./Shapes/faceskull.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.75";
	eyeOffset = "0 0 0.75";
	rotation = eulerToMatrix("0 0 0");
	scale = "0.1 0.1 0.1";
	doColorShift = false;
};

function serverCmdfaceskull(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(faceskullImage))
		{
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.mountImage(faceskullImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
			}
		}
	}
}