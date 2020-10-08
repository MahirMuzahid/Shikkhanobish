using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System.Net.Http;
using Newtonsoft.Json;
using Shikkhanobish.Model;

namespace Shikkhanobish.ContentPages.Parents
{
    [XamlCompilation ( XamlCompilationOptions.Compile )]
    public partial class PopupForParentsRegistration : PopupPage
    {
        string gender;
        public PopupForParentsRegistration ( )
        {
            InitializeComponent ();
        }

        private async void Button_Clicked ( object sender , EventArgs e )
        {
            if( pnenty.Text.Length != 11 || pnenty.Text == null )
            {
                errorlbl.Text = "Please checck phone number(Ex: 01xxxxxxxxxxx) ";
                errorlbl.TextColor = Color.Red;
            }
            else if ( passenty.Text.Length < 6 || !passenty.Text.Any ( char.IsUpper ) || !passenty.Text.Any ( char.IsDigit ) )
            {
                errorlbl.Text = "Password length must be at least 6 and must be one Uppercase character and must be one integer(0-9)";
                errorlbl.TextColor = Color.Red;
            }
            else
            {
                string url = "https://api.shikkhanobish.com/api/Master/GetStudentFromParentCode";
                HttpClient client = new HttpClient ();
                string jsonData = JsonConvert.SerializeObject ( new { ParentID = pcodeenty.Text } );// T.D: have to add student ID
                StringContent content = new StringContent ( jsonData , Encoding.UTF8 , "application/json" );
                HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( true );
                var result = await response.Content.ReadAsStringAsync ().ConfigureAwait ( true );
                var student = JsonConvert.DeserializeObject<StudentClass> ( result );

                if ( student.Name == null )
                {
                    errorlbl.Text = "Parent code is not correct!";
                    errorlbl.TextColor = Color.Red;
                }
                else
                {
                    url = "https://api.shikkhanobish.com/api/Master/SetParentInfo";
                    client = new HttpClient ();
                    jsonData = JsonConvert.SerializeObject ( new { ParentID = pcodeenty.Text , ParentName = nameenty.Text , ParentMobileNumber = pnenty.Text , StudentID = student.StudentID , Password = passenty.Text , FatherorMother = gender } );// T.D: have to add student ID
                    content = new StringContent ( jsonData , Encoding.UTF8 , "application/json" );
                    response = await client.PostAsync ( url , content ).ConfigureAwait ( true );
                    result = await response.Content.ReadAsStringAsync ().ConfigureAwait ( true );
                    var r = JsonConvert.DeserializeObject<Response> ( result );
                    if ( r.Status == 0 )
                    {
                        errorlbl.Text = "Registretion done!";
                        errorlbl.TextColor = Color.Gold;
                    }
                    else
                    {
                        errorlbl.Text = "There is a problem!";
                        errorlbl.TextColor = Color.Red;
                    }
                }
            }
            
             
            
        }

        private void Button_Clicked_1 ( object sender , EventArgs e )
        {
            gender = "Father";
            fbtn.BackgroundColor = Color.FromHex ( "#61E1B1" );
            mbtn.BackgroundColor = Color.FromHex ( "#F3F3F3" );
        }

        private void Button_Clicked_2 ( object sender , EventArgs e )
        {
            gender = "Mother";
            fbtn.BackgroundColor = Color.FromHex ( "#F3F3F3" );
            mbtn.BackgroundColor = Color.FromHex ( "#61E1B1" );
        }
    }
}