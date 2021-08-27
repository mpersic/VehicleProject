using App3.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App3.Services
{
    public class VehicleModelService
    {
        private IDataStore<VehicleModel> dataStore;
        public VehicleModelService()
        {
        }
        public VehicleModelService(IDataStore<VehicleModel> dataStore)
        {
            this.dataStore = dataStore;
        }
        public void ChangeName(VehicleModel vehicleModel, string newName)
        {
            vehicleModel.Name = newName;
        }

        public async Task<IEnumerable<VehicleModel>> GetItemsAsync(bool forceRefresh = false)
        {
            return await await Task.FromResult(dataStore.GetItemsAsync(forceRefresh));
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            return await await Task.FromResult(dataStore.DeleteItemAsync(id));
        }
        public async Task<bool> UpdateItemAsync(VehicleModel item)
        {
            return await await Task.FromResult(dataStore.UpdateItemAsync(item));
        }
        public async Task<VehicleModel> GetItemAsync(string id)
        {
            return await await Task.FromResult(dataStore.GetItemAsync(id));
        }
        public async Task<bool> AddItemAsync(VehicleModel item)
        {
            return await await Task.FromResult(dataStore.AddItemAsync(item));
        }
    }
}
