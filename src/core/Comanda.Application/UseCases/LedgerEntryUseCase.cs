namespace Comanda.Application.UseCases;

using Comanda.Domain.Entities;
using Comanda.Shared.Enums;
using Comanda.Domain.Repositories;
using Comanda.Domain;

public class LedgerEntryUseCase(
    ILedgerEntryRepository ledgerEntryRepository,
    IClientRepository clientRepository) : UseCaseBase(EntityTypePrintNames.LedgerEntry)
{
    private readonly ILedgerEntryRepository _ledgerEntryRepository = ledgerEntryRepository;
    private readonly IClientRepository _clientRepository = clientRepository;

    public async Task<LedgerEntry> CreateCreditAsync(
        string clientPublicId,
        decimal amount,
        string? orderLinePublicId = null)
    {
        var client = await _clientRepository.GetByPublicIdAsync(clientPublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Client, clientPublicId);

        var entry = LedgerEntry.CreateCredit(
            client.PublicId,
            amount,
            orderLinePublicId);

        await _ledgerEntryRepository.AddAsync(entry);

        return entry;
    }

    public async Task<LedgerEntry> CreatePaymentAsync(
        string clientPublicId,
        decimal amount,
        PaymentMethod paymentMethod)
    {
        var client = await _clientRepository.GetByPublicIdAsync(clientPublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Client, clientPublicId);

        var entry = LedgerEntry.CreatePayment(client.PublicId, amount, paymentMethod);

        await _ledgerEntryRepository.AddAsync(entry);

        return entry;
    }

    public async Task<LedgerEntry> CreateAdjustmentAsync(
        string clientPublicId,
        decimal amount)
    {
        var client = await _clientRepository.GetByPublicIdAsync(clientPublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Client, clientPublicId);

        var entry = LedgerEntry.CreateAdjustment(client.PublicId, amount);

        await _ledgerEntryRepository.AddAsync(entry);

        return entry;
    }

    public async Task<LedgerEntry> CreateWriteOffAsync(
        string clientPublicId,
        decimal amount)
    {
        var client = await _clientRepository.GetByPublicIdAsync(clientPublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Client, clientPublicId);

        var entry = LedgerEntry.CreateWriteOff(client.PublicId, amount);

        await _ledgerEntryRepository.AddAsync(entry);

        return entry;
    }

    public async Task<LedgerEntry> GetEntryByPublicIdAsync(string publicId)
        => await _ledgerEntryRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

    public async Task<IEnumerable<LedgerEntry>> GetEntriesByClientAsync(string clientPublicId)
    {
        var client = await _clientRepository.GetByPublicIdAsync(clientPublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Client, clientPublicId);

        return await _ledgerEntryRepository.GetByClientPublicIdAsync(client.PublicId);
    }

    public async Task<IEnumerable<LedgerEntry>> GetEntriesByClientAndDateRangeAsync(
        string clientPublicId,
        DateTime from,
        DateTime to)
    {
        var client = await _clientRepository.GetByPublicIdAsync(clientPublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Client, clientPublicId);

        return await _ledgerEntryRepository.GetByClientPublicIdAndDateRangeAsync(client.PublicId, from, to);
    }

    public async Task<decimal> GetClientBalanceAsync(string clientPublicId)
    {
        var client = await _clientRepository.GetByPublicIdAsync(clientPublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Client, clientPublicId);

        return await _ledgerEntryRepository.GetClientBalanceByPublicIdAsync(client.PublicId);
    }
}







