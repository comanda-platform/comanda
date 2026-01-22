namespace Comanda.Application.UseCases;

using Comanda.Domain;
using Comanda.Domain.Entities;
using Comanda.Domain.Repositories;

public class DailyMenuUseCase(
    IDailyMenuRepository dailyMenuRepository,
    IProductRepository productRepository) : UseCaseBase(EntityTypePrintNames.DailyMenu)
{
    private readonly IDailyMenuRepository _dailyMenuRepository = dailyMenuRepository;
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<DailyMenu> CreateAsync(
        DateOnly date,
        string? locationPublicId)
    {
        // Check if menu already exists for this date/location
        var existing = await _dailyMenuRepository.GetByDateAsync(
            date,
            locationPublicId: locationPublicId);

        if (existing != null)
        {
            if (locationPublicId != null)
            {
                throw new InvalidOperationException($"Daily menu already exists for date {date} and location {locationPublicId}");
            }

            throw new InvalidOperationException($"Daily menu already exists for date {date}");
        }

        var menu = new DailyMenu(date, locationPublicId);

        await _dailyMenuRepository.AddAsync(menu);

        return menu;
    }

    public async Task<DailyMenu> GetByPublicIdAsync(string publicId) 
        => await _dailyMenuRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

    public async Task<DailyMenu?> GetByDateAsync(
        DateOnly date,
        string? locationPublicId = null)
        => await _dailyMenuRepository.GetByDateAsync(date, null, locationPublicId);

    public async Task<IEnumerable<DailyMenu>> GetAllAsync()
        => await _dailyMenuRepository.GetAllAsync();

    public async Task<IEnumerable<DailyMenu>> GetByDateRangeAsync(
        DateOnly from,
        DateOnly to,
        string? locationPublicId = null)
        => await _dailyMenuRepository.GetByDateRangeAsync(from, to, null, locationPublicId);

    public async Task<IEnumerable<DailyMenu>> GetByLocationAsync(string locationPublicId)
        => await _dailyMenuRepository.GetByLocationPublicIdAsync(locationPublicId);

    public async Task AddMenuItemAsync(
        string menuPublicId,
        string productPublicId,
        int sequenceOrder,
        string? overriddenName = null,
        decimal? overriddenPrice = null)
    {
        var menu = await _dailyMenuRepository.GetByPublicIdAsync(menuPublicId)
            ?? throw new NotFoundException(EntityTypePrintName, menuPublicId);

        var product = await _productRepository.GetByPublicIdAsync(productPublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Product, productPublicId);

        var item = new DailyMenuItem(product, sequenceOrder, overriddenName, overriddenPrice);
        menu.AddItem(item);
        await _dailyMenuRepository.UpdateAsync(menu);
    }

    public async Task UpdateMenuItemAsync(
        string menuPublicId,
        string menuItemPublicId,
        int? sequenceOrder = null,
        string? overriddenName = null,
        decimal? overriddenPrice = null)
    {
        var menu = await _dailyMenuRepository.GetByPublicIdAsync(menuPublicId)
            ?? throw new NotFoundException(EntityTypePrintName, menuPublicId);

        var item = menu.Items.FirstOrDefault(i => i.PublicId == menuItemPublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.DailyMenuItem, menuItemPublicId);

        if (sequenceOrder.HasValue)
            item.UpdateSequenceOrder(sequenceOrder.Value);

        item.SetOverriddenName(overriddenName);
        item.SetOverriddenPrice(overriddenPrice);

        await _dailyMenuRepository.UpdateAsync(menu);
    }

    public async Task RemoveMenuItemAsync(string menuPublicId, string menuItemPublicId)
    {
        var menu = await _dailyMenuRepository.GetByPublicIdAsync(menuPublicId)
            ?? throw new NotFoundException(EntityTypePrintName, menuPublicId);

        var item = menu.Items.FirstOrDefault(i => i.PublicId == menuItemPublicId)
            ?? throw new NotFoundException($"MenuItem with PublicId '{menuItemPublicId}' not found in menu");

        menu.RemoveItem(item);
        await _dailyMenuRepository.UpdateAsync(menu);
    }

    public async Task ReorderMenuItemsAsync(string menuPublicId)
    {
        var menu = await _dailyMenuRepository.GetByPublicIdAsync(menuPublicId)
            ?? throw new NotFoundException(EntityTypePrintName, menuPublicId);

        menu.ReorderItems();
        await _dailyMenuRepository.UpdateAsync(menu);
    }

    public async Task DeleteAsync(string menuPublicId)
    {
        var menu = await _dailyMenuRepository.GetByPublicIdAsync(menuPublicId)
            ?? throw new NotFoundException(EntityTypePrintName, menuPublicId);

        await _dailyMenuRepository.DeleteAsync(menu);
    }
}







