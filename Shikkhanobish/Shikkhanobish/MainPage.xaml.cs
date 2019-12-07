using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Shikkhanobish
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        MainPageViewModel getTeacher = new MainPageViewModel();
        private INavigation navigation;

        public MainPage()
        {
            
            InitializeComponent();
            var image = new Image { Source = "loginwindowtext.png" };
            BindingContext = new MainPageViewModel();
        }
         protected override bool OnBackButtonPressed()
        {
            giveAlert();
            return true;
        }
        public async void giveAlert()
        {
            bool answer =  await DisplayAlert("Alert", "Would you like to quit?", "Yes", "No").ConfigureAwait(true);
            if (answer == true)
            {
                System.Environment.Exit(0);
            }
        }
    }

}
