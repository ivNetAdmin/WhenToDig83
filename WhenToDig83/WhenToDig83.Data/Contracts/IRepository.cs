using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace WhenToDig83.Data.Contracts
{
    public interface IRepository<T> where T : class
    {
        List<T> Get();
        T Get(int id);
        List<T> Get<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null);
        T Get(Expression<Func<T, bool>> predicate);
        int Insert(T entity);
        int Update(T entity);
        int Delete(int id);
    }
}