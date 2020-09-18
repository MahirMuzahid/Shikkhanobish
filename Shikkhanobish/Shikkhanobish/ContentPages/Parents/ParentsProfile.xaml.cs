using Newtonsoft.Json;
using Shikkhanobish.Model;
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
    [XamlCompilation ( XamlCompilationOptions.Compile )]
    public partial class ParentsProfile : ContentPage
    {
        private List<TuitionHistoryStudent> studentHistory = new List<TuitionHistoryStudent> ();
        public ParentsProfile ( )
        {
            InitializeComponent ();
            List<ParentsCardInfo> Pci = new List<ParentsCardInfo> ();
            walletbacklbl.IsVisible = false;
            historybacklbl.IsVisible = false;
            ParentsCardInfo pci1 = new ParentsCardInfo ();
            ParentsCardInfo pci2 = new ParentsCardInfo ();
            ParentsCardInfo pci3 = new ParentsCardInfo ();
            ParentsCardInfo pci4 = new ParentsCardInfo ();

            pci1.ImageSource = "savedmoney.jpg";
            pci1.Text = "Account Balence";
            pci1.info = "500 Taka";
            pci1.TextColors = "#FFFFFFFF";
            Pci.Add (pci1);
            pci2.ImageSource = "spentmoney.jpg";
            pci2.Text = "Total Spent";
            pci2.info = "1200 Taka";
            pci2.TextColors = "#FFFFFFFF";
            Pci.Add ( pci2 );
            pci3.ImageSource = "topsubject.jpg";
            pci3.Text = "Favourite Subject";
            pci3.info = "Physic First Paper";
            pci3.TextColors = "#FF000000";
            Pci.Add ( pci3 );
            pci4.ImageSource = "topsubject.jpg";
            pci4.Text = "Favourite Subject";
            pci4.info = "Physic First Paper";
            pci4.TextColors = "#FF000000";
            Pci.Add ( pci4 );
            cv.ItemsSource = Pci;
            callInfo ();
        }
        public async void callInfo()
        {
            await ShowWalletHisotry ();
            await ShowTuitionSistoryAsync ();
        }
        public async Task ShowTuitionSistoryAsync ( )
        {
            string url = "https://api.shikkhanobish.com/api/Master/GetTuitionHistoryStudent";
            HttpClient client = new HttpClient ();
            string jsonData = JsonConvert.SerializeObject ( new { StudentID = 23 } );// T.D: have to add student ID
            StringContent content = new StringContent ( jsonData , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( true );
            var result = await response.Content.ReadAsStringAsync ().ConfigureAwait ( true );
            var hisotyList = JsonConvert.DeserializeObject<List<TuitionHistoryStudent>> ( result );
            studentHistory = hisotyList;
            if(studentHistory.Count == 0)
            {
                historybacklbl.IsVisible = true;
            }
            else
            {
                StudentHistoryListView.ItemsSource = studentHistory;
            }
            
        }

        public async Task ShowWalletHisotry ( )
        {
            string url = "https://api.shikkhanobish.com/api/Master/GetStudentWalletInfo";
            HttpClient client = new HttpClient ();
            string jsonData = JsonConvert.SerializeObject ( new { StudentID =  23 } );
            StringContent content = new StringContent ( jsonData , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( false );
            string result = await response.Content.ReadAsStringAsync ();
            var wh = JsonConvert.DeserializeObject<List<WalletHistoryStudent>> ( result );
            
            for ( int i = 0; i < wh.Count; i++ )
            {
                if ( wh [ i ].IsPending == 1 )
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
            wh.Reverse ();
            if(wh.Count == 0)
            {
                walletbacklbl.IsVisible = true;
            }
            else
            {
                StudentWalletHistoryListView.ItemsSource = wh;
            }
            
        }
    }
}