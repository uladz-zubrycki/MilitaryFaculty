using System;
using System.Collections.Generic;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Data.Contract
{
    public interface IPublicationRepository : IRepository<Publication>
    {
        IEnumerable<Publication> All();
        int Count(Func<Publication, bool> predicate);
        double Sum(Func<Publication, decimal?> predicate);
    }
}
