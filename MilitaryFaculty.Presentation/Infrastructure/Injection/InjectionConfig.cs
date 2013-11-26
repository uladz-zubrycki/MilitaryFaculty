using System.Configuration;
using Autofac;
using MilitaryFaculty.Data;
using MilitaryFaculty.Logic;
using MilitaryFaculty.Logic.DataProviders;

namespace MilitaryFaculty.Presentation.Infrastructure
{
    public static class InjectionConfig
    {
        public static IContainer Register(ContainerBuilder builder)
        {
            RegisterRepositories(builder);
            RegisterEntityContext(builder);
            RegisterDataProvider(builder);

            return builder.Build();
        }

        private static void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(EntityContext).Assembly)
                   .Where(t => t.Name.EndsWith("Repository") && !t.Name.Contains("Base"))
                   .AsImplementedInterfaces()
                   .SingleInstance();
        }

        private static void RegisterEntityContext(ContainerBuilder builder)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Current"].ConnectionString;

            builder.RegisterType<EntityContext>()
                   .AsSelf()
                   .WithParameter("connectionString", connectionString)
                   .SingleInstance();
        }

        private static void RegisterDataProvider(ContainerBuilder builder)
        {
            var dataModule = new DataModule();
            dataModule.RegisterProviders(new IDataProvider[]
                {
                    new CustomDataProvider(),
                    new ProfessorsDataProvider(),
                    new PublicationsDataProvider(), 
                });

            builder.RegisterInstance(dataModule);
        }
    }
}
