using App3.Models;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App3.Services
{
    public class MockVehicleMakeDataStore : IDataStore<VehicleMake>
    {
        public readonly List<VehicleMake> items;

        public MockVehicleMakeDataStore()
        {

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<VehicleMakeProgramModule>();
            var container = containerBuilder.Build();

            var notificationService = container.Resolve<IInfo>();
            var vehicleMakeService = container.Resolve<VehicleMakeService>();

            items = new List<VehicleMake>()
            {
                new VehicleMake { Id = "1", Name = "Bayerische Motoren Werke", Abrv="BMW" },
                new VehicleMake { Id = "2", Name = "Auto Union Deutschland Ingolstadt", Abrv="AUDI" },
                new VehicleMake { Id = "3", Name = "PORSCHE", Abrv="PORSCHE" },
                new VehicleMake { Id = "4", Name = "FJORD", Abrv="FORD" },
            };
            vehicleMakeService.ChangeName(items.ElementAt(3), "FORD");
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

        public async Task<bool> UpdateItemAbbrAsync(string abbreviation, string id)
        {
            var item = items.Where((VehicleMake arg) => arg.Id == id).FirstOrDefault();
            item.Abrv = abbreviation;
            return await await Task.FromResult(UpdateItemAsync(item));
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

        public List<VehicleMake> GetItems()
        {
            return items;
        }

        public VehicleMake GetItem(string id)
        {
            return items.Where((VehicleMake arg) => arg.Id == id).FirstOrDefault();
        }

    }
}