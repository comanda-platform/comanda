namespace Comanda.Application.UseCases;

using Comanda.Domain;
using Comanda.Domain.Entities;
using Comanda.Domain.Repositories;

public class ClientUseCase(
    IClientRepository clientRepository,
    IClientGroupRepository clientGroupRepository) : UseCaseBase(EntityTypePrintNames.Client)
{
    private readonly IClientRepository _clientRepository = clientRepository;
    private readonly IClientGroupRepository _clientGroupRepository = clientGroupRepository;

    public async Task<Client> CreateClientAsync(
        string name,
        string? clientGroupPublicId = null)
    {
        ClientGroup? group = null;

        if (!string.IsNullOrEmpty(clientGroupPublicId))
        {
            group = await _clientGroupRepository.GetByPublicIdAsync(clientGroupPublicId)
                ?? throw new NotFoundException("Client group", clientGroupPublicId);
        }

        var client = new Client(name, group);

        await _clientRepository.AddAsync(client);

        return client;
    }

    public async Task<Client> GetClientByPublicIdAsync(string publicId)
        => await _clientRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

    public async Task<IEnumerable<Client>> GetAllClientsAsync()
        => await _clientRepository.GetAllAsync();

    public async Task<IEnumerable<Client>> GetClientsByGroupAsync(string groupPublicId)
    {
        var group = await _clientGroupRepository.GetByPublicIdAsync(groupPublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.ClientGroup, groupPublicId);

        return await _clientRepository.GetByGroupPublicIdAsync(group.PublicId);
    }

    public async Task UpdateClientNameAsync(
        string publicId,
        string name)
    {
        var client = await _clientRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        client.UpdateName(name);

        await _clientRepository.UpdateAsync(client);
    }

    public async Task AssignClientToGroupAsync(string clientPublicId, string groupPublicId)
    {
        var client = await _clientRepository.GetByPublicIdAsync(clientPublicId)
            ?? throw new NotFoundException(EntityTypePrintName, clientPublicId);

        var group = await _clientGroupRepository.GetByPublicIdAsync(groupPublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.ClientGroup, groupPublicId);

        client.AssignToGroup(group);

        await _clientRepository.UpdateAsync(client);
    }

    public async Task RemoveClientFromGroupAsync(string publicId)
    {
        var client = await _clientRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        client.RemoveFromGroup();

        await _clientRepository.UpdateAsync(client);
    }

    public async Task DeleteClientAsync(string publicId)
    {
        var client = await _clientRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        await _clientRepository.DeleteAsync(client);
    }
}







