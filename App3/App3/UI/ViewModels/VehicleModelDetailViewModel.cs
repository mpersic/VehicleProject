using App3.Models;
using App3.Services;
using App3.UI.ViewModels;
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
    public class VehicleModelDetailViewModel : BaseViewModel
    {
        public Command DeleteVehicleModelCommand { get; }
        public Command UpdateVehicleModelCommand { get; }
        public VehicleModelService VehicleModelService { get; set; }
        public VehicleModelDetailViewModel(VehicleModelService vehicleModelService)
        {
            Title = "Model";
            UpdateVehicleModelCommand = new Command(UpdateItem);
            DeleteVehicleModelCommand = new Command(DeleteItem);
            VehicleModelService = vehicleModelService;
        }

        private async void DeleteItem(object obj)
        {
            try
            {
                await VehicleModelService.DeleteItemAsync(itemId);
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async void UpdateItem(object obj)
        {
            try
            {
                await VehicleModelService.UpdateItemAsync(new VehicleModel { Id = itemId, Name = vehicleModelName, Abrv = vehicleModelAbrv, MakeId = makeId });
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private string itemId;
        private string vehicleModelName;
        private string vehicleModelAbrv;
        private string makeId;

        public string VehicleModelName
        {
            get => vehicleModelName;
            set => SetProperty(ref vehicleModelName, value);
        }

        public string VehicleModelAbrv
        {
            get => vehicleModelAbrv;
            set => SetProperty(ref vehicleModelAbrv, value);
        }

        public string MakeId
        {
            get {
                return makeId;
            }
            set
            {
                makeId = value;
            }
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
                LoadItem(value);
            }
        }
        public async void LoadItem(string itemId)
        {
            try
            {
                var item = await VehicleModelService.GetItemAsync(itemId);
                MakeId = item.MakeId;
                VehicleModelName = item.Name;
                VehicleModelAbrv = item.Abrv;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
       
    }
}