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
using Rg.Plugins.Popup.Extensions;
using Shikkhanobish.ContentPages.Common;

namespace Shikkhanobish.ContentPages
{
    [XamlCompilation ( XamlCompilationOptions.Compile )]
    public partial class CallingPageStudent : ContentPage
    {
        private TransferInfo Info;
        private bool isstudent;
        private int i;
        private int sec;
        public Session Session { get; protected set; }
        public OpenTok OpenTok { get; protected set; }
        bool isCallCut;
        HubConnection _connection = null;
        bool isAcceptedByTeacher = false;
        string note;
        string url = "https://shikkhanobishrealtimeapi.shikkhanobish.com/ShikkhanobishHub";
        public CallingPageStudent ( TransferInfo info, string shortNote )
        {
            InitializeComponent();
            searchdontcount = 0;
            note = shortNote;
            isCallCut = false;
            sec = 0;
            Info = info;
            ConnectToServer();
            tnLbl.Text = Info.Teacher.TeacherName;
            clLbl.Text = Info.Class;
            subLbl.Text = Info.SubjectName;
            ctLbl.Text = "Cost: " + Info.Teacher.Amount + " taka/min";
            shortnotelbl.Text = shortNote;
            // StaticPageForGeneralUse.TappedTeacherIdForTuition = info.Teacher.TeacherID;
            Device.StartTimer(TimeSpan.FromSeconds(1.0), startCountdown);
        }
        int searchdontcount;
        private bool isConnected;
        private string connectionStatus;

        private bool startCountdown ( )
        {
            if (isCallCut == true)
            {
                return false;
            }
            if (sec > 30)
            {
                if (isAcceptedByTeacher == false)
                {
                    _connection.StopAsync();
                    setOnTuitionOFF();
                    setIsActiveOffOrOn();
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        calllbl.Text = "Cancle Calling...";
                    });
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await Application.Current.MainPage.Navigation.PopModalAsync().ConfigureAwait(false);
                    });
                    return false;
                }

            }
           
            searchdontcount++;
            if (searchdontcount == 1)
            {
                calllbl.Text = "Connection with teacher.";
            }
            else if (searchdontcount == 2)
            {
                calllbl.Text = "Connection with Teacher..";
            }
            else if (searchdontcount == 3)
            {
                searchdontcount = 0;
                calllbl.Text = "Connection with Teacher...";
            }
            sec++;
           
            return true;
        }
        public async void setOnTuitionOFF ( )
        {
          
            string urlT = "https://api.shikkhanobish.com/api/Master/ChangeStateofIsOnTuition";
            HttpClient clientT = new HttpClient ();
            string jsonDataT = JsonConvert.SerializeObject ( new { TeacherID = Info.Teacher.TeacherID , state = 1 } );
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
           
            
        }
        protected override bool OnBackButtonPressed ( )
        {
            isCallCut = true;            
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                calllbl.Text = "Cancle Calling...";
            });
            CutVideoCAllForTeacher();
            _connection.StopAsync();
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Application.Current.MainPage.Navigation.PopModalAsync().ConfigureAwait(false);
            });
            return true;
        }

        public async Task ConnectWithTeacher ( string SessionId , string UserToken , int studentID , int teacherID , string Cls , string subject , double cost , string studentName )
        {
            string url = "https://shikkhanobishrealtimeapi.shikkhanobish.com/api/ShikkhanobishRealTimeApi/CallTeacher?&SessionId=" + SessionId + "&UserToken=" + UserToken + "&studentID=" + studentID + "&teacherID=" + teacherID + "&Cls=" + Cls + "&subject=" + subject + "&cost=" + cost + "&studentName=" + studentName;
            HttpClient client = new HttpClient ();
            StringContent content = new StringContent ( "" , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( true );
            string result = await response.Content.ReadAsStringAsync ().ConfigureAwait ( true );
        }       


        private void cancleStbtn_Clicked ( object sender , EventArgs e )
        {
            isCallCut = true;          
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                calllbl.Text = "Cancle Calling...";
            });
            CutVideoCAllForTeacher();
            _connection.StopAsync();
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Application.Current.MainPage.Navigation.PopModalAsync().ConfigureAwait(false);
            });
        }
        public async Task CutVideoCAllForTeacher ( )
        {

            string url = "https://shikkhanobishrealtimeapi.shikkhanobish.com/api/ShikkhanobishRealTimeApi/cutCall?stop=" + 1 + "&teacherID=" + Info.Teacher.TeacherID + "&studentID=" + Info.Student.StudentID + "&isStudent=" + true;
            HttpClient client = new HttpClient ();
            StringContent content = new StringContent ( "" , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( true );
           // string result = await response.Content.ReadAsStringAsync ().ConfigureAwait ( true );
            //var r = JsonConvert.DeserializeObject<string> ( result );
        }
        public async Task ConnectToServer ( )
        {
            _connection = new HubConnectionBuilder()
                 .WithUrl(url)
                 .Build();

            await _connection.StartAsync();
            isConnected = true;
            connectionStatus = "Connected";

            _connection.Closed += async (s) =>
            {
                isConnected = false;
                connectionStatus = "Disconnected";
                await _connection.StartAsync();
                isConnected = true;

            };

            _connection.On<int , int , bool> ( "SendStudentThatCallRecivedOrIgnored" , async ( studentID , teacherID , recivedOrNot ) =>
            {
                if (Info.Teacher.TeacherID == teacherID && Info.Student.StudentID == studentID)
                {
                    if (recivedOrNot == true)
                    {
                        isAcceptedByTeacher = true;
                        string urlT = "https://api.shikkhanobish.com/api/Master/GetInfoByStudentID ";
                        HttpClient clientT = new HttpClient();
                        string jsonDataT = JsonConvert.SerializeObject(new { StudentID = Info.Student.StudentID });
                        StringContent contentT = new StringContent(jsonDataT, Encoding.UTF8, "application/json");
                        HttpResponseMessage responseT = await clientT.PostAsync(urlT, contentT).ConfigureAwait(false);
                        string resultT = await responseT.Content.ReadAsStringAsync();
                        var studentClass = JsonConvert.DeserializeObject<StudentClass>(resultT);

                        OldStToNewSt cn = new OldStToNewSt();
                        
                        var student = cn.Sc_TO_S(studentClass);
                        Info.Student = student;
                        MainThread.BeginInvokeOnMainThread(() => { Application.Current.MainPage.Navigation.PushModalAsync(new TutionPage(Info)).ConfigureAwait(false); });

                    }
                    else
                    {
                        isCallCut = true;
                        isAcceptedByTeacher = true;
                        await Application.Current.MainPage.Navigation.PopModalAsync();
                        _connection.StopAsync();
                    }
                }


            } );
        }
    }
}