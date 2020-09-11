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

        public TutionPage ( TransferInfo trnsInfo , bool IsStudent)
        {
            InitializeComponent ();
            info = trnsInfo;
            sec = 0;
            min = 0;
            firstTime = true;
            isstudent = IsStudent;
           
            if(IsStudent == false)
            {
                tnamelbl.Text = info.Student.Name;
                ConnectToServer ();
            }
            else
            {
                tnamelbl.Text = trnsInfo.Teacher.TeacherName;
                Device.StartTimer ( TimeSpan.FromSeconds ( 1.0 ) , CheckPositionAndUpdateSlider );
            }
        }

        private async void OnEndCall ( object sender , EventArgs e )
        {
            if(isstudent == true)
            {
                CrossOpenTok.Current.EndSession ();
                gotoRatingPage ();
            }
            else
            {
                await Application.Current.MainPage.Navigation.PushModalAsync ( new TeacherProfile ( info.Teacher ) ).ConfigureAwait ( false );
            }
           
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
            Device.StartTimer ( new TimeSpan ( 0 , 0 , 60 ) , ( ) =>
            {
                // do something every 60 seconds
                Device.BeginInvokeOnMainThread ( ( ) =>
                {
                   
                    // interact with UI elements
                } );
                return true; // runs again, or false to stop
            } );
        }


        public async void gotoRatingPage ( )
        {
            //info.StudyTimeInAPp = Int32.Parse(TimeEntry.Text);
            if(sec > 30)
            {
                min += min;
            }
            info.StudyTimeInAPp = min;
            await Application.Current.MainPage.Navigation.PushModalAsync ( new RatingPage ( info ) ).ConfigureAwait ( false );
        }


        private bool CheckPositionAndUpdateSlider ( )
        {
            sec = sec + 1;
            if ( sec == 59 )
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
                    safelbl.IsVisible = false;
                    timerlbl.TextColor = Color.Black;
                }
            }
            timerlbl.Text = min + ":" + sec;
            SendUpdateTime (sec,info.Teacher.TeacherID);
            return true;
        }


        public async Task SendUpdateTime ( int sec  ,int teacherID )
        {
            string url = "https://shikkhanobishrealtimeapi.shikkhanobish.com/api/ShikkhanobishRealTimeApi/SendStudentThatCallRecivedOrIgnored?sec="+ sec + "&teacherID=" + teacherID ;
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
            _connection.On<int , int > ( "sendTime" , async ( sec , teacherID  ) =>
            {
                if(info.Teacher.TeacherID == teacherID && isstudent == false)
                {
                    if ( sec == 59 )
                    {
                        min = min + 1;
                        sec = 0;
                    }
                    if ( firstTime == true )
                    {
                        safelbl.IsVisible = true;
                        timerlbl.TextColor = Color.Green;
                        if ( sec == 20 )
                        {
                            sec = 0;
                            firstTime = false;
                            safelbl.IsVisible = false;
                            timerlbl.TextColor = Color.Black;
                        }
                    }
                    timerlbl.Text = min + ":" + sec;
                }

            } );
        }
    }
}