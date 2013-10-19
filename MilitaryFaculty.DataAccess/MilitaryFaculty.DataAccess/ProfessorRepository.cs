using MilitaryFaculty.DataAccess.Contract;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.DataAccess
{
    public class ProfessorRepository : BaseRepository<Professor>, IProfessorRepository
    {
        public ProfessorRepository(EntityContext context) 
            : base(context)
        {
            // Empty
        }
    }
}
