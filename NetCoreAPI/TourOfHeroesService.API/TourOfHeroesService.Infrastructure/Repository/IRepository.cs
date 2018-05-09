using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TourOfHeroesService.Infrastructure.Repository
{
    public interface IRepository<T>
    {
        IQueryable<T> Query();
        IQueryable<T> Query(Expression<Func<T, Boolean>> predicate);
        IQueryable<T> GetPage(IQueryable<T> query, int pageNumber, int pageSize);

        T Get(params object[] keyValues);
        Task<T> GetAsync(params object[] keyValues);
        
        void Add(T entity);
        void Attach(T entity);
        void Detach(T entity);
        void DetachAll();
        void Update(T entity);
        void Delete(T entity);
       
        IList<TValue> GetEntityValues<TValue>(T entity, string propertyName);
    }
}