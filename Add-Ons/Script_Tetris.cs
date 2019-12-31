// Zor's Tetris Mod

if(!$tetriscount)
 $tetriscount=1;

$brickcolors[0]="1 0 0 1";
$brickcolors[1]="1 0.5 0 1";
$brickcolors[2]="1 1 0 1";
$brickcolors[3]="0 1 0 1";
$brickcolors[4]="0 0 1 1";
$brickcolors[5]="0.55 0 1 1";
$brickcolors[6]="1 0.4 1 1";

$tetrisBubble=24;

datablock StaticShapeData(staticbrick1x1){
 shapeFile = "add-ons/shapes/brick1x1.dts";
};

function servercmdtoggletetris(%client){
 if(!%client.isSuperAdmin)
  return;
 if($pref::server::tetris){
  $pref::server::tetris=0;
  messageClient(%client,'','\c6Tetris is now \c0OFF');
 }else{
  $pref::server::tetris=1;
  messageClient(%client,'','\c6Tetris is now \c0ON');
 }
}

function servercmdtetris(%client){
 if(%client.tetris){
  messageClient(%client,'','\c5You are already playing a game of Tetris!');
  return;
 }
 if(!$pref::server::tetris){
  messageClient(%client,'','\c5Tetris is currently disabled!');
  return;
 }
 if(%client.NoTetris)
  return;
 %total=clientgroup.getcount();
 %test=0;
 for(%i=0;%i<%total;%i++){
  %cl=clientgroup.getobject(%i);
  if(%cl.tetris){
   if(vectorlen(vectorsub(%client.player.gettransform(),%cl.player.gettransform()))<$tetrisBubble){
    %test=%cl;
    break;
   }
  }
 }
 if(%test){
  bottomprint(%client,"\c6You are \c0TOO CLOSE\c6 to someone who is playing Tetris.",4,4);
  return;
 }
 %gameid=$tetriscount;
 $tetriscount++;

 %pos=%client.player.getposition();
 %x=getword(%pos,0);
 %y=getword(%pos,1);
 %z=getword(%pos,2);

 %y+=3;

 %back = new staticshape(){
  datablock="staticbrick1x1";
 };
 missioncleanup.add(%back);
 %back.settransform((%x-0.5) SPC %y SPC %z);
 %back.setscale("1 10 18");
 %back.owner="Tetris";
 $tetrisbackbrick[%gameid]=%back;
 %back.setnodecolor("Cube","0 0 0 1");

 %score=new staticshape(){
  datablock="staticbrick1x1";
 };
 missioncleanup.add(%score);
 %score.settransform((%x-0.5) SPC (%y-1) SPC (%z+4));
 %score.owner="Tetris";
 $tetrisscorebrick[%gameid]=%score;
 %score.setnodecolor("Cube","0 0 0 1");

 $tetrisclient[%gameid]=%client;
 $tetrisscore[%gameid]=0;
 $tetrisOrigin[%gameid]=%x SPC %y SPC %z;
 $tetrisx[%gameid]=%x;
 $tetrisspawn[%gameid]=(%y+2) SPC (%z+10.2);
 $tetriscoordspawn[%gameid]="4 18";
 $tetrisfalltime[%gameid]=1000;
 $tetrispaused[%gameid]=0;
 $tetrisnextbrick[%gameid]=getRandom(0,6);
 for(%x=0;%x<10;%x++){
  for(%y=0;%y<18;%y++){
   $tetriscoord[%gameid,%x SPC %y]=0;
  }
 }
 tetrisupdatescore(%gameid);
 starttetris(%client,%gameid);

 bottomprint(%client,"\c6You have started playing Tetris!",4,4);
}

function starttetris(%client,%gameID){
 %client.tetris=%gameID;
 tetrisnewbrick(%gameID);
}

