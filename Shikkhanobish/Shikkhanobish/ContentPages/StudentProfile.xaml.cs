using Newtonsoft.Json;
using Shikkhanobish.Model;
using System;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shikkhanobish
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentProfile : MasterDetailPage
    {
        public int StudentID;
        private Student _Student;

        public StudentProfile(Student student)
        {
            StudentID = student.StundentID;
            this.IsPresented = false;
            _Student = student;
            InitializeComponent();
            BindingContext = new StudentProfileVideoModel(student);
            GetPremiumStudent(student.StundentID);
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new StudentHistory()).ConfigureAwait(true);
        }
        public async void GetPremiumStudent(int S_id)
        {
            string url = "https://api.shikkhanobish.com/api/Master/GetPremiumStudent";
            HttpClient client = new HttpClient();
            string jsonData = JsonConvert.SerializeObject(new { StudentID = S_id });
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait(true);
            string result = await response.Content.ReadAsStringAsync();
            var premiumStudents = JsonConvert.DeserializeObject<PremiumStudents>(result);
            if (premiumStudents.StudentID != 0)
            {
                IsPremimum.Text = "Student: Premium";
                fbx.IsEnabled = false;
                flb.IsEnabled = false;
                pbx.IsEnabled = false;
                plb.IsEnabled = false;
            }
            else
            {
                IsPremimum.Text = "Student: Normal";
                fbx.IsEnabled = true;
                flb.IsEnabled = true;
                pbx.IsEnabled = true;
                plb.IsEnabled = true;
            }
        }
        protected override bool OnBackButtonPressed()
        {
            giveAlert();
            return true;
        }

        public async void giveAlert()
        {
            bool answer = await DisplayAlert("Alert", "Would you like to quit?", "Yes", "No").ConfigureAwait(true);
            if (answer == true)
            {
                System.Environment.Exit(0);
            }
        }

        private async void Button_Clicked_4(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new MainPage()).ConfigureAwait(true);
        }

        private void Button_Clicked_5(object sender, EventArgs e)
        {
            this.IsPresented = true;
        }

        private async void Button_Clicked_6(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new UpdateAccount(_Student)).ConfigureAwait(true);
        }

        private async void Button_Clicked_7(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new TakeTuition(StudentID, _Student.Name, _Student.UserName, _Student.Password)).ConfigureAwait(true);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {

        }
    }
}