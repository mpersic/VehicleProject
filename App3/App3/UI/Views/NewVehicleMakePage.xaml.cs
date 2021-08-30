using App3.Models;
using App3.Services;
using App3.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewVehicleMakePage : ContentPage
    {
        public VehicleMake Item { get; set; }
        NewVehicleMakeViewModel _viewModel;
        public NewVehicleMakePage()
        {
            InitializeComponent();
            VehicleMakeService vehicleMakeService = new VehicleMakeService();
            BindingContext = _viewModel = new NewVehicleMakeViewModel(vehicleMakeService);
        }
    }
}