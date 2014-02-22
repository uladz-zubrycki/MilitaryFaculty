using System.Collections.Generic;
using MilitaryFaculty.Reporting.XmlDomain;

namespace MilitaryFaculty.Reporting
{
    public interface IReportTableProvider
    {
        ICollection<XReportTable> GetTables();
    }
}