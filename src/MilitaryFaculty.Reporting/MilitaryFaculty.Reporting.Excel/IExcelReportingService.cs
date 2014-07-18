using System.Collections.Generic;
using MilitaryFaculty.Reporting.ReportDomain;

namespace MilitaryFaculty.Reporting.Excel
{
    public interface IExcelReportingService
    {
        void ExportReport(string filePath, Report reportObject);
        void ExportReport(string filePath, ICollection<Report> reportObjects);
    }
}
