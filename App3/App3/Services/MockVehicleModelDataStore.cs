using App3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App3.Services
{
    public class MockVehicleModelDataStore : IDataStore<VehicleModel>
    {
        readonly List<VehicleModel> items;

        public MockVehicleModelDataStore()
        {
            items = new List<VehicleModel>()
            {
                new VehicleModel { Id="1235", Name= "X5", Abrv="X5", MakeId="1"},
                new VehicleModel { Id="123", Name="X1", Abrv="X1", MakeId="1"},
                new VehicleModel { Id= "12", Name = "A5", Abrv="A5", MakeId="2"},
                new VehicleModel { Id= "11", Name = "FIESTA", Abrv="FIESTA", MakeId="4"},
            };
        }
        public async Task<bool> AddItemAsync(VehicleModel item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((VehicleModel arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<VehicleModel> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<VehicleModel>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public async Task<bool> UpdateItemAsync(VehicleModel item)
        {
            var oldItem = items.Where((VehicleModel arg) => arg.Id == item.Id).FirstOrDefault();
            int oldIndex = items.IndexOf(oldItem);
            items.Remove(oldItem);
            items.Insert(oldIndex, item);

            return await Task.FromResult(true);
        }
    }
}
