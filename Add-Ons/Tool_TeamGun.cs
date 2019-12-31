//Tgun.cs

//audio
datablock AudioProfile(TgunShot1Sound)
{
   filename    = "./sound/gunShot1.wav";
   description = AudioClose3d;
   preload = true;
};
datablock AudioProfile(bulletHitSound)
{
   filename    = "./sound/arrowHit.wav";
   description = AudioClose3d;
   preload = true;
};


//shell
datablock DebrisData(TgunShellDebris)
{
	shapeFile = "./shapes/gunShell.dts";
	lifetime = 2.0;
	minSpinSpeed = -400.0;
	maxSpinSpeed = 200.0;
	elasticity = 0.5;
	friction = 0.2;
	numBounces = 3;
	staticOnMaxBounce = true;
	snapOnMaxBounce = false;
	fade = true;

	gravModifier = 2;
};


//muzzle flash effects
datablock ParticleData(TgunFlashParticle)
{
	dragCoefficient      = 3;
	gravityCoefficient   = -0.5;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.0;
	lifetimeMS           = 25;
	lifetimeVarianceMS   = 15;
	textureName          = "base/data/particles/star1";
	spinSpeed		= 10.0;
	spinRandomMin		= -500.0;
	spinRandomMax		= 500.0;
	colors[0]     = "0.9 0.9 0.0 0.9";
	colors[1]     = "0.9 0.5 0.0 0.0";
	sizes[0]      = 0.5;
	sizes[1]      = 1.0;

	useInvAlpha = false;
};
datablock ParticleEmitterData(TgunFlashEmitter)
{
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;
   ejectionVelocity = 1.0;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "TgunFlashParticle";
};

datablock ParticleData(TgunSmokeParticle)
{
	dragCoefficient      = 3;
	gravityCoefficient   = -0.5;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.0;
	lifetimeMS           = 525;
	lifetimeVarianceMS   = 55;
	textureName          = "base/data/particles/cloud";
	spinSpeed		= 10.0;
	spinRandomMin		= -500.0;
	spinRandomMax		= 500.0;
	colors[0]     = "0.5 0.5 0.5 0.9";
	colors[1]     = "0.5 0.5 0.5 0.0";
	sizes[0]      = 0.15;
	sizes[1]      = 0.15;

	useInvAlpha = false;
};
datablock ParticleEmitterData(TgunSmokeEmitter)
{
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;
   ejectionVelocity = 1.0;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "TgunSmokeParticle";
};


//bullet trail effects
datablock ParticleData(bulletTrailParticle)
{
	dragCoefficient      = 3;
	gravityCoefficient   = -0.0;
	inheritedVelFactor   = 1.0;
	constantAcceleration = 0.0;
	lifetimeMS           = 525;
	lifetimeVarianceMS   = 55;
	textureName          = "base/data/particles/thinRing";
	spinSpeed		= 10.0;
	spinRandomMin		= -500.0;
	spinRandomMax		= 500.0;
	colors[0]     = "0.3 0.3 0.9 0.4";
	colors[1]     = "0.5 0.5 0.5 0.0";
	sizes[0]      = 0.15;
	sizes[1]      = 0.25;

	useInvAlpha = false;
};
datablock ParticleEmitterData(bulletTrailEmitter)
{
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;
   ejectionVelocity = 0.0;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "bulletTrailParticle";
};


datablock ParticleData(TgunExplosionParticle)
{
	dragCoefficient      = 8;
	gravityCoefficient   = -0.5;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.0;
	lifetimeMS           = 700;
	lifetimeVarianceMS   = 400;
	textureName          = "base/data/particles/cloud";
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	colors[0]     = "0.9 0.9 0.6 0.9";
	colors[1]     = "0.9 0.5 0.6 0.0";
	sizes[0]      = 0.25;
	sizes[1]      = 1.0;

	useInvAlpha = true;
};
datablock ParticleEmitterData(TgunExplosionEmitter)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   ejectionVelocity = 5;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 89;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "TgunExplosionParticle";
};


datablock ParticleData(TgunExplosionRingParticle)
{
	dragCoefficient      = 8;
	gravityCoefficient   = -0.5;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.0;
	lifetimeMS           = 300;
	lifetimeVarianceMS   = 100;
	textureName          = "base/data/particles/star1";
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	colors[0]     = "1 1 0.0 0.9";
	colors[1]     = "0.9 0.0 0.0 0.0";
	sizes[0]      = 0.2;
	sizes[1]      = 0.2;

	useInvAlpha = false;
};
datablock ParticleEmitterData(TgunExplosionRingEmitter)
{
	lifeTimeMS = 50;

   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   ejectionVelocity = 5;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 89;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "TgunExplosionRingParticle";
};

