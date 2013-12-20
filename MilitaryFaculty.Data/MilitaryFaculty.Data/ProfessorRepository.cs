using System;
using System.Collections.Generic;
using System.Linq;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Data
{
    public class ProfessorRepository : BaseRepository<Professor>, IProfessorRepository
    {
        #region Class Constructors

        public ProfessorRepository(EntityContext context) 
            : base(context)
        {
            // Empty
        }

        #endregion // Class Constructors

        #region Class Public Methods

        public int Count(Func<Professor, bool> predicate)
        {
            return predicate == null ? DbSet.Count() : DbSet.Count(predicate);
        }

        #endregion // Class Public Methods
    }
}
