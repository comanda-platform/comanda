namespace Comanda.Infrastructure.Database;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using System.Linq.Expressions;

public class GenericDatabaseRepository<TEntity> : IGenericDatabaseRepository<TEntity>
    where TEntity : class
{
    private readonly Context _database;
    private readonly DbSet<TEntity> _set;

    public GenericDatabaseRepository(Context database)
    {
        _database = database;
        _set = _database.Set<TEntity>();
    }

    public IQueryable<TEntity> Query() 
        => _set.AsQueryable();

    public virtual async Task<TEntity?> GetByIdAsync(int id)
        => await _set.FindAsync(id);

    public virtual async Task<TEntity?> GetByPublicIdAsync(string publicId)
        => throw new NotImplementedException();

    public async Task<TEntity> GetFirstAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
        => await _set.FirstAsync(predicate, cancellationToken);

    public async Task<TEntity?> GetFirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
        => await _set.FirstOrDefaultAsync(predicate, cancellationToken);

    public async Task<TEntity> GetSingleAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
        => await _set.SingleAsync(predicate, cancellationToken);

    public async Task<TEntity?> GetSingleOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
        => await _set.SingleOrDefaultAsync(predicate, cancellationToken);

    public async Task<int> GetCountAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            CancellationToken cancellationToken = default)
        => predicate != null 
            ? await _set.CountAsync(predicate, cancellationToken) 
            : await _set.CountAsync(cancellationToken);

    public async Task<IEnumerable<TEntity>> GetManyAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default) 
        => await _set.Where(predicate).ToListAsync(cancellationToken);

    public async Task<IEnumerable<TEntity>> GetPageAsync(
        int skip,
        int take,
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
        => predicate != null 
            ? await _set.Where(predicate).Skip(skip).Take(take).ToListAsync(cancellationToken) 
            : await _set.AsQueryable().Skip(skip).Take(take).ToListAsync(cancellationToken);

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default) 
        => await _set.ToListAsync(cancellationToken);

    public async Task<bool> ExistsAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
        => await _set.AnyAsync(predicate, cancellationToken);

    public async Task AddAsync(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        await _set.AddAsync(entity, cancellationToken);
        await _database.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        _set.Update(entity);
        await _database.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        _set.Remove(entity);
        await _database.SaveChangesAsync(cancellationToken);
    }
}







