if(!$FoodModRunning)
{
	$FoodModRunning = 1;
	exec("./FoodAdminGui.gui");

	$remapDivision[$remapCount] = "Food/hunger";
	$remapName[$remapCount] = "Food Admin Gui";
	$remapCmd[$remapCount] = "foodGui";
	$remapCount++;

}

function foodGui(%val)
{
	if(%val)
	{
		if(FoodAdminGui.isAwake())
			canvas.popDialog(FoodAdminGui);
		else
			canvas.pushDialog(FoodAdminGui);
	}
}

function makenewfood(%client)
{
	%name = txtnewFoodName.getValue();
	%cost = newFoodCost.getValue();
	%heal = newFoodHeal.getValue();
	commandtoserver('newfood',%name,%cost,%heal);
	schedule(100,0,"Updatelist");
}

function setdecaytime(%client)
{
	%time = decayBox.getValue();
	commandtoserver('Decaytime',%time);
}

function foodAdminGui::onWake(%this)
{
  Foodlist.clear();


  for(%i = 1; %i <= $foodcount; %i++)
     Foodlist.addRow(%i, $food[%i] TAB $foodcost[%i] TAB $foodheal[%i]);
}

function Updatelist(%this)
{
  FoodList.clear();

  for(%i = 1; %i <= $foodcount; %i++)
     Foodlist.addRow(%i, $food[%i] TAB $foodcost[%i] TAB $foodheal[%i]);
}

function killfood(%client)
{
	%id = Foodlist.getSelectedId();
	commandtoserver('delfood',%id);
	schedule(100,0,"Updatelist");
}