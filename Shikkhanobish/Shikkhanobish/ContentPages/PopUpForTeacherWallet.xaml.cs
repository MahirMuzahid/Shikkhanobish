using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Java.Nio.Channels;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Shikkhanobish.Model;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shikkhanobish.ContentPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUpForTeacherWallet : PopupPage
    {
        Teacher teacher = new Teacher();
        List<WalletHistoryTeacher> walletHistory = new List<WalletHistoryTeacher>();

        public PopUpForTeacherWallet(Teacher t)
        {
            InitializeComponent();
            teacher = t;
            namelbl.Text = "" + t.TeacherName;
            amountlbl.Text = "Current Saved Amount: " + t.RechargedAmount;
            if (t.RechargedAmount < 50)
            {
                erroelbl.Text = "You dont have more than 50 taka in your account";
                erroelbl.TextColor = Color.Red;
                agreebtn.IsVisible = false;
            }
            else
            {
                GetWalletInfo();
            }
           
            
            
        }

        public int VerificationNumber { get; private set; }

        private void agreebtn_Clicked(object sender, EventArgs e)
        {
            pnentry.IsVisible = false;
            cpentry.IsVisible = true;
            wthdrawbtn.IsVisible = true;
            agreebtn.IsVisible = false;
            erroelbl.IsVisible = true;
            wthdrawbtn.Text = "Send Otp";
        }

        private async void wthdrawbtn_Clicked(object sender, EventArgs e)
        {
            if (wthdrawbtn.Text == "Send Otp")
            {
                if (cpentry.Text == teacher.Password)
                {
                    string Phonenumber = teacher.PhoneNumber;
                    Random random = new Random();
                    VerificationNumber = random.Next(1000, 9999);
                    string text = "Shikkhanobish withdrawal request verification code: " + VerificationNumber;
                    Massage ms = new Massage();
                    await ms.SendMsg(Phonenumber, text).ConfigureAwait(false);
                    if (ms.isSent == true)
                    {
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            erroelbl.Text = "OTP sent to this number: xxxxxxx" + teacher.PhoneNumber[7] + teacher.PhoneNumber[8] + teacher.PhoneNumber[9] + teacher.PhoneNumber[10];
                        });
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            wthdrawbtn.Text = "Confirm";
                        });
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            otpentry.IsVisible = true;
                        });
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            cpentry.IsVisible = false;
                        });
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            erroelbl.TextColor = Color.Black;
                        });
                    }
                    else
                    {
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            erroelbl.Text = "Either number is off or out of network.";
                        });
                    }


                }
                else
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        erroelbl.Text = "Password doesn't match";
                    });
                    erroelbl.TextColor = Color.Red;

                }
            }
            else if (wthdrawbtn.Text == "Confirm")
            {
                if (VerificationNumber == int.Parse(otpentry.Text))
                {
                    pnentry.IsVisible = true;
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        otpentry.Text = "";
                        otpentry.Placeholder = "Withdraw amount. Ex: 100";
                        wthdrawbtn.Text = "Withdraw";
                    });
                    wthdrawbtn.BackgroundColor = Color.FromHex("#60ED9D");
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        erroelbl.Text = "";
                    });
                }
                else
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        erroelbl.Text = "Verification code doesn't match";
                    });
                    erroelbl.TextColor = Color.Red;
                }

            }
            else if (wthdrawbtn.Text == "Ok")
            {
                await Navigation.PopPopupAsync().ConfigureAwait(false);
            }
            else
            {
                try
                {
                    int amount = int.Parse(otpentry.Text);
                    if (amount > teacher.RechargedAmount)
                    {
                        erroelbl.Text = "You can't withdraw more than you have";
                    }
                    else
                    {
                        SetWalletInfo();
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            erroelbl.Text = "Withdraw request has sent succefully!";
                        });
                        erroelbl.TextColor = Color.Red;
                        wthdrawbtn.Text = "Ok";
                        erroelbl.TextColor = Color.Black;
                        pnentry.IsVisible = false;
                        cpentry.IsVisible = false;
                        otpentry.IsVisible = false;
                    }
                }
                catch(Exception ex)
                {
                    erroelbl.Text = "Only use variable in withdraw field. Ex: 100";
                }
                erroelbl.TextColor = Color.Red;

            }
        }

        public async Task SetWalletInfo()
        {

            string urlT = "https://api.shikkhanobish.com/api/Master/SetTeacherWalletInfo";
            HttpClient clientT = new HttpClient();
            string jsonDataT = JsonConvert.SerializeObject(new { TeacherID = teacher.TeacherID, WithdrawAmount = otpentry.Text, Phonenumber = pnentry.Text, TrxID = "none", Date = DateTime.Now.ToString() });
            StringContent contentT = new StringContent(jsonDataT, Encoding.UTF8, "application/json");
            HttpResponseMessage responseT = await clientT.PostAsync(urlT, contentT).ConfigureAwait(false);
            string resultT = await responseT.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<Response>(resultT);
        }
        public async Task GetWalletInfo()
        {
            erroelbl.IsVisible = true;
            string urlT = "https://api.shikkhanobish.com/api/Master/GetTeacherWalletInfo ";
            HttpClient clientT = new HttpClient();
            string jsonDataT = JsonConvert.SerializeObject(new { TeacherID = teacher.TeacherID });
            StringContent contentT = new StringContent(jsonDataT, Encoding.UTF8, "application/json");
            HttpResponseMessage responseT = await clientT.PostAsync(urlT, contentT).ConfigureAwait(false);
            string resultT = await responseT.Content.ReadAsStringAsync();
            walletHistory = JsonConvert.DeserializeObject<List<WalletHistoryTeacher>>(resultT);
            for(int i = 0; i < walletHistory.Count; i++)
            {
                if(walletHistory[i].IsPending == 1)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        erroelbl.Text = "You already have 1 pending withdrawal request";
                    });
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        erroelbl.TextColor = Color.Red;
                    });
                    agreebtn.IsVisible = false;
                    break;
                }
            }
            pnentry.IsVisible = false;
            cpentry.IsVisible = false;
            otpentry.IsVisible = false;
            wthdrawbtn.IsVisible = false;

        }
    }
}