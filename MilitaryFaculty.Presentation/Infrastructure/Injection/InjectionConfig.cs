using System.Configuration;
using Autofac;
using MilitaryFaculty.Data;
using MilitaryFaculty.Logic;
using MilitaryFaculty.Logic.DataProviders;

namespace MilitaryFaculty.Presentation.Infrastructure
{
    public static class InjectionConfig
    {
        /// <summary>
        /// Registers all neccessary services and their implementations in container.
        /// </summary>
        /// <param name="builder">Builder for service container.</param>
        /// <returns>Configured and built container.</returns>
        public static IContainer Register(ContainerBuilder builder)
        {
            RegisterRepositories(builder);
            RegisterEntityContext(builder);
            RegisterDataProvider(builder);

            return builder.Build();
        }

        /// <summary>
        /// Registers repositories as singletones.
        /// </summary>
        /// <param name="builder">Builder for service container.</param>
        private static void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(EntityContext).Assembly)
                   .Where(t => t.Name.EndsWith("Repository") && !t.Name.Contains("Base"))
                   .AsImplementedInterfaces()
                   .SingleInstance();
        }

        /// <summary>
        /// Registers entity context, using connection string from config file.
        /// </summary>
        /// <param name="builder">Builder for service container.</param>
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
                    //new CathedrasDataProvider(new CathedraRepository(context)),
                    //new ProfessorsDataProvider(new ProfessorRepository(context)),
                    //new PublicationsDataProvider(new PublicationRepository(context)),
                    //new ExhibitionsDataProvider(new ExhibitionRepository(context)),
                    //new ConferencesDataProvider(new ConferenceRepository(context)), 
                });

            builder.RegisterInstance(dataModule);
        }
    }
}
