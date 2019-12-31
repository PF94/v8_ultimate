if(!$Pref::Server::TrapSpawns)
{
 $Pref::Server::TrapSpawns = "20";
 $Pref::Server::TrapAdmin = "1";
}
 if(!isObject(arrowProjectile)){exec("./Weapon_Bow.cs");}
 if(!isObject(alarmEmitter)){exec("./Emote_Alarm.cs");}
if($AddOn__Weapon_Bow $= "-1"){schedule(1000,0,eval,"bowitem.uiname = \"\";");}
if($AddOn__Weapon_Alarm $= "-1"){schedule(1000,0,eval,"function servercmdAlarm(){}");}
if(!isObject(gunProjectile)){exec("./Weapon_Gun.cs");}
if($AddOn__Weapon_Gun $= "-1"){schedule(1000,0,eval,"gunitem.uiname = \"\";");}

////////TRAP BRICK: FIRE\\\\\\\\
datablock ItemData(TrapFireItem)
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
	uiName = "Trap Brick: Fire";
	iconName = "";
	doColorShift = true;
	colorShiftColor = "0.000 0.200 0.640 1.000";
	
	 // Dynamic properties defined by the scripts
	image = "";
	canDrop = false;
};

AddDamageType("FireDirect",   '<bitmap:add-ons/ci/generic> %1',    '%2 <bitmap:add-ons/ci/generic> %1',1,1);
datablock ProjectileData(fireProjectile : arrowprojectile)
{
	muzzleVelocity = 10;
	lifetime = 300;
	directDamage        = 4.0;
	directDamageType    = $DamageType::FireDirect;
	projectileShapeName = "./shapes/blank.dts";
	radiusDamage        = 0;
	damageRadius        = 0;
	radiusDamageType    = $DamageType::FireDirect;
	explosion = color28paintexplosion;
   	particleEmitter     = adminwandemitterB;

};

function fireProjectile::Damage(%this,%a,%b,%c,%d,%e,%f,%g,%h)
{
	Parent::Damage(%this,%a,%b,%c,%d,%e,%f,%g,%h);
	%b.burn(1000);
} 

function TrapFireItem::onPickup(%this, %item, %obj, %amount)
{
	//Nothing!
}

function TrapFireItem::onAdd(%this,%item)
{
	parent::onAdd(%this,%item);
	%item.projectile = fireProjectile;
	%item.delay = 50;
	%item.isTrap = 1;
	%item.ontime = 50;
	%item.offtime = 20;
	%item.NORMALontime = %item.ontime;
	%item.NORMALofftime = %item.offtime;
	%item.onstate = 0;
	%item.onAmount = 0;
	schedule(10,0,"trapCheck",%item);
	%item.sched = schedule(%this.delay,0,"trapFire",%item,1);
}

function TrapFireItem::onRemove(%this,%item)
{
	%item.spawnbrick.getGroup().client.trapSpawns--;
}

////////TRAP BRICK: ARROWS\\\\\\\\
datablock ItemData(TrapArrowItem)
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
	uiName = "Trap Brick: Arrows";
	iconName = "";
	doColorShift = true;
	colorShiftColor = "0.000 0.200 0.640 1.000";
	
	 // Dynamic properties defined by the scripts
	image = "";
	canDrop = false;
};


function TrapArrowItem::onPickup(%this, %item, %obj, %amount)
{
	//Nothing!
}

function TrapArrowItem::onAdd(%this,%item)
{
	parent::onAdd(%this,%item);
	%item.projectile = arrowProjectile;
	%item.delay = 500;
	%item.isTrap = 1;
	%item.ontime = "1";
	%item.offtime = 1;
	%item.NORMALontime = %item.ontime;
	%item.NORMALofftime = %item.offtime;
	schedule(10,0,"trapCheck",%item);
	%item.sched = schedule(%this.delay,0,"trapFire",%item);
}

function TrapArrowItem::onRemove(%this,%item)
{
	%item.spawnbrick.getGroup().client.trapSpawns--;
}

////////TRAP BRICK: TURRET\\\\\\\\
datablock ItemData(TrapTurretItem)
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
	uiName = "Trap Brick: Turret";
	iconName = "";
	doColorShift = true;
	colorShiftColor = "0.000 0.200 0.640 1.000";
	
	 // Dynamic properties defined by the scripts
	image = "";
	canDrop = false;
};


