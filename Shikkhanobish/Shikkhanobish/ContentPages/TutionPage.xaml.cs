using Shikkhanobish.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.OpenTok.Service;

namespace Shikkhanobish.ContentPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TutionPage : ContentPage
    {
        TransferInfo info = new TransferInfo();
        public TutionPage(TransferInfo trnsInfo )
        {
            InitializeComponent();
            info = trnsInfo;
        }

        private void OnEndCall(object sender, EventArgs e)
        {
            CrossOpenTok.Current.EndSession();
            CrossOpenTok.Current.MessageReceived -= OnMessageReceived;
            gotoRatingPage();
        }

        private void OnMessage(object sender, EventArgs e)
        {
            CrossOpenTok.Current.SendMessageAsync($"Path.GetRandomFileName: {Path.GetRandomFileName()}");
        }

        private void OnSwapCamera(object sender, EventArgs e)
        {
            CrossOpenTok.Current.CycleCamera();
        }

        private void OnMessageReceived(string message)
        {
            DisplayAlert("Random message received", message, "OK");
        }

        public async void gotoRatingPage()
        {
            //info.StudyTimeInAPp = Int32.Parse(TimeEntry.Text);

            await Application.Current.MainPage.Navigation.PushModalAsync(new RatingPage(info)).ConfigureAwait(true);
        }
    }
}