using System;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;
using System.Linq;

namespace MilitaryFaculty.Logic.DataProviders
{
    public class ProfessorsDataProvider : IDataProvider
    {
        #region Class Fields

        private readonly IProfessorRepository professorRepository;
        
        #endregion // Class Fields

        #region Class Constructors

        public ProfessorsDataProvider(IProfessorRepository professorRepository)
        {
            if (professorRepository == null)
            {
                throw new ArgumentNullException("professorRepository");
            }

            this.professorRepository = professorRepository;
        }

        #endregion // Class Constructors

        #region Class Public Argument Methods

        /// <summary>
        /// Общее количество ППС
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ProfsCount")]
        public double ProfessorsCount()
        {
            var count = professorRepository.All().Count();
            return count;
        }

        /// <summary>
        /// Количество докторантов
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("DocsCount")]
        public double DoctoralCandidatesCount()
        {
            return 1;
        }

        /// <summary>
        /// Удельный вес: адьюнктов очной формы обучения
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("AdjCountFt")]
        public double AdjunctsCountFullTime()
        {
            return 2;
        }

        /// <summary>
        /// Удельный вес: адьюнктов заочной формы обучения
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("AdjCountExt")]
        public double AdjunctsCountExtramural()
        {
            return 3;
        }

        /// <summary>
        /// Количество соискателей ученой степени доктора наук
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ApplDoctCount")]
        public double ApplicantForDoctorsCount()
        {
            return 5;
        }

        /// <summary>
        /// Количество соискателей ученой степени кандидата наук
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ApplCandCount")]
        public double ApplicantForCandidatsCount()
        {
            return 5;
        }

        /// <summary>
        /// Количество профессоров
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ProfPostsCount")]
        public double ProfessorPostsCount()
        {
            return professorRepository.All().Count(p => p.AcademicDegree == AcademicDegree.Professor);
        }

        /// <summary>
        /// Количество должностей подлежащих замещению профессорами
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ProfPostsSubsCount")]
        public double ProfessorPostsSubstitutionCount()
        {
            return 5;
        }

        /// <summary>
        /// Количество доцектов
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("DocentPostsCount")]
        public double DocentPostsCount()
        {
            return professorRepository.All().Count(p => p.AcademicDegree == AcademicDegree.Docent);
        }

        /// <summary>
        /// Количество должностей подлежащих замещению доцентами
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("DocentPostsSubsCount")]
        public double DocentPostsSubstitutionCount()
        {
            return 2;
        }

        /// <summary>
        /// Количество докторов наук
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("DosCount")]
        public double DoctorsOfScienceCount()
        {
            return professorRepository.All().Count(p => p.AcademicRank == AcademicRank.DoctorOfScience);
        }

        /// <summary>
        /// Количество кандидатов наук
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("CosCount")]
        public double CandidatsOfScienceCount()
        {
            return professorRepository.All().Count(p => p.AcademicRank == AcademicRank.CandidateOfScience);
        }

        /// <summary>
        /// Количество докторов и кандидатов наук из числа ППС
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("DosCosCount")]
        public double DoctorsOfScienceAndCandidatsCount()
        {
            return DoctorsOfScienceCount() + CandidatsOfScienceCount();
        }

        /// <summary>
        /// Количество научных работников высшей квалификации из числа ППС, 
        /// осуществляющих научное консультирование
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("SaHqProfsCount")]
        public double ScientificAdviceHqProfsCount()
        {
            return 5;
        }

        /// <summary>
        /// Количество ППС, осуществляющих научное руководство
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("SaProfsCount")]
        public double ScientificAdviceProfsCount()
        {
            return 5;
        }

        /// <summary>
        /// Общее количество научных работников высшей квалификации
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("HqProfsCount")]
        public double HighQualificationProfsCount()
        {
            return professorRepository.All().Count(p => p.AcademicDegree != AcademicDegree.None || 
                                                        p.AcademicRank != AcademicRank.None);
        }

        /// <summary>
        /// Количество научных работников высшей квалификации из числа ППС, 
        /// участвующих в работе экспертных советов ВАК Беларуси
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("HacHqProfsCount")]
        public double HacHqProfsCount() //Higher Attestation Commission
        {
            return 2;
        }

        /// <summary>
        /// Количество научных работников высшей квалификации из числа ППС, 
        /// участвующих в работе специализированных советов по защите диссертаций
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("DodHqProfsCount")]
        public double DodHqProfsCount() //Defence of dissertation counsil
        {
            return 3;
        }

        /// <summary>
        /// Количество научных работников высшей квалификации из числа ППС, 
        /// участвующих в работе научных советов вуза
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("RcHqProfsCount")]
        public double RcHqProfsCount() //research counsil
        {
            return 4;
        }
        
        /// <summary>
        /// Количество научных работников высшей квалификации из числа ППС, 
        /// участвующих в работе редакционных коллегий научных изданий
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("EbspHqProfsCount")]
        public double EbspHqProfsCount() //editorial boards of scientific publications
        {
            return 5;
        }

        /// <summary>
        /// Укомплектованность военного факультета (кафедры) научными работнкиками высшей квалификации 
        /// больше 40%
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("SoHqA40")]
        public double StaffingOfHighQualificationAbove40()
        {
            return 5;
        }

        /// <summary>
        /// Укомплектованность военного факультета (кафедры) научными работнкиками высшей квалификации 
        /// более 20%, но менее 40%
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("SoHqA20B40")]
        public double StaffingOfHighQualificationAbove20Below40()
        {
            return 1;
        }

        /// <summary>
        /// Укомплектованность военного факультета (кафедры) научными работнкиками высшей квалификации 
        /// менее 20%
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("SoHqB20")]
        public double StaffingOfHighQualificatioBelow20()
        {
            return 2;
        }

        /// <summary>
        /// Работники высшей квалификации отсутствуют
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("SoHqE0")]
        public double StaffingOfHighQualificatioEqual0()
        {
            return 3;
        }

        /// <summary>
        /// Количество ППС, участвующих в работе по проведению научной экспертизы
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("SeProfsCount")]
        public double ScientificExperticeProfessorsCount()
        {
            return 2;
        }

        #endregion // Class Public Argument Methods
    }
}