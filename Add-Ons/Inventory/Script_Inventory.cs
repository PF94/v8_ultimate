$ICTRACE = 0;
$IS::VersionNumber="IS V1.2";

function GetItemCount(%c,%i)
{
return $IItemA[%c.bl_id,%i];
}

function SetItemCount(%c,%i,%am)
{
$IItemA[%c.bl_id,%i]=%am;
}

function AddItemCount(%c,%i,%am)
{
$IItemA[%c.bl_id,%i]+=%am;
}

function SubItemCount(%c,%i,%am)
{
$IItemA[%c.bl_id,%i]-=%am;
}

function MakeNewItem(%Name,%Type)
{
	$PoI+=1;
	$InvItemName[$PoI]=%name;
	$InvItemType[$PoI]=%type;
	return $PoI;
}

function SetItemStat(%item,%stat,%newstat)
{
	eval("$InvItem"@%stat@"["@%item@"]=\""@%newstat@"\";");
}

function GetItemStat(%item,%stat)
{
	return eval("return $InvItem"@%stat@"["@%item@"];");
}

function finditembyname(%item)
{
	for(%a=0;%a<=$PoI;%a++)
	{
		if(!(strpos(strupr($InvItemName[%a]),strupr(%item))==-1))
		{
			return %a;
		}
	}
	return -1;
}

function servercmdlistinventory(%c)
{
	messageclient(%c,"","\c2Listing inventory :");
	for(%a=0;%a<=$PoI;%a++)
		{
			if ($IItemA[%c.bl_id,%a]>=1)
			{
				messageclient(%c,"","\c7["@%a@"] \c2"@$InvItemName[%a]@" - A:"@$IItemA[%c.bl_id,%a]);
			}
		}
}

function servercmdlistallitems(%c)
{
	if(%c.isSuperAdmin=1)
	{
	messageclient(%c,"","\c2Listing all available items :");
	for(%a=0;%a<=$PoI;%a++)
		{
			messageclient(%c,"","\c7["@%a@"] \c2"@$InvItemName[%a]);
		}
	}
}

function servercmdUseItem(%c,%slot,%a1,%a2,%a3,%a4)
{
	if(%slot>9000)
	{
		%slot=9000;
	}
	
	if(%slot<0)
	{
		%slot=0;
	}
	
	if($IItemA[%c.bl_id,%slot]>0)
	{ // STARTING ITEM DEFINITIONS
		call("InvItemUsed_"@$InvItemType[%slot],%c,%slot,%a1,%a2,%a3,%a4);
	} //ITEM DEFINITIONS COMPLETE
	else
	{
		messageclient(%c,"","\c0Error : You do not have this item.");
	}
}

function servercmdgrantitem(%c,%targ,%item,%amount)
{
	if(%c.isSuperAdmin)
	{
		%t=findclientbyname(%targ);
		%i=finditembyname(strreplace(%item,"_"," "));
		$IItemA[%t.bl_id,%i]+=%amount;
		messageclient(%c,"","\c2You granted\c0" SPC %t.name SPC "\c2\c0" SPC %amount SPC $InvItemName[%i] @ "s\c2!");
		messageclient(%t,"","\c2" @ %c.name SPC "\c2has granted you \c0" SPC %amount SPC $InvItemName[%i] @ "s\c2!");
	}
}

function servercmdgiveitem(%c,%targ,%item,%amount)
{
	%t=findclientbyname(%targ);
	%i=finditembyname(strreplace(%item,"_"," "));
	if($IItemA[%c.bl_id,%i]>=%amount&&%amount>0)
	{
		$IItemA[%c.bl_id,%i]-=%amount;
		$IItemA[%t.bl_id,%i]+=%amount;
		messageclient(%c,"","\c2You given\c0" SPC %t.name SPC "\c2\c0" SPC %amount SPC $InvItemName[%i] @ "s\c2!");
		messageclient(%t,"","\c2" @ %c.name SPC "\c2has given you \c0" SPC %amount SPC $InvItemName[%i] @ "s\c2!");
	}
	else
	{
		messageclient(%c,"","\c0Error, you dont have this much or you tried to trade a number less than or equal to 0!");
	}
}

