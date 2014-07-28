using System.Linq;
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

        public override void SetFacultyModificator(TimeInterval interval)
        {
            QueryModificator = research =>
                research.CreatedAt >= interval.From
                && research.CreatedAt <= interval.To;
        }

        public override void SetCathedraModificator(Cathedra cathedra, TimeInterval interval)
        {
            QueryModificator = research =>
                research.Author.Cathedra.Id == cathedra.Id
                && research.CreatedAt >= interval.From
                && research.CreatedAt <= interval.To;
        }

        public override void SetPersonModificator(Person person, TimeInterval interval)
        {
            QueryModificator = research =>
                research.Author.Id == person.Id
                && research.CreatedAt >= interval.From
                && research.CreatedAt <= interval.To;
        }

        /// <summary>
        ///     Количество выполненных НИР
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("SrwCount")]
        public double ScientificResearchWorksCount()
        {
            return QueryableCollection.Count();
        }

        /// <summary>
        ///     Количество проведенных военно-научных сопровождений
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("MssCount")]
        public double MilitaryScientificSupportsCount()
        {
            return CountOf(w => w.MaintainState == ResearchMaintainState.Performed);
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