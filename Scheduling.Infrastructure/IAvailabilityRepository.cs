using Scheduling.Domain;

namespace Scheduling.Infrastructure;

public interface IAvailabilityRepository
{
    Task AddClientAccount(Availability availability, CancellationToken cancellationToken);
}