﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Data
{
    internal class ScientificRequestConfiguration : EntityTypeConfiguration<ScientificRequest>
    {
        public ScientificRequestConfiguration()
        {
            HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.Date).IsRequired();
            Property(m => m.Name).IsRequired();
            Property(m => m.ScientificResponce).IsRequired();
            Property(m => m.ScientificType).IsRequired();

            HasRequired(m => m.Author).WithMany(p => p.ScientificRequests);
        }
    }
}
