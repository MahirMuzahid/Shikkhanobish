using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using Shikkhanobish.Model;
using Plugin.Connectivity;
using Rg.Plugins.Popup.Extensions;
using Shikkhanobish.ContentPages;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Xamarin.Essentials;

namespace Shikkhanobish
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Balance : ContentPage
    {
        List<StudentBalance> sb = new List<StudentBalance>();
        private Student stuent;
        public Balance(Student st)
        {
            InitializeComponent();
            stuent = st;
            StudentIdlbl.Text = "4. Enter " + st.StudentID + " as reference code";

            if ( CrossConnectivity.Current.IsConnected )
            {
                GetWalletInfo ();               
            }
            else
            {
                Errorlbl.Text = "Check internet connection";
            }
            
        }

        private void Button_Clicked_1 ( object sender , System.EventArgs e )
        {
            try
            {
                if ( CrossConnectivity.Current.IsConnected )
                {
                    
                    Navigation.PushPopupAsync ( new PopUpForRechargeAccount ( stuent.Password, stuent.StudentID ) );
                }
                else
                {
                    Errorlbl.Text = "Check internet connection";
                }
            }
            catch
            {
                Errorlbl.Text = "Check internet connection";
            }
            
        }       

        public async void GetWalletInfo()
        {
            string url = "https://api.shikkhanobish.com/api/Master/GetStudentWalletInfo";
            HttpClient client = new HttpClient ();
            string jsonData = JsonConvert.SerializeObject ( new { StudentID = stuent.StudentID  } );
            StringContent content = new StringContent ( jsonData , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( false );
            string result = await response.Content.ReadAsStringAsync ();
            var wh = JsonConvert.DeserializeObject<List<WalletHistoryStudent>> ( result );

            for(int i = 0; i < wh.Count; i++ )
            {
                if(wh[i].IsPending == 1)
                {
                    wh [ i ].pendingText = "Pending";
                    wh [ i ].pendingColor = "#DFD535 ";//yellow
                }
                else if ( wh [ i ].IsPending == 0 )
                {
                    wh [ i ].pendingText = "Added";
                    wh [ i ].pendingColor = "#63D342  ";//green
                }
                else if ( wh [ i ].IsPending == 2 )
                {
                    wh [ i ].pendingText = "Declined";
                    wh [ i ].pendingColor = "#ED4E4E  ";//red
                }
            }
            MainThread.BeginInvokeOnMainThread ( ( ) => { StudentWalletHistoryListView.ItemsSource = wh; } );
            
        }

        private void Button_Clicked ( object sender , System.EventArgs e )
        {
            GetWalletInfo ();

        }
    }
}