namespace Comanda.Application.UseCases;

using Comanda.Domain;
using Comanda.Domain.Entities;
using Comanda.Domain.Repositories;

public class AccountUseCase(
    IAccountRepository accountRepository) : UseCaseBase(EntityTypePrintNames.Account)
{
    private readonly IAccountRepository _accountRepository = accountRepository;

    public async Task<Account> CreateAccountAsync(string name, bool hasCreditLine = false, decimal? creditLimit = null)
    {
        var account = new Account(name, hasCreditLine, creditLimit);
        await _accountRepository.AddAsync(account);
        return account;
    }

    public async Task<Account> GetAccountByPublicIdAsync(string publicId)
    {
        return await _accountRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);
    }

    public async Task<IEnumerable<Account>> GetAllAccountsAsync()
    {
        return await _accountRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Account>> GetAccountsWithCreditLineAsync()
    {
        return await _accountRepository.GetWithCreditLineAsync();
    }

    public async Task<Account> UpdateAccountNameAsync(string publicId, string newName)
    {
        var account = await _accountRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        account.UpdateName(newName);
        await _accountRepository.UpdateAsync(account);

        return account;
    }

    public async Task<Account> EnableCreditLineAsync(string publicId, decimal? creditLimit = null)
    {
        var account = await _accountRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        account.EnableCreditLine(creditLimit);
        await _accountRepository.UpdateAsync(account);

        return account;
    }

    public async Task<Account> DisableCreditLineAsync(string publicId)
    {
        var account = await _accountRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        account.DisableCreditLine();
        await _accountRepository.UpdateAsync(account);

        return account;
    }

    public async Task<Account> UpdateCreditLimitAsync(string publicId, decimal? creditLimit)
    {
        var account = await _accountRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        account.UpdateCreditLimit(creditLimit);
        await _accountRepository.UpdateAsync(account);

        return account;
    }

    public async Task DeleteAccountAsync(string publicId)
    {
        var account = await _accountRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        await _accountRepository.DeleteAsync(account);
    }
}
