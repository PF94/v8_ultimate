function MakeBaby(%client){
if(isObject(%client.baby)){ %client.baby.delete(); cancel(%client.babyGrow); cancel(%client.babyHunger); cancel(%client.looseWeight);}
%bot = new AIPlayer(){
	datablock = PlayerNoJet;
	position = %client.player.getTransform();
		};
%lolkthx = %client.player;
%client.player = %bot;
%client.applyBodyParts();
%client.applyBodyColors();
%client.player = %lolkthx;
%bot.setScale("0.1 0.1 0.1");
%bot.setMoveObject(%client.player);
%bot.setShapeName(%client.name @ "'s Baby");
%bot.stage = "Baby";
%bot.hunger = "100";
%bot.accent = %client.accent;
%bot.name = %client.name;
%bot.faceName = %client.faceName;
%bot.decalName = %client.decalName;
%bot.accent = %client.accent;
%bot.accentColor = %client.accentColor;
%bot.chest = %client.chest;
%bot.chestColor = %client.chestColor;
%bot.hat = %client.hat;
%bot.hatColor = %client.hatColor;
%bot.headColor = %client.HeadColor;
%bot.hip = %client.hip;
%bot.hipColor = %client.hipColor;
%bot.larm = %client.larm;
%bot.larmColor = %client.larmcolor;
%bot.lhand = %client.lhand;
%bot.lhandcolor = %client.lhandcolor;
%bot.lleg = %client.lleg;
%bot.llegcolor = %client.llegcolor;
%bot.pack = %client.pack;
%bot.packcolor = %client.packcolor;
%bot.rarm = %client.rarm;
%bot.rarmcolor = %client.rarmcolor;
%bot.rhand = %client.rhand;
%bot.rhandcolor = %client.rhandcolor;
%bot.rleg = %client.rleg;
%bot.rlegcolor = %client.rlegcolor;
%bot.secondPack = %client.secondpack;
%bot.secondPackColor = %client.secondPackColor;
%gender = getRandom(0,1);
if(%gender){ %bot.sex = "Boy"; } else { %bot.sex = "Girl"; }
%bot.isGrowing = 1;
%client.baby = %bot;
}

function growUp(%client){
%baby = %client.baby;
if(isObject(%baby)){
%babySize = %baby.getScale();
if(%babySize !$= "1 1 1"){
	%baby.setScale(vectorAdd(%babySize,"0.1 0.1 0.1"));
	messageclient(%client,"","\c6Your " @ %baby.stage @ " has grown.");
	%client.babyGrow = schedule(120000,0,"GrowUp", %client);
	%babysize = %baby.getScale();
if(%babySize $= "0.4 0.4 0.4"){
	%baby.stage = "Toddler";
	%client.player.unMountObject(%baby);
	%client.player.playThread(0, root);
	%baby.inHand = 0;
}else if(%babysize $= "0.6 0.6 0.6"){
	%baby.stage = "Child";
}else if(%babysize $= "0.8 0.8 0.8"){
	%baby.stage = "Teenager";
} else if(%babysize $= "1 1 1"){
	if(%baby.sex $= "Boy"){ %a = "Son"; } else { %a = "Daughter"; }
	%baby.stage = "Adult";
	%baby.isGrowing = 0;
			}
		}
	%baby.setShapeName(%baby.name @ " [ " @ %baby.stage @ " ]");
	}
}

function babyHunger(%client){
if(isObject(%client.baby)){
	%baby = %client.baby;
	%baby.hunger = %baby.hunger--;
	%client.babyHunger = schedule(30000,0,"babyHunger",%client);
if(%baby.hunger < 50 && %baby.hunger > 25){
	messageclient(%client,"","\c3" @ %baby.name @ "\c6: I'm Hungry!!");
} else if(%baby.hunger < 25 && %baby.hunger > 10){
	messageclient(%client,"","\c3" @ %baby.name @ "\c6: Please Feed me :(!!");
	looseWeight(%client);
} else if(%baby.hunger < 10 && %baby.hunger > 0){
	messageclient(%client,"","\c3" @ %baby.name @ "\c6: Why aren't you feeding me :(??");
	looseWeight(%client);
} else if(%baby.hunger <= 0){
	messageclient(%client,"","\c6Your baby has \c0died \c6from starvation.");
	%baby.kill();
	cancel(%client.babygrow);
	cancel(%client.babyHunger);
	cancel(%client.looseWeight);
		}
	}
}

