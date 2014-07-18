using System;
using System.Linq.Expressions;
using MilitaryFaculty.Data;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Reporting.Data.DataProviders
{
    public class BooksDataProvider : DataProvider<Book>
    {
        public BooksDataProvider(IRepository<Book> repository)
            : base(repository)
        {
        }

        public BooksDataProvider(IRepository<Book> repository,
            Expression<Func<Book, bool>> modificator)
            : base(repository, modificator)
        {
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