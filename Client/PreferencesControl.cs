using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Windows.Forms;
using VitaliiPianykh.FileWall.Client.Properties;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.XtraEditors;


namespace VitaliiPianykh.FileWall.Client
{
    public partial class PreferencesControl : XtraUserControl
    {
        private readonly Dictionary<int, CultureInfo> cultureInfos = new Dictionary<int, CultureInfo>();

        public PreferencesControl()
        {
            InitializeComponent();
        }

        private void PreferencesControl_Load(object sender, System.EventArgs e)
        {
            // Load list of skins
            foreach (SkinContainer skin in SkinManager.Default.Skins)
                comboSkin.Properties.Items.Add(skin.SkinName);
            comboSkin.SelectedItem = Settings.Default.SkinName;

            // Populate the list of languages.
            var index = comboLanguage.Properties.Items.Add("default (English)");
            cultureInfos.Add(index, CultureInfo.InvariantCulture);
            foreach (var cultureInfo in GetSupportedCultureInfos())
            {
                index = comboLanguage.Properties.Items.Add(cultureInfo.NativeName);
                cultureInfos.Add(index, cultureInfo);
            }

            // Select current language.
            var selectedCultureName = Settings.Default.UICulture.NativeName;
            if (comboLanguage.Properties.Items.Contains(selectedCultureName))
                comboLanguage.SelectedIndex = comboLanguage.Properties.Items.IndexOf(selectedCultureName);
            else
                comboLanguage.SelectedIndex = 0;

            comboLanguage.SelectedIndexChanged += comboLanguage_SelectedIndexChanged;
        }

        private void comboLanguage_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Settings.Default.UICulture = cultureInfos[comboLanguage.SelectedIndex];
            Settings.Default.Save();

            XtraMessageBox.Show(Resources.WillRestart, "FileWall", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            Application.Restart();
        }

        private static CultureInfo[] GetSupportedCultureInfos()
        {
            var resourceManager = new ResourceManager(typeof(FormMain));
            resourceManager.GetObject("FormMain.Text", CultureInfo.InvariantCulture);

            ResourceSet resourceSetOriginal;
            var resourceSetParent = resourceManager.GetResourceSet(CultureInfo.InvariantCulture, false, false);
            var arrayCultures = new ArrayList();
            foreach (CultureInfo cultureInfo in CultureInfo.GetCultures(CultureTypes.InstalledWin32Cultures))
            {
                resourceSetOriginal = resourceManager.GetResourceSet(cultureInfo, false, true);
                if (resourceSetOriginal != resourceSetParent)
                    arrayCultures.Add(cultureInfo);
            }
            resourceManager.ReleaseAllResources();
            return (CultureInfo[])arrayCultures.ToArray(typeof(CultureInfo));
        }

        private void comboSkin_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var Default = UserLookAndFeel.Default;
            Default.SetSkinStyle((string)comboSkin.SelectedItem);

            Settings.Default.SkinName = (string)comboSkin.SelectedItem;
            Settings.Default.Save();
        }
    }
}
