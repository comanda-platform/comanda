namespace Comanda.Domain.Repositories;

using Comanda.Domain.Entities;
using Comanda.Shared.Enums;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(int id);
    Task<Order?> GetByPublicIdAsync(string publicId);
    Task<Order?> GetByOrderLinePublicIdAsync(string orderLinePublicId);
    Task<IEnumerable<Order>> GetAllAsync();
    Task<IEnumerable<Order>> GetActiveOrdersAsync();
    Task<IEnumerable<Order>> GetByStatusAsync(OrderStatus status);
    Task<IEnumerable<Order>> GetByClientIdAsync(int clientId);
    Task<IEnumerable<Order>> GetByClientPublicIdAsync(string clientPublicId);
    Task<IEnumerable<Order>> GetByClientGroupIdAsync(int clientGroupId);
    Task<IEnumerable<Order>> GetByClientGroupPublicIdAsync(string clientGroupPublicId);
    Task<IEnumerable<Order>> GetByDateRangeAsync(DateTime from, DateTime to);
    Task AddAsync(Order order);
    Task UpdateAsync(Order order);
}







