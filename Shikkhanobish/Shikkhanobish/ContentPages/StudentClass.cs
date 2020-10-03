using System;
using System.Collections.Generic;
using System.Text;

namespace Shikkhanobish.ContentPages
{
    public class StudentClass
    {
        public int StudentID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Class { get; set; }
        public string InstitutionName { get; set; }
        public float RechargedAmount { get; set; }
        public int IsPending { get; set; }

        public int TotalTuitionTIme { get; set; }
        public int TotalTeacherCount { get; set; }
        public int freeMin { get; set; }
        public double AvarageRating { get; set; }
        public int ParentCode { get; set; }
    }
}
