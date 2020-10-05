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
        private async void Button_Clicked ( object sender , EventArgs e )
        {
            callbtn.Text = "Calling...";
            await Application.Current.MainPage.Navigation.PushModalAsync ( new CallingPageStudent(Info) );
            await Navigation.PopPopupAsync ().ConfigureAwait ( false );
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

        




    }
}