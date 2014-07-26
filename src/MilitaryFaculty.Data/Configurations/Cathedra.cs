using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MilitaryFaculty.Data.Configurations
{
    internal class Cathedra : EntityTypeConfiguration<Domain.Cathedra>
    {
        public Cathedra()
        {
            HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.Name).IsRequired().HasMaxLength(Domain.Cathedra.NameMaxLength);
        }
    }
}