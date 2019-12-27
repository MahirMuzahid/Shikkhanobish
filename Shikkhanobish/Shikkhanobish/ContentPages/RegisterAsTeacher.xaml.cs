using Newtonsoft.Json;
using Shikkhanobish.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shikkhanobish
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterAsTeacher : ContentPage
    {
        RegisterTeacher registerteacher =  new RegisterTeacher();
        Teacher teacher = new Teacher();
        int taken = 10000000, studentID;
        string institutionID;
        public RegisterAsTeacher(int StudentID, string InstitutionID)
        {
            studentID = StudentID;
            institutionID = institutionID;
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

        private async void Button_Clicked(object sender, EventArgs e)
        {       
            bool checkCommon = CheckCommon();
            bool checkGroup = CheckGroup();
            CheckHighSchool();
            teacher.InstitutionID = institutionID;
            teacher.StudentID = studentID;
            if(checkCommon == true && checkGroup == true)
            {
                registerteacher.TeacherID = studentID + 100000 ; 
                string url = "https://api.shikkhanobish.com/api/Masters/RegisterTeacher";
                HttpClient client = new HttpClient();
                string jsonData = JsonConvert.SerializeObject(new {
                    TeacherID = registerteacher.TeacherID,
                    LSBAN01 = registerteacher.LSBAN01,
                    LSBAN02 = registerteacher.LSBAN02,
                    LSENG01 = registerteacher.LSENG01,
                    LSENG02 = registerteacher.LSENG02,
                    LSICT = registerteacher.LSICT,
                    LSBGS = registerteacher.LSBGS,
                    LSAGR = registerteacher.LSAGR,
                    LSCRE = registerteacher.LSCRE,
                    LSGSC = registerteacher.LSGSC,
                    LSMATH = registerteacher.LSMATH,
                    SBAN01 = registerteacher.SBAN01,
                    SBAN02 = registerteacher.SBAN02,
                    SENG01 = registerteacher.SENG01,
                    SENG02 = registerteacher.SENG02,
                    SGMATH = registerteacher.SGMATH,
                    SREL = registerteacher.SREL,
                    SICT = registerteacher.SICT,
                    SGSC = registerteacher.SGSC,
                    SPHY = registerteacher.SPHY,
                    SCHE = registerteacher.SCHE,
                    SBIO = registerteacher.SBIO,
                    SHMATH = registerteacher.LSMATH,
                    SECO = registerteacher.SECO,
                    SACC = registerteacher.SACC,
                    SFIN = registerteacher.SFIN,
                    SAGR = registerteacher.SAGR,
                    SHOM = registerteacher.SHOM,
                    SBENT = registerteacher.SBENT,
                    SCRE = registerteacher.SCRE,
                    SBGS = registerteacher.SBGS,
                    SGEO = registerteacher.SGEO,
                    SPEDU = registerteacher.SPEDU,
                    HSBAN01 = registerteacher.HSBAN01,
                    HSBAN02 = registerteacher.HSBAN02,
                    HSENG01 = registerteacher.HSENG01,
                    HSENG02 = registerteacher.HSENG02,
                    HSPHY01 = registerteacher.HSPHY01,
                    HSPHY02 = registerteacher.HSPHY02,
                    HSCHE01 = registerteacher.HSCHE01,
                    HSCHE02 = registerteacher.HSCHE02,
                    HSBIO01 = registerteacher.HSBIO01,
                    HSBIO02 = registerteacher.HSBIO02,
                    HSMATH01 = registerteacher.HSMATH01,
                    HSMATH02 = registerteacher.HSMATH02,
                    HSICT = registerteacher.HSICT,
                    HSSTAT = registerteacher.HSSTAT,
                    HSLOG = registerteacher.HSLOG,
                    HSFOOD = registerteacher.HSFOOD,
                    HSFIN = registerteacher.HSFIN,
                    HSACC = registerteacher.HSACC,
                    HSECO = registerteacher.HSECO
                });
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait(true);
                string result = await response.Content.ReadAsStringAsync();
                var responsE = JsonConvert.DeserializeObject<Response>(result);
                if(responsE.Status == 1)
                {
                    ErrorText.Text = responsE.Massage;
                }
                else if (responsE.Status == 0)
                {
                    ErrorText.Text = responsE.Massage;
                    //await Application.Current.MainPage.Navigation.PushModalAsync(new TeacherProfile()).ConfigureAwait(true);
                }

            }
        }

        public bool CheckCommon()
        {
            int checkCounter = 0;
            bool check = true;
            if (NTB1chbx.IsChecked == true)
            {
                checkCounter++;
                registerteacher.SBAN01 = taken;
            }
            if(NTB2chbx.IsChecked == true)
            {
                checkCounter++;
                registerteacher.SBAN02 = taken;
            }
            if(NTE1chbx.IsChecked == true)
            {
                checkCounter++;
                registerteacher.SENG01 = taken;
            }
            if(NTE2chbx.IsChecked == true)
            {
                checkCounter++;
                registerteacher.SENG02 = taken;
            }

            if(NTGMchbx.IsChecked == true)
            {
                checkCounter++;
                registerteacher.SGMATH = taken;
            }
            if(NTRchbx.IsChecked == true)
            {
                checkCounter++;
                registerteacher.SREL = taken;
            }
            if(NTBGSchbx.IsChecked == true)
            {
                checkCounter++;
                registerteacher.SBGS = taken;
            }
            if(NTICTchbx.IsChecked == true)
            {
                checkCounter++;
                registerteacher.SICT = taken;
            }
            if(checkCounter > 4)
            {
                ErrorText.Text = "You cant take more than 4 subject in common subject of class 9-10";
                registerteacher.SICT = 0;
                registerteacher.SBGS = 0;
                registerteacher.SREL = 0;
                registerteacher.SGMATH = 0;
                registerteacher.SENG02 = 0;
                registerteacher.SENG01 = 0;
                registerteacher.SBAN02 = 0;
                registerteacher.SBAN01 = 0;
                check = false;
            }
            if(check == true)
            {
                checkCounter = 0;
                if (ETB1chbx.IsChecked == true)
                {
                    checkCounter++;
                    registerteacher.HSBAN01 = taken;
                }
                if (ETB2chbx.IsChecked == true)
                {
                    checkCounter++;
                    registerteacher.HSBAN02 = taken;
                }
                if (ETE1chbx.IsChecked == true)
                {
                    checkCounter++;
                    registerteacher.HSENG01 = taken;
                }
                if (ETE2chbx.IsChecked == true)
                {
                    checkCounter++;
                    registerteacher.HSENG02 = taken;
                }
                if (ETICTchbx.IsChecked == true)
                {
                    checkCounter++;
                    registerteacher.HSICT = taken;
                }
                if (checkCounter > 3)
                {
                    ErrorText.Text = "You cant take more than 3 subject in common subject of class 11-12";
                    registerteacher.HSBAN01 = taken;
                    registerteacher.HSBAN02 = taken;
                    registerteacher.HSENG01 = taken;
                    registerteacher.HSENG02 = taken;
                    registerteacher.HSICT = taken;
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
                    registerteacher.SBIO = taken;
                }
                if (NTChechbx.IsChecked == true)
                {
                    checkCounter++;
                    registerteacher.SCHE = taken;
                }
                if (NTHMchbx.IsChecked == true)
                {
                    checkCounter++;
                    registerteacher.SHMATH = taken;
                }
                if (NTPhychbx.IsChecked == true)
                {
                    checkCounter++;
                    registerteacher.SPHY = taken;
                }
                if (checkCounter > 3)
                {
                    ErrorText.Text = "You can't take more than 3 subject from Science group in class 9-10";
                    registerteacher.SBIO = 0;
                    registerteacher.SCHE = 0;
                    registerteacher.SHMATH = 0;
                    registerteacher.SPHY = 0;
                    check = false;
                }
            }
            if (NTCommercchbx.IsChecked == true)
            {
                if (NTBEchbx.IsChecked == true)
                {
                    checkCounter++;
                    registerteacher.SBENT = taken;
                }
                if (NTAcochbx.IsChecked == true)
                {
                    checkCounter++;
                    registerteacher.SACC = taken;
                }
                if (NTEcochbx.IsChecked == true)
                {
                    checkCounter++;
                    registerteacher.SECO = taken;
                }
                if (NTFBchbx.IsChecked == true)
                {
                    checkCounter++;
                    registerteacher.SFIN = taken;
                }
                if (checkCounter > 3)
                {
                    ErrorText.Text = "You can't take more than 3 subject from Commerce group in class 9-10";
                    registerteacher.SBENT = 0;
                    registerteacher.SACC = 0;
                    registerteacher.SECO = 0;
                    registerteacher.SFIN = 0;
                    check = false;
                }
            }
            checkCounter = 0;
            if (NTArtschbx.IsChecked == true)
            {
                if (NTCEchbx.IsChecked == true)
                {
                    registerteacher.SCRE = taken;
                }
                if(NTGeochbx.IsChecked == true)
                {
                    registerteacher.SGEO = taken;
                }
            }
            if (ETSciencechbx.IsChecked == true)
            {
                if (ETBio1chbx.IsChecked == true)
                {
                    checkCounter++;
                    registerteacher.HSBIO01 = taken;
                }
                if (ETBio2chbx.IsChecked == true)
                {
                    checkCounter++;
                    registerteacher.HSBIO02 = taken;
                }
                if (ETChe1chbx.IsChecked == true)
                {
                    checkCounter++;
                    registerteacher.HSCHE01 = taken;

                }
                if (ETChe2chbx.IsChecked == true)
                {
                    checkCounter++;
                    registerteacher.HSCHE02 = taken;
                }
                if (ETPhy1chbx.IsChecked == true)
                {
                    checkCounter++;
                    registerteacher.HSPHY01 = taken;
                }
                if (ETPhy2chbx.IsChecked == true)
                {
                    checkCounter++;
                    registerteacher.HSPHY02 = taken;
                }
                if (ETM1chbx.IsChecked == true)
                {
                    checkCounter++;
                    registerteacher.HSMATH01 = taken;
                }
                if (ETM2chbx.IsChecked == true)
                {
                    checkCounter++;
                    registerteacher.HSMATH02 = taken;
                }
                if (checkCounter > 6)
                {
                    ErrorText.Text = "You can't take more than 6 subject from Science group in class 11-12";
                    registerteacher.HSBIO01 = 0;
                    registerteacher.HSBIO02 = 0;
                    registerteacher.HSCHE02 = 0;
                    registerteacher.HSCHE01 = 0;
                    registerteacher.HSPHY01 = 0;
                    registerteacher.HSPHY02 = 0;
                    registerteacher.HSMATH01 = 0;
                    registerteacher.HSMATH02 = 0;
                    check = false;
                }
            }
            if (ETCommercchbx.IsChecked == true)
            {
                if(ETAcochbx.IsChecked == true)
                {
                    checkCounter++;
                    registerteacher.HSACC = taken;
                }
                if(ETFBchbx.IsChecked == true)
                {
                    checkCounter++;
                    registerteacher.HSFIN = taken;
                }
                if(ETEcochbx.IsChecked == true)
                {
                    checkCounter++;
                    registerteacher.HSECO = taken;
                }
                if(ETSttchbx.IsChecked == true)
                {
                    checkCounter++;
                    registerteacher.HSSTAT = taken;
                }
                if (checkCounter > 6)
                {
                    ErrorText.Text = "You can't take more than 3 subject from Commerce group in class 11-12";
                    registerteacher.HSACC = 0;
                    registerteacher.HSFIN = 0;
                    registerteacher.HSECO = 0;
                    registerteacher.HSSTAT = 0;
                    check = false;
                }
            }
            if (ETArtschbx.IsChecked == true)
            {
                if(ETZukchbx.IsChecked == true)
                {
                    registerteacher.HSLOG = taken;
                }
            }
            return check;
        }
        public void CheckHighSchool()
        {
            if(SEchbx.IsChecked == true)
            {
                registerteacher.LSAGR = taken;
                registerteacher.LSBAN01 = taken;
                registerteacher.LSBAN02 = taken;
                registerteacher.LSBGS = taken;
                registerteacher.LSCRE = taken;
                registerteacher.LSENG01 = taken;
                registerteacher.LSENG02 = taken;
                registerteacher.LSGSC = taken;
                registerteacher.LSICT = taken;
                registerteacher.LSMATH = taken;
            }

        }
    }
}