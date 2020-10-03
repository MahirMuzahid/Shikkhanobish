using Newtonsoft.Json;
using Shikkhanobish.Model;
using System;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Extensions;
using Shikkhanobish.ContentPages;
using Plugin.Connectivity;
using Shikkhanobish.ContentPages.Common;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace Shikkhanobish
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentProfile : MasterDetailPage
    {
        public int StudentID;
        private Student _Student;


        public StudentProfile( Student student )
        {
            StudentID = student.StudentID;
            _Student = student;
            this.IsPresented = false;           
            InitializeComponent ();
            BindingContext = new StudentProfileVideoModel(_Student);
            SetInfoInInternalStorage ( student.UserName , student.Password , "Student" , 0 );
            //GetPremiumStudent(student.StudentID);
        }
        public async Task SetInfoInInternalStorage ( string username , string password , string usertype , int parentCode )
        {
            await SecureStorage.SetAsync ( "username" , username ).ConfigureAwait ( false );
            await SecureStorage.SetAsync ( "password" , password ).ConfigureAwait ( false );
            await SecureStorage.SetAsync ( "usertype" , usertype ).ConfigureAwait ( false );
            await SecureStorage.SetAsync ( "parentCode" , "" + parentCode ).ConfigureAwait ( false );
        }

        protected override bool OnBackButtonPressed ( )
        {
            Navigation.PushPopupAsync ( new PopUpForTextAlert ( "" , "" , true ) );
            return true;
        }

        private async void Button_Clicked_4(object sender, EventArgs e)
        {
            SecureStorage.RemoveAll();
            await Application.Current.MainPage.Navigation.PushModalAsync(new MainPage()).ConfigureAwait( false );
        }

        private async void Button_Clicked_5(object sender, EventArgs e)
        {
            try
            {
                if ( CrossConnectivity.Current.IsConnected )
                {
                    await Application.Current.MainPage.Navigation.PushModalAsync ( new StudentHistory ( _Student.StudentID ) ).ConfigureAwait ( false );
                }
            }
            catch
            {

            }
                    
        }

        private async void Button_Clicked_6(object sender, EventArgs e)
        {
            Teacher t = new Teacher();
            await Application.Current.MainPage.Navigation.PushModalAsync(new UpdateAccount(_Student, t, true)).ConfigureAwait( false );
        }

        private async void Button_Clicked_7(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new TakeTuition(StudentID, _Student.Name, _Student.UserName, _Student.Password, _Student.RechargedAmount)).ConfigureAwait( false );
        }
        private async void Button_Clicked_8 ( object sender , EventArgs e )
        {        
            await Application.Current.MainPage.Navigation.PushModalAsync ( new Balance(_Student) ).ConfigureAwait ( false );
        }

        private void Button_Clicked ( object sender , EventArgs e )
        {
            this.IsPresented = true;
        }

        private async void Button_Clicked_1 ( object sender , EventArgs e )
        {
            try
            {
                if ( CrossConnectivity.Current.IsConnected )
                {
                    Navigation.PushPopupAsync ( new PopUpForParentCode ( _Student.Password , _Student.ParentCode ) );
                }
            }
            catch
            {

            }
            
                 
        }
    }
}