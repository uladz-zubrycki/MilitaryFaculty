using System;
using System.Collections.Generic;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Data.Contract
{
    public interface IConferenceRepository : IRepository<Conference>
    {
        int Count(Func<Conference, bool> predicate);
    }
}
