namespace BudgetR.Server.Handlers.Handlers.Users;
public static class GetUserId
{
    public class Request : RequestBase, IRequest<Result<long?>>;

    public class Handler : BaseHandler<long?>, IRequestHandler<Request, Result<long?>>
    {
        public Handler(BudgetRDbContext context, StateContainer stateContainer)
            : base(context, stateContainer)
        {
        }

        public async Task<Result<long?>> Handle(Request request, CancellationToken cancellationToken)
        {
            if (_stateContainer.UserId != null)
            {
                return Result.Success(_stateContainer.UserId);
            }
            else
            {
                return Result.NotFound();
            }
        }
    }
}
