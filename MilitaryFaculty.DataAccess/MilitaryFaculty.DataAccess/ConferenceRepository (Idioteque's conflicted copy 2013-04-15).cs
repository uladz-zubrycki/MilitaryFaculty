using System;
using System.Collections.Generic;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.DataAccess
{
    public class ConferenceRepository
    {
        #region Class Methods
        public void Create(Conference conference)
        {
            #region Pre-Conditions
            if (conference == null) { throw new ArgumentNullException(); }
            #endregion // Pre-Conditions

        }

        public void Delete(Guid id)
        {
           throw new NotImplementedException();
        }

        public void Update(Guid id, Conference updatedConference)
        {
            #region Pre-Conditions
            if (updatedConference == null) { throw new ArgumentNullException(); }
            #endregion // Pre-Conditions
        }

        public IEnumerable<Conference> Select(Func<Conference, bool> predicate)
        {
            throw new NotImplementedException();
        }
        #endregion //ClassMethods
    }
}
