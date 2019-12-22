using Newtonsoft.Json;
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
    public partial class TeacherHistory : ContentPage
    {
        List<TeacherHistory> teacherHistory = new List<TeacherHistory>();
        public TeacherHistory()
        {
            InitializeComponent();
            ShowTeacherStory();
        }
        //Link will be added
        public async void ShowTeacherStory()
        {
            int TeacherID = 1017;
            string url = "LINK";
            HttpClient client = new HttpClient();
            string jsonData = JsonConvert.SerializeObject(new { StundentID = TeacherID });
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait(true);
            var result = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            var hisotyList = JsonConvert.DeserializeObject<List<TeacherHistory>>(result);
            teacherHistory = hisotyList;
            TeacherHistoryListView.ItemsSource = teacherHistory;
        }
    }
}