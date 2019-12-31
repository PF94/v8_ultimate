if(!isObject(NewTeamDMGui))
{
//----GUI File----\\
new GuiControl(NewTeamDMGui) {
   profile = "GuiDefaultProfile";
   horizSizing = "right";
   vertSizing = "bottom";
   position = "0 0";
   extent = "640 480";
   minExtent = "8 2";
   visible = "1";

   new GuiWindowCtrl() {
      profile = "BlockWindowProfile";
      horizSizing = "center";
      vertSizing = "center";
      position = "107 10";
      extent = "425 435";
      minExtent = "8 2";
      visible = "1";
      command = "canvas.popDialog(newteamDMgui);";
      accelerator = "escape";
      text = "Team Deathmatch Settings";
      maxLength = "255";
      resizeWidth = "0";
      resizeHeight = "0";
      canMove = "1";
      canClose = "1";
      canMinimize = "0";
      canMaximize = "0";
      minSize = "50 50";
      closeCommand = "canvas.popDialog(newteamDMgui);";
         helpTag = "0";

      new GuiSwatchCtrl() {
         profile = "GuiDefaultProfile";
         horizSizing = "width";
         vertSizing = "height";
         position = "14 52";
         extent = "186 186";
         minExtent = "8 2";
         visible = "1";
         color = "255 255 255 128";
      };
      new GuiTextCtrl() {
         profile = "GuiTextProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "99 32";
         extent = "18 18";
         minExtent = "8 2";
         visible = "1";
         text = "Mini";
         maxLength = "255";
      };
      new GuiBitmapButtonCtrl(butTDMsetTeam) {
         profile = "BlockButtonProfile";
         horizSizing = "left";
         vertSizing = "bottom";
         position = "205 52";
         extent = "90 19";
         minExtent = "8 2";
         visible = "1";
         command = "TDM_SetTeam();";
         text = "Set Team";
         groupNum = "-1";
         buttonType = "PushButton";
         bitmap = "base/client/ui/button1";
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
         position = "18 32";
         extent = "30 18";
         minExtent = "8 2";
         visible = "1";
         text = "Name ";
         maxLength = "255";
      };
      new GuiScrollCtrl() {
         profile = "ColorScrollProfile";
         horizSizing = "width";
         vertSizing = "height";
         position = "14 52";
         extent = "186 187";
         minExtent = "8 2";
         visible = "1";
         willFirstRespond = "1";
         hScrollBar = "alwaysOff";
         vScrollBar = "alwaysOn";
         constantThumbHeight = "0";
         childMargin = "0 0";
         rowHeight = "10";
         columnWidth = "30";

         new GuiTextListCtrl(lstTDMPlayerList) {
            profile = "GuiTextListProfile";
            horizSizing = "left";
            vertSizing = "height";
            position = "0 0";
            extent = "170 2";
            minExtent = "8 2";
            visible = "1";
            enumerate = "0";
            resizeCell = "0";
            columns = "0 88 116 156";
            fitParentWidth = "1";
            clipColumnText = "0";
               helpTag = "0";
         };
      };
      new GuiSwatchCtrl() {
         profile = "GuiDefaultProfile";
         horizSizing = "width";
         vertSizing = "height";
         position = "298 52";
         extent = "120 187";
         minExtent = "8 2";
         visible = "1";
         color = "255 255 255 128";
      };
      new GuiTextCtrl() {
         profile = "GuiTextProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "126 32";
         extent = "26 18";
         minExtent = "8 2";
         visible = "1";
         text = "Team";
         maxLength = "255";
      };
      new GuiTextCtrl() {
         profile = "GuiTextProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "166 32";
         extent = "24 18";
         minExtent = "8 2";
         visible = "1";
         text = "Lead";
         maxLength = "255";
      };
      new GuiScrollCtrl() {
         profile = "ColorScrollProfile";
         horizSizing = "width";
         vertSizing = "height";
         position = "298 52";
         extent = "120 187";
         minExtent = "8 2";
         visible = "1";
         willFirstRespond = "1";
         hScrollBar = "alwaysOff";
         vScrollBar = "alwaysOn";
         constantThumbHeight = "0";
         childMargin = "0 0";
         rowHeight = "10";
         columnWidth = "30";

         new GuiTextListCtrl(lstTDMTeamList) {
            profile = "GuiTextListProfile";
            horizSizing = "left";
            vertSizing = "height";
            position = "0 0";
            extent = "104 2";
            minExtent = "8 2";
            visible = "1";
            enumerate = "0";
            resizeCell = "0";
            columns = "0 120";
            fitParentWidth = "1";
            clipColumnText = "0";
               helpTag = "0";
         };
      };
      new GuiBitmapButtonCtrl(butTDMsetLead) {
         profile = "BlockButtonProfile";
         horizSizing = "left";
         vertSizing = "bottom";
         position = "205 73";
         extent = "90 19";
         minExtent = "8 2";
         visible = "1";
         command = "TDM_SetLead();";
         text = "Set Leader";
         groupNum = "-1";
         buttonType = "PushButton";
         bitmap = "base/client/ui/button1";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         mKeepCached = "0";
         mColor = "255 255 255 255";
      };
      new GuiBitmapButtonCtrl(butTDMsortTeams) {
         profile = "BlockButtonProfile";
         horizSizing = "left";
         vertSizing = "bottom";
         position = "205 94";
         extent = "90 19";
         minExtent = "8 2";
         visible = "1";
         command = "TDM_SortTeams();";
         text = "Sort Players";
         groupNum = "-1";
         buttonType = "PushButton";
         bitmap = "base/client/ui/button1";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         mKeepCached = "0";
         mColor = "255 255 255 255";
      };
      new GuiBitmapButtonCtrl(butTDMAutoSort) {
         profile = "BlockButtonProfile";
         horizSizing = "left";
         vertSizing = "bottom";
         position = "205 115";
         extent = "90 19";
         minExtent = "8 2";
         visible = "1";
         command = "TDM_AutoSort();";
         text = "Auto Sort";
         groupNum = "-1";
         buttonType = "PushButton";
         bitmap = "base/client/ui/button1";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         mKeepCached = "0";
         mColor = "255 255 255 128";
      };
      new GuiBitmapButtonCtrl(butTDMUniform) {
         profile = "BlockButtonProfile";
         horizSizing = "left";
         vertSizing = "bottom";
         position = "205 136";
         extent = "90 19";
         minExtent = "8 2";
         visible = "1";
         command = "TDM_Uniform();";
         text = "Uniform";
         groupNum = "-1";
         buttonType = "PushButton";
         bitmap = "base/client/ui/button1";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         mKeepCached = "0";
         mColor = "255 255 255 255";
      };
      new GuiBitmapButtonCtrl(butTDMFFire) {
         profile = "BlockButtonProfile";
         horizSizing = "left";
         vertSizing = "bottom";
         position = "205 178";
         extent = "90 19";
         minExtent = "8 2";
         visible = "1";
         command = "TDM_FFire();";
         text = "Friendly Fire";
         groupNum = "-1";
         buttonType = "PushButton";
         bitmap = "base/client/ui/button1";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         mKeepCached = "0";
         mColor = "255 255 255 255";
      };
      new GuiBitmapButtonCtrl(butTDMEditTeam) {
         profile = "BlockButtonProfile";
         horizSizing = "left";
         vertSizing = "bottom";
         position = "205 220";
         extent = "90 19";
         minExtent = "8 2";
         visible = "1";
         command = "TDM_EditTeam();";
         text = "Edit -->";
         groupNum = "-1";
         buttonType = "PushButton";
         bitmap = "base/client/ui/button1";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         mKeepCached = "0";
         mColor = "255 255 255 255";
      };
      new GuiBitmapButtonCtrl(butTDMTeamChat) {
         profile = "BlockButtonProfile";
         horizSizing = "left";
         vertSizing = "bottom";
         position = "205 157";
         extent = "90 19";
         minExtent = "8 2";
         visible = "1";
         command = "TDM_TeamChat();";
         text = "Team Chat";
         groupNum = "-1";
         buttonType = "PushButton";
         bitmap = "base/client/ui/button1";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         mKeepCached = "0";
         mColor = "255 255 255 255";
      };
      new GuiBitmapButtonCtrl(butTDMLockteams) {
         profile = "BlockButtonProfile";
         horizSizing = "left";
         vertSizing = "bottom";
         position = "205 199";
         extent = "90 19";
         minExtent = "8 2";
         visible = "1";
         command = "TDM_LockTeams();";
         text = "Lock Teams";
         groupNum = "-1";
         buttonType = "PushButton";
         bitmap = "base/client/ui/button1";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         mKeepCached = "0";
         mColor = "255 255 255 128";
      };
      new GuiPopUpMenuCtrl(popTDMColorMenu) {
         profile = "GuiPopUpMenuProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "328 262";
         extent = "90 21";
         minExtent = "8 2";
         visible = "1";
         maxLength = "255";
         maxPopupHeight = "200";
      };
      new GuiBitmapButtonCtrl(butTDMNewTeam) {
         profile = "BlockButtonProfile";
         horizSizing = "left";
         vertSizing = "bottom";
         position = "204 242";
         extent = "90 19";
         minExtent = "8 2";
         visible = "1";
         command = "TDM_NewTeam();";
         text = "New Team";
         groupNum = "-1";
         buttonType = "PushButton";
         bitmap = "base/client/ui/button1";
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
         position = "298 264";
         extent = "25 18";
         minExtent = "8 2";
         visible = "1";
         text = "Color";
         maxLength = "255";
      };
      new GuiTextEditCtrl(txtTDMTeamName) {
         profile = "GuiTextEditProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "298 242";
         extent = "120 18";
         minExtent = "8 2";
         visible = "1";
         maxLength = "255";
         historySize = "0";
         password = "0";
         tabComplete = "0";
         sinkAllKeyEvents = "0";
      };
      new GuiBitmapButtonCtrl(butTDMStart) {
         profile = "BlockButtonProfile";
         horizSizing = "left";
         vertSizing = "bottom";
         position = "44 243";
         extent = "90 40";
         minExtent = "8 2";
         visible = "1";
         command = "TDM_Start();";
         text = "Start";
         groupNum = "-1";
         buttonType = "PushButton";
         bitmap = "base/client/ui/button2";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         mKeepCached = "0";
         mColor = "0 255 0 255";
      };
      new GuiBitmapButtonCtrl(butTDMEnd) {
         profile = "BlockButtonProfile";
         horizSizing = "left";
         vertSizing = "bottom";
         position = "44 243";
         extent = "90 40";
         minExtent = "8 2";
         visible = "1";
         command = "TDM_End();";
         text = "End";
         groupNum = "-1";
         buttonType = "PushButton";
         bitmap = "base/client/ui/button2";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         mKeepCached = "0";
         mColor = "255 0 0 255";
      };
      new GuiTextCtrl() {
         profile = "GuiTextProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "301 32";
         extent = "30 18";
         minExtent = "8 2";
         visible = "1";
         text = "Name ";
         maxLength = "255";
      };
      new GuiMLTextCtrl() {
         profile = "GuiMLTextProfile";
         horizSizing = "left";
         vertSizing = "bottom";
         position = "-2 388";
         extent = "185 28";
         minExtent = "8 2";
         visible = "1";
         lineSpacing = "2";
         allowColorChars = "0";
         maxChars = "-1";
         text = "       Team Deathmatch Mod (v4.0)\n                    By Space Guy";
         maxBitmapHeight = "-1";
      };
      new GuiSwatchCtrl() {
         profile = "GuiDefaultProfile";
         horizSizing = "width";
         vertSizing = "height";
         position = "174 329";
         extent = "155 97";
         minExtent = "8 2";
         visible = "1";
         color = "255 255 255 128";
      };
      new GuiTextCtrl() {
         profile = "EditorTextProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "203 293";
         extent = "97 18";
         minExtent = "8 2";
         visible = "1";
         text = "Package Manager";
         maxLength = "255";
      };
      new GuiScrollCtrl() {
         profile = "ColorScrollProfile";
         horizSizing = "width";
         vertSizing = "height";
         position = "174 329";
         extent = "155 97";
         minExtent = "8 2";
         visible = "1";
         willFirstRespond = "1";
         hScrollBar = "alwaysOff";
         vScrollBar = "alwaysOn";
         constantThumbHeight = "0";
         childMargin = "0 0";
         rowHeight = "10";
         columnWidth = "30";

         new GuiTextListCtrl(lstTDMPackageList) {
            profile = "GuiTextListProfile";
            horizSizing = "left";
            vertSizing = "height";
            position = "0 0";
            extent = "139 2";
            minExtent = "8 2";
            visible = "1";
            enumerate = "0";
            resizeCell = "0";
            columns = "0 127";
            fitParentWidth = "1";
            clipColumnText = "0";
               helpTag = "0";
         };
      };
      new GuiTextCtrl() {
         profile = "GuiTextProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "175 309";
         extent = "30 18";
         minExtent = "8 2";
         visible = "1";
         text = "Name ";
         maxLength = "255";
      };
      new GuiBitmapButtonCtrl(butTDMpackToggle) {
         profile = "BlockButtonProfile";
         horizSizing = "left";
         vertSizing = "bottom";
         position = "333 329";
         extent = "86 19";
         minExtent = "8 2";
         visible = "1";
         command = "TDM_TogglePackage();";
         text = "Toggle";
         groupNum = "-1";
         buttonType = "PushButton";
         bitmap = "base/client/ui/button1";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         mKeepCached = "0";
         mColor = "255 255 255 255";
      };
      new GuiBitmapButtonCtrl(butTDMpackInfo) {
         profile = "BlockButtonProfile";
         horizSizing = "left";
         vertSizing = "bottom";
         position = "332 350";
         extent = "90 19";
         minExtent = "8 2";
         visible = "1";
         command = "TDM_PackageInfo();";
         text = "Info";
         groupNum = "-1";
         buttonType = "PushButton";
         bitmap = "base/client/ui/button1";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         mKeepCached = "0";
         mColor = "255 255 255 255";
      };
      new GuiBitmapCtrl() {
         profile = "GuiDefaultProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "11 380";
         extent = "158 44";
         minExtent = "8 2";
         visible = "1";
         wrap = "0";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         keepCached = "0";
      };
      new GuiBitmapButtonCtrl(butTDMNewTeam) {
         profile = "BlockButtonProfile";
         horizSizing = "left";
         vertSizing = "bottom";
         position = "103 319";
         extent = "67 19";
         minExtent = "8 2";
         visible = "1";
         command = "TDM_Load(txtTDMLoadName.getValue());";
         text = "Upload";
         groupNum = "-1";
         buttonType = "PushButton";
         bitmap = "base/client/ui/button1";
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
         position = "14 299";
         extent = "146 18";
         minExtent = "8 2";
         visible = "1";
         text = "Save and Load Team/Spawns";
         maxLength = "255";
      };
      new GuiTextEditCtrl(txtTDMLoadName) {
         profile = "GuiTextEditProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "13 319";
         extent = "86 18";
         minExtent = "8 2";
         visible = "1";
         maxLength = "255";
         historySize = "0";
         password = "0";
         tabComplete = "0";
         sinkAllKeyEvents = "0";
      };
      new GuiBitmapButtonCtrl(butTDMNewTeam) {
         profile = "BlockButtonProfile";
         horizSizing = "left";
         vertSizing = "bottom";
         position = "103 340";
         extent = "67 19";
         minExtent = "8 2";
         visible = "1";
         command = "TDM_Save(txtTDMSaveName.getValue());";
         text = "Save";
         groupNum = "-1";
         buttonType = "PushButton";
         bitmap = "base/client/ui/button1";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         mKeepCached = "0";
         mColor = "255 255 255 255";
      };
      new GuiTextEditCtrl(txtTDMSaveName) {
         profile = "GuiTextEditProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "13 341";
         extent = "86 18";
         minExtent = "8 2";
         visible = "1";
         maxLength = "255";
         historySize = "0";
         password = "0";
         tabComplete = "0";
         sinkAllKeyEvents = "0";
      };
      new GuiBitmapButtonCtrl(butTDMClearTeams) {
         profile = "BlockButtonProfile";
         horizSizing = "left";
         vertSizing = "bottom";
         position = "304 287";
         extent = "114 21";
         minExtent = "8 2";
         visible = "1";
         command = "TDM_ClearTeams();";
         text = "Clear All Teams";
         groupNum = "-1";
         buttonType = "PushButton";
         bitmap = "base/client/ui/button1";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         mKeepCached = "0";
         mColor = "255 0 0 255";
      };
      new GuiBitmapButtonCtrl(butTDMDefMini) {
         profile = "BlockButtonProfile";
         horizSizing = "left";
         vertSizing = "bottom";
         position = "244 264";
         extent = "51 19";
         minExtent = "8 2";
         visible = "1";
         command = "TDM_DefaultMini();";
         text = "Off";
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
         position = "160 264";
         extent = "81 18";
         minExtent = "8 2";
         visible = "1";
         text = "Default Minigame";
         maxLength = "255";
      };
   };
};

//--Edit Team dialog
new GuiControl(TeamEditGui) {
   profile = "GuiDefaultProfile";
   horizSizing = "right";
   vertSizing = "bottom";
   position = "0 0";
   extent = "640 480";
   minExtent = "8 2";
   visible = "1";

   new GuiWindowCtrl(teamEditWindow) {
      profile = "BlockWindowProfile";
      horizSizing = "center";
      vertSizing = "center";
      position = "170 131";
      extent = "300 217";
      minExtent = "8 2";
      visible = "1";
      text = "*Team Name*";
      maxLength = "255";
      resizeWidth = "0";
      resizeHeight = "0";
      canMove = "1";
      canClose = "1";
      canMinimize = "0";
      canMaximize = "0";
      minSize = "50 50";
      closeCommand = "canvas.popDialog(\"teamEditGUI\");";

      new GuiBitmapCtrl() {
         profile = "GuiDefaultProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "174 142";
         extent = "120 2";
         minExtent = "8 2";
         visible = "1";
         wrap = "0";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         keepCached = "0";
      };
      new GuiBitmapButtonCtrl() {
         profile = "BlockButtonProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "201 171";
         extent = "91 38";
         minExtent = "8 2";
         visible = "1";
         command = "TDM_TeamEdit();";
         altCommand = "//\\";
         text = "OK";
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
         horizSizing = "center";
         vertSizing = "bottom";
         position = "104 171";
         extent = "91 38";
         minExtent = "8 2";
         visible = "1";
         command = "TDM_TeamDisband();";
         accelerator = "";
         text = "Disband";
         groupNum = "-1";
         buttonType = "PushButton";
         bitmap = "base/client/ui/button2";
         lockAspectRatio = "0";
         alignLeft = "0";
         overflowImage = "0";
         mKeepCached = "0";
         mColor = "255 0 0 255";
      };
      new GuiBitmapButtonCtrl() {
         profile = "BlockButtonProfile";
         horizSizing = "left";
         vertSizing = "bottom";
         position = "10 171";
         extent = "91 38";
         minExtent = "8 2";
         visible = "1";
         command = "canvas.popDialog(TeamEditGUI);";
         accelerator = "escape";
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
         horizSizing = "left";
         vertSizing = "bottom";
         position = "9 124";
         extent = "159 21";
         minExtent = "8 2";
         visible = "1";
         command = "TDM_UniMember();";
         text = "Set Member Uniform";
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
         profile = "BlockButtonProfile";
         horizSizing = "left";
         vertSizing = "bottom";
         position = "9 102";
         extent = "159 21";
         minExtent = "8 2";
         visible = "1";
         command = "TDM_UniLeader();";
         text = "Set Leader Uniform";
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
         profile = "BlockButtonProfile";
         horizSizing = "left";
         vertSizing = "bottom";
         position = "9 68";
         extent = "159 21";
         minExtent = "8 2";
         visible = "1";
         command = "TDM_UniDefault();";
         text = "Default Uniforms";
         groupNum = "-1";
         buttonType = "PushButton";
         bitmap = "base/client/ui/button1";
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
         position = "34 146";
         extent = "116 18";
         minExtent = "8 2";
         visible = "1";
         text = "(to current appearance)";
         maxLength = "255";
            helpTag = "0";
      };
      new GuiTextEditCtrl(txtEditTeamName) {
         profile = "GuiTextEditProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "42 30";
         extent = "113 18";
         minExtent = "8 2";
         visible = "1";
         accelerator = "return";
         maxLength = "255";
         historySize = "0";
         password = "0";
         tabComplete = "0";
         sinkAllKeyEvents = "0";
            helpTag = "0";
      };
      new GuiTextCtrl() {
         profile = "GuiTextProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "9 30";
         extent = "30 18";
         minExtent = "8 2";
         visible = "1";
         text = "Name:";
         maxLength = "255";
            helpTag = "0";
      };
      new GuiMLTextCtrl() {
         profile = "LoadingBarTextProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "52 51";
         extent = "114 14";
         minExtent = "8 2";
         visible = "1";
         lineSpacing = "2";
         allowColorChars = "0";
         maxChars = "-1";
         text = "\c8 Team Uniform";
         maxBitmapHeight = "-1";
      };
      new GuiPopUpMenuCtrl(TeamEdit_Inv2) {
         profile = "BlockButtonProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "174 54";
         extent = "120 20";
         minExtent = "8 2";
         visible = "1";
         maxLength = "255";
         maxPopupHeight = "200";
      };
      new GuiPopUpMenuCtrl(TeamEdit_Inv3) {
         profile = "BlockButtonProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "174 75";
         extent = "120 20";
         minExtent = "8 2";
         visible = "1";
         maxLength = "255";
         maxPopupHeight = "200";
      };
      new GuiPopUpMenuCtrl(TeamEdit_Inv4) {
         profile = "BlockButtonProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "174 97";
         extent = "120 20";
         minExtent = "8 2";
         visible = "1";
         maxLength = "255";
         maxPopupHeight = "200";
      };
      new GuiPopUpMenuCtrl(TeamEdit_Inv5) {
         profile = "BlockButtonProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "174 119";
         extent = "120 20";
         minExtent = "8 2";
         visible = "1";
         maxLength = "255";
         maxPopupHeight = "200";
      };
      new GuiPopUpMenuCtrl(TeamEdit_PlayerData) {
         profile = "BlockButtonProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "174 147";
         extent = "120 20";
         minExtent = "8 2";
         visible = "1";
         maxLength = "255";
         maxPopupHeight = "200";
      };
      new GuiPopUpMenuCtrl(TeamEdit_Inv1) {
         profile = "BlockButtonProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "174 33";
         extent = "120 20";
         minExtent = "8 2";
         visible = "1";
         maxLength = "255";
         maxPopupHeight = "200";
      };
   };
};
}
//----Keybinds----\\
if (!$addedTDMMaps)
{
	$remapDivision[$remapCount] = "Team Deathmatch Mod";
	$remapName[$remapCount] = "Open GUI";
	$remapCmd[$remapCount] = "toggleTDMgui";
	$remapCount++;
	$addedTDMMaps = 1;
}
if(movemap.getBinding("toggleTDMGui") $= ""){movemap.bind("keyboard","ctrl m","","toggleTDMGUI");}
function toggleTDMgui(%val)
{
 if(%val)
  NewTeamDMGui.toggle();
}

