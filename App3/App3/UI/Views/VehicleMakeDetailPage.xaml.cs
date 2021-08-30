using App3.Services;
using App3.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace App3.Views
{
    public partial class VehicleMakeDetailPage : ContentPage
    {
        VehicleMakeDetailViewModel _viewModel;
        public VehicleMakeDetailPage()
        {
            InitializeComponent();
            VehicleMakeService vehicleMakeService = new VehicleMakeService();
            VehicleModelService vehicleModelService = new VehicleModelService();
            BindingContext = _viewModel = new VehicleMakeDetailViewModel(vehicleMakeService, vehicleModelService);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}