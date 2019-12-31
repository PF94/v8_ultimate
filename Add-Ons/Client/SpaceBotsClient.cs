//Bots Menu
if(!isObject(SBotsGUI))
{
new GuiControl(SBotsGui) {
   profile = "GuiDefaultProfile";
   horizSizing = "right";
   vertSizing = "bottom";
   position = "0 0";
   extent = "1024 768";
   minExtent = "8 2";
   visible = "1";

   new GuiWindowCtrl(SBotsWindow) {
      profile = "BlockWindowProfile";
      horizSizing = "center";
      vertSizing = "center";
      position = "370 14";
      extent = "300 720";
      minExtent = "8 2";
      visible = "1";
      command = "canvas.popDialog(SBotsGui);";
      accelerator = "escape";
      text = "Bots Mod";
      maxLength = "255";
      resizeWidth = "0";
      resizeHeight = "1";
      canMove = "1";
      canClose = "1";
      canMinimize = "0";
      canMaximize = "1";
      minSize = "50 50";
      closeCommand = "canvas.popDialog(SBotsGui);";
         helpTag = "0";

      new GuiBitmapCtrl(bitSBotCreate1) {
         profile = "GuiDefaultProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "9 37";
         extent = "106 2";
         minExtent = "8 2";
         visible = "1";
         wrap = "0";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         keepCached = "0";
      };
      new GuiBitmapCtrl(bitSBotCreate2) {
         profile = "GuiDefaultProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "186 37";
         extent = "91 2";
         minExtent = "8 2";
         visible = "1";
         wrap = "0";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         keepCached = "0";
      };
      new GuiMLTextCtrl(txtSBotCreate) {
         profile = "GuiDefaultProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "0 29";
         extent = "300 18";
         minExtent = "8 2";
         visible = "1";
         lineSpacing = "2";
         allowColorChars = "0";
         maxChars = "-1";
         text = "<font:Impact:18><just:center>Create Bot";
         maxBitmapHeight = "-1";
      };
      new GuiBitmapButtonCtrl(butSBotCreate) {
         profile = "BlockButtonProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "276 29";
         extent = "19 19";
         minExtent = "8 2";
         visible = "1";
         command = "SpaceBots::ActivateCreate();";
         text = "+";
         groupNum = "-1";
         buttonType = "PushButton";
         bitmap = "base/client/ui/button2";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         mKeepCached = "0";
         mColor = "255 255 255 255";
      };
      new GuiSwatchCtrl(fraSBotCreate) {
         profile = "BlockButtonProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "8 53";
         extent = "286 165";
         minExtent = "8 2";
         visible = "1";
         color = "255 255 255 0";

         new GuiTextCtrl() {
            profile = "GuiTextProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "0 0";
            extent = "30 18";
            minExtent = "8 2";
            visible = "1";
            text = "Name:";
            maxLength = "255";
         };
         new GuiTextCtrl() {
            profile = "GuiTextProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "0 18";
            extent = "52 18";
            minExtent = "8 2";
            visible = "1";
            text = "Behaviour:";
            maxLength = "255";
         };
         new GuiTextCtrl() {
            profile = "GuiTextProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "0 55";
            extent = "63 18";
            minExtent = "8 2";
            visible = "1";
            text = "Appearance:";
            maxLength = "255";
         };
         new GuiTextCtrl() {
            profile = "GuiTextProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "0 71";
            extent = "51 18";
            minExtent = "8 2";
            visible = "1";
            text = "Persistent:";
            maxLength = "255";
         };
         new GuiMLTextCtrl() {
            profile = "GuiMLTextProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "0 85";
            extent = "212 12";
            minExtent = "8 2";
            visible = "1";
            lineSpacing = "2";
            allowColorChars = "0";
            maxChars = "-1";
            text = "<font:Lucidia Console:12>(Respawns when dead)";
            maxBitmapHeight = "-1";
         };
         new GuiTextEditCtrl(txtSBotCreateName) {
            profile = "GuiTextEditProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "70 0";
            extent = "210 18";
            minExtent = "8 2";
            visible = "1";
            maxLength = "20";
            historySize = "0";
            password = "0";
            tabComplete = "0";
            sinkAllKeyEvents = "0";
         };
         new GuiPopUpMenuCtrl(lstSBotCreateAI) {
            profile = "GuiPopUpMenuProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "110 19";
            extent = "100 18";
            minExtent = "8 2";
            visible = "1";
            maxLength = "255";
            maxPopupHeight = "200";
         };
         new GuiRadioCtrl(radSBotCreateAppBlock) {
            profile = "GuiRadioProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "66 55";
            extent = "140 18";
            minExtent = "8 2";
            visible = "1";
            text = "Blockhead";
            groupNum = "-1";
            buttonType = "RadioButton";
         };
         new GuiRadioCtrl(radSBotCreateAppPlayer) {
            profile = "GuiRadioProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "135 55";
            extent = "140 18";
            minExtent = "8 2";
            visible = "1";
            text = "Player:";
            groupNum = "-1";
            buttonType = "RadioButton";
         };
         new GuiPopUpMenuCtrl(lstSBotCreateAppPlayer) {
            profile = "GuiPopUpMenuProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "184 55";
            extent = "96 18";
            minExtent = "8 2";
            visible = "1";
            maxLength = "255";
            maxPopupHeight = "200";
         };
         new GuiCheckBoxCtrl(chkBotCreatePersistent) {
            profile = "GuiCheckBoxProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "65 71";
            extent = "15 18";
            minExtent = "8 2";
            visible = "1";
            text = "Button";
            groupNum = "-1";
            buttonType = "ToggleButton";
         };
         new GuiBitmapButtonCtrl() {
            profile = "BlockButtonProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "5 125";
            extent = "84 39";
            minExtent = "8 2";
            visible = "1";
            command = "canvas.popDialog(SBotsGui);";
            text = "Cancel";
            groupNum = "-1";
            buttonType = "PushButton";
            bitmap = "base/client/ui/button2";
            lockAspectRatio = "0";
            alignLeft = "0";
            overflowImage = "0";
            mKeepCached = "0";
            mColor = "255 255 255 255";
         };
         new GuiBitmapButtonCtrl() {
            profile = "BlockButtonProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "197 125";
            extent = "84 39";
            minExtent = "8 2";
            visible = "1";
            command = "SpaceBots::CreateBot();";
            text = "Send >>";
            groupNum = "-1";
            buttonType = "PushButton";
            bitmap = "base/client/ui/button2";
            lockAspectRatio = "0";
            alignLeft = "0";
            overflowImage = "0";
            mKeepCached = "0";
            mColor = "255 255 255 255";
         };
         new GuiTextCtrl() {
            profile = "GuiTextProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "0 96";
            extent = "29 18";
            minExtent = "8 2";
            visible = "1";
            text = "Team:";
            maxLength = "255";
         };
         new GuiPopUpMenuCtrl(lstSBotCreateTeam) {
            profile = "GuiPopUpMenuProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "110 96";
            extent = "100 18";
            minExtent = "8 2";
            visible = "1";
            maxLength = "255";
            maxPopupHeight = "200";
         };
      };
      new GuiBitmapCtrl(bitSBotPaths1) {
         profile = "GuiDefaultProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "9 209";
         extent = "116 2";
         minExtent = "8 2";
         visible = "1";
         wrap = "0";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         keepCached = "0";
      };
      new GuiBitmapCtrl(bitSBotPaths2) {
         profile = "GuiDefaultProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "174 209";
         extent = "103 2";
         minExtent = "8 2";
         visible = "1";
         wrap = "0";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         keepCached = "0";
      };
      new GuiMLTextCtrl(txtSBotPaths) {
         profile = "GuiDefaultProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "1 200";
         extent = "300 18";
         minExtent = "8 2";
         visible = "1";
         lineSpacing = "2";
         allowColorChars = "0";
         maxChars = "-1";
         text = "<font:Impact:18><just:center>Paths";
         maxBitmapHeight = "-1";
      };
      new GuiBitmapButtonCtrl(butSBotPaths) {
         profile = "BlockButtonProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "276 202";
         extent = "19 19";
         minExtent = "8 2";
         visible = "1";
         command = "SpaceBots::ActivatePaths();";
         text = "+";
         groupNum = "-1";
         buttonType = "PushButton";
         bitmap = "base/client/ui/button2";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         mKeepCached = "0";
         mColor = "255 255 255 255";
      };
      new GuiSwatchCtrl(fraSBotPaths) {
         profile = "BlockButtonProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "8 225";
         extent = "286 112";
         minExtent = "8 2";
         visible = "1";
         color = "255 255 255 0";

         new GuiBitmapButtonCtrl() {
            profile = "GuiCenterTextProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "161 86";
            extent = "75 18";
            minExtent = "8 2";
            visible = "1";
            command = "SpaceBots::PathDeleteAll();";
            text = "Delete All";
            groupNum = "-1";
            buttonType = "PushButton";
            bitmap = "base/client/ui/button1";
            lockAspectRatio = "0";
            alignLeft = "0";
            overflowImage = "0";
            mKeepCached = "0";
            mColor = "255 0 0 255";
         };
         new GuiBitmapButtonCtrl() {
            profile = "GuiCenterTextProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "122 19";
            extent = "40 18";
            minExtent = "8 2";
            visible = "1";
            command = "SpaceBots::PathNew();";
            text = "New";
            groupNum = "-1";
            buttonType = "PushButton";
            bitmap = "base/client/ui/button2";
            lockAspectRatio = "0";
            alignLeft = "0";
            overflowImage = "0";
            mKeepCached = "0";
            mColor = "255 255 255 255";
         };
         new GuiPopUpMenuCtrl(lstSBotPathNew) {
            profile = "GuiPopUpMenuProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "91 39";
            extent = "100 18";
            minExtent = "8 2";
            visible = "1";
            maxLength = "255";
            maxPopupHeight = "200";
         };
         new GuiTextEditCtrl(txtSBotPathNew) {
            profile = "GuiTextEditProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "91 0";
            extent = "100 18";
            minExtent = "8 2";
            visible = "1";
            maxLength = "20";
            historySize = "0";
            password = "0";
            tabComplete = "0";
            sinkAllKeyEvents = "0";
         };
         new GuiBitmapButtonCtrl() {
            profile = "GuiCenterTextProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "48 63";
            extent = "75 18";
            minExtent = "8 2";
            visible = "1";
            command = "SpaceBots::PathPoint();";
            text = "Add Point";
            groupNum = "-1";
            buttonType = "PushButton";
            bitmap = "base/client/ui/button1";
            lockAspectRatio = "0";
            alignLeft = "0";
            overflowImage = "0";
            mKeepCached = "0";
            mColor = "255 255 255 255";
         };
         new GuiBitmapButtonCtrl() {
            profile = "GuiCenterTextProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "48 86";
            extent = "75 18";
            minExtent = "8 2";
            visible = "1";
            command = "SpaceBots::PathDelete();";
            text = "Delete Path";
            groupNum = "-1";
            buttonType = "PushButton";
            bitmap = "base/client/ui/button1";
            lockAspectRatio = "0";
            alignLeft = "0";
            overflowImage = "0";
            mKeepCached = "0";
            mColor = "255 0 0 255";
         };
         new GuiBitmapButtonCtrl() {
            profile = "GuiCenterTextProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "161 63";
            extent = "75 18";
            minExtent = "8 2";
            visible = "1";
            command = "SpaceBots::PathShow();";
            text = "View Path";
            groupNum = "-1";
            buttonType = "PushButton";
            bitmap = "base/client/ui/button1";
            lockAspectRatio = "0";
            alignLeft = "0";
            overflowImage = "0";
            mKeepCached = "0";
            mColor = "255 255 255 255";
         };
      };
      new GuiBitmapCtrl(bitSBotManage1) {
         profile = "GuiDefaultProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "9 351";
         extent = "96 2";
         minExtent = "8 2";
         visible = "1";
         wrap = "0";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         keepCached = "0";
      };
      new GuiBitmapCtrl(bitSBotManage2) {
         profile = "GuiDefaultProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "195 351";
         extent = "82 2";
         minExtent = "8 2";
         visible = "1";
         wrap = "0";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         keepCached = "0";
      };
      new GuiMLTextCtrl(txtSBotManage) {
         profile = "GuiDefaultProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "2 341";
         extent = "300 18";
         minExtent = "8 2";
         visible = "1";
         lineSpacing = "2";
         allowColorChars = "0";
         maxChars = "-1";
         text = "<font:Impact:18><just:center>Management";
         maxBitmapHeight = "-1";
      };
      new GuiBitmapButtonCtrl(butSBotManage) {
         profile = "BlockButtonProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "276 344";
         extent = "19 19";
         minExtent = "8 2";
         visible = "1";
         command = "SpaceBots::ActivateManage();";
         text = "+";
         groupNum = "-1";
         buttonType = "PushButton";
         bitmap = "base/client/ui/button2";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         mKeepCached = "0";
         mColor = "255 255 255 255";
      };
      new GuiSwatchCtrl(fraSBotManage) {
         profile = "BlockButtonProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "8 367";
         extent = "286 283";
         minExtent = "8 2";
         visible = "1";
         color = "255 255 255 0";

         new GuiBitmapButtonCtrl() {
            profile = "GuiCenterTextProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "177 31";
            extent = "99 18";
            minExtent = "8 2";
            visible = "1";
            command = "SpaceBots::ManageDeleteOne();";
            text = "Delete";
            groupNum = "-1";
            buttonType = "PushButton";
            bitmap = "base/client/ui/button1";
            lockAspectRatio = "0";
            alignLeft = "0";
            overflowImage = "0";
            mKeepCached = "0";
            mColor = "255 255 255 255";
         };
         new GuiSwatchCtrl() {
            profile = "GuiDefaultProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "13 32";
            extent = "146 147";
            minExtent = "8 2";
            visible = "1";
            color = "255 255 255 128";
         };
         new GuiScrollCtrl() {
            profile = "ColorScrollProfile";
            horizSizing = "width";
            vertSizing = "bottom";
            position = "13 31";
            extent = "158 149";
            minExtent = "8 2";
            visible = "1";
            willFirstRespond = "1";
            hScrollBar = "alwaysOff";
            vScrollBar = "alwaysOn";
            constantThumbHeight = "0";
            childMargin = "0 0";
            rowHeight = "10";
            columnWidth = "30";

            new GuiTextListCtrl(lstSBotManageList) {
               profile = "GuiTextListProfile";
               horizSizing = "left";
               vertSizing = "height";
               position = "0 0";
               extent = "146 16";
               minExtent = "8 2";
               visible = "1";
               enumerate = "0";
               resizeCell = "0";
               columns = "0 75";
               fitParentWidth = "1";
               clipColumnText = "0";
                  helpTag = "0";
            };
         };
         new GuiTextCtrl() {
            profile = "GuiTextProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "18 12";
            extent = "16 18";
            minExtent = "8 2";
            visible = "1";
            text = "Bot";
            maxLength = "255";
         };
         new GuiTextCtrl() {
            profile = "GuiTextProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "93 10";
            extent = "34 18";
            minExtent = "8 2";
            visible = "1";
            text = "Owner";
            maxLength = "255";
         };
         new GuiBitmapButtonCtrl() {
            profile = "GuiCenterTextProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "177 51";
            extent = "99 18";
            minExtent = "8 2";
            visible = "1";
            command = "SpaceBots::ManageClear();";
            text = "Clear Yours";
            groupNum = "-1";
            buttonType = "PushButton";
            bitmap = "base/client/ui/button1";
            lockAspectRatio = "0";
            alignLeft = "0";
            overflowImage = "0";
            mKeepCached = "0";
            mColor = "255 255 255 255";
         };
         new GuiBitmapButtonCtrl(butSBotManageClearTheirs) {
            profile = "GuiCenterTextProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "177 71";
            extent = "99 18";
            minExtent = "8 2";
            visible = "1";
            command = "SpaceBots::ManageClearTheirs();";
            text = "Clear Owner\'s";
            groupNum = "-1";
            buttonType = "PushButton";
            bitmap = "base/client/ui/button1";
            lockAspectRatio = "0";
            alignLeft = "0";
            overflowImage = "0";
            mKeepCached = "0";
            mColor = "255 255 255 255";
         };
         new GuiBitmapButtonCtrl(butSBotManageClearAll) {
            profile = "GuiCenterTextProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "177 91";
            extent = "99 18";
            minExtent = "8 2";
            visible = "1";
            command = "MessageBoxYesNo(\"Clear Bots?\",\"Are you sure you want to delete all bots?\",\"SpaceBots::ManageClearAll();\",\"\");";
            text = "Clear All";
            groupNum = "-1";
            buttonType = "PushButton";
            bitmap = "base/client/ui/button1";
            lockAspectRatio = "0";
            alignLeft = "0";
            overflowImage = "0";
            mKeepCached = "0";
            mColor = "255 0 0 255";
         };
         new GuiMLTextCtrl() {
            profile = "GuiDefaultProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "13 182";
            extent = "79 14";
            minExtent = "8 2";
            visible = "1";
            lineSpacing = "2";
            allowColorChars = "0";
            maxChars = "-1";
            text = "<font:Arial Bold:14>Max Bots Per";
            maxBitmapHeight = "-1";
         };
         new GuiMLTextCtrl() {
            profile = "GuiDefaultProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "145 182";
            extent = "100 14";
            minExtent = "8 2";
            visible = "1";
            lineSpacing = "2";
            allowColorChars = "0";
            maxChars = "-1";
            text = "<font:Arial Bold:14>Max AI Bots Per";
            maxBitmapHeight = "-1";
         };
         new GuiMLTextCtrl() {
            profile = "GuiDefaultProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "13 198";
            extent = "55 14";
            minExtent = "8 2";
            visible = "1";
            lineSpacing = "2";
            allowColorChars = "0";
            maxChars = "-1";
            text = "Player";
            maxBitmapHeight = "-1";
         };
         new GuiTextEditCtrl(txtSBotManageMaxBPlayer) {
            profile = "GuiTextEditProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "60 196";
            extent = "22 18";
            minExtent = "8 2";
            visible = "1";
            maxLength = "2";
            historySize = "0";
            password = "0";
            tabComplete = "0";
            sinkAllKeyEvents = "0";
         };
         new GuiCheckBoxCtrl(chkBotManageMaxBPlayer) {
            profile = "GuiCheckBoxProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "87 196";
            extent = "50 18";
            minExtent = "8 2";
            visible = "1";
            text = "Infinite";
            groupNum = "-1";
            buttonType = "ToggleButton";
         };
         new GuiCheckBoxCtrl(chkBotManageMaxBAdmin) {
            profile = "GuiCheckBoxProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "87 217";
            extent = "50 18";
            minExtent = "8 2";
            visible = "1";
            text = "Infinite";
            groupNum = "-1";
            buttonType = "ToggleButton";
         };
         new GuiTextEditCtrl(txtSBotManageMaxBAdmin) {
            profile = "GuiTextEditProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "60 217";
            extent = "22 18";
            minExtent = "8 2";
            visible = "1";
            maxLength = "2";
            historySize = "0";
            password = "0";
            tabComplete = "0";
            sinkAllKeyEvents = "0";
         };
         new GuiMLTextCtrl() {
            profile = "GuiDefaultProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "13 219";
            extent = "55 14";
            minExtent = "8 2";
            visible = "1";
            lineSpacing = "2";
            allowColorChars = "0";
            maxChars = "-1";
            text = "Admin";
            maxBitmapHeight = "-1";
         };
         new GuiCheckBoxCtrl(chkBotManageMaxBSAdmin) {
            profile = "GuiCheckBoxProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "87 238";
            extent = "50 18";
            minExtent = "8 2";
            visible = "1";
            text = "Infinite";
            groupNum = "-1";
            buttonType = "ToggleButton";
         };
         new GuiTextEditCtrl(txtSBotManageMaxBSAdmin) {
            profile = "GuiTextEditProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "60 238";
            extent = "22 18";
            minExtent = "8 2";
            visible = "1";
            maxLength = "2";
            historySize = "0";
            password = "0";
            tabComplete = "0";
            sinkAllKeyEvents = "0";
         };
         new GuiMLTextCtrl() {
            profile = "GuiDefaultProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "13 240";
            extent = "55 14";
            minExtent = "8 2";
            visible = "1";
            lineSpacing = "2";
            allowColorChars = "0";
            maxChars = "-1";
            text = "S-Admin";
            maxBitmapHeight = "-1";
         };
         new GuiMLTextCtrl() {
            profile = "GuiDefaultProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "145 200";
            extent = "55 14";
            minExtent = "8 2";
            visible = "1";
            lineSpacing = "2";
            allowColorChars = "0";
            maxChars = "-1";
            text = "Player";
            maxBitmapHeight = "-1";
         };
         new GuiTextEditCtrl(txtSBotManageMaxAIPlayer) {
            profile = "GuiTextEditProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "192 198";
            extent = "22 18";
            minExtent = "8 2";
            visible = "1";
            maxLength = "2";
            historySize = "0";
            password = "0";
            tabComplete = "0";
            sinkAllKeyEvents = "0";
         };
         new GuiMLTextCtrl() {
            profile = "GuiDefaultProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "145 242";
            extent = "55 14";
            minExtent = "8 2";
            visible = "1";
            lineSpacing = "2";
            allowColorChars = "0";
            maxChars = "-1";
            text = "S-Admin";
            maxBitmapHeight = "-1";
         };
         new GuiTextEditCtrl(txtSBotManageMaxAISAdmin) {
            profile = "GuiTextEditProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "192 240";
            extent = "22 18";
            minExtent = "8 2";
            visible = "1";
            maxLength = "2";
            historySize = "0";
            password = "0";
            tabComplete = "0";
            sinkAllKeyEvents = "0";
         };
         new GuiTextEditCtrl(txtSBotManageMaxAIAdmin) {
            profile = "GuiTextEditProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "192 219";
            extent = "22 18";
            minExtent = "8 2";
            visible = "1";
            maxLength = "2";
            historySize = "0";
            password = "0";
            tabComplete = "0";
            sinkAllKeyEvents = "0";
         };
         new GuiMLTextCtrl() {
            profile = "GuiDefaultProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "145 221";
            extent = "55 14";
            minExtent = "8 2";
            visible = "1";
            lineSpacing = "2";
            allowColorChars = "0";
            maxChars = "-1";
            text = "Admin";
            maxBitmapHeight = "-1";
         };
         new GuiBitmapButtonCtrl(butSBotManageLimits) {
            profile = "BlockButtonProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "217 235";
            extent = "64 39";
            minExtent = "8 2";
            visible = "1";
            command = "SpaceBots::ManageLimits();";
            text = "Set";
            groupNum = "-1";
            buttonType = "PushButton";
            bitmap = "base/client/ui/button2";
            lockAspectRatio = "0";
            alignLeft = "0";
            overflowImage = "0";
            mKeepCached = "0";
            mColor = "255 255 255 255";
         };
      };
      new GuiBitmapCtrl(bitSBotSave1) {
         profile = "GuiDefaultProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "8 650";
         extent = "112 2";
         minExtent = "8 2";
         visible = "1";
         wrap = "0";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         keepCached = "0";
      };
      new GuiBitmapCtrl(bitSBotSave2) {
         profile = "GuiDefaultProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "177 650";
         extent = "100 2";
         minExtent = "8 2";
         visible = "1";
         wrap = "0";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         keepCached = "0";
      };
      new GuiMLTextCtrl(txtSBotSave) {
         profile = "GuiDefaultProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "0 642";
         extent = "300 18";
         minExtent = "8 2";
         visible = "1";
         lineSpacing = "2";
         allowColorChars = "0";
         maxChars = "-1";
         text = "<font:Impact:18><just:center>Saving";
         maxBitmapHeight = "-1";
      };
      new GuiBitmapButtonCtrl(butSBotSave) {
         profile = "BlockButtonProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "276 642";
         extent = "19 19";
         minExtent = "8 2";
         visible = "1";
         command = "SpaceBots::ActivateSave();";
         text = "+";
         groupNum = "-1";
         buttonType = "PushButton";
         bitmap = "base/client/ui/button2";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         mKeepCached = "0";
         mColor = "255 255 255 255";
      };
      new GuiSwatchCtrl(fraSBotSave) {
         profile = "BlockButtonProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "8 665";
         extent = "286 44";
         minExtent = "8 2";
         visible = "1";
         color = "255 255 255 0";

         new GuiBitmapButtonCtrl() {
            profile = "GuiCenterTextProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "9 13";
            extent = "46 18";
            minExtent = "8 2";
            visible = "1";
            command = "SpaceBots::SaveSave();";
            text = "Save";
            groupNum = "-1";
            buttonType = "PushButton";
            bitmap = "base/client/ui/button2";
            lockAspectRatio = "0";
            alignLeft = "0";
            overflowImage = "0";
            mKeepCached = "0";
            mColor = "255 255 255 255";
         };
         new GuiTextEditCtrl(txtSBotSaveName) {
            profile = "GuiTextEditProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "59 13";
            extent = "74 18";
            minExtent = "8 2";
            visible = "1";
            maxLength = "20";
            historySize = "0";
            password = "0";
            tabComplete = "0";
            sinkAllKeyEvents = "0";
         };
         new GuiPopUpMenuCtrl(lstSBotSaveLoadList) {
            profile = "GuiPopUpMenuProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "200 13";
            extent = "74 18";
            minExtent = "8 2";
            visible = "1";
            maxLength = "255";
            maxPopupHeight = "200";
         };
         new GuiBitmapButtonCtrl(butSBotSaveLoad) {
            profile = "GuiCenterTextProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "150 13";
            extent = "46 18";
            minExtent = "8 2";
            visible = "1";
            command = "SpaceBots::SaveLoad();";
            text = "Load";
            groupNum = "-1";
            buttonType = "PushButton";
            bitmap = "base/client/ui/button2";
            lockAspectRatio = "0";
            alignLeft = "0";
            overflowImage = "0";
            mKeepCached = "0";
            mColor = "255 255 255 255";
         };
      };
   };
};
new GuiControl(SBotSpawnGUI) {
   profile = "GuiDefaultProfile";
   horizSizing = "right";
   vertSizing = "bottom";
   position = "0 0";
   extent = "640 480";
   minExtent = "8 2";
   visible = "1";

   new GuiWindowCtrl(SBotSpawnWindow) {
      profile = "BlockWindowProfile";
      horizSizing = "center";
      vertSizing = "center";
      position = "170 94";
      extent = "300 200";
      minExtent = "8 2";
      visible = "1";
      command = "canvas.popDialog(SBotSpawnGui);";
      accelerator = "escape";
      text = "Bot Spawn";
      maxLength = "255";
      resizeWidth = "0";
      resizeHeight = "1";
      canMove = "1";
      canClose = "1";
      canMinimize = "0";
      canMaximize = "1";
      minSize = "50 50";
      closeCommand = "canvas.popDialog(SBotSpawnGui);";
         helpTag = "0";

      new GuiTextEditCtrl(txtSBotSpawnMax) {
         profile = "GuiTextEditProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "259 127";
         extent = "30 18";
         minExtent = "8 2";
         visible = "1";
         maxLength = "3";
         historySize = "0";
         password = "0";
         tabComplete = "0";
         sinkAllKeyEvents = "0";
      };
      new GuiCheckBoxCtrl(chkSBotCopySpawn) {
   	profile = "GuiCheckBoxProfile";
   	horizSizing = "right";
   	vertSizing = "bottom";
   	position = "123 161";
   	extent = "55 30";
   	minExtent = "8 2";
   	visible = "1";
   	text = "Lock";
   	groupNum = "-1";
   	buttonType = "ToggleButton";
      };
      new GuiTextCtrl() {
         profile = "GuiTextProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "140 127";
         extent = "118 18";
         minExtent = "8 2";
         visible = "1";
         text = "ms; Max Bots Spawned:";
         maxLength = "255";
      };
      new GuiTextEditCtrl(txtSBotSpawnMS) {
         profile = "GuiTextEditProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "107 127";
         extent = "30 18";
         minExtent = "8 2";
         visible = "1";
         maxLength = "5";
         historySize = "0";
         password = "0";
         tabComplete = "0";
         sinkAllKeyEvents = "0";
      };
      new GuiTextCtrl() {
         profile = "GuiTextProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "8 31";
         extent = "30 18";
         minExtent = "8 2";
         visible = "1";
         text = "Name:";
         maxLength = "255";
      };
      new GuiTextCtrl() {
         profile = "GuiTextProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "8 49";
         extent = "52 18";
         minExtent = "8 2";
         visible = "1";
         text = "Behaviour:";
         maxLength = "255";
      };
      new GuiTextCtrl() {
         profile = "GuiTextProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "8 86";
         extent = "63 18";
         minExtent = "8 2";
         visible = "1";
         text = "Appearance:";
         maxLength = "255";
      };
      new GuiPopUpMenuCtrl(lstSBotSpawnTeam) {
         profile = "GuiPopUpMenuProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "118 105";
         extent = "100 18";
         minExtent = "8 2";
         visible = "1";
         maxLength = "255";
         maxPopupHeight = "200";
      };
      new GuiPopUpMenuCtrl(lstSBotSpawnPath) {
         //This list is not used for anything. It is used to copy/paste the path names in and out of when you select Patrol, Guard, etc as AI behaviours.
         profile = "GuiPopUpMenuProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "118 1024";
         extent = "100 18";
         minExtent = "8 2";
         visible = "1";
         maxLength = "255";
         maxPopupHeight = "200";
      };
      new GuiBitmapButtonCtrl() {
         profile = "BlockButtonProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "205 156";
         extent = "84 39";
         minExtent = "8 2";
         visible = "1";
         command = "SpaceBots::BotSpawn();";
         text = "Send >>";
         groupNum = "-1";
         buttonType = "PushButton";
         bitmap = "base/client/ui/button2";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         mKeepCached = "0";
         mColor = "255 255 255 255";
      };
      new GuiTextEditCtrl(txtSBotSpawnName) {
         profile = "GuiTextEditProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "78 31";
         extent = "210 18";
         minExtent = "8 2";
         visible = "1";
         maxLength = "20";
         historySize = "0";
         password = "0";
         tabComplete = "0";
         sinkAllKeyEvents = "0";
      };
      new GuiPopUpMenuCtrl(lstSBotSpawnAI) {
         profile = "GuiPopUpMenuProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "118 50";
         extent = "100 18";
         minExtent = "8 2";
         visible = "1";
         maxLength = "255";
         maxPopupHeight = "200";
      };
      new GuiRadioCtrl(radSBotSpawnAppBlock) {
         profile = "GuiRadioProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "74 86";
         extent = "140 18";
         minExtent = "8 2";
         visible = "1";
         text = "Blockhead";
         groupNum = "-1";
         buttonType = "RadioButton";
      };
      new GuiRadioCtrl(radSBotSpawnAppPlayer) {
         profile = "GuiRadioProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "143 86";
         extent = "140 18";
         minExtent = "8 2";
         visible = "1";
         text = "Player:";
         groupNum = "-1";
         buttonType = "RadioButton";
      };
      new GuiPopUpMenuCtrl(lstSBotSpawnAppPlayer) {
         profile = "GuiPopUpMenuProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "192 86";
         extent = "96 18";
         minExtent = "8 2";
         visible = "1";
         maxLength = "255";
         maxPopupHeight = "200";
            valcount = "-1";
      };
      new GuiTextCtrl() {
         profile = "GuiTextProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "8 105";
         extent = "29 18";
         minExtent = "8 2";
         visible = "1";
         text = "Team:";
         maxLength = "255";
      };
      new GuiBitmapButtonCtrl() {
         profile = "BlockButtonProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "13 156";
         extent = "84 39";
         minExtent = "8 2";
         visible = "1";
         command = "canvas.popDialog(SBotSpawnGui);";
         text = "Cancel";
         groupNum = "-1";
         buttonType = "PushButton";
         bitmap = "base/client/ui/button2";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         mKeepCached = "0";
         mColor = "255 255 255 255";
      };
      new GuiTextCtrl() {
         profile = "GuiTextProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "8 127";
         extent = "97 18";
         minExtent = "8 2";
         visible = "1";
         text = "Spawns Bots Every";
         maxLength = "255";
      };
   };
};
}

