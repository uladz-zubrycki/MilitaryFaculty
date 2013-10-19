using System.Collections.Generic;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Data.Contract
{
    public interface ICathedraRepository : IRepository<Cathedra>
    {
        ICollection<Cathedra> All();
    }
}
