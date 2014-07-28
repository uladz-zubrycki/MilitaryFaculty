using MilitaryFaculty.Data;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Reporting.Data.DataProviders
{
    public class PublicationsDataProvider : DataProvider<Publication>
    {
        public PublicationsDataProvider(IRepository<Publication> publicationRepository)
            : base(publicationRepository)
        {
        }

        public override void SetFacultyModificator(TimeInterval interval)
        {
            QueryModificator = publication =>
                publication.CreatedAt >= interval.From
                && publication.CreatedAt <= interval.To;
        }

        public override void SetCathedraModificator(Cathedra cathedra, TimeInterval interval)
        {
            QueryModificator = publication =>
                publication.Author.Cathedra.Id == cathedra.Id
                && publication.CreatedAt >= interval.From
                && publication.CreatedAt <= interval.To;
        }

        public override void SetPersonModificator(Person person, TimeInterval interval)
        {
            QueryModificator = publication =>
                publication.Author.Id == person.Id
                && publication.CreatedAt >= interval.From
                && publication.CreatedAt <= interval.To;
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