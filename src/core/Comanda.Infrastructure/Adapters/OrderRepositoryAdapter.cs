namespace Comanda.Infrastructure.Adapters;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Database.Entities;
using Comanda.Domain.Entities;
using Comanda.Shared.Enums;
using Comanda.Infrastructure.Mappers;
using Comanda.Domain;

public class OrderRepositoryAdapter(
    Database.Repositories.IOrderRepository databaseRepository,
    Context context) : Domain.Repositories.IOrderRepository
{
    private readonly Database.Repositories.IOrderRepository _databaseRepository = databaseRepository;
    private readonly Context _context = context;

    public async Task<Order?> GetByIdAsync(int id)
    {
        var entity = await _databaseRepository.GetByIdAsync(id);

        return entity?.FromPersistence();
    }

    public async Task<Order?> GetByPublicIdAsync(string publicId)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(publicId);

        return entity?.FromPersistence();
    }

    public async Task<Order?> GetByOrderLinePublicIdAsync(string orderLinePublicId)
    {
        var entity = await _databaseRepository.GetByOrderLinePublicIdAsync(orderLinePublicId);

        return entity?.FromPersistence();
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        var entities = await _databaseRepository.GetAllAsync();

        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<Order>> GetActiveOrdersAsync()
    {
        var entities = await _databaseRepository.GetActiveOrdersAsync();

        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<Order>> GetByStatusAsync(OrderStatus status)
    {
        var entities = await _databaseRepository.GetByStatusAsync((int)status);

        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<Order>> GetByClientIdAsync(int clientId)
    {
        var entities = await _databaseRepository.GetByClientIdAsync(clientId);

        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<Order>> GetByClientPublicIdAsync(string clientPublicId)
    {
        var entities = await _databaseRepository.GetByClientPublicIdAsync(clientPublicId);

        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<Order>> GetByClientGroupIdAsync(int clientGroupId)
    {
        var entities = await _databaseRepository.GetByClientGroupIdAsync(clientGroupId);

        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<Order>> GetByClientGroupPublicIdAsync(string clientGroupPublicId)
    {
        var entities = await _databaseRepository.GetByClientGroupPublicIdAsync(clientGroupPublicId);

        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<Order>> GetByDateRangeAsync(DateTime from, DateTime to)
    {
        var entities = await _databaseRepository.GetByDateRangeAsync(from, to);

        return entities.Select(e => e.FromPersistence());
    }

    public async Task AddAsync(Order order)
    {
        var entity = order.ToPersistence();

        // Add lines with required navigation properties
        foreach (var line in order.Lines)
        {
            ProductDatabaseEntity? product = null;

            if (!string.IsNullOrEmpty(line.ProductPublicId))
            {
                product = await _context.Products.FirstOrDefaultAsync(p => p.PublicId == line.ProductPublicId);
            }

            if (product == null)
                throw new NotFoundException(EntityTypePrintNames.Product, line.ProductPublicId);

            var lineEntity = line.ToPersistence(entity, product);

            entity.Lines.Add(lineEntity);
        }

        await _databaseRepository.AddAsync(entity);

        // Add initial status history
        foreach (var historyEntry in order.StatusHistory)
        {
            var historyEntity = historyEntry.ToPersistence(entity);

            _context.Set<OrderStatusHistoryDatabaseEntity>().Add(historyEntity);
        }

        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Order order)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(order.PublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Order, order.PublicId);

        order.UpdatePersistence(entity);

        // Sync lines
        var domainLinePublicIds = order.Lines
            .Select(l => l.PublicId)
            .ToHashSet();

        var linesToRemove = entity.Lines
            .Where(e => !domainLinePublicIds.Contains(e.PublicId))
            .ToList();

        foreach (var toRemove in linesToRemove)
        {
            entity.Lines.Remove(toRemove);
        }

        foreach (var line in order.Lines)
        {
            var existing = entity.Lines.FirstOrDefault(e => e.PublicId == line.PublicId);

            if (existing != null)
            {
                line.UpdatePersistence(existing);
            }
            else
            {
                ProductDatabaseEntity? product = null;

                if (!string.IsNullOrEmpty(line.ProductPublicId))
                {
                    product = await _context.Products.FirstOrDefaultAsync(p => p.PublicId == line.ProductPublicId);
                }

                if (product == null)
                    throw new NotFoundException(EntityTypePrintNames.Product, line.ProductPublicId);

                var lineEntity = line.ToPersistence(entity, product);

                entity.Lines.Add(lineEntity);
            }
        }

        // Handle status history - add new entries
        foreach (var historyEntry in order.StatusHistory)
        {
            var historyEntity = historyEntry.ToPersistence(entity);

            _context.Set<OrderStatusHistoryDatabaseEntity>().Add(historyEntity);
        }

        await _context.SaveChangesAsync();
    }
}







