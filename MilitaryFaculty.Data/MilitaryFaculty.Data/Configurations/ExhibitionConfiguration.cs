using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Data
{
    class ExhibitionConfiguration : EntityTypeConfiguration<Exhibition>
    {
        public ExhibitionConfiguration()
        {
            HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.Name).IsRequired()
                                 .HasMaxLength(Exhibition.NameMaxLength);

            Property(m => m.Date).IsRequired();
            Property(m => m.Award).IsRequired();

            HasRequired(m => m.Participant).WithMany(p => p.Exhibitions);
        }
    }
}
