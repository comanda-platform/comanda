namespace Comanda.Application.UseCases;

using Comanda.Domain;
using Comanda.Domain.Entities;
using Comanda.Domain.Repositories;
using Comanda.Shared.Enums;

public class AuthorizationUseCase(
    IAuthorizationRepository authorizationRepository,
    IPersonRepository personRepository,
    IAccountRepository accountRepository) : UseCaseBase(EntityTypePrintNames.Authorization)
{
    private readonly IAuthorizationRepository _authorizationRepository = authorizationRepository;
    private readonly IPersonRepository _personRepository = personRepository;
    private readonly IAccountRepository _accountRepository = accountRepository;

    public async Task<Authorization> CreateAuthorizationAsync(
        string personPublicId,
        string accountPublicId,
        AuthorizationRole role = AuthorizationRole.Orderer)
    {
        // Validate that Person exists
        _ = await _personRepository.GetByPublicIdAsync(personPublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Person, personPublicId);

        // Validate that Account exists
        _ = await _accountRepository.GetByPublicIdAsync(accountPublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Account, accountPublicId);

        var authorization = new Authorization(personPublicId, accountPublicId, role);
        await _authorizationRepository.AddAsync(authorization);
        return authorization;
    }

    public async Task<Authorization> GetAuthorizationByPublicIdAsync(string publicId)
    {
        return await _authorizationRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);
    }

    public async Task<IEnumerable<Authorization>> GetAllAuthorizationsAsync()
    {
        return await _authorizationRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Authorization>> GetAuthorizationsByPersonPublicIdAsync(string personPublicId)
    {
        // Validate that Person exists
        _ = await _personRepository.GetByPublicIdAsync(personPublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Person, personPublicId);

        return await _authorizationRepository.GetByPersonPublicIdAsync(personPublicId);
    }

    public async Task<IEnumerable<Authorization>> GetAuthorizationsByAccountPublicIdAsync(string accountPublicId)
    {
        // Validate that Account exists
        _ = await _accountRepository.GetByPublicIdAsync(accountPublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Account, accountPublicId);

        return await _authorizationRepository.GetByAccountPublicIdAsync(accountPublicId);
    }

    public async Task<IEnumerable<Authorization>> GetActiveAuthorizationsByPersonPublicIdAsync(string personPublicId)
    {
        // Validate that Person exists
        _ = await _personRepository.GetByPublicIdAsync(personPublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Person, personPublicId);

        return await _authorizationRepository.GetActiveByPersonPublicIdAsync(personPublicId);
    }

    public async Task<IEnumerable<Authorization>> GetActiveAuthorizationsByAccountPublicIdAsync(string accountPublicId)
    {
        // Validate that Account exists
        _ = await _accountRepository.GetByPublicIdAsync(accountPublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Account, accountPublicId);

        return await _authorizationRepository.GetActiveByAccountPublicIdAsync(accountPublicId);
    }

    public async Task<Authorization> UpdateAuthorizationRoleAsync(string publicId, AuthorizationRole newRole)
    {
        var authorization = await _authorizationRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        authorization.UpdateRole(newRole);
        await _authorizationRepository.UpdateAsync(authorization);

        return authorization;
    }

    public async Task<Authorization> ActivateAuthorizationAsync(string publicId)
    {
        var authorization = await _authorizationRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        authorization.Activate();
        await _authorizationRepository.UpdateAsync(authorization);

        return authorization;
    }

    public async Task<Authorization> DeactivateAuthorizationAsync(string publicId)
    {
        var authorization = await _authorizationRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        authorization.Deactivate();
        await _authorizationRepository.UpdateAsync(authorization);

        return authorization;
    }

    public async Task DeleteAuthorizationAsync(string publicId)
    {
        var authorization = await _authorizationRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        await _authorizationRepository.DeleteAsync(authorization);
    }
}
