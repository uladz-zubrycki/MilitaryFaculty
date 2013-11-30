using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Data
{
    public class ExhibitionRepository : BaseRepository<Exhibition>, IExhibitionRepository
    {
        public ExhibitionRepository(EntityContext context)
            : base(context)
        {
            // Empty
        }
    }
}
