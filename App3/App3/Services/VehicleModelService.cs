using App3.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace App3.Services
{
    class VehicleModelService
    {
        public VehicleModelService()
        {
        }
        public void ChangeName(VehicleModel vehicleModel, string newName)
        {
            vehicleModel.Name = newName;
        }
    }
}
