using App3.Services;
using Autofac;
using Autofac.Features.ResolveAnything;
using System;
using System.Collections.Generic;
using System.Text;

namespace App3.DIContainer
{
    public class DIContainer
    {
        private static IContainer instance;
        public static IContainer Instance {
            get {
                    if (instance == null)
                    {
                        instance = Resolve();
                    }
                        return instance;
                } 
        } 

        public static IContainer Resolve()
        {
                var builder = new ContainerBuilder();
                builder.RegisterModule<VehicleMakeProgramModule>();
                builder.RegisterModule<VehicleModelProgramModule>();
                builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
                return builder.Build();
        }
    }
}
