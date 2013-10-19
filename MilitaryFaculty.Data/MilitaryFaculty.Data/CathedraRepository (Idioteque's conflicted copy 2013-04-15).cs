using System;
using System.Collections.Generic;
using System.Data.Entity;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.DataAccess
{
    public class CathedraRepository 
    {
        #region Class Properties
        private readonly DbSet<Cathedra> dbSet;
        #endregion // Class Properties

        #region Class Constructors
        public CathedraRepository(DbContext context)
        {
            #region Pre-Conditions
            if (context == null) { throw new ArgumentNullException("context"); }
            #endregion // Pre-Conditions

            dbSet = context.Set<Cathedra>();
        }
        #endregion // Class Constructors

        #region Class Methods
        public void Create(Cathedra cathedra)
        {
            #region Pre-Conditions
            if (cathedra == null) { throw new ArgumentNullException(); }
            #endregion //Pre-Conditions

            dbSet.Add(cathedra);
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid id, Cathedra updatedCathedra)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cathedra> Select(Func<Cathedra, bool> predicate)
        {
            throw new NotImplementedException();
        }
        #endregion // ClassMethods
    }
}