function babyFeed(%client){
if(isObject(%client.baby)){
	%baby = %client.baby;
	%baby.hunger = %baby.hunger+20;
if(%baby.hunger > 150 && %baby.hunger < 175){
	messageclient(%client,"","\c3" @ %baby.name @ "\c6: Please don't feed me anymore, I feel like I'm going to explode!");
	%baby.setScale(vectorAdd(%baby.getScale(),"0.1 0.1 0"));
	%client.looseWeight = schedule(60000,0,"looseWeight",%client);
} else if(%baby.hunger > 175 && %baby.hunger < 200){
	messageclient(%client,"","\c3" @ %baby.name @ "\c6: Stop feeding me, do you want me to be fat or something!");
	%baby.setScale(vectorAdd(%baby.getScale(),"0.1 0.1 0"));
	%client.looseWeight = schedule(60000,0,"looseWeight",%client);
} else if(%baby.hunger >= 200){
	messageclient(%client,"","\c6Your baby has died from over feeding.");
	%baby.kill();
	cancel(%client.babygrow);
	cancel(%client.babyHunger);
	cancel(%client.looseWeight);
		}
	}
}

function looseWeight(%client){
if(isObject(%client.baby)){
	%baby = %client.baby;
	if(%baby.getScale() !$= "0.1 0.1 0.1" || %baby.getScale() !$= "0.1 0.1 0.2" || %baby.getScale() !$= "0.1 0.1 0.3" || %baby.getScale() !$= "0.1 0.1 0.4"|| %baby.getScale() !$= "0.1 0.1 0.5"  || %baby.getScale() !$= "0.1 0.1 0.6" || %baby.getScale() !$= "0.1 0.1 0.7" || %baby.getScale() !$= "0.1 0.1 0.8" || %baby.getScale() !$= "0.1 0.1 0.9" || %baby.getScale() !$= "0.1 0.1 1"){
	%baby.setScale(vectorSub(%baby.getScale(),"0.1 0.1 0"));
		}
	}
}

