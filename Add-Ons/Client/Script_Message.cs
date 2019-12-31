function AlignChatBox()
{
	resizeMessageHud();
	%Channel = NMH_Channel.getValue();
	%BoxPosition = "1" SPC (getWord(newChatText.position, 1 ) + getWord(newChatText.extent, 1 ) + 1);
	%TypeExtent = (getWord(NMH_Box.extent,0)-getWord(NMH_Type.position,1)) SPC getWord(NMH_Box.extent,1);
	%TypePosition = ((strLen(%Channel)-4)*NMH_Type.profile.fontsize-6) SPC getWord(NMH_Type.position,1);
	NMH_Box.position = %BoxPosition;
	NMH_Type.extent = %TypeExtent;
	NMH_Type.position = %TypePosition;
}

function AdminChat(%val)
{
	if(%val)
	{
		NMH_Channel.setValue("\c2Admin:");
		AlignChatBox();
		canvas.pushdialog(newMessageHud);
	}
}

function PmChat(%val)
{
	if(%val)
	{
		switch(InGameOptions.isAwake())
		{
			case 0:
				canvas.pushdialog(InGameOptions);
				InGameOptions.setPane("PM");
			case 1:
				canvas.popdialog(InGameOptions);
		}
	}
}

function Pm()
{
	%Pm = txtPmMessage.getValue();
	%pmClient = pmList.getSelectedID();
	if(%pmClient == -1 || %Pm $= "")
	{
		return;
	}
	commandtoserver('PmMessageSent',%pmClient,%Pm);
	txtPmMessage.setValue(%Blank);
}

function clientCmdAddMessage(%Sender,%Message)
{
	%mNumber = MessagesList.RowCount();
	%mLen = strLen(%Message);
	%Start = 0;
	%Extent = 30;
	%LoopNum = (mFloor(%mLen/%Extent)+1);
	MessagesList.addRow(%mNumber,%Sender@":");
	for(%i=0;%i<%LoopNum;%i++)
	{
		%Line = getSubStr(%Message,%Start,%Extent);
		MessagesList.addRow(%mNumber++,%Line);
		%Start+=%Extent;
	}
	if(MessagesList.getRowTextById(%mNumber) !$= "")
	{
			MessagesList.addRow(%mNumber++,%Blank);
	}
}

function SavePm()
{
	%File = new FileObject();
	%File.openForAppend("Add-Ons/Client/Saves/PmHistory.txt");
	%LoopNum = MessagesList.RowCount();
	%File.writeLine(getDateTime());
	for(%i=0;%i<%LoopNum;%i++)
	{
		%PmHistory[%i] = MessagesList.getRowTextById(%i);
		%File.writeLine(%PmHistory[%i]);
	}
	%File.close();
}

function newMessageHud::onWake(%This)
{
	AlignChatBox();
}

package MessageSend
{
	function NMH_Type::send(%this)
	{
		%Channel = NMH_Channel.getValue();
		%msg=%this.getValue();
		if(%Channel $= "\c2Admin:")
		{
			commandToServer('AdminMessageSent',%msg);
			commandToServer('stopTalking');
			canvas.popdialog(newMessageHud);
		}
		else
		{
			Parent::Send(%this);
		}
		NMH_Type.setValue(%blank);
	}
};
ActivatePackage(MessageSend);