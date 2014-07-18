using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Data
{
    internal class ConferenceConfiguration : EntityTypeConfiguration<Conference>
    {
        public ConferenceConfiguration()
        {
            HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.Name).IsRequired().HasMaxLength(Conference.NameMaxLength);

            Property(m => m.Date).IsRequired();
            Property(m => m.EventLevel).IsRequired();

            HasRequired(m => m.Curator).WithMany(p => p.Conferences);
        }
    }
}