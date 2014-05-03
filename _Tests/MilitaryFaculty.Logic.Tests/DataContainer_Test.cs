using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using MilitaryFaculty.Data;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Reporting;
using MilitaryFaculty.Reporting.Data;
using MilitaryFaculty.Reporting.Data.DataProviders;
using MilitaryFaculty.Reporting.Excel;
using MilitaryFaculty.Reporting.Providers;
using MilitaryFaculty.Reporting.ReportDomain;
using MilitaryFaculty.Reporting.XmlDomain;
using NUnit.Framework;

namespace MilitaryFaculty.Logic.Tests
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class DataContainer_Test
    {
        [SetUp]
        public void SetUp()
        {
            const string conName = "Current";

            var connectionString = ConfigurationManager.ConnectionStrings[conName].ConnectionString;
            context = new EntityContext(connectionString);
        }

        private EntityContext context;

        [Test]
        public void TestExcel()
        {
            Repository<Professor> profRepository;

            var reportDataProvider = new ReportDataProvider(new DataProvidersContainer(
                new CathedrasDataProvider(new Repository<Cathedra>(context)),
                new ConferencesDataProvider(new Repository<Conference>(context)),
                new ExhibitionsDataProvider(new Repository<Exhibition>(context)),
                new ProfessorsDataProvider(profRepository = new Repository<Professor>(context)),
                new PublicationsDataProvider(new Repository<Publication>(context))
                ));

            var relDirPath = ConfigurationManager.AppSettings["relatedFormulasPath"];

            var formulaBaseName = ConfigurationManager.AppSettings["baseFormulasName"];
            var formulasFilesCount = int.Parse(ConfigurationManager.AppSettings["formulasFilesCount"]);

            //var tableBaseName = ConfigurationManager.AppSettings["baseFacultyTableName"];
            //var tablesCount = int.Parse(ConfigurationManager.AppSettings["facultyTablesCount"]);

            //var tableBaseName = ConfigurationManager.AppSettings["baseProfessorTableName"];
            //var tablesCount = int.Parse(ConfigurationManager.AppSettings["professorsTablesCount"]);
            //var dirPath = Environment.CurrentDirectory + relDirPath;

            var curDir = @"d:\Other\git_projects\MilitaryFaculty\MilitaryFaculty.Presentation\Formulas\";
            string fileName = "formulas.xml";
            var filePath = Path.Combine(curDir, fileName);
            var formulaProvider = new FormulaProvider(filePath);
            fileName = @"Professor\";
            filePath = Path.Combine(curDir, fileName);
            var tableProvider = new ReportTableProvider(filePath);

            var reports = new List<Report>();
            var reportGenerator = new ReportGenerator(tableProvider, formulaProvider, reportDataProvider);
            
            var interval = new TimeInterval(new DateTime(2000, 1, 1), DateTime.Now);
            //var prof = profRepository.Table.Single(p => p.FullName.LastName.StartsWith("Кашкаров"));
            //reports.Add(reportGenerator.Generate(prof, interval));
            //prof = profRepository.Table.Single(p => p.FullName.LastName.StartsWith("Касанин"));
            //reports.Add(reportGenerator.Generate(prof, interval));

            //var unified = Report.Unify(reports);

            var report = reportGenerator.Generate(null, interval);

            var reportingService = new ExcelReportingService();
            reportingService.ExportReport(@"D:\2.xlsx", report);
        }
    }

    // ReSharper restore InconsistentNaming
}