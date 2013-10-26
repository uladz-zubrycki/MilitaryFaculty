using System.Data.Entity;
using System.Linq;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Data
{
    public class EntityContext : DbContext
    {
        #region Class Properties

        public DbSet<Cathedra> Cathedras { get; set; }
        public DbSet<Conference> Conferences { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Book> Books { get; set; }

        #endregion // Class Properties

        #region Class Constructors

        public EntityContext(string connectionString)
            : base(connectionString)
        {
            Configuration.LazyLoadingEnabled = true;
        }

        #endregion // Class Constructors

        #region Class Protected Methods

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CathedraConfiguration());
            modelBuilder.Configurations.Add(new ProfessorConfiguration());
            modelBuilder.Configurations.Add(new ConferenceConfiguration());
            modelBuilder.Configurations.Add(new BookConfiguration());

            modelBuilder.Configurations.Add(new ConferenceReportConfiguration());
            modelBuilder.Configurations.Add(new FullNameConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        #endregion // Class Protected Methods
    }
}