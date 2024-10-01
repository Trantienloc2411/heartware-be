using System;
using System.Linq.Expressions;

namespace Repository.Implement;

public interface IGenericRepository<T> where T : class
{
    IEnumerable<T> Get(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "",
            int? pageIndex = null,
            int? pageSize = null);

        T GetByID(object id);

        void Insert(T entity);

        bool Delete(object id);

        void Delete(T entityToDelete);
        bool Update(object id, T entityToUpdate);
        void Update(T entityToUpdate);

        IEnumerable<T> GetAll();

        Task<T> GetSingleWithIncludeAsync(
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties);
        Task<ICollection<T>> GetAllWithIncludeAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        void AddRange(ICollection<T> entities);
}
