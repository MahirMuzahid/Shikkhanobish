using Newtonsoft.Json;
using Shikkhanobish.ContentPages;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public VerifyPhonenumber(Student student , int teacherorstudent)
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
            int RecevierNumber = int.Parse(studentm.PhoneNumber);
            int VerificationNumber = random.Next(1000,9999);
            string text = "";
            if (ts == 0)
            {
                text = "Your Verification Number From Shikkhanobish Student Registration is: " + VerificationNumber;
            }
            if(ts == 1)
            {
                text = "Your Verification Number From Shikkhanobish Teacher Registration is: " + VerificationNumber;
            }
            string url = "https://www.bdgosms.com/send/?req=out&apikey=bdgov23fNg7nWmb9alTIXYSMD16GewhLBH&numb=0" + RecevierNumber + "&sms=" + text;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            string result = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            Result = result;
            vrNumber = VerificationNumber;
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new StudentProfile(studentm)).ConfigureAwait(true);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new GetInstIDandRules(studentm)).ConfigureAwait(true);
            if (Result == "{ \"status\":\"Sms sent successfully\"}")
            {
                if(codeEntry.Text ==  vrNumber.ToString() )
                {
                    if(ts == 0)
                    {
                        await Application.Current.MainPage.Navigation.PushModalAsync(new StudentProfile(studentm)).ConfigureAwait(true);
                    }
                    else if(ts == 1)
                    {
                        await Application.Current.MainPage.Navigation.PushModalAsync(new GetInstIDandRules(studentm)).ConfigureAwait(true);
                    }
                    
                }
                else
                {
                    Msglbl.Text = "Code doesn't match! Try again.";
                }
            }
            else if(codeEntry.Text == null)
            {
                Msglbl.Text = "Enter the code";
            }
            
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            VerifyUserAsync();
            Msglbl.Text = "Code sent again!";
        }
    }
}