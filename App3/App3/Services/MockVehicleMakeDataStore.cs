using App3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App3.Services
{
    public class MockVehicleMakeDataStore : IDataStore<VehicleMake>
    {
        readonly List<VehicleMake> items;

        public MockVehicleMakeDataStore()
        {
            items = new List<VehicleMake>()
            {
                new VehicleMake { Id = "1", Name = "Bayerische Motoren Werke", Abrv="BMW" },
                new VehicleMake { Id = "2", Name = "Auto Union Deutschland Ingolstadt", Abrv="AUDI" },
                new VehicleMake { Id = "3", Name = "PORSCHE", Abrv="PORSCHE" },
                new VehicleMake { Id = "4", Name = "FORD", Abrv="FORD" },
            };
        }

        public async Task<bool> AddItemAsync(VehicleMake item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(VehicleMake item)
        {
            var oldItem = items.Where((VehicleMake arg) => arg.Id == item.Id).FirstOrDefault();
            int oldIndex = items.IndexOf(oldItem);
            items.Remove(oldItem);
            items.Insert(oldIndex, item);
          
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((VehicleMake arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<VehicleMake> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<VehicleMake>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

    }
}