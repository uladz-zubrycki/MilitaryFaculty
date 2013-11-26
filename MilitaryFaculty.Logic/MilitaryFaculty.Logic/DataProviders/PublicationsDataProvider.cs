using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Logic.DataProviders
{
    public class PublicationsDataProvider : IDataProvider
    {
        #region Class Public Argument Methods

        [FormulaArgument("TotalBooksPc")]
        public double TotalBooksPagesCount()
        {
            return 5;
        }

        [FormulaArgument("TotalTutorialsPc")]
        public double TotalTutorialsPagesCount()
        {
            return 1;
        }

        [FormulaArgument("MonographyPc")]
        public double MonographyPagesCount()
        {
            return 4;
        }

        [FormulaArgument("CritArticlesCount")]
        public double CriticizeArticlesCount()
        {
            return 5;
        }

        [FormulaArgument("UnCritArticlesCount")]
        public double UnCriticizeArticlesCount()
        {
            return 1;
        }

        #endregion // Class Public Argument Methods
    }
}
