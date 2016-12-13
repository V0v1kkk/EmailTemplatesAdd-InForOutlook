using System;
using System.Threading;
using Masshelper.BL;
using Mass_helper.Properties;
using Microsoft.Office.Interop.Outlook;
using NLog;
using System.IO;
using System.Diagnostics;
using System.Reflection;

namespace Mass_helper
{
    public class MainPresenter
    {
        private static IMessageService _messageService;
        private static ILogic _logic;
        private static IMailCreator _mailCreator;

        static Logger _logger;

        public MainPresenter(IRibbonObserver observer, ILogic logic, IMessageService message, IMailCreator mailCreator) //В конструкторе получаем ссылку на экземляр логического класса, инициализируем переменные класса
        {
            _logic = logic;
            _logic.Error += _logic_Error;
            _messageService = message; //Передаем ссылку на службу соообщений (сообщений пользователю)
            _mailCreator = mailCreator;
            observer.RibbonCreate += delegate(object sender, RibbonEventArgs args)
            {
                args.RibbonInMail.CreateMailClick += _view_CreateMailClick; //Подписываемся на событие нажатия кнопки
                args.RibbonInMail.UpdateSettings += UpdateSettings; //Подписываемся на нажатие кнопки "обновить настройки"
                args.RibbonInMail.Open3 += _view_Open3; //Подписываемся на нажатие кнопки "Открыть приложение №3"
                args.RibbonInMail.OpenHelp += _view_OpenHelp; //Подписываемся на событие нажатия кнопки "открыть справку"
                args.RibbonInMail.PanelElements = _logic.InterfaceTree; //Передаём структуру меню в виде дерева для отображения на панели
            };
            observer.RibbonDispose += delegate(object sender, RibbonEventArgs args) //Отписываемся
            {
                args.RibbonInMail.CreateMailClick -= _view_CreateMailClick; 
                args.RibbonInMail.UpdateSettings -= UpdateSettings;
                args.RibbonInMail.Open3 -= _view_Open3;
                args.RibbonInMail.OpenHelp -= _view_OpenHelp;
            };


            _logger = LogManager.GetCurrentClassLogger();
            _logger.Info("Приложение запущено. Версия: " + Assembly.GetExecutingAssembly().GetName().Version);

            if (!_logic.Update(Settings.Default.databasePath, true)) { _messageService.ShowError(@"Ошибка получения настроек. Помощник СС не работает!"); }

            Thread autoUpdateThread = new Thread(AutoUpdateSettings) //Запускаем поток автообновления
            {
                IsBackground = true,
                Name = "AutoUpdateThread"
            }; 
            autoUpdateThread.Start();
        }

        void _logic_Error(object sender, EventArgs e) //Обработчик ошибки в логике
        {
            _messageService.ShowError(sender as string);
        }

        private void AutoUpdateSettings() //Отвечат за процесс обновления конфигурации в процессе работы
        {
            while (true)
            {
                Thread.Sleep(Settings.Default.AutoUpdatePeriod * 60000); //Период автообновленияя в минутах берём из пользовательских настроек
                if (Settings.Default.AutoUpdate) //Если автообновление включено в настройках
                {
                    _logger.Debug("Запущено автообновление конфигурации");
                    if (!_logic.Update(Settings.Default.databasePath, false)) _logger.Error("Автообновление не удалось");
                } 
            }
            // ReSharper disable once FunctionNeverReturns
        }

        static void UpdateSettings(object sender, EventArgs e) //Метод для обработки ручного обновления конфигурации
        {
            _logger.Debug("Обновление настроек запущено вручную");

            Thread updateThread = //Запуск обновление в анонимном методе
                new Thread(
                    new ParameterizedThreadStart(
                        delegate
                        {
                            if (!_logic.Update(Settings.Default.databasePath, true)) _logger.Error("Ручное обновление не удалось");
                        }))
                {
                    IsBackground = true,
                    Name = "ManualUpdateThread"
                }; 
            updateThread.Start();
        }
        
