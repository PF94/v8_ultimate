//--- OBJECT WRITE BEGIN ---
new GuiControl(InventoryConsole) {
   profile = "GuiDefaultProfile";
   horizSizing = "right";
   vertSizing = "bottom";
   position = "0 0";
   extent = "640 480";
   minExtent = "8 2";
   visible = "1";

   new GuiWindowCtrl() {
      profile = "GuiWindowProfile";
      horizSizing = "right";
      vertSizing = "bottom";
      position = "92 45";
      extent = "300 396";
      minExtent = "8 2";
      visible = "1";
      text = "IS Console";
      maxLength = "255";
      resizeWidth = "1";
      resizeHeight = "1";
      canMove = "1";
      canClose = "1";
      canMinimize = "1";
      canMaximize = "1";
      minSize = "50 50";
      closeCommand = "canvas.popdialog(InventoryConsole);";

      new GuiTextEditCtrl(InvC_CB) {
         profile = "GuiTextEditProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "5 372";
         extent = "290 18";
         minExtent = "8 2";
         visible = "1";
         altCommand = "InvC_CBF();";
         maxLength = "255";
         historySize = "0";
         password = "0";
         tabComplete = "0";
         sinkAllKeyEvents = "0";
      };
      new GuiScrollCtrl(InvC_Scroller) {
         profile = "GuiScrollProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "5 30";
         extent = "285 340";
         minExtent = "8 2";
         visible = "1";
         willFirstRespond = "1";
         hScrollBar = "alwaysOff";
         vScrollBar = "alwaysOn";
         constantThumbHeight = "0";
         childMargin = "0 0";
         rowHeight = "40";
         columnWidth = "30";

         new GuiTextListCtrl(InvC_CL) {
            profile = "GuiTextListProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "1 -3405";
            extent = "268 3744";
            minExtent = "8 2";
            visible = "1";
            enumerate = "0";
            resizeCell = "1";
            columns = "0";
            fitParentWidth = "1";
            clipColumnText = "0";
         };
      };
   };
};
//--- OBJECT WRITE END ---
