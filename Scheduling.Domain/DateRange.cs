using Framework.Domain;

namespace Scheduling.Domain;

public class DateRange : ValueObject//Range<DateOnly>
{
    private DateRange()
    {
        
    }
    public DateRange(DateOnly minimum, DateOnly maximum)
    {
        Minimum = minimum;
        Maximum = maximum;
    }
    public DateOnly Minimum { get; private set; }
    public DateOnly Maximum { get; private set; }

    public bool IsOverlapWith(DateRange range)
    {
        return Minimum.CompareTo(range.Maximum) < 0 &&
               range.Minimum.CompareTo(Maximum) < 0;
    }
    public IEnumerable<DateOnly> EachDay()
    {
        for (var day = Minimum; day <= Maximum; day = day.AddDays(1))
            yield return day;
    }

    public bool IsBetween(DateOnly date) =>
        Minimum <= date && Maximum >= date;

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Minimum;
        yield return Maximum;
    }
}