        static void _view_Open3(object sender, EventArgs e) //Метод для открытия приложения №3
        {
            try
            {
                if (Directory.Exists(@"\\fs-ekb-a0009\Отдел поддержки пользователей\Группа обработки обращений\!Общая ГОО")) //Если папка с приложением №3 существует
                {
                    string[] files = Directory.GetFiles(@"\\fs-ekb-a0009\Отдел поддержки пользователей\Группа обработки обращений\!Общая ГОО", "Приложение №3*"); //Находим подходящие файлы

                    string filenew = null; //Буферные переменные
                    DateTime datenew = DateTime.Parse("01.01.2001");

                    foreach (var file in files) //Прогоняем последовательно имена всех найденых файлов
                    {
                        String[] words = file.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries); //Отделяем дату в имени от всего остального
                        if (words.Length >= 2) //Если части получилось 2 и больше (должно быть 2)
                        {
                            String[] words1 = words[1].Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries); //Отделяем расширение файла от всего даты
                            if (words1.Length >= 2) //Если части 2 и больше (должно быть 2)
                            {
                                DateTime date = DateTime.Parse(words1[0].Replace(" ", ".")); //Заменяем проблы в дате на точки
                                if ((DateTime.Compare(date, datenew)) > 0) //Если у файла дата больше, чем у буфера, то заменить буфер
                                {
                                    datenew = date;
                                    filenew = file;
                                }
                            }
                        }
                    }
                    if (filenew != null) //Если мы нашли искомый файл
                    {
                        Process.Start(filenew); //Открываем его
                    }
                    else //Если не нашли выдаем ошибки
                    {
                        _logger.Error(@"Не удалось открыть приложение №3");
                        _messageService.ShowError(@"Не удалось открыть приложение №3");
                    }
                }
                else
                {
                    _logger.Error(@"Не удалось найти папку с приложением №3 (!Общая ГОО)");
                    _messageService.ShowError(@"Не удалось найти папку с приложением №3 (!Общая ГОО)");
                }
            }
            catch (System.Exception ex)
            {
                _logger.Error(@"Не удалось открыть приложение №3. Исключение: " + ex.Message);
                _messageService.ShowError(@"Не удалось открыть приложение №3");
            }
        }

        static void _view_OpenHelp(object sender, EventArgs e) //Метод для открытя справки
        {
            try
            {
                string patch = @"\\fs-ekb-a0009\Отдел поддержки пользователей\Группа обработки обращений\Старшие смены\Mass_helper\Help\Открыть записную книжку.onetoc2";
                if (File.Exists(patch))
                {
                    Process.Start(patch);
                }
                else
                {
                    _logger.Error(@"Не удалось открыть справку");
                    _messageService.ShowError(@"Не удалось открыть справку");
                }
            }
            catch(System.Exception ex)
            {
                _logger.Error(@"Не удалось открыть справку");
                _messageService.ShowError(@"Не удалось открыть справку. Исключение: " + ex.Message);
            }
        }

        static void _view_CreateMailClick(object sender, RibbonEventArgs e) //Сюда пробрасывается событие нажатия пользователем кнопки "создать письмо"
        {
            var mailItem = (MailItem)Globals.ThisAddIn.Application.ActiveInspector().CurrentItem;
            int buttonId = _logic.GetButtonId(sender.ToString());
            MailProperty prop = _logic.GetPropery(buttonId);
            if (prop!=null)
            {
                _logger.Info("Нажата кнопка с ID " + buttonId + ". Формирование письма: "+prop.Description);
                MailsTemplate temp = _logic.GetTemplate(prop.BodyID);
                // ReSharper disable once RedundantAssignment
                mailItem = _mailCreator.FillMail(mailItem, prop, temp, _logic.WorkTtNow); //Заполняем письмо

                mailItem.Recipients.ResolveAll(); //Распознать адреса


                string note = "";
                if (prop.Zametka1 != @"''") note += prop.Zametka1.Replace("/n", " "); //Заполнение заметок в письме
                if (prop.Zametka2 != @"''") note += Environment.NewLine + prop.Zametka2.Replace("/n", " ");
                if (prop.Zametka3 != @"''") note += Environment.NewLine + prop.Zametka3.Replace("/n", " ");
                e.RibbonInMail.Note = note;



                if (prop.Only6565 && !(mailItem.SendUsingAccount.SmtpAddress.Contains("6565"))) //Переключение ящика отправки на 6565
                {
                    bool flag = false;
                    foreach (Account account in Globals.ThisAddIn.Application.Session.Accounts)
                    {
                        if (account.SmtpAddress.Contains("6565"))
                        {
                            mailItem.SendUsingAccount = account;
                            flag = true;
                            _messageService.ShowMessage("Проверь подпись!");
                        }
                    }
                    if (!flag)
                    {
                        _logger.Info("Ну удалось переключить ящик на 6565");
                        _messageService.ShowMessage("Ящик отправки не удалось переключить на 6565. /n Отправка данного оповещения возможна только с ящика 6565");
                    }
                }

                if (prop.Only6690 && !(mailItem.SendUsingAccount.SmtpAddress.Contains("6690"))) //Переключение ящика отправки на 6690
                {
                    bool flag = false;
                    foreach (Account account in Globals.ThisAddIn.Application.Session.Accounts)
                    {
                        if (account.SmtpAddress.Contains("6690"))
                        {
                            mailItem.SendUsingAccount = account;
                            flag = true;
                            _messageService.ShowMessage("Проверь подпись!");
                        }
                    }
                    if (!flag)
                    {
                        _logger.Info("Ну удалось переключить ящик на 6690");
                        _messageService.ShowMessage("Ящик отправки не удалось переключить на 6690. /n Отправка данного оповещения возможна только с ящика 6690");
                    }
                }
                
            }
            else _logger.Error($"Кнопке {sender} не поставлен в соответствие обработчик");
        }
    }
}
