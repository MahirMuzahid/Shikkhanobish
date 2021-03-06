﻿using Microsoft.AspNetCore.SignalR.Client;
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
        float teacherEarn;
        public TuitionPageTeacher ( TransferInfo trnsInfo )
        {
            teacherEarn = 0;
            InitializeComponent ();
            info = trnsInfo;
            sec = 0;
            min = 0;
            tnamelbl.Text = info.Student.Name;
            ConnectToServer();
            safelbl.IsVisible = true;
            safelbl.Text = "Safe Time";
            safelbl.TextColor = Color.DarkGray;
            timerlbl.TextColor = Color.DarkGray;
            timerlbl.Text = "0:0";            
        }
        protected override bool OnBackButtonPressed ( )
        {
            Navigation.PushPopupAsync ( new PopUpForTextAlert ( "Do You want to cut the call?" , "If you want to cut the call, press cut video icon" , false ) );
            return true;
        }
        public async Task CutVideoCAll ( )
        {
            string url = "https://shikkhanobishrealtimeapi.shikkhanobish.com/api/ShikkhanobishRealTimeApi/cutCall?stop=" + 1 + "&teacherID=" + info.Teacher.TeacherID + "&studentID=" + info.Student.StudentID + "&isStudent=false";
            HttpClient client = new HttpClient ();
            StringContent content = new StringContent ( "" , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( true );
            string result = await response.Content.ReadAsStringAsync ().ConfigureAwait ( true );
            var r = JsonConvert.DeserializeObject<string> ( result );
        }
        private async void OnEndCall ( object sender , EventArgs e )
        {
            CutVideoCAll ();
            _connection.StopAsync ();            
            GoProfile();
            CrossOpenTok.Current.EndSession();
        }
        public void GoProfile ()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new TeacherProfile(info.Teacher)).ConfigureAwait(false);
            });
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


        private void UpdateMin ( )
        {
            min = min + 1;
         
            timerlbl.Text = min + " Minute";

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

            _connection.On<float , int , int> ( "sendCost" , async ( cost , teacherID , studentID ) =>
            {
                if ( info.Teacher.TeacherID == teacherID && info.Student.StudentID == studentID )
                {
                    teacherEarn = teacherEarn + cost;
                    timerlbl.TextColor = Color.Black;
                    if(info.Teacher.Total_Min + min < 15)
                    {
                        safelbl.TextColor = Color.Gold;
                        safelbl.Text = "Placement Time";
                    }
                    else
                    {
                        safelbl.TextColor = Color.Green;
                        safelbl.Text = "Earned: " + teacherEarn;
                    }
                    
                    UpdateMin ();
                }

            } );
            _connection.On<int , int, int, bool> ( "cutCall" , async ( stop , teacherID ,  studentID ,  isStudent ) =>
            {
                if (isStudent == true )
                {
                    if ( info.Teacher.TeacherID == teacherID )
                    {
                        CrossOpenTok.Current.EndSession();
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await Application.Current.MainPage.Navigation.PushModalAsync(new TeacherProfile(info.Teacher)).ConfigureAwait(false);
                        });                        
                        _connection.StopAsync();
                       
                        
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