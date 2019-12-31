if (!$ISBindings)
{
	$remapDivision[$remapCount] = "Inventory System";
	$remapName[$remapCount] = "Show GUI";
	$remapCmd[$remapCount] = "IS_Showgui";
	$remapCount++;
	$ISBindings=true;
	$remapName[$remapCount] = "Show ItemAdmin";
	$remapCmd[$remapCount] = "IS_ItemAdmin";
	$remapCount++;
	$ISBindings=true;
}

function IS_showgui()
{
	canvas.pushdialog(Inventorygui);
}

function IS_Use()
{
	commandtoserver('useitem',IS_TL1.getselectedid(),IS_A1Edit.getvalue(),IS_A2Edit.getvalue(),IS_A3Edit.getvalue(),IS_A4Edit.getvalue());
	IS_InfoPanel.setVisible(0);
	IS_RefreshGui();
}

function IS_TL1Selected(%ID)
{
	commandtoserver('getbasiciteminfo',%ID);
}

function InventoryGui::OnWake(%this)
{
	IS_InfoPanel.setvisible(0);
	IS_RefreshGui();
}

function IS_RefreshGui()
{
	IS_A1Panel.setvisible(0);
	IS_A2Panel.setvisible(0);
	IS_A3Panel.setvisible(0);
	IS_A4Panel.setvisible(0);
	IS_DropPanel.setvisible(0);
	IS_TL1.clear();
	IS_CO_SWATCH.clear();
	$Temp=0;
	commandtoserver('getitems');
	IS_RefreshEquipGui();
}

function clientcmdsetitems(%a,%name,%count)
{
	IS_TL1.addrow(%a,%name TAB %count*1);
	%n=new GuiCheckBoxCtrl()
	{
		profile = "GuiCheckBoxProfile";
		horizSizing = "right";
		vertSizing = "bottom";
		position = "1 "@$Temp*30;
		extent = "140 30";
		minExtent = "8 2";
		visible = "1";
		text = %name @ " - " @ %count;
		groupNum = "-1";
		buttonType = "ToggleButton";
		ItemID=%a;
	};IS_CO_SWATCH.add(%n);
	$Choices[$Temp]=%n;
	$Temp++;
	IS_CO_SWATCH.resize(1,1,145,($Temp*30));
}

function clientcmdsetbasiciteminfo(%name,%type,%desc,%a1,%a2,%a3,%a4)
{
	IS_Name.setvalue(%name);
	IS_Type.setvalue(%type);
	IS_Desc.setvalue(%desc);
	
	if(%a1!$="")
	{
	IS_A1Text.setvalue(%a1);
	IS_A1Panel.setvisible(1);
	}
	else
	{
	IS_A1Panel.setvisible(0);
	}
	
	if(%a2!$="")
	{
	IS_A2Text.setvalue(%a2);
	IS_A2Panel.setvisible(1);
	}
	else
	{
	IS_A2Panel.setvisible(0);
	}
	
	if(%a3!$="")
	{
	IS_A3Text.setvalue(%a3);
	IS_A3Panel.setvisible(1);
	}
	else
	{
	IS_A3Panel.setvisible(0);
	}
	
	if(%a4!$="")
	{
	IS_A4Text.setvalue(%a4);
	IS_A4Panel.setvisible(1);
	}
	else
	{
	IS_A4Panel.setvisible(0);
	}
	
	IS_InfoPanel.setvisible(1);
	IS_DropPanel.setvisible(1);
}

function EquipGui::OnWake(%this)
{
	IS_RefreshEquipGui();
}

function IS_RefreshEquipGui()
{
	IS_EQ_TL1.clear();
	commandtoserver('getequips');
}

function clientcmdsetequips(%a,%slot,%name)
{
	IS_EQ_TL1.addrow(%a,%slot TAB %name);
}

function IS_EQ_Unequip()
{
	if(IS_EQ_TL1.getselectedid()==-1) {return;}
	commandtoserver('useitem',IS_EQ_TL1.getselectedid());
	IS_RefreshGui();
}

function ComboGui::OnWake(%this)
{
	IS_RefreshGui();
}

function IS_CombineItems()
{
	%count=IS_CO_SWATCH.getcount();
	for(%a=0;%a<%count;%a++)
	{
		if(IS_CO_SWATCH.getobject(%a).getValue()$="1")
		{
			if(%Outstr$="") {%space="";}else{%space=" ";}
			%Outstr=%Outstr@%space@IS_CO_SWATCH.getobject(%a).ItemID;
		}
	}
	commandtoserver('CombineItems',%outstr);
	IS_RefreshGui();
}

function InventoryShopGui::OnWake(%this)
{
	RefreshShopGui();
}

function RefreshShopGui()
{
	IS_S_TL1.clear();
	IS_S_Money.setvalue("?");
	commandtoserver('getshopitems');
	commandtoserver('getmoney');
}

function clientcmdsetshopitems(%i,%name,%cost)
{
	IS_S_TL1.addrow(%i,%name TAB %cost);
}

function clientcmdsetmoney(%money)
{
	IS_S_Money.setvalue(%money);
}

function clientcmdopenshopgui()
{
	canvas.pushdialog(InventoryShopGui);
}
	
function IS_BuyItemGui()
{
	commandtoserver('buyitem',IS_S_TL1.getselectedid(),IS_S_AMOUNT.getValue()*1);
}

function clientcmdItemBought()
{
	RefreshShopGUI();
}

function ItemAdminGui::OnWake(%this)
{
	IS_IA_TL1.clear();
	commandtoserver('IA_isallowed');
}

function IS_ItemAdmin()
{
	canvas.pushdialog(ItemAdminGui);
}

function IS_GrantItemGui()
{
	commandtoserver('Grantitem',IS_IA_PLAYER.getValue(),IS_IA_TL1.getselectedid(),IS_IA_AMOUNT.getValue());
}

function clientCmdIA_NotAllowed()
{
	canvas.popdialog(ItemAdminGui);
	MessageBoxOK("Error","You are not allowed to go into the ItemAdmin.");
}

function clientcmdIA_setitems(%id,%name)
{
	IS_IA_TL1.addrow(%id,%id TAB %name);
}

function InventoryDumpGui::OnWake(%this)
{
	commandtoserver('IS_Dump');
}

function clientcmdAddDumpLine(%line)
{
	IS_DUMP.setvalue(IS_DUMP.getvalue() NL %line);
}

function clientcmdReloadDumpData()
{
	IS_DUMP.setvalue("<color:FF0000>Player Data");
}

//Shoplist
function GlobalShopGui::onWake(%this)
{
	IS_GS_TL1.clear();
	commandtoserver('getshoplist');
}

function clientcmdSetShopList(%a,%name)
{
	IS_GS_TL1.addrow(%a,%a TAB %name);
}

function GlobalShop_Click()
{
	%id=IS_GS_TL1.getselectedid();
	canvas.popdialog(GlobalShopGui);
	commandtoserver('openshopid',%id);
}

function IS_CL_DropItem()
{
	commandtoserver('dropiitem',IS_TL1.getselectedid(),IS_CL_Amount.getValue());
	IS_RefreshGui();
}