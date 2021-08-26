using App3.Models;
using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace App3.Services
{
    class VehicleMakeProgramModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<VehicleMakeService>().AsSelf();
        }
    }
}
