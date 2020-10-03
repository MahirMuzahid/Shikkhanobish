using Newtonsoft.Json;
using Shikkhanobish.ContentPages;
using Shikkhanobish.Model;
using System;
using System.Net.Http;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shikkhanobish
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPasswordWindow : ContentPage
    {
        private int VerificationNumber;
        private Random random = new Random();
        private string Phonenumber;
        private string Result = null;
        private Student student = new Student();
        private int studentorTeacher = 0;
        public string Username;

        public ForgotPasswordWindow()
        {
            InitializeComponent();
            rubtn.IsEnabled = false;
            rpbtn.IsEnabled = false;
            Errorlbl.Text = null;
        }

        private async void Sentbtn_Clicked(object sender, EventArgs e)
        {
            if (Sentbtn.Text == "Verify")
            {
                if (VerificationNumber.ToString() == PlaceholderEntry.Text)
                {
                    rubtn.IsEnabled = true;
                    rpbtn.IsEnabled = true;
                    rpbtn.BackgroundColor = Color.FromHex("#40A4DF");
                    rubtn.BackgroundColor = Color.FromHex("#BB87FF");
                    rpbtn.TextColor = Color.White;
                    rubtn.TextColor = Color.White;
                    Errorlbl.Text = null;
                    PlaceholderEntry.IsEnabled = false;
                    PlaceholderEntry.Text = "Code Matched!";
                    Sentbtn.IsEnabled = false;
                }
                else
                {
                    Errorlbl.Text = "Wrong Verification Number";
                }
            }
            else
            {
                Sentbtn.Text = "Sending Code";
                Phonenumber = PlaceholderEntry.Text;
                string url = "https://api.shikkhanobish.com/api/Master/RecoverInfoStudent";
                HttpClient client = new HttpClient();
                string jsonData = JsonConvert.SerializeObject(new { phonenumber = Phonenumber });
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait(false);
                string result = await response.Content.ReadAsStringAsync();
                student = JsonConvert.DeserializeObject<Student>(result);
                Username = student.UserName;
               
                if (student.UserName != null)
                {
                    studentorTeacher = 0;
                    VerificationNumber = random.Next(1000, 9999);
                    string text = "Your Username or Password reset verification code is: " + VerificationNumber;
                    Massage ms = new Massage ();
                    await ms.SendMsg ( Phonenumber , text ).ConfigureAwait(false);
                    if (ms.isSent == true)
                    {
                        MainThread.BeginInvokeOnMainThread ( ( ) => {
                            Sentbtn.Text = "Verify";
                            PlaceholderEntry.Text = null;
                            PlaceholderEntry.Placeholder = "Enter 4 Digit Code";
                        } );
                        
                    }
                    else
                    {
                        MainThread.BeginInvokeOnMainThread ( ( ) => {
                            Sentbtn.Text = "Send";
                            Errorlbl.Text = "Invalid Number. Maybe number is switched off";
                        } );
                    }                  
                }
                else
                {
                    studentorTeacher = 1;
                    Teacher teacher = new Teacher ();
                    string urlt = "https://api.shikkhanobish.com/api/Master/RecoverInfoTeacher";
                    HttpClient clientt = new HttpClient ();
                    string jsonDatat = JsonConvert.SerializeObject ( new { phonenumber = Phonenumber } );
                    StringContent contentt = new StringContent ( jsonDatat , Encoding.UTF8 , "application/json" );
                    HttpResponseMessage responset = await clientt.PostAsync ( urlt , contentt ).ConfigureAwait ( true );
                    string resultt = await responset.Content.ReadAsStringAsync ();
                    teacher = JsonConvert.DeserializeObject<Teacher> ( resultt );
                    if ( teacher.UserName != null )
                    {
                        VerificationNumber = random.Next ( 1000 , 9999 );
                        string text = "Your Username or Password reset verification code is: " + VerificationNumber;
                        string apiKey = "bdgoQKW5OyLe748FUlrBmgCEXZn3oivhuf";
                        Massage ms = new Massage ();
                        await ms.SendMsg ( Phonenumber , text ).ConfigureAwait(false);
                        if ( ms.isSent == true )
                        {
                            MainThread.BeginInvokeOnMainThread ( ( ) => {
                            Sentbtn.Text = "Verify";
                            PlaceholderEntry.Text = null;
                            PlaceholderEntry.Placeholder = "Enter 4 Digit Code";
                            } );
                        }
                        else
                        {
                            MainThread.BeginInvokeOnMainThread ( ( ) => {
                                Sentbtn.Text = "Send";
                                Errorlbl.Text = "Invalid Number. Maybe number is switched off";
                            } );
                        }
                    }
                    else
                    {
                        MainThread.BeginInvokeOnMainThread ( ( ) => {
                            Sentbtn.Text = "Send";
                            Errorlbl.Text = "There is no account with this phone number";
                        } );
                    }

                }
            }
        }

        private async void rubtn_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new ResetInfo(Username, studentorTeacher, 1)).ConfigureAwait(false);
        }

        private async void rpbtn_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new ResetInfo(Username, studentorTeacher, 0)).ConfigureAwait( false );
        }
    }
}