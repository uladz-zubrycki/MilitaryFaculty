using MilitaryFaculty.DataAccess.Contract;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.DataAccess
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(EntityContext context) 
            : base(context)
        {
            // Empty
        }
    }
}
