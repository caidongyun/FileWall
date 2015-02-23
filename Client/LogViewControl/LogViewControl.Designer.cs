namespace VitaliiPianykh.FileWall.Client
{
    partial class LogViewControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gridControlEvents = new DevExpress.XtraGrid.GridControl();
            this.gridViewEvents = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colActionPerformed = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEditActionPerformed = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colRequestType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEditRequestType = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colPath = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProcessPath = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControlDetails = new DevExpress.XtraEditors.GroupControl();
            this.editProcessPath = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.editPath = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.editAccessType = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.editAction = new DevExpress.XtraEditors.TextEdit();
            this.editDate = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlEvents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewEvents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditActionPerformed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditRequestType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlDetails)).BeginInit();
            this.groupControlDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editProcessPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editAccessType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editAction.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editDate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlEvents
            // 
            this.gridControlEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlEvents.Location = new System.Drawing.Point(0, 0);
            this.gridControlEvents.MainView = this.gridViewEvents;
            this.gridControlEvents.Name = "gridControlEvents";
            this.gridControlEvents.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEditActionPerformed,
            this.repositoryItemCheckEditRequestType});
            this.gridControlEvents.Size = new System.Drawing.Size(496, 288);
            this.gridControlEvents.TabIndex = 0;
            this.gridControlEvents.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewEvents});
            // 
            // gridViewEvents
            // 
            this.gridViewEvents.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDate,
            this.colActionPerformed,
            this.colRequestType,
            this.colPath,
            this.colProcessPath});
            this.gridViewEvents.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridViewEvents.GridControl = this.gridControlEvents;
            this.gridViewEvents.Name = "gridViewEvents";
            this.gridViewEvents.OptionsBehavior.Editable = false;
            this.gridViewEvents.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewEvents.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewEvents.OptionsView.EnableAppearanceOddRow = true;
            this.gridViewEvents.OptionsView.ShowGroupPanel = false;
            // 
            // colDate
            // 
            this.colDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDate.Caption = "Date";
            this.colDate.FieldName = "Date";
            this.colDate.Name = "colDate";
            this.colDate.Visible = true;
            this.colDate.VisibleIndex = 0;
            this.colDate.Width = 114;
            // 
            // colActionPerformed
            // 
            this.colActionPerformed.AppearanceHeader.Options.UseTextOptions = true;
            this.colActionPerformed.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colActionPerformed.Caption = "Action Performed";
            this.colActionPerformed.ColumnEdit = this.repositoryItemCheckEditActionPerformed;
            this.colActionPerformed.FieldName = "IsAllowed";
            this.colActionPerformed.Name = "colActionPerformed";
            this.colActionPerformed.OptionsColumn.FixedWidth = true;
            this.colActionPerformed.Visible = true;
            this.colActionPerformed.VisibleIndex = 1;
            this.colActionPerformed.Width = 20;
            // 
            // repositoryItemCheckEditActionPerformed
            // 
            this.repositoryItemCheckEditActionPerformed.AutoHeight = false;
            this.repositoryItemCheckEditActionPerformed.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined;
            this.repositoryItemCheckEditActionPerformed.Name = "repositoryItemCheckEditActionPerformed";
            this.repositoryItemCheckEditActionPerformed.PictureChecked = global::VitaliiPianykh.FileWall.Client.Properties.Resources.AllowAccess;
            this.repositoryItemCheckEditActionPerformed.PictureUnchecked = global::VitaliiPianykh.FileWall.Client.Properties.Resources.BlockAccess;
            // 
            // colRequestType
            // 
            this.colRequestType.AppearanceHeader.Options.UseTextOptions = true;
            this.colRequestType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRequestType.Caption = "Request Type";
            this.colRequestType.ColumnEdit = this.repositoryItemCheckEditRequestType;
            this.colRequestType.FieldName = "AccessType";
            this.colRequestType.Name = "colRequestType";
            this.colRequestType.OptionsColumn.FixedWidth = true;
            this.colRequestType.Visible = true;
            this.colRequestType.VisibleIndex = 2;
            this.colRequestType.Width = 20;
            // 
            // repositoryItemCheckEditRequestType
            // 
            this.repositoryItemCheckEditRequestType.AutoHeight = false;
            this.repositoryItemCheckEditRequestType.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined;
            this.repositoryItemCheckEditRequestType.Name = "repositoryItemCheckEditRequestType";
            this.repositoryItemCheckEditRequestType.PictureChecked = global::VitaliiPianykh.FileWall.Client.Properties.Resources.Registry;
            this.repositoryItemCheckEditRequestType.PictureUnchecked = global::VitaliiPianykh.FileWall.Client.Properties.Resources.Filesystem;
            // 
            // colPath
            // 
            this.colPath.AppearanceCell.Options.UseTextOptions = true;
            this.colPath.AppearanceCell.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisPath;
            this.colPath.AppearanceHeader.Options.UseTextOptions = true;
            this.colPath.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPath.Caption = "Path";
            this.colPath.FieldName = "Path";
            this.colPath.Name = "colPath";
            this.colPath.Visible = true;
            this.colPath.VisibleIndex = 3;
            this.colPath.Width = 204;
            // 
            // colProcessPath
            // 
            this.colProcessPath.AppearanceCell.Options.UseTextOptions = true;
            this.colProcessPath.AppearanceCell.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisPath;
            this.colProcessPath.AppearanceHeader.Options.UseTextOptions = true;
            this.colProcessPath.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProcessPath.Caption = "Process Path";
            this.colProcessPath.FieldName = "ProcessPath";
            this.colProcessPath.Name = "colProcessPath";
            this.colProcessPath.Visible = true;
            this.colProcessPath.VisibleIndex = 4;
            this.colProcessPath.Width = 216;
            // 
            // groupControlDetails
            // 
            this.groupControlDetails.Controls.Add(this.editProcessPath);
            this.groupControlDetails.Controls.Add(this.labelControl5);
            this.groupControlDetails.Controls.Add(this.editPath);
            this.groupControlDetails.Controls.Add(this.labelControl4);
            this.groupControlDetails.Controls.Add(this.editAccessType);
            this.groupControlDetails.Controls.Add(this.labelControl3);
            this.groupControlDetails.Controls.Add(this.editAction);
            this.groupControlDetails.Controls.Add(this.editDate);
            this.groupControlDetails.Controls.Add(this.labelControl2);
            this.groupControlDetails.Controls.Add(this.labelControl1);
            this.groupControlDetails.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupControlDetails.Location = new System.Drawing.Point(0, 288);
            this.groupControlDetails.Name = "groupControlDetails";
            this.groupControlDetails.Size = new System.Drawing.Size(496, 100);
            this.groupControlDetails.TabIndex = 1;
            this.groupControlDetails.Text = "Details";
            // 
            // editProcessPath
            // 
            this.editProcessPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.editProcessPath.Location = new System.Drawing.Point(83, 75);
            this.editProcessPath.Name = "editProcessPath";
            this.editProcessPath.Properties.Appearance.Options.UseTextOptions = true;
            this.editProcessPath.Properties.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisPath;
            this.editProcessPath.Properties.ReadOnly = true;
            this.editProcessPath.Size = new System.Drawing.Size(408, 20);
            this.editProcessPath.TabIndex = 11;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(15, 78);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(62, 13);
            this.labelControl5.TabIndex = 10;
            this.labelControl5.Text = "Process Path";
            // 
            // editPath
            // 
            this.editPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.editPath.Location = new System.Drawing.Point(83, 50);
            this.editPath.Name = "editPath";
            this.editPath.Properties.Appearance.Options.UseTextOptions = true;
            this.editPath.Properties.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisPath;
            this.editPath.Properties.ReadOnly = true;
            this.editPath.Size = new System.Drawing.Size(408, 20);
            this.editPath.TabIndex = 9;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(7, 53);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(70, 13);
            this.labelControl4.TabIndex = 8;
            this.labelControl4.Text = "Accessed Path";
            // 
            // editAccessType
            // 
            this.editAccessType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.editAccessType.Location = new System.Drawing.Point(382, 24);
            this.editAccessType.Name = "editAccessType";
            this.editAccessType.Properties.ReadOnly = true;
            this.editAccessType.Size = new System.Drawing.Size(109, 20);
            this.editAccessType.TabIndex = 7;
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl3.Location = new System.Drawing.Point(346, 27);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(33, 13);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "Source";
            // 
            // editAction
            // 
            this.editAction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.editAction.Location = new System.Drawing.Point(202, 24);
            this.editAction.Name = "editAction";
            this.editAction.Properties.ReadOnly = true;
            this.editAction.Size = new System.Drawing.Size(109, 20);
            this.editAction.TabIndex = 5;
            this.editAction.CustomDisplayText += new DevExpress.XtraEditors.Controls.CustomDisplayTextEventHandler(this.editAction_CustomDisplayText);
            // 
            // editDate
            // 
            this.editDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.editDate.Location = new System.Drawing.Point(36, 24);
            this.editDate.Name = "editDate";
            this.editDate.Properties.ReadOnly = true;
            this.editDate.Size = new System.Drawing.Size(108, 20);
            this.editDate.TabIndex = 2;
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl2.Location = new System.Drawing.Point(166, 27);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(30, 13);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Action";
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl1.Location = new System.Drawing.Point(7, 27);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(23, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Date";
            // 
            // LogViewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControlEvents);
            this.Controls.Add(this.groupControlDetails);
            this.Name = "LogViewControl";
            this.Size = new System.Drawing.Size(496, 388);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlEvents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewEvents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditActionPerformed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditRequestType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlDetails)).EndInit();
            this.groupControlDetails.ResumeLayout(false);
            this.groupControlDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editProcessPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editAccessType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editAction.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editDate.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlEvents;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewEvents;
        private DevExpress.XtraGrid.Columns.GridColumn colDate;
        private DevExpress.XtraGrid.Columns.GridColumn colActionPerformed;
        private DevExpress.XtraGrid.Columns.GridColumn colRequestType;
        private DevExpress.XtraGrid.Columns.GridColumn colPath;
        private DevExpress.XtraGrid.Columns.GridColumn colProcessPath;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEditActionPerformed;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEditRequestType;
        private DevExpress.XtraEditors.GroupControl groupControlDetails;
        private DevExpress.XtraEditors.TextEdit editProcessPath;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit editPath;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit editAccessType;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit editAction;
        private DevExpress.XtraEditors.TextEdit editDate;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}
