using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Masshelper.BL;
using Microsoft.Office.Interop.Outlook;
using NLog;
using Exception = System.Exception;

namespace Mass_helper
{
    public interface IMailCreator
    {
        /// <summary>
        /// Заполняет данными переданный ему объект письма. 
        /// </summary>
        /// <param name="mail">Объект письма.</param>
        /// <param name="property">Объект описывающий такие параметры письма как тема, адресаты, важность и т.д.</param>
        /// <param name="template">Объект содержащий в себе тело письма.</param>
        /// <param name="workTtNow">Делегат принимающий string и возвращающий string. 
        /// Макрос #%workTT в адресатах или скрытой копии письма заменяется на значение, возвращенное делегатом.
        /// В случае, если макрос встретился в адресатах, в делегат передаётся строка "TO", если в скрытой копии,
        /// то строка "HCOPY".</param>
        /// <returns>Обработанный объект письма.</returns>
        MailItem FillMail(MailItem mail, MailProperty property, MailsTemplate template, Func<string, string> workTtNow);
    }
    public class MailCreator : IMailCreator
    {
        private static MailCreator _instance;
        private static readonly object SyncRoot = new Object();
        static Logger _logger;

        private MailCreator()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public static MailCreator GetInstance()
        {
            if (_instance == null)
            {
                lock (SyncRoot)
                {
                    if (_instance == null) _instance = new MailCreator();
                }
            }
            return _instance;
        }


        public MailItem FillMail(MailItem mail, MailProperty property, MailsTemplate template, Func<string, string> workTtNow) //Заполнить письмо
        {
            string temp;
            if (property.FillTO)
            {
                if (property.TO.Contains(@"#%workTT"))
                {
                    temp = ";" + property.TO.Replace(@"#%workTT", workTtNow("TO"));
                    //Замена макроса для вставки адресов работающих ТТ
                }
                else
                {
                    temp = ";" + property.TO;
                }

                if (temp != ";")
                {
                    temp = mail.To + temp;
                    mail.To = "";
                    mail.To = temp;
                }
            }
            if (property.FillCopy)
            {
                temp = mail.CC + ";" + property.Copy;
                mail.CC = "";
                mail.CC = temp;
            }
            if (property.FillHideCopy)
            {
                if (property.HideCopy.Contains(@"#%workTT")) temp = ";" + property.HideCopy.Replace(@"#%workTT", workTtNow("HCOPY")); //Замена макроса для вставки адресов работающих ТТ
                else temp = ";" + property.HideCopy;
                if (temp != ";")
                {
                    temp = mail.BCC + temp;
                    mail.BCC = "";
                    mail.BCC = temp;
                }
            }
            if (property.FillSubject)
            {
                temp = ReplaceMacros(property.Subject);
                mail.Subject = temp;
            }
            if (property.HighImportance)
            {
                mail.Importance = OlImportance.olImportanceHigh;
            }
            if (property.Reminder)
            {
                //Настройки уведомления
                mail.FlagRequest = "К исполнению";
                mail.FlagIcon = OlFlagIcon.olRedFlagIcon;
                DateTime tmpTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, property.ReminderTime.Hour, property.ReminderTime.Minute, 0);
                mail.ReminderTime = tmpTime;
                mail.ReminderOverrideDefault = true;
                mail.ReminderSet = true;
            }
            if (property.FillBody)
            {
                //Тут нужно немного улучшить код, чтобы в случае проблем не убирались перносы строки и без лишних обращений к mail.HTMLBody
                for (int j = 0; j < 1; j++) //Удаление переносов строки в письме
                {
                    var i = mail.HTMLBody.IndexOf(@"<o:p>&nbsp;</o:p>", StringComparison.Ordinal);
                    if (i != -1)
                    {
                        mail.HTMLBody = mail.HTMLBody.Remove(i, 17);
                    }
                }
                try //Пробуем прочитать шаблон
                {
                    string mailbody = Encoding.GetEncoding("windows-1251").GetString //Извлечения шаблона из БД
                        (Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding("windows-1251"), template.TemplateBody));
                    if (string.IsNullOrEmpty(mailbody)) //Если строка с телом письма пуста или null, то сообщить об этом, выйти из метода
                    {
                        _logger.Error(@"Не удалось заполнить шаблон письма. Шаблон пуст");
                        return mail;

                    }
                    mail.HTMLBody = ReplaceMacros(mailbody) + mail.HTMLBody;
                }
                catch (Exception exception) //Если возникла ошибка, то пишем в лог, и возращаем обратно письмо в неизменном виде
                {
                    _logger.Error("Не удалось прочитать шаблон при построении письма: " + exception);
                    mail.HTMLBody = mail.HTMLBody;
                }
            }
            return mail;
        }



        private string ReplaceMacros(string input) //Метод для замены макросов
        {
            //Добавлять длинные шаблоны только сверху. Короткие могут ломать длинные
            input = input.Replace(@"#%prevdaymonth", DateTime.Today.AddDays(-1).ToString("d MMMM",
                    CultureInfo.CreateSpecificCulture("ru-RU"))); // #%prevdaymonth - предыдущий день месяц и год в формате "14 мая"
            input = input.Replace(@"#%yearshortprevday", DateTime.Today.AddDays(-1).ToString("dd.MM.yy",
                    CultureInfo.CreateSpecificCulture("ru-RU"))); // #%yearshortprevday - предыдущий день месяц и год в формате "08.10.15"
            input = input.Replace(@"#%shortprevday", DateTime.Today.AddDays(-1).ToString("dd.MM",
                    CultureInfo.CreateSpecificCulture("ru-RU"))); // #%prevdayshort - предыдущий день месяц и год в формате "08.10"
            input = input.Replace(@"#%onlymonthprevday", DateTime.Today.AddDays(-1).ToString("MMMM",
                    CultureInfo.CreateSpecificCulture("ru-RU")).ToLower(CultureInfo.CreateSpecificCulture("ru-RU"))); // месяц к которому относится предыдущий день в формате "май"
            input = input.Replace(@"#%onlydateprevday", DateTime.Today.AddDays(-1).ToString("dd",
                    CultureInfo.CreateSpecificCulture("ru-RU"))); // #%prevdayonlydate - предыдущий день месяц и год в формате "08"
            input = input.Replace(@"#%tomorrowdaymonth", DateTime.Today.AddDays(1).ToString("d MMMM",
                    CultureInfo.CreateSpecificCulture("ru-RU"))); // #%tomorrowdaymonth - завтрашний день и месяц в формате "14 мая"
            input = input.Replace(@"#%daymonth", DateTime.Now.ToString("d MMMM",
                    CultureInfo.CreateSpecificCulture("ru-RU"))); // #%daymonth - день и месяц в формате "14 мая"
            input = input.Replace(@"#%prevday", DateTime.Today.AddDays(-1).ToString("dd.MM.yyyy",
                    CultureInfo.CreateSpecificCulture("ru-RU"))); // #%prevday - предыдущий день месяц и год в формате "01.05.2015"
            input = input.Replace(@"#%tomorrow", DateTime.Today.AddDays(1).ToString("dd.MM.yyyy",
                    CultureInfo.CreateSpecificCulture("ru-RU"))); // #%tomorrow - завтрашний день месяц и год в формате "01.05.2015"

            return input;
        }
    }
}
