using System;
using Mass_helper.Properties;
using Microsoft.Office.Tools.Ribbon;
using Office = Microsoft.Office.Core;
using System.Collections.Generic;
using System.Drawing;
using Masshelper.BL;
using Mass_helper.AdditionalForms;

// ReSharper disable TryCastAlwaysSucceeds


// ReSharper disable once CheckNamespace
namespace Mass_helper
{
    public interface IRibbonInMail
    {
        string Note { set; }

        List<PanelElement> PanelElements { set; }

        event RibbonEventHandler CreateMailClick; //Событие нажатия кнопки "сформироваь письмо"
        event EventHandler UpdateSettings; //Событие включения ручного обновления настроек
        event EventHandler Open3; //Нажатие на кнопку открытия приложения №3
        event EventHandler OpenHelp; //Нажатие на кнопку справки
    }
    public partial class MassHelperRibbon: IRibbonInMail
    {
        private int _currentGroupPosition;
        private string _note;

        public string Note
        {
            set
            {
                _note = value;
                if (Settings.Default.DisplayNote && !String.IsNullOrWhiteSpace(value))
                {
                    NotifyButton.Visible = true;
                    tipsGroup.Visible = true;
                    if (Settings.Default.forceDisplayNote) new NotesForm(_note).Show();
                }
                else
                {
                    NotifyButton.Visible = false;
                    tipsGroup.Visible = false;
                }
            }
        }

        public List<PanelElement> PanelElements
        {
            set
            {
                if (value != null)
                {
                    CreateInterfaceRecurcive(value, null);
                }
            }
        }


        public event RibbonEventHandler CreateMailClick;
        public event EventHandler UpdateSettings;
        public event EventHandler Open3;
        public event EventHandler OpenHelp;


        public MassHelperRibbon() : base(Globals.Factory.GetRibbonFactory()) //Конструктор панели (панель вызывается пользователем как часть окна письма)
        {
            InitializeComponent();
            RibbonObserver.AddRibbon(this); // Регистрируемся в неком подобии сервис-локатора

            massHelperTab.Label = Settings.Default.tabLabel;

            UpdateButton.Click += UpdateButton_Click;
            ChangeSettingsButton.Click += ChangeSettingsButton_Click;
            openPrilozhenieButton.Click += openPrilozhenieButton_Click;
            helpButton.Click += helpButton_Click;
            AboutButton.Click += aboutButton_Click;
            NotifyButton.Click += NotifyButton_Click;
        }

