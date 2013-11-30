using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using MilitaryFaculty.Data;
using MilitaryFaculty.Logic.DataProviders;
using MilitaryFaculty.Logic.XmlDomain;
using NUnit.Framework;
using Excel = Microsoft.Office.Interop.Excel;

namespace MilitaryFaculty.Logic.Tests
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class DataContainer_Test
    {
        private string savePath, xmlPath;
        private Excel.Application xlApp;
        private Excel.Workbook xlWorkBook;
        private Excel.Worksheet xlWorkSheet;
        private readonly object misValue = System.Reflection.Missing.Value;

        private const string projectDir = @"d:\Other\git_projects\MilitaryFaculty";

        private EntityContext context;

        [SetUp]
        public void SetUp()
        {
            const string conName = "Current";

            var connectionString = ConfigurationManager.ConnectionStrings[conName].ConnectionString;
            context = new EntityContext(connectionString);

            //var basePath = AppDomain.CurrentDomain.BaseDirectory;
            savePath = @"d:\Other\git_projects\";
            savePath += @"csharp-test-Excel.xls";

            xmlPath = projectDir + @"\MilitaryFaculty.Logic\MilitaryFaculty.Logic.Services\XmlTables";
        }

        [Test]
        public void TestExcel()
        {
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet) xlWorkBook.Worksheets.Item[1];

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
                dc.GenerateExcelSheet(xlWorkSheet);
            }

            File.Delete(savePath);
            xlWorkBook.SaveAs(savePath, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue,
                              misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue,
                              misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            ReleaseObject(xlWorkSheet);
            ReleaseObject(xlWorkBook);
            ReleaseObject(xlApp);
        }

        private static void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception Occured while releasing object " + ex);
            }
            finally
            {
                GC.Collect();
            }
        }
    }
    // ReSharper restore InconsistentNaming
}
