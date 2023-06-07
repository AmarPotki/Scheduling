namespace Scheduling.Domain.Periodic;

public class Slot
{
    public Slot(DateOnly date, TimeRage rangeOfSlot)
    {
        RangeOfSlot = rangeOfSlot;
        Date = date;
    }

    public DateOnly Date { get; private set; }
    public TimeRage RangeOfSlot { get; private set; }

}