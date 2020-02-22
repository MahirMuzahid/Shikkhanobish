﻿using Newtonsoft.Json;
using Shikkhanobish.Model;
using Shikkhanobish.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shikkhanobish.ContentPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RatingPage : ContentPage
    {
        TransferInfo info = new TransferInfo();
        Calculate calculate = new Calculate();
        public RatingPage(TransferInfo trnsInfo)
        {
            InitializeComponent();
            info = trnsInfo;
        }
        public void showEverything()
        {
            
            tnamelbl.Text = info.Teacher.TeacherName;
            tIDlbl.Text = "" + info.Teacher.TeacherID;
            sClasslbl.Text = info.Class;
            sSubject.Text = info.Subject;
            inapptimelbl.Text = "" +info.StudyTimeInAPp;
            costlbl.Text = "" + calculate.CalculateCost(info);
        }

        private void ostarClicked(object sender, EventArgs e)
        {
            RatingColorBox.CornerRadius = 1;
            RatingColorBox.Color = Color.FromHex("#FF5A5A");
            info.GivenRating = 1;
            Ratelbl.Text = "Newbie!!";
        }
        private void tstarClicked(object sender, EventArgs e)
        {
            RatingColorBox.CornerRadius = 2;
            RatingColorBox.Color = Color.FromHex("#F0BE05");
            info.GivenRating = 2;
            Ratelbl.Text = "Avarage!";
        }
        private void thstarClicked(object sender, EventArgs e)
        {
            RatingColorBox.CornerRadius = 3;
            RatingColorBox.Color = Color.FromHex("#3BCF64");
            info.GivenRating = 3;
            Ratelbl.Text = "Good";
        }
        private void fstarClicked(object sender, EventArgs e)
        {
            RatingColorBox.CornerRadius = 4;
            RatingColorBox.Color = Color.FromHex("#50B2ED");
            info.GivenRating = 4;
            Ratelbl.Text = "Veteran!";
        }
        private void fistarClicked(object sender, EventArgs e)
        {
            RatingColorBox.CornerRadius = 5;
            RatingColorBox.Color = Color.FromHex("#B161F3");
            info.GivenRating = 5;
            Ratelbl.Text = "Master!!";
        }

        public async void FinishTHeUpdate()
        {
            string tuitionClass = null;
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
            string url = "https://api.shikkhanobish.com/api/Master/RegisterStudent";
            HttpClient client = new HttpClient();
            string jsonData = JsonConvert.SerializeObject(new
            {
                TeacherID = info.Teacher.TeacherID,
                IsActive = 0,
                IsOnTuition = 0,
                StudentID = info.Student.StundentID,
                Rating = info.GivenRating,
                InAppMin = info.StudyTimeInAPp,
                Tuition_Point = calculate.CalculateTuitionPoint(info),
                Teacher_Rank = calculate.CalculateRank(info),
                Date = Date,
                Subject = info.Subject,
                SubjectName = info.SubjectName,
                Class = tuitionClass,
                IsPenidng = 0,
                Teacher_Name = info.Teacher.TeacherName,
                Student_Name = info.Student.Name
            });
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait(true);
            string result = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            Response responseData = JsonConvert.DeserializeObject<Response>(result);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            FinishTHeUpdate();
        }
    }
}