using System;
using System.Collections.Generic;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Data.Contract
{
    public interface IExhibitionRepository : IRepository<Exhibition>
    {
        ICollection<Exhibition> All();
        int Count(Func<Exhibition, bool> predicate);
    }
}