        private void MassHelperRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            if (Settings.Default.firstStart)
            {
                Settings.Default.firstStart = false;
                Settings.Default.Save();
                new SettingsForm().ShowDialog();
            }
        }

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing) RibbonObserver.DeleteRibbbon(this);

            if (disposing)
            {
                components?.Dispose();
            }
            base.Dispose(disposing);

            GC.Collect(); //test
        }



        private void CreateInterfaceRecurcive(List<PanelElement> elements, RibbonComponent parent)
        {
            if (parent == null)
            {
                foreach (PanelElement panelElement in elements)
                {
                    if ((panelElement.Parent == null) && (panelElement is GroupElement))
                    {
                        GroupElement groupElement = panelElement as GroupElement;
                        RibbonGroup group = Factory.CreateRibbonGroup();
                        group.Name = groupElement.Name;
                        group.Label = groupElement.Label;
                        massHelperTab.Groups.Insert(_currentGroupPosition, group);
                        _currentGroupPosition++;

                        if(groupElement.Childs.Count !=0 ) CreateInterfaceRecurcive(groupElement.Childs, group);
                    }
                }
            }
            else
            {
                foreach (PanelElement panelElement in elements)
                {
                    if (panelElement is MenuElement)
                    {
                        MenuElement menuElement = panelElement as MenuElement;
                        RibbonMenu menu = Factory.CreateRibbonMenu();
                        menu.Label = menuElement.Label;
                        menu.Name = menuElement.Name;
                        if (menuElement.ScreenTip != null) menu.ScreenTip = menuElement.ScreenTip;
                        if (menuElement.SuperTip != null) menu.SuperTip = menuElement.SuperTip;
                        menu.Image = menuElement.Image ?? GetParentImageRecurcive(menuElement.Parent);
                        if (menu.Image != null) menu.ShowImage = true;

                        if (parent is RibbonGroup)
                        {
                            RibbonGroup ribbonGroup = parent as RibbonGroup;
                            menu.ControlSize = Office.RibbonControlSize.RibbonControlSizeLarge;
                            ribbonGroup.Items.Add(menu);
                        }
                        else if (parent is RibbonMenu)
                        {
                            RibbonMenu ribbonMenu = parent as RibbonMenu;
                            menu.ControlSize = Office.RibbonControlSize.RibbonControlSizeRegular;
                            ribbonMenu.Items.Add(menu);
                        }

                        if (menuElement.Childs.Count != 0) CreateInterfaceRecurcive(menuElement.Childs, menu);
                    }

                    else if (panelElement is ButtonElement)
                    {
                        ButtonElement buttonElement = panelElement as ButtonElement;
                        RibbonButton button = Factory.CreateRibbonButton();
                        button.Label = buttonElement.Label;
                        if (buttonElement.TemplateNo.HasValue)
                        {
                            button.Name = buttonElement.Name + "_" + buttonElement.TemplateNo;
                        }
                        else
                        {
                            button.Name = buttonElement.Name + "_0";
                        }
                        if (buttonElement.ScreenTip != null) button.ScreenTip = buttonElement.ScreenTip;
                        if (buttonElement.SuperTip != null) button.SuperTip = buttonElement.SuperTip;
                        button.Image = buttonElement.Image ?? GetParentImageRecurcive(buttonElement.Parent);
                        if (button.Image != null) button.ShowImage = true;
                        button.Click += button_Click;

                        if (parent is RibbonGroup)
                        {
                            RibbonGroup ribbonGroup = parent as RibbonGroup;
                            button.ControlSize = Office.RibbonControlSize.RibbonControlSizeLarge;
                            ribbonGroup.Items.Add(button);
                        }
                        else if (parent is RibbonMenu)
                        {
                            RibbonMenu ribbonMenu = parent as RibbonMenu;
                            button.ControlSize = Office.RibbonControlSize.RibbonControlSizeRegular;
                            ribbonMenu.Items.Add(button);
                        }
                    }

                    else if (panelElement is SeparatorElement)
                    {
                        SeparatorElement separatorElement = panelElement as SeparatorElement;
                        RibbonSeparator separator = Factory.CreateRibbonSeparator();
                        separator.Name = separatorElement.Name;

                        if (parent is RibbonGroup)
                        {
                            RibbonGroup ribbonGroup = parent as RibbonGroup;
                            ribbonGroup.Items.Add(separator);
                        }
                        else if (parent is RibbonMenu)
                        {
                            RibbonMenu ribbonMenu = parent as RibbonMenu;
                            ribbonMenu.Items.Add(separator);
                        }
                    }
                }
            }
        }
        private Image GetParentImageRecurcive(PanelElement panelElement)
        {   
            if (panelElement is MenuElement)
            {
                MenuElement tMenuElement = panelElement as MenuElement;
                if (tMenuElement.Image != null) return tMenuElement.Image;
                if (tMenuElement.Parent != null) return GetParentImageRecurcive(tMenuElement.Parent);
            }
            return null;
        }


        private void CheckButton(object sender)
        {
            if (CreateMailClick != null)
            {
                var ribbonButton = sender as RibbonButton;
                if (ribbonButton != null)
                    CreateMailClick(ribbonButton.Name, new RibbonEventArgs(this));
            }
        }


        #region Buttons handlers
        private void button_Click(object sender, RibbonControlEventArgs e)
        {
            CheckButton(sender);
        }


        private void aboutButton_Click(object sender, RibbonControlEventArgs e)
        {
            AboutBox a = new AboutBox();
            a.ShowDialog();
        }
        private void UpdateButton_Click(object sender, RibbonControlEventArgs e)
        {
            UpdateSettings?.Invoke(sender, EventArgs.Empty);
        }
        private void openPrilozhenieButton_Click(object sender, RibbonControlEventArgs e)
        {
            Open3?.Invoke(sender, EventArgs.Empty);
        }
        private void helpButton_Click(object sender, RibbonControlEventArgs e)
        {
            OpenHelp?.Invoke(sender, EventArgs.Empty);
        }
        private void NotifyButton_Click(object sender, RibbonControlEventArgs e)
        {
            NotesForm notesForm = new NotesForm(_note);
            notesForm.Show();
        }
        private void ChangeSettingsButton_Click(object sender, RibbonControlEventArgs e)
        {
            new SettingsForm().ShowDialog();
        }

        #endregion

    }
}
