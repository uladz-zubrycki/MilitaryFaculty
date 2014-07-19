using System.Data.Entity;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Data
{
    public class EntityContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Cathedra> Cathedras { get; set; }
        public DbSet<Conference> Conferences { get; set; }
        public DbSet<Exhibition> Exhibitions { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<DissertationWork> Synopses { get; set; }

        public DbSet<ScientificRequest> ScientificRequests { get; set; }
        public DbSet<ScientificResearch> ScientificResearches { get; set; }
        public DbSet<ImprovementSuggestion> ImprovementSuggestions { get; set; }

        public DbSet<AcademicDegreeChanging> AcademicDegreeChangings { get; set; }
        public DbSet<Participation> Participations { get; set; }
        public DbSet<ScientificExpertise> ScientificExpertises { get; set; }

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