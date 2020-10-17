using Shikkhanobish.ViewModel;
using System;
using Xamarin.Forms;
using System.Timers;
using Xamarin.Forms.OpenTok.Service;
using Xamarin.Forms.Xaml;
using Java.Lang;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Shikkhanobish.Model;
using Shikkhanobish.ContentPages.Common;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Essentials;

namespace Shikkhanobish.ContentPages
{
    [XamlCompilation ( XamlCompilationOptions.Compile )]
    public partial class TutionPage : ContentPage
    {
        private bool iscut;
        private TransferInfo info = new TransferInfo ();
        int sec, min;
        int ownthing = 0;
        bool firstTime , isNewTeacher;
        float totalCost;
        float costPerMinInThisTuition, TotalCostInThisTuitionForStudent, costPerMinInThisTuitionForTeacher;
        Calculate calculate = new Calculate();
        HubConnection _connection = null;
        bool isConnected = false;
        string connectionStatus = "Closed";
        string url = "https://shikkhanobishrealtimeapi.shikkhanobish.com/ShikkhanobishHub";
        int cutCallFirstTime;
        bool cutThisCallNow;
        int TeacherPlaceMentTime = 15;
        public TutionPage ( TransferInfo trnsInfo)
        {
            isNewTeacher = false;
            cutThisCallNow = false;
            iscut = false;
            cutCallFirstTime = 0;
            InitializeComponent ();
            info = trnsInfo;
            sec = 0;
            min = 0;
            firstTime = true;
            safelbl.Text = "Safe Time";
            tnamelbl.Text = trnsInfo.Teacher.TeacherName;
            SetIsPending ();
            //SetSubject ();
            SecureStorage.SetAsync ( "subject_name" , info.SubjectName );
            ConnectToServer ();
            costPerMinInThisTuition = calculate.CalculateCost(info);
           
            Device.StartTimer ( TimeSpan.FromSeconds ( 1.0 ) , UpdateTimerAndInfo );                      
        }
        /// <summary>
        /// Noraml Function
        /// </summary>
        public async void SetSubject ()
        {
            await SecureStorage.SetAsync ( "subject_name" , info.SubjectName ).ConfigureAwait ( false );
        }
        private async void OnEndCall ( object sender , EventArgs e )
        {
            EndCall();
        }
        public async void EndCall ()
        {
            info.StudentCost = calculate.CalculateCost(info);
            iscut = true;
            CrossOpenTok.Current.EndSession();
            _connection.StopAsync();
            await CutVideoCAll().ConfigureAwait(false);
            gotoRatingPage();
        }
        private void OnSwapCamera ( object sender , EventArgs e )
        {
            CrossOpenTok.Current.CycleCamera ();
        }
        private void StopOwnVideo ( object sender , EventArgs e )
        {
            ownthing++;

            if(ownthing%2 == 1)
            {
                ownvideo.IsVisible = false;

            }
            else
            {
                ownvideo.IsVisible = true;

            }
            
        }
        protected override bool OnBackButtonPressed ( )
        {
            Navigation.PushPopupAsync ( new PopUpForTextAlert ( "Do You want to cut the call?" , "If you want to cut the call, press cut video icon" , false ) );
            return true;
        }
        public void gotoRatingPage ( )
        {
            Device.BeginInvokeOnMainThread ( async ( ) =>
            {
                await Application.Current.MainPage.Navigation.PushModalAsync ( new RatingPage ( info , true ) ).ConfigureAwait ( false );
            } );
        }





        /// <summary>
        /// Every Second Call
        /// </summary>
        private bool UpdateTimerAndInfo()
        {
            sec = sec + 1;
            if (sec == 60)
            {
                min = min + 1;
                sec = 0;
            }
            info.StudyTimeInAPp = min;
            if(sec == 15)
            {
                StartTime();
                if(sec == 31 && firstTime == true)
                {
                    firstTime = false;
                }
                
            }
            if (sec == 31 && firstTime == false)
            {         
                if (cutThisCallNow)
                {
                    EndCall();
                }
                else
                {
                    //Student
                    if (IsCostCountAvailableForStudent())
                    {
                        var cost = CalculateTotalCostStudent();
                        safelbl.Text = "Cost: " + cost;
                        SetCost(0);
                    }
                    else
                    {
                        SetCost(1);
                    }
                    //Teacher
                    if (IsCostCountAvailableForTeacher())
                    {
                        CalculateTotalCostTeacher();
                        SendCostRoTeacher(costPerMinInThisTuitionForTeacher);
                    }
                    else
                    {
                        SendCostRoTeacher(0);
                    }

                }                            
                IsLastMinLimit();
            }                      
            timerlbl.Text = min + ":" + sec;
            if( iscut == false)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }





