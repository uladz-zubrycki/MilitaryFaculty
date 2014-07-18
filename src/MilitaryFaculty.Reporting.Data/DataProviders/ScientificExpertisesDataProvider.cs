using System;
using System.Linq;
using System.Linq.Expressions;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Reporting.Data.DataProviders
{
    public class ScientificExpertisesDataProvider : DataProvider<ScientificExpertise>
    {
        public ScientificExpertisesDataProvider(IRepository<ScientificExpertise> professorRepository)
            : base(professorRepository)
        {
        }

        public ScientificExpertisesDataProvider(IRepository<ScientificExpertise> repository,
            Expression<Func<ScientificExpertise, bool>> modificator)
            : base(repository, modificator)
        {
        }

        /// <summary>
        ///     Количество ППС, участвующих в работе по проведению научной экспертизы
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("SeProfsCount")]
        public double ScientificExperticeProfessorsCount()
        {
            return QueryableCollection.Select(se => se.Author.Id)
                                      .Distinct()
                                      .Count();
        }
    }
}