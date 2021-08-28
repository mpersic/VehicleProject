using App3.Models;
using App3.Services;
using App3.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace App3.ViewModels
{
    public class NewVehicleMakeViewModel : BaseViewModel
    {

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public VehicleMakeService VehicleMakeService { get; set; }

        private string name;
        private string abbreviation;

        public NewVehicleMakeViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            VehicleMakeService = new VehicleMakeService();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(name)
                && !String.IsNullOrWhiteSpace(abbreviation);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Abbreviation
        {
            get => abbreviation;
            set => SetProperty(ref abbreviation, value);
        }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            VehicleMake newItem = new VehicleMake()
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name,
                Abrv = Abbreviation
            };

            await VehicleMakeService.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
