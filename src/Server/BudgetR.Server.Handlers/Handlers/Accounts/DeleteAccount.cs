using BudgetR.Server.Application;

public static class DeleteAccount
{
    public record Request(long AccountId) : IRequest<Result<NoValue>>;

    public class Handler : BaseHandler<NoValue>, IRequestHandler<Request, Result<NoValue>>
    {
        public Handler(BudgetRDbContext context, StateContainer stateContainer)
            : base(context, stateContainer)
        {
        }

        public async Task<Result<NoValue>> Handle(Request request, CancellationToken cancellationToken)
        {
            int result = await _context.Accounts
                .Where(a => a.AccountId == request.AccountId)
                .ExecuteDeleteAsync();

            await _context.SaveChangesAsync();

            return result == 0 ? Result.NotFound() : Result.Success();
        }
    }
}
