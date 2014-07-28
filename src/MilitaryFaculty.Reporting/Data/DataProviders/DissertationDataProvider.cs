using MilitaryFaculty.Data;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Reporting.Data.DataProviders
{
    public class DissertationDataProvider : DataProvider<Dissertation>
    {
        public DissertationDataProvider(IRepository<Dissertation> repository)
            : base(repository)
        {
        }

        public override void SetFacultyModificator(TimeInterval interval)
        {
            QueryModificator = dissertation =>
                dissertation.CreatedAt >= interval.From
                && dissertation.CreatedAt <= interval.To;
        }

        public override void SetCathedraModificator(Cathedra cathedra, TimeInterval interval)
        {
            QueryModificator = dissertation =>
                dissertation.Author.Cathedra.Id == cathedra.Id
                && dissertation.CreatedAt >= interval.From
                && dissertation.CreatedAt <= interval.To;
        }

        public override void SetPersonModificator(Person person, TimeInterval interval)
        {
            QueryModificator = dissertation =>
                dissertation.Author.Id == person.Id
                && dissertation.CreatedAt >= interval.From
                && dissertation.CreatedAt <= interval.To;
        }


        /// <summary>
        ///     Количество ППС, которым присвоена ученая степень доктора наук (в проверяемый период)
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("DocThesisesCount")]
        public double DoctorThesisesCount()
        {
            return CountOf(s => s.TargetAcademicRank == AcademicRank.DoctorOfScience
                                && s.State == DissertationState.Defenced);
        }

        /// <summary>
        ///     Количество ППС, которым присвоена ученая степень кандидата наук (в проверяемый период)
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("CandThesisesCount")]
        public double CandidateThesisesCount()
        {
            return CountOf(s => s.TargetAcademicRank == AcademicRank.CandidateOfScience
                                && s.State == DissertationState.Defenced);
        }

        /// <summary>
        ///     Количество соискателей ученой степени доктора наук
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ApplDoctCount")]
        public double ApplicantForDoctorsCount()
        {
            return CountOf(d => d.TargetAcademicRank == AcademicRank.DoctorOfScience
                                && d.State == DissertationState.Development);
        }

        /// <summary>
        ///     Количество соискателей ученой степени кандидата наук
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ApplCandCount")]
        public double ApplicantForCandidatsCount()
        {
            return CountOf(d => d.TargetAcademicRank == AcademicRank.CandidateOfScience
                                && d.State == DissertationState.Development);
        }
    }
}