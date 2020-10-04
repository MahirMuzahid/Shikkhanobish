using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
using Shikkhanobish.ContentPages;
using Shikkhanobish.ContentPages.Parents;
using Shikkhanobish.Model;
using Shikkhanobish.ViewModel;
using System;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Shikkhanobish
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private INavigation navigation;
        bool isThereLoggedUser;
        public Student student = new Student ();
        public Teacher teacher = new Teacher ();
        public Parent parent = new Parent();
        public MainPage()
        {
            isThereLoggedUser = false;          
            InitializeComponent ();
            var image = new Image { Source = "loginwindow.png" };
            var vm = new MainPageViewModel ();
            this.BindingContext = vm;
            Automate ();
            getInternalStorageInfo ();

        }
        public async Task getInternalStorageInfo ()
        {
            var username = await SecureStorage.GetAsync( "username" ).ConfigureAwait ( false );
            var password = await SecureStorage.GetAsync ( "password" ).ConfigureAwait ( false ); 
            var usertype = await SecureStorage.GetAsync ( "usertype" ).ConfigureAwait ( false ); 
            var parentcode = await SecureStorage.GetAsync ( "parentCode" ).ConfigureAwait ( false );
            
            if(usertype == "Student" )
            {
                if(username != "" && password != "")
                {
                    isThereLoggedUser = true;
                    await SearchStudent ( username , password ).ConfigureAwait(false);
                }
                else
                {
                    isThereLoggedUser = false;
                }
            }
            else if( usertype == "Teacher" )
            {
                if ( username != "" && password != "" )
                {
                    isThereLoggedUser = true;
                    await SearchTeacher ( username , password ).ConfigureAwait ( false );
                }
                else
                {
                    isThereLoggedUser = false;
                }
            }
            else if( usertype == "Parent" )
            {
                if ( parentcode != "" && password != "" )
                {
                    isThereLoggedUser = true;
                    int pc  = Int32.Parse ( parentcode );
                    await SearchParent ( password , pc ).ConfigureAwait ( false );
                }
                else
                {
                    isThereLoggedUser = false;
                }
            }
        }
        public void Automate()
        {
            Username.Completed += (object sender, EventArgs e) =>
            {
                Password.Focus();
            };
            Password.Completed += (object sender, EventArgs e) =>
            {
                var vm = new MainPageViewModel();
                vm.Login.Execute(null);
            };
        }

        protected override bool OnBackButtonPressed()
        {
            System.Environment.Exit ( 0 );
            return true;
        }


        private void Button_Clicked(object sender, EventArgs e)
        {
            Loginbtn.IsEnabled = false;
            if (Loginbtn.Text != "Wait")
            {
                Loginbtn.IsEnabled = true;
            }
        }

        private async void Button_Clicked_1 ( object sender , EventArgs e )
        {
            await Application.Current.MainPage.Navigation.PushPopupAsync ( new PopUpForLoginParents () ).ConfigureAwait ( false );
        }

        public async Task SearchTeacher ( string UserName ,string Password )
        {
            string urlT = "https://api.shikkhanobish.com/api/Master/GetInfoByLoginTeacher";
            HttpClient clientT = new HttpClient ();
            string jsonDataT = JsonConvert.SerializeObject ( new { UserName = UserName , Password = Password } );
            StringContent contentT = new StringContent ( jsonDataT , Encoding.UTF8 , "application/json" );
            HttpResponseMessage responseT = await clientT.PostAsync ( urlT , contentT ).ConfigureAwait ( false );
            string resultT = await responseT.Content.ReadAsStringAsync ();
            var teacher = JsonConvert.DeserializeObject<Teacher> ( resultT );
            
            MainThread.BeginInvokeOnMainThread ( ( )  => { Application.Current.MainPage.Navigation.PushModalAsync ( new TeacherProfile ( teacher ) ).ConfigureAwait ( false ) ; } );
        }
        public async Task SearchStudent ( string UserName , string Password )
        {
            string url = "https://api.shikkhanobish.com/api/Master/GetInfoByLogin";
            HttpClient client = new HttpClient ();
            string jsonData = JsonConvert.SerializeObject ( new { UserName = UserName , Password = Password } );
            StringContent content = new StringContent ( jsonData , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( true );
            string result = await response.Content.ReadAsStringAsync ();
            var student = JsonConvert.DeserializeObject<Student> ( result );
            if ( student.IsPending == 1 )
            {
                string urlT = "https://api.shikkhanobish.com/api/Master/GetPending";
                HttpClient clientT = new HttpClient ();
                string jsonDataT = JsonConvert.SerializeObject ( new { StudentID = student.StudentID } );
                StringContent contentT = new StringContent ( jsonDataT , Encoding.UTF8 , "application/json" );
                HttpResponseMessage responseT = await clientT.PostAsync ( urlT , contentT ).ConfigureAwait ( false );
                string resultT = await responseT.Content.ReadAsStringAsync ();
                var pendningRating = JsonConvert.DeserializeObject<IsPending> ( resultT );
                urlT = "https://api.shikkhanobish.com/api/Master/GetInfoByTeacherID";
                jsonDataT = JsonConvert.SerializeObject ( new { TeacherID = pendningRating.TeacherID } );
                contentT = new StringContent ( jsonDataT , Encoding.UTF8 , "application/json" );
                responseT = await clientT.PostAsync ( urlT , contentT ).ConfigureAwait ( false );
                resultT = await responseT.Content.ReadAsStringAsync ();
                var teacher = JsonConvert.DeserializeObject<Teacher> ( resultT );
                TransferInfo trns = new TransferInfo ();
                trns.Student = student;
                trns.Class = pendningRating.Class;
                trns.Subject = pendningRating.Subject;
                trns.StudentCost = pendningRating.Cost;
                trns.StudyTimeInAPp = pendningRating.Time;
                trns.Teacher = teacher;
                
                MainThread.BeginInvokeOnMainThread ( ( ) => { Application.Current.MainPage.Navigation.PushModalAsync ( new RatingPage ( trns , false ) ).ConfigureAwait ( false ); } );
            }
            else
            {
                MainThread.BeginInvokeOnMainThread ( ( ) => { Application.Current.MainPage.Navigation.PushModalAsync ( new StudentProfile ( student ) ).ConfigureAwait ( false ); } );
            }
        }
                

        public async Task SearchParent(string password, int parentCode)
        {
            string url = "https://api.shikkhanobish.com/api/Master/GetParentInfo";
            HttpClient client = new HttpClient ();
            string jsonData = JsonConvert.SerializeObject ( new { ParentID = parentCode , Password = password } );
            StringContent content = new StringContent ( jsonData , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( true );
            var result = await response.Content.ReadAsStringAsync ().ConfigureAwait ( true );
            var parent = JsonConvert.DeserializeObject<Parent> ( result );
            MainThread.BeginInvokeOnMainThread ( async ( ) => { await Application.Current.MainPage.Navigation.PushModalAsync ( new ParentsProfile ( parent ) ).ConfigureAwait ( false ); } );
        }
    }
}