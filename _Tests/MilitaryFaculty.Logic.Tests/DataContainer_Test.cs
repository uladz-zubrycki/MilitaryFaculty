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
            //var basePath = AppDomain.CurrentDomain.BaseDirectory;
            savePath = @"d:\Other\git_projects\MilitaryFaculty\_Tests\";
            savePath += @"csharp-test-Excel.xls";
            pathFormulas = @"d:\Other\git_projects\MilitaryFaculty\MilitaryFaculty.Logic\MilitaryFaculty.Logic.Services\XmlTables\";
            //pathFormulas += @"SecondTableFormulas.xml";
            pathInfo = @"d:\Other\git_projects\MilitaryFaculty\MilitaryFaculty.Logic\MilitaryFaculty.Logic.Services\XmlTables\";
            //pathInfo += @"SecondTableInfo.xml";
        }

        [Test]
        public void TestExcel()
        {
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet) xlWorkBook.Worksheets.Item[1];

            var dc = new DataContainer(pathFormulas + @"FirstTableFormulas.xml", pathInfo + @"FirstTableInfo.xml");
            dc.GenerateExcelSheet(xlWorkSheet);
            dc = new DataContainer(pathFormulas + @"SecondTableFormulas.xml", pathInfo + @"SecondTableInfo.xml");
            dc.GenerateExcelSheet(xlWorkSheet);
            dc = new DataContainer(pathFormulas + @"ThirdTableFormulas.xml", pathInfo + @"ThirdTableInfo.xml");
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
