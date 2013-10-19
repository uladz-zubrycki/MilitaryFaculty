using System.Data.Entity;
using MilitaryFaculty.DataAccess.Configurations;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.DataAccess
{
    public class AppDbContext : DbContext
    {
        #region Class Properties
        private DbSet<Cathedra> cathedras;
        private DbSet<Conference> conferences;
        private DbSet<Professor> professors;
        #endregion // Class Properties

        #region Class Methods
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CathedraConfiguration());
            modelBuilder.Configurations.Add(new ConferenceConfiguration());

            base.OnModelCreating(modelBuilder);
        }
        #endregion // ClassMethods
    }
}