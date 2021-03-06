﻿using SQLite.Net.Async;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WhenToDig83.Data.Contracts;

namespace WhenToDig83.Data
{
    public class RepositoryAsync<T> : IRepositoryAsync<T> where T : class
    {

        private static readonly AsyncLock Mutex = new AsyncLock();
        private readonly SQLiteAsyncConnection _connection;


        public RepositoryAsync()
        {
            _connection = Xamarin.Forms.DependencyService.Get<ISQLite>().GetAsyncConnection();
            Initialise();
        }

        public AsyncTableQuery<T> AsQueryable()
        {
            throw new NotImplementedException();
        }

        public async Task<int> Delete(T entity)
        {
            using (await Mutex.LockAsync().ConfigureAwait(false))
            {
                return await _connection.DeleteAsync(entity);
            }
        }

       public async Task<List<T>> Get()
        {
            List<T> entityList = new List<T>();
            using (await Mutex.LockAsync().ConfigureAwait(false))
            {
                entityList = await _connection.Table<T>().ToListAsync().ConfigureAwait(false);
            }
            return entityList;           
        }

        public async Task<T> Get(Expression<Func<T, bool>> predicate)
        {
            T task;
            using (await Mutex.LockAsync().ConfigureAwait(false))
            {
                task = await _connection.FindAsync<T>(predicate).ConfigureAwait(false);
            }
            return task;
        }

        public async Task<T> Get(int id)
        {
            using (await Mutex.LockAsync().ConfigureAwait(false))
            {
                return await _connection.FindAsync<T>(id).ConfigureAwait(false);
            }
        }

        public async Task<List<T>> Get<TValue>(
            Expression<Func<T, bool>> predicate = null,
            string sortOrder = null,
            Expression<Func<T, TValue>> orderBy = null, 
            Expression<Func<T, TValue>> thenBy = null,
            int? take = null)
        {
            var query = _connection.Table<T>();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (sortOrder == "asc")
            {
                if (orderBy != null)
                {
                    if (thenBy != null)
                    {
                        query = query.OrderBy<TValue>(orderBy).ThenBy<TValue>(thenBy);
                    }
                    query = query.OrderBy<TValue>(orderBy);
                }
            }else if (sortOrder == "desc")
            {
                if (orderBy != null)
                {
                    if (thenBy != null)
                    {
                        query = query.OrderByDescending<TValue>(orderBy).ThenByDescending<TValue>(thenBy);
                    }
                    query = query.OrderByDescending<TValue>(orderBy);
                }
            }

            if (take != null) query = query.Take(take.GetValueOrDefault());

            return await query.ToListAsync().ConfigureAwait(false);
        }

        public async Task<int> Insert(T entity)
        {
            int entityId = 0;
            using (await Mutex.LockAsync().ConfigureAwait(false))
            {
                entityId = await _connection.InsertAsync(entity).ConfigureAwait(false);
            }
            return entityId;
        }

        public async Task<int> Update(T entity)
        {
            int entityId = 0;
            using (await Mutex.LockAsync().ConfigureAwait(false))
            {
                entityId = await _connection.UpdateAsync(entity).ConfigureAwait(false);
            }
            return entityId;
        }

        private async void Initialise()
        {
            using (await Mutex.LockAsync().ConfigureAwait(false))
            {
             //   await _connection.DeleteAllAsync<T>().ConfigureAwait(false);
                await _connection.CreateTableAsync<T>().ConfigureAwait(false);
            }
        }
    }
}
