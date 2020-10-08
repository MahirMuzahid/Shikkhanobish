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
using System.Collections.Generic;
using Shikkhanobish.ViewModel;
using Shikkhanobish.Interface;
using Plugin.LocalNotification;

namespace Shikkhanobish
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentProfile : MasterDetailPage
    {
        public int StudentID;
        private Student _Student;
        public List<OfferAndVoucherSource> offers;


        public StudentProfile( Student student )
        {
            StudentID = student.StudentID;
            _Student = student;
            this.IsPresented = false;           
            InitializeComponent ();
            BindingContext = new StudentProfileVideoModel(_Student);
            SetInfoInInternalStorage ( student.UserName , student.Password , "Student" , 0 );
            GetVoucherImage ();
           //

            //GetPremiumStudent(student.StudentID);
        }
        public async Task SetInfoInInternalStorage ( string username , string password , string usertype , int parentCode )
        {
            await SecureStorage.SetAsync ( "username" , username ).ConfigureAwait ( false );
            await SecureStorage.SetAsync ( "password" , password ).ConfigureAwait ( false );
            await SecureStorage.SetAsync ( "usertype" , usertype ).ConfigureAwait ( false );
            await SecureStorage.SetAsync ( "parentCode" , "" + parentCode ).ConfigureAwait ( false );
        }

        public async Task GetVoucherImage ()
        {
            string urlT = "https://api.shikkhanobish.com/api/Master/GetVoucherSource";
            HttpClient clientT = new HttpClient ();
            string jsonData = JsonConvert.SerializeObject ( new {  } );
            StringContent content = new StringContent ( jsonData , Encoding.UTF8 , "application/json" );
            HttpResponseMessage responseT = await clientT.PostAsync ( urlT , content ).ConfigureAwait ( false );
            string resultT = await responseT.Content.ReadAsStringAsync ();
            var voucherInfo = JsonConvert.DeserializeObject<List<OfferAndVoucherSource>> ( resultT );
            offers = voucherInfo;
            cvForVoucher.ItemsSource = voucherInfo;
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
                    MainThread.BeginInvokeOnMainThread ( ( ) => { DependencyService.Get<INotification> ().CreateNotification ( "Shikkhanobish" , "This is a notification" ); } );
                }
            }
            catch(Exception ex)
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
            await Application.Current.MainPage.Navigation.PushModalAsync(new TakeTuition(StudentID, _Student.Name, _Student.UserName, _Student.Password, _Student.RechargedAmount, offers ) ).ConfigureAwait( false );
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