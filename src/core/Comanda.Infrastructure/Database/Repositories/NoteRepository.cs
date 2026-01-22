namespace Comanda.Infrastructure.Database.Repositories;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Database.Entities;

public class NoteRepository(Context context) : GenericDatabaseRepository<NoteDatabaseEntity>(context), INoteRepository
{
    public override async Task<NoteDatabaseEntity?> GetByPublicIdAsync(string publicId) =>
        await Query().FirstOrDefaultAsync(n => n.PublicId == publicId);

    public async Task<IEnumerable<NoteDatabaseEntity>> GetByClientIdAsync(int clientId) => 
        await Query()
            .Where(n => n.ClientId == clientId)
            .ToListAsync();

    public async Task<IEnumerable<NoteDatabaseEntity>> GetByClientGroupIdAsync(int clientGroupId) =>
        await Query()
            .Where(n => n.ClientGroupId == clientGroupId)
            .ToListAsync();

    public async Task<IEnumerable<NoteDatabaseEntity>> GetByLocationIdAsync(int locationId) =>
        await Query()
            .Where(n => n.LocationId == locationId)
            .ToListAsync();

    public async Task<IEnumerable<NoteDatabaseEntity>> GetByOrderIdAsync(int orderId) =>
        await Query()
            .Where(n => n.OrderId == orderId)
            .ToListAsync();

    public async Task<IEnumerable<NoteDatabaseEntity>> GetByOrderLineIdAsync(int orderLineId) => 
        await Query()
            .Where(n => n.OrderLineId == orderLineId)
            .ToListAsync();

    public async Task<IEnumerable<NoteDatabaseEntity>> GetByProductIdAsync(int productId) =>
        await Query()
            .Where(n => n.ProductId == productId)
            .ToListAsync();

    public async Task<IEnumerable<NoteDatabaseEntity>> GetBySideIdAsync(int sideId) =>
        await Query()
            .Where(n => n.SideId == sideId)
            .ToListAsync();

    public async Task<IEnumerable<NoteDatabaseEntity>> GetByClientPublicIdAsync(string clientPublicId)
        => await Query()
            .Where(n => 
                n.Client != null 
                && n.Client.PublicId == clientPublicId)
            .ToListAsync();

    public async Task<IEnumerable<NoteDatabaseEntity>> GetByClientGroupPublicIdAsync(string clientGroupPublicId)
        => await Query()
            .Where(n => 
                n.ClientGroup != null 
                && n.ClientGroup.PublicId == clientGroupPublicId)
            .ToListAsync();

    public async Task<IEnumerable<NoteDatabaseEntity>> GetByLocationPublicIdAsync(string locationPublicId)
        => await Query()
            .Where(n => 
                n.Location != null
                && n.Location.PublicId == locationPublicId)
            .ToListAsync();

    public async Task<IEnumerable<NoteDatabaseEntity>> GetByOrderPublicIdAsync(string orderPublicId)
        => await Query()
            .Where(n => 
                n.Order != null
                && n.Order.PublicId == orderPublicId)
            .ToListAsync();

    public async Task<IEnumerable<NoteDatabaseEntity>> GetByOrderLinePublicIdAsync(string orderLinePublicId)
        => await Query()
            .Where(n => 
                n.OrderLine != null 
                && n.OrderLine.PublicId == orderLinePublicId)
            .ToListAsync();

    public async Task<IEnumerable<NoteDatabaseEntity>> GetByProductPublicIdAsync(string productPublicId)
        => await Query()
            .Where(n =>
                n.Product != null 
                && n.Product.PublicId == productPublicId)
            .ToListAsync();

    public async Task<IEnumerable<NoteDatabaseEntity>> GetBySidePublicIdAsync(string sidePublicId)
        => await Query()
            .Where(n => 
                n.Side != null 
                && n.Side.PublicId == sidePublicId)
            .ToListAsync();
}







