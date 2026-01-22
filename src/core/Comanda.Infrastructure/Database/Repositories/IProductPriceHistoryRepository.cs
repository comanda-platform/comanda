namespace Comanda.Infrastructure.Database.Repositories;

using Comanda.Database.Entities;

public interface IProductPriceHistoryRepository : IGenericDatabaseRepository<ProductPriceHistoryDatabaseEntity> 
{
    Task<ProductPriceHistoryDatabaseEntity?> EffectiveOnAsync(DateTime date);
    Task<ProductPriceHistoryDatabaseEntity?> CurrentlyEffectiveAsync(DateTime date);
}







