//ID getting
$TWMode[$TW]="ID getter";
$TWModeF[$TW]="TW_GetID";
$TWModeInfo[$TW]="This tool will get the ID of the object hit.";
$TWModeAccess[$TW]=0;
$TW++;

function TW_GetID(%obj,%col)
{
	messageClient(%obj.client,'',"\c2This objects ID is: "@%col);
}

//Destructo wand
$TWMode[$TW]="Destructo";
$TWModeF[$TW]="TW_DW";
$TWModeInfo[$TW]="This tool will destroy bricks.";
$TWModeAccess[$TW]=2;
$TW++;

function TW_DW(%obj,%col)
{
	if(%col.getClassName()$="fxDtsBrick")
	{
		%col.killbrick();
	}
}

//Brick deleter
$TWMode[$TW]="Brick Deleter";
$TWModeF[$TW]="TW_BDel";
$TWModeInfo[$TW]="This tool will delete bricks.";
$TWModeAccess[$TW]=0;
$TW++;

function TW_BDel(%obj,%col)
{
	if(%col.getClassName()$="fxDtsBrick")
	{
		if(getTrustLevel(%obj,%col)==2)
			%col.delete();
		else
			centerprint(%obj.client, "\c0" @ %col.getGroup().name @ " does not trust you enough to do that.", 2, 2);
	}
}

//Impulse
$TWMode[$TW]="Impulse wand";
$TWModeF[$TW]="TW_Imp";
$TWModeInfo[$TW]="This tool will impulse players." NL "\c2To set your impulse force, /impforce ###.";
$TWModeAccess[$TW]=1;
$TW++;

function TW_Imp(%obj,%col)
{
	%obj.client.impForce=%obj.client.impForce*1;
	if(%col.getClassName()$="Player"||%col.getClassName()$="AIPlayer"||%col.getClassName()$="Vehicle"||%col.getClassName()$="WheeledVehicle"||%col.getClassName()$="FlyingVehicle")
	{
		%col.setVelocity(vectorAdd(%col.getVelocity(),vectorScale(%obj.client.player.getEyeVector(),%obj.client.impforce*1)));
	}
}

function servercmdImpForce(%c,%f)
{
	%f=%f*1;
	if(%f>20000) {%f=20000;}
	if(%f<1) {%f=1;}
	%c.impForce=%f;
	messageClient(%c,'',"\c2Your impulse force has been set to: \c3"@%f);
}

//Brick Decollisionner
$TWMode[$TW]="Brick decollisionner";
$TWModeF[$TW]="TW_BDe";
$TWModeInfo[$TW]="This tool will decollision bricks.";
$TWModeAccess[$TW]=0;
$TW++;

function TW_BDe(%obj,%col)
{
	if(%col.getClassName()$="fxDtsBrick")
	{
		if(getTrustLevel(%obj,%col)!=2) {centerprint(%obj.client, "\c0" @ %col.getGroup().name @ " does not trust you enough to do that.", 2, 2);return;}
		if(%col.iscolliding)
		{
			%col.setColliding(1);
			%col.iscolliding="";
		}
		else
		{
			%col.setColliding(0);
			%col.iscolliding=1;
		}
	}
}

//Wrench
$TWMode[$TW]="Wrench";
$TWModeF[$TW]="TW_Wren";
$TWModeInfo[$TW]="Just your typical wrench.";
$TWModeAccess[$TW]=0;
$TW++;

function TW_Wren(%obj,%col)
{
	if(%col.getClassName()$="fxDtsBrick")
	{
		wrenchprojectile.onCollision(%obj,%col);
	}
}

//Mass destructo
$TWMode[$TW]="Mass destructo";
$TWModeF[$TW]="TW_MD";
$TWModeInfo[$TW]="Destructo wand, but alot more power." NL "\c2Set the radius with /Destructorad ###.";
$TWModeAccess[$TW]=2;
$TW++;

function TW_MD(%obj,%col)
{
	%posi=%obj.getPosition();
	if(%posi$="") {return;}
	%obj.client.destructoradius=%obj.client.destructoradius*1;
	initContainerRadiusSearch(%posi,%obj.client.destructoradius,$TypeMasks::FxBrickObjectType);
	while(1==1)
	{
		%search=ContainerSearchNext();
		if(!isObject(%search)) {break;}
		%search.killBrick();
	}
}

