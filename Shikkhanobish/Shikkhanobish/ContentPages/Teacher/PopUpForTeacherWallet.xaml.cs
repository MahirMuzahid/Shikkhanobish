using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Shikkhanobish.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shikkhanobish.ContentPages
{
    [XamlCompilation ( XamlCompilationOptions.Compile )]
    public partial class PopUpForTeacherWallet : PopupPage
    {
        Teacher teacher = new Teacher ();
        private Random random;

        public PopUpForTeacherWallet (Teacher t )
        {
            InitializeComponent ();
            teacher = t;
            namelbl.Text = ""+ t.TeacherName;
            amountlbl.Text = "Current Saved Amount: " + t.RechargedAmount;
            pnentry.IsVisible = false;
            cpentry.IsVisible = false;
            otpentry.IsVisible = false;
            wthdrawbtn.IsVisible = false;
            erroelbl.IsVisible = true;
            if(t.RechargedAmount < 50)
            {
                erroelbl.Text = "Error: You dont have more than 50 taka in your account";
                erroelbl.TextColor = Color.Red;
                agreebtn.IsVisible = false;
            }
        }

        public object VerificationNumber { get; private set; }

        private void agreebtn_Clicked ( object sender , EventArgs e )
        {
            pnentry.IsVisible = true;
            cpentry.IsVisible = true;           
            wthdrawbtn.IsVisible = true;
            agreebtn.IsVisible = false;
            erroelbl.IsVisible = true;
            wthdrawbtn.Text = "Send Otp";
        }

        private async void wthdrawbtn_Clicked ( object sender , EventArgs e )
        {
            if(wthdrawbtn.Text == "Send Otp" )
            {
                if ( cpentry.Text == teacher.Password )
                {
                    string Phonenumber = teacher.PhoneNumber;
                    VerificationNumber = random.Next ( 1000 , 9999 );
                    string text = "Your Username or Password reset verification code is: " + VerificationNumber;
                    string apiKey = "bdgoQKW5OyLe748FUlrBmgCEXZn3oivhuf";
                    Massage ms = new Massage ();
                    await ms.SendMsg ( Phonenumber , text , apiKey ).ConfigureAwait ( false );
                    if(ms.isSent == true)
                    {
                        erroelbl.Text = "Message: OTP sent to this number: xxxxxxx" + teacher.PhoneNumber [ 7 ] + teacher.PhoneNumber [ 8 ] + teacher.PhoneNumber [ 9 ] + teacher.PhoneNumber [ 10 ];
                        wthdrawbtn.Text = "Confirm";
                        otpentry.IsVisible = true;
                        pnentry.IsVisible = false;
                        cpentry.IsVisible = false;
                        erroelbl.TextColor = Color.Black;
                    }
                    else
                    {
                        erroelbl.Text = "Either number is off or out of network.";
                    }
                    

                }
                else
                {
                    erroelbl.Text = "Error: Password doesn't match";
                    erroelbl.TextColor = Color.Red;

                }
            }
            else if( wthdrawbtn.Text == "Confirm" )
            {
                //check otp number
                otpentry.Text = "";
                otpentry.Placeholder = "Withdraw amount";
                wthdrawbtn.Text = "Withdraw";
                wthdrawbtn.BackgroundColor = Color.FromHex ( "#60ED9D" );
                erroelbl.Text = "";

            }
            else
            {
                //send info in database
                erroelbl.Text = "Message: Withdraw request has sent succefully!";
                erroelbl.TextColor = Color.Black;
            }
            
            
        }
    }
}