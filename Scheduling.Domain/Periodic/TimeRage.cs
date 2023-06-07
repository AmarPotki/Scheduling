namespace Scheduling.Domain.Periodic;

public class TimeRage : Range<TimeOnly>
{
    public TimeRage(TimeOnly minimum, TimeOnly maximum) : base(minimum, maximum)
    {
    }
}