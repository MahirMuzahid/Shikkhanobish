﻿namespace Shikkhanobish.Model
{
    internal class TuitionHistoryTeacher
    {
        public int TeacherID { get; set; }
        public int TuitionStudentID { get; set; }
        public string Class { get; set; }
        public string Subject { get; set; }
        public string Time { get; set; }
        public string Date { get; set; }
        public int Ratting { get; set; }
        public string Student_Name { get; set; }
        public string Response { get; set; }
    }
}