function serverCmdBaby(%client, %action, %victim, %a){
if(isObject(%client.baby)){
%baby = %client.baby;
	if(%action $= "Fetch"){
	%baby.setTransform(vectorAdd(%client.player.getTransform(),"0 0 0.5"));
	%baby.playThread(0, root);
	%baby.clearAim();
	cancel(%baby.freeroam);
	} else if(%action $= "Stay"){
	cancel(%baby.freeroam);
	%baby.setMoveObject(-1);
	%baby.playThread(0, root);
	%baby.clearAim();
	%baby.stop();
	} else if(%action $= "Follow"){
	cancel(%baby.freeroam);
	%baby.setMoveObject(%client.player);
	%baby.playThread(0, root);
	%baby.clearAim();
	} else if(%action $= "Kill"){
	%baby.kill();
	%client.player.playThread(0, root);
	cancel(%client.babyGrow);
	cancel(%client.babyHunger);
	cancel(%client.looseWeight);
	} else if(%action $= "Dress"){
	%lolkthx = %client.player;
	%client.player = %baby;
	%client.applyBodyParts();
	%client.applyBodyColors();
	%client.player = %lolkthx;
	%baby.playThread(0, root);
	%baby.faceName = %client.faceName;
	%baby.decalName = %client.decalName;
	%baby.accent = %client.accent;
	%baby.accentColor = %client.accentColor;
	%baby.chest = %client.chest;
	%baby.chestColor = %client.chestColor;
	%baby.hat = %client.hat;
	%baby.hatColor = %client.hatColor;
	%baby.headColor = %client.HeadColor;
	%baby.hip = %client.hip;
	%baby.hipColor = %client.hipColor;
	%baby.larm = %client.larm;
	%baby.larmColor = %client.larmcolor;
	%baby.lhand = %client.lhand;
	%baby.lhandcolor = %client.lhandcolor;
	%baby.lleg = %client.lleg;
	%baby.llegcolor = %client.llegcolor;
	%baby.pack = %client.pack;
	%baby.packcolor = %client.packcolor;
	%baby.rarm = %client.rarm;
	%baby.rarmcolor = %client.rarmcolor;
	%baby.rhand = %client.rhand;
	%baby.rhandcolor = %client.rhandcolor;
	%baby.rleg = %client.rleg;
	%baby.rlegcolor = %client.rlegcolor;
	%baby.secondPack = %client.secondpack;
	%baby.secondPackColor = %client.secondPackColor;
	} else if(%action $= "Gender"){
	messageclient(%client,"","\c6Your child is a \c0" @ %baby.sex @ "\c6.");
	} else if(%action $= "Freeroam"){
	%baby.setMoveObject(-1);
	%baby.randomMove();
	%baby.playThread(0, root);
	%baby.clearAim();
	} else if(%action $= "hug"){
		%b = findClientByName(%victim);
		%baby.clearAim();
		cancel(%baby.freeroam);
		if(%a $= ""){
		%baby.setMoveObject(%b.player);
		%baby.playThread(0, armreadyboth);
		} else if(%a $= "Baby"){
		%baby.setMoveObject(%b.baby);
		%baby.playThread(0, armreadyboth);
		}
	} else if(%action $= "Jump"){
		if(%baby.stage $= "Baby"){
		%baby.jump = "0 0 2"; 
		} else if(%baby.stage $= "Toddler"){ 
		%baby.jump = "0 0 5";
		} else if(%baby.stage $= "Child"){
		%baby.jump = "0 0 8";	
		} else if(%baby.stage $= "Teenager"){
		%baby.jump = "0 0 10"; 
		} else if(%baby.stage $= "Adult"){
		%baby.jump = "0 0 12";
			}
		%baby.setVelocity(%baby.jump);
		%baby.clearAim();
		%baby.playThread(0, root);
	} else if(%action $= "Emote"){
		%baby.emote(%victim @ "Image");
		%baby.clearAim();
	} else if(%action $= "Help"){
		if(%victim $= ""){
		messageclient(%client,"","\c6Please type /baby Help and one of the following, Fetch | Stay | Follow | Kill | Dress | Gender | Freeroam | Hug | Jump | Emote | Pause | Grow | Sit | setHome | goHome | Carry | Watch | Shake | Feed | Talk | Name");
		} else if(%victim $= "Fetch"){
		messageclient(%client,"","\c6/Baby Fetch - Brings your child to you.");
		} else if(%victim $= "Stay"){
		messageclient(%client,"","\c6/Baby Stay - Your baby stays where it is.");
		} else if(%victim $= "Follow"){
		messageclient(%client,"","\c6/Baby Follow - makes your child follow you..");
		} else if(%victim $= "Kill"){
		messageclient(%client,"","\c6/Baby Kill - Kills you baby.");
		} else if(%victim $= "Dress"){
		messageclient(%client,"","\c6/Baby Dress - Changes your baby's clothes to you current clothes.");
		} else if(%victim $= "Gender"){
		messageclient(%client,"","\c6/Baby Gender - Tells you what gender your baby is, incase you forget.");
		} else if(%victim $= "Freeroam"){
		messageclient(%client,"","\c6/Baby Freeroam - Makes your baby walk about randomly.");
		} else if(%victim $= "Hug"){
		messageclient(%client,"","\c6/Baby Hug [Name] - Makes your baby hug the baby of the person you say, If you put yourself your baby will hug you.");
		} else if(%victim $= "Jump"){
		messageclient(%client,"","\c6/Baby Jump - Makes your baby jump.");
		} else if(%victim $= "Emote"){
		messageclient(%client,"","\c6/Baby Emote [Emote] - Makes your baby do the emote you say.");
		} else if(%victim $= "Pause"){
		messageclient(%client,"","\c6/Baby Pause - Put your baby on pills to stop it growing.");
		} else if(%victim $= "Grow"){
		messageclient(%client,"","\c6/Baby Grow - Take your baby off the anti-growth pills and make it grow again.");
		} else if(%victim $= "Sit"){
		messageclient(%client,"","\c6/Baby Sit - Makes your baby sit.");
		} else if(%victim $= "setHome"){
		messageclient(%client,"","\c6/Baby setHome - Set's your baby's home where it will return to if you type /baby gohome.");
		} else if(%victim $= "goHome"){
		messageclient(%client,"","\c6/Baby goHome - Makes your baby walk home.");
		} else if(%victim $= "Carry"){
		messageclient(%client,"","\c6/Baby Carry - Picks your baby up, can only do it if it's a baby.");
		} else if(%victim $= "Watch"){
		messageclient(%client,"","\c6/Baby Watch - Centers your camera on the baby so you can watch him, type it again to come out of baby watch.");
		} else if(%victim $= "Shake"){
		messageclient(%client,"","\c6/Baby Shake - Type this while carrying your baby to shake it to death.");
		} else if(%victim $= "Feed"){
		messageclient(%client,"","\c6/Baby Feed - Feed your baby, 0 and 200 hunger levels = Death");
		} else if(%victim $= "Talk"){
		messageclient(%client,"","\c6/Baby Talk - Makes your baby speak to you.");
		} else if(%victim $= "Name"){
		messageclient(%client,"","\c6/Baby Name X Y - Make's your babies name what ever you put as X or Y.");
		}

	} else if(%action $= "Pause"){
		cancel(%client.babyGrow);
		%baby.isGrowing = 0;
		messageclient(%client,"","\c6You have stopped your " @ %baby.stage @ " from growing with the new Anti-Growth pills.");
	} else if(%action $= "Grow"){
		cancel(%client.babygrow);
		%baby.isGrowing = 1;
		%client.babyGrow = schedule(120000,0,"GrowUp", %client);
		messageclient(%client,"","\c6Your " @ %baby.stage @ " will now grow.");
	} else if(%action $= "Sit"){
		%baby.playthread(0, sit);
	} else if(%action $= "Name"){
	if(%a !$= ""){
		%name = %victim SPC %a;
		} else {
		%name = %victim;
		}
	if(strLen(%name)<16){
		%baby.name = %name;
		%baby.setShapeName(%baby.name @ " [ " @ %baby.stage @ " ]");
		} else {
			messageclient(%client,"","\c6You can only have a max of 16 characters in your baby's name");
		}
	} else if(%action $= "setHome"){
		%baby.home = %client.player.getPosition();
		messageclient(%client,"","\c6Your baby's home has been set.");
	} else if(%action $= "goHome"){
		%baby.setMoveObject(-1);
		%baby.setMoveDestination(%baby.home);
		cancel(%baby.freeroam);
		if(%baby.stage $= "Adult"){ if(%baby.sex $= "Boy"){ %b = "Son"; } else { %b = "Daughter"; } } else { %b = %baby.stage; }
		messageclient(%client,"","\c6Your " @ %b @ " is walking home.");
	} else if(%action $= "carry"){
		if(%baby.stage $= "Baby" && !%baby.inHand){
			%client.player.mountObject(%client.baby, 1);
			cancel(%baby.freeroam);
			%baby.clearAim();
			%baby.setMoveObject(-1);
			%client.player.playThread(0, armreadyleft);
			%baby.inHand = 1;
		} else if(%baby.inHand){
			%client.player.unmountObject(%client.baby);
			%client.player.playThread(0, root);
			%baby.inHand = 0;
			}
		} else if(%action $= "watch"){
		if(!%client.isWatching){
			%client.Camera.setOrbitMode(%baby,"",0.5,5,0,0);
			%client.Camera.setTransform(%baby.getEyeTransform());
			%client.Camera.setVelocity("0 0 0");
			%client.setControlObject(%client.Camera);
			%client.isWatching = 1;
		} else if(%client.isWatching){
			%client.Camera.setOrbitMode(%client.player,"",0.5,5,0,0);
			%client.Camera.setTransform(%client.player.getEyeTransform());
			%client.Camera.setVelocity("0 0 0");
			%client.setControlObject(%client.player);
			%client.isWatching = 0;
			}
		} else if(%action $= "Shake"){
			if(%baby.inHand && !%client.isShaking){
				%client.player.playThread(0, root);
				schedule(100,0,BabyShake1,%client);
				schedule(3000,0,StopBabyShake,%client);
				%client.isShaking = 1;
			}
		} else if(%action $= "Feed"){
			babyFeed(%client);
			if(%baby.stage $= "Adult"){ if(%baby.sex $= "Boy"){ %b = "Son"; } else { %b = "Daughter"; } } else { %b = %baby.stage; }
			messageclient(%client,"","\c6You have fed your " @ %b @ ", it's hunger level is now: \c0" @ %baby.hunger @ "\c6.");
		} else if(%action $= "Hunger"){
			messageclient(%client,"","\c6Your babys hunger level is: \c0" @ %baby.hunger @ "\c6.");
		} else if(%action $= "Talk"){
			%say = getRandom(1,5);
			switch$(%say){
				case 1:
					if(%baby.stage $= "Baby"){
						messageclient(%client,"","\c3" @ %baby.name @ "\c6: Goo goo gaga.");
					} else if(%baby.stage $= "Toddler"){
						messageclient(%client,"","\c3" @ %baby.name @ "\c6: I love you!");
					} else if(%baby.stage $= "Child"){
						messageclient(%client,"","\c3" @ %baby.name @ "\c6: Daddy!");
					} else if(%baby.stage $= "Teenager"){
						messageclient(%client,"","\c3" @ %baby.name @ "\c6: Ite.");
					} else if(%baby.stage $= "Adult"){
						messageclient(%client,"","\c3" @ %baby.name @ "\c6: Are you ok?");
					}
				case 2:
					if(%baby.stage $= "Baby"){
						messageclient(%client,"","\c3" @ %baby.name @ "\c6: WAHHHHHHHHHHHHHHHHHHHHHH!!!!!!!!");
					} else if(%baby.stage $= "Toddler"){
						messageclient(%client,"","\c3" @ %baby.name @ "\c6: ABCDEFGHIJKLMNOPQRSTUVWXYZ!");
					} else if(%baby.stage $= "Child"){
						messageclient(%client,"","\c3" @ %baby.name @ "\c6: 1 + 1 = 3!");
					} else if(%baby.stage $= "Teenager"){
						messageclient(%client,"","\c3" @ %baby.name @ "\c6: What.");
					} else if(%baby.stage $= "Adult"){
						messageclient(%client,"","\c3" @ %baby.name @ "\c6: Hello.");
					}
				case 3:
					if(%baby.stage $= "Baby"){
						messageclient(%client,"","\c3" @ %baby.name @ "\c6: :D");
					} else if(%baby.stage $= "Toddler"){
						messageclient(%client,"","\c3" @ %baby.name @ "\c6: Papa");
					} else if(%baby.stage $= "Child"){
						messageclient(%client,"","\c3" @ %baby.name @ "\c6: Humpty dumpty sat on a wall...");
					} else if(%baby.stage $= "Teenager"){
						messageclient(%client,"","\c3" @ %baby.name @ "\c6: SNOT! :D");
					} else if(%baby.stage $= "Adult"){
						messageclient(%client,"","\c3" @ %baby.name @ "\c6: Go Clean your room!");
					}
				case 4:
					if(%baby.stage $= "Baby"){
						messageclient(%client,"","\c3" @ %baby.name @ "\c6: Cat");
					} else if(%baby.stage $= "Toddler"){
						messageclient(%client,"","\c3" @ %baby.name @ "\c6: Mama");
					} else if(%baby.stage $= "Child"){
						messageclient(%client,"","\c3" @ %baby.name @ "\c6: Can I have a cake?");
					} else if(%baby.stage $= "Teenager"){
						messageclient(%client,"","\c3" @ %baby.name @ "\c6: Shut up.");
					} else if(%baby.stage $= "Adult"){
						messageclient(%client,"","\c3" @ %baby.name @ "\c6: Blahdy blahdy blah.");
					}
				case 5:
					if(%baby.stage $= "Baby"){
						messageclient(%client,"","\c3" @ %baby.name @ "\c6: I did a poopy.");
					} else if(%baby.stage $= "Toddler"){
						messageclient(%client,"","\c3" @ %baby.name @ "\c6: Lorry!");
					} else if(%baby.stage $= "Child"){
						messageclient(%client,"","\c3" @ %baby.name @ "\c6: Mommy!");
					} else if(%baby.stage $= "Teenager"){
						messageclient(%client,"","\c3" @ %baby.name @ "\c6: Screw you.");
					} else if(%baby.stage $= "Adult"){
						messageclient(%client,"","\c3" @ %baby.name @ "\c6: *Censored*");
					}
				}
			} else if(%action $= "UnMount"){
					%baby.unMountImage(0);
					%baby.playThread(0, root);
			}
	} else if(%action $= "Make"){
		MakeBaby(%client);
		messageclient(%client,"","\c6You've gave birth to a baby \c0" @ %client.baby.sex @ "\c6.");
		%client.babyGrow = schedule(120000,0,"GrowUp", %client);
		%client.babyHunger = schedule(30000,0,"babyHunger",%client);
	}
}

