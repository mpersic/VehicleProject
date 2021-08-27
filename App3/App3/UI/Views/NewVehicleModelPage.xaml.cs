using App3.Models;
using App3.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewVehicleModelPage : ContentPage
    {
        public VehicleModel Item { get; set; }
        NewVehicleModelViewModel _viewModel;
        public NewVehicleModelPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new NewVehicleModelViewModel();
        }
    }
}