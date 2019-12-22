using System;
using System.Collections.Generic;
using System.Text;

namespace Shikkhanobish.Model
{
    class TuitionHistoryTeacher
    {
        public int TeacherID { get; set; }
        public int TuitionStundentID { get; set; }
        public string Class { get; set; }
        public string Subject { get; set; }
        public string Time { get; set; }
        public string Date { get; set; }
        public int Ratting { get; set; }
        public string Response { get; set; }
    }
}
