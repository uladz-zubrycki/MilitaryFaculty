using System;
using System.Linq;
using System.Linq.Expressions;
using MilitaryFaculty.Data;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Domain.Base;

namespace MilitaryFaculty.Reporting.Data
{
    public abstract class DataProvider<T> : IDataProvider 
        where T : class, IUniqueEntity
    {
        public abstract void SetFacultyModificator(TimeInterval interval);
        public abstract void SetCathedraModificator(Cathedra cathedra, TimeInterval interval);
        public abstract void SetProfessorModificator(Professor professor, TimeInterval interval);

        private readonly IQueryable<T> _queryableCollection;
        protected Expression<Func<T, bool>> QueryModificator;
        protected IQueryable<T> QueryableCollection
        {
            get
            {
                return QueryModificator == null ? _queryableCollection : _queryableCollection.Where(QueryModificator);
            }
        }

        protected DataProvider(IRepository<T> repository)
        {
            _queryableCollection = repository.Table;
            QueryModificator = null;
        }

        protected int CountOf(Func<T, bool> predicate)
        {
            return QueryableCollection.Count(predicate);
        }

        protected double SumOf(Func<T, bool> predicate, Func<T, double> evaluator)
        {
            return QueryableCollection.Where(predicate).Sum(evaluator);
        }
    }
}