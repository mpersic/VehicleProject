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
            BindingContext = _viewModel = new VehicleMakeDetailViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}