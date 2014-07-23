using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Data
{
    internal class ReviewConfiguration : EntityTypeConfiguration<Review>
    {
        public ReviewConfiguration()
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
