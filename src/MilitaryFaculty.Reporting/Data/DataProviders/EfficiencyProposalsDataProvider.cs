using System.Linq;
using MilitaryFaculty.Data;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Reporting.Data.DataProviders
{
    public class ImprovementSuggestionsDataProvider : DataProvider<EfficiencyProposal>
    {
        public ImprovementSuggestionsDataProvider(IRepository<EfficiencyProposal> repository)
            : base(repository)
        {
        }

        public override void SetFacultyModificator(TimeInterval interval)
        {
            QueryModificator = efficiencyProposal =>
                efficiencyProposal.CreatedAt >= interval.From
                && efficiencyProposal.CreatedAt <= interval.To;
        }

        public override void SetCathedraModificator(Cathedra cathedra, TimeInterval interval)
        {
            QueryModificator = efficiencyProposal =>
                efficiencyProposal.Author.Cathedra.Id == cathedra.Id
                && efficiencyProposal.CreatedAt >= interval.From
                && efficiencyProposal.CreatedAt <= interval.To;
        }

        public override void SetProfessorModificator(Professor professor, TimeInterval interval)
        {
            QueryModificator = efficiencyProposal =>
                efficiencyProposal.Author.Id == professor.Id
                && efficiencyProposal.CreatedAt >= interval.From
                && efficiencyProposal.CreatedAt <= interval.To;
        }

        /// <summary>
        ///     Количество принятих рационализаторских предложений
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ImprovSuggCount")]
        public double ImprovementSuggestionsCount()
        {
            return QueryableCollection.Count();
        }
    }
}