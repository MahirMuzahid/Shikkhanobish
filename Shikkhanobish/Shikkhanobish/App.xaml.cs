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
                    MainPage = new  CallingPageForTeacher ( StaticPageForOnSleep.info );
                } );
            }
            else
            {
                StaticPageForOnSleep.isSleep = false;
                StaticPageForOnSleep.isCallPending = false;
                MainPage = new SplashScreen();
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