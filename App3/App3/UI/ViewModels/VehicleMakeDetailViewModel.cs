using App3.Models;
using App3.Services;
using App3.Views;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace App3.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class VehicleMakeDetailViewModel : BaseViewModel
    {
        public Command LoadVehicleModelsCommand { get; }
        public Command AddVehicleModelCommand { get; }
        public Command<VehicleModel> VehicleModelTapped { get; }
        public Command DeleteVehicleMakeCommand { get; }
        public Command UpdateVehicleMakeCommand { get; }
        public Command SortVehicleModelCommand { get; }
        public ObservableRangeCollection<VehicleModel> VehicleModels { get; }
        public ObservableRangeCollection<VehicleModel> AllItems { get; set; }
        public ObservableRangeCollection<string> FilterOptions { get; }
        public VehicleMakeService VehicleMakeService { get; set; }
        public VehicleModelService VehicleModelService { get; set; }

        private VehicleModel _selectedItem;
        private string selectedFilter = "All";
        private string orderState = "Ascending";
        private string itemId;
        private string vehicleMakeName;
        private string vehicleMakeAbrv;

        public VehicleMakeDetailViewModel(VehicleMakeService vehicleMakeService, VehicleModelService vehicleModelService)
        {
            Title = "Manufacturer";
            VehicleModels = new ObservableRangeCollection<VehicleModel>();
            AllItems = new ObservableRangeCollection<VehicleModel>();
            LoadVehicleModelsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            VehicleModelTapped = new Command<VehicleModel>(OnVehicleModelSelected);
            AddVehicleModelCommand = new Command(OnAddItem);
            UpdateVehicleMakeCommand = new Command(UpdateItem);
            DeleteVehicleMakeCommand = new Command(DeleteItem);
            SortVehicleModelCommand = new Command(SortItems);
            FilterOptions = new ObservableRangeCollection<string>
            {
                "All",
                "A1",
                "A7",
                "X5"
            };
            VehicleMakeService = vehicleMakeService;
            VehicleModelService = vehicleModelService;
        }

        private void SortItems()
        {
            if (orderState == "Ascending")
            {
                VehicleModels.ReplaceRange(AllItems.Where(a => a.MakeId == itemId && (a.Abrv == SelectedFilter || SelectedFilter == "All")).OrderBy(x => x.Name));
                orderState = "Descending";
            }
            else
            {
                VehicleModels.ReplaceRange(AllItems.Where(a => a.MakeId == itemId && (a.Abrv == SelectedFilter || SelectedFilter == "All")).OrderByDescending(x => x.Name));
                orderState = "Ascending";
            }
        }

        void FilterItems()
        {
            VehicleModels.ReplaceRange(AllItems.Where( a => a.MakeId == itemId && (a.Name == SelectedFilter || SelectedFilter == "All")));
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                VehicleModels.Clear();
                var items = await VehicleModelService.GetItemsAsync(true);
                AllItems.ReplaceRange(items);
                FilterItems();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }
        public VehicleModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnVehicleModelSelected(value);
            }
        }

        async void OnVehicleModelSelected(VehicleModel item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(VehicleModelDetailPage)}?{nameof(VehicleModelDetailViewModel.ItemId)}={item.Id}");
        }

        private async void DeleteItem(object obj)
        {
            try
            {
                foreach(var item in VehicleModels)
                {
                    if (item.MakeId == Id)
                    {
                        await VehicleModelService.DeleteItemAsync(item.Id);
                    }
                }
                await VehicleMakeService.DeleteItemAsync(Id);

                await Shell.Current.GoToAsync("../..");
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
                await VehicleMakeService.UpdateItemAsync(new VehicleMake { Id= Id, Name = VehicleMakeName, Abrv=VehicleMakeAbrv });
                foreach(var item in VehicleModels)
                {
                    if (item.MakeId == Id)
                    {
                        await VehicleModelService.UpdateItemAsync(new VehicleModel { Id = item.Id, Name = item.Name, Abrv = item.Abrv, MakeId = item.MakeId });
                    }
                }
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(NewVehicleModelPage)}?{nameof(NewVehicleModelViewModel.ItemId)}={ItemId}");
        }
        public string SelectedFilter
        {
            get => selectedFilter;
            set
            {
                if (SetProperty(ref selectedFilter, value))
                    FilterItems();
            }
        }
        public string Id { get; set; }

        public string VehicleMakeName
        {
            get => vehicleMakeName;
            set => SetProperty(ref vehicleMakeName, value);
        }

        public string VehicleMakeAbrv
        {
            get => vehicleMakeAbrv;
            set => SetProperty(ref vehicleMakeAbrv, value);
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
                LoadItemId(value);
            }
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await VehicleMakeService.GetItemAsync(itemId);
                Id = item.Id;
                VehicleMakeName = item.Name;
                VehicleMakeAbrv = item.Abrv;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
       
    }
}
