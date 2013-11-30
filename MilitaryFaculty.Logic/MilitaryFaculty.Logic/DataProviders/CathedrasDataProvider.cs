using System;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Logic.DataProviders
{
    public class CathedrasDataProvider : IDataProvider
    {
        #region Class Fields

        private readonly ICathedraRepository cathedraRapository;
        
        #endregion // Class Fields

        #region Class Constructors

        public CathedrasDataProvider(ICathedraRepository cathedraRepository)
        {
            if (cathedraRepository == null)
            {
                throw new ArgumentNullException("cathedraRepository");
            }

            this.cathedraRapository = cathedraRepository;
        }

        #endregion // Class Constructors

        #region Class Public Argument Methods

        /// <summary>
        /// Планирующие документы разработаны в соответствии с требованиями прововых актов, мероприятия по научной работе выполнены своевременно и имеют практический результат
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("PlanDocsOrg")]
        public double PlanningDocumentsOrganization()
        {
            return 1;
        }

        /// <summary>
        /// Тематика исследований соответствует направлениям строительства и развития Вооруженный Сил
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ResTopicsOrg")]
        public double ResearchTopicsOrganization()
        {
            return 2;
        }

        /// <summary>
        /// Лица из числа руководящего и профессорско-преподавательского состаово знают требования правовых актов Министерства обороны по организации научной работы и руководствуются ими в повседневной деятельности, непосредственно принимают участие в научной работе
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ProfsOrg")]
        public double ProfessorsOrganization()
        {
            return 3;
        }

        /// <summary>
        /// Международных конференций
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("IntConfOrg")]
        public double InternationalConferencesOrganization()
        {
            return 4;
        }

        /// <summary>
        /// Республиканских конференций
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("RepConfOrg")]
        public double RepublicanConferenceOrganization()
        {
            return 5;
        }

        /// <summary>
        /// Конференций военного учебного заведения
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("UnConfOrg")]
        public double UniversityConferenceOrganization()
        {
            return 1;
        }

        /// <summary>
        /// Республиканских семинаров
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("RepSemOrg")]
        public double RepablicanSeminarOrganization()
        {
            return 2;
        }

        /// <summary>
        /// Семинаров военного учебного заведения
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("UnSemOrg")]
        public double UniversitySeminarOrganization()
        {
            return 3;
        }

        /// <summary>
        /// Подготовка научно-педагогических работников высшей квалификации организована (разработаны годовой и перспективный планы подготовки научных работников высшей квалификации; имеются в налиии и выполнены индивидуальные планы соискателей)
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ProfsOrgFull")]
        public double ProfessorsOrganizationFull()
        {
            return 4;
        }

        /// <summary>
        /// Подготовка научно-педагогических работников высшей квалификации организована, но ведется с отдельными недостатками
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ProfsOrgCust")]
        public double ProfessorsOrganizationCustom()
        {
            return 5;
        }

        /// <summary>
        /// Военно-историческая работа организована и ведется в соответствии с требованиями нормативных правовых актов
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("HistWorkFull")]
        public double HistoricalWorkOrganizationFull()
        {
            return 1;
        }

        /// <summary>
        /// Военно-историческая работа органозована, но ведется с отдельными недостатками
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("HistWorkCust")]
        public double HistoricalWorkOrganizationCustom()
        {
            return 2;
        }

        /// <summary>
        /// Работа научного кружка курсантов (студентов) организована и ведется в соответствии с требованиями нормативных правовых актов
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("MssFull")]
        public double MilitaryScientificSocietyOrganizationFull()
        {
            return 3;
        }

        /// <summary>
        /// Работа научного кружка курсантов (стедентов) организована, но ведется с отдельными недостатками
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("MssCust")]
        public double MilitaryScientificSocietyOrganizationCustom()
        {
            return 4;
        }

        /// <summary>
        /// Другие частные показатели, характеризующие качество организации научной работы
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("CustSwOrg")]
        public double CustomScientificWorkOrganizationRating()
        {
            return 5;
        }

        /// <summary>
        /// Другие частные показатели, характеризующие проведение научных исследований
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("CustSrOrg")]
        public double CustomScientificResearchOrganizationRating()
        {
            return 3;
        }

        /// <summary>
        /// Другие частные показатели, характеризующие апробацию результатов научных исследований
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("CustArsrOrg")]
        public double CustomAprobationResultsOfScientificWork()
        {
            return 3;
        }

        /// <summary>
        /// Другие частные показатели, характеризующие подготовку и аттестацию научных работников высшей квалификации
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("CustTcSpHq")]
        public double CustomTcSpHq() //training and certification of scientific personnel of higher qualification
        {
            return 1;
        }
        
        #endregion Class Public Argument Methods
    }
}