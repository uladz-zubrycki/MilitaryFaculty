using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Data
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
