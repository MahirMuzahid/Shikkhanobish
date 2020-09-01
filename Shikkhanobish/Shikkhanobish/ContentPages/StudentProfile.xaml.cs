using Newtonsoft.Json;
using Shikkhanobish.Model;
using System;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Extensions;
using Shikkhanobish.ContentPages;
using Plugin.Connectivity;

namespace Shikkhanobish
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentProfile : MasterDetailPage
    {
        public int StudentID;
        private Student _Student;

        public StudentProfile(Student student)
        {
            NavigationPage.SetHasNavigationBar ( this , true );
            StudentID = student.StundentID;
            this.IsPresented = false;
            _Student = student;
            InitializeComponent();
            BindingContext = new StudentProfileVideoModel(student);
            GetPremiumStudent(student.StundentID);
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
            IsPremimum.Text = "Student";
        }
        protected override bool OnBackButtonPressed ( )
        {
            return true;
        }

        private async void Button_Clicked_4(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new MainPage()).ConfigureAwait(true);
        }

        private async void Button_Clicked_5(object sender, EventArgs e)
        {
            try
            {
                if ( CrossConnectivity.Current.IsConnected )
                {
                    await Application.Current.MainPage.Navigation.PushModalAsync ( new StudentHistory ( _Student.StundentID ) ).ConfigureAwait ( true );
                }
            }
            catch
            {

            }
                    
        }

        private async void Button_Clicked_6(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new UpdateAccount(_Student)).ConfigureAwait(true);
        }

        private async void Button_Clicked_7(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new TakeTuition(StudentID, _Student.Name, _Student.UserName, _Student.Password)).ConfigureAwait(true);
        }
        private async void Button_Clicked_8 ( object sender , EventArgs e )
        {        
            await Application.Current.MainPage.Navigation.PushModalAsync ( new Balance(_Student) ).ConfigureAwait ( true );
        }

        private void Button_Clicked ( object sender , EventArgs e )
        {
            this.IsPresented = true;
        }

        private async void Button_Clicked_1 ( object sender , EventArgs e )
        {
            try
            {
                if ( CrossConnectivity.Current.IsConnected )
                {
                    Navigation.PushPopupAsync ( new PopUpForParentCode ( _Student.Password , _Student.ParentCode ) );
                }
            }
            catch
            {

            }
            
                 
        }
    }
}