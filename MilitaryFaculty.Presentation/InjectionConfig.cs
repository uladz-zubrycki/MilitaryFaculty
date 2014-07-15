﻿using System;
using System.Configuration;
using System.IO;
using System.Linq;
using Autofac;
using Autofac.Features.Indexed;
using MilitaryFaculty.Data;
using MilitaryFaculty.Reporting;
using MilitaryFaculty.Reporting.Data;
using MilitaryFaculty.Reporting.Excel;
using MilitaryFaculty.Reporting.Providers;

namespace MilitaryFaculty.Presentation
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
            RegisterReportTableResolver(builder);
            RegisterExcelReportingServices(builder);
            RegisterReportGenerator(builder);

            return builder.Build();
        }

        /// <summary>
        ///     Registers repositories
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
            var connectionStringSettings = ConfigurationManager.ConnectionStrings["Current"];
            var connectionString = connectionStringSettings.ConnectionString;

            builder.RegisterType<EntityContext>()
                   .AsSelf()
                   .WithParameter("connectionString", connectionString)
                   .SingleInstance();
        }

        private static void RegisterFormulaProvider(ContainerBuilder builder)
        {
            var curPath = Environment.CurrentDirectory;
            var formulasFileName = ConfigurationManager.AppSettings["formulasFile"];
            var formulasFilePath = Path.Combine(curPath, formulasFileName);

            builder.RegisterType<FormulaProvider>()
                   .As<IFormulaProvider>()
                   .WithParameter("filePath", formulasFilePath)
                   .SingleInstance();
        }

        //<Refactor>
        private static void RegisterReportTableTypeFactory(ContainerBuilder builder)
        {
            Func<IComponentContext, Func<ReportType, IReportTableProvider>> factory =
                ctx => type =>
                {
                    var index = ctx.Resolve<IIndex<ReportType, IReportTableProvider>>();
                    return index[type];
                };

            builder.Register(factory)
                   .As<Func<ReportType, IReportTableProvider>>()
                   .SingleInstance();
        }

        private static void RegisterFacultyReportTableProvider(ContainerBuilder builder)
        {
            var curPath = Environment.CurrentDirectory;
            var tablesRelPath = ConfigurationManager.AppSettings["facultyTablesPath"];
            var tablesPath = Path.Combine(curPath, tablesRelPath);

            builder.RegisterType<ReportTableProvider>()
                   .Keyed<IReportTableProvider>(ReportType.Faculty)
                   .WithParameter("tablesPath", tablesPath)
                   .SingleInstance();
        }

        private static void RegisterProfessorReportTableProvider(ContainerBuilder builder)
        {
            var curPath = Environment.CurrentDirectory;
            var tablesRelPath = ConfigurationManager.AppSettings["professorTablesPath"];
            var tablesPath = Path.Combine(curPath, tablesRelPath);

            builder.RegisterType<ReportTableProvider>()
                   .Keyed<IReportTableProvider>(ReportType.Professor)
                   .WithParameter("tablesPath", tablesPath)
                   .SingleInstance();
        }

        private static void RegisterReportTableResolver(ContainerBuilder builder)
        {
            builder.RegisterType<ReportTableResolver>()
                   .As<IReportTableResolver>();
        }
        //</Refactor>

        private static void RegisterDataProviders(ContainerBuilder builder)
        {
            builder.RegisterType<ReportDataProvider>()
                   .AsSelf();

            builder.RegisterType<DataProvidersContainer>()
                   .AsSelf();

            builder.RegisterAssemblyTypes(typeof (DataProvidersContainer).Assembly)
                   .Where(type => typeof (IDataProvider).IsAssignableFrom(type))
                   .AsSelf();
        }

        private static void RegisterExcelReportingServices(ContainerBuilder builder)
        {
            builder.RegisterType<ExcelReportingService>()
                   .As<IExcelReportingService>()
                   .SingleInstance();
        }

        private static void RegisterReportGenerator(ContainerBuilder builder)
        {
            builder.RegisterType<ReportGenerator>()
                   .As<IReportGenerator>()
                   .SingleInstance();
        }
    }
}