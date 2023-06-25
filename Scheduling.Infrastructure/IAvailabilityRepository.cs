using Scheduling.Domain;

namespace Scheduling.Infrastructure;

public interface IAvailabilityRepository
{
    Task AddAvailability(Availability availability, CancellationToken cancellationToken);
    ValueTask<Availability?> GetById(long id, CancellationToken none);
    Availability Update(Availability current, CancellationToken none);
}