using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Data
{
    public class DissertationConfiguration : EntityTypeConfiguration<Dissertation>
    {
        public DissertationConfiguration()
        {
            HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.Name).IsRequired();
            Property(m => m.CreatedAt).IsRequired();
            Property(m => m.TargetAcademicRank).IsRequired();

            HasRequired(m => m.Author).WithMany(p => p.Dissertations);
        }
    }
}