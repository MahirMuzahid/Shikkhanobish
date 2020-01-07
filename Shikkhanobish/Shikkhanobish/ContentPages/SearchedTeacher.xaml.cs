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

namespace Shikkhanobish.ContentPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchedTeacher : ContentPage
    {
        private int studentID;
        private string subject;
        public SearchedTeacher(int StudentID, string Subject)
        {
            InitializeComponent();
            studentID = StudentID;
            subject = Subject;
            getTeacherID();
            getTeacher();
        }

        public async void getTeacherID()
        {
            string url = "https://api.shikkhanobish.com/api/Masters/GetTeacherBySubject";
            HttpClient client = new HttpClient();
            string jsonData = JsonConvert.SerializeObject(new { Subject = subject });
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait(true);
            string result = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            var TeacherID = JsonConvert.DeserializeObject<List<TeacherID>>(result);
        }
        public async void getTeacher()
        {
            string url = "https://api.shikkhanobish.com/api/Masters/GetTeacher";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url).ConfigureAwait(true);
            string result = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            var TeacherList = JsonConvert.DeserializeObject<List<Teacher>>(result);
        }
    }
}