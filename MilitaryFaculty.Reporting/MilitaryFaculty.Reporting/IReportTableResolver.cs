using System;
using MilitaryFaculty.Reporting.Providers;

namespace MilitaryFaculty.Reporting
{
    public interface IReportTableResolver
    {
        IReportTableProvider GetTableProvider(Type type);
    }
}
