using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shikkhanobish.Model;
using Shikkhanobish.ViewModel;
using Xamarin.Forms.OpenTok.Service;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System.Diagnostics.Tracing;
using OpenTokSDK;

namespace Shikkhanobish.ContentPages
{
    [XamlCompilation ( XamlCompilationOptions.Compile )]
    public partial class CallingPageStudent : ContentPage
    {
        private TransferInfo Info;
        private int apiKey;
        private string SessionID, Token;
        private bool isstudent;
        private int i;
        private int sec;
        

        HubConnection _connection = null;
        bool isConnected = false;
        string connectionStatus = "Closed";
        string url = "https://shikkhanobishrealtimeapi.shikkhanobish.com/ShikkhanobishHub", msgFromApi = "";
        public CallingPageStudent ( TransferInfo info )
        {
            sec = 0;
            InitializeComponent ();
            Info = info;
            ConnectToServer ();
            tnLbl.Text = Info.Teacher.TeacherName;
            clLbl.Text = Info.Class;
            subLbl.Text = Info.SubjectName;
            ctLbl.Text = "Cost: " + Info.Teacher.Amount + " taka/min";
            calllbl.Text = "Calling teacher...";
            GetKeys ();
            checkSession ();
            Device.StartTimer ( TimeSpan.FromSeconds ( 1.0 ) , startCountdown );


        }
        private bool startCountdown ( )
        {
            sec++;
            if ( sec > 15 )
            {
                callOut ();
                return false;
            }
            return true;
        }
        public async Task callOut ( )
        {
            CutVideoCAll ();
            await Application.Current.MainPage.Navigation.PopModalAsync ();
            CrossOpenTok.Current.EndSession ();
            _connection.StopAsync ();
        }
        protected override bool OnBackButtonPressed ( )
        {
            callOut ();
            return true;
        }
        public async void checkSession ( )
        {
            if ( !CrossOpenTok.Current.TryStartSession () )
            {
                return;
            }
            await ConnectToServer ().ConfigureAwait ( false );
            await ConnectWithTeacher ( SessionID , Token , Info.Student.StudentID , Info.Teacher.TeacherID , Info.SubjectName , Info.Class , Info.Teacher.Amount , Info.Teacher.TeacherName ).ConfigureAwait ( false );

        }

        public async Task ConnectWithTeacher ( string SessionId , string UserToken , int studentID , int teacherID , string Cls , string subject , double cost , string studentName )
        {
            string url = "https://shikkhanobishrealtimeapi.shikkhanobish.com/api/ShikkhanobishRealTimeApi/CallTeacher?&SessionId=" + SessionId + "&UserToken=" + UserToken + "&studentID=" + studentID + "&teacherID=" + teacherID + "&Cls=" + Cls + "&subject=" + subject + "&cost=" + cost + "&studentName=" + studentName;
            HttpClient client = new HttpClient ();
            StringContent content = new StringContent ( "" , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( true );
            string result = await response.Content.ReadAsStringAsync ().ConfigureAwait ( true );
        }
        protected async void GetKeys ( )
        {

            using ( var client = new HttpClient () )
            {
                try
                {
                    apiKey = 46485492;
                    string apiSecreat = "c255c95670bc11eecaf5950baf375d7478f74665";
                    OpenTok opentok = new OpenTok ( apiKey , apiSecreat );
                    opentok.SetDefaultRequestTimeout ( 15 );
                    var session = opentok.CreateSession ();
                    SessionID = session.Id;
                    Token = opentok.GenerateToken ( SessionID );
                    CrossOpenTok.Current.ApiKey = "" + apiKey;
                    CrossOpenTok.Current.UserToken = Token;
                }
                catch ( Exception ex )
                {

                }
            }

        }


        private async void cancleStbtn_Clicked ( object sender , EventArgs e )
        {
            callOut ();
        }
        public async Task CutVideoCAll ( )
        {

            string url = "https://shikkhanobishrealtimeapi.shikkhanobish.com/api/ShikkhanobishRealTimeApi/cutCall?stop=" + 1 + "&teacherID=" + Info.Teacher.TeacherID + "&studentID=" + Info.Student.StudentID + "&isStudent=" + true;
            HttpClient client = new HttpClient ();
            StringContent content = new StringContent ( "" , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( true );
            string result = await response.Content.ReadAsStringAsync ().ConfigureAwait ( true );
            var r = JsonConvert.DeserializeObject<string> ( result );
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
            _connection.On<int , int , bool> ( "SendStudentThatCallRecivedOrIgnored" , async ( studentID , teacherID , recivedOrNot ) =>
            {
                i++;
                if ( i == 1 )
                {
                    if ( Info.Teacher.TeacherID == teacherID && isstudent == false )
                    {
                        if ( recivedOrNot == true )
                        {
                            await Application.Current.MainPage.Navigation.PushModalAsync ( new TutionPage ( Info ) ).ConfigureAwait ( false );
                        }
                        else
                        {
                            callOut ();
                        }
                    }
                }


            } );
        }
    }
}