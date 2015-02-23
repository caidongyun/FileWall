using System;
using System.ComponentModel;
using System.Windows.Forms;
using VitaliiPianykh.FileWall.Shared;
using DevExpress.XtraEditors;

namespace VitaliiPianykh.FileWall.Client
{
    public partial class LogViewControl : XtraUserControl, ILogView
    {
        public LogViewControl()
        {
            InitializeComponent();
            repositoryItemCheckEditRequestType.ValueUnchecked = AccessType.FILESYSTEM;
            repositoryItemCheckEditRequestType.ValueChecked = AccessType.REGISTRY;
            DetailsVisible = false;
        }

        #region Implementation of ILogView

        public BindingList<LogEntryData> Data
        {
            get {return (BindingList<LogEntryData>) gridControlEvents.DataSource; }
            set
            {
                gridControlEvents.DataSource = value;

                editDate.DataBindings.Clear();
                editAction.DataBindings.Clear();
                editAccessType.DataBindings.Clear();
                editPath.DataBindings.Clear();
                editProcessPath.DataBindings.Clear();

                editDate.DataBindings.Add(new Binding("Text", value, "Date"));
                editAction.DataBindings.Add("Text", value, "IsAllowed");
                editAccessType.DataBindings.Add("Text", value, "AccessType");
                editPath.DataBindings.Add("Text", value, "Path");
                editProcessPath.DataBindings.Add("Text", value, "ProcessPath");
            }
        }

        public bool DetailsVisible
        {
            get { return groupControlDetails.Visible; }
            set { groupControlDetails.Visible = value; }
        }

        #endregion

        private void editAction_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            if (e.Value == null)
                return;

            if ((string) e.Value == "True")
                e.DisplayText = "Allowed";
            else
                e.DisplayText = "Blocked";
        }
    }
}
