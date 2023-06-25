using Framework.Domain;

namespace Scheduling.Domain;

public class DateRange : ValueObject//Range<DateOnly>
{
    private DateRange()
    {
        
    }
    public DateRange(DateOnly beginDate, DateOnly endDate)
    {
        BeginDate = beginDate;
        EndDate = endDate;
    }
    public DateOnly BeginDate { get; private set; }
    public DateOnly EndDate { get; private set; }

    public bool IsOverlapWith(DateRange range)
    {
        return BeginDate.CompareTo(range.EndDate) < 0 &&
               range.BeginDate.CompareTo(EndDate) < 0;
    }
    public IEnumerable<DateOnly> EachDay()
    {
        for (var day = BeginDate; day <= EndDate; day = day.AddDays(1))
            yield return day;
    }

    public bool IsBetween(DateOnly date) =>
        BeginDate <= date && EndDate >= date;

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return BeginDate;
        yield return EndDate;
    }
}
