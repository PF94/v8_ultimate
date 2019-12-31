//pill.cs

datablock ParticleData(HealParticle)
{
   dragCoefficient      = 5.0;
   gravityCoefficient   = -0.2;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 1000;
   lifetimeVarianceMS   = 500;
   useInvAlpha          = false;
   textureName          = "./Particles/heal";
   colors[0]     = "1.0 1.0 1.0 1";
   colors[1]     = "1.0 1.0 1.0 1";
   colors[2]     = "0.0 0.0 0.0 0";
   sizes[0]      = 0.4;
   sizes[1]      = 0.6;
   sizes[2]      = 0.4;
   times[0]      = 0.0;
   times[1]      = 0.2;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(HealEmitter)
{
   ejectionPeriodMS = 35;
   periodVarianceMS = 0;
   ejectionVelocity = 0.5;
   ejectionOffset   = 1.0;
   velocityVariance = 0.49;
   thetaMin         = 0;
   thetaMax         = 120;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "HealParticle";

   uiName = "Emote - Heal";
};

datablock ShapeBaseImageData(HealImage)
{
	shapeFile = "base/data/shapes/empty.dts";
	emap = false;

	mountPoint = $HeadSlot;

	stateName[0]					= "Ready";
	stateTransitionOnTimeout[0]		= "FireA";
	stateTimeoutValue[0]			= 0.01;

	stateName[1]					= "FireA";
	stateTransitionOnTimeout[1]		= "Done";
	stateWaitForTimeout[1]			= True;
	stateTimeoutValue[1]			= 0.350;
	stateEmitter[1]					= HealEmitter;
	stateEmitterTime[1]				= 0.350;

	stateName[2]					= "Done";
	stateScript[2]					= "onDone";
};
function HealImage::onDone(%this,%obj,%slot)
{
	%obj.unMountImage(%slot);
}

datablock ItemData(PillItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./shapes/pill.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Pill";
	iconName = "./ItemIcons/pill";
	doColorShift = false;

	 // Dynamic properties defined by the scripts
	image = pillImage;
	canDrop = true;
};

datablock ShapeBaseImageData(pillImage)
{
   // Basic Item properties
   shapeFile = "./shapes/pill.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0.09 -0.07 -0.2";
   eyeOffset = 0; //"0.7 1.2 -0.5";
   rotation = eulerToMatrix( "0 0 0" );

   className = "WeaponImage";
   item = PillItem;

   //raise your arm up or not
   armReady = false;

   doColorShift = false;

   // Initial start up state
	stateName[0]                     = "Ready";
	stateTransitionOnTriggerDown[0]  = "Fire";
	stateAllowImageChange[0]         = true;

	stateName[1]                     = "Fire";
	stateTransitionOnTimeout[1]      = "Ready";
	stateAllowImageChange[1]         = true;
      stateScript[1]                   = "onFire";
	stateTimeoutValue[1]		   = 1;
};

function pillImage::onFire(%this,%obj,%slot)
{
	if(!isObject(%obj.client.minigame))
		return;

	for(%i=0;%i<5;%i++)
	{
		%toolDB = %obj.tool[%i];
		if(%toolDB $= %this.item.getID())
		{
			%obj.setDamageLevel(0);
			%obj.emote(HealImage);
			%obj.tool[%i] = 0;
			%obj.weaponCount--;
			messageClient(%obj.client,'MsgItemPickup','',%i,0);
			serverCmdUnUseTool(%obj.client);
			break;
		}
	}
}

package PillPackage
{
	function Armor::onCollision(%this, %obj, %col, %thing, %other)
	{
		if(%col.dataBlock $= "PillItem" && isObject(%obj.client.minigame) && %col.pickupNow !$= 1)
		{
			if(!isObject(%col.spawnbrick))	
			{
				if((%obj.getDamageLevel() >= 1) && %obj.client.minigame $= %col.minigame)
				{
					%obj.setDamageLevel(0);
					%obj.emote(HealImage);
					%col.delete();
				}
				else
					%col.pickupNow = 1;
				return;
			}
		}
		Parent::onCollision(%this, %obj, %col, %thing, %others);
	}
};
activatePackage(PillPackage);

datablock DecalData(PillIcon)
{
   textureName = "Add-Ons/ItemIcons/pill";
};