function TrapTurretItem::onPickup(%this, %item, %obj, %amount)
{
	//Nothing!
}

function TrapTurretItem::onAdd(%this,%item)
{
	parent::onAdd(%this,%item);
	%item.projectile = gunProjectile;
	%item.delay = 500;
	%item.isTrap = 1;
	%item.ontime = "1";
	%item.offtime = 1;
	%item.NORMALontime = %item.ontime;
	%item.NORMALofftime = %item.offtime;
	schedule(10,0,"trapCheck",%item);
	%item.sched = schedule(%this.delay,0,"trapFire2",%item);
}

function TrapTurretItem::onRemove(%this,%item)
{
	%item.spawnbrick.getGroup().client.trapSpawns--;
}

////////TRAP BRICK: TRIGGER\\\\\\\\
datablock ItemData(TrapTriggerItem)
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
	uiName = "Trap Brick: Trigger";
	iconName = "";
	doColorShift = true;
	colorShiftColor = "0.000 0.200 0.640 1.000";
	
	 // Dynamic properties defined by the scripts
	image = "";
	canDrop = false;
};

function TrapTriggerItem::onPickup(%this,%item,%amount)
{
}

function TrapTriggerItem::onAdd(%this,%item)
{
	Parent::onAdd(%this,%item);
	%item.isTrigger = 1;
	schedule(9,0,triggerMake,%item,TrapTriggerTrigger);
}

datablock TriggerData(TrapTriggerTrigger)
{
 tickPeriodMS = 0;
};

function TrapTriggerTrigger::onTickTrigger(%this,%trigger)
{
 if(%trigger.activatedlist $= ""){return;}
 	%item = %trigger.spawnbrick.item;
	trapTrig(%item);
}

function TrapTriggerTrigger::onEnterTrigger(%this,%trigger,%obj)
{
	%trigger.activatedlist = %obj SPC %trigger.activatedlist;
}

function TrapTriggerTrigger::onLeaveTrigger(%this,%trigger,%obj)
{
	%trigger.activatedlist = strReplace(%trigger.activatedlist,%obj @ " ","");
}

function TrapTriggerItem::onRemove(%this,%item)
{
	TrapTrigDelete(%item);
	if(isObject(%item.spawnbrick.traptrigger)){%item.spawnbrick.traptrigger.delete();}
}

////////TRAP BRICK: LASER\\\\\\\\
datablock ItemData(TrapLaserItem)
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
	uiName = "Trap Brick: Laser Trigger";
	iconName = "";
	doColorShift = true;
	colorShiftColor = "0.000 0.200 0.640 1.000";
	
	 // Dynamic properties defined by the scripts
	image = "";
	canDrop = false;
};

datablock ParticleData(LaserParticleB)
{
	textureName          = "base/data/particles/dot";
	dragCoefficient      = 0.0;
	gravityCoefficient   = 0.0;
	inheritedVelFactor   = 0.0;
	windCoefficient      = 0;
	constantAcceleration = 0;
	lifetimeMS           = 120;
	lifetimeVarianceMS   = 0;
	spinSpeed     = 0;
	spinRandomMin = -90.0;
	spinRandomMax =  90.0;
	useInvAlpha   = false;

	colors[0]	= "1.0 0.0 0.0 1.0";
	colors[1]	= "1.0 0.0 0.0 1.0";
	colors[2]	= "1.0 0.0 0.0 0.0";

	sizes[0]	= 0.1;
	sizes[1]	= 0.1;
	sizes[2]	= 0.1;

	times[0]	= 0.0;
	times[1]	= 0.9;
	times[2]	= 1.0;
};

datablock ParticleEmitterData(LaserEmitterB)
{
   ejectionPeriodMS = 8;
   periodVarianceMS = 0;
   ejectionVelocity = 0.0;
   ejectionOffset   = 0.0;
   velocityVariance = 0.0;
   thetaMin         = 0;
   thetaMax         = 0;
   phiReferenceVel  = 0;
   phiVariance      = 0;
   overrideAdvance = false;

   //lifetimeMS = 5000;
   particles = LaserParticleB;   

	uiName = "";
};

