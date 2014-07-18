using System;
using System.Linq.Expressions;
using MilitaryFaculty.Data;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Domain.Synopsis;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Reporting.Data.DataProviders
{
    public class SynopsesDataProvider : DataProvider<Synopsis>
    {
        public SynopsesDataProvider(IRepository<Synopsis> repository)
            : base(repository)
        {
        }

        public SynopsesDataProvider(IRepository<Synopsis> repository,
            Expression<Func<Synopsis, bool>> modificator)
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
            return CountOf(s => s.SynopsisType == SynopsisType.Dissertation
                                && s.SynopsisDegree == SynopsisDegree.Doctor);
        }

        /// <summary>
        ///     Количество ППС, которым присвоена ученая степень кандидата наук (в проверяемый период)
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("CandThesisesCount")]
        public double CandidateThesisesCount()
        {
            return CountOf(s => s.SynopsisType == SynopsisType.Dissertation
                                && s.SynopsisDegree == SynopsisDegree.Candidate);
        }

        /// <summary>
        ///     Подготовлено экспертных заключений по диссертациям: докторским
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("DocEoCount")]
        public double DoctorsOfScienceExpertOpinionsCount()
        {
            return CountOf(s => s.SynopsisType == SynopsisType.OpinionOnTheDissertation
                                && s.SynopsisDegree == SynopsisDegree.Doctor);
        }

        /// <summary>
        ///     Подготовлено экспертных заключений по диссертациям: кандидатским
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("CandEoCount")]
        public double CandidatsExpertOpinionsCount()
        {
            return CountOf(s => s.SynopsisType == SynopsisType.OpinionOnTheDissertation
                                && s.SynopsisDegree == SynopsisDegree.Candidate);
        }

        /// <summary>
        ///     Отзывов на авторефераты диссертаций: докторских
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("EssayReviewDosCount")]
        public double EssayReviewsByDosCount()
        {
            return CountOf(s => s.SynopsisType == SynopsisType.ReviewedOnSynopsis
                                && s.SynopsisDegree == SynopsisDegree.Doctor);
        }

        /// <summary>
        ///     Отзывов на авторефераты диссертаций: кандидатских
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("EssayReviewCanCount")]
        public double EssayReviewByCandidatsCount()
        {
            return CountOf(s => s.SynopsisType == SynopsisType.ReviewedOnSynopsis
                                && s.SynopsisDegree == SynopsisDegree.Candidate);
        }
    }
}