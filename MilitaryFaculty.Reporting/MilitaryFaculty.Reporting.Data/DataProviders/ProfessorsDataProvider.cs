using System;
using System.Linq;
using System.Linq.Expressions;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Reporting.Data.DataProviders
{
    public class ProfessorsDataProvider : DataProvider<Professor>
    {
        public ProfessorsDataProvider(IRepository<Professor> professorRepository)
            : base(professorRepository)
        {
        }

        public ProfessorsDataProvider(IRepository<Professor> repository,
            Expression<Func<Professor, bool>> modificator)
            : base(repository, modificator)
        {
        }

        /// <summary>
        ///     Общее количество ППС
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ProfsCount")]
        public double ProfessorsCount()
        {
            return CountOf(x => true);
        }

        /// <summary>
        ///     Количество докторантов
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("DocsCount")]
        public double DoctoralCandidatesCount()
        {
            //TODO: Узнать про докторантов
            return 1;
        }

        /// <summary>
        ///     Удельный вес: адьюнктов очной формы обучения
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("AdjCountFt")]
        public double AdjunctsCountFullTime()
        {
            //TODO: адьюнкты
            return 1;
        }

        /// <summary>
        ///     Удельный вес: адьюнктов заочной формы обучения
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("AdjCountExt")]
        public double AdjunctsCountExtramural()
        {
            return 1;
        }

        /// <summary>
        ///     Количество соискателей ученой степени доктора наук
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ApplDoctCount")]
        public double ApplicantForDoctorsCount()
        {
            //TODO: соискатели
            return 1;
        }

        /// <summary>
        ///     Количество соискателей ученой степени кандидата наук
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ApplCandCount")]
        public double ApplicantForCandidatsCount()
        {
            return 1;
        }

        /// <summary>
        ///     Количество профессоров
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ProfPostsCount")]
        public double ProfessorPostsCount()
        {
            return CountOf(p => p.AcademicDegree == AcademicDegree.Professor);
        }

        /// <summary>
        ///     Количество должностей подлежащих замещению профессорами
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ProfPostsSubsCount")]
        public double ProfessorPostsSubstitutionCount()
        {
            return 1;
        }

        /// <summary>
        ///     Количество доцектов
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("DocentPostsCount")]
        public double DocentPostsCount()
        {
            return CountOf(p => p.AcademicDegree == AcademicDegree.Docent);
        }

        /// <summary>
        ///     Количество должностей подлежащих замещению доцентами
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("DocentPostsSubsCount")]
        public double DocentPostsSubstitutionCount()
        {
            //TODO: DaFaq?
            return 1;
        }

        /// <summary>
        ///     Количество докторов наук
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("DosCount")]
        public double DoctorsOfScienceCount()
        {
            return CountOf(p => p.AcademicRank == AcademicRank.DoctorOfScience);
        }

        /// <summary>
        ///     Количество кандидатов наук
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("CosCount")]
        public double CandidatsOfScienceCount()
        {
            return CountOf(p => p.AcademicRank == AcademicRank.CandidateOfScience);
        }

        /// <summary>
        ///     Количество докторов и кандидатов наук из числа ППС
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("DosCosCount")]
        public double DoctorsOfScienceAndCandidatsCount()
        {
            return DoctorsOfScienceCount() + CandidatsOfScienceCount();
        }

        /// <summary>
        ///     Количество научных работников высшей квалификации из числа ППС,
        ///     осуществляющих научное консультирование
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("SaHqProfsCount")]
        public double ScientificAdviceHqProfsCount()
        {
            return 1;
        }

        /// <summary>
        ///     Количество ППС, осуществляющих научное руководство
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("SaProfsCount")]
        public double ScientificAdviceProfsCount()
        {
            return 1;
        }

        /// <summary>
        ///     Общее количество научных работников высшей квалификации
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("HqProfsCount")]
        public double HighQualificationProfsCount()
        {
            return CountOf(p => p.AcademicDegree != AcademicDegree.None ||
                                p.AcademicRank != AcademicRank.None);
        }

        /// <summary>
        ///     Количество научных работников высшей квалификации из числа ППС,
        ///     участвующих в работе экспертных советов ВАК Беларуси
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("HacHqProfsCount")]
        public double HacHqProfsCount() //Higher Attestation Commission
        {
            //TODO: Start here
            return CountOf(
                p =>
                    p.Participations.Count(
                        partisipation => partisipation.PlaceType == ParticipationPlace.HigherAttestationCommission
                        ) != 0
                );
        }

        /// <summary>
        ///     Количество научных работников высшей квалификации из числа ППС,
        ///     участвующих в работе специализированных советов по защите диссертаций
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("DodHqProfsCount")]
        public double DodHqProfsCount() //Defence of dissertation counsil
        {
            return CountOf(
                p =>
                    p.Participations.Count(
                        partisipation => partisipation.PlaceType == ParticipationPlace.DefenceOfDissertationCounsil
                        ) != 0
                );
        }

        /// <summary>
        ///     Количество научных работников высшей квалификации из числа ППС,
        ///     участвующих в работе научных советов вуза
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("RcHqProfsCount")]
        public double RcHqProfsCount() //research counsil
        {
            return CountOf(
                p =>
                    p.Participations.Count(
                        partisipation => partisipation.PlaceType == ParticipationPlace.ResearchCounsil
                        ) != 0
                );
        }

        /// <summary>
        ///     Количество научных работников высшей квалификации из числа ППС,
        ///     участвующих в работе редакционных коллегий научных изданий
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("EbspHqProfsCount")]
        public double EbspHqProfsCount() //editorial boards of scientific publications
        {
            return CountOf(
                p =>
                    p.Participations.Count(
                        partisipation =>
                            partisipation.PlaceType == ParticipationPlace.EditorialBoardsOfScientificPublications
                        ) != 0
                );
        }

        /// <summary>
        ///     Укомплектованность военного факультета (кафедры) научными работниками высшей квалификации (1 или 0)
        ///     больше 40%
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("SoHqA40")]
        public double StaffingOfHighQualificationAbove40()
        {
            var ratio = HighQualificationProfsCount()/ProfessorsCount();
            return ratio > 0.4 && ratio <= 1.001 ? 1 : 0;
        }

        /// <summary>
        ///     Укомплектованность военного факультета (кафедры) научными работниками высшей квалификации (1 или 0)
        ///     более 20%, но менее 40%
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("SoHqA20B40")]
        public double StaffingOfHighQualificationAbove20Below40()
        {
            var ratio = HighQualificationProfsCount()/ProfessorsCount();
            return ratio > 0.2 && ratio <= 0.4 ? 1 : 0;
        }

        /// <summary>
        ///     Укомплектованность военного факультета (кафедры) научными работниками высшей квалификации (1 или 0)
        ///     менее 20%
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("SoHqB20")]
        public double StaffingOfHighQualificatioBelow20()
        {
            var ratio = HighQualificationProfsCount()/ProfessorsCount();
            return ratio > 0.0001 && ratio <= 0.2 ? 1 : 0;
        }

        /// <summary>
        ///     Работники высшей квалификации отсутствуют
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("SoHqE0")]
        public double StaffingOfHighQualificatioEqual0()
        {
            return Math.Abs(HighQualificationProfsCount()) < 0.0001 ? 1 : 0;
        }

        /// <summary>
        ///     Количество ППС, участвующих в работе по проведению научной экспертизы
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("SeProfsCount")]
        public double ScientificExperticeProfessorsCount()
        {
            return CountOf(p => p.ScientificExpertises.Count != 0);
        }
    }
}