if($version $= "9")
{
 chkSBotCopySpawn.setText("Copy");
}

//Create Menu open
$SpaceBots::Client::Create = 1;
$SpaceBots::Client::Paths = 0;
$SpaceBots::Client::Manage = 0;
$SpaceBots::Client::Save = 0;

//----Keybinds----\\
if (!$addedSBotMaps)
{
	$remapDivision[$remapCount] = "Bots Mod";
	$remapName[$remapCount] = "Open GUI";
	$remapCmd[$remapCount] = "toggleBotgui";
	$remapCount++;
	$addedSBotMaps = 1;
}
if(movemap.getBinding("toggleBotGui") $= ""){movemap.bind("keyboard","ctrl b","","toggleBotGUI");}
function toggleBotgui(%val)
{
 if(!$SpaceBots::Client::ServerHasMod)
 {
  clientcmdMessageBoxOK("Bots Mod","The server you are in isn't running the Bots Mod.");
  return;
 }
 if(%val)
  SBotsGui.toggle();
}

function SBotsGui::toggle(%this)
{
   if (%this.isAwake())
      Canvas.popDialog(%this);
   else
      Canvas.pushDialog(%this);
}

function clientcmdOpenSBotSpawnMenu(%params,%player,%teams,%paths)
{
 if(%teams $= "")
 {
  %teams = "NONE";
 }
 else
 {
  %teams = "NONE" TAB %teams;
 }

 canvas.pushDialog(SBotSpawnGUI);

 if(chkSBotCopySpawn.getValue())
 {
  %p = lstSBotSpawnAppPlayer.getText();
  lstSBotSpawnAppPlayer.clear();
  for(%i=0;%i<getFieldCount(%player);%i++)
  {
   lstSBotSpawnAppPlayer.add(getField(%player,%i),%i);
  }
  lstSBotSpawnAppPlayer.setText(%p);
  %p = lstSBotSpawnTeam.getText();
  lstSBotSpawnTeam.clear();
  for(%i=0;%i<getFieldCount(%teams);%i++)
  {
   lstSBotSpawnTeam.add(getField(%teams,%i),%i);
  }
  lstSBotSpawnTeam.setText(%p);
  lstSBotSpawnPath.clear();
  for(%i=0;%i<getFieldCount(%paths);%i++)
  {
   lstSBotSpawnPath.add(getField(%paths,%i),%i);
  }
  return;
 }

 txtSBotSpawnName.setText(getField(%params,0));
 lstSBotSpawnAI.clear();
 for(%i=0;(%txt = getField($SpaceBots::Client::BehaviourList,%i)) !$= "";%i++) 
 {
  lstSBotSpawnAI.add(%txt,%i);
 }
 %p = getField(%params,1) TAB getField(%params,2);
 if(%p !$= "\t" && %p !$= "")
 {
  %txt = %p;
 }
 else
 {
  %txt = "Stand";
 }
 lstSBotSpawnAI.setText(getField(%params,1));
 lstSBotSpawnAI.schedule(100,onSelect,0,%p);

  lstSBotSpawnPath.clear();
  for(%i=0;%i<getFieldCount(%paths);%i++)
  {
   lstSBotSpawnPath.add(getField(%paths,%i),%i);
  }

 lstSBotSpawnAppPlayer.clear();
 for(%i=0;%i<getFieldCount(%player);%i++)
 {
  lstSBotSpawnAppPlayer.add(getField(%player,%i),%i);
 }
 %p = getField(%params,3);
 if(%p $= "")
 {
  radSBotSpawnAppBlock.performClick();
  lstSBotSpawnAppPlayer.setText(lstSBotSpawnAppPlayer.getTextByID(0));
 }
 else
 {
  radSBotSpawnAppPlayer.performClick();
  lstSBotSpawnAppPlayer.setText(%p);
 }

 lstSBotSpawnTeam.clear();
 for(%i=0;%i<getFieldCount(%teams);%i++)
 {
  lstSBotSpawnTeam.add(getField(%teams,%i),%i);
 }

 %p = getField(%params,4);
 if(%p $= "")
 {
  lstSBotSpawnTeam.setText(lstSBotSpawnTeam.getTextByID(0));
 }
 else
 {
  lstSBotSpawnTeam.setText(%p);
 }

 txtSBotSpawnMS.setvalue(getField(%params,5));
 txtSBotSpawnMax.setvalue(getField(%params,6));
}

