using Framework.Domain;
using Scheduling.Domain.Exceptions;

namespace Scheduling.Domain;

public class Range<T> where T : ValueObject,IComparable<T>
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
