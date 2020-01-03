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
    public partial class GetInstIDandRules : ContentPage
    {
        public int Studentid;
        public string InstitutionID;
        public GetInstIDandRules(int StudentID)
        {
            InitializeComponent();
            Studentid = StudentID;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            InstitutionID = InstitutionIDEntry.Text;
            await Application.Current.MainPage.Navigation.PushModalAsync(new RegisterAsTeacher(Studentid, InstitutionID)).ConfigureAwait(true);
        }
    }
}