using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using VitaliiPianykh.FileWall.Shared;


namespace Cleany
{
    /// <summary>
    /// Represents registry browser form.
    /// </summary>
    public partial class FormRegistryBrowser : XtraForm
    {
        #region Constructors

        private FormRegistryBrowser()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Shows RegistryBrowser form as dialog.
        /// </summary>
        /// <param KeyPath="Key">Name of the key selected by user</param>
        /// <param KeyPath="ValueName">Name of value selected by user. If user didn't selected value returns null</param>
        public static DialogResult Show(out String KeyName, out String ValueName)
        {
            FormRegistryBrowser form = new FormRegistryBrowser();
            DialogResult result = form.ShowDialog();

            KeyName = form.KeyName;
            ValueName = form.ValueName;

            return result;
        }

        #endregion

        #region Private Properties

        /// <summary>
        /// Name of the key selected by user.
        /// If no no keys selected returns null.
        /// If user selected value - returns key to which this value belongs to.
        /// </summary>
        private String KeyName
        {
            get
            {
                // No selection?
                if (treeRegistry.SelectedNode == null)
                    return null;

                if ((String)treeRegistry.SelectedNode.Tag == "key")
                    return treeRegistry.SelectedNode.FullPath;
                else if ((String)treeRegistry.SelectedNode.Tag == "val")
                    return Regex.Replace(treeRegistry.SelectedNode.FullPath, "(.*)\\\\(?:.*)", "$1");
                else
                    return null;
            }
        }

        /// <summary>
        /// Value selected by user.
        /// If no value selected - returns null.
        /// </summary>
        private String ValueName
        {
            get
            {
                if (treeRegistry.SelectedNode == null)
                    return null;

                if ((String)treeRegistry.SelectedNode.Tag == "val")
                    return Regex.Replace(treeRegistry.SelectedNode.FullPath, "(?:.*)\\\\(.*)", "$1");
                else
                    return null;
            }
        }

        #endregion

        #region Controls event handlers

        private void FormRegistryBrowser_Load(object sender, EventArgs e)
        {
            // Populate treeview with root keys such as HKEY_CURRENT_USER
            // Other keys will be populated "on demand"

            RegistryKey[] RootKeys = new RegistryKey[]
            {
                Registry.ClassesRoot,
                Registry.CurrentConfig,
                Registry.CurrentUser,
                Registry.DynData,
                Registry.LocalMachine,
                Registry.PerformanceData,
                Registry.Users
            };

            foreach (RegistryKey CurrentKey in RootKeys)
            {
                TreeNode CurrentTreeNode = treeRegistry.Nodes.Add(CurrentKey.Name);
                CurrentTreeNode.ImageIndex = 0;
                CurrentTreeNode.Nodes.Add("\\dummy", String.Empty);
                CurrentTreeNode.Tag = "key";
            }
        }

        private void treeRegistry_AfterExpand(object sender, TreeViewEventArgs e)
        {
            TreeNode CurrentTreeNode = e.Node;

            try
            {
                // Remove dummy node.
                CurrentTreeNode.Nodes["\\dummy"].Remove();

                // Open key associated with current node.
                RegistryKey CurrentKey = AdvRegistry.OpenSubKey(CurrentTreeNode.FullPath);

                // Populate keys.
                foreach (String CurrentSubKeyName in CurrentKey.GetSubKeyNames())
                    AddKey(CurrentTreeNode, CurrentSubKeyName);

                // Populate values.
                foreach (String CurrentValueName in CurrentKey.GetValueNames())
                    AddValue(CurrentTreeNode, CurrentValueName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void treeRegistry_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            TreeNode CurrentTreeNode = e.Node;

            CurrentTreeNode.Nodes.Clear();
            CurrentTreeNode.Nodes.Add("\\dummy", String.Empty);
        }

        private void AddKey(TreeNode Node, String SubKeyName)
        {
            TreeNode AddedNode = Node.Nodes.Add(SubKeyName);
            AddedNode.ImageIndex = 0;
            AddedNode.Nodes.Add("\\dummy", String.Empty);
            AddedNode.Tag = "key";
        }

        private void AddValue(TreeNode Node, String ValueName)
        {
            TreeNode AddedNode = Node.Nodes.Add(ValueName);
            AddedNode.ImageIndex = 1;
            AddedNode.SelectedImageIndex = 1;
            AddedNode.Tag = "val" ;
        }

        #endregion
    }
}