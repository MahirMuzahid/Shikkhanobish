using Shikkhanobish.Model;
using Shikkhanobish.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shikkhanobish.ContentPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RatingPage : ContentPage
    {
        TransferInfo info = new TransferInfo();
        public RatingPage(TransferInfo trnsInfo)
        {
            InitializeComponent();
            info = trnsInfo;
        }
        public void showEverything()
        {
            Calculate calculate = new Calculate();
            tnamelbl.Text = info.Teacher.TeacherName;
            tIDlbl.Text = "" + info.Teacher.TeacherID;
            sClasslbl.Text = info.Class;
            sSubject.Text = info.Subject;
            inapptimelbl.Text = "" +info.StudyTimeInAPp;
            costlbl.Text = "" + calculate.CalculateCost(info);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
        }
        private void ostarClicked(object sender, EventArgs e)
        {
            RatingColorBox.CornerRadius = 1;
            RatingColorBox.Color = Color.FromHex("#FF5A5A");
            info.GivenRating = 1;
        }
        private void tstarClicked(object sender, EventArgs e)
        {
            RatingColorBox.CornerRadius = 2;
            RatingColorBox.Color = Color.FromHex("#F0BE05");
            info.GivenRating = 2;
        }
        private void thstarClicked(object sender, EventArgs e)
        {
            RatingColorBox.CornerRadius = 3;
            RatingColorBox.Color = Color.FromHex("#3BCF64");
            info.GivenRating = 3;
        }
        private void fstarClicked(object sender, EventArgs e)
        {
            RatingColorBox.CornerRadius = 4;
            RatingColorBox.Color = Color.FromHex("#50B2ED");
            info.GivenRating = 4;
        }
        private void fistarClicked(object sender, EventArgs e)
        {
            RatingColorBox.CornerRadius = 5;
            RatingColorBox.Color = Color.FromHex("#B161F3");
            info.GivenRating = 5;
        }
        
    }
}