using App3.Models;
using App3.Services;
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
    class NewVehicleModelViewModel: INotifyPropertyChanged
    {

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public IDataStore<VehicleMake> BaseVehicleMakeDataStore => DependencyService.Get<IDataStore<VehicleMake>>();
        public IDataStore<VehicleModel> BaseVehicleModelDataStore => DependencyService.Get<IDataStore<VehicleModel>>();
        public VehicleModelService VehicleModelService { get; set; }
        public VehicleMakeService VehicleMakeService { get; set; }

        private IMapper mapper;
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

            VehicleMakeService = new VehicleMakeService(BaseVehicleMakeDataStore);
            VehicleModelService = new VehicleModelService(BaseVehicleModelDataStore);

            var configuration = new MapperConfiguration(cfg =>
            cfg.AddProfile<NewVehicleModelProfile>());
            mapper = configuration.CreateMapper();
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