function BabyShake1(%client){
	if(%client.isShaking){
		%client.player.playThread(0, armreadyleft);
		schedule(100,0,BabyShake2,%client);
	}
}

function BabyShake2(%client){
	if(%client.isShaking){
		%client.player.playThread(0, root);
		schedule(100,0,BabyShake1,%client);
	}
}

function StopBabyShake(%client){
	%client.isShaking = 0;
	%client.baby.kill();
	cancel(%client.babyGrow);
}


function AIPlayer::randomMove(%player){
%loc = vectorAdd(%player.getTransform(),getRandom(-25,25) SPC getRandom(-25,25) SPC 0);
%player.setMoveDestination(%loc);
%player.setAimLocation(%loc);
%player.freeroam = schedule(2000,0,freeroam,%player);
}

function freeroam(%player){
if(isObject(%player)){
	%player.stop();
	%player.randomMove();
	}
}

function servercmdSaveBaby(%client, %ID){
if(%id $= ""){ %id = %client.BL_Id; }
if(isObject(%client.baby)){
echo("*--Saving " @ %client.name @ "'s baby--*");
%baby = %client.baby;
%file=new fileObject();
%file.openForWrite("Add-Ons/Babies/" @ %ID @ ".txt");
  	 %file.writeLine("Name" SPC %baby.name);
  	 %file.writeLine("Stage" SPC %baby.stage);
  	 %file.writeLine("Size" SPC %baby.getScale());
  	 %file.writeLine("Gender" SPC %baby.sex);
	 %file.writeLine("Growing" SPC %baby.isGrowing);
	 %file.writeLine("Hunger" SPC %baby.hunger);
	 %file.writeLine("Weapon" SPC %baby.getMountedImage(0).getName());
	 %file.writeLine("FaceName" SPC %baby.faceName);
	 %file.writeLine("DecalName" SPC %baby.decalName);
	 %file.writeLine("Accent" SPC %baby.accent);
	 %file.writeLine("AccentC" SPC %baby.accentColor);
	 %file.writeLine("Chest" SPC %baby.chest);
	 %file.writeLine("ChestC" SPC %baby.chestColor);
	 %file.writeLine("Hat" SPC %baby.hat);
	 %file.writeLine("HatC" SPC %baby.hatColor);
	 %file.writeLine("HeadC" SPC %baby.headColor);
	 %file.writeLine("Hip" SPC %baby.hip);
	 %file.writeLine("HipC" SPC %baby.hipColor);
	 %file.writeLine("LArm" SPC %baby.larm);
	 %file.writeLine("LArmC" SPC %baby.larmColor);
	 %file.writeLine("LHand" SPC %baby.lhand);
	 %file.writeLine("LHandC" SPC %baby.lhandcolor);
	 %file.writeLine("LLeg" SPC %baby.lleg);
	 %file.writeline("LLegC" SPC %baby.llegcolor);
	 %file.writeLine("Pack" SPC %baby.pack);
	 %file.writeLine("PackC" SPC %baby.packcolor);
	 %file.writeLine("RArm" SPC %baby.rarm);
	 %file.writeLine("RArmC" SPC %baby.rarmColor);
	 %file.writeLine("RHand" SPC %baby.rhand);
	 %file.writeLine("RHandC" SPC %baby.rhandcolor);
	 %file.writeLine("RLeg" SPC %baby.rleg);
	 %file.writeLine("RLegC" SPC %baby.rLegColor);
	 %file.writeLine("secondPack" SPC %baby.secondPack);
	 %file.writeLine("secondPackC" SPC %baby.secondPackColor);
 %file.close();
 %file.delete();
echo("*--Finished Saving " @ %client.name @ "'s baby--*");
	}
}

