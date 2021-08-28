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

        public async Task<IEnumerable<VehicleModel>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(dataStore.GetItemsAsync(forceRefresh)).Unwrap();
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            return await Task.FromResult(dataStore.DeleteItemAsync(id)).Unwrap();
        }
        public async Task<bool> UpdateItemAsync(VehicleModel item)
        {
            return await Task.FromResult(dataStore.UpdateItemAsync(item)).Unwrap();
        }
        public async Task<VehicleModel> GetItemAsync(string id)
        {
            return await Task.FromResult(dataStore.GetItemAsync(id)).Unwrap();
        }
        public async Task<bool> AddItemAsync(VehicleModel item)
        {
            return await Task.FromResult(dataStore.AddItemAsync(item)).Unwrap();
        }
    }
}
