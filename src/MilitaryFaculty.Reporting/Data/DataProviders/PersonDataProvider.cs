using System;
using MilitaryFaculty.Data;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Reporting.Data.DataProviders
{
    public class PersonDataProvider : DataProvider<Person>
    {
        public PersonDataProvider(IRepository<Person> professorRepository)
            : base(professorRepository)
        {
        }

        public override void SetFacultyModificator(TimeInterval interval)
        {
            QueryModificator = p =>
                (p.EnrollmentDate >= interval.From && p.EnrollmentDate <= interval.To)
                || (p.DismissalDate >= interval.From && p.DismissalDate <= interval.To)
                || (p.EnrollmentDate <= interval.From && p.DismissalDate >= interval.To);
        }

        public override void SetCathedraModificator(Cathedra cathedra, TimeInterval interval)
        {
            QueryModificator = p =>
                p.Cathedra.Id == cathedra.Id
                && ((p.EnrollmentDate >= interval.From && p.EnrollmentDate <= interval.To)
                    || (p.DismissalDate >= interval.From && p.DismissalDate <= interval.To)
                    || (p.EnrollmentDate <= interval.From && p.DismissalDate >= interval.To));
        }

        public override void SetPersonModificator(Person person, TimeInterval interval)
        {
            QueryModificator = p =>
                p.Id == person.Id
                && ((p.EnrollmentDate >= interval.From && p.EnrollmentDate <= interval.To)
                    || (p.DismissalDate >= interval.From && p.DismissalDate <= interval.To)
                    || (p.EnrollmentDate <= interval.From && p.DismissalDate >= interval.To));
        }

        /// <summary>
        ///     Общее количество ППС
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ProfsCount")]
        public double ProfessorsCount()
        {
            return CountOf(p => p.JobPosition >= JobPosition.Teacher);
        }

        /// <summary>
        ///     Количество докторантов
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("DocsCount")]
        public double DoctoralCandidatesCount()
        {
            return CountOf(p => p.JobPosition == JobPosition.Doctoral);
        }

        /// <summary>
        ///     Удельный вес: адьюнктов очной формы обучения
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("AdjCountFt")]
        public double AdjunctsCountFullTime()
        {
            return CountOf(p => p.JobPosition == JobPosition.AdjunctFullTime);
        }

        /// <summary>
        ///     Удельный вес: адьюнктов заочной формы обучения
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("AdjCountExt")]
        public double AdjunctsCountExtramural()
        {
            return CountOf(p => p.JobPosition == JobPosition.AdjunctPartTime);
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
            return CountOf(p => p.JobPosition == JobPosition.Professor
                                && p.AcademicDegree != AcademicDegree.Professor);
        }

        /// <summary>
        ///     Количество доцентов
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
            return CountOf(p => p.JobPosition == JobPosition.Docent
                                && p.AcademicDegree != AcademicDegree.Docent);
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
        ///     Количество ППС, осуществляющих научное руководство докторских
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("SaHqProfsCountDoc")]
        public double ScientificAdviceProfsCount()
        {
            //TODO
            return 1;
        }

        /// <summary>
        ///     Количество ППС, осуществляющих научное руководство кандидатских
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("SaHqProfsCountCand")]
        public double ScientificAdviceHqProfsCount()
        {
            //TODO
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
        ///     Укомплектованность военного факультета (кафедры) научными работниками высшей квалификации (1 или 0)
        ///     больше 40%
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("SoHqA40")]
        public double StaffingOfHighQualificationAbove40()
        {
            double ratio = HighQualificationProfsCount()/ProfessorsCount();
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
            double ratio = HighQualificationProfsCount()/ProfessorsCount();
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
            double ratio = HighQualificationProfsCount()/ProfessorsCount();
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
    }
}