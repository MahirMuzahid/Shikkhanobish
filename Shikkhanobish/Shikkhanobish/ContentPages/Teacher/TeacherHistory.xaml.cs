using Newtonsoft.Json;
using Shikkhanobish.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shikkhanobish
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeacherHistory : ContentPage
    {
        private List<TuitionHistoryTeacher> teacherHistory = new List<TuitionHistoryTeacher>();
        int TeacherID;
        public TeacherHistory(int id)
        {
            TeacherID = id;
            InitializeComponent ();
            ShowTeacherStory();
        }

        //Link will be added
        public async void ShowTeacherStory()
        {
            string url = "https://api.shikkhanobish.com/api/Master/GetTuitionHistoryTeacher";
            HttpClient client = new HttpClient();
            string jsonData = JsonConvert.SerializeObject(new { TeacherID = TeacherID });
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait(true);
            var result = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            var hisotyList = JsonConvert.DeserializeObject<List<TuitionHistoryTeacher>>(result);
            teacherHistory = hisotyList;
            TeacherHistoryListView.ItemsSource = teacherHistory;
        }
    }
}