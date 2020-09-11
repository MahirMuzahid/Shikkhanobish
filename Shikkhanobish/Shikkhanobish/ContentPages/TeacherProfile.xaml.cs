using Newtonsoft.Json;
using Plugin.Connectivity;
using Rg.Plugins.Popup.Extensions;
using Shikkhanobish.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;
using Shikkhanobish.ContentPages;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Shikkhanobish.ViewModel;

namespace Shikkhanobish
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeacherProfile : MasterDetailPage
    {
        public int StudentID;
        private Student _Student;
        private Teacher teacher;
        private int ac = 0;
        public TeacherProfile(Teacher t)
        {
            InitializeComponent ();
            BindingContext = new ProfileViewModel ( t );
            teacher = t;
            activeback.BackgroundColor = Color.FromHex ( "#A7A7A7" );
            var image = new Image { Source = "BackColor.jpg" };
            activelbl.Text = "Inactive";
            setRankInfo ();
            ConnectToServer ();
        }

        public async void setRankInfo ( )
        {
            float avg = ( teacher.One_Star + teacher.Two_Star + teacher.Three_Star + teacher.Four_Star + teacher.Five_Star ) / 5;
            tplbl.Text = "Tuition Point: " + teacher.Tuition_Point;
            avglbl.Text = "Average: " + avg;
            float place = 0;
            Calculate cal = new Calculate ();
            List<int> tpInfo = new List<int> ();
            List<float> avgInfo = new List<float> ();
            tpInfo = cal.GetTuitionPointInfo ();
            avgInfo = cal.GetAverageInfo ();
            if ( teacher.Tuition_Point < 20 )
            {
                ranklbl.Text = "Placement";
                place = (teacher.Tuition_Point / tpInfo[0])*.166f;            }
            else if ( teacher.Tuition_Point <= 999 || ( teacher.Tuition_Point > 999 && avg < 3.75f ) )
            {
                ranklbl.Text = "Newbie";
                if( teacher.Tuition_Point > 999 && avg < 3.75f )
                {
                    place = .3f;
                    avglbl.TextColor = Color.FromHex ( "F68181" );
                    tplbl.TextColor = Color.FromHex ( "F5C24B" );
                }
                else
                   {
                    float point = ( float ) teacher.Tuition_Point / ( float ) tpInfo [ 1 ];
                    place = ( point * .166f ) + .166f;
                    avglbl.TextColor = Color.FromHex ( "F68181" );
                    tplbl.TextColor = Color.FromHex ( "F68181" );
                }
                ranklbl.TextColor = Color.FromHex ( "F68181" );
                progress.ProgressColor = Color.FromHex ( "F68181" );
            }
            else if ( teacher.Tuition_Point <= 3999 && avg >= 3.750f || ( teacher.Tuition_Point > 3999 && avg < 3.75f ) )
            {
                ranklbl.Text = "Average";
                if ( teacher.Tuition_Point > 3999 && avg < 3.75f )
                {
                    place = .3f;
                    avglbl.TextColor = Color.FromHex ( "F5C24B" );
                    tplbl.TextColor = Color.FromHex ( "8AF077" );
                }
                else
                {
                    float point = ( float ) teacher.Tuition_Point / ( float ) tpInfo [ 1 ];
                    place = ( point * .166f ) + .166f;
                    avglbl.TextColor = Color.FromHex ( "F5C24B" );
                    tplbl.TextColor = Color.FromHex ( "F5C24B" );
                }
                ranklbl.TextColor = Color.FromHex ( "F68181" );
                progress.ProgressColor = Color.FromHex ( "F68181" );
            }
            else if ( teacher.Tuition_Point <= 8999 && avg >= 4.00f || ( teacher.Tuition_Point > 8999 && avg < 4.00f ) )
            {
                ranklbl.Text = "Good";
                if ( teacher.Tuition_Point > 8999 && avg < 4.00f )
                {
                    place = .3f;
                    avglbl.TextColor = Color.FromHex ( "8AF077" );
                    tplbl.TextColor = Color.FromHex ( "77CDF0" );
                }
                else
                {
                    float point = ( float ) teacher.Tuition_Point / ( float ) tpInfo [ 1 ];
                    place = ( point * .166f ) + .166f;
                    avglbl.TextColor = Color.FromHex ( "8AF077" );
                    tplbl.TextColor = Color.FromHex ( "8AF077" );
                }
                ranklbl.TextColor = Color.FromHex ( "8AF077" );
                progress.ProgressColor = Color.FromHex ( "8AF077" );
            }
            else if ( teacher.Tuition_Point <= 15999 && avg >= 4.30f || ( teacher.Tuition_Point > 15999 && avg < 4.30f ) )
            {
                ranklbl.Text = "Veteran";
                if ( teacher.Tuition_Point > 15999 && avg < 4.30f )
                {
                    place = .3f;
                    avglbl.TextColor = Color.FromHex ( "77CDF0" );
                    tplbl.TextColor = Color.FromHex ( "CA6AF1" );
                }
                else
                {
                    float point = ( float ) teacher.Tuition_Point / ( float ) tpInfo [ 1 ];
                    place = ( point * .166f ) + .166f;
                    avglbl.TextColor = Color.FromHex ( "77CDF0" );
                    tplbl.TextColor = Color.FromHex ( "77CDF0" );
                }
                ranklbl.TextColor = Color.FromHex ( "77CDF0" );
                progress.ProgressColor = Color.FromHex ( "77CDF0" );
            }
            else if ( teacher.Tuition_Point > 16000 && avg >= 4.30f )
            {
                ranklbl.Text = "Master";
                float point = ( float ) teacher.Tuition_Point / ( float ) tpInfo [ 1 ];
                place = ( point * .166f ) + .166f;
                avglbl.TextColor = Color.FromHex ( "CA6AF1" );
                tplbl.TextColor = Color.FromHex ( "CA6AF1" );
                ranklbl.TextColor = Color.FromHex ( "CA6AF1" );
                progress.ProgressColor = Color.FromHex ( "CA6AF1" );
            }
            await progress.ProgressTo ( place , 500 , Easing.SinIn ).ConfigureAwait ( false );
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            //await Application.Current.MainPage.Navigation.PushModalAsync(new RegisterAsTeacher()).ConfigureAwait(true);
            //this.IsPresented = true;
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            this.IsPresented = true;
        }


        private async void Button_Clicked_3(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new TeacherHistory()).ConfigureAwait( false );
        }

        protected override bool OnBackButtonPressed()
        {
            giveAlert();
            return true;
        }

        public async void giveAlert()
        {
            bool answer = await DisplayAlert("Alert", "Would you like to quit?", "Yes", "No").ConfigureAwait( false );
            if (answer == true)
            {
                System.Environment.Exit(0);
            }
        }

        private async void Button_Clicked_4(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new MainPage()).ConfigureAwait( false );
        }

        private void Button_Clicked_5(object sender, EventArgs e)
        {
            this.IsPresented = true;
        }

        private async void Button_Clicked_6(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new UpdateAccount(_Student)).ConfigureAwait( false );
        }

        private async void Button_Clicked_7(object sender, EventArgs e)
        {
            ac++;

            if(ac%2 == 1)
            {
                
                activelbl.Text = "Active";
                activeback.BackgroundColor = Color.FromHex ( "#54E36B" );
                string urlT = "https://api.shikkhanobish.com/api/Master/ChangeStateofIsActive";
                HttpClient clientT = new HttpClient ();
                string jsonDataT = JsonConvert.SerializeObject ( new { TeacherID = teacher.TeacherID , state = 1 } );
                StringContent contentT = new StringContent ( jsonDataT , Encoding.UTF8 , "application/json" );
                HttpResponseMessage responseT = await clientT.PostAsync ( urlT , contentT ).ConfigureAwait ( false );
                string resultT = await responseT.Content.ReadAsStringAsync ();
                var response = JsonConvert.DeserializeObject<Response> ( resultT );
                ConnectToServer ();

            }
            else
            {
                await _connection.StopAsync ();
                activelbl.Text = "Inactive";
                activeback.BackgroundColor = Color.FromHex ( "#A7A7A7" );
                string urlT = "https://api.shikkhanobish.com/api/Master/ChangeStateofIsActive";
                HttpClient clientT = new HttpClient ();
                string jsonDataT = JsonConvert.SerializeObject ( new { TeacherID = teacher.TeacherID , state = 0 } );
                StringContent contentT = new StringContent ( jsonDataT , Encoding.UTF8 , "application/json" );
                HttpResponseMessage responseT = await clientT.PostAsync ( urlT , contentT ).ConfigureAwait ( false );
                string resultT = await responseT.Content.ReadAsStringAsync ();
                var response = JsonConvert.DeserializeObject<Response> ( resultT );
                              
            }
            
        }

        private void Button_Clicked_2 ( object sender , EventArgs e )
        {
            try
            {
                if ( CrossConnectivity.Current.IsConnected )
                {
                    Navigation.PushPopupAsync ( new PopUpForTeacherWallet (teacher ) );
                }
            }
            catch
            {

            }
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
            _connection.On< string , string , int , int , string , string, double, string> ( "CallInfo" , async ( SessionId , UserToken , studentID , teacherID , Class , subject,cost, studentName ) =>
            {
                if(teacher.TeacherID == teacherID)
                {
                    TransferInfo Info = new TransferInfo ();
                    Info.Student.Name = studentName;
                    Info.Class = Class;
                    Info.SubjectName = subject;
                    Info.SessionID = SessionId;
                    Info.UserToken = UserToken;
                    Info.Student.StundentID = studentID;
                    Info.Teacher.TeacherID = teacherID;
                    Info.Teacher.Amount = cost;
                    await Application.Current.MainPage.Navigation.PushModalAsync ( new CallingPageForTeacher (Info) ).ConfigureAwait ( false );
                }
                
            } );
        }
    }
}