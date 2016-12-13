using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using NLog;
using Exception = System.Exception;
using System.Data.SQLite;
using System.Collections.Generic;

namespace Masshelper.BL
{
    public interface ILogic
    {
        /// <summary>
        /// Возвращает ID кнопки переданной (сплитит строку)
        /// </summary>
        /// <param name="buttonName">Имя кнопки</param>
        /// <returns>ID кнопки</returns>
        int GetButtonId(string buttonName);
        
        /// <summary>
        /// Возвращает объект представляющий тело письма
        /// </summary>
        /// <param name="templateid">Номер шаблона</param>
        /// <returns>Объект представляющий тело письма</returns>
        MailsTemplate GetTemplate(int templateid);
        
        /// <summary>
        /// Возвращает объект представляющий собой шаблон письма. Содержит в себе адресатов, тему, заметки и т.д.
        /// </summary>
        /// <param name="buttonId">ID шаблона</param>
        /// <returns>Объект представляющий собой шаблон письма</returns>
        MailProperty GetPropery(int buttonId);
        
        /// <summary>
        /// Коллекция элементов интерфейса представленных в виде дерева
        /// </summary>
        List<PanelElement> InterfaceTree { get; }

        /// <summary>
        /// Выполняет загрузку данных из БД в DataSet
        /// </summary>
        /// <param name="databasePath">Путь к БД SQLite</param>
        /// <param name="force">Необходимо ли обновлять данные, если к этому нет предпосылок</param>
        /// <returns>Истину в случае, если данные были загружены (или обновлены) из БД.</returns>
        bool Update(string databasePath, bool force);

        /// <summary>
        /// Возвращает список e-mail адресов работающих сейчас ТТ
        /// </summary>
        /// <param name="field">"TO" (адресаты) или "HCOPY" (скрытая копия)
        /// - указатель на то для какого поля предназначены данные </param>
        /// <returns>e-mail адреса разделённые ';'</returns>
        string WorkTtNow(string field);

        /// <summary>
        /// Возникает в случае возникновения ошибкок.
        /// Текст ошибки передаётся в виде строки 1м агрументом
        /// </summary>
        event EventHandler Error;

    }
    public class Logic: ILogic
    {
        private static Logic _instance;

        public event EventHandler Error;
        private static DataSet VirtualDb { get; set; }

        private static readonly object SyncRoot = new Object(); //Возможно можно использовать имеющиеся локи
        private static readonly object ThreadLock = new object();
        private static readonly object ThreadLockGetTree = new object();

        private bool _firstGetDataSuccsess; // Хранит сведенья о том была ли произведена успешная загрузка данных в текущем сеансе

        private readonly Logger _logger = LogManager.GetCurrentClassLogger(); //Презентер будет обращаться к этому логгеру

        private Logic()
        {
            VirtualDb=new DataSet();
        }

        public static Logic GetInstance()
        {
            if (_instance == null)
            {
                lock (SyncRoot)
                {
                    if (_instance == null) _instance = new Logic();
                }
            }
            return _instance;
        }


        private List<PanelElement> _interfaceTree;
        public List<PanelElement> InterfaceTree
        {
            get
            {
                if (_interfaceTree != null) return _interfaceTree;
                _interfaceTree = PanelElement.CreateTree(GetFlatMenuElements());
                return _interfaceTree;
            }
            private set
            {
                _interfaceTree = value;

            }
        }

        private void FlatMenuElementsToTree()
        {
            try
            {
                //Monitor.Enter(ThreadLockGetTree);
                InterfaceTree = PanelElement.CreateTree(GetFlatMenuElements());
            }
            catch (Exception ex)
            {
                _logger.Fatal("Ошибка получения элементов пользовательского интерфейса. Ошибка: " + ex.Message);
                //Monitor.Exit(ThreadLockGetTree);
            }
        }

        private List<PanelElement> GetFlatMenuElements()
        {
            if (VirtualDb.Tables.Count == 0) return null; //Если в памяти нет данных, то возвращаем null

            List<PanelElement> list = new List<PanelElement>();

            foreach (DataRow row in VirtualDb.Tables["Generated"].Rows)
            {
                string name = String.IsNullOrWhiteSpace(row["Name"].ToString()) ? null : row["Name"].ToString();
                string parentName = String.IsNullOrWhiteSpace(row["ParentName"].ToString()) ? null : row["ParentName"].ToString();
                string label = String.IsNullOrWhiteSpace(row["Label"].ToString()) ? null : row["Label"].ToString();
                string superTup = String.IsNullOrWhiteSpace(row["SuperTip"].ToString()) ? null : row["SuperTip"].ToString();
                string screenTip = String.IsNullOrWhiteSpace(row["ScreenTip"].ToString()) ? null : row["ScreenTip"].ToString();
                string image = String.IsNullOrWhiteSpace(row["Image"].ToString()) ? null : row["Image"].ToString();
                string templateno = String.IsNullOrWhiteSpace(row["TemplateNO"].ToString()) ? null : row["TemplateNO"].ToString();

                if (row["IsButton"].Equals(true))
                {
                    int t;
                    list.Add(new ButtonElement(name, parentName, label, superTup, screenTip, image, Int32.TryParse(templateno, out t) ? t : (int?)null));
                }
                else if (row["IsMenu"].Equals(true))
                {
                    list.Add(new MenuElement(name, parentName, label, superTup, screenTip, image));
                }
                else if (row["IsSeparator"].Equals(true))
                {
                    list.Add(new SeparatorElement(name, parentName));
                }
                else if (row["IsGroup"].Equals(true))
                {
                    list.Add(new GroupElement(name, parentName, label));
                }
            }
            return list.Count == 0 ? null : list;
        }


