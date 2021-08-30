using App3.Services;
using Autofac;
using Autofac.Features.ResolveAnything;
using AutoMapper;

namespace App3.DIContainer
{
    public class DIContainer
    {
        private static IContainer instance;
        public static IContainer Instance {
            get {
                    if (instance == null)
                    {
                        instance = Configure();
                    }
                        return instance;
                } 
        }

        private static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<VehicleMakeProgramModule>();
            builder.RegisterModule<VehicleModelProgramModule>();
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());

            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<NewVehicleModelProfile>();
            }
            )).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();
            return builder.Build();
        }
    }
}
