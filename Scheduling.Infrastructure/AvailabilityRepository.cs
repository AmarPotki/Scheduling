using Scheduling.Domain;

namespace Scheduling.Infrastructure;

public class AvailabilityRepository : IAvailabilityRepository
{
    private readonly SchedulingDataContext _dataContext;

    public AvailabilityRepository(SchedulingDataContext dbContext)
    {
        _dataContext = dbContext;
    }

    public async Task AddAvailability(Availability availability, CancellationToken cancellationToken) =>
        await _dataContext.Set<Availability>().AddAsync(availability, cancellationToken);

    public ValueTask<Availability?> GetById(long id, CancellationToken none)
    {
        return _dataContext.Set<Availability>().FindAsync(id);
    }

    public Availability Update(Availability current, CancellationToken none)
    {
        var entry= _dataContext.Set<Availability>().Update(current);
        return entry.Entity;
    }
}