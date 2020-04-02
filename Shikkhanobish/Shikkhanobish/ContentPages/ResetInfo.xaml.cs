using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shikkhanobish.ContentPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResetInfo : ContentPage
    {
        string un;
        int st,pu;
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
            if(mainEntry.Text == confirmEntry.Text)
            {
                string urlt = "https://api.shikkhanobish.com/api/Master/SetnewPasswordOrUsername";
                HttpClient clientt = new HttpClient();
                string jsonDatat = JsonConvert.SerializeObject(new { Username = un, IsTeacherorStudent = st, IsPasswordOrUsername = pu, NewpassorUsername = mainEntry.Text });
                StringContent contentt = new StringContent(jsonDatat, Encoding.UTF8, "application/json");
                HttpResponseMessage responset = await clientt.PostAsync(urlt, contentt).ConfigureAwait(true);
                string resultt = await responset.Content.ReadAsStringAsync();
                var r = JsonConvert.DeserializeObject<Response>(resultt);
                if (r.Status == 0)
                {
                    await Application.Current.MainPage.Navigation.PushModalAsync(new MainPage()).ConfigureAwait(true);
                }
                
            }
        }
    }
}