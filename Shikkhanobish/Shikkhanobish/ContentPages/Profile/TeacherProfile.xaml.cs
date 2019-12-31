using Shikkhanobish.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Shikkhanobish
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class TeacherProfile : MasterDetailPage
    {
        public TeacherProfile(Student student, int teacherID)
        {
            this.IsPresented = false;
            InitializeComponent();
            TeacherSttLabel.Text = "Teacher Status: OFF";
            var image = new Image { Source = "BackColor.jpg" };
            BindingContext = new TeacherProfileViewModel(student, teacherID);
            MasterBehavior = MasterBehavior.Popover;

        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new StudentHistory()).ConfigureAwait(true);
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
            await Application.Current.MainPage.Navigation.PushModalAsync(new UpdateAccount()).ConfigureAwait(true);
        }

        private async void Button_Clicked_7(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new TakeTuition()).ConfigureAwait(true);
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            if(TeachetToggle.IsToggled == true)
            {
                TeacherSttLabel.Text = "Teacher Status: ON";
            }
            if (TeachetToggle.IsToggled == false)
            {
                TeacherSttLabel.Text = "Teacher Status: OFF";
            }
        }
    }
}