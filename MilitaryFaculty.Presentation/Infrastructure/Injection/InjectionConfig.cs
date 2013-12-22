using System;
using System.Configuration;
using System.Linq;
using Autofac;
using MilitaryFaculty.Data;
using MilitaryFaculty.Reporting;
using MilitaryFaculty.Reporting.Data;
using MilitaryFaculty.Reporting.Excel;

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
            RegisterDataProviders(builder);
            RegisterFormulaProvider(builder);
            RegisterReportTableProvider(builder);
            RegisterExcelReportingService(builder);

            return builder.Build();
        }

        /// <summary>
        /// Registers repositories.
        /// </summary>
        /// <param name="builder">Builder for service container.</param>
        private static void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof (Repository<>))
                   .AsImplementedInterfaces();
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

        private static void RegisterFormulaProvider(ContainerBuilder builder)
        {
            const string relDirPath = @"..\..\..\..\MilitaryFaculty.Reporting\MilitaryFaculty.Reporting\formulas\";
            const string baseName = "formulas-";
            var dirPath = Environment.CurrentDirectory + relDirPath;

            var files = Enumerable.Range(1, 4)
                                  .Select(i => dirPath + baseName + i + ".xml")
                                  .ToList();

            builder.RegisterType<FormulaProvider>()
                   .As<IFormulaProvider>()
                   .WithParameter("files", files)
                   .SingleInstance();
        }

        private static void RegisterReportTableProvider(ContainerBuilder builder)
        {
            const string relDirPath = @"..\..\..\..\MilitaryFaculty.Reporting\MilitaryFaculty.Reporting\formulas\";
            const string baseName = "table-";
            var dirPath = Environment.CurrentDirectory + relDirPath;

            var files = Enumerable.Range(1, 4)
                                  .Select(i => dirPath + baseName + i + ".xml")
                                  .ToList();

            builder.RegisterType<ReportTableProvider>()
                   .As<IReportTableProvider>()
                   .WithParameter("files", files)
                   .SingleInstance();
        }

        private static void RegisterDataProviders(ContainerBuilder builder)
        {
            builder.RegisterType<ReportDataProvider>()
                   .AsSelf();

            builder.RegisterAssemblyTypes(typeof (ReportDataProvider).Assembly)
                   .Where(type => type.IsAssignableTo<IDataProvider>())
                   .As<IDataProvider>();
        }

        private static void RegisterExcelReportingService(ContainerBuilder builder)
        {
            builder.RegisterType<ExcelReportingService>()
                   .AsSelf()
                   .SingleInstance();
        }
    }
}
