datablock fxLightData(GYRLightNS)
{
	uiName = "Traffic Light N/S";

	LightOn = true;
	radius = 11;
	brightness = 11;
	color = "1 1 1";

	FlareOn			= true;
	FlareTP			= true;
	Flarebitmap		= "base/lighting/corona";
	FlareColor		= "1 1 1";
	ConstantSizeOn	= false;
	ConstantSize	= 1;
	NearSize		= 1;
	FarSize			= 0.9;
	NearDistance	= 10.0;
	FarDistance		= 30.0;
	FadeTime		= 0.1;
	BlendMode		= 0;

	AnimColor		= true;
	AnimBrightness	= false;
	AnimOffsets		= false;
	AnimRotation	= false;
	LinkFlare		= true;
	LinkFlareSize	= false;
	MinColor		= "0 0 0";
	MaxColor		= "1 1 1";
	MinBrightness	= 0.0;
	MaxBrightness	= 1.0;
	MinRadius		= 0.1;
	MaxRadius		= 20;
	StartOffset		= "-5 0 0";
	EndOffset		= "5 0 0";
	MinRotation		= 0;
	MaxRotation		= 359;

	SingleColorKeys	= false;
	RedKeys			= "AAAAAAAAZZZZZZZZZZZZA";
	GreenKeys		= "ZZZZZZZZZZAAAAAAAAAAA";
	BlueKeys		= "AAAAAAAAAAAAAAAAAAAAA";
	
	BrightnessKeys	= "ZZZZZZZ";
	RadiusKeys		= "ZZZZZZZ";
	OffsetKeys		= "ZZZZZZZ";
	RotationKeys	= "ZZZZZZZ";

	ColorTime		= 40.0;
	BrightnessTime	= 1.0;
	RadiusTime		= 1.0;
	OffsetTime		= 1.0;
	RotationTime	= 1.0;

	LerpColor		= false;
	LerpBrightness	= true;
	LerpRadius		= true;
	LerpOffset		= true;
	LerpRotation	= true;
};
datablock fxLightData(GYRLightEW)
{
	uiName = "Traffic Light E/W";

	LightOn = true;
	radius = 11;
	brightness = 11;
	color = "1 1 1";

	FlareOn			= true;
	FlareTP			= true;
	Flarebitmap		= "base/lighting/corona";
	FlareColor		= "1 1 1";
	ConstantSizeOn	= false;
	ConstantSize	= 1;
	NearSize		= 1;
	FarSize			= 0.9;
	NearDistance	= 10.0;
	FarDistance		= 30.0;
	FadeTime		= 0.1;
	BlendMode		= 0;

	AnimColor		= true;
	AnimBrightness	= false;
	AnimOffsets		= false;
	AnimRotation	= false;
	LinkFlare		= true;
	LinkFlareSize	= false;
	MinColor		= "0 0 0";
	MaxColor		= "1 1 1";
	MinBrightness	= 0.0;
	MaxBrightness	= 1.0;
	MinRadius		= 0.1;
	MaxRadius		= 20;
	StartOffset		= "-5 0 0";
	EndOffset		= "5 0 0";
	MinRotation		= 0;
	MaxRotation		= 359;

	SingleColorKeys	= false;
	RedKeys			= "ZZZZZZZZZZAAAAAAAAZZA";
	GreenKeys		= "AAAAAAAAAAZZZZZZZZZZA";
	BlueKeys		= "AAAAAAAAAAAAAAAAAAAAA";
	
	BrightnessKeys	= "ZZZZZZZ";
	RadiusKeys		= "ZZZZZZZ";
	OffsetKeys		= "ZZZZZZZ";
	RotationKeys	= "ZZZZZZZ";

	ColorTime		= 40.0;
	BrightnessTime	= 1.0;
	RadiusTime		= 1.0;
	OffsetTime		= 1.0;
	RotationTime	= 1.0;

	LerpColor		= false;
	LerpBrightness	= true;
	LerpRadius		= true;
	LerpOffset		= true;
	LerpRotation	= true;
};

//These lights were made by Slezak, with inspiration from the Cityscape light mod.
//They are my first lights, and I hope to make more! Thanks for dl'ing!