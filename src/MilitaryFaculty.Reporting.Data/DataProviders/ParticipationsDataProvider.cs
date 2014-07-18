using System;
using System.Linq.Expressions;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Reporting.Data.DataProviders
{
    public class ParticipationsDataProvider : DataProvider<Participation>
    {
        public ParticipationsDataProvider(IRepository<Participation> repository)
            : base(repository)
        {
        }

        public ParticipationsDataProvider(IRepository<Participation> repository,
            Expression<Func<Participation, bool>> modificator)
            : base(repository, modificator)
        {
        }
    }
}