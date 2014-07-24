using MilitaryFaculty.Data;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Reporting.Data.DataProviders
{
    public class CathedrasDataProvider : DataProvider<Cathedra>
    {
        public CathedrasDataProvider(IRepository<Cathedra> repository)
            : base(repository)
        {
        }

        public override void SetFacultyModificator(TimeInterval interval)
        {
            QueryModificator = null;
        }

        public override void SetCathedraModificator(Cathedra cathedra, TimeInterval interval)
        {
            QueryModificator = c =>
                c.Id == cathedra.Id;
        }

        public override void SetProfessorModificator(Professor professor, TimeInterval interval)
        {
            QueryModificator = null;
        }

        /// <summary>
        ///     Планирующие документы разработаны в соответствии с требованиями прововых актов, мероприятия по научной работе
        ///     выполнены своевременно и имеют практический результат
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("PlanDocsOrg")]
        public double PlanningDocumentsOrganization()
        {
            return 0;
        }

        /// <summary>
        ///     Тематика исследований соответствует направлениям строительства и развития Вооруженный Сил
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ResTopicsOrg")]
        public double ResearchTopicsOrganization()
        {
            return 0;
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
            return 0;
        }

        /// <summary>
        ///     Уровень оргинизации международных конференций
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("IntConfOrg")]
        public double InternationalConferencesOrganization()
        {
            return 0;
        }

        /// <summary>
        ///     Уровень оргинизации республиканских конференций
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("RepConfOrg")]
        public double RepublicanConferenceOrganization()
        {
            return 0;
        }

        /// <summary>
        ///     Уровень оргинизации конференций военного учебного заведения
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("UnConfOrg")]
        public double UniversityConferenceOrganization()
        {
            return 0;
        }

        /// <summary>
        ///     Уровень оргинизации республиканских семинаров
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("RepSemOrg")]
        public double RepablicanSeminarOrganization()
        {
            return 0;
        }

        /// <summary>
        ///     Уровень оргинизации семинаров военного учебного заведения
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("UnSemOrg")]
        public double UniversitySeminarOrganization()
        {
            return 0;
        }

        /// <summary>
        ///     Подготовка научно-педагогических работников высшей квалификации организована (разработаны годовой и перспективный
        ///     планы подготовки научных работников высшей квалификации; имеются в налиии и выполнены индивидуальные планы
        ///     соискателей)
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ProfsOrgFull")]
        public double ProfessorsOrganizationFull()
        {
            return 0;
        }

        /// <summary>
        ///     Подготовка научно-педагогических работников высшей квалификации организована, но ведется с отдельными недостатками
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ProfsOrgCust")]
        public double ProfessorsOrganizationCustom()
        {
            return 0;
        }

        /// <summary>
        ///     Военно-историческая работа организована и ведется в соответствии с требованиями нормативных правовых актов
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("HistWorkFull")]
        public double HistoricalWorkOrganizationFull()
        {
            return 0;
        }

        /// <summary>
        ///     Военно-историческая работа органозована, но ведется с отдельными недостатками
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("HistWorkCust")]
        public double HistoricalWorkOrganizationCustom()
        {
            return 0;
        }

        /// <summary>
        ///     Работа научного кружка курсантов (студентов) организована и ведется в соответствии с требованиями нормативных
        ///     правовых актов
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("MssFull")]
        public double MilitaryScientificSocietyOrganizationFull()
        {
            return 0;
        }

        /// <summary>
        ///     Работа научного кружка курсантов (стедентов) организована, но ведется с отдельными недостатками
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("MssCust")]
        public double MilitaryScientificSocietyOrganizationCustom()
        {
            return 0;
        }

        /// <summary>
        ///     Другие частные показатели, характеризующие качество организации научной работы
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("CustSwOrg")]
        public double CustomScientificWorkOrganizationRating()
        {
            return 0;
        }

        /// <summary>
        ///     Другие частные показатели, характеризующие проведение научных исследований
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("CustSrOrg")]
        public double CustomScientificResearchOrganizationRating()
        {
            return 0;
        }

        /// <summary>
        ///     Другие частные показатели, характеризующие апробацию результатов научных исследований
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("CustArsrOrg")]
        public double CustomAprobationResultsOfScientificWork()
        {
            return 0;
        }

        /// <summary>
        ///     Другие частные показатели, характеризующие подготовку и аттестацию научных работников высшей квалификации
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("CustTcSpHq")]
        public double CustomTcSpHq() //training and certification of scientific personnel of higher qualification
        {
            return 0;
        }
    }
}