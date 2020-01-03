using Newtonsoft.Json;
using Syncfusion.XForms.Graphics;
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
    public partial class StudentHistory : ContentPage
    {
        private List<TuitionHistoryStudent> studentHistory = new List<TuitionHistoryStudent>();
        public StudentHistory()
        {
            InitializeComponent();
            ShowTuitionSistoryAsync();
        }
        public async void ShowTuitionSistoryAsync()
        {
            int StudentID = 1017;
            string url = "https://api.shikkhanobish.com/api/Masters/GetTuitionHistoryStudent";
            HttpClient client = new HttpClient();
            string jsonData = JsonConvert.SerializeObject(new { StundentID = StudentID });
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait(true);
            var result = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            var hisotyList = JsonConvert.DeserializeObject<List<TuitionHistoryStudent>>(result);
            studentHistory = hisotyList;
            StudentHistoryListView.ItemsSource = studentHistory;
        }
    }
}