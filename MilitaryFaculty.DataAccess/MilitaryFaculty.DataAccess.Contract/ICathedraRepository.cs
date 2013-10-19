using System.Collections.Generic;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.DataAccess.Contract
{
    public interface ICathedraRepository : IRepository<Cathedra>
    {
        ICollection<Cathedra> All();
    }
}
