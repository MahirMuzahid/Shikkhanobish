using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
using Shikkhanobish.ContentPages.Common;
using Shikkhanobish.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.OpenTok.Service;
using Xamarin.Forms.Xaml;

namespace Shikkhanobish.ContentPages
{
    [XamlCompilation ( XamlCompilationOptions.Compile )]
    public partial class TuitionPageTeacher : ContentPage
    {
        private TransferInfo info = new TransferInfo ();
        int sec, min;
        int ownthing = 0, i = 0;
        bool firstTime;
        public TuitionPageTeacher ( TransferInfo trnsInfo )
        {
            InitializeComponent ();
            info = trnsInfo;
            sec = 0;
            min = 0;
            tnamelbl.Text = info.Student.Name;
            ConnectToServer();
            safelbl.IsVisible = true;
            safelbl.Text = "Safe Time";
            safelbl.TextColor = Color.Yellow;
            timerlbl.TextColor = Color.Yellow;
            timerlbl.Text = "0:0";
           
        }
        protected override bool OnBackButtonPressed ( )
        {
            Navigation.PushPopupAsync ( new PopUpForTextAlert ( "" , "" , true ) );
            return true;
        }
        public async Task CutVideoCAll ( )
        {
            string url = "https://shikkhanobishrealtimeapi.shikkhanobish.com/api/ShikkhanobishRealTimeApi/cutCall?stop=" + 1 + "&teacherID=" + info.Teacher.TeacherID + "&studentID=" + info.Student.StundentID + "&isStudent=" + false;
            HttpClient client = new HttpClient ();
            StringContent content = new StringContent ( "" , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( true );
            string result = await response.Content.ReadAsStringAsync ().ConfigureAwait ( true );
            var r = JsonConvert.DeserializeObject<string> ( result );
        }
        private async void OnEndCall ( object sender , EventArgs e )
        {
            setOnTuitionOFF ();
            setIsActiveOFF ();
            CutVideoCAll ();
            _connection.StopAsync ();
            CrossOpenTok.Current.EndSession ();
            await Application.Current.MainPage.Navigation.PushModalAsync ( new TeacherProfile ( info.Teacher ) ).ConfigureAwait ( false );

        }

        private void OnSwapCamera ( object sender , EventArgs e )
        {
            CrossOpenTok.Current.CycleCamera ();
        }
        private void StopOwnVideo ( object sender , EventArgs e )
        {
            ownthing++;

            if ( ownthing % 2 == 1 )
            {
                ownvideo.IsVisible = false;

            }
            else
            {
                ownvideo.IsVisible = true;

            }

        }


        private bool CheckPositionAndUpdateSlider ( )
        {
            timerlbl.TextColor = Color.Black;
            safelbl.TextColor = Color.Green;
            safelbl.Text = "Pay Time";
            sec = sec + 1;
            if ( sec == 59 )
            {
                min = min + 1;
                sec = 0;
            }           
            timerlbl.Text = min + ":" + sec;

            return true;
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
            _connection.On<int , int> ( "sendTime" , async ( sec , teacherID ) =>
            {
                if ( info.Teacher.TeacherID == teacherID )
                {
                    Device.StartTimer ( TimeSpan.FromSeconds ( 1.0 ) , CheckPositionAndUpdateSlider );
                }

            } );
            _connection.On<int , int, int, bool> ( "cutCall" , async ( stop , teacherID ,  studentID ,  isStudent ) =>
            {
                cutCallFirstTime++;
                if (isStudent == true && cutCallFirstTime == 1 )
                {
                    if ( info.Teacher.TeacherID == teacherID )
                    {
                        setOnTuitionOFF ();
                        setIsActiveOFF ();
                        CrossOpenTok.Current.EndSession ();
                        _connection.StopAsync ();
                        await Application.Current.MainPage.Navigation.PushModalAsync ( new TeacherProfile ( info.Teacher ) ).ConfigureAwait ( false );
                    }
                }
                
            } );


        }

        public async void setOnTuitionOFF()
        {
            string urlT = "https://api.shikkhanobish.com/api/Master/ChangeStateofIsOnTuition";
            HttpClient clientT = new HttpClient ();
            string jsonDataT = JsonConvert.SerializeObject ( new { TeacherID = info.Teacher.TeacherID , state = 0 } );
            StringContent contentT = new StringContent ( jsonDataT , Encoding.UTF8 , "application/json" );
            HttpResponseMessage responseT = await clientT.PostAsync ( urlT , contentT ).ConfigureAwait ( false );
            string resultT = await responseT.Content.ReadAsStringAsync ();
            var response = JsonConvert.DeserializeObject<Response> ( resultT );
        }

        public async void setIsActiveOFF()
        {
            string urlT = "https://api.shikkhanobish.com/api/Master/ChangeStateofIsActive";
            HttpClient clientT = new HttpClient ();
            string jsonDataT = JsonConvert.SerializeObject ( new { TeacherID = info.Teacher.TeacherID , state = 0 } );
            StringContent contentT = new StringContent ( jsonDataT , Encoding.UTF8 , "application/json" );
            HttpResponseMessage responseT = await clientT.PostAsync ( urlT , contentT ).ConfigureAwait ( false );
            string resultT = await responseT.Content.ReadAsStringAsync ();
            var response = JsonConvert.DeserializeObject<Response> ( resultT );
        }
    }
}