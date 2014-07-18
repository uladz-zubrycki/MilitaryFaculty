using System;
using System.Linq;
using System.Linq.Expressions;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Reporting.Data
{
    public abstract class DataProvider<T> : IDataProvider where T : class, IUniqueEntity
    {
        private readonly IQueryable<T> _queryableCollection;

        public Expression<Func<T, bool>> QueryModificator;

        protected IQueryable<T> QueryableCollection
        {
            get
            {
                return QueryModificator == null ? _queryableCollection : _queryableCollection.Where(QueryModificator);
            }
        }

        protected DataProvider(IRepository<T> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            _queryableCollection = repository.Table;
        }

        protected DataProvider(IRepository<T> repository, Expression<Func<T, bool>> modificator)
            : this(repository)
        {
            QueryModificator = modificator;
        }

        protected int CountOf(Func<T, bool> predicate)
        {
            return QueryableCollection.Count(predicate);
        }

        protected double SumOf(Func<T, bool> selector, Func<T, double> evaluator)
        {
            return QueryableCollection.Where(selector).Sum(evaluator);
        }
    }
}