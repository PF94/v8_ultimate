//Fill Can by Zor
$Pref::Server::FillCan_MaxBricks=500; //Maximum number of bricks a non-admin can paint at a time

function paintfill(%client,%obj,%newcolor){
 %oldcolor=%obj.getcolorid();
 if(%oldcolor==%newcolor)
  return;
 if(gettrustlevel(%client,%obj)!=2)
  return;
 %obj.setcolor(%newcolor);
 %client.paintfill=%client.paintfill TAB %obj;
 if(!%client.isSomeAdmin){
  %client.paintfillcount++;
  if(%client.paintfillcount>=$pref::server::fillcan_maxbricks){
   messageclient(%client,'MsgPlantError_Limit');
   centerprint(%client,"\c3Reached Fill Can Brick Limit ("@$pref::server::fillcan_maxbricks@")",3,3);
   return;
  }
 }
 %data=%obj.getdatablock();
 %rot=round(getword(%obj.rotation,3));
 if(%rot==90 || %rot==270){
  %sizex=%data.bricksizey;
  %sizey=%data.bricksizex;
 }else{
  %sizex=%data.bricksizex;
  %sizey=%data.bricksizey;
 }
 %sizez=%data.bricksizez;
 %box=(%sizex*0.5+0.6) SPC (%sizey*0.5+0.6) SPC (%sizez*0.2+0.3);
 %spreadcount=0;
 InitContainerBoxSearch(%obj.getposition(),%box,$typemasks::fxbrickobjecttype);
 while(%target=containerSearchNext()){
  if(%target.isplanted() && %target.getcolorid()==%oldcolor){
   %spread[%spreadcount]=%target;
   %spreadcount++;
  }
 }
 for(%x=0;%x<%spreadcount;%x++)
   paintfill(%client,%spread[%x],%newcolor);
}

function paintFXfill(%client,%obj,%oldcolor,%newFX){
 %newcolor=%obj.getcolorid();
 if(%oldcolor!=%newcolor || %obj.getcolorfxid()==%newfx)
  return;
 if(gettrustlevel(%client,%obj)!=2)
  return;
 %client.paintfill=%client.paintfill TAB %obj SPC %obj.getcolorfxid();
 %obj.setcolorfx(%newFX);
 if(!%client.isSomeAdmin){
  %client.paintfillcount++;
  if(%client.paintfillcount>=$pref::server::fillcan_maxbricks){
   messageclient(%client,'MsgPlantError_Limit');
   centerprint(%client,"\c3Reached Fill Can Brick Limit ("@$pref::server::fillcan_maxbricks@")",3,3);
   return;
  }
 }
 %data=%obj.getdatablock();
 %rot=round(getword(%obj.rotation,3));
 if(%rot==90 || %rot==270){
  %sizex=%data.bricksizey;
  %sizey=%data.bricksizex;
 }else{
  %sizex=%data.bricksizex;
  %sizey=%data.bricksizey;
 }
 %sizez=%data.bricksizez;
 %box=(%sizex*0.5+0.6) SPC (%sizey*0.5+0.6) SPC (%sizez*0.2+0.3);
 %spreadcount=0;
 InitContainerBoxSearch(%obj.getposition(),%box,$typemasks::fxbrickobjecttype);
 while(%target=containerSearchNext()){
  if(%target.isplanted() && %target.getcolorid()==%oldcolor){
   %spread[%spreadcount]=%target;
   %spreadcount++;
  }
 }
 for(%x=0;%x<%spreadcount;%x++)
   paintFXfill(%client,%spread[%x],%oldcolor,%newFX);
}

function shapeFXfill(%client,%obj,%oldcolor,%newFX){
 %newcolor=%obj.getcolorid();
 if(%oldcolor!=%newcolor || %obj.getshapefxid()==%newfx)
  return;
 if(gettrustlevel(%client,%obj)!=2)
  return;
 %client.paintfill=%client.paintfill TAB %obj SPC %obj.getshapefxid();
 %obj.setshapefx(%newFX);
 if(!%client.isSomeAdmin){
  %client.paintfillcount++;
  if(%client.paintfillcount>=$pref::server::fillcan_maxbricks){
   messageclient(%client,'MsgPlantError_Limit');
   centerprint(%client,"\c3Reached Fill Can Brick Limit ("@$pref::server::fillcan_maxbricks@")",3,3);
   return;
  }
 }
 %data=%obj.getdatablock();
 %rot=round(getword(%obj.rotation,3));
 if(%rot==90 || %rot==270){
  %sizex=%data.bricksizey;
  %sizey=%data.bricksizex;
 }else{
  %sizex=%data.bricksizex;
  %sizey=%data.bricksizey;
 }
 %sizez=%data.bricksizez;
 %box=(%sizex*0.5+0.6) SPC (%sizey*0.5+0.6) SPC (%sizez*0.2+0.3);
 %spreadcount=0;
 InitContainerBoxSearch(%obj.getposition(),%box,$typemasks::fxbrickobjecttype);
 while(%target=containerSearchNext()){
  if(%target.isplanted() && %target.getcolorid()==%oldcolor){
   %spread[%spreadcount]=%target;
   %spreadcount++;
  }
 }
 for(%x=0;%x<%spreadcount;%x++)
   shapeFXfill(%client,%spread[%x],%oldcolor,%newFX);
}

