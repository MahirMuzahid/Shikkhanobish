using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Shikkhanobish
{
    class StudentProfileVideoModel
    {
        public string _studentID;
        public string _name;
        public string _age;
        public string _institutionNmae;
        public string _class;
        public string _isTeacherorStudent;
        public string _amount;
        public string _fee;
        public float _avarage;
        public string _totalTaken;
        public string _totalTeacher;
        public string _totalSpent;
        public string _availableMin;



        public StudentProfileVideoModel(Student student)
        {
            Name = student.Name;
            StudentIDTxt = "Student ID: " + student.StundentID;
            Age = "Age: " + student.Age;
            Class = "Class: " + student.Class;
            InstitutionName = "Institution Name: " + student.InstitutionName;
            IsTeacherorStudent = "Student";
            AmountTxt = "" + student.RechargedAmount + " Taka";
            AvailableMintxt = "Available Minute: " + student.RechargedAmount;
            Fee = "NTY";
            Avarage = 0;
            TotalTaken = "Total Tuition Taken: " + 0;
            TotalTeacher = "Total Teacher: " + 0;
            TotalSpent = "Total Money Spent: " + 0;
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
