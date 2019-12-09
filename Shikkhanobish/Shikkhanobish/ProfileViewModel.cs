using IdentityModel.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using Xamarin.Forms;
namespace Shikkhanobish
{
    class ProfileViewModel
    {
        private INavigation navigation;
        public string _name;
        public string _age;
        public string _institutionNmae;
        public string _class;
        public string _isTeacherorStudent;
        public string _amount;
        public string _fee;
        public float _avarage;
        public string _subject;
        public int _tuitionPoint;
        public string _totalTaken;
        public string _totalTeacher;
        public string _totalSpent;
        public string _offredTuitionTime;
        public string _totalTuitionCount;
        public string _teacherRank;
        public bool _isEnableTeacher;

        public ProfileViewModel(Student student)
        {
            Name = student.Name;
            Age = "Age: " + student.Age;
            Class = "Class: "+ student.Class;
            InstitutionName = "Institution Name: " + student.InstitutionName;
            IsTeacherorStudent = "Student";
            Amount ="Balance: " + student.RechargedAmount;
            Fee = "NTY";
            Avarage = 0;
            Subject = "NTY";
            TuitionPoint = 0;
            TotalTaken = "Total Tuition Taken: " + 0;
            TotalTeacher = "Total Teacher: " + 0;
            TotalSpent = "Total Money Spent: " + 0;
            OffredTuitionTime = "Total Offred Tuition Time: " + 0;
            TotalTuitionCount = "Total Tuition Coutn: " + 0 ;
            TeacherRank = "Teacher Rank: NTY";
            IsEnableTeacher = false;
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != null)
                {
                    _name = value;
                    OnPropertyChanged();
                }

            }
        }
        public string Age
        {
            get
            {
                return _age;
            }
            set
            {
                if (value != null)
                {
                    _age = value;
                    OnPropertyChanged();
                }

            }
        }
        public string Class
        {
            get
            {
                return _class;
            }
            set
            {
                if (value != null)
                {
                    _class = value;
                    OnPropertyChanged();
                }

            }
        }
        public string InstitutionName
        {
            get
            {
                return _institutionNmae;
            }
            set
            {
                if (value != null)
                {
                    _institutionNmae = value;
                    OnPropertyChanged();
                }

            }
        }
        public string IsTeacherorStudent
        {
            get
            {
                return _isTeacherorStudent;
            }
            set
            {
                if (value != null)
                {
                    _isTeacherorStudent = value;
                    OnPropertyChanged();
                }

            }
        }
        public string Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                if (value != null)
                {
                    _amount = value;
                    OnPropertyChanged();
                }

            }
        }
        public string Fee
        {
            get
            {
                return _fee;
            }
            set
            {
                if (value != null)
                {
                    _fee = value;
                    OnPropertyChanged();
                }

            }
        }
        public float Avarage
        {
            get
            {
                return _avarage;
            }
            set
            {
                _avarage = value;
                OnPropertyChanged();

            }
        }
        public string Subject
        {
            get
            {
                return _subject;
            }
            set
            {
                if (value != null)
                {
                    _subject = value;
                    OnPropertyChanged();
                }

            }
        }
        public int TuitionPoint
        {
            get
            {
                return _tuitionPoint;
            }
            set
            {
                _tuitionPoint = value;
                OnPropertyChanged();

            }
        }
        public string TotalTaken
        {
            get
            {
                return _totalTaken;
            }
            set
            {
                if (value != null)
                {
                    _totalTaken = value;
                    OnPropertyChanged();
                }

            }
        }
        public string TotalTeacher
        {
            get
            {
                return _totalTeacher;
            }
            set
            {
                if (value != null)
                {
                    _totalTeacher = value;
                    OnPropertyChanged();
                }

            }
        }
        public string TotalSpent
        {
            get
            {
                return _totalSpent;
            }
            set
            {
                if (value != null)
                {
                    _totalSpent = value;
                    OnPropertyChanged();
                }

            }
        }
        public string OffredTuitionTime
        {
            get
            {
                return _offredTuitionTime;
            }
            set
            {
                if (value != null)
                {
                    _offredTuitionTime = value;
                    OnPropertyChanged();
                }

            }
        }
        public string TotalTuitionCount
        {
            get
            {
                return _totalTuitionCount;
            }
            set
            {
                if (value != null)
                {
                    _totalTuitionCount = value;
                    OnPropertyChanged();
                }

            }
        }
        public string TeacherRank
        {
            get
            {
                return _teacherRank;
            }
            set
            {
                if (value != null)
                {
                   _teacherRank = value;
                    OnPropertyChanged();
                }

            }
        }
        public bool IsEnableTeacher
        {
            get
            {
                return _isEnableTeacher;
            }
            set
            {
                _isEnableTeacher = value;
                OnPropertyChanged();

            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}

