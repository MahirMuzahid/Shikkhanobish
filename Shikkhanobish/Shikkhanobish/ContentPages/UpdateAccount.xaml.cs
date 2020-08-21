using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shikkhanobish
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateAccount : ContentPage
    {
        public Student Student;

        public UpdateAccount(Student student)
        {
            InitializeComponent();
            Student = student;          
        }

        
        private void Button_Clicked(object sender, EventArgs e)
        {
            
        }
    }
}