using MilitaryFaculty.Data;
using MilitaryFaculty.Domain;
using Dissertation = MilitaryFaculty.Domain.Dissertation;

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

        public override void SetProfessorModificator(Professor professor, TimeInterval interval)
        {
            QueryModificator = dissertation =>
                dissertation.Author.Id == professor.Id
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
            return CountOf(s => s.TargetAcademicRank == AcademicRank.DoctorOfScience);
        }

        /// <summary>
        ///     Количество ППС, которым присвоена ученая степень кандидата наук (в проверяемый период)
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("CandThesisesCount")]
        public double CandidateThesisesCount()
        {
            return CountOf(s => s.TargetAcademicRank == AcademicRank.CandidateOfScience);
        }
    }
}