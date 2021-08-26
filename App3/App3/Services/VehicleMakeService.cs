using App3.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace App3.Services
{
    class VehicleMakeService
    {
        private IDataStore<VehicleMake> dataStore;
        //public VehicleMakeService(IDataStore<VehicleMake> dataStore)
        public VehicleMakeService()
        {
            //this.dataStore = dataStore;
        }
        public void ChangeName(VehicleMake vehicleMake, string newName)
        {
            vehicleMake.Name = newName;
        }
    }
}