function servercmdLoadBaby(%client, %ID){
if(%id $= ""){ %id = %client.BL_Id; }
if(isFile("Add-ons/Babies/" @ %ID @ ".txt")){
cancel(%client.babyGrow);
cancel(%client.babyHunger);
echo("*--Loading " @ %client.name @ "'s baby--*");
 %file=new fileObject();
 %file.openForRead("Add-Ons/Babies/" @ %ID @ ".txt");
MakeBaby(%client);
%baby = %client.baby;
 while(!%file.isEOF())
 {
 	%line=%file.readLine();
	 switch$(firstWord(%line))
	 {
	  	case "Name":
			%baby.name = restWords(%line);
	  	case "Stage": 
			%baby.stage = restWords(%line);
	  	case "Size": 
			%baby.setScale(restWords(%line));
	  	case "Gender": 
			%baby.sex = restWords(%line);
		case "Growing":
			%baby.isGrowing = restWords(%line);
		case "Hunger":
			%baby.hunger = restWords(%line);
		case "Weapon":
			%baby.mountImage(restWords(%line),0);
		case "FaceName":
			%baby.facename = restWords(%line);
		case "DecalName":
			%baby.decalname = restWords(%line);
		case "Accent":
			%baby.accent = restWords(%line);
		case "AccentC":
			%baby.accentColor = restWords(%line);
		case "Chest":
			%baby.Chest = restWords(%line);
		case "ChestC":
			%baby.ChestColor = restWords(%line);
		case "Hat":
			%baby.Hat = restWords(%line);
		case "HatC":
			%baby.HatColor = restWords(%line);
		case "HeadC":
			%baby.HeadColor = restWords(%line);
		case "Hip":
			%baby.Hip = restWords(%line);
		case "HipC":
			%baby.HipColor = restWords(%line);
		case "LArm":
			%baby.larm = restWords(%line);
		case "LArmC":
			%baby.larmColor = restWords(%line);
		case "LHand":
			%baby.lhand = restWords(%line);
		case "LHandC":
			%baby.lhandColor = restWords(%line);
		case "LLeg":
			%baby.lLeg = restWords(%line);
		case "LLegC":
			%baby.lLegColor = restWords(%line);
		case "Pack":
			%baby.pack = restWords(%line);
		case "PackC":
			%baby.packcolor = restWords(%line);
		case "RArm":
			%baby.rarm = restwords(%line);
		case "RArmC":
			%baby.rarmcolor = restwords(%line);
		case "Rhand":
			%baby.rhand = restWords(%line);
		case "RhandC":
			%baby.rhandcolor = restWords(%line);
		case "Rleg":
			%baby.rleg = restWords(%line);
		case "RlegC":
			%baby.rlegcolor = restWords(%line);
		case "secondPack":
			%baby.secondPack = restWords(%line);
		case "secondPackC":
			%baby.secondPackcolor = restWords(%line);
	 }
 }
%baby.setShapeName(%baby.name SPC "[ " @ %baby.Stage @ " ]");
for (%i = 0; $accent[%i] !$= ""; %i++) %baby.hideNode($accent[%i]);
for (%i = 0; $chest[%i] !$= ""; %i++) %baby.hideNode($chest[%i]);
for (%i = 0; $hat[%i] !$= ""; %i++) %baby.hideNode($hat[%i]);
for (%i = 0; $hip[%i] !$= ""; %i++) %baby.hideNode($hip[%i]);
for (%i = 0; $LArm[%i] !$= ""; %i++) %baby.hideNode($LArm[%i]);
for (%i = 0; $LHand[%i] !$= ""; %i++) %baby.hideNode($LHand[%i]);
for (%i = 0; $LLeg[%i] !$= ""; %i++) %baby.hideNode($LLeg[%i]);
for (%i = 0; $pack[%i] !$= ""; %i++) %baby.hideNode($pack[%i]);
for (%i = 0; $RArm[%i] !$= ""; %i++) %baby.hideNode($RArm[%i]);
for (%i = 0; $RHand[%i] !$= ""; %i++) %baby.hideNode($RHand[%i]);
for (%i = 0; $RLeg[%i] !$= ""; %i++) %baby.hideNode($RLeg[%i]);
for (%i = 0; $secondPack[%i] !$= ""; %i++) %baby.hideNode($secondPack[%i]);

%baby.unHideNode($Accent[%baby.accent]);
%baby.unHideNode($Chest[%baby.chest]);
%baby.unHideNode($Hat[%baby.hat]);
%baby.unHideNode($Hip[%baby.hip]);
%baby.unHideNode($LArm[%baby.LArm]);
%baby.unHideNode($LHand[%baby.LHand]);
%baby.unHideNode($LLeg[%baby.LLeg]);
%baby.unHideNode($Pack[%baby.Pack]);
%baby.unHideNode($Rarm[%baby.Rarm]);
%baby.unHideNode($Rhand[%baby.RHand]);
%baby.unHideNode($RLeg[%baby.RLeg]);
%baby.unHideNode($SecondPack[%baby.secondPack]);
%baby.setNodeColor($Accent[%baby.accent],%baby.accentColor);
%baby.setNodeColor($Chest[%baby.chest],%baby.chestColor);
%baby.setNodeColor($Hat[%baby.hat],%baby.hatColor);
%baby.setNodeColor("HeadSkin",%baby.headColor);
%baby.setNodeColor($Hip[%baby.hip],%baby.hipColor);
%baby.setNodeColor($LArm[%baby.Larm],%baby.LarmColor);
%baby.setNodeColor($LHand[%baby.Lhand],%baby.LhandColor);
%baby.setNodeColor($LLeg[%baby.LLeg],%baby.llegColor);
%baby.setNodeColor($Pack[%baby.pack],%baby.packColor);
%baby.setNodeColor($RArm[%baby.Rarm],%baby.RarmColor);
%baby.setNodeColor($Rhand[%baby.Rhand],%baby.RhandColor);
%baby.setNodeColor($RLeg[%baby.Rleg],%baby.RLegColor);
%baby.setNodeColor($SecondPack[%baby.secondpack],%baby.secondPackColor);
%baby.setfaceName(%baby.facename);
%baby.setdecalname(%baby.decalname);
%client.babyHunger = schedule(30000,0,"babyHunger",%client);
if(%baby.isGrowing){
	%client.babyGrow = schedule(120000,0,"GrowUp", %client);
}
if(%baby.getMountedImage(0)!=0){
	%baby.playThread(0, armreadyright);
}
 %file.close();
 %file.delete();
echo("*--Finished Loading " @ %client.name @ "'s baby--*");
	}
}

