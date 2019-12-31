function OpenIAGui()
{
	commandtoserver('IAGuiRequest');
}

function clientcmdIARequestAccepted()
{
	canvas.pushdialog(ItemAdmin);
	IA_Refresh();
}

function clientcmdIARequestDenied()
{
	clientcmdMessageBoxOk("Denied","Access Denied");
}

function IA_Refresh()
{
	IAWnd_Itemlist.clear();
	commandtoserver('IA_Getallitems');
}

function clientcmdSetIAItems(%itemid,%name)
{
	IAWnd_Itemlist.addrow(%itemid,%itemid TAB %name);
}

function IAWnd_GrantItem()
{
	commandtoserver('grantitembyid',IAWnd_TN.getValue(),IAWnd_Itemlist.getselectedid(),IAWnd_A.getValue());
}

function IAWnd_Selectitem()
{

}