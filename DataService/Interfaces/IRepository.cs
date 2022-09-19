using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataService
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    }
}
