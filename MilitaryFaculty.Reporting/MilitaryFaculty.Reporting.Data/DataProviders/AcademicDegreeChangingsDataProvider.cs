using System;
using System.Linq.Expressions;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Reporting.Data.DataProviders
{
    public class AcademicDegreeChangingsDataProvider : DataProvider<AcademicDegreeChanging>
    {
        public AcademicDegreeChangingsDataProvider(IRepository<AcademicDegreeChanging> repository)
            : base(repository)
        {
        }

        public AcademicDegreeChangingsDataProvider(IRepository<AcademicDegreeChanging> repository,
            Expression<Func<AcademicDegreeChanging, bool>> modificator)
            : base(repository, modificator)
        {
        }
    }
}