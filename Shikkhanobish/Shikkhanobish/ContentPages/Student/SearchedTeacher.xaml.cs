using Newtonsoft.Json;
using Shikkhanobish.Model;
using Shikkhanobish.ViewModel;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

using Xamarin.Forms.Xaml;
using System.Threading.Tasks;

namespace Shikkhanobish.ContentPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchedTeacher : ContentPage
    {
        private TransferInfo info = new TransferInfo();
        private List<TeacherID> TeacherIDListBySearch = new List<TeacherID>();
        private List<Teacher> TeacherList = new List<Teacher>();
        private List<Teacher> FilteredTeacher = new List<Teacher>();

        public SearchedTeacher(TransferInfo transInfo)
        {
            InitializeComponent();
            info = transInfo;
            getTeacherID ();
            getTeacher ();


        }

        public async Task getTeacherID()
        {
            string url = "https://api.shikkhanobish.com/api/Master/TeacherIDListFromSubject";
            HttpClient client = new HttpClient();
            string jsonData = JsonConvert.SerializeObject(new { Subject = info.Subject });
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait(true);
            string result = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            TeacherIDListBySearch = JsonConvert.DeserializeObject<List<TeacherID>>(result);
        }

        public async Task getTeacher()
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
            for (int i = 0; i < TeacherList.Count; i++)
            {
                for (int j = 0; j < TeacherIDListBySearch.Count; j++)
                {
                    if ((TeacherList[i].TeacherID == TeacherIDListBySearch[j].teacherID) && TeacherList[i].IsActive == 1)
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
            Calculate cal = new Calculate();
            double total, count;
            for (int i = 0; i < FilteredTeacher.Count; i++)
            {

                total = FilteredTeacher[i].Five_Star * 5 + FilteredTeacher[i].Four_Star * 4 + FilteredTeacher[i].Three_Star * 3 + FilteredTeacher[i].Two_Star * 2 + FilteredTeacher[i].One_Star * 1;
                count = FilteredTeacher[i].Five_Star + FilteredTeacher[i].Four_Star + FilteredTeacher[i].Three_Star + FilteredTeacher[i].Two_Star + FilteredTeacher[i].One_Star;
                if ( FilteredTeacher [ i ].IsOnTuition == 1 )
                {
                    FilteredTeacher [ i ].TeacherStatus = "Ofline";
                    FilteredTeacher [ i ].TeacherStatusColor = "#939393";
                }
                else
                {
                    FilteredTeacher [ i ].TeacherStatus = "Call Now ";
                    FilteredTeacher [ i ].TeacherStatusColor = "#43CF56";
                }

                if(FilteredTeacher[i].isFounder == 1)
                {
                    FilteredTeacher [ i ].FoundingTeacherColor = "#282E58";
                }
                else
                {
                    FilteredTeacher [ i ].FoundingTeacherColor = "#F5F5F5";
                }


                if (count == 0)
                {
                    FilteredTeacher[i].Avarage = 0;
                }
                else
                {
                    FilteredTeacher[i].Avarage = System.Math.Round(total / count, 2);
                }

                if (FilteredTeacher[i].Teacher_Rank == "Placement")
                {
                    FilteredTeacher[i].Color = "#B9B9B9";
                }
                else if (FilteredTeacher[i].Teacher_Rank == "Newbie")
                {
                    FilteredTeacher[i].Color = "#F68181";
                }
                else if (FilteredTeacher[i].Teacher_Rank == "Average")
                {
                    FilteredTeacher[i].Color = "#F5C24B";
                }
                else if (FilteredTeacher[i].Teacher_Rank == "Good")
                {
                    FilteredTeacher[i].Color = "#8AF077";
                }
                else if (FilteredTeacher[i].Teacher_Rank == "Veteran")
                {
                    FilteredTeacher[i].Color = "#77CDF0";
                }
                else if (FilteredTeacher[i].Teacher_Rank == "Master")
                {
                    FilteredTeacher[i].Color = "#CA6AF1";
                }
                FilteredTeacher[i].Amount = cal.RatingAndCostRange ( FilteredTeacher [ i ].Teacher_Rank , info.ClassCode, info.Teacher.Total_Min, 30,true );
            }
        }

        private async void TeacherListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedTeacher = e.Item as Teacher;
            info.Teacher = selectedTeacher;
            beSure();
        }

        public async void beSure()
        {
            if(info.Teacher.IsOnTuition == 0)
            {
                await Navigation.PushPopupAsync ( new PopUpForSelectedTeacher ( info ) ).ConfigureAwait ( false );
            }
                      
        }

        
    }
}