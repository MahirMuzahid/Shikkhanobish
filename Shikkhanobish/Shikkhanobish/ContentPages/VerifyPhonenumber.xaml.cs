using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shikkhanobish
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerifyPhonenumber : ContentPage
    {
        public Student studentm = new Student();
        public VerifyPhonenumber(Student student)
        {
            InitializeComponent();
            studentm = student;
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new StudentProfile(studentm)).ConfigureAwait(true);
        }
    }
}