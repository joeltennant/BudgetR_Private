using BudgetR.Core.Enums;

namespace BudgetR.Server.Application.Handlers.Authentication;
public class GetAuthenticationAction
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
                action = AuthenticationAction.GoToRegistration;
            }
            else if (_stateContainer.HouseholdId.IsNullOrZero())
            {
                action = AuthenticationAction.GoToRegistration;
            }
            else
            {
                action = AuthenticationAction.AlreadyFullyRegistered;
            }

            return Result.Success(action);
        }
    }
}
