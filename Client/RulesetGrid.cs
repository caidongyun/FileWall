using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using VitaliiPianykh.FileWall.Client.Properties;
using VitaliiPianykh.FileWall.Shared;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;


namespace VitaliiPianykh.FileWall.Client
{
    public partial class RulesetGrid : DevExpress.XtraEditors.XtraUserControl
    {
        private bool _ReadOnly;

        #region Public Methods

        public RulesetGrid()
        {
            InitializeComponent();
        }


        /// <summary>Zooms category.</summary>
        /// <returns>true if category zoommed, false if no category zoommed.</returns>
        public bool ZoomCategory(int categoryID)
        {
            if (RuleSet != null)
            {
                // Getting RowHandle...
                var CategoryRow = RuleSet.Categories.FindByID(categoryID);
                int DataSourceIndex = RuleSet.Categories.Rows.IndexOf(CategoryRow);
                int RowHandle = gridViewCategories.GetRowHandle(DataSourceIndex);

                // We have only one relation.
                const int RelationIndex = 0;

                // Zooming view...
                gridViewCategories.SetMasterRowExpandedEx(RowHandle, RelationIndex, true);
                GridView ItemsView = (GridView)gridViewCategories.GetDetailView(RowHandle, RelationIndex);
                if (ItemsView != null)
                {
                    ItemsView.ZoomView();
                    return true;
                }
            }

            return false;
        }


        /// <summary>Unzoom category and show all available categories.</summary>
        public void UnzoomAll()
        {
            gridViewCategories.NormalView(); // Just show all categories.
        }

        #endregion


        #region Public Properties

        [Category("Data")]
        public Ruleset RuleSet
        {
            get { return _ruleSet; }
            set
            {
                _ruleSet = value;
                grid.DataSource = _ruleSet;
            }
        }
        
        /// <summary>Hides or shows rules under every item.</summary>
        [Category("View")]
        [Description("Hides or shows rules under every item.")]
        [DefaultValue(true)]
        public bool ShowRules
        {
            get { return gridViewItems.OptionsDetail.EnableMasterViewMode; }
            set { gridViewItems.OptionsDetail.EnableMasterViewMode = value; }
        }
        
        [Category("View")]
        [DefaultValue(true)]
        public bool ReadOnly
        {
            get { return _ReadOnly; }
            set
            {
                _ReadOnly = value;

                foreach(GridColumn col in gridViewCategories.Columns)
                    col.OptionsColumn.AllowFocus = !_ReadOnly;

                foreach (GridColumn col in gridViewItems.Columns)
                    col.OptionsColumn.AllowFocus = !_ReadOnly;

                foreach (GridColumn col in gridViewRules.Columns)
                    col.OptionsColumn.AllowFocus = !_ReadOnly;

                gridViewCategories.OptionsView.ShowIndicator = !_ReadOnly;
                gridViewCategories.OptionsView.ShowColumnHeaders = !_ReadOnly;

                gridViewItems.OptionsView.ShowIndicator = !_ReadOnly;
                gridViewItems.OptionsView.ShowColumnHeaders = !_ReadOnly;

                gridViewRules.OptionsView.ShowIndicator = !_ReadOnly;

                // Allow/disable image editing.
                editorImage.ShowMenu = !_ReadOnly;

                // Allow/disable expandding of empty details.
                // For example: it's needed if adding new items to empty category.
                gridViewCategories.OptionsDetail.AllowExpandEmptyDetails = !_ReadOnly;
                gridViewItems.OptionsDetail.AllowExpandEmptyDetails = !_ReadOnly;
                gridViewRules.OptionsDetail.AllowExpandEmptyDetails = !_ReadOnly;

                grid.UseEmbeddedNavigator = !_ReadOnly;

                if(!_ReadOnly)
                {
                    // Show "new row"
                    gridViewCategories.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
                    gridViewItems.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
                    gridViewRules.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
                }
                else
                {
                    // Hide "new row"
                    gridViewCategories.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                    gridViewItems.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                    gridViewRules.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                }

                grid.Refresh();
            }
        }

        #endregion


        #region gridViewRules event's handling

        /// <summary>Provides GridView with ProtectedPath and ProcessPath column's values.</summary>
        private void gridViewRules_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            GridView view = (GridView)sender;

            if (e.IsGetData && e.RowHandle < 0)
            {
                e.Value = null;
                return;
            }

            if (e.IsGetData && e.Column.Name == colProtectedPath.Name)
                e.Value = _ruleSet.Rules.FindByID((int)view.GetRowCellValue(e.RowHandle, "ID")).ProtectedPath;

            if (e.IsGetData && e.Column.Name == colProcessPath.Name)
                e.Value = _ruleSet.Rules.FindByID((int)view.GetRowCellValue(e.RowHandle, "ID")).ProcessPath;

            if(e.IsSetData)
            {
                if (e.Column.Name == colProtectedPath.Name)
                {
                    var RuleRow = _ruleSet.Rules.FindByID((int) view.GetRowCellValue(e.RowHandle, "ID"));
                    RuleRow.PathsRow.Path = (string) e.Value;
                }
            }
        }

        #endregion


        private void editorActionColumn_QueryCheckStateByValue(object sender, DevExpress.XtraEditors.Controls.QueryCheckStateByValueEventArgs e)
        {
            var Action = (RuleAction) e.Value;

            if (Action == RuleAction.Allow)
                e.CheckState = CheckState.Checked;
            else if(Action == RuleAction.Block)
                e.CheckState = CheckState.Unchecked;
            else if(Action == RuleAction.Block)
                e.CheckState = CheckState.Indeterminate;

            e.Handled = true;
        }

        // Provides category view with default image if needed.
        private void gridViewCategories_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.IsGetData && e.Column.Name == colCategoryImage.Name)
            {
                if (_ruleSet.Categories[e.RowHandle].ImageData == null || _ruleSet.Categories[e.RowHandle].ImageData.Length == 0)
                    e.Value = Resources.DefaultCategoryIcon;
                else
                    e.Value = _ruleSet.Categories[e.RowHandle].Image;
            }
        }

        // Provides items view with default image if needed.
        private void gridViewItems_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.IsGetData && e.Column.Name == colItemImage.Name)
            {
                if (_ruleSet.Items[e.RowHandle].ImageData == null || _ruleSet.Items[e.RowHandle].ImageData.Length == 0)
                    e.Value = Resources.DefaulItemIcon;
                else
                    e.Value = _ruleSet.Items[e.RowHandle].Image;
            }
        }

        private Point _ClickLocation;

        private void gridViewRules_MouseDown(object sender, MouseEventArgs e)
        {
            _ClickLocation = e.Location;
        }

        private void gridViewRules_DoubleClick(object sender, System.EventArgs e)
        {
            var gridView = (GridView) sender;
            var hitInfo = gridView.CalcHitInfo(_ClickLocation);

            if(hitInfo.InRowCell)
            {
                var frm = new FormRuleEdit();
                var ruleID = (int)gridView.GetRowCellValue(hitInfo.RowHandle, "ID");
                frm.Rule = RuleSet.Rules.FindByID(ruleID);
                MessageBox.Show(frm.ShowDialog().ToString());
            }
        }
    }
}