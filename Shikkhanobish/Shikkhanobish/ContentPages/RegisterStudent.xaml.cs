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
    public partial class RegisterStudent : ContentPage
    {

        public RegisterStudent()
        {
            InitializeComponent();
            var image = new Image { Source = "onlylogo.png" };
            RegisterStudentViewModel register = new RegisterStudentViewModel(Navigation);
            BindingContext = new RegisterStudentViewModel(Navigation);
            //RegisterButton.IsEnabled = false;
            Automate();
        }
        public void Automate()
        {
            var vm = new RegisterStudentViewModel(Navigation);
            UsernameEntry.Completed += (object sender, EventArgs e) =>
            {
                PasswordEntry.Focus();   
            };
            PasswordEntry.Completed += (object sender, EventArgs e) =>
            {
                ConfirmPasswordEntry.Focus();
            };
            ConfirmPasswordEntry.Completed += (object sender, EventArgs e) =>
            {
                PhoneNumberEntry.Focus();
            };
            PhoneNumberEntry.Completed += (object sender, EventArgs e) =>
            {
                NameEntry.Focus();
            };
            NameEntry.Completed += (object sender, EventArgs e) =>
            {
                AgeEntry.Focus();
            };
            AgeEntry.Completed += (object sender, EventArgs e) =>  
            {
                ClassEntry.Focus();
            };
            ClassEntry.Completed += (object sender, EventArgs e) =>
            {
                INameEntry.Focus();
            };
            INameEntry.Completed += (object sender, EventArgs e) =>
            {
                
                vm.RegisterStudent.Execute(null);
            };
        }

    }
}