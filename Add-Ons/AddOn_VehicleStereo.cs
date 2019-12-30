function getMusicList()
{
 for(%i=0;%i<DataBlockGroup.getCount();%i++)
 {
  if(strstr(DataBlockGroup.getObject(%i).getName(),"musicData_") > -1)
  {
   %name = strReplace(DataBlockGroup.getObject(%i).getName(),"_"," ");
   %name = strReplace(%name,"musicData","");
   %name = stripTrailingSpaces(%name);
   %name = strReplace(%name,"","_");
   $StereoMusic_[%name] = DataBlockGroup.getObject(%i).getName();
  }
 }
}

function servercmdStereo(%client,%name1,%name2,%name3,%name4)
{
 %name = "";
 %name = %name @ (%name1 !$= "" ? (" " @ %name1) : "");
 %name = %name @ (%name2 !$= ""  ? (" " @ %name2) : "");
 %name = %name @ (%name3 !$= ""  ? (" " @ %name3) : "");
 %name = %name @ (%name4 !$= ""  ? (" " @ %name4) : "");
 if(isObject(%client.player) && isObject(%client.player.getControlObject()) && %client.player.getControlObject().getClassName() !$= "AIPlayer")
 {
  %client.player.getControlObject().stopAudio(1);
  %client.player.getControlObject().playAudio(1,$StereoMusic_[%name]);
  if(isObject($StereoMusic_[%name]))
  messageclient(%client,'',"\c1Stereo music set to \c4\"" @ %name @ "\"\c1.");
  else
  messageclient(%client,'',"\c1Turned the stereo off.");
 }
}

package StereoList
{
 function GameConnection::onConnect(%client,%name,%a,%b,%c,%d,%e)
 {
  Parent::onConnect(%client,%name,%a,%b,%c,%d,%e);
  getMusicList();
 }
};activatepackage(StereoList);