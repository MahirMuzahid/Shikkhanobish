using Newtonsoft.Json;
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
    public partial class SearchedTeacher : ContentPage
    {
        TransferInfo info = new TransferInfo();
        List<TeacherID> TeacherIDListBySearch = new List<TeacherID>();
        List<Teacher> TeacherList = new List<Teacher>();
        List<Teacher> FilteredTeacher = new List<Teacher>();
        public SearchedTeacher(TransferInfo transInfo)
        {
            InitializeComponent();
            info = transInfo;
            getTeacherID();
            getTeacher();



        }

        public async void getTeacherID()
        {
            string url = "https://api.shikkhanobish.com/api/Masters/TeacherIDListFromSubject";
            HttpClient client = new HttpClient();
            string jsonData = JsonConvert.SerializeObject(new { Subject = info.Subject });
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait(true);
            string result = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            TeacherIDListBySearch = JsonConvert.DeserializeObject<List<TeacherID>>(result);
        }
        public async void getTeacher()
        {
            string urlN = "https://api.shikkhanobish.com/api/Masters/GetTeacher";
            HttpClient clientN = new HttpClient();
            HttpResponseMessage responseN = await clientN.GetAsync(urlN).ConfigureAwait(true);
            string resultN = await responseN.Content.ReadAsStringAsync().ConfigureAwait(true);
            TeacherList = JsonConvert.DeserializeObject<List<Teacher>>(resultN);
            getSearchedTeacherInList();
        }



        //I fking have to change isActive to 1 Come on
        public void getSearchedTeacherInList()
        {
            int c = 0;
            float avg = 0f;
            for(int i = 0; i < TeacherList.Count; i++)
            {
                for(int j = 0; j < TeacherIDListBySearch.Count; j++)
                {
                    if((TeacherList[i].TeacherID == TeacherIDListBySearch[j].teacherID) && TeacherList[i].IsActive == 0 && TeacherList[i].IsOnTuition == 0)
                    {
                        FilteredTeacher.Add(TeacherList[i]);                       
                    }
                }
            }
            SetEveryThing();
        }
        public void SetEveryThing()
        {
            TeacherListView.ItemsSource = FilteredTeacher;
        }

        private async void TeacherListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedTeacher = e.Item as Teacher;
            info.Teacher = selectedTeacher;
            await Application.Current.MainPage.Navigation.PushModalAsync(new TutionPage(info)).ConfigureAwait(true);
        }
    }
}