function SpaceBots::UpdateGUI()
{
 butSBotCreate.setText(($SpaceBots::Client::Create ? "-" : "+"));
 butSBotPaths.setText(($SpaceBots::Client::Paths ? "-" : "+"));
 butSBotManage.setText(($SpaceBots::Client::Manage ? "-" : "+"));
 butSBotSave.setText(($SpaceBots::Client::Save ? "-" : "+"));

 %height = 29;
 bitSBotCreate1.resize(9,%height+8,106,2);
 bitSBotCreate2.resize(186,%height+8,91,2);
 butSBotCreate.resize(276,%height,19,19);
 txtSBotCreate.resize(0,%height,300,18);
 %height += 24;
 fraSBotCreate.setVisible(0);
 if($SpaceBots::Client::Create)
 {
  fraSBotCreate.resize(8,%height,286,165);
  fraSBotCreate.setVisible(1);
  %height += 167;
 }
 %aitype = lstSBotCreateAI.getText();
 lstSBotCreateAI.clear();
 for(%i=0;(%txt = getField($SpaceBots::Client::BehaviourList,%i)) !$= "";%i++) 
 {
  lstSBotCreateAI.add(%txt,%i);
 }
 lstSBotCreateAI.settext(%aitype);

 bitSBotPaths1.resize(9,%height+8,118,2);
 bitSBotPaths2.resize(174,%height+8,103,2);
 butSBotPaths.resize(276,%height,19,19);
 txtSBotPaths.resize(0,%height,300,18);
 %height += 24;
 fraSBotPaths.setVisible(0);
 if($SpaceBots::Client::Paths)
 {
  fraSBotPaths.resize(8,%height,286,112);
  fraSBotPaths.setVisible(1);
  %height += 114;
 }

 bitSBotManage1.resize(9,%height+8,97,2);
 bitSBotManage2.resize(195,%height+8,82,2);
 butSBotManage.resize(276,%height,19,19);
 txtSBotManage.resize(0,%height,300,18);
 %height += 24;
 fraSBotManage.setVisible(0);
 if($SpaceBots::Client::Manage)
 {
  fraSBotManage.resize(8,%height,286,283);
  fraSBotManage.setVisible(1);
  %height += 285;
 }

 bitSBotSave1.resize(9,%height+8,115,2);
 bitSBotSave2.resize(176,%height+8,101,2);
 butSBotSave.resize(276,%height,19,19);
 txtSBotSave.resize(0,%height,300,18);
 %height += 24;
 fraSBotSave.setVisible(0);
 if($SpaceBots::Client::Save)
 {
  fraSBotSave.resize(8,%height,286,44);
  fraSBotSave.setVisible(1);
  %height += 50;
 }
 SBotsWindow.resize(370,14,300,%height);

 SpaceBots::GetGUIValues();
}