//----GUI Functions----\\
function NewTeamDMGui::toggle(%this)
{
   if (%this.isAwake())
      Canvas.popDialog(%this);
   else
      Canvas.pushDialog(%this);
}

function NewTeamDMGui::onWake(%this)
{
 %this.getUpdates();
}

function NewTeamDMGui::clearAll(%this)
{
 $TeamDM::ClientPackages = -1;
 lstTDMPackageList.clear();
 lstTDMTeamList.clear();
 lstTDMPlayerList.clear();
 popTDMcolormenu.clear();
}

function NewTeamDMGui::getUpdates(%this)
{
 %this.clearAll();
 commandtoserver('TDMgetPackages');
 commandtoserver('TDMgetTeams');
 commandtoserver('TDMgetPlayers');
 commandtoserver('TDMgetColorList');
 commandtoserver('TDMgetEnabledList');
}

//----Button Functions----\\
//TDM_*name*()
//Type: Button Function
//Sends commands based on which button you push.
function TDM_SetTeam()
{
 commandtoserver('setTeamGUI',lstTDMTeamList.getSelectedID(),lstTDMPlayerList.getSelectedID());
 NewTeamDMGui.getUpdates();
}
function TDM_SetLead()
{
 commandtoserver('setLeadGUI',lstTDMTeamList.getSelectedID(),lstTDMPlayerList.getSelectedID());
 NewTeamDMGui.getUpdates();
}
function TDM_SortTeams()
{
 commandtoserver('sortTeams');
 NewTeamDMGui.getUpdates();
}
function TDM_AutoSort()
{
 commandtoserver('togAutoSort');
 NewTeamDMGui.getUpdates();
}
function TDM_Uniform()
{
 commandtoserver('togUniform');
 NewTeamDMGui.getUpdates();
}
function TDM_TeamChat()
{
 commandtoserver('togChat');
 NewTeamDMGui.getUpdates();
}
function TDM_FFire()
{
 commandtoserver('togFFire');
 NewTeamDMGui.getUpdates();
}
function TDM_EditTeam()
{
 commandtoserver('editTeam',lstTDMTeamList.getSelectedID());
}
function TDM_LockTeams()
{
 commandtoserver('togTeamLock');
 NewTeamDMGui.getUpdates();
}
function TDM_NewTeam()
{
 commandtoserver('addTeam',txtTDMTeamName.getValue(),popTDMcolormenu.getText());
 NewTeamDMGui.getUpdates();
}
function TDM_Start()
{
 commandtoserver('teamson');
 NewTeamDMGui.getUpdates();
}
function TDM_End()
{
 commandtoserver('teamsoff');
 NewTeamDMGui.getUpdates();
}
function TDM_TogglePackage()
{
 commandtoserver('startpackage',getPackage());
}
function TDM_PackageInfo()
{
 clientcmdMessageBoxOK("Package Info",$TeamDM::PackageInfo);
}
function TDM_TeamEdit()
{
 commandtoserver('editTeam2',  $TeamDM::EditingID,  txtEditTeamName.getValue(),  TeamEdit_Inv1.getText(),  TeamEdit_Inv2.getText(),  TeamEdit_Inv3.getText(),  TeamEdit_Inv4.getText(),  TeamEdit_Inv5.getText(),  TeamEdit_PlayerData.getText());
 NewTeamDMGui.getUpdates();
 canvas.popdialog(TeamEditGUI);
}
function TDM_UniDefault()
{
 commandtoserver('teamUniform',$TeamDM::EditingID,"DEFAULT");
 commandtoserver('leadUniform',$TeamDM::EditingID,"DEFAULT");
}
function TDM_UniLeader()
{
 commandtoserver('leadUniform',$TeamDM::EditingID,"CURRENT");
}
function TDM_UniMember()
{
 commandtoserver('teamUniform',$TeamDM::EditingID,"CURRENT");
}
function TDM_TeamDisband()
{
 commandtoserver('teamdisband',$TeamDM::EditingID);
 NewTeamDMGui.getUpdates();
 canvas.popdialog(TeamEditGUI);
}