function tetrisnewbrick(%gameID){
 %new=$tetrisnextbrick[%gameid];
 if(%new==0){
  $tetriscurroffset[%gameid,0]="-1 0";
  $tetriscurroffset[%gameid,1]="-.5 0";
  $tetriscurroffset[%gameid,2]="0 0";
  $tetriscurroffset[%gameid,3]=".5 0";
 }else if(%new==1){
  $tetriscurroffset[%gameid,0]="0 0";
  $tetriscurroffset[%gameid,1]=".5 0";
  $tetriscurroffset[%gameid,2]="0 -.5";
  $tetriscurroffset[%gameid,3]=".5 -.5";
 }else if(%new==2){
  $tetriscurroffset[%gameid,0]="-.5 0";
  $tetriscurroffset[%gameid,1]="0 0";
  $tetriscurroffset[%gameid,2]=".5 0";
  $tetriscurroffset[%gameid,3]="0 -.5";
 }else if(%new==3){
  $tetriscurroffset[%gameid,0]="-.5 0";
  $tetriscurroffset[%gameid,1]="0 0";
  $tetriscurroffset[%gameid,2]=".5 0";
  $tetriscurroffset[%gameid,3]="-.5 -.5";
 }else if(%new==4){
  $tetriscurroffset[%gameid,0]="-.5 0";
  $tetriscurroffset[%gameid,1]="0 0";
  $tetriscurroffset[%gameid,2]=".5 0";
  $tetriscurroffset[%gameid,3]=".5 -.5";
 }else if(%new==5){
  $tetriscurroffset[%gameid,0]="-.5 0";
  $tetriscurroffset[%gameid,1]="0 0";
  $tetriscurroffset[%gameid,2]="0 -.5";
  $tetriscurroffset[%gameid,3]=".5 -.5";
 }else if(%new==6){
  $tetriscurroffset[%gameid,0]="0 0";
  $tetriscurroffset[%gameid,1]=".5 0";
  $tetriscurroffset[%gameid,2]="-.5 -.5";
  $tetriscurroffset[%gameid,3]="0 -.5";
 }
 for(%i=0;%i<4;%i++){
  $tetriscurr[%gameid,%i]=new staticshape(){
   datablock=staticbrick1x1;
  };
  missioncleanup.add($tetriscurr[%gameid,%i]);
  $tetriscurr[%gameid,%i].settransform($tetrisx[%gameid] SPC vectoradd($tetrisspawn[%gameid],$tetriscurroffset[%gameid,%i]));
  $tetriscurr[%gameid,%i].owner="Tetris";
  $tetriscurr[%gameid,%i].setnodecolor("Cube",$brickcolors[%new]);
  
  $tetriscurrcoord[%gameid,%i]=vectoradd($tetriscoordspawn[%gameid],vectorscale($tetriscurroffset[%gameid,%i],"2 2 0"));
 }

 $tetrisfallsched[%gameid]=schedule($tetrisfalltime[%gameid],0,tetrisshiftdown,%gameid,1);

 $tetrisnextbrick[%gameid]=getRandom(0,6);
 $tetrisscorebrick[%gameid].setnodecolor("Cube",$brickcolors[$tetrisnextbrick[%gameid]]);
}

function tetrisshiftleft(%gameid){
 if($tetrispaused[%gameid])
  return;
 for(%i=0;%i<4;%i++){
  %coord=$tetriscurrcoord[%gameid,%i];
  %x=getword(%coord,0);
  %y=getword(%coord,1);
  %x--;
  if($tetriscoord[%gameid,%x SPC %y] || %x<0){
   return;
  }
 }
 for(%i=0;%i<4;%i++){
  %coord=$tetriscurrcoord[%gameid,%i];
  %x=getword(%coord,0);
  %y=getword(%coord,1);
  %x--;
  $tetriscurrcoord[%gameid,%i]=%x SPC %y;

  %pos=$tetriscurr[%gameid,%i].getposition();
  %x=getword(%pos,0);
  %y=getword(%pos,1);
  %z=getword(%pos,2);
  %y-=0.5;
  $tetriscurr[%gameid,%i].settransform(%x SPC %y SPC %z);
 }
}

