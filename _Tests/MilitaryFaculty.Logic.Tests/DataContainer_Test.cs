using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Serialization;
using MilitaryFaculty.Data;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Reporting;
using MilitaryFaculty.Reporting.Data;
using MilitaryFaculty.Reporting.Excel;
using MilitaryFaculty.Reporting.ReportObjectDomain;
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
            var reportDataProvider = new ReportDataProvider(new IDataProvider[]
                                                            {
                                                                new CathedrasDataProvider(
                                                                    new Repository<Cathedra>(context)),
                                                                new ProfessorsDataProvider(
                                                                    new Repository<Professor>(context)),
                                                                new PublicationsDataProvider(
                                                                    new Repository<Publication>(context)),
                                                                new ExhibitionsDataProvider(
                                                                    new Repository<Exhibition>(context)),
                                                                new ConferencesDataProvider(
                                                                    new Repository<Conference>(context))
                                                            });

            const string relDirPath = @"..\..\..\..\..\MilitaryFaculty.Reporting\MilitaryFaculty.Reporting\formulas\";
            const string tableBaseName = "table-";
            const string formulaBaseName = "formulas-";
            var dirPath = Environment.CurrentDirectory + relDirPath;

            var tableFiles = Enumerable.Range(1, 4)
                                       .Select(i => dirPath + tableBaseName + i + ".xml")
                                       .ToList();

            var formulaFiles = Enumerable.Range(1, 4)
                                         .Select(i => dirPath + formulaBaseName + i + ".xml")
                                         .ToList();

            var formulaProvider = new FormulaProvider(formulaFiles);
            var tableProvider = new FacultyReportTableProvider(tableFiles);

            var reportObject = new ReportObject(tableProvider, formulaProvider, reportDataProvider);
            var reportingService = new SingleInstanceExcelService(reportObject);
            reportingService.ExportReport(@"D:\1.xlsx");
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
                    }
                ;

            new XmlSerializer(typeof(List<XFormula>)).Serialize(XmlWriter.Create(@"D:\1.xml"), t);
        }
    }

    // ReSharper restore InconsistentNaming
}