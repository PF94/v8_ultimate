//Round card thing
datablock ShapeBaseImageData(RotCardImage)
{
   // Basic Item properties
   shapeFile = "./card/card.dts";
   emap = false;
   mountPoint = 0;
   offset = "-0.53 0.1 0";
   doColorShift = false;
   //colorShiftColor = "0.471 0.471 0.471 1.000";

};

function servercmdholdcard(%client,%picture,%arms)
{
	
	%picturef = addtaggedstring(%picture);
	%client.player.mountimage(RotCardImage,0,0,%picturef);
	%client.player.playthread(2,armreadyboth);
	if(%arms == 1)
	{
		%client.player.setarmthread(look);
		return;
	}
	%client.player.setarmthread(shiftup);

}
function RotCardImage::onunmount(%this,%obj,%two)
{
	%obj.setarmthread(look);
	//%obj.stopthread(2);
}