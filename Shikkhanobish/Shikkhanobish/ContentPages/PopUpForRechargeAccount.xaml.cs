using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Pages;
using System.Globalization;

namespace Shikkhanobish.ContentPages
{
    [XamlCompilation ( XamlCompilationOptions.Compile )]
    public partial class PopUpForRechargeAccount : PopupPage
    {
        private string password;
        public PopUpForRechargeAccount ( string pass)
        {
            InitializeComponent ();
            password = pass;
        }

        private void Button_Clicked ( object sender , EventArgs e )
        {
            if(passlbl.Text != password)
            {
                errorlbl.Text = "Incorrect password!";
            }
            else
            {
                //recharge system
            }
        }
    }
}