using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Shikkhanobish.Model;
using Shikkhanobish.ViewModel;
using Xamarin.Forms.OpenTok.Service;
using System.Net.Http;
using Rg.Plugins.Popup.Extensions;

namespace Shikkhanobish.ContentPages
{
    [XamlCompilation ( XamlCompilationOptions.Compile )]
    public partial class PopUpForSelectedTeacher : PopupPage
    {
        private TransferInfo Info;
        private void Button_Clicked ( object sender , EventArgs e )
        {
            GetKeys ();
            checkSession ();
        }

        
        public PopUpForSelectedTeacher ( TransferInfo info)
        {
            InitializeComponent ();
            Info = info;
            tnLbl.Text = Info.Teacher.TeacherName;
            clLbl.Text = Info.Class;
            subLbl.Text = Info.SubjectName;
            ctLbl.Text = "Cost: " + Info.Teacher.Amount + " taka/min";
        }


        public async void checkSession ( )
        {
            if ( !CrossOpenTok.Current.TryStartSession () )
            {
                return;
            }
            Navigation.PopPopupAsync( );
            await Application.Current.MainPage.Navigation.PushModalAsync ( new TutionPage ( Info ) ).ConfigureAwait ( true );
        }

        protected async void GetKeys ( )
        {
            using ( var client = new HttpClient () )
            {
                try
                {
                    //var resp = await client.GetAsync(Config.KeysUrl);
                    //var json = await resp.Content.ReadAsStringAsync();
                    //var keys = JsonConvert.DeserializeObject<Keys>(json);

                    CrossOpenTok.Current.ApiKey = "46485492";// keys.ApiKey;
                    CrossOpenTok.Current.SessionId = "1_MX40NjQ4NTQ5Mn5-MTU4NDU2NDI5NTQ1OX52VEZPZ1dDbDM3U1Y5MmtpYnNjMXdYOUZ-fg";//keys.SessionId;
                    CrossOpenTok.Current.UserToken = "T1==cGFydG5lcl9pZD00NjQ4NTQ5MiZzaWc9NDJiNjg0MGEyM2ZiZjIxMjM5MzkzMGRjMjkyZTRjZmZmYzU2ZjRkMzpzZXNzaW9uX2lkPTFfTVg0ME5qUTROVFE1TW41LU1UVTRORFUyTkRJNU5UUTFPWDUyVkVaUFoxZERiRE0zVTFZNU1tdHBZbk5qTVhkWU9VWi1mZyZjcmVhdGVfdGltZT0xNTg0NTY0MzA5Jm5vbmNlPTAuNTI1NzE0OTA3MjM2Mjc2OCZyb2xlPXB1Ymxpc2hlciZleHBpcmVfdGltZT0xNTg3MTU2MzA1JmluaXRpYWxfbGF5b3V0X2NsYXNzX2xpc3Q9";//keys.Token;
                }
                catch ( Exception ex )
                {
                    // await MainPage.DisplayAlert(null, "MAKE SURE YOU SET API URL FOR RETRIEVING NECESSARY KEYS (Config.cs) OR YOU MAY HARDCODE THEM.", "GOT IT");
                }
            }
            //CrossOpenTok.Current.Error += (m) => TakeTuition.DisplayAlert("ERROR", m, "OK");
        }
    }
}