function SpaceBots::ActivateCreate()
{
 $SpaceBots::Client::Create = !$SpaceBots::Client::Create;
 butSBotCreate.setText(($SpaceBots::Client::Create ? "-" : "+"));
 SpaceBots::UpdateGUI();
}

function SpaceBots::ActivatePaths()
{
 $SpaceBots::Client::Paths = !$SpaceBots::Client::Paths;
 butSBotPaths.setText(($SpaceBots::Client::Paths ? "-" : "+"));
 SpaceBots::UpdateGUI();
}

function SpaceBots::ActivateManage()
{
 $SpaceBots::Client::Manage = !$SpaceBots::Client::Manage;
 butSBotManage.setText(($SpaceBots::Client::Manage ? "-" : "+"));
 SpaceBots::UpdateGUI();
}

function SpaceBots::ActivateSave()
{
 $SpaceBots::Client::Save = !$SpaceBots::Client::Save;
 butSBotSave.setText(($SpaceBots::Client::Save ? "-" : "+"));
 if($SpaceBots::Client::Save)  updateBotLoadMenu();
 SpaceBots::UpdateGUI();
}

function SBotsGUI::onWake(%this)
{
 SpaceBots::UpdateGUI();
}

function SpaceBots::ClearAllGUI()
{
 txtSBotCreateName.setValue("");
 lstSBotCreateAI.setText("Stand");
 for(%i=0;isObject(lstSBotCreateAI.ctrl[%i]);%i++)
 {
  lstSBotCreateAI.ctrl[%i].delete();
 }
 lstSBotCreateAI.clear();
 lstSBotCreateAI.setText("Stand");
 lstSBotCreateAI.ctrl0 = new GuiBitmapCtrl() {
         profile = "GuiDefaultProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "40 43";
         extent = "200 2";
         minExtent = "8 2";
         visible = "1";
         wrap = "0";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         keepCached = "0";
 };fraSBotCreate.add(lstSBotCreateAI.ctrl0);
 radSBotCreateAppBlock.performClick();
 lstSBotCreateAppPlayer.clear();
 lstSBotCreateAppPlayer.setText("");
 //When you open the GUI this list is populated
 chkBotCreatePersistent.setValue(0);
 lstSBotCreateTeam.clear();
 lstSBotCreateTeam.setText("NONE");
 //When you open the GUI this list is populated

 txtSBotPathNew.setText("");
 lstSBotPathNew.clear();
 lstSBotPathNew.setText("");
 lstSBotPathNew.valcount = -1;
 //When you open the GUI this list is populated

 lstSBotManageList.clear();
 //When you open the GUI this list is populated
 //When you open the GUI all the Max Bots/Paths values and 'Allowed to Clear' buttons are set
}

