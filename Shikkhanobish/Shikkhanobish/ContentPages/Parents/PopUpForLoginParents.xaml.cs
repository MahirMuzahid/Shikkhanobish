using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Shikkhanobish.Model;
using Shikkhanobish.ViewModel;
using Xamarin.Essentials;
using Rg.Plugins.Popup.Extensions;

namespace Shikkhanobish.ContentPages.Parents
{
    [XamlCompilation ( XamlCompilationOptions.Compile )]
    public partial class PopUpForLoginParents : PopupPage
    {
        public PopUpForLoginParents ( )
        {
            loginbtn.Text = "Login";
            errorlbl.Text = "";
            InitializeComponent ();
        }

        private async void Button_Clicked ( object sender , EventArgs e )
        {
            loginbtn.Text = "Wait";
            string code = pcodeenty.Text;
            string pass = passenty.Text;
            string url = "https://api.shikkhanobish.com/api/Master/GetParentInfo";
            HttpClient client = new HttpClient ();
            string jsonData = JsonConvert.SerializeObject ( new { ParentID = code , Password = pass } );// T.D: have to add student ID
            StringContent content = new StringContent ( jsonData , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( true );
            var result = await response.Content.ReadAsStringAsync ().ConfigureAwait ( true );
            var parent = JsonConvert.DeserializeObject<Parent> ( result );
            if(parent.ParentName== null)
            {
                errorlbl.Text = "Password or parent code is incorrect";
                loginbtn.Text = "Login";
            }
            else
            {
                await Navigation.PopPopupAsync ().ConfigureAwait ( false );
                MainThread.BeginInvokeOnMainThread ( async ( ) => { await Application.Current.MainPage.Navigation.PushModalAsync ( new ParentsProfile ( parent ) ).ConfigureAwait ( false ); } );
            }
            
        }

        private void Button_Clicked_1 ( object sender , EventArgs e )
        {

        }

        private void Button_Clicked_2 ( object sender , EventArgs e )
        {

        }
    }
}