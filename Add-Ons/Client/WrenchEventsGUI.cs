//GUI References:
//-1: No option.
//0: Tickbox [Text]
//1: Number [Text, Max Digits]
//2: Text [Text, Default]
//3: Select Box [Text, [Value 0...n %[Data Ref] - Seperator _ Space]
//Example: 3 Particles %ParticleEmitterData - AdminWand_A AdminWand_B
//4: Select Box 2 [Text, !PREVIOUSVALX [Value 0...n %[Data Ref] - Seperator _ Space] !PREVIOUSVALY : [Value 0...n %[Data Ref] - Seperator _ Space]]
//Example: 4 Weapons !Special Fire Spread Aiming Artillery_Rocket !Weapons : %ItemData
//^^ Only works if the control right above it is a Select Box (or another select box 2 which would get really confusing)
//5: Direction Chooser [Text, Style [0 = NESW, 1 = UDNESW, 2 = Altitude]]
//6: Color Chooser [Text]
function clientexec(){exec("./WrenchEVentsGUI.cs");}

if(!isObject(wrenchEventDlg)){exec("./wrencheventdlg.gui");}
if(!isObject(wrenchEventSetDlg)){exec("./wrencheventsetdlg.gui");}
if(!isObject(butWrenchEvents))
{
      new GuiBitmapButtonCtrl(butWrenchEvents) {
         profile = "BlockButtonProfile";
         horizSizing = "left";
         vertSizing = "bottom";
         position = "110 251";
         extent = "76 38";
         minExtent = "8 2";
         visible = "1";
         command = "canvas.pushDialog(wrenchEventDlg);";
         text = "Events";
         groupNum = "-1";
         buttonType = "PushButton";
         bitmap = "base/client/ui/button2";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         mKeepCached = "0";
         mColor = "0 128 255 255";
      };
}
schedule(400,0,addwindow);
function addwindow()
{
 Wrench_Window.add(butWrenchEvents);
}

package WrenchGet
{
 function PlayGUI::onWake(%this)
 {
  Parent::onWake(%this);
 }
 function WrenchEventDlg::onWake(%this)
 {
  //Parent::onWake(%this);
  if(!chkWrenchEventTime_lock.getValue())
  {
   commandtoserver('getWrenchEventList');
   butWrenchEventAdd.setColor("0 255 0 255");butWrenchEventAdd.setActive(1);
  }
 }
 function WrenchEventDlg::onClose(%this)
 {
  if(!chkWrenchEventTime_lock.getValue())
  {
   for(%i=0;%i<10;%i++)
   {
    if(isObject(%a=("txtWrenchEventTime_txt" @ %i))){%a.delete();}
    if(isObject(%a=("txtWrenchEventTime_txt" @ %i @ "b"))){%a.delete();}
    if(isObject(%a=("txtWrenchEventTime_sec" @ %i))){%a.delete();}
    if(isObject(%a=("selWrenchEventTime_sel" @ %i))){%a.delete();}
    if(isObject(%a=("butWrenchEventTime_kill" @ %i))){%a.delete();}
   }
      %pos = WrenchEventWindow.getPosition();
      WrenchEventWindow.resize(getWord(%pos,0),getWord(%pos,1),265,141);
  }
 }
 function GUICanvas::popDialog(%this,%a,%b,%c)
 {
  Parent::popDialog(%this,%a,%b,%c);
  if(%a $= "WrenchEventDlg"){%a.onClose();}
 }
 function killWrenchEvent(%num)
 {
  for(%i=0;%i<10;%i++)
  {
   if(%i $= %num){%ms[%i] = "kill";continue;}
   %ms[%i] = (isObject(%a=("txtWrenchEventTime_sec" @ %i)) ? %a.getValue() : "kill");
   %ev[%i] = (isObject(%a=("selWrenchEventTime_sel" @ %i)) ? %a.getText() : "kill");
   if(isObject("selWrenchEventTime_sel" @ %i))
   {
    for(%j=0;%j<5;%j++)
    {
     %p[%i,%j] = (selWrenchEventTime_sel @ %i).ev[%j];
    }
   }
  }
  %setnum = 0;
  clientcmdClearWrenchEvents();
  for(%i=0;%i<10;%i++)
  {
   if(%setnum $= %num){%setnum++;}
   if(%ms[%i] !$= "kill")
   {
    clientcmdWrenchEventGUIRow(%ms[%i]*100,%ev[%i],%p[%i+%setnum,0],%p[%i+%setnum,1],%p[%i+%setnum,2],%p[%i+%setnum,3],%p[%i+%setnum,4]);
   }
  }
 }
function guipopupmenuctrl::onSelect(%this,%num,%text)
{
 %a = %this.getname();
 if(strStr(strLwr(%a),"selwrencheventtime_sel") !$= "-1")
 {
  if(%text !$= %this.lasttext){for(%i=0;%i<5;%i++){(selwrencheventtime_sel @ getSubStr(%a,22,1)).ev[%i] = "";}}
  %this.setText(%text);
  %this.lasttext = %text;
  if(%text !$= "NONE")
  {
   canvas.pushDialog(wrenchEventSetdlg);
 %p = wrencheventsetwindow.getPosition();
 WrenchEventSetWindow.resize(getWord(%p,0),getWord(%p,1),240,%height+40);
   setWrenchSetGUI(wrenchEventSetWindow,%text,getSubStr(%a,22,1));
  }
 }
 if(strStr(strLwr(%a),"ctrlwrencheventset") !$= "-1")
 {
  %this.setText(%text);
  if(("ctrlWrenchEventSet" @ getSubStr(%a,18,1)+1 @ "_1").isDependant !$= "")
  {
	%obj = ("ctrlWrenchEventSet" @ getSubStr(%a,18,1)+1 @ "_1");
	%obj.clear();
	for(%i=0;getWord(%obj.cat[%text],%i) !$= "";%i++)
	{
		%txt = strReplace(getWord(%obj.cat[%text],%i),"_"," ");
		%count = -1;
			if(getSubStr(%txt,0,1) $= "%")
			{
				%sub = getSubStr(%txt,1,60);
				if(datablockgroup.getCount() > 2){%g = datablockgroup;}else{%g = serverconnection;}
				for(%k=0;%k<%g.getCount();%k++)
				{
					if((%g.getObject(%k)).getClassname() $= %sub && %g.getObject(%k).uiname !$= "")
					{
						%obj.add(%g.getObject(%k).uiname,%count++);
					}
				}
			}
			else
			{
				%obj.add(%txt,%count++);
			}
	}
	%obj.setText(%obj.getTextByID(0));
  }
 }
}
};activatepackage(WrenchGet);

