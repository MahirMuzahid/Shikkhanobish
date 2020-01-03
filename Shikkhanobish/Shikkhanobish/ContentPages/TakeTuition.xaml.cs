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
    public partial class TakeTuition : ContentPage
    {
        List<int> a = new List<int>();
        public TakeTuition()
        {
            InitializeComponent();
            BindingContext = new TaketuitionViewModel();
            a.Add(1);
            a.Add(2);
            a.Add(3);
            ClassPicker.BindingContext = a;
        }

        
    }
}