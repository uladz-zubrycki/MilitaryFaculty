using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MilitaryFaculty.Data.Configurations
{
    public class EfficiencyProposal : EntityTypeConfiguration<Domain.EfficiencyProposal>
    {
        public EfficiencyProposal()
        {
            HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.Text).IsRequired();
            Property(m => m.CreatedAt).IsRequired();
            Property(m => m.Status).IsRequired();

            HasRequired(m => m.Author).WithMany(p => p.EfficiencyProposals);
        }
    }
}