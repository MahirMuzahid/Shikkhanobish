using Newtonsoft.Json;
using Plugin.LocalNotification;
using Shikkhanobish.ContentPages;
using Shikkhanobish.Interface;
using Shikkhanobish.Model;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Shikkhanobish
{

    public partial class App : Application
    {
        INotification notificationManager;
        public App()
        {
            InitializeComponent ();
            if ( StaticPageForOnSleep.isStudent == false && StaticPageForOnSleep.isParent == false && StaticPageForOnSleep.isCallPending == true && StaticPageForOnSleep.isSleep == true)
            {
                MainThread.BeginInvokeOnMainThread ( async ( ) => {
                    Application.Current.MainPage = new NavigationPage(new CallingPageForTeacher(StaticPageForOnSleep.info, 1, StaticPageForOnSleep.shortNote)); 
                } );
            }
            else
            {
                StaticPageForOnSleep.isSleep = false;
                StaticPageForOnSleep.isCallPending = false;
                Application.Current.MainPage = new NavigationPage(new SplashScreen());
                
            }
            

        }
        
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected async override void OnSleep()
        {

            StaticPageForOnSleep.isSleep = true;
        }

        protected override void OnResume()
        {
            StaticPageForOnSleep.isSleep = false;
            
            
            // Handle when your app resumes
        }
        
    }
}