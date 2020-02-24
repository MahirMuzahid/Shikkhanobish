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
            string url = "https://api.shikkhanobish.com/api/Master/TeacherIDListFromSubject";
            HttpClient client = new HttpClient();
            string jsonData = JsonConvert.SerializeObject(new { Subject = info.Subject });
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait(true);
            string result = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            TeacherIDListBySearch = JsonConvert.DeserializeObject<List<TeacherID>>(result);
        }
        public async void getTeacher()
        {
            string urlN = "https://api.shikkhanobish.com/api/Master/GetTeacher";
            HttpClient clientN = new HttpClient();
            HttpResponseMessage responseN = await clientN.GetAsync(urlN).ConfigureAwait(true);
            string resultN = await responseN.Content.ReadAsStringAsync().ConfigureAwait(true);
            TeacherList = JsonConvert.DeserializeObject<List<Teacher>>(resultN);
            getSearchedTeacherInList();
        }



        //I fking have to change isActive to 1 Come on
        public void getSearchedTeacherInList()
        {
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
            Perfection();
            TeacherListView.ItemsSource = FilteredTeacher;
        }

        public void Perfection()
        {
            int total,count;
            for (int i = 0; i < FilteredTeacher.Count(); i++)
            {
                total = FilteredTeacher[i].Five_Star*5+FilteredTeacher[i].Four_Star * 4+FilteredTeacher[i].Three_Star * 3+FilteredTeacher[i].Two_Star * 2+FilteredTeacher[i].One_Star * 1;
                count = FilteredTeacher[i].Five_Star + FilteredTeacher[i].Four_Star + FilteredTeacher[i].Three_Star + FilteredTeacher[i].Two_Star + FilteredTeacher[i].One_Star ;
                if(count == 0)
                {
                    FilteredTeacher[i].Avarage = 0;
                }
                else
                {
                    FilteredTeacher[i].Avarage = total / count;
                }
                

                if (FilteredTeacher[i].Teacher_Rank == "Placement")
                {
                    FilteredTeacher[i].Color = "#B9B9B9";
                }
                else if (FilteredTeacher[i].Teacher_Rank == "Newbie")
                {
                    FilteredTeacher[i].Color = "#E85959";
                }
                else if (FilteredTeacher[i].Teacher_Rank == "Average")
                {
                    FilteredTeacher[i].Color = "#E6B133";
                }
                else if (FilteredTeacher[i].Teacher_Rank == "Good")
                {
                    FilteredTeacher[i].Color = "#43D727";
                }
                else if (FilteredTeacher[i].Teacher_Rank == "Veteran")
                {
                    FilteredTeacher[i].Color = "#43D727";
                }
                else if (FilteredTeacher[i].Teacher_Rank == "Master")
                {
                    FilteredTeacher[i].Color = "#B033E4";
                }
            }
        }

        private async void TeacherListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedTeacher = e.Item as Teacher;
            info.Teacher = selectedTeacher;
            await Application.Current.MainPage.Navigation.PushModalAsync(new TutionPage(info)).ConfigureAwait(true);
        }
    }
}