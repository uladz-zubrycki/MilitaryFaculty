using System.Collections.Generic;
using MilitaryFaculty.Reporting.Structure.XmlDomain;

namespace MilitaryFaculty.Reporting.Structure
{
    public interface IReportTableProvider
    {
        ICollection<XReportTable> GetTables();
    }
}