using Newtonsoft.Json;
using Shikkhanobish.ContentPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shikkhanobish
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPasswordWindow : ContentPage
    {
        int VerificationNumber;
        Random random = new Random();
        string Phonenumber;
        string Result = null;
        Student student = new Student();
        int studentorTeacher = 0;
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
                string vrtext = "{ \"status\":\"Sms sent successfully\"}";
                Phonenumber = PlaceholderEntry.Text;
                string url = "https://api.shikkhanobish.com/api/Master/RecoverInfoStudent";
                HttpClient client = new HttpClient();
                string jsonData = JsonConvert.SerializeObject(new { phonenumber = Phonenumber });
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait(true);
                string result = await response.Content.ReadAsStringAsync();
                student = JsonConvert.DeserializeObject<Student>(result);
                Username = student.UserName;
                if (student.UserName != null)
                {
                    studentorTeacher = 0;
                    VerificationNumber = random.Next(1000, 9999);
                    string text = "Your Username or Password reset verification code is: " + VerificationNumber;
                    string urlv = "https://www.bdgosms.com/send/?req=out&apikey=bdgov23fNg7nWmb9alTIXYSMD16GewhLBH&numb=" + Phonenumber + "&sms=" + text;
                    HttpClient clientv = new HttpClient();
                    HttpResponseMessage responsev = await clientv.GetAsync(urlv);
                    string resultv = await responsev.Content.ReadAsStringAsync().ConfigureAwait(true);
                    if (resultv[0] == '{')
                    {
                        Sentbtn.Text = "Verify";
                        PlaceholderEntry.Text = null;
                        PlaceholderEntry.Placeholder = "Enter 4 Digit Code";                        
                    }
                    else
                    {
                        studentorTeacher = 1;
                        Teacher teacher = new Teacher();
                        string urlt = "https://api.shikkhanobish.com/api/Master/RecoverInfoTeacher";
                        HttpClient clientt = new HttpClient();
                        string jsonDatat = JsonConvert.SerializeObject(new { phonenumber = Phonenumber });
                        StringContent contentt = new StringContent(jsonDatat, Encoding.UTF8, "application/json");
                        HttpResponseMessage responset = await clientt.PostAsync(urlt, contentt).ConfigureAwait(true);
                        string resultt = await responset.Content.ReadAsStringAsync();
                        teacher = JsonConvert.DeserializeObject<Teacher>(resultt);
                    }
                }
                else
                {
                    Sentbtn.Text = "Send";
                    Errorlbl.Text = "This number is not registered";
                }
            }
        }
        private async void rubtn_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new ResetInfo(Username,studentorTeacher,1)).ConfigureAwait(true);
        }

        private async void rpbtn_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new ResetInfo(Username, studentorTeacher, 0)).ConfigureAwait(true);
        }
    }
}