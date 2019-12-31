<?php

class Sparse3D
{
   var $mArray;

   function Sparse3D()
   {
      $mArray = array();
   }

   function get($x, $y, $z)
   {
      if(!isset($this->mArray[$x]))
         return -1;

      if(!isset($this->mArray[$x][$y]))
         return -1;

      if(!isset($this->mArray[$x][$y][$z]))
         return -1;

      return $this->mArray[$x][$y][$z];
   }

   function set($x, $y, $z, $v)
   {
      if(!isset($this->mArray[$x]))
      {
         $this->mArray[$x] = array();
         $this->mArray[$x][$y] = array();
         $this->mArray[$x][$y][$z] = $v;
         return;
      }

      if(!isset($this->mArray[$x][$y]))
      {
         $this->mArray[$x][$y] = array();
         $this->mArray[$x][$y][$z] = $v;
         return;
      }

      $this->mArray[$x][$y][$z] = $v;
   }

   function isSetX($x)
   {
      return isset($this->mArray[$x]);
   }

   function isSetXY($x, $y)
   {
      return isset($this->mArray[$x]) && isset($this->mArray[$x][$y]);
   }

   function isSetXYZ($x, $y, $z)
   {
      return isset($this->mArray[$x]) && isset($this->mArray[$x][$y]) && isset($this->mArray[$x][$y][$z]);
   }
}

function projectIso($blahx, $blahy, $z)
{
	global $direction;

	switch($direction)
	{
		case 0:
			$x = $blahx;
			$y = $blahy;
			break;
		case 1:
			$x = $blahy;
			$y = $bounds_x - $blahx;
			break;
		case 2:
			$x = $bounds_x - $blahx;
			$y = $bounds_y - $blahy;
			break;
		case 3:
			$x = $bounds_y - $blahy;
			$y = $blahx;
			break;
		default:
			$x = $blahx;
			$y = $blahy;
			break;
	}

   global $bounds_y;
   $y = $bounds_y - $y;
   $pixelX = $x - $y;
   $pixelY = (( $x + $y )*0.5) - ($z * 0.6);

   return array($pixelX, $pixelY);
}

function clampColor($x)
{
   if($x > 255)
      return 255;
   else if ($x < 0)
      return 0;
   else
      return $x;
}

$debug = false;

//process command arguments
$direction = 0;
for($i = 0; $i < $argc; $i++)
{
	switch($argv[$i])
	{
		case "-direction":
			if($i >= ($argc - 1))
				die("usage: -direction <0-3>");
			$i++;
			$direction = $argv[$i];
			break;
		default:
			//unknown argument - ignore
			break;
	}
}


// Load up the bloxel file.
$fd = fopen("PixelCrap.txt", 'r') or die("couldn't open PixelCrap.txt");

// Read the bounds
$l = fgets($fd);

if(!strcmp("[BOUNDS]", $l))
{
   die("didn't get bounds section");
}

$boundsStr = fgets($fd);
list($bounds_x, $bounds_y, $bounds_z) = sscanf($boundsStr, "%f %f %f");

// Create an image and figure out our visible range.

// Origin is always one corner - what is the other?
$minX = 1; $maxX = 0;
$minY = 1; $maxY = 0;

list($upperX, $upperY) = projectIso($bounds_x, $bounds_y, $bounds_z);
if($upperX < $minX) $minX = $upperX; if($upperX > $maxX) $maxX = $upperX; if($upperY < $minY) $minY = $upperY; if($upperY > $maxY) $maxY = $upperY;
list($upperX, $upperY) = projectIso($bounds_x, $bounds_y, 0);
if($upperX < $minX) $minX = $upperX; if($upperX > $maxX) $maxX = $upperX; if($upperY < $minY) $minY = $upperY; if($upperY > $maxY) $maxY = $upperY;
list($upperX, $upperY) = projectIso($bounds_x, 0, $bounds_z);
if($upperX < $minX) $minX = $upperX; if($upperX > $maxX) $maxX = $upperX; if($upperY < $minY) $minY = $upperY; if($upperY > $maxY) $maxY = $upperY;
list($upperX, $upperY) = projectIso($bounds_x, 0, 0);
if($upperX < $minX) $minX = $upperX; if($upperX > $maxX) $maxX = $upperX; if($upperY < $minY) $minY = $upperY; if($upperY > $maxY) $maxY = $upperY;
list($upperX, $upperY) = projectIso(0, $bounds_y, $bounds_z);
if($upperX < $minX) $minX = $upperX; if($upperX > $maxX) $maxX = $upperX; if($upperY < $minY) $minY = $upperY; if($upperY > $maxY) $maxY = $upperY;
list($upperX, $upperY) = projectIso(0, $bounds_y, 0);
if($upperX < $minX) $minX = $upperX; if($upperX > $maxX) $maxX = $upperX; if($upperY < $minY) $minY = $upperY; if($upperY > $maxY) $maxY = $upperY;
list($upperX, $upperY) = projectIso(0, 0, $bounds_z);
if($upperX < $minX) $minX = $upperX; if($upperX > $maxX) $maxX = $upperX; if($upperY < $minY) $minY = $upperY; if($upperY > $maxY) $maxY = $upperY;
list($upperX, $upperY) = projectIso(0, 0, 0);
if($upperX < $minX) $minX = $upperX; if($upperX > $maxX) $maxX = $upperX; if($upperY < $minY) $minY = $upperY; if($upperY > $maxY) $maxY = $upperY;

