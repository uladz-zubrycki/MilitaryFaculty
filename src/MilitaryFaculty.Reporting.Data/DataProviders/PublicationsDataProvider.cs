using System;
using System.Linq.Expressions;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Reporting.Data.DataProviders
{
    public class PublicationsDataProvider : DataProvider<Publication>
    {
        public PublicationsDataProvider(IRepository<Publication> publicationRepository)
            : base(publicationRepository)
        {
        }

        public PublicationsDataProvider(IRepository<Publication> repository,
            Expression<Func<Publication, bool>> modificator)
            : base(repository, modificator)
        {
        }

        /// <summary>
        ///     Количество условно-печатных листов изданных монографий
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("MonographyPc")]
        public double MonographyPagesCount()
        {
            return SumOf(p => p.PublicationType == PublicationType.Monograph, p => p.PagesCount);
        }

        /// <summary>
        ///     Издано научных статей в научных рецензируемых журналах
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ReviewedArticlesCount")]
        public double ReviewedArticlesCount()
        {
            return CountOf(p => p.PublicationType == PublicationType.ReviewedArticle);
        }

        /// <summary>
        ///     Издано научных статей в научных нерецензируемых журналах
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ArticlesCount")]
        public double ArticlesCount()
        {
            return CountOf(p => p.PublicationType == PublicationType.Article);
        }

        /// <summary>
        ///     Издано тезисов докладов в сборниках материалов научных конференций
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ThesisesCount")]
        public double LecturesCount()
        {
            return CountOf(p => p.PublicationType == PublicationType.Thesis);
        }
    }
}