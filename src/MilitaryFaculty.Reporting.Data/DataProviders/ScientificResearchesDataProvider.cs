using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Reporting.Data.DataProviders
{
    public class ScientificResearchesDataProvider : DataProvider<ScientificResearch>
    {
        public ScientificResearchesDataProvider(IRepository<ScientificResearch> repository)
            : base(repository)
        {
        }

        public ScientificResearchesDataProvider(IRepository<ScientificResearch> repository,
            Expression<Func<ScientificResearch, bool>> modificator)
            : base(repository, modificator)
        {
        }

        /// <summary>
        ///     Количество выполненных НИР
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("SrwCount")]
        public double ScientificResearchWorksCount()
        {
            return CountOf(w => true);
        }

        /// <summary>
        ///     Количество проведенных военно-научных сопровождений
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("MssCount")]
        public double MilitaryScientificSupportsCount()
        {
            return CountOf(w => w.State == MilitaryScientificSupportState.Completed);
        }

        /// <summary>
        ///     Количество ППС, участвующих во всех НИР
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("SrwProfsCount")]
        public double ScientificResearchWorkProfessorsCount()
        {
            //TODO: вероятная оптимизация - вынести модификатор на уровень репозитория?
            var professorMemo = new HashSet<Guid>();
            return CountOf(sr =>
            {
                if (!professorMemo.Contains(sr.Author.Id))
                {
                    professorMemo.Add(sr.Author.Id);
                    return true;
                }
                return false;
            });
        }
    }
}