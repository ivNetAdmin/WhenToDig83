using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WhenToDig83.Data.Contracts;

namespace WhenToDig83.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly SQLiteConnection _connection;
        static object locker = new object();

        public Repository()
        {
            _connection = Xamarin.Forms.DependencyService.Get<ISQLite>().GetConnection();
            Initialise();
        }

        private void Initialise()
        {
            _connection.CreateTable<T>();
        }

        public int Delete(int id)
        {
            lock (locker)
            {
                return _connection.Delete<T>(id);
            }
        }

        public List<T> Get()
        {
            lock (locker)
            {
                return _connection.Table<T>().ToList();
            }
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public T Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<T> Get<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null)
        {
            throw new NotImplementedException();
        }

        public int Insert(T entity)
        {
            lock (locker)
            {
                return _connection.Insert(entity);
            } 
        }

        public int Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}