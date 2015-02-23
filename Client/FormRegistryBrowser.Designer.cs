namespace Cleany
{
    /// <summary>
    /// Represent registry browser form.
    /// </summary>
    partial class FormRegistryBrowser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param KeyPath="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRegistryBrowser));
            this.treeRegistry = new System.Windows.Forms.TreeView();
            this.imageListItems = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.buttonOK = new DevExpress.XtraEditors.SimpleButton();
            this.buttonCancel = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // treeRegistry
            // 
            this.treeRegistry.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeRegistry.ImageIndex = 0;
            this.treeRegistry.ImageList = this.imageListItems;
            this.treeRegistry.Location = new System.Drawing.Point(12, 45);
            this.treeRegistry.Name = "treeRegistry";
            this.treeRegistry.SelectedImageIndex = 0;
            this.treeRegistry.Size = new System.Drawing.Size(298, 202);
            this.treeRegistry.TabIndex = 0;
            this.treeRegistry.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.treeRegistry_AfterCollapse);
            this.treeRegistry.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeRegistry_AfterExpand);
            // 
            // imageListItems
            // 
            this.imageListItems.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListItems.ImageStream")));
            this.imageListItems.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListItems.Images.SetKeyName(0, "key1.png");
            this.imageListItems.Images.SetKeyName(1, "cube_green.png");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select the key or value you want to add";
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(154, 253);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "&OK";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(235, 253);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "&Cancel";
            // 
            // FormRegistryBrowser
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(322, 288);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.treeRegistry);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(330, 315);
            this.Name = "FormRegistryBrowser";
            this.ShowInTaskbar = false;
            this.Text = "Registry";
            this.Load += new System.EventHandler(this.FormRegistryBrowser_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeRegistry;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton buttonOK;
        private DevExpress.XtraEditors.SimpleButton buttonCancel;
        private System.Windows.Forms.ImageList imageListItems;
    }
}