datablock ProjectileData(laserProjectile : arrowprojectile)
{
	muzzleVelocity = 10;
	lifetime = 2000;
	gravitymod = 0;
	directDamage        = 0.0;
	directDamageType    = $DamageType::FireDirect;
	projectileShapeName = "./shapes/blank.dts";
	radiusDamage        = 0;
	damageRadius        = 0;
	radiusDamageType    = $DamageType::FireDirect;
	explosion = "";
   	particleEmitter     = LaserEmitterB;

};

datablock ProjectileData(alarmProjectile : arrowprojectile)
{
	muzzleVelocity = 10;
	lifetime = 2000;
	gravitymod = 0;
	directDamage        = 0.0;
	directDamageType    = $DamageType::FireDirect;
	projectileShapeName = "./shapes/blank.dts";
	radiusDamage        = 0;
	damageRadius        = 0;
	radiusDamageType    = $DamageType::FireDirect;
	explosion = "";
   	particleEmitter     = AlarmEmitter;

};

function laserProjectile::onCollision(%this,%a,%b,%c,%d,%e,%f,%g,%h)
{
	Parent::onCollision(%this,%a,%b,%c,%d,%e,%f,%g,%h);
	if(%b.getClassName() $= "Player")
 	{
	  if(!%item.checkitem)
	  {
	   %p = new (Projectile)() {
	      dataBlock        = alarmProjectile;
	      initialVelocity  = "0 0 0";
	      initialPosition  = vectorAdd(%a.sourceObject.spawnbrick.position,"0 0 1");
	      sourceObject     = %b;
	      sourceSlot       = 0;
	      client           = %a.client;
	      minigame = %a.client.minigame; 
	   };
	  }
	   %item = %a.sourceObject;
	    trapTrig(%item);
	}
} 

function TrapLaserItem::onPickup(%this, %item, %obj, %amount)
{
	//Nothing!
}

function TrapLaserItem::onAdd(%this,%item)
{
	parent::onAdd(%this,%item);
	%item.projectile = laserProjectile;
	%item.delay = 50;
	%item.isTrap = 1;
	%item.isTrigger = 1;
	%item.ontime = 50;
	%item.offtime = 0;
	%item.NORMALontime = %item.ontime;
	%item.NORMALofftime = %item.offtime;
	%item.onstate = 0;
	%item.onAmount = 0;
	%item.sched = schedule(%this.delay,0,"trapFire",%item,1);
}

function TrapLaserItem::onRemove(%this,%item)
{
 TrapTrigDelete(%item);
}

////////TRAP BRICK: WEAK BRICK\\\\\\\\
datablock ItemData(TrapWeakItem)
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
	uiName = "Trap Brick: Weak Brick";
	iconName = "";
	doColorShift = true;
	colorShiftColor = "0.000 0.200 0.640 1.000";
	
	 // Dynamic properties defined by the scripts
	image = "";
	canDrop = false;
};

function TrapWeakItem::onPickup(%this,%item,%amount)
{
}

function TrapWeakItem::onAdd(%this,%item)
{
	Parent::onAdd(%this,%item);
	schedule(10,0,"trapCheck",%item);
	schedule(9,0,triggerMake,%item,TrapWeakTrigger);
}

datablock TriggerData(TrapWeakTrigger)
{
 tickPeriodMS = 0;
};

function TrapWeakTrigger::onTickTrigger(%this,%trigger)
{
 //Nothing
}

function CheckWeak(%this,%trigger)
{
 	%item = %trigger.spawnbrick.item;
	if(%item.isExp){return;}
	%item.isExp = 0;
	trapExp(%item);
	%item.onTime = 1;
}

function TrapWeakTrigger::onEnterTrigger(%this,%trigger,%obj)
{
	if(%obj.getClassName() $= "Player"){
	%trigger.activatedlist = %obj SPC %trigger.activatedlist;schedule(%trigger.spawnbrick.itemrespawntime,0,checkWeak,%this,%trigger);}
	
}

function TrapWeakTrigger::onLeaveTrigger(%this,%trigger,%obj)
{
	if(%obj.getClassName() $= "Player"){
	%trigger.activatedlist = strReplace(%trigger.activatedlist,%obj @ " ","");}
}

