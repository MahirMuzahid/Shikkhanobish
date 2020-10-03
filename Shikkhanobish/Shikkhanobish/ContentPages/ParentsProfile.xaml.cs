using Newtonsoft.Json;
using Plugin.Connectivity;
using Rg.Plugins.Popup.Extensions;
using Shikkhanobish.ContentPages.Common;
using Shikkhanobish.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shikkhanobish.ContentPages
{
    [XamlCompilation ( XamlCompilationOptions.Compile )]
    public partial class ParentsProfile : ContentPage
    {
        private List<TuitionHistoryStudent> studentHistory = new List<TuitionHistoryStudent> ();
        Parent parentinfo;
        StudentClass student;
        string topsub = "";
        float totalSpent = 0;
        public ParentsProfile ( Parent p)
        {
            InitializeComponent ();
            parentinfo = p;
            getstudnetinfo ();                                           
            walletbacklbl.IsVisible = false;
            historybacklbl.IsVisible = false;
            SetInfoInInternalStorage ( "" , "" , "Parent" , p.ParentID );
        }
        public async Task SetInfoInInternalStorage ( string username , string password , string usertype , int parentCode )
        {
            await SecureStorage.SetAsync ( "username" , username ).ConfigureAwait ( false );
            await SecureStorage.SetAsync ( "password" , password ).ConfigureAwait ( false );
            await SecureStorage.SetAsync ( "usertype" , usertype ).ConfigureAwait ( false );
            await SecureStorage.SetAsync ( "parentCode" , "" + parentCode ).ConfigureAwait ( false );
        }
        public async Task getstudnetinfo()
        {
            cvboxf.BackgroundColor = Color.FromHex ( onColor );
            cvboxs.BackgroundColor = Color.FromHex ( offColor );
            cvboxt.BackgroundColor = Color.FromHex ( offColor );
            cvboxfo.BackgroundColor = Color.FromHex ( offColor );
            string url = "https://api.shikkhanobish.com/api/Master/GetInfoByStudentID";
            HttpClient client = new HttpClient ();
            string jsonData = JsonConvert.SerializeObject ( new { StudentID = parentinfo.StudentID } );// T.D: have to add student ID
            StringContent content = new StringContent ( jsonData , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( true );
            var result = await response.Content.ReadAsStringAsync ().ConfigureAwait ( true );
            student = JsonConvert.DeserializeObject<StudentClass> ( result );
            await callInfo ();
            List<ParentsCardInfo> Pci = new List<ParentsCardInfo> ();
            ParentsCardInfo pci1 = new ParentsCardInfo ();
            ParentsCardInfo pci2 = new ParentsCardInfo ();
            ParentsCardInfo pci3 = new ParentsCardInfo ();
            ParentsCardInfo pci4 = new ParentsCardInfo ();
            pci1.ImageSource = "rechargeins.jpg";
            pci1.Text = "Recharge Instruction";
            pci1.info = "Send money to 01833368125 and use reference code: " + student.StudentID;//have to rewrite
            pci1.TextColors = "#FF000000";
            pci1.fontSize = 13;
            Pci.Add ( pci1 );
            pci2.ImageSource = "savedmoney.jpg";
            pci2.Text = "Account Balence";
            pci2.info = "" + student.RechargedAmount + " Taka";
            pci2.TextColors = "#FFFFFFFF";
            pci2.fontSize = 18;
            Pci.Add ( pci2 );
            pci3.ImageSource = "spentmoney.jpg";
            pci3.Text = "Total Spent";
            pci3.info = totalSpent+" Taka";
            pci3.fontSize = 18;
            pci3.TextColors = "#FFFFFFFF";
            Pci.Add ( pci3 );
            pci4.ImageSource = "topsubject.jpg";
            pci4.Text = "Favourite Subject";
            pci4.info = topsub;
            pci4.TextColors = "#FF000000";
            pci4.fontSize = 18;
            Pci.Add ( pci4 );

            cv.ItemsSource = Pci;
            cv.CurrentItemChanged += OnCurrentItemChanged;
            
        }
        string offColor = "#D3D3D3", onColor = "#474747";
        void OnCurrentItemChanged ( object sender , CurrentItemChangedEventArgs e )
        {
            ParentsCardInfo a = ( ParentsCardInfo ) e.CurrentItem;
            if(a.Text[0] == 'R')
            {
                cvboxf.BackgroundColor = Color.FromHex ( onColor);
                cvboxs.BackgroundColor = Color.FromHex ( offColor );
                cvboxt.BackgroundColor = Color.FromHex ( offColor );
                cvboxfo.BackgroundColor = Color.FromHex ( offColor );
            }
            if (  a.Text [ 0 ] == 'A' )
            {
                cvboxf.BackgroundColor = Color.FromHex ( offColor );
                cvboxs.BackgroundColor = Color.FromHex ( onColor );
                cvboxt.BackgroundColor = Color.FromHex ( offColor );
                cvboxfo.BackgroundColor = Color.FromHex ( offColor );
            }
            if (  a.Text [ 0 ] == 'T' )
            {
                cvboxf.BackgroundColor = Color.FromHex ( offColor );
                cvboxs.BackgroundColor = Color.FromHex ( offColor );
                cvboxt.BackgroundColor = Color.FromHex ( onColor );
                cvboxfo.BackgroundColor = Color.FromHex ( offColor );
            }
            if (  a.Text [ 0 ] == 'F' )
            {
                cvboxf.BackgroundColor = Color.FromHex ( offColor );
                cvboxs.BackgroundColor = Color.FromHex ( offColor );
                cvboxt.BackgroundColor = Color.FromHex ( offColor );
                cvboxfo.BackgroundColor = Color.FromHex ( onColor );
            }
        }
        public async Task callInfo()
        {
            await ShowWalletHisotry ();
            await ShowTuitionSistoryAsync ();
        }
        public async Task ShowTuitionSistoryAsync ( )
        {
            string url = "https://api.shikkhanobish.com/api/Master/GetTuitionHistoryStudent";
            HttpClient client = new HttpClient ();
            string jsonData = JsonConvert.SerializeObject ( new { StudentID = student.StudentID } );// T.D: have to add student ID
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

            List<string> subjects = new List<string> ();

            for(int i = 0; i < studentHistory.Count; i++ )
            {
                subjects.Add (studentHistory [ i ].Subject);
            }
            topsub = countTopSubject ( subjects );
            float totalCost = 0;
            
            for(int i = 0; i < studentHistory.Count; i++ )
            {
                totalCost = totalCost + studentHistory[i].Cost;
            }
            totalSpent = totalCost;
        }


       
        public string countTopSubject(List<string> subjects)
        {
            string topSubject = "";
            int thnumbersubject= 0;
            List<string> subjectcountlist = new List<string> ();
            List<int> countList = new List<int> ();
            for(int i = 0; i< subjects.Count; i++ )
            {
                bool isMatch = false;
                int count = 0;
                for ( int k = 0; k < subjectcountlist.Count; k++ )
                {
                    if ( subjectcountlist [ k ] == subjects [ i ] )
                    {
                        isMatch = true;
                    }
                }
                for (int j = 0; j < subjects.Count; j++ )
                {   
                                
                    if(isMatch == true)
                    {
                        continue;
                    }
                    else
                    {
                        if ( subjects [ i ] == subjects [ j ] )
                        {
                            count++;
                        }
                    }                   
                   
                    
                }
                if( isMatch == false )
                {
                    countList.Add ( count );
                    subjectcountlist.Add ( subjects [ i ] );
                }
                
            }

            int max = countList[0];
            int maxthnumber = 0;

            for(int i = 0; i < countList.Count; i++ )
            {
                if(max < countList [ i ] )
                {
                    max = countList [ i ];
                    maxthnumber = i;
                }
            }
            topSubject = subjectcountlist [ maxthnumber ];
            return topSubject;
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
                MainThread.BeginInvokeOnMainThread ( ( ) => { StudentWalletHistoryListView.ItemsSource = wh; } );
                
            }
            
        }

        private void Button_Clicked ( object sender , EventArgs e )
        {
            try
            {
                if ( CrossConnectivity.Current.IsConnected )
                {

                    Navigation.PushPopupAsync ( new PopUpForRechargeAccount ( parentinfo.Password , parentinfo.StudentID ) );
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

        private void Button_Clicked_1 ( object sender , EventArgs e )
        {
            ShowWalletHisotry ();
        }

        private async void Button_Clicked_2 ( object sender , EventArgs e )
        {
            SecureStorage.RemoveAll ();
            await Application.Current.MainPage.Navigation.PushModalAsync ( new MainPage () ).ConfigureAwait ( false );
        }

        protected override bool OnBackButtonPressed ( )
        {
            Navigation.PushPopupAsync ( new PopUpForTextAlert ( "" , "" , true ) );
            return true;
        }
    }
}