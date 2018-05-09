using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TourOfHeroesService.Infrastructure.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private DbSet<T> _dbSet;
        private DbContext _dbContext;

        public GenericRepository(DbContext context)
        {
            _dbContext = context;
            _dbSet = context.Set<T>();
        }

        public IQueryable<T> Query()
        {
            return Query(null);
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> predicate)
        {
            return predicate != null ? _dbSet.Where(predicate) : _dbSet;
        }

        public IQueryable<T> GetPage(IQueryable<T> query, int pageNumber, int pageSize)
        {
            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public T Get(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        public async Task<T> GetAsync(params object[] keyValues)
        {
            return await _dbSet.FindAsync(keyValues);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Attach(T entity)
        {
            _dbSet.Attach(entity);
        }

        public void Detach(T entity)
        {
            _dbContext.Entry<T>(entity).State = EntityState.Detached;
        }

        public void DetachAll()
        {
            IList<T> entityToDetached = _dbSet.Local.ToList();
            foreach (var entity in entityToDetached)
            {
                _dbContext.Entry<T>(entity).State = EntityState.Detached;
            }
        }

        public void Update(T entity)
        {
            if (_dbContext.Entry<T>(entity).State == EntityState.Detached)
            {
                _dbContext.Entry<T>(entity).State = EntityState.Modified;
            }
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public IList<TValue> GetEntityValues<TValue>(T entity, string propertyName)
        {
            IList<TValue> values = new List<TValue>();
            var entityEntry = _dbContext.Entry<T>(entity);
            values.Add(entityEntry.OriginalValues.GetValue<TValue>(propertyName));
            values.Add(entityEntry.CurrentValues.GetValue<TValue>(propertyName));
            return values;
        }
    }
}