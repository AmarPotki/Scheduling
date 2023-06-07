namespace Scheduling.Domain.Periodic;

public class DateRange : Range<DateOnly>
{
    public DateRange(DateOnly minimum, DateOnly maximum) : base(minimum, maximum)
    {
    }

    public IEnumerable<DateOnly> EachDay()
    {
        for (var day = Minimum; day <= Maximum; day = day.AddDays(1))
            yield return day;
    }

    public bool IsBetween(DateOnly date) =>
        Minimum <= date && Maximum >= date;
}
