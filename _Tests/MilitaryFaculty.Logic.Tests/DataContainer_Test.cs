using System.Collections.Generic;
using System.Configuration;
using ClosedXML.Excel;
using MilitaryFaculty.Data;
using MilitaryFaculty.Logic.DataProviders;
using MilitaryFaculty.Logic.XmlDomain;
using NUnit.Framework;

namespace MilitaryFaculty.Logic.Tests
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class DataContainer_Test
    {
        private string savePath, xmlPath;

        private const string projectDir = @"d:\Other\git_projects\MilitaryFaculty";

        private EntityContext context;

        [SetUp]
        public void SetUp()
        {
            const string conName = "Current";

            var connectionString = ConfigurationManager.ConnectionStrings[conName].ConnectionString;
            context = new EntityContext(connectionString);

            //var basePath = AppDomain.CurrentDomain.BaseDirectory;
            savePath = @"d:\Other\git_projects";
            
            xmlPath = projectDir + @"\MilitaryFaculty.Logic\MilitaryFaculty.Logic.Services\XmlTables";
        }

        [Test]
        public void TestExcel()
        {
            //var workbook = new XLWorkbook();
            //var worksheet = workbook.Worksheets.Add("Sample Sheet");
            //worksheet.Cell("A1").Value = "Hello World!";
            //workbook.SaveAs(path + @"\HelloWorld.xlsx");

            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Results");

            var dataModule = new DataModule();
            dataModule.RegisterProviders(new IDataProvider[]
                {
                    new CathedrasDataProvider(new CathedraRepository(context)),
                    new ProfessorsDataProvider(new ProfessorRepository(context)),
                    new PublicationsDataProvider(new PublicationRepository(context)),
                    new ExhibitionsDataProvider(new ExhibitionRepository(context)),
                    new ConferencesDataProvider(new ConferenceRepository(context))
                });

            var infoNames = new List<string>
                {
                    @"\FirstTableInfo.xml",
                    @"\SecondTableInfo.xml",
                    @"\ThirdTableInfo.xml",
                    @"\FourthTableInfo.xml"
                };

            var formulaNames = new List<string>
                {
                    @"\FirstTableFormulas.xml",
                    @"\SecondTableFormulas.xml",
                    @"\ThirdTableFormulas.xml",
                    @"\FourthTableFormulas.xml"
                };

            for (var i = 0; i < infoNames.Count; i++)
            {
                var tableInfo = TableInfo.Deserialize(xmlPath + infoNames[i]);
                var tableFormulas = TableFormulas.Deserialize(xmlPath + formulaNames[i]);
                var dc = new DataContainer(tableInfo, tableFormulas, dataModule);
                dc.GenerateExcelSheet(worksheet);
            }

            workbook.SaveAs(savePath + @"\results.xlsx");
        }
    }
    // ReSharper restore InconsistentNaming
}
