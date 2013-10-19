using MilitaryFaculty.DataAccess.Contract;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.DataAccess
{
    public class ConferenceRepository : BaseRepository<Conference>, IConferenceRepository
    {
        public ConferenceRepository(EntityContext context) 
            : base(context)
        {
            // Empty
        }
    }
}
