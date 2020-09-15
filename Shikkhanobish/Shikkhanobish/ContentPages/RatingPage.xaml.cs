using Newtonsoft.Json;
using Shikkhanobish.Model;
using Shikkhanobish.ViewModel;
using System;
using System.Net.Http;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shikkhanobish.ContentPages
{
    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RatingPage : ContentPage
    {
        private TransferInfo info = new TransferInfo();
        private Calculate calculate = new Calculate();
        private object c;

        public RatingPage(TransferInfo trnsInfo)
        {
            InitializeComponent ();
            info = trnsInfo;
            showEverything();
        }

        public void showEverything()
        {
            tnamelbl.Text = info.Teacher.TeacherName;
            tIDlbl.Text = "" + info.Teacher.TeacherID;
            sClasslbl.Text = info.Class;
            sSubject.Text = info.SubjectName;
            inapptimelbl.Text = "" + info.StudyTimeInAPp;
            costlbl.Text = "" + calculate.CalculateCost(info);
            Ratelbl.Text = "";
            sbtn.IsEnabled = false;
        }

        private async void ostarClicked(object sender, EventArgs e)
        {
            oply.Fill = Brush.Gold;
            tply.Fill = Brush.White;
            thply.Fill = Brush.White;
            fply.Fill = Brush.White;
            fiply.Fill = Brush.White;
            await pbar.ProgressTo ( 0.2, 500 , Easing.Linear );
            pbar.ProgressColor = Color.FromHex ( "#FF5A5A" );
            info.GivenRating = 1;
            Ratelbl.Text = "Newbie";
            Ratelbl.TextColor = Color.FromHex ( "#FF5A5A" );
            sbtn.IsEnabled = true;
        }

        private async void tstarClicked(object sender, EventArgs e)
        {
            oply.Fill = Brush.Gold;
            tply.Fill = Brush.Gold;
            thply.Fill = Brush.White;
            fply.Fill = Brush.White;
            fiply.Fill = Brush.White;
            await pbar.ProgressTo ( 0.4 , 500 , Easing.Linear );
            pbar.ProgressColor = Color.FromHex ( "#F0BE05" );
            info.GivenRating = 2;
            Ratelbl.Text = "Avarage";
            sbtn.IsEnabled = true;
            Ratelbl.TextColor = Color.FromHex ( "#F0BE05" );
        }

        private async void thstarClicked(object sender, EventArgs e)
        {
            oply.Fill = Brush.Gold;
            tply.Fill = Brush.Gold;
            thply.Fill = Brush.Gold;
            fply.Fill = Brush.White;
            fiply.Fill = Brush.White;
            await pbar.ProgressTo ( 0.6 , 500 , Easing.Linear );
            pbar.ProgressColor = Color.FromHex ( "#3BCF64" );
            info.GivenRating = 3;
            Ratelbl.Text = "Good";
            sbtn.IsEnabled = true;
            Ratelbl.TextColor = Color.FromHex ( "#3BCF64" );

        }

        private async void fstarClicked(object sender, EventArgs e)
        {
            oply.Fill = Brush.Gold;
            tply.Fill = Brush.Gold;
            thply.Fill = Brush.Gold;
            fply.Fill = Brush.Gold;
            fiply.Fill = Brush.White;
            await pbar.ProgressTo ( 0.8 , 500 , Easing.Linear );
            pbar.ProgressColor = Color.FromHex ( "#50B2ED" );
            info.GivenRating = 4;
            Ratelbl.Text = "Veteran";
            sbtn.IsEnabled = true;
            Ratelbl.TextColor = Color.FromHex ( "#50B2ED" );

        }

        private async void fistarClicked(object sender, EventArgs e)
        {
            oply.Fill = Brush.Gold;
            tply.Fill = Brush.Gold;
            thply.Fill = Brush.Gold;
            fply.Fill = Brush.Gold;
            fiply.Fill = Brush.Gold;
            await pbar.ProgressTo ( 1 , 500 , Easing.Linear );
            pbar.ProgressColor = Color.FromHex ( "#B161F3" );
            info.GivenRating = 5;
            Ratelbl.Text = "Master";
            sbtn.IsEnabled = true;
            Ratelbl.TextColor = Color.FromHex ( "#B161F3" );
        }

        public async void FinishTHeUpdate()
        {
            int tuitionPoint = calculate.CalculateTuitionPoint ( info );
            info.Teacher.OverallTP = tuitionPoint + info.Teacher.Tuition_Point;
            string tuitionClass = null, 
            rank = calculate.CalculateRank(info);
            String Date = DateTime.Now.ToString();
            for (int i = 0; i < info.Class.Length; i++)
            {
                if (info.Class.Length == 7)
                {
                    tuitionClass = "" + info.Class[6];
                }
                if (info.Class.Length == 8)
                {
                    tuitionClass = "" + info.Class[7];
                    tuitionClass = tuitionClass + info.Class[8];
                }
            }
            string url = "https://api.shikkhanobish.com/api/Master/UpdateInfo";
            HttpClient client = new HttpClient();
            string jsonData = JsonConvert.SerializeObject(new
            {
                TeacherID = info.Teacher.TeacherID,
                IsActive = 0,
                IsOnTuition = 0,
                StudentID = info.Student.StundentID,
                Rating = info.GivenRating,
                InAppMin = info.StudyTimeInAPp,
                Tuition_Point = tuitionPoint ,
                Teacher_Rank = rank,
                Date = Date,
                Subject = info.Subject,
                SubjectName = info.SubjectName,
                Class = tuitionClass,
                IsPenidng = 0,
                Teacher_Name = info.Teacher.TeacherName,
                Cost = calculate.CalculateCost(info),
                Student_Name = info.Student.Name
            });
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait( false );
            string result = await response.Content.ReadAsStringAsync().ConfigureAwait( false );
            Response responseData = JsonConvert.DeserializeObject<Response>(result);
            backtoProfile();
        }

        public async void backtoProfile()
        {
            Student student = new Student();
            string url = "https://api.shikkhanobish.com/api/Master/GetInfoByLogin";
            HttpClient client = new HttpClient();
            string jsonData = JsonConvert.SerializeObject(new { UserName = info.Student.UserName, Password = info.Student.Password });
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait( false );
            string result = await response.Content.ReadAsStringAsync();
            student = JsonConvert.DeserializeObject<Student>(result);
            Device.BeginInvokeOnMainThread ( async ( ) =>
            {
                await Application.Current.MainPage.Navigation.PushModalAsync ( new StudentProfile ( student ) ).ConfigureAwait ( false );
            } );
            
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            FinishTHeUpdate();
        }

        private void Button_Clicked_1 ( object sender , EventArgs e )
        {
            //report teacher
        }
    }
}