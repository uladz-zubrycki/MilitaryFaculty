using System.Data.Entity;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Data
{
    public class EntityContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Cathedra> Cathedras { get; set; }
        public DbSet<Conference> Conferences { get; set; }
        public DbSet<CouncilParticipation> CouncilParticipations { get; set; }
        public DbSet<Dissertation> Dissertations { get; set; }
        public DbSet<EfficiencyProposal> EfficiencyProposals { get; set; }
        public DbSet<Exhibition> Exhibitions { get; set; }
        public DbSet<InventiveApplication> InventiveApplications { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Research> Researches { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ScientificExpertise> ScientificExpertises { get; set; }
        public DbSet<ScienceRank> ScientificRanks { get; set; }
        public DbSet<ScienceRankMetric> ScientificRankMetrics { get; set; }
        public DbSet<ScienceRankMetricDefinition> ScientificRankMetricDefinitions { get; set; }

        public EntityContext(string connectionString)
            : base(connectionString)
        {
            Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(typeof (EntityContext).Assembly);
        }
    }
}