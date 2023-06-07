namespace Scheduling.Domain.Periodic;

public class PeriodicPlan
{
    public PeriodicPlan
        (DateRange rangeOfPlan, TimeRage timeOfPlane,
        List<DayOfWeek> weeklyPlan)
    {
        RangeOfPlane = rangeOfPlan;
        TimeOfPlan = timeOfPlane;
        WeeklyPlan = weeklyPlan;
    }

    public DateRange RangeOfPlane { get; private set; }
    public TimeRage TimeOfPlan { get; private set; }
    public List<DayOfWeek> WeeklyPlan { get; private set; }

    public List<Slot> Calculate(DateRange dateRange)
    {
        return RangeOfPlane.EachDay()
            .Where(dateRange.IsBetween)
            .Where(c => WeeklyPlan.Contains(c.DayOfWeek))
            .Select(c => new Slot(c, TimeOfPlan))
            .ToList();
    }

}
