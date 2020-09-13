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
        public CallingPageStudent ( TransferInfo info )
        {
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


        }

        public async void checkSession ( )
        {
            if ( !CrossOpenTok.Current.TryStartSession () )
            {
                return;
            }
            await ConnectToServer ().ConfigureAwait ( false );
            await ConnectWithTeacher ( SessionID , Token , Info.Student.StundentID , Info.Teacher.TeacherID , Info.SubjectName , Info.Class , Info.Teacher.Amount , Info.Teacher.TeacherName ) .ConfigureAwait ( false );    
            
        }

        public async Task ConnectWithTeacher ( string SessionId , string UserToken , int studentID , int teacherID , string Cls , string subject , double cost , string studentName )
        {
            string url = "https://shikkhanobishrealtimeapi.shikkhanobish.com/api/ShikkhanobishRealTimeApi/CallTeacher?&SessionId=" + SessionId + "&UserToken=" + UserToken + "&studentID=" + studentID + "&teacherID=" + teacherID + "&Cls=" + Cls + "&subject=" + subject + "&cost=" + cost + "&studentName=" + studentName;
            HttpClient client = new HttpClient ();
            StringContent content = new StringContent ( "" , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( true );
            string result = await response.Content.ReadAsStringAsync ().ConfigureAwait ( true );
           // var r = JsonConvert.DeserializeObject<string> ( result );
        }
        protected async void GetKeys ()
        {

            apiKey = 46485492;
            SessionID = "2_MX40NjQ4NTQ5Mn5-MTU5ODc2MDk4MTU2M35vTDBMZjU0c21BcGhtNTE2Ylp1cllSS1F-fg";
            Token = "T1==cGFydG5lcl9pZD00NjQ4NTQ5MiZzaWc9NTRmZjBhYzFkZGQ2MmZkZGJhZjI4NWY5NmE1Y2E2MzQ0OTVhZTMxMjpzZXNzaW9uX2lkPTJfTVg0ME5qUTROVFE1TW41LU1UVTVPRGMyTURrNE1UVTJNMzV2VERCTVpqVTBjMjFCY0dodE5URTJZbHAxY2xsU1MxRi1mZyZjcmVhdGVfdGltZT0xNTk4NzYwOTkwJm5vbmNlPTAuODYzMzk2NTI0NTQxNzAxNyZyb2xlPXB1Ymxpc2hlciZleHBpcmVfdGltZT0xNjAxMzUyOTg4JmluaXRpYWxfbGF5b3V0X2NsYXNzX2xpc3Q9";
            using ( var client = new HttpClient () )
            {
                try
                {
                    //var resp = await client.GetAsync(Config.KeysUrl);
                    //var json = await resp.Content.ReadAsStringAsync();
                    //var keys = JsonConvert.DeserializeObject<Keys>(json);

                    CrossOpenTok.Current.ApiKey = "46485492";// keys.ApiKey;
                    CrossOpenTok.Current.SessionId = "2_MX40NjQ4NTQ5Mn5-MTU5ODc2MDk4MTU2M35vTDBMZjU0c21BcGhtNTE2Ylp1cllSS1F-fg";//keys.SessionId;
                    CrossOpenTok.Current.UserToken = "T1==cGFydG5lcl9pZD00NjQ4NTQ5MiZzaWc9NTRmZjBhYzFkZGQ2MmZkZGJhZjI4NWY5NmE1Y2E2MzQ0OTVhZTMxMjpzZXNzaW9uX2lkPTJfTVg0ME5qUTROVFE1TW41LU1UVTVPRGMyTURrNE1UVTJNMzV2VERCTVpqVTBjMjFCY0dodE5URTJZbHAxY2xsU1MxRi1mZyZjcmVhdGVfdGltZT0xNTk4NzYwOTkwJm5vbmNlPTAuODYzMzk2NTI0NTQxNzAxNyZyb2xlPXB1Ymxpc2hlciZleHBpcmVfdGltZT0xNjAxMzUyOTg4JmluaXRpYWxfbGF5b3V0X2NsYXNzX2xpc3Q9";//keys.Token;
                }
                catch ( Exception ex )
                {
                    // await MainPage.DisplayAlert(null, "MAKE SURE YOU SET API URL FOR RETRIEVING NECESSARY KEYS (Config.cs) OR YOU MAY HARDCODE THEM.", "GOT IT");
                }
            }
            //CrossOpenTok.Current.Error += (m) => TakeTuition.DisplayAlert("ERROR", m, "OK");
        }


        //for teacher
        
        private async void cancleStbtn_Clicked ( object sender , EventArgs e )
        {
            await Application.Current.MainPage.Navigation.PopModalAsync ();
        }

        //for student

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
            _connection.On<int , int , bool> ( "SendStudentThatCallRecivedOrIgnored" , async ( studentID , teacherID , recivedOrNot ) =>
            {
                i++;
                if(i == 1)
                {
                    if ( Info.Teacher.TeacherID == teacherID && isstudent == false )
                    {
                        if ( recivedOrNot == true )
                        {
                            await Application.Current.MainPage.Navigation.PushModalAsync ( new TutionPage ( Info ) ).ConfigureAwait ( false );
                        }
                        else
                        {
                            await Application.Current.MainPage.Navigation.PopModalAsync ();
                        }
                    }
                }
                
                
            } );
        }
    }
}