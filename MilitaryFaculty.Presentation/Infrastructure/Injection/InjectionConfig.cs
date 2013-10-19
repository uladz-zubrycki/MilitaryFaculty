using System.Configuration;
using Autofac;
using MilitaryFaculty.Data;

namespace MilitaryFaculty.Presentation.Infrastructure
{
    public static class InjectionConfig
    {
        public static IContainer Init(ContainerBuilder builder)
        {
            RegisterRepositories(builder);
            RegisterEntityContext(builder);

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
    }
}
