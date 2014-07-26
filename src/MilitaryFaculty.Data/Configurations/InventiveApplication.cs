using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MilitaryFaculty.Data.Configurations
{
    internal class InventiveApplication : EntityTypeConfiguration<Domain.InventiveApplication>
    {
        public InventiveApplication()
        {
            HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.Name).IsRequired();
            Property(m => m.CreatedAt).IsRequired();
            Property(m => m.Type).IsRequired();
            Property(m => m.Status).IsRequired();

            HasRequired(m => m.Author).WithMany(p => p.InventiveApplications);
        }
    }
}