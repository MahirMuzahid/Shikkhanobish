using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shikkhanobish
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Balance : ContentPage
    {
        public Balance()
        {
            InitializeComponent();
            BindingContext = new BalanceViewModel();
        }
    }
}