using MilitaryFaculty.Data;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Reporting.Data.DataProviders
{
    public class AcademicDegreePromotionsDataProvider : DataProvider<AcademicDegreePromotion>
    {
        public AcademicDegreePromotionsDataProvider(IRepository<AcademicDegreePromotion> repository)
            : base(repository)
        {
        }

        public override void SetFacultyModificator(TimeInterval interval)
        {
            QueryModificator = adp =>
                adp.PromotedAt >= interval.From
                && adp.PromotedAt <= interval.To;
        }

        public override void SetCathedraModificator(Cathedra cathedra, TimeInterval interval)
        {
            QueryModificator = adp =>
                adp.Professor.Cathedra.Id == cathedra.Id
                && adp.PromotedAt >= interval.From
                && adp.PromotedAt <= interval.To;
        }

        public override void SetPersonModificator(Person person, TimeInterval interval)
        {
            QueryModificator = adp =>
                adp.Professor.Id == person.Id
                && adp.PromotedAt >= interval.From
                && adp.PromotedAt <= interval.To;
        }

        /// <summary>
        ///     Количество ППС, которым присвоена ученое звание профессора (в проверяемый период)
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("DocAssignCount")]
        public double DoctorsAssignedCount()
        {
            return CountOf(adp => adp.TargetAcademicDegree == AcademicDegree.Professor);
        }

        /// <summary>
        ///     Количество ППС, которым присвоена ученое звание доцента (в проверяемый период)
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("DocentsAssignCount")]
        public double DocentsAssignedCount()
        {
            return CountOf(adp => adp.TargetAcademicDegree == AcademicDegree.Docent);
        }
    }
}