function TrapWeakItem::onRemove(%this,%item)
{
			if(isObject(%item.spawnbrick.traptrigger)){%item.spawnbrick.traptrigger.delete();}
}

////////TRAP BRICK: SPIKE\\\\\\\\
datablock ItemData(TrapSpikeItem)
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
	uiName = "Trap Brick: Spike";
	iconName = "";
	doColorShift = true;
	colorShiftColor = "0.000 0.200 0.640 1.000";
	
	 // Dynamic properties defined by the scripts
	image = "";
	canDrop = false;
};

datablock ProjectileData(spikeProjectile)
{
   projectileShapeName = "./shapes/arrow.dts";

   directDamage        = 60;
   directDamageType    = $DamageType::ArrowDirect;

   radiusDamage        = 0;
   damageRadius        = 0;
   radiusDamageType    = $DamageType::ArrowDirect;

   explosion           = "";
   impactImpulse	   = 500;
   verticalImpulse	   = 2000;
   particleEmitter     = "";

   muzzleVelocity      = 2;
   velInheritFactor    = 1;

   armingDelay         = 0;
   lifetime            = 500;
   fadeDelay           = 3500;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = 0;
   gravityMod = 0.1;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";
};

function TrapSpikeItem::onPickup(%this, %item, %obj, %amount)
{
	//Nothing!
}

function TrapSpikeItem::onAdd(%this,%item)
{
	parent::onAdd(%this,%item);
	%item.projectile = spikeProjectile;
	%item.delay = 10;
	%item.isTrap = 1;
	%item.ontime = 200;
	%item.offtime = 200;
	%item.NORMALontime = %item.ontime;
	%item.NORMALofftime = %item.offtime;
	%item.onstate = 0;
	%item.onAmount = 0;
	schedule(10,0,"trapCheck",%item);
	%item.sched = schedule(%this.delay,0,"trapFire",%item,1);
}

function TrapSpikeItem::onRemove(%this,%item)
{
	%item.spawnbrick.getGroup().client.trapSpawns--;
}

////////TRAP BRICK: DEATH\\\\\\\\
datablock ItemData(TrapDeathItem)
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
	uiName = "Trap Brick: Death";
	iconName = "";
	doColorShift = true;
	colorShiftColor = "0.000 0.200 0.640 1.000";
	
	 // Dynamic properties defined by the scripts
	image = "";
	canDrop = false;
};

function TrapDeathItem::onPickup(%this, %item, %obj, %amount)
{
	//Nothing!
}

function TrapDeathItem::onRemove(%this,%item)
{
	%item.spawnbrick.getGroup().client.trapSpawns--;
	%item.spawnbrick.traptrigger.delete();	
}

function TrapDeathItem::onAdd(%this,%item)
{
	Parent::onAdd(%this,%item);
	schedule(10,0,trapCheck,%item);
	schedule(9,0,triggerMake,%item,TrapDeathTrigger);
	%item.isTrigBrick = 1;
	%item.isTrigOn = 1;
}

datablock TriggerData(TrapDeathTrigger)
{
 tickPeriodMS = 0;
};

function TrapDeathTrigger::onTickTrigger(%this,%trigger)
{
 if(%trigger.spawnbrick.item.isTrigOn == 0){return;}
 %trigger.minigame = %trigger.spawnbrick.getGroup().client.minigame;
 %trigger.player = %trigger.spawnbrick.getGroup().client.player;
 for(%i=0;getWord(%trigger.activatedlist,%i) !$= "";%i++)
 {
  %obj = getWord(%trigger.activatedlist,%i);
 if(strStr(%obj.getClassName(),"Vehicle") !$= "-1"){%obj.client = %obj.brickgroup.client;}
 if((!isObject(%obj.client) || %obj.client.minigame == %trigger.minigame) && isObject(%obj.client.minigame))
 {
  if(strStr(%obj.getClassName(),"Player") !$= -1)
  Armor::damage(%obj.getDataBlock(),%obj, %trigger.player, %obj.position, 10000, $DamageType::Suicide);
  if(strStr(%obj.getClassName(),"Vehicle") !$= -1)
  Vehicle::damage(%obj, %trigger.player, %obj.position, 10000, 1);
 }
 }
}

