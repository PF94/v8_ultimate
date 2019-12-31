function OpenCombineGui()
{
	canvas.pushdialog(CombineGui);
	ComWndI1L.clear();
	ComWndI2L.clear();
	ComWndCS.clear();
	commandtoserver('getcombineitems');
}

function clientcmdsetcombineitems(%item,%itemname,%amount)
{
	ComWndI1L.addrow(%item,%itemname TAB %amount);
	ComWndI2L.addrow(%item,%itemname TAB %amount);
}

function ComWnd_Combine()
{
	%a=ComWndI1L.getselectedid();
	%b=ComWndI2L.getselectedid();
	%c=ComWndCS.getselectedid();
	if(%a!$=""&&%b!$=""&&%c!$="")
	{
		commandtoserver('combineitems',%a,%b,%c);
	}
	ComWndI1L.clear();
	ComWndI2L.clear();
	ComWndCS.clear();
	commandtoserver('getcombineitems');
}

function ComWnd_Check()
{
	%a=ComWndI1L.getselectedid();
	%b=ComWndI2L.getselectedid();
	ComWndCS.clear();
	if(%a!$=""&&%b!$="")
	{
			commandtoserver('getcombos',%a,%b);
	}
}

function clientcmdSetCombos(%id,%name,%a1,%a2)
{
ComWndCS.addrow(%id,%name TAB %a1 TAB %a2);
}