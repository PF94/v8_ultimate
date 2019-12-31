if($Server::Dedicated $= 1)
	return;

if(!isObject(macroGui))
	exec("./macroGui.gui");

if (!$addedMacroMaps)
{
	$remapDivision[$remapCount] = "Macro Saver";
	$remapName[$remapCount] = "Toggle Macro Saver";
	$remapCmd[$remapCount] = "toggleMacroWindow";
	$remapCount++;
	$addedMacroMaps = true;
}

function toggleMacroWindow(%val)
{
	if(%val)
		if(macroGui.isAwake())
			canvas.popdialog(macrogui);
		else
			canvas.pushdialog(macrogui);
}

function lstMacroList::onWake(%this)
{
   txtMacroSaveName.setValue("");
   txtMacroDesc.setValue("");
	%this.clear();
	%MacroFile = FindFirstFile("base/config/client/SavedMacros/*.txt");
	while (strlen(%MacroFile) > 0)
	{
		%name = getSubStr(%MacroFile,31,strPos(getSubStr(%MacroFile,31,strLen(%MacroFile)),".txt"));
		if(isvalidmacrosave(%MacroFile))
		   %this.addRow(%i++,%name,%i);
		%MacroFile = FindNextFile("base/config/client/SavedMacros/*.txt");
	}
	%this.sort(0,1);
	$LastMacroSelectType = "Auto";
	%this.setSelectedRow(0);
	checkConvertButtonNeeded();
}

package MacroSaver
{
   function ToggleBuildMacroRecording(%val)
   {
      if(%val)
      {
         if($RecordingBuildMacro)
         {
            for(%i=0;%i<10;%i++)
            {
               %contents = $BSD_InvData[%i].uiName;
               if(%contentsArray $= "")
                  %contentsArray = %contents;
               else
                  %contentsArray = %contentsArray@"\t"@%contents;
            }
            $BuildMacroSO.brickArray = %contentsArray;
         }
      }
      Parent::ToggleBuildMacroRecording(%val);
   }
};
activatePackage(MacroSaver);

function resortListWithItems()
{
   %tname = txtMacroSaveName.getValue();
   %this = lstMacroList;
	%this.clear();
	%MacroFile = FindFirstFile("base/config/client/SavedMacros/*.txt");
	while (strlen(%MacroFile) > 0)
	{
		%name = getSubStr(%MacroFile,31,strPos(getSubStr(%MacroFile,31,strLen(%MacroFile)),".txt"));
		if(isvalidmacrosave(%MacroFile) && strPos(strLwr(%name),strLwr(%tname)) >= 0)
		   %this.addRow(%i++,%name,%i);
		%MacroFile = FindNextFile("base/config/client/SavedMacros/*.txt");
	}
	%this.sort(0,1);
}

function isValidMacroSave(%fileName)
{
   if(isFile(%fileName))
   {
      %file = new FileObject();
      if(%file.openForRead(%fileName))
      {
         if(%file.readLine() !$= "")
         {
            %file.delete();
            return 1;
         }
         else
         {
            %file.delete();
            return 0;
         }
      }
      else
      {
         %file.delete();
         return 0;
      }
   }
   else
      return 0;
}

function lstMacroList::onSelect(%this, %id, %text)
{
	txtMacroSaveName.setValue(getField(%text,0));
	txtMacroDesc.setValue(getMacroDesc(getField(%text,0)));
	
	if(%text $= $LastMacroSelect && vectorSub($Sim::Time,$LastMacroSelectT) <= 1 && $LastMacroSelectType !$= "Auto")
	{
	   LoadMacroFromFile();
	}
	$LastMacroSelectType = "";
	$LastMacroSelect = %text;
	$LastMacroSelectT = $Sim::Time;
}

function getMacroDesc(%filename)
{
   %file = new FileObject();
   if(%file.openForRead("base/config/client/SavedMacros/"@%filename@".txt"))
   {
      %file.readLine();
      %macroDesc = %file.readLine();
      %file.close();
      %file.delete();
      %macroDesc = strReplace(%macroDesc,"\\n","\n");
      return %macroDesc;
   }
   else
      return "<<Unable to read Description>>";
}