function TrapDeathTrigger::onEnterTrigger(%this,%trigger,%obj)
{
	if(%obj.getClassName() $= "Player" || %obj.getClassName() $= "Vehicle" || %obj.getClassName() $= "AIPlayer"){
	%trigger.activatedlist = %obj SPC %trigger.activatedlist;}
	
}

function TrapDeathTrigger::onLeaveTrigger(%this,%trigger,%obj)
{
	if(%obj.getClassName() $= "Player" || %obj.getClassName() $= "Vehicle" || %obj.getClassName() $= "AIPlayer"){
	%trigger.activatedlist = strReplace(%trigger.activatedlist,%obj @ " ","");}
}

////////TRAP BRICK: IMPULSE\\\\\\\\
datablock ItemData(TrapPulseItem)
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
	uiName = "Trap Brick: Impulse";
	iconName = "";
	doColorShift = true;
	colorShiftColor = "0.000 0.200 0.640 1.000";
	
	 // Dynamic properties defined by the scripts
	image = "";
	canDrop = false;
};

function TrapPulseItem::onPickup(%this, %item, %obj, %amount)
{
	//Nothing!
}

function TrapPulseItem::onRemove(%this,%item)
{
	%item.spawnbrick.getGroup().client.trapSpawns--;
	%item.spawnbrick.traptrigger.delete();	
}

function TrapPulseItem::onAdd(%this,%item)
{
	Parent::onAdd(%this,%item);
	schedule(10,0,trapCheck,%item);
	schedule(9,0,triggerMake,%item,TrapPulseTrigger);
	%item.isTrigBrick = 1;
	%item.isTrigOn = 1;
}

datablock TriggerData(TrapPulseTrigger)
{
 tickPeriodMS = 0;
};

function TrapPulseTrigger::onTickTrigger(%this,%trigger)
{
 if(%trigger.spawnbrick.item.isTrigOn == 0){return;}
 %trigger.minigame = %trigger.spawnbrick.getGroup().client.minigame;
 %trigger.player = %trigger.spawnbrick.getGroup().client.player;
 for(%i=0;getWord(%trigger.activatedlist,%i) !$= "";%i++)
 {
  %obj = getWord(%trigger.activatedlist,%i);
 if((!isObject(%obj.client) || %obj.client.minigame == %trigger.minigame) && isObject(%obj.client.minigame))
 {
  %obj.setVelocity(VectorAdd(%obj.getVelocity(),"0 0 35"));
 }
 }
}

function TrapPulseTrigger::onEnterTrigger(%this,%trigger,%obj)
{
	if(%obj.getClassName() $= "Player" || %obj.getClassName() $= "AIPlayer"){
	%trigger.activatedlist = %obj SPC %trigger.activatedlist;}
	
}

function TrapPulseTrigger::onLeaveTrigger(%this,%trigger,%obj)
{
	if(%obj.getClassName() $= "Player" || %obj.getClassName() $= "AIPlayer"){
	%trigger.activatedlist = strReplace(%trigger.activatedlist,%obj @ " ","");}
}

////////TRAP BRICK: EXPLODE\\\\\\\\
datablock ItemData(TrapExpItem)
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
	uiName = "Trap Brick: Explode";
	iconName = "";
	doColorShift = true;
	colorShiftColor = "0.000 0.200 0.640 1.000";
	
	 // Dynamic properties defined by the scripts
	image = "";
	canDrop = false;
};

function TrapExpItem::onPickup(%this, %item, %obj, %amount)
{
	//Nothing!
}

function TrapExpItem::onAdd(%this,%item)
{
	parent::onAdd(%this,%item);
	%item.isTrap = 1;
	schedule(10,0,"trapCheck",%item);
	%item.sched = schedule(5000,0,"trapExp",%item);
}

function TrapExpItem::onRemove(%this,%item)
{
	%item.spawnbrick.getGroup().client.trapSpawns--;
  %item.isExp = 0;
  %item.spawnbrick.setcolor(%item.spawnbrick.realcolor);
  %item.spawnbrick.setcolliding(1);
  %item.spawnbrick.setraycasting(1);
}

////////TRAP BRICK: CLICK TRIGGER\\\\\\\\
datablock ItemData(TrapClickTrigItem)
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
	uiName = "Trap Brick: Click Trigger";
	iconName = "";
	doColorShift = true;
	colorShiftColor = "0.000 0.200 0.640 1.000";
	
	 // Dynamic properties defined by the scripts
	image = "";
	canDrop = false;
};

