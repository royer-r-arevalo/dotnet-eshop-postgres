﻿namespace Application.Data;

public interface IRepository<TEntity> where TEntity : class
{
    Task<List<TEntity>> GetAllAsync();

    Task<TEntity?> GetByIdAsync(Guid id);
    
    IQueryable<TEntity> GetQueryable();

    void Insert(TEntity entity);

    void Update(TEntity entity);

    void Delete(TEntity id);

    Task SaveChangesAsync();
}
