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
        public Task<IEnumerable<VehicleMake>> GetItemsAsync(bool forceRefresh = false)
        {
            return dataStore.GetItemsAsync(forceRefresh);
        }
        public Task<VehicleMake> GetItemAsync(string id)
        {
            return dataStore.GetItemAsync(id);
        }
        public Task<bool> DeleteItemAsync(string id)
        {
            return dataStore.DeleteItemAsync(id);
        }
        public Task<bool> UpdateItemAsync(VehicleMake item)
        {
            return dataStore.UpdateItemAsync(item);
        }
        public Task<bool> AddItemAsync(VehicleMake item)
        {
            return dataStore.AddItemAsync(item);
        }
    }
}
