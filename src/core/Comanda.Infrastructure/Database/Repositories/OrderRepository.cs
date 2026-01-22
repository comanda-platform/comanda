namespace Comanda.Infrastructure.Database.Repositories;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Database.Entities;
using Comanda.Shared.Enums;

public class OrderRepository(Context context)
    : GenericDatabaseRepository<OrderDatabaseEntity>(context), IOrderRepository
{
    private static readonly HashSet<int> ActiveStatusIds =
    [
        (int)OrderStatus.Created,
        (int)OrderStatus.Accepted,
        (int)OrderStatus.Preparing,
        (int)OrderStatus.Ready,
        (int)OrderStatus.InTransit
    ];

    // Override Query to always include Lines navigation property
    public new IQueryable<OrderDatabaseEntity> Query() =>
        base.Query().Include(o => o.Lines);

    public override async Task<OrderDatabaseEntity?> GetByPublicIdAsync(string publicId) =>
        await Query().FirstOrDefaultAsync(o => o.PublicId == publicId);

    public async Task<OrderDatabaseEntity?> GetByOrderLinePublicIdAsync(string orderLinePublicId) =>
        await Query().FirstOrDefaultAsync(o => o.Lines.Any(l => l.PublicId == orderLinePublicId));

    public async Task<IEnumerable<OrderDatabaseEntity>> GetActiveOrdersAsync() => 
        await Query()
            .Where(o => ActiveStatusIds.Contains(o.OrderStatusTypeId))
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

    public async Task<IEnumerable<OrderDatabaseEntity>> GetByStatusAsync(int orderStatusTypeId) =>
        await Query()
            .Where(o => o.OrderStatusTypeId == orderStatusTypeId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

    public async Task<IEnumerable<OrderDatabaseEntity>> GetByClientIdAsync(int clientId) =>
        await Query()
            .Where(o => o.ClientId == clientId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

    public async Task<IEnumerable<OrderDatabaseEntity>> GetByClientPublicIdAsync(string clientPublicId) =>
        await Query()
            .Where(o => 
                o.Client != null 
                && o.Client.PublicId == clientPublicId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

    public async Task<IEnumerable<OrderDatabaseEntity>> GetByClientGroupIdAsync(int clientGroupId) =>
        await Query()
            .Where(o => o.ClientGroupId == clientGroupId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

    public async Task<IEnumerable<OrderDatabaseEntity>> GetByClientGroupPublicIdAsync(string clientGroupPublicId) =>
        await Query()
            .Where(o => 
                o.ClientGroup != null 
                && o.ClientGroup.PublicId == clientGroupPublicId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

    public async Task<IEnumerable<OrderDatabaseEntity>> GetByDateRangeAsync(DateTime from, DateTime to) =>
        await Query()
            .Where(o => o.CreatedAt >= from && o.CreatedAt <= to)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
}







