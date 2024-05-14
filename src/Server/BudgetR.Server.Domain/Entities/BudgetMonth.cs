using Microsoft.EntityFrameworkCore;

namespace BudgetR.Server.Domain.Entities;
public class BudgetMonth
{
    [Key]
    [Column(Order = 0)]
    public long BudgetMonthId { get; set; }

    [Column(Order = 1)]
    public long MonthYearId { get; set; }
    public MonthYear? MonthYear { get; set; }

    [Column(Order = 2)]
    [Precision(19, 2)]
    public decimal IncomeTotal { get; set; }

    [Column(Order = 3)]
    [Precision(19, 2)]
    public decimal ExpenseTotal { get; set; }

    //public IList<Expense>? Expenses { get; set; }

    //public IList<Income>? Incomes { get; set; }

    [Column(Order = 4)]
    public long HouseholdId { get; set; }
    public Household? Household { get; set; }
}
