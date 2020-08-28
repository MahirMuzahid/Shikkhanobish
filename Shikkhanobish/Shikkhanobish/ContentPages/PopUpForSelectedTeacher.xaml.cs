﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shikkhanobish.ContentPages
{
    [XamlCompilation ( XamlCompilationOptions.Compile )]
    public partial class PopUpForSelectedTeacher : PopupPage
    {
        private string Pass;
        private int parentcode;
        public PopUpForSelectedTeacher ( string p, int pcode )
        {
            InitializeComponent ();
            Pass = p;
            parentcode = pcode;
        }

        private void Button_Clicked ( object sender , EventArgs e )
        {
            if(passEntry.Text == Pass)
            {
                prntTxt.Text = "Your parent code is: " + parentcode;
            }
            else
            {
                prntTxt.Text = "Wrong Password";
            }
        }
    }
}