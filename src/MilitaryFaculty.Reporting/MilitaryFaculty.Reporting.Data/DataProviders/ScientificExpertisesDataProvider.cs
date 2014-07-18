using System;
using System.Collections.Generic;
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
            //TODO: вероятная оптимизация - вынести модификатор на уровень репозитория?
            var professorMemo = new HashSet<Guid>();
            return CountOf(se =>
            {
                if (!professorMemo.Contains(se.Author.Id))
                {
                    professorMemo.Add(se.Author.Id);
                    return true;
                }
                return false;
            });
        }
    }
}