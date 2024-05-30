namespace BudgetR.RegressionTests.Comparers;
public class BudgetMonthComparer
{
    public List<BudgetMonth>? BeforeTest { get; set; }
    public List<BudgetMonth>? AfterTest { get; set; }

    public BudgetMonthComparer()
    {
        BeforeTest = new List<BudgetMonth>();
        AfterTest = new List<BudgetMonth>();
    }

    //with before and after test data methods
    public BudgetMonthComparer WithBeforeTest(List<BudgetMonth> beforeTest)
    {
        BeforeTest = beforeTest;
        return this;
    }
    //with after test data methods
    public BudgetMonthComparer WithAfterTest(List<BudgetMonth> afterTest)
    {
        AfterTest = afterTest;
        return this;
    }

    //verify new amount added
    public bool NewAmountTotalMatches(decimal amount)
    {
        foreach (var before in BeforeTest)
        {
            var afterTestMonth = AfterTest.FirstOrDefault(a => a.MonthYearId == before.MonthYearId);
            if (before.ExpenseTotal + amount != afterTestMonth.ExpenseTotal)
            {
                return false;
            }
        }

        return true;
    }
}