function SpaceBots::GetGUIValues()
{
 lstSBotCreateAppPlayer.clear();
 lstSBotCreateAppPlayer.valcount = -1;

 %t = lstSBotCreateTeam.gettext();
 lstSBotCreateTeam.clear();
 lstSBotCreateTeam.add("NONE",0);
 if(%t $= "")
 {
  lstSBotCreateTeam.setText("NONE");
 }
 else
 {
  lstSBotCreateTeam.setText(%t);
 }

 %t = lstSBotPathNew.gettext();
 lstSBotPathNew.clear();
 lstSBotPathNew.valCount = -1;
 lstSBotPathNew.setText(%t);

 lstSBotManageList.clear();
 commandtoserver('SBotsGUIUpdates');
}

function SpaceBots::CreateBot()
{
 %name = txtSBotCreateName.getValue();
 %perm = chkBotCreatePersistent.getValue();
 %behaviour = lstSBotCreateAI.getText();
 %team = lstSBotCreateTeam.getText();
 if(%team $= "NONE"){%team = "";}
 if(isObject(lstSBotCreateAI.ctrl1))
 {
  if(lstSBotCreateAI.ctrl1.getClassName() $= "GUITextEditCtrl")
  {
   %arg = lstSBotCreateAI.ctrl1.getValue();
  }
  else
  {
   %arg = lstSBotCreateAI.ctrl1.getText();
  }
 }
 else
 {
  %arg = "";
 }
 if(radSBotCreateAppPlayer.getValue())
 {
  commandtoserver('createclone',lstSBotCreateAppPlayer.getText(),%name,%perm,%behaviour,%arg,%team);
 }
 else
 {
  commandtoserver('createbot',%name,%perm,%behaviour,%arg,%team);
 }
 canvas.popDialog(SBotsGUI);
}

