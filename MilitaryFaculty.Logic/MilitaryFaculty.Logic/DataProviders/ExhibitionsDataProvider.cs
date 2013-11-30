using System;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Logic.DataProviders
{
    public class ExhibitionsDataProvider : IDataProvider
    {
        #region Class Fields

        private readonly IExhibitionRepository exhibitionRepository;
        
        #endregion // Class Fields

        #region Class Constructors

        public ExhibitionsDataProvider(IExhibitionRepository exhibitionRepository)
        {
            if (exhibitionRepository == null)
            {
                throw new ArgumentNullException("exhibitionRepository");
            }

            this.exhibitionRepository = exhibitionRepository;
        }

        #endregion // Class Constructors

        /// <summary>
        /// На вузовских выставках и конкурсах: Диплом 1 степени
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("UnFirstDiplCount")]
        public double UniversityFirstDiplomasCount()
        {
            return 5;
        }

        /// <summary>
        /// На вузовских выставках и конкурсах: Диплом 2 степен
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("UnSecondDiplCount")]
        public double UniversitySecondDiplomasCount()
        {
            return 1;
        }

        /// <summary>
        /// На вузовских выставках и конкурсах: Диплом 3 степени
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("UnThirdDiplCount")]
        public double UniversityThirdDiplomasCount()
        {
            return 2;
        }

        /// <summary>
        /// На вузовских выставках и конкурсах: Диплом (грамота)
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("UnLettersCommendationCount")]
        public double UniversityLettersOfCommendationCount()
        {
            return 3;
        }

        /// <summary>
        /// На республиканских выставках и конкурсах: Диплом 1 степени
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ReFirstDiplCount")]
        public double RepublicanFirstDiplomasCount()
        {
            return 5;
        }

        /// <summary>
        /// На республиканских выставках и конкурсах: Диплом 2 степени
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ReSecondDiplCount")]
        public double RepublicanSecondDiplomasCount()
        {
            return 1;
        }

        /// <summary>
        /// На республиканских выставках и конкурсах: Диплом 3 степени
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ReThirdDiplCount")]
        public double RepublicanThirdDiplomasCount()
        {
            return 2;
        }

        /// <summary>
        /// На республиканских выставках и конкурсах: Диплом (грамота)
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ReLettersCommendationCount")]
        public double RepublicanLettersOfCommendationCount()
        {
            return 3;
        }

        /// <summary>
        /// На международных выставках и конкурсах: Диплом 1 степени
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("InFirstDiplCount")]
        public double InternationalFirstDiplomasCount()
        {
            return 5;
        }

        /// <summary>
        /// На международных выставках и конкурсах: Диплом 2 степени
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("InSecondDiplCount")]
        public double InternationalSecondDiplomasCount()
        {
            return 1;
        }

        /// <summary>
        /// На международных выставках и конкурсах: Диплом 3 степени
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("InThirdDiplCount")]
        public double InternationalThirdDiplomasCount()
        {
            return 2;
        }

        /// <summary>
        /// На международных выставках и конкурсах: Диплом (грамота)
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("InLettersCommendationCount")]
        public double InternationalLettersOfCommendationCount()
        {
            return 3;
        }
    }
}
