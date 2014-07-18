using System;
using System.Linq.Expressions;
using MilitaryFaculty.Data;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Domain.ImprovementSuggestion;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Reporting.Data.DataProviders
{
    public class ImprovementSuggestionsDataProvider : DataProvider<ImprovementSuggestion>
    {
        public ImprovementSuggestionsDataProvider(IRepository<ImprovementSuggestion> repository)
            : base(repository)
        {
        }

        public ImprovementSuggestionsDataProvider(IRepository<ImprovementSuggestion> repository,
            Expression<Func<ImprovementSuggestion, bool>> modificator)
            : base(repository, modificator)
        {
        }

        /// <summary>
        ///     Количество принятих рационализаторских предложений
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ImprovSuggCount")]
        public double ImprovementSuggestionsCount()
        {
            return CountOf(s => s.SuggestionState == SuggestionState.Accepted);
        }
    }
}