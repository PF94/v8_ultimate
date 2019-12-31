//by rkynick v2
function servercmdDungeon(%client,%name){
%file = new FileObject();
%file.openForRead("Add-Ons/Client/Dungeons/"@ %client.dung @"/clearinv.txt");
while(!%file.isEOF())
{
   %line = %file.readLine();
   %client.dunginv[%line]=0;
}
%file.close();
%file.delete();
//end of clear inv
%file = new FileObject();
%file.openForRead("Add-Ons/Client/Dungeons/"@ %name @"/start.txt");
%i = 0;
%client.dung = %name;
%textsoon=0;
%g=0;
while(!%file.isEOF())
{
   %line = %file.readLine();
   if(!%i){messageclient(%client,'',%line);}
   if(%i==1){
	
	messageclient(%client,'',"\c2You can" SPC %line);
	%client.commands=getwordcount(%line);
	
	while(%b<getwordcount(%line)){
		%client.choice[%b]=getword(%line,%b);
		%b++;
	}

   }
   if(%i>1&&%g<%client.commands){%client.dungm[%g]=%line;%g++;}
   %i++;
}
%file.close();
%file.delete();
}

function contdung(%client,%name,%name2){
%file = new FileObject();
%file.openForRead("Add-Ons/Client/Dungeons/"@ %name @"/"@ %name2 @".txt");
%i = 0;
%g = 0;
%textsoon=0;
while(!%file.isEOF())
{
   %line = %file.readLine();
   if(!%i){messageclient(%client,'',%line);}
   if(%i==1){
	
	messageclient(%client,'',"\c2You can" SPC %line);
	%client.commands=getwordcount(%line);
	while(%b<getwordcount(%line)){
		%client.choice[%b]=getword(%line,%b);
		%b++;
	}

   }
   if(%i>1&&%g<%client.commands){%client.dungm[%g]=%line;%g++;}
   %i++;
}
%file.close();
%file.delete();
}

function servercmdDdo(%client,%option){
%option = %option--;
//announce(%option);
if(%client.choice[%option]||%option<%client.commands){
//announce(%option);
%effect=getword(%client.dungm[%option],0);
if(getsubstr(%effect,0,2)!$="I?"){
messageclient(%client,'',restwords(%client.dungm[%option]));}
if(getsubstr(%effect,0,2)$="go"){
contdung(%client,%client.dung,getsubstr(%effect,2,999));}
if(getsubstr(%effect,0,2)$="X("){
messageclient(%client,'',"GameOver! score:"SPC getsubstr(%effect,2,999));%client.commands=0;}
if(getsubstr(%effect,0,2)$=":D"){
messageclient(%client,'',"You Win! score:"SPC getsubstr(%effect,2,999));%client.commands=0;}
if(getsubstr(%effect,0,2)$="I+"){
%client.dunginv[getsubstr(%effect,2,6)]++;}
if(getsubstr(%effect,0,2)$="I-"){
%client.dunginv[getsubstr(%effect,2,6)]--;}
//begin if inventory


if(getsubstr(%effect,0,2)$="I?"){
//announce("pp");
if(%client.dunginv[getsubstr(%effect,2,6)]>1){
messageclient(%client,'',restwords(%client.dungm[%option]));
//announce(getsubstr(%effect,8,2));
if(getsubstr(%effect,8,2)$="go"){
contdung(%client,%client.dung,getsubstr(%effect,10,999));}
if(getsubstr(%effect,8,2)$="X("){
messageclient(%client,'',"GameOver! score:"SPC getsubstr(%effect,10,999));%client.commands=0;}
if(getsubstr(%effect,8,2)$=":D"){
messageclient(%client,'',"You Win! score:"SPC getsubstr(%effect,10,999));%client.commands=0;}
if(getsubstr(%effect,8,2)$="I+"){
%client.dunginv[getsubstr(%effect,10,6)]++;}
if(getsubstr(%effect,8,2)$="I-"){
%client.dunginv[getsubstr(%effect,10,6)]--;}
if(getsubstr(%effect,8,2)$="ms"){
//announce("pp");
messageclient(%client,'',getsubstr(%effect,10,999));}
}
}
}
}


