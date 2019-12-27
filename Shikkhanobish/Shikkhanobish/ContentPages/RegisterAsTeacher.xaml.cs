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

        private void NTSciencechbx_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (NTSciencechbx.IsChecked == true)
            {
                NTAcochbx.IsEnabled = false;
                NTBiochbx.IsEnabled = true;
                NTBEchbx.IsEnabled = false;
                NTCEchbx.IsEnabled = false;
                NTChechbx.IsEnabled = true;
                NTEcochbx.IsEnabled = false;
                NTGeochbx.IsEnabled = false;
                NTHMchbx.IsEnabled = true;
                NTPhychbx.IsEnabled = true;
                NTFBchbx.IsEnabled = false;
                NTCommercchbx.IsChecked = false;
                NTArtschbx.IsChecked = false;
            }
            else
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

                NTAcochbx.IsChecked = false;
                NTBiochbx.IsChecked = false;
                NTBEchbx.IsChecked = false;
                NTCEchbx.IsChecked = false;
                NTChechbx.IsChecked = false;
                NTEcochbx.IsChecked = false;
                NTGeochbx.IsChecked = false;
                NTHMchbx.IsChecked = false;
                NTPhychbx.IsChecked = false;
                NTFBchbx.IsChecked = false;
            }
        }

        private void NTCommercchbx_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (NTCommercchbx.IsChecked == true)
            {
                NTAcochbx.IsEnabled = true;
                NTBiochbx.IsEnabled = false;
                NTBEchbx.IsEnabled = true;
                NTCEchbx.IsEnabled = false;
                NTChechbx.IsEnabled = false;
                NTEcochbx.IsEnabled = true;
                NTGeochbx.IsEnabled = false;
                NTHMchbx.IsEnabled = false;
                NTPhychbx.IsEnabled = false;
                NTFBchbx.IsEnabled = true;
                NTArtschbx.IsChecked = false;
                NTSciencechbx.IsChecked = false;
            }
            else
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

                NTAcochbx.IsChecked = false;
                NTBiochbx.IsChecked = false;
                NTBEchbx.IsChecked = false;
                NTCEchbx.IsChecked = false;
                NTChechbx.IsChecked = false;
                NTEcochbx.IsChecked = false;
                NTGeochbx.IsChecked = false;
                NTHMchbx.IsChecked = false;
                NTPhychbx.IsChecked = false;
                NTFBchbx.IsChecked = false;
            }
        }

        private void NTArtschbx_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if(NTArtschbx.IsChecked == true)
            {
                NTAcochbx.IsEnabled = false;
                NTBiochbx.IsEnabled = false;
                NTBEchbx.IsEnabled = false;
                NTCEchbx.IsEnabled = true;
                NTChechbx.IsEnabled = false;
                NTEcochbx.IsEnabled = false;
                NTGeochbx.IsEnabled = true;
                NTHMchbx.IsEnabled = false;
                NTPhychbx.IsEnabled = false;
                NTFBchbx.IsEnabled = false;
                NTSciencechbx.IsChecked = false;
                NTCommercchbx.IsChecked = false;
            }
            else
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

                NTAcochbx.IsChecked = false;
                NTBiochbx.IsChecked = false;
                NTBEchbx.IsChecked = false;
                NTCEchbx.IsChecked = false;
                NTChechbx.IsChecked = false;
                NTEcochbx.IsChecked = false;
                NTGeochbx.IsChecked = false;
                NTHMchbx.IsChecked = false;
                NTPhychbx.IsChecked = false;
                NTFBchbx.IsChecked = false;
            }           
        }

        private void ETSciencechbx_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (ETSciencechbx.IsChecked == true)
            {
                ETBio1chbx.IsEnabled = true;
                ETBio2chbx.IsEnabled = true;
                ETChe1chbx.IsEnabled = true;
                ETChe2chbx.IsEnabled = true;
                ETPhy1chbx.IsEnabled = true;
                ETPhy2chbx.IsEnabled = true;
                ETM1chbx.IsEnabled = true;
                ETM2chbx.IsEnabled = true;
                ETAcochbx.IsEnabled = false;
                ETFBchbx.IsEnabled = false;
                ETEcochbx.IsEnabled = false;
                ETM1chbx.IsEnabled = true;
                ETM2chbx.IsEnabled = true;
                ETSttchbx.IsEnabled = false;
                ETZukchbx.IsEnabled = false;
                ETCommercchbx.IsChecked = false;
                ETArtschbx.IsChecked = false;
            }
            else
            {
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

                ETAcochbx.IsChecked= false;
                ETBio1chbx.IsChecked= false;
                ETBio2chbx.IsChecked= false;
                ETChe1chbx.IsChecked= false;
                ETChe2chbx.IsChecked= false;
                ETPhy1chbx.IsChecked= false;
                ETPhy2chbx.IsChecked= false;
                ETFBchbx.IsChecked= false;
                ETEcochbx.IsChecked= false;
                ETM1chbx.IsChecked= false;
                ETM2chbx.IsChecked= false;
                ETSttchbx.IsChecked= false;
                ETZukchbx.IsChecked= false;
            }          
        }

        private void ETCommercchbx_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (ETCommercchbx.IsChecked == true)
            {
                ETBio1chbx.IsEnabled = false;
                ETBio2chbx.IsEnabled = false;
                ETChe1chbx.IsEnabled = false;
                ETChe2chbx.IsEnabled = false;
                ETPhy1chbx.IsEnabled = false;
                ETPhy2chbx.IsEnabled = false;
                ETAcochbx.IsEnabled = true;
                ETFBchbx.IsEnabled = true;
                ETEcochbx.IsEnabled = true;
                ETSttchbx.IsEnabled = true;
                ETM1chbx.IsEnabled = false;
                ETM2chbx.IsEnabled = false;

                ETZukchbx.IsEnabled = false;
                ETSciencechbx.IsChecked = false;
                ETArtschbx.IsChecked = false;
            }
            else
            {
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

                ETAcochbx.IsChecked = false;
                ETBio1chbx.IsChecked = false;
                ETBio2chbx.IsChecked = false;
                ETChe1chbx.IsChecked = false;
                ETChe2chbx.IsChecked = false;
                ETPhy1chbx.IsChecked = false;
                ETPhy2chbx.IsChecked = false;
                ETFBchbx.IsChecked = false;
                ETEcochbx.IsChecked = false;
                ETM1chbx.IsChecked = false;
                ETM2chbx.IsChecked = false;
                ETSttchbx.IsChecked = false;
                ETZukchbx.IsChecked = false;
            }          
        }

        private void ETArtschbx_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (ETArtschbx.IsChecked == true)
            {
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
                ETZukchbx.IsEnabled = true;
                ETSciencechbx.IsChecked = false;
                ETCommercchbx.IsChecked = false;
            }
            else
            {
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

                ETAcochbx.IsChecked = false;
                ETBio1chbx.IsChecked = false;
                ETBio2chbx.IsChecked = false;
                ETChe1chbx.IsChecked = false;
                ETChe2chbx.IsChecked = false;
                ETPhy1chbx.IsChecked = false;
                ETPhy2chbx.IsChecked = false;
                ETFBchbx.IsChecked = false;
                ETEcochbx.IsChecked = false;
                ETM1chbx.IsChecked = false;
                ETM2chbx.IsChecked = false;
                ETSttchbx.IsChecked = false;
                ETZukchbx.IsChecked = false;
            }            
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            int taken = 10000000;
            int notTaken = 0;
       
            bool checkCommon = CheckCommon();
            bool checkGroup = CheckGroup();
            if(checkCommon == true && checkGroup == true)
            {

            }
        }

        public bool CheckCommon()
        {
            int checkCounter = 0;
            bool check = true;
            if (NTB1chbx.IsChecked == true)
            {
                checkCounter++;
            }
            if(NTB2chbx.IsChecked == true)
            {
                checkCounter++;
            }
            if(NTE1chbx.IsChecked == true)
            {
                checkCounter++;
            }
            if(NTE2chbx.IsChecked == true)
            {
                checkCounter++;
            }

            if(NTGMchbx.IsChecked == true)
            {
                checkCounter++;
            }
            if(NTRchbx.IsChecked == true)
            {
                checkCounter++;
            }
            if(NTBGSchbx.IsChecked == true)
            {
                checkCounter++;
            }
            if(NTICTchbx.IsChecked == true)
            {
                checkCounter++;
            }
            if(checkCounter > 4)
            {
                ErrorText.Text = "You cant take more than 4 subject in common subject of class 9-10";
                check = false;
            }
            if(check == true)
            {
                checkCounter = 0;
                if (ETB1chbx.IsChecked == true)
                {
                    checkCounter++;
                }
                if (ETB2chbx.IsChecked == true)
                {
                    checkCounter++;
                }
                if (ETE1chbx.IsChecked == true)
                {
                    checkCounter++;
                }
                if (ETE2chbx.IsChecked == true)
                {
                    checkCounter++;
                }
                if (ETICTchbx.IsChecked == true)
                {
                    checkCounter++;
                }
                if (checkCounter > 3)
                {
                    ErrorText.Text = "You cant take more than 3 subject in common subject of class 11-12";
                    check = false;
                }
            }
            return check;
           
        }
        public bool CheckGroup()
        {
            bool check = true;
            int checkCounter = 0;

            if (NTSciencechbx.IsChecked == true)
            {
                if (NTBiochbx.IsChecked == true)
                {
                    checkCounter++;
                }
                if (NTChechbx.IsChecked == true)
                {
                    checkCounter++;
                }
                if (NTHMchbx.IsChecked == true)
                {
                    checkCounter++;
                }
                if (NTPhychbx.IsChecked == true)
                {
                    checkCounter++;
                }
                if (checkCounter > 3)
                {
                    ErrorText.Text = "You can't take more than 3 subject from Science group in class 9-10";
                    check = false;
                }
            }
            else if (NTCommercchbx.IsChecked == true)
            {
                if (NTBEchbx.IsChecked == true)
                {
                    checkCounter++;
                }
                if (NTAcochbx.IsChecked == true)
                {
                    checkCounter++;
                }
                if (NTEcochbx.IsChecked == true)
                {
                    checkCounter++;
                }
                if (NTFBchbx.IsChecked == true)
                {
                    checkCounter++;
                }
                if (checkCounter > 3)
                {
                    ErrorText.Text = "You can't take more than 3 subject from Commerce group in class 9-10";
                    check = false;
                }
            }
            else if (ETSciencechbx.IsChecked == true)
            {
                if (ETBio1chbx.IsChecked == true)
                {
                    checkCounter++;
                }
                if (ETBio2chbx.IsChecked == true)
                {
                    checkCounter++;
                }
                if (ETChe1chbx.IsChecked == true)
                {
                    checkCounter++;

                }
                if (ETChe2chbx.IsChecked == true)
                {
                    checkCounter++;

                }
                if (ETPhy1chbx.IsChecked == true)
                {
                    checkCounter++;

                }
                if (ETPhy2chbx.IsChecked == true)
                {
                    checkCounter++;

                }
                if (ETM1chbx.IsChecked == true)
                {
                    checkCounter++;

                }
                if (ETM2chbx.IsChecked == true)
                {
                    checkCounter++;

                }
                if (checkCounter > 6)
                {
                    ErrorText.Text = "You can't take more than 6 subject from Science group in class 11-12";
                    check = false;
                }
            }
            else if (ETCommercchbx.IsChecked == true)
            {
                if(ETAcochbx.IsChecked == true)
                {
                    checkCounter++;
                }
                if(ETFBchbx.IsChecked == true)
                {
                    checkCounter++;
                }
                if(ETEcochbx.IsChecked == true)
                {
                    checkCounter++;
                }
                if(ETSttchbx.IsChecked == true)
                {
                    checkCounter++;
                }
                if (checkCounter > 6)
                {
                    ErrorText.Text = "You can't take more than 3 subject from Commerce group in class 11-12";
                    check = false;
                }
            }
            return check;
        }
    }
}