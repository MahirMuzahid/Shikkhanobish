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
        private int apiKey;
        private string SessionID, Token;
        private void Button_Clicked ( object sender , EventArgs e )
        {
            callbtn.Text = "Calling teacher...";
            GetKeys ();
            checkSession ();
        }

        
        public PopUpForSelectedTeacher ( TransferInfo info)
        {
            InitializeComponent ();
            callbtn.Text = "Call Teacher";
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
            ConnectToRealTimeApiServer connectRealTimeAPi = new ConnectToRealTimeApiServer ();
            connectRealTimeAPi.ConnectToServer ();
            connectRealTimeAPi.ConnectWithTeacher (apiKey,SessionID,Token,Info.Student.StundentID, Info.Teacher.TeacherID,Info.SubjectName, Info.Class);
            Navigation.PopPopupAsync( );
            await Application.Current.MainPage.Navigation.PushModalAsync ( new TutionPage ( Info ) ).ConfigureAwait ( true );
        }

        protected async void GetKeys ( )
        {
            apiKey = 46485492;
            SessionID = "2_MX40NjQ4NTQ5Mn5-MTU5ODc2MDk4MTU2M35vTDBMZjU0c21BcGhtNTE2Ylp1cllSS1F-fg";
            Token = "T1==cGFydG5lcl9pZD00NjQ4NTQ5MiZzaWc9NTRmZjBhYzFkZGQ2MmZkZGJhZjI4NWY5NmE1Y2E2MzQ0OTVhZTMxMjpzZXNzaW9uX2lkPTJfTVg0ME5qUTROVFE1TW41LU1UVTVPRGMyTURrNE1UVTJNMzV2VERCTVpqVTBjMjFCY0dodE5URTJZbHAxY2xsU1MxRi1mZyZjcmVhdGVfdGltZT0xNTk4NzYwOTkwJm5vbmNlPTAuODYzMzk2NTI0NTQxNzAxNyZyb2xlPXB1Ymxpc2hlciZleHBpcmVfdGltZT0xNjAxMzUyOTg4JmluaXRpYWxfbGF5b3V0X2NsYXNzX2xpc3Q9";
            using ( var client = new HttpClient () )
            {
                try
                {
                    //var resp = await client.GetAsync(Config.KeysUrl);
                    //var json = await resp.Content.ReadAsStringAsync();
                    //var keys = JsonConvert.DeserializeObject<Keys>(json);

                    CrossOpenTok.Current.ApiKey = "46485492";// keys.ApiKey;
                    CrossOpenTok.Current.SessionId = "2_MX40NjQ4NTQ5Mn5-MTU5ODc2MDk4MTU2M35vTDBMZjU0c21BcGhtNTE2Ylp1cllSS1F-fg";//keys.SessionId;
                    CrossOpenTok.Current.UserToken = "T1==cGFydG5lcl9pZD00NjQ4NTQ5MiZzaWc9NTRmZjBhYzFkZGQ2MmZkZGJhZjI4NWY5NmE1Y2E2MzQ0OTVhZTMxMjpzZXNzaW9uX2lkPTJfTVg0ME5qUTROVFE1TW41LU1UVTVPRGMyTURrNE1UVTJNMzV2VERCTVpqVTBjMjFCY0dodE5URTJZbHAxY2xsU1MxRi1mZyZjcmVhdGVfdGltZT0xNTk4NzYwOTkwJm5vbmNlPTAuODYzMzk2NTI0NTQxNzAxNyZyb2xlPXB1Ymxpc2hlciZleHBpcmVfdGltZT0xNjAxMzUyOTg4JmluaXRpYWxfbGF5b3V0X2NsYXNzX2xpc3Q9";//keys.Token;
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