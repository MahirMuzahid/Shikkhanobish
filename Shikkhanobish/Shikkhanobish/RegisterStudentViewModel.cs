using IdentityModel.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        Student checkStudent = new Student();
        public string txt = "Everything is ok!!";
        public string _bindtext;

        public RegisterStudentViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            BindButtonText = "Register";
        }
        public Command RegisterStudent
        {
            
            get
            {
                return new Command(async () =>
                {
                    BindButtonText = "Checking Info. Wait...";
                    var current = Connectivity.NetworkAccess;

                    if (current == NetworkAccess.Internet)
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
                            checkUserNamenadPhoneNumber();
                        }
                        catch (Exception ex)
                        {
                            ConfirmationText = ex.Message;
                        }
                    }
                    else
                    {
                        ConfirmationText = "Check internet connection";
                        BindButtonText = "Try Again";
                    }
                          
                });
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
            if (RUserName.UserName != UserName)
            {
                ConfirmationText = "Username already exist!";
                BindButtonText = "Try Again";
            }
            else if (RUserName.PhoneNumber != PhoneNumber)
            {
                ConfirmationText = "You can use only one phonenumber per account";
                BindButtonText = "Try Again";
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
            else if (checkStudent.PhoneNumber.Length != 11 || checkStudent.PhoneNumber == null )
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
            else if (checkStudent.Password.Length < 6 || !checkStudent.Password.Any(char.IsUpper) || !checkStudent.Password.Any(char.IsDigit))
            {
                ConfirmationText = "Password length must be at least 6 and must be one Uppercase character and must be one integer(0-9)";
                BindButtonText = "Try Again";
            }
            else if(checkStudent.Password != ConfirmPass)
            {
                ConfirmationText = "Password doesn't match";
                BindButtonText = "Try Again";
            }
            else if(checkStudent.Age < 10 || checkStudent.Age > 100)
            {
                ConfirmationText = "Enter Valid Age";
                BindButtonText = "Try Again";
            }
            else
            {
                BindButtonText = "Completing Registration...";
                ConfirmationText = txt;
                string url = "https://api.shikkhanobish.com/api/Masters/RegisterStudent";
                HttpClient client = new HttpClient();
                string jsonData = JsonConvert.SerializeObject(checkStudent);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait(true);
                string result = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                Response responseData = JsonConvert.DeserializeObject<Response>(result);
                ConfirmationText = responseData.Massage;
                await Application.Current.MainPage.Navigation.PushModalAsync(new VerifyPhonenumber(checkStudent)).ConfigureAwait(true);
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


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}