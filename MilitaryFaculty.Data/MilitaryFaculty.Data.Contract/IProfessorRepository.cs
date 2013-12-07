using System;
using System.Collections.Generic;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Data.Contract
{
    public interface IProfessorRepository : IRepository<Professor>
    {
        IEnumerable<Professor> All();
        int Count(Func<Professor,bool> predicate);
    }
}