function TDM_ClearTeams()
{
 commandtoserver('clearteams');
 NewTeamDMGui.getUpdates();
}

function TDM_DefaultMini()
{
 commandtoserver('toggleTeamMini');
 NewTeamDMGui.getUpdates();
}

//----Server to Client Communication----\\
function clientcmdButtonSet(%name,%value,%val2)
{
 switch(%value)
 {
  case 0: %name.setVisible(0); //Disabled by mod
  case 1: %name.setVisible(1);%name.mColor = "255 255 255 128"; //Turned off
  case 2: %name.setVisible(1);%name.mColor = "255 255 255 255"; //Turned on
  case 3: %name.setVisible(1);
  case 4: %name.setText(%val2);
 }
}
function clientCmdOpenTeamEdit(%title,%id,%ui0,%ui1,%ui2,%ui3,%ui4,%pl)
{
 canvas.pushDialog(TeamEditGUI);
 TeamEditWindow.setText(%title);
 txtEditTeamName.setText(%title);
 $TeamDM::EditingID = %id;
 TeamEdit_Inv1.setText(%ui0);
 TeamEdit_Inv2.setText(%ui1);
 TeamEdit_Inv3.setText(%ui2);
 TeamEdit_Inv4.setText(%ui3);
 TeamEdit_Inv5.setText(%ui4);
 TeamEdit_PlayerData.setText(%pl);
}
function clientcmdGUIset(%type,%obj,%text,%index)
{
	switch$(%type)
	{
		case "listClear": %obj.clear();
		case "listRow": %obj.addRow(%index, %text);
		case "textSet": %obj.setText(%text);
		case "popRow": %obj.add(%index,%text);
	}
}

