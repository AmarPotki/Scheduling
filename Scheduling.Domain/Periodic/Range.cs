using Scheduling.Domain.Periodic.Exceptions;

namespace Scheduling.Domain.Periodic;

public class Range<T> where T : IComparable<T>
{
    public Range(T minimum, T maximum)
    {
        if (minimum.CompareTo(maximum) > 0)
            throw new InvalidRangeException();

        Minimum = minimum;
        Maximum = maximum;
    }

    public T Minimum { get; private set; }
    public T Maximum { get; private set; }

    public bool IsOverlapWith(Range<T> range)
    {
        return Minimum.CompareTo(range.Maximum) < 0 &&
            range.Minimum.CompareTo(Maximum) < 0;
    }
}