function SpaceBots::BotSpawn()
{
 %name = txtSBotSpawnName.getValue();
 %behaviour = lstSBotSpawnAI.getText();
 %team = lstSBotSpawnTeam.getText();
 if(%team $= "NONE"){%team = "";}
 if(isObject(lstSBotSpawnAI.ctrl1))
 {
  if(lstSBotSpawnAI.ctrl1.getClassName() $= "GUITextEditCtrl")
  {
   %arg = lstSBotSpawnAI.ctrl1.getValue();
  }
  else
  {
   %arg = lstSBotSpawnAI.ctrl1.getText();
  }
 }
 else
 {
  %arg = "";
 }
 %ms = txtSBotSpawnMS.getValue();
 %max = txtSBotSpawnMax.getValue();
 if(radSBotSpawnAppPlayer.getValue())
 {
  commandtoserver('setBotSpawn',lstSBotSpawnAppPlayer.getText(),%name,%behaviour,%arg,%team,%ms,%max);
 }
 else
 {
  commandtoserver('setBotSpawn',"",%name,%behaviour,%arg,%team,%ms,%max);
 }
 canvas.popDialog(SBotSpawnGUI);
}

function clientcmdSBotsConfirmInit(%behaviourlist,%behaviourcontrols)
{
 $SpaceBots::Client::BehaviourList = %behaviourlist;
 $SpaceBots::Client::BehaviourControls = %behaviourcontrols;
 $SpaceBots::Client::ServerHasMod = 1;
}

function clientcmdSBotsGUIPlayer(%name)
{
 if(lstSBotCreateAppPlayer.valcount == -1)
 {
  lstSBotCreateAppPlayer.setText(%name);
 }
 lstSBotCreateAppPlayer.add(%name,lstSBotCreateAppPlayer.valcount++);
}

function clientcmdSBotsGUITeam(%name)
{
 lstSBotCreateTeam.add(%name,lstSBotCreateTeam.valcount++);
}

function clientcmdSBotsGUIBot(%name,%owner,%i)
{
 lstSBotManageList.addrow(%i,%name TAB %owner);
}

function clientcmdSBotsGUIPath(%name)
{
 if(lstSBotPathNew.getText() $= "")
 {
  lstSBotPathNew.setText(%name);
 }
 
 lstSBotPathNew.add(%name,lstSBotPathNew.valcount++);
}

function clientcmdSBotsIsAdmin(%admin,%sadmin,%host)
{
 //Editing the script to make these buttons work won't let you use them.
 //They are checked server-side too, don't try.
 butSBotManageClearTheirs.setActive((%admin || %sadmin || %host));
 butSBotManageClearAll.setActive((%sadmin || %host));
 butSBotManageLimits.setActive(%host);
 butSBotSaveLoad.setActive((%admin || %sadmin || %host));
}

