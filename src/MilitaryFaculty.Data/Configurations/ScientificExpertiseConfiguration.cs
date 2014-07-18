using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Domain.ScientificExpertise;

namespace MilitaryFaculty.Data
{
    internal class ScientificExpertiseConfiguration : EntityTypeConfiguration<ScientificExpertise>
    {
        public ScientificExpertiseConfiguration()
        {
            HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.Date).IsRequired();
            Property(m => m.Name).IsRequired();

            HasRequired(m => m.Author).WithMany(p => p.ScientificExpertises);
        }
    }
}