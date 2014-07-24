using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Data
{
    internal class PublicationConfiguration : EntityTypeConfiguration<Publication>
    {
        public PublicationConfiguration()
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