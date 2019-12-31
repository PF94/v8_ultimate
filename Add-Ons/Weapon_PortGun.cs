//-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

$TrustLevel::PortalUse = 1; 		//Trust for using other people's portals
$TrustLevel::PortalMakeUnder = 2;	//Trust for placing portals under people
$TrustLevel::PortalMakeBrick = 1; 	//Trust for placing portals on people's bricks

$TrustLevel::PortalMiniUse = 0; 		//Trust for using other people's portals
$TrustLevel::PortalMiniMakeUnder = 2;	//Trust for placing portals under people
$TrustLevel::PortalMiniMakeBrick = 0; 	//Trust for placing portals on people's bricks

//-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

if(!isEventPending($spaceTick))
{
 if(isFile("Add-Ons/Support_SpaceTick.cs.noexec"))
 {
  exec("Add-Ons/Support_SpaceTick.cs.noexec");
 }
 else
 {
  clientcmdMessageBoxOK("Portal Gun","Add-Ons/Support_SpaceTick.cs.noexec not found. Reinstall the mod. Correctly.");
  return;
 }
}

//-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//Trader's Euler/Axis Angle conversion functions. Very useful.

function eulerToAxis(%euler)
{
%euler = VectorScale(%euler,$pi / 180);
%matrix = MatrixCreateFromEuler(%euler);
return getWords(%matrix,3,6);
}

function axisToEuler(%axis)
{
%angleOver2 = getWord(%axis,3) * 0.5;
%angleOver2 = -%angleOver2;
%sinThetaOver2 = mSin(%angleOver2);
%cosThetaOver2 = mCos(%angleOver2);
%q0 = %cosThetaOver2;
%q1 = getWord(%axis,0) * %sinThetaOver2;
%q2 = getWord(%axis,1) * %sinThetaOver2;
%q3 = getWord(%axis,2) * %sinThetaOver2;
%q0q0 = %q0 * %q0;
%q1q2 = %q1 * %q2;
%q0q3 = %q0 * %q3;
%q1q3 = %q1 * %q3;
%q0q2 = %q0 * %q2;
%q2q2 = %q2 * %q2;
%q2q3 = %q2 * %q3;
%q0q1 = %q0 * %q1;
%q3q3 = %q3 * %q3;
%m13 = 2.0 * (%q1q3 - %q0q2);
%m21 = 2.0 * (%q1q2 - %q0q3);
%m22 = 2.0 * %q0q0 - 1.0 + 2.0 * %q2q2;
%m23 = 2.0 * (%q2q3 + %q0q1);
%m33 = 2.0 * %q0q0 - 1.0 + 2.0 * %q3q3;
return mRadToDeg(mAsin(%m23)) SPC mRadToDeg(mAtan(-%m13, %m33)) SPC mRadToDeg(mAtan(-%m21, %m22));
}

//-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

function PGunCtrlSO::addValue(%this,%obj)
{
 %this.value[%this.count] = %obj;
 %this.count++;
}

function PGunCtrlSO::delValue(%this,%index)
{
 if(%index < 0 || %index > %this.count){error("[PGunCtrlSO::delValue] ERROR: Array index out of bounds. " @ %index @ "/" @ %this.count);return;}
 %this.value[%index] = "";
 for(%i = %index+1;%i<%this.count;%i++)
 {
  %this.value[%i-1] = %this.value[%i];
 }
 %this.count--;
}

function PGunCtrlSO::delValueID(%this,%obj)
{
 for(%i=0;%i<%this.count;%i++)
 {
  if(%this.value[%i] $= %obj)
  {
   %this.value[%i] = "";
   for(%j=%i+1;%j<%this.count;%j++)
   {
    %this.value[%j-1] = %this.value[%j];
   }
   %this.count--;
   return;
  }
 }
 warn("[PGunCtrlSO::delValueID()] " @ %obj @ " does not exist in the stack.");
}

function PGunCtrlSO::getValue(%this,%index)
{
 if(%index < 0 || %index > %this.count){error("[PGunCtrlSO::getValue] ERROR: Array index out of bounds. " @ %index @ "/" @ %this.count);return -1;}
 return %this.value[%index];
}

function PGunCtrlSO::dumpStack(%this)
{
 echo("[PGunCtrlSO::dumpStack()]");
 echo("Total Values: " @ %this.count);
 for(%i=0;%i<%this.count;%i++)
 {
  echo(">Value " @ %i @ ": " @ %this.value[%i]);
 }
}

hammerProjectile.noportal = 1;
wrenchProjectile.noportal = 1;
printgunprojectile.noportal = 1;
deathProjectile.noportal = 1;
spawnProjectile.noportal = 1;
wandProjectile.noportal = 1;
adminwandProjectile.noportal = 1;
brickDeployProjectile.noportal = 1;

