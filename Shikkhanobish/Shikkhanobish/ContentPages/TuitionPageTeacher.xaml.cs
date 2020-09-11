using Microsoft.AspNetCore.SignalR.Client;
using Shikkhanobish.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
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
            ConnectToServer ();
        }

        private async void OnEndCall ( object sender , EventArgs e )
        {
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
            _connection.On<int , int> ( "sendTime" , async ( sec , teacherID ) =>
            {
                if ( info.Teacher.TeacherID == teacherID )
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