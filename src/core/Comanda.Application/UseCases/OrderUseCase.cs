namespace Comanda.Application.UseCases;

using Comanda.Application.Notifications;
using Comanda.Application.Notifications.Events;
using Comanda.Domain;
using Comanda.Domain.Entities;
using Comanda.Domain.Repositories;
using Comanda.Shared.Enums;

public class OrderUseCase(
    IOrderRepository orderRepository,
    IClientRepository clientRepository,
    IClientGroupRepository clientGroupRepository,
    ILocationRepository locationRepository,
    IProductRepository productRepository,
    INotificationPublisher notifications) : UseCaseBase(EntityTypePrintNames.Order)
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IClientRepository _clientRepository = clientRepository;
    private readonly IClientGroupRepository _clientGroupRepository = clientGroupRepository;
    private readonly ILocationRepository _locationRepository = locationRepository;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly INotificationPublisher _notifications = notifications;

    public async Task<Order> CreateOrderAsync(
        OrderFulfillmentType fulfillmentType,
        OrderSource source,
        string? clientPublicId = null,
        string? clientGroupPublicId = null,
        string? locationPublicId = null)
    {
        // Validate existence but keep public ids for domain
        if (!string.IsNullOrEmpty(clientPublicId))
        {
            _ = await _clientRepository.GetByPublicIdAsync(clientPublicId)
                ?? throw new NotFoundException($"Client '{clientPublicId}' not found");
        }

        if (!string.IsNullOrEmpty(clientGroupPublicId))
        {
            _ = await _clientGroupRepository.GetByPublicIdAsync(clientGroupPublicId)
                ?? throw new NotFoundException($"Client group '{clientGroupPublicId}' not found");
        }

        if (!string.IsNullOrEmpty(locationPublicId))
        {
            _ = await _locationRepository.GetByPublicIdAsync(locationPublicId)
                ?? throw new NotFoundException($"Location '{locationPublicId}' not found");
        }

        var order = new Order(
            fulfillmentType,
            source,
            clientPublicId,
            clientGroupPublicId,
            locationPublicId);

        await _orderRepository.AddAsync(order);

        await _notifications.PublishAsync(new OrderCreatedEvent(order.PublicId));

        return order;
    }

    public async Task<Order> GetOrderByPublicIdAsync(string publicId)
        => await _orderRepository.GetByPublicIdAsync(publicId);

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        => await _orderRepository.GetAllAsync();

    public async Task<IEnumerable<Order>> GetActiveOrdersAsync()
        => await _orderRepository.GetActiveOrdersAsync();

    public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status)
        => await _orderRepository.GetByStatusAsync(status);

    public async Task<IEnumerable<Order>> GetOrdersByClientAsync(string clientPublicId)
    {
        var client = await _clientRepository.GetByPublicIdAsync(clientPublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Client, clientPublicId);

        return await _orderRepository.GetByClientPublicIdAsync(client.PublicId);
    }

    public async Task<IEnumerable<Order>> GetOrdersByClientGroupAsync(string clientGroupPublicId)
    {
        var group = await _clientGroupRepository.GetByPublicIdAsync(clientGroupPublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.ClientGroup, clientGroupPublicId);

        return await _orderRepository.GetByClientGroupPublicIdAsync(group.PublicId);
    }

    public async Task<IEnumerable<Order>> GetOrdersByDateRangeAsync(DateTime from, DateTime to)
        => await _orderRepository.GetByDateRangeAsync(from, to);

    public async Task AddOrderLineAsync(
        string orderPublicId,
        string productPublicId,
        int quantity,
        string? clientPublicId = null,
        List<string>? selectedSides = null)
    {
        var order = await _orderRepository.GetByPublicIdAsync(orderPublicId)
            ?? throw new NotFoundException(EntityTypePrintName, orderPublicId);

        var product = await _productRepository.GetByPublicIdAsync(productPublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Product, productPublicId);

        // Validate optional client
        if (!string.IsNullOrEmpty(clientPublicId))
        {
            _ = await _clientRepository.GetByPublicIdAsync(clientPublicId)
                ?? throw new NotFoundException(EntityTypePrintNames.Client, clientPublicId);
        }

        // Determine container type based on fulfillment type
        var containerType = order.FulfillmentType == OrderFulfillmentType.Delivery
            ? "Delivery Container"
            : "Plate";

        var line = new OrderLine(
            product.PublicId,
            quantity,
            product.CurrentPrice,
            clientPublicId,
            containerType,
            selectedSides);

        order.AddLine(line);

        await _orderRepository.UpdateAsync(order);

        await _notifications.PublishAsync(new OrderLineCreatedEvent(
            line.PublicId,
            order.PublicId));
    }

    public async Task AcceptOrderAsync(string publicId, string? changedByPublicId = null)
    {
        var order = await _orderRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        order.Accept(changedByPublicId);

        await _orderRepository.UpdateAsync(order);

        await _notifications.PublishAsync(new OrderAcceptedEvent(order.PublicId));
    }

    public async Task StartPreparingOrderAsync(string publicId, string? changedByPublicId = null)
    { 
        var order = await _orderRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        order.StartPreparing(changedByPublicId);

        await _orderRepository.UpdateAsync(order);

        await _notifications.PublishAsync(new OrderPreparingStartedEvent(order.PublicId));
    }

    public async Task MarkOrderReadyAsync(string publicId, string? changedByPublicId = null)
    {
        var order = await _orderRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        order.MarkReady(changedByPublicId);

        await _orderRepository.UpdateAsync(order);

        await _notifications.PublishAsync(new OrderReadyEvent(order.PublicId));
    }

    public async Task StartDeliveryAsync(string publicId, string? changedByPublicId = null)
    {
        var order = await _orderRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        order.StartDelivery(changedByPublicId);

        await _orderRepository.UpdateAsync(order);

        await _notifications.PublishAsync(new OrderDeliveryStartedEvent(order.PublicId));
    }

    public async Task MarkOrderDeliveredAsync(string publicId, string? changedByPublicId = null)
    {
        var order = await _orderRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        order.MarkDelivered(changedByPublicId);

        await _orderRepository.UpdateAsync(order);

        await _notifications.PublishAsync(new OrderDeliveryFinishedEvent(order.PublicId));
    }

    public async Task CompleteOrderAsync(string publicId, string? changedByPublicId = null)
    {
        var order = await _orderRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        order.Complete(changedByPublicId);

        await _orderRepository.UpdateAsync(order);

        await _notifications.PublishAsync(new OrderCompletedEvent(order.PublicId));
    }

    public async Task CancelOrderAsync(string publicId, string? changedByPublicId = null)
    {
        var order = await _orderRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        order.Cancel(changedByPublicId);

        await _orderRepository.UpdateAsync(order);

        await _notifications.PublishAsync(new OrderCancelledEvent(order.PublicId));
    }

    public async Task AssignDeliveryLocationAsync(string orderPublicId, string locationPublicId)
    {
        var order = await _orderRepository.GetByPublicIdAsync(orderPublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Order, orderPublicId);

        var location = await _locationRepository.GetByPublicIdAsync(locationPublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Location, locationPublicId);

        order.AssignLocation(location.PublicId);

        await _orderRepository.UpdateAsync(order);

        await _notifications.PublishAsync(new OrderDeliveryLocationAssignedEvent(order.PublicId));
    }

    // Order Line Prep Methods
    
    public async Task StartLinePrepAsync(string orderLinePublicId)
    {
        var order = await _orderRepository.GetByOrderLinePublicIdAsync(orderLinePublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.OrderLine, orderLinePublicId);

        var line = order.Lines.FirstOrDefault(l => l.PublicId == orderLinePublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.OrderLine, orderLinePublicId);

        line.StartPrep();

        await _orderRepository.UpdateAsync(order);

        await _notifications.PublishAsync(new OrderLinePrepStartedEvent(
            line.PublicId,
            order.PublicId));
    }

    public async Task CompleteLinePlatingAsync(string orderLinePublicId)
    {
        var order = await _orderRepository.GetByOrderLinePublicIdAsync(orderLinePublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.OrderLine, orderLinePublicId);

        var line = order.Lines.FirstOrDefault(l => l.PublicId == orderLinePublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.OrderLine, orderLinePublicId);

        line.CompletePlating();

        await _orderRepository.UpdateAsync(order);

        await _notifications.PublishAsync(new OrderLinePlatedEvent(
            line.PublicId,
            order.PublicId,
            line.ContainerType ?? "Plate",
            line.SelectedSides?.ToList()));
    }

    public async Task<OrderLine?> GetOrderLineByPublicIdAsync(string orderLinePublicId)
    {
        var order = await _orderRepository.GetByOrderLinePublicIdAsync(orderLinePublicId);
        return order?.Lines.FirstOrDefault(l => l.PublicId == orderLinePublicId);
    }
}







