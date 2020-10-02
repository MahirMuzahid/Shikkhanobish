using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Shikkhanobish
{
    internal class RegisterStudentViewModel : INotifyPropertyChanged
    {
        public string _confirmationText;
        public string ConfirmPass;
        public string confirm;
        public static string _userName;
        public static string _password;
        public string _confirmPassword;
        public string _phoneNumber;
        public string _name;
        public int _age;
        public string _class;
        public string _institutionName;
        public int _rechargedAmount;
        public int _isPending;
        private INavigation navigation;
        private Student checkStudent = new Student();
        public string txt = "Everything is ok!!";
        public string _bindtext, _bindtextteacher;

        public RegisterStudentViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            BindButtonText = "Student Registretion";
            BindButtonTextTeacher = "Teacher Registretion";
        }

        public Command RegisterStudent
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        BindButtonText = "Checking Info...";

                        if ( Connectivity.NetworkAccess == NetworkAccess.Internet )
                        {
                            checkStudent.UserName = UserName;
                            checkStudent.Password = Password;
                            ConfirmPass = ConfirmPassword;
                            checkStudent.PhoneNumber = PhoneNumber;
                            checkStudent.Name = Name;
                            checkStudent.Age = Age;
                            checkStudent.Class = Class;
                            checkStudent.InstitutionName = InstitutionName;
                            checkStudent.RechargedAmount = RechargedAmount;
                            checkStudent.IsPending = IsPending;
                            checkStudent.TotalTuitionTIme = 0;
                            checkStudent.TotalTeacherCount = 0;
                            checkStudent.AvarageRating = 0;
                            try
                            {
                                checkUserNamenadPhoneNumber ();
                            }
                            catch ( Exception ex )
                            {
                                ConfirmationText = ex.Message;
                            }
                        }
                        else
                        {
                            ConfirmationText = "Check internet connection";
                            BindButtonText = "Try Again";
                        }
                    }
                    catch (Exception ex)
                    {
                        ConfirmationText = "Connection Reset! Check internet connection";
                    }
                    
                });
            }
        }

        public Command RegisterTeacher
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        BindButtonTextTeacher = "Checking Info...";
                        var current = Connectivity.NetworkAccess;

                        if ( current == NetworkAccess.Internet )
                        {
                            checkStudent.UserName = UserName;
                            checkStudent.Password = Password;
                            ConfirmPass = ConfirmPassword;
                            checkStudent.PhoneNumber = PhoneNumber;
                            checkStudent.Name = Name;
                            checkStudent.Age = Age;
                            checkStudent.Class = Class;
                            checkStudent.InstitutionName = InstitutionName;
                            checkStudent.RechargedAmount = RechargedAmount;
                            checkStudent.IsPending = IsPending;
                            checkStudent.TotalTuitionTIme = 0;
                            checkStudent.TotalTeacherCount = 0;
                            checkStudent.AvarageRating = 0;
                            try
                            {
                                checkUsernameAndPhonenumberTeacher ();
                            }
                            catch ( Exception ex )
                            {
                                ConfirmationText = ex.Message;
                            }
                        }
                        else
                        {
                            ConfirmationText = "Check internet connection";
                            BindButtonTextTeacher = "Try Again";
                        }
                    }
                    catch ( Exception ex )
                    {
                        ConfirmationText = "Connection Reset! Check internet connection";
                    }
                    
                });
            }
        }

        //This shit will change(custom teacher checking api)----------------------------------------------------------------------------------------------
        public async void checkUsernameAndPhonenumberTeacher()
        {
            string url = "https://api.shikkhanobish.com/api/Masters/SearchUserName";
            HttpClient client = new HttpClient();
            string jsonData = JsonConvert.SerializeObject(new { UserName = UserName, PhoneNumber = PhoneNumber });
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait(true);
            string result = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            var RUserName = JsonConvert.DeserializeObject<Student>(result);
            if (RUserName.UserName != null)
            {
                if (RUserName.UserName == UserName)
                {
                    ConfirmationText = "User Name already exist";
                    BindButtonTextTeacher = "Try Again";
                }
                else if (RUserName.PhoneNumber == PhoneNumber)
                {
                    ConfirmationText = "You can use only one phonenumber per account";
                    BindButtonTextTeacher = "Try Again";
                }
                else
                {
                    checkinfoTeacher();
                }
            }
            else
            {
                checkinfoTeacher();
            }
        }

        public async void checkinfoTeacher()
        {
            if (checkStudent.UserName == null)
            {
                ConfirmationText = "Empty Username!";
                BindButtonTextTeacher = "Try Again";
            }
            else if (checkStudent.UserName.Contains(" "))
            {
                ConfirmationText = "Can't use \"Space\" in user name";
                BindButtonTextTeacher = "Try Again";
            }
            else if (checkStudent.Password == null)
            {
                ConfirmationText = "Empty Password!";
                BindButtonTextTeacher = "Try Again";
            }
            else if (ConfirmPass == null)
            {
                ConfirmationText = "Password Doesn't match!";
                BindButtonTextTeacher = "Try Again";
            }
            else if (checkStudent.PhoneNumber.Length != 11 || checkStudent.PhoneNumber == null)
            {
                ConfirmationText = "Enter valid Phone Number!";
                BindButtonTextTeacher = "Try Again";
            }
            else if (checkStudent.Name == null)
            {
                ConfirmationText = "Empty Name!";
                BindButtonTextTeacher = "Try Again";
            }
            else if (checkStudent.Class == null)
            {
                ConfirmationText = "Empty Class!";
                BindButtonTextTeacher = "Try Again";
            }
            else if (checkStudent.InstitutionName == null)
            {
                ConfirmationText = "Empty Institution Name!";
                BindButtonTextTeacher = "Try Again";
            }
            else if (checkStudent.Password.Length < 6 || !checkStudent.Password.Any(char.IsUpper) || !checkStudent.Password.Any(char.IsDigit))
            {
                ConfirmationText = "Password length must be at least 6 and must be one Uppercase character and must be one integer(0-9)";
                BindButtonTextTeacher = "Try Again";
            }
            else if (checkStudent.Password != ConfirmPass)
            {
                ConfirmationText = "Password doesn't match";
                BindButtonTextTeacher = "Try Again";
            }
            else if (checkStudent.Age < 10 || checkStudent.Age > 100)
            {
                ConfirmationText = "Enter Valid Age";
                BindButtonTextTeacher = "Try Again";
            }
            else
            {
                BindButtonTextTeacher = "All Done!";
                ConfirmationText = txt;
                await Application.Current.MainPage.Navigation.PushModalAsync(new VerifyPhonenumber(checkStudent, 1)).ConfigureAwait( false );
            }
        }

        public async void checkUserNamenadPhoneNumber()
        {
            string url = "https://api.shikkhanobish.com/api/Masters/SearchUserName";
            HttpClient client = new HttpClient();
            string jsonData = JsonConvert.SerializeObject(new { UserName = UserName, PhoneNumber = PhoneNumber });
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait(true);
            string result = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            var RUserName = JsonConvert.DeserializeObject<Student>(result);
            if (RUserName.UserName != null)
            {
                if (RUserName.UserName == UserName)
                {
                    ConfirmationText = "User Name already exist";
                    BindButtonText = "Try Again";
                }
                else if (RUserName.PhoneNumber == PhoneNumber)
                {
                    ConfirmationText = "You can use only one phonenumber per account";
                    BindButtonText = "Try Again";
                }
                else
                {
                    checkInfo();
                }
            }
            else
            {
                checkInfo();
            }
        }

        public async void checkInfo()
        {
            if (checkStudent.UserName == null)
            {
                ConfirmationText = "Empty Username!";
                BindButtonText = "Try Again";
            }
            else if (checkStudent.UserName.Contains(" "))
            {
                ConfirmationText = "Can't use \"Space\" in user name";
                BindButtonText = "Try Again";
            }
            else if (checkStudent.Password == null)
            {
                ConfirmationText = "Empty Password!";
                BindButtonText = "Try Again";
            }
            else if (ConfirmPass == null)
            {
                ConfirmationText = "Password Doesn't match!";
                BindButtonText = "Try Again";
            }
            else if (checkStudent.PhoneNumber.Length != 11 || checkStudent.PhoneNumber == null || !checkStudent.PhoneNumber.Any(char.IsDigit))
            {
                ConfirmationText = "Enter valid Phone Number!";
                BindButtonText = "Try Again";
            }
            else if (checkStudent.Name == null)
            {
                ConfirmationText = "Empty Name!";
                BindButtonText = "Try Again";
            }
            else if (checkStudent.Class == null)
            {
                ConfirmationText = "Empty Class!";
                BindButtonText = "Try Again";
            }
            else if (checkStudent.InstitutionName == null)
            {
                ConfirmationText = "Empty Institution Name!";
                BindButtonText = "Try Again";
            }
            else if (checkStudent.Password.Length < 6 || !checkStudent.Password.Any(char.IsUpper))
            {
                ConfirmationText = "Password length must be at least 6 and must be one Uppercase character and must be one integer(0-9)";
                BindButtonText = "Try Again";
            }
            else if (checkStudent.Password != ConfirmPass)
            {
                ConfirmationText = "Password doesn't match";
                BindButtonText = "Try Again";
            }
            else if (checkStudent.Age < 10 || checkStudent.Age > 100)
            {
                ConfirmationText = "Enter Valid Age";
                BindButtonText = "Try Again";
            }
            else
            {
                BindButtonText = "Completing Registration...";
                ConfirmationText = txt;


                //ConfirmationText = responseData.Massage;

                await Application.Current.MainPage.Navigation.PushModalAsync(new VerifyPhonenumber(checkStudent, 0)).ConfigureAwait( false );
            }
        }

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                if (value != null)
                {
                    _userName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (value != null)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ConfirmPassword
        {
            get
            {
                return _confirmPassword;
            }
            set
            {
                if (value != null)
                {
                    _confirmPassword = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                if (value != null)
                {
                    _phoneNumber = value;
                    //ProfileViewModel setPhoneNumber = new ProfileViewModel(navigation);
                    //setPhoneNumber.PhoneNumber = value;
                    OnPropertyChanged();
                }
            }
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

        public int Age
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
                return _institutionName;
            }
            set
            {
                if (value != null)
                {
                    _institutionName = value;
                    OnPropertyChanged();
                }
            }
        }

        public int RechargedAmount
        {
            get
            {
                return _rechargedAmount;
            }
            set
            {
                _rechargedAmount = 0;
                OnPropertyChanged();
            }
        }

        public int IsPending
        {
            get
            {
                return _isPending;
            }
            set
            {
                _isPending = 0;
                OnPropertyChanged();
            }
        }

        public string ConfirmationText
        {
            get
            {
                return _confirmationText;
            }
            set
            {
                _confirmationText = value;
                confirm = _confirmationText;
                OnPropertyChanged();
            }
        }

        public string BindButtonText
        {
            get
            {
                return _bindtext;
            }
            set
            {
                if (value != null)
                {
                    _bindtext = value;
                    OnPropertyChanged();
                }
            }
        }

        public string BindButtonTextTeacher
        {
            get
            {
                return _bindtextteacher;
            }
            set
            {
                if (value != null)
                {
                    _bindtextteacher = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}