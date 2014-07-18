using System;
using System.Linq.Expressions;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Reporting.Data.DataProviders
{
    public class ParticipationsDataProvider : DataProvider<Participation>
    {
        public ParticipationsDataProvider(IRepository<Participation> repository)
            : base(repository)
        {
        }

        public ParticipationsDataProvider(IRepository<Participation> repository,
            Expression<Func<Participation, bool>> modificator)
            : base(repository, modificator)
        {
        }

        /// <summary>
        ///     Количество научных работников высшей квалификации из числа ППС,
        ///     участвующих в работе экспертных советов ВАК Беларуси
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("HacHqProfsCount")]
        public double HacHqProfsCount() //Higher Attestation Commission
        {
            return CountOf(part => part.PlaceType == ParticipationPlace.HigherAttestationCommission);
        }

        /// <summary>
        ///     Количество научных работников высшей квалификации из числа ППС,
        ///     участвующих в работе специализированных советов по защите диссертаций
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("DodHqProfsCount")]
        public double DodHqProfsCount() //Defence of dissertation counsil
        {
            return CountOf(part => part.PlaceType == ParticipationPlace.DefenceOfDissertationCounsil);
        }

        /// <summary>
        ///     Количество научных работников высшей квалификации из числа ППС,
        ///     участвующих в работе научных советов вуза
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("RcHqProfsCount")]
        public double RcHqProfsCount() //research counsil
        {
            return CountOf(part => part.PlaceType == ParticipationPlace.ResearchCounsil);
        }

        /// <summary>
        ///     Количество научных работников высшей квалификации из числа ППС,
        ///     участвующих в работе редакционных коллегий научных изданий
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("EbspHqProfsCount")]
        public double EbspHqProfsCount() //editorial boards of scientific publications
        {
            return CountOf(part => part.PlaceType == ParticipationPlace.EditorialBoardsOfScientificPublications);
        }
    }
}