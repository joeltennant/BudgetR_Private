namespace BudgetR.Core.Extensions;
public static class CommonExtensions
{
    public static bool IsPopulated<T>(this IEnumerable<T> list)
    {
        return list != null && list.Any();
    }

    public static bool IsNotPopulated<T>(this IEnumerable<T> list)
    {
        return !list.IsPopulated();
    }

    public static bool IsNullOrWhiteSpace(this string? value)
    {
        return string.IsNullOrWhiteSpace(value);
    }

    public static bool IsNullOrZero(this long? value)
    {
        return value == null || value == 0;
    }

    public static bool IsFalse(this bool value)
    {
        return !value;
    }
}
