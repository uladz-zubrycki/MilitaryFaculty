using System;
using System.Collections.Generic;
using System.Linq;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Data
{
    public class ConferenceRepository : BaseRepository<Conference>, IConferenceRepository
    {
        #region Class Constructors

        public ConferenceRepository(EntityContext context) 
            : base(context)
        {
            // Empty
        }

        #endregion // Class Constructors

        #region Class Public Methods

        public int Count(Func<Conference, bool> predicate)
        {
            return DbSet.Count(predicate);
        }

        #endregion // Class Public Methods
    }
}