function clientcmdtdmPackage(%name,%desc,%allow)
{
 lstTDMPackageList.addRow($TeamDM::ClientPackages++,%name);
 $TeamDM::PackageClientDesc[%name] = %desc;
 $TeamDM::PackageClientAllow[%name] = %allow;
}

function getPackage()
{
 %text = lstTDMPackageList.getSelectedID();
 %text = lstTDMPackageList.getRowTextByID(%text);
 return %text;
}

function lstTDMPackageList::onSelect(%this, %id, %text)
{
 $TeamDM::PackageInfo = $TeamDM::PackageClientDesc[getPackage()];
}

package TDMClientOverRide
{
 function clientcmdWrench_loadmenus()
 {
  Parent::clientcmdWrench_loadmenus();
  if(datablockGroup.getCount() > 2){%group = datablockGroup;}else{%group = serverConnection;}
  %count = 0;
  %count2 = 0;
  TeamEdit_Inv1.clear();TeamEdit_Inv2.clear();TeamEdit_Inv3.clear();TeamEdit_Inv4.clear();TeamEdit_Inv5.clear();TeamEdit_PlayerData.clear();
  TeamEdit_Inv1.setText("NONE");TeamEdit_Inv2.setText("NONE");TeamEdit_Inv3.setText("NONE");TeamEdit_Inv4.setText("NONE");TeamEdit_Inv5.setText("NONE");TeamEdit_PlayerData.setText("Standard Player");
  for(%i=0;%i<%group.getCount();%i++)
  {
   %test = %group.getObject(%i);
   if(%test.getClassName() $= "ItemData" && %test.uiname !$= "")
   {
    TeamEdit_Inv1.add(%test.uiname,%count);
    TeamEdit_Inv2.add(%test.uiname,%count);
    TeamEdit_Inv3.add(%test.uiname,%count);
    TeamEdit_Inv4.add(%test.uiname,%count);
    TeamEdit_Inv5.add(%test.uiname,%count);
    %count++;
   }
   if(%test.getClassName() $= "PlayerData" && %test.uiname !$= "")
   {
    TeamEdit_PlayerData.add(%test.uiname,%count2);
    %count2++;
   }
  }
 TeamEdit_Inv1.sort();TeamEdit_Inv2.sort();TeamEdit_Inv3.sort();TeamEdit_Inv4.sort();TeamEdit_Inv5.sort();TeamEdit_PlayerData.sort();
  TeamEdit_Inv1.add("NONE",0);TeamEdit_Inv2.add("NONE",0);TeamEdit_Inv3.add("NONE",0);TeamEdit_Inv4.add("NONE",0);TeamEdit_Inv5.add("NONE",0);
 }
};activatepackage(TDMClientOverRide);