if($debug) echo("min (" . $minX . "," . $minY . ") max (" . $maxX . "," . $maxY . ")\n");

$xOffset = -$minX; $yOffset = -$minY;

$gd = imagecreatetruecolor($maxX - $minX, $maxY - $minY);
imagefill($gd, 0,0, imagecolorallocatealpha($gd,0,0,0,127));

// Process the colors section.
$l = fgets($fd);

if(!strcmp("[COLORS]", $l))
{
   die("didn't get colors section");
}

$colors = array();

$centerValue    = -25; //change this to adjust global brightness

$shadeShift     = $centerValue - 35;
$normalShift    = $centerValue - 0;
$lightShift     = $centerValue + 25;
$veryLightShift = $centerValue + 50;

$numColors = fgets($fd) or die("couldn't read number of colors");
for($i=0; $i<$numColors; $i++)
{
   $colStr = fgets($fd) or die("couldn't read line from colors section, there should be 64 lines");

   $r = $g = $b = $a = 0;
   list($r, $g, $b, $a) = sscanf($colStr, "%d %d %d %d");
   if($debug) echo(" - Noting color " . $r . " " . $g . " " . $b . " " . ($a/2) . "\n");
   $colors[$i]     = imagecolorallocatealpha($gd, clampColor($r + $normalShift   ), clampColor($g + $normalShift   ), clampColor($b + $normalShift   ), round((255 - $a) / 2));
   $colors[$i + $numColors]   = imagecolorallocatealpha($gd, clampColor($r + $shadeShift    ), clampColor($g + $shadeShift    ), clampColor($b + $shadeShift    ), round((255 - $a) / 2));
   $colors[$i + $numColors*2] = imagecolorallocatealpha($gd, clampColor($r + $lightShift    ), clampColor($g + $lightShift    ), clampColor($b + $lightShift    ), round((255 - $a) / 2));
   $colors[$i + $numColors*3] = imagecolorallocatealpha($gd, clampColor($r + $veryLightShift), clampColor($g + $veryLightShift), clampColor($b + $veryLightShift), round((255 - $a) / 2));
}

if($debug)
{
   for($i=0; $i<8; $i++)
      for($j=0; $j<8; $j++)
         imagesetpixel($gd, $i, $j, $colors[$i*8+$j]);
}

// Now read the points and plot them as we go.
$curColor = 0;

$l = fgets($fd);

if(!strcmp("[POINTS]", $l))
{
   die("didn't get points section");
}

// Read the rest of the file.
$arr = new Sparse3D();
while(!feof($fd))
{
   // And process each line as we come to it.
   $l = fgets($fd);

   // Color change?
   if(strstr($l, "COLOR"))
   {
      list($curColor) = sscanf($l, "COLOR %d");
      if($debug) echo(" changing color to " . $curColor . "\n");
      continue;
   }

   // New point...
   list($x, $y, $z) = sscanf($l, "%f %f %f");
   if($debug) echo("   plotting point at " . $x . ", " . $y . ", " . $z . " with color " . $curColor . "\n");


   // Figure its position in isometric space...
   $arr->set(round($x), round($y), round($z), $curColor);
}

