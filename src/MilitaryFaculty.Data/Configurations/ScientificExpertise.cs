using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MilitaryFaculty.Data.Configurations
{
    internal class ScientificExpertise : EntityTypeConfiguration<Domain.ScientificExpertise>
    {
        public ScientificExpertise()
        {
            HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.CreatedAt).IsRequired();
            Property(m => m.Name).IsRequired();
            Property(m => m.Type).IsRequired();

            HasRequired(m => m.Author).WithMany(p => p.ScientificExpertises);
        }
    }
}