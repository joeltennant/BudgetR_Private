using BudgetR.Core;
using BudgetR.Server.Application.Handlers.Expenses;
using FluentAssertions;

namespace BudgetR.RegressionTests.Expenses;

[TestClass]
public class CreateTests
{
    [TestMethod]
    public async Task Handle_ValidRequest_ReturnsSuccessResult()
    {
        var _context = new BudgetRDbMockContext().CreateContext();

        // Arrange
        try
        {
            var request = new Create.Request
            {
                Name = "Expense Name",
                Amount = 100.0m,
                BudgetMonths = new long[] { 1, 2, 3 }
            };

            //create new state container object
            var _stateContainer = new StateContainer();

            var handler = new Create.Handler(_context, _stateContainer);
            //handler.SetValidator(validatorMock.Object);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Value.Should().BeOfType<NoValue>();
            result.IsSuccess.Should().BeTrue();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