function tetrisshiftright(%gameid){
 if($tetrispaused[%gameid])
  return;
 for(%i=0;%i<4;%i++){
  %coord=$tetriscurrcoord[%gameid,%i];
  %x=getword(%coord,0);
  %y=getword(%coord,1);
  %x++;
  if($tetriscoord[%gameid,%x SPC %y] || %x>9){
   return;
  }
 }
 for(%i=0;%i<4;%i++){
  %coord=$tetriscurrcoord[%gameid,%i];
  %x=getword(%coord,0);
  %y=getword(%coord,1);
  %x++;
  $tetriscurrcoord[%gameid,%i]=%x SPC %y;

  %pos=$tetriscurr[%gameid,%i].getposition();
  %x=getword(%pos,0);
  %y=getword(%pos,1);
  %z=getword(%pos,2);
  %y+=0.5;
  $tetriscurr[%gameid,%i].settransform(%x SPC %y SPC %z);
 }
}

function tetrisrotate(%gameid){
 if($tetrispaused[%gameid])
  return;
 for(%i=0;%i<4;%i++){
  %coord=$tetriscurrcoord[%gameid,%i];
  %offset=$tetriscurroffset[%gameid,%i];
  %cx=getword(%coord,0);
  %cy=getword(%coord,1);
  %x=getword(%offset,0)*2;
  %y=getword(%offset,1)*2;
  %ox=%cx-%x;
  %oy=%cy-%y;
  %temp=%x;
  %x=%y*-1;
  %y=%temp;
  %cx=%ox+%x;
  %cy=%oy+%y;
  
  if($tetriscoord[%gameid,%cx SPC %cy] || %cx>9 || %cx<0 || %cy>18 || %cy<0){
   return;
  }
 }
 for(%i=0;%i<4;%i++){
  %coord=$tetriscurrcoord[%gameid,%i];
  %offset=$tetriscurroffset[%gameid,%i];
  %cx=getword(%coord,0);
  %cy=getword(%coord,1);
  %x=getword(%offset,0)*2;
  %y=getword(%offset,1)*2;
  %ox=%cx-%x;
  %oy=%cy-%y;
  %temp=%x;
  %tempx=%cx;
  %tempy=%cy;
  %x=%y*-1;
  %y=%temp;
  %cx=%ox+%x;
  %cy=%oy+%y;
  $tetriscurroffset[%gameid,%i]=(%x/2) SPC (%y/2);
  $tetriscurrcoord[%gameid,%i]=%cx SPC %cy;

  %pos=$tetriscurr[%gameid,%i].getposition();
  %x=getword(%pos,0);
  %y=getword(%pos,1);
  %z=getword(%pos,2);
  %y+=(%cx-%tempx)*0.5;
  %z+=(%cy-%tempy)*0.5666666666667;
  $tetriscurr[%gameid,%i].settransform(%x SPC %y SPC %z);
 }
}

function tetrisshiftdown(%gameid,%sched){
 if($tetrispaused[%gameid])
  return;
 for(%i=0;%i<4;%i++){
  %coord=$tetriscurrcoord[%gameid,%i];
  %x=getword(%coord,0);
  %y=getword(%coord,1);
  %y--;
  if($tetriscoord[%gameid,%x SPC %y] || %y<0){
   if(%sched)
    tetrisstopbrick(%gameid);
   $stop=1;
   return;
  }
 }
 for(%i=0;%i<4;%i++){
  %coord=$tetriscurrcoord[%gameid,%i];
  %x=getword(%coord,0);
  %y=getword(%coord,1);
  %y--;
  $tetriscurrcoord[%gameid,%i]=%x SPC %y;

  %pos=$tetriscurr[%gameid,%i].getposition();
  %x=getword(%pos,0);
  %y=getword(%pos,1);
  %z=getword(%pos,2);
  %z-=0.566666666666667;
  $tetriscurr[%gameid,%i].settransform(%x SPC %y SPC %z);
 }
 if(%sched==1)
  $tetrisfallsched[%gameid]=schedule($tetrisfalltime[%gameid],0,tetrisshiftdown,%gameid,1);
}

