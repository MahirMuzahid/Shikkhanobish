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
    public partial class ParentsProfile : ContentPage
    {
        public ParentsProfile ( )
        {
            InitializeComponent ();
            List<int> cvList = new List<int> ();
            cvList.Add ( 1 );
            cvList.Add ( 2 );
            cvList.Add ( 3 );
            cvList.Add ( 4 );
            cvList.Add ( 5 );
            cv.ItemsSource = cvList;
        }
    }
}