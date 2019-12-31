exec("./ToolWandGUI.gui");

if (!$TWBindings)
{
	$remapDivision[$remapCount] = "Tool Wand";
	$remapName[$remapCount] = "Show GUI";
	$remapCmd[$remapCount] = "TW_Open";
	$remapCount++;
	$TWbindings=true;
}

$AccessColors[0]="0 255 0 255";
$AccessColors[1]="0 0 255 255";
$AccessColors[2]="255 99 00 255";
$AccessColors[3]="255 0 0 255";

$AccessLetters[0]="P";
$AccessLetters[1]="A";
$AccessLetters[2]="S";
$AccessLetters[3]="F";

function TW_Open()
{
	canvas.pushDialog(ToolWandGUI);
}

function ToolWandGUI::onwake(%this)
{
	TWList.clear();
	$TWL::Y=0;
	commandToServer('getTWList');
}

function clientcmdTWListAdd(%name,%access)
{
	%color=$AccessColors[%access];
	%letter=$AccessLetters[%access];
	%b=new GuiBitmapButtonCtrl()
	{
		profile = "BlockButtonProfile";
		horizSizing = "left";
		vertSizing = "bottom";
		position = "150 "@$TWL::Y*20;
		extent = "18 18";
		minExtent = "8 2";
		visible = "1";
		text = %letter;
		groupNum = "-1";
		buttonType = "PushButton";
		bitmap = "base/client/ui/button2";
		lockAspectRatio = "0";
		alignLeft = "0";
		command="TWButton("@$TWL::Y@");";
		overflowImage = "0";
		mKeepCached = "0";
		mColor = %color;
	};TWList.add(%b);

	%t=new GuiTextCtrl() {
		profile = "GuiTextProfile";
		horizSizing = "right";
		vertSizing = "bottom";
		position = "10 "@$TWL::Y*20;
		extent = "21 18";
		minExtent = "8 2";
		visible = "1";
		text = %name;
		maxLength = "255";
	};TWList.add(%t);
	TWList.resize(0,0,170,($TWL::Y+1)*20);
	$TWL::Y++;
}

function TWButton(%ID)
{
	commandToServer('setTW',%ID);
	TWList.clear();
	$TWL::Y=0;
	commandToServer('getTWList');
}