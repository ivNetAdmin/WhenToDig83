using SQLite.Net.Async;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WhenToDig83.Data.Contracts
{

    public interface IRepositoryAsync<T> where T : class
    {
        Task<List<T>> Get();
        Task<T> Get(int id);
        Task<List<T>> Get<TValue>(Expression<Func<T, bool>> predicate = null, string sortOrder = null, Expression<Func<T, TValue>> orderBy = null, Expression<Func<T, TValue>> thenBy = null, int? take = null);
        Task<T> Get(Expression<Func<T, bool>> predicate);
        AsyncTableQuery<T> AsQueryable();
        Task<int> Insert(T entity);
        Task<int> Update(T entity);
        Task<int> Delete(T entity);
    }
}
