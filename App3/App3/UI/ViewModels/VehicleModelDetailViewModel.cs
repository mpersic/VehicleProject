using App3.Models;
using App3.Services;
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
    public class VehicleModelDetailViewModel : INotifyPropertyChanged
    {
        public Command DeleteVehicleModelCommand { get; }
        public Command UpdateVehicleModelCommand { get; }
        public IDataStore<VehicleModel> BaseVehicleModelDataStore => DependencyService.Get<IDataStore<VehicleModel>>();

        public VehicleModelDetailViewModel()
        {
            Title = "Model";
            UpdateVehicleModelCommand = new Command(UpdateItem);
            DeleteVehicleModelCommand = new Command(DeleteItem);
        }

        private async void DeleteItem(object obj)
        {
            try
            {
                await BaseVehicleModelDataStore.DeleteItemAsync(itemId);
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
                MockVehicleModelDataStore mockVehicleModelDataStore = new MockVehicleModelDataStore();
                await BaseVehicleModelDataStore.UpdateItemAsync(new VehicleModel { Id = itemId, Name = vehicleModelName, Abrv = vehicleModelAbrv, MakeId = makeId });
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
                var item = await BaseVehicleModelDataStore.GetItemAsync(itemId);
                MakeId = item.MakeId;
                VehicleModelName = item.Name;
                VehicleModelAbrv = item.Abrv;
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