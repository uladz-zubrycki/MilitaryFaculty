using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mime;
using Autofac;
using Autofac.Features.Indexed;
using MilitaryFaculty.Data;
using MilitaryFaculty.Reporting;
using MilitaryFaculty.Reporting.Data;
using MilitaryFaculty.Reporting.Excel;

namespace MilitaryFaculty.Presentation.Infrastructure
{
    public static class InjectionConfig
    {
        /// <summary>
        ///     Registers all neccessary services and their implementations in container.
        /// </summary>
        /// <param name="builder">Builder for service container.</param>
        /// <returns>Configured and built container.</returns>
        public static IContainer Register(ContainerBuilder builder)
        {
            RegisterRepositories(builder);
            RegisterEntityContext(builder);
            RegisterDataProviders(builder);
            RegisterFormulaProvider(builder);
            RegisterReportTableTypeFactory(builder);
            RegisterFacultyReportTableProvider(builder);
            RegisterProfessorReportTableProvider(builder);
            RegisterExcelReportingServices(builder);

            return builder.Build();
        }

        /// <summary>
        ///     Registers repositories.
        /// </summary>
        /// <param name="builder">Builder for service container.</param>
        private static void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof (Repository<>))
                .AsImplementedInterfaces();
        }

        /// <summary>
        ///     Registers entity context, using connection string from config file.
        /// </summary>
        /// <param name="builder">Builder for service container.</param>
        private static void RegisterEntityContext(ContainerBuilder builder)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Current"].ConnectionString;

            builder.RegisterType<EntityContext>()
                .AsSelf()
                .WithParameter("connectionString", connectionString)
                .SingleInstance();
        }

        private static void RegisterFormulaProvider(ContainerBuilder builder)
        {
            string relDirPath = ConfigurationManager.AppSettings["relatedFormulasPath"];
            string baseName = ConfigurationManager.AppSettings["baseFormulasName"];
            string dirPath = Environment.CurrentDirectory + relDirPath;

            List<string> files = Enumerable.Range(1, 4)
                .Select(i => dirPath + baseName + i + ".xml")
                .ToList();
            
            builder.RegisterType<FormulaProvider>()
                .As<IFormulaProvider>()
                .WithParameter("files", files)
                .SingleInstance();
        }

        private static void RegisterReportTableTypeFactory(ContainerBuilder builder)
        {
            Func<IComponentContext, Func<ReportType, IReportTableProvider>> factory =
                ctx => type =>
                {
                    var factDict = ctx.Resolve<IIndex<ReportType, IReportTableProvider>>();
                    return factDict[type];
                };

            builder.Register(factory)
                .As<Func<ReportType, IReportTableProvider>>()
                .SingleInstance();
        }

        private static void RegisterFacultyReportTableProvider(ContainerBuilder builder)
        {
            string relDirPath = ConfigurationManager.AppSettings["relatedFormulasPath"];
            string baseName = ConfigurationManager.AppSettings["baseFacultyTableName"];
            string dir = Environment.CurrentDirectory;
            
            string dirPath = Environment.CurrentDirectory + relDirPath;

            List<string> files = Enumerable.Range(1, 4)
                .Select(i => dirPath + baseName + i + ".xml")
                .ToList();

            builder.RegisterType<FacultyReportTableProvider>()
                .Keyed<IReportTableProvider>(ReportType.Faculty)
                .WithParameter("files", files)
                .SingleInstance();
        }

        private static void RegisterProfessorReportTableProvider(ContainerBuilder builder)
        {
            string relDirPath = ConfigurationManager.AppSettings["relatedFormulasPath"];
            string baseName = ConfigurationManager.AppSettings["baseProfessorTableName"]; ;
            string dirPath = Environment.CurrentDirectory + relDirPath;

            List<string> files = Enumerable.Range(1, 3)
                .Select(i => dirPath + baseName + i + ".xml")
                .ToList();

            builder.RegisterType<FacultyReportTableProvider>()
                .Keyed<IReportTableProvider>(ReportType.Professor)
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

        private static void RegisterExcelReportingServices(ContainerBuilder builder)
        {
            builder.RegisterType<SingleInstanceExcelService>()
                .AsSelf()
                .SingleInstance();

            builder.RegisterType<MultipleInstancesExcelService>()
                .AsSelf()
                .SingleInstance();

            builder.RegisterType<FacultyExcelReportingService>()
                .AsSelf()
                .SingleInstance();
        }
    }
}