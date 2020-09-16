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
            Device.StartTimer ( TimeSpan.FromSeconds ( 1.0 ) , CheckPositionAndUpdateSlider );
            ConnectToServer ();
        }

        private async void OnEndCall ( object sender , EventArgs e )
        {
            CrossOpenTok.Current.EndSession ();
            _connection.StopAsync ();
            CutVideoCAll ();
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


        public async void gotoRatingPage ( )
        {
            //info.StudyTimeInAPp = Int32.Parse(TimeEntry.Text);
            if(sec > 30)
            {
                min = min+1;
            }
            info.StudyTimeInAPp = min;
            await Application.Current.MainPage.Navigation.PushModalAsync ( new RatingPage ( info ) ).ConfigureAwait ( false );
        }

        TransferInfo timeinfo = new TransferInfo ();
        Calculate cal = new Calculate ();
        private bool CheckPositionAndUpdateSlider ( )
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
            if(firstTime == false && sec > 30/*&& info.Teacher.Teacher_Rank != "Placement"*/)
            {
                min = min + 1;
                timeinfo.StudyTimeInAPp = min;
                safelbl.Text = "Pay Time, Cost: " + cal.CalculateCost (timeinfo); 
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
            string url = "https://shikkhanobishrealtimeapi.shikkhanobish.com/api/ShikkhanobishRealTimeApi/cutCall?stop=" + 1 +"&teacherID=" + info.Teacher.TeacherID + "&studentID=" + info.Student.StundentID + "&isStudent=" + true;
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
                    if ( info.Student.StundentID == studentID )
                    {
                        CrossOpenTok.Current.EndSession ();
                        _connection.StopAsync ();
                        gotoRatingPage ();
                    }
                }

            } );
        }

    }
}