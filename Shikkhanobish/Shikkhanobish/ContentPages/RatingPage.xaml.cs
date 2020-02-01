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
    }
}