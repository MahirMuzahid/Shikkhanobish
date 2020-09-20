using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Pages;
using System.Globalization;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Rg.Plugins.Popup.Extensions;

namespace Shikkhanobish.ContentPages
{
    [XamlCompilation ( XamlCompilationOptions.Compile )]
    public partial class PopUpForRechargeAccount : PopupPage
    {
        int ft;
        private string password;
        private int studentid;
        public PopUpForRechargeAccount ( string pass, int sID)
        {
            ft = 0;
            InitializeComponent ();
            password = pass;
            studentid = sID;
        }

        private async void Button_Clicked ( object sender , EventArgs e )
        {
            ft++;
            if(ft == 1)
            {

                if ( passlbl.Text != password )
                {
                    errorlbl.Text = "Incorrect password!";
                }
                else
                {
                    string url = "https://api.shikkhanobish.com/api/Master/SetStudentWalletInfo";
                    HttpClient client = new HttpClient ();
                    string jsonData = JsonConvert.SerializeObject ( new { StudentID = studentid , RechargedAmount = Int32.Parse ( amtlbl.Text ) , Phonenumber = pnlbl.Text , TrxID = trxlbl.Text , Date = DateTime.Now.ToString () } );
                    StringContent content = new StringContent ( jsonData , Encoding.UTF8 , "application/json" );
                    HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( false );
                    string result = await response.Content.ReadAsStringAsync ();
                    var r = JsonConvert.DeserializeObject<Response> ( result );
                    if ( r.Status == 0 )
                    {
                        //send msg in admin mobile
                        MainThread.BeginInvokeOnMainThread ( ( ) => { errorlbl.Text = "Thank you! We have sent payment request to admin."; } );

                    }
                    else
                    {
                        MainThread.BeginInvokeOnMainThread ( ( ) => { errorlbl.Text = "Some error occured! Please connect with 01833368125"; } );

                    }
                    MainThread.BeginInvokeOnMainThread ( ( ) => { sendbtn.Text = "Ok"; } );
                    
                }
            }
            else
            {
                await Navigation.PopPopupAsync ().ConfigureAwait ( false );
            }
            
        }
    }
}