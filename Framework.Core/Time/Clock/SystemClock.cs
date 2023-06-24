namespace Framework.Domain;

public class SystemClock : IClock
{
    public static IClock Instance = new SystemClock();
    private SystemClock() { }
    public DateTime Now() => DateTime.Now;
    public DateOnly DateNow() => DateOnly.FromDateTime(DateTime.Now);
   
}