function clientcmdTeamInfo(%team,%info)
{
 $TeamDM::SaveInfo[%team] = %info;
}

function clientcmdTeamGUICheck()
{
 commandtoserver('confirmTeamGUIcheck');
}

function clientcmdTeamUniformSet(%name,%part,%col)
{
 $TeamDM::saveUniform[%name,%part] = %col;
}

function TDM_Save(%filename)
{
 %filename = strReplace(%filename,".Tsave","");
 clientcmdServerMessage('',"\c5Writing to \c3" @ %filename @ ".Tsave\c5...");
 %file = new FileObject();
 %file.openforWrite("add-ons/TeamDMv4/Saves/" @ %filename @ ".Tsave");
 %file.writeline("// Teams - TEAM word1 [words...] color");
 for(%i=1;lstTDMTeamList.getRowText(%i) !$= "";%i++)
 {
  %team = lstTDMTeamList.getRowText(%i);
  %file.writeLine("TEAM" SPC %team);
  %file.writeLine("INFO" SPC $TeamDM::SaveInfo[getWords(lstTDMTeamList.getRowText(%i),0,getWordCount(lstTDMTeamList.getRowText(%i))-2)]);
   %list = "accent accentColor chest chestColor decalName faceName hat hatColor lhand lhandColor rhand rhandColor larm larmcolor rarm rarmcolor lleg llegcolor rleg rlegColor pack packColor secondpack";
   %list = %list SPC "secondPackColor headColor hip hipColor";
   for(%j=0;%j<getWordCount(%list);%j++)
   {
    %file.writeLine("UNI" SPC getWord(%list,%j) SPC $TeamDM::saveUniform[getWords(%team,0,getWordCount(%team)-2),getWord(%list,%j)]);
    %file.writeLine("UNI" SPC "LEAD" @ getWord(%list,%j) SPC $TeamDM::saveUniform[getWords(%team,0,getWordCount(%team)-2),"LEAD" @ getWord(%list,%j)]);
   }
 }
 %file.writeline("// Spawn Bricks - BRICK p1 p2 p3 r1 r2 r3 r4 c1 c2 c3 c4");
 for(%i=0;%i<ServerConnection.getCount();%i++)
 { 
  %obj = serverConnection.getobject(%i);
  if(%obj.getClassname() $= "fxDTSbrick" && %obj.getDatablock().uiname $= "Team DM Spawn")
  {
   %file.writeline("BRICK" SPC %obj.getTransform() SPC %obj.getangleID() SPC getColorIDTable(%obj.getColorID()));
  }
 }
 %file.writeline("// End");
 %file.close();%file.delete();
}

