//--- OBJECT WRITE BEGIN ---
new GuiControl(ItemAdmin) {
   profile = "GuiDefaultProfile";
   horizSizing = "right";
   vertSizing = "bottom";
   position = "0 0";
   extent = "640 480";
   minExtent = "8 2";
   visible = "1";

   new GuiWindowCtrl(IAWnd) {
      profile = "GuiWindowProfile";
      horizSizing = "right";
      vertSizing = "bottom";
      position = "101 32";
      extent = "350 450";
      minExtent = "8 2";
      visible = "1";
      text = "Item Admin";
      maxLength = "255";
      resizeWidth = "0";
      resizeHeight = "0";
      canMove = "1";
      canClose = "1";
      canMinimize = "0";
      canMaximize = "0";
      minSize = "50 50";
      closeCommand = "canvas.popdialog(ItemAdmin);";

      new GuiBitmapButtonCtrl() {
         profile = "BlockButtonProfile";
         horizSizing = "left";
         vertSizing = "bottom";
         position = "245 420";
         extent = "100 25";
         minExtent = "8 2";
         visible = "1";
         command = "canvas.popdialog(ItemAdmin);";
         text = "Exit";
         groupNum = "-1";
         buttonType = "PushButton";
         bitmap = "base/client/ui/button2";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         mKeepCached = "0";
         mColor = "255 0 0 255";
      };
      new GuiScrollCtrl() {
         profile = "GuiScrollProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "10 30";
         extent = "150 400";
         minExtent = "8 2";
         visible = "1";
         willFirstRespond = "1";
         hScrollBar = "dynamic";
         vScrollBar = "alwaysOn";
         constantThumbHeight = "0";
         childMargin = "0 0";
         rowHeight = "40";
         columnWidth = "30";

         new GuiTextListCtrl(IAWnd_Itemlist) {
            profile = "GuiTextListProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "1 1";
            extent = "132 2";
            minExtent = "8 2";
            visible = "1";
            command = "IAWnd_SelectItem();";
            enumerate = "0";
            resizeCell = "1";
            columns = "0 20";
            fitParentWidth = "1";
            clipColumnText = "0";
         };
      };
      new GuiTextEditCtrl(IAWnd_TN) {
         profile = "GuiTextEditProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "220 30";
         extent = "100 18";
         minExtent = "8 2";
         visible = "1";
         maxLength = "255";
         historySize = "0";
         password = "0";
         tabComplete = "0";
         sinkAllKeyEvents = "0";
      };
      new GuiTextCtrl() {
         profile = "GuiTextProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "170 30";
         extent = "33 18";
         minExtent = "8 2";
         visible = "1";
         text = "Name :";
         maxLength = "255";
      };
      new GuiTextCtrl() {
         profile = "GuiTextProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "170 50";
         extent = "43 18";
         minExtent = "8 2";
         visible = "1";
         text = "Amount :";
         maxLength = "255";
      };
      new GuiTextEditCtrl(IAWnd_A) {
         profile = "GuiTextEditProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "220 50";
         extent = "100 18";
         minExtent = "8 2";
         visible = "1";
         maxLength = "255";
         historySize = "0";
         password = "0";
         tabComplete = "0";
         sinkAllKeyEvents = "0";
      };
      new GuiBitmapButtonCtrl(IAWnd_Grant) {
         profile = "BlockButtonProfile";
         horizSizing = "left";
         vertSizing = "bottom";
         position = "180 70";
         extent = "100 25";
         minExtent = "8 2";
         visible = "1";
         command = "IAWnd_GrantItem();";
         text = "Grant";
         groupNum = "-1";
         buttonType = "PushButton";
         bitmap = "base/client/ui/button2";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         mKeepCached = "0";
         mColor = "255 99 0 255";
      };
   };
};
//--- OBJECT WRITE END ---
