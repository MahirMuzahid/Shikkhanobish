using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shikkhanobish.ContentPages.Common
{
    [XamlCompilation ( XamlCompilationOptions.Compile )]
    public partial class PopUpForTextAlert : PopupPage
    {
        bool quit;
        public PopUpForTextAlert (string Title, string Text, bool isQuit )
        {
            InitializeComponent ();
            if(isQuit == true)
            {
                titlelbl.Text = "Close App";
                textlbl.Text = "Do you want to close the application?";
                okbtn.Text = "Yes";
                quit = true;
            }
            else
            {
                quit = false;
                titlelbl.Text = Title;
                textlbl.Text = Text;
            }
            
        }

        private async void Button_Clicked ( object sender , EventArgs e )
        {
            if(quit == true)
            {
                System.Environment.Exit ( 0 );
            }
            else
            {
                await Navigation.PopPopupAsync ().ConfigureAwait ( false );
            }
            
        }
    }
}