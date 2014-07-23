using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Domain.Base;

namespace MilitaryFaculty.Data
{
    internal class InventiveApplicationConfiguration : EntityTypeConfiguration<InventiveApplication>
    {
        public InventiveApplicationConfiguration()
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