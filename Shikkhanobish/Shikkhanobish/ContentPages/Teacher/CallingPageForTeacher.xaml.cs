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
            GetKeys ();
        }

        private async void callbtn_Clicked ( object sender , EventArgs e )
        {
            if ( !CrossOpenTok.Current.TryStartSession () )
            {
                return;
            }
            setOnTuitionOFFOrOn(1);
            ConnectWithStudent ( Info.Student.StudentID , Info.Teacher.TeacherID , true );
            await Application.Current.MainPage.Navigation.PushModalAsync ( new TuitionPageTeacher ( Info ) ).ConfigureAwait ( false );
        }
        protected  override bool OnBackButtonPressed ( )
        {
            setOnTuitionOFFOrOn (0);
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
            setOnTuitionOFFOrOn(0);
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

        protected async void GetKeys ( )
        {

            apiKey = 46485492;
            SessionID = "2_MX40NjQ4NTQ5Mn5-MTYwMTIwNTc0NzY4OX5USmhraGhKNzduREtjamtsRjdOZWg2dnV-fg";
            Token = "T1==cGFydG5lcl9pZD00NjQ4NTQ5MiZzaWc9ODZmMzg4ZGU1YTljNGIzMTVmZTRkZDA1ZDAwYzE5ZjhmNjc2OWU1MTpzZXNzaW9uX2lkPTJfTVg0ME5qUTROVFE1TW41LU1UWXdNVEl3TlRjME56WTRPWDVVU21ocmFHaEtOemR1UkV0amFtdHNSamRPWldnMmRuVi1mZyZjcmVhdGVfdGltZT0xNjAxMjA1Nzg1Jm5vbmNlPTAuOTcxNzE2ODQ3NjU1NTQ4NCZyb2xlPXB1Ymxpc2hlciZleHBpcmVfdGltZT0xNjAzNzk3Nzg1JmluaXRpYWxfbGF5b3V0X2NsYXNzX2xpc3Q9";
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

        public async void setOnTuitionOFFOrOn (int oforon )
        {
            string urlT = "https://api.shikkhanobish.com/api/Master/ChangeStateofIsOnTuition";
            HttpClient clientT = new HttpClient ();
            string jsonDataT = JsonConvert.SerializeObject ( new { TeacherID = Info.Teacher.TeacherID , state = oforon } );
            StringContent contentT = new StringContent ( jsonDataT , Encoding.UTF8 , "application/json" );
            HttpResponseMessage responseT = await clientT.PostAsync ( urlT , contentT ).ConfigureAwait ( false );
            string resultT = await responseT.Content.ReadAsStringAsync ();
            var response = JsonConvert.DeserializeObject<Response> ( resultT );
        }
        
    }
}