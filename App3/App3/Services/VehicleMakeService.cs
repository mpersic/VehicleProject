using App3.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App3.Services
{
    public class VehicleMakeService
    {
        private IDataStore<VehicleMake> dataStore;
        public VehicleMakeService()
        {
            //this.dataStore = dataStore;
        }

        public VehicleMakeService(IDataStore<VehicleMake> dataStore)
        {
            this.dataStore = dataStore;
        }

        public void ChangeName(VehicleMake vehicleMake, string newName)
        {
            vehicleMake.Name = newName;
        }
        public async Task<IEnumerable<VehicleMake>> GetItemsAsync(bool forceRefresh = false)
        {
            return await await Task.FromResult(dataStore.GetItemsAsync(forceRefresh));
        }
        public async Task<VehicleMake> GetItemAsync(string id)
        {
            return await await Task.FromResult(dataStore.GetItemAsync(id));
        }
        public async Task<bool> DeleteItemAsync(string id)
        {
            return await await Task.FromResult(dataStore.DeleteItemAsync(id));
        }
        public async Task<bool> UpdateItemAsync(VehicleMake item)
        {
            return await await Task.FromResult(dataStore.UpdateItemAsync(item));
        }
        public async Task<bool> AddItemAsync(VehicleMake item)
        {
            return await await Task.FromResult(dataStore.AddItemAsync(item));
        }
    }
}
