namespace Comanda.Application.UseCases;

using Comanda.Domain;
using Comanda.Domain.Entities;
using Comanda.Domain.Repositories;

public class ClientGroupUseCase(IClientGroupRepository clientGroupRepository) : UseCaseBase(EntityTypePrintNames.ClientGroup)
{
    private readonly IClientGroupRepository _clientGroupRepository = clientGroupRepository;

    public async Task<ClientGroup> CreateClientGroupAsync(
        string name,
        bool hasCreditLine = false)
    {
        var group = new ClientGroup(name, hasCreditLine);

        await _clientGroupRepository.AddAsync(group);

        return group;
    }

    public async Task<ClientGroup> GetClientGroupByPublicIdAsync(string publicId)
        => await _clientGroupRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

    public async Task<IEnumerable<ClientGroup>> GetAllClientGroupsAsync()
        => await _clientGroupRepository.GetAllAsync();

    public async Task<IEnumerable<ClientGroup>> GetClientGroupsWithCreditLineAsync()
        => await _clientGroupRepository.GetWithCreditLineAsync();

    public async Task UpdateClientGroupNameAsync(
        string publicId,
        string name)
    {
        var group = await _clientGroupRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        group.UpdateName(name);

        await _clientGroupRepository.UpdateAsync(group);
    }

    public async Task EnableCreditLineAsync(string publicId)
    {
        var group = await _clientGroupRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        group.EnableCreditLine();

        await _clientGroupRepository.UpdateAsync(group);
    }

    public async Task DisableCreditLineAsync(string publicId)
    {
        var group = await _clientGroupRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        group.DisableCreditLine();

        await _clientGroupRepository.UpdateAsync(group);
    }

    public async Task DeleteClientGroupAsync(string publicId)
    {
        var group = await _clientGroupRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        await _clientGroupRepository.DeleteAsync(group);
    }
}







