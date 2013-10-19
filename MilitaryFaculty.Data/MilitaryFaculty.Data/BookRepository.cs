using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Data
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
