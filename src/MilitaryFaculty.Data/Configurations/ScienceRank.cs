using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MilitaryFaculty.Data.Configurations
{
    public class ScienceRankMetricDefinition : EntityTypeConfiguration<Domain.ScienceRankMetricDefinition>
    {
        public ScienceRankMetricDefinition()
        {
            HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.Name).IsRequired();
            Property(m => m.MaxValue).IsOptional();
        }
    }

    public class ScienceRankMetric : EntityTypeConfiguration<Domain.ScienceRankMetric>
    {
        public ScienceRankMetric()
        {
            HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.Value).IsRequired();

            HasRequired(m => m.Definition);
        }
    }

    public class ScienceRank: EntityTypeConfiguration<Domain.ScienceRank>
    {
        public ScienceRank()
        {
            HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.CreatedAt).IsRequired();

            HasMany(m => m.Metrics).WithRequired();
            HasRequired(m => m.Cathedra).WithMany(m => m.ScienceRanks);
        }
    }
}
