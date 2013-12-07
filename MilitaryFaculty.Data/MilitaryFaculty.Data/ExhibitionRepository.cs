using System;
using System.Collections.Generic;
using System.Linq;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Data
{
    public class ExhibitionRepository : BaseRepository<Exhibition>, IExhibitionRepository
    {
        #region Class Constructors

        public ExhibitionRepository(EntityContext context) 
            : base(context)
        {
            // Empty
        }

        #endregion // Class Constructors

        #region Class Public Methods

        public ICollection<Exhibition> All()
        {
            return DbSet.ToList();
        }

        public int Count(Func<Exhibition, bool> predicate)
        {
            return DbSet.Count(predicate);
        }

        #endregion // Class Public Methods
    }
}