function PGunCtrlSO::tick(%this,%ticknum)
{
 for(%i=0;%i<%this.count;%i++)
 {
  %obj = %this.getValue(%i);
  if(!isObject(%obj)){%this.delValue(%i);continue;}
  if(!isObject(%obj.client) || !isObject(%obj.client.player) || %obj.client.player.getState() $= "Dead"){%this.delValue(%i);continue;}
  if(!isObject(%obj.source) || %obj.source.getClassname() $= "fxDTSBrick" && %obj.source.isDead()){%obj.delete();continue;}
  initContainerRadiusSearch(%obj.position,18,$TypeMasks::PlayerObjectType | $TypeMasks::VehicleObjectType | $TypeMasks::ProjectileObjectType);
  while(isObject(firstWord(%searchObj = containerSearchNext())))
  {
   %searchObj = firstWord(%searchObj);
   if(%searchObj.getClassName() $= "Player" || %searchObj.getClassName() $= "AIPlayer")
   {
    %pos = %searchObj.getHackPosition();
    %r = 3;
   }
   else if(%searchObj.getClassName() !$= "Projectile")
   {
    %pos = %searchObj.getWorldBoxCenter();
    %r = 3.5;
   }
   else
   {
    if(%searchObj.getdatablock().noportal){continue;}
    %pos = %searchObj.position;
    %r = 6.5;
   }
   if(vectorDist(%pos,%obj.position) < %r) //Radius searches find bounding box not actual object and mess up with players
   portalTeleport(%obj,%searchObj);
  }
  continue;
 }
 for(%i=0;%i<clientgroup.getcount();%i++)
 {
  if(!isObject((%searchObj = clientgroup.getobject(%i).player))){return;}
    if(mAbs(%searchObj.portalVel - vectorLen(%searchobj.getVelocity())) > 2)
    {
    %searchobj.portalVel = vectorLen(%searchobj.getVelocity());
    }
 }
 if(!isObject(%this.value[0])){%this.delete();}
}

function servercmdclearportalgun(%c)
{
 if(!%c.isSuperAdmin){return;}
 if(!isObject($Pgunctrl)){return;}
 messageall('MsgClearBricks',"\c3" @ %c.name @ "\c0 cleared all \c3Portals\c0.");
 for(%i=0;%i<$Pgunctrl.count;%i++)
 {
  $Pgunctrl.getValue(%i).delete();
 }
}

function portalTeleport(%portal,%player)
{
 if($Sim::Time-%player.lastDetected < 0.75 && %portal $= %player.lastportal){return;}
 //%portal.client.player.pPortal[X] will always exist, the portal dies if the player is gone
 if(%player.client.minigame $= %portal.client.minigame && isObject(%portal.client.minigame)){%level = $TrustLevel::PortalMiniUse;}else{%level = $TrustLevel::PortalUse;}
 if(%player.getClassName() $= "Player" && getTrustLevel(%player.client,%portal.client) < %level)
 {
    commandtoclient(%player.client,'centerprint',%portal.client.name @ " does not trust you enough to do that.",2,3);
    return;
 }
 if(%portal.portalType $= "Blue"){%end = %portal.client.player.pPortalOrange;}
 else{ %end = %portal.client.player.pPortalBlue;}
 if(!isObject(%end)){return;}
 if(%player.getClassName() $= "Projectile")
 {
  %type = %player.getDatablock();
  %t = %player.createtime;
  %life = %type.lifetime * %type.muzzleVelocity; //There should be more equations for ballistic projectiles but it's close enough (2340 vs 2500 for HEGrenade etc)
  %sched = (%t - $Sim::Time + %life/1000)*1000;
  %p = new Projectile()
  {
			dataBlock = %type;
			initialVelocity = vectorScale(%end.normal,%type.muzzleVelocity);
			initialPosition = vectorAdd(%end.getTransform(),vectorScale(%end.normal,0));
			sourceObject = $wrenchItem;
			sourceSlot = %player.sourceSlot;
			client = %player.client;
			lastDetected = $Sim::Time;
			lastPortal = %end;
  };missionCleanup.add(%p);schedule(0,0,projectileset,%p,%t);%p.schedule(%sched,forceExplode);
  %player.delete();
  
  return;
 }
 if(%player.isHeld){return;}
 %player.lastDetected = $Sim::Time;
 %player.lastPortal = %end;
 if(%player.isMounted()){return;}
 if(%player.getclassname() $= "Player" || %player.getclassname() $= "AIPlayer")
 {
 %add = vectorDist(%player.getPosition(),%player.getHackPosition());
 }else{%add = 0;}
   %normal = %end.normal;
   %pos1 = vectorAdd(%end.position,"0 0 0");
   %pos2 = vectorAdd(%end.position,vectorScale(%end.normal,2));
   %masks =	$typemasks::interiorobjecttype | //InteriorInstance
		$TypeMasks::StaticObjectType | //StaticShape?
		$TypeMasks::StaticShapeObjectType | //StaticShape?
		$TypeMasks::StaticTSObjectType | //TSStatics
		$typemasks::fxbrickobjecttype | //fxDTSBricks
		$TypeMasks::TerrainObjectType | //Terrain
		$TypeMasks::ShapeBaseObjectType; //Players vehicles etc will have a Portal Error on them when you try to place one on them
   %scanTarg = ContainerRayCast (%pos1, %pos2, %masks,%player);
   if(isObject(firstWord(%scanTarg)))
   {
    %player.lastDetected = $Sim::Time;%player.lastportal = %portal;
    serverplay3d(PgunInvalidSound,%portal.position SPC "0 0 1 0");
    commandtoclient(%player.client,'centerprint',"Portal Blocked!",1,3);
    return;
   }

 if(!isObject(%player)){return;}
 %normal = %end.normal;
 %r = rotFromTransform(%player.gettransform());
 %n = rotFromTransform(%end.gettransform());
 %vel = vectorLen(%player.getVelocity()) > 4 ? vectorLen(%player.getVelocity()) : 4;
 //%vel = vectorLen(%player.getVelocity());
 %vel = (%player.portalVel > %vel ? %player.portalVel : %vel);
 %player.setVelocity(vectorScale(%end.normal,%vel));
 %player.setTransform(vectorAdd(%end.getTransform(),vectorScale(%end.normal,%add+1)) SPC %r);
 %player.portalVel = "";
}

