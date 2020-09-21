using Newtonsoft.Json;
using Shikkhanobish.ContentPages;
using Shikkhanobish.Model;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Shikkhanobish
{

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent ();
           MainPage = new MainPage();
           // MainPage = new ParentsProfile ();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected async override void OnSleep()
        {
            var page = Application.Current.MainPage.Navigation.ModalStack.Last ();
            var name = page.Title;
            if(name == "TutionPage" )
            {
               await  SetIsPending ().ConfigureAwait(false);
              // await CutVideoCAll ().ConfigureAwait(false);
            }
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        public async Task SetIsPending ( )
        {
            string url = "https://api.shikkhanobish.com/api/Master/SetPending";
            HttpClient client = new HttpClient ();
            string jsonData = JsonConvert.SerializeObject ( new { StudentID = StaticPageForSavingInfoOnStop.StudentID , 
                TeacherName = StaticPageForSavingInfoOnStop.TeacherName , 
                TeacherID = StaticPageForSavingInfoOnStop.TeacherID ,
                Class = StaticPageForSavingInfoOnStop.Class , 
                Subject = StaticPageForSavingInfoOnStop.Subject , 
                Cost = StaticPageForSavingInfoOnStop.Cost
            } );
            StringContent content = new StringContent ( jsonData , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( false );
            string result = await response.Content.ReadAsStringAsync ();
        }
        public async Task CutVideoCAll ( )
        {
            string url = "https://shikkhanobishrealtimeapi.shikkhanobish.com/api/ShikkhanobishRealTimeApi/cutCall?stop=" + 1 + 
                "&teacherID=" + StaticPageForSavingInfoOnStop.StudentID + 
                "&studentID=" + StaticPageForSavingInfoOnStop.TeacherID + 
                "&isStudent=" + true;
            HttpClient client = new HttpClient ();
            StringContent content = new StringContent ( "" , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( true );
            string result = await response.Content.ReadAsStringAsync ().ConfigureAwait ( true );
            var r = JsonConvert.DeserializeObject<string> ( result );
        }
    }
}