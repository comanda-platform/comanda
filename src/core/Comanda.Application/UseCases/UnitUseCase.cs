namespace Comanda.Application.UseCases;

using Comanda.Domain.Entities;
using Comanda.Shared.Enums;
using Comanda.Domain.Repositories;
using Comanda.Domain;

public class UnitUseCase(IUnitRepository unitRepository) : UseCaseBase(EntityTypePrintNames.Unit)
{
    private readonly IUnitRepository _unitRepository = unitRepository;

    public async Task<Unit> CreateAsync(
        string code,
        string name,
        UnitCategory category,
        decimal? toBaseMultiplier = null)
    {
        var unit = new Unit(code, name, category, toBaseMultiplier);
        await _unitRepository.AddAsync(unit);
        return unit;
    }

    public async Task<Unit?> GetByPublicIdAsync(string publicId)
        => await _unitRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

    public async Task<Unit?> GetByCodeAsync(string code)
        => await _unitRepository.GetByCodeAsync(code);

    public async Task<IEnumerable<Unit>> GetAllAsync()
        => await _unitRepository.GetAllAsync();

    public async Task<IEnumerable<Unit>> GetByCategoryAsync(UnitCategory category)
        => await _unitRepository.GetByCategoryAsync(category);

    public async Task UpdateAsync(
        string publicId,
        string code,
        string name,
        UnitCategory category,
        decimal? toBaseMultiplier)
    {
        var unit = await _unitRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        unit.UpdateDetails(code, name, category, toBaseMultiplier);
        await _unitRepository.UpdateAsync(unit);
    }

    public async Task DeleteAsync(string publicId)
    {
        var unit = await _unitRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        await _unitRepository.DeleteAsync(unit);
    }
}







