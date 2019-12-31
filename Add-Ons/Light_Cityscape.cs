datablock fxLightData(NaFlickerLight)
{
	uiName = "Sodium Lamp";

	LightOn = true;
	radius = 18;
	brightness = 8;
	color = "1 1 1";

	FlareOn			= true;
	FlareTP			= true;
	Flarebitmap		= "base/lighting/corona";
	FlareColor		= "1 1 1";
	ConstantSizeOn	= false;
	ConstantSize	= 1;
	NearSize		= 3;
	FarSize			= 0.5;
	NearDistance	= 10.0;
	FarDistance		= 30.0;
	FadeTime		= 0.1;
	BlendMode		= 0;

	AnimColor		= true;
	AnimBrightness	= true;
	AnimOffsets		= false;
	AnimRotation	= false;
	LinkFlare		= true;
	LinkFlareSize	= true;
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
	RedKeys			= "ZZZZZZZ";
	GreenKeys		= "MPKPONJ";
	BlueKeys		= "AAAAAAA";
	
	BrightnessKeys	= "ZZZZZZZ";
	RadiusKeys		= "ZZZZZZZ";
	OffsetKeys		= "ZZZZZZZ";
	RotationKeys	= "AAAAAAA";

	ColorTime		= 0.3;
	BrightnessTime	= 1.0;
	RadiusTime		= 1.0;
	OffsetTime		= 1.0;
	RotationTime	= 1.0;

	LerpColor		= false;
	LerpBrightness	= false;
	LerpRadius		= false;
	LerpOffset		= false;
	LerpRotation	= false;
};


datablock fxLightData(GYRLight)
{
	uiName = "Traffic Light";

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
	FarSize			= 0.5;
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
	RedKeys			= "AAZZZZA";
	GreenKeys		= "ZZZZAAA";
	BlueKeys		= "AAAAAAA";
	
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

datablock fxLightData(PoliceLight)
{
	uiName = "Police Lights";

	LightOn = true;
	radius = 10;
	brightness = 10;
	color = "1 1 1";

	FlareOn			= true;
	FlareTP			= true;
	Flarebitmap		= "base/lighting/corona";
	FlareColor		= "1 1 1";
	ConstantSizeOn	= false;
	ConstantSize	= 1;
	NearSize		= 1;
	FarSize			= 0.5;
	NearDistance	= 10.0;
	FarDistance		= 30.0;
	FadeTime		= 0.1;
	BlendMode		= 0;

	AnimColor		= true;
	AnimBrightness	= false;
	AnimOffsets		= false;
	AnimRotation	= false;
	AnimRadius		= true;
	LinkFlare		= true;
	LinkFlareSize	= true;
	MinColor		= "0 0 0";
	MaxColor		= "1 1 1";
	MinBrightness	= 0.0;
	MaxBrightness	= 1.0;
	MinRadius		= 1;
	MaxRadius		= 10;
	StartOffset		= "-5 0 0";
	EndOffset		= "5 0 0";
	MinRotation		= 0;
	MaxRotation		= 359;

	SingleColorKeys	= false;
	RedKeys			= "FZGZFFAAAAAA";
	GreenKeys		= "AAAAAAAAAAAA";
	BlueKeys		= "AAAAAAFZGZFF";
	
	BrightnessKeys	= "AAAAAAAAAAAA";
	RadiusKeys		= "FZGZFFFZGZFF";
	OffsetKeys		= "AAAAAAAAAAAA";
	RotationKeys	= "AAAAAAAAAAAA";

	ColorTime		= 0.4;
	BrightnessTime	= 1.0;
	RadiusTime		= 0.4;
	OffsetTime		= 1.0;
	RotationTime	= 1.0;

	LerpColor		= true;
	LerpBrightness	= true;
	LerpRadius		= true;
	LerpOffset		= true;
	LerpRotation	= true;
};