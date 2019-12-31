//a little script that makes the player drop their current weapon when they die
//made by zor

package dropweapon{
 function GameConnection::onDeath(%client,%a,%b,%c,%d,%e,%f){
  if(%client.minigame)
   servercmddroptool(%client,%client.player.currtool);
  Parent::onDeath(%client,%a,%b,%c,%d,%e,%f);
 }
};
ActivatePackage(dropweapon);