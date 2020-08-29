using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using Shikkhanobish.Model;
using Plugin.Connectivity;
using Rg.Plugins.Popup.Extensions;
using Shikkhanobish.ContentPages;

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
            StudentIdlbl.Text = "4. Enter " + st.StundentID + " as reference code";

            if ( CrossConnectivity.Current.IsConnected )
            {
                for ( int i = 0; i < 20; i++ )
                {
                    StudentBalance SB = new StudentBalance ();
                    SB.number = 0235903425;
                    SB.amount = 23;
                    SB.date = "20.03.2021";
                    SB.trxID = "TSCV236YG2KJ3265H";
                    sb.Add ( SB );
                }
                StudentWalletHistoryListView.ItemsSource = sb;
            }
            else
            {
                Errorlbl.Text = "Check internet connection";
            }
            
        }

        private void Button_Clicked_1 ( object sender , System.EventArgs e )
        {
            if ( CrossConnectivity.Current.IsConnected )
            {
                Navigation.PushPopupAsync ( new PopUpForRechargeAccount ( stuent.Password ) );
            }
            else
            {
                Errorlbl.Text = "Check internet connection";
            }
        }
    }
}