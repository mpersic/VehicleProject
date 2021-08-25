﻿using App3.Models;
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
    public class VehicleMakeDetailViewModel : INotifyPropertyChanged
    {
        public Command LoadVehicleModelsCommand { get; }
        public Command AddVehicleModelCommand { get; }
        public Command<VehicleModel> VehicleModelTapped { get; }
        public Command DeleteVehicleMakeCommand { get; }
        public Command UpdateVehicleMakeCommand { get; }
        public Command SortVehicleModelCommand { get; }
        public IDataStore<VehicleMake> BaseVehicleMakeDataStore => DependencyService.Get<IDataStore<VehicleMake>>();
        public IDataStore<VehicleModel> BaseVehicleModelDataStore => DependencyService.Get<IDataStore<VehicleModel>>();
        public ObservableRangeCollection<VehicleModel> VehicleModels { get; }
        public ObservableRangeCollection<VehicleModel> AllItems { get; set; }
        public ObservableRangeCollection<string> FilterOptions { get; }

        private VehicleModel _selectedItem;
        private string selectedFilter = "All";
        private string orderState = "Ascending";
        private string itemId;
        private string vehicleMakeName;
        private string vehicleMakeAbrv;

        public VehicleMakeDetailViewModel()
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
                var items = await BaseVehicleModelDataStore.GetItemsAsync(true);
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
                        await BaseVehicleModelDataStore.DeleteItemAsync(item.Id);
                    }
                }
                await BaseVehicleMakeDataStore.DeleteItemAsync(Id);

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
                await BaseVehicleMakeDataStore.UpdateItemAsync(new VehicleMake { Id= Id, Name = VehicleMakeName, Abrv=VehicleMakeAbrv });
                foreach(var item in VehicleModels)
                {
                    if (item.MakeId == Id)
                    {
                        await BaseVehicleModelDataStore.UpdateItemAsync(new VehicleModel { Id = item.Id, Name = item.Name, Abrv = item.Abrv, MakeId = item.MakeId });
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
                var item = await BaseVehicleMakeDataStore.GetItemAsync(itemId);
                Id = item.Id;
                VehicleMakeName = item.Name;
                VehicleMakeAbrv = item.Abrv;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
