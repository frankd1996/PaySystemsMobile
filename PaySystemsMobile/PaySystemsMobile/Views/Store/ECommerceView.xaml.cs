﻿using PaySystemsMobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PaySystemsMobile.Views.Store
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ECommerceView : ContentPage
    {
        StreamingViewModel vm = new StreamingViewModel();
        public ECommerceView()
        {
            InitializeComponent();
            this.BindingContext = vm;
        }
    }
}