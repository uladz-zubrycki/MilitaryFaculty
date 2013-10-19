using System.Data.Entity.ModelConfiguration;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Data
{
    internal class ConferenceReportConfiguration : ComplexTypeConfiguration<ConferenceReport> 
    {
        public ConferenceReportConfiguration()
        {
            Property(m => m.OrganizationCorrectness).IsRequired();
            Property(m => m.ReportMaterials).IsRequired();
            Property(m => m.ResultsUsage).IsRequired();
            Property(m => m.ThemeActuality).IsRequired();
        }
    }
}
