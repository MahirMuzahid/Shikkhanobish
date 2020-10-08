using Newtonsoft.Json;
using Shikkhanobish.ContentPages;
using Shikkhanobish.Model;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shikkhanobish
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerifyPhonenumber : ContentPage
    {
        public Student studentm = new Student();
        public string Result;
        public int vrNumber, ts;
        Massage ms = new Massage ();

        public VerifyPhonenumber(Student student, int teacherorstudent)
        {
            InitializeComponent();
            studentm = student;
            ts = teacherorstudent;
            VerifyUserAsync();
        }

        public async Task VerifyUserAsync()
        {
            Numberlbl.Text = "" + studentm.PhoneNumber;
            Random random = new Random();
            string Password = "Biggan12345";
            int VerificationNumber = random.Next(1000, 9999);
            string text = "";
            if (ts == 0)
            {
                text = "Your Verification Number From Shikkhanobish Student Registration is: " + VerificationNumber;
            }
            if (ts == 1)
            {
                text = "Your Verification Number From Shikkhanobish Teacher Registration is: " + VerificationNumber;
            }
            await ms.SendMsg ( studentm.PhoneNumber  , text ).ConfigureAwait(false);
            vrNumber = VerificationNumber;
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {

            if ( ts == 0 )
            {
                
                await Application.Current.MainPage.Navigation.PushModalAsync ( new StudentProfile ( studentm ) ).ConfigureAwait ( false );
            }
            if ( ts == 1 )
            {
                OldStToNewSt chng = new OldStToNewSt ();
                await Application.Current.MainPage.Navigation.PushModalAsync ( new GetInstIDandRules ( chng.S_TO_Sc(studentm) ) ).ConfigureAwait ( false );
            }
            
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (ms.isSent == true)
            {
                if (codeEntry.Text == vrNumber.ToString())
                {
                    if (ts == 0)
                    {
                        string url = "https://api.shikkhanobish.com/api/Master/RegisterStudent";
                        HttpClient client = new HttpClient();                      
                        string jsonData = JsonConvert.SerializeObject( new { 
                            UserName = studentm.UserName ,
                            Password = studentm.Password ,
                            PhoneNumber = studentm.PhoneNumber ,
                            Name = studentm.Name ,
                            Age = studentm.Age ,
                            Class = studentm.Class ,
                            InstitutionName = studentm.InstitutionName ,
                            RechargedAmount = 0 ,
                            IsPending = 0 ,
                            TotalTuitionTime = 0 ,
                            TotalTeacherCount = 0 ,
                            AvarageRating = 0 ,
                        } );
                        StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait(true);
                        string result = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                        Response responseData = JsonConvert.DeserializeObject<Response>(result);
                        LoginByUserNameAndPassword ();
                    }
                    else if (ts == 1)
                    {
                        OldStToNewSt chng = new OldStToNewSt ();
                        await Application.Current.MainPage.Navigation.PushModalAsync(new GetInstIDandRules( chng.S_TO_Sc(studentm) ) ).ConfigureAwait( false );
                    }
                }
                else
                {
                    Msglbl.Text = "Code doesn't match! Try again.";
                    //92746
                }
            }
            else if (codeEntry.Text == null)
            {
                Msglbl.Text = "Enter the code";
            }
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            VerifyUserAsync();
            Msglbl.Text = "Code sent again!";
        }

        public async void LoginByUserNameAndPassword ( )
        {
            string url = "https://api.shikkhanobish.com/api/Master/GetInfoByLogin";
            HttpClient client = new HttpClient ();
            string jsonData = JsonConvert.SerializeObject ( new { UserName = studentm.UserName , Password = studentm.Password } );
            StringContent content = new StringContent ( jsonData , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( false );
            string result = await response.Content.ReadAsStringAsync ();
            Student student = JsonConvert.DeserializeObject<Student> ( result );
            MainThread.BeginInvokeOnMainThread ( async ( ) =>
            {
                await Application.Current.MainPage.Navigation.PushModalAsync ( new StudentProfile ( student ) ).ConfigureAwait ( false );
            } );
        }
    }
}