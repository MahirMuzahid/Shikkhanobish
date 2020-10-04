using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.OpenTok.Service;
using Xamarin.Forms.Xaml;
using Microsoft.AspNetCore.SignalR.Client;
using Shikkhanobish.Model;
using Shikkhanobish.ViewModel;
using Newtonsoft.Json;
using OpenTokSDK;

namespace Shikkhanobish.ContentPages
{
    [XamlCompilation ( XamlCompilationOptions.Compile )]
    public partial class CallingPageForTeacher : ContentPage
    {
        private TransferInfo Info;
        private int apiKey;
        private string SessionID, Token;
        private bool isstudent;
        public CallingPageForTeacher ( TransferInfo info )
        {
            InitializeComponent ();
            Info = info;
            tnLbl.Text = Info.Student.Name;
            clLbl.Text = Info.Class;
            subLbl.Text = Info.SubjectName;
            ctLbl.Text = "Cost: " + Info.Teacher.Amount + " taka/min";
            calllbl.Text = "Student Call...";
            CrossOpenTok.Current.ApiKey = "46485492";
            CrossOpenTok.Current.SessionId = info.SessionID;
            CrossOpenTok.Current.UserToken = info.UserToken;
        }

        private async void callbtn_Clicked ( object sender , EventArgs e )
        {
            if ( !CrossOpenTok.Current.TryStartSession () )
            {
                return;
            }
            setOnTuitionOFFOrOn ( 1 );
            ConnectWithStudent ( Info.Student.StudentID , Info.Teacher.TeacherID , true );
            await Application.Current.MainPage.Navigation.PushModalAsync ( new TuitionPageTeacher ( Info ) ).ConfigureAwait ( false );
        }
        protected override bool OnBackButtonPressed ( )
        {
            setOnTuitionOFFOrOn ( 0 );
            ConnectWithStudent ( Info.Student.StudentID , Info.Teacher.TeacherID , false );
            popPage ();
            //End Call session
            return true;
        }
        public async void popPage ( )
        {
            await Application.Current.MainPage.Navigation.PopModalAsync ();
        }
        //for teacher
        private async void canclebtn_Clicked ( object sender , EventArgs e )
        {
            setOnTuitionOFFOrOn ( 0 );
            ConnectWithStudent ( Info.Student.StudentID , Info.Teacher.TeacherID , false );
            await Application.Current.MainPage.Navigation.PopModalAsync ();
        }

        public async Task ConnectWithStudent ( int studentID , int teacherID , bool recivedOrNot )
        {
            string url = "https://shikkhanobishrealtimeapi.shikkhanobish.com/api/ShikkhanobishRealTimeApi/SendStudentThatCallRecivedOrIgnored?studentID=" + studentID + "&teacherID=" + teacherID + "&recivedOrNot=" + recivedOrNot;
            HttpClient client = new HttpClient ();
            StringContent content = new StringContent ( "" , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( true );
            string result = await response.Content.ReadAsStringAsync ().ConfigureAwait ( true );
            var r = JsonConvert.DeserializeObject<string> ( result );
        }     

        public async void setOnTuitionOFFOrOn ( int oforon )
        {
            string urlT = "https://api.shikkhanobish.com/api/Master/ChangeStateofIsOnTuition";
            HttpClient clientT = new HttpClient ();
            string jsonDataT = JsonConvert.SerializeObject ( new { TeacherID = Info.Teacher.TeacherID , state = oforon } );
            StringContent contentT = new StringContent ( jsonDataT , Encoding.UTF8 , "application/json" );
            HttpResponseMessage responseT = await clientT.PostAsync ( urlT , contentT ).ConfigureAwait ( false );
            string resultT = await responseT.Content.ReadAsStringAsync ();
            var response = JsonConvert.DeserializeObject<Response> ( resultT );
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
                if ( isStudent == true && cutCallFirstTime == 1 && teacherID == Info.Teacher.TeacherID )
                {
                    setOnTuitionOFFOrOn ( 0 );
                    ConnectWithStudent ( Info.Student.StudentID , Info.Teacher.TeacherID , false );
                    await Application.Current.MainPage.Navigation.PopModalAsync ();
                }

            } );
        }

    }
}