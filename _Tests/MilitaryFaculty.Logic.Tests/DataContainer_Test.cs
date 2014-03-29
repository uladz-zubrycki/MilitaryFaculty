using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
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
			//Кашкаров
			//Касанин
			Expression<Func<Professor, bool>> profModifier = prof => prof.FullName.LastName.StartsWith("Касанин");
			Expression<Func<Publication, bool>> pubModifier = pub => pub.Author.FullName.LastName.StartsWith("Касанин");
			Expression<Func<Exhibition, bool>> exhModifier = exh => exh.Participant.FullName.LastName.StartsWith("Касанин");
			Expression<Func<Conference, bool>> conModifier = con => con.Curator.FullName.LastName.StartsWith("Касанин");

			var reportDataProvider = new ReportDataProvider(new IDataProvider[]
			{
				new CathedrasDataProvider(new Repository<Cathedra>(context)),
				new ProfessorsDataProvider(new Repository<Professor>(context), profModifier),
				new PublicationsDataProvider(new Repository<Publication>(context), pubModifier),
				new ExhibitionsDataProvider(new Repository<Exhibition>(context), exhModifier),
				new ConferencesDataProvider(new Repository<Conference>(context), conModifier)
			});

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

			//var reportObjectFactory = new FacultyReportObjectFactory(tableProvider, formulaProvider, reportDataProvider);
			var reportObject = new ReportObject(tableProvider, formulaProvider, reportDataProvider);
			var reportingService = new ExcelReportingService();
			reportingService.ExportReport(@"D:\1.xlsx", reportObject);
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

			new XmlSerializer(typeof(List<XFormula>)).Serialize(XmlWriter.Create(@"D:\1.xml"), t);
		}
	}

	// ReSharper restore InconsistentNaming
}