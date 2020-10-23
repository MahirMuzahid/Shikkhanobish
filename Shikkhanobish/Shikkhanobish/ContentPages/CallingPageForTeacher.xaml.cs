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
using Xamarin.Essentials;
using Shikkhanobish.Interface;

namespace Shikkhanobish.ContentPages
{
    [XamlCompilation ( XamlCompilationOptions.Compile )]
    public partial class CallingPageForTeacher : ContentPage
    {
        private TransferInfo Info;
        private int apiKey;
        private string SessionID, Token;
        private bool isstudent;
        private int fromWhere;
        public CallingPageForTeacher ( TransferInfo info , int fw, string sn)
        {           
            InitializeComponent ();
            shortnotelbl.Text = "Note: "+sn;
            fromWhere = fw;
            isCallCut = false;
            Info = info;
            tnLbl.Text = Info.Student.Name;
            clLbl.Text = Info.Class;
            subLbl.Text = Info.SubjectName;
            ctLbl.Text = "Cost: " + Info.Teacher.Amount + " taka/min";
            calllbl.Text = "Student Call...";
            ConnectToServer();
            Device.StartTimer(TimeSpan.FromSeconds(1.0), startCountdown);
        }
        int sec = 0;
        bool isCallCut;
        private bool startCountdown()
        {
            if (isCallCut == true)
            {
                return false;
            }
            sec++;
            if (sec > 25)
            {
                popPage();
                return false;                
            }           
            else
            {
                return true;
            }
            
        }
        private async void callbtn_Clicked ( object sender , EventArgs e )
        {
            CrossOpenTok.Current.ApiKey = "46485492";
            CrossOpenTok.Current.SessionId = Info.SessionID;
            CrossOpenTok.Current.UserToken = Info.UserToken;
            if (!CrossOpenTok.Current.TryStartSession())
            {
                return;
            }
            setOnTuitionOFFOrOn ( 1 );
            ConnectWithStudent ( Info.Student.StudentID , Info.Teacher.TeacherID , true );
            isCallCut = true;
            await Application.Current.MainPage.Navigation.PushModalAsync ( new TuitionPageTeacher ( Info ) ).ConfigureAwait ( false );
        }
        protected override bool OnBackButtonPressed ( )
        {
            popPage ();
            //End Call session
            return true;
        }
        public async void popPage ( )
        {
            isCallCut = true;
            StaticPageForOnSleep.isCallPending = false;
            setOnTuitionOFFOrOn(0);
            ConnectWithStudent(Info.Student.StudentID, Info.Teacher.TeacherID, false);
            _connection.StopAsync();
            if (fromWhere == 0)
            {
                await Application.Current.MainPage.Navigation.PopModalAsync();
            }
            else
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new TeacherProfile(Info.Teacher)).ConfigureAwait(false);
            }
            
        }
        //for teacher
        private async void canclebtn_Clicked ( object sender , EventArgs e )
        {
            popPage();
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
        public async void setIsActiveOffOrOn(int state)
        {
            string urlT = "https://api.shikkhanobish.com/api/Master/ChangeStateofIsActive";
            HttpClient clientT = new HttpClient();
            string jsonDataT = JsonConvert.SerializeObject(new { TeacherID = Info.Teacher.TeacherID, state = 0 });
            StringContent contentT = new StringContent(jsonDataT, Encoding.UTF8, "application/json");
            HttpResponseMessage responseT = await clientT.PostAsync(urlT, contentT).ConfigureAwait(false);
            string resultT = await responseT.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<Response>(resultT);
        }

        HubConnection _connection = null;
        string url = "https://shikkhanobishrealtimeapi.shikkhanobish.com/ShikkhanobishHub";
        int cutCallFirstTime = 0;
        public async Task ConnectToServer ( )
        {

            _connection = new HubConnectionBuilder ()
                .WithUrl ( url )
                .Build ();
            await _connection.StartAsync ();
            _connection.On<int , int , int , bool> ( "cutCall" , async ( stop , teacherID , studentID , isStudent ) =>
            {
                cutCallFirstTime++;
                if ( isStudent == true && cutCallFirstTime == 1 && teacherID == Info.Teacher.TeacherID )
                {
                    isCallCut = true;
                    StaticPageForOnSleep.isCallPending = false;
                    setOnTuitionOFFOrOn ( 0 );                   
                    if (fromWhere == 0)
                    {
                        await Application.Current.MainPage.Navigation.PopModalAsync();
                    }
                    else
                    {
                        await Application.Current.MainPage.Navigation.PushModalAsync(new TeacherProfile(Info.Teacher)).ConfigureAwait(false);
                    }
                }

            } );
        }

        

    }
}