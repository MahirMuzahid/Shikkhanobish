using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shikkhanobish
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterAsTeacher : ContentPage
    {
        public RegisterAsTeacher()
        {
            InitializeComponent();
            BindingContext = new RegisterAsTeacherViewModel();
            DisableCheckkBox();

        }
        public void DisableCheckkBox()
        {
            NTAcochbx.IsEnabled = false;
            NTBiochbx.IsEnabled = false;
            NTBEchbx.IsEnabled = false;
            NTCEchbx.IsEnabled = false;
            NTChechbx.IsEnabled = false;
            NTEcochbx.IsEnabled = false;
            NTGeochbx.IsEnabled = false;
            NTHMchbx.IsEnabled = false;
            NTPhychbx.IsEnabled = false;
            NTFBchbx.IsEnabled = false;

            ETAcochbx.IsEnabled = false;
            ETBio1chbx.IsEnabled = false;
            ETBio2chbx.IsEnabled = false;
            ETChe1chbx.IsEnabled = false;
            ETChe2chbx.IsEnabled = false;
            ETPhy1chbx.IsEnabled = false;
            ETPhy2chbx.IsEnabled = false;
            ETFBchbx.IsEnabled = false;
            ETEcochbx.IsEnabled = false;
            ETM1chbx.IsEnabled = false;
            ETM2chbx.IsEnabled = false;
            ETSttchbx.IsEnabled = false;
            ETZukchbx.IsEnabled = false;

            
        }
    }
}