function clientcmdWrenchEventCat(%a,%name,%p0,%p1,%p2,%p3,%p4)
{
 $EventBricks::Client::WrenchCategory[%a] = %name;
 for(%i=0;%i<5;%i++)
 {
  $EventBricks::Client::WrenchEventCat[%name,%i] = %p[%i];
 }
}

function clientcmdClearWrenchEvents()
{
   for(%i=0;%i<10;%i++)
   {
    if(isObject(%a=("txtWrenchEventTime_txt" @ %i))){%a.delete();}
    if(isObject(%a=("txtWrenchEventTime_txt" @ %i @ "b"))){%a.delete();}
    if(isObject(%a=("txtWrenchEventTime_sec" @ %i))){%a.delete();}
    if(isObject(%a=("selWrenchEventTime_sel" @ %i))){%a.delete();}
    if(isObject(%a=("butWrenchEventTime_kill" @ %i))){%a.delete();}
   }
      %pos = WrenchEventWindow.getPosition();
      WrenchEventWindow.resize(getWord(%pos,0),getWord(%pos,1),265,141);
}

function clientcmdwrencheventmiscdata(%b,%t)
{
 txtWrenchTimeGroup.setText(%b);
 switch$(%t)
 {
  case 0: radWrenchEvTrust_none.performClick();
  case 1: radWrenchEvTrust_build.performClick();
  case 2: radWrenchEvTrust_full.performClick();
  case 3: radWrenchEvTrust_mini.performClick();
  default: radWrenchEvTrust_none.performClick();
 }
}

function clientcmdWrenchEventGUIRow(%ms,%ev,%p0,%p1,%p2,%p3,%p4)
{
      for(%i=0;isObject("txtWrenchEventTime_txt" @ %i);%i++){}
      if(%i >= 10){return;}
      %height = %i*22 + 20;
      %pos = WrenchEventWindow.getPosition();
      WrenchEventWindow.resize(getWord(%pos,0),getWord(%pos,1),265,141 + %height);
      %a = new GuiTextCtrl() {
         profile = "BlockListProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "8" SPC 70 + %height;
         extent = "32 18";
         minExtent = "8 2";
         visible = "1";
         text = "For ";
         maxLength = "255";
      };WrenchEventWindow.add(%a);%a.setName("txtWrenchEventTime_txt" @ %i);
      %a = new GuiTextCtrl() {
         profile = "BlockListProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "45" SPC 70 + %height;
         extent = "70 18";
         minExtent = "8 2";
         visible = "1";
         text = "00 ms, ";
         maxLength = "255";
      };WrenchEventWindow.add(%a);%a.setName("txtWrenchEventTime_txt" @ %i @ "b");
      %a = new GuiTextEditCtrl() {
         profile = "GuiTextEditProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "37" SPC 70 + %height;
         extent = "20 18";
         minExtent = "8 2";
         visible = "1";
         text = (%ms $= "" ? "10" : %ms / 100);
         maxLength = "3";
         historySize = "0";
         password = "0";
         tabComplete = "0";
         sinkAllKeyEvents = "0";
      };WrenchEventWindow.add(%a);%a.setName("txtWrenchEventTime_sec" @ %i);
      %a = new GuiPopUpMenuCtrl() {
         profile = "GuiPopUpMenuProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "110" SPC 70 + %height;
         extent = "100 20";
         minExtent = "8 2";
         visible = "1";
         maxLength = "255";
         maxPopupHeight = "200";
      };WrenchEventWindow.add(%a);%a.setName("selWrenchEventTime_sel" @ %i);%a.setText(%ev);
      for(%b=0;%b<5;%b++){%a.ev[%b] = %p[%b];}
      for(%n=0;$EventBricks::Client::WrenchCategory[%n] !$= "";%n++){%a.add($EventBricks::Client::WrenchCategory[%n],%n);}
      %a.lasttext = (%ev !$= "" ? %ev : %a.getTextByID(0));
      %a.setText((%ev !$= "" ? %ev : %a.getTextByID(0)));
      %a = new GuiBitmapButtonCtrl() {
         profile = "BlockButtonProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "235" SPC 70 + %height;
         extent = "20 20";
         minExtent = "8 2";
         visible = "1";
         command = "killWrenchEvent(" @ %i @ ");";
         text = "X";
         groupNum = "-1";
         buttonType = "PushButton";
         bitmap = "base/client/ui/button2";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         mKeepCached = "0";
         mColor = "255 255 255 255";
      };WrenchEventWindow.add(%a);%a.setName("butWrenchEventTime_kill" @ %i);
      if(isObject("txtWrenchEventTime_txt9")){butWrenchEventAdd.setColor("255 255 255 255");butWrenchEventAdd.setActive(0);}else{butWrenchEventAdd.setColor("0 255 0 255");butWrenchEventAdd.setActive(1);}
}

