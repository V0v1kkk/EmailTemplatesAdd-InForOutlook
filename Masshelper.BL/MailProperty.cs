using System;

namespace Masshelper.BL
{
    [Serializable]
    public class MailProperty
    {
        public int ButtonID { get; set; } //ID кнопки
        public string Description { get; set; } //Описание
        public bool Only6565 { get; set; } //Изменять ли ящик на 6565 при заполнении по шаблону
        public bool Only6690 { get; set; } //Изменять ли ящик на 6690 при заполнении по шаблону
        public bool FillTO { get; set; } //Заполнять адресатов
        public string TO { get; set; } //Адресаты
        public bool FillCopy { get; set; } //Заполнять копию
        public string Copy { get; set; } // Копия
        public bool FillHideCopy { get; set; } //Заполнять скрытую копию
        public string HideCopy { get; set; } //Скрытая копия
        public bool FillSubject { get; set; } //Заполнять тему (на случай ответа на письмо)
        public string Subject { get; set; } //Тема
        public bool HighImportance { get; set; } //Высокая важность
        public bool Reminder { get; set; } //Ставить ли напоминание
        public DateTime ReminderTime { get; set; } //Время напоминания
        public bool FillBody { get; set; } //Заполнять текст письма
        public int BodyID { get; set; } //Текст письма
        public string Zametka1 { get; set; } //Заметка 1я строка
        public string Zametka2 { get; set; } //Заметка 2я строка
        public string Zametka3 { get; set; } //Заметка 3я строка

        public MailProperty()
        {
            ButtonID = 0;
            Description = "";
            Only6565 = true;
            Only6690 = false;
            FillTO = false;
            TO = "";
            FillCopy = false;
            Copy = "";
            FillHideCopy = false;
            HideCopy = "";
            FillSubject = false;
            Subject = "";
            HighImportance = false;
            Reminder = false;
            ReminderTime = DateTime.Now;
            FillBody = false;
            BodyID = 0;
            Zametka1 = "";
            Zametka2 = "";
            Zametka3 = "";

        }
        public MailProperty(int buttonid, string description, bool only6565, bool only6690, bool fillTO, string tO, bool fillCopy, string copy, bool fillHideCopy, string hideCopy,
            bool fillsubject, string subject, bool highimportance, bool reminder, DateTime remindertime, bool fillBody,
            string zametka1, string zametka2, string zametka3, int bodyID)
        {
            ButtonID = buttonid;
            Description = description;
            Only6565 = only6565;
            Only6690 = only6690;
            FillTO = fillTO;
            TO = tO;
            FillCopy = fillCopy;
            Copy = copy;
            FillHideCopy = fillHideCopy;
            HideCopy = hideCopy;
            FillSubject = fillsubject;
            Subject = subject;
            HighImportance = highimportance;
            Reminder = reminder;
            ReminderTime = remindertime;
            FillBody = fillBody;
            BodyID = bodyID;
            Zametka1 = zametka1;
            Zametka2 = zametka2;
            Zametka3 = zametka3;
        }
    }
}