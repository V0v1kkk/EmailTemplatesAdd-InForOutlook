﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masshelper.BL
{
    class WorkTime
    {
        public string RegionOrTT;
        public bool TT;
        public TimeSpan OpenTime;
        public TimeSpan CloseTime;
        public string Email;

        public WorkTime()
        {
            RegionOrTT = "";
            TT = true;
            OpenTime = TimeSpan.Parse("00:00");
            CloseTime = TimeSpan.Parse("00:00");
            Email = "";
        }
    }
}
