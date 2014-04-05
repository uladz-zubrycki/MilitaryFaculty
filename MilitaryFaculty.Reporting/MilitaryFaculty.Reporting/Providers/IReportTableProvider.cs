using System.Collections.Generic;
using MilitaryFaculty.Reporting.XmlDomain;

namespace MilitaryFaculty.Reporting.Providers
{
    public interface IReportTableProvider
    {
        ICollection<XReportTable> GetTables();
    }
}