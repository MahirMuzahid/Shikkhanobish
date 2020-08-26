using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using Shikkhanobish.Model;

namespace Shikkhanobish
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Balance : ContentPage
    {
        List<StudentBalance> sb = new List<StudentBalance>();
        public Balance(Student st)
        {
            InitializeComponent();

            StudentIdlbl.Text = "4. Enter " + st.StundentID + " as reference code";
            for(int i = 0; i < 20; i++ )
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

        private async void Button_Clicked ( object sender , System.EventArgs e )
        {
            string phonenumber = await DisplayPromptAsync ( "Enter Phonenumber" , "" );
            if( phonenumber != "" && phonenumber.Length == 11)
            {
                string trxID = await DisplayPromptAsync ( "Enter Trasaction ID or TrxID" , "" );
            }
            else
            {
                await DisplayAlert ( "" , "Phone Number is not valid" , "OK" );
            }
        }
    }
}