function tetrisstopbrick(%gameid){
 //echo("BRICK STOPPED: "@%gameid);
 $tetrisscore[%gameid]++;
 %gameover=0;
 for(%i=0;%i<4;%i++){
  $tetriscoord[%gameid,$tetriscurrcoord[%gameid,%i]]=$tetriscurr[%gameid,%i];
  if(getword($tetriscurrcoord[%gameid,%i],1)$=18)
   %gameover=1;
 }
 if(%gameover){
  tetrisendgame(%gameid);
 }else{
  testlines(%gameid);
  tetrisnewbrick(%gameid);
//  schedule(1000,0,tetrisnewbrick,%gameid);
  if($tetrisfalltime[%gameid]>250)
   $tetrisfalltime[%gameid]-=10;
  tetrisupdatescore(%gameid);
 }
}

function testlines(%gameid){
 %mod=10;
 %brickcounter=0;
 %linecounter=0;
 for(%y=0;%y<18;%y++){
  %counter=0;
  for(%x=0;%x<10;%x++){
   if($tetriscoord[%gameid,%x SPC %y])
    %counter++;
  }
  if(%counter==10){
   %linecounter++;
   $tetrisscore[%gameid]+=%mod;
   %mod=%mod*2;
   for(%x=0;%x<10;%x++){
    if($tetriscoord[%gameid,%x SPC %y])
     $tetriscoord[%gameid,%x SPC %y].delete();
    $tetriscoord[%gameid,%x SPC %y]=0;
    for(%i=%y;%i<18;%i++){
     $tetriscoord[%gameid,%x SPC %i]=$tetriscoord[%gameid,%x SPC (%i+1)];
     if($tetriscoord[%gameid,%x SPC %i]){
      %pos=$tetriscoord[%gameid,%x SPC %i].gettransform();
      %posx=getword(%pos,0);
      %posy=getword(%pos,1);
      %posz=getword(%pos,2);
      $tetriscoord[%gameid,%x SPC %i].settransform(%posx SPC %posy SPC (%posz-0.5666666666667));
     }
    }
   }
   %y--;
  }else
   %brickcounter+=%counter;
 }
 if(%brickcounter==0 && %linecounter==4){
  $tetrisscore[%gameid]+=132;
  messageAll('','\c2%1\c6 just got a \c24 Lines/Board Clear\c6 combo!!',$tetrisclient[%gameid].name);
  bottomprint($tetrisclient[%gameid],"\c24 Lines/ Board Clear Combo!! Awesome!!",4,4);
  return;
 }else if(%brickcounter==0){
  $tetrisscore[%gameid]+=66;
  bottomprint($tetrisclient[%gameid],"\c2Board Clear!",4,4);
 }else if(%linecounter==4)
  bottomprint($tetrisclient[%gameid],"\c24 Lines!",4,4);
}

function tetriscleanup(%gameid){
 for(%x=0;%x<10;%x++){
  for(%y=0;%y<18;%y++){
   if($tetriscoord[%gameid,%x SPC %y])
    $tetriscoord[%gameid,%x SPC %y].delete();
    $tetriscoord[%gameid,%x SPC %y]=0;
  }
 }
 for(%i=0;%i<4;%i++)
  $tetriscurr[%gameid,%i].delete();
 $tetrisscorebrick[%gameid].delete();
 $tetrisbackbrick[%gameid].delete();
}

function tetrisupdatescore(%gameid){
 %text=$tetrisclient[%gameid].player.getshapename() @ ": " @ $tetrisscore[%gameid];
 //echo(%text);
 $tetrisscorebrick[%gameid].setShapeName(%text);
}