function TrapClickTrigItem::onPickup(%this,%item,%amount)
{
}

function TrapClickTrigItem::onAdd(%this,%item)
{
	Parent::onAdd(%this,%item);
	%item.isTrigger = 1;
}

function TrapClickTrigItem::onRemove(%this,%item)
{
	TrapTrigDelete(%item);
}

////////TRAP BRICKS: SUPPORT FUNCTIONS\\\\\\\\
function trapCheck(%this)
{
	%client = %this.spawnbrick.getGroup().client;
	%client.TrapSpawns++;
	if(%client.TrapSpawns > $Pref::Server::TrapSpawns){%this.delete();commandtoclient(%client,'centerprint',"\c5No more than \c3" @ 	$Pref::Server::TrapSpawns @ "\c5 Trap Spawns at once.",2,2,2);return;}
	if($Pref::Server::TrapAdmin && !%client.isSuperAdmin){%this.delete();commandtoclient(%client,'centerprint',"\c5You need to be a \c3Super Admin\c5 to place trap bricks.",2,2,2);return;}
}

function trapExp(%item)
{
 if(!isObject(%item.spawnbrick) || !isObject(%item)){return;}
 if(!%item.isExp)
 {
  %item.isExp = 1;
  %item.spawnbrick.realcolor = %item.spawnbrick.getColorID();
  %item.spawnbrick.setcolor(63);
  %item.spawnbrick.setcolliding(0);
  %item.spawnbrick.setraycasting(0);
  %i = new FXdtsBrick(){datablock = %item.spawnbrick.getDatablock();position = %item.spawnbrick.position;angleID = %item.spawnbrick.angleID;};
  %i.setColor(%item.spawnbrick.realcolor);
  %i.onPlant();
  %i.killbrick();
  %item.sched = schedule(%item.spawnbrick.itemrespawntime,0,trapExp,%item);
 }
 else
 {
  %item.isExp = 0;
  %item.spawnbrick.setcolor(%item.spawnbrick.realcolor);
  %item.spawnbrick.setcolliding(1);
  %item.spawnbrick.setraycasting(1);
  if(!%item.onTime){%item.sched = schedule(%item.spawnbrick.itemrespawntime*2,0,trapExp,%item);}
 }
}

function trapFire(%this,%on)
{
	%item = %this;
	if(!isObject(%this)){return;}
	%direction = %this.spawnBrick.itemPosition;
	if(%this.ontime)
	if(%this.onState)
	{
		%item.onAmount++;
		if(%item.onAmount >= %item.offTime){%item.onState = 0;%item.onAmount = 0;}
		%item.sched = schedule(%this.delay,0,"trapFire",%this);
		return;
	}
	else
	{
		%item.onAmount++;
		if(%item.onAmount >= %item.onTime){%item.onState = 1;%item.onAmount = 0;}
	}
	%projectile = %this.projectile;

	   %muzzleVector = %this.position;
	   switch(%direction)
	   {
		case 0: //Up
			%vectorAdd = vectorScale("0 0 1",%projectile.muzzleVelocity);
		case 1: //Down
			%vectorAdd = vectorScale("0 0 -1",%projectile.muzzleVelocity);
		case 2: //North
			%vectorAdd = vectorScale("0 1 0",%projectile.muzzleVelocity);
		case 3: //East
			%vectorAdd = vectorScale("1 0 0",%projectile.muzzleVelocity);
		case 4: //South
			%vectorAdd = vectorScale("0 -1 0",%projectile.muzzleVelocity);
		case 5: //West
			%vectorAdd = vectorScale("-1 0 0",%projectile.muzzleVelocity);
                   }
	   %velocity = %vectorAdd;

	   %p = new (Projectile)() {
	      dataBlock        = %projectile;
	      initialVelocity  = %velocity;
	      initialPosition  = %this.position;
	      sourceObject     = %this;
	      sourceSlot       = 0;
	      client           = %this.spawnbrick.getGroup().client;
	      minigame = %this.spawnbrick.getGroup().client.minigame; 
	   };
	   MissionCleanup.add(%p);
	%this.minigame = %this.spawnbrick.getGroup().client.minigame; 
	%item.sched = schedule(%this.delay,0,"trapFire",%this);
}

