using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Data
{
    internal class AcademicDegreeChangingConfiguraton : EntityTypeConfiguration<AcademicDegreeChanging>
    {
        public AcademicDegreeChangingConfiguraton()
        {
            HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.Date).IsRequired();
            Property(m => m.ResultedDegree).IsRequired();

            HasRequired(m => m.Targer).WithMany(p => p.AcademicDegreeChangings);
        }
    }
}