function serverCmdDestructoRad(%c,%rad)
{
	%rad=%rad*1;
	if(%rad>1000) {%rad=1000;}
	if(%rad<1) {%rad=1;}
	%c.destructoradius=%rad;
	messageClient(%c,'',"\c2Your destructo radius has been set to: \c3"@%rad);
}

//Mass brick deleter
$TWMode[$TW]="Mass brick deleter";
$TWModeF[$TW]="TW_MBD";
$TWModeInfo[$TW]="Mass brick deleter" NL "\c2Set the radius with /DeleteRad ###.";
$TWModeAccess[$TW]=2;
$TW++;

function TW_MBD(%obj,%col)
{
	%posi=%obj.getPosition();
	if(%posi$="") {return;}
	%obj.client.destructoradius=%obj.client.destructoradius*1;
	initContainerRadiusSearch(%posi,%obj.client.delrad,$TypeMasks::FxBrickObjectType);
	while(1==1)
	{
		%search=ContainerSearchNext();
		if(!isObject(%search)) {break;}
		%search.delete();
	}
}

function serverCmdDeleteRad(%c,%rad)
{
	%rad=%rad*1;
	if(%rad>1000) {%rad=1000;}
	if(%rad<1) {%rad=1;}
	%c.delrad=%rad;
	messageClient(%c,'',"\c2Your deleting radius has been set to: \c3"@%rad);
}

//Scale Wand
$TWMode[$TW]="Scale Wand";
$TWModeF[$TW]="TW_SW";
$TWModeInfo[$TW]="Set the scale of Players and Bots with this wand." NL "\c2Set the scale with /wandscale X Y Z.";
$TWModeAccess[$TW]=2;
$TW++;

function TW_SW(%obj,%col)
{
	if(%col.getClassName()$="Player"||%col.getClassName()$="AIPlayer")
	{
		%col.setscale(%obj.client.wandscale);
	}
}

function serverCmdWandScale(%c,%x,%y,%z)
{
	%c.wandScale=%x SPC %y SPC %z;
	messageClient(%c,'',"\c2Your wand scale has been set to: \c3"@%x SPC %y SPC %z);
}

//PrintFill Wand
$TWMode[$TW]="Fill Printer";
$TWModeF[$TW]="TW_FP";
$TWModeInfo[$TW]="This is like the fillcan, but with prints.";
$TWModeAccess[$TW]=0;
$TW++;

function TW_FP(%obj,%col)
{
	if(%col.getClassName()$="fxDtsBrick")
	{
		if(getTrustLevel(%obj,%col)!=2) {centerprint(%obj.client, "\c0" @ %col.getGroup().name @ " does not trust you enough to do that.", 2, 2);return;}
		%obj.client.lastprintedBrick=%col;
		%obj.client.isfillprinting=1;
		PrintGunImage.onFire(%obj.client.player);
	}
}

package FillPrinting
{
	function serverCmdSetPrint(%c,%printID)
	{
		if(%c.isfillprinting)
		{
			if(%c.lastprintedbrick.getPrintID()==%printID) {return;}
			fillPrintBrick(%c,%c.lastprintedBrick,%printID,%c.lastPrintedBrick.getPrintID());
			%c.isfillprinting=0;
		}
		else
		{
			Parent::serverCmdSetPrint(%c,%printID);
		}
	}
};
ActivatePackage(fillprinting);

function fillPrintBrick(%c,%brick,%printID,%LPID)
{
	if(getTrustLevel(%c,%brick)!=2) {return;}
	
	
	%brick.setPrint(%printID);
	
	%data=%brick.getDatablock();
	
	%rot=getWord(%obj.rotation,3);
	
	if(%rot==0 || %rot==180)
	{
		%sizex=%data.bricksizex;
		%sizey=%data.bricksizey;
	}
	else
	{
		%sizex=%data.bricksizey;
		%sizey=%data.bricksizex;
	}

	%cube=(%sizex*0.5+0.6) SPC (%sizey*0.5+0.6) SPC (%data.bricksizez*0.2+0.3);
	
	InitContainerBoxSearch(%brick.getPosition(),%cube,$TypeMasks::FxBrickObjectType);
	
	%list=0;
	
	while(1)
	{
		%obj=containerSearchNext();
		if(!isObject(%obj)) {break;}
		if(%obj.getPrintID()!=%LPID) {continue;}
		if(%obj.getPrintID()==%PrintID) {continue;}
		%list[%list]=%obj;
		%list++;
	}
	
	for(%i=0;%i<%list;%i++)
	{
		fillPrintBrick(%c,%list[%i],%printID,%LPID);
	}
}

