using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Plugin.Connectivity;
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
        string us, pss;
        bool isUpdated;
        public UpdateAccount(Student student, Teacher t, bool IsStudent)
        {
            InitializeComponent();
            isUpdated = false;
            Student = student;
            Teacher = t;
            isstudent = IsStudent;
        }

        string s1, s2, s3, s4, s5, s6;
        private async void Button_Clicked(object sender, EventArgs e)
        {
            go ();
            try
            {
                if ( CrossConnectivity.Current.IsConnected )
                {
                    if ( isstudent == true )
                    {
                        if ( CurPassEntry.Text == Student.Password )
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
                            if ( UserEntry.Text == "" || UserEntry.Text == null )
                            {
                                s5 = Student.UserName;
                            }
                            else
                            {
                                s5 = UserEntry.Text;
                            }
                            if ( PassEntry.Text == "" || PassEntry.Text == null )
                            {
                                s6 = Student.Password;
                            }
                            else
                            {
                                s6 = PassEntry.Text;
                            }
                            string url = "https://api.shikkhanobish.com/api/Master/UpdateStudentInfo";
                            HttpClient client = new HttpClient ();
                            string jsonData = JsonConvert.SerializeObject ( new { StudentID = Student.StudentID , Name = s1 , Age = s2 , Class = s3 , InstitutionName = s4 , UserName = s5 , Password = s6 } );
                            StringContent content = new StringContent ( jsonData , Encoding.UTF8 , "application/json" );
                            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( true );
                            string result = await response.Content.ReadAsStringAsync ();
                            var r = JsonConvert.DeserializeObject<Response> ( result );

                            //Api call to update user info
                            isUpdated = true;
                            Errortxt.Text = "Update Done";
                            upbtn.Text = "Go Profile";
                        }
                        else
                        {
                            Errortxt.Text = "Password dosen't match";
                        }
                    }
                    if ( isstudent == false )
                    {
                        if ( CurPassEntry.Text == Teacher.Password )
                        {
                            string url = "https://api.shikkhanobish.com/api/Master/UpdateTeacherInfo";
                            HttpClient client = new HttpClient ();
                            string jsonData = JsonConvert.SerializeObject ( new { TeacherID = Teacher.TeacherID , TeacherName = NameEntry.Text , Age = AgeEntry.Text , Class = ClassEntry.Text , InstitutionName = InstEntry.Text , UserName = UserEntry.Text , Password = PassEntry.Text } );
                            StringContent content = new StringContent ( jsonData , Encoding.UTF8 , "application/json" );
                            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( true );
                            string result = await response.Content.ReadAsStringAsync ();
                            var r = JsonConvert.DeserializeObject<Response> ( result );

                            //Api call to update user info
                            isUpdated = true;
                            Errortxt.Text = "Update Done";
                            upbtn.Text = "Go Profile";
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
            if ( upbtn.Text == "Go Profile" && isstudent == true )
            {
                string url = "https://api.shikkhanobish.com/api/Master/GetInfoByLogin";
                HttpClient client = new HttpClient ();
                string jsonData = JsonConvert.SerializeObject ( new { UserName = s5 , Password = s6 } );
                StringContent content = new StringContent ( jsonData , Encoding.UTF8 , "application/json" );
                HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( true );
                string result = await response.Content.ReadAsStringAsync ();
                var s = JsonConvert.DeserializeObject<Student> ( result );
                await Application.Current.MainPage.Navigation.PushModalAsync ( new StudentProfile ( s ) ).ConfigureAwait ( false );
            }
            else if ( upbtn.Text == "Go Profile" && isstudent == false )
            {
                string urlT = "https://api.shikkhanobish.com/api/Master/GetInfoByLoginTeacher";
                HttpClient clientT = new HttpClient ();
                string jsonDataT = JsonConvert.SerializeObject ( new { UserName = s5 , Password = s6 } );
                StringContent contentT = new StringContent ( jsonDataT , Encoding.UTF8 , "application/json" );
                HttpResponseMessage responseT = await clientT.PostAsync ( urlT , contentT ).ConfigureAwait ( false );
                string resultT = await responseT.Content.ReadAsStringAsync ();
                var t = JsonConvert.DeserializeObject<Teacher> ( resultT );
                await Application.Current.MainPage.Navigation.PushModalAsync ( new TeacherProfile ( t ) ).ConfigureAwait ( false );
            }
        }
    }
}