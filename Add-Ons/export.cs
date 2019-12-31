function exportBricks(%filename)
{
 %file = new FileObject();
 %file.openforWrite("Add-Ons/" @ %filename);
 %pos = MainBrickGroup.getObject(0).getObject(0).getPosition();
 for(%i=0;%i<MainBrickGroup.getCount();%i++)
 {
  for(%j=0;%j<MainBrickGroup.getObject(%i).getCount();%j++)
  {
   %b = MainBrickGroup.getObject(%i).getObject(%j);
   %rot = (%b.angleID / 2 $= mFloor(%b.angleID / 2));
   %data = %b.getDatablock();
   %x = (%rot ? %data.bricksizeX * 0.5 : %data.brickSizeY * 0.5);
   %y = (%rot ? %data.bricksizeY * 0.5 : %data.brickSizeX * 0.5);
   %z = (%data.bricksizeZ / 3) * 0.6;
   %file.writeLine("BRICK" SPC %x SPC %y SPC %z SPC vectorSub(%b.getPosition(),%pos) SPC %b.colorID);
  }
 }
 for(%i=0;%i<64;%i++)
 {
  %file.writeLine("PAINT" SPC getColorIDTable(%i));
 }
 %file.close();
}