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

        public Task<int> Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<T> Get(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<T> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> Get<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null)
        {
            var query = _connection.Table<T>();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (orderBy != null)
            {
                query = query.OrderBy<TValue>(orderBy);
            }

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

        public Task<int> Update(T entity)
        {
            throw new NotImplementedException();
        }

        private async void Initialise()
        {
            using (await Mutex.LockAsync().ConfigureAwait(false))
            {
                // await _connection.DeleteAllAsync<T>().ConfigureAwait(false);
                await _connection.CreateTableAsync<T>().ConfigureAwait(false);
            }
        }

   
    }
}