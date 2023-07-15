﻿using ProductManagement.Domain.Commons;
using System.Linq.Expressions;

namespace ProductManagement.Data.IRepositories;
public interface IRepository<TEntity> where TEntity : Auditable
{
    ValueTask<TEntity> InsertAsync(TEntity entity);
    TEntity Update(TEntity entity);
    IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>> expression = null, string[] includes = null);
    ValueTask<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression, string[] includes = null);
    ValueTask<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression);

    ValueTask SaveAsync();
}