        public bool Update(string databasePath, bool force)
        {
            try
            {
                if (force)
                {
                    return ForceUpdateSetting(databasePath);
                }
                else
                {
                    if ((VirtualDb.Tables.Count != 0) && File.GetLastWriteTime(databasePath) != (DateTime)VirtualDb.ExtendedProperties["LastModiff"])
                    {
                        _logger.Debug("Кеш не нуждается в обновлении");
                        return false;
                    }
                    return ForceUpdateSetting(databasePath);
                }
            }
            catch (Exception exception)
            {
                if (force) _logger.Debug("Произошла ошибка при начальном или ручном обновлении: " + exception.Message);
                else _logger.Debug("Произошла ошибка при автоматическом обновлении: " + exception.Message);

                return false;
            }
        }

        private bool ForceUpdateSetting(string databasePath)
        {
            if (File.Exists(databasePath))
            {
                if (FillDataSet(databasePath)) return true;
            }
            //todo: убрать отладочную ф-ю
            else if (File.Exists(Environment.GetEnvironmentVariable("LOCALAPPDATA") + @"\Mass helper\base.sqlite3"))
            {
                if (FillDataSet(Environment.GetEnvironmentVariable("LOCALAPPDATA") + @"\Mass helper\base.sqlite3")) return true;
            }
            return false;
        }

        private bool FillDataSet(string basepath)
        {
            //Monitor.Enter(ThreadLock);

            lock (this)
            {
                SQLiteConnection connection = null;
                SQLiteCommand command = null;

                try
                {
                    VirtualDb.Clear();
                    VirtualDb.ExtendedProperties.Clear();

                    connection = new SQLiteConnection("Data Source=" + basepath + ";") { ParseViaFramework = true };
                    connection.Open();

                    command = new SQLiteCommand("SELECT * FROM 'MailPropertys';", connection);
                    SQLiteDataAdapter objDataAdapter = new SQLiteDataAdapter(command);
                    objDataAdapter.Fill(VirtualDb, "Propertys");

                    command = new SQLiteCommand("SELECT * FROM 'MailsTemplates';", connection);
                    objDataAdapter = new SQLiteDataAdapter(command);
                    objDataAdapter.Fill(VirtualDb, "Templates");

                    command = new SQLiteCommand("SELECT * FROM 'WorkTime';", connection);
                    objDataAdapter = new SQLiteDataAdapter(command);
                    objDataAdapter.Fill(VirtualDb, "WorkTime");


                    if (!_firstGetDataSuccsess) //Данные интерфейса загружаем 1 раз за сеанс
                    {
                        command = new SQLiteCommand("SELECT * FROM 'Generated';", connection);
                        objDataAdapter = new SQLiteDataAdapter(command);
                        objDataAdapter.Fill(VirtualDb, "Generated");
                        FlatMenuElementsToTree(); //Заготавливаем данные заранее 
                    }

                    VirtualDb.ExtendedProperties.Add("LastModiff", File.GetLastWriteTime(basepath));

                    _firstGetDataSuccsess = true;
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.Fatal("Ошибка при заполнении буфера данных: " + ex.Message);
                    return false;
                }
                finally
                {
                    command?.Dispose();
                    if (connection != null)
                    {
                        try
                        {
                            connection.Close();
                        }
                        catch (Exception ex)
                        {
                            _logger.Fatal("Ошбка при освобождкении ресурсов после заполнения буфера: " + ex.Message);
                        }
                        finally
                        {
                            connection.Dispose();
                        }
                    }

                    //Monitor.Exit(ThreadLock);
                }
            }   
        }

        

        #region Методы для генераци письма

