using Shikkhanobish.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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

        private async void Button_Clicked(object sender, EventArgs e)
        {
            info.StudyTimeInAPp = Int32.Parse(TimeEntry.Text);

            await Application.Current.MainPage.Navigation.PushModalAsync(new RatingPage(info)).ConfigureAwait(true);
        }
    }
}