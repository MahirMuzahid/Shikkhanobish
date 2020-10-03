using System;
using System.Collections.Generic;
using System.Text;

namespace Shikkhanobish.Model
{
    public class OfferAndVoucherSource
    {
        public string code { get; set; }
        public string type { get; set; }
        public int limit { get; set; }
        public int amount { get; set; }
        public string imageSource { get; set; }
        public int voucherID { get; set; }
        public string response { get; set; }
    }
}
