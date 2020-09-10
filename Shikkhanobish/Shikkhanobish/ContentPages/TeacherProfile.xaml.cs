using Newtonsoft.Json;
using System;
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
            tplbl.Text = "Tuition Point: " + teacher.Tuition_Point;
            ranklbl.Text = teacher.Teacher_Rank;
            await progress.ProgressTo ( 0.75 , 500 , Easing.Linear );
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            //await Application.Current.MainPage.Navigation.PushModalAsync(new RegisterAsTeacher()).ConfigureAwait(true);
            //this.IsPresented = true;
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
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