function tetrishighscore(%name,%score){
 %placesuffix1="st";
 %placesuffix2="nd";
 %placesuffix3="rd";
 %placesuffix4="th";
 %placesuffix5="th";
 %placesuffix6="th";
 %score7=$pref::server::tetrishiscore[6];
 for(%x=1;%x<=6;%x++){
  if(strreplace(getword($pref::server::tetrishiscore[%x],0),"_"," ")$=%name){
   if(getword($pref::server::tetrishiscore[%x],1)>%score)
    return;
  }
  if(%score>=getword($pref::server::tetrishiscore[%x],1)){
   for(%i=6;%i>%x;%i--)
    $pref::server::tetrishiscore[%i]=$pref::server::tetrishiscore[%i-1];
   $pref::server::tetrishiscore[%x]=strreplace(%name," ","_") SPC %score;
   for(%i=%x+1;%i<=6;%i++){
    if(strreplace(getword($pref::server::tetrishiscore[%i],0),"_"," ")$=%name){
     for(%j=%i;%j<6;%j++)
      $pref::server::tetrishiscore[%j]=$pref::server::tetrishiscore[%j+1];
     $pref::server::tetrishiscore[6]=%score7;
     break;
    }
   }
   for(%i=1;%i<=6;%i++)
    $tetrisHSbricks[%i].setshapename(%i@". "@strreplace(getword($pref::server::tetrishiscore[%i],0),"_"," ")@": "@getword($pref::server::tetrishiscore[%i],1));
   messageAll('','\c2%1\c6 moves into \c2%2\c6 place with a score of \c2%3\c6!',%name,%x@%placesuffix[%x],%score);
   echo(%name@" moves into "@%x@%placesuffix[%x]@" place with a score of "@%score@"!");
   break;
  }
 }
}

function servercmdtetrisresetscores(%client){
 if(%client.isSuperAdmin){
  for(%i=1;%i<6;%i++){
   $pref::server::tetrishiscore[%i]="No_one 0";
   $tetrishsbricks[%i].setshapename(%i@". No one: 0");
  }
  messageAll('','\c6Tetris High Scores were reset by \c2%1\c6.',%client.name);
 }
}

function servercmdtetrishighscores(%client){
 messageclient(%client,'','\c3--\c5Tetris High Scores\c3--');
 for(%x=1;%x<=6;%x++)
  messageclient(%client,'','\c5%1. \c0%2\c5 - \c0%3',%x,strreplace(getword($pref::server::tetrishiscore[%x],0),"_"," "),getword($pref::server::tetrishiscore[%x],1));
}

function tetrispausegame(%gameid){
 if(!%gameid)
  return;
 if($tetrispaused[%gameid]){
  $tetrisbackbrick[%gameid].setscale("1 10 18");
  $tetrisfallsched[%gameid]=schedule($tetrisfalltime[%gameid],0,tetrisshiftdown,%gameid,1);
  bottomprint($tetrisclient[%gameid],"Game Resumed",1,1);
//  messageClient($tetrisclient[%gameid],'','\c5Game Resumed.');
  $tetrispaused[%gameid]=0;
 }else{
  cancel($tetrisfallsched[%gameid]);
  $tetrisbackbrick[%gameid].setscale("3 10 18");
  bottomprint($tetrisclient[%gameid],"Game Paused");
//  messageClient($tetrisclient[%gameid],'','\c5Game Paused.');
  $tetrispaused[%gameid]=1;
 }
}

function tetrisendgame(%gameid){
  if(!%gameid)
   return;
  //echo("GAME OVER: "@%gameid);
  cancel($tetrisfallsched[%gameid]);
  $tetrisclient[%gameid].tetris=0;
  tetrisupdatescore(%gameid);
  schedule(6000,0,tetriscleanup,%gameid);
  bottomprint($tetrisclient[%gameid],"\c6Game over! Score: \c2"@$tetrisscore[%gameid],6,6);
  tetrishighscore($tetrisclient[%gameid].player.getshapename(),$tetrisscore[%gameid]);
}

