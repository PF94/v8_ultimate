if(!isobject(TCList)){
exec("./Trading Cards/TCList.gui");
}

if(!isobject(TCTrading)){
exec("./Trading Cards/TCTrading.gui");
}

if(!isobject(TCGive)){
exec("./Trading Cards/TCGive.gui");
}

if (!$addedTCMaps)
{
	$remapDivision[$remapCount] = "Trading Cards";
	$remapName[$remapCount] = "Toggle TC Window";
	$remapCmd[$remapCount] = "toggleTCWindow";
	$remapCount++;
	$addedTCMaps = true;
}

function toggleTCwindow(%val)
{
if(%val){
		if(TCList.isAwake()){
			canvas.popdialog(TCList);
		}else{
			canvas.pushdialog(TCList);
			}
			}
}

//BETA: Delete card cmd.
function ProccessCardDel()
{
//ask if your SURE.
messageboxyesno("Are you sure?","Do you want to delete this card?","ProccessCardDel2();","");
}

function ProccessCardDel2()
{

//Get the card ID then remove it
%id = Cardtxt.getvalue();
%id = GetCardID(%id);
DeleteMyCard(%id);
}

function DeleteMyCard(%cardid)
{
%file = new FileObject();
%file.openforread("add-ons/client/Trading Cards/YourCards.txt");
%file2 = new FileObject();
%file2.openforwrite("add-ons/client/Trading Cards/YourCards.txt");
while(!%file.isEOF()){
%line = %file.readline();
if(%line == %cardid && %ok != 1){
%ok = 1;
echo("Card found");
}else{
%file2.writeline(%line);
}
}
%file.close();
%file2.close();
%file.delete();
%file2.delete();
if(%ok != 1){
messageboxok("Error","You don't have that card.");
}else{
//add success message here
DeletePic.setvisible(1);
DeleteMsg.setvisible(1);
schedule(2000,0,"FinDel");
%this = CardList;
%this.clear();
%cardlist = "";
%file = new FileObject();
%file.openforread("add-ons/client/trading cards/YourCards.txt");
while(!%file.isEOF()){
%line = %file.readline();
if(%cardlist $= ""){
%cardlist = %line;
}else{
%cardlist = %cardlist SPC %line;
}
}
for(%i = 0; %i < getwordcount(%cardlist); %i++){
%curname = getword(%cardlist,%i);
%ccname = GetCardName(%curname);
if(%ccname !$= ""){
%this.addRow(%i, %ccname, %i);
}
}
%this.sort(0,1);
}
}

function FinDel()
{
DeletePic.setvisible(0);
DeleteMsg.setvisible(0);
}


//Trading GUI stuff.

function TradeCardList::onWake(%this)
{
%this.clear();
%cardlist = "";
%file = new FileObject();
%file.openforread("add-ons/client/trading cards/YourCards.txt");
while(!%file.isEOF()){
%line = %file.readline();
if(%cardlist $= ""){
%cardlist = %line;
}else{
%cardlist = %cardlist SPC %line;
}
}
for(%i = 0; %i < getwordcount(%cardlist); %i++){
%curname = getword(%cardlist,%i);
%ccname = GetCardName(%curname);
if(%ccname !$= ""){
%this.addRow(%i, %ccname, %i);
}
}
%this.sort(0,1);
}

function TradeCardList::onSelect(%this, %id, %text)
{
%cardid = GetCardID(%text);
myCard.setbitmap("add-ons/client/Trading Cards/" @ %cardid @ "");
$CurCardID = %cardid;
}

function SelectTradeCard()
{
if(!$CurCardID){
messageboxok("Error","Please select a card.");
return;
}
commandtoserver('TradeCardSelect',$CurCardID);
SelectTradeCardButton.setvisible(0);
}

function ClientCmdTradeCardRecieved(%cardid)
{
TraderCard.setbitmap("add-ons/client/Trading Cards/" @ %cardid @ "");
TraderCardName.setvalue(GetCardName(%cardid));
}

function ClientCmdConfirmTrade()
{
messageboxyesno("Do you accept?","Do you accept this trade?","commandtoserver(\'FinishTrade\');","commandtoserver(\'DeclineTrade\');");
$CurCardID = "";
}

