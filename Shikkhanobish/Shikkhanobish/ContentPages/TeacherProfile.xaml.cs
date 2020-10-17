using Newtonsoft.Json;
using Plugin.Connectivity;
using Rg.Plugins.Popup.Extensions;
using Shikkhanobish.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;
using Shikkhanobish.ContentPages;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Shikkhanobish.ViewModel;
using Xamarin.Essentials;
using Shikkhanobish.Interface;
using Xamarin.Forms.OpenTok.Service;

namespace Shikkhanobish
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeacherProfile : MasterDetailPage
    {
        public int StudentID;
        private Student _Student;
        private Teacher teacher;
        private int ac = 0;
        private bool takeTuition;
        double total, avg;
        int count;
        
        public TeacherProfile(Teacher t)
        {
            takeTuition = false;
            InitializeComponent ();
            BindingContext = new ProfileViewModel ( t );
            teacher = t;
            activeback.BackgroundColor = Color.FromHex ( "#A7A7A7" );
            var image = new Image { Source = "BackColor.jpg" };
            activelbl.Text = "Inactive";
            setRankInfo ();
            setOnTuitionOFF ();
            setIsActiveOffOrOn ( 0 );
            GetPeddingInfo (teacher.TeacherID);
            ConnectToServer ();
            SetInfoInInternalStorage ( teacher.UserName , teacher.Password , "Teacher" , 0 );
            StaticPageForOnSleep.isCallPending = false;
            isPermiteed();
        }
        public async void isPermiteed()
        {
            var cmStatus = await Permissions.CheckStatusAsync<Permissions.Camera>();
            var mediaStatus = await Permissions.CheckStatusAsync<Permissions.Media>();
            if (cmStatus != PermissionStatus.Granted || mediaStatus != PermissionStatus.Granted)
            {
                CrossOpenTok.Current.TryStartSession();
            }

        }
        public async Task SetInfoInInternalStorage ( string username , string password , string usertype , int parentCode )
        {
            await SecureStorage.SetAsync ( "username" , username ).ConfigureAwait ( false );
            await SecureStorage.SetAsync ( "password" , password ).ConfigureAwait ( false );
            await SecureStorage.SetAsync ( "usertype" , usertype ).ConfigureAwait ( false );
            await SecureStorage.SetAsync ( "parentCode" , "" + parentCode ).ConfigureAwait ( false );
        }
        public async Task GetPeddingInfo ( int id )
        {
            string urlT = "https://api.shikkhanobish.com/api/Master/GetPendingForTeacher";
            HttpClient clientT = new HttpClient ();
            string jsonDataT = JsonConvert.SerializeObject ( new { TeacherID = id } );
            StringContent contentT = new StringContent ( jsonDataT , Encoding.UTF8 , "application/json" );
            HttpResponseMessage responseT = await clientT.PostAsync ( urlT , contentT ).ConfigureAwait ( false );
            string resultT = await responseT.Content.ReadAsStringAsync ();
            var pendningRating = JsonConvert.DeserializeObject<List<IsPending>> ( resultT );
            var pending = pendningRating.Count;
            pendinglbl.Text = "" + pending;
        }
        public async void setRankInfo ( )
        {
            total = teacher.Five_Star * 5 + teacher.Four_Star * 4 + teacher.Three_Star * 3 + teacher.Two_Star * 2 + teacher.One_Star * 1;
            count = teacher.Five_Star + teacher.Four_Star + teacher.Three_Star + teacher.Two_Star + teacher.One_Star;
            avg = System.Math.Round ( total / count , 2 );
            tplbl.Text = "Tuition Point: " + teacher.Tuition_Point;
            avglbl.Text = "Average: " + avg;
            float place = 0;
            Calculate cal = new Calculate ();
            List<int> tpInfo = new List<int> ();
            List<float> avgInfo = new List<float> ();
            tpInfo = cal.GetTuitionPointInfo ();
            avgInfo = cal.GetAverageInfo ();
            if ( teacher.Tuition_Point < 20 )
            {
                ranklbl.Text = "Placement";
                place = (teacher.Tuition_Point / tpInfo[0])*.166f;            }
            else if ( teacher.Tuition_Point <= 999 || ( teacher.Tuition_Point > 999 && avg < 3.75f ) )
            {
                ranklbl.Text = "Newbie";
                if( teacher.Tuition_Point > 999 && avg < 3.75f )
                {
                    place = .3f;
                    avglbl.TextColor = Color.FromHex ( "F68181" );
                    tplbl.TextColor = Color.FromHex ( "F5C24B" );
                }
                else
                   {
                    float point = ( float ) teacher.Tuition_Point / ( float ) tpInfo [ 1 ];
                    place = ( point * .166f ) + .166f;
                    avglbl.TextColor = Color.FromHex ( "F68181" );
                    tplbl.TextColor = Color.FromHex ( "F68181" );
                }
                ranklbl.TextColor = Color.FromHex ( "F68181" );
                progress.ProgressColor = Color.FromHex ( "F68181" );
            }
            else if ( teacher.Tuition_Point <= 3999 && avg >= 3.750f || ( teacher.Tuition_Point > 3999 && avg < 3.75f ) )
            {
                ranklbl.Text = "Average";
                if ( teacher.Tuition_Point > 3999 && avg < 3.75f )
                {
                    place = .3f;
                    avglbl.TextColor = Color.FromHex ( "F5C24B" );
                    tplbl.TextColor = Color.FromHex ( "8AF077" );
                }
                else
                {
                    float point = ( float ) teacher.Tuition_Point / ( float ) tpInfo [ 1 ];
                    place = ( point * .166f ) + .166f;
                    avglbl.TextColor = Color.FromHex ( "F5C24B" );
                    tplbl.TextColor = Color.FromHex ( "F5C24B" );
                }
                ranklbl.TextColor = Color.FromHex ( "F5C24B" );
                progress.ProgressColor = Color.FromHex ( "F5C24B" );
            }
            else if ( teacher.Tuition_Point <= 8999 && avg >= 4.00f || ( teacher.Tuition_Point > 8999 && avg < 4.00f ) )
            {
                ranklbl.Text = "Good";
                if ( teacher.Tuition_Point > 8999 && avg < 4.00f )
                {
                    place = .3f;
                    avglbl.TextColor = Color.FromHex ( "8AF077" );
                    tplbl.TextColor = Color.FromHex ( "77CDF0" );
                }
                else
                {
                    float point = ( float ) teacher.Tuition_Point / ( float ) tpInfo [ 1 ];
                    place = ( point * .166f ) + .166f;
                    avglbl.TextColor = Color.FromHex ( "8AF077" );
                    tplbl.TextColor = Color.FromHex ( "8AF077" );
                }
                ranklbl.TextColor = Color.FromHex ( "8AF077" );
                progress.ProgressColor = Color.FromHex ( "8AF077" );
            }
            else if ( teacher.Tuition_Point <= 15999 && avg >= 4.30f || ( teacher.Tuition_Point > 15999 && avg < 4.30f ) )
            {
                ranklbl.Text = "Veteran";
                if ( teacher.Tuition_Point > 15999 && avg < 4.30f )
                {
                    place = .3f;
                    avglbl.TextColor = Color.FromHex ( "77CDF0" );
                    tplbl.TextColor = Color.FromHex ( "CA6AF1" );
                }
                else
                {
                    float point = ( float ) teacher.Tuition_Point / ( float ) tpInfo [ 1 ];
                    place = ( point * .166f ) + .166f;
                    avglbl.TextColor = Color.FromHex ( "77CDF0" );
                    tplbl.TextColor = Color.FromHex ( "77CDF0" );
                }
                ranklbl.TextColor = Color.FromHex ( "77CDF0" );
                progress.ProgressColor = Color.FromHex ( "77CDF0" );
            }
            else if ( teacher.Tuition_Point > 16000 && avg >= 4.30f )
            {
                ranklbl.Text = "Master";
                float point = ( float ) teacher.Tuition_Point / ( float ) tpInfo [ 1 ];
                place = ( point * .166f ) + .166f;
                avglbl.TextColor = Color.FromHex ( "CA6AF1" );
                tplbl.TextColor = Color.FromHex ( "CA6AF1" );
                ranklbl.TextColor = Color.FromHex ( "CA6AF1" );
                progress.ProgressColor = Color.FromHex ( "CA6AF1" );
            }
            await progress.ProgressTo ( place , 500 , Easing.SinIn ).ConfigureAwait ( false );
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            //await Application.Current.MainPage.Navigation.PushModalAsync(new RegisterAsTeacher()).ConfigureAwait(true);
            //this.IsPresented = true;
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            this.IsPresented = true;
        }


        

        protected override bool OnBackButtonPressed()
        {
            giveAlert();
            return true;
        }

        public async void giveAlert()
        {
            bool answer = await DisplayAlert("Alert", "Would you like to quit?", "Yes", "No").ConfigureAwait( false );
            if (answer == true)
            {
                System.Environment.Exit(0);
            }
        }

        private async void Button_Clicked_4(object sender, EventArgs e)
        {
            SecureStorage.RemoveAll ();
            await Application.Current.MainPage.Navigation.PushModalAsync(new MainPage()).ConfigureAwait( false );
        }

        private async void Button_Clicked_5(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync ( new TeacherHistory (teacher.TeacherID) ).ConfigureAwait ( false );
        }

        private async void Button_Clicked_6(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new UpdateAccount(_Student, teacher ,false) ).ConfigureAwait( false );
        }

        private async void Button_Clicked_7(object sender, EventArgs e)
        {

            ac++;

            if(ac%2 == 1)
            {
                ShowOffLineInStudentWindowRealTime (true);
                takeTuition = true;
                activelbl.Text = "Active";
                activeback.BackgroundColor = Color.FromHex ( "#54E36B" );
                setIsActiveOffOrOn ( 1 );
            }
            else
            {
                ShowOffLineInStudentWindowRealTime (false);
                takeTuition = false;
                activelbl.Text = "Inactive";
                activeback.BackgroundColor = Color.FromHex ( "#A7A7A7" );
                setIsActiveOffOrOn (0);
            }
            
        }

        private void Button_Clicked_2 ( object sender , EventArgs e )
        {
            try
            {
                if ( CrossConnectivity.Current.IsConnected )
                {
                    Navigation.PushPopupAsync ( new PopUpForTeacherWallet (teacher ) );
                }
            }
            catch
            {

            }
        }

        HubConnection _connection = null;
        bool isConnected = false;
        string connectionStatus = "Closed";
        string url = "https://shikkhanobishrealtimeapi.shikkhanobish.com/ShikkhanobishHub", msgFromApi = "";
        public async Task ConnectToServer ( )
        {
            _connection = new HubConnectionBuilder ()
                .WithUrl ( url )
                .Build ();

            await _connection.StartAsync ();
            isConnected = true;
            connectionStatus = "Connected";

            _connection.Closed += async ( s ) =>
            {
                isConnected = false;
                connectionStatus = "Disconnected";
                await _connection.StartAsync ();
                isConnected = true;

            };
            _connection.On< string , string , int , int , string , string, double, string> ( "CallInfo" , async ( SessionId , UserToken , studentID , teacherID , Class , subject,cost, studentName ) =>
            { 
                if(takeTuition == true)
                {
                    if ( teacher.TeacherID == teacherID )
                    {
                        takeTuition = false;
                        TransferInfo Info = new TransferInfo ();
                        Info.Student.Name = studentName;
                        Info.Class = Class;
                        Info.SubjectName = subject;
                        Info.SessionID = SessionId;
                        Info.UserToken = UserToken;
                        Info.Student.StudentID = studentID;
                        Info.Teacher = teacher;
                        Info.Teacher.Amount = cost;
                        activelbl.Text = "Inactive";
                        activeback.BackgroundColor = Color.FromHex ( "#A7A7A7" );
                        ShowOffLineInStudentWindowRealTime (false);
                        if ( StaticPageForOnSleep.isSleep == true)
                        {
                            StaticPageForOnSleep.isCallPending = true;
                            StaticPageForOnSleep.info = Info;
                            MainThread.BeginInvokeOnMainThread ( ( ) => { DependencyService.Get<INotification> ().CreateNotification ( "Shikkhanobish" , studentName + " is calling you for tuition. Tap to continue..." ); } );
                        }
                        else
                        {
                            MainThread.BeginInvokeOnMainThread ( async ( ) => {
                                await Application.Current.MainPage.Navigation.PushModalAsync ( new CallingPageForTeacher ( Info,0 ) ).ConfigureAwait ( false );
                            } );
                        }
                        
                       
                    }
                }
                
                
            } );
        }

        public async void setOnTuitionOFF (  )
        {
            string urlT = "https://api.shikkhanobish.com/api/Master/ChangeStateofIsOnTuition";
            HttpClient clientT = new HttpClient ();
            string jsonDataT = JsonConvert.SerializeObject ( new { TeacherID = teacher.TeacherID , state = 0 } );
            StringContent contentT = new StringContent ( jsonDataT , Encoding.UTF8 , "application/json" );
            HttpResponseMessage responseT = await clientT.PostAsync ( urlT , contentT ).ConfigureAwait ( false );
            string resultT = await responseT.Content.ReadAsStringAsync ();
            var response = JsonConvert.DeserializeObject<Response> ( resultT );
        }

        public async void setIsActiveOffOrOn(int state)
        {
            string urlT = "https://api.shikkhanobish.com/api/Master/ChangeStateofIsActive";
            HttpClient clientT = new HttpClient ();
            string jsonDataT = JsonConvert.SerializeObject ( new { TeacherID = teacher.TeacherID , state = state } );
            StringContent contentT = new StringContent ( jsonDataT , Encoding.UTF8 , "application/json" );
            HttpResponseMessage responseT = await clientT.PostAsync ( urlT , contentT ).ConfigureAwait ( false );
            string resultT = await responseT.Content.ReadAsStringAsync ();
            var response = JsonConvert.DeserializeObject<Response> ( resultT );
        }

        public async Task ShowOffLineInStudentWindowRealTime ( bool isOnline)
        {
            string url = "https://shikkhanobishrealtimeapi.shikkhanobish.com/api/ShikkhanobishRealTimeApi/TurnOffActiveStatus?TeacherID=" + teacher.TeacherID + "&isOnline="+ isOnline;
            HttpClient client = new HttpClient ();
            StringContent content = new StringContent ( "" , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( true );
            string result = await response.Content.ReadAsStringAsync ().ConfigureAwait ( true );
            var r = JsonConvert.DeserializeObject<string> ( result );
        
        }
    }
}