datablock ParticleData(fillcanTrailParticle)
{
	dragCoefficient		= 3.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 200;
	lifetimeVarianceMS	= 0;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= false;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "base/data/particles/star1";
	//animTexName		= "~/data/particles/dot";

	// Interpolation variables
	colors[0]	= "1 1 1 0.2";
	colors[1]	= "1 1 1 0.0";
	sizes[0]	= 0.3;
	sizes[1]	= 0.0;
	times[0]	= 0.0;
	times[1]	= 1.0;
};

datablock ParticleEmitterData(fillcanTrailEmitter)
{
   ejectionPeriodMS = 2;
   periodVarianceMS = 0;

   ejectionVelocity = 0; //0.25;
   velocityVariance = 0; //0.10;

   ejectionOffset = 0;

   thetaMin         = 0.0;
   thetaMax         = 90.0;  

   particles = fillcanTrailParticle;
};

//effects
datablock ParticleData(fillcanExplosionParticle)
{
	dragCoefficient      = 5;
	gravityCoefficient   = 0.1;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.0;
	lifetimeMS           = 500;
	lifetimeVarianceMS   = 300;
	textureName          = "base/data/particles/chunk";
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	colors[0]     = "0.9 0.9 0.6 0.9";
	colors[1]     = "0.9 0.5 0.6 0.0";
	sizes[0]      = 0.25;
	sizes[1]      = 0.0;
};

datablock ParticleEmitterData(fillcanExplosionEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   ejectionVelocity = 5;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "fillcanExplosionParticle";
};

datablock ExplosionData(fillcanExplosion)
{
   //explosionShape = "";
//	soundProfile = 0;

   lifeTimeMS = 150;

   particleEmitter = fillcanExplosionEmitter;
   particleDensity = 10;
   particleRadius = 0.2;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = false;
   camShakeFreq = "10.0 11.0 10.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.5;
   camShakeRadius = 10.0;

   // Dynamic light
   lightStartRadius = 0;
   lightEndRadius = 2;
   lightStartColor = "0.3 0.6 0.7";
   lightEndColor = "0 0 0";
};


//projectile
datablock ProjectileData(fillcanProjectile)
{
   //projectileShapeName = "./shapes/arrow.dts";

   directDamage        = 0;
   directDamageType    = $DamageType::ArrowDirect;

   radiusDamage        = 0;
   damageRadius        = 0;
   radiusDamageType    = $DamageType::ArrowDirect;

   explosion           = fillcanExplosion;
   particleEmitter     = fillcanTrailEmitter;

   muzzleVelocity      = 20;
   velInheritFactor    = 1;

   armingDelay         = 0;
   lifetime            = 500;
   fadeDelay           = 250;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.0;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";
};


//////////
// item //
//////////
datablock ItemData(fillcanItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "base/data/shapes/spraycan.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Fill Can";
	iconName = "./ItemIcons/wand";
	doColorShift = true;
	colorShiftColor = "1 1 1 1";

	 // Dynamic properties defined by the scripts
	image = fillcanImage;
	canDrop = true;
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(fillcanImage)
{
   // Basic Item properties
   shapeFile = "base/data/shapes/spraycan.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = "0.7 1 -0.6";
   rotation = eulerToMatrix( "0 0 10" );

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = fillcanItem;
   ammo = " ";
   projectile = fillcanProjectile;
   projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;

   doColorShift = true;
   colorShiftColor = fillcanItem.colorShiftColor;

	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.5;
	stateTransitionOnTimeout[0]       = "Ready";
	stateSound[0]					= sprayactivateSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Reload";
	stateTimeoutValue[2]            = 0.05;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;
	stateEmitter[2]="glowPaintExplosionEmitter";
	stateEmitterNode[2]="muzzleNode";
	stateEmitterTime[2]=1;
	stateSound[2]					= sprayfireSound;

	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]            = 0.5;
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTimeout[3]     = "Check";

	stateName[4]			= "Check";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "Fire";

	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.2;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	//stateSequence[5]                = "Reload";
	stateScript[5]                  = "onStopFire";


};