function ClientCmdRequestTrade(%with)
{
messageboxyesno("Trade","Would you like to trade with " @ %with @ "?","commandtoserver(\'accepttrade\');","commandtoserver(\'refusetrade\');");
}

function ClientCmdStartTrade(%name)
{
canvas.pushdialog(TCTrading);
TraderName.setvalue(%name);
}

function ClientCmdCloseTradingGUI()
{
canvas.popdialog(TCTrading);
schedule(100,0,"ResetTradeGUI");
}

function ResetTradeGUI()
{
TCTrading.delete();
exec("./Trading Cards/TCTrading.gui");
}




//Card Gifting GUI stuff.

function GiftCard()
{
commandtoserver('GiveCardNow',$CurCardID);
}

function ClientCmdCloseGiftingGUI()
{
canvas.popdialog(TCGive);
}

function ClientCmdOpenGiftGUI()
{
canvas.pushdialog(TCGive);
}

function CardGiveList::onWake(%this)
{
%this.clear();
%cardlist = "";
%file = new FileObject();
%file.openforread("add-ons/client/trading cards/YourCards.txt");
while(!%file.isEOF()){
%line = %file.readline();
if(%cardlist $= ""){
%cardlist = %line;
}else{
%cardlist = %cardlist SPC %line;
}
}
for(%i = 0; %i < getwordcount(%cardlist); %i++){
%curname = getword(%cardlist,%i);
%ccname = GetCardName(%curname);
if(%ccname !$= ""){
%this.addRow(%i, %ccname, %i);
}
}
%this.sort(0,1);
}

function CardGiveList::onSelect(%this, %id, %text)
{
%cardid = GetCardID(%text);
CardGivePic.setbitmap("add-ons/client/Trading Cards/" @ %cardid @ "");
$CurCardID = %cardid;
}




//GUI Stuffs.

function CardList::onWake(%this)
{
%this.clear();
%cardlist = "";
%file = new FileObject();
%file.openforread("add-ons/client/trading cards/YourCards.txt");
while(!%file.isEOF()){
%line = %file.readline();
if(%cardlist $= ""){
%cardlist = %line;
}else{
%cardlist = %cardlist SPC %line;
}
}
for(%i = 0; %i < getwordcount(%cardlist); %i++){
%curname = getword(%cardlist,%i);
%ccname = GetCardName(%curname);
if(%ccname !$= ""){
%this.addRow(%i, %ccname, %i);
}
}
%this.sort(0,1);
SetCardCount();
}

function CardList::onSelect(%this, %id, %text)
{
%cardid = GetCardID(%text);
	if($FlipCards){
	CardPic.setbitmap("add-ons/client/Trading Cards/" @ %cardid @ "a");
	}else{
	CardPic.setbitmap("add-ons/client/Trading Cards/" @ %cardid @ "");
	}
CardTxt.setvalue(%text);
}

function clientCmdGiveCard(%cardid)
{
//Give the client a new card
%file = new FileObject();
%file.openforappend("add-ons/client/Trading Cards/YourCards.txt");
%newline = %cardid;
%file.writeline(%newline);
%file.close();
%file.delete();
%cardname = GetCardName(%cardid);
clientCmdCenterPrint("<color:00FF00> You got a " @ %cardname @ " card!",4);
echo("Got card");
}

function clientcmdRemoveCard(%cardid)
{
%file = new FileObject();
%file.openforread("add-ons/client/Trading Cards/YourCards.txt");
%file2 = new FileObject();
%file2.openforwrite("add-ons/client/Trading Cards/YourCards.txt");
while(!%file.isEOF()){
%line = %file.readline();
if(%line == %cardid && %ok != 1){
echo("Removed Card " @ %cardid);
%ok = 1;
}else{
%file2.writeline(%line);
}
}
%file.close();
%file2.close();
%file.delete();
%file2.delete();
}