        public int GetButtonId(string buttonName) //Возвращает id кнопки в int. Если id не удалось определить возвращает 0. Если возвращен 0 нужно писать ошибку в лог.
        {
            try
            {
                int number;
                String[] words = buttonName.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries); //Опцию нужно убрать чтобы не нарваться на исключение
                if (int.TryParse(words[1], out number)) { }
                else
                {
                    _logger.Error("Не удалось определить id кнопки. Имя кнопки: " + buttonName);
                    return 0;
                }
                return number;
            }
            catch (Exception e)
            {
                _logger.Error("Ошибка при обработке имени кнопки. Имя кнопки: " + buttonName + " Ошибка: " + e.Message);
                return 0;
            }
        }
        public MailProperty GetPropery(int buttonId)
        {
            if (VirtualDb.Tables.Count == 0) return null; //Если в памяти нет данных, то возвращаем null
            var query =                                             //В противном случае ищем данные
                VirtualDb.Tables["Propertys"].AsEnumerable()
                    .Where(s => s.Field<long>("ButtonID") == buttonId)
                    .Select(s => new MailProperty()
                    {
                        ButtonID = (int)s.Field<long>("ButtonID"),
                        Description = s.Field<string>("Description"),
                        Only6565 = s.Field<bool>("Only6565"),
                        Only6690 = s.Field<bool>("Only6690"),
                        FillTO = s.Field<bool>("FillTO"),
                        TO = s.Field<string>("TO"),
                        FillCopy = s.Field<bool>("FillCopy"),
                        Copy = s.Field<string>("Copy"),
                        FillHideCopy = s.Field<bool>("FillHideCopy"),
                        HideCopy = s.Field<string>("HideCopy"),
                        FillSubject = s.Field<bool>("FillSubject"),
                        Subject = s.Field<string>("Subject"),
                        HighImportance = s.Field<bool>("HighImportance"),
                        Reminder = s.Field<bool>("Reminder"),
                        ReminderTime = Convert.ToDateTime(s.Field<string>("ReminderTime").Trim('\'')),
                        FillBody = s.Field<bool>("FillBody"),
                        BodyID = (int)s.Field<long>("BodyID"),
                        Zametka1 = s.Field<string>("Zametka1"),
                        Zametka2 = s.Field<string>("Zametka2"),
                        Zametka3 = s.Field<string>("Zametka3")
                    });

            if (query.Count() != 0) return query.First(); //Если в выборке что-то есть, то возвращаем результат
            return null; // Иначе возвращаем null
        }
        public MailsTemplate GetTemplate(int templateid)
        {
            var query =
            VirtualDb.Tables["Templates"].AsEnumerable()
            .Where(s => s.Field<long>("Templateid") == templateid)
            .Select(s => new MailsTemplate()
            {
                Templateid = (int)s.Field<long>("Templateid"),
                Templadescription =
                s.Field<string>("Templadescription"),
                TemplateBody = s.Field<byte[]>("TemplateBody")
            });
            return query.First();
        }

        private WorkTime[] GetWorkTime()
        {
            WorkTime[] query =
            VirtualDb.Tables["WorkTime"].AsEnumerable()
            .Select(s => new WorkTime()
            {
                RegionOrTT = s.Field<string>("RegionOrTT"),
                TT = s.Field<bool>("TT"),
                OpenTime = TimeSpan.Parse(s.Field<string>("OpenTime")),
                CloseTime = TimeSpan.Parse(s.Field<string>("CloseTime")),
                Email = s.Field<string>("Email")
            }).ToArray();
            return query;
        }

        public string WorkTtNow(string field)
        {
            TimeSpan timeinEkb = DateTime.Now.TimeOfDay;

            WorkTime[] regions = GetWorkTime();
            WorkTime allRegions = regions.FirstOrDefault(s => s.RegionOrTT == "Все ТТ");
            var sb = new StringBuilder();
            sb.Append(";");

            if ((allRegions != null) && (field == "TO")) //Если объект "все регионы" существует
            {
                if ((allRegions.OpenTime <= timeinEkb) && (timeinEkb <= allRegions.CloseTime)) //Если рабочее время всех регионов
                {
                    sb.Append(allRegions.Email);
                    sb.Append(";");
                    return sb.ToString(); //То вернуть строку
                }

                foreach (WorkTime region in regions) //Если предыдущий блок не отработал, то собираем адресатов по частям
                {
                    if ((!region.TT) && (region.OpenTime <= timeinEkb) && (timeinEkb <= region.CloseTime))
                    {
                        sb.Append(region.Email);
                        sb.Append(";");
                    }
                }
            }
            else if (field == "TO") //Если "все регионы" не существуют, то собираем адресатов по частям
            {
                _logger.Error("В таблице отсутствует регион с названием 'Все ТТ'.");
                foreach (WorkTime region in regions)
                {
                    if ((!region.TT) && (region.OpenTime <= timeinEkb) && (timeinEkb <= region.CloseTime))
                    {
                        sb.Append(region.Email);
                        sb.Append(";");
                    }
                }
            }
            else if (field == "HCOPY") //Если заполняем скрытую копию
            {
                foreach (WorkTime region in regions)
                {
                    if ((region.TT) && (region.OpenTime <= timeinEkb) && (timeinEkb <= region.CloseTime))
                    {
                        sb.Append(region.Email);
                        sb.Append(";");
                    }
                }
            }
            else //Если какой-то глюк
            {
                _logger.Error("Не удалось определить группу рассылки по времени. Местное время: " + timeinEkb);
                return "";
            }

            return sb.ToString();
        }
        #endregion
    
    }
}