function servercmdend(%client){
 if(%client.tetris)
  tetrisendgame(%client.tetris);
}

datablock TriggerData(NoTetris){
   tickPeriodMS = 500;
};

function NoTetris::onLeaveTrigger(%this,%trigger,%obj){
%obj.client.NoTetris=0;
bottomprint(%obj.client,"",1,1);
}

function NoTetris::onEnterTrigger(%this,%trigger,%obj){
%obj.client.NoTetris=1;
bottomprint(%obj.client,"\c0This is a No Tetris Area.");
}

function NoTetris::onTickTrigger(%this){

}

function servercmdCreateTetrisHS(%client){
 if(!%client.isSuperAdmin)
  return;
 %back=new staticshape(){datablock=staticbrick1x1;};
 missioncleanup.add(%back);
 %pos=vectoradd(%client.player.gettransform(),"4 0 0");
 %back.settransform(%pos);
 %back.setscale("5 1 12");
 %back.setnodecolor("Cube","0 0 0 1");
 if(isObject($tetrisHSback))
  $tetrisHSback.delete();
 $tetrisHSback=%back;
 %top=new staticshape(){datablock=staticbrick1x1;};
 missioncleanup.add(%top);
 %top.settransform(vectoradd(%pos,"1 0 7.2"));
 %top.setscale("1 1 1");
 %top.setshapename("Tetris High Scores");
 %top.setnodecolor("Cube",$brickcolors[6]);
 if(isObject($tetrisHStop))
  $tetrisHStop.delete();
 $tetrisHStop=%top;
 for(%i=1;%i<=6;%i++){
  %new=new staticshape(){datablock=staticbrick1x1;};
  missioncleanup.add(%new);
  %new.setscale("1 1 1");
  %new.settransform(vectoradd(%pos,"1 -0.5 "@(1*(7-%i))));
  %new.setnodecolor("Cube",$brickcolors[%i-1]);
  if(isObject($tetrisHSbricks[%i]))
   $tetrisHSbricks[%i].delete();
  $tetrisHSbricks[%i]=%new;
  %new.setshapename(%i@". "@strreplace(getword($pref::server::tetrishiscore[%i],0),"_"," ")@": "@getword($pref::server::tetrishiscore[%i],1));
 }
}

package tetris{
 function servercmdshiftbrick(%client,%x,%y,%z){
  if(%client.tetris){
   if(%y==1)
    tetrisshiftleft(%client.tetris);
   else if(%y==-1)
    tetrisshiftright(%client.tetris);
   else if(%x==1)
    tetrisrotate(%client.tetris);
   else if(%x==-1)
    tetrisshiftdown(%client.tetris);
   else if(%z==-3)
    tetrispausegame(%client.tetris);
  }else
  Parent::servercmdshiftbrick(%client,%x,%y,%z);
 }
 function GameConnection::OnClientLeaveGame(%client,%a,%b,%c,%d,%f){
  if(%client.tetris)
   tetrisendgame(%client.tetris);
  Parent::OnClientLeaveGame(%client,%a,%b,%c,%d,%f);
 }
};
ActivatePackage(tetris);

function servercmdtetrishelp(%client){
 messageClient(%client,'','\c6Type \c2/tetris \c6to start a game');
 messageClient(%client,'','\c2Numpad 4 & 6: \c6Move block left and right');
 messageClient(%client,'','\c2Numpad 8: \c6Rotate block');
 messageClient(%client,'','\c2Numpad 2: \c6Advance block downward');
 messageClient(%client,'','\c2Numpad 5: \c6Pause/Unpause');
 messageClient(%client,'','\c6Type \c2/end \c6to end your current game');
}