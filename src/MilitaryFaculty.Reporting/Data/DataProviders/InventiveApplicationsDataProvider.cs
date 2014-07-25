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

        public override void SetFacultyModificator(TimeInterval interval)
        {
            QueryModificator = inventiveApplication =>
                inventiveApplication.CreatedAt >= interval.From
                && inventiveApplication.CreatedAt <= interval.To;
        }

        public override void SetCathedraModificator(Cathedra cathedra, TimeInterval interval)
        {
            QueryModificator = inventiveApplication =>
                inventiveApplication.Author.Cathedra.Id == cathedra.Id
                && inventiveApplication.CreatedAt >= interval.From
                && inventiveApplication.CreatedAt <= interval.To;
        }

        public override void SetProfessorModificator(Professor professor, TimeInterval interval)
        {
            QueryModificator = inventiveApplication =>
                inventiveApplication.Author.Id == professor.Id
                && inventiveApplication.CreatedAt >= interval.From
                && inventiveApplication.CreatedAt <= interval.To;
        }

        /// <summary>
        ///     Количество заявок на изобретение
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("InnCount")]
        public double InnovationsCount()
        {
            return CountOf(ia => ia.Type == InventiveApplicationType.Invention);
        }

        /// <summary>
        ///     Количество заявок на полезную модель
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("UsModCount")]
        public double UsefulModelsCount()
        {
            return CountOf(ia => ia.Type == InventiveApplicationType.UtilityModel);
        }

        /// <summary>
        ///     Количество положительных ответов на заявки на изобретение
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("PosInnCount")]
        public double PositiveInnovationsCount()
        {
            return CountOf(ia => ia.Type == InventiveApplicationType.Invention
                                 && ia.Status == ApplicationStatus.Accepted);
        }

        /// <summary>
        ///     Количество положительных ответов на завку на полезную модель
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("PosUsModCount")]
        public double PositiveUsefulModelsCount()
        {
            return CountOf(ia => ia.Type == InventiveApplicationType.UtilityModel
                                 && ia.Status == ApplicationStatus.Accepted);
        }
    }
}