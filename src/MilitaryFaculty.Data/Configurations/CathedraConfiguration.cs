using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Data
{
    internal class CathedraConfiguration : EntityTypeConfiguration<Cathedra>
    {
        public CathedraConfiguration()
        {
            HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.Name).IsRequired()
                                 .HasMaxLength(Cathedra.NameMaxLength);
        }
    }
}