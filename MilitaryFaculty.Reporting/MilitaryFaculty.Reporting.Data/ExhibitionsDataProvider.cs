using System;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Reporting.Data
{
    public class ExhibitionsDataProvider : IDataProvider
    {
        private readonly IRepository<Exhibition> _exhibitionRepository;

        public ExhibitionsDataProvider(IRepository<Exhibition> exhibitionRepository)
        {
            if (exhibitionRepository == null)
            {
                throw new ArgumentNullException("exhibitionRepository");
            }

            _exhibitionRepository = exhibitionRepository;
        }

        /// <summary>
        ///     На вузовских выставках и конкурсах: Диплом 1 степени
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("UnFirstDiplCount")]
        public double UniversityFirstDiplomasCount()
        {
            //TODO: Уровень выставки
            return _exhibitionRepository.CountOf(e => e.AwardType == AwardType.FirstDegree);
        }

        /// <summary>
        ///     На вузовских выставках и конкурсах: Диплом 2 степен
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("UnSecondDiplCount")]
        public double UniversitySecondDiplomasCount()
        {
            return _exhibitionRepository.CountOf(e => e.AwardType == AwardType.SecondDegree);
        }

        /// <summary>
        ///     На вузовских выставках и конкурсах: Диплом 3 степени
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("UnThirdDiplCount")]
        public double UniversityThirdDiplomasCount()
        {
            return _exhibitionRepository.CountOf(e => e.AwardType == AwardType.ThirdDegree);
        }

        /// <summary>
        ///     На вузовских выставках и конкурсах: Диплом (грамота)
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("UnLettersCommendationCount")]
        public double UniversityLettersOfCommendationCount()
        {
            //TODO: Грамота
            return _exhibitionRepository.CountOf(e => e.AwardType == AwardType.WithoutDegree);
        }

        /// <summary>
        ///     На республиканских выставках и конкурсах: Диплом 1 степени
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ReFirstDiplCount")]
        public double RepublicanFirstDiplomasCount()
        {
            return _exhibitionRepository.CountOf(e => e.AwardType == AwardType.FirstDegree);
        }

        /// <summary>
        ///     На республиканских выставках и конкурсах: Диплом 2 степени
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ReSecondDiplCount")]
        public double RepublicanSecondDiplomasCount()
        {
            return _exhibitionRepository.CountOf(e => e.AwardType == AwardType.SecondDegree);
        }

        /// <summary>
        ///     На республиканских выставках и конкурсах: Диплом 3 степени
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ReThirdDiplCount")]
        public double RepublicanThirdDiplomasCount()
        {
            return _exhibitionRepository.CountOf(e => e.AwardType == AwardType.ThirdDegree);
        }

        /// <summary>
        ///     На республиканских выставках и конкурсах: Диплом (грамота)
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ReLettersCommendationCount")]
        public double RepublicanLettersOfCommendationCount()
        {
            return _exhibitionRepository.CountOf(e => e.AwardType == AwardType.WithoutDegree);
        }

        /// <summary>
        ///     На международных выставках и конкурсах: Диплом 1 степени
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("InFirstDiplCount")]
        public double InternationalFirstDiplomasCount()
        {
            return _exhibitionRepository.CountOf(e => e.AwardType == AwardType.FirstDegree);
        }

        /// <summary>
        ///     На международных выставках и конкурсах: Диплом 2 степени
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("InSecondDiplCount")]
        public double InternationalSecondDiplomasCount()
        {
            return _exhibitionRepository.CountOf(e => e.AwardType == AwardType.SecondDegree);
        }

        /// <summary>
        ///     На международных выставках и конкурсах: Диплом 3 степени
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("InThirdDiplCount")]
        public double InternationalThirdDiplomasCount()
        {
            return _exhibitionRepository.CountOf(e => e.AwardType == AwardType.ThirdDegree);
        }

        /// <summary>
        ///     На международных выставках и конкурсах: Диплом (грамота)
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("InLettersCommendationCount")]
        public double InternationalLettersOfCommendationCount()
        {
            return _exhibitionRepository.CountOf(e => e.AwardType == AwardType.WithoutDegree);
        }
    }
}