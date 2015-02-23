namespace VitaliiPianykh.FileWall.Client
{
    partial class FormRuleEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRuleEdit));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.textName = new DevExpress.XtraEditors.TextEdit();
            this.checkLog = new DevExpress.XtraEditors.CheckEdit();
            this.textProtectedPath = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.buttonBrowseFolder = new DevExpress.XtraEditors.SimpleButton();
            this.buttonBrowseRegistry = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.textProcessPath = new DevExpress.XtraEditors.TextEdit();
            this.buttonBrowseProcess = new DevExpress.XtraEditors.SimpleButton();
            this.buttonSuggestName = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.comboAction = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.buttonApply = new DevExpress.XtraEditors.SimpleButton();
            this.buttonCancel = new DevExpress.XtraEditors.SimpleButton();
            this.buttonOK = new DevExpress.XtraEditors.SimpleButton();
            this.imagesRuleActions = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.textName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkLog.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textProtectedPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textProcessPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboAction.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imagesRuleActions)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(25, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(50, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Rule name";
            // 
            // textName
            // 
            this.textName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textName.Location = new System.Drawing.Point(81, 12);
            this.textName.Name = "textName";
            this.textName.Size = new System.Drawing.Size(242, 20);
            this.textName.TabIndex = 1;
            this.textName.EditValueChanged += new System.EventHandler(this.textName_EditValueChanged);
            // 
            // checkLog
            // 
            this.checkLog.Location = new System.Drawing.Point(24, 131);
            this.checkLog.Name = "checkLog";
            this.checkLog.Properties.Caption = "Always log";
            this.checkLog.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.checkLog.Size = new System.Drawing.Size(73, 19);
            this.checkLog.TabIndex = 9;
            this.checkLog.EditValueChanged += new System.EventHandler(this.textName_EditValueChanged);
            // 
            // textProtectedPath
            // 
            this.textProtectedPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textProtectedPath.Location = new System.Drawing.Point(81, 46);
            this.textProtectedPath.Name = "textProtectedPath";
            this.textProtectedPath.Size = new System.Drawing.Size(242, 20);
            this.textProtectedPath.TabIndex = 3;
            this.textProtectedPath.EditValueChanged += new System.EventHandler(this.textName_EditValueChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(3, 49);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(72, 13);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Protected Path";
            // 
            // buttonBrowseFolder
            // 
            this.buttonBrowseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseFolder.Location = new System.Drawing.Point(329, 46);
            this.buttonBrowseFolder.Name = "buttonBrowseFolder";
            this.buttonBrowseFolder.Size = new System.Drawing.Size(20, 20);
            this.buttonBrowseFolder.TabIndex = 4;
            this.buttonBrowseFolder.Text = "...";
            this.buttonBrowseFolder.Click += new System.EventHandler(this.buttonBrowseFolder_Click);
            // 
            // buttonBrowseRegistry
            // 
            this.buttonBrowseRegistry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseRegistry.Location = new System.Drawing.Point(355, 46);
            this.buttonBrowseRegistry.Name = "buttonBrowseRegistry";
            this.buttonBrowseRegistry.Size = new System.Drawing.Size(20, 20);
            this.buttonBrowseRegistry.TabIndex = 5;
            this.buttonBrowseRegistry.Text = "...";
            this.buttonBrowseRegistry.Click += new System.EventHandler(this.buttonBrowseRegistry_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(13, 80);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(62, 13);
            this.labelControl3.TabIndex = 7;
            this.labelControl3.Text = "Process Path";
            // 
            // textProcessPath
            // 
            this.textProcessPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textProcessPath.Location = new System.Drawing.Point(81, 77);
            this.textProcessPath.Name = "textProcessPath";
            this.textProcessPath.Size = new System.Drawing.Size(242, 20);
            this.textProcessPath.TabIndex = 6;
            this.textProcessPath.EditValueChanged += new System.EventHandler(this.textName_EditValueChanged);
            // 
            // buttonBrowseProcess
            // 
            this.buttonBrowseProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseProcess.Location = new System.Drawing.Point(329, 77);
            this.buttonBrowseProcess.Name = "buttonBrowseProcess";
            this.buttonBrowseProcess.Size = new System.Drawing.Size(20, 20);
            this.buttonBrowseProcess.TabIndex = 7;
            this.buttonBrowseProcess.Text = "...";
            this.buttonBrowseProcess.Click += new System.EventHandler(this.buttonBrowseProcess_Click);
            // 
            // buttonSuggestName
            // 
            this.buttonSuggestName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSuggestName.Location = new System.Drawing.Point(329, 12);
            this.buttonSuggestName.Name = "buttonSuggestName";
            this.buttonSuggestName.Size = new System.Drawing.Size(20, 20);
            this.buttonSuggestName.TabIndex = 2;
            this.buttonSuggestName.Text = "...";
            this.buttonSuggestName.Click += new System.EventHandler(this.buttonSuggestName_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelControl1.Location = new System.Drawing.Point(3, 38);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(379, 2);
            this.panelControl1.TabIndex = 11;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(45, 108);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(30, 13);
            this.labelControl4.TabIndex = 12;
            this.labelControl4.Text = "Action";
            // 
            // comboAction
            // 
            this.comboAction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboAction.Location = new System.Drawing.Point(81, 105);
            this.comboAction.Name = "comboAction";
            this.comboAction.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboAction.Properties.DropDownRows = 3;
            this.comboAction.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Always allow access", 0, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Ask user about this action", 1, 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Deny access for this resource", 2, 2)});
            this.comboAction.Properties.SmallImages = this.imagesRuleActions;
            this.comboAction.Size = new System.Drawing.Size(242, 20);
            this.comboAction.TabIndex = 8;
            this.comboAction.EditValueChanged += new System.EventHandler(this.textName_EditValueChanged);
            // 
            // buttonApply
            // 
            this.buttonApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonApply.Location = new System.Drawing.Point(303, 156);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 23);
            this.buttonApply.TabIndex = 12;
            this.buttonApply.Text = "&Apply";
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(222, 156);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.Text = "&Cancel";
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(141, 156);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 10;
            this.buttonOK.Text = "&OK";
            // 
            // imagesRuleActions
            // 
            this.imagesRuleActions.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imagesRuleActions.ImageStream")));
            // 
            // FormRuleEdit
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(390, 191);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.comboAction);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.buttonSuggestName);
            this.Controls.Add(this.buttonBrowseProcess);
            this.Controls.Add(this.textProcessPath);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.buttonBrowseRegistry);
            this.Controls.Add(this.buttonBrowseFolder);
            this.Controls.Add(this.textProtectedPath);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.checkLog);
            this.Controls.Add(this.textName);
            this.Controls.Add(this.labelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormRuleEdit";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Rule Edit";
            ((System.ComponentModel.ISupportInitialize)(this.textName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkLog.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textProtectedPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textProcessPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboAction.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imagesRuleActions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit textName;
        private DevExpress.XtraEditors.CheckEdit checkLog;
        private DevExpress.XtraEditors.TextEdit textProtectedPath;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton buttonBrowseFolder;
        private DevExpress.XtraEditors.SimpleButton buttonBrowseRegistry;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit textProcessPath;
        private DevExpress.XtraEditors.SimpleButton buttonBrowseProcess;
        private DevExpress.XtraEditors.SimpleButton buttonSuggestName;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.ImageComboBoxEdit comboAction;
        private DevExpress.XtraEditors.SimpleButton buttonApply;
        private DevExpress.XtraEditors.SimpleButton buttonCancel;
        private DevExpress.XtraEditors.SimpleButton buttonOK;
        private DevExpress.Utils.ImageCollection imagesRuleActions;
    }
}