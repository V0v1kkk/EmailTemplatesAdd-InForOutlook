using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Masshelper.BL;
using Microsoft.Practices.Unity;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;

namespace Mass_helper
{
    public partial class ThisAddIn
    {

        //[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Tools.Office.ProgrammingModel.dll", "12.0.0.0")]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        protected override void Initialize()
        {
            //Разрешаем зависимости
            var container = new UnityContainer();
            container.RegisterType<IRibbonObserver, RibbonObserver>(new ContainerControlledLifetimeManager(), new InjectionFactory(c => RibbonObserver.GetInstance()));
            container.RegisterType<ILogic, Logic>(new ContainerControlledLifetimeManager() ,new InjectionFactory(c => Logic.GetInstance()));
            container.RegisterType<IMessageService, MessageService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMailCreator, MailCreator>(new ContainerControlledLifetimeManager(), new InjectionFactory(c => MailCreator.GetInstance()));

            MainPresenter presenter = container.Resolve<MainPresenter>();

            base.Initialize();
            this.Application = this.GetHostItem<Microsoft.Office.Interop.Outlook.Application>(typeof(Microsoft.Office.Interop.Outlook.Application), "Application");
            Globals.ThisAddIn = this;
            global::System.Windows.Forms.Application.EnableVisualStyles();
            this.InitializeCachedData();
            this.InitializeControls();
            this.InitializeComponents();
            this.InitializeData();
        }

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {

        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region Код, автоматически созданный VSTO

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion

        




        
    }
}
