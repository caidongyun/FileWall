using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using VitaliiPianykh.FileWall.Client.Properties;
using VitaliiPianykh.FileWall.Shared;
using DevExpress.XtraEditors;


namespace VitaliiPianykh.FileWall.Client
{
    public partial class FormAccessRequest : XtraForm
    {
        public FormAccessRequest()
        {
            InitializeComponent();

            // BUG FIX: FormAccessRequest doesn't use client’s skin and localization info.
            LookAndFeel.SetSkinStyle(Settings.Default.SkinName); // Set skin.
            var resources = new ComponentResourceManager(typeof(FormAccessRequest));// Localize form.
            resources.ApplyResources(this, "$this", Settings.Default.UICulture);
            LocalizeControls(Controls, resources, Settings.Default.UICulture);
        }

        public FormAccessRequest(CoreAccessRequestedEventArgs e)
            : this()
        {
            labelProcessPath.Text = e.ProcessPath;
            labelOperation.Text = e.OperationName; 
            labelProtectedPath.Text = e.ProtectedPath;
        }


        public static void Show(CoreAccessRequestedEventArgs e)
        {
            var frm = new FormAccessRequest(e);

            DialogResult res = frm.ShowDialog();

            if (res == DialogResult.Yes)
                e.Allow = true;
            else if (res == DialogResult.No)
                e.Allow = false;
            else if(res == DialogResult.OK) //Create rule.
            {
                e.CreateRule = true;
                e.CategoryName = "User Created Rules";
                e.ItemName = "User Created Rules";
                e.RuleName = Path.GetFileName(e.ProtectedPath) + " for " +
                             Path.GetFileName(e.ProcessPath);

                if (frm.radioAllow.Checked)
                    e.Allow = true;
                else if (frm.radioBlock.Checked)
                    e.Allow = false;
            }
        }

        private static void LocalizeControls(Control.ControlCollection controls, ComponentResourceManager resources, CultureInfo cultureInfo)
        {
            foreach (Control control in controls)
            {
                LocalizeControls(control.Controls, resources, cultureInfo);
                resources.ApplyResources(control, control.Name, cultureInfo);
            }
        }

        private void FormAccessRequest_Shown(object sender, System.EventArgs e)
        {
            // BUGFIX: Security warning appears in background.
            Focus();
            Activate();
        }
    }
}