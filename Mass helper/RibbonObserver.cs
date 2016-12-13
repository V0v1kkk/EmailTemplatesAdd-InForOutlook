using System.Collections.Generic;

// ReSharper disable InconsistentNaming

namespace Mass_helper
{
    public interface IRibbonObserver
    {
        event RibbonEventHandler RibbonCreate;
        event RibbonEventHandler RibbonDispose;
    }
    public class RibbonObserver : IRibbonObserver
    {
        public event RibbonEventHandler RibbonCreate;
        public event RibbonEventHandler RibbonDispose;
        public List<IRibbonInMail> RibbonsRibbonInMails { get; } = new List<IRibbonInMail>();

        private static RibbonObserver _instance;
        private static readonly object SyncRoot = new object();


        public static RibbonObserver GetInstance()
        {
            if (_instance == null)
            {
                lock (SyncRoot)
                {
                    if (_instance == null) _instance = new RibbonObserver();
                }
            }
            return _instance;
        }

        public static void AddRibbon(IRibbonInMail ribbonInMail)
        {
            GetInstance().RibbonsRibbonInMails.Add(ribbonInMail);
            GetInstance().RibbonCreate?.Invoke(null, new RibbonEventArgs(ribbonInMail));
        }

        public static void DeleteRibbbon(IRibbonInMail ribbonInMail)
        {
            GetInstance().RibbonsRibbonInMails.Remove(ribbonInMail);
            GetInstance().RibbonDispose?.Invoke(null, new RibbonEventArgs(ribbonInMail));
        }
                
    }
}
