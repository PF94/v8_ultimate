function servercmdChangeLight(%client,%light)
{
if(!isObject(%client.light)){servercmdLight(%client);}
switch$(%light)
{
  case "Red": if(isObject(redLight)){%client.light.setDatablock(RedLight);}
  case "Blue": if(isObject(blueLight)){%client.light.setDatablock(BlueLight);}
  case "Green": if(isObject(greenLight)){%client.light.setDatablock(GreenLight);}
  case "RGB": if(isObject(rgbLight)){%client.light.setDatablock(rgbLight);}
  case "White": if(isObject(playerLight)){%client.light.setDatablock(playerLight);}
  case "Bright": if(isObject(brightLight)){%client.light.setDatablock(brightLight);}
  case "Alarm": if(isObject(alarmLightA)){%client.light.setDatablock(alarmLightA);}
}
}