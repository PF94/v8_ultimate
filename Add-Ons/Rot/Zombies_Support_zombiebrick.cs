//zombiebrick.cs
datablock AudioProfile(zombiebrickDrawSound)
{
   filename    = "./sound/zombiebrickDraw.wav";
   description = AudioClosest3d;
   preload = true;
};
datablock AudioProfile(zombiebrickHitSound)
{
   filename    = "./sound/zombiebrickHit.wav";
   description = AudioClosest3d;
   preload = true;
};


//effects
datablock ParticleData(zombiebrickExplosionParticle)
{
   dragCoefficient      = 2;
   gravityCoefficient   = 1.0;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   spinRandomMin = -90;
   spinRandomMax = 90;
   lifetimeMS           = 500;
   lifetimeVarianceMS   = 300;
   textureName          = "base/data/particles/chunk";
   colors[0]     = "0.7 0.7 0.9 0.9";
   colors[1]     = "0.9 0.9 0.9 0.0";
   sizes[0]      = 0.5;
   sizes[1]      = 0.25;
};

datablock ParticleEmitterData(zombiebrickExplosionEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   ejectionVelocity = 8;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 60;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "zombiebrickExplosionParticle";
};

datablock ExplosionData(zombiebrickExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 500;

   soundProfile = zombiebrickHitSound;

   particleEmitter = zombiebrickExplosionEmitter;
   particleDensity = 10;
   particleRadius = 0.2;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = false;
   camShakeFreq = "20.0 22.0 20.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.5;
   camShakeRadius = 10.0;

   // Dynamic light
   lightStartRadius = 3;
   lightEndRadius = 0;
   lightStartColor = "00.0 0.2 0.6";
   lightEndColor = "0 0 0";
};


