//Variables you can set:
$Pref::Duplicator::AdminOnly = false;                 //> Can only admin use the Duplicator?
$Pref::Duplicator::MaxBricks = -1;                    //> Max Brick selectable by anyone with the duplicator (-1 is Infinite)
$Pref::Duplicator::MaxBricksForNonAdmin = 500;        //> Max Bricks a Non-Admin using the Duplicator can select (-1 is Infinite)
$Pref::Duplicator::NonAdminTimeout = 40;              //> Ammount of seconds before a Non-Admin can duplicate again.

package Duplicator
{
	function determineBrightestColor()
	{
		%closest = 1;
		%color = "1 1 1 1";
		for(%i=0;%i<64;%i++)
		{
			%colori = getColorIDTable(%i);
			for(%c=0;%c<4;%c++)
			{
				%coloradd += getWord(%color, %c) - getWord(%colori, %c);
			}
			%coloradd /= 4;
			if(%coloradd < %closest)
			{
				%closest = %coloradd;
				%closestID = %i;
			}
		}
		$HilightColor = %closestID;
	}
   
	function unSelectDup()
	{
		for(%a=0;%a<mainBrickGroup.getCount();%a++)
		{
			%brickGroup = mainBrickGroup.getObject(%a);
			for(%b=0;%b<%brickGroup.getCount();%b++)
			{
				%brick = %brickGroup.getObject(%b);
				%brick.duplicated = 0;
			}
		}
	}

	function fxDtsBrick::findOwner(%this)
	{
		for(%i=0;%i<ClientGroup.getCount();%i++)
		{
			%cl = ClientGroup.getObject(%i);
			if(isObject(%cl.player.tempBrick))
				if(%cl.player.tempBrick $= %this)
					return %cl;
		}
		return 0;
	}

	function GameConnection::Duplicate(%this, %startbrick)
	{
		if($Sim::Time < %this.lastDuplicateTime+3)
			return;
		%this.lastDuplicateTime = $Sim::Time;
		%this.maxedOutDupe = 0;
		%objname = "Duplicator_"@%this.bl_id;
		if(isObject(%objname))
			%objname.delete();
		%dupeObj = new ScriptObject(%objname)
		{
			class = "DuplicatorObjSO";
			bricks = 0;
		};
		%startbrick.Duplicate(%this);
		for(%i=0;%i<%startbrick.getNumUpBricks();%i++)
		{
			%brick = %startbrick.getUpBrick(%i);
			%brick.DuplicateBrick(%this);
		}
		%s = %dupeObj.bricks > 1 ? "s" : "";
		unSelectDup();
		if(isObject(%this.player.tempBrick))
			%this.player.tempBrick.delete();
		if(%this.maxedOutDupe)
		{
			messageClient(%this,'MsgPlantError_Limit');
			if(%this.dupeMaxout == 2)
				centerPrint(%this, "You have reached the Limit of "@$Pref::Duplicator::MaxBricks@" Duplicated Bricks",3,2);
			else
				centerPrint(%this, "You have reached the Limit of "@$Pref::Duplicator::MaxBricksForNonAdmin@" Duplicated Bricks",3,2);
		}
		bottomPrint(%this, "<color:FF8888>Duplication<color:FF0000> Mode\n"@%dupeObj.bricks@" Brick"@%s@" Selected",0,3);
		%this.player.tempBrick = new fxDtsBrick()
		{
			position = %startbrick.position;
			rotation = %startbrick.rotation;
			angleID = %startbrick.angleID;
			colorID = %startbrick.oldcolorID;
			colorfxID = %startbrick.oldcolorfxID;
			shapefxID = %startbrick.shapeFXID;
			printID = %startbrick.printID;
			dataBlock = %startbrick.dataBlock;
			isBasePlate = %startbrick.isBasePlate();
			dupStartBrick = 1;
		};
	}

	function fxDtsBrick::DuplicateBrick(%this,%client)
	{
		%dupObj = "Duplicator_" @ %client.bl_id;
		if(!allowedToDuplicateBrick(%client, %dupObj.bricks))
			return;
		if(%this.duplicated)
			return;
		%this.Duplicate(%client);
		for(%i=0;%i<%this.getNumUpBricks();%i++)
		{
			%brick = %this.getUpBrick(%i);
			%brick.DuplicateBrick(%client);
		}
		for(%i=0;%i<%this.getNumDownBricks();%i++)
		{
			%brick = %this.getDownBrick(%i);
			%brick.DuplicateBrick(%client);
		}
	}

	function IDtoRot(%id)
	{
		switch(%id)
		{
			case 0:
				%trans = %trans SPC " 1 0 0 0";
			case 1:
				%trans = %trans SPC " 0 0 1" SPC $piOver2;
			case 2:
				%trans = %trans SPC " 0 0 1" SPC $pi;
			case 3:
				%trans = %trans SPC " 0 0 -1" SPC $piOver2;
		}
		return %trans;
	}

	function allowedToDuplicateBrick(%client,%count)
	{
		if($Pref::Duplicator::MaxBricks >= 1)
			if(%count >= $Pref::Duplicator::MaxBricks)
			{
				%client.maxedOutDupe = 1;
				%client.dupeMaxout = 2;
				return 0;
			}
            
		if(!%client.isAdmin && !%client.isSuperAdmin)
		{
			if($Pref::Duplicator::MaxBricksForNonAdmin >= 1)
			{
				if(%count >= $Pref::Duplicator::MaxBricksForNonAdmin)
				{
					%client.maxedOutDupe = 1;
					%client.dupeMaxout = 1;
					return 0;
				}
			}
		}
		return 1;
	}

	function fxDtsBrick::Duplicate(%this,%client)
	{
		%dup = "Duplicator_"@%client.bl_id;
		if(!allowedToDuplicateBrick(%client, %dup.bricks))
			return;
		if(%dup.bricks != 0)
			%pos = vectorsub(%this.position, getWords(%dup.brick0, 1, 3));
		else
			%pos = %this.position;
		%brickstr = %this.dataBlock.getID() SPC %pos SPC %this.angleID SPC %this.colorID SPC %this.printID SPC %this.colorFXID SPC %this.shapeFxID SPC %this.isBasePlate() SPC %this.stackBL_ID;
		%dup.brick[%dup.bricks] = %brickstr;
		%totalData = -1;
		if(isObject(%this.vehicleDataBlock))
			%dup.brickdata[%dup.bricks,%totalData++] = "VEHICLE "@%this.vehicleDataBlock SPC %this.recolorVehicle;
		if(isObject(%this.emitter))
			%dup.brickdata[%dup.bricks,%totalData++] = "EMITTER "@%this.emitter.emitter.getID() SPC %this.emitterDirection;
		if(isObject(%this.light))
			%dup.brickdata[%dup.bricks,%totalData++] = "LIGHT "@%this.light.dataBlock;
		if(isObject(%this.Item))
			%dup.brickdata[%dup.bricks,%totalData++] = "ITEM "@%this.item.dataBlock SPC %this.itemPosition SPC %this.itemDirection SPC %this.itemRespawnTime;
		%dup.bricks++;
		if(!%this.beingDuped)
		{
			%oldColor = %this.colorId;
			%oldColorFX = %this.colorFX;
		}
		else
		{
			cancel(%this.dupeRes1);
			cancel(%this.dupeRes2);
			%oldColor = %this.oldColorId;
			%oldColorFX = %this.oldColorFX;
		}
		%this.oldColorID = %oldColor;
		%this.oldColorFX = %oldColorFX;
		if($HilightColor $= "")
			determineBrightestColor();
		%this.setColor($HilightColor);
		%this.setColorFX(3);
		%this.dupeRes1 = %this.schedule(2000, "setColor", %oldColor);
		%this.dupeRes2 = %this.schedule(2000, "setColorFX", %oldColorFX);
		%this.beingDuped = 1;
		%this.schedule(2001, "unsetDupe");
		%this.duplicated = 1;
	}
	
	function fxDTSBrick::unsetDupe(%this)
	{
		%this.beingDuped = "";
	}

	function GameConnection::PlayDup(%this, %startbrick)
	{
		%totalBricksPlaced = 1;
		%totalBricks = 1;
		%dupobj = "Duplicator_" @ %this.bl_id;
		%rotchange = %startbrick.angleID - getWord(%dupobj.brick0, 4);
		%rotchange2 = %rotchange;
		if(%rotchange2 != 0)
		{
			if(%rotchange2 < 0)
				%rotchange2 = 4 + %rotchange2;
		}
		for(%i=1;%i<%dupobj.bricks;%i++)
		{
			%totalBricks++;
			%brickstr = %dupobj.brick[%i];
			%position = getwords(%brickstr, 1, 3);
			if(%rotchange2 > 0)
			{
				%posx = 0;
				%posy = 0;
				if(%rotchange2 == 1)
				{
					%posy -= getWord(%position, 0);
					%posx = getWord(%position, 1);
				}
				else if(%rotchange2 == 2)
				{
					%posy -= getWord(%position, 1);
					%posx -= getWord(%position, 0);
				}
				else if(%rotchange2 == 3)
				{
					%posy = getWord(%position, 0);
					%posx -= getWord(%position, 1);
				}
				%position = %posx SPC %posy SPC getWord(%position, 2);
			}
			%position = vectoradd(%position, %startbrick.getposition());
			%rot = getWord(%brickstr, 4);
			%rot += %rotchange;
			if(%rot < 0)
				%rot += 4;
			else if(%rot > 3)
				%rot -= 4;
			%rotation = IDtoRot(%rot);
			%colorid = getWord(%brickstr, 5);
			%printID = getWord(%brickstr, 6);
			%colorFXID = getWord(%brickstr, 7);
			%shapeFXID = getWord(%brickstr, 8);
			%dataBlock = getWord(%brickstr, 0);
			%baseplate = getWord(%brickstr, 9);
			%brick = new fxDtsBrick()
			{
				angleID = %rot;
				colorID = %colorID;
				colorFXID = %colorFXID;
				shapeFXID = %shapeFXID;
				printID = %printID;
				dataBlock = %dataBlock;
				isBaseplate = %baseplate;
				isPlanted = 1;
			};
			if($Server::LAN)
				BrickGroup_LAN.add(%brick);
			else
				%this.brickGroup.add(%brick);
			%brick.stackBL_ID = getWord(%brickstr,10);
			%brick.setTrusted(1);
			%brick.setTransform(%position SPC %rotation);
			%err = %brick.plant();
			%totalBricksPlaced++;
			if(%err == 1 || %err == 2 || %err == 3 || %err == 4 || %err == 5)
			{
				%brick.delete();
				%totalBricksPlaced--;
			}
			if(%brick.isBasePlate())
				%this.undoStack.push(%brick@"\tPLANT");
			if(isObject(%brick))
			{
				%brick.applySpecialAttributes(%dupobj,%i);
			}
		}
		centerPrint(%this,"<color:00FF00>"@%totalBricksPlaced@"/"@%totalBricks@" Duplicated Successfully",3,2);
	}

	function fxDTSBrick::applySpecialAttributes(%brick,%dupeObj,%lineid)
	{
		%dataLine = %dupeObj.brickData[%lineid,0];
		while(%dataLine !$= "")
		{
			if(firstWord(%dataLine) $= "VEHICLE")
			{
				%data = getWord(%dataLine,1);
				if(($Pref::Server::MaxPlayerVehicles_Total > $Server::numPlayerVehicles && %data.getClassName() $= "PlayerData") || $Pref::Server::MaxPhysVehicles_Total > $Server::numPhysVehicles)
					if(($Pref::Server::MaxPlayerVehicles_PerPlayer > %brick.getGroup().numPlayerVehicles && %data.getClassName() $= "PlayerData") || $Pref::Server::MaxPhysVehicles_PerPlayer > %brick.getGroup().numPhysVehicles)
					{
						%brick.setVehicle(getWord(%dataLine,1));
						%brick.setReColorVehicle(getWord(%dataLine,2));
					}
			}
			else if(firstWord(%dataLine) $= "EMITTER")
			{
				if($Pref::Server::MaxEmitters_Total > $Server::numEmitters)
					if($Pref::Server::MaxEmitters_PerPlayer > %brick.getGroup().numEmitters)
					{
						%brick.setEmitter(getWord(%dataLine,1));
						%brick.setEmitterDirection(getWord(%dataLine,2));
					}
			}
			else if(firstWord(%dataLine) $= "ITEM")
			{
				%brick.setItem(getWord(%dataLine,1));
				%brick.setItemPosition(getWord(%dataLine,2));
				%brick.setItemDirection(getWord(%dataLine,3));
				%brick.setItemRespawnTime(getWord(%dataLine,4));
			}
			else if(firstWord(%dataLine) $= "LIGHT")
			{
				if($Pref::Server::MaxLights_Total > $Server::numLights)
					if($Pref::Server::MaxLights_PerPlayer > %brick.getGroup().numLights)
					{
						%brick.setLight(getWord(%dataLine,1));
					}
			}
			%dataLine = %dupeObj.brickData[%lineid,%dataCount++];
		}
	}

	function serverCmdDuplicator(%client)
	{
		%player = %client.player;
		if($Pref::Duplicator::AdminOnly && (!%client.isAdmin && !%client.isSuperAdmin))
			return;
		if(isObject(%player))
		{
			%player.updateArm(duplicatorImage);
			%player.mountImage(duplicatorImage,0);
		}
	}
	
	function fxDtsBrickData::onRemove(%this, %obj)
	{
		if(!%obj.isPlanted() && %obj.dupStartBrick)
		{
			%client = %obj.findOwner();
			if(isObject(%client))
				bottomPrint(%client,"<color:FF8888>Normal <color:FF0000>Mode",2,2);
		}
		Parent::onRemove(%this, %obj);
	}

	function fxDtsBrick::setDataBlock(%this, %dataBlock)
	{
		if(%this.dupStartBrick && %dataBlock != %this.getDataBlock())
		{
			%client = %this.findOwner();
			if(isObject(%client))
				bottomPrint(%client,"<color:FF8888>Normal <color:FF0000>Mode",2,2);
			%this.dupStartBrick = "";
		}
		Parent::setDataBlock(%this, %dataBlock);
	}

	function serverCmdPlantBrick(%client)
	{
		if(isObject(%client.player.tempBrick))
		{
			if(%client.player.tempBrick.dupStartBrick)
			{
				%tempBrick = %client.player.tempBrick;
				if(%tempBrick.dupStartBrick)
				{
					%newBrick = new fxDtsBrick()
					{
						position = %tempBrick.position;
						rotation = %tempBrick.rotation;
						angleID = %tempBrick.angleID;
						colorID = %tempBrick.colorID;
						colorFXID = %tempBrick.colorFXID;
						shapeFXID = %tempBrick.shapeFXID;
						printID = %tempBrick.printID;
						dataBlock = %tempBrick.dataBlock;
						isBasePlate = %tempBrick.isBasePlate();
						isPlanted = 1;
					};
					if($Server::LAN)
						BrickGroup_LAN.add(%newBrick);
					else
						%client.brickGroup.add(%newBrick);
					%newBrick.setTrusted(1);
					%err = %newBrick.plant();
					if(%err == 1 || %err == 2 || %err == 3 || %err == 4 || %err == 5)
					{
						if(%err == 1)
							messageClient(%client, 'MsgPlantError_Overlap');
						else if(%err == 2)
							messageClient(%client, 'MsgPlantError_Float');
						else if(%err == 3)
							messageClient(%client, 'MsgPlantError_Stuck');
						else if(%err == 4)
							messageClient(%client, 'MsgPlantError_Unstable');
						else if(%err == 5)
							messageClient(%client, 'MsgPlantError_Buried');
						%newBrick.delete();
						return;
					}
					%dupeObj = "Duplicator_"@%client.bl_id;
					%newBrick.applySpecialAttributes(%dupeObj,0);
					if($Pref::Duplicator::NonAdminTimeout >= 1 && !%client.isAdmin && !%client.isSuperAdmin)
					{
						if($Sim::Time < %client.lastDuplicationPlace+$Pref::Duplicator::NonAdminTimeout && %client.lastDuplicationPlace !$= "")
						{
							%newBrick.delete();
							%secsLeft = mCeil((%client.lastDuplicationPlace+$Pref::Duplicator::NonAdminTimeout) - $Sim::Time);
							centerPrint(%client,"You can only place a Duplicate every "@$Pref::Duplicator::NonAdminTimeout@" Seconds\n<color:FF8888>"@%secsLeft@" Seconds Left",2,3);
							messageClient(%client,'MsgPlantError_Flood');
							return;
						}
						%client.lastDuplicationPlace = $Sim::Time;
					}
					%client.undoStack.push(%newBrick@"\tPLANT");
					%client.PlayDup(%newBrick);
				}
			}
			else
				Parent::serverCmdPlantBrick(%client);
		}
	}
	
	function GameConnection::OnClientLeaveGame(%this)
	{
		%objname = "Duplicator_"@%this.bl_id;
		if(isObject(%objname))
			%objname.delete();
		Parent::OnClientLeaveGame(%this);
	}
};
ActivatePackage(Duplicator);