function wrenchEvParamSet()
{
 for(%i=0;$EventBricks::Client::WrenchEventCat[$EventBricks::Client::EditName,%i] !$= "";%i++)
 {
  (selWrenchEventTime_sel @ $EventBricks::Client::EditState).ev[%i] = getWrenchEvParam((selWrenchEventTime_sel @ $EventBricks::Client::EditState).gettext(),%i);
 }
 canvas.popDialog(wrenchEventSetDlg);
}

function sendWrenchEvents()
{
 commandtoserver('clearwrenchevents');
 %t = 0;
 if(radWrenchEvTrust_none.getValue()){%t = 0;}
 if(radWrenchEvTrust_build.getValue()){%t = 1;}
 if(radWrenchEvTrust_full.getValue()){%t = 2;}
 if(radWrenchEvTrust_mini.getValue()){%t = 3;}
 commandtoserver('wrenchevgroup',txtWrenchTimeGroup.getValue(),%t);
 for(%i=0;%i<10;%i++)
 {
  if(isObject(selWrenchEventTime_sel @ %i) && (selWrenchEventTime_sel @ %i).gettext() !$= "NONE")
  {
   commandtoserver('wrencheventadd',(selWrenchEventTime_sel @ %i).gettext(),(txtWrenchEventTime_sec @ %i).getValue(),txtWrenchTimeGroup.getValue(),(selWrenchEventTime_sel @ %i).ev[0],(selWrenchEventTime_sel @ %i).ev[1],(selWrenchEventTime_sel @ %i).ev[2],(selWrenchEventTime_sel @ %i).ev[3],(selWrenchEventTime_sel @ %i).ev[4]);
  }
 }
 canvas.popDialog(wrenchEventDlg);
}


