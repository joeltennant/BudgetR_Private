using BudgetR.Core.Enums;

namespace BudgetR.Server.Application.Handlers.Registration;
public class CreateUserProfile
{
    public class Request : RequestBase, IRequest<Result<long>>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public Request(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    };

    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.FirstName)
                .NotNull()
                .MinimumLength(3);
        }
    }

    public class Handler : BaseHandler<long>, IRequestHandler<Request, Result<long>>
    {
        private readonly Validator _validator = new();

        public Handler(BudgetRDbContext context, StateContainer stateContainer)
            : base(context, stateContainer)
        {
        }

        public async Task<Result<long>> Handle(Request request, CancellationToken cancellationToken)
        {
            var validation = await _validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return Result.Error(validation.Errors);
            }

            var transaction = _context.Database.BeginTransaction();
            try
            {

                string authenticationId = _stateContainer.ApplicationUserId;

                await CreateBta();

                var user = new User
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    AuthenticationId = authenticationId,
                    IsActive = true,
                    UserType = UserType.User,
                    BusinessTransactionActivityId = _stateContainer.BtaId.Value
                };

                _context.Add(user);
                _context.SaveChanges();

                _stateContainer.UserId = user.UserId;

                transaction.Commit();

                return Result.Success(user.UserId);
            }
            catch (Exception ex)
            {
                return Result.SystemError(ex.Message);
            }
        }
    }
}