function SaveMacroToFile(%conf)
{
	%macroName = txtMacroSaveName.getValue();
	if(!isObject($BuildMacroSO) || $BuildMacroSO.numEvents <= 0)
	{
		MessageBoxOK("Ooopsie!","It appears you don't have a macro in use! Please make one, then try again...");
		return;
	}
	if(%macroName $= "")
	{
		MessageBoxOK("Whoops!","Please fill in the Macro Name box, or select a Macro to Overwrite!");
		return;
	}
	if(!isValidFilename(%macroName))
	{
	   MessageBoxOK("Uh Oh!","The Name you entered contains characters that cannot be used in a Filename.");
	   return;
	}
   if(!%conf && isvalidmacrosave("base/config/client/SavedMacros/"@%macroName@".txt"))
   {
      MessageBoxYesNo("Oh Dear!","Are you sure you wish to overwrite the Macro \""@%macroName@"\"?","saveMacroToFile(1);","");
      return;
   }
   
	%file = new FileObject();
	%file.openForWrite("base/config/client/SavedMacros/"@%macroName@".txt");
	%file.writeLine(%macroName);
	//Dumb haxx because I dont remember how to do it
	%description = txtMacroDesc.getValue();
   for(%i=0;%i<getLineCount(%description);%i++)
	{
	   if(%macroDescription $= "")
	      %macroDescription = getLine(%description,%i);
	   else
	      %macroDescription = %macroDescription@"\\n"@getLine(%description,%i);
	}
   %macroDescription = getSubStr(%macroDescription,0,strLen(%macroDescription));
   %file.writeLine(%macroDescription);
   %file.writeLine($BuildMacroSO.brickArray);
   for(%i=0;%i<$BuildMacroSO.numEvents;%i++)
   {
      %event = $BuildMacroSO.event[%i];
      %file.writeLine(%event);
   }
   %file.close();
   %file.delete();
	canvas.popDialog(macroGui);
	clientCmdCenterPrint("<color:00FF00>Your Macro has been saved as <color:FFFFFF>"@%macroName@"<color:00FF00> successfully.",4);
}

function deleteMacro(%conf)
{
   %macroName = txtMacroSaveName.getValue();
	if(%macroName $= "")
	{
		MessageBoxOK("Oh No!","Please select a Macro to Delete!");
		return;
	}

	if(!isvalidmacrosave("base/config/client/SavedMacros/"@%macroName@".txt"))
	{
		MessageBoxOK("Oh Darn...","Sorry, The saved macro with this name could not be found!");
		return;
	}
	
	if(%conf !$= 1)
	{
	   MessageBoxYesNo("Oh Dear!","Are you sure you wish to delete the Macro \""@%macroName@"\"?","deleteMacro(1);","");
	   return;
	}

   %file = new FileObject();
   %file.openForWrite("base/config/client/SavedMacros/"@%macroName@".txt");
   %file.writeLine("");
   %file.close();
   %file.delete();

	lstMacroList::onWake(lstMacroList);
}

function convertOldMacros()
{
   MessageBoxOK("This may take a While...","Please Wait.");
   schedule(100,0,"beginConvertOldMacros");
}

function beginconvertOldMacros()
{
   
   %MacroFile = FindFirstFile("base/config/client/SavedMacros/*.cs");
	while (strlen(%MacroFile) > 0)
	{
      if(!isValidMacroSave(%MacroFile))
      {
         %MacroFile = FindNextFile("base/config/client/SavedMacros/*.cs");
         continue;
      }
      
	   if(isObject(BuildMacroSOTemp))
	      BuildMacroSOTemp.delete();
      
		%name = getSubStr(%MacroFile,31,strPos(getSubStr(%MacroFile,31,strLen(%MacroFile)),".cs"));
		exec(%MacroFile);
		
		if(!isObject(BuildMacroSOTemp))
		{
		   %couldntLoad++;
         %MacroFile = FindNextFile("base/config/client/SavedMacros/*.cs");
         continue;
		}
		$OldBuildMacro = $BuildMacroSO;
		$BuildMacroSO = BuildMacroSOTemp;
	   %file = new FileObject();
	   %file.openForWrite("base/config/client/SavedMacros/"@%name@".txt");
	   %file.writeLine(%name);
      %file.writeLine("");
      %file.writeLine($BuildMacroSO.bricksArray);
      for(%i=0;%i<$BuildMacroSO.numEvents;%i++)
      {
         %event = $BuildMacroSO.event[%i];
         %file.writeLine(%event);
      }
      %file.close();
      %file.openForWrite("base/config/client/SavedMacros/"@%name@".cs");
      %file.writeLine("");
      %file.close();
      %file.delete();
      BuildMacroSOTemp.delete();
		%MacroFile = FindNextFile("base/config/client/SavedMacros/*.cs");
	}
	$BuildMacroSO = $OldBuildMacro;
	lstMacroList::onWake(lstMacroList);
	if(%couldntLoad $= 1)
      MessageBoxOK("Huzzah!","One of your Old Macros was not Converted, but the Rest have been.");
   else if(%couldntLoad >= 2)
      MessageBoxOK("Huzzah!",%couldntLoad@" of your Old Macros were not Converted, but the Rest have been.");
   else
      MessageBoxOK("Huzzah!","Your Old Macros were Converted Successfully.");
}