function setWrenchSetGUI(%this,%name,%ev)
{
 clearWrenchSetGUI();
 %height = 25;
 $EventBricks::Client::EditState = %ev;
 $EventBricks::Client::EditName = %name;
 for(%i=0;$EventBricks::Client::WrenchEventCat[%name,%i] !$= "";%i++)
 {
	%param = (selWrenchEventTime_sel @ %ev).ev[%i];
	switch$(getWord($EventBricks::Client::WrenchEventCat[%name,%i],0))
	{
		case "-1": %height+=22;continue;
		case "0":
			%a = new GuiTextCtrl() {
				profile = "GuiTextProfile";
            				horizSizing = "right";
            				vertSizing = "bottom";
            				position = "10" SPC %height+4;
            				extent = "85 18";
            				minExtent = "8 8";
            				visible = "1";
            				variable = "";
				text = strReplace(getWord($EventBricks::Client::WrenchEventCat[%name,%i],1),"_"," ");
			};%this.add(%a);%a.setName("ctrlWrenchEventSet" @ %i @ "_0");
			%a = new GuiCheckBoxCtrl() {
            				profile = "GuiCheckBoxProfile";
            				horizSizing = "right";
            				vertSizing = "bottom";
            				position = "85" SPC %height;
            				extent = "25 25";
            				minExtent = "8 8";
            				visible = "1";
            				variable = "";
            				helpTag = "0";
            				text = "";
            				groupNum = %i;
            				buttonType = "ToggleButton";
              			maxLength = "255";
       			};%this.add(%a);%a.setName("ctrlWrenchEventSet" @ %i @ "_1");%height+=22;if(%param){%a.performClick();}
		case "1":
			%a = new GuiTextCtrl() {
				profile = "GuiTextProfile";
            				horizSizing = "right";
            				vertSizing = "bottom";
            				position = "10" SPC %height+2;
            				extent = "75 18";
            				minExtent = "8 8";
            				visible = "1";
            				variable = "";
				text = strReplace(getWord($EventBricks::Client::WrenchEventCat[%name,%i],1),"_"," ");
			};%this.add(%a);%a.setName("ctrlWrenchEventSet" @ %i @ "_0");
			%a =       new GuiTextEditCtrl() {
         				profile = "GuiTextEditProfile";
         				horizSizing = "right";
         				vertSizing = "bottom";
         				position = "65" SPC %height+2;
         				extent = "30 18";
         				minExtent = "8 2";
         				visible = "1";
         				maxLength = getWord($EventBricks::Client::WrenchEventCat[%name,%i],2);
         				historySize = "0";
         				password = "0";
         				tabComplete = "0";
         				sinkAllKeyEvents = "0";
      			};%this.add(%a);%a.setValue((%param $= "" ? 1 : %param));%a.setName("ctrlWrenchEventSet" @ %i @ "_1");%height+=22;
		case "2":
			%a = new GuiTextCtrl() {
				profile = "GuiTextProfile";
            				horizSizing = "right";
            				vertSizing = "bottom";
            				position = "10" SPC %height+2;
            				extent = "75 18";
            				minExtent = "8 8";
            				visible = "1";
            				variable = "";
				text = strReplace(getWord($EventBricks::Client::WrenchEventCat[%name,%i],1),"_"," ");
			};%this.add(%a);%a.setName("ctrlWrenchEventSet" @ %i @ "_0");
			%a =       new GuiTextEditCtrl() {
         				profile = "GuiTextEditProfile";
         				horizSizing = "right";
         				vertSizing = "bottom";
         				position = "65" SPC %height+2;
         				extent = "155 18";
         				minExtent = "8 2";
         				visible = "1";
         				maxLength = 20;
        				historySize = "0";
         				password = "0";
         				tabComplete = "0";
         				sinkAllKeyEvents = "0";
      			};%this.add(%a);%a.setName("ctrlWrenchEventSet" @ %i @ "_1");%txt = strReplace(getWord($EventBricks::Client::WrenchEventCat[%name,%i],2),"_"," ");
			  if(%param !$= ""){%a.setValue(%param);}else{%a.setValue(%txt);}%height+=22;
		case "3":
			%a = new GuiTextCtrl() {
				profile = "GuiTextProfile";
            				horizSizing = "right";
            				vertSizing = "bottom";
            				position = "10" SPC %height+2;
            				extent = "75 18";
            				minExtent = "8 8";
            				visible = "1";
            				variable = "";
				text = strReplace(getWord($EventBricks::Client::WrenchEventCat[%name,%i],1),"_"," ");
			};%this.add(%a);%a.setName("ctrlWrenchEventSet" @ %i @ "_0");
			%a = new GuiPopUpMenuCtrl() {
			         profile = "BlockButtonProfile";
			         horizSizing = "right";
			         vertSizing = "bottom";
			         position = "65" SPC %height;
			         extent = "155 20";
			         minExtent = "8 2";
			         visible = "1";
			         maxLength = "255";
			         maxPopupHeight = "200";
		      };%this.add(%a);%a.setName("ctrlWrenchEventSet" @ %i @ "_1");%count = -1;
	                    for(%j=2;(%txt = strReplace(getWord($EventBricks::Client::WrenchEventCat[%name,%i],%j),"_"," ")) !$= "";%j++)
		      {
			if(getSubStr(%txt,0,1) $= "%")
			{
				%sub = getSubStr(%txt,1,60);
				if(datablockgroup.getCount() > 2){%g = datablockgroup;}else{%g = serverconnection;}
				for(%k=0;%k<%g.getCount();%k++)
				{
					if((%g.getObject(%k)).getClassname() $= %sub && %g.getObject(%k).uiname !$= "")
					{
						%a.add(%g.getObject(%k).uiname,%count++);
					}
				}
			}
			else
			{
				%a.add(%txt,%count++);
			}
		      }
		      %txt = strReplace(getWord($EventBricks::Client::WrenchEventCat[%name,%i],2),"_"," ");
		      if(%param !$= ""){%a.setValue(%param);}else{%a.setValue(%a.getTextByID(0));}
		      %a.schedule(100,onSelect,0,%a.getValue());
		      %height+=22;
		      case "4":
			%a = new GuiTextCtrl() {
				profile = "GuiTextProfile";
            				horizSizing = "right";
            				vertSizing = "bottom";
            				position = "10" SPC %height+2;
            				extent = "75 18";
            				minExtent = "8 8";
            				visible = "1";
            				variable = "";
				text = strReplace(getWord($EventBricks::Client::WrenchEventCat[%name,%i],1),"_"," ");
			};%this.add(%a);%a.setName("ctrlWrenchEventSet" @ %i @ "_0");
			%a = new GuiPopUpMenuCtrl() {
			         profile = "BlockButtonProfile";
			         horizSizing = "right";
			         vertSizing = "bottom";
			         position = "65" SPC %height;
			         extent = "155 20";
			         minExtent = "8 2";
			         visible = "1";
			         maxLength = "255";
			         maxPopupHeight = "200";
			         isDependant = %name;
		      };%this.add(%a);%a.setName("ctrlWrenchEventSet" @ %i @ "_1");%str = "";%laststr = getSubStr(strReplace(getWord($EventBricks::Client::WrenchEventCat[%name,%i],2),"_"," "),1,60);
	                    for(%j=2;(%txt = getWord($EventBricks::Client::WrenchEventCat[%name,%i],%j)) !$= "";%j++)
		      {
			if(getSubStr(%txt,0,1) $= "!")
			{
				%a.cat[getSubStr(%laststr,1,60)] = %str;
				%str = "";
				%laststr = %txt;
			}
			else
			{
				%str = %str @ %txt @ " ";
			}
		      }
		      %a.cat[getSubStr(%laststr,1,60)] = %str;
		      %a.schedule(200,setText,%param);
		      %height+=22;
		      case "5":
			%a = new GuiTextCtrl() {
				profile = "GuiTextProfile";
            				horizSizing = "right";
            				vertSizing = "bottom";
            				position = "10" SPC %height+5;
            				extent = "75 18";
            				minExtent = "8 8";
            				visible = "1";
            				variable = "";
				text = strReplace(getWord($EventBricks::Client::WrenchEventCat[%name,%i],1),"_"," ");
			};%this.add(%a);%a.setName("ctrlWrenchEventSet" @ %i @ "_0");
			if(getWord($EventBricks::Client::WrenchEventCat[%name,%i],2) $= "0")
			{
			 %a = new GuiRadioCtrl() {
         				profile = "GuiRadioProfile";
         				horizSizing = "right";
         				vertSizing = "bottom";
         				position = "65" SPC %height-1;
         				extent = "53 30";
         				minExtent = "8 2";
         				visible = "1";
         				text = "N";
         				groupNum = %i;
         				buttonType = "RadioButton";
      			 };%this.add(%a);%a.setName("ctrlWrenchEventSet" @ %i @ "_1");if(%param $= "0"){%a.performClick();}
			 %a = new GuiRadioCtrl() {
         				profile = "GuiRadioProfile";
         				horizSizing = "right";
         				vertSizing = "bottom";
         				position = "90" SPC %height-1;
         				extent = "53 30";
         				minExtent = "8 2";
         				visible = "1";
         				text = "E";
         				groupNum = %i;
         				buttonType = "RadioButton";
      			 };%this.add(%a);%a.setName("ctrlWrenchEventSet" @ %i @ "_2");if(%param $= "1"){%a.performClick();}
			 %a = new GuiRadioCtrl() {
         				profile = "GuiRadioProfile";
         				horizSizing = "right";
         				vertSizing = "bottom";
         				position = "115" SPC %height-1;
         				extent = "53 30";
         				minExtent = "8 2";
         				visible = "1";
         				text = "S";
         				groupNum = %i;
         				buttonType = "RadioButton";
      			 };%this.add(%a);%a.setName("ctrlWrenchEventSet" @ %i @ "_3");if(%param $= "2"){%a.performClick();}
			 %a = new GuiRadioCtrl() {
         				profile = "GuiRadioProfile";
         				horizSizing = "right";
         				vertSizing = "bottom";
         				position = "140" SPC %height-1;
         				extent = "53 30";
         				minExtent = "8 2";
         				visible = "1";
         				text = "W";
         				groupNum = %i;
         				buttonType = "RadioButton";
      			 };%this.add(%a);%a.setName("ctrlWrenchEventSet" @ %i @ "_4");if(%param $= "3"){%a.performClick();}
			 %height+=22;
			}
			if(getWord($EventBricks::Client::WrenchEventCat[%name,%i],2) $= "1")
			{
			 %a = new GuiRadioCtrl() {
         				profile = "GuiRadioProfile";
         				horizSizing = "right";
         				vertSizing = "bottom";
         				position = "65" SPC %height-1;
         				extent = "53 30";
         				minExtent = "8 2";
         				visible = "1";
         				text = "U";
         				groupNum = %i;
         				buttonType = "RadioButton";
      			 };%this.add(%a);%a.setName("ctrlWrenchEventSet" @ %i @ "_1");if(%param $= "0"){%a.performClick();}
			 %a = new GuiRadioCtrl() {
         				profile = "GuiRadioProfile";
         				horizSizing = "right";
         				vertSizing = "bottom";
         				position = "90" SPC %height-1;
         				extent = "53 30";
         				minExtent = "8 2";
         				visible = "1";
         				text = "D";
         				groupNum = %i;
         				buttonType = "RadioButton";
      			 };%this.add(%a);%a.setName("ctrlWrenchEventSet" @ %i @ "_2");if(%param $= "1"){%a.performClick();}
			 %a = new GuiRadioCtrl() {
         				profile = "GuiRadioProfile";
         				horizSizing = "right";
         				vertSizing = "bottom";
         				position = "115" SPC %height-1;
         				extent = "53 30";
         				minExtent = "8 2";
         				visible = "1";
         				text = "N";
         				groupNum = %i;
         				buttonType = "RadioButton";
      			 };%this.add(%a);%a.setName("ctrlWrenchEventSet" @ %i @ "_3");if(%param $= "2"){%a.performClick();}
			 %a = new GuiRadioCtrl() {
         				profile = "GuiRadioProfile";
         				horizSizing = "right";
         				vertSizing = "bottom";
         				position = "140" SPC %height-1;
         				extent = "53 30";
         				minExtent = "8 2";
         				visible = "1";
         				text = "E";
         				groupNum = %i;
         				buttonType = "RadioButton";
      			 };%this.add(%a);%a.setName("ctrlWrenchEventSet" @ %i @ "_4");if(%param $= "3"){%a.performClick();}
			 %a = new GuiRadioCtrl() {
         				profile = "GuiRadioProfile";
         				horizSizing = "right";
         				vertSizing = "bottom";
         				position = "165" SPC %height-1;
         				extent = "53 30";
         				minExtent = "8 2";
         				visible = "1";
         				text = "S";
         				groupNum = %i;
         				buttonType = "RadioButton";
      			 };%this.add(%a);%a.setName("ctrlWrenchEventSet" @ %i @ "_5");if(%param $= "4"){%a.performClick();}
			 %a = new GuiRadioCtrl() {
         				profile = "GuiRadioProfile";
         				horizSizing = "right";
         				vertSizing = "bottom";
         				position = "190" SPC %height-1;
         				extent = "53 30";
         				minExtent = "8 2";
         				visible = "1";
         				text = "W";
         				groupNum = %i;
         				buttonType = "RadioButton";
      			 };%this.add(%a);%a.setName("ctrlWrenchEventSet" @ %i @ "_6");if(%param $= "5"){%a.performClick();}
			 %height+=22;
			}
			if(getWord($EventBricks::Client::WrenchEventCat[%name,%i],2) $= "2")
			{
			 %a = new GuiBitmapCtrl() {
				   profile = "GuiDefaultProfile";
				   horizSizing = "right";
				   vertSizing = "bottom";
				   position = "65" SPC %height+15;
				   extent = "64 100";
				   minExtent = "8 2";
				   visible = "1";
				   bitmap = "base/client/ui/wrenchGUI_attitude";
				   wrap = "0";
				   lockAspectRatio = "0";
				   alignLeft = "0";
				   overflowImage = "0";
				   keepCached = "0";
				};%this.add(%a);%a.setName("ctrlWrenchEventSet" @ %i @ "_1");
			 %a = new GuiRadioCtrl() {
         				profile = "GuiRadioProfile";
         				horizSizing = "right";
         				vertSizing = "bottom";
         				position = 78 SPC %height-1;
         				extent = "53 30";
         				minExtent = "8 2";
         				visible = "1";
         				text = "";
         				groupNum = %i;
         				buttonType = "RadioButton";
      			 };%this.add(%a);%a.setName("ctrlWrenchEventSet" @ %i @ "_2");if(%param $= "0"){%a.performClick();}
			 %a = new GuiRadioCtrl() {
         				profile = "GuiRadioProfile";
         				horizSizing = "right";
         				vertSizing = "bottom";
         				position = 78 SPC %height+104;
         				extent = "53 30";
         				minExtent = "8 2";
         				visible = "1";
         				text = "";
         				groupNum = %i;
         				buttonType = "RadioButton";
      			 };%this.add(%a);%a.setName("ctrlWrenchEventSet" @ %i @ "_3");if(%param $= "1"){%a.performClick();}
			 %a = new GuiRadioCtrl() {
         				profile = "GuiRadioProfile";
         				horizSizing = "right";
         				vertSizing = "bottom";
         				position = 118 SPC %height+10;
         				extent = "53 30";
         				minExtent = "8 2";
         				visible = "1";
         				text = "";
         				groupNum = %i;
         				buttonType = "RadioButton";
      			 };%this.add(%a);%a.setName("ctrlWrenchEventSet" @ %i @ "_4");if(%param $= "2"){%a.performClick();}
			 %a = new GuiRadioCtrl() {
         				profile = "GuiRadioProfile";
         				horizSizing = "right";
         				vertSizing = "bottom";
         				position = 118 SPC %height+94;
         				extent = "53 30";
         				minExtent = "8 2";
         				visible = "1";
         				text = "";
         				groupNum = %i;
         				buttonType = "RadioButton";
      			 };%this.add(%a);%a.setName("ctrlWrenchEventSet" @ %i @ "_5");if(%param $= "3"){%a.performClick();}
			 %a = new GuiRadioCtrl() {
         				profile = "GuiRadioProfile";
         				horizSizing = "right";
         				vertSizing = "bottom";
         				position = 134 SPC %height+51;
         				extent = "53 30";
         				minExtent = "8 2";
         				visible = "1";
         				text = "";
         				groupNum = %i;
         				buttonType = "RadioButton";
      			 };%this.add(%a);%a.setName("ctrlWrenchEventSet" @ %i @ "_6");if(%param $= "4"){%a.performClick();}
			 %height+=128;
			}
			case "6":
			%a = new GuiTextCtrl() {
				profile = "GuiTextProfile";
            				horizSizing = "right";
            				vertSizing = "bottom";
            				position = "10" SPC %height+5;
            				extent = "100 22";
            				minExtent = "8 8";
            				visible = "1";
            				variable = "";
				text = strReplace(getWord($EventBricks::Client::WrenchEventCat[%name,%i],1),"_"," ");
			};%this.add(%a);%a.setName("ctrlWrenchEventSet" @ %i @ "_0");
			%a = new GuiSliderCtrl() {
				profile = "GuiSliderProfile";
            				horizSizing = "right";
            				vertSizing = "bottom";
            				position = "61" SPC %height+5;
            				extent = "100 22";
            				minExtent = "8 8";
			            	visible = "1";
            				variable = "";
				altcommand = "updateWrenchColor(" @ %i @ ");";
				range = "0 1";
				value= "1";
			};%this.add(%a);%a.setName("ctrlWrenchEventSet" @ %i @ "_1");%a.setValue((getWord(%param,0) $= "" ? 1 : getWord(%param,0)));
			%a = new GuiSliderCtrl() {
				profile = "GuiSliderProfile";
            				horizSizing = "right";
            				vertSizing = "bottom";
            				position = "61" SPC %height+27;
            				extent = "100 22";
            				minExtent = "8 8";
			            	visible = "1";
            				variable = "";
				range = "0 1";
				value= "1";
				altcommand = "updateWrenchColor(" @ %i @ ");";
			};%this.add(%a);%a.setName("ctrlWrenchEventSet" @ %i @ "_2");%a.setValue((getWord(%param,1) $= "" ? 1 : getWord(%param,1)));
			%a = new GuiSliderCtrl() {
				profile = "GuiSliderProfile";
            				horizSizing = "right";
            				vertSizing = "bottom";
            				position = "61" SPC %height+49;
            				extent = "100 22";
            				minExtent = "8 8";
			            	visible = "1";
            				variable = "";
				range = "0 1";
				value= "1";
				altcommand = "updateWrenchColor(" @ %i @ ");";
			};%this.add(%a);%a.setName("ctrlWrenchEventSet" @ %i @ "_3");%a.setValue((getWord(%param,2) $= "" ? 1 : getWord(%param,2)));
			%a = new GuiSliderCtrl() {
				profile = "GuiSliderProfile";
            				horizSizing = "right";
            				vertSizing = "bottom";
            				position = "61" SPC %height+72;
            				extent = "100 22";
            				minExtent = "8 8";
			            	visible = "1";
            				variable = "";
				range = "0 1";
				value= "1";
				altcommand = "updateWrenchColor(" @ %i @ ",ctrlWrenchEventSet" @ %i @ "_1.getValue() SPC ctrlWrenchEventSet" @ %i @ "_2.getValue() SPC ctrlWrenchEventSet" @ %i @ "_3.getValue() SPC ctrlWrenchEventSet" @ %i @ "_4.getValue());";
			};%this.add(%a);%a.setName("ctrlWrenchEventSet" @ %i @ "_4");%a.setValue((getWord(%param,3) $= "" ? 1 : getWord(%param,3)));
			%a = new GuiSwatchCtrl() {
				profile = "GuiSliderProfile";
            				horizSizing = "right";
            				vertSizing = "bottom";
            				position = "170" SPC %height+40;
            				extent = "20 20";
            				minExtent = "8 8";
			            	visible = "1";
            				variable = "";
				color = "128 128 128 255";
			};%this.add(%a);%a.setName("ctrlWrenchEventSet" @ %i @ "_5");%a.setColor((%param $= "" ? "1 1 1 1" : %param));
			%a = new GuiBitmapCtrl() {
				profile = "GuiSliderProfile";
            				horizSizing = "right";
            				vertSizing = "bottom";
            				position = "169" SPC %height+39;
            				extent = "22 22";
            				minExtent = "8 8";
			            	visible = "1";
            				variable = "";
			};%this.add(%a);%a.setName("ctrlWrenchEventSet" @ %i @ "_6");
			 %height+=95;

	}
 }
 %p = wrencheventsetwindow.getPosition();
 WrenchEventSetWindow.resize(getWord(%p,0),getWord(%p,1),240,%height+40);
}

