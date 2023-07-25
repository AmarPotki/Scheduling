using ErrorOr;
using Framework.Domain;

namespace Scheduling.Domain;
public class TimeRange : ValueObject //Range<TimeOnly>
{
    private TimeRange()
    {

    }
    public TimeOnly StartTime { get; }
    public TimeOnly EndTime { get; }

    private TimeRange(TimeOnly startTime, TimeOnly endTime)
    {
        StartTime = startTime;
        EndTime = endTime;
    }
    public static ErrorOr<TimeRange> Create(TimeOnly beginDate, TimeOnly endDate)
    {
        if (beginDate > endDate) return Error.Validation("Invalid Date");
        return new TimeRange(beginDate, endDate);
    }
    public bool IsOverlapWith(TimeRange range)
    {
        return StartTime.CompareTo(range.EndTime) < 0 &&
               range.StartTime.CompareTo(EndTime) < 0;
    }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return StartTime;
        yield return EndTime;
    }
}