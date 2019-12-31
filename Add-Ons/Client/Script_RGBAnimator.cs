// RGB Animator Tool created by Zor :D
$rgbani_maxxdim=24;
$rgbani_maxydim=16;

if(!$rgbani_xdim){
 $rgbani_xdim=$rgbani_maxxdim;
 $rgbani_ydim=$rgbani_maxydim;
 $rgbani_bitmap0="base/data/shapes/whitecheck";
 $rgbani_bitmap1="base/data/shapes/black";
 $rgbani_frame=1;
}

if (!$addedrgbanimaps){
 $remapDivision[$remapCount] = "RGB Animator";
 $remapName[$remapCount] = "Toggle RGB Animator Window";
 $remapCmd[$remapCount] = "toggleRGBAniWindow";
 $remapCount++;
 $addedrgbanimaps=1;
}

function toggleRGBAniWindow(%a){
 if(%a){
  if(rgbanimatorgui.isawake())
   canvas.popdialog(rgbanimatorgui);
  else
   canvas.pushdialog(rgbanimatorgui);
 }
}

if(!isobject(rgbanimatorgui)){
 exec("./rgbanimatorgui.gui");
 rgbani_drawframe(1);
 rgbanimatorgui_xdimtxt.setvalue($rgbani_ydim);
 rgbanimatorgui_ydimtxt.setvalue($rgbani_xdim);
}

function rgbani_togglebutton(%x,%y){
 %f=$rgbani_frame;
 if($rgbani_framegrid[%f,%x,%y])
  $rgbani_framegrid[%f,%x,%y]=0;
 else
  $rgbani_framegrid[%f,%x,%y]=1;
 eval("rgbanibutton"@%x@"_"@%y@".setbitmap(\""@$rgbani_bitmap[$rgbani_framegrid[%f,%x,%y]]@"\");");
 rgbanimatorgui_createmacrobutton.settext("Create Macro");
}

function rgbani_frame(%f){
 $rgbani_frame=%f;
 rgbani_drawframe(%f);
}

function rgbani_drawframe(%f){
 for(%y=0;%y<$rgbani_maxydim;%y++){
  for(%x=0;%x<$rgbani_maxxdim;%x++){
   eval("rgbanibutton"@%x@"_"@%y@".setbitmap(\""@$rgbani_bitmap[$rgbani_framegrid[%f,%x,%y]]@"\");");
  }
 }
 for(%x=1;%x<4;%x++)
  eval("rgbanimatorgui_framebutton"@%x@".setvisible(1);");
 eval("rgbanimatorgui_framebutton"@%f@".setvisible(0);");
}

function rgbani_createmacro(){
 $rgbani_colorred=rgbani_findpaintcolor("1 0 0");
 $rgbani_colorgreen=rgbani_findpaintcolor("0 1 0");
 $rgbani_colorblue=rgbani_findpaintcolor("0 0 1");
 $rgbani_coloryellow=rgbani_findpaintcolor("1 1 0");
 $rgbani_colormagenta=rgbani_findpaintcolor("1 0 1");
 $rgbani_colorcyan=rgbani_findpaintcolor("0 1 1");
 $rgbani_colorblack=rgbani_findpaintcolor("0 0 0");
 $rgbani_colorwhite=rgbani_findpaintcolor("1 1 1");

 if(isobject($buildmacroso))
  $buildmacroso.delete();
 $buildmacroso=new scriptobject(){
  class=buildmacroso;
 };
 %count=0;
 %oldcolor=-1;
 for(%y=$rgbani_ydim-1;%y>-1;%y--){
  for(%x=0;%x<$rgbani_xdim;%x++){
   %color=rgbani_getcolor(%x,%y);
   if(%color!=%oldcolor)
    $buildmacroso.event[%count]="Server\tUseSprayCan\t"@%color@"\t\t\t\t\t";
   %oldcolor=%color;
   %count++;
   $buildmacroso.event[%count]="Server\tPlantBrick\t\t\t\t\t\t";
   %count++;
   $buildmacroso.event[%count]="Server\tShiftBrick\t0\t-1\t0\t\t\t";
   %count++;
  }
  $buildmacroso.event[%count]="Server\tShiftBrick\t0\t"@$rgbani_xdim@"\t3\t\t\t";
  %count++;
 }
 $buildmacroso.numevents=%count;
 %inv="";
 for(%x=0;%x<10;%x++){
  %inv=%inv@$invdata[%x]@"\t";
 }
 $buildmacroso.brickarray=getsubstr(%inv,0,strlen(%inv)-1);
 rgbanimatorgui_createmacrobutton.settext("Macro Created");
}

