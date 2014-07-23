using System;
using System.Linq.Expressions;
using MilitaryFaculty.Common;
using MilitaryFaculty.Data;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Reporting.Data.DataProviders
{
    public class DissertationWorksDataProvider : DataProvider<Dissertation>
    {
        public DissertationWorksDataProvider(IRepository<Dissertation> repository)
            : base(repository)
        {
        }

        public DissertationWorksDataProvider(IRepository<Dissertation> repository,
            Expression<Func<Dissertation, bool>> modificator)
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
            throw new NotImplementedException();
            //return CountOf(s => s.SynopsisType == DissertationWorkType.Dissertation
            //                    && s.SynopsisDegree == DissertationWorkDegree.Doctor);
        }

        /// <summary>
        ///     Количество ППС, которым присвоена ученая степень кандидата наук (в проверяемый период)
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("CandThesisesCount")]
        public double CandidateThesisesCount()
        {
            throw new NotImplementedException();
            //return CountOf(s => s.SynopsisType == DissertationWorkType.Dissertation
            //                    && s.SynopsisDegree == DissertationWorkDegree.Candidate);
        }

        /// <summary>
        ///     Подготовлено экспертных заключений по диссертациям: докторским
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("DocEoCount")]
        public double DoctorsOfScienceExpertOpinionsCount()
        {
            throw new NotImplementedException();
            //return CountOf(s => s.SynopsisType == DissertationWorkType.OpinionOnTheDissertation
            //                    && s.SynopsisDegree == DissertationWorkDegree.Doctor);
        }

        /// <summary>
        ///     Подготовлено экспертных заключений по диссертациям: кандидатским
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("CandEoCount")]
        public double CandidatsExpertOpinionsCount()
        {
            throw new NotImplementedException();
            //return CountOf(s => s.SynopsisType == DissertationWorkType.OpinionOnTheDissertation
            //                    && s.SynopsisDegree == DissertationWorkDegree.Candidate);
        }

        /// <summary>
        ///     Отзывов на авторефераты диссертаций: докторских
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("EssayReviewDosCount")]
        public double EssayReviewsByDosCount()
        {
            throw new NotImplementedException();
            //return CountOf(s => s.SynopsisType == DissertationWorkType.ReviewedOnSynopsis
            //                    && s.SynopsisDegree == DissertationWorkDegree.Doctor);
        }

        /// <summary>
        ///     Отзывов на авторефераты диссертаций: кандидатских
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("EssayReviewCanCount")]
        public double EssayReviewByCandidatsCount()
        {
            throw new NotImplementedException();

            //return CountOf(s => s.SynopsisType == DissertationWorkType.ReviewedOnSynopsis
            //                    && s.SynopsisDegree == DissertationWorkDegree.Candidate);
        }
    }
}