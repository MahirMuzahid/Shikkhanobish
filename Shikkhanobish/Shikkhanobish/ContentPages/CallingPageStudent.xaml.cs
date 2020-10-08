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
using System.Net;
using Xamarin.Essentials;

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
        VideoCAllApiInfo api = new VideoCAllApiInfo();
        public Session Session { get; protected set; }
        public OpenTok OpenTok { get; protected set; }
        bool isCallCutByStudent;
        HubConnection _connection = null;
        bool isConnected = false;
        string connectionStatus = "Closed";
        bool isAcceptedByTeacher = false;
        string url = "https://shikkhanobishrealtimeapi.shikkhanobish.com/ShikkhanobishHub", msgFromApi = "";
        public CallingPageStudent ( TransferInfo info )
        {
            isCallCutByStudent = false;
            sec = 0;
            InitializeComponent ();
            Info = info;
            ConnectToServer ();
            tnLbl.Text = Info.Teacher.TeacherName;
            clLbl.Text = Info.Class;
            subLbl.Text = Info.SubjectName;
            ctLbl.Text = "Cost: " + Info.Teacher.Amount + " taka/min";
            calllbl.Text = "Calling teacher...";
            CrossOpenTok.Current.ApiKey = "" + 46485492;
            CrossOpenTok.Current.UserToken = api.Token ;
            CrossOpenTok.Current.SessionId = api.Session.Id;
            checkSession ();
            Device.StartTimer ( TimeSpan.FromSeconds ( 1.0 ) , startCountdown );

        }
        private bool startCountdown ( )
        {
            sec++;
            if ( sec > 15 )
            {
                if(isAcceptedByTeacher == false)
                {
                    setOnTuitionOFF ();
                    setIsActiveOffOrOn ();
                    callOut ();
                    return false;
                }
               
            }
            if( isCallCutByStudent == true)
            {
                return false;
            }
            return true;
        }
        public async void setOnTuitionOFF ( )
        {
            string urlT = "https://api.shikkhanobish.com/api/Master/ChangeStateofIsOnTuition";
            HttpClient clientT = new HttpClient ();
            string jsonDataT = JsonConvert.SerializeObject ( new { TeacherID = Info.Teacher.TeacherID , state = 0 } );
            StringContent contentT = new StringContent ( jsonDataT , Encoding.UTF8 , "application/json" );
            HttpResponseMessage responseT = await clientT.PostAsync ( urlT , contentT ).ConfigureAwait ( false );
            string resultT = await responseT.Content.ReadAsStringAsync ();
            var response = JsonConvert.DeserializeObject<Response> ( resultT );
        }

        public async void setIsActiveOffOrOn ( )
        {
            string urlT = "https://api.shikkhanobish.com/api/Master/ChangeStateofIsActive";
            HttpClient clientT = new HttpClient ();
            string jsonDataT = JsonConvert.SerializeObject ( new { TeacherID = Info.Teacher.TeacherID , state = 0 } );
            StringContent contentT = new StringContent ( jsonDataT , Encoding.UTF8 , "application/json" );
            HttpResponseMessage responseT = await clientT.PostAsync ( urlT , contentT ).ConfigureAwait ( false );
            string resultT = await responseT.Content.ReadAsStringAsync ();
            var response = JsonConvert.DeserializeObject<Response> ( resultT );
        }
        public async Task callOut ( )
        {
            CutVideoCAllForTeacher ();            
            CrossOpenTok.Current.EndSession ();
            _connection.StopAsync ();
            await Application.Current.MainPage.Navigation.PopModalAsync ();
        }
        protected override bool OnBackButtonPressed ( )
        {
            isCallCutByStudent = true;
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
            await ConnectWithTeacher ( api.Session.Id , api.Token , Info.Student.StudentID , Info.Teacher.TeacherID , Info.SubjectName , Info.Class , Info.Teacher.Amount , Info.Teacher.TeacherName ).ConfigureAwait ( false );

        }

        public async Task ConnectWithTeacher ( string SessionId , string UserToken , int studentID , int teacherID , string Cls , string subject , double cost , string studentName )
        {
            string url = "https://shikkhanobishrealtimeapi.shikkhanobish.com/api/ShikkhanobishRealTimeApi/CallTeacher?&SessionId=" + SessionId + "&UserToken=" + UserToken + "&studentID=" + studentID + "&teacherID=" + teacherID + "&Cls=" + Cls + "&subject=" + subject + "&cost=" + cost + "&studentName=" + studentName;
            HttpClient client = new HttpClient ();
            StringContent content = new StringContent ( "" , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( true );
            string result = await response.Content.ReadAsStringAsync ().ConfigureAwait ( true );
        }       


        private async void cancleStbtn_Clicked ( object sender , EventArgs e )
        {
            isCallCutByStudent = true;
            callOut ();
        }
        public async Task CutVideoCAllForTeacher ( )
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
           
            _connection.On<int , int , bool> ( "SendStudentThatCallRecivedOrIgnored" , async ( studentID , teacherID , recivedOrNot ) =>
            {
                i++;
                if ( i == 1 )
                {
                    if ( Info.Teacher.TeacherID == teacherID && isstudent == false )
                    {
                        if ( recivedOrNot == true )
                        {
                            isAcceptedByTeacher = true;
                            string urlT = "https://api.shikkhanobish.com/api/Master/GetInfoByStudentID ";
                            HttpClient clientT = new HttpClient ();
                            string jsonDataT = JsonConvert.SerializeObject ( new { StudentID = Info.Student.StudentID } );
                            StringContent contentT = new StringContent ( jsonDataT , Encoding.UTF8 , "application/json" );
                            HttpResponseMessage responseT = await clientT.PostAsync ( urlT , contentT ).ConfigureAwait ( false );
                            string resultT = await responseT.Content.ReadAsStringAsync ();
                            var studentClass = JsonConvert.DeserializeObject<StudentClass> ( resultT );

                            OldStToNewSt cn = new OldStToNewSt ();

                            var student = cn.Sc_TO_S ( studentClass );
                            Info.Student = student;
                            MainThread.BeginInvokeOnMainThread ( ( ) => {  Application.Current.MainPage.Navigation.PushModalAsync ( new TutionPage ( Info ) ).ConfigureAwait ( false ); } );
                            
                        }
                        else
                        {
                            await Application.Current.MainPage.Navigation.PopModalAsync ();
                            CrossOpenTok.Current.EndSession ();
                            _connection.StopAsync ();
                        }
                    }
                }


            } );
        }
    }
}