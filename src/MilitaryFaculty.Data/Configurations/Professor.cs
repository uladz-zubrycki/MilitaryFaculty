using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MilitaryFaculty.Data.Configurations
{
    internal class Professor : EntityTypeConfiguration<Domain.Professor>
    {
        public Professor()
        {
            HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.AcademicDegree).IsRequired();
            Property(m => m.AcademicRank).IsRequired();
            Property(m => m.JobPosition).IsRequired();
            Property(m => m.MilitaryRank).IsRequired();
            Property(m => m.EnrollmentDate).IsRequired();
            Property(m => m.DismissalDate).IsOptional();

            HasRequired(m => m.Cathedra).WithMany(c => c.Professors);
        }
    }
}