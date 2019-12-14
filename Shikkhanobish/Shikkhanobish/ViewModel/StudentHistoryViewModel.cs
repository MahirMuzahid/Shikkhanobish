using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Shikkhanobish
{
    class StudentHistoryViewModel
    {
        public StudentHistoryViewModel()
        {
            ShowTuitionSistoryAsync();
        }

        public async void ShowTuitionSistoryAsync()
        {
            int StudentID = 1017;
            string url = "https://api.shikkhanobish.com/api/Masters/TuitionHsitoryStudent";
            HttpClient client = new HttpClient();
            string jsonData = JsonConvert.SerializeObject(new { StudentID = StudentID });
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait(true);
            var result = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            var RUserName = JsonConvert.DeserializeObject<TuitionHistoryStudent>(result);
        }

    }
}
