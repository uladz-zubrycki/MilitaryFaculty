using System.Collections.Generic;

namespace MilitaryFaculty.Reporting.Excel
{
    public interface IExcelReportingService
    {
        void ExportReport(string filePath, Report reportObject);
        void ExportReport(string filePath, ICollection<Report> reportObjects);
    }
}
