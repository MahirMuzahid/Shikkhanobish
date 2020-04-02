using Newtonsoft.Json;
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
        public ForgotPasswordWindow()
        {
            InitializeComponent();
            rubtn.IsEnabled = false;
            rpbtn.IsEnabled = false;
        }

        private async void Sentbtn_Clicked(object sender, EventArgs e)
        {

            if (Sentbtn.Text == "Verify")
            {
                if (VerificationNumber.ToString() == Phonenumber)
                {
                    //
                }
            }
            else
            {
                string vrtext = "{ \"status\":\"Sms sent successfully\"}";
                Phonenumber = PlaceholderEntry.Text;
                string url = "https://api.shikkhanobish.com/api/Master/RecoverInfoStudent";
                HttpClient client = new HttpClient();
                string jsonData = JsonConvert.SerializeObject(new { phonenumber = Phonenumber });
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait(true);
                string result = await response.Content.ReadAsStringAsync();
                student = JsonConvert.DeserializeObject<Student>(result);
                if (student.UserName != null)
                {
                    VerificationNumber = random.Next(1000, 9999);
                    string text = "Your Username or Password reset verification code is: " + VerificationNumber;
                    string urlv = "https://www.bdgosms.com/send/?req=out&apikey=bdgov23fNg7nWmb9alTIXYSMD16GewhLBH&numb=" + Phonenumber + "&sms=" + text;
                    HttpClient clientv = new HttpClient();
                    HttpResponseMessage responsev = await clientv.GetAsync(urlv);
                    string resultv = await responsev.Content.ReadAsStringAsync().ConfigureAwait(true);
                    if (resultv[0] == '{')
                    {
                        Sentbtn.Text = "Verify";
                        PlaceholderEntry.Text = "Enter 4 Digit Code";
                        rubtn.IsEnabled = true;
                        rpbtn.IsEnabled = true;
                        rpbtn.BackgroundColor = Color.FromHex("#BB87FF");
                        rubtn.BackgroundColor = Color.FromHex("#BB87FF");
                    }
                    else
                    {
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
            }
        }
    }
}