function servercmdgrantitembyid(%c,%targ,%item,%amount)
{
	if(%c.isSuperAdmin)
	{
		%t=findclientbyname(%targ);
		%i=%item;
		$IItemA[%t.bl_id,%i]+=%amount;
		messageclient(%c,"","\c2You granted\c0" SPC %t.name SPC "\c2\c0" SPC %amount SPC $InvItemName[%i] @ "s\c2!");
		messageclient(%t,"","\c2" @ %c.name SPC "\c2has granted you \c0" SPC %amount SPC $InvItemName[%i] @ "s\c2!");
	}
}

function servercmdgiveitembyid(%c,%targ,%item,%amount)
{
	%t=findclientbyname(%targ);
	%i=%item;
	if($IItemA[%c.bl_id,%i]>=%amount&&%amount>0)
	{
		$IItemA[%c.bl_id,%i]-=%amount;
		$IItemA[%t.bl_id,%i]+=%amount;
		messageclient(%c,"","\c2You given\c0" SPC %t.name SPC "\c2\c0" SPC %amount SPC $InvItemName[%i] @ "s\c2!");
		messageclient(%t,"","\c2" @ %c.name SPC "\c2has given you \c0" SPC %amount SPC $InvItemName[%i] @ "s\c2!");
	}
	else
	{
		messageclient(%c,"","\c0Error, you dont have this much or you tried to trade a number less than or equal to 0!");
	}
}

//Combining
function AddItemCombine(%i1,%i1a,%i2,%i2a,%i3)
{
	if($ItemsCombined[%i1,%i2]!$="")
	{
		$ItemsCombined[%i1,%i2]=$ItemsCombined[%i1,%i2]@" "@%i3@" "@%i1a@" "@%i2a;
	}
	else
	{
		$ItemsCombined[%i1,%i2]=%i3@" "@%i1a@" "@%i2a;
	}
}

function AddScriptedItemCombine(%i1,%i2,%name,%func)
{
	if($ItemsCombined[%i1,%i2]!$="")
	{
		$ItemsCombined[%i1,%i2]=$ItemsCombined[%i1,%i2]@" "@"SCRIPTED"@" "@0@" "@0;
	}
	else
	{
		$ItemsCombined[%i1,%i2]="SCRIPTED"@" "@0@" "@0;
	}
	$ICName[%i1,%i2]=%name;
	$ICFunc[%i1,%i2]=%func;
}

function servercmdcombineitems(%c,%a,%b,%ci)
{
	%str=getwords($ItemsCombined[%a,%b],%ci*3,(%ci*3)+3);
	echo(%str);
	if($Itemscombined[%a,%b]!$="")
	{
		if(getword(%str,0)$="SCRIPTED")
		{
			call("InvItemsCombined_"@$ICFunc[%a,%b],%c);
		}
		else
		{
			if(getword(%str,0)!$=""&&getword(%str,1)!$=""&&getword(%str,2)!$="")
				if(GetItemCount(%c,%a)>=getword(%str,1)&&GetItemCount(%c,%b)>=getword(%str,2))
				{
					SubItemCount(%c,%a,getword(%str,1));
					SubItemCount(%c,%b,getword(%str,2));
					
					AddItemCount(%c,getword(%str,0),1);
					messageclient(%c,"","\c2Item combining succeeded!");
				}
				else
				{
					messageclient(%c,"","\c0Item combining failed : You do not have enough items to combine!");
				}
		}
	}
	else
	{
		messageclient(%c,"","\c0Item combining failed : Invalid combination!");
	}
}