datablock ParticleData(DuplicatorExplosionParticle)
{
	dragCoefficient      = 9.99995;
	gravityCoefficient   = -0.002;
	inheritedVelFactor   = 0.19;
	constantAcceleration = 0.0;
	spinRandomMin        = -90;
	spinRandomMax        = 90;
	lifetimeMS           = 500;
	lifetimeVarianceMS   = 300;
	textureName          = "base/data/particles/bubble";
	colors[0]            = "0.2 1.0 0.1 0.6";
	colors[1]            = "0.1 1.0 0.1 0.6";
	colors[2]            = "0.7 1.0 0.7 0.6";
	colors[3]            = "0.8 1.0 0.8 0.6";
	sizes[0]             = 0.3;
	sizes[1]             = 0.4;
	sizes[2]             = 0.8;
	sizes[3]             = 1.2;
	times[0]             = 0;
	times[1]             = 0.8;
	times[2]             = 1;
	times[3]             = 2;
};

datablock ParticleEmitterData(DuplicatorExplosionEmitter)
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
	particles        = "DuplicatorExplosionParticle";
};

datablock ExplosionData(duplicatorExplosion : wandExplosion)
{
	lifetimeMS      = 180;
	emitter[0]      = duplicatorExplosionEmitter;
	lightStartColor = "0 1 0 1";
	lightEndColor   = "0 0 0 0";
};

