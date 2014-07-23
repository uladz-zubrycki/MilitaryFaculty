using System;
using System.Linq.Expressions;
using MilitaryFaculty.Common;
using MilitaryFaculty.Data;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Reporting.Data.DataProviders
{
    public class InventiveApplicationsDataProvider : DataProvider<InventiveApplication>
    {
        public InventiveApplicationsDataProvider(IRepository<InventiveApplication> repository)
            : base(repository)
        {
        }

        public InventiveApplicationsDataProvider(IRepository<InventiveApplication> repository,
            Expression<Func<InventiveApplication, bool>> modificator)
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
            throw new NotImplementedException();
            //return CountOf(r => r.ScientificType == ScientificRequestType.Invention);
        }

        /// <summary>
        ///     Количество заявок на полезную модель
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("UsModCount")]
        public double UsefulModelsCount()
        {
            throw new NotImplementedException();
            //return CountOf(r => r.ScientificType == ScientificRequestType.UtilityModel);
        }

        /// <summary>
        ///     Количество положительных ответов на заявки на изобретение
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("PosInnCount")]
        public double PositiveInnovationsCount()
        {
            throw new NotImplementedException();

            //return CountOf(r => r.ScientificType == ScientificRequestType.Invention
            //                    && r.ScientificResponce == ScientificRequestResponce.Positive);
        }

        /// <summary>
        ///     Количество положительных ответов на завку на полезную модель
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("PosUsModCount")]
        public double PositiveUsefulModelsCount()
        {
            throw new NotImplementedException();

            //return CountOf(r => r.ScientificType == ScientificRequestType.UtilityModel
            //                    && r.ScientificResponce == ScientificRequestResponce.Positive);
        }
    }
}