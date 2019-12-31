datablock fxLightData(RainbowLight)
{
	uiName = "Rainbow";

	LightOn = true;
	radius = 10;
	brightness = 10;
	color = "1 1 1 1";

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
	RedKeys			= "ZYXWVUTSRQPONMLKJIHGFEDCBAAAAAAAAAAAAAAAAAAAAAAAAABCDEFGHIJKLMNOPQRSTUVWXYZ";
	GreenKeys		= "BCDEFGHIJKLMNOPQRSTUVWXYZZYXWVUTSRQPONMLKJIHGFEDCBAAAAAAAAAAAAAAAAAAAAAAAAA";
	BlueKeys		= "AAAAAAAAAAAAAAAAAAAAAAAAABCDEFGHIJKLMNOPQRSTUVWXYZZYXWVUTSRQPONMLKJIHGFEDCB";
	
	BrightnessKeys	= "AAAAAAA";
	RadiusKeys		= "AAAAAAA";
	OffsetKeys		= "AAAAAAA";
	RotationKeys	= "AAAAAAA";

	ColorTime		= 60;
	BrightnessTime	= 1.0;
	RadiusTime		= 1.0;
	OffsetTime		= 1.0;
	RotationTime	= 1.0;

	LerpColor		= true;
	LerpBrightness	= false;
	LerpRadius		= false;
	LerpOffset		= false;
	LerpRotation	= false;
};
datablock fxLightData(RainbowQuick)
{
	uiName = "Rainbow Quick";

	LightOn = true;
	radius = 10;
	brightness = 10;
	color = "1 1 1 1";

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
	RedKeys			= "ZYXWVUTSRQPONMLKJIHGFEDCBAAAAAAAAAAAAAAAAAAAAAAAAABCDEFGHIJKLMNOPQRSTUVWXYZ";
	GreenKeys		= "BCDEFGHIJKLMNOPQRSTUVWXYZZYXWVUTSRQPONMLKJIHGFEDCBAAAAAAAAAAAAAAAAAAAAAAAAA";
	BlueKeys		= "AAAAAAAAAAAAAAAAAAAAAAAAABCDEFGHIJKLMNOPQRSTUVWXYZZYXWVUTSRQPONMLKJIHGFEDCB";
	
	BrightnessKeys	= "AAAAAAA";
	RadiusKeys		= "AAAAAAA";
	OffsetKeys		= "AAAAAAA";
	RotationKeys	= "AAAAAAA";

	ColorTime		= 10;
	BrightnessTime	= 1.0;
	RadiusTime		= 1.0;
	OffsetTime		= 1.0;
	RotationTime	= 1.0;

	LerpColor		= true;
	LerpBrightness	= false;
	LerpRadius		= false;
	LerpOffset		= false;
	LerpRotation	= false;
};
datablock fxLightData(RainbowStrobe)
{
	uiName = "Rainbow Strobe";

	LightOn = true;
	radius = 10;
	brightness = 30;
	color = "1 1 1 1";

	FlareOn			= true;
	FlareTP			= true;
	Flarebitmap		= "base/lighting/flare";
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
	AnimBrightness	= true;
	AnimOffsets		= false;
	AnimRotation	= false;
	LinkFlare		= true;
	LinkFlareSize	= false;
	MinColor		= "0.2 0.2 0.2";
	MaxColor		= "1 1 1";
	MinBrightness	= 0.0;
	MaxBrightness	= 5.0;
	MinRadius		= 0.1;
	MaxRadius		= 20;
	StartOffset		= "-5 0 0";
	EndOffset		= "5 0 0";
	MinRotation		= 0;
	MaxRotation		= 359;

	SingleColorKeys	= false;
	RedKeys			= "ZYXWVUTSRQPONMLKJIHGFEDCBAAAAAAAAAAAAAAAAAAAAAAAAABCDEFGHIJKLMNOPQRSTUVWXYZ";
	GreenKeys		= "BCDEFGHIJKLMNOPQRSTUVWXYZZYXWVUTSRQPONMLKJIHGFEDCBAAAAAAAAAAAAAAAAAAAAAAAAA";
	BlueKeys		= "AAAAAAAAAAAAAAAAAAAAAAAAABCDEFGHIJKLMNOPQRSTUVWXTZZYXWVUTSRQPONMLKJIHGFEDCB";
	
	BrightnessKeys		= "AZAZAZA";
	RadiusKeys		= "AAAAAAA";
	OffsetKeys		= "AAAAAAA";
	RotationKeys	= "AAAAAAA";

	ColorTime		= 10;
	BrightnessTime	= 0.3;
	RadiusTime		= 1.0;
	OffsetTime		= 1.0;
	RotationTime	= 1.0;

	LerpColor		= true;
	LerpBrightness	= false;
	LerpRadius		= false;
	LerpOffset		= false;
	LerpRotation	= false;
};