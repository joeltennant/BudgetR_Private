using BudgetR.Server.Services;

namespace BudgetR.Server.Application.Handlers.System;
public class InactivatePastMonths
{
    public record Request() : IRequest<Result<NoValue>>;

    public class Handler : BaseHandler<NoValue>, IRequestHandler<Request, Result<NoValue>>
    {
        private BusinessTransactionActivity bta;

        public Handler(BudgetRDbContext context, StateContainer stateContainer)
            : base(context, stateContainer)
        {
        }

        public async Task<Result<NoValue>> Handle(Request request, CancellationToken cancellationToken)
        {
            try
            {
                new UpdateMonthsService(_context).Execute();
                return Result.Success();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
