using BudgetR.Core.Enums;

namespace BudgetR.Server.Handlers.Handlers.Registration;
public static class GetRegistrationAction
{
    public class Request : RequestBase, IRequest<Result<AuthenticationAction>>
    {
    };

    public class Handler : BaseHandler<AuthenticationAction>, IRequestHandler<Request, Result<AuthenticationAction>>
    {
        public Handler(BudgetRDbContext context, StateContainer stateContainer)
            : base(context, stateContainer)
        {
        }

        public async Task<Result<AuthenticationAction>> Handle(Request request, CancellationToken cancellationToken)
        {
            AuthenticationAction action;

            if (_stateContainer.UserId.IsNullOrZero())
            {
                action = AuthenticationAction.NoUserExists;
            }
            else if (_stateContainer.HouseholdId.IsNullOrZero())
            {
                action = AuthenticationAction.HasNoHousehold;
            }
            else
            {
                action = AuthenticationAction.AlreadyFullyRegistered;
            }

            return Result.Success(action);
        }
    }
}