//Packs
function execip(%file)
{
%fo = new FileObject();
%fo.openforread(%file);
while(!%fo.isEOF())
{
%IF=%fo.readline();
evalip(%IF);
}
%fo.close();
%fo.delete();
}

function evalip(%str)
{
%IF=%str;
switch$(getword(strupr(%IF),0))
{
	case "NEWITEM":
		$NI=MakeNewItem(strreplace(getword(%IF,1),"_"," "),strreplace(getword(%IF,2),"_"," "));
		if($ICTRACE)
		{
			IC_echo("Created a new item :");
			IC_echo("NAME : "@strreplace(getword(%IF,1),"_"," "));
			IC_echo("TYPE : "@strreplace(getword(%IF,2),"_"," "));
		}
	case "STAT":
		SetItemStat($NI,strreplace(getword(%IF,1),"_"," "),getwords(%IF,2));
		if($ICTRACE)
		{
			IC_echo("Added a stat to the last created item :");
			IC_echo("ITEM : "@GetItemStat($NI,"Name"));
			IC_echo("STAT : "@strreplace(getword(%IF,1),"_"," "));
			IC_echo("CONTENTS :"@getwords(%IF,2));
		}
	case "EXECFILE":
		exec(getwords(%IF,1));
		if($ICTRACE)
		{
			IC_echo("Executed a file :");
			IC_echo("FILE : "@getwords(%IF,1));
		}
	case "ADDCOMBO":
		AddItemCombine(finditembyname(strreplace(getword(%IF,1),"_"," ")),getword(%IF,2),finditembyname(strreplace(getword(%IF,3),"_"," ")),getword(%IF,4),finditembyname(strreplace(getword(%IF,5),"_"," ")));
		if($ICTRACE)
		{
			IC_echo("Added a new combination :");
			IC_echo("ITEM 1 : "@strreplace(getword(%IF,1),"_"," "));
			IC_echo("ITEM 1 AMOUNT : "@getword(%IF,2));
			IC_echo("ITEM 2 : "@strreplace(getword(%IF,3),"_"," "));
			IC_echo("ITEM 2 AMOUNT : "@getword(%IF,4));
			IC_echo("ITEM 3 : "@strreplace(getword(%IF,5),"_"," "));
		}
	case "ADDSCOMBO":
		AddScriptedItemCombine(finditembyname(strreplace(getword(%IF,1),"_"," ")), finditembyname(strreplace(getword(%IF,2),"_"," ")), strreplace(getword(%IF,3),"_"," "), strreplace(getword(%IF,4),"_"," "));
		if($ICTRACE)
		{
			IC_echo("Added a new scripted combination :");
			IC_echo("ITEM 1 : "@strreplace(getword(%IF,1),"_"," "));
			IC_echo("ITEM 2 : "@strreplace(getword(%IF,2),"_"," "));
			IC_echo("Name : "@strreplace(getword(%IF,3),"_"," "));
			IC_echo("Function : "@strreplace(getword(%IF,4),"_"," "));
		}
	case "TRACE":
		$ICTRACE=getword(%IF,1);
	default:
		if(getsubstr(%IF,0,1)!$="#"&&%IF!$="")
		{
			IC_echo("ERROR : Syntax error");
		}
		
}
}

function IC_echo(%text)
{
	for(%a=0;%a<clientgroup.getcount();%a++)
	{
		if(clientgroup.getobject(%a).isSuperAdmin)
		{
			commandtoclient(clientgroup.getobject(%a),'AddISConsoleLine',%text);
		}
	}
}

function servercmdISC_eval(%c,%eval)
{
	if(%c.isSuperAdmin)
	{
		IC_echo(%c.name@" ==> "@%eval);
		evalip(%eval);
	}
}

