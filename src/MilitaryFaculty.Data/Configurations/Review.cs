using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MilitaryFaculty.Data.Configurations
{
    internal class Review : EntityTypeConfiguration<Domain.Review>
    {
        public Review()
        {
            HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.Name).IsRequired();
            Property(m => m.CreatedAt).IsRequired();
            Property(m => m.Type).IsRequired();
            Property(m => m.TargetAcademicRank).IsRequired();

            HasRequired(m => m.Author).WithMany(p => p.Reviews);
        }
    }
}
