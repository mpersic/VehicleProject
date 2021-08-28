using App3.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App3.Services
{
    public class VehicleMakeService
    {
        private IDataStore<VehicleMake> dataStore  => DependencyService.Get<IDataStore<VehicleMake>>();

        public void ChangeName(VehicleMake vehicleMake, string newName)
        {
            vehicleMake.Name = newName;
        }
        public async Task<IEnumerable<VehicleMake>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(dataStore.GetItemsAsync(forceRefresh)).Unwrap();
        }
        public async Task<VehicleMake> GetItemAsync(string id)
        {
            return await Task.FromResult(dataStore.GetItemAsync(id)).Unwrap();
        }
        public async Task<bool> DeleteItemAsync(string id)
        {
            return await Task.FromResult(dataStore.DeleteItemAsync(id)).Unwrap();
        }
        public async Task<bool> UpdateItemAsync(VehicleMake item)
        {
            return await Task.FromResult(dataStore.UpdateItemAsync(item)).Unwrap();
        }
        public async Task<bool> AddItemAsync(VehicleMake item)
        {
            return await Task.FromResult(dataStore.AddItemAsync(item)).Unwrap();
        }
    }
}
