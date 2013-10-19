using System.Collections.Generic;
using System.Linq;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Data
{
    public class CathedraRepository : BaseRepository<Cathedra>, ICathedraRepository
    {
        #region Class Constructors

        public CathedraRepository(EntityContext context) 
            : base(context)
        {
            // Empty
        }

        #endregion // Class Constructors

        #region Class Public Methods

        public ICollection<Cathedra> All()
        {
            return DbSet.ToList();
        }  

        #endregion // Class Public Methods
    }
}