function execiepacks()
{
	%file=findfirstfile("Add-Ons/Inventory/Expansions/*.e");
	echo("Loading Expansion Packs");
	while(%file!$="")
	{
		echo("Loading ExpansionPack : "@%file);
		exec(%file);
		echo("Loaded");
		%file=findnextfile("Add-Ons/Inventory/Expansions/*.e");
	}
	echo("Done loading Expansion Packs");
}
if($Execediepacks$="") {execiepacks();$Execediepacks=1;}

function execitempacks()
{
	$PoI=-1;
	%file=findfirstfile("Add-Ons/Inventory/ItemPacks/*.ipack");
	echo("Loading Item Packs");
	while(%file!$="")
	{
		echo("Loading ItemPack : "@%file);
		execip(%file);
		echo("Loaded");
		%file=findnextfile("Add-Ons/Inventory/ItemPacks/*.ipack");
	}
	echo("Done loading Item Packs");
}
if($Execeditempacks$="") {execitempacks();$Execeditempacks=1;}

//GUI interfacing

function servercmdgetcombos(%c,%a,%b)
{
%cn=getwordcount($ItemsCombined[%a,%b]);
if($ItemsCombined[%a,%b]$="")
{
	messageclient("","\c0Error : There are no combinations for those items!");
}
else
{
	for(%i=0;%i<%cn;%i+=3)
	{
		%str=getwords($ItemsCombined[%a,%b],%i,%i+3);
		if($ICName[%a,%b]$="")
		{
			%ist=GetItemStat(getword(%str,0),"Name");
		}
		else
		{
			%ist=$ICName[%a,%b];
		}
		commandtoclient(%c,'setcombos',%i/3,%ist,getword(%str,1),getword(%str,2));
	}
}
}

function servercmdgetcombinations(%c,%a,%b)
{
%cn=getwordcount($ItemsCombined[%a,%b]);
if($ItemsCombined[%a,%b]$="")
{
	messageclient("","\c0Error : There are no combinations for those items!");
}
else
{
	for(%i=0;%i<%cn;%i+=3)
	{
		%str=getwords($ItemsCombined[%a,%b],%i,%i+3);
		if($ICName[%a,%b]$="")
		{
			%ist=GetItemStat(getword(%str,0),"Name");
		}
		else
		{
			%ist=$ICName[%a,%b];
		}
		messageclient(%c,"","\c7[\c2"@%i/3@"\c7] \c2"@%ist@" \c3-\c2 "@getword(%str,1)@" "@getword(%str,2));
	}
}
}

function servercmdgetinventorylist(%c)
{
	commandtoclient(%c,'clearinventorylist');
	for(%a=0;%a<=$PoI;%a++)
	{
		if ($IItemA[%c.bl_id,%a]>=1)
		{
			commandtoclient(%c,'addinventoryitem',%a,GetItemStat(%a,"Name"),GetItemCount(%c,%a));
		}
	}
}

function servercmdgetitemdata(%c,%id)
{
	commandtoclient(%c,'setinventorydesc',$InvItemName[%id],$InvItemDesc[%id],$IItemA[%c.bl_id,%id],GetItemStat(%id,"A1"),GetItemStat(%id,"A2"),GetItemStat(%id,"A3"),GetItemStat(%id,"A4"));
}

function servercmdgetcombineitems(%c)
{
	for(%a=0;%a<=$PoI;%a++)
	{
		if ($IItemA[%c.bl_id,%a]>=1)
		{
			commandtoclient(%c,'setcombineitems',%a,GetItemStat(%a,"Name"),GetItemCount(%c,%a));
		}
	}
}

function servercmdIAGuiRequest(%c)
{
	if(%c.isSuperAdmin)
	{
		commandtoclient(%c,'IARequestAccepted');
	}
	else
	{
		commandtoclient(%c,'IARequestDenied');
	}
}

function servercmdIA_Getallitems(%c)
{
	if(%c.isSuperAdmin)
	{
		for(%a=0;%a<=$PoI;%a++)
		{
			commandtoclient(%c,'SetIAItems',%a,GetItemStat(%a,"Name"));
		}
	}
}

