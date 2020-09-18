using System;
using System.Collections.Generic;
using System.Text;

namespace Shikkhanobish.Model
{
    class WalletHistoryTeacher
    {
        public int TeacherID { get; set; }
        public int WithdrawAmount { get; set; }
        public int Phonenumber { get; set; }
        public string TrxID { get; set; }
        public string Date { get; set; }
        public int IsPending { get; set; }
        public string Response { get; set; }
    }
}
