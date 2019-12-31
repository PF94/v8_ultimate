datablock fxLightData(Candlelight)
{
	uiName = "Candle";

	LightOn = true;
	radius = 4;
	brightness = 1;
	color = "1 1 1 1";

	FlareOn			= true;
	FlareTP			= true;
	Flarebitmap		= "base/lighting/corona";
	FlareColor		= "1 1 1";
	ConstantSizeOn	= false;
	ConstantSize	= 1;
	NearSize		= 0.2;
	FarSize			= 0.1;
	NearDistance	= 10.0;
	FarDistance		= 30.0;
	FadeTime		= 0.1;
	BlendMode		= 0;

	AnimColor		= True;
	AnimBrightness	= false;
	AnimOffsets		= true;
	AnimRadius	= true;
	AnimRotation	= false;
	LinkFlare		= True;
	LinkFlareSize	= true;
	MinColor		= "0 0 0";
	MaxColor		= "1 1 1";
	MinBrightness	= 0.0;
	MaxBrightness	= 1.0;
	MinRadius		= 3.5;
	MaxRadius		= 4.5;
	StartOffset		= "0 0 0.4";
	EndOffset		= "0 0 0.4";
	MinRotation		= 0;
	MaxRotation		= 359;

	SingleColorKeys	= False;
	RedKeys			= "ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ";
	GreenKeys		= "LRMONKPMLNOPLSQNRNRPNLNRMKPNKQROMSPKQMNPLPRMKRLNKN";
	BlueKeys		= "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
	
	BrightnessKeys	= "AAAAAAA";
	RadiusKeys		= "NCOICUNSDICNSIDNZXIPNCIHZDBNCIPHBNXCZIHCZBNDCHIICB";
	OffsetKeys		= "AAAAAAA";
	RotationKeys	= "AAAAAAA";

	ColorTime		= 5;
	BrightnessTime	= 1.0;
	RadiusTime		= 5;
	OffsetTime		= 1.0;
	RotationTime	= 1.0;

	LerpColor		= True;
	LerpBrightness	= false;
	LerpRadius		= true;
	LerpOffset		= false;
	LerpRotation	= false;
};