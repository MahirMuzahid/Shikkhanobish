using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shikkhanobish.ContentPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GetInstIDandRules : ContentPage
    {
        public StudentClass student;
        public string InstitutionID;

        public GetInstIDandRules( StudentClass s )
        {
            InitializeComponent();
            chsubbtn.IsEnabled = false;
            chsubbtn.BackgroundColor = Color.DarkGray;
            student = s;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            InstitutionID = InstitutionIDEntry.Text;
            await Application.Current.MainPage.Navigation.PushModalAsync(new RegisterAsTeacher(student, InstitutionID)).ConfigureAwait( false );
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Browser.OpenAsync("https://shikkhanobish.com/Terms-And-Conditions.html", BrowserLaunchMode.SystemPreferred).ConfigureAwait(false);
        }

        private void chkbx_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if(chkbx.IsChecked == false)
            {
                chsubbtn.IsEnabled = false;
                chsubbtn.BackgroundColor = Color.DarkGray;
            }
            else
            {
                chsubbtn.IsEnabled = true;
                chsubbtn.BackgroundColor = Color.FromHex("#41B4ED");
            }
        }
    }
}