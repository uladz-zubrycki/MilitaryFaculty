using System;
using System.Linq.Expressions;
using MilitaryFaculty.Data;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Reporting.Data.DataProviders
{
    public class DissertationWorksDataProvider : DataProvider<DissertationWork>
    {
        public DissertationWorksDataProvider(IRepository<DissertationWork> repository)
            : base(repository)
        {
        }

        public DissertationWorksDataProvider(IRepository<DissertationWork> repository,
            Expression<Func<DissertationWork, bool>> modificator)
            : base(repository, modificator)
        {
        }

        /// <summary>
        ///     Количество ППС, которым присвоена ученая степень доктора наук (в проверяемый период)
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("DocThesisesCount")]
        public double DoctorThesisesCount()
        {
            return CountOf(s => s.SynopsisType == DissertationWorkType.Dissertation
                                && s.SynopsisDegree == DissertationWorkDegree.Doctor);
        }

        /// <summary>
        ///     Количество ППС, которым присвоена ученая степень кандидата наук (в проверяемый период)
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("CandThesisesCount")]
        public double CandidateThesisesCount()
        {
            return CountOf(s => s.SynopsisType == DissertationWorkType.Dissertation
                                && s.SynopsisDegree == DissertationWorkDegree.Candidate);
        }

        /// <summary>
        ///     Подготовлено экспертных заключений по диссертациям: докторским
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("DocEoCount")]
        public double DoctorsOfScienceExpertOpinionsCount()
        {
            return CountOf(s => s.SynopsisType == DissertationWorkType.OpinionOnTheDissertation
                                && s.SynopsisDegree == DissertationWorkDegree.Doctor);
        }

        /// <summary>
        ///     Подготовлено экспертных заключений по диссертациям: кандидатским
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("CandEoCount")]
        public double CandidatsExpertOpinionsCount()
        {
            return CountOf(s => s.SynopsisType == DissertationWorkType.OpinionOnTheDissertation
                                && s.SynopsisDegree == DissertationWorkDegree.Candidate);
        }

        /// <summary>
        ///     Отзывов на авторефераты диссертаций: докторских
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("EssayReviewDosCount")]
        public double EssayReviewsByDosCount()
        {
            return CountOf(s => s.SynopsisType == DissertationWorkType.ReviewedOnSynopsis
                                && s.SynopsisDegree == DissertationWorkDegree.Doctor);
        }

        /// <summary>
        ///     Отзывов на авторефераты диссертаций: кандидатских
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("EssayReviewCanCount")]
        public double EssayReviewByCandidatsCount()
        {
            return CountOf(s => s.SynopsisType == DissertationWorkType.ReviewedOnSynopsis
                                && s.SynopsisDegree == DissertationWorkDegree.Candidate);
        }
    }
}