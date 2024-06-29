using Microsoft.EntityFrameworkCore.Storage;

namespace BudgetR.Server.Application;
public abstract class BaseHandler<T>
{
    protected readonly BudgetRDbContext _context;
    protected readonly StateContainer _stateContainer;
    protected Result<T> Result;
    protected IDbContextTransaction? transaction;

    protected BaseHandler(BudgetRDbContext dbContext, StateContainer stateContainer)
    {
        _context = dbContext;
        _stateContainer = stateContainer;
        Result = new Result<T>();
    }

    protected async Task<long> CreateBta()
    {
        long userId = _stateContainer.UserId.HasValue
            ? _stateContainer.UserId.Value
            : 1;

        var bta = new BusinessTransactionActivity
        {
            ProcessName = _stateContainer.ProcessName,
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };

        await _context.BusinessTransactionActivities.AddAsync(bta);
        await _context.SaveChangesAsync();

        _stateContainer.BtaId = bta.BusinessTransactionActivityId;

        return bta.BusinessTransactionActivityId;
    }
}
