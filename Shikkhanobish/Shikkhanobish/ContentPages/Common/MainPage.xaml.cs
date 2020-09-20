using Rg.Plugins.Popup.Extensions;
using Shikkhanobish.ContentPages.Parents;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace Shikkhanobish
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private INavigation navigation;

        public MainPage()
        {
            InitializeComponent();

            var image = new Image { Source = "loginwindow.png" };
            var vm = new MainPageViewModel();
            this.BindingContext = vm;
            Automate();
        }

        public void Automate()
        {
            Username.Completed += (object sender, EventArgs e) =>
            {
                Password.Focus();
            };
            Password.Completed += (object sender, EventArgs e) =>
            {
                var vm = new MainPageViewModel();
                vm.Login.Execute(null);
            };
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