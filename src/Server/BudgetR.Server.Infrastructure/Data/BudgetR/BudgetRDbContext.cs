using BudgetR.Server.Domain.Entities.Transactions;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;

namespace BudgetR.Server.Infrastructure.Data.BudgetR;
public class BudgetRDbContext : DbContext
{
    public BudgetRDbContext()
    {
    }

    public BudgetRDbContext(DbContextOptions<BudgetRDbContext> options) : base(options)
    {
    }

    public bool IsTestMode { get; set; } = false;

    public DbSet<Household> Households { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<BusinessTransactionActivity> BusinessTransactionActivities { get; set; }

    public DbSet<AccountType> AccountTypes { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<MonthYear> MonthYears { get; set; }
    public DbSet<BudgetMonth> BudgetMonths { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<ExpenseDetail> ExpenseDetails { get; set; }
    public DbSet<Income> Incomes { get; set; }
    public DbSet<IncomeDetail> IncomeDetails { get; set; }
    public DbSet<TransactionBatch> TransactionBatches { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<TransactionCategory> TransactionCategories { get; set; }
    public DbSet<TransactionCategoryRule> TransactionCategoryRules { get; set; }
    public DbSet<TransactionTypeRule> TransactionTypeRules { get; set; }
    public DbSet<FlaggedTransaction> FlaggedTransactions { get; set; }
    public DbSet<HouseholdParameter> HouseholdParameters { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            entityType.GetForeignKeys()
                      .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                      .ToList()
                      .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
        }
    }

    public async Task<IDbContextTransaction?> BeginTransactionContext()
    {
        IDbContextTransaction? transaction = this.Database.CurrentTransaction;
        if (!IsTestMode)
        {
            if (transaction is null)
            {
                return await this.Database.BeginTransactionAsync();
            }
            else
            {
                return transaction;
            }
        }
        else
        {
            return null;
        }
    }

    public async Task CommitTransactionContext(IDbContextTransaction? transaction)
    {
        if (transaction is not null)
        {
            await transaction.CommitAsync();
        }
    }
}