function getWrenchEvParam(%name,%i)
{
	switch$(getWord($EventBricks::Client::WrenchEventCat[%name,%i],0))
	{
		case "-1": return "";
		case "0":
			return ("ctrlWrenchEventSet" @ %i @ "_1").getValue();
		case "1":
			return ("ctrlWrenchEventSet" @ %i @ "_1").getValue();
		case "2":
			return ("ctrlWrenchEventSet" @ %i @ "_1").getValue();
		case "3":
			return ("ctrlWrenchEventSet" @ %i @ "_1").getText();
		case "4":
			return ("ctrlWrenchEventSet" @ %i @ "_1").getText();
		      case "5":
			if(getWord($EventBricks::Client::WrenchEventCat[%name,%i],2) $= "0")
			{
				if(("ctrlWrenchEventSet" @ %i @ "_1").getValue()){return "0";}
				if(("ctrlWrenchEventSet" @ %i @ "_2").getValue()){return "1";}
				if(("ctrlWrenchEventSet" @ %i @ "_3").getValue()){return "2";}
				if(("ctrlWrenchEventSet" @ %i @ "_4").getValue()){return "3";}
				return 0;
			}
			if(getWord($EventBricks::Client::WrenchEventCat[%name,%i],2) $= "1")
			{
				if(("ctrlWrenchEventSet" @ %i @ "_1").getValue()){return "0";}
				if(("ctrlWrenchEventSet" @ %i @ "_2").getValue()){return "1";}
				if(("ctrlWrenchEventSet" @ %i @ "_3").getValue()){return "2";}
				if(("ctrlWrenchEventSet" @ %i @ "_4").getValue()){return "3";}
				if(("ctrlWrenchEventSet" @ %i @ "_5").getValue()){return "4";}
				if(("ctrlWrenchEventSet" @ %i @ "_6").getValue()){return "5";}
				return 0;
			}
			if(getWord($EventBricks::Client::WrenchEventCat[%name,%i],2) $= "2")
			{
				if(("ctrlWrenchEventSet" @ %i @ "_2").getValue()){return "0";}
				if(("ctrlWrenchEventSet" @ %i @ "_3").getValue()){return "1";}
				if(("ctrlWrenchEventSet" @ %i @ "_4").getValue()){return "2";}
				if(("ctrlWrenchEventSet" @ %i @ "_5").getValue()){return "3";}
				if(("ctrlWrenchEventSet" @ %i @ "_6").getValue()){return "4";}
				return 0;
			}
			case "6":
			return ("ctrlWrenchEventSet" @ %i @ "_1").getValue() SPC ("ctrlWrenchEventSet" @ %i @ "_2").getValue() SPC ("ctrlWrenchEventSet" @ %i @ "_3").getValue() SPC ("ctrlWrenchEventSet" @ %i @ "_4").getValue();
 }
}

