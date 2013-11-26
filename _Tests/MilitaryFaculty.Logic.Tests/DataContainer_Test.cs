using System;
using System.IO;
using System.Windows.Forms;
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
        private string savePath, pathInfo, pathFormulas;
        private Excel.Application xlApp;
        private Excel.Workbook xlWorkBook;
        private Excel.Worksheet xlWorkSheet;
        private readonly object misValue = System.Reflection.Missing.Value;

        [SetUp]
        public void SetUp()
        {
            //var basePath = AppDomain.CurrentDomain.BaseDirectory;
            savePath = @"d:\Other\git_projects\";
            savePath += @"csharp-test-Excel.xls";
            pathFormulas = @"d:\Other\git_projects\MilitaryFaculty\MilitaryFaculty.Logic\MilitaryFaculty.Logic.Services\XmlTables\";
            pathInfo = @"d:\Other\git_projects\MilitaryFaculty\MilitaryFaculty.Logic\MilitaryFaculty.Logic.Services\XmlTables\";
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
                    new CustomDataProvider(),
                    new ProfessorsDataProvider(),
                    new PublicationsDataProvider(), 
                });

            var tableInfo = TableInfo.Deserialize(pathInfo + @"FirstTableInfo.xml");
            var tableFormulas = TableFormulas.Deserialize(pathFormulas + @"FirstTableFormulas.xml");
            var dc = new DataContainer(tableInfo, tableFormulas, dataModule);
            dc.GenerateExcelSheet(xlWorkSheet);

            tableInfo = TableInfo.Deserialize(pathInfo + @"SecondTableInfo.xml");
            tableFormulas = TableFormulas.Deserialize(pathFormulas + @"SecondTableFormulas.xml");
            dc = new DataContainer(tableInfo, tableFormulas, dataModule);
            dc.GenerateExcelSheet(xlWorkSheet);

            tableInfo = TableInfo.Deserialize(pathInfo + @"ThirdTableInfo.xml");
            tableFormulas = TableFormulas.Deserialize(pathFormulas + @"ThirdTableFormulas.xml");
            dc = new DataContainer(tableInfo, tableFormulas, dataModule);
            dc.GenerateExcelSheet(xlWorkSheet);

            tableInfo = TableInfo.Deserialize(pathInfo + @"FourthTableInfo.xml");
            tableFormulas = TableFormulas.Deserialize(pathFormulas + @"FourthTableFormulas.xml");
            dc = new DataContainer(tableInfo, tableFormulas, dataModule);
            dc.GenerateExcelSheet(xlWorkSheet);

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