function rgbani_getcolor(%x,%y){
 if($rgbani_framegrid[1,%x,%y]){
  if($rgbani_framegrid[2,%x,%y]){
   if($rgbani_framegrid[3,%x,%y]){
    //white
    return($rgbani_colorwhite);
   }else{
    //yellow
    return($rgbani_coloryellow);
   }
  }else{
   if($rgbani_framegrid[3,%x,%y]){
    //magenta
    return($rgbani_colormagenta);
   }else{
    //red
    return($rgbani_colorred);
   }
  }
 }else{
  if($rgbani_framegrid[2,%x,%y]){
   if($rgbani_framegrid[3,%x,%y]){
    //cyan
    return($rgbani_colorcyan);
   }else{
    //green
    return($rgbani_colorgreen);
   }
  }else{
   if($rgbani_framegrid[3,%x,%y]){
    //blue
    return($rgbani_colorblue);
   }else{
    //black
    return($rgbani_colorblack);
   }
  }
 }
}

function rgbani_findpaintcolor(%color){
 %count=0;
 %mindif=256;
 %len=vectorlen(%color);
 while(getword(%c=getcoloridtable(%count),3)){
  if(getword(%c,3)==1){
   %dif=vectordist(%color,%c)+0.5*(vectorlen(%c)-%len);
   if(%dif<%minDif){
    %minDif=%dif;
    %result=%count;
   }
  }
  %count++;
 }
 return(%result);
}

function rgbani_clearframe(){
 for(%x=0;%x<$rgbani_maxxdim;%x++){
  for(%y=0;%y<$rgbani_maxydim;%y++)
   $rgbani_framegrid[$rgbani_frame,%x,%y]=0;
 }
 rgbani_drawframe($rgbani_frame);
 rgbanimatorgui_createmacrobutton.settext("Create Macro");
}

function rgbani_setdims(){
 %x=rgbanimatorgui_ydimtxt.getvalue();
 %y=rgbanimatorgui_xdimtxt.getvalue();
 if(%x>0 && %x<=$rgbani_maxxdim && %y>0 && %y<=$rgbani_maxydim){
  $rgbani_xdim=%x;
  $rgbani_ydim=%y;
 }else{
  $rgbani_xdim=$rgbani_maxxdim;
  $rgbani_ydim=$rgbani_maxydim;
  rgbanimatorgui_xdimtxt.setvalue($rgbani_ydim);
  rgbanimatorgui_ydimtxt.setvalue($rgbani_xdim);
 }
 for(%x=0;%x<$rgbani_maxxdim;%x++){
  for(%y=0;%y<$rgbani_maxydim;%y++){
   if(%x<$rgbani_xdim && %y<$rgbani_ydim){
    eval("rgbanibutton"@%x@"_"@%y@".setvisible(1);");
    eval("rgbanibuttoncover"@%x@"_"@%y@".setvisible(1);");
   }else{
    eval("rgbanibutton"@%x@"_"@%y@".setvisible(0);");
    eval("rgbanibuttoncover"@%x@"_"@%y@".setvisible(0);");
   }
  }
 }
 rgbanimatorgui_createmacrobutton.settext("Create Macro");

 //realign buttons n' stuff
 %xdim=$rgbani_xdim;
 %ydim=$rgbani_ydim;
 if(%xdim<12)
  %xdim=12;
 %p=rgbanimatorgui_window.getposition();
 rgbanimatorgui_window.resize(getword(%p,0),getword(%p,1),(120+%xdim*15),(156+%ydim*18));
 rgbanimatorgui_createmacrobutton.position=(-3+%xdim*15)@" 44";
 rgbanimatorgui_clearframebutton.position="20 "@(106+%ydim*18);
 rgbanimatorgui_setdimsbutton.position=(-3+%xdim*15) SPC (106+%ydim*18);
 rgbanimatorgui_xdimtxt.position=(-35+%xdim*15) SPC (112+%ydim*18);
 rgbanimatorgui_ydimtxt.position=(-65+%xdim*15) SPC (112+%ydim*18);
 rgbanimatorgui_window.setactive(1);
}

// Zor 2007/11/09