using System;
using System.Collections.Generic;
using System.Configuration;
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

            var tableBaseName = ConfigurationManager.AppSettings["baseProfessorTableName"];
            var tablesCount = int.Parse(ConfigurationManager.AppSettings["professorsTablesCount"]);

            var dirPath = Environment.CurrentDirectory + relDirPath;

            var tableFiles = Enumerable.Range(1, tablesCount)
                                       .Select(i => dirPath + tableBaseName + i + ".xml")
                                       .ToList();

            var formulaFiles = Enumerable.Range(1, formulasFilesCount)
                                         .Select(i => dirPath + formulaBaseName + i + ".xml")
                                         .ToList();

            var formulaProvider = new FormulaProvider(formulaFiles);
            var tableProvider = new ReportTableProvider(tableFiles);

            var report = new List<Report>();
            var reportGenerator = new ReportGenerator(tableProvider, formulaProvider, reportDataProvider);
            
            var interval = new TimeInterval(new DateTime(2000, 1, 1), DateTime.Now);
            var prof = profRepository.Table.Single(p => p.FullName.LastName.StartsWith("Кашкаров"));
            report.Add(reportGenerator.Generate(prof, interval));
            prof = profRepository.Table.Single(p => p.FullName.LastName.StartsWith("Касанин"));
            report.Add(reportGenerator.Generate(prof, interval));

            var reportingService = new ExcelReportingService();
            reportingService.ExportReport(@"D:\1.xlsx", report);
        }

        [Test]
        public void fdf()
        {
            var t = new List<XFormula>
            {
                new XFormula
                {
                    Arguments = new List<XArgument>
                    {
                        new XArgument
                        {
                            Name = "fdf",
                            Text = "fdf"
                        }
                    },
                    Coefficients = new List<XCoefficient>
                    {
                        new XCoefficient
                        {
                            Name = "fdf",
                            Value = 43
                        }
                    },
                    Name = "fdf",
                    MaxValue = 43,
                    Id = "fd",
                    Expression = "fdf"
                }
            };

            new XmlSerializer(typeof (List<XFormula>)).Serialize(XmlWriter.Create(@"D:\1.xml"), t);
        }
    }

    // ReSharper restore InconsistentNaming
}