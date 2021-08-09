using App3.ViewModels;
using App3.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace App3
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(VehicleMakesPage), typeof(VehicleMakesPage));
            Routing.RegisterRoute(nameof(VehicleMakeDetailPage), typeof(VehicleMakeDetailPage));
            Routing.RegisterRoute(nameof(NewVehicleMakePage), typeof(NewVehicleMakePage));
            Routing.RegisterRoute(nameof(NewVehicleModelPage), typeof(NewVehicleModelPage));
            Routing.RegisterRoute(nameof(VehicleModelDetailPage), typeof(VehicleModelDetailPage));
        }

    }
}
