using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
using Shikkhanobish.ContentPages;
using Shikkhanobish.ContentPages.Parents;
using Shikkhanobish.Model;
using Shikkhanobish.ViewModel;
using System;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Shikkhanobish
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private INavigation navigation;
        bool isThereLoggedUser;
        public Student student = new Student ();
        public Teacher teacher = new Teacher ();
        public Parent parent = new Parent();
        public MainPage()
        {
            StaticPageForOnSleep.isParent = false;
            isThereLoggedUser = false;          
            InitializeComponent ();
            MainThread.BeginInvokeOnMainThread ( async ( ) =>
            {
                var image = new Image { Source = "loginwindow.png" };
                var vm = new MainPageViewModel ();
                this.BindingContext = vm;
            } );
            
        }
       

        protected override bool OnBackButtonPressed()
        {
            System.Environment.Exit ( 0 );
            return true;
        }


        private void Button_Clicked(object sender, EventArgs e)
        {
            Loginbtn.IsEnabled = false;
            if (Loginbtn.Text != "Wait")
            {
                Loginbtn.IsEnabled = true;
            }
        }

        private async void Button_Clicked_1 ( object sender , EventArgs e )
        {
            await Application.Current.MainPage.Navigation.PushPopupAsync ( new PopUpForLoginParents () ).ConfigureAwait ( false );
        }

        
    }
}