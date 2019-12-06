using Syncfusion.XForms.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shikkhanobish
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentHistory : ContentPage
    {
        public StudentHistory()
        {
            InitializeComponent();
            BindingContext = new StudentHistoryViewModel();
          
        }
    }
}