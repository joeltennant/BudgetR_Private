namespace BudgetR.Core.Models;
public class MonthSelection
{
    public MonthSelection()
    {
        SelectedMonths = new List<long>();
        Duration = 2;
        StartMonth = DateTime.Now.Month;
        StartYear = DateTime.Now.Year;
    }
    public IList<long>? SelectedMonths { get; set; }

    public int? StartMonth { get; set; }
    public int? StartYear { get; set; }
    public int Duration { get; set; }

    public SelectionType SelectionType { get; set; }

    public string? StartingMonth
    {
        get
        {
            DateTime now = DateTime.Now;
            return $"{now.Month}/{now.Year}";
        }
    }
}

public enum SelectionType
{
    All,
    Selected,
    Once,
    Monthly,
    Yearly,
    EveryOtherMonth,
    EveryThreeMonths
}
