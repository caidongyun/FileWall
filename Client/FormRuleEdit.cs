using System.IO;
using System.Windows.Forms;
using VitaliiPianykh.FileWall.Shared;
using Cleany;

namespace VitaliiPianykh.FileWall.Client
{
    public partial class FormRuleEdit : DevExpress.XtraEditors.XtraForm
    {
        private Ruleset.RulesRow _Rule;

        public FormRuleEdit()
        {
            InitializeComponent();
        }
        
        public Ruleset.RulesRow Rule
        {
            get
            {
                return null;
            }
            set
            {
                _Rule = value;

                if(_Rule == null)
                {
                    textName.Text = string.Empty;
                    textProtectedPath.Text = string.Empty;
                    textProcessPath.Text = string.Empty;
                    comboAction.SelectedIndex = 1;
                    checkLog.Checked = false;
                    buttonApply.Enabled = false;
                }
                else
                {
                    textName.Text = _Rule.Name;

                    if (_Rule.PathsRow != null)
                        textProtectedPath.Text = _Rule.PathsRow.Path;
                    else
                        textProtectedPath.Text = string.Empty;

                    if(_Rule.ProcessesRow != null)
                        textProcessPath.Text = _Rule.ProcessesRow.Path;
                    else
                        textProcessPath.Text = string.Empty;

                    if(_Rule.Action == RuleAction.Allow)
                        comboAction.SelectedIndex = 0;
                    else if (_Rule.Action == RuleAction.Ask)
                        comboAction.SelectedIndex = 1;
                    else if (_Rule.Action == RuleAction.Block)
                        comboAction.SelectedIndex = 2;

                    checkLog.Checked = _Rule.Log;

                    buttonApply.Enabled = false;
                }
            }
        }

        private void buttonApply_Click(object sender, System.EventArgs e)
        {

        }

        private void textName_EditValueChanged(object sender, System.EventArgs e)
        {
            buttonApply.Enabled = true;
        }

        private void buttonSuggestName_Click(object sender, System.EventArgs e)
        {
            var suggetsedName = string.Empty;

            if (comboAction.SelectedIndex == 0)
                suggetsedName = "Allow when ";
            else if (comboAction.SelectedIndex == 1)
                suggetsedName = "Ask when ";
            else if (comboAction.SelectedIndex == 2)
                suggetsedName = "Deny when ";

            suggetsedName += textProcessPath.Text== "*" ? "anybody " : Path.GetFileName(textProcessPath.Text) + " ";
            suggetsedName += "tries to access " + Path.GetFileName(textProtectedPath.Text);

            textName.Text = suggetsedName;
        }

        private void buttonBrowseFolder_Click(object sender, System.EventArgs e)
        {
            var frm = new FolderBrowserDialog();
            if(frm.ShowDialog() == DialogResult.Cancel)
                return;

            textProtectedPath.Text = frm.SelectedPath;
        }

        private void buttonBrowseRegistry_Click(object sender, System.EventArgs e)
        {
            string keyName;
            string valName;

            if(FormRegistryBrowser.Show(out keyName, out valName) == DialogResult.Cancel)
                return;
            textProtectedPath.Text = keyName;
        }

        private void buttonBrowseProcess_Click(object sender, System.EventArgs e)
        {
            var frm = new OpenFileDialog();
            if (frm.ShowDialog() == DialogResult.Cancel)
                return;

            textProcessPath.Text = frm.FileName;
        }
    }
}