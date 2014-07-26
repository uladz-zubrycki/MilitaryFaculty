using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MilitaryFaculty.Data.Configurations
{
    internal class Publication : EntityTypeConfiguration<Domain.Publication>
    {
        public Publication()
        {
            HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.PublicationType).IsRequired();
            Property(m => m.Name).IsRequired();
            Property(m => m.PagesCount).IsRequired();
            Property(m => m.CreatedAt).IsRequired();

            HasRequired(m => m.Author).WithMany(p => p.Publications);
        }
    }
}