using Newtonsoft.Json;
using Shikkhanobish.ContentPages;
using Shikkhanobish.Model;
using Shikkhanobish.ViewModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Plugin.Connectivity;
using System;
using Android.Net;
using Xamarin.Essentials;
using Javax.Net.Ssl;
using Xamarin.Forms.OpenTok;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Android.Content.Res;
using Shikkhanobish.ContentPages.Parents;
using System.Collections.Generic;

namespace Shikkhanobish
{
    internal class MainPageViewModel : INotifyPropertyChanged
    {
        public static string _userName;
        public static string _password;
        public string _loginText;
        public Student student = new Student();
        public Teacher teacher = new Teacher();
        private INavigation navigation;
        public string text;
        public string _errorText;
        public TransferInfo Trns = new TransferInfo ();

        public MainPageViewModel()
        {
            loginText = "Login";
        }
        
        public Command Login
        {
            get
            {
                return new Command( async() =>
               {
                   
                   if ( Connectivity.NetworkAccess == NetworkAccess.Internet)
                   {
                       int pagenumber = 0;
                       if (UserName == null && Password == null)
                       {
                           ErrorText = "User Name and Password is empty!";
                           loginText = "Login";
                       }
                       else if (UserName == null)
                       {
                           ErrorText = "User Name is empty!";
                           loginText = "Login";
                       }
                       else if (Password == null)
                       {
                           ErrorText = "Password is empty!";
                           loginText = "Login";
                       }
                       else
                       {
                           try
                           {
                               loginText = "Wait...";
                               await SearchTeacher ();
                               await SearchStudent ();
                               //Task.Run ( ()=>SearchTeacher ());
                               //Task.Run ( ( ) => SearchStudent () );
                               
                               if(student.Name == null)
                               {
                                   await GetPeddingInfo ( teacher.TeacherID ).ConfigureAwait(false);
                                   teacher.pendingcount = pending;
                                   pagenumber = 0;
                                   //goPage ( 0 );
                               }                            
                               else
                               {
                                   if ( student.IsPending == 1 )
                                   {
                                       string urlT = "https://api.shikkhanobish.com/api/Master/GetPending";
                                       HttpClient clientT = new HttpClient ();
                                       string jsonDataT = JsonConvert.SerializeObject ( new { StudentID = student.StudentID } );
                                       StringContent contentT = new StringContent ( jsonDataT , Encoding.UTF8 , "application/json" );
                                       HttpResponseMessage responseT = await clientT.PostAsync ( urlT , contentT ).ConfigureAwait ( false );
                                       string resultT = await responseT.Content.ReadAsStringAsync ();
                                       var pendningRating = JsonConvert.DeserializeObject<IsPending> ( resultT );
                                       urlT = "https://api.shikkhanobish.com/api/Master/GetInfoByTeacherID";
                                       jsonDataT = JsonConvert.SerializeObject ( new { TeacherID = pendningRating.TeacherID } );
                                       contentT = new StringContent ( jsonDataT , Encoding.UTF8 , "application/json" );
                                       responseT = await clientT.PostAsync ( urlT , contentT ).ConfigureAwait ( false );
                                       resultT = await responseT.Content.ReadAsStringAsync ();
                                       var teacher = JsonConvert.DeserializeObject<Teacher> ( resultT );
                                       TransferInfo trns = new TransferInfo ();
                                       trns.Student = student;
                                       trns.Class = pendningRating.Class;
                                       trns.Subject = pendningRating.Subject;
                                       trns.StudentCost = pendningRating.Cost;
                                       trns.StudyTimeInAPp = pendningRating.Time;
                                       trns.Teacher = teacher;
                                       Trns = trns;
                                       pagenumber = 2;
                                      // goPage ( 2 );
                                   }
                                   else
                                   {
                                       pagenumber = 1;
                                       //goPage ( 1 );
                                   }
                               }
                           }
                           catch ( Exception ex )
                           {
                               loginText = "Login";
                               ErrorText = "Connection Reset! Check internet connection";
                           }
                            MainThread.BeginInvokeOnMainThread ( ( ) => { goPage ( pagenumber ); } );                               
                       }                      
                   }
                   else
                   {
                       ErrorText = "Check Internet Connection";
                       loginText = "Login";
                   }
                   
               });
            }

        }

        public async Task SearchTeacher()
        {
            string urlT = "https://api.shikkhanobish.com/api/Master/GetInfoByLoginTeacher";
            HttpClient clientT = new HttpClient ();
            string jsonDataT = JsonConvert.SerializeObject ( new { UserName = UserName , Password = Password } );
            StringContent contentT = new StringContent ( jsonDataT , Encoding.UTF8 , "application/json" );
            HttpResponseMessage responseT = await clientT.PostAsync ( urlT , contentT ).ConfigureAwait ( false );
            string resultT = await responseT.Content.ReadAsStringAsync ();
            teacher = JsonConvert.DeserializeObject<Teacher> ( resultT );
        }
        public async Task SearchStudent ( )
        {
            string url = "https://api.shikkhanobish.com/api/Master/GetInfoByLogin";
            HttpClient client = new HttpClient ();
            string jsonData = JsonConvert.SerializeObject ( new { UserName = UserName , Password = Password } );
            StringContent content = new StringContent ( jsonData , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( true );
            string result = await response.Content.ReadAsStringAsync ();
            student = JsonConvert.DeserializeObject<Student> ( result );
        }

        public async void goPage(int i)
        {
            if(i == 0)
            {
                await Application.Current.MainPage.Navigation.PushModalAsync ( new TeacherProfile ( teacher ) ).ConfigureAwait ( false );
            }
            if(i == 1 )
            {
                await Application.Current.MainPage.Navigation.PushModalAsync ( new StudentProfile ( student ) ).ConfigureAwait ( false );
            }
            if ( i == 2 )
            {
                await Application.Current.MainPage.Navigation.PushModalAsync ( new RatingPage ( Trns,false ) ).ConfigureAwait ( false );
            }
        }
        int pending;
        public async Task GetPeddingInfo ( int id )
        {
            string urlT = "https://api.shikkhanobish.com/api/Master/GetPendingForTeacher";
            HttpClient clientT = new HttpClient ();
            string jsonDataT = JsonConvert.SerializeObject ( new { TeacherID = id } );
            StringContent contentT = new StringContent ( jsonDataT , Encoding.UTF8 , "application/json" );
            HttpResponseMessage responseT = await clientT.PostAsync ( urlT , contentT ).ConfigureAwait ( false );
            string resultT = await responseT.Content.ReadAsStringAsync ();
            var pendningRating = JsonConvert.DeserializeObject<List<IsPending>> ( resultT );
            pending = pendningRating.Count;
        }

        //public int Error { get; set; }
        public Command RegisterStudent
        {
            get
            {
                return new Command(async () =>
                {
                    await Application.Current.MainPage.Navigation.PushModalAsync(new RegisterStudent()).ConfigureAwait( false );
                });
            }
        }

        public Command ForgotPassword
        {
            get
            {
                return new Command(async () =>
                {
                    await Application.Current.MainPage.Navigation.PushModalAsync(new ForgotPasswordWindow()).ConfigureAwait( false );
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

        public string loginText
        {
            get
            {
                return _loginText;
            }
            set
            {
                if (value != null)
                {
                    _loginText = value;
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