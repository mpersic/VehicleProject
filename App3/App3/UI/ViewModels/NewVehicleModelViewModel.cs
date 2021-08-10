using App3.Models;
using App3.Services;
using AutoMapper;
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
        private string name;
        private string itemId;

        public NewVehicleModelViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            Name = name;
            ItemId = itemId;
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
                //LoadItemId(value);
            }
        }


        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await BaseVehicleMakeDataStore.GetItemAsync(itemId);
                ItemId = item.Id;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        private async void OnSave()
        {
            var configuration = new MapperConfiguration(cfg =>
            cfg.CreateMap<VehicleMake, VehicleModel>()
            .ForMember(dest => dest.MakeId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Abrv, opt => opt.MapFrom(src => src.Abrv))
            );
            var mapper = configuration.CreateMapper();
            var item = await BaseVehicleMakeDataStore.GetItemAsync(ItemId);
            VehicleModel newItem = mapper.Map<VehicleMake, VehicleModel>(item);
            newItem.Id = Guid.NewGuid().ToString();
            newItem.Name = Name;
            await BaseVehicleModelDataStore.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
