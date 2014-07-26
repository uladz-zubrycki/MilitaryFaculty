using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MilitaryFaculty.Data.Configurations
{
    internal class Exhibition : EntityTypeConfiguration<Domain.Exhibition>
    {
        public Exhibition()
        {
            HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.Name).IsRequired().HasMaxLength(Domain.Exhibition.NameMaxLength);
            Property(m => m.Date).IsRequired();
            Property(m => m.Award).IsRequired();
            Property(m => m.EventLevel).IsRequired();

            HasRequired(m => m.Participant).WithMany(p => p.Exhibitions);
        }
    }
}