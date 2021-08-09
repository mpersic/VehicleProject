using App3.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace App3.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    class NewVehicleModelViewModel: BaseViewModel
    {
        private string text;
        private string description;
        private string itemId;

        public NewVehicleModelViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            Text = ItemId;
            Description = ItemId;
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(text)
                && !String.IsNullOrWhiteSpace(description);
        }
        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                //LoadItemId(value);
            }
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await BaseVehicleMakeDataStore.GetItemAsync(itemId);
                ItemId = item.Id;
                //VehicleMakeName = item.Name;
                //VehicleMakeAbrv = item.Abrv;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
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
            VehicleModel newItem = new VehicleModel()
            {
                Id = Guid.NewGuid().ToString(),
                Name = Text,
                Abrv = Description,
                MakeId = ItemId
            };

            await BaseVehicleModelDataStore.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
