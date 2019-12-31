function servercmdgetallitems(%c)
{
	for(%a=0;%a<=$PoI;%a++)
		{
			SetItemCount(%c,%a,10000);
		}
}