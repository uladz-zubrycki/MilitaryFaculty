using MilitaryFaculty.Data;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Reporting.Data.DataProviders
{
    public class ReviewDataProvider : DataProvider<Review>
    {
        public ReviewDataProvider(IRepository<Review> repository) : base(repository)
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
        ///     Подготовлено экспертных заключений по диссертациям: докторским
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("DocEoCount")]
        public double DoctorsOfScienceExpertOpinionsCount()
        {
            return CountOf(r => r.Type == ReviewType.ExpertAdvice
                                && r.TargetAcademicRank == AcademicRank.DoctorOfScience);
        }

        /// <summary>
        ///     Подготовлено экспертных заключений по диссертациям: кандидатским
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("CandEoCount")]
        public double CandidatsExpertOpinionsCount()
        {
            return CountOf(r => r.Type == ReviewType.ExpertAdvice
                                && r.TargetAcademicRank == AcademicRank.CandidateOfScience);
        }

        /// <summary>
        ///     Отзывов на авторефераты диссертаций: докторских
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("EssayReviewDosCount")]
        public double EssayReviewsByDosCount()
        {
            return CountOf(s => s.Type == ReviewType.SummaryFeedback
                                && s.TargetAcademicRank == AcademicRank.DoctorOfScience);
        }

        /// <summary>
        ///     Отзывов на авторефераты диссертаций: кандидатских
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("EssayReviewCanCount")]
        public double EssayReviewByCandidatsCount()
        {
            return CountOf(s => s.Type == ReviewType.SummaryFeedback
                                && s.TargetAcademicRank == AcademicRank.CandidateOfScience);
        }
    }
}