using System;


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
            student = s;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            InstitutionID = InstitutionIDEntry.Text;
            await Application.Current.MainPage.Navigation.PushModalAsync(new RegisterAsTeacher(student, InstitutionID)).ConfigureAwait( false );
        }
    }
}