function SpaceBots::PathNew()
{
 %txt = txtSBotPathNew.getValue();
 txtSBotPathNew.setValue("");
 lstSBotPathNew.setText(%txt); //So it shows up when you open the GUI again
 commandtoserver('botpathpoint',%txt);
 canvas.popDialog(SBotsGUI);
}

function SpaceBots::PathPoint()
{
 if(trim(lstSBotPathNew.getText()) !$= "")
 {
  commandtoserver('botpathpoint',lstSBotPathNew.getText());
  canvas.popDialog(SBotsGUI);
 }
}

function SpaceBots::PathShow()
{
 if(trim(lstSBotPathNew.getText()) !$= "")
 {
  commandtoserver('botshowpath',lstSBotPathNew.getText());
  canvas.popDialog(SBotsGUI);
 }
}

function SpaceBots::PathDelete()
{
 if(trim(lstSBotPathNew.getText()) !$= "")
 {
  commandtoserver('botclearpath',lstSBotPathNew.getText());
  lstSBotPathNew.setText("");
  canvas.popDialog(SBotsGUI);
 }
}

function SpaceBots::PathDeleteAll()
{
 if(trim(lstSBotPathNew.getText()) !$= "") //You have at least one path
 {
  commandtoserver('botclearallpaths');
  lstSBotPathNew.setText("");
  canvas.popDialog(SBotsGUI);
 }
}

function SpaceBots::ManageDeleteOne()
{
 commandtoserver('GUIDeleteBot',lstSBotManageList.getSelectedID(),getField(lstSBotManageList.getRowText(lstSBotManageList.getSelectedID()),0));
 lstSBotCreateAppPlayer.clear();
 lstSBotCreateAppPlayer.valcount = -1;

 %t = lstSBotCreateTeam.getText();
 lstSBotCreateTeam.clear();
 lstSBotCreateTeam.add("NONE",0);
 if(%t $= ""){lstSBotCreateTeam.setText("NONE");}else{lstSBotCreateTeam.setText(%t);}

 %t = lstSBotPathNew.gettext();
 lstSBotPathNew.clear();
 lstSBotPathNew.valCount = -1;
 lstSBotPathNew.setText(%t);

 lstSBotManageList.clear();
}

function SpaceBots::ManageClear()
{
 commandtoserver('clearbots');
 canvas.popDialog(SBotsGUI);
}

function SpaceBots::ManageClearAll()
{
 commandtoserver('clearallbots');
 canvas.popDialog(SBotsGUI);
}

function SpaceBots::ManageClearTheirs()
{
 commandtoserver('clearplayerbots',getField(lstSBotManageList.getRowText(lstSBotManageList.getSelectedID()),2));
 canvas.popDialog(SBotsGUI);
}

function SpaceBots::ManageLimits()
{
 if(chkBotManageMaxBPlayer.getValue()){%maxplayer = -1;}else{%maxplayer = txtSBotManageMaxBPlayer.getValue();}
 if(chkBotManageMaxBAdmin.getValue()){%maxadmin = -1;}else{%maxadmin = txtSBotManageMaxBAdmin.getValue();}
 if(chkBotManageMaxBSAdmin.getValue()){%maxsadmin = -1;}else{%maxsadmin = txtSBotManageMaxBSAdmin.getValue();}
 commandtoserver('GUIBotPrefs',%maxplayer TAB %maxadmin TAB %maxsadmin TAB txtSBotManageMaxAIPlayer.getValue() TAB txtSBotManageMaxAIAdmin.getValue() TAB txtSBotManageMaxAISAdmin.getValue());
 canvas.popDialog(SBotsGUI);
}

function SpaceBots::SaveSave()
{
 %fname = trim(txtSBotSaveName.getValue());
 if(%fname $= "") return;
 if(isFile("base/config/client/BotSaves/" @ %fname @ ".bsave"))
 {
  MessageBoxYesNo("Overwrite?","The file " @ %fname @ ".bsave already exists. Overwrite?","SpaceBots::SaveSave2(" @ %fname @ ");","");
 }
 else
 {
  SpaceBots::SaveSave2(%fname);
 }
}

function SpaceBots::SaveSave2(%fname)
{
 %file = new FileObject();
 %file.openForWrite("base/config/client/BotSaves/" @ %fname @ ".bsave");
 %botcount = 0;
 %brickcount = 0;
 if(isObject(MissionCleanup) && MissionCleanup.getCount() > 1)
 {
  %group = MissionCleanup;
  for(%i=0;%i<%group.getCount();%i++)
  {
   %o=%group.getobject(%i);
   if(%o.getclassname() $= "AIPlayer" && %o.clientSaveData2 !$= "")
   {
    %botcount++;
    %file.writeLine("BOT" TAB %o.getTransform() TAB %o.clientSaveData1);
    %file.writeLine("APP" TAB %o.clientSaveData2);
   }
  }
 }
 %group = ServerConnection;
 for(%i=0;%i<%group.getCount();%i++)
 {
  %o=%group.getobject(%i);
  if(%o.getclassname() $= "AIPlayer" && %o.clientSaveData2 !$= "")
  {
   %botcount++;
   %file.writeLine("BOT" TAB %o.getTransform() TAB %o.clientSaveData1);
   %file.writeLine("APP" TAB %o.clientSaveData2);
  }
  if(%o.getClassName() $= "fxDTSBrick" && %o.clientSaveData0 !$= "")
  {
   %brickcount++;
   %file.writeLine("SPAWN" TAB %o.getTransform() TAB %o.getangleID() TAB %o.getColorID() TAB %o.getColorFXID() TAB %o.getShapeFXID());
   %file.writeLine("DATA" TAB %o.clientSaveData0);
   %file.writeLine("APP" TAB %o.clientSaveData1);
  }
 }
 canvas.popDialog(SBotsGUI);
 if(%botcount == 0){%botStr = "No bots";}else if(%botcount == 1){%botStr = "\c31\c5 bot";}else{%botStr = "\c3" @ %botcount @ "\c5 bots";}
 if(%brickcount == 0){%brickStr = "no spawns";}else if(%brickcount == 1){%brickStr = "\c31\c5 spawn";}else{%brickStr = "\c3" @ %brickcount @ "\c5 spawns";}
 if(%botcount == 1 && %brickcount == 1){%otherstr = "was";}else{%otherstr = "were";}
 clientcmdcenterprint("\c5" @ %botStr @ " and " @ %brickStr @ " " @ %otherstr @ " saved.",2,3);
 %file.close();
 %file.delete();
 updateBotLoadMenu();
}

function SpaceBots::SaveLoad()
{
 %fname = trim(lstSBotSaveLoadList.getText());
 if(%fname $= "") return;
 if(!isFile("base/config/client/BotSaves/" @ %fname @ ".bsave")) return;
 %f = new FileObject();
 %f.openForRead("base/config/client/BotSaves/" @ %fname @ ".bsave");
 %l = %f.readLine();
 if(%l $= ""){%f.close();%f.delete();return;}
 
 if(isObject($SpaceBots::Client::LoadObject)){$SpaceBots::Client::LoadObject.delete();}
 $SpaceBots::Client::LoadObject = new ScriptObject();
 $SpaceBots::Client::LoadLine = -1;
 $SpaceBots::Client::LoadMax = -1;
 while(%l !$= "")
 {
  $SpaceBots::Client::LoadObject.line[$SpaceBots::Client::LoadMax++] = %l;
  %l = %f.readLine();
 }
 %f.close();
 %f.delete();
 commandtoserver('LoadBotSave');
 canvas.popDialog(SBotsGUI);
}

