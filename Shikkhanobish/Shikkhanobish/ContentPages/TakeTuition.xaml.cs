using Shikkhanobish.ContentPages;
using Shikkhanobish.ViewModel;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Shikkhanobish
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TakeTuition : ContentPage
    {
        private string selectedClass;
        private string selectedGroup;
        private string selectedSubject;

        private string subject, Sub,subjectName;
        private TransferInfo transferNow = new TransferInfo();
        public TakeTuition( int StudentID, string StudentName)
        {
            InitializeComponent();
            BindingContext = new TaketuitionViewModel();
            StudentIDTxt.Text = "" + StudentID;
            StudentNameTxt.Text = "" + StudentName;
            transferNow.Student.Name = StudentName;
            transferNow.Student.StundentID = StudentID;
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
            GroupTxt.TextColor = Color.FromHex("#808080");
            SearchBtn.IsEnabled = false;
        }

        private void ClassPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            SubjectPicker.Items.Clear();
            GroupPicker.Items.Clear();
            selectedClass = ClassPicker.Items[ClassPicker.SelectedIndex];
            if (selectedClass == "Class 6")
            {
                SubjectPicker.SelectedIndex = 5;
                SubjectPicker.Items.Add("Bangla 1st Paper");
                SubjectPicker.Items.Add("Bangla 2nd Paper");
                SubjectPicker.Items.Add("Englist 1st Paper");
                SubjectPicker.Items.Add("English 2nd Paper");
                SubjectPicker.Items.Add("Math");
                GroupPicker.IsEnabled = false;
                GroupBoxView.BackgroundColor = Color.FromHex("#CDCDCD");
                GroupTxt.TextColor = Color.FromHex("#808080");
            }
            else if (selectedClass == "Class 7")
            {
                SubjectPicker.SelectedIndex = 5;
                SubjectPicker.Items.Add("Bangla 1st Paper");
                SubjectPicker.Items.Add("Bangla 2nd Paper");
                SubjectPicker.Items.Add("Englist 1st Paper");
                SubjectPicker.Items.Add("English 2nd Paper");
                SubjectPicker.Items.Add("Math");
                GroupPicker.IsEnabled = false;
                GroupBoxView.BackgroundColor = Color.FromHex("#CDCDCD");
                GroupTxt.TextColor = Color.FromHex("#808080");
            }
            else if (selectedClass == "Class 8")
            {
                SubjectPicker.SelectedIndex = 5;
                SubjectPicker.Items.Add("Bangla 1st Paper");
                SubjectPicker.Items.Add("Bangla 2nd Paper");
                SubjectPicker.Items.Add("Englist 1st Paper");
                SubjectPicker.Items.Add("English 2nd Paper");
                SubjectPicker.Items.Add("Math");
                GroupPicker.IsEnabled = false;
                GroupBoxView.BackgroundColor = Color.FromHex("#CDCDCD");
                GroupTxt.TextColor = Color.FromHex("#808080");
            }
            else if( selectedClass == "Class 9" || selectedClass == "Class 10" || selectedClass == "Class 11" || selectedClass == "Class 12")
            {
                GroupPicker.SelectedIndex = 3;
                GroupPicker.Items.Add("Science");
                GroupPicker.Items.Add("Commerce");
                GroupPicker.Items.Add("Arts");
                GroupPicker.IsEnabled = true;
                GroupBoxView.BackgroundColor = Color.FromHex("#FFFFFF");
                GroupTxt.TextColor = Color.FromHex("#00203F");
            }
            
        }

        private void GroupPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            SubjectPicker.Items.Clear();
            if(GroupPicker.Items.Count != 0)
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
                    SubjectPicker.Items.Add("Logic");
                }
            }
            
        }
        private async void SubjectPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchBtn.IsEnabled = true;
            selectedSubject = SubjectPicker.Items[SubjectPicker.SelectedIndex];
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string Class = null, sub = null, paper = null;
            string Sub = null, Paper = null;
            if (selectedClass == "Class 6" || selectedClass == "Class 7" || selectedClass == "Class 8")
            {
                Class = "LS";
                if (selectedSubject[0] == 'B')
                {
                    sub = "BAN";
                    Sub = "Bangla";
                }
                else if (selectedSubject[0] == 'E')
                {
                    sub = "ENG";
                    Sub = "English";
                }
                else if (selectedSubject[0] == 'M')
                {
                    sub = "MATH";
                    Sub = "Match";
                }
                else if (selectedSubject[0] == 'I')
                {
                    sub = "ICT";
                    Sub = "ICT";
                }
                for (int i = 0; i < selectedSubject.Length; i++)
                {
                    if (selectedSubject[i] == ' ')
                    {
                        if (selectedSubject[i + 1] == '1')
                        {
                            paper = "01";
                            Paper = "First Paper";
                        }
                        else if (selectedSubject[i + 1] == '2')
                        {
                            paper = "02";
                            Paper = "Second Paper";
                        }
                    }
                    subject = Class + sub + paper;
                    subjectName = Sub + " " + Paper;
                    transferNow.ClassCode = Class;
                }
                //------------------------------------------------------------------------------------------------------
                
            }
            if (selectedClass == "Class 9" || selectedClass == "Class 10")
            {
                Class = "S";
                if (selectedSubject[0] == 'B')
                {
                    sub = "BAN";
                    Sub = "Bangla";
                }
                else if (selectedSubject[0] == 'E')
                {
                    sub = "ENG";
                    Sub = "English";
                }
                else if (selectedSubject[0] == 'M')
                {
                    sub = "MATH";
                    Sub = "Math";
                }
                else if (selectedSubject[0] == 'I')
                {
                    sub = "ICT";
                    Sub = "ICT";
                }
                else if (selectedSubject[0] == 'P')
                {
                    sub = "PHY";
                    Sub = "Physics";
                }
                else if (selectedSubject[0] == 'C')
                {
                    sub = "CHE";
                    Sub = "Chemistry";
                }
                else if (selectedSubject[0] == 'B' && selectedSubject[1] == 'i')
                {
                    sub = "BIO";
                    Sub = "Biology";
                }
                else if (selectedSubject[0] == 'H')
                {
                    sub = "HMATH";
                    Sub = "Higher Math";
                }
                //--------------------------------------
                else if (selectedSubject[0] == 'E' && selectedSubject[1] == 'c')
                {
                    sub = "ECO";
                    Sub = "Economics";
                }
                else if (selectedSubject[0] == 'A')
                {
                    sub = "ACC";
                    Sub = "Accounting";
                }
                else if (selectedSubject[0] == 'F')
                {
                    sub = "FIN";
                    Sub = "Finance";
                }
                else if (selectedSubject[0] == 'B' && selectedSubject[1] == 'u')
                {
                    sub = "BENT";
                    Sub = "Business & Entrepreneurship";
                }
                //------------------------------------
                else if (selectedSubject[0] == 'C' && selectedSubject[1] == 'a')
                {
                    sub = "CRE";
                    Sub = "Career Education";
                }
                else if (selectedSubject[0] == 'G')
                {
                    sub = "GEO";
                    Sub = "Geology";
                }
                for (int i = 0; i < selectedSubject.Length; i++)
                {
                    if (selectedSubject[i] == ' ')
                    {
                        if (selectedSubject[i + 1] == '1')
                        {
                            paper = "01";
                            Paper = "First Paper";
                        }
                        else if (selectedSubject[i + 1] == '2')
                        {
                            paper = "02";
                            Paper = "Second Paper";
                        }
                    }
                    subject = Class + sub + paper;
                    subjectName = Sub + " " + Paper;
                    transferNow.ClassCode = Class;
                }
            }            
            if (selectedClass == "Class 11" || selectedClass == "Class 12")
            {
                Class = "HS";
                if (selectedSubject[0] == 'B')
                {
                    sub = "BAN";
                    Sub = "Bangla";
                }
                else if (selectedSubject[0] == 'E')
                {
                    sub = "ENG";
                    Sub = "English";
                }
                else if (selectedSubject[0] == 'M')
                {
                    sub = "MATH";
                    Sub = "Math";
                }
                else if (selectedSubject[0] == 'I')
                {
                    sub = "ICT";
                }
                else if (selectedSubject[0] == 'P')
                {
                    sub = "PHY";
                    Sub = "Physics";
                }
                else if (selectedSubject[0] == 'C')
                {
                    sub = "CHE";
                    Sub = "Chemistry";
                }
                else if (selectedSubject[0] == 'B' && selectedSubject[1] == 'i')
                {
                    sub = "BIO";
                    Sub = "Biology";
                }
                else if (selectedSubject[0] == 'H')
                {
                    sub = "HMATH";
                    Sub = "Higher Math";
                }
                //--------------------------------------
                else if (selectedSubject[0] == 'E' && selectedSubject[1] == 'c')
                {
                    sub = "ECO";
                    Sub = "Economics";
                }
                else if (selectedSubject[0] == 'A')
                {
                    sub = "ACC";
                    Sub = "Accounting";

                }
                else if (selectedSubject[0] == 'F')
                {
                    sub = "FIN";
                    Sub = "Finance";
                }
                else if (selectedSubject[0] == 'S')
                {
                    sub = "STAT";
                    Sub = "Statistics";
                }
                //------------------------------------
                else if (selectedSubject[0] == 'Z')
                {
                    sub = "LOG";
                    Sub = "Logic";
                }
                for (int i = 0; i < selectedSubject.Length; i++)
                {
                    if (selectedSubject[i] == ' ')
                    {
                        if (selectedSubject[i + 1] == '1')
                        {
                            paper = "01";
                            Paper = "First Paper";
                        }
                        else if (selectedSubject[i + 1] == '2')
                        {
                            paper = "02";
                            Paper = "Second Paper";
                        }
                    }
                    subject = Class + sub + paper;
                    subjectName = Sub + " " + Paper;
                    transferNow.ClassCode = Class;
                }
            }
            if(sub != null)
            {
                SearchBtn.Text = "We are searching best teacher for you";
                transferNow.Class = selectedClass;
                transferNow.Subject = subject;
                transferNow.SubjectName = subjectName;
                await Application.Current.MainPage.Navigation.PushModalAsync(new SearchedTeacher(transferNow)).ConfigureAwait(true);

            }
        }

        
    }
}