datablock ExplosionData(TgunExplosion)
{
   //explosionShape = "";
	soundProfile = bulletHitSound;

   lifeTimeMS = 150;

   particleEmitter = TgunExplosionEmitter;
   particleDensity = 10;
   particleRadius = 0.2;

   emitter[0] = TgunExplosionRingEmitter;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = true;
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


AddDamageType("Tgun",   '<bitmap:add-ons/ci/gun> %1',    '%2 <bitmap:add-ons/ci/gun> %1',0.5,1);
AddDamageType("Misuse",   '<bitmap:add-ons/ci/blueRibbon> %1',    '%2 <bitmap:add-ons/ci/gun> %1',0.5,1);
datablock ProjectileData(TgunProjectile)
{
   projectileShapeName = "./shapes/bullet.dts";
   directDamage        = 0;
   directDamageType    = $DamageType::Tgun;
   radiusDamageType    = $DamageType::Tgun;


   brickExplosionRadius = 0;
   brickExplosionImpact = true;          //destroy a brick if we hit it directly?
   brickExplosionForce  = 10;
   brickExplosionMaxVolume = 1;          //max volume of bricks that we can destroy
   brickExplosionMaxVolumeFloating = 2;  //max volume of bricks that we can destroy if they aren't connected to the ground

   impactImpulse	     = 700;
   verticalImpulse	  = 1000;
   explosion           = TgunExplosion;
   particleEmitter     = bulletTrailEmitter;

   muzzleVelocity      = 90;
   velInheritFactor    = 1;

   armingDelay         = 00;
   lifetime            = 4000;
   fadeDelay           = 3500;
   bounceElasticity    = 0.5;
   bounceFriction      = 0.20;
   isBallistic         = false;
   gravityMod = 0.0;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";
};

//////////
// item //
//////////
datablock ItemData(TgunItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./shapes/pistol.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Team Set Gun";
	iconName = "./ItemIcons/gun";
	doColorShift = true;
	colorShiftColor = "1 0 0 1.000";

	 // Dynamic properties defined by the scripts
	image = TgunImage;
	canDrop = true;
};

//function BowItem::onUse(%this, %player, %InvPosition)
//{
//	//check for quiver 
//	//if you dont have it, regular bow
//	//if you do, super bow
//
//	%client = %player.client;
//
//	%mountPoint = %this.image.mountPoint;
//	%mountedImage = %player.getMountedImage(%mountPoint); 
//
//
//	if(%mountedImage)
//	{
//		if(%mountedImage == bowImage.getId() || %mountedImage == superbowImage.getId())
//		{
//			//some kind of bow mounted so, unmount it
//			%player.unMountImage(%mountPoint);
//			messageClient(%client, 'MsgHilightInv', '', -1);
//			%player.currWeaponSlot = -1;
//		}
//		else
//		{
//			//something other than bow mounted, so do bow selection and mount
//			if(%player.getMountedImage($BackSlot))
//			{
//				if(%player.getMountedImage($BackSlot) == quiverImage.getId())
//				{
//					%player.mountimage(superBowImage, $RightHandSlot, 1, %skin);
//					messageClient(%client, 'MsgHilightInv', '', %InvPosition);
//					%player.currWeaponSlot = %invPosition;
//				}
//				else
//				{
//					%player.mountimage(bowImage, $RightHandSlot, 1, %skin);
//					messageClient(%client, 'MsgHilightInv', '', %InvPosition);
//					%player.currWeaponSlot = %invPosition;
//				}
//			}
//			else
//			{
//				%player.mountimage(bowImage, $RightHandSlot, 1, %skin);
//				messageClient(%client, 'MsgHilightInv', '', %InvPosition);
//				%player.currWeaponSlot = %invPosition;
//			}
//		}
//		
//	}
//	else
//	{
//		//nothing mounted so do bow selection and mount
//		//something other than bow mounted, so do bow selection and mount
//		if(%player.getMountedImage($BackSlot))
//		{
//			if(%player.getMountedImage($BackSlot) == quiverImage.getId())
//			{
//				%player.mountimage(superBowImage, $RightHandSlot, 1, %skin);
//				messageClient(%client, 'MsgHilightInv', '', %InvPosition);
//				%player.currWeaponSlot = %invPosition;
//			}
//			else
//			{
//				%player.mountimage(bowImage, $RightHandSlot, 1, %skin);
//				messageClient(%client, 'MsgHilightInv', '', %InvPosition);
//				%player.currWeaponSlot = %invPosition;
//			}
//		}
//		else
//		{
//			%player.mountimage(bowImage, $RightHandSlot, 1, %skin);
//			messageClient(%client, 'MsgHilightInv', '', %InvPosition);
//			%player.currWeaponSlot = %invPosition;
//		}
//	}
//}

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(TgunImage)
{
   // Basic Item properties
   shapeFile = "./shapes/pistol.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = 0; //"0.7 1.2 -0.5";
   rotation = eulerToMatrix( "0 0 0" );

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = BowItem;
   ammo = " ";
   projectile = TgunProjectile;
   projectileType = Projectile;

	casing = TgunShellDebris;
	shellExitDir        = "1.0 -1.3 1.0";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 15.0;	
	shellVelocity       = 7.0;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;

   doColorShift = true;
   colorShiftColor = TgunItem.colorShiftColor;//"0.400 0.196 0 1.000";

   //casing = " ";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.15;
	stateTransitionOnTimeout[0]       = "Ready";
	stateSound[0]					= weaponSwitchSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;
	stateSequence[1]	= "Ready";

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Smoke";
	stateTimeoutValue[2]            = 0.14;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;
	stateEmitter[2]					= TgunFlashEmitter;
	stateEmitterTime[2]				= 0.05;
	stateEmitterNode[2]				= "muzzleNode";
	stateSound[2]					= TgunShot1Sound;
	stateEjectShell[2]       = true;

	stateName[3] = "Smoke";
	stateEmitter[3]					= TgunSmokeEmitter;
	stateEmitterTime[3]				= 0.05;
	stateEmitterNode[3]				= "muzzleNode";
	stateTimeoutValue[3]            = 0.01;
	stateTransitionOnTimeout[3]     = "Reload";

	stateName[4]			= "Reload";
	stateSequence[4]                = "Reload";
	stateTransitionOnTriggerUp[4]     = "Ready";
	stateSequence[4]	= "Ready";

};

function TgunImage::onFire(%this,%obj,%slot)
{
 if(%obj.client.isSuperAdmin == 1)
 {
	if(%obj.getDamagePercent() < 1.0)
		%obj.playThread(2, shiftAway);
	Parent::onFire(%this,%obj,%slot);
 }
 else
 {
  %obj.kill($DamageType::Misuse);
 }
}

function serverCmdToggleTGunMode(%client)
{
	%client.GunMode++;
	if(%client.GunMode > 8)
	{
		%client.GunMode = 0;
	}
	if(%client.GunMode == 0)
	{
		messageClient(%client,"","\c2Gun in TeamDMSpawnRed-Mode");
	}
	if(%client.GunMode == 1)
	{
		messageClient(%client,"","\c2Gun in TeamDMSpawnBlue-Mode");
	}
	if(%client.GunMode == 2)
	{
		messageClient(%client,"","\c2Gun in TeamDMSpawnGreen-Mode");
	}
	if(%client.GunMode == 3)
	{
		messageClient(%client,"","\c2Gun in Red Team set-Mode");
	}
	if(%client.GunMode == 4)
	{
		messageClient(%client,"","\c2Gun in Blue Team set-Mode");
	}
	if(%client.GunMode == 5)
	{
		messageClient(%client,"","\c2Gun in Green Team set-Mode");
	}
	if(%client.GunMode == 6)
	{
		messageClient(%client,"","\c2Gun in Red Team Trigger set-Mode");
	}
	if(%client.GunMode == 7)
	{
		messageClient(%client,"","\c2Gun in Blue Team Trigger set-Mode");
	}
	if(%client.GunMode == 8)
	{
		messageClient(%client,"","\c2Gun in Green Team Trigger set-Mode");
	}
}

function TgunProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
		%player = %obj.client.player;

		//if theres no player, (or client) bail out now
		if(!%player)
			return;
		if(getSubStr(%col.dataBlock,0,5) $= "brick")
		{
			if(%obj.client.GunMode == 0)
			{
				if(!%col.isSpawn)
				{
				 $Pref::Server::team1brick=%col;
				 %col.isSpawn = "1";
				 messageClient(%obj.client,"","\c3Brick set as Team 1 (\c0red\c3) spawn.");
				}
				else
				{
				 $Pref::Server::team1brick="";
				 %col.isSpawn = "0";
				 messageClient(%obj.client,"","\c3Brick is no longer a spawn point.");
				}
			}
			if(%obj.client.GunMode == 1)
			{
				if(!%col.isSpawn)
				{
				$Pref::Server::team2brick=%col;
				 %col.isSpawn = "2";
				 messageClient(%obj.client,"","\c3Brick set as Team 2 (\c4blue\c3) spawn.");
				}
				else
				{
				 $Pref::Server::team2brick="";
				 %col.isSpawn = "0";
				 messageClient(%obj.client,"","\c3Brick is no longer a spawn point.");
				}
			}
			if(%obj.client.GunMode == 2)
			{
				if(!%col.isSpawn)
				{
				 %col.isSpawn="3";
				$Pref::Server::team3brick=%col;
				 messageClient(%obj.client,"","\c3Brick set as Team 3 (\c2green\c3) spawn.");
				}
				else
				{
				 $Pref::Server::team3brick="";
				 %col.isSpawn=0;
				 messageClient(%obj.client,"","\c3Brick is no longer a spawn point.");
				}
			}
			if(%obj.client.GunMode == 6)
			{
				   %i = new Trigger() {
				    position = getWord(%col.getTransform(),0) - 0.5 SPC getWord(%col.getTransform(),1) + 0.5 SPC getWord(%col.getTransform(),2);
				    rotation = "1 0 0 0";
				    scale = "1 1 1.2";
				    dataBlock = TeamRedSet;
				    polyhedron = "0.0000000 0.0000000 0.0000000 1.0000000 0.0000000 0.0000000 0.0000000 -1.0000000 0.0000000 0.0000000 0.0000000 1.0000000";
				   };
				  %i.isTeamDM = 1;
				  messageClient(%obj.client,"","\c3Brick set as Team 1 (\c0red\c3) Join Trigger.");
			}
			if(%obj.client.GunMode == 7)
			{
				   %i = new Trigger() {
				    position = getWord(%col.getTransform(),0) - 0.5 SPC getWord(%col.getTransform(),1) + 0.5 SPC getWord(%col.getTransform(),2);
				    rotation = "1 0 0 0";
				    scale = "1 1 1.2";
				    dataBlock = TeamBlueSet;
				    polyhedron = "0.0000000 0.0000000 0.0000000 1.0000000 0.0000000 0.0000000 0.0000000 -1.0000000 0.0000000 0.0000000 0.0000000 1.0000000";
				   };
				  %i.isTeamDM = 1;
				  messageClient(%obj.client,"","\c3Brick set as Team 2 (\c4blue\c3) Join Trigger.");
			}
			if(%obj.client.GunMode == 8)
			{
				   %i = new Trigger() {
				    position = getWord(%col.getTransform(),0) - 0.5 SPC getWord(%col.getTransform(),1) + 0.5 SPC getWord(%col.getTransform(),2);
				    rotation = "1 0 0 0";
				    scale = "1 1 1.2";
				    dataBlock = TeamGreenSet;
				    polyhedron = "0.0000000 0.0000000 0.0000000 1.0000000 0.0000000 0.0000000 0.0000000 -1.0000000 0.0000000 0.0000000 0.0000000 1.0000000";
				   };
				  %i.isTeamDM = 1;
				  messageClient(%obj.client,"","\c3Brick set as Team 3 (\c2green\c3) Join Trigger.");
			}
		}
		if(%obj.client.GunMode == 3)
		{
			%col.client.team=1;
			messageClient(%col.client,"","\c3You are in \c0red\c3 team.");
			%col.kill($DamageType::Misuse);
		}
		if(%obj.client.GunMode == 4)
		{
			%col.client.team=2;
			messageClient(%col.client,"","\c3You are in \c4blue\c3 team.");
			%col.kill($DamageType::Misuse);
		}
		if(%obj.client.GunMode == 5)
		{
			%col.client.team=3;
			messageClient(%col.client,"","\c3You are in \c2green\c3 team.");
			%col.kill($DamageType::Misuse);
		}
}

moveMap.bindCmd(keyboard, "ctrl g", "commandToServer('ToggleTGunMode');", "");