using App3.DIContainer;
using App3.Models;
using App3.Services;
using App3.Views;
using Autofac;
using AutoMapper;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockVehicleMakeDataStore>();

            MockVehicleMakeDataStore mockVehicleMakeDataStore = new MockVehicleMakeDataStore();

            var container = DIContainer.DIContainer.Instance;
            MockVehicleModelDataStore mockVehicleModelDataStore = new MockVehicleModelDataStore(mockVehicleMakeDataStore, container);
            DependencyService.RegisterSingleton<IDataStore<VehicleModel>>(mockVehicleModelDataStore);
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
