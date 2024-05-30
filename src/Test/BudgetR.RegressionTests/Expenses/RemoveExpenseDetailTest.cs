using BudgetR.RegressionTests.Builders;
using BudgetR.RegressionTests.Comparers;
using BudgetR.Server.Application.Handlers.Expenses;
using FluentAssertions;

namespace BudgetR.RegressionTests.Expenses;

[TestClass]
public class RemoveExpenseDetailTest
{
    [TestMethod]
    public async Task RemoveExpenseDetailFromOneMonthTest()
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

            //Now that we've created a new expense, let's remove one of the expense details
            var expenseDetailId = expenseComparer.Expense?.ExpenseDetails.First().ExpenseDetailId;
            var budgetMonthId = expenseComparer.Expense?.ExpenseDetails
                .Where(x => x.ExpenseDetailId == expenseDetailId)
                .Select(x => x.BudgetMonthId)
                .First();

            var budgetMonth = await GetBudgetMonthById(_context, budgetMonthId.Value);

            var removeRequest = new RemoveExpenseDetail.Request(expenseDetailId.Value);

            var removeHandler = new RemoveExpenseDetail.Handler(_context, _stateContainer);
            var Removalresult = await removeHandler.Handle(removeRequest, CancellationToken.None);

            _context.ChangeTracker.Clear();

            var budgetMonthAfterTest = await GetBudgetMonthById(_context, budgetMonthId.Value);
            var expenseDetailAfterTest = await GetExpenseDetailById(_context, expenseDetailId.Value);

            //Asserts
            Removalresult.IsSuccess.Should().BeTrue();
            budgetMonthAfterTest.Should().NotBeNull();
            budgetMonthAfterTest?.ExpenseTotal.Should().Be(budgetMonth?.ExpenseTotal - request.Amount);

            expenseDetailAfterTest.Should().BeNull();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private async Task<ExpenseDetail?> GetExpenseDetailById(BudgetRDbContext context, long value)
    {
        return await context.ExpenseDetails
            .Where(x => x.ExpenseDetailId == value)
            .Select(x => new ExpenseDetail
            {
                ExpenseDetailId = x.ExpenseDetailId,
                BudgetMonthId = x.BudgetMonthId,
                Expense = new Expense
                {
                    Amount = x.Expense.Amount
                }
            })
            .FirstOrDefaultAsync();
    }

    private async Task<BudgetMonth>? GetBudgetMonthById(BudgetRDbContext context, long id)
    {
        return await context.BudgetMonths
            .Where(x => x.BudgetMonthId == id)
            .Select(x => new BudgetMonth
            {
                BudgetMonthId = x.BudgetMonthId,
                IncomeTotal = x.IncomeTotal,
                ExpenseTotal = x.ExpenseTotal,
            })
            .FirstAsync();
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
}
