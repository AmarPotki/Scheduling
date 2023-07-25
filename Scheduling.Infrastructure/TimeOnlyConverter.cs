using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Scheduling.Infrastructure;

public class TimeOnlyConverter : ValueConverter<TimeOnly, TimeSpan>
{
    public TimeOnlyConverter() : base(
        timeOnly => timeOnly.ToTimeSpan(),
        timeSpan => TimeOnly.FromTimeSpan(timeSpan))
    { }
}

public class DayOfWeeksConverter : ValueConverter<HashSet<Domain.DayOfWeek>, string>
{
    public DayOfWeeksConverter() : base(
        dateOnly => string.Join(',', dateOnly.Select(c => c.Id)),
        v => v.Split(",", StringSplitOptions.None)
            .Select(c => Domain.DayOfWeek.From(int.Parse(c))).ToHashSet())
    { }
}