//projectile
//AddDamageType("zombiebrick",   '<bitmap:add-ons/ci/zombiebrick> %1',    '%2 <bitmap:add-ons/ci/zombiebrick> %1',1,1);
datablock ProjectileData(zombiebrickProjectile)
{
   //projectileShapeName = "~/shapes/arrow.dts";
   directDamage        = 0;
   directDamageType  = $DamageType::zombiebrick;
   radiusDamageType  = $DamageType::zombiebrick;
   explosion           = zombiebrickExplosion;
   //particleEmitter     = as;

   muzzleVelocity      = 30;
   velInheritFactor    = 1;

   armingDelay         = 0;
   lifetime            = 100;
   fadeDelay           = 70;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(zombiebrickImage)
{
   // Basic Item properties
   shapeFile = "./nogun.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   //eyeOffset = "0.1 0.2 -0.55";

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = true;

   eyeOffset = "0 0 -1";

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   //item = zombiebrickItem;
   ammo = " ";
   //projectile = zombiebrickProjectile;
   //projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = false;
   doRetraction = false;
   //raise your arm up or not
   armReady = false;

   //casing = " ";
   doColorShift = true;
   colorShiftColor = "0.471 0.471 0.471 1.000";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.8;
	stateTransitionOnTimeout[0]      = "Ready";
	//stateSound[0]                    = zombiebrickDrawSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "PreFire";
	stateAllowImageChange[1]         = true;

	stateName[2]			= "PreFire";
	stateScript[2]                  = "onPreFire";
	stateAllowImageChange[2]        = false;
	stateTimeoutValue[2]            = 0.8;
	stateTransitionOnTimeout[2]     = "Fire";

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "CheckFire";
	stateTimeoutValue[3]            = 0.8;
	stateFire[3]                    = true;
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateScript[3]                  = "onFire";
	stateWaitForTimeout[3]		= true;
	//stateTransitionOnTriggerUp[3]	= "StopFire";

	stateName[4]			= "CheckFire";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "Fire";

	
	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.8;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	stateSequence[5]                = "StopFire";
	stateScript[5]                  = "onStopFire";


};

function zombiebrickImage::onFire(%this, %obj, %slot)
{
	//messageAll( 'MsgClient', 'zombiebrick prefired!!!');
	//Parent::onFire(%this, %obj, %slot);
	//%obj.playthread(2, armattack);
	////echo((%obj);
	////echo((%obj.dump());
	zombiecast(%obj,Brick);
}

function zombiebrickProjectile::onCollision(%this, %obj, %col, %fade, %pos, %norm)
{
  if(%col.getClassName() $= "FxDtsBrick" )
	{
			if(%col.isunbreakable != 1 && $zombie::brickdestruction == 1){
				%col.killbrick();
      }
	}

}

datablock ProjectileData(zombieJumpcheckProjectile)
{
   //projectileShapeName = "~/shapes/arrow.dts";
   directDamage        = 0;
   directDamageType  = $DamageType::zombiebrick;
   radiusDamageType  = $DamageType::zombiebrick;
   //explosion           = zombiebrickExplosion;
   //particleEmitter     = as;

   muzzleVelocity      = 55;
   velInheritFactor    = 1;

   armingDelay         = 0;
   lifetime            = 200;
   fadeDelay           = 70;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";
};
//ZombieJumpCheck
datablock ShapeBaseImageData(zombieJumpcheckImage)
{
   // Basic Item properties
   shapeFile = "./nogun.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 2;
   offset = "0 0 0";
   //eyeOffset = "0.1 0.2 -0.55";

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = false;

   eyeOffset = "0 0 -1.4";

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   //item = zombiebrickItem;
   ammo = " ";
 //  projectile = zombieJumpcheckProjectile;
 //  projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = false;
   doRetraction = false;
   //raise your arm up or not
   armReady = false;

   //casing = " ";
   doColorShift = true;
   colorShiftColor = "0.471 0.471 0.471 1.000";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 1;
	stateTransitionOnTimeout[0]      = "Ready";
	//stateSound[0]                    = zombiebrickDrawSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "PreFire";
	stateAllowImageChange[1]         = true;

	stateName[2]			= "PreFire";
	stateScript[2]                  = "onPreFire";
	stateAllowImageChange[2]        = false;
	stateTimeoutValue[2]            = 1;
	stateTransitionOnTimeout[2]     = "Fire";

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "CheckFire";
	stateTimeoutValue[3]            = 1;
	stateFire[3]                    = true;
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateScript[3]                  = "onFire";
	stateWaitForTimeout[3]		= true;
	//stateTransitionOnTriggerUp[3]	= "StopFire";

	stateName[4]			= "CheckFire";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "Fire";

	
	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 1;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	stateSequence[5]                = "StopFire";
	stateScript[5]                  = "onStopFire";


};

function zombieJumpcheckImage::onFire(%this, %obj, %slot)
{
	zombiecast(%obj,Jump);
}

function zombieJumpcheckImage::onStopFire(%this, %obj, %slot)
{	
	//%obj.playthread(2, root);
	//messageAll( 'MsgClient', 'stopfire');
}
function zombieJumpcheckProjectile::onCollision(%this, %obj, %col, %fade, %pos, %norm)
{
	//%search = containercastray(%start,%end,%mask,%exemptobject);
  if(%col.getClassName() $= "FxDtsBrick")
      {
			%check = getbrickheight(%col);
			if(%check <= 12){
			%obj.sourceobject.setimagetrigger(2,1);
			%obj.sourceobject.schedule(500,setimagetrigger,2,0);
			}
      }
	 if(%col.getclassname() $= "AIPlayer" || %col.getClassName() $= "WheeledVehicle"){
			////echo((wtfaaaaaaaaaaaaaaa);
			%obj.sourceobject.setimagetrigger(2,1);
			%obj.sourceobject.schedule(500,setimagetrigger,2,0);
	}

}
//Donothingimage
datablock ShapeBaseImageData(DonothingImage)
{
   // Woahthis does nothing
   shapeFile = "./nogun.dts";

};

function Getbrickheight(%brick,%bot)
{
	//%blah = sex.getdatablock().brickfile; $file = new fileobject(); $file.openforread(%blah); //echo(($file.readline());
	%loc = %brick.getdatablock().brickfile;
 // Read the name
    %file = new FileObject();
    %file.openForRead(%loc);
    %xyz = %file.readLine();
	%zcheck = getword(%xyz,2);
    %file.close();
    %file.delete();
	return %zcheck;
// notes: There are also openForRead,openForAppend, readLine and isEof methods.
}

function Getbrickstacknumber(%brick)
{
	for(%a = 0; %a < 3; %a++){
		//%brick2 = %brick.getupbrick(0);
		%upbrick = %brick.getupbrick(0);
		////echo((%upbrick);
		%ch2 = 0;
		%brick = %upbrick;
		if(%upbrick == 0){
			%ch2 = 1;
			return true;
		}
		else{
			////echo((notyet);
		}
	}

	if(%ch2 == 1){
		return;

	}
	else{
		return false;
	}
}

//zombiecallgun
datablock ShapeBaseImageData(zombiecallImage)
{
   // Basic Item properties
   shapeFile = "./nogun.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   //eyeOffset = "0.1 0.2 -0.55";

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = false;

   eyeOffset = "0 0 -1.4";

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   //item = zombiebrickItem;
   ammo = " ";
   //projectile = zombiecallProjectile;
  // projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = false;
   doRetraction = false;
   //raise your arm up or not
   armReady = false;

   //casing = " ";
   doColorShift = true;
   colorShiftColor = "0.471 0.471 0.471 1.000";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.1;
	stateTransitionOnTimeout[0]      = "Prefire";
	
	stateName[1]                     = "Prefire";
	stateTimeoutValue[1]             = 0.1;
	stateTransitionOnTimeout[1]      = "Prefire";

};
datablock ProjectileData(zombiecallImageProjectile)
{
   //projectileShapeName = "~/shapes/arrow.dts";
   directDamage        = 0;
   directDamageType  = $DamageType::zombiebrick;
   radiusDamageType  = $DamageType::zombiebrick;
   //explosion           = zombiebrickExplosion;
   //particleEmitter     = as;

   muzzleVelocity      = 55;
   velInheritFactor    = 1;

   armingDelay         = 0;
   lifetime            = 200;
   fadeDelay           = 70;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";
};
function zombiecallImage::onPreFire(%this, %obj, %slot)
{
	//messageAll( 'MsgClient', 'zombiebrick prefired!!!');
	//Parent::onFire(%this, %obj, %slot);
	//%obj.playthread(2, armattack);
	////echo((%obj);
	////echo((%obj.dump());
	zomsearch(%obj.client);
	//echo((fcuk);
}
function zombiecallImage::onFire(%this, %obj, %slot)
{
	//messageAll( 'MsgClient', 'zombiebrick prefired!!!');
	//Parent::onFire(%this, %obj, %slot);
	//%obj.playthread(2, armattack);
	////echo((%obj);
	////echo((%obj.dump());
}
function zombiecallImage::onActivate(%this, %obj, %slot)
{
	//messageAll( 'MsgClient', 'zombiebrick prefired!!!');
	//Parent::onFire(%this, %obj, %slot);
	//%obj.playthread(2, armattack);
	
}
function testraycast()
{
	%this = findclientbyname(rot).player;
	%eyeVec = %this.getEyeVector();

%startPos = %this.getEyePoint();
%endPos = VectorAdd(%startPos,vectorscale(%eyeVec,5));

%mask = $TypeMasks::FxBrickObjectType;
%target = ContainerRayCast(%startPos, %endPos, %mask);

if (%target)
{
	//echo((getword(%target,0));
}
else
{
   //echo(("Nothign in your line of sight");
}
}

function zombiecast(%this,%type)
{
	if(%type $= "Brick"){
		//%this = findclientbyname(rot).player;
		%eyeVec = %this.getEyeVector();

		%startPos = %this.getEyePoint();
		%endPos = VectorAdd(%startPos,vectorscale(%eyeVec,3));

		%mask = $TypeMasks::FxBrickObjectType;
		%target = ContainerRayCast(%startPos, %endPos, %mask);

		if (%target)
		{
			//echo((wooo);
			killminigamebrick(getword(%target,0),%this);
		}
	}
	if(%type $= "Jump"){
		//%this = findclientbyname(rot).player;
		%eyeVec = %this.getforwardVector();

		%startPos = vectoradd(%this.getEyePoint(),"0 0 -2");
		
		%endPos = VectorAdd(%startPos,vectorscale(%eyeVec,8));

		%mask = $TypeMasks::FxBrickObjectType | $typemasks::PlayerObjectType;
		%target = ContainerRayCast(%startPos, %endPos, %mask);

		if (%target)
		{
			jumpbrick(getword(%target,0),%this);
		}
	}
}

function killminigamebrick(%col,%sender)
{
  if(%col.getClassName() $= "FxDtsBrick" )
	{
	 // %sender.playthread(2,shiftup);
			if(%col.getdatablock().getvolume() >= 1000)
		{
				//return;
		}
		if($zombie::killallbricks == 1)
		{
			%col.killbrick();
			return;
		}
			if(%col.isunbreakable != 1 && minigamecandamage(%col,%sender) && $zombie::brickdestruction == 1){
				if(%col.zombiedamage <= 0){
				
				%col.killbrick();
				}
				else
				{
				%col.zombiedamage--;
				}
      }
	}
}

function jumpbrick(%col,%obj)
{
  if(%col.getClassName() $= "FxDtsBrick")
      {
			%check = getbrickheight(%col);
			if(%check <= 14){
			%obj.setimagetrigger(2,1);
			%obj.schedule(500,setimagetrigger,2,0);
			}
      }
	 if(%col.getclassname() $= "AIPlayer" || %col.getClassName() $= "WheeledVehicle" || %col.getclassname() $= "Player"){
			%obj.setimagetrigger(2,1);
			%obj.schedule(500,setimagetrigger,2,0);
	}
}