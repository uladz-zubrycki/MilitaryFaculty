using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Data
{
    internal class ResearchConfiguration : EntityTypeConfiguration<Research>
    {
        public ResearchConfiguration()
        {
            HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.Name).IsRequired();
            Property(m => m.CreatedAt).IsRequired();

            HasRequired(m => m.Author).WithMany(p => p.Researches);
            HasMany(m => m.Maintaners);
        }
    }
}