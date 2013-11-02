using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Data
{
    public class PublicationRepository : BaseRepository<Publication>, IPublicationRepository
    {
        public PublicationRepository(EntityContext context) 
            : base(context)
        {
            // Empty
        }
    }
}
