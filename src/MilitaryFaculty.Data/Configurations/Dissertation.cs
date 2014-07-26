using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MilitaryFaculty.Data.Configurations
{
    public class Dissertation : EntityTypeConfiguration<Domain.Dissertation>
    {
        public Dissertation()
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