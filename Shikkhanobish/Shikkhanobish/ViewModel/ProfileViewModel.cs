using Newtonsoft.Json;
using Shikkhanobish.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Shikkhanobish
{
    internal class ProfileViewModel
    {
        private INavigation navigation;
        public string _activeText;
        public int _teachertID;
        public string _name;
        public string _age;
        public string _institutionNmae;
        public string _class;
        public string _isTeacherorStudent;
        public string _amount;
        public string _fee;
        public double _avarage;
        public string _subject;
        public int _tuitionPoint;
        public string _totalTaken;
        public string _totalTeacher;
        public string _totalSpent;
        public string _offredTuitionTime;
        public string _totalTuitionCount;
        public string _teacherRank;
        public string _availableMin;
        public bool _isEnableTeacher;
        public int _pendingNumber;
        double total;
        int count;
        public ProfileViewModel(Teacher t)
        {
            GetPeddingInfo ( t.TeacherID );
            total = t.Five_Star * 5 + t.Four_Star * 4 + t.Three_Star * 3 + t.Two_Star * 2 + t.One_Star * 1;
            count = t.Five_Star  + t.Four_Star + t.Three_Star + t.Two_Star + t.One_Star;
            Name = t.TeacherName;
            TeacherID = t.TeacherID;
            Age = "" + t.Age;
            Class = "Class: " + t.Class;
            InstitutionName = "" + t.InstitutionName;
            IsTeacherorStudent = "Teacher";
            AmountTxt = "" + t.RechargedAmount + " Taka";
            AvailableMintxt = "" + t.RechargedAmount;
            Fee = "NTY";
            Avarage = System.Math.Round ( total / count , 2 );
            Subject = "NTY";
            TuitionPoint = t.Tuition_Point;
            OffredTuitionTime = " " + t.Total_Min;
            TotalTuitionCount = "" + t.Number_Of_Tution;
            TeacherRank = t.Teacher_Rank;
        }
        public async Task GetPeddingInfo (int id)
        {
            string urlT = "https://api.shikkhanobish.com/api/Master/GetPendingForTeacher";
            HttpClient clientT = new HttpClient ();
            string jsonDataT = JsonConvert.SerializeObject ( new { TeacherID = id } );
            StringContent contentT = new StringContent ( jsonDataT , Encoding.UTF8 , "application/json" );
            HttpResponseMessage responseT = await clientT.PostAsync ( urlT , contentT ).ConfigureAwait ( false );
            string resultT = await responseT.Content.ReadAsStringAsync ();
            var pendningRating = JsonConvert.DeserializeObject<List<IsPending>> ( resultT );
            PendingNumber = pendningRating.Count;
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

        public int TeacherID
        {
            get
            {
                return _teachertID;
            }
            set
            {
                if (value != null)
                {
                    _teachertID = value;
                    OnPropertyChanged();
                }
            }
        }
        public int PendingNumber
        {
            get
            {
                return _pendingNumber;
            }
            set
            {
                if ( value != null )
                {
                    _pendingNumber = value;
                    OnPropertyChanged ();
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

        public double Avarage
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