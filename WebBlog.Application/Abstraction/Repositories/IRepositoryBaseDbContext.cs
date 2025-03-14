﻿using System.Linq.Expressions;

namespace WebBlog.Application.Abstraction
{
    public interface IRepositoryBaseDbContext
    {
        Task<List<T>> GetAsync<T>(Expression<Func<T, bool>> predicate = null) where T : class;
        Task<List<R>> GetAsync<T, R>(Expression<Func<T, R>> selector, Expression<Func<T, bool>> predicate = null) where T : class;
        IQueryable<T?> GetSet<T>(Expression<Func<T, bool>> predicate = null) where T : class;
        IQueryable<T?> GetSetAsTracking<T>(Expression<Func<T, bool>> predicate = null) where T : class;
        Task<List<T>> GetWithOrderAsync<T>(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null) where T : class;
        Task<T?> FindAsync<T>(Expression<Func<T, bool>> predicate) where T : class;
        Task<T?> FindForUpdateAsync<T>(Expression<Func<T, bool>> predicate) where T : class;
        Task<bool> AnyAsync<T>(Expression<Func<T, bool>> predicate) where T : class;
        Task<T> AddAsync<T>(T entity, bool clearTracker = false) where T : class;
        Task<int> AddRangeAsync<T>(IEnumerable<T> entities, bool clearTracker = false) where T : class;
        Task<T> UpdateAsync<T>(T entity, bool clearTracker = false) where T : class;
        Task<int> UpdateRangeAsync<T>(IEnumerable<T> entities, bool clearTracker = false) where T : class;
        Task<int> DeleteAsync<T>(T entity, bool clearTracker = false) where T : class;
        Task<int> DeleteRangeAsync<T>(IEnumerable<T> entities, bool clearTracker = false) where T : class;

        //#region Cache
        //Task<T> FindFromCacheAsync<T>(Expression<Func<T, bool>> predicate = null) where T : class;
        //Task<List<T>> GetFromCacheAsync<T>(Expression<Func<T, bool>> predicate = null) where T : class;
        //#endregion
    }
}
