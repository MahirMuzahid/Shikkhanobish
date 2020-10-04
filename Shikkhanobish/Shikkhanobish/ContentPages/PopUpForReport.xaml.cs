using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Pages;
using Shikkhanobish.Model;
using Shikkhanobish.ViewModel;
using Newtonsoft.Json;
using System.Net.Http;

namespace Shikkhanobish.ContentPages.Student
{
    [XamlCompilation ( XamlCompilationOptions.Compile )]
    public partial class PopUpForReport : PopupPage
    {
        TransferInfo trans = new TransferInfo ();
        string ReportType, ReportText;
        public PopUpForReport ( TransferInfo tr )
        {
            InitializeComponent ();         
            trans = tr;
            errorlbl.Text = "";
            reportbtn.IsEnabled = false;
            reportTxtentry.IsEnabled = false;
            reportTxtentry.Placeholder = "";
            reportbtn.BackgroundColor = Color.FromHex ( "#F0F0F0" );
            namelbl.Text = "Teacher: " + tr.Teacher.TeacherName;
        }

        private void otherchk_CheckedChanged ( object sender , CheckedChangedEventArgs e )
        {
            if(otherchk.IsChecked == true)
            {
                ReportType = "Other";
                reportbtn.IsEnabled = true;
                reportTxtentry.IsEnabled = true;
                reportTxtentry.Placeholder = "Write your complain here...";
                reportbtn.BackgroundColor = Color.FromHex ( "#DC4444" );
            }
            else if ( hrschk.IsChecked == false && otherchk.IsChecked == false && inbhvchk.IsChecked == false && twchk.IsChecked == false )
            {
                reportbtn.IsEnabled = false;
                reportTxtentry.IsEnabled = false;
                reportTxtentry.Placeholder = "";
                reportTxtentry.Text = "";
                reportbtn.BackgroundColor = Color.FromHex ( "#F0F0F0" );
            }

            if( otherchk.IsChecked == false )
            {
                reportTxtentry.IsEnabled = false;
                reportTxtentry.Text = "";
                reportTxtentry.Placeholder = "";
            }
        }

        private void hrschk_CheckedChanged ( object sender , CheckedChangedEventArgs e )
        {
            if ( hrschk.IsChecked == true )
            {
                ReportType = "Harresment";
                reportbtn.IsEnabled = true;
                reportbtn.BackgroundColor = Color.FromHex ( "#DC4444" );
            }
            else if( hrschk.IsChecked == false && otherchk.IsChecked == false && inbhvchk.IsChecked == false  && twchk.IsChecked == false)
            {
                reportbtn.IsEnabled = false;
                reportbtn.BackgroundColor = Color.FromHex ( "#F0F0F0" );
            }
        }

        private void inbhvchk_CheckedChanged ( object sender , CheckedChangedEventArgs e )
        {
            if ( inbhvchk.IsChecked == true )
            {
                ReportType = "Inappropiate Behave";
                reportbtn.IsEnabled = true;
                reportbtn.BackgroundColor = Color.FromHex ( "#DC4444" );
            }
            else if ( hrschk.IsChecked == false && otherchk.IsChecked == false && inbhvchk.IsChecked == false && twchk.IsChecked == false )
            {
                reportbtn.IsEnabled = false;
                reportbtn.BackgroundColor = Color.FromHex ( "#F0F0F0" );
            }
        }
        private void twchk_CheckedChanged ( object sender , CheckedChangedEventArgs e )
        {
            if ( twchk.IsChecked == true )
            {
                ReportType = "Time Waste";
                reportbtn.IsEnabled = true;
                reportbtn.BackgroundColor = Color.FromHex ( "#DC4444" );
            }
            else if ( hrschk.IsChecked == false && otherchk.IsChecked == false && inbhvchk.IsChecked == false && twchk.IsChecked == false )
            {
                reportbtn.IsEnabled = false;
                reportbtn.BackgroundColor = Color.FromHex ( "#F0F0F0" );
            }
        }
        private async void reportbtn_Clicked ( object sender , EventArgs e )
        {
            if ( hrschk.IsChecked == true || otherchk.IsChecked == true || inbhvchk.IsChecked == true || twchk.IsChecked == true )
            {
                ReportText = reportTxtentry.Text;
                string url = "https://api.shikkhanobish.com/api/Master/ReportTeacher";
                HttpClient client = new HttpClient ();
                string jsonData = JsonConvert.SerializeObject ( new { StudentID = trans.Student.StudentID , TeacherID = trans.Teacher.TeacherID , ReportType = ReportType , ReportText = ReportText } );
                StringContent content = new StringContent ( jsonData , Encoding.UTF8 , "application/json" );
                HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( true );
                var result = await response.Content.ReadAsStringAsync ().ConfigureAwait ( true );
                var res = JsonConvert.DeserializeObject<Response> ( result );
                errorlbl.TextColor = Color.Green;
                errorlbl.Text = "Report Done";
                reportbtn.IsEnabled = false;
                await Navigation.PopPopupAsync ().ConfigureAwait ( false );
            }
        }

        
    }
}