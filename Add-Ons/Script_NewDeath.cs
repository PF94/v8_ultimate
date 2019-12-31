//Originally By Zor.
//Minigame only feature removed by Itake.
package deathzor{
 function GameConnection::onDeath(%this, %sourceObject, %sourceClient, %damageType, %damLoc){
  if(!%this.player.normaldeath){
   tumble(%this.player);
  }
  Parent::onDeath(%this, %sourceObject, %sourceClient, %damageType, %damLoc);
 }
};
activatepackage(deathzor);
package tumbledamage{
function DeathVehicle::onImpact(%this,%obj){
   parent::onimpact(%this,%obj);
   if($pref::server::fallingdamage){
    if(vectorlen(%obj.getvelocity())>30){
     messageall('',"<bitmap:add-ons/ci/crater> "@%obj.getmountedobject(0).client.name);
     %obj.getmountedobject(0).kill();
    }
   }
}
};
activatepackage(tumbledamage);