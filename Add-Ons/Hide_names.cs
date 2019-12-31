package HideNames{ 
   function serverCmdToggleNames(%client){ 
      if(%client.isSuperAdmin){ 
         if($HidePlayerNames){ 
            messageClient(%client, '', 'Player names ON.'); 
            $HidePlayerNames = 0; 
            for(%i=0;%i<ClientGroup.getCount();%i++){ 
               %cl = ClientGroup.getObject(%i); 
               if(isObject(%cl.player)){ 
                  %cl.player.setShapeName(%cl.name); 
               } 
            } 
         } else { 
            messageClient(%client, '', 'Player names OFF.'); 
            $HidePlayerNames = 1; 
            for(%i=0;%i<ClientGroup.getCount();%i++){ 
               %cl = ClientGroup.getObject(%i); 
               if(isObject(%cl.player)){ 
                  %cl.player.setShapeName(""); 
               } 
            } 
         } 
      } 
   } 
   function GameConnection::spawnPlayer(%this){ 
      Parent::spawnPlayer(%this); 
      if($HidePlayerNames){ 
         %this.player.setShapeName(""); 
      } 
   } 
}; 
ActivatePackage(HideNames);