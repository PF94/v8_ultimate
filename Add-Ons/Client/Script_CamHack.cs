//--------------------------------------------------------- -- -
// Script_CamHack
// by SolarFlare tutorial by Itake
// To set the value type: camHack(<value here>);
//--------------------------------------------------------- -- -

//--------------------------------------------------------- -- -
package autoChaseCam
{
   function chaseCam(%val)
   {
      serverConnection.chaseCam(%val);
   }
   function playGui::onWake(%this)
   {
      chaseCam($Pref::chaseCam);
      parent::onWake(%this);
   }
};
activatePackage(autoChaseCam);
function camHack(%cam)
{
   chaseCam(%cam);
   $Pref::ChaseCam = %cam;
}