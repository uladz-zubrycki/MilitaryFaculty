using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Data
{
    internal class ParticipationConfiguration : EntityTypeConfiguration<Participation>
    {
        public ParticipationConfiguration()
        {
            HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.StartDate).IsRequired();
            Property(m => m.Name).IsRequired();
            Property(m => m.PlaceType).IsRequired();

            HasRequired(m => m.Participant).WithMany(p => p.Participations);
        }
    }
}