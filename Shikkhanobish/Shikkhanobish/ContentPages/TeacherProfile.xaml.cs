using Newtonsoft.Json;
using Shikkhanobish.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            activeback.BackgroundColor = Color.FromHex ( "#9B69F7" );
            var image = new Image { Source = "BackColor.jpg" };
            setRankInfo ();
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
                float point = ( float ) teacher.Tuition_Point / ( float ) tpInfo [ 1 ];
                place = ( point * .166f ) + .166f;            
            }
            else if ( teacher.Tuition_Point <= 8999 && avg >= 4.00f || ( teacher.Tuition_Point > 8999 && avg < 4.00f ) )
            {
                ranklbl.Text = "Good";
                float point = ( float ) teacher.Tuition_Point / ( float ) tpInfo [ 1 ];
                place = ( point * .166f ) + .166f;
            }
            else if ( teacher.Tuition_Point <= 15999 && avg >= 4.30f || ( teacher.Tuition_Point > 15999 && avg < 4.30f ) )
            {
                ranklbl.Text = "Veteran";
                float point = ( float ) teacher.Tuition_Point / ( float ) tpInfo [ 1 ];
                place = ( point * .166f ) + .166f;
            }
            else if ( teacher.Tuition_Point > 16000 && avg >= 4.30f )
            {
                ranklbl.Text = "Master";
                float point = ( float ) teacher.Tuition_Point / ( float ) tpInfo [ 1 ];
                place = ( point * .166f ) + .166f;
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
                
                string urlT = "https://api.shikkhanobish.com/api/Master/ChangeStateofIsActive";
                HttpClient clientT = new HttpClient ();
                string jsonDataT = JsonConvert.SerializeObject ( new { TeacherID = teacher.TeacherID , state = 1 } );
                StringContent contentT = new StringContent ( jsonDataT , Encoding.UTF8 , "application/json" );
                HttpResponseMessage responseT = await clientT.PostAsync ( urlT , contentT ).ConfigureAwait ( false );
                string resultT = await responseT.Content.ReadAsStringAsync ();
                var response = JsonConvert.DeserializeObject<Response> ( resultT );
                if(response.Status == 0)
                {
                    activeback.BackgroundColor = Color.FromHex ( "#54E36B" );
                    activelbl.Text = "Teacher Status: Active";
                }
            }
            else
            {
                string urlT = "https://api.shikkhanobish.com/api/Master/ChangeStateofIsActive";
                HttpClient clientT = new HttpClient ();
                string jsonDataT = JsonConvert.SerializeObject ( new { TeacherID = teacher.TeacherID , state = 0 } );
                StringContent contentT = new StringContent ( jsonDataT , Encoding.UTF8 , "application/json" );
                HttpResponseMessage responseT = await clientT.PostAsync ( urlT , contentT ).ConfigureAwait ( false );
                string resultT = await responseT.Content.ReadAsStringAsync ();
                var response = JsonConvert.DeserializeObject<Response> ( resultT );
                if ( response.Status == 0 )
                {
                    activeback.BackgroundColor = Color.FromHex ( "#54E36B" );
                    activelbl.Text = "Teacher Status: Inactive";
                }
            }
            
        }
    }
}