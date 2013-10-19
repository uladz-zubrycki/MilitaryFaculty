using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.DataAccess.Configurations
{
    internal class ProfessorConfiguration : EntityTypeConfiguration<Professor>
    {
        public ProfessorConfiguration()
        {
            HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.AcademicDegree).IsRequired();
            Property(m => m.AcademicRank).IsRequired();
            Property(m => m.JobPosition).IsRequired();
            Property(m => m.MilitaryRank).IsRequired();

            HasRequired(m => m.Cathedra).WithMany(c => c.Professors);
        }
    }
}