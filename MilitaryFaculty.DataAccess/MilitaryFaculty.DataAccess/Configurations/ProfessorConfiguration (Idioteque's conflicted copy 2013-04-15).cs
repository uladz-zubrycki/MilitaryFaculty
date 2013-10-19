using System.Data.Entity.ModelConfiguration;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.DataAccess.Configurations
{
    public class ProfessorConfiguration : EntityTypeConfiguration<Professor>
    {
        public ProfessorConfiguration()
        {
            //this.Property(p => p.FullName).IsRequired();
            //this.Property(p => p.Cathedra).IsRequired();
            Property(p => p.AcademicDegree).IsRequired();
            Property(p => p.AcademicRank).IsRequired();
            Property(p => p.JobPosition).IsRequired();
            Property(p => p.MilitaryRank).IsRequired();
        }
    }
}