using Shikkhanobish.ViewModel;
using System;
using Xamarin.Forms;
using System.Timers;
using Xamarin.Forms.OpenTok.Service;
using Xamarin.Forms.Xaml;
using Java.Lang;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Shikkhanobish.Model;
using Shikkhanobish.ContentPages.Common;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Essentials;

namespace Shikkhanobish.ContentPages
{
    [XamlCompilation ( XamlCompilationOptions.Compile )]
    public partial class TutionPage : ContentPage
    {
        private bool iscut;
        private TransferInfo info = new TransferInfo ();
        private Timer timer = new Timer ();
        int sec, min;
        int ownthing = 0, i=0;
        bool firstTime ,isstudent, isNewTeacher;
        float totalCost;
        public TutionPage ( TransferInfo trnsInfo)
        {
            isNewTeacher = false;
            iscut = false;
            InitializeComponent ();
            info = trnsInfo;
            sec = 0;
            min = 0;
            firstTime = true;
            safelbl.Text = "Safe Time";
            tnamelbl.Text = trnsInfo.Teacher.TeacherName;
            SetIsPending ();
            //SetSubject ();
            SecureStorage.SetAsync ( "subject_name" , info.SubjectName );
            ConnectToServer ();
            if(trnsInfo.Teacher.Total_Min <= 20)
            {
                isNewTeacher = true;
            }
   
            Device.StartTimer ( TimeSpan.FromSeconds ( 1.0 ) , UpdateTimerAndInfo );                      
        }
        public async void SetSubject ()
        {
            await SecureStorage.SetAsync ( "subject_name" , info.SubjectName ).ConfigureAwait ( false );
        }
        private async void OnEndCall ( object sender , EventArgs e )
        {
            info.StudentCost = calculate.CalculateCost ( info  );
            iscut = true;
            CrossOpenTok.Current.EndSession ();
            _connection.StopAsync ();
            await CutVideoCAll ().ConfigureAwait(false);
            gotoRatingPage ();           
           
        }
        
        private void OnSwapCamera ( object sender , EventArgs e )
        {
            CrossOpenTok.Current.CycleCamera ();
        }
        private void StopOwnVideo ( object sender , EventArgs e )
        {
            ownthing++;

            if(ownthing%2 == 1)
            {
                ownvideo.IsVisible = false;

            }
            else
            {
                ownvideo.IsVisible = true;

            }
            
        }

