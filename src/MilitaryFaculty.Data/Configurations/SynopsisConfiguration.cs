using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MilitaryFaculty.Domain;


namespace MilitaryFaculty.Data 
{
    internal class SynopsisConfiguration : EntityTypeConfiguration<Synopsis>
    {
        public SynopsisConfiguration()
        {
            HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.Date).IsRequired();
            Property(m => m.Name).IsRequired();
            Property(m => m.SynopsisType).IsRequired();
            Property(m => m.SynopsisDegree).IsRequired();

            HasRequired(m => m.Author).WithMany(p => p.Synopses);
        }
    }
}
