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
            List<StudentBalance> sb = new List<StudentBalance> ();
            for ( int i = 0; i < 20; i++ )
            {
                StudentBalance SB = new StudentBalance ();
                SB.Number = 0235903425;
                SB.Amount = 23;
                SB.Date = "20.03.2021";
                SB.TrxID = "TSCV236YG2KJ3265H";
                sb.Add ( SB );
            }
            StudentWalletHistoryListView.ItemsSource = sb;
            ShowTuitionSistoryAsync ();
            
        }
        public async void ShowTuitionSistoryAsync ( )
        {
            string url = "https://api.shikkhanobish.com/api/Master/GetTuitionHistoryStudent";
            HttpClient client = new HttpClient ();
            string jsonData = JsonConvert.SerializeObject ( new { StudentID = 23 } );
            StringContent content = new StringContent ( jsonData , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( true );
            var result = await response.Content.ReadAsStringAsync ().ConfigureAwait ( true );
            var hisotyList = JsonConvert.DeserializeObject<List<TuitionHistoryStudent>> ( result );
            studentHistory = hisotyList;
            StudentHistoryListView.ItemsSource = studentHistory;
        }
    }
}