datablock ParticleData(DuplicatorParticleA)
{
	dragCoefficient      = 0;
	gravityCoefficient   = -0.5;
	inheritedVelFactor   = 0;
	constantAcceleration = 0.0;
	spinRandomMin        = -90;
	spinRandomMax        = 90;
	lifetimeMS           = 600;
	lifetimeVarianceMS   = 200;
	textureName          = "base/data/particles/bubble";
	colors[0]            = "0.2 1.0 0.1 0.9";
	colors[1]            = "0.1 1.0 0.1 0.9";
	colors[2]            = "0.7 1.0 0.7 0.9";
	colors[3]            = "0.8 1.0 0.8 0.9";
	sizes[0]             = 0.1;
	sizes[1]             = 0.2;
	sizes[2]             = 0.4;
	sizes[3]             = 1;
	times[0]             = 0;
	times[1]             = 0.8;
	times[2]             = 1;
	times[3]             = 2;
};

datablock ParticleEmitterData(DuplicatorEmitterA)
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
	particles        = "DuplicatorParticleA";
};

datablock ParticleData(DuplicatorParticleB)
{
	dragCoefficient      = 0;
	gravityCoefficient   = -0.2;
	inheritedVelFactor   = 0.5;
	constantAcceleration = 0.0;
	spinRandomMin        = -90;
	spinRandomMax        = 90;
	lifetimeMS           = 600;
	lifetimeVarianceMS   = 200;
	textureName          = "base/data/particles/bubble";
	colors[0]            = "0.2 1.0 0.1 0.9";
	colors[1]            = "0.1 1.0 0.1 0.9";
	colors[2]            = "0.7 1.0 0.7 0.9";
	colors[3]            = "0.8 1.0 0.8 0.9";
	sizes[0]             = 0.1;
	sizes[1]             = 0.2;
	sizes[2]             = 0.4;
	sizes[3]             = 1;
	times[0]             = 0;
	times[1]             = 0.8;
	times[2]             = 1;
	times[3]             = 2;
};

datablock ParticleEmitterData(DuplicatorEmitterB)
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
	particles        = "DuplicatorParticleB";
};

datablock ProjectileData(duplicatorProjectile : wandProjectile)
{
	explosion = duplicatorExplosion;
};

datablock ShapeBaseImageData(duplicatorImage : wandImage)
{
	armReady        = true;
	doColorShift    = true;
	colorShiftColor = "0 1 0 1";
	projectile      = duplicatorProjectile;
	stateEmitter[1] = duplicatorEmitterA;
	stateEmitter[3] = duplicatorEmitterB;
};

function duplicatorImage::onPreFire(%this,%obj,%slot)
{
	%obj.playthread(2, armattack);
}

function duplicatorImage::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}

function duplicatorProjectile::onCollision(%this, %obj, %col)
{
	if(%col.getClassName() $= "fxDtsBrick")
	{
		if(getTrustLevel(%obj, %col) == 2)
			%obj.client.Duplicate(%col);
		else
			centerprint(%obj.client, "\c0" @ %col.getGroup().name @ " does not trust you enough to do that.", 2, 2);
	}
}