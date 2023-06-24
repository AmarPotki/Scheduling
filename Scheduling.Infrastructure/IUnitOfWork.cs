using System.Data.Common;

namespace Scheduling.Infrastructure;

public interface IUnitOfWork
{
    Task<DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}