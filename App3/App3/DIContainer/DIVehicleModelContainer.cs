using App3.Services;
using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace App3.DIContainer
{
    public class DIVehicleModelContainer
    {
        private static IContainer instance;
        public static IContainer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Configure();
                }
                return instance;
            }
            set => Configure();
        }

        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<VehicleModelProgramModule>();
            return builder.Build();
        }
    }
}
