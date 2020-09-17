using Shikkhanobish.ContentPages;
using Xamarin.Forms;

namespace Shikkhanobish
{

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent ();
            MainPage = new MainPage();
            //MainPage = new ParentsProfile ();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }
        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}