function trapFire2(%this,%on)
{
	%item = %this;
	if(!isObject(%this)){return;}
	if(%this.ontime)
	if(%this.onState)
	{
		%item.onAmount++;
		if(%item.onAmount >= %item.offTime){%item.onState = 0;%item.onAmount = 0;}
		%item.sched = schedule(%this.delay,0,"trapFire2",%this);
		return;
	}
	else
	{
		%item.onAmount++;
		if(%item.onAmount >= %item.onTime){%item.onState = 1;%item.onAmount = 0;}
	}
	%projectile = %this.projectile;
	%this.minigame = %this.spawnbrick.getGroup().client.minigame; 
	%item.sched = schedule(%this.delay,0,"trapFire2",%this);
   	InitContainerRadiusSearch(%this.spawnbrick.position, 20, $TypeMasks::playerObjectType);

	   %found = 0;
	   %mindist = 10000;
	%this.minigame = %this.spawnbrick.getGroup().client.minigame; 
	   while ((%targetObject = containerSearchNext()) != 0) {
		if(vectorDist(%targetObject.position,%this.spawnbrick.position)<%mindist && %targetObject.client.minigame $= %this.minigame)
		{
			%found = %targetObject;
			%mindist = vectorDist(%targetObject.position,%this.spawnbrick.position);
		}
	   }
	   if(%found)
	   {	   
	     %velocity = vectorScale(vectorNormalize(vectorSub(vectorAdd(%found.position,"0 0 2"),%this.spawnbrick.position)),%projectile.muzzleVelocity);
	    %p = new (Projectile)() {
	       dataBlock        = %projectile;
	       initialVelocity  = %velocity;
	       initialPosition  = %this.position;
	       sourceObject     = %this;
	       sourceSlot       = 0;
	       client           = %this.spawnbrick.getGroup().client;
	       minigame = %this.spawnbrick.getGroup().client.minigame; 
	    };
	    MissionCleanup.add(%p);
	    return;
	   }
}

package WrenchOverRide
{
	function WrenchProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
	{
		%client = %obj.client;
		%brickclient = %col.getgroup();
		if(%col.getClassName() !$= "fxDtsBrick"){Parent::onCollision(%this,%obj,%col,%fade,%pos,%normal);return;}
		if(!getTrustLevel(%client,%brickClient)){commandtoclient(%client,'centerprint',%brickclient.name SPC "doesn't trust you enough to do that.",2,2);return;}
		%item = %col.item;
		if(!%obj.client.isTriggering && %item.isTrigger)
		{
			%item.trap[0] = -1;
			%item.trapnum = -1;
			%obj.client.isTriggering = %item;
			messageclient(%obj.client,'',"\c5Traps cleared - hit trap spawns with the wrench to connect them to the trigger.");
			glow(%item.spawnbrick);
			return;
		}
		if(%obj.client.isTriggering !$= "" && ((!isObject(%obj.client.isTriggering)) || (%obj.client.isTriggering $= %item) || (!%item.isTrap && !%item.isTrigger && !%item.isTrigBrick)))
		{
			%obj.client.isTriggering = "";
			messageclient(%obj.client,'',"\c5Ended trap setting.");
			return;
		}
		if(%obj.client.isTriggering)
		{
		 if(%item.isTrap)
		 {
			%obj.client.isTriggering.trap[%obj.client.isTriggering.trapnum++] = %item;
			%item.ontime = 10000 / %item.delay;
			%item.offtime = 200000;
			%item.onstate = 1;
			%item.onAmount = 0;
			if(%item.getDatablock().getName() $= "trapExpItem"){cancel(%item.sched);}
			glow(%item.spawnbrick);
			glow(%obj.client.isTriggering.spawnbrick);
			messageclient(%obj.client,'',"\c5Trap connected to brick.");
			return;
		 }
		 if(%item.isTrigBrick)
		 {
			%obj.client.isTriggering.trap[%obj.client.isTriggering.trapnum++] = %item;
			%item.isTrigOn = 0;
			glow(%item.spawnbrick);
			glow(%obj.client.isTriggering.spawnbrick);
			messageclient(%obj.client,'',"\c5Trap connected to brick.");
			return;
		 }
		}
		return Parent::onCollision(%this,%obj,%col,%fade,%pos,%normal);
	}
function Player::activateStuff(%this)
{
   Parent::activateStuff(%this);
   %mouseVec = %this.getEyeVector();
   %cameraPoint = %this.getEyePoint();
   %selectRange = 8;
   %mouseScaled = VectorScale(%mouseVec, %selectRange);
   %rangeEnd = VectorAdd(%cameraPoint, %mouseScaled);

   %searchMasks = $TypeMasks::VehicleObjectType | $TypeMasks::FXBrickObjectType;

   %player = %client.player;
   if($firstPerson)
   {
	  %scanTarg = ContainerRayCast (%cameraPoint, %rangeEnd, %searchMasks, %player);
   }
   else //3rd person - player is selectable in this case
   {
	  %scanTarg = ContainerRayCast (%cameraPoint, %rangeEnd, %searchMasks);
   }
   if(!isObject(firstWord(%scanTarg))){return;}
    if(strStr(firstWord(%scanTarg).getClassName(),"fxDTSBrick") !$= -1 && isObject(firstWord(%scanTarg).item) && firstWord(%scanTarg).item.getDatablock().getname() $= "trapClickTrigItem")
    {
     %item = firstWord(%scanTarg).item;
     TrapTrig(%item);
     glow(%item.spawnbrick);
    }
}

};
activatePackage("wrenchoverride");