function clearWrenchSetGUI()
{
 for(%i=0;isObject(("ctrlWrenchEventSet" @ %i @ "_0"));%i++)
 {
  
	for(%j=0;isObject("ctrlWrenchEventSet" @ %i @ "_" @ %j);%j++)
	{
		("ctrlWrenchEventSet" @ %i @ "_" @ %j).delete();
	}
 }
}

function updateWrenchColor(%i)
{
 if(!isObject("ctrlWrenchEventSet" @ %i @ "_5")){return;}
 %color = ("ctrlWrenchEventSet" @ %i @ "_1").getValue() SPC ("ctrlWrenchEventSet" @ %i @ "_2").getValue() SPC ("ctrlWrenchEventSet" @ %i @ "_3").getValue() SPC ("ctrlWrenchEventSet" @ %i @ "_4").getValue();
 ("ctrlWrenchEventSet" @ %i @ "_5").setColor(%color);
}

function clientcmdclearwrenchsavefile(%file)
{
 %f = new FileObject();
 if(!isFile("base/config/client/wrenchsaves/" @ %file)){return;}
 %f.openForWrite("base/config/client/wrenchsaves/" @ %file);%f.close();%f.delete();
}

function clientcmdappendwrenchsavefile(%file,%pos,%line)
{
 %f = new FileObject();
 //if(!isFile("base/config/client/wrenchsaves/" @ %file)){return;}
 %f.openForAppend("base/config/client/wrenchsaves/" @ %file);
  %f.writeLine(%pos TAB %line);
 %f.close();
 %f.delete();
}