//The naming from IDs...
function GetCardName(%id)
{
if(%id == 158){
%name = "Aloshi";
}

if(%id == -158){
%name = "Aloshi V1";
}

if(%id == 144){
%name = "Oneill";
}

if(%id == 204){
%name = "Itachi";
}

if(%id == 364){
%name = "Boshi";
}

if(%id == 1148){
%name = "Sagez";
}

if(%id == 708){
%name = "Bushido";
}

if(%id == -708){
%name = "Bushido V1";
}

if(%id == 150){
%name = "Ephilates";
}

if(%id == -1){
%name = "DD Kart";
}

if(%id == 1161){
%name = "Naes Draw";
}

if(%id == 182){
%name = "Sheezy";
}

if(%id == 331){
%name = "adc90";
}

if(%id == 112){
%name = "KITTY";
}

if(%id == 137){
%name = "Kaphix";
}

if(%id == 1422){
%name = "DanSwanson";
}

if(%id $= "BS"){
%name = "Badspot";
}

if(%id == 1324){
%name = "Minion";
}

if(%id == 294){
%name = "Tails";
}

if(%id == -2){
%name = "Explosion A";
}

if(%id == 1804){
%name = "E. B. Slowpoke";
}

if(%id == 1018){
%name = "Lord Jake";
}

if(%id == 1120){
%name = "N0cturni";
}

if(%id == 1009){
%name = "Kyle Brickalo";
}

if(%id == 387){
%name = "TheGeek";
}

if(%id == 186){
%name = "General Jake";
}

if(%id == 713){
%name = "The Legend";
}

if(%id == 481){
%name = "Marcem";
}

if(%id == 724){
%name = "Sky Builder";
}

if(%id == 499){
%name = "Grimreaper";
}

if(%id == 759){
%name = "Zackin5";
}

if(%id == 909){
%name = "SpaceDude";
}


if(%id == 1927){
%name = "Edmond";
}

if(%id == 420){
%name = "Shadow Ninja";
}

if(%id == 205){
%name = "Trader";
}

if(%id == 348){
%name = "Chexguy331";
}

if(%id == 590){
%name = "Masterlegodude";
}

if(%id == 320){
%name = "Mr. Smash";
}

//BATCH 3 BEGIN
if(%id == 171){
%name = "Ronin";
}

if(%id == 119){
%name = "Jookia";
}

if(%id == 146){
%name = "Devastator";
}

if(%id == 183){
%name = "Scobby";
}

if(%id == 188){
%name = "StoreClerk";
}

if(%id == 190){
%name = "Zor";
}

if(%id == 201){
%name = "HKL";
}

if(%id == 232){
%name = "Ladios";
}

if(%id == 235){
%name = "Law";
}

if(%id == 293){
%name = "Dr. Block";
}

if(%id == 381){
%name = "Jam";
}

if(%id == 388){
%name = "Bum";
}

if(%id == 391){
%name = "Minomato[TSM]";
}

if(%id == 419){
%name = "CNCwarlord";
}

if(%id == 511){
%name = "Gikon";
}

if(%id == 547){
%name = "fishpen0";
}

if(%id == 639){
%name = "Nasa";
}

if(%id == 665){
%name = "Dan";
}

if(%id == 1139){
%name = "Ace";
}

if(%id == 1158){
%name = "Mutanz";
}

if(%id == 1336){
%name = "TheWorm";
}

if(%id == 1511){
%name = "Starwars";
}

if(%id == 1544){
%name = "OFC Videos";
}

if(%id == 1664){
%name = "Xang";
}

if(%id == 1866){
%name = "C.M. Django";
}

if(%id == 2154){
%name = "Wedge";
}

//WEDGE'S CARDS
if(%id $= "1009b"){
%name = "Kyle Brickalo-B";
}

if(%id $= "1010b"){
%name = "Tony Swanson-B";
}

if(%id $= "-3"){
%name = "Thin Ice";
}

if(%id $= "-4"){
%name = "NOD Minigunner";
}

if(%id $= "1393b"){
%name = "plastic0-B";
}

if(%id $= "1120b"){
%name = "n0cturni-B";
}

if(%id $= "1158b"){
%name = "Mutantz-B";
}

if(%id $= "590b"){
%name = "Masterlegodude-B";
}

if(%id $= "232b"){
%name = "Ladios-B";
}

if(%id $= "364b"){
%name = "Boshi-B";
}

if(%id $= "647b"){
%name = "Azerath-B";
}

if(%id $= "195b"){
%name = "Awoken-B";
}

//Next up is Batch 4!
if(%id == 1133){
%name = "Kirbylikesit";
}

if(%id == 1335){
%name = "TicTac";
}

if(%id == 106){
%name = "Pimpin";
}

if(%id == 1441){
%name = "Metian";
}

if(%id == 1500){
%name = "King";
}

if(%id == 167){
%name = "Skele";
}

if(%id == 1677){
%name = "Antallion";
}

if(%id == 1748){
%name = "XD!";
}

if(%id == 1790){
%name = "CheezeWiz";
}

if(%id == 262){
%name = "Black Panther";
}

if(%id == 440){
%name = "Spenny";
}

if(%id == 483){
%name = "Itake";
}

if(%id == 691){
%name = "Kungfu-Ninja";
}

if(%id == 863){
%name = "Mario Fan 11";
}

if(%id == 457){
%name = "Larry";
}

if(%id == 522){
%name = "Eidolon";
}

if(%id == 846){
%name = "Block_Dude";
}

if(%id == 958){
%name = "spartan";
}

if(%id == 277){
%name = "Loz";
}

if(%id == 121){
%name = "Tape";
}

if(%id == 135){
%name = "Packer";
}

if(%id == 228){
%name = "MegaScience";
}


//End batch 4

if(%name $= "" && %id !$= ""){
%name = "Unknown";
echo(%id @ " is unknown!");
}
return %name;
}

