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
            Property(m => m.Name).IsRequired().HasMaxLength(Cathedra.NameMaxLength);

            Property(m => m.LevelOfOrganizationIc).IsRequired();
            Property(m => m.LevelOfOrganizationRc).IsRequired();
            Property(m => m.LevelOfOrganizationRrs).IsRequired();
            Property(m => m.LevelOfOrganizationUc).IsRequired();
            Property(m => m.LevelOfOrganizationUrs).IsRequired();

            Property(m => m.HistoricalWorkOrganization).IsRequired();
            Property(m => m.MilitaryScientificSupportState).IsRequired();
            Property(m => m.ProfsOrganization).IsRequired();
        }
    }
}