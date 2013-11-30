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

        #endregion // Class Public Methods
    }
}