function projectileset(%p,%time)
{
 %p.createtime = %time;
}

//-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

datablock AudioProfile(PgunUseSound)
{
   filename    = "./sound/PortalGun/portalgun_powerup1.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(PgunFireSound)
{
   filename    = "./sound/PortalGun/portalgun_shoot_red1.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(PgunInvalidSound)
{
   filename    = "./sound/PortalGun/portal_invalid_surface3.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(PgunOpenSound)
{
   filename    = "./sound/PortalGun/portal_open3.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(PgunCloseSound)
{
   filename    = "./sound/PortalGun/portal_close2.wav";
   description = AudioClose3d;
   preload = true;
};

//-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

datablock ParticleData(pGunParticle : printGunParticle)
{
 	colors[0]     = "0.9 0.2 0.0 0.9";
	colors[1]     = "0.9 0.2 0.0 0.0";
};

datablock ParticleEmitterData(pGunExplodeEmitter : printGunExplosionEmitter)
{
 particles = pGunParticle;
};

datablock ExplosionData(pGunExplosion : printGunExplosion)
{
 emitter[0] = pGunExplodeEmitter;
};

datablock ParticleData(pGunMuzzleParticle)
{
	dragCoefficient      = 3;
	gravityCoefficient   = -0.0;
	inheritedVelFactor   = 0.0;
	constantAcceleration = 0.0;
	lifetimeMS           = 100;
	lifetimeVarianceMS   = 0;
	textureName          = "base/data/particles/thinRing";
	spinSpeed		= 0.0;
	spinRandomMin		= 0.0;
	spinRandomMax		= 0.0;
	colors[0]     = "0 0.5 0.9 0.4";
	colors[1]     = "0.0 0 0.9 0.0";
	sizes[0]      = 0.15;
	sizes[1]      = 0.5;

	useInvAlpha = false;
};
datablock ParticleEmitterData(pGunMuzzleEmitter)
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 0;
   ejectionVelocity = 0.0;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "pGunMuzzleParticle";
};

datablock ParticleData(pGun2MuzzleParticle)
{
	dragCoefficient      = 3;
	gravityCoefficient   = -0.0;
	inheritedVelFactor   = 0.0;
	constantAcceleration = 0.0;
	lifetimeMS           = 100;
	lifetimeVarianceMS   = 0;
	textureName          = "base/data/particles/thinRing";
	spinSpeed		= 0.0;
	spinRandomMin		= 0.0;
	spinRandomMax		= 0.0;
	colors[0]     = "1 0.25 0 0.4";
	colors[1]     = "1 0 0 0.0";
	sizes[0]      = 0.15;
	sizes[1]      = 0.5;

	useInvAlpha = false;
};
datablock ParticleEmitterData(pGun2MuzzleEmitter)
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 0;
   ejectionVelocity = 0.0;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "pGun2MuzzleParticle";
};

datablock ItemData(PGunItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "add-ons/shapes/l-portalgun.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Portal Gun";
	iconName = "add-ons/ItemIcons/pGun";
	doColorShift = false;
	colorShiftColor = "1 1 1 1";

	 // Dynamic properties defined by the scripts
	image = PgunImage;
	canDrop = true;
};

datablock ShapeBaseImageData(PGunImage)
{
   shapeFile = "./shapes/l-portalgun.dts";
   emap = true;

   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = "0"; //"0.7 1.2 -0.5";
   rotation = eulerToMatrix( "0 0 0" );

   correctMuzzleVector = true;

   className = "WeaponImage";

   item = BowItem;
   ammo = " ";
   projectile = hammerProjectile;
   projectileType = Projectile;

   casing = "";
   shellExitDir        = "1.0 -1.3 1.0";
   shellExitOffset     = "0 0 0";
   shellExitVariance   = 15.0;	
   shellVelocity       = 7.0;

   melee = false;
   armReady = true;

   doColorShift = true;
   colorShiftColor = "0 0 1 1.000";


	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.15;
	stateTransitionOnTimeout[0]       = "Ready";
	stateSound[0]					= "";

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;
	stateEmitter[1]					= pGunMuzzleEmitter;
	stateEmitterTime[1]				= 10000;
	stateEmitterNode[1]				= "muzzlePoint";
	stateSequence[1]	= "Ready";

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Reload";
	stateTimeoutValue[2]            = 0.1;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;
	stateEmitter[2]					= pGunMuzzleEmitter;
	stateEmitterTime[2]				= 0.5;
	stateEmitterNode[2]				= "muzzlePoint";
	stateSound[2]					= "";
	stateEjectShell[2]       = false;

	stateName[3]			= "Reload";
	stateScript[3] = "onReload";
	stateSequence[3]                = "Reload";
	stateTransitionOnTriggerUp[3]     = "Ready";
	stateSequence[3]	= "Ready";
	stateEmitter[3] = "";
	stateEmitterTime[3]				= 0.01;

};

datablock ShapeBaseImageData(PGun2Image : PGunImage)
{
   shapeFile = "./shapes/l-portalgun.dts";
   colorShiftColor = "1 0.25 0 1.000";
   stateSound[0]					= "";
   stateEmitter[1]					= pGun2MuzzleEmitter;
   stateEmitter[2]					= pGun2MuzzleEmitter;
};

//-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

datablock ParticleData(PgunBlueRingParticle)
{
	dragCoefficient      = 2;
	gravityCoefficient   = 0;
	inheritedVelFactor   = 0;
	constantAcceleration = 0.0;
	lifetimeMS           = 1000;
	lifetimeVarianceMS   = 100;
	textureName          = "base/data/particles/dot";
	spinSpeed		= 10.0;
	spinRandomMin		= 0.0;
	spinRandomMax		= 0.0;
	colors[0]     = "0 1 1 0.9";
	colors[1]     = "0 0 1 1";
	colors[2]     = "0 0 1 1";
	sizes[0]      = 0.5;
	sizes[1]      = 1;
	sizes[2]      = 2;

	useInvAlpha = false;
};
datablock ParticleEmitterData(PgunBlueRingEmitter)
{
	lifeTimeMS = 3000;

   ejectionPeriodMS = 2;
   periodVarianceMS = 0;
   ejectionVelocity = 5;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 80;
   thetaMax         = 100;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "PgunBlueRingParticle";
};

datablock ParticleData(pGunNoPortalParticle)
{
	dragCoefficient      = 3;
	gravityCoefficient   = 0;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.0;
	lifetimeMS           = 1000;
	lifetimeVarianceMS   = 0;
	textureName          = "base/data/particles/ring";
	spinSpeed		= 0.0;
	spinRandomMin		= -000.0;
	spinRandomMax		= 000.0;
	colors[0]     = "0.9 0.9 0.9";
	colors[1]     = "0.9 0.9 0.9 0.0";
	sizes[0]      = 0.1;
	sizes[1]      = 3.5;

	useInvAlpha = false;
};
datablock ParticleEmitterData(pGunNoPortalEmitter)
{
   ejectionPeriodMS = 100;
   periodVarianceMS = 0;
   ejectionVelocity = 0;
   velocityVariance = 0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 00;
   phiReferenceVel  = 0;
   faceViewer = false;
   phiVariance      = 0;
   overrideAdvance = false;
   particles = "pGunNoPortalParticle";
};

datablock ParticleData(PgunOrangeRingParticle : PgunBlueRingParticle)
{
	colors[0]     = "0.5 0.25 0 0.9";
	colors[1]     = "0.5 0 0 0.5";
	colors[2]     = "0.5 0 0 0";
	dragCoefficient      = 2; 
};
datablock ParticleEmitterData(PgunOrangeRingEmitter : PgunBlueRingEmitter)
{
   particles = "PgunOrangeRingParticle";
   ejectionVelocity = 5;
   thetaMin         = 80;
   thetaMax         = 100;
};

//-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

function PortalCollision(%type,%obj,%col,%pos,%normal)
{
  %player = %obj;
  if(!isObject(%player) || %player.getState() $= "Dead" || !isObject(%player.client)){return;}
   %r = vectorScale(-(getWord(%normal,1)) SPC -(getWord(%normal,0)) SPC getWord(%normal,2),1) SPC "90";
 if(%col.getClassName() $= "InteriorInstance" || %col.getClassName() $= "StaticShape" || %col.getClassName() $= "TSStatic" || %col.getClassName() $= "fxDTSBrick" || %col.getClassName() $= "TerrainBlock")
 {
  %portal = %type;
  for(%i=0;%i<$PGunCtrl.count;%i++)
  {
   if(vectorDist($PGunCtrl.getValue(%i).position,%pos) < 5 && $PGunCtrl.getValue(%i) !$= %player.pPortal[%portal]){portError(%pos);return;}
  }
  for(%i=0;%i<clientgroup.getcount();%i++)
  {
   %portaledplayer = clientgroup.getobject(%i).player;
   if(!isObject(%portaledplayer)){continue;}
   if(%obj.client.minigame $= %portaledplayer.client.minigame && isObject(%portaledplayer.client.minigame)){%level = $TrustLevel::PortalMiniMakeUnder;}else{%level = $TrustLevel::PortalMakeUnder;}
   if(vectorDist(%portaledplayer.getPosition(),%pos) < 2.5 && getTrustLevel(%portaledplayer.client,%obj.client)<%level)
   {
    commandtoclient(%obj.client,'centerprint',%portaledplayer.client.name @ " does not trust you enough to do that.",2,3);
    portError(%pos);
    return;
   }
  }
  initContainerRadiusSearch(%pos,0.2,$TypeMasks::FXBrickObjectType);
  while(isObject(firstWord(%searchObj = containerSearchNext())))
  {
   //if(firstWord(%searchObj).dePortal)
   if(firstWord(%searchObj).getDatablock().noportal)
   {
    portError(%pos);
    return;
   }
   if(%obj.client.minigame $= firstWord(%searchObj).getGroup().client.minigame && isObject(%obj.client.minigame)){%level = $TrustLevel::PortalMiniMakeBrick;}else{%level = $TrustLevel::PortalMakeBrick;}
   if(isObject(%obj.client) && getTrustLevel(firstWord(%searchObj).getGroup(),%obj.client) < %level)
   {
    commandtoclient(%obj.client,'centerprint',firstWord(%searchObj).getGroup().name @ " does not trust you enough to do that.",2,3);
    portError(%pos);
    return;
   }
  }
  if(isObject(%player.pPortal[%portal])){%player.pPortal[%portal].delete();}
  serverplay3d(PgunOpenSound,%pos SPC "0 0 1 0");
  serverplay3d(PgunFireSound,%obj.getTransform());
  %p = new ParticleEmitterNode()
  {
   datablock = genericEmitterNode;
   emitter = "Pgun" @ %portal @ "RingEmitter";
   scale = "0.1 0.1 0.1";
   rotation = %r;
   velocity = "1";
   position = vectorAdd(%pos,vectorScale(%normal,0.4));
  };
  for(%i=0;%i<$PGunCtrl.count;%i++)
  {
   if($PGunctrl.getvalue(%i) $= %p){%noadd = 1;} //To list of portals
  }
  //%p.schedule(PgunBlueRingEmitter.lifeTimeMS,delete);
  %p.brick = %p;
  %p.portalType = %portal;
  %p.client = %obj.client;
  %p.normal = %normal;
  %p.source = %col;
  %player.pPortal[%portal] = %p;
  MissionCleanup.add(%p);

  if(!isObject($PGunCtrl)){$PGunCtrl = new ScriptObject(){class = PGunCtrlSO;count = 0;};}
  %obj.schedule(10,mountImage,%obj.nextPGun,0);
  if(!%noadd){$PGunCtrl.addValue(%p);}
 }
 else
 {
  portError(%pos);
 }
}

function portError(%pos)
{
  serverplay3d(PgunInvalidSound,%pos SPC "0 0 1 0");
  %p = new ParticleEmitterNode()
  {
   datablock = genericEmitterNode;
   emitter = "pGunNoPortalEmitter";
   scale = "0 0 0";
   rotation = %r;
   velocity = "1";
   position = %pos;
  };%p.brick = %p;%p.schedule(100,delete);
}

function pGunImage::onFire(%this,%obj,%slot)
{
  %player = %obj.client.player;
  if(!isObject(%player) || %player.getState() $= "Dead"){return;}
 %obj.playThread(2, shiftAway);
 if(%obj.getImageAmmo(0)){%type = "Orange";%obj.nextpgun = "pgun2image";%parts = pGunExplodeEmitter;}else{%type = "Blue";%obj.nextpgun = "pgunimage";%parts = printGunExplosionEmitter;}
 %obj.dontplay = 1;
 //%normal = %obj.getMuzzleVector(0);
 //%pos = %obj.getMuzzlePoint(0);


 %normal = %obj.getEyeVector();
 %pos1 = %obj.getEyePoint();
   %masks =	$typemasks::interiorobjecttype | //InteriorInstance
		$TypeMasks::StaticObjectType | //StaticShape?
		$TypeMasks::StaticShapeObjectType | //StaticShape?
		$TypeMasks::StaticTSObjectType | //TSStatics
		$typemasks::fxbrickobjecttype | //fxDTSBricks
		$TypeMasks::TerrainObjectType | //Terrain
		$TypeMasks::ShapeBaseObjectType; //Players vehicles etc will have a Portal Error on them when you try to place one on them
   %pos2 = vectorAdd(%pos1,vectorScale(%normal,1000));
   %scanTarg = ContainerRayCast (%pos1, %pos2, %masks);
   if(!isObject(firstWord(%scanTarg))){return;}
   %pos = posFromRaycast(%scanTarg);
   %normal = normalFromRaycast(%scanTarg);
   %col = firstWord(%scanTarg);
   PortalCollision(%type,%obj,%col,%pos,%normal);

 %r = vectorScale(-(getWord(%normal,1)) SPC -(getWord(%normal,0)) SPC getWord(%normal,2),1) SPC "90";
  %p = new ParticleEmitterNode()
  {
   datablock = genericEmitterNode;
   emitter = %parts;
   scale = "0.1 0.1 0.1";
   rotation = %r;
   velocity = "1";
   position = vectorAdd(%pos,vectorScale(%r,0.45));
  };
  %p.brick = %p;
  MissionCleanup.add(%p);
  
  %p.schedule(%parts.lifetimeMS,delete);
 //Parent::onFire(%this,%obj,%slot);
 //%this.projectile = portGunBlueProjectile;
}

function pGun2Image::onFire(%this,%obj,%slot)
{
 pGunImage::onFire(%this,%obj,%slot);
}

function pGunImage::onReload(%this,%obj,%slot)
{
}

function pGun2Image::onReload(%this,%obj,%slot)
{
}



function pGunImage::onMount(%this,%obj,%slot)
{
 if(%obj.dontplay $= ""){serverplay3d(pGunUseSound,%obj.getMuzzlePoint(0) SPC "0 0 1 0");}
 %obj.dontplay = "";
 %obj.setImageAmmo(0,0);
}

function pGun2Image::onMount(%this,%obj,%slot)
{
 if(%obj.dontplay $= ""){serverplay3d(pGunUseSound,%obj.getMuzzlePoint(0) SPC "0 0 1 0");}
 %obj.dontplay = "";
 %obj.setImageAmmo(0,0);
}

datablock ProjectileData(PortalForceExplodeProjectile)
{
   projectileShapeName = "./shapes/blank.dts";
   //directDamage
   //directDamageType
   //radiusDamageType
   //impactImpulse
   //verticalImpulse
   //explosion
   //particleEmitter

   //brickExplosionRadius
   //brickExplosionImpact
   //brickExplosionForce            
   //brickExplosionMaxVolume
   //brickExplosionMaxVolumeFloating

   //muzzleVelocity
   //velInheritFactor
   explodeOnDeath = true;

   armingDelay         = 2; 
   lifetime            = 5;
   fadeDelay           = 5;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = 0;
   gravityMod = 0;

   hasLight    = false;
};

package Portals
{
 function Armor::onRemove(%this,%obj,%a,%b,%c,%d)
 {
  if(isObject(%obj.pPortalBlue)){%obj.pPortalBlue.delete();}
  if(isObject(%obj.pPortalOrange)){%obj.pPortalOrange.delete();}
  Parent::onRemove(%this,%obj,%a,%b,%c,%d);
 }
 function Armor::onDisabled(%this,%obj,%a,%b,%c,%d)
 {
  if(isObject(%obj.pPortalBlue)){%obj.pPortalBlue.delete();}
  if(isObject(%obj.pPortalOrange)){%obj.pPortalOrange.delete();}
  Parent::onDisabled(%this,%obj,%a,%b,%c,%d);
 }
 function Armor::onTrigger(%this,%player,%slot,%trigger)
 {
  if(%slot !$= "4" || %trigger !$= "1" || !%player.getMountedImage(0) || (%player.getMountedImage(0) !$= nametoID(pGunImage) && %player.getMountedImage(0) !$= nametoID(pGun2Image))){Parent::onTrigger(%this,%player,%slot,%trigger);return;}
  %player.setImageAmmo(0,1);
  %player.setImageTrigger(0,1);
  %player.setImageAmmo(0,0);
  %player.setImageTrigger(0,0);
 }
 function servercmdcancelbrick(%client)
 {
  Parent::servercmdcancelbrick(%client);
  %obj = %client.player;
  if(isObject(%obj.pPortalBlue)){%obj.pPortalBlue.delete();%play = 1;}
  if(isObject(%obj.pPortalOrange)){%obj.pPortalOrange.delete();%play = 1;}
  if(!isObject(%obj) || !%play){return;}
  serverplay3d(pGunCloseSound,%obj.getTransform());
 }
 function servercmdplantbrick(%client)
 {
  %brick = %client.player.tempbrick;
  if(isObject(%brick) && %brick.getDatablock().noPortal && !%client.isSuperAdmin && !%client.isAdmin)
  {
   commandtoclient(%client,'centerprint',"\c5You are not an Admin!",2,3);
   return;
  }
  Parent::servercmdplantbrick(%client);
 }
 function Projectile::onAdd(%this,%obj,%a,%b,%c)
 {
  Parent::onAdd(%this,%obj,%a,%b,%c);
  %obj.createTime = $Sim::Time;
 }
 function Projectile::forceExplode(%obj)
 {
  if(isObject(%obj))
  {
   if(isObject(%obj.getDatablock().explosion) && %obj.getDatablock().explodeondeath)
   {
    %data = %obj.getDatablock();
    PortalForceExplodeProjectile.explosion			= %data.explosion;
    PortalForceExplodeProjectile.radiusDamageType		= %data.radiusDamageType;
    PortalForceExplodeProjectile.impactImpulse			= %data.impactImpulse;
    PortalForceExplodeProjectile.verticalImpulse			= %data.verticalImpulse;

    PortalForceExplodeProjectile.brickExplosionRadius 		= %data.brickExplosionRadius;
    PortalForceExplodeProjectile.brickExplosionImpact 		= %data.brickExplosionImpact;
    PortalForceExplodeProjectile.brickExplosionForce   		= %data.brickExplosionForce;
    PortalForceExplodeProjectile.brickExplosionMaxVolume	= %data.brickExplosionMaxVolume;
    PortalForceExplodeProjectile.brickExplosionMaxVolumeFloating	= %data.brickExplosionMaxVolumeFloating;

    %p = new Projectile()
    {
			dataBlock = PortalForceExplodeProjectile;
			initialVelocity = "0 0 0";
			initialPosition = %obj.position;
			sourceObject = $wrenchItem;
			sourceSlot = %obj.sourceSlot;
			client = %obj.client;
    };missionCleanup.add(%p);

   }
   %obj.delete();
  }
 }
 function GameConnection::OnDeath(%client,%obj,%a,%b,%c)
 {
  if(%obj.holder !$= ""){%a = %obj.holder.client;%b = $DamageType::ThrowCar;}
  Parent::onDeath(%client,%obj,%a,%b,%c);
 }
function servercmdLight(%client)
{
 if(isObject(%client.player) && isObject(%client.player.getMountedImage(0)))
 {
  if(%client.player.getMountedImage(0).getName() $= "pGunImage" || %client.player.getMountedImage(0).getName() $= "pGun2Image")
  {
   %client.player.mountImage(PGravGunImage,0);return;
  }
  else
  {
   %client.player.mountImage(PGunImage,0);return;
  }
 }
 Parent::servercmdLight(%client);
}
};activatepackage(Portals);

//No jets, no fall damage, regenerating health, lower jump, (slightly) more air control

datablock PlayerData(PlayerPortal : PlayerStandardArmor)
{
	maxDamage = 1;
	maxEnergy = 59;
	canJet = 0;
	rechargeRate = 0.1;
	airControl = 0.2;
	upmaxspeed = 500;
	jumpforce = PlayerStandardArmor.jumpforce * 0.67;
	speedDamagescale = 0;
	//maxForwardCrouchSpeed = PlayerStandardArmor.maxForwardSpeed;
	//maxForwardBackwardSpeed = PlayerStandardArmor.maxBackSpeed;
   	//maxForwardSideSpeed = PlayerStandardArmor.maxSideSpeed;
	//crouchBoundingBox = PlayerStandardArmor.boundingBox;
	uiName = "Portal-Style Player";
	showEnergyBar = true;
	groundimpactminspeed = 0;
	groundimpactshakeamp = "0.1 0.1 0.1";
};

function PlayerPortal::damage(%this, %obj, %sourceObject, %position, %damage, %damageType)
{
 if(%damageType $= $DamageType::Fall || %damagetype $= $Damagetype::groundFall){return;}
 %curEnergy = %obj.getEnergyLevel();
 if(%damage >= %curEnergy)
 {
  %obj.setEnergyLevel(0);
  %damage = %damage - %curEnergy;
 }
 else
 {
  %obj.setEnergyLevel(%curEnergy - %damage);
  %damage = 0;
 }
 Parent::damage(%this, %obj, %sourceObject, %position, %damage, %damageType);
 if(%obj.getEnergyLevel() < 40)
 {
  %obj.emote(painMidImage,1);
 }
 if(%obj.getEnergyLevel() < 15)
 {
  %obj.emote(painHighImage,1);
 }
}

//-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
datablock ProjectileData(PGravGunProjectile : printGunProjectile)
{
muzzleVelocity      = 145;
fadeDelay           = 3500;
lifetime            = 100;
explosion = "";
particleEmitter = "";
explodeondeath = 0;
};

datablock ParticleData(PGravGunParticle : printGunParticle)
{
 	colors[0]     = "0.9 0.2 0.0 0.9";
	colors[1]     = "0.9 0.2 0.0 0.0";
	sizes[0]      = 0.2;
	sizes[1]      = 0.2;
              dragCoefficient = 0;
	lifetimeMS           = 500;
};

datablock ParticleEmitterData(PGravGunEmitter : printGunEmitter)
{
 particles = PGravGunParticle;
};

datablock ShapeBaseImageData(PGravGunImage : printGunImage)
{
   shapeFile = "./shapes/l-portalgun.dts";
   emap = false;

   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = "0"; //"0.7 1.2 -0.5";
   rotation = eulerToMatrix( "0 0 0" );

   correctMuzzleVector = true;

   className = "WeaponImage";

   item = BowItem;
   ammo = " ";
   projectile = PGravGunProjectile;
   projectileType = Projectile;

   casing = "";
   shellExitDir        = "1.0 -1.3 1.0";
   shellExitOffset     = "0 0 0";
   shellExitVariance   = 15.0;	
   shellVelocity       = 7.0;

   melee = false;
   armReady = true;

   doColorShift = true;
   colorShiftColor = "1 1 1 1.000";


	// States
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.15;
	stateTransitionOnTimeout[0]       = "Ready";
	stateSound[0]					= pGunUseSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;
	stateSequence[1]	= "Ready";
	stateEmitter[1]					= pGun2MuzzleEmitter;
	stateEmitterTime[1]				= 1000000;
	stateEmitterNode[1]				= "muzzleNode";

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Reload";
	stateTimeoutValue[2]            = 0.2;
              stateTransitionOnAmmo[2] = "GravLoop";
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;
	stateEmitter[2]					= pGun2MuzzleEmitter;
	stateEmitterTime[2]				= 0.15;
	stateEmitterNode[2]				= "muzzleNode";
	stateSound[2]					= printFireSound;
	stateEjectShell[2]       = false;

	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateTransitionOnTrigger[3]     = "Ready";
	stateSequence[3]	= "Ready";

	stateName[4]			= "GravLoop";
	stateEmitter[4]					= PGravGunEmitter;
	stateEmitterTime[4]				= 0.02;
	stateEmitterNode[4]				= "muzzleNode";
              stateTransitionOnTimeout[4]     = "GravLoop";
              stateTransitionOnTriggerUp[4] = "Cancel";
              stateTransitionOnNoAmmo[4] = "Cancel";
              stateTimeoutValue[4]            = 0.01;
              stateScript[4]                  = "onGrav";
	stateSequence[4]	= "Ready";

              stateName[5] = "Cancel";
              stateScript[5] = "onCancel";
              stateTimeoutValue[5] = "0.1";
              stateTransitionOnTimeout[5] = "Ready";
};

function PGravGunProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
 if((strStr(strLwr(%col.getClassName()),"vehicle") !$= "-1" || %col.getclassname() $= "AIPlayer") && (!%col.isHeld) && %col.spawnbrick.getGroup().client.minigame $= %obj.client.minigame)
 {
  %obj.client.player.gravobj = %col;
  %obj.client.player.gravobj.holder = %obj.client.player;
  %obj.client.player.gravobj.lastpos = %col.position;
  %obj.client.player.gravobj.lastpos2 = %col.position;
  %obj.client.player.gravobj.lastrot = "0 0 0";
  %obj.client.player.setImageAmmo(0,1);
  %obj.client.player.gravobj.disablemove = 1;
  %obj.client.player.gravobj.setVelocity("0 0 1"); //Make cubes above it fall when you stack and pull one out
 }
}

function isMountedTo(%a,%b)
{
 for(%i=0;%i<%b.getDatablock().numMountPoints;%i++)
 {
  if(%b.getMountedObject(%i) $= %a){return 1;}
 }
 return 0;
}

function PGravGunImage::onGrav(%this,%obj,%slot)
{
 %obj.setImageTrigger(0,1);
 if(!isObject(%obj.gravobj)){%obj.setImageTrigger(0,0);%obj.setImageAmmo(0,0);return;}
 if(isMountedTo(%obj,%obj.gravObj)){%obj.setImageTrigger(0,0);%obj.setImageAmmo(0,0);return;}
   %mouseVec = %obj.getEyeVector();
   %cameraPoint = %obj.getEyePoint();
   %selectRange = %obj.gravObj.getDatablock().mass / 50;
   %mouseScaled = VectorScale(%mouseVec, %selectRange);
   %rangeEnd = VectorAdd(%cameraPoint, %mouseScaled);

   %searchMasks = $TypeMasks::VehicleObjectType | $TypeMasks::FXBrickObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::StaticObjectType | $TypeMasks::TerrainObjectType;

   %player = %obj;
	  %scanTarg = ContainerRayCast (%cameraPoint, %rangeEnd, %searchMasks,%obj.gravobj);
   if(!isObject(firstWord(%scanTarg)))
   {
    %pos = %rangeEnd;
   }
   else
   {
    %sub = vectorSub(posFromRaycast(%scanTarg),%cameraPoint);
    %nom = vectorNormalize(%sub);
    %len = vectorLen(%sub);
    %pos = vectorAdd(%cameraPoint,vectorScale(%nom,%len*0.5));
   }
   %obj.gravObj.holder = %obj;
   %obj.gravObj.isHeld = 1;
   %obj.gravObj.lastPos2 = %obj.gravObj.lastPos;
   %obj.gravObj.lastPos = %obj.gravObj.getTransform();
   %t = %obj.gravObj.getTransform();
   %rot =  eulerToMatrix(%obj.gravobj.lastrot); 
     //Uncomment the line below and comment out the one below this to use alternate control method. It "swings" more realistically but kills people very easily and you if you turn too fast.
   //%obj.gravObj.setTransform(%obj.gravObj.position SPC %rot);
   %obj.gravObj.setTransform(%pos SPC %rot);
  %obj.gravobj.lastrot = vectorAdd(%obj.gravobj.lastrot,"0 0 1");
  //Uncomment the line below and comment out the one below this to use alternate control method. It "swings" more realistically but kills people very easily and you if you turn too fast.
   //%obj.gravObj.setVelocity(vectorScale(vectorSub(%pos,%obj.gravObj.position),5));
   %obj.gravObj.setVelocity("0 0 0");
}

function PGravGunImage::onCancel(%this,%obj,%slot)
{
 if(isObject(%obj.gravObj)){ %obj.gravObj.setVelocity(vectorScale(vectorSub(%obj.gravObj.getTransform(),%obj.gravObj.lastpos2),5));}
 %obj.gravobj.disablemove = 0;
 %obj.gravObj.isHeld = 0;
 schedule(3000,0,gravhold,%obj.gravobj);
 %obj.gravObj = "";
 %obj.setImageAmmo(0,0);
}

function PGravGunImage::onUnMount(%this,%obj,%slot)
{
 %obj.gravobj.disablemove = 0;
 schedule(3000,0,gravhold,%obj.gravobj);
 %obj.gravObj.holder = "";
 %obj.gravObj.isHeld = "";
 %obj.gravObj = "";
}

function PGravGunImage::onMount(%this,%obj,%slot)
{
 %obj.setImageAmmo(0,0);
}

function gravclient(%a,%b){%a.client = %b;}
function gravhold(%a){%a.holder = "";}