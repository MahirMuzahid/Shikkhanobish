using Shikkhanobish.ContentPages;
using Shikkhanobish.ViewModel;
using System;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Rg.Plugins.Popup.Extensions;
using Shikkhanobish.ContentPages.Common;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.Collections.Generic;
using Shikkhanobish.Model;

namespace Shikkhanobish
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TakeTuition : ContentPage
    {
        private string selectedClass;
        private string selectedGroup;
        private string selectedSubject;
        int searchdontcount = 0;
        public bool IsCancled;
        private string subject, Sub, subjectName;
        int studentd;
        private TransferInfo transferNow = new TransferInfo();
        private List<TeacherID> TeacherIDListBySearch = new List<TeacherID> ();
        private List<Teacher> TeacherList = new List<Teacher> ();
        private List<Teacher> FilteredTeacher = new List<Teacher> ();
        public bool ok;
        List<string> subjectNameC6to8 = new List<string> ();
        List<string> subjectNameC9to10s = new List<string> ();        
        List<string> subjectNameC9to10a = new List<string> ();        
        List<string> subjectNameC9to10c = new List<string> ();
        List<string> subjectNameC11to12a = new List<string> ();
        List<string> subjectNameC11to12c = new List<string> ();
        List<string> subjectNameC11to12s = new List<string> ();

        List<string> groupName = new List<string> ();




        public TakeTuition ( int StudentID , string StudentName , string username , string pass , float amount)
        { subjectNameC6to8.Add ( "Bangla 1st Paper" );
            subjectNameC6to8.Add ( "Bangla 2nd Paper" );
            subjectNameC6to8.Add ( "Englist 1st Paper" );
            subjectNameC6to8.Add ( "English 2nd Paper" );
            subjectNameC6to8.Add ( "Math" );

            groupName.Add ( "Science" );
            groupName.Add ( "Commerce" );
            groupName.Add ( "Arts" );

            subjectNameC9to10s.Add ( "Bangla 1st Paper" );
            subjectNameC9to10s.Add ( "Bangla 2nd Paper" );
            subjectNameC9to10s.Add ( "Englist 1st Paper" );
            subjectNameC9to10s.Add ( "English 2nd Paper" );
            subjectNameC9to10s.Add ( "ICT" );
            subjectNameC9to10s.Add ( "Physics" );
            subjectNameC9to10s.Add ( "Cheistry" );
            subjectNameC9to10s.Add ( "Biology" );
            subjectNameC9to10s.Add ( "Math" );

            subjectNameC9to10c.Add ( "Bangla 1st Paper" );
            subjectNameC9to10c.Add ( "Bangla 2nd Paper" );
            subjectNameC9to10c.Add ( "Englist 1st Paper" );
            subjectNameC9to10c.Add ( "English 2nd Paper" );
            subjectNameC9to10c.Add ( "ICT" );
            subjectNameC9to10c.Add ( "Economics" );
            subjectNameC9to10c.Add ( "Accounting" );
            subjectNameC9to10c.Add ( "Finance & Banking" );
            subjectNameC9to10c.Add ( "Business Entrepreneurship" );

            subjectNameC9to10a.Add ( "Bangla 1st Paper" );
            subjectNameC9to10a.Add ( "Bangla 2nd Paper" );
            subjectNameC9to10a.Add ( "Englist 1st Paper" );
            subjectNameC9to10a.Add ( "English 2nd Paper" );
            subjectNameC9to10a.Add ( "ICT" );
            subjectNameC9to10a.Add ( "Geology" );
            subjectNameC9to10a.Add ( "Career Education" );

            subjectNameC11to12s.Add ( "Bangla 1st Paper" );
            subjectNameC11to12s.Add ( "Bangla 2nd Paper" );
            subjectNameC11to12s.Add ( "Englist 1st Paper" );
            subjectNameC11to12s.Add ( "English 2nd Paper" );
            subjectNameC11to12s.Add ( "ICT" );
            subjectNameC11to12s.Add ( "Physics 1st Paper" );
            subjectNameC11to12s.Add ( "Physics 2nd Paper" );
            subjectNameC11to12s.Add ( "Cheistry 1st Paper" );
            subjectNameC11to12s.Add ( "Cheistry 2nd Paper" );
            subjectNameC11to12s.Add ( "Biology 1st Paper" );
            subjectNameC11to12s.Add ( "Biology 2nd Paper" );
            subjectNameC11to12s.Add ( "Math 1st Paper" );
            subjectNameC11to12s.Add ( "Math 2nd Paper" );

            subjectNameC11to12c.Add ( "Bangla 1st Paper" );
            subjectNameC11to12c.Add ( "Bangla 2nd Paper" );
            subjectNameC11to12c.Add ( "Englist 1st Paper" );
            subjectNameC11to12c.Add ( "English 2nd Paper" );
            subjectNameC11to12c.Add ( "ICT" );
            subjectNameC11to12c.Add ( "Economics" );
            subjectNameC11to12c.Add ( "Accounting" );
            subjectNameC11to12c.Add ( "Finance & Banking" );

            subjectNameC11to12a.Add ( "Bangla 1st Paper" );
            subjectNameC11to12a.Add ( "Bangla 2nd Paper" );
            subjectNameC11to12a.Add ( "Englist 1st Paper" );
            subjectNameC11to12a.Add ( "English 2nd Paper" );
            subjectNameC11to12a.Add ( "ICT" );
            subjectNameC11to12a.Add ( "Logic" );
            InitializeComponent ();
            clicked = 0;
            IsCancled = false;
            studentd = StudentID;
            SearchBtn.Text = "Search Teacher";
            SearchBtn.TextColor = Color.FromHex ( "#2F2F2F" );
            BindingContext = new TaketuitionViewModel();
            StudentIDTxt.Text = "" + StudentID;
            StudentNameTxt.Text = "" + StudentName;
            if(amount < 2)
            {
                amountTxt.TextColor = Color.Red;
            }
            amountTxt.Text = ""+ amount;
            transferNow.Student.UserName = username;
            transferNow.Student.Password = pass;
            transferNow.Student.Name = StudentName;
            transferNow.Student.StudentID = StudentID;
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
        string preClass = "";
        private void ClassPicker_SelectedIndexChanged(object sender, EventArgs e)
        {          
            selectedClass = ClassPicker.SelectedItem.ToString();
            if (selectedClass == "Class 6"|| selectedClass == "Class 7"|| selectedClass == "Class 8" )
            {               
                preClass = selectedClass;
                SubjectPicker.ItemsSource = subjectNameC6to8;
                GroupPicker.IsEnabled = false;
                GroupBoxView.BackgroundColor = Color.FromHex("#CDCDCD");
                GroupTxt.TextColor = Color.FromHex("#808080");
            }           
            else if (selectedClass == "Class 9" || selectedClass == "Class 10" || selectedClass == "Class 11" || selectedClass == "Class 12")
            {
                SubjectPicker.SelectedIndex = 0;
                GroupPicker.IsEnabled = true;
                GroupPicker.ItemsSource = groupName;
                GroupBoxView.BackgroundColor = Color.FromHex( "#7FDD78FF" );
                GroupTxt.TextColor = Color.FromHex( "#00203F" );
            }

        }

        private void GroupPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ( GroupPicker.Items.Count != 0 )
                {                    
                    selectedGroup = GroupPicker.SelectedItem.ToString ();
                    if ( ( selectedClass == "Class 9" || selectedClass == "Class 10" ) & selectedGroup == "Science" )
                    {
                        SubjectPicker.ItemsSource = subjectNameC9to10s;
                    }
                    else if ( ( selectedClass == "Class 9" || selectedClass == "Class 10" ) & selectedGroup == "Commerce" )
                    {
                        SubjectPicker.ItemsSource = subjectNameC9to10c;
                    }
                    else if ( ( selectedClass == "Class 9" || selectedClass == "Class 10" ) & selectedGroup == "Arts" )
                    {

                        SubjectPicker.ItemsSource = subjectNameC9to10a;
                    }
                    else if ( ( selectedClass == "Class 11" || selectedClass == "Class 12" ) & selectedGroup == "Science" )
                    {
                        SubjectPicker.ItemsSource = subjectNameC11to12s;
                    }
                    else if ( ( selectedClass == "Class 11" || selectedClass == "Class 12" ) & selectedGroup == "Commerce" )
                    {
                        SubjectPicker.ItemsSource = subjectNameC11to12c;
                    }
                    else if ( ( selectedClass == "Class 11" || selectedClass == "Class 12" ) & selectedGroup == "Arts" )
                    {
                        SubjectPicker.ItemsSource = subjectNameC11to12a;
                    }
                }
                SubjectPicker.SelectedIndex = 1;
            }
            catch (Exception ex )
            {

            }
            
        }

        private async void SubjectPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchBtn.IsEnabled = true;
            SearchBtn.BackgroundColor = Color.FromHex ( "#4AB3D8" );
            SearchBtn.TextColor = Color.FromHex ( "#FFFFFF" );
            selectedSubject = SubjectPicker.SelectedItem.ToString ();
        }

        public async Task finalizeClassAndSubject()
        {
            try
            {
                if ( CrossConnectivity.Current.IsConnected )
                {
                    string urlT = "https://api.shikkhanobish.com/api/Master/GetInfoByStudentID";
                    HttpClient clientT = new HttpClient ();
                    string jsonDataT = JsonConvert.SerializeObject ( new { StudentID = studentd } );
                    StringContent contentT = new StringContent ( jsonDataT , Encoding.UTF8 , "application/json" );
                    HttpResponseMessage responseT = await clientT.PostAsync ( urlT , contentT ).ConfigureAwait ( false );
                    string resultT = await responseT.Content.ReadAsStringAsync ();
                    var studentinfo = JsonConvert.DeserializeObject<Student> ( resultT );
                    if ( studentinfo.RechargedAmount < 2 && studentinfo.freeMin == 0 )
                    {
                        Navigation.PushPopupAsync ( new PopUpForTextAlert ( "Not Enough Money" , "You do not have enough recharged amount to take tuition" , false ) );
                    }
                    else
                    {
                        string Class = null, sub = null, paper = null;
                        string Sub = null, Paper = null;
                        if ( selectedClass == "Class 6" || selectedClass == "Class 7" || selectedClass == "Class 8" )
                        {
                            Class = "LS";
                            if ( selectedSubject [ 0 ] == 'B' )
                            {
                                sub = "BAN";
                                Sub = "Bangla";
                            }
                            else if ( selectedSubject [ 0 ] == 'E' )
                            {
                                sub = "ENG";
                                Sub = "English";
                            }
                            else if ( selectedSubject [ 0 ] == 'M' )
                            {
                                sub = "MATH";
                                Sub = "Match";
                            }
                            else if ( selectedSubject [ 0 ] == 'I' )
                            {
                                sub = "ICT";
                                Sub = "ICT";
                            }
                            for ( int i = 0; i < selectedSubject.Length; i++ )
                            {
                                if ( selectedSubject [ i ] == ' ' )
                                {
                                    if ( selectedSubject [ i + 1 ] == '1' )
                                    {
                                        paper = "01";
                                        Paper = "First Paper";
                                    }
                                    else if ( selectedSubject [ i + 1 ] == '2' )
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
                        if ( selectedClass == "Class 9" || selectedClass == "Class 10" )
                        {
                            Class = "S";
                            if ( selectedSubject [ 0 ] == 'B' )
                            {
                                sub = "BAN";
                                Sub = "Bangla";
                            }
                            else if ( selectedSubject [ 0 ] == 'E' )
                            {
                                sub = "ENG";
                                Sub = "English";
                            }
                            else if ( selectedSubject [ 0 ] == 'M' )
                            {
                                sub = "MATH";
                                Sub = "Math";
                            }
                            else if ( selectedSubject [ 0 ] == 'I' )
                            {
                                sub = "ICT";
                                Sub = "ICT";
                            }
                            else if ( selectedSubject [ 0 ] == 'P' )
                            {
                                sub = "PHY";
                                Sub = "Physics";
                            }
                            else if ( selectedSubject [ 0 ] == 'C' )
                            {
                                sub = "CHE";
                                Sub = "Chemistry";
                            }
                            else if ( selectedSubject [ 0 ] == 'B' && selectedSubject [ 1 ] == 'i' )
                            {
                                sub = "BIO";
                                Sub = "Biology";
                            }
                            else if ( selectedSubject [ 0 ] == 'H' )
                            {
                                sub = "HMATH";
                                Sub = "Higher Math";
                            }
                            //--------------------------------------
                            else if ( selectedSubject [ 0 ] == 'E' && selectedSubject [ 1 ] == 'c' )
                            {
                                sub = "ECO";
                                Sub = "Economics";
                            }
                            else if ( selectedSubject [ 0 ] == 'A' )
                            {
                                sub = "ACC";
                                Sub = "Accounting";
                            }
                            else if ( selectedSubject [ 0 ] == 'F' )
                            {
                                sub = "FIN";
                                Sub = "Finance";
                            }
                            else if ( selectedSubject [ 0 ] == 'B' && selectedSubject [ 1 ] == 'u' )
                            {
                                sub = "BENT";
                                Sub = "Business & Entrepreneurship";
                            }
                            //------------------------------------
                            else if ( selectedSubject [ 0 ] == 'C' && selectedSubject [ 1 ] == 'a' )
                            {
                                sub = "CRE";
                                Sub = "Career Education";
                            }
                            else if ( selectedSubject [ 0 ] == 'G' )
                            {
                                sub = "GEO";
                                Sub = "Geology";
                            }
                            for ( int i = 0; i < selectedSubject.Length; i++ )
                            {
                                if ( selectedSubject [ i ] == ' ' )
                                {
                                    if ( selectedSubject [ i + 1 ] == '1' )
                                    {
                                        paper = "01";
                                        Paper = "First Paper";
                                    }
                                    else if ( selectedSubject [ i + 1 ] == '2' )
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
                        if ( selectedClass == "Class 11" || selectedClass == "Class 12" )
                        {
                            Class = "HS";
                            if ( selectedSubject [ 0 ] == 'B' )
                            {
                                sub = "BAN";
                                Sub = "Bangla";
                            }
                            else if ( selectedSubject [ 0 ] == 'E' )
                            {
                                sub = "ENG";
                                Sub = "English";
                            }
                            else if ( selectedSubject [ 0 ] == 'M' )
                            {
                                sub = "MATH";
                                Sub = "Math";
                            }
                            else if ( selectedSubject [ 0 ] == 'I' )
                            {
                                sub = "ICT";
                            }
                            else if ( selectedSubject [ 0 ] == 'P' )
                            {
                                sub = "PHY";
                                Sub = "Physics";
                            }
                            else if ( selectedSubject [ 0 ] == 'C' )
                            {
                                sub = "CHE";
                                Sub = "Chemistry";
                            }
                            else if ( selectedSubject [ 0 ] == 'B' && selectedSubject [ 1 ] == 'i' )
                            {
                                sub = "BIO";
                                Sub = "Biology";
                            }
                            else if ( selectedSubject [ 0 ] == 'H' )
                            {
                                sub = "HMATH";
                                Sub = "Higher Math";
                            }
                            //--------------------------------------
                            else if ( selectedSubject [ 0 ] == 'E' && selectedSubject [ 1 ] == 'c' )
                            {
                                sub = "ECO";
                                Sub = "Economics";
                            }
                            else if ( selectedSubject [ 0 ] == 'A' )
                            {
                                sub = "ACC";
                                Sub = "Accounting";
                            }
                            else if ( selectedSubject [ 0 ] == 'F' )
                            {
                                sub = "FIN";
                                Sub = "Finance";
                            }
                            else if ( selectedSubject [ 0 ] == 'S' )
                            {
                                sub = "STAT";
                                Sub = "Statistics";
                            }
                            //------------------------------------
                            else if ( selectedSubject [ 0 ] == 'Z' )
                            {
                                sub = "LOG";
                                Sub = "Logic";
                            }
                            for ( int i = 0; i < selectedSubject.Length; i++ )
                            {
                                if ( selectedSubject [ i ] == ' ' )
                                {
                                    if ( selectedSubject [ i + 1 ] == '1' )
                                    {
                                        paper = "01";
                                        Paper = "First Paper";
                                    }
                                    else if ( selectedSubject [ i + 1 ] == '2' )
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
                        if ( sub != null )
                        {
                            ok = true;
                            transferNow.Class = selectedClass;
                            transferNow.Subject = subject;
                            transferNow.SubjectName = subjectName;                            
                        }
                        else
                        {
                            ok = false;
                        }
                    }

                }
                else
                {
                    MainThread.BeginInvokeOnMainThread ( ( ) =>
                    {
                        Errorlbl.Text = "Check network connection";
                        ok = false;
                    } );
                }
            }
            catch ( Exception ex )
            {
                MainThread.BeginInvokeOnMainThread ( ( ) =>
                {
                    ok = false;
                    Errorlbl.Text = ex.Message;
                } );
            }
        }
        int clicked;

        private async void Button_Clicked(object sender, EventArgs e)
        {
            FilteredTeacher.Clear();
            clicked++;
            if(clicked%2 == 1)
            {
                await finalizeClassAndSubject ();
                if ( ok == true )
                {
                    IsCancled = false;
                    Errorlbl.TextColor = Color.DarkSeaGreen;
                    Errorlbl.Text = "You can click Search Button, to cancle search.";
                    Device.StartTimer ( TimeSpan.FromSeconds ( 1.0 ) , searchAgain );
                }
                else
                {
                    SearchBtn.Text = "Search";
                    IsCancled = true;
                    Errorlbl.Text = "There might be some problem! Search Again";
                }
            }
            else
            {
                SearchBtn.Text = "Search";
                IsCancled = true;
                Errorlbl.Text = "Searching Cancled";
            }
            
            
        }//Search Button

        private bool searchAgain ( )
        {
            if(IsCancled == false)
            {
                if ( FilteredTeacher.Count > 0 )
                {
                    IsCancled = false;
                    Errorlbl.Text = "";
                    SearchBtn.Text = "Search";
                    clicked = 0;
                    MainThread.BeginInvokeOnMainThread ( async ( ) => { await Application.Current.MainPage.Navigation.PushModalAsync ( new SearchedTeacher ( transferNow , FilteredTeacher ) ).ConfigureAwait ( false ); } );
                    return false;
                }
                else
                {
                    searchdontcount++;
                    if ( searchdontcount == 1 )
                    {
                        SearchBtn.Text = "Searching.";
                    }
                    else if ( searchdontcount == 2 )
                    {
                        SearchBtn.Text = "Searching..";
                    }
                    else if ( searchdontcount == 3 )
                    {
                        searchdontcount = 0;
                        SearchBtn.Text = "Searching...";
                    }
                    getTeacher ();
                    return true;
                }
            }
            else
            {
                SearchBtn.Text = "Search";
                return false;
            }
            
            
        }


        public async Task getTeacherID ( )
        {
            string url = "https://api.shikkhanobish.com/api/Master/TeacherIDListFromSubject";
            HttpClient client = new HttpClient ();
            string jsonData = JsonConvert.SerializeObject ( new { Subject = transferNow.Subject } );
            StringContent content = new StringContent ( jsonData , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( true );
            string result = await response.Content.ReadAsStringAsync ().ConfigureAwait ( true );
            TeacherIDListBySearch = JsonConvert.DeserializeObject<List<TeacherID>> ( result );
        }

        public async Task getTeacher ( )
        {
            string urlN = "https://api.shikkhanobish.com/api/Master/GetTeacher";
            HttpClient clientN = new HttpClient ();
            HttpResponseMessage responseN = await clientN.GetAsync ( urlN ).ConfigureAwait ( true );
            string resultN = await responseN.Content.ReadAsStringAsync ().ConfigureAwait ( true );
            TeacherList = JsonConvert.DeserializeObject<List<Teacher>> ( resultN );
            await getTeacherID ().ConfigureAwait(false);
            if( TeacherIDListBySearch.Count > 0)
            {
                getSearchedTeacherInList ();
            }
            
        }

        public void getSearchedTeacherInList ( )
        {
            for ( int i = 0; i < TeacherList.Count; i++ )
            {
                for ( int j = 0; j < TeacherIDListBySearch.Count; j++ )
                {
                    if ( ( TeacherList [ i ].TeacherID == TeacherIDListBySearch [ j ].teacherID ) && TeacherList [ i ].IsActive == 1 )
                    {
                        FilteredTeacher.Add ( TeacherList [ i ] );
                    }
                }
            }
            Perfection ();
        }

        public void Perfection ( )
        {
            Calculate cal = new Calculate ();
            double total, count;
            for ( int i = 0; i < FilteredTeacher.Count; i++ )
            {

                total = FilteredTeacher [ i ].Five_Star * 5 + FilteredTeacher [ i ].Four_Star * 4 + FilteredTeacher [ i ].Three_Star * 3 + FilteredTeacher [ i ].Two_Star * 2 + FilteredTeacher [ i ].One_Star * 1;
                count = FilteredTeacher [ i ].Five_Star + FilteredTeacher [ i ].Four_Star + FilteredTeacher [ i ].Three_Star + FilteredTeacher [ i ].Two_Star + FilteredTeacher [ i ].One_Star;
                if ( FilteredTeacher [ i ].IsOnTuition == 1 )
                {
                    FilteredTeacher [ i ].TeacherStatus = "On Tuition";
                    FilteredTeacher [ i ].TeacherStatusColor = "#939393";
                }
                else if ( FilteredTeacher [ i ].IsActive == 0 )
                {
                    FilteredTeacher [ i ].TeacherStatus = "Offline";
                    FilteredTeacher [ i ].TeacherStatusColor = "#939393";
                }
                else
                {
                    FilteredTeacher [ i ].TeacherStatus = "Call Now ";
                    FilteredTeacher [ i ].TeacherStatusColor = "#43CF56";
                }

                if ( FilteredTeacher [ i ].isFounder == 1 )
                {
                    FilteredTeacher [ i ].FoundingTeacherColor = "#282E58";
                }
                else
                {
                    FilteredTeacher [ i ].FoundingTeacherColor = "#F5F5F5";
                }


                if ( count == 0 )
                {
                    FilteredTeacher [ i ].Avarage = 0;
                }
                else
                {
                    FilteredTeacher [ i ].Avarage = System.Math.Round ( total / count , 2 );
                }

                if ( FilteredTeacher [ i ].Teacher_Rank == "Placement" )
                {
                    FilteredTeacher [ i ].Color = "#B9B9B9";
                }
                else if ( FilteredTeacher [ i ].Teacher_Rank == "Newbie" )
                {
                    FilteredTeacher [ i ].Color = "#F68181";
                }
                else if ( FilteredTeacher [ i ].Teacher_Rank == "Average" )
                {
                    FilteredTeacher [ i ].Color = "#F5C24B";
                }
                else if ( FilteredTeacher [ i ].Teacher_Rank == "Good" )
                {
                    FilteredTeacher [ i ].Color = "#8AF077";
                }
                else if ( FilteredTeacher [ i ].Teacher_Rank == "Veteran" )
                {
                    FilteredTeacher [ i ].Color = "#77CDF0";
                }
                else if ( FilteredTeacher [ i ].Teacher_Rank == "Master" )
                {
                    FilteredTeacher [ i ].Color = "#CA6AF1";
                }
                FilteredTeacher [ i ].Amount = cal.RatingAndCostRange ( FilteredTeacher [ i ].Teacher_Rank , transferNow.ClassCode , transferNow.Teacher.Total_Min , 30 , true );
            }
        }
    }
}