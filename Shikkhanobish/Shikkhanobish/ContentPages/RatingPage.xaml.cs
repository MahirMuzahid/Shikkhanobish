using Newtonsoft.Json;
using Shikkhanobish.Model;
using Shikkhanobish.ViewModel;
using System;
using System.Net.Http;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Extensions;
using Shikkhanobish.ContentPages.Common;
using Shikkhanobish.ContentPages.Student;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xamarin.Essentials;
using System.Net.WebSockets;

namespace Shikkhanobish.ContentPages
{
    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RatingPage : ContentPage
    {
        private TransferInfo info = new TransferInfo();
        private Calculate calculate = new Calculate();
        public List<OfferAndVoucherSource> offers = new List<OfferAndVoucherSource> ();
        public List<VoucherAndOfferHistory> offerHistory = new List<VoucherAndOfferHistory> ();
        public List<VoucherAndOfferHistory> offerHistoryTeacher = new List<VoucherAndOfferHistory> ();
        bool isft;
        string subjectName;
        public RatingPage(TransferInfo trnsInfo, bool IsFromTuition)
        {
            InitializeComponent ();           
            info = trnsInfo;
            isft = IsFromTuition;
            if ( IsFromTuition == false )
            {
                GetSubjectName ();
                Navigation.PushPopupAsync ( new PopUpForTextAlert ( "Rate Your Teacher Please" , "You didn't rate your teacher last time you took tuition" , false ) );
            }
            else
            {
                subjectName = info.SubjectName;
            }           
            showEverything ();
            CalculateAutomateVoucher ();
        }
        public async void GetSubjectName ()
        {
            subjectName = await SecureStorage.GetAsync ( "subject_name" ).ConfigureAwait ( false );
        }
        public void OffReportBtn()
        {
            rptbtn.IsVisible = false;
        }
        protected override bool OnBackButtonPressed ( )
        {
            Navigation.PushPopupAsync ( new PopUpForTextAlert ( "" , "" , true ) );
            return true;
        }
        
        public void showEverything()
        {
            tnamelbl.Text = info.Teacher.TeacherName;
            tIDlbl.Text = "" + info.Teacher.TeacherID;
            sClasslbl.Text = info.Class;
            sSubject.Text = subjectName;
            inapptimelbl.Text = "" + info.StudyTimeInAPp;
            costlbl.Text = "" + info.StudentCost;
            Ratelbl.Text = "";
            sbtn.IsEnabled = false;
            //SetIsPending ();
        }

        private async void ostarClicked(object sender, EventArgs e)
        {
            sbtn.BackgroundColor = Color.FromHex ( "#6DC8B6" );
            oply.Stroke = Brush.Gray;
            tply.Stroke = Brush.Gray;
            thply.Stroke = Brush.Gray;
            fply.Stroke = Brush.Gray;
            fiply.Stroke = Brush.Gray;
            oply.Fill = Brush.Red;
            tply.Fill = Brush.White;
            thply.Fill = Brush.White;
            fply.Fill = Brush.White;
            fiply.Fill = Brush.White;
            oply.Stroke = Brush.Red;
            pbar.ProgressColor = Color.FromHex ( "#FF5A5A" );
            info.GivenRating = 1;
            Ratelbl.Text = "Newbie";
            Ratelbl.TextColor = Color.FromHex ( "#FF5A5A" );
            sbtn.IsEnabled = true;
            await pbar.ProgressTo ( 0.2, 500 , Easing.Linear );           
           
        }

        private async void tstarClicked(object sender, EventArgs e)
        {
            sbtn.BackgroundColor = Color.FromHex ( "#6DC8B6" );
            oply.Stroke = Brush.Gray;
            tply.Stroke = Brush.Gray;
            thply.Stroke = Brush.Gray;
            fply.Stroke = Brush.Gray;
            fiply.Stroke = Brush.Gray;
            oply.Fill = Brush.Yellow;
            tply.Fill = Brush.Yellow;
            oply.Stroke = Brush.Yellow;
            tply.Stroke = Brush.Yellow;
            thply.Fill = Brush.White;
            fply.Fill = Brush.White;
            fiply.Fill = Brush.White;
            info.GivenRating = 2;
            Ratelbl.Text = "Avarage";
            sbtn.IsEnabled = true;
            Ratelbl.TextColor = Color.FromHex ( "#F0BE05" );
            pbar.ProgressColor = Color.FromHex ( "#F0BE05" );
            await pbar.ProgressTo ( 0.4 , 500 , Easing.Linear );           
            
        }

        private async void thstarClicked(object sender, EventArgs e)
        {
            sbtn.BackgroundColor = Color.FromHex ( "#6DC8B6" );
            oply.Stroke = Brush.Gray;
            tply.Stroke = Brush.Gray;
            thply.Stroke = Brush.Gray;
            fply.Stroke = Brush.Gray;
            fiply.Stroke = Brush.Gray;
            oply.Fill = Brush.Green;
            tply.Fill = Brush.Green;
            thply.Fill = Brush.Green;
            oply.Stroke = Brush.Green;
            tply.Stroke = Brush.Green;
            thply.Stroke = Brush.Green;
            fply.Fill = Brush.White;
            fiply.Fill = Brush.White;
            info.GivenRating = 3;
            Ratelbl.Text = "Good";
            sbtn.IsEnabled = true;
            Ratelbl.TextColor = Color.FromHex ( "#3BCF64" );
            pbar.ProgressColor = Color.FromHex ( "#3BCF64" );
            await pbar.ProgressTo ( 0.6 , 500 , Easing.Linear );         
            

        }

        private async void fstarClicked(object sender, EventArgs e)
        {
            sbtn.BackgroundColor = Color.FromHex ( "#6DC8B6" );
            oply.Stroke = Brush.Gray;
            tply.Stroke = Brush.Gray;
            thply.Stroke = Brush.Gray;
            fply.Stroke = Brush.Gray;
            fiply.Stroke = Brush.Gray;
            oply.Fill = Brush.DeepSkyBlue;
            tply.Fill = Brush.DeepSkyBlue;
            thply.Fill = Brush.DeepSkyBlue;
            fply.Fill = Brush.DeepSkyBlue;
            oply.Stroke = Brush.DeepSkyBlue;
            tply.Stroke = Brush.DeepSkyBlue;
            thply.Stroke = Brush.DeepSkyBlue;
            fply.Stroke = Brush.DeepSkyBlue;
            fiply.Fill = Brush.White;
            info.GivenRating = 4;
            Ratelbl.Text = "Veteran";
            sbtn.IsEnabled = true;
            Ratelbl.TextColor = Color.FromHex ( "#50B2ED" );
            pbar.ProgressColor = Color.FromHex ( "#50B2ED" );
            await pbar.ProgressTo ( 0.8 , 500 , Easing.Linear );           
            

        }

        private async void fistarClicked(object sender, EventArgs e)
        {
            sbtn.BackgroundColor = Color.FromHex ( "#6DC8B6" );
            oply.Stroke = Brush.Gray;
            tply.Stroke = Brush.Gray;
            thply.Stroke = Brush.Gray;
            fply.Stroke = Brush.Gray;
            fiply.Stroke = Brush.Gray;
            oply.Fill = Brush.MediumPurple;
            tply.Fill = Brush.MediumPurple;
            thply.Fill = Brush.MediumPurple;
            fply.Fill = Brush.MediumPurple;
            fiply.Fill = Brush.MediumPurple;
            oply.Stroke = Brush.MediumPurple;
            tply.Stroke = Brush.MediumPurple;
            thply.Stroke = Brush.MediumPurple;
            fply.Stroke = Brush.MediumPurple;
            fiply.Stroke = Brush.MediumPurple;
            pbar.ProgressColor = Color.FromHex ( "#B161F3" );
            info.GivenRating = 5;
            Ratelbl.Text = "Master";
            sbtn.IsEnabled = true;
            Ratelbl.TextColor = Color.FromHex ( "#B161F3" );
            await pbar.ProgressTo ( 1 , 500 , Easing.Linear );            
           
        }

        public async void FinishTHeUpdate()
        {
            int tuitionPoint = calculate.CalculateTuitionPoint ( info );
            info.Teacher.OverallTP = tuitionPoint + info.Teacher.Tuition_Point;
            string tuitionClass = null, 
            rank = calculate.CalculateRank(info);
            String Date = DateTime.Now.ToString();
            for (int i = 0; i < info.Class.Length; i++)
            {
                if (info.Class.Length == 7)
                {
                    tuitionClass = "" + info.Class[6];
                }
                if (info.Class.Length == 8)
                {
                    tuitionClass = "" + info.Class[7];
                    tuitionClass = tuitionClass + info.Class[8];
                }
            }
            string url = "https://api.shikkhanobish.com/api/Master/UpdateInfo";
            HttpClient client = new HttpClient();
            string jsonData = JsonConvert.SerializeObject ( new
            {
                TeacherID = info.Teacher.TeacherID ,
                IsActive = 0 ,
                IsOnTuition = 0 ,
                StudentID = info.Student.StudentID ,
                Rating = info.GivenRating ,
                InAppMin = info.StudyTimeInAPp ,
                Tuition_Point = tuitionPoint ,
                Teacher_Rank = rank ,
                Date = Date ,
                Subject = info.Subject ,
                SubjectName = info.SubjectName ,
                Class = tuitionClass ,
                IsPenidng = 0 ,
                Teacher_Name = info.Teacher.TeacherName ,
                StudentCost = 0 ,
                TeacherEarn = 0,
                Student_Name = info.Student.Name
            } ); ;
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait( false );
            string result = await response.Content.ReadAsStringAsync().ConfigureAwait( false );
            Response responseData = JsonConvert.DeserializeObject<Response>(result);
            DeletePending ();
            backtoProfile ();
        }

        public async void backtoProfile()
        {
            StudentClass student = new StudentClass ();
            string url = "https://api.shikkhanobish.com/api/Master/GetInfoByLogin";
            HttpClient client = new HttpClient();
            string jsonData = JsonConvert.SerializeObject(new { UserName = info.Student.UserName, Password = info.Student.Password });
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait( false );
            string result = await response.Content.ReadAsStringAsync();
            student = JsonConvert.DeserializeObject<StudentClass>(result);
            OldStToNewSt chng = new OldStToNewSt ();

            
            Device.BeginInvokeOnMainThread ( async ( ) =>
            {
                await Application.Current.MainPage.Navigation.PushModalAsync ( new StudentProfile ( chng.Sc_TO_S ( student ) ) ).ConfigureAwait ( false );
            } );
            
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            FinishTHeUpdate();
        }

        private async void Button_Clicked_1 ( object sender , EventArgs e )
        {
            await Navigation.PushPopupAsync ( new PopUpForReport ( info ) ).ConfigureAwait ( false );
        }

        public async void SetIsPending ()
        {
            string url = "https://api.shikkhanobish.com/api/Master/SetPending";
            HttpClient client = new HttpClient ();
            string jsonData = JsonConvert.SerializeObject ( new {  StudentID = info.Student.StudentID , TeacherName = info.Teacher.TeacherName, TeacherID = info.Teacher.TeacherID , Class = info.Class, Subject = info.Subject , Cost = info.StudentCost } );
            StringContent content = new StringContent ( jsonData , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( false );
            string result = await response.Content.ReadAsStringAsync ();
        }
        public async void DeletePending ( )
        {
            string url = "https://api.shikkhanobish.com/api/Master/DeletePending";
            HttpClient client = new HttpClient ();
            string jsonData = JsonConvert.SerializeObject ( new { StudentID = info.Student.StudentID } );
            StringContent content = new StringContent ( jsonData , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( false );
            string result = await response.Content.ReadAsStringAsync ();
        }

        public async Task CalculateAutomateVoucher ()
        {
            await GetVoucherOffers ().ConfigureAwait(false);
            await GetVoucherHistory ().ConfigureAwait(false);
            int limit, amoun;
            int minOrTaka = 0;
            int usedMin = info.StudyTimeInAPp + info.Student.TotalTuitionTIme;

            for( int i = 0; i < offers.Count; i++ )
            {
                Console.WriteLine ( "dff" );
                if ( offers[i].type == "Goal_Taka" )
                {
                    bool isVoucherUsed = false;
                    if( usedMin >=  offers [ i ].limit )
                    {
                        for(int j = 0; j < offerHistory.Count; j++ )
                        {
                            if( offerHistory [ j].voucherID == offers[i].voucherID )
                            {
                                isVoucherUsed = true;
                                continue;
                            }
                        }
                        if(isVoucherUsed == false)
                        {
                            string url = "https://api.shikkhanobish.com/api/Master/AddMinOrAddFundStudent";
                            HttpClient client = new HttpClient ();
                            string jsonData = JsonConvert.SerializeObject ( new { StudentID = info.Student.StudentID, Counter = 2, Amount = offers[i].amount, TeacherID = "" } );
                            StringContent content = new StringContent ( jsonData , Encoding.UTF8 , "application/json" );
                            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( false );
                            var result = await response.Content.ReadAsStringAsync ();
                            var r = JsonConvert.DeserializeObject<Response> ( result );
                            SetVoucherHistory ( offers [ i ].voucherID );
                            int amount = offers [ i ].amount;
                            MainThread.BeginInvokeOnMainThread ( ( ) => { Navigation.PushPopupAsync ( new PopUpForTextAlert ( "Congratulation!!" , "You got " + amount + " points for completing " + usedMin + " min tuition. " , false ) ); } );
                        }
                    }           
                }
                else if ( offers [ i ].type == "Goal_Min" )
                {
                    bool isVoucherUsed = false;
                    if ( usedMin >= offers [ i ].limit )
                    {
                        for ( int j = 0; j < offerHistory.Count; j++ )
                        {
                            if ( offerHistory [ j ].voucherID == offers [ i ].voucherID )
                            {
                                isVoucherUsed = true;
                                continue;
                            }
                        }
                        if ( isVoucherUsed == false )
                        {
                            string url = "https://api.shikkhanobish.com/api/Master/AddMinOrAddFundStudent";
                            HttpClient client = new HttpClient ();
                            string jsonData = JsonConvert.SerializeObject ( new { StudentID = info.Student.StudentID , Counter = 1 , Amount = offers [ i ].amount ,TeacherID = "" } );
                            StringContent content = new StringContent ( jsonData , Encoding.UTF8 , "application/json" );
                            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( false );
                            var result = await response.Content.ReadAsStringAsync ();
                            var r = JsonConvert.DeserializeObject<Response> ( result );
                            SetVoucherHistory ( offers [ i ].voucherID );
                            int amount = offers [ i ].amount;
                            MainThread.BeginInvokeOnMainThread ( ( ) => { Navigation.PushPopupAsync ( new PopUpForTextAlert ( "Congratulation!!" , "You got " + amount + " minuites free for completing " + usedMin + " min tuition. " , false ) ); } );

                        }
                    }
                }
                else if ( offers [ i ].type == "Goal_Taka" )
                {
                    bool isVoucherUsed = false;
                    if ( usedMin >= offers [ i ].limit )
                    {
                        for ( int j = 0; j < offerHistoryTeacher.Count; j++ )
                        {
                            if ( offerHistoryTeacher [ j ].voucherID == offers [ i ].voucherID )
                            {
                                isVoucherUsed = true;
                                continue;
                            }
                        }
                        if ( isVoucherUsed == false )
                        {
                            string url = "https://api.shikkhanobish.com/api/Master/AddMinOrAddFundStudent";
                            HttpClient client = new HttpClient ();
                            string jsonData = JsonConvert.SerializeObject ( new { StudentID = "" , Counter = 3 , Amount = offers [ i ].amount , TeacherID = info.Teacher.TeacherID } );
                            StringContent content = new StringContent ( jsonData , Encoding.UTF8 , "application/json" );
                            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( false );
                            var result = await response.Content.ReadAsStringAsync ();
                            var r = JsonConvert.DeserializeObject<Response> ( result );
                            SetVoucherHistory ( offers [ i ].voucherID );
                            int amount = offers [ i ].amount;
                            MainThread.BeginInvokeOnMainThread ( ( ) => { Navigation.PushPopupAsync ( new PopUpForTextAlert ( "Congratulation!!" , "You got " + amount + " minuites free for completing " + usedMin + " min tuition. " , false ) ); } );

                        }
                    }
                }
            }
        }

       
        public async Task GetVoucherOffers ( )
        {
            string urlT = "https://api.shikkhanobish.com/api/Master/GetVoucherSource";
            HttpClient clientT = new HttpClient ();
            string jsonData = JsonConvert.SerializeObject ( new { } );
            StringContent content = new StringContent ( jsonData , Encoding.UTF8 , "application/json" );
            HttpResponseMessage responseT = await clientT.PostAsync ( urlT , content ).ConfigureAwait ( false );
            string resultT = await responseT.Content.ReadAsStringAsync ();
            var voucherInfo = JsonConvert.DeserializeObject<List<OfferAndVoucherSource>> ( resultT );
            offers = voucherInfo;            
        }

        public async Task GetVoucherHistory ()
        {
            string url = "https://api.shikkhanobish.com/api/Master/GetVoucherInfo";
            HttpClient client = new HttpClient ();
            string jsonData = JsonConvert.SerializeObject ( new { StudentID = info.Student.StudentID } );
            StringContent content = new StringContent ( jsonData , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( false );
            string result = await response.Content.ReadAsStringAsync ();
            offerHistory = JsonConvert.DeserializeObject<List<VoucherAndOfferHistory>> ( result );
        }
        public async Task GetVoucherHistoryTeacher ( )
        {
            string url = "https://api.shikkhanobish.com/api/Master/GetVoucherInfo";
            HttpClient client = new HttpClient ();
            string jsonData = JsonConvert.SerializeObject ( new { StudentID = info.Teacher.TeacherID } );
            StringContent content = new StringContent ( jsonData , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( false );
            string result = await response.Content.ReadAsStringAsync ();
            offerHistoryTeacher = JsonConvert.DeserializeObject<List<VoucherAndOfferHistory>> ( result );
        }
        public async Task SetVoucherHistory ( int vID)
        {
            string url = "https://api.shikkhanobish.com/api/Master/SetVoucherInfo";
            HttpClient client = new HttpClient ();
            string jsonData = JsonConvert.SerializeObject ( new { StudentID = info.Student.StudentID, voucherID  = vID} );
            StringContent content = new StringContent ( jsonData , Encoding.UTF8 , "application/json" );
            HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( false );
            string result = await response.Content.ReadAsStringAsync ();
            var r = JsonConvert.DeserializeObject<Response> ( result );
        }

        private async void voucher_Clicked ( object sender , EventArgs e )
        {
            bool isVoucherUsed = false;
            int count = 0;
            for (int i = 0; i < offers.Count; i++ )
            { 
                if(offers[i].code == voucherentry.Text)
                {
                    count++;
                    for ( int j = 0; j < offerHistory.Count; j++ )
                    {
                        if ( offerHistory [ j ].voucherID == offers [ i ].voucherID )
                        {
                            isVoucherUsed = true;
                            continue;
                        }
                    }
                    if ( isVoucherUsed == false )
                    {
                        float cut = 0;
                        string url = "https://api.shikkhanobish.com/api/Master/AddAmountOnAcceptedVoucher";
                        HttpClient client = new HttpClient ();
                        if ( info.StudyTimeInAPp > offers [ i ].limit )
                        {
                            cut = ( offers [ i ].amount * offers [ i ].limit ) / 100;
                        }
                        else
                        {
                            cut = ( offers [ i ].amount * info.StudyTimeInAPp ) / 100f;
                        }
                        string jsonData = JsonConvert.SerializeObject ( new { StudentID = info.Student.StudentID , amount = cut } );
                        StringContent content = new StringContent ( jsonData , Encoding.UTF8 , "application/json" );
                        HttpResponseMessage response = await client.PostAsync ( url , content ).ConfigureAwait ( false );
                        var result = await response.Content.ReadAsStringAsync ();
                        var r = JsonConvert.DeserializeObject<Response> ( result );
                        SetVoucherHistory ( offers [ i ].voucherID );
                        MainThread.BeginInvokeOnMainThread ( ( ) => {
                            costlbl.Text = info.StudentCost + " - " + offers [ i ].amount + "% = " + cut;
                            costlbl.BackgroundColor = Color.ForestGreen;
                        } );
                        break;
                    }
                }               
            }
            if (  count != offers.Count)
            {
                if( isVoucherUsed == true)
                {
                    MainThread.BeginInvokeOnMainThread ( ( ) => { Navigation.PushPopupAsync ( new PopUpForTextAlert ( "Invalid Voucher" , "This voucher already used" , false ) ); } );
                    
                }
                else
                {
                    MainThread.BeginInvokeOnMainThread ( ( ) => { Navigation.PushPopupAsync ( new PopUpForTextAlert ( "Invalid Voucher" , "This voucher is not applicable" , false ) ); } );
                }
                
            }
        }
    }
}