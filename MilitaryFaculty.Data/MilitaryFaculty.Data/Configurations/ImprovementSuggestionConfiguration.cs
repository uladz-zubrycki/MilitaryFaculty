using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Data
{
    internal class ImprovementSuggestionConfiguration : EntityTypeConfiguration<ImprovementSuggestion>
    {
        public ImprovementSuggestionConfiguration()
        {
            HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.Date).IsRequired();
            Property(m => m.Name).IsRequired();

            HasRequired(m => m.Author).WithMany(p => p.ImprovementSuggestions);
        }
    }
}