﻿using System;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Shikkhanobish.Model;
using Shikkhanobish.ViewModel;
using Xamarin.Forms.OpenTok.Service;
using System.Net.Http;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Essentials;
using Android.SE.Omapi;
using Shikkhanobish.ContentPages.Common;

namespace Shikkhanobish.ContentPages
{
    [XamlCompilation ( XamlCompilationOptions.Compile )]
    public partial class PopUpForSelectedTeacher : PopupPage
    {
        private TransferInfo Info;
        string SessionID, Token;
        string permission;
        private async void Button_Clicked ( object sender , EventArgs e )
        {
            try
            {
                ConnectWithTeacher(SessionID, Token, Info.Student.StudentID, Info.Teacher.TeacherID, Info.SubjectName, Info.Class, Info.Teacher.Amount, Info.Teacher.TeacherName);
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current.MainPage.Navigation.PushModalAsync(new CallingPageStudent(Info)).ConfigureAwait(false);
                });

                await Navigation.PopPopupAsync().ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                await Navigation.PushPopupAsync(new PopUpForTextAlert("Error",ex.Message,false )).ConfigureAwait(false);
            }
            
        }

        public async Task ConnectWithTeacher(string SessionId, string UserToken, int studentID, int teacherID, string Cls, string subject, double cost, string studentName)
        {
            string url = "https://shikkhanobishrealtimeapi.shikkhanobish.com/api/ShikkhanobishRealTimeApi/CallTeacher?&SessionId=" + SessionId + "&UserToken=" + UserToken + "&studentID=" + studentID + "&teacherID=" + teacherID + "&Cls=" + Cls + "&subject=" + subject + "&cost=" + cost + "&studentName=" + studentName;
            HttpClient client = new HttpClient();
            StringContent content = new StringContent("", Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait(true);
            string result = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
        }
        public PopUpForSelectedTeacher ( TransferInfo info,string sid, string token)
        {
            InitializeComponent ();
            SessionID = sid ;
            Token = token;
            callbtn.Text = "Call Teacher";
            Info = info;
            tnLbl.Text = Info.Teacher.TeacherName;
            clLbl.Text = Info.Class;
            subLbl.Text = Info.SubjectName;
            ctLbl.Text = "Cost: " + Info.Teacher.Amount + " taka/min";;
        }
        
        




    }
}