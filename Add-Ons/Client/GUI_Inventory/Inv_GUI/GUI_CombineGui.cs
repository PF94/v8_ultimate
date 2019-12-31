//--- OBJECT WRITE BEGIN ---
new GuiControl(CombineGui) {
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
      position = "81 79";
      extent = "500 300";
      minExtent = "8 2";
      visible = "1";
      text = "Combining";
      maxLength = "255";
      resizeWidth = "0";
      resizeHeight = "0";
      canMove = "1";
      canClose = "1";
      canMinimize = "0";
      canMaximize = "0";
      minSize = "50 50";
      closeCommand = "canvas.popdialog(CombineGui);";

      new GuiScrollCtrl() {
         profile = "GuiScrollProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "10 30";
         extent = "150 250";
         minExtent = "8 2";
         visible = "1";
         willFirstRespond = "1";
         hScrollBar = "alwaysOff";
         vScrollBar = "alwaysOn";
         constantThumbHeight = "0";
         childMargin = "0 0";
         rowHeight = "40";
         columnWidth = "30";

         new GuiTextListCtrl(ComWndI1L) {
            profile = "GuiTextListProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "1 1";
            extent = "133 2";
            minExtent = "8 2";
            visible = "1";
            enumerate = "0";
            resizeCell = "1";
            columns = "0 100";
            fitParentWidth = "1";
            clipColumnText = "0";
         };
      };
      new GuiScrollCtrl() {
         profile = "GuiScrollProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "340 30";
         extent = "150 250";
         minExtent = "8 2";
         visible = "1";
         willFirstRespond = "1";
         hScrollBar = "alwaysOff";
         vScrollBar = "alwaysOn";
         constantThumbHeight = "0";
         childMargin = "0 0";
         rowHeight = "40";
         columnWidth = "30";

         new GuiTextListCtrl(ComWndI2L) {
            profile = "GuiTextListProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "1 1";
            extent = "133 2";
            minExtent = "8 2";
            visible = "1";
            enumerate = "0";
            resizeCell = "1";
            columns = "0 100";
            fitParentWidth = "1";
            clipColumnText = "0";
         };
      };
      new GuiBitmapButtonCtrl(ComWndComButton) {
         profile = "BlockButtonProfile";
         horizSizing = "left";
         vertSizing = "bottom";
         position = "225 30";
         extent = "100 25";
         minExtent = "8 2";
         visible = "1";
         command = "ComWnd_Combine();";
         text = "Combine";
         groupNum = "-1";
         buttonType = "PushButton";
         bitmap = "base/client/ui/button2";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         mKeepCached = "0";
         mColor = "255 99 0 255";
      };
      new GuiBitmapButtonCtrl() {
         profile = "BlockButtonProfile";
         horizSizing = "left";
         vertSizing = "bottom";
         position = "200 270";
         extent = "100 25";
         minExtent = "8 2";
         visible = "1";
         command = "canvas.popdialog(CombineGui);";
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
         position = "175 60";
         extent = "150 200";
         minExtent = "8 2";
         visible = "1";
         willFirstRespond = "1";
         hScrollBar = "dynamic";
         vScrollBar = "alwaysOn";
         constantThumbHeight = "0";
         childMargin = "0 0";
         rowHeight = "40";
         columnWidth = "30";

         new GuiTextListCtrl(ComWndCS) {
            profile = "GuiTextListProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "1 1";
            extent = "133 2";
            minExtent = "8 2";
            visible = "1";
            enumerate = "0";
            resizeCell = "1";
            columns = "0 80 100";
            fitParentWidth = "1";
            clipColumnText = "0";
         };
      };
      new GuiBitmapButtonCtrl(ComWndCheckButton) {
         profile = "BlockButtonProfile";
         horizSizing = "left";
         vertSizing = "bottom";
         position = "175 30";
         extent = "50 25";
         minExtent = "8 2";
         visible = "1";
         command = "ComWnd_Check();";
         text = "Check";
         groupNum = "-1";
         buttonType = "PushButton";
         bitmap = "base/client/ui/button2";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         mKeepCached = "0";
         mColor = "99 0 255 255";
      };
   };
};
//--- OBJECT WRITE END ---
