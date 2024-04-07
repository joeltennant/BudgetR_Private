namespace BudgetR.Core;
public static class AppConstants
{
    public enum ExpenseType
    {
        Rent = 0,
        Mortgage = 1,
        Utilities = 2,
        Groceries = 3,
        Gas = 4,
        Insurance = 5,
        Entertainment = 6,
        Other = 7
    }

    public static class AccountTypes
    {
        public const long Checking = 1;
        public const long Savings = 2;
        public const long CreditCard = 3;
        public const long Cash = 4;
        public const long Investment = 5;
        public const long Retirement = 8;
        public const long Loan = 6;
        public const long Other = 7;
    }

    public enum IncomeType
    {
        Paycheck,
        Interest,
        Dividend,
        Other
    }

    public static class Routes
    {
        public const string Dashboard = "/";
    }
}
