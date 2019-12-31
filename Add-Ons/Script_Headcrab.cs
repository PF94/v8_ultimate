datablock ShapeBaseImageData(HeadcrabImage)
{
	shapeFile = "./Shapes/Headcrab.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0.023 0.4";
	eyeOffset = "0 0.023 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = false;
	colorShiftColor = "1.000 1.000 1.000 1.000";
};

function serverCmdHeadcrab(%client)
{

	bottomPrintAll(%client.name SPC "has become a headcrab zombie!",3,1);
	//other stuff

	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(HeadcrabImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(HeadcrabImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}