namespace Mass_helper
{
    partial class MassHelperRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        

        #region Код, автоматически созданный конструктором компонентов

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MassHelperRibbon));
            this.massHelperTab = this.Factory.CreateRibbonTab();
            this.serviceGroup = this.Factory.CreateRibbonGroup();
            this.SettingsMenu = this.Factory.CreateRibbonMenu();
            this.UpdateButton = this.Factory.CreateRibbonButton();
            this.ChangeSettingsButton = this.Factory.CreateRibbonButton();
            this.openPrilozhenieButton = this.Factory.CreateRibbonButton();
            this.helpButton = this.Factory.CreateRibbonButton();
            this.AboutButton = this.Factory.CreateRibbonButton();
            this.tipsGroup = this.Factory.CreateRibbonGroup();
            this.NotifyButton = this.Factory.CreateRibbonButton();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.massHelperTab.SuspendLayout();
            this.serviceGroup.SuspendLayout();
            this.tipsGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // massHelperTab
            // 
            this.massHelperTab.Groups.Add(this.serviceGroup);
            this.massHelperTab.Groups.Add(this.tipsGroup);
            this.massHelperTab.Label = "ПОМОЩНИК СС";
            this.massHelperTab.Name = "massHelperTab";
            // 
            // serviceGroup
            // 
            this.serviceGroup.Items.Add(this.SettingsMenu);
            this.serviceGroup.Items.Add(this.openPrilozhenieButton);
            this.serviceGroup.Items.Add(this.helpButton);
            this.serviceGroup.Items.Add(this.AboutButton);
            this.serviceGroup.Label = "Сервис";
            this.serviceGroup.Name = "serviceGroup";
            // 
            // SettingsMenu
            // 
            this.SettingsMenu.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.SettingsMenu.Dynamic = true;
            this.SettingsMenu.Image = global::Mass_helper.Properties.Resources.Tools1;
            this.SettingsMenu.Items.Add(this.UpdateButton);
            this.SettingsMenu.Items.Add(this.ChangeSettingsButton);
            this.SettingsMenu.ItemSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.SettingsMenu.Label = "Настройки";
            this.SettingsMenu.Name = "SettingsMenu";
            this.SettingsMenu.ShowImage = true;
            // 
            // UpdateButton
            // 
            this.UpdateButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.UpdateButton.Image = ((System.Drawing.Image)(resources.GetObject("UpdateButton.Image")));
            this.UpdateButton.Label = "Обновить настройки";
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.ShowImage = true;
            // 
            // ChangeSettingsButton
            // 
            this.ChangeSettingsButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.ChangeSettingsButton.Image = ((System.Drawing.Image)(resources.GetObject("ChangeSettingsButton.Image")));
            this.ChangeSettingsButton.Label = "Конфиграция";
            this.ChangeSettingsButton.Name = "ChangeSettingsButton";
            this.ChangeSettingsButton.ShowImage = true;
            // 
            // openPrilozhenieButton
            // 
            this.openPrilozhenieButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.openPrilozhenieButton.Image = global::Mass_helper.Properties.Resources.MS_Word;
            this.openPrilozhenieButton.Label = "Открыть приложение №3";
            this.openPrilozhenieButton.Name = "openPrilozhenieButton";
            this.openPrilozhenieButton.ShowImage = true;
            // 
            // helpButton
            // 
            this.helpButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.helpButton.Image = global::Mass_helper.Properties.Resources.Help;
            this.helpButton.Label = "Открыть справку";
            this.helpButton.Name = "helpButton";
            this.helpButton.ShowImage = true;
            // 
            // AboutButton
            // 
            this.AboutButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.AboutButton.Image = global::Mass_helper.Properties.Resources.Info2;
            this.AboutButton.Label = "О программе";
            this.AboutButton.Name = "AboutButton";
            this.AboutButton.ShowImage = true;
            // 
            // tipsGroup
            // 
            this.tipsGroup.Items.Add(this.NotifyButton);
            this.tipsGroup.Label = "Заметки";
            this.tipsGroup.Name = "tipsGroup";
            this.tipsGroup.Visible = false;
            // 
            // NotifyButton
            // 
            this.NotifyButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.NotifyButton.Image = global::Mass_helper.Properties.Resources.Danger;
            this.NotifyButton.Label = "Нажмите для просмотра заметок";
            this.NotifyButton.Name = "NotifyButton";
            this.NotifyButton.ShowImage = true;
            this.NotifyButton.Visible = false;
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.Description = "Путь к папке настроек (должна содержать папку Templates)";
            this.folderBrowserDialog.ShowNewFolderButton = false;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.Filter = "Database files|*.sqlite3";
            // 
            // MassHelperRibbon
            // 
            this.Name = "MassHelperRibbon";
            this.RibbonType = "Microsoft.Outlook.Mail.Compose";
            this.Tabs.Add(this.massHelperTab);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.MassHelperRibbon_Load);
            this.massHelperTab.ResumeLayout(false);
            this.massHelperTab.PerformLayout();
            this.serviceGroup.ResumeLayout(false);
            this.serviceGroup.PerformLayout();
            this.tipsGroup.ResumeLayout(false);
            this.tipsGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab massHelperTab;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup serviceGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup tipsGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu SettingsMenu;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton UpdateButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton ChangeSettingsButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton openPrilozhenieButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton helpButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton AboutButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton NotifyButton;

    }

    partial class ThisRibbonCollection
    {
        internal MassHelperRibbon MassHelperRibbon
        {
            get { return this.GetRibbon<MassHelperRibbon>(); }
        }
    }
}
