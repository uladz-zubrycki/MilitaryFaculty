using System;
using System.Collections.Generic;
using System.Linq;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Data
{
    public class PublicationRepository : BaseRepository<Publication>, IPublicationRepository
    {
        #region Class Constructors

        public PublicationRepository(EntityContext context) 
            : base(context)
        {
            // Empty
        }

        #endregion // Class Constructors

        #region Class Public Methods

        public IEnumerable<Publication> All()
        {
            return DbSet.ToList();
        }

        public int Count(Func<Publication, bool> predicate)
        {
            return DbSet.Count(predicate);
        }

        public double Sum(Func<Publication, decimal?> predicate)
        {
            return (double)DbSet.Sum(predicate);
        }

        #endregion // Class Public Methods
    }
}
