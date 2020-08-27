using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shikkhanobish
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeacherProfile : MasterDetailPage
    {
        public int StudentID;
        private Student _Student;
        private Teacher teacher;

        public TeacherProfile(Teacher t)
        {
            teacher = t;
            this.IsPresented = false;
            InitializeComponent();

            var image = new Image { Source = "BackColor.jpg" };
            BindingContext = new ProfileViewModel(t);
            MasterBehavior = MasterBehavior.Popover;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //await Application.Current.MainPage.Navigation.PushModalAsync(new RegisterAsTeacher()).ConfigureAwait(true);
            //this.IsPresented = true;
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
        }


        private async void Button_Clicked_3(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new TeacherHistory()).ConfigureAwait(true);
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
            await Application.Current.MainPage.Navigation.PushModalAsync(new MainPage()).ConfigureAwait(true);
        }

        private void Button_Clicked_5(object sender, EventArgs e)
        {
            this.IsPresented = true;
        }

        private async void Button_Clicked_6(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new UpdateAccount(_Student)).ConfigureAwait(true);
        }

        private async void Button_Clicked_7(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new TakeTuition(StudentID, _Student.Name, _Student.UserName, _Student.Password)).ConfigureAwait(true);
        }
    }
}