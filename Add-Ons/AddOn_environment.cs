//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------



//-----------------------------------------------------------------------------

datablock AudioProfile(HeavyRainSound)
{
   filename    = "./environmentS/ambient/rain.ogg";
   description = AudioLooping2d;
};

datablock PrecipitationData(HeavyRain)
{
   soundProfile = "HeavyRainSound";

   dropTexture = "./environment/rain";
   splashTexture = "./environment/water_splash";
   dropSize = 0.75;
   splashSize = 0.2;
   useTrueBillboards = false;
   splashMS = 250;
};

datablock PrecipitationData(HeavyRain2)
{
   dropTexture = "./environment/mist";
   splashTexture = "./environment/mist2";
   dropSize = 10;
   splashSize = 0.1;
   useTrueBillboards = false;
   splashMS = 250;
};

 //-----------------------------------------------------------------------------

datablock AudioProfile(ThunderCrash1Sound)
{
   filename  = "./environmentS/ambient/thunder1.ogg";
   description = Audio2d;
};

datablock AudioProfile(ThunderCrash2Sound)
{
   filename  = "./environmentS/ambient/thunder2.ogg";
   description = Audio2d;
};

datablock AudioProfile(ThunderCrash3Sound)
{
   filename  = "./environmentS/ambient/thunder3.ogg";
   description = Audio2d;
};

datablock AudioProfile(ThunderCrash4Sound)
{
   filename  = "./environmentS/ambient/thunder4.ogg";
   description = Audio2d;
};

datablock LightningData(LightningStorm)
{
   strikeTextures[0]  = "Add-Ons/environment/lightning1frame1";
   strikeTextures[1]  = "Add-Ons/environment/lightning1frame2";
   strikeTextures[2]  = "Add-Ons/environment/lightning1frame3";
   
   //strikeSound = LightningHitSound;
   thunderSounds[0] = ThunderCrash1Sound;
   thunderSounds[1] = ThunderCrash2Sound;
   thunderSounds[2] = ThunderCrash3Sound;
   thunderSounds[3] = ThunderCrash4Sound;
};

