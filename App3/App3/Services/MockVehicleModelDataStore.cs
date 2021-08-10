using App3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace App3.Services
{
    public class MockVehicleModelDataStore : IDataStore<VehicleModel>
    {
        readonly List<VehicleModel> items;
        public MockVehicleModelDataStore()
        {
            var configuration = new MapperConfiguration(cfg =>
            cfg.CreateMap<VehicleMake, VehicleModel>()
            .ForMember(dest => dest.MakeId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Abrv, opt => opt.MapFrom(src => src.Abrv))
            );
            var mapper = configuration.CreateMapper();
            MockVehicleMakeDataStore mockVehicleMakeDataStore = new MockVehicleMakeDataStore();
            List<VehicleMake> vehicleMakes = mockVehicleMakeDataStore.GetItems();
            VehicleModel x2 = mapper.Map<VehicleMake, VehicleModel>(vehicleMakes.ElementAt(0));
            VehicleModel x1 = mapper.Map<VehicleMake, VehicleModel>(vehicleMakes.ElementAt(0));
            VehicleModel x5 = mapper.Map<VehicleMake, VehicleModel>(vehicleMakes.ElementAt(0));
            x2.Name = "X2";
            x1.Name = "X1";
            x5.Name = "X5";
            VehicleModel a5 = mapper.Map<VehicleMake, VehicleModel>(vehicleMakes.ElementAt(1));
            VehicleModel a8 = mapper.Map<VehicleMake, VehicleModel>(vehicleMakes.ElementAt(1));
            VehicleModel a1 = mapper.Map<VehicleMake, VehicleModel>(vehicleMakes.ElementAt(1));
            VehicleModel a3 = mapper.Map<VehicleMake, VehicleModel>(vehicleMakes.ElementAt(1));
            a5.Name = "A5";
            a8.Name = "A8";
            a3.Name = "A1";
            a1.Name = "A3";
            VehicleModel fiesta = mapper.Map<VehicleMake, VehicleModel>(vehicleMakes.ElementAt(3));
            fiesta.Name = "Fiesta";
            items = new List<VehicleModel>()
            {
                x2,
                x5,
                x1,
                a5,
                a8,
                a3,
                a1,
                fiesta
            };
            foreach(var item in items)
            {
                item.Id = Guid.NewGuid().ToString();
            }
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

        public async Task<bool> UpdateItemsAsync(string abbreviation, string ID)
        {
            foreach(var vehicle in items)
            {
                if (ID == vehicle.MakeId)
                    vehicle.Abrv = abbreviation;
            }
            MockVehicleMakeDataStore mockVehicleMakeDataStore = new MockVehicleMakeDataStore();
            await  mockVehicleMakeDataStore.UpdateItemAbbrAsync(abbreviation,ID);
            return await Task.FromResult(true);
        }
    }
}
