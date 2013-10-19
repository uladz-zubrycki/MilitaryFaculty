using System;
using System.Collections.Generic;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.DataAccess
{
    public class ProfessorRepository
    {
        #region Class Methods
        public void Create(Professor professor)
        {
            #region Pre-Conditions
            if(professor == null) { throw new ArgumentNullException(); }
            #endregion // Pre-Conditions
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid id, Professor updatedProfessor)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Professor> Select(Func<Professor, bool> predicate)
        {
            throw new NotImplementedException();
        }
        #endregion //ClassMethods
    }
}