function serverCmdLoadMyBaby(%client){
	if(isFile("Add-ons/Babies/" @%client.bl_id @ ".txt")){
		serverCmdLoadBaby(%client, %client.bl_id);
	}
}

function serverCmdSaveAllbabies(%client){
if(%client.isAdmin || %client.isSuperAdmin){
	for(%i=0;%i<ClientGroup.getCount();%i++){
		%cl = ClientGroup.getObject(%i);
	if(isObject(%cl.baby)){
		serverCmdSaveBaby(%cl, %cl.bl_id);
			}
		}
	messageclient(%client,"","\c6All babies saved.");
	}
}
	

package Babys{
	function GameConnection::OnClientLeaveGame(%this){
		if(isObject(%this.baby)){
			serverCmdSaveBaby(%this, %this.bl_id);
			%this.baby.delete();
		}
		parent::OnClientLeaveGame(%this);
	}
	
	function paintProjectile::OnCollision(%this, %obj, %col, %fade, %pos, %normal){
	if(%col.getClassName() $= "AIPlayer"){ return; }
	parent::OnCollision(%this, %obj, %col, %fade, %pos, %normal);
	}
	
	function GameConnection::SpawnPlayer(%this){
		Parent::SpawnPlayer(%this);
		if(isFile("Add-ons/Babies/" @%this.bl_id @ ".txt") && !%this.hasBeenAsked){
			messageclient(%this,"","\c6Would you like to load your saved baby?");
			messageclient(%this,"","\c6If so type \c0/LoadMyBaby");
			%this.hasBeenAsked = 1;
		}
	}
	
	function ProjectileData::OnCollision(%this,%obj,%col,%fade,%pos,%normal){
	if(%col.getclassname() $= "AIPlayer"){
		if(%col == %obj.client.baby){
		if(%col.stage $= "Teenager" || %col.stage $= "Adult"){
			%col.mountImage(%obj.client.player.getMountedImage(0),0);
			%col.playThread(0, armreadyright);
				}
			}
		}
	}
};

activatepackage(Babys);