function TDM_Load(%filename)
{
 commandtoserver('initTDMupload',strReplace(%filename,".Tsave",""));
}

function clientcmdTDM_Load_Confirm(%filename)
{
 %open = new FileObject();
 %open.openforRead("Add-Ons/TeamDMv4/Saves/" @ %filename @ ".Tsave");
 %lines = -1;
 while(!%open.isEOF())
 {
  %line = %open.readline();
  %lines = %lines + 1;
  $lines[%lines] = %line;
  echo("Line Loaded - ", %line, " (",%lines,")");
 }
 %open.close();
 %open.delete();
 commandtoserver('TDMuploadline',%filename,$lines[0],0,%lines);
}

function clientcmdTDMUploadLineConf(%file,%curline,%numlines)
{
 commandtoserver('TDMuploadline',%file,$lines[%curline],%curline,%numlines);
}

function clientcmdClearLists()
{
 NewTeamDMGUI.clearAll();
}

package SaveOverwrite //Saved team spawns have no colour when loaded, so this disables their saving.
{
 function SaveBricks_WriteSingleBrick(%this,%brick)
 {
  if(%brick.getDatablock().uiname !$= "Team DM Spawn")
  return Parent::SaveBricks_WriteSingleBrick(%this,%brick);
 }
};activatepackage(SaveOverwrite);

function GameConnection::initialControlSet(%this)
{
   //echo ("*** Initial Control Object"); //Console spam fix. Unfortunantly I can't prevent this for everyone, just the server and those who have it.

   // The first control object has been set by the server
   // and we are now ready to go.
   
   // first check if the editor is active
      if (Canvas.getContent() != PlayGui.getId())
         Canvas.setContent(PlayGui);
}

package ChatFix
{
 function newChatSO::addLine(%this,%line)
 {
  Parent::addLine(%this,"<color:FF0000>" @ %line);
 }
};activatePackage(chatfix);