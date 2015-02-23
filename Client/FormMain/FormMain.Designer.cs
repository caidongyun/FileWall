using DevExpress.XtraEditors;


namespace VitaliiPianykh.FileWall.Client
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.buttonMain = new DevExpress.XtraBars.BarButtonItem();
            this.buttonShowRules = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.buttonCloseRules = new DevExpress.XtraBars.BarButtonItem();
            this.buttonRefreshRules = new DevExpress.XtraBars.BarButtonItem();
            this.buttonPreferences = new DevExpress.XtraBars.BarButtonItem();
            this.buttonClosePreferences = new DevExpress.XtraBars.BarButtonItem();
            this.buttonCloseEvents = new DevExpress.XtraBars.BarButtonItem();
            this.buttonRefreshEvents = new DevExpress.XtraBars.BarButtonItem();
            this.checkAutoRefreshEvents = new DevExpress.XtraBars.BarCheckItem();
            this.buttonClearEvents = new DevExpress.XtraBars.BarButtonItem();
            this.buttonShowEvents = new DevExpress.XtraBars.BarButtonItem();
            this.buttonSaveEvents = new DevExpress.XtraBars.BarButtonItem();
            this.buttonExitAndShutdown = new DevExpress.XtraBars.BarButtonItem();
            this.buttonShowEventDetails = new DevExpress.XtraBars.BarButtonItem();
            this.pageCategoryRules = new DevExpress.XtraBars.Ribbon.RibbonPageCategory();
            this.ribbonPageRules = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupRules = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupCloseRules = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.pageCategoryPreferences = new DevExpress.XtraBars.Ribbon.RibbonPageCategory();
            this.ribbonPagePreferences = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupClosePreferences = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.pageCategoryEvents = new DevExpress.XtraBars.Ribbon.RibbonPageCategory();
            this.ribbonPageEvents = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupRefresh = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupEvents = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupCloseEvents = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageMain = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.groupActions = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupExit = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.clientPanel = new DevExpress.XtraEditors.PanelControl();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemShow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemShutdown = new System.Windows.Forms.ToolStripMenuItem();
            this.barCheckItem1 = new DevExpress.XtraBars.BarCheckItem();
            this.dialogSaveEvents = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.clientPanel)).BeginInit();
            this.contextMenuTray.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ApplicationIcon = global::VitaliiPianykh.FileWall.Client.Properties.Resources.FileWall;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.buttonMain,
            this.buttonShowRules,
            this.barButtonItem1,
            this.buttonCloseRules,
            this.buttonRefreshRules,
            this.buttonPreferences,
            this.buttonClosePreferences,
            this.buttonCloseEvents,
            this.buttonRefreshEvents,
            this.checkAutoRefreshEvents,
            this.buttonClearEvents,
            this.buttonShowEvents,
            this.buttonSaveEvents,
            this.buttonExitAndShutdown,
            this.buttonShowEventDetails});
            resources.ApplyResources(this.ribbon, "ribbon");
            this.ribbon.MaxItemId = 33;
            this.ribbon.Name = "ribbon";
            this.ribbon.PageCategories.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageCategory[] {
            this.pageCategoryRules,
            this.pageCategoryPreferences,
            this.pageCategoryEvents});
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPageMain});
            this.ribbon.SelectedPage = this.ribbonPageMain;
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // buttonMain
            // 
            resources.ApplyResources(this.buttonMain, "buttonMain");
            this.buttonMain.Id = 7;
            this.buttonMain.LargeGlyph = global::VitaliiPianykh.FileWall.Client.Properties.Resources.FileWall;
            this.buttonMain.Name = "buttonMain";
            this.buttonMain.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonMain_ItemClick);
            // 
            // buttonShowRules
            // 
            resources.ApplyResources(this.buttonShowRules, "buttonShowRules");
            this.buttonShowRules.Id = 15;
            this.buttonShowRules.LargeGlyph = global::VitaliiPianykh.FileWall.Client.Properties.Resources.ShowRules;
            this.buttonShowRules.Name = "buttonShowRules";
            this.buttonShowRules.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonShowRules_ItemClick);
            // 
            // barButtonItem1
            // 
            resources.ApplyResources(this.barButtonItem1, "barButtonItem1");
            this.barButtonItem1.Id = 16;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // buttonCloseRules
            // 
            resources.ApplyResources(this.buttonCloseRules, "buttonCloseRules");
            this.buttonCloseRules.Id = 17;
            this.buttonCloseRules.LargeGlyph = global::VitaliiPianykh.FileWall.Client.Properties.Resources.Close;
            this.buttonCloseRules.Name = "buttonCloseRules";
            this.buttonCloseRules.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonClose_ItemClick);
            // 
            // buttonRefreshRules
            // 
            resources.ApplyResources(this.buttonRefreshRules, "buttonRefreshRules");
            this.buttonRefreshRules.Id = 18;
            this.buttonRefreshRules.LargeGlyph = global::VitaliiPianykh.FileWall.Client.Properties.Resources.RefreshRuleset;
            this.buttonRefreshRules.Name = "buttonRefreshRules";
            this.buttonRefreshRules.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonRefreshRules_ItemClick);
            // 
            // buttonPreferences
            // 
            resources.ApplyResources(this.buttonPreferences, "buttonPreferences");
            this.buttonPreferences.Id = 19;
            this.buttonPreferences.LargeGlyph = global::VitaliiPianykh.FileWall.Client.Properties.Resources.Preferences1;
            this.buttonPreferences.Name = "buttonPreferences";
            this.buttonPreferences.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonPreferences_ItemClick);
            // 
            // buttonClosePreferences
            // 
            resources.ApplyResources(this.buttonClosePreferences, "buttonClosePreferences");
            this.buttonClosePreferences.Id = 20;
            this.buttonClosePreferences.LargeGlyph = global::VitaliiPianykh.FileWall.Client.Properties.Resources.Close;
            this.buttonClosePreferences.Name = "buttonClosePreferences";
            this.buttonClosePreferences.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonClose_ItemClick);
            // 
            // buttonCloseEvents
            // 
            resources.ApplyResources(this.buttonCloseEvents, "buttonCloseEvents");
            this.buttonCloseEvents.Id = 21;
            this.buttonCloseEvents.LargeGlyph = global::VitaliiPianykh.FileWall.Client.Properties.Resources.Close;
            this.buttonCloseEvents.Name = "buttonCloseEvents";
            this.buttonCloseEvents.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonClose_ItemClick);
            // 
            // buttonRefreshEvents
            // 
            resources.ApplyResources(this.buttonRefreshEvents, "buttonRefreshEvents");
            this.buttonRefreshEvents.Glyph = global::VitaliiPianykh.FileWall.Client.Properties.Resources.RefreshEvents;
            this.buttonRefreshEvents.Id = 22;
            this.buttonRefreshEvents.Name = "buttonRefreshEvents";
            this.buttonRefreshEvents.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonRefreshEvents_ItemClick);
            // 
            // checkAutoRefreshEvents
            // 
            resources.ApplyResources(this.checkAutoRefreshEvents, "checkAutoRefreshEvents");
            this.checkAutoRefreshEvents.Id = 24;
            this.checkAutoRefreshEvents.Name = "checkAutoRefreshEvents";
            this.checkAutoRefreshEvents.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.checkAutoRefreshEvents_CheckedChanged);
            // 
            // buttonClearEvents
            // 
            resources.ApplyResources(this.buttonClearEvents, "buttonClearEvents");
            this.buttonClearEvents.Id = 25;
            this.buttonClearEvents.LargeGlyph = global::VitaliiPianykh.FileWall.Client.Properties.Resources.ClearEvents;
            this.buttonClearEvents.Name = "buttonClearEvents";
            this.buttonClearEvents.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonClearEvents_ItemClick);
            // 
            // buttonShowEvents
            // 
            resources.ApplyResources(this.buttonShowEvents, "buttonShowEvents");
            this.buttonShowEvents.Id = 27;
            this.buttonShowEvents.LargeGlyph = global::VitaliiPianykh.FileWall.Client.Properties.Resources.Events;
            this.buttonShowEvents.Name = "buttonShowEvents";
            this.buttonShowEvents.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonShowEvents_ItemClick);
            // 
            // buttonSaveEvents
            // 
            resources.ApplyResources(this.buttonSaveEvents, "buttonSaveEvents");
            this.buttonSaveEvents.Id = 28;
            this.buttonSaveEvents.LargeGlyph = global::VitaliiPianykh.FileWall.Client.Properties.Resources.SaveAs;
            this.buttonSaveEvents.Name = "buttonSaveEvents";
            this.buttonSaveEvents.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonSaveEvents_ItemClick);
            // 
            // buttonExitAndShutdown
            // 
            resources.ApplyResources(this.buttonExitAndShutdown, "buttonExitAndShutdown");
            this.buttonExitAndShutdown.Id = 30;
            this.buttonExitAndShutdown.LargeGlyph = global::VitaliiPianykh.FileWall.Client.Properties.Resources.Exit;
            this.buttonExitAndShutdown.Name = "buttonExitAndShutdown";
            this.buttonExitAndShutdown.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonExitAndShutdown_ItemClick);
            // 
            // buttonShowEventDetails
            // 
            resources.ApplyResources(this.buttonShowEventDetails, "buttonShowEventDetails");
            this.buttonShowEventDetails.Id = 32;
            this.buttonShowEventDetails.LargeGlyph = global::VitaliiPianykh.FileWall.Client.Properties.Resources.EventDetails;
            this.buttonShowEventDetails.Name = "buttonShowEventDetails";
            this.buttonShowEventDetails.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonShowEventDetail_ItemClick);
            // 
            // pageCategoryRules
            // 
            this.pageCategoryRules.Name = "pageCategoryRules";
            this.pageCategoryRules.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPageRules});
            resources.ApplyResources(this.pageCategoryRules, "pageCategoryRules");
            // 
            // ribbonPageRules
            // 
            this.ribbonPageRules.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupRules,
            this.ribbonPageGroupCloseRules});
            this.ribbonPageRules.Name = "ribbonPageRules";
            resources.ApplyResources(this.ribbonPageRules, "ribbonPageRules");
            this.ribbonPageRules.Visible = false;
            // 
            // ribbonPageGroupRules
            // 
            this.ribbonPageGroupRules.ItemLinks.Add(this.buttonRefreshRules);
            this.ribbonPageGroupRules.Name = "ribbonPageGroupRules";
            resources.ApplyResources(this.ribbonPageGroupRules, "ribbonPageGroupRules");
            // 
            // ribbonPageGroupCloseRules
            // 
            this.ribbonPageGroupCloseRules.ItemLinks.Add(this.buttonCloseRules);
            this.ribbonPageGroupCloseRules.Name = "ribbonPageGroupCloseRules";
            resources.ApplyResources(this.ribbonPageGroupCloseRules, "ribbonPageGroupCloseRules");
            // 
            // pageCategoryPreferences
            // 
            this.pageCategoryPreferences.Name = "pageCategoryPreferences";
            this.pageCategoryPreferences.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPagePreferences});
            resources.ApplyResources(this.pageCategoryPreferences, "pageCategoryPreferences");
            // 
            // ribbonPagePreferences
            // 
            this.ribbonPagePreferences.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupClosePreferences});
            this.ribbonPagePreferences.Name = "ribbonPagePreferences";
            resources.ApplyResources(this.ribbonPagePreferences, "ribbonPagePreferences");
            this.ribbonPagePreferences.Visible = false;
            // 
            // ribbonPageGroupClosePreferences
            // 
            this.ribbonPageGroupClosePreferences.ItemLinks.Add(this.buttonClosePreferences);
            this.ribbonPageGroupClosePreferences.Name = "ribbonPageGroupClosePreferences";
            resources.ApplyResources(this.ribbonPageGroupClosePreferences, "ribbonPageGroupClosePreferences");
            // 
            // pageCategoryEvents
            // 
            this.pageCategoryEvents.Name = "pageCategoryEvents";
            this.pageCategoryEvents.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPageEvents});
            resources.ApplyResources(this.pageCategoryEvents, "pageCategoryEvents");
            // 
            // ribbonPageEvents
            // 
            this.ribbonPageEvents.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupRefresh,
            this.ribbonPageGroupEvents,
            this.ribbonPageGroupCloseEvents});
            this.ribbonPageEvents.Name = "ribbonPageEvents";
            resources.ApplyResources(this.ribbonPageEvents, "ribbonPageEvents");
            this.ribbonPageEvents.Visible = false;
            // 
            // ribbonPageGroupRefresh
            // 
            this.ribbonPageGroupRefresh.ItemLinks.Add(this.buttonRefreshEvents);
            this.ribbonPageGroupRefresh.ItemLinks.Add(this.checkAutoRefreshEvents);
            this.ribbonPageGroupRefresh.Name = "ribbonPageGroupRefresh";
            resources.ApplyResources(this.ribbonPageGroupRefresh, "ribbonPageGroupRefresh");
            // 
            // ribbonPageGroupEvents
            // 
            this.ribbonPageGroupEvents.ItemLinks.Add(this.buttonClearEvents);
            this.ribbonPageGroupEvents.ItemLinks.Add(this.buttonShowEventDetails);
            this.ribbonPageGroupEvents.ItemLinks.Add(this.buttonSaveEvents);
            this.ribbonPageGroupEvents.Name = "ribbonPageGroupEvents";
            resources.ApplyResources(this.ribbonPageGroupEvents, "ribbonPageGroupEvents");
            // 
            // ribbonPageGroupCloseEvents
            // 
            this.ribbonPageGroupCloseEvents.ItemLinks.Add(this.buttonCloseEvents);
            this.ribbonPageGroupCloseEvents.Name = "ribbonPageGroupCloseEvents";
            resources.ApplyResources(this.ribbonPageGroupCloseEvents, "ribbonPageGroupCloseEvents");
            // 
            // ribbonPageMain
            // 
            this.ribbonPageMain.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.groupActions,
            this.ribbonPageGroupExit});
            this.ribbonPageMain.Name = "ribbonPageMain";
            resources.ApplyResources(this.ribbonPageMain, "ribbonPageMain");
            // 
            // groupActions
            // 
            this.groupActions.ItemLinks.Add(this.buttonMain);
            this.groupActions.ItemLinks.Add(this.buttonShowRules);
            this.groupActions.ItemLinks.Add(this.buttonPreferences);
            this.groupActions.ItemLinks.Add(this.buttonShowEvents);
            this.groupActions.Name = "groupActions";
            resources.ApplyResources(this.groupActions, "groupActions");
            // 
            // ribbonPageGroupExit
            // 
            this.ribbonPageGroupExit.ItemLinks.Add(this.buttonExitAndShutdown);
            this.ribbonPageGroupExit.Name = "ribbonPageGroupExit";
            resources.ApplyResources(this.ribbonPageGroupExit, "ribbonPageGroupExit");
            // 
            // ribbonStatusBar
            // 
            resources.ApplyResources(this.ribbonStatusBar, "ribbonStatusBar");
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            // 
            // clientPanel
            // 
            this.clientPanel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            resources.ApplyResources(this.clientPanel, "clientPanel");
            this.clientPanel.Name = "clientPanel";
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuTray;
            resources.ApplyResources(this.notifyIcon, "notifyIcon");
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            // 
            // contextMenuTray
            // 
            this.contextMenuTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemShow,
            this.toolStripMenuItemShutdown});
            this.contextMenuTray.Name = "contextMenuTray";
            this.contextMenuTray.ShowImageMargin = false;
            resources.ApplyResources(this.contextMenuTray, "contextMenuTray");
            // 
            // toolStripMenuItemShow
            // 
            resources.ApplyResources(this.toolStripMenuItemShow, "toolStripMenuItemShow");
            this.toolStripMenuItemShow.Name = "toolStripMenuItemShow";
            this.toolStripMenuItemShow.Click += new System.EventHandler(this.toolStripMenuItemShow_Click);
            // 
            // toolStripMenuItemShutdown
            // 
            this.toolStripMenuItemShutdown.Name = "toolStripMenuItemShutdown";
            resources.ApplyResources(this.toolStripMenuItemShutdown, "toolStripMenuItemShutdown");
            this.toolStripMenuItemShutdown.Click += new System.EventHandler(this.toolStripMenuItemShutdown_Click);
            // 
            // barCheckItem1
            // 
            resources.ApplyResources(this.barCheckItem1, "barCheckItem1");
            this.barCheckItem1.Checked = true;
            this.barCheckItem1.Id = -1;
            this.barCheckItem1.Name = "barCheckItem1";
            // 
            // dialogSaveEvents
            // 
            this.dialogSaveEvents.DefaultExt = "*.csv";
            resources.ApplyResources(this.dialogSaveEvents, "dialogSaveEvents");
            this.dialogSaveEvents.RestoreDirectory = true;
            // 
            // FormMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.clientPanel);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.Name = "FormMain";
            this.Ribbon = this.ribbon;
            this.StatusBar = this.ribbonStatusBar;
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.VisibleChanged += new System.EventHandler(this.FormMain_VisibleChanged);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.clientPanel)).EndInit();
            this.contextMenuTray.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion


        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraEditors.PanelControl clientPanel;
        private DevExpress.XtraBars.BarButtonItem buttonMain;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup groupActions;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuTray;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemShow;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemShutdown;
        private DevExpress.XtraBars.BarButtonItem buttonShowRules;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageRules;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupRules;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageMain;
        private DevExpress.XtraBars.BarButtonItem buttonPreferences;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPagePreferences;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupClosePreferences;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageEvents;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupEvents;
        private DevExpress.XtraBars.BarCheckItem barCheckItem1;
        private DevExpress.XtraBars.BarButtonItem buttonShowEvents;
        private DevExpress.XtraBars.BarButtonItem buttonCloseRules;
        private DevExpress.XtraBars.BarButtonItem buttonRefreshRules;
        private DevExpress.XtraBars.BarButtonItem buttonClosePreferences;
        private DevExpress.XtraBars.BarButtonItem buttonCloseEvents;
        private DevExpress.XtraBars.BarButtonItem buttonRefreshEvents;
        private DevExpress.XtraBars.BarCheckItem checkAutoRefreshEvents;
        private DevExpress.XtraBars.BarButtonItem buttonClearEvents;
        private DevExpress.XtraBars.BarButtonItem buttonSaveEvents;
        private System.Windows.Forms.SaveFileDialog dialogSaveEvents;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupRefresh;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupCloseEvents;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupCloseRules;
        private DevExpress.XtraBars.BarButtonItem buttonExitAndShutdown;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupExit;
        private DevExpress.XtraBars.Ribbon.RibbonPageCategory pageCategoryRules;
        private DevExpress.XtraBars.Ribbon.RibbonPageCategory pageCategoryPreferences;
        private DevExpress.XtraBars.Ribbon.RibbonPageCategory pageCategoryEvents;
        private DevExpress.XtraBars.BarButtonItem buttonShowEventDetails;
    }
}