        protected override bool OnBackButtonPressed ( )
        {
            Navigation.PushPopupAsync ( new PopUpForTextAlert ( "Do You want to cut the call?" , "If you want to cut the call, press cut video icon" , false ) );
            return true;
        }
        public async void gotoRatingPage ( )
        {
            Device.BeginInvokeOnMainThread ( async ( ) =>
            {
                await Application.Current.MainPage.Navigation.PushModalAsync ( new RatingPage ( info , true ) ).ConfigureAwait ( false );
            } );
        }
        Calculate calculate = new Calculate();
        TransferInfo timeinfo = new TransferInfo ();
        Calculate cal = new Calculate ();
        private bool UpdateTimerAndInfo ( )
        {
            sec = sec + 1;
            if ( sec == 60 )
            {
                min = min + 1;
                sec = 0;
            }
            if(firstTime ==  true)
            {
                safelbl.IsVisible = true;
                timerlbl.TextColor = Color.Green;
                if(sec == 10)
                {
                    sec = 0;
                    firstTime = false;
                    safelbl.Text = "Pay Time";
                    safelbl.TextColor = Color.DarkSlateBlue;
                    timerlbl.TextColor = Color.Black;
                    StartTime ();
                }
            }
            if(firstTime == false && sec == 31)
            {
                info.StudyTimeInAPp = min+1;
                safelbl.Text = "Cost: " + cal.CalculateCost (info );
                SendCostRoTeacher ( calculate.CalculateCostForTeacher ( info , isNewTeacher) );
                if(min  < info.Student.freeMin)
                {
                    totalCost = 0;
                    SetCost ( 0 , 0 , calculate.CalculateCostForTeacher ( info, isNewTeacher ),1 );
                }
                else
                {
                    totalCost = calculate.CalculateCost ( info );
                    SetCost ( calculate.CalculateCost ( info  ) , calculate.CalculateCostPerminStudent ( info) , calculate.CalculateCostForTeacher ( info , isNewTeacher) ,0);
                }
                
            }
            timerlbl.Text = min + ":" + sec;
            if( iscut == false)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        

        
        HubConnection _connection = null;
        bool isConnected = false;
        string connectionStatus = "Closed";
        string url = "https://shikkhanobishrealtimeapi.shikkhanobish.com/ShikkhanobishHub", msgFromApi = "";


        int cutCallFirstTime = 0;
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
            _connection.On<int , int , int , bool> ( "cutCall" , async ( stop , teacherID , studentID , isStudent ) =>
            {
                cutCallFirstTime++;
                if ( isStudent == false  && cutCallFirstTime == 1)
                {
                    if ( info.Student.StudentID == studentID )
                    {
                        CrossOpenTok.Current.EndSession ();
                        _connection.StopAsync ();
                        gotoRatingPage ();
                    }
                }

            } );
        }

        
        public async void SetIsPending ( )
        {
            string url = "https://api.shikkhanobish.com/api/Master/SetPending";
            HttpClient client = new HttpClient ();
            string jsonData = JsonConvert.SerializeObject ( new { StudentID = info.Student.StudentID , TeacherName = info.Teacher.TeacherName , TeacherID = info.Teacher.TeacherID , Class = info.Class , Subject = info.Subject ,Time = 0, Cost = 0 } );
            StringContent content = new StringContent ( jsonData , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( false );
            string result = await response.Content.ReadAsStringAsync ();
        }
        float previouscostst, previousearnteach;
        public async void SetCost ( float cost, float stcost, float teacherearn, int FreeMinMinus )
        {
            float miancostst = stcost - previouscostst;
            float mianearnt = teacherearn - previousearnteach;
            previouscostst = stcost;
            previousearnteach = teacherearn;
            string url = "https://api.shikkhanobish.com/api/Master/SetCostinIspending";
            HttpClient client = new HttpClient ();
            string jsonData = JsonConvert.SerializeObject ( new { StudentID = info.Student.StudentID ,  Cost = cost, Time = info.StudyTimeInAPp , StudentCostMin  = miancostst , TeacherEarnMin = mianearnt , TeacherID = info.Teacher.TeacherID, FreeMinMinus = FreeMinMinus } );
            StringContent content = new StringContent ( jsonData , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( false );
            string result = await response.Content.ReadAsStringAsync ();
        }
        public async Task CutVideoCAll ( )
        {

            string url = "https://shikkhanobishrealtimeapi.shikkhanobish.com/api/ShikkhanobishRealTimeApi/cutCall?stop=" + 1 + "&teacherID=" + info.Teacher.TeacherID + "&studentID=" + info.Student.StudentID + "&isStudent=true";
            HttpClient client = new HttpClient ();
            StringContent content = new StringContent ( "" , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( true );
            string result = await response.Content.ReadAsStringAsync ().ConfigureAwait ( true );
            
        }
        public async Task SendCostRoTeacher ( float cost)
        {

            string url = "https://shikkhanobishrealtimeapi.shikkhanobish.com/api/ShikkhanobishRealTimeApi/sendCost?cost=" + cost + "&teacherID=" + info.Teacher.TeacherID + "&studentID=" + info.Student.StudentID;
            HttpClient client = new HttpClient ();
            StringContent content = new StringContent ( "" , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( true );
            string result = await response.Content.ReadAsStringAsync ().ConfigureAwait ( true );
            var r = JsonConvert.DeserializeObject<string> ( result );
        }

        public async Task StartTime ( )
        {
            string url = "https://shikkhanobishrealtimeapi.shikkhanobish.com/api/ShikkhanobishRealTimeApi/sendTime?teacherID="+ info.Teacher.TeacherID;
            HttpClient client = new HttpClient ();
            StringContent content = new StringContent ( "" , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( true );
            string result = await response.Content.ReadAsStringAsync ().ConfigureAwait ( true );
            var r = JsonConvert.DeserializeObject<string> ( result );
        }

    }
}