        /// <summary>
        /// SignalR Function
        /// </summary>
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
            _connection.On<int , int , int , bool> ( "cutCall" , ( stop , teacherID , studentID , isStudent ) =>
            {
                cutCallFirstTime++;
                if ( isStudent == false  && cutCallFirstTime == 1)
                {
                    if ( info.Student.StudentID == studentID )
                    {
                        CrossOpenTok.Current.EndSession ();
                        _connection.StopAsync ();
                        gotoRatingPage ();
                    }
                }

            } );
        }





        /// <summary>
        /// All Usend Indivual Function In this page
        /// </summary>

        public async void SetIsPending ( )
        {
            string url = "https://api.shikkhanobish.com/api/Master/SetPending";
            HttpClient client = new HttpClient ();
            string jsonData = JsonConvert.SerializeObject ( new { StudentID = info.Student.StudentID , TeacherName = info.Teacher.TeacherName , TeacherID = info.Teacher.TeacherID , Class = info.Class , Subject = info.Subject ,Time = 0, Cost = 0 } );
            StringContent content = new StringContent ( jsonData , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( false );
            string result = await response.Content.ReadAsStringAsync ();
        }
        float previouscostst, previousearnteach;
        public async void SetCost ( int FreeMinMinus)
        {
            string url = "https://api.shikkhanobish.com/api/Master/SetCostinIspending";
            HttpClient client = new HttpClient ();
            string jsonData = JsonConvert.SerializeObject ( new { StudentID = info.Student.StudentID ,  Cost = TotalCostInThisTuitionForStudent, Time = info.StudyTimeInAPp , StudentCostMin  = costPerMinInThisTuition , TeacherEarnMin = costPerMinInThisTuitionForTeacher, TeacherID = info.Teacher.TeacherID, FreeMinMinus = FreeMinMinus } );
            StringContent content = new StringContent ( jsonData , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( false );
            string result = await response.Content.ReadAsStringAsync ();
        }
        public async Task CutVideoCAll ( )
        {

            string url = "https://shikkhanobishrealtimeapi.shikkhanobish.com/api/ShikkhanobishRealTimeApi/cutCall?stop=" + 1 + "&teacherID=" + info.Teacher.TeacherID + "&studentID=" + info.Student.StudentID + "&isStudent=true";
            HttpClient client = new HttpClient ();
            StringContent content = new StringContent ( "" , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( true );
            string result = await response.Content.ReadAsStringAsync ().ConfigureAwait ( true );
            
        }
        public async Task SendCostRoTeacher ( float cost)
        {

            string url = "https://shikkhanobishrealtimeapi.shikkhanobish.com/api/ShikkhanobishRealTimeApi/sendCost?cost=" + cost + "&teacherID=" + info.Teacher.TeacherID + "&studentID=" + info.Student.StudentID;
            HttpClient client = new HttpClient ();
            StringContent content = new StringContent ( "" , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( true );
            string result = await response.Content.ReadAsStringAsync ().ConfigureAwait ( true );
            var r = JsonConvert.DeserializeObject<string> ( result );
        }

        public async Task StartTime ( )
        {
            string url = "https://shikkhanobishrealtimeapi.shikkhanobish.com/api/ShikkhanobishRealTimeApi/sendTime?teacherID="+ info.Teacher.TeacherID;
            HttpClient client = new HttpClient ();
            StringContent content = new StringContent ( "" , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( true );
            string result = await response.Content.ReadAsStringAsync ().ConfigureAwait ( true );
            var r = JsonConvert.DeserializeObject<string> ( result );
        }
        public bool IsCostCountAvailableForStudent()
        {
            if(info.StudyTimeInAPp - info.Student.freeMin <= 0 )
            {
                return false;
            }
            else
            {
                return true;
            }
        }
       
        public float CalculateTotalCostStudent()
        {
            TotalCostInThisTuitionForStudent = TotalCostInThisTuitionForStudent + costPerMinInThisTuition;

            return TotalCostInThisTuitionForStudent;
        }

        public bool IsCostCountAvailableForTeacher()
        {
            if (info.Teacher.Total_Min+ info.StudyTimeInAPp < TeacherPlaceMentTime)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public float CalculateTotalCostTeacher()
        {
            costPerMinInThisTuitionForTeacher = costPerMinInThisTuition * .8f;
            return costPerMinInThisTuitionForTeacher;
        }
        /// <summary>
        /// Common For Teacher And Student
        /// </summary>
        public void IsLastMinLimit()
        {
            if (((TotalCostInThisTuitionForStudent + costPerMinInThisTuition) > info.Student.RechargedAmount) || (info.Student.freeMin - info.StudyTimeInAPp == 1 && info.Student.RechargedAmount < 2))
            {
                //show last min alert
                cutThisCallNow = true;
            }
            else
            {
                cutThisCallNow = false;
            }
        }
    }
}