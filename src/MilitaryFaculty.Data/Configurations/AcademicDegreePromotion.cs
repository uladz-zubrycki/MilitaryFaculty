using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MilitaryFaculty.Data.Configurations
{
    internal class AcademicDegreePromotion : EntityTypeConfiguration<Domain.AcademicDegreePromotion>
    {
        public AcademicDegreePromotion()
        {
            HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.PromotedAt).IsRequired();
            Property(m => m.TargetAcademicDegree).IsRequired();

            HasRequired(m => m.Professor).WithMany(p => p.AcademicDegreePromotions);
        }
    }
}
