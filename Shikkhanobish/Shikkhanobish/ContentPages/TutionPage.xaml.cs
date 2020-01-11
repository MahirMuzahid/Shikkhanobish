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
        Teacher SelectedTeacher;
        int StudentID;
        string Subject;
        public TutionPage(Teacher selectedTeacher, int studentID, string subject )
        {
            InitializeComponent();
            SelectedTeacher = selectedTeacher;
            StudentID = studentID;
            Subject = subject;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            int time = Int32.Parse(TimeEntry.Text);

            await Application.Current.MainPage.Navigation.PushModalAsync(new RatingPage(SelectedTeacher, StudentID, time, Subject)).ConfigureAwait(true);
        }
    }
}