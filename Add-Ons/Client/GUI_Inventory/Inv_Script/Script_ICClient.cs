function OISC()
{
canvas.pushdialog(InventoryConsole);
}

function InvC_CBF()
{
commandtoserver('ISC_eval',InvC_CB.getvalue());
InvC_CB.setvalue("");
}

function clientcmdAddISConsoleLine(%text)
{
InvC_CL.addrow(0,%text);
InvC_Scroller.ScrollToBottom();
}