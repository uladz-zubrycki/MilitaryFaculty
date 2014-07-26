using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MilitaryFaculty.Data.Configurations
{
    internal class CouncilParticipation : EntityTypeConfiguration<Domain.CouncilParticipation>
    {
        public CouncilParticipation()
        {
            HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.Name).IsRequired();
            Property(m => m.StartDate).IsRequired();
            Property(m => m.EndDate).IsOptional();
            Property(m => m.Type).IsRequired();

            HasRequired(m => m.Participant).WithMany(p => p.CouncilsParticipations);
        }
    }
}
