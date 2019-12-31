//Allows you to crash through bricks if your moving fast enough.
//Script by Aloshi.
//Idea by Skele.

function PlayerStandardArmor::onImpact(%this, %obj, %col, %vec, %vecLen)
{
	if(%col.getclassname() $= "fxDTSBrick"){
	%dmg = %obj.getdamagelevel();
	if(%dmg < 80){
	%obj.applydamage(20);
	}else{
	%obj.kill();
	messageAll('','%1 <Bitmap:Add-Ons/CI/splat.png>',%obj.client.name);
	}
	%vel = %obj.getvelocity();
	new Projectile()
	{
	dataBlock        = "BrickdestroyercProjectile";
	initialPosition  = %obj.getposition();
	initialVelocity  = %vel;
	sourceObject     = %obj;
	client = %obj.client;
	};
	return;
	}
parent::onImpact(%this, %obj, %col, %vec, %vecLen);
}

datablock ProjectileData(BrickdestroyercProjectile)
{
 //projectileShapeName = "./shapes/RocketProjectile.dts";
   directDamage        = 0;
   directDamageType    = $DamageType::ArrowDirect;
   radiusDamage        = 0;
   damageRadius        = 0;
   radiusDamageType    = $DamageType::ArrowDirect;
   muzzleVelocity      = 500;
   velInheritFactor    = 1;
   armingDelay         = 0;
   lifetime            = 5;
   fadeDelay           = 499;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.0;
   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";
   brickExplosionRadius = 1.6;
   brickExplosionImpact = true;          //destroy a brick if we hit it directly?
   brickExplosionForce  = 15;             
   brickExplosionMaxVolume = 50;          //max volume of bricks that we can destroy
   brickExplosionMaxVolumeFloating = 60;
};

function BrickdestroyercProjectile::onCollision(%a, %b, %c, %d, %e, %f)
{
echo("Woot, we hit something");
echo("hit pos " @ %c.getposition());
$Pos = %c.gettransform();
parent::onCOllision(%a, %b, %c, %d, %e, %f);
}


function WheeledVehicleData::onImpact(%this, %obj, %col, %vec, %vecLen)
{
	%vel = %obj.getvelocity();
	%pos = %obj.getposition();
	%player = %obj.getmountedobject(0).client.player;
	%client = %player.client;
	new Projectile()
	{
	dataBlock        = "BrickdestroyerdProjectile";
	initialPosition  = %pos;
	initialVelocity  = %vel;
	sourceObject     = %obj;
	client = %client;
	};
}

datablock ProjectileData(BrickdestroyerdProjectile)
{
 //projectileShapeName = "./shapes/RocketProjectile.dts";
   directDamage        = 0;
   directDamageType    = $DamageType::ArrowDirect;
   radiusDamage        = 0;
   damageRadius        = 0;
   radiusDamageType    = $DamageType::ArrowDirect;
   muzzleVelocity      = 1000;
   velInheritFactor    = 1;
   armingDelay         = 0;
   lifetime            = 500;
   fadeDelay           = 499;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.1;
   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";
   brickExplosionRadius = 5.75;
   brickExplosionImpact = true;          //destroy a brick if we hit it directly?
   brickExplosionForce  = 30;             
   brickExplosionMaxVolume = 500;          //max volume of bricks that we can destroy
   brickExplosionMaxVolumeFloating = 600;
};
