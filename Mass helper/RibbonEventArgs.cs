using System;

namespace Mass_helper
{
    public delegate void RibbonEventHandler(object sender, RibbonEventArgs a);
    public class RibbonEventArgs : EventArgs
    {
        public RibbonEventArgs(IRibbonInMail ribbonInMail)
        {
            RibbonInMail = ribbonInMail;
        }
        public IRibbonInMail RibbonInMail { get; private set; }
    }
}
