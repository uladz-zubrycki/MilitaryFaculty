using System.Collections.Generic;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Data.Contract
{
    public interface IPublicationRepository : IRepository<Publication>
    {
        IEnumerable<Publication> All();
    }
}
