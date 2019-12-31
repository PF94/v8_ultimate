datablock ItemData(ToolWandItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "base/data/shapes/wand.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "ToolWand";
	iconName = "./ItemIcons/wand";
	doColorShift = true;
	colorShiftColor = "0 0 1 1";

	 // Dynamic properties defined by the scripts
	image = ToolWandImage;
	canDrop = true;
};

datablock ParticleData(ToolWandExplosionParticle)
{
	dragCoefficient      = 9.99995;
	gravityCoefficient   = -0.002;
	inheritedVelFactor   = 0.19;
	constantAcceleration = 0.0;
	spinRandomMin        = -90;
	spinRandomMax        = 90;
	lifetimeMS           = 500;
	lifetimeVarianceMS   = 300;
	textureName          = "base/data/particles/chunk";
	colors[0]            = "0.2 1.0 0.1 0.6";
	colors[1]            = "0.1 1.0 0.1 0.6";
	colors[2]            = "0.7 1.0 0.7 0.6";
	colors[3]            = "0.8 1.0 0.8 0.6";
	sizes[0]             = 0.3;
	sizes[1]             = 0.3;
	sizes[2]             = 0.2;
	sizes[3]             = 0.2;
	times[0]             = 0;
	times[1]             = 0.8;
	times[2]             = 1;
	times[3]             = 2;
};

datablock ParticleEmitterData(ToolWandExplosionEmitter)
{
	ejectionPeriodMS = 2;
	periodVarianceMS = 0;
	ejectionVelocity = 15;
	velocityVariance = 5;
	ejectionOffset   = 0.25;
	thetaMin         = 0;
	thetaMax         = 120;
	phiReferenceVel  = 0;
	phiVariance      = 360;
	overrideAdvance  = false;
	particles        = "ToolWandExplosionParticle";
};

datablock ExplosionData(ToolWandExplosion : wandExplosion)
{
	lifetimeMS      = 180;
	emitter[0]      = ToolWandExplosionEmitter;
	lightStartColor = "0 1 0 1";
	lightEndColor   = "0 0 0 0";
};

datablock ParticleData(ToolWandParticleA)
{
	dragCoefficient      = 0;
	gravityCoefficient   = -0.5;
	inheritedVelFactor   = 0;
	constantAcceleration = 0.0;
	spinRandomMin        = -90;
	spinRandomMax        = 90;
	lifetimeMS           = 600;
	lifetimeVarianceMS   = 200;
	textureName          = "base/data/particles/dot";
	colors[0]            = "0 1 0 0.9";
	colors[1]            = "0 1 0 0.7";
	colors[2]            = "0 1 0 0.5";
	colors[3]            = "0 1 0 0.3";
	sizes[0]             = 0.1;
	sizes[1]             = 0.1;
	sizes[2]             = 0.2;
	sizes[3]             = 1;
	times[0]             = 0;
	times[1]             = 0.8;
	times[2]             = 1;
	times[3]             = 2;
};

datablock ParticleEmitterData(ToolWandEmitterA)
{
	ejectionPeriodMS = 10;
	periodVarianceMS = 2;
	ejectionVelocity = 0.2;
	velocityVariance = 0;
	ejectionOffset   = 0.09;
	thetaMin         = 0;
	thetaMax         = 180;
	phiReferenceVel  = 0;
	phiVariance      = 360;
	overrideAdvance  = false;
	particles        = "ToolWandParticleA";
};

datablock ParticleData(ToolWandParticleB)
{
	dragCoefficient      = 0;
	gravityCoefficient   = -0.2;
	inheritedVelFactor   = 0.5;
	constantAcceleration = 0.0;
	spinRandomMin        = -90;
	spinRandomMax        = 90;
	lifetimeMS           = 600;
	lifetimeVarianceMS   = 200;
	textureName          = "base/data/particles/dot";
	colors[0]            = "1 0 0 0.9";
	colors[1]            = "1 0 0 0.9";
	colors[2]            = "1 0 0 0.9";
	colors[3]            = "1 0 0 0.9";
	sizes[0]             = 0.1;
	sizes[1]             = 0.1;
	sizes[2]             = 0.2;
	sizes[3]             = 1;
	times[0]             = 0;
	times[1]             = 0.8;
	times[2]             = 1;
	times[3]             = 2;
};

datablock ParticleEmitterData(ToolWandEmitterB)
{
	ejectionPeriodMS = 4;
	periodVarianceMS = 2;
	ejectionVelocity = 0.2;
	velocityVariance = 0;
	ejectionOffset   = 0.09;
	thetaMin         = 0;
	thetaMax         = 180;
	phiReferenceVel  = 0;
	phiVariance      = 360;
	overrideAdvance  = false;
	particles        = "ToolWandParticleB";
};

datablock ProjectileData(ToolWandProjectile : wandProjectile)
{
	explosion = ToolWandExplosion;
};

datablock ShapeBaseImageData(ToolWandImage : wandImage)
{
	armReady        = true;
	doColorShift    = true;
	colorShiftColor = "0 0 1 1";
	projectile      = ToolWandProjectile;
	stateEmitter[1] = ToolWandEmitterA;
	stateEmitter[3] = ToolWandEmitterB;
};