function fillcanprojectile::oncollision(%this,%obj,%col,%fade,%pos,%normal){
 %client=%obj.client;
 if(%col.getclassname()$=fxdtsbrick){
  if(gettrustlevel(%obj.client,%col)==2){
   if(%col.getcolorid()!=%obj.client.currentcolor){
    %oldcolor=%col.getcolorid();
    %obj.client.paintfill="";
    %obj.client.paintfillcount=0;
    if(%client.isadmin || %client.issuperadmin)
     %client.issomeadmin=1;
    else
     %client.issomeadmin=0;
    if(%client.currentFXcan){
     if(%client.currentFXcan>7)
      shapeFXfill(%obj.client,%col,%col.getcolorid(),%obj.client.currentFXcan-8);
     else
      paintFXfill(%obj.client,%col,%col.getcolorid(),%obj.client.currentFXcan-1);
     %obj.client.undostack.push(%oldcolor TAB %client.currentFXcan TAB "FILLPAINTFX" TAB getsubstr(%obj.client.paintfill,1,strlen(%obj.client.paintfill)));
    }else{
     paintfill(%obj.client,%col,%obj.client.currentcolor);
     %obj.client.undostack.push(%oldcolor TAB %col.getcolorid() TAB "FILLPAINT" TAB getsubstr(%obj.client.paintfill,1,strlen(%obj.client.paintfill)));
    }
   }
  }else{
   %name=%col.getgroup().name;
   centerprint(%client,%name@" does not trust you enough to do that.",3,3);
  }
 }else if(%col.getclassname()$=player){
  %color=getcoloridtable(%obj.client.currentcolor);
  %col.settempcolor(%color,2500);
  if(getword(%color,3)==1)
   %col.emote(hateimage);
 }
}

function servercmdfillcan(%client){
 %client.player.updatearm(fillcanimage);
 %client.player.mountimage(fillcanimage,0);
}

package fillcan{
 function servercmdundobrick(%client){
//  %str=strreplace(%client.undostack.val[%client.undostack.head-1],"\t"," ");
  %str=%client.undostack.val[%client.undostack.head-1];
  if(getfield(%str,2)$="FILLPAINT"){
   %oldcolor=getfield(%str,0);
   %newcolor=getfield(%str,1);
   for(%x=getfieldcount(%str)-1;%x>2;%x--){
    %b=getfield(%str,%x);
    if(isobject(%b)){
     if(%b.getcolorid()==%newcolor)
      %b.setcolor(%oldcolor);
    }
   }
   %client.undostack.val[%client.undostack.head-1]=0;
   %client.undostack.head--;
   %client.player.playthread(0,undo);
  }else if(getfield(%str,2)$="FILLPAINTFX"){
   %oldcolor=getfield(%str,0);
   %oldfx=getfield(%str,1)-1;
   if(%oldfx>6){
    %oldfx-=7;
    for(%x=getfieldcount(%str)-1;%x>2;%x--){
     %f=getfield(%str,%x);
     %b=getword(%f,0);
     if(isobject(%b)){
      if(%b.getshapefxid()==%oldfx)
       %b.setshapefx(getword(%f,1));
     }
    }
   }else{
    for(%x=getfieldcount(%str)-1;%x>2;%x--){
     %f=getfield(%str,%x);
     %b=getword(%f,0);
     if(isobject(%b)){
      if(%b.getcolorfxid()==%oldfx)
       %b.setcolorfx(getword(%f,1));
     }
    }
   }
   %client.undostack.val[%client.undostack.head-1]=0;
   %client.undostack.head--;
   %client.player.playthread(0,undo);
  }else
   parent::servercmdundobrick(%client);
 }

 function servercmdusefxcan(%client,%can){
  parent::servercmdusefxcan(%client,%can);
  %client.currentFXcan=%can+1;
 }
 function servercmdusespraycan(%client,%can){
  parent::servercmdusespraycan(%client,%can);
  %client.currentFXcan=0;
 }
};
activatepackage(fillcan);