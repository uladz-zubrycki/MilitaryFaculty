using System;
using System.Linq.Expressions;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Reporting.Data.DataProviders
{
    public class ScientificRequestsDataProvider : DataProvider<ScientificRequest>
    {
        public ScientificRequestsDataProvider(IRepository<ScientificRequest> repository)
            : base(repository)
        {
        }

        public ScientificRequestsDataProvider(IRepository<ScientificRequest> repository,
            Expression<Func<ScientificRequest, bool>> modificator)
            : base(repository, modificator)
        {
        }

        /// <summary>
        ///     Количество заявок на изобретение
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("InnCount")]
        public double InnovationsCount()
        {
            return CountOf(r => r.ScientificType == ScientificRequestType.Invention);
        }

        /// <summary>
        ///     Количество заявок на полезную модель
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("UsModCount")]
        public double UsefulModelsCount()
        {
            return CountOf(r => r.ScientificType == ScientificRequestType.UtilityModel);
        }

        /// <summary>
        ///     Количество положительных ответов на заявки на изобретение
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("PosInnCount")]
        public double PositiveInnovationsCount()
        {
            return CountOf(r => r.ScientificType == ScientificRequestType.Invention
                                && r.ScientificResponce == ScientificRequestResponce.Positive);
        }

        /// <summary>
        ///     Количество положительных ответов на завку на полезную модель
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("PosUsModCount")]
        public double PositiveUsefulModelsCount()
        {
            return CountOf(r => r.ScientificType == ScientificRequestType.UtilityModel
                                && r.ScientificResponce == ScientificRequestResponce.Positive);
        }
    }
}