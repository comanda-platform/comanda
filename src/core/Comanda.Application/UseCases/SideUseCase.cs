namespace Comanda.Application.UseCases;

using Comanda.Domain;
using Comanda.Domain.Entities;
using Comanda.Domain.Repositories;

public class SideUseCase(ISideRepository sideRepository) : UseCaseBase(EntityTypePrintNames.Side)
{
    private readonly ISideRepository _sideRepository = sideRepository;

    public async Task<Side> CreateAsync(string name)
    {
        var side = new Side(name);
        await _sideRepository.AddAsync(side);
        return side;
    }

    public async Task<Side> GetByPublicIdAsync(string publicId)
        => await _sideRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

    public async Task<IEnumerable<Side>> GetAllAsync()
        => await _sideRepository.GetAllAsync();

    public async Task<IEnumerable<Side>> GetActiveAsync()
        => await _sideRepository.GetActiveAsync();

    public async Task UpdateNameAsync(string publicId, string name)
    {
        var side = await _sideRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        side.UpdateName(name);
        await _sideRepository.UpdateAsync(side);
    }

    public async Task ActivateAsync(string publicId)
    {
        var side = await _sideRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        side.Activate();
        await _sideRepository.UpdateAsync(side);
    }

    public async Task DeactivateAsync(string publicId)
    {
        var side = await _sideRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        side.Deactivate();
        await _sideRepository.UpdateAsync(side);
    }

    public async Task DeleteAsync(string publicId)
    {
        var side = await _sideRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        await _sideRepository.DeleteAsync(side);
    }
}







