namespace Comanda.Infrastructure.Database.Repositories;

using Comanda.Database.Entities;

public interface IOrderRepository : IGenericDatabaseRepository<OrderDatabaseEntity>
{
    Task<OrderDatabaseEntity?> GetByPublicIdAsync(string publicId);
    Task<OrderDatabaseEntity?> GetByOrderLinePublicIdAsync(string orderLinePublicId);
    Task<IEnumerable<OrderDatabaseEntity>> GetActiveOrdersAsync();
    Task<IEnumerable<OrderDatabaseEntity>> GetByStatusAsync(int orderStatusTypeId);
    Task<IEnumerable<OrderDatabaseEntity>> GetByClientIdAsync(int clientId);
    Task<IEnumerable<OrderDatabaseEntity>> GetByClientPublicIdAsync(string clientPublicId);
    Task<IEnumerable<OrderDatabaseEntity>> GetByClientGroupIdAsync(int clientGroupId);
    Task<IEnumerable<OrderDatabaseEntity>> GetByClientGroupPublicIdAsync(string clientGroupPublicId);
    Task<IEnumerable<OrderDatabaseEntity>> GetByDateRangeAsync(DateTime from, DateTime to);
}







