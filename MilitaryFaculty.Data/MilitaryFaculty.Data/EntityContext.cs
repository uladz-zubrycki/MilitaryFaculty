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
        public DbSet<Synopsis> Synopses { get; set; }

        public DbSet<ScientificExpertise> ScientificExpertises { get; set; }
        public DbSet<ScientificRequest> ScientificRequests { get; set; }
        public DbSet<ScientificResearch> ScientificResearches { get; set; }
        public DbSet<ImprovementSuggestion> ImprovementSuggestions { get; set; }

        public EntityContext(string connectionString)
            : base(connectionString)
        {
            Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CathedraConfiguration());
            modelBuilder.Configurations.Add(new ProfessorConfiguration());
            modelBuilder.Configurations.Add(new ConferenceConfiguration());
            modelBuilder.Configurations.Add(new PublicationConfiguration());
            modelBuilder.Configurations.Add(new ExhibitionConfiguration());
            modelBuilder.Configurations.Add(new BookConfiguration());

            modelBuilder.Configurations.Add(new ScientificExpertiseConfiguration());
            modelBuilder.Configurations.Add(new ScientificRequestConfiguration());
            modelBuilder.Configurations.Add(new ScientificResearchConfiguration());
            modelBuilder.Configurations.Add(new ImprovementSuggestionConfiguration());
            modelBuilder.Configurations.Add(new ParticipationConfiguration());
            modelBuilder.Configurations.Add(new AcademicDegreeChangingConfiguraton());
            modelBuilder.Configurations.Add(new SynopsisConfiguration());

            modelBuilder.Configurations.Add(new ConferenceReportConfiguration());
            modelBuilder.Configurations.Add(new FullNameConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}