//Dropping

datablock ItemData(IIDItem)
{
   category = "Weapon";
   className = "Weapon";

   shapeFile = "base/data/shapes/brickWeapon.dts";
   mass = 1;
   density = 0.2;
   elasticity = 0.4;
   friction = 0.6;
   emap = true;

   doColorShift = true;
   colorShiftColor = "1 0.5 0 1";
   image = IIIImage;
   candrop = true;
};

datablock ShapeBaseImageData(IIIImage)
{
   shapeFile = "base/data/shapes/brickWeapon.dts";
   emap = true;

   doColorShift = true;
   colorShiftColor = "1 0.5 0 1";
};

package IInventory
{
	function Armor::onCollision(%this, %obj, %col, %thing, %other)
	{
		if(%col.dataBlock $= "IIDItem")
		{
			AddItemCount(%obj.client,%col.ItemId,%col.ItemAmount);
			messageclient(%obj.client,'MsgPickupItem',"\c2You picked up a(n) \c3"@GetItemStat(%col.ItemId,"Name")@"\c1("@%col.ItemAmount*1@")");
			%col.delete();
		}
		Parent::onCollision(%this, %obj, %col, %thing, %others);
	}
};ActivatePackage(IInventory);

function servercmddropiitem(%c,%id,%amount)
{
	%amount=mfloor(%amount);
	if(%amount>0&&%amount<=GetItemCount(%c,%id))
	{
		%item = new Item()
		{
			datablock = IIDItem;
		};
		%item.setVelocity(VectorScale(%c.player.getEyeVector(), 10));
		%item.setTransform(setWord(%c.player.getTransform(), 2, getWord(%c.player.getTransform(), 2) + 2));
		MissionCleanup.add(%item);
		%item.setShapeName(GetItemStat(%id,"Name")@"["@%amount*1@"]");
		%item.canPickup=0;
		%item.ItemId=%id;
		%item.ItemAmount=%amount;
		SubItemCount(%c,%id,%amount);
		messageclient(%c,'MsgDropItem',"\c2You dropped a(n) \c3"@GetItemStat(%id,"Name")@"\c1("@%amount*1@")");
	}
}

function servercmdgetitemarguments(%c,%i)
{
	messageclient(%c,"","\c2Listing the arguments...");
	messageclient(%c,"","\c7[\c21\c7] A1 : "@GetItemStat(%i,"A1"));
	messageclient(%c,"","\c7[\c22\c7] A2 : "@GetItemStat(%i,"A2"));
	messageclient(%c,"","\c7[\c23\c7] A3 : "@GetItemStat(%i,"A3"));
	messageclient(%c,"","\c7[\c24\c7] A4 : "@GetItemStat(%i,"A4"));
}

function servercmdinventoryhelp(%c)
{
	messageclient(%c,"","\c2--Inventory system help ["@$IS::VersionNumber@"]--");
	messageclient(%c,"","\c6/listinventory to list your items.");
	messageclient(%c,"","\c6/useitem I1 (A1 A2 A3 A4) to use item id I1 , A1 A2 A3 and A4 are arguments taken by the item.");
	messageclient(%c,"","\c6/getcombinations I1 I2 to get all the combinations of item id I1 and I2.");
	messageclient(%c,"","\c6/combineitems I1 I2 C1 to combine I1 and I2 to make combo ID C1.");
	messageclient(%c,"","\c6/getitemarguments I1 is to get the arguments taken by an item of ID I1.");
	messageclient(%c,"","\c2---");
	messageclient(%c,"","\c6Item IDs and combo IDs are the numbers standing next to the items in the listings.");
	messageclient(%c,"","\c2---");
}

%f=findFirstFile("Add-Ons/*.cs");
while(fileBase(%f) !$= "Script_Inventory"){%f=findNextFile("Add-Ons/*.cs");}