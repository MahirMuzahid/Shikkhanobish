﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shikkhanobish
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterStudent : ContentPage
    {
        public RegisterStudent()
        {
            InitializeComponent();
            var image = new Image { Source = "onlylogo.png" };
            RegisterStudentViewModel register = new RegisterStudentViewModel(Navigation);
            BindingContext = new RegisterStudentViewModel(Navigation);  
        }

    }
}