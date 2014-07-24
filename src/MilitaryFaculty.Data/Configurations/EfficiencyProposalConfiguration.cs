using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Data
{
    public class EfficiencyProposalConfiguration : EntityTypeConfiguration<EfficiencyProposal>
    {
        public EfficiencyProposalConfiguration()
        {
            HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.Text).IsRequired();
            Property(m => m.CreatedAt).IsRequired();

            HasRequired(m => m.Author).WithMany(p => p.EfficiencyProposals);
        }
    }
}