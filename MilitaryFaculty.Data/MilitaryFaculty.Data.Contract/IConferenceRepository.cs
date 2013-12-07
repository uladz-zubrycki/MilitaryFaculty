using System;
using System.Collections.Generic;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Data.Contract
{
    public interface IConferenceRepository : IRepository<Conference>
    {
        ICollection<Conference> All();
        int Count(Func<Conference, bool> predicate);
    }
}
