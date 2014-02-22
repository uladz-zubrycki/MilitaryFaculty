using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Data
{
    internal class ExhibitionConfiguration : EntityTypeConfiguration<Exhibition>
    {
        public ExhibitionConfiguration()
        {
            HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.Name).IsRequired()
                                 .HasMaxLength(Exhibition.NameMaxLength);

            Property(m => m.Date).IsRequired();
            Property(m => m.AwardType).IsRequired();
            Property(m => m.EventLevel).IsRequired();

            HasRequired(m => m.Participant).WithMany(p => p.Exhibitions);
        }
    }
}