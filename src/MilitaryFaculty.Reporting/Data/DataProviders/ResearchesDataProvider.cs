using System;
using System.Linq;
using System.Linq.Expressions;
using MilitaryFaculty.Data;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Reporting.Data.DataProviders
{
    public class ResearchesDataProvider : DataProvider<Research>
    {
        public ResearchesDataProvider(IRepository<Research> repository)
            : base(repository)
        {
        }

        public ResearchesDataProvider(IRepository<Research> repository,
            Expression<Func<Research, bool>> modificator)
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
            throw new NotImplementedException();
            //return CountOf(w => w.State == MilitaryScientificSupportState.Completed);
        }

        /// <summary>
        ///     Количество ППС, участвующих во всех НИР
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("SrwProfsCount")]
        public double ScientificResearchWorkProfessorsCount()
        {
            return QueryableCollection.Select(sr => sr.Author.Id)
                                      .Distinct()
                                      .Count();
        }
    }
}