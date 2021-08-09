using App3.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace App3.ViewModels
{
    public class NewVehicleMakeViewModel : BaseVehicleMakeViewModel
    {
        private string name;
        private string abbreviation;

        public NewVehicleMakeViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
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

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

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

            await VehicleMakeDataStore.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
