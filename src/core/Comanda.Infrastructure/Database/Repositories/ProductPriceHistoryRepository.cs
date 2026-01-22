namespace Comanda.Infrastructure.Database.Repositories;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Database.Entities;
using System;

public class ProductPriceHistoryRepository(Context context) : GenericDatabaseRepository<ProductPriceHistoryDatabaseEntity>(context), IProductPriceHistoryRepository
{
    public async Task<ProductPriceHistoryDatabaseEntity?> EffectiveOnAsync(DateTime date) => 
        await Query().SingleOrDefaultAsync(ph =>
            ph.EffectiveFrom <= date
            && (ph.EffectiveTo == null
                || ph.EffectiveTo > date));

    public async Task<ProductPriceHistoryDatabaseEntity?> CurrentlyEffectiveAsync(DateTime date) =>
        await Query().SingleOrDefaultAsync(ph => ph.EffectiveTo == null);
}








