datablock shapeBaseImageData(JPodImage){shapeFile="Add-Ons/Jookia/JPod/JPod.dts";emap=false;mountPoint=1;offset="0 0 0";eyeOffset=0;rotation=eulerToMatrix("-90 90 0");correctMuzzleVector=false;className="weaponImage";melee=true;armReady=false;doColorShift=false;};
function cacheMusicList(){new scriptObject(JPod);
for(%a=0;%a<dataBlockGroup.getCount();%a++){
if(strStr(dataBlockGroup.getObject(%a).getName(),"musicData_")>-1){
if(!%b){%b=0;}JPod.songName[%b]=strReplace(strReplace(dataBlockGroup.getObject(%a).getName(),"musicData_",""),"_","");JPod.songDatablock[%b]=dataBlockGroup.getObject(%a).getName();%b++;}}}
function serverCmdJPod(%client,%word){
if(%word!$=""){
for(%a=0;JPod.songName[%a]!$="";%a++){
if(strReplace(strLwr(JPod.songName[%a]),strLwr(%word),"")!$=strLwr(JPod.songName[%a])){%client.JPodSong=JPod.songDatablock[%a];}}
if(%client.JPodSong!$=""){%client.player.playAudio(1,nameToID(%client.JPodSong));%client.player.mountImage(JPodImage,1);return;}}%client.player.stopAudio(1);%client.player.unmountImage(1);}schedule(1000,0,"cacheMusicList");