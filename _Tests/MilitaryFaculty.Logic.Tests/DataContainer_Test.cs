using System;
using System.Windows.Forms;
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
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            savePath = @"d:\Other\git_projects\MilitaryFaculty\_Tests\";
            savePath += @"csharp-test-Excel.xls";
            pathFormulas = @"d:\Other\git_projects\MilitaryFaculty\MilitaryFaculty.Logic\MilitaryFaculty.Logic.Services\XmlTables\";
            pathFormulas += @"FirstTableFormulas.xml";
            pathInfo = @"d:\Other\git_projects\MilitaryFaculty\MilitaryFaculty.Logic\MilitaryFaculty.Logic.Services\XmlTables\";
            pathInfo += @"FirstTableInfo.xml";
        }

        [Test]
        public void TestExcel()
        {
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet) xlWorkBook.Worksheets.Item[1];

            var dc = new DataContainer(pathFormulas, pathInfo);
            dc.GenerateExcelSheet(xlWorkSheet);

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
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }
    // ReSharper restore InconsistentNaming
}
