using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Data
{
    internal class BookConfiguration : EntityTypeConfiguration<Book>
    {
        public BookConfiguration()
        {
            HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.Name).IsRequired();
            Property(m => m.CreatedAt).IsRequired();
            Property(m => m.BookType).IsRequired();
            Property(m => m.PagesCount).IsRequired();

            HasRequired(m => m.Author).WithMany(p => p.Books);
        }
    }
}