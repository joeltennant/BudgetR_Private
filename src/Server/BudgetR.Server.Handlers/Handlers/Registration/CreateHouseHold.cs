using BudgetR.Core.Authentication;
using Microsoft.AspNetCore.Identity;

namespace BudgetR.Server.Handlers.Handlers.Registration;
public class CreateHouseHold
{
    public class Request : RequestBase, IRequest<Result<NoValue>>
    {
        public string HouseholdName { get; set; }

        public Request(string householdName)
        {
            HouseholdName = householdName;
        }
    }

    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.HouseholdName)
                .NotNull()
                .MinimumLength(3);
        }
    }

    public class Handler : BaseHandler<NoValue>, IRequestHandler<Request, Result<NoValue>>
    {
        private readonly Validator _validator = new();

        private readonly UserManager<ApplicationUser> _userManager;
        public Handler(BudgetRDbContext context, StateContainer stateContainer)
            : base(context, stateContainer)
        {
        }

        public async Task<Result<NoValue>> Handle(Request request, CancellationToken cancellationToken)
        {
            var validation = await _validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return Result.Error(validation.Errors);
            }

            var transaction = _context.Database.BeginTransaction();

            try
            {
                long BtaId = await CreateBta();

                Household household = new()
                {
                    Name = request.HouseholdName,
                    BusinessTransactionActivityId = BtaId,
                };

                await _context.Households.AddAsync(household);
                await _context.SaveChangesAsync();

                await _context.Users
                    .Where(u => u.UserId == _stateContainer.UserId)
                    .ExecuteUpdateAsync(u => u
                        .SetProperty(p => p.HouseholdId, household.HouseholdId)
                        .SetProperty(p => p.BusinessTransactionActivityId, _stateContainer.BtaId));

                await _context.AddRangeAsync(BuildMonthBudgetList(household.HouseholdId));
                await _context.SaveChangesAsync();

                transaction.Commit();
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.SystemError(ex.Message);
            }
        }

        private List<BudgetMonth> BuildMonthBudgetList(long HouseholdId)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);

            List<MonthYear> monthYears = _context.MonthYears
                .Where(m => m.IsActive)
                .OrderBy(m => m.MonthYearId)
                .ToList();

            List<BudgetMonth> monthBudgets = new();

            foreach (var monthYear in monthYears)
            {
                monthBudgets.Add(new BudgetMonth
                {
                    MonthYearId = monthYear.MonthYearId,
                    HouseholdId = HouseholdId,
                });
            }

            return monthBudgets;
        }
    }
}
