using Autofac;
using MilitaryFaculty.Application.AppStartup;
using MilitaryFaculty.Data;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Reporting;
using MilitaryFaculty.Reporting.Excel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MilitaryFaculty.Logic.Tests
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class DataContainer_Test
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void TestExcel()
        {
            var container = InjectionConfig.Register(new ContainerBuilder());
            var excelService = container.Resolve<IExcelReportingService>();
            var reportGenerator = container.Resolve<IReportGenerator>();
            var professorRepository = container.Resolve<IRepository<Professor>>();

            var interval = new TimeInterval(new DateTime(2000, 1, 1), DateTime.Now);

            var reports = new List<Report>();
            var prof = professorRepository.Table.Single(p => p.FullName.LastName.StartsWith("Кашкаров"));
            reports.Add(reportGenerator.GenerateProfessorReport(prof, interval));
            prof = professorRepository.Table.Single(p => p.FullName.LastName.StartsWith("Касанин"));
            reports.Add(reportGenerator.GenerateProfessorReport(prof, interval));

            var report = reportGenerator.GenerateFacultyReport(interval);

            //Generation
            excelService.ExportReport(@"D:\1.xlsx", reports);
            excelService.ExportReport(@"D:\2.xlsx", report);
        }
    }

    // ReSharper restore InconsistentNaming
}