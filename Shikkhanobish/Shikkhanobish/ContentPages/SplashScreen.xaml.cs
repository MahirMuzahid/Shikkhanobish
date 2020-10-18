using Newtonsoft.Json;
using Shikkhanobish.Model;
using Shikkhanobish.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shikkhanobish.ContentPages
{
    [XamlCompilation ( XamlCompilationOptions.Compile )]
    public partial class SplashScreen : ContentPage
    {
        bool isThereLoggedUser;
        public SplashScreen ( )
        {
            InitializeComponent ();
            loadinglbl.Text = "Loading Shikkhanobish...";
            getInternalStorageInfo ();
        }
        public async Task getInternalStorageInfo ( )
        {
            await progress.ProgressTo ( .2 , 300 , Easing.SinIn ).ConfigureAwait ( false );
            var username = await SecureStorage.GetAsync ( "username" ).ConfigureAwait ( false );
            var password = await SecureStorage.GetAsync ( "password" ).ConfigureAwait ( false );
            var usertype = await SecureStorage.GetAsync ( "usertype" ).ConfigureAwait ( false );
            var parentcode = await SecureStorage.GetAsync ( "parentCode" ).ConfigureAwait ( false );
            MainThread.BeginInvokeOnMainThread ( ( ) => {
                loadinglbl.Text = "Checking Logged User...";
            } );
            if ( usertype == "Student" )
            {
                if ( username != "" && password != "" )
                {
                    await progress.ProgressTo ( .5 , 300 , Easing.SinIn ).ConfigureAwait ( false );
                    isThereLoggedUser = true;
                    await SearchStudent ( username , password ).ConfigureAwait ( false );
                }
                else
                {
                    isThereLoggedUser = false;
                }
            }
            else if ( usertype == "Teacher" )
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
            else if ( usertype == "Parent" )
            {
                if ( parentcode != "" && password != "" )
                {
                    isThereLoggedUser = true;
                    int pc = Int32.Parse ( parentcode );
                    await SearchParent ( password , pc ).ConfigureAwait ( false );
                }
                else
                {
                    isThereLoggedUser = false;
                }
            }
            else
            {
                MainThread.BeginInvokeOnMainThread ( async ( ) => {
                    loadinglbl.Text = "Going Main Page...";
                    await progress.ProgressTo ( 1 , 300 , Easing.SinIn ).ConfigureAwait ( false );
                  
                } );
                MainThread.BeginInvokeOnMainThread ( async ( ) => {
                    await Application.Current.MainPage.Navigation.PushModalAsync ( new MainPage () ).ConfigureAwait ( false );
                } );
            }
        }
        public async Task SearchTeacher ( string UserName , string Password )
        {
            string urlT = "https://api.shikkhanobish.com/api/Master/GetInfoByLoginTeacher";
            HttpClient clientT = new HttpClient ();
            string jsonDataT = JsonConvert.SerializeObject ( new { UserName = UserName , Password = Password } );
            StringContent contentT = new StringContent ( jsonDataT , Encoding.UTF8 , "application/json" );
            HttpResponseMessage responseT = await clientT.PostAsync ( urlT , contentT ).ConfigureAwait ( false );
            string resultT = await responseT.Content.ReadAsStringAsync ();
            var teacher = JsonConvert.DeserializeObject<Teacher> ( resultT );
            MainThread.BeginInvokeOnMainThread ( ( ) => {
                loadinglbl.Text = "Logging As "+ teacher.TeacherName + "..." ;
            } );
            StaticPageForOnSleep.isStudent = false;
            MainThread.BeginInvokeOnMainThread ( async ( ) => {
                await progress.ProgressTo ( 1 , 300 , Easing.SinIn ).ConfigureAwait ( false );
            } );
            MainThread.BeginInvokeOnMainThread ( ( ) => { Application.Current.MainPage.Navigation.PushModalAsync ( new TeacherProfile ( teacher ) ).ConfigureAwait ( false ); } );
        }
        public async Task SearchStudent ( string UserName , string Password )
        {
            string url = "https://api.shikkhanobish.com/api/Master/GetInfoByLogin";
            HttpClient client = new HttpClient ();
            string jsonData = JsonConvert.SerializeObject ( new { UserName = UserName , Password = Password } );
            StringContent content = new StringContent ( jsonData , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( true );
            string result = await response.Content.ReadAsStringAsync ();
            var studentcl = JsonConvert.DeserializeObject<StudentClass> ( result );
            OldStToNewSt convert = new OldStToNewSt ();
            var student = convert.Sc_TO_S ( studentcl );
            MainThread.BeginInvokeOnMainThread ( ( ) => {
                loadinglbl.Text = "Logging As " + student.Name + "...";
            } );
            if ( student.IsPending == 1 )
            {
                await progress.ProgressTo ( .7 , 300 , Easing.SinIn ).ConfigureAwait ( false );
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
                StaticPageForOnSleep.isStudent = true;
                MainThread.BeginInvokeOnMainThread ( ( ) => { Application.Current.MainPage.Navigation.PushModalAsync ( new RatingPage ( trns , false ) ).ConfigureAwait ( false ); } );
            }
            else
            {
                StaticPageForOnSleep.isStudent = true;
                MainThread.BeginInvokeOnMainThread ( async ( ) =>
                {
                    await progress.ProgressTo ( 1 , 300 , Easing.SinIn ).ConfigureAwait ( false );
                } );
                MainThread.BeginInvokeOnMainThread ( ( ) => { Application.Current.MainPage.Navigation.PushModalAsync ( new StudentProfile ( student ) ).ConfigureAwait ( false ); } );
            }
        }


        public async Task SearchParent ( string password , int parentCode )
        {
            string url = "https://api.shikkhanobish.com/api/Master/GetParentInfo";
            HttpClient client = new HttpClient ();
            string jsonData = JsonConvert.SerializeObject ( new { ParentID = parentCode , Password = password } );
            StringContent content = new StringContent ( jsonData , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( true );
            var result = await response.Content.ReadAsStringAsync ().ConfigureAwait ( true );
            var parent = JsonConvert.DeserializeObject<Parent> ( result );
            StaticPageForOnSleep.isParent = true;
            MainThread.BeginInvokeOnMainThread ( async ( ) =>
            {
                await progress.ProgressTo ( 1 , 300 , Easing.SinIn ).ConfigureAwait ( false );
                loadinglbl.Text = "Logging As " + parent.ParentName + "...";
            } );
            MainThread.BeginInvokeOnMainThread ( async ( ) => { await Application.Current.MainPage.Navigation.PushModalAsync ( new ParentsProfile ( parent ) ).ConfigureAwait ( false ); } );
        }
    }
}