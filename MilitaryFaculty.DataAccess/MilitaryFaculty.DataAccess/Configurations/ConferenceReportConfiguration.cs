using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.DataAccess.Configurations
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
