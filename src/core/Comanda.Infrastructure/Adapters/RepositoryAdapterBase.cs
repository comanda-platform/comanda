namespace Comanda.Infrastructure.Adapters;

public abstract class RepositoryAdapterBase<TDomain, TEntity>(Database.IGenericDatabaseRepository<TEntity> repository)
    where TDomain : class
    where TEntity : class
{
    protected readonly Database.IGenericDatabaseRepository<TEntity> Repository = repository;

    protected abstract TDomain FromPersistence(TEntity entity);
    protected abstract TEntity ToPersistence(TDomain domain);

    public virtual async Task<TDomain?> GetByIdAsync(int id)
        => (await Repository.GetByIdAsync(id)) is { } e ? FromPersistence(e) : null;

    public virtual async Task<IEnumerable<TDomain>> GetAllAsync()
        => (await Repository.GetAllAsync()).Select(FromPersistence);

    public virtual async Task AddAsync(TDomain domain)
        => await Repository.AddAsync(ToPersistence(domain));

    public virtual async Task DeleteAsync(int id)
    {
        var entity = await Repository.GetByIdAsync(id)
            ?? throw new InvalidOperationException("Entity not found");

        await Repository.DeleteAsync(entity);
    }
}







