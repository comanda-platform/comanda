namespace Comanda.Infrastructure.Database;

using System.Linq.Expressions;

public interface IGenericDatabaseRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> Query();

    Task<TEntity?> GetByIdAsync(int id);

    Task<TEntity> GetFirstAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default);

    Task<TEntity?> GetFirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default);

    Task<TEntity> GetSingleAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default);

    Task<TEntity?> GetSingleOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default);

    Task<int> GetCountAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> GetPageAsync(
        int skip,
        int take,
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> GetManyAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        TEntity entity,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        TEntity entity,
        CancellationToken cancellationToken = default);
}







