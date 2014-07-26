using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MilitaryFaculty.Data.Configurations
{
    internal class Book : EntityTypeConfiguration<Domain.Book>
    {
        public Book()
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