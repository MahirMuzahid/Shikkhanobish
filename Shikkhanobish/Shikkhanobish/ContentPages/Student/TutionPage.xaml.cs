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

namespace Shikkhanobish.ContentPages
{
    [XamlCompilation ( XamlCompilationOptions.Compile )]
    public partial class TutionPage : ContentPage
    {
        private TransferInfo info = new TransferInfo ();
        private Timer timer = new Timer ();
        int sec, min;
        int ownthing = 0, i=0;
        bool firstTime ,isstudent;

        public TutionPage ( TransferInfo trnsInfo)
        {
            InitializeComponent ();
            info = trnsInfo;
            sec = 0;
            min = 0;
            firstTime = true;
            safelbl.Text = "Safe Time";
            tnamelbl.Text = trnsInfo.Teacher.TeacherName;
            StaticPageForSavingInfoOnStop.StudentID = trnsInfo.Student.StudentID;
            StaticPageForSavingInfoOnStop.TeacherName = trnsInfo.Teacher.TeacherName;
            StaticPageForSavingInfoOnStop.TeacherID = trnsInfo.Teacher.TeacherID;
            StaticPageForSavingInfoOnStop.Class = trnsInfo.Class;
            StaticPageForSavingInfoOnStop.Subject = trnsInfo.Subject;
            SetIsPending ();
            Device.StartTimer ( TimeSpan.FromSeconds ( 1.0 ) , UpdateTimerAndInfo );           
            ConnectToServer ();
        }

        private async void OnEndCall ( object sender , EventArgs e )
        {
            CrossOpenTok.Current.EndSession ();
            _connection.StopAsync ();
            await TransferPoint ().ConfigureAwait(false);
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
            Navigation.PushPopupAsync ( new PopUpForTextAlert ( "" , "" , true ) );
            return true;
        }
        public async void gotoRatingPage ( )
        {
            //info.StudyTimeInAPp = Int32.Parse(TimeEntry.Text);
            if(sec > 30)
            {
                min = min+1;
            }
            info.StudyTimeInAPp = min;
            await Application.Current.MainPage.Navigation.PushModalAsync ( new RatingPage ( info,true ) ).ConfigureAwait ( false );
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
                if(sec == 20)
                {
                    sec = 0;
                    firstTime = false;
                    safelbl.Text = "Pay Time";
                    safelbl.TextColor = Color.DarkSlateBlue;
                    timerlbl.TextColor = Color.Black;
                    StartTime ();
                }
            }
            if(firstTime == false && sec > 30 && info.Teacher.Teacher_Rank != "Placement")
            {
                info.StudyTimeInAPp = min+1;
                safelbl.Text = "Pay Time, Cost: " + cal.CalculateCost (info);                
                StaticPageForSavingInfoOnStop.StudentCost = calculate.CalculateCost ( info );
                StaticPageForSavingInfoOnStop.TeacherEarn = calculate.CalculateCostForTeacher ( info );
                SetCost ( calculate.CalculateCost  (info ), calculate.CalculateCost ( info ) , calculate.CalculateCostForTeacher ( info ) );
            }
            timerlbl.Text = min + ":" + sec;

            return true;
        }

        public async Task StartTime ( )
        {
            string url = "https://shikkhanobishrealtimeapi.shikkhanobish.com/api/ShikkhanobishRealTimeApi/sendTime?sec=" + 1 + "&teacherID=" + info.Teacher.TeacherID;
            HttpClient client = new HttpClient ();
            StringContent content = new StringContent ( "" , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( true );
            string result = await response.Content.ReadAsStringAsync ().ConfigureAwait ( true );
            var r = JsonConvert.DeserializeObject<string> ( result );
        }

        public async Task CutVideoCAll (  )
        {
            
            string url = "https://shikkhanobishrealtimeapi.shikkhanobish.com/api/ShikkhanobishRealTimeApi/cutCall?stop=" + 1 +"&teacherID=" + info.Teacher.TeacherID + "&studentID=" + info.Student.StudentID + "&isStudent=" + true;
            HttpClient client = new HttpClient ();
            StringContent content = new StringContent ( "" , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( true );
            string result = await response.Content.ReadAsStringAsync ().ConfigureAwait ( true );
            var r = JsonConvert.DeserializeObject<string> ( result );
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
                        TransferPoint ();
                        _connection.StopAsync ();
                        gotoRatingPage ();
                    }
                }

            } );
        }

        public async Task TransferPoint ()
        {
            int TeacherEarn = calculate.CalculateCostForTeacher ( info );
            int StudentCost = calculate.CalculateCost ( info );
            info.StudentCost = StudentCost;
            info.TeacherEarn = TeacherEarn;


            string urlT = "https://api.shikkhanobish.com/api/Master/TransferPoint";
            HttpClient clientT = new HttpClient ();
            string jsonDataT = JsonConvert.SerializeObject ( new { TeacherEarn = TeacherEarn , StudentCost = StudentCost , StudentID = info.Student.StudentID, TeacherID =info.Teacher.TeacherID } );
            StringContent contentT = new StringContent ( jsonDataT , Encoding.UTF8 , "application/json" );
            HttpResponseMessage responseT = await clientT.PostAsync ( urlT , contentT ).ConfigureAwait ( false );
            string resultT = await responseT.Content.ReadAsStringAsync ();
            var r = JsonConvert.DeserializeObject<Response> ( resultT );
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
        int previouscostst, previousearnteach;
        public async void SetCost ( int cost, int stcost, int teacherearn)
        {
            int miancostst = stcost - previouscostst;
            int mianearnt = teacherearn - previousearnteach;
            previouscostst = stcost;
            previousearnteach = teacherearn;
            string url = "https://api.shikkhanobish.com/api/Master/SetCostinIspending";
            HttpClient client = new HttpClient ();
            string jsonData = JsonConvert.SerializeObject ( new { StudentID = info.Student.StudentID ,  Cost = cost, Time = info.StudyTimeInAPp , StudentCostMin  = miancostst , TeacherEarnMin = mianearnt , TeacherID = info.Teacher.TeacherID} );
            StringContent content = new StringContent ( jsonData , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( false );
            string result = await response.Content.ReadAsStringAsync ();
        }


    }
}