function clientcmdbotLoadNext()
{
 if($SpaceBots::Client::LoadLine < $SpaceBots::Client::LoadMax)
 {
  commandtoserver('botLoadLine',$SpaceBots::Client::LoadObject.line[$SpaceBots::Client::LoadLine++]);
  %a = $SpaceBots::Client::LoadLine;
  %b = $SpaceBots::Client::LoadMax;
  if(%b <= 0){return;}
  %str = "\c2";
  %fin = (%a / %b) * 65;
  for(%i=0;%i<=65;%i++)
  {
   if(%i >= %fin && %done $= ""){%str = %str @ "\c0";%done = "1";}
   %str = %str @ "|";
  }
  clientcmdBottomPrint(%str,1,3);
 }
 else
 {
  commandtoserver('botLoadFin');
  %str = "\c4";
  for(%i=0;%i<=65;%i++)
  {
   %str = %str @ "|";
  }
  clientcmdBottomPrint(%str,1,3);
 }
}

function updateBotLoadMenu()
{
 //setModPaths(getModPaths());
 lstSBotSaveLoadList.clear();
 %c = -1;
 %path = findFirstFile("base/config/client/BotSaves/*.bsave");
 while(%path !$= "")
 {
  lstSBotSaveLoadList.add(fileBase(%path),%c++);
  %path = findNextFile("base/config/client/BotSaves/*.bsave");
 }
}

function clientcmdSBotsGUIManageLimits(%fields)
{
 %a = getField(%fields,0);
 if(%a == -1)
 {
  chkBotManageMaxBPlayer.setValue(1);
  txtSBotManageMaxBPlayer.setValue(0);
 }
 else
 {
  chkBotManageMaxBPlayer.setValue(0);
  txtSBotManageMaxBPlayer.setValue(%a);
 }
 %a = getField(%fields,1);
 if(%a == -1)
 {
  chkBotManageMaxBAdmin.setValue(1);
  txtSBotManageMaxBAdmin.setValue(0);
 }
 else
 {
  chkBotManageMaxBAdmin.setValue(0);
  txtSBotManageMaxBAdmin.setValue(%a);
 }
 %a = getField(%fields,2);
 if(%a == -1)
 {
  chkBotManageMaxBSAdmin.setValue(1);
  txtSBotManageMaxBSAdmin.setValue(0);
 }
 else
 {
  chkBotManageMaxBSAdmin.setValue(0);
  txtSBotManageMaxBSAdmin.setValue(%a);
 }
 txtSBotManageMaxAIPlayer.setValue(getField(%fields,3));
 txtSBotManageMaxAIAdmin.setValue(getField(%fields,4));
 txtSBotManageMaxAISAdmin.setValue(getField(%fields,5));
}

package SBotsClient
{
 function guipopupmenuctrl::onSelect(%this,%num,%text)
 {
  Parent::onSelect(%this,%num,%text);
  %a = %this.getname();
  if(%a $= "lstSBotCreateAI")
  {
   %frame = fraSBotCreate;
   %add = "0 0";
   %copyNames = lstSBotCreateAppPlayer;
   %copyPaths = lstSBotPathNew;
  }
  if(%a $= "lstSBotSpawnAI")
  {
   %frame = SBotSpawnWindow;
   %add = "8 31";
   %copyNames = lstSBotSpawnAppPlayer;
   %copyPaths = lstSBotSpawnPath;
  }
  if(isObject(%frame))
  {
   for(%i=0;isObject(%this.ctrl[%i]);%i++)
   {
    %this.ctrl[%i].delete();
   }
   %ctrl = 0;
   for(%i=0;(%txt = getField($SpaceBots::Client::BehaviourList,%i)) !$= "";%i++) 
   {
    if(%txt $= getField(%text,0)){%ctrl = getField($SpaceBots::Client::BehaviourControls,%i);break;}
   }
   switch$(%ctrl)
   {
    case 0: //Line
      %this.ctrl0 = new GuiBitmapCtrl() {
         profile = "GuiDefaultProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = vectorAdd("40 43",%add);
         extent = "200 2";
         minExtent = "8 2";
         visible = "1";
         wrap = "0";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         keepCached = "0";
      };%frame.add(%this.ctrl0);

    case 1: //Player List
     %this.ctrl0 = new GuiTextCtrl() {
         profile = "GuiDefaultProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = vectorAdd("0 36",%add);
         extent = "52 18";
         minExtent = "8 2";
         visible = "1";
         text = "Player:";
         maxLength = "255";
      };%frame.add(%this.ctrl0);
      %this.ctrl1 = new GuiPopUpMenuCtrl() {
         profile = "GuiPopUpMenuProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = vectorAdd("110 37",%add);
         extent = "100 18";
         minExtent = "8 2";
         visible = "1";
         maxLength = "255";
         maxPopupHeight = "200";
      };%frame.add(%this.ctrl1);
      for(%i=0;(%txt = %copynames.getTextByID(%i)) !$= "";%i++){%this.ctrl1.add(%txt,%i);}
      if(getFields(%text,1) $= "")
       %this.ctrl1.settext(%copynames.getTextByID(0));
      else
       %this.ctrl1.settext(getFields(%text,1));

    case 2: //Path List
     %this.ctrl0 = new GuiTextCtrl() {
         profile = "GuiDefaultProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = vectorAdd("0 36",%add);
         extent = "52 18";
         minExtent = "8 2";
         visible = "1";
         text = "Path:";
         maxLength = "255";
      };%frame.add(%this.ctrl0);
      %this.ctrl1 = new GuiPopUpMenuCtrl() {
         profile = "GuiPopUpMenuProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = vectorAdd("110 37",%add);
         extent = "100 18";
         minExtent = "8 2";
         visible = "1";
         maxLength = "255";
         maxPopupHeight = "200";
      };%frame.add(%this.ctrl1);
      for(%i=0;(%txt = %copypaths.getTextByID(%i)) !$= "";%i++){%this.ctrl1.add(%txt,%i);}
      if(getFields(%text,1) $= "")
       %this.ctrl1.settext(%copypaths.getTextByID(0));
      else
       %this.ctrl1.settext(getFields(%text,1));

    case 3: //Text Box
     %this.ctrl0 = new GuiTextCtrl() {
         profile = "GuiDefaultProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = vectorAdd("0 36",%add);
         extent = "52 18";
         minExtent = "8 2";
         visible = "1";
         text = "Line:";
         maxLength = "255";
      };%frame.add(%this.ctrl0);
     %this.ctrl1 = new GuiTextEditCtrl() {
         profile = "GuiTextEditProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = vectorAdd("70 37",%add);
         extent = "210 18";
         minExtent = "8 2";
         visible = "1";
         maxLength = "255";
         maxPopupHeight = "200";
      };%frame.add(%this.ctrl1);%this.ctrl1.setValue(getFields(%text,1));
   }
  }
 }
 function PlayGUI::onWake(%this)
 {
  Parent::onWake(%this);
  $SpaceBots::Client::ServerHasMod = 0;
  commandtoserver('SBotsInit');
  SpaceBots::ClearAllGUI();
 }
 function Armor::onAdd(%this,%a,%b,%c)
 {
  Parent::onAdd(%this,%a,%b,%c);
  if(!isObject($SpaceBots::Client::ObjectStorer)){$SpaceBots::Client::ObjectStorer = new ScriptObject();}
  $SpaceBots::Client::ObjectStorer.botposition[%a.getPosition()] = %a;
 }
 function clientcmdSBotInformation(%pos,%d1,%d2,%d3,%d4)
 {
  %bot = $SpaceBots::Client::ObjectStorer.botposition[%pos];
  if(!isObject(%bot)){return;}
  %bot.clientSaveData1 = %d1;
  %bot.clientSaveData2 = %d2;
 }
 function clientcmdBotSpawnInformation(%pos,%num,%d1)
 {
  initClientBrickSearch(%pos,"0.1 0.1 0.1");
  %brick = clientbrickSearchNext();
  if(isObject(%brick))
  {
   %brick.clientSaveData[%num] = %d1;
  }
 }
};activatepackage(SBotsClient);