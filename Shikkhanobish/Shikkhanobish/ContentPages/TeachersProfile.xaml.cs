using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shikkhanobish.ContentPages
{
    [XamlCompilation ( XamlCompilationOptions.Compile )]
    public partial class TeachersProfile : ContentPage
    {
        Teacher teacher = new Teacher ( );
        public TeachersProfile (Teacher t )
        {
            InitializeComponent ();
            //BindingContext = new ProfileViewModel ( t );
            teacher = t;
            //activeback.BackgroundColor = Color.FromHex ( "#9B69F7" );
            //var image = new Image { Source = "BackColor.jpg" };
        }
    }
}