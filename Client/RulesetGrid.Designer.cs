namespace VitaliiPianykh.FileWall.Client
{
    partial class RulesetGrid
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RulesetGrid));
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            this.gridViewItems = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colItemImage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.editorImage = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.colItemName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grid = new DevExpress.XtraGrid.GridControl();
            this._ruleSet = new VitaliiPianykh.FileWall.Shared.Ruleset();
            this.gridViewCategories = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCategoryID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCategoryImage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCategoryName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCategoryDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.editorActionColumn = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridViewRules = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colRuleID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRuleName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProtectedPath = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProcessPath = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAction = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editorImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._ruleSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCategories)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editorActionColumn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRules)).BeginInit();
            this.SuspendLayout();
            // 
            // gridViewItems
            // 
            this.gridViewItems.Appearance.FocusedCell.BackColor = System.Drawing.Color.LightSteelBlue;
            this.gridViewItems.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gridViewItems.Appearance.FocusedRow.BackColor = System.Drawing.Color.LightSteelBlue;
            this.gridViewItems.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridViewItems.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colItemImage,
            this.colItemName});
            this.gridViewItems.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridViewItems.GridControl = this.grid;
            this.gridViewItems.Name = "gridViewItems";
            this.gridViewItems.OptionsBehavior.AutoPopulateColumns = false;
            this.gridViewItems.OptionsDetail.ShowDetailTabs = false;
            this.gridViewItems.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewItems.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewItems.OptionsView.EnableAppearanceOddRow = true;
            this.gridViewItems.OptionsView.ShowColumnHeaders = false;
            this.gridViewItems.OptionsView.ShowGroupPanel = false;
            this.gridViewItems.OptionsView.ShowIndicator = false;
            this.gridViewItems.OptionsView.ShowVertLines = false;
            this.gridViewItems.RowHeight = 32;
            this.gridViewItems.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.gridViewItems_CustomUnboundColumnData);
            // 
            // colID
            // 
            resources.ApplyResources(this.colID, "colID");
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            // 
            // colItemImage
            // 
            resources.ApplyResources(this.colItemImage, "colItemImage");
            this.colItemImage.ColumnEdit = this.editorImage;
            this.colItemImage.FieldName = "colItemImage";
            this.colItemImage.Name = "colItemImage";
            this.colItemImage.OptionsColumn.AllowFocus = false;
            this.colItemImage.OptionsColumn.FixedWidth = true;
            this.colItemImage.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            // 
            // editorImage
            // 
            this.editorImage.Name = "editorImage";
            this.editorImage.PictureStoreMode = DevExpress.XtraEditors.Controls.PictureStoreMode.ByteArray;
            this.editorImage.ShowMenu = false;
            this.editorImage.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            // 
            // colItemName
            // 
            resources.ApplyResources(this.colItemName, "colItemName");
            this.colItemName.FieldName = "Name";
            this.colItemName.Name = "colItemName";
            this.colItemName.OptionsColumn.AllowFocus = false;
            // 
            // grid
            // 
            resources.ApplyResources(this.grid, "grid");
            this.grid.DataSource = this._ruleSet;
            gridLevelNode1.LevelTemplate = this.gridViewItems;
            gridLevelNode2.LevelTemplate = this.gridViewRules;
            gridLevelNode2.RelationName = "FK_Items_Rules";
            gridLevelNode1.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode2});
            gridLevelNode1.RelationName = "FK_Categories_Items";
            this.grid.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.grid.MainView = this.gridViewCategories;
            this.grid.Name = "grid";
            this.grid.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.editorImage,
            this.editorActionColumn});
            this.grid.ShowOnlyPredefinedDetails = true;
            this.grid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewCategories,
            this.gridViewRules,
            this.gridViewItems});
            // 
            // _ruleSet
            // 
            this._ruleSet.DataSetName = "Ruleset";
            this._ruleSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gridViewCategories
            // 
            this.gridViewCategories.Appearance.FocusedCell.BackColor = System.Drawing.Color.LightSteelBlue;
            this.gridViewCategories.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gridViewCategories.Appearance.FocusedRow.BackColor = System.Drawing.Color.LightSteelBlue;
            this.gridViewCategories.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridViewCategories.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCategoryID,
            this.colCategoryImage,
            this.colCategoryName,
            this.colCategoryDescription});
            this.gridViewCategories.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridViewCategories.GridControl = this.grid;
            this.gridViewCategories.Name = "gridViewCategories";
            this.gridViewCategories.OptionsBehavior.AutoPopulateColumns = false;
            this.gridViewCategories.OptionsDetail.ShowDetailTabs = false;
            this.gridViewCategories.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewCategories.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewCategories.OptionsView.EnableAppearanceOddRow = true;
            this.gridViewCategories.OptionsView.ShowColumnHeaders = false;
            this.gridViewCategories.OptionsView.ShowGroupPanel = false;
            this.gridViewCategories.OptionsView.ShowIndicator = false;
            this.gridViewCategories.OptionsView.ShowVertLines = false;
            this.gridViewCategories.RowHeight = 32;
            this.gridViewCategories.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.gridViewCategories_CustomUnboundColumnData);
            // 
            // colCategoryID
            // 
            resources.ApplyResources(this.colCategoryID, "colCategoryID");
            this.colCategoryID.FieldName = "ID";
            this.colCategoryID.Name = "colCategoryID";
            this.colCategoryID.OptionsColumn.AllowFocus = false;
            // 
            // colCategoryImage
            // 
            resources.ApplyResources(this.colCategoryImage, "colCategoryImage");
            this.colCategoryImage.ColumnEdit = this.editorImage;
            this.colCategoryImage.FieldName = "colCategoryImage";
            this.colCategoryImage.Name = "colCategoryImage";
            this.colCategoryImage.OptionsColumn.AllowFocus = false;
            this.colCategoryImage.OptionsColumn.FixedWidth = true;
            this.colCategoryImage.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            // 
            // colCategoryName
            // 
            resources.ApplyResources(this.colCategoryName, "colCategoryName");
            this.colCategoryName.FieldName = "Name";
            this.colCategoryName.Name = "colCategoryName";
            this.colCategoryName.OptionsColumn.AllowFocus = false;
            // 
            // colCategoryDescription
            // 
            resources.ApplyResources(this.colCategoryDescription, "colCategoryDescription");
            this.colCategoryDescription.FieldName = "Description";
            this.colCategoryDescription.Name = "colCategoryDescription";
            this.colCategoryDescription.OptionsColumn.AllowFocus = false;
            // 
            // editorActionColumn
            // 
            this.editorActionColumn.AllowGrayed = true;
            resources.ApplyResources(this.editorActionColumn, "editorActionColumn");
            this.editorActionColumn.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined;
            this.editorActionColumn.Name = "editorActionColumn";
            this.editorActionColumn.PictureChecked = global::VitaliiPianykh.FileWall.Client.Properties.Resources.AllowAccess;
            this.editorActionColumn.PictureGrayed = global::VitaliiPianykh.FileWall.Client.Properties.Resources.AlwaysAsk;
            this.editorActionColumn.PictureUnchecked = global::VitaliiPianykh.FileWall.Client.Properties.Resources.BlockAccess;
            this.editorActionColumn.ValueChecked = 1;
            this.editorActionColumn.ValueGrayed = -1;
            this.editorActionColumn.ValueUnchecked = 0;
            this.editorActionColumn.QueryCheckStateByValue += new DevExpress.XtraEditors.Controls.QueryCheckStateByValueEventHandler(this.editorActionColumn_QueryCheckStateByValue);
            // 
            // gridViewRules
            // 
            this.gridViewRules.Appearance.FocusedCell.BackColor = System.Drawing.Color.LightSteelBlue;
            this.gridViewRules.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gridViewRules.Appearance.FocusedRow.BackColor = System.Drawing.Color.LightSteelBlue;
            this.gridViewRules.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridViewRules.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colRuleID,
            this.colRuleName,
            this.colProtectedPath,
            this.colProcessPath,
            this.colAction});
            this.gridViewRules.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridViewRules.GridControl = this.grid;
            this.gridViewRules.Name = "gridViewRules";
            this.gridViewRules.OptionsBehavior.AutoPopulateColumns = false;
            this.gridViewRules.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewRules.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewRules.OptionsView.EnableAppearanceOddRow = true;
            this.gridViewRules.OptionsView.ShowGroupPanel = false;
            this.gridViewRules.OptionsView.ShowIndicator = false;
            this.gridViewRules.OptionsView.ShowVertLines = false;
            this.gridViewRules.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.gridViewRules_CustomUnboundColumnData);
            this.gridViewRules.DoubleClick += new System.EventHandler(this.gridViewRules_DoubleClick);
            this.gridViewRules.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridViewRules_MouseDown);
            // 
            // colRuleID
            // 
            resources.ApplyResources(this.colRuleID, "colRuleID");
            this.colRuleID.FieldName = "ID";
            this.colRuleID.Name = "colRuleID";
            // 
            // colRuleName
            // 
            resources.ApplyResources(this.colRuleName, "colRuleName");
            this.colRuleName.FieldName = "Name";
            this.colRuleName.Name = "colRuleName";
            this.colRuleName.OptionsColumn.AllowFocus = false;
            // 
            // colProtectedPath
            // 
            this.colProtectedPath.AppearanceCell.Options.UseTextOptions = true;
            this.colProtectedPath.AppearanceCell.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisPath;
            resources.ApplyResources(this.colProtectedPath, "colProtectedPath");
            this.colProtectedPath.FieldName = "ProtectedPath";
            this.colProtectedPath.Name = "colProtectedPath";
            this.colProtectedPath.OptionsColumn.AllowFocus = false;
            this.colProtectedPath.UnboundType = DevExpress.Data.UnboundColumnType.String;
            // 
            // colProcessPath
            // 
            this.colProcessPath.AppearanceCell.Options.UseTextOptions = true;
            this.colProcessPath.AppearanceCell.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisPath;
            resources.ApplyResources(this.colProcessPath, "colProcessPath");
            this.colProcessPath.FieldName = "ProcessPath";
            this.colProcessPath.Name = "colProcessPath";
            this.colProcessPath.OptionsColumn.AllowFocus = false;
            this.colProcessPath.UnboundType = DevExpress.Data.UnboundColumnType.String;
            // 
            // colAction
            // 
            resources.ApplyResources(this.colAction, "colAction");
            this.colAction.ColumnEdit = this.editorActionColumn;
            this.colAction.FieldName = "Action";
            this.colAction.Name = "colAction";
            this.colAction.OptionsColumn.AllowFocus = false;
            // 
            // RulesetGrid
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grid);
            this.Name = "RulesetGrid";
            ((System.ComponentModel.ISupportInitialize)(this.gridViewItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editorImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._ruleSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCategories)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editorActionColumn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRules)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit editorImage;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewRules;
        private DevExpress.XtraGrid.Columns.GridColumn colRuleName;
        private DevExpress.XtraGrid.Columns.GridColumn colProtectedPath;
        private DevExpress.XtraGrid.Columns.GridColumn colProcessPath;
        private DevExpress.XtraGrid.Columns.GridColumn colAction;
        private DevExpress.XtraGrid.GridControl grid;
        private VitaliiPianykh.FileWall.Shared.Ruleset _ruleSet;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewItems;
        private DevExpress.XtraGrid.Columns.GridColumn colItemImage;
        private DevExpress.XtraGrid.Columns.GridColumn colItemName;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewCategories;
        private DevExpress.XtraGrid.Columns.GridColumn colCategoryID;
        private DevExpress.XtraGrid.Columns.GridColumn colCategoryName;
        private DevExpress.XtraGrid.Columns.GridColumn colCategoryImage;
        private DevExpress.XtraGrid.Columns.GridColumn colCategoryDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colRuleID;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit editorActionColumn;

    }
}