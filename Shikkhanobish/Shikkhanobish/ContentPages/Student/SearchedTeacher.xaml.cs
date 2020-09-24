﻿using Newtonsoft.Json;
using Shikkhanobish.Model;
using Shikkhanobish.ViewModel;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Xamarin.Essentials;
using Android.Database;

namespace Shikkhanobish.ContentPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchedTeacher : ContentPage
    {
        private TransferInfo info = new TransferInfo();
        private List<TeacherID> TeacherIDListBySearch = new List<TeacherID>();
        private List<Teacher> TeacherList = new List<Teacher>();
        private List<Teacher> FilteredTeacher = new List<Teacher>();
        HubConnection _connection = null;
        bool isConnected = false;
        string connectionStatus = "Closed";
        string url = "https://shikkhanobishrealtimeapi.shikkhanobish.com/ShikkhanobishHub", msgFromApi = "";
        int cutCallFirstTime = 0;

        public SearchedTeacher(TransferInfo transInfo, List<Teacher> teacherList)
        {
            InitializeComponent();
            info = transInfo;
            FilteredTeacher = teacherList;
            SetEveryThing ();
            ConnectToServer ();
        }

        public void SetEveryThing()
        {
            TeacherListView.ItemsSource = FilteredTeacher;
        }

        private async void TeacherListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedTeacher = e.Item as Teacher;
            info.Teacher = selectedTeacher;
            beSure();
        }

        public async void beSure()
        {
            if ( info.Teacher.IsOnTuition == 0 && info.Teacher.IsActive == 1)
            {
                await Navigation.PushPopupAsync ( new PopUpForSelectedTeacher ( info ) ).ConfigureAwait ( false );
            }
                      
        }

        
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
            _connection.On<int,bool > ( "TurnOffActiveStatus" , async ( TeacherID, isOnline ) =>
            {
                bool isTeacherhere = false;
                if(isOnline == false)
                {
                    for ( int i = 0; i < FilteredTeacher.Count; i++ )
                    {
                        if ( FilteredTeacher [ i ].TeacherID == TeacherID )
                        {
                            isTeacherhere = true;
                            FilteredTeacher [ i ].IsActive = 0;
                            FilteredTeacher [ i ].TeacherStatus = "Offline";
                            FilteredTeacher [ i ].TeacherStatusColor = "#939393";
                        }
                    }
                    if(isTeacherhere == true)
                    {
                        await Task.Run ( ( ) =>
                        {
                            MainThread.BeginInvokeOnMainThread ( ( ) =>
                            {
                                TeacherListView.ItemsSource = null;
                                TeacherListView.ItemsSource = FilteredTeacher;
                            } );
                        } ).ConfigureAwait ( false );
                    }
                    
                }
                if ( isOnline == true )
                {
                    for ( int i = 0; i < FilteredTeacher.Count; i++ )
                    {
                        if ( FilteredTeacher [ i ].TeacherID == TeacherID )
                        {
                            isTeacherhere = true;
                            MainThread.BeginInvokeOnMainThread ( ( ) => {
                                FilteredTeacher [ i ].IsActive = 1;
                                FilteredTeacher [ i ].TeacherStatus = "Call Now";
                                FilteredTeacher [ i ].TeacherStatusColor = "#43CF56";

                            } );
                        }
                    }
                    if ( isTeacherhere == true )
                    {
                        await Task.Run ( ( ) =>
                        {
                            MainThread.BeginInvokeOnMainThread ( ( ) =>
                            {
                                TeacherListView.ItemsSource = null;
                                TeacherListView.ItemsSource = FilteredTeacher;
                            } );
                        } ).ConfigureAwait ( false );
                        
                    }

                }

            } );
        }


    }
}