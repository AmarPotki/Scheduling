using Scheduling.Domain;

namespace Scheduling.Infrastructure;

public class AvailabilityRepository : IAvailabilityRepository
{
    private readonly SchedulingDataContext _dataContext;

    public AvailabilityRepository(SchedulingDataContext dbContext)
    {
        _dataContext = dbContext;
    }

    public async Task AddClientAccount(Availability availability, CancellationToken cancellationToken) =>
        await _dataContext.Set<Availability>().AddAsync(availability, cancellationToken);

}