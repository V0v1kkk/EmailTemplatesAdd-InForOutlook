using System;
using System.IO;
using System.Windows.Forms;
using Mass_helper.Properties;

namespace Mass_helper.AdditionalForms
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();

            LoadState();

            choiceDBbutton.Click += ChoiceDBbutton_Click;
            saveButton.Click += (sender, args) => { SaveState(); Close(); };
            cancelButton.Click += (sender, args) => { Close(); };
        }

        private void LoadState()
        {
            currentDbTextBox.Text = Settings.Default.databasePath;
            autoUpdateCheckBox.Checked = Settings.Default.AutoUpdate;
            periodTextBox.Text = Settings.Default.AutoUpdatePeriod.ToString();
            showNotifyCheckBox.Checked = Settings.Default.DisplayNote;
            showNotifyForceCheckBox.Checked = Settings.Default.forceDisplayNote;
            tabLabelTextBox.Text = Settings.Default.tabLabel;
        }

        private void SaveState()
        {
            Settings.Default.databasePath = currentDbTextBox.Text;
            Settings.Default.AutoUpdate = autoUpdateCheckBox.Checked;
            int t;
            int.TryParse(periodTextBox.Text, out t);
            if (t > 0) Settings.Default.AutoUpdatePeriod = t;
            Settings.Default.DisplayNote = showNotifyCheckBox.Checked;
            Settings.Default.forceDisplayNote = showNotifyForceCheckBox.Checked;
            if (!string.IsNullOrWhiteSpace(tabLabelTextBox.Text)) Settings.Default.tabLabel = tabLabelTextBox.Text;

            Settings.Default.Save();
        }

        private void ChoiceDBbutton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(currentDbTextBox.Text))
            {
                if (Directory.Exists(Path.GetDirectoryName(Settings.Default.databasePath)))
                {
                    openFileDialog.InitialDirectory = Path.GetDirectoryName(Settings.Default.databasePath);
                }
                openFileDialog.FileName = Path.GetFileName(Settings.Default.databasePath);
            }
            else
            {
                if (Directory.Exists(Path.GetDirectoryName(currentDbTextBox.Text)))
                {
                    openFileDialog.InitialDirectory = Path.GetDirectoryName(currentDbTextBox.Text);
                }
                openFileDialog.FileName = Path.GetFileName(currentDbTextBox.Text);
            }
            
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                currentDbTextBox.Text = openFileDialog.FileName;
            }
        }
    }
}
