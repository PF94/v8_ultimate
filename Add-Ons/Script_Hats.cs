datablock ShapeBaseImageData(AfroImage)
{
	shapeFile = "./Shapes/Afro.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 -0.27 0.2";
	eyeOffset = "0 -0.27 0.2";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.800 0.000 0.000 1.000";
};

function serverCmdAfroRed(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(AfroImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(AfroImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(AfroGreenImage)
{
	shapeFile = "./Shapes/Afro.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 -0.27 0.2";
	eyeOffset = "0 -0.27 0.2";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.000 0.500 0.250 1.000";
};

function serverCmdAfroGreen(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(AfroGreenImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(AfroGreenImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(AfroBlueImage)
{
	shapeFile = "./Shapes/Afro.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 -0.27 0.2";
	eyeOffset = "0 -0.27 0.2";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.000 0.000 0.800 1.000";
};

function serverCmdAfroBlue(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(AfroBlueImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(AfroBlueImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(AfroBlackImage)
{
	shapeFile = "./Shapes/Afro.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 -0.27 0.2";
	eyeOffset = "0 -0.27 0.2";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.050 0.050 0.050 1.000";
};

function serverCmdAfroBlack(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(AfroBlackImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(AfroBlackImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(AfroWhiteImage)
{
	shapeFile = "./Shapes/Afro.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 -0.27 0.2";
	eyeOffset = "0 -0.27 0.2";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "1.000 1.000 1.000 1.000";
};

function serverCmdAfroWhite(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(AfroWhiteImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(AfroWhiteImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(AfroOrangeImage)
{
	shapeFile = "./Shapes/Afro.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 -0.27 0.2";
	eyeOffset = "0 -0.27 0.2";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.800 0.400 0.000 1.000";
};

function serverCmdAfroOrange(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(AfroOrangeImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(AfroOrangeImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(AfroCyanImage)
{
	shapeFile = "./Shapes/Afro.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 -0.27 0.2";
	eyeOffset = "0 -0.27 0.2";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.000 0.800 0.800 1.000";
};

function serverCmdAfroCyan(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(AfroCyanImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(AfroCyanImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(AfroBrownImage)
{
	shapeFile = "./Shapes/Afro.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 -0.27 0.2";
	eyeOffset = "0 -0.27 0.2";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.400 0.196 0.000 1.000";
};

function serverCmdAfroBrown(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(AfroBrownImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(AfroBrownImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(AfroYellowImage)
{
	shapeFile = "./Shapes/Afro.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 -0.27 0.2";
	eyeOffset = "0 -0.27 0.2";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.800 0.800 0.000 1.000";
};

function serverCmdAfroYellow(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(AfroYellowImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(AfroYellowImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(AfroPurpleImage)
{
	shapeFile = "./Shapes/Afro.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 -0.27 0.2";
	eyeOffset = "0 -0.27 0.2";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.500 0.000 0.500 1.000";
};

function serverCmdAfroPurple(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(AfroPurpleImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(AfroPurpleImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

function serverCmdAfrohelp(%client){
 messageClient(%client,'','\c6Type /AfroRed for a red afro');
 messageClient(%client,'','\c6Type /AfroGreen for a green afro');
 messageClient(%client,'','\c6Type /AfroBlue for a blue afro');
 messageClient(%client,'','\c6Type /AfroWhite for a white afro');
 messageClient(%client,'','\c6Type /AfroBlack for a black afro');
 messageClient(%client,'','\c6Type /AfroOrange for a orange afro');
 messageClient(%client,'','\c6Type /AfroCyan for a cyan afro');
 messageClient(%client,'','\c6Type /AfroBrown for a brown afro');
 messageClient(%client,'','\c6Type /AfroYellow for a yellow afro');
 messageClient(%client,'','\c6Type /AfroPurple for a purple afro');
 messageClient(%client,'','\c3ATTENTION! \c6Please scroll up (Page Up key) for other afros!');
}

datablock ShapeBaseImageData(BackwardCapImage)
{
	shapeFile = "./Shapes/BackwardBlokoCap.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.2";
	eyeOffset = "0 0 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.800 0.000 0.000 1.000";
};

function serverCmdBackwardCapRed(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(BackwardCapImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(BackwardCapImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(BackwardCapGreenImage)
{
	shapeFile = "./Shapes/BackwardBlokoCap.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.2";
	eyeOffset = "0 0 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.000 0.500 0.250 1.000";
};

function serverCmdBackwardCapGreen(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(BackwardCapGreenImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(BackwardCapGreenImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(BackwardCapBlueImage)
{
	shapeFile = "./Shapes/BackwardBlokoCap.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.2";
	eyeOffset = "0 0 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.000 0.000 0.800 1.000";
};

function serverCmdBackwardCapBlue(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(BackwardCapBlueImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(BackwardCapBlueImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(BackwardCapBlackImage)
{
	shapeFile = "./Shapes/BackwardBlokoCap.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.2";
	eyeOffset = "0 0 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.050 0.050 0.050 1.000";
};

function serverCmdBackwardCapBlack(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(BackwardCapBlackImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(BackwardCapBlackImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(BackwardCapWhiteImage)
{
	shapeFile = "./Shapes/BackwardBlokoCap.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.2";
	eyeOffset = "0 0 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "1.000 1.000 1.000 1.000";
};

function serverCmdBackwardCapWhite(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(BackwardCapWhiteImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(BackwardCapWhiteImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(BackwardCapOrangeImage)
{
	shapeFile = "./Shapes/BackwardBlokoCap.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.2";
	eyeOffset = "0 0 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.800 0.400 0.000 1.000";
};

function serverCmdBackwardCapOrange(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(BackwardCapOrangeImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(BackwardCapOrangeImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(BackwardCapCyanImage)
{
	shapeFile = "./Shapes/BackwardBlokoCap.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.2";
	eyeOffset = "0 0 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.000 0.800 0.800 1.000";
};

function serverCmdBackwardCapCyan(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(BackwardCapCyanImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(BackwardCapCyanImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(BackwardCapBrownImage)
{
	shapeFile = "./Shapes/BackwardBlokoCap.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.2";
	eyeOffset = "0 0 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.400 0.196 0.000 1.000";
};

function serverCmdBackwardCapBrown(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(BackwardCapBrownImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(BackwardCapBrownImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(BackwardCapYellowImage)
{
	shapeFile = "./Shapes/BackwardBlokoCap.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.2";
	eyeOffset = "0 0 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.800 0.800 0.000 1.000";
};

function serverCmdBackwardCapYellow(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(BackwardCapYellowImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(BackwardCapYellowImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(BackwardCapPurpleImage)
{
	shapeFile = "./Shapes/BackwardBlokoCap.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.2";
	eyeOffset = "0 0 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.500 0.000 0.500 1.000";
};

function serverCmdBackwardCapPurple(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(BackwardCapPurpleImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(BackwardCapPurpleImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

function serverCmdBackwardCaphelp(%client){
 messageClient(%client,'','\c6Type /BackwardCapRed for a red bloko cap');
 messageClient(%client,'','\c6Type /BackwardCapGreen for a green bloko cap');
 messageClient(%client,'','\c6Type /BackwardCapBlue for a blue bloko cap');
 messageClient(%client,'','\c6Type /BackwardCapWhite for a white bloko cap');
 messageClient(%client,'','\c6Type /BackwardCapBlack for a black bloko cap');
 messageClient(%client,'','\c6Type /BackwardCapOrange for a orange bloko cap');
 messageClient(%client,'','\c6Type /BackwardCapCyan for a cyan bloko cap');
 messageClient(%client,'','\c6Type /BackwardCapBrown for a brown bloko cap');
 messageClient(%client,'','\c6Type /BackwardCapYellow for a yellow bloko cap');
 messageClient(%client,'','\c6Type /BackwardCapPurple for a purple bloko cap');
 messageClient(%client,'','\c3ATTENTION! \c6Please scroll up (Page Up key) for other bloko caps!');
}

datablock ShapeBaseImageData(BlokoCapImage)
{
	shapeFile = "./Shapes/BlokoCap.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.2";
	eyeOffset = "0 0 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.800 0.000 0.000 1.000";
};

function serverCmdCapRed(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(BlokoCapImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(BlokoCapImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(BlokoCapGreenImage)
{
	shapeFile = "./Shapes/BlokoCap.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.2";
	eyeOffset = "0 0 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.000 0.500 0.250 1.000";
};

function serverCmdCapGreen(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(BlokoCapGreenImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(BlokoCapGreenImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(BlokoCapBlueImage)
{
	shapeFile = "./Shapes/BlokoCap.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.2";
	eyeOffset = "0 0 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.000 0.000 0.800 1.000";
};

function serverCmdCapBlue(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(BlokoCapBlueImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(BlokoCapBlueImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(BlokoCapBlackImage)
{
	shapeFile = "./Shapes/BlokoCap.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.2";
	eyeOffset = "0 0 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.050 0.050 0.050 1.000";
};

function serverCmdCapBlack(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(BlokoCapBlackImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(BlokoCapBlackImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(BlokoCapWhiteImage)
{
	shapeFile = "./Shapes/BlokoCap.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.2";
	eyeOffset = "0 0 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "1.000 1.000 1.000 1.000";
};

function serverCmdCapWhite(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(BlokoCapWhiteImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(BlokoCapWhiteImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(BlokoCapOrangeImage)
{
	shapeFile = "./Shapes/BlokoCap.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.2";
	eyeOffset = "0 0 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.800 0.400 0.000 1.000";
};

function serverCmdCapOrange(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(BlokoCapOrangeImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(BlokoCapOrangeImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(BlokoCapCyanImage)
{
	shapeFile = "./Shapes/BlokoCap.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.2";
	eyeOffset = "0 0 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.000 0.800 0.800 1.000";
};

function serverCmdCapCyan(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(BlokoCapCyanImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(BlokoCapCyanImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(BlokoCapBrownImage)
{
	shapeFile = "./Shapes/BlokoCap.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.2";
	eyeOffset = "0 0 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.400 0.196 0.000 1.000";
};

function serverCmdCapBrown(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(BlokoCapBrownImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(BlokoCapBrownImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(BlokoCapYellowImage)
{
	shapeFile = "./Shapes/BlokoCap.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.2";
	eyeOffset = "0 0 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.800 0.800 0.000 1.000";
};

function serverCmdCapYellow(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(BlokoCapYellowImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(BlokoCapYellowImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(BlokoCapPurpleImage)
{
	shapeFile = "./Shapes/BlokoCap.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.2";
	eyeOffset = "0 0 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.500 0.000 0.500 1.000";
};

function serverCmdCapPurple(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(BlokoCapPurpleImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(BlokoCapPurpleImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

function serverCmdCaphelp(%client){
 messageClient(%client,'','\c6Type /CapRed for a red bloko cap');
 messageClient(%client,'','\c6Type /CapGreen for a green bloko cap');
 messageClient(%client,'','\c6Type /CapBlue for a blue bloko cap');
 messageClient(%client,'','\c6Type /CapWhite for a white bloko cap');
 messageClient(%client,'','\c6Type /CapBlack for a black bloko cap');
 messageClient(%client,'','\c6Type /CapOrange for a orange bloko cap');
 messageClient(%client,'','\c6Type /CapCyan for a cyan bloko cap');
 messageClient(%client,'','\c6Type /CapBrown for a brown bloko cap');
 messageClient(%client,'','\c6Type /CapYellow for a yellow bloko cap');
 messageClient(%client,'','\c6Type /CapPurple for a purple bloko cap');
 messageClient(%client,'','\c3ATTENTION! \c6Please scroll up (Page Up key) for other bloko caps!');
}

datablock ShapeBaseImageData(LinkHatImage)
{
	shapeFile = "./Shapes/LinkHat.dts";
	emap = true;

	mountPoint = $HeadSlot;
	offset = "0 0 0.24";
	eyeoffset = "0 0 0.24";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.800 0.000 0.000 1.000";
};

function serverCmdLinkHatRed(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(LinkHatImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(LinkHatImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(LinkHatGreenImage)
{
	shapeFile = "./Shapes/LinkHat.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.24";
	eyeoffset = "0 0 0.24";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.000 0.500 0.250 1.000";
};

function serverCmdLinkHatGreen(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(LinkHatGreenImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(LinkHatGreenImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(LinkHatBlueImage)
{
	shapeFile = "./Shapes/LinkHat.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.24";
	eyeoffset = "0 0 0.24";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.000 0.000 0.800 1.000";
};

function serverCmdLinkHatBlue(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(LinkHatBlueImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(LinkHatBlueImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(LinkHatBlackImage)
{
	shapeFile = "./Shapes/LinkHat.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.24";
	eyeoffset = "0 0 0.24";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.050 0.050 0.050 1.000";
};

function serverCmdLinkHatBlack(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(LinkHatBlackImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(LinkHatBlackImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(LinkHatWhiteImage)
{
	shapeFile = "./Shapes/LinkHat.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.24";
	eyeoffset = "0 0 0.24";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "1.000 1.000 1.000 1.000";
};

function serverCmdLinkHatWhite(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(LinkHatWhiteImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(LinkHatWhiteImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(LinkHatOrangeImage)
{
	shapeFile = "./Shapes/LinkHat.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.24";
	eyeoffset = "0 0 0.24";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.800 0.400 0.000 1.000";
};

function serverCmdLinkHatOrange(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(LinkHatOrangeImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(LinkHatOrangeImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(LinkHatCyanImage)
{
	shapeFile = "./Shapes/LinkHat.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.24";
	eyeoffset = "0 0 0.24";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.000 0.800 0.800 1.000";
};

function serverCmdLinkHatCyan(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(LinkHatCyanImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(LinkHatCyanImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(LinkHatBrownImage)
{
	shapeFile = "./Shapes/LinkHat.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.24";
	eyeoffset = "0 0 0.24";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.400 0.196 0.000 1.000";
};

function serverCmdLinkHatBrown(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(LinkHatBrownImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(LinkHatBrownImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(LinkHatYellowImage)
{
	shapeFile = "./Shapes/LinkHat.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.24";
	eyeoffset = "0 0 0.24";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.800 0.800 0.000 1.000";
};

function serverCmdLinkHatYellow(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(LinkHatYellowImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(LinkHatYellowImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(LinkHatPurpleImage)
{
	shapeFile = "./Shapes/LinkHat.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.24";
	eyeoffset = "0 0 0.24";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.500 0.000 0.500 1.000";
};

function serverCmdLinkHatPurple(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(LinkHatPurpleImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(LinkHatPurpleImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

function serverCmdLinkHathelp(%client){
 messageClient(%client,'','\c6Type /LinkHatRed for a red LinkHat');
 messageClient(%client,'','\c6Type /LinkHatGreen for a green LinkHat');
 messageClient(%client,'','\c6Type /LinkHatBlue for a blue LinkHat');
 messageClient(%client,'','\c6Type /LinkHatWhite for a white LinkHat');
 messageClient(%client,'','\c6Type /LinkHatBlack for a black LinkHat');
 messageClient(%client,'','\c6Type /LinkHatOrange for a orange LinkHat');
 messageClient(%client,'','\c6Type /LinkHatCyan for a cyan LinkHat');
 messageClient(%client,'','\c6Type /LinkHatBrown for a brown LinkHat');
 messageClient(%client,'','\c6Type /LinkHatYellow for a yellow LinkHat');
 messageClient(%client,'','\c6Type /LinkHatPurple for a purple LinkHat');
 messageClient(%client,'','\c3ATTENTION! \c6Please scroll up (Page Up key) for other LinkHats!');
}

datablock ShapeBaseImageData(SantaHatImage)
{
	shapeFile = "./Shapes/Santahat.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.2";
	eyeOffset = "0 0 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.800 0.000 0.000 1.000";
};

function serverCmdSantaHatRed(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(SantaHatImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(SantaHatImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(SantaHatGreenImage)
{
	shapeFile = "./Shapes/SantaHat.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.2";
	eyeOffset = "0 0 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.000 0.500 0.250 1.000";
};

function serverCmdSantaHatGreen(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(SantaHatGreenImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(SantaHatGreenImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(SantaHatBlueImage)
{
	shapeFile = "./Shapes/SantaHat.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.2";
	eyeOffset = "0 0 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.000 0.000 0.800 1.000";
};

function serverCmdSantaHatBlue(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(SantaHatBlueImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(SantaHatBlueImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(SantaHatBlackImage)
{
	shapeFile = "./Shapes/SantaHat.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.2";
	eyeOffset = "0 0 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.050 0.050 0.050 1.000";
};

function serverCmdSantaHatBlack(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(SantaHatBlackImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(SantaHatBlackImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(SantaHatWhiteImage)
{
	shapeFile = "./Shapes/SantaHat.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.2";
	eyeOffset = "0 0 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "1.000 1.000 1.000 1.000";
};

function serverCmdSantaHatWhite(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(SantaHatWhiteImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(SantaHatWhiteImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(SantaHatOrangeImage)
{
	shapeFile = "./Shapes/SantaHat.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.2";
	eyeOffset = "0 0 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.800 0.400 0.000 1.000";
};

function serverCmdSantaHatOrange(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(SantaHatOrangeImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(SantaHatOrangeImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(SantaHatCyanImage)
{
	shapeFile = "./Shapes/SantaHat.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.2";
	eyeOffset = "0 0 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.000 0.800 0.800 1.000";
};

function serverCmdSantaHatCyan(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(SantaHatCyanImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(SantaHatCyanImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(SantaHatBrownImage)
{
	shapeFile = "./Shapes/SantaHat.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.2";
	eyeOffset = "0 0 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.400 0.196 0.000 1.000";
};

function serverCmdSantaHatBrown(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(SantaHatBrownImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(SantaHatBrownImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(SantaHatYellowImage)
{
	shapeFile = "./Shapes/SantaHat.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.2";
	eyeOffset = "0 0 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.800 0.800 0.000 1.000";
};

function serverCmdSantaHatYellow(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(SantaHatYellowImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(SantaHatYellowImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(SantaHatPurpleImage)
{
	shapeFile = "./Shapes/SantaHat.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.2";
	eyeOffset = "0 0 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.500 0.000 0.500 1.000";
};

function serverCmdSantaHatPurple(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(SantaHatPurpleImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(SantaHatPurpleImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

function servercmdSantaHathelp(%client){
 messageClient(%client,'','\c6Type /SantaHatRed for a red santa hat');
 messageClient(%client,'','\c6Type /SantaHatGreen for a green santa hat');
 messageClient(%client,'','\c6Type /SantaHatBlue for a blue santa hat');
 messageClient(%client,'','\c6Type /SantaHatWhite for a white santa hat');
 messageClient(%client,'','\c6Type /SantaHatBlack for a black santa hat');
 messageClient(%client,'','\c6Type /SantaHatOrange for a orange santa hat');
 messageClient(%client,'','\c6Type /SantaHatCyan for a cyan santa hat');
 messageClient(%client,'','\c6Type /SantaHatBrown for a brown santa hat');
 messageClient(%client,'','\c6Type /SantaHatYellow for a yellow santa hat');
 messageClient(%client,'','\c6Type /SantaHatPurple for a purple santa hat');
 messageClient(%client,'','\c3ATTENTION! \c6Please scroll up (Page Up key) for other santa hats!');
}

datablock ShapeBaseImageData(ScouterImage)
{
	shapeFile = "./Shapes/Scouter.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "-0.07 0.08 0.06";
	eyeOffset = "-0.07 0.02 0";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.800 0.000 0.000 1.000";
};

function serverCmdScouterRed(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(ScouterImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(ScouterImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(ScouterGreenImage)
{
	shapeFile = "./Shapes/Scouter.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "-0.07 0.08 0.06";
	eyeOffset = "-0.07 0.02 0";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.000 0.500 0.250 1.000";
};

function serverCmdScouterGreen(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(ScouterGreenImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(ScouterGreenImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(ScouterBlueImage)
{
	shapeFile = "./Shapes/Scouter.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "-0.07 0.08 0.06";
	eyeOffset = "-0.07 0.02 0";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.000 0.000 0.800 1.000";
};

function serverCmdScouterBlue(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(ScouterBlueImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(ScouterBlueImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(ScouterBlackImage)
{
	shapeFile = "./Shapes/Scouter.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "-0.07 0.08 0.06";
	eyeOffset = "-0.07 0.02 0";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.050 0.050 0.050 1.000";
};

function serverCmdScouterBlack(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(ScouterBlackImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(ScouterBlackImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(ScouterWhiteImage)
{
	shapeFile = "./Shapes/Scouter.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "-0.07 0.08 0.06";
	eyeOffset = "-0.07 0.02 0";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "1.000 1.000 1.000 1.000";
};

function serverCmdScouterWhite(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(ScouterWhiteImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(ScouterWhiteImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(ScouterOrangeImage)
{
	shapeFile = "./Shapes/Scouter.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "-0.07 0.08 0.06";
	eyeOffset = "-0.07 0.02 0";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.800 0.400 0.000 1.000";
};

function serverCmdScouterOrange(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(ScouterOrangeImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(ScouterOrangeImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(ScouterCyanImage)
{
	shapeFile = "./Shapes/Scouter.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "-0.07 0.08 0.06";
	eyeOffset = "-0.07 0.02 0";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.000 0.800 0.800 1.000";
};

function serverCmdScouterCyan(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(ScouterCyanImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(ScouterCyanImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(ScouterBrownImage)
{
	shapeFile = "./Shapes/Scouter.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "-0.07 0.08 0.06";
	eyeOffset = "-0.07 0.02 0";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.400 0.196 0.000 1.000";
};

function serverCmdScouterBrown(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(ScouterBrownImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(ScouterBrownImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(ScouterYellowImage)
{
	shapeFile = "./Shapes/Scouter.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "-0.07 0.08 0.06";
	eyeOffset = "-0.07 0.02 0";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.800 0.800 0.000 1.000";
};

function serverCmdScouterYellow(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(ScouterYellowImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(ScouterYellowImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(ScouterPurpleImage)
{
	shapeFile = "./Shapes/Scouter.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "-0.07 0.08 0.06";
	eyeOffset = "-0.07 0.02 0";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.500 0.000 0.500 1.000";
};

function serverCmdScouterPurple(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(ScouterPurpleImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(ScouterPurpleImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

function serverCmdScouterhelp(%client){
 messageClient(%client,'','\c6Type /ScouterRed for a red scouter');
 messageClient(%client,'','\c6Type /ScouterGreen for a green scouter');
 messageClient(%client,'','\c6Type /ScouterBlue for a blue scouter');
 messageClient(%client,'','\c6Type /ScouterWhite for a white scouter');
 messageClient(%client,'','\c6Type /ScouterBlack for a black scouter');
 messageClient(%client,'','\c6Type /ScouterOrange for a orange scouter');
 messageClient(%client,'','\c6Type /ScouterCyan for a cyan scouter');
 messageClient(%client,'','\c6Type /ScouterBrown for a brown scouter');
 messageClient(%client,'','\c6Type /ScouterYellow for a yellow scouter');
 messageClient(%client,'','\c6Type /ScouterPurple for a purple scouter');
 messageClient(%client,'','\c3ATTENTION! \c6Please scroll up (Page Up key) for other scouters!');
}

datablock ShapeBaseImageData(TopHatImage)
{
	shapeFile = "./Shapes/TopHat.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.1";
	eyeOffset = "0 0 0.2";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.800 0.000 0.000 1.000";
};

function serverCmdTopHatRed(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(TopHatImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(TopHatImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(TopHatGreenImage)
{
	shapeFile = "./Shapes/TopHat.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.1";
	eyeOffset = "0 0 0.2";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.000 0.500 0.250 1.000";
};

function serverCmdTopHatGreen(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(TopHatGreenImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(TopHatGreenImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(TopHatBlueImage)
{
	shapeFile = "./Shapes/TopHat.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.1";
	eyeOffset = "0 0 0.2";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.000 0.000 0.800 1.000";
};

function serverCmdTopHatBlue(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(TopHatBlueImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(TopHatBlueImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(TopHatBlackImage)
{
	shapeFile = "./Shapes/TopHat.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.1";
	eyeOffset = "0 0 0.2";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.050 0.050 0.050 1.000";
};

function serverCmdTopHatBlack(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(TopHatBlackImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(TopHatBlackImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(TopHatWhiteImage)
{
	shapeFile = "./Shapes/TopHat.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.1";
	eyeOffset = "0 0 0.2";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "1.000 1.000 1.000 1.000";
};

function serverCmdTopHatWhite(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(TopHatWhiteImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(TopHatWhiteImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(TopHatOrangeImage)
{
	shapeFile = "./Shapes/TopHat.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.1";
	eyeOffset = "0 0 0.2";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.800 0.400 0.000 1.000";
};

function serverCmdTopHatOrange(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(TopHatOrangeImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(TopHatOrangeImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(TopHatCyanImage)
{
	shapeFile = "./Shapes/TopHat.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.1";
	eyeOffset = "0 0 0.2";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.000 0.800 0.800 1.000";
};

function serverCmdTopHatCyan(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(TopHatCyanImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(TopHatCyanImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(TopHatBrownImage)
{
	shapeFile = "./Shapes/TopHat.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.1";
	eyeOffset = "0 0 0.2";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.400 0.196 0.000 1.000";
};

function serverCmdTopHatBrown(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(TopHatBrownImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(TopHatBrownImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(TopHatYellowImage)
{
	shapeFile = "./Shapes/TopHat.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.1";
	eyeOffset = "0 0 0.2";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.800 0.800 0.000 1.000";
};

function serverCmdTopHatYellow(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(TopHatYellowImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(TopHatYellowImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(TopHatPurpleImage)
{
	shapeFile = "./Shapes/TopHat.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.1";
	eyeOffset = "0 0 0.2";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.500 0.000 0.500 1.000";
};

function serverCmdTopHatPurple(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(TopHatPurpleImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(TopHatPurpleImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

function servercmdTopHathelp(%client){
 messageClient(%client,'','\c6Type /TopHatRed for a red top hat');
 messageClient(%client,'','\c6Type /TopHatGreen for a green top hat');
 messageClient(%client,'','\c6Type /TopHatBlue for a blue top hat');
 messageClient(%client,'','\c6Type /TopHatWhite for a white top hat');
 messageClient(%client,'','\c6Type /TopHatBlack for a black top hat');
 messageClient(%client,'','\c6Type /TopHatOrange for a orange top hat');
 messageClient(%client,'','\c6Type /TopHatCyan for a cyan top hat');
 messageClient(%client,'','\c6Type /TopHatBrown for a brown top hat');
 messageClient(%client,'','\c6Type /TopHatYellow for a yellow top hat');
 messageClient(%client,'','\c6Type /TopHatPurple for a purple top hat');
 messageClient(%client,'','\c3ATTENTION! \c6Please scroll up (Page Up key) for other top hats!');
}

datablock ShapeBaseImageData(WizardHatImage)
{
	shapeFile = "./Shapes/WizardHat.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.2";
	eyeOffset = "0 0 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.800 0.000 0.000 1.000";
};

function serverCmdWizHatRed(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(WizardHatImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(WizardHatImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(WizardHatGreenImage)
{
	shapeFile = "./Shapes/WizardHat.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.2";
	eyeOffset = "0 0 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.000 0.500 0.250 1.000";
};

function serverCmdWizHatGreen(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(WizardHatGreenImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(WizardHatGreenImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(WizardHatBlueImage)
{
	shapeFile = "./Shapes/WizardHat.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.2";
	eyeOffset = "0 0 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.000 0.000 0.800 1.000";
};

function serverCmdWizHatBlue(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(WizardHatBlueImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(WizardHatBlueImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(WizardHatBlackImage)
{
	shapeFile = "./Shapes/WizardHat.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.2";
	eyeOffset = "0 0 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.050 0.050 0.050 1.000";
};

function serverCmdWizHatBlack(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(WizardHatBlackImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(WizardHatBlackImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(WizardHatWhiteImage)
{
	shapeFile = "./Shapes/WizardHat.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.2";
	eyeOffset = "0 0 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "1.000 1.000 1.000 1.000";
};

function serverCmdWizHatWhite(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(WizardHatWhiteImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(WizardHatWhiteImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(WizardHatOrangeImage)
{
	shapeFile = "./Shapes/WizardHat.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.2";
	eyeOffset = "0 0 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.800 0.400 0.000 1.000";
};

function serverCmdWizHatOrange(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(WizardHatOrangeImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(WizardHatOrangeImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(WizardHatCyanImage)
{
	shapeFile = "./Shapes/WizardHat.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.2";
	eyeOffset = "0 0 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.000 0.800 0.800 1.000";
};

function serverCmdWizHatCyan(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(WizardHatCyanImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(WizardHatCyanImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(WizardHatBrownImage)
{
	shapeFile = "./Shapes/WizardHat.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.2";
	eyeOffset = "0 0 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.400 0.196 0.000 1.000";
};

function serverCmdWizHatBrown(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(WizardHatBrownImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(WizardHatBrownImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(WizardHatYellowImage)
{
	shapeFile = "./Shapes/WizardHat.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.2";
	eyeOffset = "0 0 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.800 0.800 0.000 1.000";
};

function serverCmdWizHatYellow(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(WizardHatYellowImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(WizardHatYellowImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

datablock ShapeBaseImageData(WizardHatPurpleImage)
{
	shapeFile = "./Shapes/WizardHat.dts";
	emap = true;
	mountPoint = $HeadSlot;
	offset = "0 0 0.2";
	eyeOffset = "0 0 0.4";
	rotation = eulerToMatrix("0 0 0");
	scale = "1 1 1";
	doColorShift = true;
	colorShiftColor = "0.500 0.000 0.500 1.000";
};

function serverCmdWizHatPurple(%client)
{
	%player = %client.player;

	if(isObject(%player))
	{
		if(%player.getMountedImage(2) $= nametoID(WizardHatPurpleImage))
		{
			%player.unmountImage(2);
			%client.applyBodyParts();
			%client.applyBodyColors();
		}
		else
		{
			%player.unmountImage(2);
			%player.mountImage(WizardHatPurpleImage,2);

			for(%i = 0;$hat[%i] !$= "";%i++)
			{
				%player.hideNode($hat[%i]);
				%player.hideNode($accent[%i]);
			}
		}
	}
}

function servercmdWizHathelp(%client){
 messageClient(%client,'','\c6Type /WizHatRed for a red wizard hat');
 messageClient(%client,'','\c6Type /WizHatGreen for a green wizard hat');
 messageClient(%client,'','\c6Type /WizHatBlue for a blue wizard hat');
 messageClient(%client,'','\c6Type /WizHatWhite for a white wizard hat');
 messageClient(%client,'','\c6Type /WizHatBlack for a black wizard hat');
 messageClient(%client,'','\c6Type /WizHatOrange for a orange wizard hat');
 messageClient(%client,'','\c6Type /WizHatCyan for a cyan wizard hat');
 messageClient(%client,'','\c6Type /WizHatBrown for a brown wizard hat');
 messageClient(%client,'','\c6Type /WizHatYellow for a yellow wizard hat');
 messageClient(%client,'','\c6Type /WizHatPurple for a purple wizard hat');
 messageClient(%client,'','\c3ATTENTION! \c6Please scroll up (Page Up key) for other wizard hats!');
}

function serverCmdHathelp(%client){
 messageClient(%client,'','\c6Type /LinkHatHelp for a list of Link hats');
 messageClient(%client,'','\c6Type /BackwardCapHelp for a list of Backward Caps');
 messageClient(%client,'','\c6Type /SantaHatHelp for a list of Santa Hats');
 messageClient(%client,'','\c6Type /AfroHelp for a list of Afros');
 messageClient(%client,'','\c6Type /TopHatHelp for a list of Top Hats');
 messageClient(%client,'','\c6Type /ScouterHelp for a list of Scouters');
 messageClient(%client,'','\c6Type /WizHatHelp for a list of Wizard Hats');
 messageClient(%client,'','\c3ATTENTION! \c6Please scroll up (Page Up key) for other hats!');
}