using System.Linq;
using MilitaryFaculty.Data;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Reporting.Data.DataProviders
{
    public class ScienceRanksDataProvider : DataProvider<ScienceRank>
    {
        public ScienceRanksDataProvider(IRepository<ScienceRank> repository)
            : base(repository)
        {
        }

        public override void SetFacultyModificator(TimeInterval interval)
        {
            QueryModificator = sr =>
                sr.CreatedAt >= interval.From
                && sr.CreatedAt <= interval.To;
        }

        public override void SetCathedraModificator(Cathedra cathedra, TimeInterval interval)
        {
            QueryModificator = sr =>
                sr.Cathedra.Id == cathedra.Id
                && sr.CreatedAt >= interval.From
                && sr.CreatedAt <= interval.To;
        }

        public override void SetPersonModificator(Person person, TimeInterval interval)
        {
            QueryModificator = null;
        }

        private double MetricValuesCalculation(string sectionName)
        {
            var collection = QueryableCollection.Select(sr => sr.Metrics
                                                                .FirstOrDefault(m => m.Definition
                                                                                      .Name
                                                                                      .StartsWith(
                                                                                          sectionName))
                                                                                          )
                                                                .Select(m => m.Value);
            if (!collection.Any()) return 0;
            return collection.Sum() / (double)collection.Count();
        }

        /// <summary>
        ///     Планирующие документы разработаны в соответствии с требованиями прововых актов, мероприятия по научной работе
        ///     выполнены своевременно и имеют практический результат
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("PlanDocsOrg")]
        public double PlanningDocumentsOrganization()
        {
            return MetricValuesCalculation("Полнота разработки планирующих документов");
        }

        /// <summary>
        ///     Тематика исследований соответствует направлениям строительства и развития Вооруженный Сил
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ResTopicsOrg")]
        public double ResearchTopicsOrganization()
        {
            return MetricValuesCalculation("Уровень соответствия тематики исследований");
        }

        /// <summary>
        ///     Лица из числа руководящего и профессорско-преподавательского состава знают требования правовых актов Министерства
        ///     обороны по организации научной работы и руководствуются ими в повседневной деятельности, непосредственно принимают
        ///     участие в научной работе
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ProfsOrg")]
        public double ProfessorsOrganization()
        {
            return MetricValuesCalculation("Уровень ознакомления лиц из числа ППС с требованиями правовых актов");
        }

        /// <summary>
        ///     Уровень оргинизации международных конференций
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("IntConfOrg")]
        public double InternationalConferencesOrganization()
        {
            return MetricValuesCalculation("Уровень организации международных конференций");
        }

        /// <summary>
        ///     Уровень оргинизации республиканских конференций
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("RepConfOrg")]
        public double RepublicanConferenceOrganization()
        {
            return MetricValuesCalculation("Уровень организации республиканских конференций");
        }

        /// <summary>
        ///     Уровень оргинизации конференций военного учебного заведения
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("UnConfOrg")]
        public double UniversityConferenceOrganization()
        {
            return MetricValuesCalculation("Уровень организации вузовских конференций");
        }

        /// <summary>
        ///     Уровень оргинизации республиканских семинаров
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("RepSemOrg")]
        public double RepablicanSeminarOrganization()
        {
            return MetricValuesCalculation("Уровень организации республиканских семинаров");
        }

        /// <summary>
        ///     Уровень оргинизации семинаров военного учебного заведения
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("UnSemOrg")]
        public double UniversitySeminarOrganization()
        {
            return MetricValuesCalculation("Уровень организации вузовских семинаров");
        }

        /// <summary>
        ///     Подготовка научно-педагогических работников высшей квалификации
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ProfsOrgFull")]
        public double ProfessorsOrganizationFull()
        {
            return MetricValuesCalculation("Уровень организации подготовки научно-педагогических работников");
        }

        /// <summary>
        ///     Военно-историческая работа
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("HistWorkFull")]
        public double HistoricalWorkOrganizationFull()
        {
            return MetricValuesCalculation("Уровень организации военно-исторической работы");
        }

        /// <summary>
        ///     Работа научного кружка курсантов (студентов)
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("MssFull")]
        public double MilitaryScientificSocietyOrganizationFull()
        {
            return MetricValuesCalculation("Уровень организации работы научных кружков");
        }

        /// <summary>
        ///     Другие частные показатели, характеризующие качество организации научной работы
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("CustSwOrg")]
        public double CustomScientificWorkOrganizationRating()
        {
            return MetricValuesCalculation("Другие частные показатели, характеризующие качество организации");
        }

        /// <summary>
        ///     Другие частные показатели, характеризующие проведение научных исследований
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("CustSrOrg")]
        public double CustomScientificResearchOrganizationRating()
        {
            return MetricValuesCalculation("Другие частные показатели, характеризующие проведение");
        }

        /// <summary>
        ///     Другие частные показатели, характеризующие апробацию результатов научных исследований
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("CustArsrOrg")]
        public double CustomAprobationResultsOfScientificWork()
        {
            return MetricValuesCalculation("Другие частные показатели, характеризующие апробацию результатов");
        }

        /// <summary>
        ///     Другие частные показатели, характеризующие подготовку и аттестацию научных работников высшей квалификации
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("CustTcSpHq")]
        public double CustomTcSpHq() //training and certification of scientific personnel of higher qualification
        {
            return MetricValuesCalculation("Другие частные показатели, характеризующие подготовку");
        }
    }
}