// See https://aka.ms/new-console-template for more information

using BudgetR.Core;
using BudgetR.Server.Infrastructure.Data.BudgetR;
using BudgetR.Server.Services;
using BudgetR.Server.Services.AccountGenerator;
using BudgetR.Server.Services.Transactions;
using Microsoft.EntityFrameworkCore;

string ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=BudgetR;Trusted_Connection=True;MultipleActiveResultSets=true";
var context = new BudgetRDbContext(
            new DbContextOptionsBuilder<BudgetRDbContext>()
                .UseSqlServer(ConnectionString)
                .Options);

//CONFIGURATION
//Always set up your configuration here before running the program

Console.WriteLine("Hello! Choose your action");

Console.WriteLine("1. Load and Process");
Console.WriteLine("2. ReProcess Transactions");
Console.WriteLine("3. Update Months");
Console.WriteLine("4. Seed Account");
Console.WriteLine("");
Console.WriteLine("Enter Number: ");

StateContainer stateContainer = new()
{
    UserId = 1,//system user
    HouseholdId = 1
};

long householdId = stateContainer.HouseholdId.Value;

var response = Console.ReadLine();

if (response == "1")
{
    new UpdateMonthsService(context).Execute();

    var TransactionService = new TransactionService(context, stateContainer);
    await TransactionService.LoadAndProcessTransactions();
}
else if (response == "2")
{
    new UpdateMonthsService(context).Execute();

}
else if (response == "3")
{
    new UpdateMonthsService(context).Execute();
}
else if (response == "4")
{
    new UpdateMonthsService(context).Execute();
    await new BuildAccountsFromTransactions(context).Build(householdId);
    await new BuildCategoriesFromTransactions(context).Build(householdId);
}
else
{
    Console.WriteLine("Invalid response");
}