$TWDir="Add-ons/ToolWand/";

function serverCmdToolWand(%client)
{
	%player = %client.player;
	if(isObject(%player))
	{
		%player.updateArm(ToolWandImage);
		%player.mountImage(ToolWandImage,0);
		PrintTWModeInfo(%client);
	}
}

function ToolWandImage::onPreFire(%this,%obj,%slot)
{
	%obj.playthread(2, armattack);
}

function ToolWandImage::onStopFire(%this, %obj, %slot)
{
	%obj.playthread(2, root);	
}

function GameConnection::getAdminLevel(%this)
{
	%al=0;
	if(%this.isAdmin) {%al=1;}
	if(%this.isSuperAdmin) {%al=2;}
	return %al;
}

function ToolWandProjectile::onCollision(%this, %obj, %col)
{
	%c=%obj.client;
	if(%c.TWMode$="") {return;}
	if($TWModeAccess[%c.TWMode]<=%obj.client.getAdminLevel())
	{
		call($TWModeF[%obj.client.TWMode],%obj,%col);
	}
	else if($TWModeAccess[%c.TWMode]==1)
	{
		centerprint(%c, "\c0Only admins can use this tool.", 2, 2);
	}
	else if($TWModeAccess[%c.TWMode]==2)
	{
		centerprint(%c, "\c0Only super admins can use this tool.", 2, 2);
	}
	else
	{
		centerprint(%c, "\c0This tool is forbidden to use.", 2, 2);
	}
}

package ToolWand
{
	function serverCmdLight(%c)
	{
		if(%c.player.getMountedImage(0)$=nameToID(ToolWandImage))
		{
			%a=%c.TWMode;
			while(1)
			{
				%c.TWMode++;
				if(%c.TWMode>=$TW)
				{
					%c.TWMode=0;
				}
				if($TWModeAccess[%c.TWMode]<=%c.getAdminLevel()) {PrintTWModeInfo(%c);return;}
				if(%a==%c.TWMode) {%c.TWMode="";messageClient(%c,'',"\c0No tools for you found.");return;}
			}
		}
		else
		{
			Parent::serverCmdLight(%c);
		}
	}
};ActivatePackage(ToolWand);

$TWT[0]="<color:339933>(P)";
$TWT[1]="<color:333399>(A)";
$TWT[2]="<color:995533>(S)";
$TWT[3]="<color:993333>(F)";

function PrintTWModeInfo(%c)
{
	if(%c.TWMode$="") {return;}
	if(!isObject(%c.player)) {return;}
	if(%c.player.getMountedImage(0)!$=nameToID(ToolWandImage)) {return;}
	bottomPrint(%c,"<font:arial:14><just:left>"@$TWT[$TWModeAccess[%c.TWMode]]@"<font:impact:17><just:center>"@"\c1[ \c2"@$TWMode[%c.TWMode]@"\c1 ]<just:left><font:arial:17>" NL "\c6"@$TWModeInfo[%c.TWMode],1,3);
	schedule(900,0,PrintTWModeInfo,%c);
}


//Save-Load

function getTWModebyname(%name)
{
	for(%i=0;%i<$TW;%i++)
	{
		if($TWMode[%i]$=%name)
		{
			return %i;
		}
	}
	return "";
}

function loadTWmodesInfo()
{
	%FO=new FileObject();
	%FO.openForRead($TWDir@"ToolWandInfo.txt");
	while(!%FO.isEOF())
	{
		evalTWmodeInfo(%FO.readline());
	}
	%FO.close();
	%FO.delete();
}

function evalTWmodeInfo(%line)
{
	%TWM=getTWModebyname(getField(%line,0));
	if(%TWM$="") {return;}
	$TWModeAccess[%TWM]=getField(%line,1);
}

function saveTWModesInfo()
{
	%FO=new FileObject();
	%FO.openForWrite($TWDir@"ToolWandInfo.txt");
	for(%i=0;%i<$TW;%i++)
	{
		%FO.writeline($TWMode[%i] TAB $TWModeAccess[%i]);
	}
	%FO.close();
	%FO.delete();
}

function serverCmdGetTWList(%c)
{
	if(!%c.isSuperAdmin) {return;}
	for(%i=0;%i<$TW;%i++)
	{
		commandToClient(%c,'TWlistAdd',$TWMode[%i],$TWModeAccess[%i]);
	}
}

function serverCmdSetTW(%c,%id)
{
	if(!%c.isSuperAdmin) {return;}
	$TWModeAccess[%id]++;
	if($TWModeAccess[%id]>3)
	{
		$TWModeAccess[%id]=0;
	}
	saveTWModesInfo();
}

function TW_LoadTools()
{
	%f=findfirstfile($TWDir @ "Tools/*.e");
	while(%f!$="")
	{
		echo("Loading Tool : "@%f);
		exec(%f);
		echo("Done");
		%f=findnextfile($TWDir @ "Tools/*.e");
	}
}

$TW=0;
TW_LoadTools();
loadTWmodesInfo();

%f=findFirstFile("Add-Ons/*.cs");
while(fileBase(%f) !$= "tool_toolwand"){%f=findNextFile("Add-Ons/*.cs");}