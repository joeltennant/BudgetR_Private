using BudgetR.RegressionTests.Builders;
using BudgetR.RegressionTests.Comparers;
using BudgetR.Server.Application.Handlers.Expenses;
using FluentAssertions;

namespace BudgetR.RegressionTests.Expenses;

[TestClass]
public class CreateExpenseTests
{
    [TestMethod]
    public async Task Handle_ValidRequest_ReturnsSuccessResult()
    {
        // Arrange
        try
        {
            var mockContext = new BudgetRDbMockContext();
            var _context = mockContext.CreateContext();

            mockContext.UpdateSystemMonths();
            await _context.Database.BeginTransactionAsync();

            var dataBuilder = new TestDataBuilder(_context)
                .WithHouseholdId(1)
                .WithUserId(1)
                .Build();

            await dataBuilder.EnsureMonthsPopulated();

            var budgetMonths = await GetThreeActiveBudgetMonths(_context, dataBuilder.HouseholdId.Value);

            var request = new Create.Request
            {
                Name = "Expense Name",
                Amount = 100.0m,
                BudgetMonths = budgetMonths.Select(x => x.BudgetMonthId)
            };

            var _stateContainer = dataBuilder.BuildStateContainer("CreateExpense");

            var handler = new Create.Handler(_context, _stateContainer);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            //clear
            _context.ChangeTracker.Clear();

            var budgetMonthsAfterTest = await GetBudgetMonthsByIdList(_context, budgetMonths.Select(x => x.BudgetMonthId).ToArray());

            var budgetMonthComparer = new BudgetMonthComparer()
                .WithBeforeTest(budgetMonths)
                .WithAfterTest(budgetMonthsAfterTest);

            var expenseComparer = await new ExpenseComparer(_context)
                    .GetExpense(result.Value);

            // Assert
            result.IsSuccess.Should().BeTrue();

            result.Value.Should().NotBeNull().And.BeGreaterThan(0);
            expenseComparer.Expense.Should().NotBeNull();
            expenseComparer.Expense?.Name.Should().Be(request.Name);
            expenseComparer.Expense?.Amount.Should().Be(request.Amount);
            expenseComparer.Expense?.IsActive.Should().BeTrue();
            expenseComparer.Expense?.BusinessTransactionActivityId.Should().BeGreaterThan(0);
            expenseComparer.Expense?.ExpenseDetails.Should().HaveCount(3);

            //test budget month balances
            budgetMonthsAfterTest.Should().HaveCount(3);
            Assert.IsTrue(budgetMonthComparer.NewAmountTotalMatches(request.Amount.Value), "New Budget Month balances don't match");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private async Task<List<BudgetMonth>> GetThreeActiveBudgetMonths(BudgetRDbContext context, long householdId)
    {
        var result = await context.BudgetMonths
            .Where(x => x.HouseholdId == householdId && x.MonthYear.IsActive)
            .OrderBy(x => x.MonthYear.Year)
                .ThenBy(x => x.MonthYear.Month)
            .Take(3)
            .Select(x => new BudgetMonth
            {
                BudgetMonthId = x.BudgetMonthId,
                IncomeTotal = x.IncomeTotal,
                ExpenseTotal = x.ExpenseTotal,
            })
            .ToListAsync();

        if (result.Count() == 0)
        {
            throw new Exception("Not enough active budget months");
        }

        return result;
    }

    private async Task<List<BudgetMonth>> GetBudgetMonthsByIdList(BudgetRDbContext context, long[] budgetmonthIds)
    {
        var result = await context.BudgetMonths
            .Where(x => budgetmonthIds.Contains(x.BudgetMonthId))
            .Select(x => new BudgetMonth
            {
                BudgetMonthId = x.BudgetMonthId,
                IncomeTotal = x.IncomeTotal,
                ExpenseTotal = x.ExpenseTotal,
            })
            .ToListAsync();

        return result;
    }
}
