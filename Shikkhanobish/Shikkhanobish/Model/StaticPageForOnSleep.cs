using Shikkhanobish.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shikkhanobish.Model
{
    public class StaticPageForOnSleep
    {
        public static bool isSleep { get; set; } 
        public static bool isStudent { get; set; }
        public static bool isParent { get; set; }
        public static bool isCallPending { get; set; }
        public static TransferInfo info { get; set; }
    }
}
