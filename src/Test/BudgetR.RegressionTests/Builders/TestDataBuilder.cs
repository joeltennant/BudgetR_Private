using BudgetR.Core;
using BudgetR.RegressionTests.Builders.Helpers;

namespace BudgetR.RegressionTests.Builders;
public class TestDataBuilder
{
    public long? HouseholdId { get; set; }
    public long? UserId { get; set; }

    protected readonly BudgetRDbContext _context;

    public TestDataBuilder(BudgetRDbContext context)
    {
        _context = context;
    }

    public TestDataBuilder WithHouseholdId(long householdId)
    {
        HouseholdId = householdId;
        return this;
    }

    public TestDataBuilder WithUserId(long userId)
    {
        UserId = userId;
        return this;
    }

    public async Task EnsureMonthsPopulated()
    {
        if (HouseholdId.HasValue)
        {
            await PopulateHouseholdMonths.Populate(_context, HouseholdId.Value);
        }
        else
        {
            throw new Exception("HouseholdId must be set before calling EnsureMonthsPopulated");
        }
    }

    //setup state container
    public StateContainer BuildStateContainer(string processName = "")
    {
        //create StateContainer object and fill with user and holdhold data
        return new StateContainer
        {
            ProcessName = processName,
            UserId = UserId,
            HouseholdId = HouseholdId
        };
    }

    public TestDataBuilder Build()
    {
        return this;
    }
}
