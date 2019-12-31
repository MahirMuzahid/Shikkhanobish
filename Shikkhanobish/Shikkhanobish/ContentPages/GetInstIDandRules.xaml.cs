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
        private Student Student;
        private string InstitutionID;
        public GetInstIDandRules(Student student)
        {
            InitializeComponent();
            Student =  student;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            InstitutionID = InstitutionIDEntry.Text;
            await Application.Current.MainPage.Navigation.PushModalAsync(new RegisterAsTeacher(Student, InstitutionID)).ConfigureAwait(true);
        }
    }
}