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
    public partial class UpdateAccount : ContentPage
    {
        public Student Student;
        public UpdateAccount(Student student)
        {
            InitializeComponent();
            Student = student;
            NameEntry.IsEnabled = false;
            UserNameEntry.IsEnabled = false;
            PassEntry.IsEnabled = false;
            AgeEntry.IsEnabled = false;
            ClassEntry.IsEnabled = false;
            InstEntry.IsEnabled = false;
            unlbl.Text = student.UserName;
            plbl.Text = "*****";
            nlbl.Text = student.Name;
            albl.Text = ""+student.Age;
            clbl.Text = "" + student.Class;
            ilbl.Text = student.InstitutionName;
        }


        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (oChk.IsChecked == true)
            {
                UserNameEntry.IsEnabled = true;
            }    
            else
            {
                UserNameEntry.IsEnabled = false;
                UserNameEntry.Text = "";
            }
        }

        private void CheckBox_CheckedChanged_1(object sender, CheckedChangedEventArgs e)
        {
            if (tChk.IsChecked == true)
            {
                PassEntry.IsEnabled = true;
            }
            else
            {
                PassEntry.IsEnabled = false;
                PassEntry.Text = "";
            }
        }

        private void CheckBox_CheckedChanged_2(object sender, CheckedChangedEventArgs e)
        {
            if (thChk.IsChecked == true)
            {
                NameEntry.IsEnabled = true;
            }
            else
            {
                NameEntry.IsEnabled = false;
                NameEntry.Text = "";
            }
        }

        private void CheckBox_CheckedChanged_3(object sender, CheckedChangedEventArgs e)
        {
            if (fChk.IsChecked == true)
            {
                AgeEntry.IsEnabled = true;
            }
            else
            {
                AgeEntry.IsEnabled = false;
                AgeEntry.Text = "";
            }
        }

        private void CheckBox_CheckedChanged_4(object sender, CheckedChangedEventArgs e)
        {
            if (fiChk.IsChecked == true)
            {
                ClassEntry.IsEnabled = true;
            }
            else
            {
                ClassEntry.IsEnabled = false;
                ClassEntry.Text = "";
            }
        }

        private void CheckBox_CheckedChanged_5(object sender, CheckedChangedEventArgs e)
        {
            if (sChk.IsChecked == true)
            {
                InstEntry.IsEnabled = true;
            }
            else
            {
                InstEntry.IsEnabled = false;
                InstEntry.Text = "";
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
           if( CurPassEntry.Text != ConCurPassEntry.Text)
           {
                Errortxt.Text = "Password Doen't Match!";
           }
           else if(CurPassEntry.Text != Student.Password)
            {
                Errortxt.Text = "This is not your password!";
            }
        }
    }
}