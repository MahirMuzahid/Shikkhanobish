using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Shikkhanobish
{
    class MainPageViewModel : INotifyPropertyChanged
    {
  
        public static string _userName;
        public static string _password;
        public Student student = new Student();
        private INavigation navigation;
        public string text;
        public string _errorText;

        public MainPageViewModel()
        {
        }

        public Command Login
        {
            get
            {
                return new Command( () =>
                {
                    var current = Connectivity.NetworkAccess;

                    if (current == NetworkAccess.Internet)
                    {
                        if (UserName == null && Password == null)
                        {
                            ErrorText = "User Name and Password is empty!";
                        }
                        else if (UserName == null)
                        {
                            ErrorText = "User Name is empty!";
                        }
                        else if (Password == null)
                        {
                            ErrorText = "Password is empty!";
                        }
                        else
                        {
                            LoginByUserNameAndPassword();
                        }
                    }
                    else
                    {
                        ErrorText = "Check Internet Connection";
                    }
                    

                    
                });
            }        
        }

        public async void LoginByUserNameAndPassword()
        {
            string url = "https://api.shikkhanobish.com/api/Masters/GetInfoByLogin";
            HttpClient client = new HttpClient();
            string jsonData = JsonConvert.SerializeObject(new { UserName = UserName, Password = Password });
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait(true);
            string result = await response.Content.ReadAsStringAsync();
            student = JsonConvert.DeserializeObject<Student>(result);
            if (student.Name == null)
            {
                ErrorText = "Wrong User Name or Password!";
            }
            else
            {
                ActivityIndicator indicator = new ActivityIndicator { IsRunning = true };
                await Application.Current.MainPage.Navigation.PushModalAsync(new Profile(student)).ConfigureAwait(true);

            }
        }
        //public int Error { get; set; }
        public Command RegisterStudent
        {
            get
            {
                return new Command(async () =>
                {
                    await Application.Current.MainPage.Navigation.PushModalAsync(new RegisterStudent()).ConfigureAwait(true);
                    

                });
            }
        }
        public Command ForgotPassword
        {
            get
            {
                return new Command(async () =>
                {
                    await Application.Current.MainPage.Navigation.PushModalAsync(new ForgotPasswordWindow()).ConfigureAwait(true);


                });
            }
        }
        public string ErrorText
        {
            get
            {
                return _errorText;
            }
            set
            {
                _errorText = value;
                OnPropertyChanged();
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
