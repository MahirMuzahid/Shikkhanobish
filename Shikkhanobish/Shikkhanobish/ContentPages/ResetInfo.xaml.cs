using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shikkhanobish.ContentPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResetInfo : ContentPage
    {
        private string un;
        private int st, pu;

        public ResetInfo(string Username, int isTteacherOrStudnet, int IsPasswordOrUsername)
        {
            InitializeComponent();
            un = Username;
            st = isTteacherOrStudnet;
            pu = IsPasswordOrUsername;
            mainEntry.Text = "";
            confirmEntry.Text = "";
            if (IsPasswordOrUsername == 1)
            {
                mainEntry.Placeholder = "New Username";
                confirmEntry.Placeholder = "Confirm Username";
            }
            if (IsPasswordOrUsername == 0)
            {
                mainEntry.Placeholder = "New Password";
                confirmEntry.Placeholder = "Confirm Password";
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (pu == 0)
            {
                if (mainEntry.Text.Length > 6 || mainEntry.Text.Any(char.IsUpper) || mainEntry.Text.Any(char.IsDigit))
                {
                    if (mainEntry.Text == confirmEntry.Text)
                    {
                        string urlt = "https://api.shikkhanobish.com/api/Master/SetnewPasswordOrUsername";
                        HttpClient clientt = new HttpClient();
                        string jsonDatat = JsonConvert.SerializeObject(new { Username = un, IsTeacherorStudent = st, IsPasswordOrUsername = pu, NewpassorUsername = mainEntry.Text });
                        StringContent contentt = new StringContent(jsonDatat, Encoding.UTF8, "application/json");
                        HttpResponseMessage responset = await clientt.PostAsync(urlt, contentt).ConfigureAwait( false );
                        string resultt = await responset.Content.ReadAsStringAsync();
                        var r = JsonConvert.DeserializeObject<Response>(resultt);
                        if (r.Status == 0)
                        {
                            await Application.Current.MainPage.Navigation.PushModalAsync(new MainPage()).ConfigureAwait( false );
                        }
                    }
                }
                else
                {
                    Errorblb.Text = "Password should be atleast 6 character and one capital latter and one digit";
                }
            }
            else if (mainEntry.Text != "" && mainEntry.Text == confirmEntry.Text)
            {
                string urlt = "https://api.shikkhanobish.com/api/Master/SetnewPasswordOrUsername";
                HttpClient clientt = new HttpClient();
                string jsonDatat = JsonConvert.SerializeObject(new { Username = un, IsTeacherorStudent = st, IsPasswordOrUsername = pu, NewpassorUsername = mainEntry.Text });
                StringContent contentt = new StringContent(jsonDatat, Encoding.UTF8, "application/json");
                HttpResponseMessage responset = await clientt.PostAsync(urlt, contentt).ConfigureAwait( false );
                string resultt = await responset.Content.ReadAsStringAsync();
                var r = JsonConvert.DeserializeObject<Response>(resultt);
                if (r.Status == 0)
                {
                    await Application.Current.MainPage.Navigation.PushModalAsync(new MainPage()).ConfigureAwait( false );
                }
            }
            else
            {
                Errorblb.Text = "Enter valid Username";
            }
        }
    }
}