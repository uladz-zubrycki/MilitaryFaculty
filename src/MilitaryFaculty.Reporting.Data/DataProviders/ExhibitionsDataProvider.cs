using System;
using System.Linq.Expressions;
using MilitaryFaculty.Data;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Reporting.Data.DataProviders
{
    public class ExhibitionsDataProvider : DataProvider<Exhibition>
    {
        public ExhibitionsDataProvider(IRepository<Exhibition> repository)
            : base(repository)
        {
        }

        public ExhibitionsDataProvider(IRepository<Exhibition> repository,
                                       Expression<Func<Exhibition, bool>> modificator)
            : base(repository, modificator)
        {
        }

        /// <summary>
        ///     На вузовских выставках и конкурсах: Диплом 1 степени
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("UnFirstDiplCount")]
        public double UniversityFirstDiplomasCount()
        {
            return CountOf(e => e.AwardType == AwardType.FirstDegree);
        }

        /// <summary>
        ///     На вузовских выставках и конкурсах: Диплом 2 степен
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("UnSecondDiplCount")]
        public double UniversitySecondDiplomasCount()
        {
            return CountOf(e => e.AwardType == AwardType.SecondDegree);
        }

        /// <summary>
        ///     На вузовских выставках и конкурсах: Диплом 3 степени
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("UnThirdDiplCount")]
        public double UniversityThirdDiplomasCount()
        {
            return CountOf(e => e.AwardType == AwardType.ThirdDegree);
        }

        /// <summary>
        ///     На вузовских выставках и конкурсах: Диплом (грамота)
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("UnLettersCommendationCount")]
        public double UniversityLettersOfCommendationCount()
        {
            return CountOf(e => e.AwardType == AwardType.WithoutDegree);
        }

        /// <summary>
        ///     На республиканских выставках и конкурсах: Диплом 1 степени
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ReFirstDiplCount")]
        public double RepublicanFirstDiplomasCount()
        {
            return CountOf(e => e.AwardType == AwardType.FirstDegree);
        }

        /// <summary>
        ///     На республиканских выставках и конкурсах: Диплом 2 степени
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ReSecondDiplCount")]
        public double RepublicanSecondDiplomasCount()
        {
            return CountOf(e => e.AwardType == AwardType.SecondDegree);
        }

        /// <summary>
        ///     На республиканских выставках и конкурсах: Диплом 3 степени
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ReThirdDiplCount")]
        public double RepublicanThirdDiplomasCount()
        {
            return CountOf(e => e.AwardType == AwardType.ThirdDegree);
        }

        /// <summary>
        ///     На республиканских выставках и конкурсах: Диплом (грамота)
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ReLettersCommendationCount")]
        public double RepublicanLettersOfCommendationCount()
        {
            return CountOf(e => e.AwardType == AwardType.WithoutDegree);
        }

        /// <summary>
        ///     На международных выставках и конкурсах: Диплом 1 степени
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("InFirstDiplCount")]
        public double InternationalFirstDiplomasCount()
        {
            return CountOf(e => e.AwardType == AwardType.FirstDegree);
        }

        /// <summary>
        ///     На международных выставках и конкурсах: Диплом 2 степени
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("InSecondDiplCount")]
        public double InternationalSecondDiplomasCount()
        {
            return CountOf(e => e.AwardType == AwardType.SecondDegree);
        }

        /// <summary>
        ///     На международных выставках и конкурсах: Диплом 3 степени
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("InThirdDiplCount")]
        public double InternationalThirdDiplomasCount()
        {
            return CountOf(e => e.AwardType == AwardType.ThirdDegree);
        }

        /// <summary>
        ///     На международных выставках и конкурсах: Диплом (грамота)
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("InLettersCommendationCount")]
        public double InternationalLettersOfCommendationCount()
        {
            return CountOf(e => e.AwardType == AwardType.WithoutDegree);
        }
    }
}