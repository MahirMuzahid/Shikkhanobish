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
    public partial class TakeTuition : ContentPage
    {
        List<string> subject = new List<string>();
        private string selectedClass;
        private string selectedGroup;
        private string selectedSubject;
        public TakeTuition()
        {
            InitializeComponent();
            BindingContext = new TaketuitionViewModel();
            ClassPicker.SelectedIndex = 7;
            ClassPicker.Items.Add("Class 6");
            ClassPicker.Items.Add("Class 7");
            ClassPicker.Items.Add("Class 8");
            ClassPicker.Items.Add("Class 9");
            ClassPicker.Items.Add("Class 10");
            ClassPicker.Items.Add("Class 11");
            ClassPicker.Items.Add("Class 12");
            GroupPicker.IsEnabled = false;
            GroupBoxView.BackgroundColor = Color.FromHex("#CDCDCD");
        }

        private void ClassPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedClass = ClassPicker.Items[ClassPicker.SelectedIndex];
            if (selectedClass == "Class 6")
            {
                SubjectPicker.SelectedIndex = 4;
                SubjectPicker.Items.Add("Bangla 1st Paper");
                SubjectPicker.Items.Add("Bangla 2nd Paper");
                SubjectPicker.Items.Add("Englist 1st Paper");
                SubjectPicker.Items.Add("English 2nd Paper");
                SubjectPicker.Items.Add("Math");
                GroupPicker.IsEnabled = false;
                GroupBoxView.BackgroundColor = Color.FromHex("#CDCDCD");
            }
            else if (selectedClass == "Class 7")
            {
                SubjectPicker.SelectedIndex = 4;
                SubjectPicker.Items.Add("Bangla 1st Paper");
                SubjectPicker.Items.Add("Bangla 2nd Paper");
                SubjectPicker.Items.Add("Englist 1st Paper");
                SubjectPicker.Items.Add("English 2nd Paper");
                SubjectPicker.Items.Add("Math");
                GroupPicker.IsEnabled = false;
                GroupBoxView.BackgroundColor = Color.FromHex("#CDCDCD");
            }
            else if (selectedClass == "Class 8")
            {
                SubjectPicker.SelectedIndex = 4;
                SubjectPicker.Items.Add("Bangla 1st Paper");
                SubjectPicker.Items.Add("Bangla 2nd Paper");
                SubjectPicker.Items.Add("Englist 1st Paper");
                SubjectPicker.Items.Add("English 2nd Paper");
                SubjectPicker.Items.Add("Math");
                GroupPicker.IsEnabled = false;
                GroupBoxView.BackgroundColor = Color.FromHex("#CDCDCD");
            }
            else if( selectedClass == "Class 9" || selectedClass == "Class 10" || selectedClass == "Class 11" || selectedClass == "Class 12")
            {
                GroupPicker.SelectedIndex = 3;
                GroupPicker.Items.Add("Science");
                GroupPicker.Items.Add("Commerce");
                GroupPicker.Items.Add("Arts");
                GroupPicker.IsEnabled = true;
                GroupBoxView.BackgroundColor = Color.FromHex("#FFFFFF");
            }
            
        }

        private void GroupPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedGroup = GroupPicker.Items[GroupPicker.SelectedIndex];
            if ((selectedClass == "Class 9" || selectedClass == "Class 10") & selectedGroup == "Science")
            {
                SubjectPicker.SelectedIndex = 9;
                SubjectPicker.Items.Add("Bangla 1st Paper");
                SubjectPicker.Items.Add("Bangla 2nd Paper");
                SubjectPicker.Items.Add("Englist 1st Paper");
                SubjectPicker.Items.Add("English 2nd Paper");
                SubjectPicker.Items.Add("ICT");
                SubjectPicker.Items.Add("Physics");
                SubjectPicker.Items.Add("Cheistry");
                SubjectPicker.Items.Add("Biology");
                SubjectPicker.Items.Add("Math");
            }
            if ((selectedClass == "Class 9" || selectedClass == "Class 10") & selectedGroup == "Commerce")
            {
                SubjectPicker.SelectedIndex = 4;
                SubjectPicker.Items.Add("Bangla 1st Paper");
                SubjectPicker.Items.Add("Bangla 2nd Paper");
                SubjectPicker.Items.Add("Englist 1st Paper");
                SubjectPicker.Items.Add("English 2nd Paper");
                SubjectPicker.Items.Add("ICT");
                SubjectPicker.Items.Add("Economics");
                SubjectPicker.Items.Add("Accounting");
                SubjectPicker.Items.Add("Finance & Banking");
                SubjectPicker.Items.Add("Business Entrepreneurship");
            }
            if ((selectedClass == "Class 9" || selectedClass == "Class 10") & selectedGroup == "Arts")
            {
                SubjectPicker.SelectedIndex = 7;
                SubjectPicker.Items.Add("Bangla 1st Paper");
                SubjectPicker.Items.Add("Bangla 2nd Paper");
                SubjectPicker.Items.Add("Englist 1st Paper");
                SubjectPicker.Items.Add("English 2nd Paper");
                SubjectPicker.Items.Add("ICT");
                SubjectPicker.Items.Add("Geology");
                SubjectPicker.Items.Add("Career Education");
            }
            if ((selectedClass == "Class 11" || selectedClass == "Class 12") & selectedGroup == "Science")
            {
                SubjectPicker.SelectedIndex = 13;
                SubjectPicker.Items.Add("Bangla 1st Paper");
                SubjectPicker.Items.Add("Bangla 2nd Paper");
                SubjectPicker.Items.Add("Englist 1st Paper");
                SubjectPicker.Items.Add("English 2nd Paper");
                SubjectPicker.Items.Add("ICT");
                SubjectPicker.Items.Add("Physics 1st Paper");
                SubjectPicker.Items.Add("Physics 2nd Paper");
                SubjectPicker.Items.Add("Cheistry 1st Paper");
                SubjectPicker.Items.Add("Cheistry 2nd Paper");
                SubjectPicker.Items.Add("Biology 1st Paper");
                SubjectPicker.Items.Add("Biology 2nd Paper");
                SubjectPicker.Items.Add("Math 1st Paper");
                SubjectPicker.Items.Add("Math 2nd Paper");
            }
            if ((selectedClass == "Class 11" || selectedClass == "Class 12") & selectedGroup == "Commerce")
            {
                SubjectPicker.SelectedIndex = 9;
                SubjectPicker.Items.Add("Bangla 1st Paper");
                SubjectPicker.Items.Add("Bangla 2nd Paper");
                SubjectPicker.Items.Add("Englist 1st Paper");
                SubjectPicker.Items.Add("English 2nd Paper");
                SubjectPicker.Items.Add("ICT");
                SubjectPicker.Items.Add("Economics");
                SubjectPicker.Items.Add("Accounting");
                SubjectPicker.Items.Add("Finance & Banking");
                SubjectPicker.Items.Add("Statistics");
            }
            if ((selectedClass == "Class 11" || selectedClass == "Class 12") & selectedGroup == "Arts")
            {
                SubjectPicker.SelectedIndex = 6;
                SubjectPicker.Items.Add("Bangla 1st Paper");
                SubjectPicker.Items.Add("Bangla 2nd Paper");
                SubjectPicker.Items.Add("Englist 1st Paper");
                SubjectPicker.Items.Add("English 2nd Paper");
                SubjectPicker.Items.Add("ICT");
                SubjectPicker.Items.Add("Zuktibidda");
            }
        }
        private void SubjectPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

       
    }
}