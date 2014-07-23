using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Data
{
    internal class ScientificExpertiseConfiguration : EntityTypeConfiguration<ScientificExpertise>
    {
        public ScientificExpertiseConfiguration()
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