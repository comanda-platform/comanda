namespace Comanda.Application.UseCases;

using Comanda.Domain;
using Comanda.Domain.Entities;
using Comanda.Domain.Repositories;

public class DailySideAvailabilityUseCase(
    IDailySideAvailabilityRepository availabilityRepository,
    ISideRepository sideRepository)
{
    private readonly IDailySideAvailabilityRepository _availabilityRepository = availabilityRepository;
    private readonly ISideRepository _sideRepository = sideRepository;

    public async Task<DailySideAvailability> SetAsync(
        string sidePublicId,
        DateOnly date,
        bool isAvailable)
    {
        var side = await _sideRepository.GetByPublicIdAsync(sidePublicId)
            ?? throw new NotFoundException($"Side '{sidePublicId}' not found");

        // Check if availability record already exists
        //var existing = await _availabilityRepository.GetBySidePublicIdAndDateAsync(side.PublicId, date);

        //if (existing != null)
        //{
        //    if (isAvailable)
        //        existing.SetAvailable();
        //    else
        //        existing.SetUnavailable();

        //    await _availabilityRepository.UpdateAsync(existing);
        //    return existing;
        //}

        var availability = new DailySideAvailability(side, date, isAvailable);
        await _availabilityRepository.AddAsync(availability);
        return availability;
    }

    //public async Task<DailySideAvailability?> GetAsync(int sideId, DateOnly date)
    //    => await _availabilityRepository.GetBySideIdAndDateAsync(sideId, date);

    public async Task<IEnumerable<DailySideAvailability>> GetByDateAsync(DateOnly date)
        => await _availabilityRepository.GetByDateAsync(date);

    public async Task<IEnumerable<DailySideAvailability>> GetAvailableByDateAsync(DateOnly date)
        => await _availabilityRepository.GetAvailableByDateAsync(date);

    //public async Task<IEnumerable<DailySideAvailability>> GetBySidePublicIdAsync(string sidePublicId)
    //{
    //    var side = await _sideRepository.GetByPublicIdAsync(sidePublicId)
    //        ?? throw new NotFoundException($"Side '{sidePublicId}' not found");

    //    return await _availabilityRepository.GetBySidePublicIdAsync(side.PublicId);
    //}

    public async Task SetAvailableAsync(string sidePublicId, DateOnly date)
    {
        await SetAsync(sidePublicId, date, true);
    }

    public async Task SetUnavailableAsync(string sidePublicId, DateOnly date)
    {
        await SetAsync(sidePublicId, date, false);
    }

    public async Task DeleteAsync(string availabilityPublicId)
    {
        var availability = await _availabilityRepository.GetByPublicIdAsync(availabilityPublicId)
            ?? throw new NotFoundException($"DailySideAvailability '{availabilityPublicId}' not found");

        await _availabilityRepository.DeleteAsync(availability);
    }
}