function checkConvertButtonNeeded()
{
	%MacroFile = FindFirstFile("base/config/client/SavedMacros/*.cs");
	while (strlen(%MacroFile) > 0)
	{
		if(isvalidmacrosave(%MacroFile))
		{
		   btnConvertMacros.setVisible(1);
		   return;
		}
		%MacroFile = FindNextFile("base/config/client/SavedMacros/*.cs");
	}
	btnConvertMacros.setVisible(0);
}

function LoadMacroFromFile()
{
	%macroName = txtMacroSaveName.getValue();
	if(%macroName $= "")
	{
		MessageBoxOK("Oh No!","Please select a Macro to Load!");
		return;
	}

	if(!isvalidmacrosave("base/config/client/SavedMacros/"@%macroName@".txt"))
	{
		MessageBoxOK("Oh Darn...","Sorry, The saved macro with this name could not be found!");
		return;
	}

	if(DataBlockGroup.getCount() >= 2)
	{
		%searchGroup = DataBlockGroup.getID();
	}
	else
	{
		%searchGroup = ServerConnection.getID();
	}
	
	$BuildMacroSO.delete();
   %file = new FileObject();
   if(%file.openForRead("base/config/client/SavedMacros/"@%macroName@".txt"))
   {
      %macroName = %file.readLine();
      %macroDesc = %file.readLine();
      %bricksArray = %file.readLine();
      $BuildMacroSO = new ScriptObject()
      {
         class = BuildMacroSO;
      };
      %count = 0;
      while(!%file.isEOF())
      {
         $BuildMacroSO.event[%count] = %file.readLine();
         %count++;
      }
      $BuildMacroSO.numEvents = %count;
      $BuildMacroSO.brickArray = %bricksArray;
   }
   else
   {
      %file.delete();
      canvas.popDialog(macroGui);
      clientCmdCenterPrint("<color:FF0000>Your Macro \"<color:FFFFFF>"@%macroName@"<color:FF0000>\" failed to load\nFile unable to be Opened",4);
      return;
   }

	BSD_ClickClear();
	for(%i=0;%i<10;%i++)
	{
		%foundBrickDB = 0;
		if(getField(%bricksArray,%i) !$= "" && getField(%bricksArray,%i) !$= "-1")
		{
			%bitmapLoc = $BSD_InvIcon[%i];
			for(%j=0;%j<%searchGroup.getCount();%j++)
			{
				%dB = %searchGroup.getObject(%j);
				if(%dB.getClassName() !$= "fxDTSBrickData")
					continue;

				if(%dB.uiName $= getField(%bricksArray,%i) && %dB.uiName !$= "")
				{
					%foundBrickDB = 1;
					%bitmap = %dB.iconName;
					break;
				}
				else if(%db $= getField(%bricksArray,%i))
				{
				   %foundBrickDB = 1;
				   %bitmap = %db.iconName;
				   break;
				}
			}
			if(%foundBrickDB $= 1)
			{
				%bitmapLoc.setBitmap(%bitmap);
				$BSD_InvData[%i] = %dB;
			}
			else
			{
			   %file.delete();
				canvas.popDialog(macroGui);
				clientCmdCenterPrint("<color:FF0000>Your Macro \"<color:FFFFFF>"@%macroName@"<color:FF0000>\" failed to load.\nBricks Invalid",4);
				return;
			}
		}
	}
	%file.delete();
	BSD_BuyBricks();
	UseFirstSlot(1);
	canvas.popDialog(macroGui);
	clientCmdCenterPrint("<color:00FF00>Your Macro \"<color:FFFFFF>"@%macroName@"<color:00FF00>\" has been loaded successfully.",4);
}