using Shikkhanobish.ContentPages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shikkhanobish.Model
{
    class OldStToNewSt
    {

        public Student Sc_TO_S ( StudentClass st ) {
            Student _Student = new Student ();

            _Student.Name = st.Name;
            _Student.Class = st.Class;
            _Student.InstitutionName = st.InstitutionName;
            _Student.Age = st.Age;
            _Student.StudentID = st.StudentID;
            _Student.UserName = st.UserName;
            _Student.Password = st.Password;
            _Student.PhoneNumber = st.PhoneNumber;
            _Student.Age = st.Age;
            _Student.RechargedAmount = st.RechargedAmount;
            _Student.IsPending = st.IsPending;
            _Student.TotalTuitionTIme = st.TotalTuitionTIme;
            _Student.TotalTeacherCount = st.TotalTeacherCount;
            _Student.AvarageRating = st.AvarageRating;
            _Student.freeMin = st.freeMin;

            return _Student;
        }
        public StudentClass S_TO_Sc ( Student st )
        {
            StudentClass _Student = new StudentClass ();

            _Student.Name = st.Name;
            _Student.Class = st.Class;
            _Student.InstitutionName = st.InstitutionName;
            _Student.Age = st.Age;
            _Student.StudentID = st.StudentID;
            _Student.UserName = st.UserName;
            _Student.Password = st.Password;
            _Student.PhoneNumber = st.PhoneNumber;
            _Student.Age = st.Age;
            _Student.RechargedAmount = st.RechargedAmount;
            _Student.IsPending = st.IsPending;
            _Student.TotalTuitionTIme = st.TotalTuitionTIme;
            _Student.TotalTeacherCount = st.TotalTeacherCount;
            _Student.AvarageRating = st.AvarageRating;
            _Student.freeMin = st.freeMin;

            return _Student;
        }
    }
}
