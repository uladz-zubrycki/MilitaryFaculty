using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Logic
{
    public class DataProvider
    {
        #region Class Fields
        private readonly Professor professor;
        private readonly List<Cathedra> cathedras;
        #endregion Class Fields

        #region Class Constructors

        private DataProvider()
        {
            professor = null;
            cathedras = null;
        }

        public DataProvider(Professor professor) : this()
        {
            if (professor == null)
            {
                throw new ArgumentNullException("professor");
            }

            this.professor = professor;
        }

        public DataProvider(Cathedra cathedra) : this()
        {
            if (cathedra == null)
            {
                throw new ArgumentNullException("cathedra");
            }

            cathedras = new List<Cathedra> {cathedra};
        }

        public DataProvider(List<Cathedra> cathedras) : this()
        {
            if (cathedras == null)
            {
                throw new ArgumentNullException("cathedras");
            }

            this.cathedras = cathedras;
        }

        #endregion Class Constructors

        #region Class Public Methods

        public double GetValue(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            if (professor != null)
                value += "Prof";

            var method = GetType().GetMethods()
                                  .FirstOrDefault(m =>
                                      {
                                          var attr = m.GetCustomAttribute<FormulaArgumentAttribute>();
                                          return attr != null && attr.Name == value;
                                      });
            return method != null ? (double) method.Invoke(this, null) : 0;
        }

        #endregion // Class Public Methods

        #region Class Public Argument Methods

        [FormulaArgument("PlanDocsOrg")]
        public double PlanningDocumentsOrganization()
        {
            return cathedras == null ? 1 : cathedras.Count;
        }

        [FormulaArgument("ResTopicsOrg")]
        public double ResearchTopicsOrganization()
        {
            return 2;
        }

        [FormulaArgument("ProfsOrg")]
        public double ProfessorsOrganization()
        {
            return 3;
        }

        [FormulaArgument("IntConfOrg")]
        public double InternationalConferencesOrganization()
        {
            return 4;
        }

        [FormulaArgument("RepConfOrg")]
        public double RepublicanConferenceOrganization()
        {
            return 5;
        }

        [FormulaArgument("UnConfOrg")]
        public double UniversityConferenceOrganization()
        {
            return 1;
        }

        [FormulaArgument("RepSemOrg")]
        public double RepablicanSeminarOrganization()
        {
            return 2;
        }

        [FormulaArgument("UnSemOrg")]
        public double UniversitySeminarOrganization()
        {
            return 3;
        }

        [FormulaArgument("ProfsOrgFull")]
        public double ProfessorsOrganizationFull()
        {
            return 4;
        }

        [FormulaArgument("ProfsOrgCust")]
        public double ProfessorsOrganizationCustom()
        {
            return 5;
        }

        [FormulaArgument("HistWorkFull")]
        public double HistoricalWorkOrganizationFull()
        {
            return 1;
        }

        [FormulaArgument("HistWorkCust")]
        public double HistoricalWorkOrganizationCustom()
        {
            return 2;
        }

        [FormulaArgument("MssFull")]
        public double MilitaryScientificSocietyOrganizationFull()
        {
            return 3;
        }

        [FormulaArgument("MssCust")]
        public double MilitaryScientificSocietyOrganizationCustom()
        {
            return 4;
        }

        [FormulaArgument("CustSwOrg")]
        public double CustomScientificWorkOrganizationRating()
        {
            return 5;
        }

        [FormulaArgument("SrwProfsCount")]
        public double ScientificResearchWorkProfessorsCount()
        {
            return 1;
        }

        [FormulaArgument("ProfsCount")]
        public double ProfessorsCount()
        {
            return 2;
        }

        [FormulaArgument("SrwCount")]
        public double ScientificResearchWorksCount()
        {
            return 3;
        }

        [FormulaArgument("MssCount")]
        public double MilitaryScientificSupportsCount()
        {
            return 4;
        }

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

        [FormulaArgument("InnCount")]
        public double InnovationsCount()
        {
            return 2;
        }

        [FormulaArgument("UsModCount")]
        public double UsefulModelsCount()
        {
            return 3;
        }

        [FormulaArgument("PosInnCount")]
        public double PositiveInnovationsCount()
        {
            return 4;
        }

        [FormulaArgument("PosUsModCount")]
        public double PositiveUsefulModelsCount()
        {
            return 5;
        }

        [FormulaArgument("RationPropCount")]
        public double RationalizationProposalsCount()
        {
            return 1;
        }

        [FormulaArgument("SeProfsCount")]
        public double ScientificExperticeProfessorsCount()
        {
            return 2;
        }

        [FormulaArgument("CustSrOrg")]
        public double CustomScientificResearchOrganizationRating()
        {
            return 3;
        }

        [FormulaArgument("UnConfProfsCount")]
        public double UniversityConferenceProfessorsCount()
        {
            return 4;
        }

        [FormulaArgument("UnConfStudsCount")]
        public double UniversityConferenceStudentsCount()
        {
            return 5;
        }

        [FormulaArgument("ReConfProfsCount")]
        public double RepublicanConferenceProfessorsCount()
        {
            return 1;
        }

        [FormulaArgument("ReConfStudsCount")]
        public double RepublicanConferenceStudentsCount()
        {
            return 2;
        }

        [FormulaArgument("InConfProfsCount")]
        public double InternationalConferenceProfessorsCount()
        {
            return 3;
        }

        [FormulaArgument("InConfStudsCount")]
        public double InternationalConferenceStudentsCount()
        {
            return 4;
        }

        [FormulaArgument("UnFirstDiplCount")]
        public double UniversityFirstDiplomasCount()
        {
            return 5;
        }

        [FormulaArgument("UnSecondDiplCount")]
        public double UniversitySecondDiplomasCount()
        {
            return 1;
        }

        [FormulaArgument("UnThirdDiplCount")]
        public double UniversityThirdDiplomasCount()
        {
            return 2;
        }

        [FormulaArgument("UnLettersCommendationCount")]
        public double UniversityLettersOfCommendationCount()
        {
            return 3;
        }

        [FormulaArgument("ReFirstDiplCount")]
        public double RepublicanFirstDiplomasCount()
        {
            return 5;
        }

        [FormulaArgument("ReSecondDiplCount")]
        public double RepublicanSecondDiplomasCount()
        {
            return 1;
        }

        [FormulaArgument("ReThirdDiplCount")]
        public double RepublicanThirdDiplomasCount()
        {
            return 2;
        }

        [FormulaArgument("ReLettersCommendationCount")]
        public double RepublicanLettersOfCommendationCount()
        {
            return 3;
        }

        [FormulaArgument("InFirstDiplCount")]
        public double InternationalFirstDiplomasCount()
        {
            return 5;
        }

        [FormulaArgument("InSecondDiplCount")]
        public double InternationalSecondDiplomasCount()
        {
            return 1;
        }

        [FormulaArgument("InThirdDiplCount")]
        public double InternationalThirdDiplomasCount()
        {
            return 2;
        }

        [FormulaArgument("InLettersCommendationCount")]
        public double InternationalLettersOfCommendationCount()
        {
            return 3;
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

        [FormulaArgument("LecturesCount")]
        public double LecturesCount()
        {
            return 2;
        }

        [FormulaArgument("CustArsrOrg")]
        public double CustomAprobationResultsOfScientificWork()
        {
            return 3;
        }

        #endregion Class Public Argument Methods
    }
}
