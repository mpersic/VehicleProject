using App3.Models;
using App3.Services;
using App3.UI.ViewModels;
using Autofac;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace App3.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    class NewVehicleModelViewModel: BaseViewModel
    {

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public VehicleModelService VehicleModelService { get; set; }
        public VehicleMakeService VehicleMakeService { get; set; }

        private IMapper mapper;
        private string name;
        private string itemId;

        public NewVehicleModelViewModel(VehicleMakeService vehicleMakeService, VehicleModelService vehicleModelService, Autofac.IContainer container)
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            Name = name;
            ItemId = itemId;

            mapper = container.Resolve<IMapper>();
            VehicleMakeService = vehicleMakeService;
            VehicleModelService = vehicleModelService;
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(name);
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
            }
        }


        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            var item = await VehicleMakeService.GetItemAsync(ItemId);
            VehicleModel newItem = mapper.Map<VehicleMake, VehicleModel>(item);
            newItem.Id = Guid.NewGuid().ToString();
            newItem.Name = Name;
            await VehicleModelService.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await VehicleMakeService.GetItemAsync(itemId);
                ItemId = item.Id;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

    }
}
