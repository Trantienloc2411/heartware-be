using System;
using System.Data;
using System.Linq.Expressions;
using BusinessObjects.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Repository.Implement;

public class GenericRepositiory<TEntity> : IGenericRepository<TEntity> where TEntity : class
{



    protected MyDbContext _context;
    protected DbSet<TEntity> _dbSet;

#pragma warning disable IDE0290 // Use primary constructor
    public GenericRepositiory(MyDbContext context)
#pragma warning restore IDE0290 // Use primary constructor
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

#pragma warning disable CS8767 // Nullability of reference types in type of parameter doesn'TEntity match implicitly implemented member (possibly because of nullability attributes).
#pragma warning disable CS8767 // Nullability of reference types in type of parameter doesn'TEntity match implicitly implemented member (possibly because of nullability attributes).
    public virtual IEnumerable<TEntity> Get(
#pragma warning restore CS8767 // Nullability of reference types in type of parameter doesn'TEntity match implicitly implemented member (possibly because of nullability attributes).
#pragma warning restore CS8767 // Nullability of reference types in type of parameter doesn'TEntity match implicitly implemented member (possibly because of nullability attributes).
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
       Expression<Func<TEntity, bool>> filter = null,
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
       string includeProperties = "",
       int? pageIndex = null,
       int? pageSize = null)
    {
        IQueryable<TEntity> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        if (pageIndex.HasValue && pageSize.HasValue)
        {
            int validPageIndex = pageIndex.Value > 0 ? pageIndex.Value - 1 : 0;
            int validPageSize = pageSize.Value > 0 ? pageSize.Value : 10;

            query = query.Skip(validPageIndex * validPageSize).Take(validPageSize);
        }

        return query.ToList();
    }

    public virtual TEntity GetByID(object id)
    {
#pragma warning disable CS8603 // Possible null reference return.
        return _dbSet.Find(id);
#pragma warning restore CS8603 // Possible null reference return.
    }

    public virtual void Insert(TEntity entity)
    {
        if (entity == null) return;
        _dbSet.Add(entity);
    }

    public virtual bool Delete(object id)
    {
        TEntity entityToDelete = GetByID(id);
        if (entityToDelete == null) return false;
        Delete(entityToDelete);
        return true;
    }
    public virtual bool Update(object id, TEntity entityUpdate)
    {
        TEntity entity = GetByID(id);
        if (entity == null) return false;
        Update(entityUpdate);
        return true;
    }

    public virtual void Delete(TEntity entityToDelete)
    {
        if (_context.Entry(entityToDelete).State == EntityState.Detached)
        {
            _dbSet.Attach(entityToDelete);
        }
        _dbSet.Remove(entityToDelete);
    }

    public virtual void Update(TEntity entityToUpdate)
    {
        var trackedEntities = _context.ChangeTracker.Entries<TEntity>().ToList();
        foreach (var trackedEntity in trackedEntities)
        {
            trackedEntity.State = EntityState.Detached;
        }
        _dbSet.Attach(entityToUpdate);
        _context.Entry(entityToUpdate).State = EntityState.Modified;
    }

    public IEnumerable<TEntity> GetAll()
    {
        return _dbSet.ToList();
    }

    public async Task<ICollection<TEntity>> GetAllWithIncludeAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = _dbSet;

        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }
        return await query.Where(predicate).ToListAsync();
    }

    public async Task<TEntity> GetSingleWithIncludeAsync(
        Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = _dbSet;

        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }

#pragma warning disable CS8603 // Possible null reference return.
        return await query.FirstOrDefaultAsync(predicate);
#pragma warning restore CS8603 // Possible null reference return.
    }

    public void AddRange(ICollection<TEntity> entities)
    {
        _dbSet.AddRange(entities);
    }
    
    public IEnumerable<TResult> ExecuteStoredProcedure<TResult>(string storedProcedure, params SqlParameter[] parameters) where TResult : class, new()
    {
        using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = storedProcedure;
            command.CommandType = CommandType.StoredProcedure;

            if (parameters != null && parameters.Length > 0)
            {
                command.Parameters.AddRange(parameters);
            }

            _context.Database.OpenConnection();

            using (var result = command.ExecuteReader())
            {
                var entities = new List<TResult>();
                while (result.Read())
                {
                    var entity = new TResult();
                    foreach (var property in typeof(TResult).GetProperties())
                    {
                        if (!result.IsDBNull(result.GetOrdinal(property.Name)))
                        {
                            property.SetValue(entity, result[property.Name]);
                        }
                    }
                    entities.Add(entity);
                }
                return entities;
            }
        }
    }

}
