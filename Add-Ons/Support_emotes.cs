function serverCmdMessageSent(%client, %text){
%newcurselist = $pref::Server::CurseList;
%newcurselist = strreplace(%newcurselist,","," ");
if($Pref::Server::Cursefilter){
for (%i = 0; %i < getWordCount(%newcurselist) + 1; %i++){
%word = getword(%newcurselist, %i);
if(%word $= "" || %word $= " "){
%word = "uadsnfhsdag bhfsgsbgbns bgk sfgksjdfksb sjkb fgbjfg jf j ";
}for(%i2 = 0; %i2 < getWordCount(%text); %i2++)
{
%word2 = getword(%text,%i2);
if(%word $= %word2){
%foundbad = 1;
}
}
}
if(%foundbad == 1){
messageclient(%client,"","Please do not curse.");
return;
}
}
if($Pref::Server::ETardfilter){
%newetardlist = $pref::Server::etardList;
%newetardlist = strreplace(%newetardlist,","," ");
for (%i = 0; %i < getWordCount(%newetardlist) + 1; %i++){
%word = getword(%newetardlist, %i);for(%i2 = 0; %i2 < getWordCount(%text); %i2++)
{
%word2 = getword(%text,%i2);
if(%word $= %word2){
%foundbad = 1;
}
}
}
if(%foundbad == 1){
messageclient(%client,"","This is a civilized game.");
return;
}
}
%spam = SpamAlert2(%client);
if(%spam)
{
return;
}
%text = strreplace(%text,"bomb","<Bitmap:Add-Ons/CI/bomb.png>\c6");
%text = strreplace(%text,">:>","<Bitmap:Add-Ons/Emotes/evil.png>\c6");
%text = strreplace(%text,">:<","<Bitmap:Add-Ons/Emotes/mad.png>\c6");
%text = strreplace(%text,":D","<Bitmap:Add-Ons/Emotes/happy.png>\c6");
%text = strreplace(%text,":O","<Bitmap:Add-Ons/Emotes/gasp.png>\c6");
%text = strreplace(%text,":o","<Bitmap:Add-Ons/Emotes/gasp.png>\c6");
%text = strreplace(%text,":P","<Bitmap:Add-Ons/Emotes/tongue.png>\c6");
%text = strreplace(%text,":)","<Bitmap:Add-Ons/Emotes/smile.png>\c6");
%text = strreplace(%text,":(","<Bitmap:Add-Ons/Emotes/sad.png>\c6");
%text = strreplace(%text,"xD","<Bitmap:Add-Ons/Emotes/lolhappy.png>\c6");
%text = strreplace(%text,"XD","<Bitmap:Add-Ons/Emotes/lolhappy.png>\c6");
%text = strreplace(%text,":|","<Bitmap:Add-Ons/Emotes/neutral.png>\c6");
%text = strreplace(%text,":cookie:","<Bitmap:Add-Ons/Emotes/cookie.png>\c6");
%text = strreplace(%text,":cookiemonster:","<Bitmap:Add-Ons/Emotes/cookiemonster.png>\c6");
chatmessageall(%client,'\c7%2\c3%3\c7%4\c6: %1',%text,%client.clanprefix,%client.name,%client.clansuffix);
}

//datablock StaticShapeData(EmoteHax)
//{
	//category = "Static Shapes";   //Mission editor category
	//item = EmoteHax;
	//shapeFile = "add-ons/emotes/emotehax.dts";
//};


datablock DecalData(DecalParticle1)
{
   textureName = "add-ons/Emotes/cookiemonster.png";
};



datablock DecalData(DecalParticle2)
{
   textureName = "add-ons/Emotes/lolhappy.png";
};

datablock DecalData(DecalParticle3)
{
   textureName = "add-ons/Emotes/gasp.png";
};


datablock DecalData(DecalParticle4)
{
   textureName = "add-ons/Emotes/cookie.png";
};

datablock DecalData(DecalParticle5)
{
   textureName = "add-ons/Emotes/evil.png";
};

datablock DecalData(DecalParticle6)
{
   textureName = "add-ons/Emotes/mad.png";
};

datablock DecalData(DecalParticle7)
{
   textureName = "add-ons/Emotes/sad.png";
};

datablock DecalData(DecalParticle8)
{
   textureName = "add-ons/Emotes/tongue.png";
};

datablock DecalData(DecalParticle9)
{
   textureName = "add-ons/Emotes/smile.png";
};

datablock DecalData(DecalParticle10)
{
   textureName = "add-ons/Emotes/neutral.png";
};

datablock DecalData(DecalParticle11)
{
   textureName = "add-ons/Emotes/happy.png";
};


function GameConnection::spamMessageTimeout(%this)
{
   if(%this.spamMessageCount > 0)
      %this.spamMessageCount--;
}

$SPAM_PROTECTION_PERIOD     = 10000;
$SPAM_MESSAGE_THRESHOLD     = 3;
$SPAM_PENALTY_PERIOD        = 10000;
$SPAM_MESSAGE               = '\c3FLOOD PROTECTION:\c0 You must wait another %1 seconds.';

function GameConnection::spamReset(%this)
{
   %this.isSpamming = false;
}

function spamAlert2(%client)
{
   if($Pref::Server::FloodProtectionEnabled != true)
      return(false);

   if(!%client.isSpamming && %client.spamMessageCount > $SPAM_MESSAGE_THRESHOLD)
   {
      %client.spamProtectStart = getSimTime();
      %client.isSpamming = true;
      %client.schedule($SPAM_PENALTY_PERIOD, spamReset);
   }

   if(%client.isSpamming)
   {
      %wait = mFloor(($SPAM_PENALTY_PERIOD - (getSimTime() - %client.spamProtectStart)) / 1000);
      messageClient(%client, "", $SPAM_MESSAGE, %wait);
      return(true);
   }

   %client.spamMessageCount++;
   %client.schedule($SPAM_PROTECTION_PERIOD, spamMessageTimeout);
   return(false);
}
