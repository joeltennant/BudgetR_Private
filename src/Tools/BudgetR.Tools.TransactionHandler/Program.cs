// See https://aka.ms/new-console-template for more information

using BudgetR.Server.Infrastructure.Data.BudgetR;
using Microsoft.EntityFrameworkCore;

string ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=BudgetR.Test.Data;Trusted_Connection=True;MultipleActiveResultSets=true";
var context = new BudgetRDbContext(
            new DbContextOptionsBuilder<BudgetRDbContext>()
                .UseSqlServer(ConnectionString)
                .Options);

//CONFIGURATION
//Always set up your configuration here before running the program

Console.WriteLine("Hello! Choose your action");

Console.WriteLine("1. Load and Process");
Console.WriteLine("2. ReProcess Transactions");

