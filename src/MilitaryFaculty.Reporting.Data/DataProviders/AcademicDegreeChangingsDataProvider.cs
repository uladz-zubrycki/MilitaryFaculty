using System;
using System.Linq.Expressions;
using MilitaryFaculty.Data;
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

        /// <summary>
        ///     Количество ППС, которым присвоена ученое звание профессора (в проверяемый период)
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("DocAssignCount")]
        public double DoctorsAssignedCount()
        {
            return CountOf(adc => adc.ResultedDegree == AcademicDegree.Professor);
        }

        /// <summary>
        ///     Количество ППС, которым присвоена ученое звание доцента (в проверяемый период)
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("DocentsAssignCount")]
        public double DocentsAssignedCount()
        {
            return CountOf(adc => adc.ResultedDegree == AcademicDegree.Docent);
        }
    }
}