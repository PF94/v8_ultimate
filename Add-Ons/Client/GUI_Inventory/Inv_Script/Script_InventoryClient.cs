function InvWnd_Panel(%en)
{
InvWndUse.Setvisible(%en);
InvWndItemDesc.Setvisible(%en);
InvWndItemName.Setvisible(%en);
InvWndItemAmount.Setvisible(%en);
InvWndPN.Setvisible(%en);
InvWndPD.Setvisible(%en);
InvWndPA.Setvisible(%en);
InvWnd_GB.Setvisible(%en);
InvWnd_GN.Setvisible(%en);
InvWnd_GA.Setvisible(%en);
InvWnd_DB.Setvisible(%en);
InvWnd_DTB.Setvisible(%en);
if(%en==0)
{
	InvWndPA1.setvisible(0);
	InvWndPA2.setvisible(0);
	InvWndPA3.setvisible(0);
	InvWndPA4.setvisible(0);
	InvWndA1.setvisible(0);
	InvWndA2.setvisible(0);
	InvWndA3.setvisible(0);
	InvWndA4.setvisible(0);
}
}

function DisplayInventoryGui()
{
	canvas.pushdialog(InventoryGui);
	commandtoserver('getinventorylist');
	InvWnd_Panel(0);
}

if (!$InventoryBindings)
{
	$remapDivision[$remapCount] = "RPG Inventory";
	$remapName[$remapCount] = "Display GUI";
	$remapCmd[$remapCount] = "DisplayInventoryGui";
	$remapCount++;
	$remapName[$remapCount]= "IS Console";
	$remapCmd[$remapCount]= "OISC";
	$remapCount++;
	$InventoryBindings=true;
}

function clientcmdclearinventorylist()
{
	InvWndItemList.clear();
}

function clientcmdaddinventoryitem(%id,%name,%amount)
{
	InvWndItemList.addrow(%id,%name TAB %amount);
}

function clientcmdsetinventorydesc(%name,%desc,%amount,%a1,%a2,%a3,%a4)
{
	InvWndItemName.setText(%name);
	InvWndItemDesc.setText(%desc);
	InvWndItemAmount.setText(%amount);
	InvWnd_Panel(1);
	
	if(%a1!$="")
	{
		InvWndPA1.setvisible(1);
		InvWndA1.setvisible(1);
		InvWndPA1.settext(%a1);
		InvWndA1.settext("");
	}
	else
	{
		InvWndPA1.setvisible(0);
		InvWndA1.setvisible(0);
	}
	$a1=%a1;
	
	if(%a2!$="")
	{
		InvWndPA2.setvisible(1);
		InvWndA2.setvisible(1);
		InvWndPA2.settext(%a2);
		InvWndA2.settext("");
	}
	else
	{
		InvWndPA2.setvisible(0);
		InvWndA2.setvisible(0);
	}
	$a2=%a2;
	
	if(%a3!$="")
	{
		InvWndPA3.setvisible(1);
		InvWndA3.setvisible(1);
		InvWndPA3.settext(%a3);
		InvWndA3.settext("");
	}
	else
	{
		InvWndPA3.setvisible(0);
		InvWndA3.setvisible(0);
	}
	$a3=%a3;
	
	if(%a4!$="")
	{
		InvWndPA4.setvisible(1);
		InvWndA4.setvisible(1);
		InvWndPA4.settext(%a4);
		InvWndA4.settext("");
	}
	else
	{
		InvWndPA4.setvisible(0);
		InvWndA4.setvisible(0);
	}
	$a4=%a4;
}

function InvWnd_Use()
{
	commandtoserver('useitem',InvWndItemList.getselectedid(),InvWndA1.getvalue(),InvWndA2.getvalue(),InvWndA3.getvalue(),InvWndA4.getvalue());
	%sel=InvWndItemList.getselectedid();
	commandtoserver('getinventorylist');
	InvWndItemAmount.setText("<amount>");
	InvWndItemName.setText("<name>");
	InvWndItemDesc.setText("<description>");
	InvWndItemList.clearselection();
	InvWnd_Panel(0);
}

function InvWnd_Selectitem()
{
	commandtoserver('getitemdata',InvWndItemList.getselectedid());
}

function InvWnd_Give()
{
	commandtoserver('giveitembyid',InvWnd_GN.getValue(),InvWndItemList.getselectedid(),InvWnd_GA.getValue());
}

function InvWnd_DropItem()
{
	%id=InvWndItemList.getselectedid();
	%amount=InvWnd_DTB.getValue();
	commandtoserver('DropIItem',%id,%amount);
	commandtoserver('getinventorylist');
	InvWndItemAmount.setText("<amount>");
	InvWndItemName.setText("<name>");
	InvWndItemDesc.setText("<description>");
	InvWndItemList.clearselection();
	InvWnd_Panel(0);
}