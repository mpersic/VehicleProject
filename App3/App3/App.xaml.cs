using App3.Services;
using App3.Views;
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
            DependencyService.Register<MockVehicleModelDataStore>();
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
