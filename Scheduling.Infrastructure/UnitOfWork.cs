using System.Data.Common;
using Microsoft.EntityFrameworkCore.Storage;

namespace Scheduling.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly SchedulingDataContext _dataContext;
    private DbTransaction? _dbTransaction;

    public UnitOfWork(SchedulingDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_dbTransaction is not null) return _dbTransaction;
        var transaction = await _dataContext.Database.BeginTransactionAsync(cancellationToken);
        _dbTransaction = transaction.GetDbTransaction();
        return _dbTransaction;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dataContext.SaveChangesAsync(cancellationToken);
        if (_dbTransaction is not null)
        {
            await _dataContext.Database.CommitTransactionAsync(cancellationToken);
        }
    }
}