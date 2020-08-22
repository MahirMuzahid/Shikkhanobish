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
            if(CurPassEntry.Text == Student.Password)
            {
                
                Student student = new Student ();
                student.UserName =  UserEntry.Text;
                student.Password = PassEntry.Text;
                if(AgeEntry.Text != null)
                {
                    student.Age = int.Parse ( AgeEntry.Text );
                }               
                student.Name = NameEntry.Text;
                student.InstitutionName = InstEntry.Text;
                student.Class = ClassEntry.Text;

                //Api call to update user info

                Errortxt.Text = "Update Done";
            }
            else
            {
                Errortxt.Text = "Password dosen't match";
            }
        }
    }
}