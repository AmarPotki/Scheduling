using Framework.Domain;

namespace Scheduling.Domain;

public class TimeRange :ValueObject //Range<TimeOnly>
{
    public TimeOnly Minimum { get; }
    public TimeOnly Maximum { get; }

    public TimeRange(TimeOnly minimum, TimeOnly maximum)
    {
        Minimum = minimum;
        Maximum = maximum;
    }
    public bool IsOverlapWith(TimeRange range)
    {
        return Minimum.CompareTo(range.Maximum) < 0 &&
               range.Minimum.CompareTo(Maximum) < 0;
    }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Minimum;
        yield return Maximum;
    }
}