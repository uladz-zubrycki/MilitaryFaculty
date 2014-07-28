using MilitaryFaculty.Data;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Reporting.Data.DataProviders
{
    public class CouncilParticipationDataProvider : DataProvider<CouncilParticipation>
    {
        public CouncilParticipationDataProvider(IRepository<CouncilParticipation> repository) : base(repository)
        {
        }

        public override void SetFacultyModificator(TimeInterval interval)
        {
            QueryModificator = cp =>
                (cp.StartDate >= interval.From && cp.StartDate <= interval.To)
                || (cp.EndDate >= interval.From && cp.EndDate <= interval.To)
                || (cp.StartDate <= interval.From && cp.EndDate >= interval.To);
        }

        public override void SetCathedraModificator(Cathedra cathedra, TimeInterval interval)
        {
            QueryModificator = cp =>
                cp.Participant.Cathedra.Id == cathedra.Id
                && (cp.StartDate >= interval.From && cp.StartDate <= interval.To)
                || (cp.EndDate >= interval.From && cp.EndDate <= interval.To)
                || (cp.StartDate <= interval.From && cp.EndDate >= interval.To);
        }

        public override void SetPersonModificator(Person person, TimeInterval interval)
        {
            QueryModificator = cp =>
                cp.Participant.Id == person.Id
                && (cp.StartDate >= interval.From && cp.StartDate <= interval.To)
                || (cp.EndDate >= interval.From && cp.EndDate <= interval.To)
                || (cp.StartDate <= interval.From && cp.EndDate >= interval.To);
        }

        /// <summary>
        ///     Количество научных работников высшей квалификации из числа ППС,
        ///     участвующих в работе экспертных советов ВАК Беларуси
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("HacHqProfsCount")]
        public double HacHqProfsCount()
        {
            return CountOf(c => c.Type == CouncilType.SupremeCertificationCommissionCouncil);
        }

        /// <summary>
        ///     Количество научных работников высшей квалификации из числа ППС,
        ///     участвующих в работе специализированных советов по защите диссертаций
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("DodHqProfsCount")]
        public double DodHqProfsCount()
        {
            return CountOf(c => c.Type == CouncilType.DefenceOfDissertationsCouncil);
        }

        /// <summary>
        ///     Количество научных работников высшей квалификации из числа ППС,
        ///     участвующих в работе научных советов вуза
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("RcHqProfsCount")]
        public double RcHqProfsCount()
        {
            return CountOf(c => c.Type == CouncilType.ResearchCounsil);
        }

        /// <summary>
        ///     Количество научных работников высшей квалификации из числа ППС,
        ///     участвующих в работе редакционных коллегий научных изданий
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("EbspHqProfsCount")]
        public double EbspHqProfsCount()
        {
            return CountOf(c => c.Type == CouncilType.EditorialBoardCouncil);
        }
    }
}