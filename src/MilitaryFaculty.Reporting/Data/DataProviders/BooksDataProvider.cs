using MilitaryFaculty.Data;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Reporting.Data.DataProviders
{
    public class BooksDataProvider : DataProvider<Book>
    {
        public BooksDataProvider(IRepository<Book> repository)
            : base(repository)
        {
        }

        public override void SetFacultyModificator(TimeInterval interval)
        {
            QueryModificator = book =>
                book.CreatedAt >= interval.From
                && book.CreatedAt <= interval.To;
        }

        public override void SetCathedraModificator(Cathedra cathedra, TimeInterval interval)
        {
            QueryModificator = book =>
                book.Author.Cathedra.Id == cathedra.Id
                && book.CreatedAt >= interval.From
                && book.CreatedAt <= interval.To;
        }

        public override void SetPersonModificator(Person person, TimeInterval interval)
        {
            QueryModificator = book =>
                book.Author.Id == person.Id
                && book.CreatedAt >= interval.From
                && book.CreatedAt <= interval.To;
        }

        /// <summary>
        ///     Количество условно-печатных листов изданных учебников
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("TotalBooksPc")]
        public double TotalBooksPagesCount()
        {
            return SumOf(b => b.BookType == BookType.Schoolbook, b => b.PagesCount);
        }

        /// <summary>
        ///     Количество условно-печатных листов изданных учебных пособий
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("TotalTutorialsPc")]
        public double TotalTutorialsPagesCount()
        {
            return SumOf(b => b.BookType == BookType.Tutorial, b => b.PagesCount);
        }
    }
}