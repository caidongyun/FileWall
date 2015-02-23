namespace VitaliiPianykh.FileWall.Client
{
    partial class FormBugReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBugReport));
            this.pictureBug = new DevExpress.XtraEditors.PictureEdit();
            this.labelTitle = new DevExpress.XtraEditors.LabelControl();
            this.labelAppeal = new DevExpress.XtraEditors.LabelControl();
            this.labelWhatYouDo = new DevExpress.XtraEditors.LabelControl();
            this.editWhatYouDid = new DevExpress.XtraEditors.MemoEdit();
            this.editEmail = new DevExpress.XtraEditors.TextEdit();
            this.labelEmail = new DevExpress.XtraEditors.LabelControl();
            this.buttonSend = new DevExpress.XtraEditors.SimpleButton();
            this.buttonDontSend = new DevExpress.XtraEditors.SimpleButton();
            this.labelShowDetails = new DevExpress.XtraEditors.LabelControl();
            this.editDetails = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBug.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editWhatYouDid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editDetails.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBug
            // 
            this.pictureBug.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBug.EditValue = global::VitaliiPianykh.FileWall.Client.Properties.Resources.Bug;
            this.pictureBug.Location = new System.Drawing.Point(12, 12);
            this.pictureBug.Name = "pictureBug";
            this.pictureBug.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureBug.Properties.Appearance.Options.UseBackColor = true;
            this.pictureBug.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureBug.Properties.ReadOnly = true;
            this.pictureBug.Properties.ShowMenu = false;
            this.pictureBug.Size = new System.Drawing.Size(46, 53);
            this.pictureBug.TabIndex = 33;
            // 
            // labelTitle
            // 
            this.labelTitle.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.labelTitle.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelTitle.Appearance.Options.UseBackColor = true;
            this.labelTitle.Appearance.Options.UseFont = true;
            this.labelTitle.Appearance.Options.UseForeColor = true;
            this.labelTitle.Location = new System.Drawing.Point(152, 12);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(233, 25);
            this.labelTitle.TabIndex = 34;
            this.labelTitle.Text = "FileWall problem";
            // 
            // labelAppeal
            // 
            this.labelAppeal.AllowHtmlString = true;
            this.labelAppeal.Appearance.ForeColor = System.Drawing.Color.Silver;
            this.labelAppeal.Appearance.Options.UseForeColor = true;
            this.labelAppeal.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelAppeal.Location = new System.Drawing.Point(12, 71);
            this.labelAppeal.Name = "labelAppeal";
            this.labelAppeal.Size = new System.Drawing.Size(373, 121);
            this.labelAppeal.TabIndex = 35;
            this.labelAppeal.Text = resources.GetString("labelAppeal.Text");
            // 
            // labelWhatYouDo
            // 
            this.labelWhatYouDo.Location = new System.Drawing.Point(12, 228);
            this.labelWhatYouDo.Name = "labelWhatYouDo";
            this.labelWhatYouDo.Size = new System.Drawing.Size(297, 13);
            this.labelWhatYouDo.TabIndex = 36;
            this.labelWhatYouDo.Text = "What were you doing when the problem happened (optional)?";
            // 
            // editWhatYouDid
            // 
            this.editWhatYouDid.Location = new System.Drawing.Point(12, 247);
            this.editWhatYouDid.Name = "editWhatYouDid";
            this.editWhatYouDid.Size = new System.Drawing.Size(373, 100);
            this.editWhatYouDid.TabIndex = 37;
            // 
            // editEmail
            // 
            this.editEmail.Location = new System.Drawing.Point(141, 353);
            this.editEmail.Name = "editEmail";
            this.editEmail.Size = new System.Drawing.Size(244, 20);
            this.editEmail.TabIndex = 38;
            // 
            // labelEmail
            // 
            this.labelEmail.Location = new System.Drawing.Point(12, 356);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(122, 13);
            this.labelEmail.TabIndex = 39;
            this.labelEmail.Text = "E-mail address (optional):";
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(229, 504);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(75, 23);
            this.buttonSend.TabIndex = 40;
            this.buttonSend.Text = "&Send";
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // buttonDontSend
            // 
            this.buttonDontSend.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonDontSend.Location = new System.Drawing.Point(310, 504);
            this.buttonDontSend.Name = "buttonDontSend";
            this.buttonDontSend.Size = new System.Drawing.Size(75, 23);
            this.buttonDontSend.TabIndex = 41;
            this.buttonDontSend.Text = "&Don\'t send";
            this.buttonDontSend.Click += new System.EventHandler(this.buttonDontSend_Click);
            // 
            // labelShowDetails
            // 
            this.labelShowDetails.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline);
            this.labelShowDetails.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.labelShowDetails.Appearance.Options.UseFont = true;
            this.labelShowDetails.Appearance.Options.UseForeColor = true;
            this.labelShowDetails.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelShowDetails.Location = new System.Drawing.Point(12, 379);
            this.labelShowDetails.Name = "labelShowDetails";
            this.labelShowDetails.Size = new System.Drawing.Size(166, 13);
            this.labelShowDetails.TabIndex = 42;
            this.labelShowDetails.Text = "To see the error report, click here.";
            this.labelShowDetails.Click += new System.EventHandler(this.labelShowDetails_Click);
            // 
            // editDetails
            // 
            this.editDetails.Location = new System.Drawing.Point(12, 398);
            this.editDetails.Name = "editDetails";
            this.editDetails.Properties.ReadOnly = true;
            this.editDetails.Size = new System.Drawing.Size(373, 100);
            this.editDetails.TabIndex = 43;
            // 
            // FormBugReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 534);
            this.Controls.Add(this.editDetails);
            this.Controls.Add(this.labelShowDetails);
            this.Controls.Add(this.buttonDontSend);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.labelEmail);
            this.Controls.Add(this.editEmail);
            this.Controls.Add(this.editWhatYouDid);
            this.Controls.Add(this.labelWhatYouDo);
            this.Controls.Add(this.labelAppeal);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.pictureBug);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormBugReport";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "FileWall";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.FormBugReport_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBug.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editWhatYouDid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editDetails.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PictureEdit pictureBug;
        private DevExpress.XtraEditors.LabelControl labelTitle;
        private DevExpress.XtraEditors.LabelControl labelAppeal;
        private DevExpress.XtraEditors.LabelControl labelWhatYouDo;
        private DevExpress.XtraEditors.MemoEdit editWhatYouDid;
        private DevExpress.XtraEditors.TextEdit editEmail;
        private DevExpress.XtraEditors.LabelControl labelEmail;
        private DevExpress.XtraEditors.SimpleButton buttonSend;
        private DevExpress.XtraEditors.SimpleButton buttonDontSend;
        private DevExpress.XtraEditors.LabelControl labelShowDetails;
        private DevExpress.XtraEditors.MemoEdit editDetails;
    }
}