function glow(%brick)
{
 %brick.setColorFX(3);
 schedule(2000,0,glow2,%brick);
}

function glow2(%brick)
{
 %brick.setColorFX(0);
}

function TrapActivate(%item)
{
	if(%item.isTrap)
	{
		%item.ontime = 5000 / %item.delay;
		%item.onstate = 0;
	}
	if(%item.isTrigBrick)
	{
		%item.isTrigOn = 1;
		schedule(100,0,trigoff,%item);
	}
	if(%item.getDatablock().getname() $= "TrapExpItem" && !%item.isExp){cancel(%item.sched);TrapExp(%item);}
}
function trigoff(%item){%item.isTrigOn = 0;}

function TrapTrig(%item)
{
 for(%i=0;isObject(%item.trap[%i]);%i++)
 {
  TrapActivate(%item.trap[%i]);
 }
}

function TrapTrigDelete(%item)
{
 if(!isObject(%item.trap[0])){return;}
 for(%i=0;%i<%item.trapnum+1;%i++)
 {
	%item.trap[%i].ontime = %item.trap[%i].NORMALontime;
	%item.trap[%i].offtime = %item.trap[%i].NORMALofftime;
	%item.trap[%i].onstate = 0;
	%item.trap[%i].isTrigOn = 1;
	if(%item.trap[%i].getDatablock().getName() $= "TrapExpItem"){%item.trap[%i].sched = schedule(0,0,trapexp,%item.checkitem);}
 }
}

function TriggerMake(%item,%trig)
{
	%dist1 = getWords(%item.spawnbrick.getWorldBox(),0,2);
	%dist2 = getWords(%item.spawnbrick.getWorldBox(),3,6);
	%x = mAbs(getWord(%dist1,0) - getWord(%dist2,0));
	%y = mAbs(getWord(%dist1,1) - getWord(%dist2,1));
	%z = mAbs(getWord(%dist1,2) - getWord(%dist2,2));
	%pos = %item.spawnbrick.position;
	%pos = getWord(%pos,0) SPC getWord(%pos,1) SPC getWord(%pos,2);
	%p = new Trigger()
	{
		datablock = %trig;
		position = vectorAdd(%pos,-%x/2-0.1 SPC %y/2+0.1 SPC -%z/2-0.1);
		polyhedron = "0.0000000 0.0000000 0.0000000 1.0000000 0.0000000 0.0000000 0.0000000 -1.0000000 0.0000000 0.0000000 0.0000000 1.0000000";
		scale = %x+0.2 SPC %y+0.2 SPC %z + 0.5;
		rotation = "1 0 0 0";
	};
	%item.spawnbrick.traptrigger = %p;
	%p.spawnbrick = %item.spawnbrick;
	%p.player = %item.spawnbrick.getGroup().client.player;
	MissionCleanup.add(%p);
}