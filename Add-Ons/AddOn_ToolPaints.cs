package tools
{
 function servercmdUseSprayCan(%client,%num)
 {
  if(%num < 36 || %num > 39){Parent::servercmdUseSprayCan(%client,%num);return;}
  if(isObject(%client.minigame)){commandtoclient(%client,'centerprint',"You can't use Tool Paints in a minigame.",1,2);return;}
  switch(%num)
  {
   case 36: %im = HammerImage;
   case 37: %im = WrenchImage;
   case 38: %im = PrintGunImage;
   case 39: if(%client.isAdmin || %client.isSuperAdmin){%im = AdminWandImage;}else{%im = WandImage;}
   default: %im = HammerImage;
  }
  %client.player.mountImage(%im,0);
  %client.player.playthread(1,armreadyright);
 }
 function Player::giveDefaultEquipment(%this)
 {
  for(%i=0;%i<5;%i++)
  {
   %this.tool[%i] = 0;
   if(isObject(%this.client)){messageClient(%this.client,'MsgItemPickup','',%i,0);}
  }
 }
};activatepackage(tools);