using App3.Models;
using App3.ViewModels;
using App3.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.Views
{
    public partial class VehicleMakesPage : ContentPage
    {
        VehicleMakesViewModel _viewModel;

        public VehicleMakesPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new VehicleMakesViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}