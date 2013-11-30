using System;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;
using System.Linq;

namespace MilitaryFaculty.Logic.DataProviders
{
    public class PublicationsDataProvider : IDataProvider
    {
        #region Class Fields

        private readonly IPublicationRepository publicationRepository;
        
        #endregion // Class Fields

        #region Class Constructors

        public PublicationsDataProvider(IPublicationRepository publicationRepository)
        {
            if (publicationRepository == null)
            {
                throw new ArgumentNullException("publicationRepository");
            }

            this.publicationRepository = publicationRepository;
        }

        #endregion // Class Constructors
        
        #region Class Public Argument Methods

        /// <summary>
        /// Количество условно-печатных листов изданных учебников
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("TotalBooksPc")]
        public double TotalBooksPagesCount()
        {
            return 0;
        }

        /// <summary>
        /// Количество условно-печатных листов изданных учебных пособий
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("TotalTutorialsPc")]
        public double TotalTutorialsPagesCount()
        {
            return 1;
        }

        /// <summary>
        /// Количество условно-печатных листов изданных монографий
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("MonographyPc")]
        public double MonographyPagesCount()
        {
            return publicationRepository.All().Sum(p => p.PagesCount);
        }

        /// <summary>
        /// Издано научных статей в научных рецензируемых журналах
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ReviewedArticlesCount")]
        public double ReviewedArticlesCount()
        {
            return publicationRepository.All().Count(p => p.PublicationType == PublicationType.ReviewedArticle);
        }

        /// <summary>
        /// Издано научных статей в научных нерецензируемых журналах
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ArticlesCount")]
        public double ArticlesCount()
        {
            return publicationRepository.All().Count(p => p.PublicationType == PublicationType.Article);
        }

        /// <summary>
        /// Издано тезисов докладов в сборниках материалов научных конференций
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ThesisesCount")]
        public double LecturesCount()
        {
            return publicationRepository.All().Count(p => p.PublicationType == PublicationType.Thesis);
        }

        /// <summary>
        /// Количество ППС, которым присвоена ученая степень доктора наук (в проверяемый период)
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("DocThesisesCount")]
        public double DoctorThesisesCount()
        {
            return 1;
        }

        /// <summary>
        /// Количество ППС, которым присвоена ученая степень кандидата наук (в проверяемый период)
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("CandThesisesCount")]
        public double CandidateThesisesCount()
        {
            return 2;
        }

        /// <summary>
        /// Количество ППС, которым присвоена ученое звание профессора (в проверяемый период)
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("DocAssignCount")]
        public double DoctorsAssignedCount()
        {
            return 3;
        }

        /// <summary>
        /// Количество ППС, которым присвоена ученое звание доцента (в проверяемый период)
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("DocentsAssignCount")]
        public double DocentsAssignedCount()
        {
            return 4;
        }

        /// <summary>
        /// Подготовлено экспертных заключений по диссертациям: докторским
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("DocEoCount")]
        public double DoctorsOfScienceExpertOpinionsCount()
        {
            return 5;
        }

        /// <summary>
        /// Подготовлено экспертных заключений по диссертациям: кандидатским
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("CandEoCount")]
        public double CandidatsExpertOpinionsCount()
        {
            return 1;
        }

        /// <summary>
        /// Отзывов на авторефераты диссертаций: докторских
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("EssayReviewDosCount")]
        public double EssayReviewsByDosCount()
        {
            return 3;
        }

        /// <summary>
        /// Отзывов на авторефераты диссертаций: кандидатских
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("EssayReviewCanCount")]
        public double EssayReviewByCandidatsCount()
        {
            return 4;
        }

        /// <summary>
        /// Количество ППС, участвующих во всех НИР
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("SrwProfsCount")]
        public double ScientificResearchWorkProfessorsCount()
        {
            return 1;
        }

        /// <summary>
        /// Количество выполненных НИР
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("SrwCount")]
        public double ScientificResearchWorksCount()
        {
            return 3;
        }

        /// <summary>
        /// Количество проведенных военно-научных сопровождений
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("MssCount")]
        public double MilitaryScientificSupportsCount()
        {
            return 4;
        }

        /// <summary>
        /// Количество заявок на изобретение
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("InnCount")]
        public double InnovationsCount()
        {
            return 2;
        }

        /// <summary>
        /// Количество заявок на полезную модель
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("UsModCount")]
        public double UsefulModelsCount()
        {
            return 3;
        }

        /// <summary>
        /// Количество положительных ответов на заявки на изобретение
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("PosInnCount")]
        public double PositiveInnovationsCount()
        {
            return 4;
        }

        /// <summary>
        /// Количество положительных ответов на завку на полезную модель
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("PosUsModCount")]
        public double PositiveUsefulModelsCount()
        {
            return 5;
        }

        /// <summary>
        /// Количество принятих рационализаторских предложений
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("RationPropCount")]
        public double RationalizationProposalsCount()
        {
            return 1;
        }

        #endregion // Class Public Argument Methods
    }
}
