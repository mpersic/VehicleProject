using App3.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App3.Services
{
    public class VehicleModelService
    {
        private IDataStore<VehicleModel> dataStore => DependencyService.Get<IDataStore<VehicleModel>>();
       
        public void ChangeName(VehicleModel vehicleModel, string newName)
        {
            vehicleModel.Name = newName;
        }

        public Task<IEnumerable<VehicleModel>> GetItemsAsync(bool forceRefresh = false)
        {
            return dataStore.GetItemsAsync(forceRefresh);
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            return dataStore.DeleteItemAsync(id);
        }
        public Task<bool> UpdateItemAsync(VehicleModel item)
        {
            return dataStore.UpdateItemAsync(item);
        }
        public Task<VehicleModel> GetItemAsync(string id)
        {
            return dataStore.GetItemAsync(id);
        }
        public Task<bool> AddItemAsync(VehicleModel item)
        {
            return dataStore.AddItemAsync(item);
        }
    }
}
