using Shikkhanobish.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.OpenTok.Service;
using Xamarin.Forms.Xaml;

namespace Shikkhanobish.ContentPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TutionPage : ContentPage
    {
        private TransferInfo info = new TransferInfo();

        public TutionPage(TransferInfo trnsInfo)
        {
            InitializeComponent();
            info = trnsInfo;
            CrossOpenTok.Current.CycleCamera ();
        }

        private void OnEndCall(object sender, EventArgs e)
        {
            CrossOpenTok.Current.EndSession();
            gotoRatingPage();
        }

        private void OnSwapCamera(object sender, EventArgs e)
        {
            CrossOpenTok.Current.CycleCamera();
            CrossOpenTok.Current.SendMessageAsync("HogaMara");
        }


        public async void gotoRatingPage()
        {
            //info.StudyTimeInAPp = Int32.Parse(TimeEntry.Text);
            info.StudyTimeInAPp = 30;
            await Application.Current.MainPage.Navigation.PushModalAsync(new RatingPage(info)).ConfigureAwait(true);
        }
    }
}