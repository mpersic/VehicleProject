using App3.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace App3
{
    class NewVehicleModelProfile: Profile
    {
        public NewVehicleModelProfile()
        {
            CreateMap<VehicleMake, VehicleModel>()
            .ForMember(dest => dest.MakeId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Abrv, opt => opt.MapFrom(src => src.Abrv));
        }
    }
}
