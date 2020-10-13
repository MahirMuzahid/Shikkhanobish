using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Shikkhanobish.Model;
using Shikkhanobish.ViewModel;
using Xamarin.Essentials;
using Rg.Plugins.Popup.Extensions;

namespace Shikkhanobish.ContentPages.Parents
{
    [XamlCompilation ( XamlCompilationOptions.Compile )]
    public partial class PopUpForLoginParents : PopupPage
    {
        public PopUpForLoginParents ( )
        {
            InitializeComponent ();
            loginbtn.Text = "Login";
            errorlbl.Text = "";
        }

        private async void Button_Clicked ( object sender , EventArgs e )
        {
            try
            {
                loginbtn.Text = "Wait";
                string code = pcodeenty.Text;
                string pass = passenty.Text;
                if (code.Length == 0)
                {
                    MainThread.BeginInvokeOnMainThread( () =>
                    {
                        errorlbl.Text = "Password can't be empty";
                    });
                }
                else if (code.Length == 0)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        errorlbl.Text = "Code can't be empty";
                    });
                }
                else
                {
                    string url = "https://api.shikkhanobish.com/api/Master/GetParentInfo";
                    using (HttpClient client = new HttpClient())
                    {
                        string jsonData = JsonConvert.SerializeObject(new { ParentID = code, Password = pass });
                        StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait(true);
                        var result = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                        var parent = JsonConvert.DeserializeObject<Parent>(result);
                        if (parent.ParentName == null)
                        {
                            MainThread.BeginInvokeOnMainThread(() =>
                            {
                                errorlbl.Text = parent.Response;
                            });
                            loginbtn.Text = "Login";
                        }
                        else
                        {
                            await Navigation.PopPopupAsync().ConfigureAwait(false);
                            StaticPageForOnSleep.isParent = true;
                            MainThread.BeginInvokeOnMainThread(async () => { await Application.Current.MainPage.Navigation.PushModalAsync(new ParentsProfile(parent)).ConfigureAwait(false); });
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                MainThread.BeginInvokeOnMainThread( () =>
                {
                    errorlbl.Text = ex.Message;
                });
            }
            
            
        }

        private async void Button_Clicked_1 ( object sender , EventArgs e )
        {
            await Navigation.PopPopupAsync ().ConfigureAwait ( false );
            await Application.Current.MainPage.Navigation.PushPopupAsync ( new PopupForParentsRegistration () ).ConfigureAwait ( false );
        }

        private void Button_Clicked_2 ( object sender , EventArgs e )
        {

        }
    }
}