// Great - now walk along the y slices and plot everything.
switch($direction)
{
	case 0:
		//echo("FUCK");
		ksort($arr->mArray);
		foreach($arr->mArray as $walkX => $xSlice)
		{
		   ksort($xSlice);
		   foreach($xSlice as $walkY => $ySlice)
		   {
			  ksort($ySlice);
			  foreach($ySlice as $walkZ => $pixel)
			  {
				 // normal - dark - light - very light
				 if ($arr->get($walkX - 1, $walkY, $walkZ) == -1 && $arr->get($walkX + 1, $walkY, $walkZ) != -1 && $arr->get($walkX, $walkY, $walkZ + 1) == -1)
					$finalColor = $pixel + $numColors*3; //very light
				 else if($arr->get($walkX + 1, $walkY, $walkZ) == -1 || ($colors[$arr->get($walkX, $walkY, $walkZ)] >> 24 == 0 && $colors[$arr->get($walkX + 1, $walkY, $walkZ)] >> 24 > 0) )
					$finalColor = $pixel + $numColors; //dark
				 else if ($arr->get($walkX, $walkY, $walkZ + 1) == -1 || ($arr->get($walkX - 1, $walkY, $walkZ) == -1 && $arr->get($walkX + 1, $walkY, $walkZ) != -1)  )
					$finalColor = $pixel + $numColors*2; //light
				 else
					$finalColor = $pixel; //normal

				 list($x, $y) = projectIso($walkX, $walkY, $walkZ);
				 imagesetpixel($gd, $x + $xOffset, $y + $yOffset, $colors[$finalColor]);
			  }
		   }
		}
		break;
	case 1:
		ksort($arr->mArray);
		foreach($arr->mArray as $walkX => $xSlice)
		{
		   ksort($xSlice);
		   foreach($xSlice as $walkY => $ySlice)
		   {
			  ksort($ySlice);
			  foreach($ySlice as $walkZ => $pixel)
			  {
				 // normal - dark - light - very light
				 if ($arr->get($walkX, $walkY - 1, $walkZ) == -1 && $arr->get($walkX, $walkY + 1, $walkZ) != -1 && $arr->get($walkX, $walkY, $walkZ + 1) == -1)
					$finalColor = $pixel + $numColors*3; //very light
				 else if($arr->get($walkX, $walkY + 1, $walkZ) == -1 || ($colors[$arr->get($walkX, $walkY, $walkZ)] >> 24 == 0 && $colors[$arr->get($walkX, $walkY + 1, $walkZ)] >> 24 > 0) )
					$finalColor = $pixel + $numColors; //dark
				 else if ($arr->get($walkX, $walkY, $walkZ + 1) == -1 || ($arr->get($walkX, $walkY - 1, $walkZ) == -1 && $arr->get($walkX, $walkY + 1, $walkZ) != -1)  )
					$finalColor = $pixel + $numColors*2; //light
				 else
					$finalColor = $pixel; //normal

				 list($x, $y) = projectIso($walkX, $walkY, $walkZ);
				 imagesetpixel($gd, $x + $xOffset, $y + $yOffset, $colors[$finalColor]);
			  }
		   }
		}
		break;
	case 2:
		krsort($arr->mArray);
		foreach($arr->mArray as $walkX => $xSlice)
		{
		   ksort($xSlice);
		   foreach($xSlice as $walkY => $ySlice)
		   {
			  ksort($ySlice);
			  foreach($ySlice as $walkZ => $pixel)
			  {
				 // normal - dark - light - very light
				 if ($arr->get($walkX + 1, $walkY, $walkZ) == -1 && $arr->get($walkX - 1, $walkY, $walkZ) != -1 && $arr->get($walkX, $walkY, $walkZ + 1) == -1)
					$finalColor = $pixel + $numColors*3; //very light
				 else if($arr->get($walkX - 1, $walkY, $walkZ) == -1 || ($colors[$arr->get($walkX, $walkY, $walkZ)] >> 24 == 0 && $colors[$arr->get($walkX - 1, $walkY, $walkZ)] >> 24 > 0) )
					$finalColor = $pixel + $numColors; //dark
				 else if ($arr->get($walkX, $walkY, $walkZ + 1) == -1 || ($arr->get($walkX + 1, $walkY, $walkZ) == -1 && $arr->get($walkX - 1, $walkY, $walkZ) != -1)  )
					$finalColor = $pixel + $numColors*2; //light
				 else
					$finalColor = $pixel; //normal

				 list($x, $y) = projectIso($walkX, $walkY, $walkZ);
				 imagesetpixel($gd, $x + $xOffset, $y + $yOffset, $colors[$finalColor]);
			  }
		   }
		}
		break;
	case 3:
		krsort($arr->mArray);
		foreach($arr->mArray as $walkX => $xSlice)
		{
		   ksort($xSlice);
		   foreach($xSlice as $walkY => $ySlice)
		   {
			  ksort($ySlice);
			  foreach($ySlice as $walkZ => $pixel)
			  {
				 // normal - dark - light - very light
				 if ($arr->get($walkX, $walkY + 1, $walkZ) == -1 && $arr->get($walkX, $walkY - 1, $walkZ) != -1 && $arr->get($walkX, $walkY, $walkZ + 1) == -1)
					$finalColor = $pixel + $numColors*3; //very light
				 else if($arr->get($walkX, $walkY - 1, $walkZ) == -1 || ($colors[$arr->get($walkX, $walkY, $walkZ)] >> 24 == 0 && $colors[$arr->get($walkX, $walkY - 1, $walkZ)] >> 24 > 0) )
					$finalColor = $pixel + $numColors; //dark
				 else if ($arr->get($walkX, $walkY, $walkZ + 1) == -1 || ($arr->get($walkX, $walkY + 1, $walkZ) == -1 && $arr->get($walkX, $walkY - 1, $walkZ) != -1)  )
					$finalColor = $pixel + $numColors*2; //light
				 else
					$finalColor = $pixel; //normal

				 list($x, $y) = projectIso($walkX, $walkY, $walkZ);
				 imagesetpixel($gd, $x + $xOffset, $y + $yOffset, $colors[$finalColor]);
			  }
		   }
		}
		break;
}


header('Content-Type: image/png');
imagesavealpha($gd, true);
imagepng($gd);

?>
