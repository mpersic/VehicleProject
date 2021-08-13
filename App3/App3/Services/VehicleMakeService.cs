using App3.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace App3.Services
{
    class VehicleMakeService
    {
        public VehicleMakeService()
        {
        }
        public void ChangeName(VehicleMake vehicleMake, string newName)
        {
            vehicleMake.Name = newName;
        }
    }
}
