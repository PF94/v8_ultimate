//Script_PixelExport.cs


function PixelExport()
{
   %group = ServerConnection.getId();
	%count = %group.getCount();

   //find lowest xyz points
   %lowX = 9999999;
   %lowY = 9999999;
   %lowZ = 9999999;

   %highX = -9999999;
   %highY = -9999999;
   %highZ = -9999999;

	for(%i = 0; %i < %count; %i++)
	{
		%obj = %group.getObject(%i);
      if(%obj.getClassName() !$= "FxDTSBrick")
         continue;       
      if(!%obj.isPlanted())
         continue;
      if(%obj.isDead())
         continue;
      
      %wb = %obj.getWorldBox();

      %minx = getWord(%wb, 0);
      %miny = getWord(%wb, 1);
      %minz = getWord(%wb, 2);

      %maxx = getWord(%wb, 3);
      %maxy = getWord(%wb, 4);
      %maxz = getWord(%wb, 5);

      if(%minx < %lowX)
         %lowX = %minx;
      if(%miny < %lowY)
         %lowY = %miny;
      if(%minz < %lowZ)
         %lowZ = %minz;

      if(%maxx > %highX)
         %highX = %maxx;
      if(%maxy > %highY)
         %highY = %maxy;
      if(%maxz > %highZ)
         %highZ = %maxz;
   }

   %lowX = round(%lowX);
   %lowY = round(%lowY);
   %lowZ = round(%lowZ);

   %highX = round(%highX);
   %highY = round(%highY);
   %highZ = round(%highZ);

   %file = new FileObject();
   %file.openForWrite("Add-Ons/Bloxel/PixelCrap.txt");

   %file.writeLine("[BOUNDS]");
   %file.writeLine(round((%highX - %lowX) / 0.5) SPC
                   round((%highY - %lowY) / 0.5) SPC
                   round((%highZ - %lowZ) / 0.2));

   //write out colors
   %file.writeLine("[COLORS]");
   %file.writeLine($maxSpraycolors + 4); //number of colors to be written
   for(%i = 0; %i < $maxSpraycolors; %i++)
	{	
		//get the color
		%color = getColorIDTable(%i);
      %r = mFloor(getWord(%color, 0) * 255);
      %g = mFloor(getWord(%color, 1) * 255);
      %b = mFloor(getWord(%color, 2) * 255);
      %a = mFloor(getWord(%color, 3) * 255);
      %color = %r SPC %g SPC %b SPC %a;
		%file.writeLine(%color);
	}
   %i--;

   $COLOR_ROAD_BLACK = %i++;
   %file.writeLine("51 51 51 255");       
   $COLOR_ROAD_WHITE = %i++;
   %file.writeLine("254 254 232 255"); 
   $COLOR_ROAD_YELLOW = %i++;
   %file.writeLine("248 204 0 255");   
   $COLOR_WINDOW_GLASS = %i++;
   %file.writeLine("255 255 255 77");    

   %file.writeLine("[POINTS]");
   for(%i = 0; %i < %count; %i++)
	{
		%obj = %group.getObject(%i);
      if(%obj.getClassName() !$= "FxDTSBrick")
         continue;        
      if(!%obj.isPlanted())
         continue;
      if(%obj.isDead())
         continue;
      
      %color = %obj.getColorID();
      %file.writeLine("COLOR " @ %color);

      %wb = %obj.getWorldBox();
      %minx = round(getWord(%wb, 0));
      %miny = round(getWord(%wb, 1));
      %minz = round(getWord(%wb, 2));
      %maxx = round(getWord(%wb, 3));
      %maxy = round(getWord(%wb, 4));
      %maxz = round(getWord(%wb, 5));
      
      %wbX = round(%maxX - %minX) / 0.5;
      %wbY = round(%maxY - %minY) / 0.5;
      %wbZ = round(%maxZ - %minZ) / 0.2;

      %xOffset = round((%minX - %lowX) / 0.5);
      %yOffset = round((%minY - %lowY) / 0.5);
      %zOffset = round((%minZ - %lowZ) / 0.2);

      switch(%obj.getDataBlock().getID())
      {
         case brickPineTreeData.getID():
            for(%x = 0; %x < %wbX; %x++)
               for(%y = 0; %y < %wbY; %y++)
                  for(%z = 0; %z < %wbZ; %z++)
                     if(brickPineTreeData.getBlockArrayBit(%x, %y, %z))
                     {
                        %file.writeLine(mFloor(%x + %xOffset) SPC 
                                        mFloor(%y + %yOffset) SPC 
                                        mFloor(%z + %zOffset) );
                     }
         case brick4x1x5windowData.getID():
            if(%wbX > %wbY)
            {
               for(%z = 0; %z < %wbZ; %z++)
               {
                  %file.writeLine(mFloor(%xOffset)        SPC 
                                  mFloor(%yOffset)        SPC 
                                  mFloor(%z + %zOffset)   );
                  %file.writeLine(mFloor(%wbX + %xOffset) SPC 
                                  mFloor(%yOffset)        SPC 
                                  mFloor(%z + %zOffset)   );
               }
               for(%x = 0; %x < %wbX; %x++)
               {
                  %file.writeLine(mFloor(%x + %xOffset)  SPC 
                                  mFloor(%yOffset)       SPC 
                                  mFloor(%zOffset)       );
                  %file.writeLine(mFloor(%x + %xOffset)  SPC 
                                  mFloor(%yOffset)       SPC 
                                  mFloor(%wbZ + %zOffset));
               }
               %file.writeLine("COLOR 17");
               for(%x = 1; %x < %wbX - 1; %x++)
                  for(%z = 1; %z < %wbZ - 1; %z++)
                     %file.writeLine(mFloor(%x + %xOffset)  SPC 
                                     mFloor(%yOffset)       SPC 
                                     mFloor(%z + %zOffset)  );
            }
            else
            {
               for(%z = 0; %z < %wbZ; %z++)
               {
                  %file.writeLine(mFloor(%xOffset)        SPC 
                                  mFloor(%yOffset)        SPC 
                                  mFloor(%z + %zOffset)   );
                  %file.writeLine(mFloor(%xOffset)        SPC 
                                  mFloor(%wbY + %yOffset) SPC 
                                  mFloor(%z + %zOffset)   );
               }
               for(%y = 0; %y < %wbY; %y++)
               {
                  %file.writeLine(mFloor(%xOffset)       SPC 
                                  mFloor(%y + %yOffset)  SPC 
                                  mFloor(%zOffset)       );
                  %file.writeLine(mFloor(%xOffset)       SPC 
                                  mFloor(%y + %yOffset)  SPC 
                                  mFloor(%wbZ + %zOffset));
               }
               %file.writeLine("COLOR " @ $COLOR_WINDOW_GLASS);
               for(%y = 1; %y < %wbY - 1; %y++)
                  for(%z = 1; %z < %wbZ - 1; %z++)
                     %file.writeLine(mFloor(%xOffset)       SPC 
                                     mFloor(%y + %yOffset)  SPC 
                                     mFloor(%z + %zOffset)  );
            }
         case brick32x32froadsData.getID():
            %b = -1;
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";

            doPixelRoad(%xOffset,%yOffset,%zOffset,%file, %obj, 0);
            %file.writeLine("COLOR " @ $COLOR_ROAD_WHITE );
            doPixelRoad(%xOffset,%yOffset,%zOffset,%file, %obj, 1);
            %file.writeLine("COLOR " @ $COLOR_ROAD_BLACK);
            doPixelRoad(%xOffset,%yOffset,%zOffset,%file, %obj, 2);
            %file.writeLine("COLOR " @ $COLOR_ROAD_YELLOW);
            doPixelRoad(%xOffset,%yOffset,%zOffset,%file, %obj, 3);

         case brick32x32froadtData.getid():
            %b = -1;
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221111111111";
            $blah[%b++] = "00000000012222233222221222222222";
            $blah[%b++] = "00000000012222233222221222222222";
            $blah[%b++] = "00000000012222233222221222222222";
            $blah[%b++] = "00000000012222233222221222222222";
            $blah[%b++] = "00000000012222233222221222222222";
            $blah[%b++] = "00000000012222233222221233333333";
            $blah[%b++] = "00000000012222233222221233333333";
            $blah[%b++] = "00000000012222233222221222222222";
            $blah[%b++] = "00000000012222233222221222222222";
            $blah[%b++] = "00000000012222233222221222222222";
            $blah[%b++] = "00000000012222233222221222222222";
            $blah[%b++] = "00000000012222233222221222222222";
            $blah[%b++] = "00000000012222233222221111111111";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";

            doPixelRoad(%xOffset,%yOffset,%zOffset,%file, %obj, 0);
            %file.writeLine("COLOR " @ $COLOR_ROAD_WHITE );
            doPixelRoad(%xOffset,%yOffset,%zOffset,%file, %obj, 1);
            %file.writeLine("COLOR " @ $COLOR_ROAD_BLACK);
            doPixelRoad(%xOffset,%yOffset,%zOffset,%file, %obj, 2);
            %file.writeLine("COLOR " @ $COLOR_ROAD_YELLOW);
            doPixelRoad(%xOffset,%yOffset,%zOffset,%file, %obj, 3);
         
         case brick32x32froadcData.getid():
            %b = -1;
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222222100000000";
            $blah[%b++] = "00000000012222233222222100000000";
            $blah[%b++] = "00000000012222233222222100000000";
            $blah[%b++] = "00000000001222223322222210000000";
            $blah[%b++] = "00000000001222223322222210000000";
            $blah[%b++] = "00000000001222223322222221000000";
            $blah[%b++] = "00000000001222222332222222110000";
            $blah[%b++] = "00000000001222222332222222221110";
            $blah[%b++] = "00000000000122222233222222222221";
            $blah[%b++] = "00000000000012222223322222222222";
            $blah[%b++] = "00000000000012222222332222222222";
            $blah[%b++] = "00000000000001222222233222222222";
            $blah[%b++] = "00000000000001222222223332222222";
            $blah[%b++] = "00000000000000122222222333332222";
            $blah[%b++] = "00000000000000012222222223333333";
            $blah[%b++] = "00000000000000001222222222223333";
            $blah[%b++] = "00000000000000000122222222222222";
            $blah[%b++] = "00000000000000000011222222222222";
            $blah[%b++] = "00000000000000000000112222222222";
            $blah[%b++] = "00000000000000000000001222222222";
            $blah[%b++] = "00000000000000000000000111112222";
            $blah[%b++] = "00000000000000000000000000001111";
            $blah[%b++] = "00000000000000000000000000000000";
            $blah[%b++] = "00000000000000000000000000000000";
            $blah[%b++] = "00000000000000000000000000000000";
            $blah[%b++] = "00000000000000000000000000000000";
            $blah[%b++] = "00000000000000000000000000000000";
            $blah[%b++] = "00000000000000000000000000000000";
            $blah[%b++] = "00000000000000000000000000000000";
            $blah[%b++] = "00000000000000000000000000000000";
            $blah[%b++] = "00000000000000000000000000000000";
               
            doPixelRoad(%xOffset,%yOffset,%zOffset,%file, %obj, 0);
            %file.writeLine("COLOR " @ $COLOR_ROAD_WHITE );
            doPixelRoad(%xOffset,%yOffset,%zOffset,%file, %obj, 1);
            %file.writeLine("COLOR " @ $COLOR_ROAD_BLACK);
            doPixelRoad(%xOffset,%yOffset,%zOffset,%file, %obj, 2);
            %file.writeLine("COLOR " @ $COLOR_ROAD_YELLOW);
            doPixelRoad(%xOffset,%yOffset,%zOffset,%file, %obj, 3);

         case brick32x32froadxData.getID(): 
            %b = -1;
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222222222221000000000";
            $blah[%b++] = "11111111111111111111111111111111";
            $blah[%b++] = "22222222212222222222221222222222";
            $blah[%b++] = "22222222212222222222221222222222";
            $blah[%b++] = "22222222212222222222221222222222";
            $blah[%b++] = "22222222212222222222221222222222";
            $blah[%b++] = "22222222212222222222221222222222";
            $blah[%b++] = "33333333212222222222221233333333";
            $blah[%b++] = "33333333212222222222221233333333";
            $blah[%b++] = "22222222212222222222221222222222";
            $blah[%b++] = "22222222212222222222221222222222";
            $blah[%b++] = "22222222212222222222221222222222";
            $blah[%b++] = "22222222212222222222221222222222";
            $blah[%b++] = "22222222212222222222221222222222";
            $blah[%b++] = "11111111111111111111111111111111";
            $blah[%b++] = "00000000012222222222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            $blah[%b++] = "00000000012222233222221000000000";
            
            doPixelRoad(%xOffset,%yOffset,%zOffset,%file, %obj, 0);
            %file.writeLine("COLOR " @ $COLOR_ROAD_WHITE );
            doPixelRoad(%xOffset,%yOffset,%zOffset,%file, %obj, 1);
            %file.writeLine("COLOR " @ $COLOR_ROAD_BLACK);
            doPixelRoad(%xOffset,%yOffset,%zOffset,%file, %obj, 2);
            %file.writeLine("COLOR " @ $COLOR_ROAD_YELLOW);
            doPixelRoad(%xOffset,%yOffset,%zOffset,%file, %obj, 3);


         case brick4x1x2FenceData.getID():
            for(%x = 0; %x < %wbX; %x++)
               for(%y = 0; %y < %wbY; %y++)
                  %file.writeLine(   mFloor(%x + %xOffset) SPC 
                                     mFloor(%y + %yOffset) SPC 
                                     mFloor(%zOffset) );
            if(%wbX > %wbY)
            {
               for(%x = 0; %x < %wbX; %x++)
                  for(%y = 0; %y < %wbY; %y++)
                     for(%z = 1; %z < %wbZ; %z++)
                     {
                        if(%z > 2)
                        {
                           if(%x % 2 == 0)
                              continue;
                        }
                        else
                        {
                           if(%x % 2 != 0)
                              continue;
                        }
                        
                        %file.writeLine(   mFloor(%x + %xOffset) SPC 
                                           mFloor(%y + %yOffset) SPC 
                                           mFloor(%z + %zOffset) );
                     }
            }
            else
            {
               for(%x = 0; %x < %wbX; %x++)
                  for(%y = 0; %y < %wbY; %y++)
                     for(%z = 1; %z < %wbZ; %z++)
                     {
                        if(%z > 2)
                        {
                           if(%y % 2 == 0)
                              continue;
                        }
                        else
                        {
                           if(%y % 2 != 0)
                              continue;
                        }
                        
                        %file.writeLine(   mFloor(%x + %xOffset) SPC 
                                           mFloor(%y + %yOffset) SPC 
                                           mFloor(%z + %zOffset) );
                     }
            }
         default:
            for(%x = 0; %x < %wbX; %x++)
               for(%y = 0; %y < %wbY; %y++)
                  for(%z = 0; %z < %wbZ; %z++)
                     %file.writeLine(mFloor(%x + %xOffset) SPC 
                                     mFloor(%y + %yOffset) SPC 
                                     mFloor(%z + %zOffset) );
            
      } //end switch
   } //end object loop
   %file.close();
   %file.delete();

   
   deleteVariables("$blah*");
   
   echo("Pixel Export Done!");
}

function doPixelRoad(%xOffset,%yOffset,%zOffset,%file, %obj, %match)
{
   for(%x = 0; %x < 32; %x++)
   {
      for(%y = 0; %y < 32; %y++)
      {
         switch(%obj.getAngleID())
         {
            case 0:
               %testX = %x;
               %testY = %y;
            case 3:
               %testX = %y;
               %testY = 31-%x;
            case 2:
               %testX = 31-%x;
               %testY = 31-%y;
            case 1:
               %testX = 31-%y;
               %testY = %x;
            default:
         }
         if(mFloor(getSubStr($blah[%testY], %testX, 1)) == %match)
            %file.writeLine(mFloor(%x + %xOffset) SPC 
                            mFloor(%y + %yOffset) SPC 
                            mFloor(%zOffset) );
      }
   }
}