function GetCardID(%id)
{
if(%id $= "Aloshi"){
%name = "158";
}

if(%id $= "Aloshi V1"){
%name = "-158";
}

if(%id $= "Bushido"){
%name = "708";
}

if(%id $= "Bushido V1"){
%name = "-708";
}

if(%id $= "Ephilates"){
%name = "150";
}

if(%id $= "DD Kart"){
%name = "-1";
}

if(%id $= "Naes Draw"){
%name = "1161";
}

if(%id $= "Sheezy"){
%name = "182";
}

if(%id $= "adc90"){
%name = "331";
}

if(%id $= "KITTY"){
%name = "112";
}

if(%id $= "Kaphix"){
%name = "137";
}

if(%id $= "DanSwanson"){
%name = "1422";
}

if(%id $= "Oneill"){
%name = "144";
}

if(%id $= "Itachi"){
%name = 204;
}

if(%id $= "Boshi"){
%name = 364;
}

if(%id $= "Sagez"){
%name = 1148;
}

if(%id $= "Badspot"){
%name = "BS";
}

if(%id $= "Minion"){
%name = "1324";
}

if(%id $= "Tails"){
%name = "294";
}

if(%id $= "Explosion A"){
%name = "-2";
}

if(%id $= "E. B. Slowpoke"){
%name = "1804";
}

if(%id $= "Lord Jake"){
%name = "1018";
}

if(%id $= "N0cturni"){
%name = "1120";
}

if(%id $= "Kyle Brickalo"){
%name = "1009";
}

if(%id $= "TheGeek"){
%name = "387";
}

if(%id $= "General Jake"){
%name = "186";
}

if(%id $= "The Legend"){
%name = "713";
}

if(%id $= "Marcem"){
%name = "481";
}

if(%id $= "Sky Builder"){
%name = "724";
}

if(%id $= "Grimreaper"){
%name = "499";
}

if(%id $= "Zackin5"){
%name = "759";
}

if(%id $= "SpaceDude"){
%name = "909";
}

if(%id $= "Edmond"){
%name = "1927";
}

if(%id $= "Shadow Ninja"){
%name = "420";
}

if(%id $= "Trader"){
%name = "205";
}

if(%id $= "Chexguy331"){
%name = "348";
}

if(%id $= "Masterlegodude"){
%name = "590";
}

if(%id $= "Mr. Smash"){
%name = "320";
}

//BATCH 3 BEGIN
if(%id $= "Ronin"){
%name = 171;
}

if(%id $= "Jookia"){
%name = 119;
}

if(%id $= "Devastator"){
%name = 146;
}

if(%id $= "Scobby"){
%name = 183;
}

if(%id $= "StoreClerk"){
%name = 188;
}

if(%id $= "Zor"){
%name = 190;
}

if(%id $= "HKL"){
%name = 201;
}

if(%id $= "Ladios"){
%name = 232;
}

if(%id $= "Law"){
%name = 235;
}

if(%id $= "Dr. Block"){
%name = 293;
}

if(%id $= "Jam"){
%name = 381;
}

if(%id $= "Minomato[TSM]"){
%name = 391;
}

if(%id $= "CNCwarlord"){
%name = 419;
}

if(%id $= "Gikon"){
%name = 511;
}

if(%id $= "fishpen0"){
%name = 547;
}

if(%id $= "Nasa"){
%name = 639;
}

if(%id $= "Dan"){
%name = 665;
}

if(%id $= "Ace"){
%name = 1139;
}

if(%id $= "Mutanz"){
%name = 1158;
}

if(%id $= "TheWorm"){
%name = 1336;
}

if(%id $= "Starwars"){
%name = 1511;
}

if(%id $= "OFC Videos"){
%name = 1544;
}

if(%id $= "Xang"){
%name = 1664;
}

if(%id $= "C.M. Django"){
%name = 1866;
}

if(%id $= "Wedge"){
%name = 2154;
}

if(%id $= "Bum"){
%name = 388;
}
//Wedgecards
if(%id $= "Kyle Brickalo-B"){
%name = "1009b";
}

if(%id $= "Tony Swanson-B"){
%name = "1010b";
}

if(%id $= "Thin Ice"){
%name = "-3";
}

if(%id $= "NOD Minigunner"){
%name = "-4";
}

if(%id $= "plastic0-B"){
%name = "1393b";
}

if(%id $= "n0cturni-B"){
%name = "1120b";
}

if(%id $= "Mutantz-B"){
%name = "1158b";
}

if(%id $= "Masterlegodude-B"){
%name = "590b";
}

if(%id $= "Ladios-B"){
%name = "232b";
}

if(%id $= "Boshi-B"){
%name = "364b";
}

if(%id $= "Azerath-B"){
%name = "647b";
}

if(%id $= "Awoken-B"){
%name = "195b";
}
//batch 4
if(%id $= "Kirbylikesit"){
%name = 1133;
}

if(%id $= "TicTac"){
%name = 1335;
}

if(%id $= "Pimpin"){
%name = 106;
}

if(%id $= "Metian"){
%name = 1441;
}

if(%id $= "King"){
%name = 1500;
}

if(%id $= "Skele"){
%name = 167;
}

if(%id $= "Antallion"){
%name = 1677;
}

if(%id $= "XD!"){
%name = 1748;
}

if(%id $= "CheezeWiz"){
%name = 1790;
}

if(%id $= "Black Panther"){
%name = 262;
}

if(%id $= "Spenny"){
%name = 440;
}

if(%id $= "Itake"){
%name = 483;
}

if(%id $= "Kungfu-Ninja"){
%name = 691;
}

if(%id $= "Mario Fan 11"){
%name = 863;
}

if(%id $= "Larry"){
%name = 457;
}

if(%id $= "Eidolon"){
%name = 522;
}

if(%id $= "Block_Dude"){
%name = 846;
}

if(%id $= "spartan"){
%name = 958;
}

if(%id $= "Loz"){
%name = 277;
}

if(%id $= "Tape"){
%name = 121;
}

if(%id $= "Packer"){
%name = 135;
}

if(%id $= "MegaScience"){
%name = 228;
}

if(%id $= "Chazpelo"){
%name = 47101;
}

if(%id $= "PF94"){
%name = 47101;
}

if(%name $= "" && %id !$= ""){
%name = "Unknown";
}
return %name;
}

function clientCmdCheckForTC()
{
commandtoserver('DoHaveTC');
}

//function ReexecTC()
//{
//$Execed = 1;
//exec("./Script_tradingcards.cs");
//}

//if($Execed != 1){
//schedule(5000,0,"ReexecTC");
//}

function FlipCard()
{
if($FlipCards == 1)
{
$FlipCards = 0;
CardPic.setbitmap("add-ons/client/Trading Cards/" @ GetCardID(Cardtxt.getvalue()) @ "");
}else{
$FlipCards = 1;
CardPic.setbitmap("add-ons/client/Trading Cards/" @ GetCardID(Cardtxt.getvalue()) @ "a");
}
}

if(!$pie){
$pie = 1;
error("Reexec'd");
schedule(5000,0,"exec","add-ons/client/Script_tradingcards.cs");
}

function SetCardCount()
{
%file = new FileObject();
%file.openforread("add-ons/client/trading cards/YourCards.txt");
%count = 0;
while(!%file.isEOF()){
%line = %file.readline();
if(%line !$= ""){
%count++;
}
}
%file.close();
%file.delete();
CardCount.setvalue("You have " @ %count @ " card(s).");
}
