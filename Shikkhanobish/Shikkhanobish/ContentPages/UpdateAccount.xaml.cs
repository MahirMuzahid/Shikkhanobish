using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shikkhanobish
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateAccount : ContentPage
    {
        public Student Student;
        public Teacher Teacher;
        public bool isstudent;
        bool isUpdated;
        int error;
        public UpdateAccount(Student student, Teacher t, bool IsStudent)
        {
            InitializeComponent();
            isUpdated = false;
            Student = student;
            Teacher = t;
            isstudent = IsStudent;
            error = 0;
        }

        string s1, s2, s3, s4, s5, s6,s7;
        private async void Button_Clicked(object sender, EventArgs e)
        {
            if ( upbtn.Text == "Go Profile" )
            {
                go ();
            }
            else
            {
                try
                {
                    if ( CrossConnectivity.Current.IsConnected )
                    {

                        if ( isstudent == true )
                        {
                            if ( NameEntry.Text == "" || NameEntry.Text == null )
                            {
                                s1 = Student.Name;
                            }
                            else
                            {
                                s1 = NameEntry.Text;
                            }
                            if ( AgeEntry.Text == "" || AgeEntry.Text == null )
                            {
                                s2 = "" + Student.Age;
                            }
                            else
                            {
                                s2 = AgeEntry.Text;
                            }
                            if ( ClassEntry.Text == "" || ClassEntry.Text == null )
                            {
                                s3 = Student.Class;
                            }
                            else
                            {
                                s3 = ClassEntry.Text;
                            }
                            if ( InstEntry.Text == "" || InstEntry.Text == null )
                            {
                                s4 = Student.InstitutionName;
                            }
                            else
                            {
                                s4 = InstEntry.Text;
                            }
                            if ( pnentry.Text == "" || pnentry.Text == null )
                            {
                                s7 = Student.PhoneNumber;
                            }
                            else
                            {
                                if ( pnentry.Text.All ( char.IsDigit ) && pnentry.Text.Length == 11 )
                                {
                                    s7 = InstEntry.Text;
                                }
                                else
                                {
                                    error = 1;
                                }

                            }
                            if ( UserEntry.Text == "" || UserEntry.Text == null )
                            {
                                s5 = Student.UserName;
                            }
                            else
                            {
                                if ( UserEntry.Text.Contains ( " " ) )
                                {
                                    error = 3;

                                }
                                else
                                {
                                    s5 = UserEntry.Text;
                                }
                            }
                            if ( PassEntry.Text == "" || PassEntry.Text == null )
                            {
                                s6 = Student.Password;
                            }
                            else
                            {
                                if ( PassEntry.Text.Length > 6 && PassEntry.Text.Any ( char.IsUpper ) )
                                {
                                    s6 = PassEntry.Text;
                                }
                                else
                                {
                                    error = 2;
                                }

                            }
                            if ( CurPassEntry.Text == Student.Password )
                            {
                                if ( error == 0 )
                                {
                                    string url = "https://api.shikkhanobish.com/api/Master/UpdateStudentInfo";
                                    HttpClient client = new HttpClient ();
                                    string jsonData = JsonConvert.SerializeObject ( new { StudentID = Student.StudentID , Name = s1 , Age = s2 , Class = s3 , PhoneNumber = s7 , InstitutionName = s4 , UserName = s5 , Password = s6 } );
                                    StringContent content = new StringContent ( jsonData , Encoding.UTF8 , "application/json" );
                                    HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( true );
                                    string result = await response.Content.ReadAsStringAsync ();
                                    var r = JsonConvert.DeserializeObject<Response> ( result );

                                    isUpdated = true;
                                    Errortxt.Text = "Update Done";
                                    upbtn.Text = "Go Profile";
                                }
                                else
                                {
                                    if ( error == 1 )
                                    {
                                        Errortxt.Text = "Invalid Password. Password must contain one Upper Case Character and a Digit";
                                    }
                                    if ( error == 2 )
                                    {
                                        Errortxt.Text = "Invalid Phonenumber";
                                    }
                                    if ( error == 3 )
                                    {
                                        Errortxt.Text = "Username can't contain space";
                                    }
                                }

                            }
                            else
                            {
                                Errortxt.Text = "Password dosen't match";
                            }
                        }
                        if ( isstudent == false )
                        {
                            if ( NameEntry.Text == "" || NameEntry.Text == null )
                            {
                                s1 = Teacher.TeacherName;
                            }
                            else
                            {
                                s1 = NameEntry.Text;
                            }
                            if ( AgeEntry.Text == "" || AgeEntry.Text == null )
                            {
                                s2 = "" + Teacher.Age;
                            }
                            else
                            {
                                s2 = AgeEntry.Text;
                            }
                            if ( ClassEntry.Text == "" || ClassEntry.Text == null )
                            {
                                s3 = Teacher.Class;
                            }
                            else
                            {
                                s3 = ClassEntry.Text;
                            }
                            if ( InstEntry.Text == "" || InstEntry.Text == null )
                            {
                                s4 = Teacher.InstitutionName;
                            }
                            else
                            {
                                s4 = InstEntry.Text;
                            }
                            if ( pnentry.Text == "" || pnentry.Text == null )
                            {
                                s7 = Teacher.PhoneNumber;
                            }
                            else
                            {
                                if ( pnentry.Text.All ( char.IsDigit ) && pnentry.Text.Length == 11 )
                                {
                                    s7 = InstEntry.Text;
                                }
                                else
                                {
                                    error = 1;
                                }

                            }
                            if ( UserEntry.Text == "" || UserEntry.Text == null )
                            {
                                s5 = Teacher.UserName;
                            }
                            else
                            {
                                if ( UserEntry.Text.Contains ( " " ) )
                                {
                                    error = 3;

                                }
                                else
                                {
                                    s5 = UserEntry.Text;
                                    await SecureStorage.SetAsync ( "username" , s5 ).ConfigureAwait ( false );
                                    
                                }

                            }
                            if ( PassEntry.Text == "" || PassEntry.Text == null )
                            {
                                s6 = Teacher.Password;
                            }
                            else
                            {
                                if ( PassEntry.Text.Length > 6 && PassEntry.Text.Any ( char.IsUpper ) )
                                {
                                    s6 = PassEntry.Text;
                                    await SecureStorage.SetAsync ( "password" , s6 ).ConfigureAwait ( false );
                                }
                                else
                                {
                                    error = 2;
                                }

                            }
                            if ( CurPassEntry.Text == Teacher.Password )
                            {
                                if ( error == 0 )
                                {
                                    string url = "https://api.shikkhanobish.com/api/Master/UpdateTeacherInfo";
                                    HttpClient client = new HttpClient ();
                                    string jsonData = JsonConvert.SerializeObject ( new { TeacherID = Teacher.TeacherID , TeacherName = s1 , Age = s2 , Class = s3 , PhoneNumber = s7 , InstitutionName = s4 , UserName = s5 , Password = s6 } );
                                    StringContent content = new StringContent ( jsonData , Encoding.UTF8 , "application/json" );
                                    HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( true );
                                    string result = await response.Content.ReadAsStringAsync ();
                                    var r = JsonConvert.DeserializeObject<Response> ( result );

                                    isUpdated = true;
                                    Errortxt.Text = "Update Done";
                                    upbtn.Text = "Go Profile";
                                }
                                else
                                {
                                    if ( error == 1 )
                                    {
                                        Errortxt.Text = "Invalid Password. Password must contain one Upper Case Character and a Digit";
                                    }
                                    if ( error == 2 )
                                    {
                                        Errortxt.Text = "Invalid Phonenumber";
                                    }
                                }

                            }
                            else
                            {
                                Errortxt.Text = "Password dosen't match";
                            }
                        }

                    }
                    else
                    {
                        Errortxt.Text = "Check network connection";
                    }
                }
                catch
                {
                    Errortxt.Text = "Check network connection";
                }
            }


        }

        protected override bool OnBackButtonPressed ( )
        {
            if(isUpdated == true)
            {
                go ();


            }
            else
            {
                Navigation.PopModalAsync ();
            }
            return true;
        }

        public async void go()
        {
            if ( isstudent == true )
            {
                string url = "https://api.shikkhanobish.com/api/Master/GetInfoByLogin";
                HttpClient client = new HttpClient ();
                string jsonData = JsonConvert.SerializeObject ( new { UserName = s5 , Password = s6 } );
                StringContent content = new StringContent ( jsonData , Encoding.UTF8 , "application/json" );
                HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( true );
                string result = await response.Content.ReadAsStringAsync ();
                var s = JsonConvert.DeserializeObject<Student> ( result );
                MainThread.BeginInvokeOnMainThread ( async ( ) =>
                {
                    await Application.Current.MainPage.Navigation.PushModalAsync ( new StudentProfile ( s ) ).ConfigureAwait ( false );
                } );
            }
            else if ( isstudent == false )
            {
                string urlT = "https://api.shikkhanobish.com/api/Master/GetInfoByLoginTeacher";
                HttpClient clientT = new HttpClient ();
                string jsonDataT = JsonConvert.SerializeObject ( new { UserName = s5 , Password = s6 } );
                StringContent contentT = new StringContent ( jsonDataT , Encoding.UTF8 , "application/json" );
                HttpResponseMessage responseT = await clientT.PostAsync ( urlT , contentT ).ConfigureAwait ( false );
                string resultT = await responseT.Content.ReadAsStringAsync ();
                var t = JsonConvert.DeserializeObject<Teacher> ( resultT );
                MainThread.BeginInvokeOnMainThread ( async ( ) =>
                {
                    await Application.Current.MainPage.Navigation.PushModalAsync ( new TeacherProfile ( t ) ).ConfigureAwait ( false );
                } );
            }
        }
    }
}