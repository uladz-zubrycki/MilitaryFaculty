using System;
using System.Linq;
using System.Linq.Expressions;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain.Contract;

namespace MilitaryFaculty.Reporting.Data
{
	public abstract class DataProvider<T> : IDataProvider where T : class, IUniqueEntity
	{
		protected IQueryable<T> QueryableCollection;
		protected Expression<Func<T, bool>> QueryModificator;

		protected DataProvider(IRepository<T> repository)
		{
			if (repository == null)
			{
				throw new ArgumentNullException("repository");
			}

			QueryableCollection = repository.Table;
		}

		protected DataProvider(IRepository<T> repository, Expression<Func<T, bool>> modificator)
			: this(repository)
		{
			QueryableCollection = QueryableCollection.Where(modificator);
		}

		protected int CountOf(Func<T, bool> predicate)
		{
			return QueryableCollection.Count(predicate);
		}

		protected double SumOf(Func<T, double> evaluator)
		{
			return QueryableCollection.Sum(evaluator);
		}
	}
}