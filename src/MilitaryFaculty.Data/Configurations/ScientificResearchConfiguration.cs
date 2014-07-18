using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Domain.ScientificResearch;

namespace MilitaryFaculty.Data
{
    internal class ScientificResearchConfiguration : EntityTypeConfiguration<ScientificResearch>
    {
        public ScientificResearchConfiguration()
        {
            HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.Date).IsRequired();
            Property(m => m.Name).IsRequired();
            Property(m => m.PagesCount).IsRequired();
            Property(m => m.State).IsRequired();

            HasRequired(m => m.Author).WithMany(p => p.ScientificResearches);
        }
    }
}