function clientcmdWrenchLoadAccepted(%file)
{
 %file = strReplace(%file,".Wsave","");
 %f = new fileobject();
 %f.openForRead("base/config/client/WrenchSaves/" @ %file @ ".Wsave");
 for(%i=0;$EventBricks::Client::LineLoaded[%i] !$= "";%i++){$EventBricks::Client::LineLoaded[%i] = "";}
 for(%i=0;($EventBricks::Client::LineLoaded[%i]=%f.readLine()) !$= "";%i++){}
 %f.close();
 %f.delete();
 $WrenchEvents::Client::CurLine = 7;
 $WrenchEvents::Client::MaxLine = %i;
 commandtoserver('LoadEvents_line',$EventBricks::Client::LineLoaded[0],
				$EventBricks::Client::LineLoaded[1],
				$EventBricks::Client::LineLoaded[2],
				$EventBricks::Client::LineLoaded[3],
				$EventBricks::Client::LineLoaded[4],
				$EventBricks::Client::LineLoaded[5],
				$EventBricks::Client::LineLoaded[6],
				$EventBricks::Client::LineLoaded[7],%i,%i);
}

function clientcmdWrenchLoadNext()
{
 commandtoserver('LoadEvents_line',$EventBricks::Client::LineLoaded[$WrenchEvents::Client::CurLine++],
				$EventBricks::Client::LineLoaded[$WrenchEvents::Client::CurLine++],
				$EventBricks::Client::LineLoaded[$WrenchEvents::Client::CurLine++],
				$EventBricks::Client::LineLoaded[$WrenchEvents::Client::CurLine++],
				$EventBricks::Client::LineLoaded[$WrenchEvents::Client::CurLine++],
				$EventBricks::Client::LineLoaded[$WrenchEvents::Client::CurLine++],
				$EventBricks::Client::LineLoaded[$WrenchEvents::Client::CurLine++],
				$EventBricks::Client::LineLoaded[$WrenchEvents::Client::CurLine++]);
	%a = $WrenchEvents::Client::CurLine;
	%b = $WrenchEvents::Client::MaxLine;
	if(%b <= 0){return;}
	%str = "\c2";
	%fin = (%a / %b) * 65;
	if($EventBricks::Client::LineLoaded[$WrenchEvents::Client::CurLine] $= ""){%str = %str @ "\c4";%done = "1";}
	for(%i=0;%i<=65;%i++)
	{
		if(%i >= %fin && %done $= ""){%str = %str @ "\c0";%done = "1";}
		%str = %str @ "|";
	}
	clientcmdBottomPrint(%str,1,3);
 if($EventBricks::Client::LineLoaded[$WrenchEvents::Client::CurLine] $= "")
 {
  commandtoserver('LoadEvents_fin');
 }
}