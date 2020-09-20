using Shikkhanobish.ContentPages;
using System.Linq;
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

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            //var page = App.Current.MainPage.Navigation.ModalStack.LastOrDefault ();
            var name = GetCurrentPage ();
            //var s = page.Title;
        }
        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        public static string GetCurrentPage ( )
        {
            var page = Application.Current.MainPage.Navigation.ModalStack.Last ();
            return page.Title;
        }
    }
}