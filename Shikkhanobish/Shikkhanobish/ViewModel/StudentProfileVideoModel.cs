using Newtonsoft.Json;
using Shikkhanobish.Model;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;

namespace Shikkhanobish
{
    internal class StudentProfileVideoModel
    {
        public string _studentID;
        public string _name;
        public string _age;
        public string _institutionNmae;
        public string _class;
        public string _isTeacherorStudent;
        public string _amount;
        public string _fee;
        public string _avarage;
        public string _totalTaken;
        public string _totalTeacher;
        public string _totalSpent;
        public string _availableMin;
        public string _isPremium;

        public StudentProfileVideoModel(Student student)
        {
            Name = student.Name;
            StudentIDTxt = "ID: " + student.StudentID;
            Age = "Age : " + student.Age;
            Class = "Class : " + student.Class;
            InstitutionName = "" + student.InstitutionName;
            IsTeacherorStudent = "Student";
            AmountTxt = "Coin: " + student.RechargedAmount;
            AvailableMintxt = "Available Amount: " + student.RechargedAmount + "Taka";
            Avarage = "Average: " + student.AvarageRating;
            TotalTaken = "Total Minute: " + student.TotalTuitionTIme;
            TotalTeacher = "Total Tuition: " + student.TotalTeacherCount;
            AvailableMintxt = student.RechargedAmount * 2 + " - " + student.RechargedAmount * 4 + " min";



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
        public string IsPremimum
        {
            get
            {
                return _isPremium;
            }
            set
            {
                if (value != null)
                {
                    _isPremium = value;
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

        public string StudentIDTxt
        {
            get
            {
                return _studentID;
            }
            set
            {
                if (value != null)
                {
                    _studentID = value;
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

        public string AmountTxt
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

        public string Avarage
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

        public string AvailableMintxt
        {
            get
            {
                return _availableMin;
            }
            set
            {
                _availableMin = value;
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