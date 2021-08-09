using App3.Models;
using App3.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace App3.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class VehicleMakeDetailViewModel : BaseViewModel
    {

        private VehicleModel _selectedItem;

        public ObservableCollection<VehicleModel> VehicleModels { get; }
        public Command LoadVehicleModelsCommand { get; }
        public Command AddVehicleModelCommand { get; }

        public Command<VehicleModel> VehicleModelTapped { get; }

        public Command DeleteVehicleMakeCommand { get; }

        public Command UpdateVehicleMakeCommand { get; }

        public VehicleMakeDetailViewModel()
        {
            Title = "Manufacturer";
            VehicleModels = new ObservableCollection<VehicleModel>();
            LoadVehicleModelsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            VehicleModelTapped = new Command<VehicleModel>(OnVehicleModelSelected);

            AddVehicleModelCommand = new Command(OnAddItem);

            UpdateVehicleMakeCommand = new Command(UpdateItem);
            DeleteVehicleMakeCommand = new Command(DeleteItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                VehicleModels.Clear();
                var items = await BaseVehicleModelDataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    if (item.MakeId == itemId)
                        VehicleModels.Add(item);
                }
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

        private string itemId;
        private string vehicleMakeName;
        private string vehicleMakeAbrv;
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
    }
}
