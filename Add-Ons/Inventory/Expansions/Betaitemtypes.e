//New Item Type Definitions

function InvItemUsed_PotionHP(%c,%id,%a1,%a2,%a3,%a4)
{
	%c.player.setdamagelevel(%c.player.getdamagelevel()-$InvItemHeal[%id]);
	messageclient(%c,"","\c2You used a \c0"@GetItemStat(%id,"Name")@" \c2,it recovered "@GetItemStat(%id,"Heal")@" HP!");
	SubItemCount(%c,%id,1);
}

function InvItemUsed_Weapon(%c,%id,%a1,%a2,%a3,%a4)
{
	messageclient(%c,"","\c2You equipped a \c0"@GetItemStat(%id,"Name")@"\c2!");
	%c.player.updateArm(GetItemStat(%id,"Image"));
	%c.player.mountImage(GetItemStat(%id,"Image"),0);
}

function InvItemUsed_KScroll(%c,%id,%a1,%a2,%a3,%a4)
{
	messageall("","\c3"@%c.name@" \c0used a scroll that kills everyone!");
	for(%a=0;%a<clientgroup.getcount();%a++)
	{
		if(clientgroup.getobject(%a)!=%c)
		{
			clientgroup.getobject(%a).player.kill();
		}
	}
	SubItemCount(%c,%id,1);
}

function InvItemUsed_MScroll(%c,%id,%a1,%a2,%a3,%a4)
{
	messageclient(%c,"","\c2Message sent!");
	commandtoclient(findclientbyname(%a1),'MessageBoxOk',"Message from "@%c.name,%a2);
	SubItemCount(%c,%id,1);
}

function InvItemUsed_TScroll(%c,%id,%a1,%a2,%a3,%a4)
{
	messageclient(%c,"","\c2Teleported successfully!");
	%t=findclientbyname(%a1);
	%c.player.settransform(vectoradd(%t.player.gettransform(),"0 0 10"));
	SubItemCount(%c,%id,1);
}

function InvItemUsed_TKScroll(%c,%id,%a1,%a2,%a3,%a4)
{
	messageclient(%c,"","\c2Teleported successfully!");
	%t=findclientbyname(%a1);
	%c.player.settransform(vectoradd(%t.player.gettransform(),"0 0 10"));
	%t.player.kill();
	SubItemCount(%c,%id,1);
}

function InvItemsCombined_MakeFire(%c)
{
	%c.player.kill();
}