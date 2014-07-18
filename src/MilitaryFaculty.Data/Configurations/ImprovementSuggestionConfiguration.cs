using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Domain.ImprovementSuggestion;

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
            Property(m => m.SuggestionState).IsRequired();

            HasRequired(m => m.Author).WithMany(p => p.ImprovementSuggestions);
        }
    }
}