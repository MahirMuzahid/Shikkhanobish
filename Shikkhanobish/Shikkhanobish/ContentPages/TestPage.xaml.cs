using Shikkhanobish.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Shikkhanobish.Model.Infoforteacherwindow;

namespace Shikkhanobish.ContentPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestPage : ContentPage
    {
        public TestPage()
        {
            InitializeComponent();
        }

       
        protected override void OnAppearing()
        {
            base.OnAppearing();

            Info.ItemsSource = new getlist().infolist();
        }
    }
}