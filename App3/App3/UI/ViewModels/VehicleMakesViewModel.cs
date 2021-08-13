﻿using App3.Models;
using App3.Services;
using App3.Views;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App3.ViewModels
{
    public class VehicleMakesViewModel : BaseViewModel
    {
        private VehicleMake _selectedItem;
        public ObservableCollection<string> FilterOptions { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<VehicleMake> ItemTapped { get; }
        public Command FilterVehicleMakersCommand { get; }
        public Command SortCommand { get; }

        public ObservableRangeCollection<VehicleMake> Items { get; set; }
        public ObservableRangeCollection<VehicleMake> AllItems { get; set; }

        string selectedFilter = "All";

        public VehicleMakesViewModel()
        {
            Title = "Manufacturers";
            Items = new ObservableRangeCollection<VehicleMake>();
            AllItems = new ObservableRangeCollection<VehicleMake>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<VehicleMake>(OnItemSelected);
            AddItemCommand = new Command(OnAddItem);
            FilterOptions = new ObservableCollection<string>
                {
                    "BMW",
                    "AUDI",
                    "All"
                };
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
        void FilterItems()
        {
            Items.ReplaceRange(AllItems.Where(a => a.Abrv == SelectedFilter || SelectedFilter == "All"));
        }


        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await BaseVehicleMakeDataStore.GetItemsAsync(true);
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

        public VehicleMake SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewVehicleMakePage));
        }

        async void OnItemSelected(VehicleMake item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(VehicleMakeDetailPage)}?{nameof(VehicleMakeDetailViewModel.ItemId)}={item.Id}");
        }
    }
}