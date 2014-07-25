using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Data
{
    internal class CouncilParticipationConfiguration : EntityTypeConfiguration<CouncilParticipation>
    {
        public CouncilParticipationConfiguration()
        {
            HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.Name).IsRequired();
            Property(m => m.Start).IsRequired();
            Property(m => m.End).IsOptional();
            Property(m => m.Type).IsRequired();

            HasRequired(m => m.Participant).WithMany(p => p.CouncilsParticipations);
        }
    }
}
