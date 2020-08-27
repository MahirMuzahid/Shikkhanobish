using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shikkhanobish
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        public Profile(Student student)
        {
            InitializeComponent();
            var image = new Image { Source = "BackColor.jpg" };
            //BindingContext = new ProfileViewModel(student);
        }

        private async void Button_Clicked(object sender, EventArgs e   )
        {
        }

        private async void Button_Clicked_1(object sender, EventArgs e )
        {
           
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new TeacherHistory()).ConfigureAwait(true);
        }

        private async void Button_Clicked_3(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new StudentHistory(3)).ConfigureAwait(true);
        }

        protected override bool OnBackButtonPressed()
        {
            giveAlert();
            return true;
        }

        public async void giveAlert()
        {
            bool answer = await DisplayAlert("Alert", "Would you like to quit?", "Yes", "No").ConfigureAwait(true);
            if (answer == true)
            {
                System.Environment.Exit(0);
            }
        }

        private async void Button_Clicked_4(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new Settings()).ConfigureAwait(true);
        }
    }
}