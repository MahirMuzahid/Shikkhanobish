using Newtonsoft.Json;
using Plugin.LocalNotification;
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
        }
        
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected async override void OnSleep()
        {

        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        
    }
}